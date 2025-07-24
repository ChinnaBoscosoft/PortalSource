using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DonorAuditorSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.DonorAuditor).FullName)
            {
                query = GetDonorAuditorSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Donor auditor details.
        /// </summary>
        /// <returns></returns>
        private string GetDonorAuditorSQL()
        {
            string query = "";
            SQLCommand.DonorAuditor sqlCommandId = (SQLCommand.DonorAuditor)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.DonorAuditor.Add:
                    {
                        query = "INSERT INTO MASTER_DONAUD ( " +
                               "NAME, " +
                               "TYPE, " +
                               "PLACE," +
                               "COMPANY_NAME," +
                               "COUNTRY_ID," +
                               "PINCODE," +
                               "PHONE," +
                               "FAX," +
                               "EMAIL," +
                               "IDENTITYKEY," +
                               "URL," +                           
                               "STATE," +
                               "ADDRESS," +
                               "NOTES," +
                               "PAN ) VALUES( " +
                               "?NAME, " +
                               "?TYPE, " +
                               "?PLACE," +
                               "?COMPANY_NAME," +
                               "?COUNTRY_ID," +
                               "?PINCODE," +
                               "?PHONE," +
                               "?FAX," +
                               "?EMAIL," +
                               "?IDENTITYKEY," +
                               "?URL," +
                               "?STATE," +
                               "?ADDRESS," +
                               "?NOTES," +
                               "?PAN)";
                        break;
                    }
                case SQLCommand.DonorAuditor.Update:
                    {
                        query = "UPDATE MASTER_DONAUD SET " +
                                    "NAME = ?NAME, " +
                                    "TYPE =?TYPE, " +
                                    "PLACE=?PLACE, " +
                                    "COMPANY_NAME=?COMPANY_NAME, " +
                                    "COUNTRY_ID=?COUNTRY_ID," +
                                    "PINCODE=?PINCODE," +
                                    "PHONE=?PHONE, " +
                                    "FAX=?FAX, " +
                                    "EMAIL=?EMAIL, " +
                                    "IDENTITYKEY=?IDENTITYKEY, " +
                                    "URL=?URL, " +
                                    "STATE=?STATE, " +
                                    "ADDRESS=?ADDRESS ," +
                                    "NOTES=?NOTES ," +
                                    "PAN=?PAN " +
                                    "WHERE DONAUD_ID=?DONAUD_ID ";
                        break;
                    }
                case SQLCommand.DonorAuditor.Delete:
                    {
                        query = "DELETE FROM MASTER_DONAUD WHERE DONAUD_ID=?DONAUD_ID";
                        break;
                    }
                case SQLCommand.DonorAuditor.Fetch:
                    {
                        query = "SELECT " +
                                "NAME, " +
                                "TYPE, " +
                                "PLACE," +
                                "COMPANY_NAME," +
                                "COUNTRY_ID," +
                                "PINCODE," +
                                "PHONE," +
                                "FAX," +
                                "EMAIL," +
                                "IDENTITYKEY," +
                                "URL," +
                                "STATE," +
                                "ADDRESS, " +
                                "NOTES, " +
                                "PAN " +
                            "FROM " +
                                "MASTER_DONAUD " +
                                " WHERE DONAUD_ID=?DONAUD_ID ";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchDonor:
                    {
                        query = "SELECT " +
                                "DONAUD_ID, " +
                                "NAME, " +
                                "PLACE," +
                                "D.COUNTRY_ID," +
                                "COUNTRY," +
                                "PHONE," +
                                "STATE," +
                                "ADDRESS," +
                                "PINCODE," +
                                "FAX," +
                                "EMAIL," +
                                "URL " +
                            "FROM " +
                                "MASTER_DONAUD D " +
                                "INNER JOIN MASTER_COUNTRY C ON D.COUNTRY_ID=C.COUNTRY_ID " +
                                " WHERE IDENTITYKEY=0" +
                                " ORDER BY NAME ASC";
                        break;
                    }

                case SQLCommand.DonorAuditor.FetchAuditor:
                    {
                        query = "SELECT " +
                                "DONAUD_ID, " +
                                "NAME, " +
                                "PLACE," +
                                "COMPANY_NAME," +
                                "COUNTRY," +
                                "PHONE," +
                                "STATE," +
                                "ADDRESS," +
                                "PINCODE," +
                                "FAX," +
                                "EMAIL," +
                                "URL " +
                            "FROM " +
                                "MASTER_DONAUD A " +
                                " INNER JOIN MASTER_COUNTRY C ON A.COUNTRY_ID=C.COUNTRY_ID " +
                                " WHERE IDENTITYKEY=1 " +
                                " ORDER BY NAME ASC";
                        break;
                    }

                case SQLCommand.DonorAuditor.FetchAuditorList:
                    {
                        query = "SELECT DONAUD_ID, NAME, IDENTITYKEY\n" +
                                "  FROM MASTER_DONAUD\n" +
                                " WHERE IDENTITYKEY = 1\n" +
                                "ORDER BY NAME ASC";
                        ;
                        break;
                    }

            }
            return query;
        }
        #endregion Bank SQL
    }
}
