using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class LegalEntitySQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string Query = string.Empty;
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.LegalEntity).FullName)
            {
                Query = VoucherSQLManipulition();
            }

            sqlType = this.sqlType;
            return Query;
        }
        #endregion

        #region SQL Script
        private string VoucherSQLManipulition()
        {
            string Query = string.Empty;
            SQLCommand.LegalEntity sqlQueryType = (SQLCommand.LegalEntity)(this.dataCommandArgs.SQLCommandId);
            switch (sqlQueryType)
            {
                #region Insert Query
                case SQLCommand.LegalEntity.Add:
                    {
                        Query = @"INSERT INTO master_insti_perference(SOCIETYNAME, CONTACTPERSON, ADDRESS, PLACE, STATE, COUNTRY, PINCODE, PHONE, FAX, EMAIL,
                                URL, REGNO, REGDATE, PERMISSIONNO, PERMISSIONDATE, A12NO, PANNO, GIRNO, TANNO,FCRINO,FCRIREGDATE,EIGHTYGNO,EIGHTY_GNO_REG_DATE, 
                                ASSOCIATIONNATURE,OTHER_ASSOCIATION_NATURE, DENOMINATION,OTHER_DENOMINATION,IS_FOUNDATION,PRINCIPAL_ACTIVITY)
                                VALUES(?SOCIETYNAME, ?CONTACTPERSON, ?ADDRESS, ?PLACE,?STATE, ?COUNTRY, ?PINCODE, ?PHONE, ?FAX, ?EMAIL,
                                ?URL, ?REGNO, ?REGDATE,?PERMISSIONNO, ?PERMISSIONDATE, ?A12NO, ?PANNO, ?GIRNO, ?TANNO,?FCRINO,
                                ?FCRIREGDATE,?EIGHTYGNO,?EIGHTY_GNO_REG_DATE,?ASSOCIATIONNATURE,?OTHER_ASSOCIATION_NATURE, ?DENOMINATION,?OTHER_DENOMINATION,?IS_FOUNDATION,?PRINCIPAL_ACTIVITY);";

                        break;
                    }
                #endregion

                #region Updat Query
                case SQLCommand.LegalEntity.Update:
                    {
                        Query = @"UPDATE master_insti_perference
                                         SET                                            
                                            SOCIETYNAME=?SOCIETYNAME,
                                            CONTACTPERSON=?CONTACTPERSON,
                                            ADDRESS=?ADDRESS,
                                            PLACE=?PLACE,
                                            STATE=?STATE,
                                            COUNTRY=?COUNTRY,
                                            PINCODE=?PINCODE,
                                            PHONE=?PHONE,
                                            FAX=?FAX,
                                            EMAIL=?EMAIL,
                                            URL=?URL,
                                            REGNO=?REGNO,
                                            REGDATE=?REGDATE,
                                            PERMISSIONNO=?PERMISSIONNO,
                                            PERMISSIONDATE=?PERMISSIONDATE,
                                            A12NO=?A12NO,
                                            PANNO=?PANNO,
                                            GIRNO=?GIRNO,
                                            IS_FOUNDATION=?IS_FOUNDATION,
                                            PRINCIPAL_ACTIVITY=?PRINCIPAL_ACTIVITY,
                                            TANNO=?TANNO,
                                            FCRINO=?FCRINO,
                                            FCRIREGDATE=?FCRIREGDATE,
                                            EIGHTYGNO=?EIGHTYGNO,
                                            EIGHTY_GNO_REG_DATE=?EIGHTY_GNO_REG_DATE,
                                            ASSOCIATIONNATURE=?ASSOCIATIONNATURE,
                                            OTHER_ASSOCIATION_NATURE=?OTHER_ASSOCIATION_NATURE,
                                            DENOMINATION=?DENOMINATION,
                                            OTHER_DENOMINATION=?OTHER_DENOMINATION
                                        WHERE CUSTOMERID=?CUSTOMERID;";
                        break;
                    }
                #endregion

                #region Delete Query
                case SQLCommand.LegalEntity.Delete:
                    {
                        Query = "DELETE FROM master_insti_perference WHERE CUSTOMERID=?CUSTOMERID;";
                        break;
                    }
                #endregion

                #region Fetch Query
                case SQLCommand.LegalEntity.FetchAll:
                    {
                        //  Query = @"SELECT CUSTOMERID, SOCIETYNAME AS 'Society Name',SOCIETYNAME AS SOCIETY_FILTER, REGNO AS 'Reg No',
                        //  DATE_FORMAT(REGDATE,'%d/%m/%Y') AS 'Reg Date',0 AS 'SELECT' FROM master_insti_perference ORDER BY SOCIETYNAME ASC ;";

                        Query = @"SELECT MIP.CUSTOMERID, MIP.SOCIETYNAME AS 'Society Name',MIP.SOCIETYNAME AS SOCIETY_FILTER, MIP.REGNO AS 'Reg No',
                        DATE_FORMAT(MIP.REGDATE,'%d/%m/%Y') AS 'Reg Date',0 AS 'SELECT' FROM MASTER_INSTI_PERFERENCE MIP
                        LEFT JOIN MASTER_PROJECT MP ON MIP.CUSTOMERID=MP.CUSTOMERID
                        LEFT JOIN PROJECT_BRANCH PB ON MP.PROJECT_ID = PB.PROJECT_ID
                        LEFT JOIN BRANCH_LOCATION BL ON BL.LOCATION_ID = PB.LOCATION_ID
                        LEFT JOIN BRANCH_OFFICE BO ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID
                        { WHERE PB.BRANCH_ID =?BRANCH_OFFICE_ID } GROUP BY MIP.SOCIETYNAME ORDER BY MIP.SOCIETYNAME ASC;";

                        break;
                    }
                case SQLCommand.LegalEntity.FetchLegalEntityByBranch:
                    {
                        Query = "SELECT MLE.CUSTOMERID, MLE.SOCIETYNAME AS 'Society Name',MLE.SOCIETYNAME AS SOCIETY_FILTER,0 AS 'SELECT' FROM master_insti_perference MLE " +
                                " { LEFT JOIN MASTER_PROJECT MRP ON MRP.CUSTOMERID=MLE.CUSTOMERID " +
                                "LEFT JOIN PROJECT_BRANCH PB ON MRP.PROJECT_ID=PB.PROJECT_ID " +
                                "WHERE PB.BRANCH_ID IN (?BRANCH_OFFICE_ID) GROUP BY MRP.CUSTOMERID } ";
                        break;
                    }
                case SQLCommand.LegalEntity.FetchBranchAttachedSociety:
                    {
                        Query = "SELECT MLE.CUSTOMERID, MLE.SOCIETYNAME AS 'Society Name',MLE.SOCIETYNAME AS SOCIETY_FILTER,0 AS 'SELECT' FROM master_insti_perference MLE " +
                                "INNER JOIN MASTER_PROJECT MRP ON MRP.CUSTOMERID=MLE.CUSTOMERID " +
                                "INNER JOIN PROJECT_BRANCH PB ON MRP.PROJECT_ID=PB.PROJECT_ID " +
                                "GROUP BY MRP.CUSTOMERID";
                        break;
                    }
                case SQLCommand.LegalEntity.FetchSocieties:
                    {
                        Query = @"SELECT CUSTOMERID,SOCIETYNAME,REGNO,
                                DATE_FORMAT(REGDATE,'%d/%m/%Y') AS 'Reg Date' FROM master_insti_perference;";
                        break;
                    }
                case SQLCommand.LegalEntity.FetchByID:
                    {
                        Query = "SELECT CUSTOMERID,\n" +
                                "       SOCIETYNAME,\n" +
                                "       CONTACTPERSON,\n" +
                                "       ADDRESS,\n" +
                                "       PLACE,\n" +
                                "       STATE,\n" +
                                "       COUNTRY,\n" +
                                "       PINCODE,\n" +
                                "       PHONE,\n" +
                                "       FAX,\n" +
                                "       EMAIL,\n" +
                                "       URL,\n" +
                                "       REGNO,\n" +
                                "       REGDATE,\n" +
                                "       PERMISSIONNO,\n" +
                                "       PERMISSIONDATE,\n" +
                                "       A12NO,\n" +
                                "       PANNO,\n" +
                                "       GIRNO,\n" +
                                "       TANNO,\n" +
                                "       ASSOCIATIONNATURE,\n" +
                                "       OTHER_ASSOCIATION_NATURE,\n" +
                                "       DENOMINATION,\n" +
                                "       OTHER_DENOMINATION,\n" +
                                "       FCRINO,\n" +
                                "       FCRIREGDATE,\n" +
                                "       EIGHTYGNO, EIGHTY_GNO_REG_DATE, \n" +
                                "       PRINCIPAL_ACTIVITY,\n" +
                                "       IS_FOUNDATION\n" +
                                "  FROM MASTER_INSTI_PERFERENCE\n" +
                                " WHERE CUSTOMERID = ?CUSTOMERID;";
                        break;
                    }

                case SQLCommand.LegalEntity.LegalEntityFetchAll:
                    {
                        Query = "SELECT SOCIETYNAME, CONTACTPERSON, ADDRESS, PLACE, STATE, " +
                               "COUNTRY, PINCODE, PHONE, FAX, EMAIL, URL, REGNO, REGDATE, PERMISSIONNO, PERMISSIONDATE, " +
                               "A12NO, PANNO, GIRNO, TANNO, ASSOCIATIONNATURE, DENOMINATION,IS_FOUNDATION,PRINCIPAL_ACTIVITY FROM MASTER_INSTI_PERFERENCE MLE " +
                               "LEFT JOIN MASTER_PROJECT MRP ON MRP.CUSTOMERID=MLE.CUSTOMERID " +
                               "LEFT JOIN PROJECT_BRANCH PB ON MRP.PROJECT_ID=PB.PROJECT_ID " +
                               "WHERE PB.BRANCH_ID IN (SELECT BRANCH_OFFICE_ID FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE) " +
                               "GROUP BY MRP.CUSTOMERID";
                        break;
                    }
                case SQLCommand.LegalEntity.CheckLegalEntity:
                    {
                        Query = "SELECT MI.CUSTOMERID\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " LEFT JOIN MASTER_INSTI_PERFERENCE MI\n" +
                                "    ON MI.CUSTOMERID = MP.CUSTOMERID\n" +
                                " WHERE MP.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " GROUP BY MP.CUSTOMERID;";
                        break;
                    }
                case SQLCommand.LegalEntity.FetchSocietyByProject:
                    {
                        Query = "SELECT MI.CUSTOMERID, MI.SOCIETYNAME, MI.ADDRESS\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " LEFT JOIN MASTER_INSTI_PERFERENCE MI\n" +
                                "    ON MI.CUSTOMERID = MP.CUSTOMERID\n" +
                                " WHERE MP.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " GROUP BY MP.CUSTOMERID;";
                        break;
                    }
                case SQLCommand.LegalEntity.LegalEntityCount:
                    {
                        Query = "SELECT COUNT(*) FROM MASTER_INSTI_PERFERENCE";
                        break;
                    }
                case SQLCommand.LegalEntity.FetchCustomerIdyBySocietyName:
                    {
                        Query = @"SELECT CUSTOMERID,SOCIETYNAME FROM master_insti_perference WHERE SOCIETYNAME=?SOCIETYNAME";
                        break;

                    }
                #endregion
            }
            return Query;
        }
        #endregion
    }
}
