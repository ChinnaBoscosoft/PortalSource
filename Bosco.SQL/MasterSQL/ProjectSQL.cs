using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class ProjectSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Project).FullName)
            {
                query = GetProjectSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Project details.
        /// </summary>
        /// <returns></returns>
        private string GetProjectSQL()
        {
            string query = "";
            SQLCommand.Project sqlCommandId = (SQLCommand.Project)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Project.Add:
                    {
                        query = "INSERT INTO MASTER_PROJECT ( " +
                               "PROJECT_CODE, " +
                               "CUSTOMERID, " +
                               "PROJECT, " +
                               "DIVISION_ID, " +
                               "ACCOUNT_DATE," +
                               "DATE_STARTED," +
                               "DATE_CLOSED," +
                               "DESCRIPTION," +
                               "NOTES, " +
                               "PROJECT_CATEGORY_ID, CLOSED_BY) VALUES( " +
                               "?PROJECT_CODE, " +
                               "?CUSTOMERID, " +
                               "?PROJECT, " +
                               "?DIVISION_ID, " +
                               "?ACCOUNT_DATE," +
                               "?DATE_STARTED," +
                               "?DATE_CLOSED," +
                               "?DESCRIPTION," +
                               "?NOTES, " +
                               "?PROJECT_CATEGORY_ID, ?CLOSED_BY)";
                        break;
                    }
                case SQLCommand.Project.Update:
                    {
                        query = "UPDATE MASTER_PROJECT SET " +
                                    "PROJECT_CODE = ?PROJECT_CODE, " +
                                    "CUSTOMERID = ?CUSTOMERID, " +
                                    "PROJECT =?PROJECT, " +
                                    "DIVISION_ID =?DIVISION_ID, " +
                                    "ACCOUNT_DATE=?ACCOUNT_DATE, " +
                                    "DATE_STARTED=?DATE_STARTED," +
                                    "DATE_CLOSED=?DATE_CLOSED," +
                                    "DESCRIPTION=?DESCRIPTION ," +
                                    "NOTES=?NOTES ," +
                                    "PROJECT_CATEGORY_ID =?PROJECT_CATEGORY_ID, CLOSED_BY=?CLOSED_BY " +
                                    " WHERE PROJECT_ID=?PROJECT_ID ";
                        break;
                    }
                case SQLCommand.Project.UpdateClosedDate:
                    {
                        query = "UPDATE MASTER_PROJECT SET DATE_CLOSED=?DATE_CLOSED WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.Delete:
                    {
                        query = "DELETE FROM MASTER_PROJECT WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.Fetch:
                    {
                        query = "SELECT " +
                                "PROJECT_ID," +
                                "PROJECT_CODE, " +
                                "PROJECT, " +
                                "MD.DIVISION_ID," +
                                "MD.DIVISION, " +
                                "ACCOUNT_DATE, " +
                                "DATE_STARTED, " +
                                "DATE_CLOSED, " +
                                "DESCRIPTION ," +
                                "NOTES ," +
                                "CUSTOMERID ," +
                                "PROJECT_CATEGORY_ID " +
                            " FROM " +
                                " MASTER_PROJECT MP " +
                                " INNER JOIN MASTER_DIVISION MD ON " +
                                " MP.DIVISION_ID=MD.DIVISION_ID " +
                                " WHERE PROJECT_ID=?PROJECT_ID AND DELETE_FLAG<>1 ";
                        break;
                    }
                case SQLCommand.Project.FetchVoucherTypes:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER WHERE VOUCHER_NAME IS NOT NULL GROUP BY VOUCHER_TYPE ";
                        break;
                    }
                case SQLCommand.Project.FetchDivision:
                    {
                        query = "SELECT DIVISION_ID, DIVISION FROM MASTER_DIVISION WHERE DIVISION <>'' AND DIVISION IS NOT NULL ";
                        break;
                    }

                case SQLCommand.Project.FetchAll:
                    {
                        query = "SELECT MP.PROJECT_ID, MP.PROJECT_CODE AS 'Code',MP.PROJECT AS 'Name',MIP.SOCIETYNAME AS Society,MD.DIVISION AS Division,PJC.PROJECT_CATOGORY_NAME AS 'Category'," +
                                " DATE_FORMAT(DATE_STARTED,'%d/%m/%Y') AS 'Start Date',DATE_FORMAT(DATE_CLOSED,'%d/%m/%Y') AS 'Close Date',DESCRIPTION AS 'Description' " +
                                " FROM MASTER_PROJECT MP" +
                                " INNER JOIN MASTER_DIVISION MD ON" +
                                " MP.DIVISION_ID=MD.DIVISION_ID " +
                                " INNER JOIN MASTER_PROJECT_CATOGORY PJC " +
                                " ON PJC.PROJECT_CATOGORY_ID=MP.PROJECT_CATEGORY_ID " +
                                " INNER JOIN MASTER_INSTI_PERFERENCE MIP " +
                                " ON MIP.CUSTOMERID=MP.CUSTOMERID " +
                                " { INNER JOIN PROJECT_BRANCH PB " +
                                " ON MP.PROJECT_ID = PB.PROJECT_ID " +
                                " AND PB.BRANCH_ID =?BRANCH_OFFICE_ID } " +
                                " WHERE DELETE_FLAG<>1 ORDER BY MP.PROJECT_CODE ASC";
                        break;
                    }
                case SQLCommand.Project.FetchAllWithBranch:
                    {
                        query = "SELECT MP.PROJECT_ID, MP.PROJECT_CODE AS 'Code',MP.PROJECT AS 'Name',MIP.SOCIETYNAME AS Society,MD.DIVISION AS Division,PJC.PROJECT_CATOGORY_NAME AS 'Category'," +
                                " DATE_FORMAT(DATE_STARTED,'%d/%m/%Y') AS 'Start Date',DATE_FORMAT(DATE_CLOSED,'%d/%m/%Y') AS 'Close Date'," +
                                " GROUP_CONCAT(BO.BRANCH_OFFICE_NAME SEPARATOR '; ') AS Branch,BL.LOCATION_NAME AS Location,DESCRIPTION AS 'Description'" +
                                " FROM MASTER_PROJECT MP " +
                                " INNER JOIN MASTER_DIVISION MD ON MP.DIVISION_ID=MD.DIVISION_ID " +
                                " INNER JOIN MASTER_PROJECT_CATOGORY PJC ON PJC.PROJECT_CATOGORY_ID=MP.PROJECT_CATEGORY_ID " +
                                " INNER JOIN MASTER_INSTI_PERFERENCE MIP ON MIP.CUSTOMERID=MP.CUSTOMERID " +
                                " LEFT JOIN PROJECT_BRANCH PB ON MP.PROJECT_ID = PB.PROJECT_ID  " +
                                " LEFT JOIN BRANCH_LOCATION BL ON BL.LOCATION_ID = PB.LOCATION_ID " +
                                " LEFT JOIN BRANCH_OFFICE BO ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID " +
                                " WHERE DELETE_FLAG<>1 { AND PB.BRANCH_ID =?BRANCH_OFFICE_ID } GROUP BY MP.PROJECT_ID ORDER BY MP.PROJECT_CODE ASC";
                        break;
                    }
                case SQLCommand.Project.FetchVouchers:
                    {
                        query = "SELECT MV.VOUCHER_ID, MP.PROJECT_ID, VOUCHER_NAME ,CASE  VOUCHER_TYPE WHEN 1 THEN 'Receipts' " +
                                  " WHEN 2 THEN 'Payments'" +
                                  " WHEN 3 THEN 'Contra' " +
                                  " ELSE 'Journal' END AS VOUCHER_TYPE, " +
                                  " CASE VOUCHER_METHOD WHEN 1 THEN 'Automatic' " +
                                  " ELSE 'Manual' END AS VOUCHER_METHOD,PREFIX_CHAR ,SUFFIX_CHAR ,MONTH  " +
                                  " FROM MASTER_VOUCHER MV " +
                                  " INNER JOIN PROJECT_VOUCHER MPV  ON " +
                                  " MV.VOUCHER_ID =MPV.VOUCHER_ID " +
                                  " INNER JOIN MASTER_PROJECT MP ON " +
                                  " MPV.PROJECT_ID=MP.PROJECT_ID WHERE MP.DELETE_FLAG<>1 ORDER BY VOUCHER_TYPE ASC;";
                        break;
                    }
                case SQLCommand.Project.AddProjectVouchers:
                    {
                        query = "INSERT INTO PROJECT_VOUCHER ( " +
                                "PROJECT_ID," +
                                "VOUCHER_ID) VALUES( " +
                                "?PROJECT_ID," +
                                "?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.AvailableVoucher:
                    {
                        query = " SELECT MV.VOUCHER_ID, VOUCHER_NAME ,VOUCHER_TYPE" +
                                " FROM MASTER_VOUCHER MV " +
                                " WHERE VOUCHER_ID NOT IN( " +
                                " SELECT VOUCHER_ID FROM PROJECT_VOUCHER MPV " +
                                " WHERE MV.VOUCHER_ID =MPV.VOUCHER_ID " +
                                " AND MPV.PROJECT_ID=?PROJECT_ID) ORDER BY VOUCHER_TYPE ASC";
                        break;
                    }
                case SQLCommand.Project.ProjectVoucher:
                    {
                        query = " SELECT MV.VOUCHER_ID, VOUCHER_NAME,VOUCHER_TYPE " +
                                " FROM MASTER_VOUCHER MV" +
                                " INNER JOIN PROJECT_VOUCHER MPV ON " +
                                " MV.VOUCHER_ID = MPV.VOUCHER_ID" +
                                " WHERE MPV.PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
               
                case SQLCommand.Project.DeleteProjectVouchers:
                    {
                        query = "DELETE FROM PROJECT_VOUCHER WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Project.FetchProjectList:
                    {
                        query = "SELECT " +
                                "PROJECT_ID, " +
                                "PROJECT " +
                                "FROM " +
                                "MASTER_PROJECT WHERE MP.DELETE_FLAG<>1 ORDER BY PROJECT ASC;";
                        break;
                    }
                case SQLCommand.Project.FetchLedgers:
                    {
                        query = "SELECT MP.PROJECT_ID, ML.LEDGER_ID, LEDGER_CODE,LEDGER_NAME, GROUP_CODE,LEDGER_GROUP " +
                                " FROM MASTER_LEDGER ML " +
                                " INNER JOIN  MASTER_LEDGER_GROUP MLG ON ML.GROUP_ID=MLG.GROUP_ID " +
                                " INNER JOIN  PROJECT_LEDGER PLM ON ML.LEDGER_ID=PLM.LEDGER_ID " +
                                " INNER JOIN MASTER_PROJECT MP ON PLM.PROJECT_ID=MP.PROJECT_ID WHERE MP.DELETE_FLAG<>1";
                        break;
                    }

                case SQLCommand.Project.ProjectCategory:
                    {
                        query = " SELECT PROJECT_CATOGORY_ID ,PROJECT_CATOGORY_NAME,0 AS 'SELECT' " +
                                " FROM MASTER_PROJECT_CATOGORY" +
                                " WHERE  PROJECT_CATOGORY_NAME IS NOT NULL " +
                                " ORDER BY PROJECT_CATOGORY_NAME ASC ";
                        break;
                    }
                case SQLCommand.Project.LoadAllLedgerByProjectId:
                    {
                        query = @"SELECT ML.LEDGER_ID,IF(AMOUNT IS NULL,0.00,AMOUNT) AS AMOUNT,LEDGER_CODE,LEDGER_NAME,ML.SORT_ID,
                                  CASE WHEN LEDGER_SUB_TYPE = 'BK' THEN 'Bank Accounts'
                                  ELSE
                                  CASE WHEN LEDGER_SUB_TYPE = 'FD' THEN 'Fixed Deposit'
                                  ELSE
                                  LEDGER_GROUP
                                  END
                                  END AS 'TYPE',
                                  IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                                  CASE
                                  WHEN LEDGER_TYPE = 'GN' THEN 'General'
                                  ELSE
                                  CASE
                                  WHEN LEDGER_TYPE = 'IK' THEN
                                  'In kind'
                                  END
                                  END 'GROUP',
                                  ML.BANK_ACCOUNT_ID
                                  FROM MASTER_LEDGER ML
                                  LEFT JOIN LEDGER_BALANCE LB ON LB.LEDGER_ID=ML.LEDGER_ID AND LB.PROJECT_ID=?PROJECT_ID AND TRANS_FLAG='OP'
                                  LEFT JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID
                                  WHERE STATUS = 0 ORDER BY SORT_ID;";
                        break;
                    }

                case SQLCommand.Project.FetchDefaultVouchers:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER M " +
                                " WHERE VOUCHER_TYPE NOT IN(4) AND VOUCHER_NAME IS NOT NULL " +
                                " GROUP BY VOUCHER_TYPE";
                        break;
                    }
                case SQLCommand.Project.FetchAvailableVouchers:
                    {
                        query = " SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER M " +
                                " WHERE NOT FIND_IN_SET(VOUCHER_ID, ?VOUCHER_ID) AND VOUCHER_NAME IS NOT NULL";
                        break;
                    }

                case SQLCommand.Project.FetchVoucherDetailsByProjectId:
                    {
                        query = "SELECT MV.VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE,VOUCHER_METHOD,PREFIX_CHAR,SUFFIX_CHAR,STARTING_NUMBER,NUMBERICAL_WITH,PREFIX_WITH_ZERO, " +
                               " MONTH,DURATION,ALLOW_DUPLICATE,NOTE FROM MASTER_VOUCHER MV " +
                               " INNER JOIN PROJECT_VOUCHER PV ON MV.VOUCHER_ID=PV.VOUCHER_ID " +
                               " INNER JOIN MASTER_PROJECT MP  ON PV.PROJECT_ID=MP.PROJECT_ID " +
                               " WHERE PV.PROJECT_ID=?PROJECT_ID AND FIND_IN_SET(MV.VOUCHER_TYPE,?VOUCHER_TYPE) AND MP.DELETE_FLAG<>1";
                        break;
                    }
                case SQLCommand.Project.FetchRecentProject:
                    {
                        query = "SELECT VMT.PROJECT_ID, MP.PROJECT " +
                                  " FROM VOUCHER_MASTER_TRANS VMT " +
                                 " INNER JOIN MASTER_PROJECT MP " +
                                    " ON VMT.PROJECT_ID = MP.PROJECT_ID " +
                                    " WHERE MP.DELETE_FLAG<>1 AND VMT.STATUS=1 AND VMT.CREATED_BY=?CREATED_BY" +
                                 " ORDER BY VOUCHER_ID DESC LIMIT 1";
                        break;

                    }
                case SQLCommand.Project.DeleteVoucher:
                    {
                        query = " DELETE FROM PROJECT_VOUCHER " +
                                " WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }

                case SQLCommand.Project.DeleteProject:
                    {
                        query = "DELETE FROM MASTER_PROJECT WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }

                case SQLCommand.Project.FetchProjectCodes:
                    {
                        query = "SELECT PROJECT_CODE\n" +
                                    "  FROM MASTER_PROJECT\n" +
                                    " WHERE DELETE_FLAG = 0\n" +
                                    " ORDER BY PROJECT_ID DESC";
                        break;
                    }

                case SQLCommand.Project.FetchProjectDetails:
                    {
                        query = "SELECT PROJECT_ID, CONCAT(CONCAT(PROJECT, '-'), DIVISION) AS PROJECT\n" +
                                "  FROM MASTER_PROJECT AS MP\n" +
                                " INNER JOIN MASTER_DIVISION MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                " WHERE PROJECT_ID NOT IN (?PROJECT_ID)";
                        break;
                    }
                case SQLCommand.Project.FetchDefaultProjectVouchers:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER WHERE VOUCHER_NAME IS NOT NULL AND VOUCHER_ID NOT IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.Project.FetchSelectedProjectVouchers:
                    {
                        query = "SELECT MV.VOUCHER_ID,MV.VOUCHER_NAME,VOUCHER_TYPE FROM MASTER_VOUCHER MV " +
                                " INNER JOIN PROJECT_VOUCHER AS PV " +
                                " ON MV.VOUCHER_ID=PV.VOUCHER_ID " +
                                " WHERE PV.PROJECT_ID=?PROJECT_ID ORDER BY VOUCHER_TYPE ASC";
                        break;
                    }
                case SQLCommand.Project.ProjectFetchAll:
                    {
                        #region oldquery
                        /*query = "SELECT MPRJ.PROJECT_CODE, MPRJ.PROJECT, MD.DIVISION_ID, MPRJ.ACCOUNT_DATE, MPRJ.DATE_STARTED, " +
                               "MPRJ.DATE_CLOSED, MPRJ.DESCRIPTION, MPRJ.NOTES, MPRJC.PROJECT_CATOGORY_NAME, MPRJ.DELETE_FLAG,MIS.SOCIETYNAME " +
                               "FROM MASTER_PROJECT MPRJ LEFT JOIN MASTER_DIVISION MD ON MPRJ.DIVISION_ID=MD.DIVISION_ID " +
                               "LEFT JOIN  MASTER_INSTI_PERFERENCE MIS ON MIS.CUSTOMERID=MPRJ.CUSTOMERID " +
                               "LEFT JOIN MASTER_PROJECT_CATOGORY MPRJC ON MPRJC.PROJECT_CATOGORY_ID=MPRJ.PROJECT_CATEGORY_ID ";*/
                        #endregion

                        query = "SELECT MPRJ.PROJECT_CODE, MPRJ.PROJECT, MD.DIVISION_ID, MPRJ.ACCOUNT_DATE, MPRJ.DATE_STARTED, " +
                                "MPRJ.DATE_CLOSED, MPRJ.DESCRIPTION, MPRJ.NOTES, MPRJC.PROJECT_CATOGORY_NAME, MPRJ.DELETE_FLAG,MIS.SOCIETYNAME " +
                                "FROM MASTER_PROJECT MPRJ LEFT JOIN MASTER_DIVISION MD ON MPRJ.DIVISION_ID=MD.DIVISION_ID " +
                                "LEFT JOIN  MASTER_INSTI_PERFERENCE MIS ON MIS.CUSTOMERID=MPRJ.CUSTOMERID " +
                                "LEFT JOIN MASTER_PROJECT_CATOGORY MPRJC ON MPRJC.PROJECT_CATOGORY_ID=MPRJ.PROJECT_CATEGORY_ID " +
                                "LEFT JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID=MPRJ.PROJECT_ID WHERE " +
                                "PB.BRANCH_ID IN (SELECT BRANCH_OFFICE_ID FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE)";

                        break;
                    }
                case SQLCommand.Project.FetchProjectByUser:
                    {
                        query = "SELECT MP.PROJECT_ID,\n" +
                        "       MP.PROJECT_CODE AS 'Code',\n" +
                        "       MP.PROJECT AS 'Name',\n" +
                        "       MIP.SOCIETYNAME AS Society,\n" +
                        "       MD.DIVISION AS Division,\n" +
                        "       PJC.PROJECT_CATOGORY_NAME AS 'Category',\n" +
                        "       DATE_FORMAT(DATE_STARTED, '%d/%m/%Y') AS 'Start Date',\n" +
                        "       DATE_FORMAT(DATE_CLOSED, '%d/%m/%Y') AS 'Close Date',\n" +
                        "       DESCRIPTION AS 'Description'\n" +
                        "  FROM MASTER_PROJECT MP\n" +
                        " INNER JOIN MASTER_DIVISION MD\n" +
                        "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        " INNER JOIN MASTER_PROJECT_CATOGORY PJC\n" +
                        "    ON PJC.PROJECT_CATOGORY_ID = MP.PROJECT_CATEGORY_ID\n" +
                        " INNER JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                        "    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                        " INNER JOIN PROJECT_USER PU\n" +
                        "    ON MP.PROJECT_ID = PU.PROJECT_ID\n" +
                        "   AND PU.USER_ID = ?USER_ID\n" +
                        " WHERE DELETE_FLAG <> 1\n" +
                        " ORDER BY MP.PROJECT_CODE ASC";
                        break;
                    }
                case SQLCommand.Project.FetchpProjectByLegalEntity:
                    {
                        query = "SELECT PROJECT_ID, PROJECT_CODE AS 'Project Code',PROJECT AS 'Project',DIVISION AS 'Division', " +
                                "DATE_FORMAT(DATE_STARTED,'%d/%m/%Y') AS 'Start Date',DATE_FORMAT(DATE_CLOSED,'%d/%m/%Y') AS 'Close Date',DESCRIPTION AS 'Description' " +
                                "FROM MASTER_PROJECT MP " +
                                "INNER JOIN MASTER_DIVISION MD ON " +
                                "MP.DIVISION_ID=MD.DIVISION_ID WHERE DELETE_FLAG<>1 { AND MP.CUSTOMERID IN(?CUSTOMERID) } ORDER BY PROJECT ASC";
                        break;
                    }
                case SQLCommand.Project.FetchProjectIdByProjectCategory:
                    {
                        query = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT_CATEGORY_ID=?PROJECT_CATEGORY_ID";
                        break;
                    }
                case SQLCommand.Project.FetchProjectIdsByProjectCategory:
                    {
                        query = "SELECT IFNULL(GROUP_CONCAT(PROJECT_ID),0) AS PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT_CATEGORY_ID IN (?PROJECT_CATEGORY_ID)";
                        break;
                    }
                case SQLCommand.Project.FetchProjectByBranch:
                    {
                        //"       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                        query = "SELECT MP.PROJECT_ID,\n" +
                        "       PB.BRANCH_ID,\n" +
                        "        MP.PROJECT AS 'PROJECT',\n" +
                        "        MIP.SOCIETYNAME, \n" +
                        "       (SELECT ' ') AS TRANS_MODE,\n" +
                        "     {  IF(PB.BRANCH_ID = ?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT',}\n" +
                        "       TOP.AMOUNT AS OPAMOUNT,\n" +
                        "       TCB.AMOUNT AS CLAMOUNT,\n" +
                        "       RC.RC_AMOUNT,\n" +
                        "       PY.PY_AMOUNT\n" +
                        "  FROM MASTER_PROJECT MP\n" +
                        " INNER JOIN MASTER_DIVISION AS MD\n" +
                        "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        " INNER JOIN PROJECT_BRANCH PB\n" +
                        "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                        " {  AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID } \n" +
                        " INNER JOIN MASTER_INSTI_PERFERENCE AS MIP \n" +
                        "    ON MP.CUSTOMERID=MIP.CUSTOMERID \n" +
                        "  LEFT JOIN (SELECT FNL.BRANCH_ID,\n" +
                        "                    FNL.PROJECT_ID,\n" +
                        "                    SUM(FNL.DR) - SUM(FNL.CR) AS AMOUNT\n" +
                        "               FROM (SELECT ML.LEDGER_ID AS 'ID',\n" +
                        "                            LB2.PROJECT_ID,\n" +
                        "                            LB2.BRANCH_ID,\n" +
                        "                            LG.GROUP_ID,\n" +
                        "                            LG.GROUP_CODE,\n" +
                        "                            LG.LEDGER_GROUP,\n" +
                        "                            CASE\n" +
                        "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                        "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                        "                                      CONCAT(MB.BANK, ' - '),\n" +
                        "                                      MB.BRANCH)\n" +
                        "                              ELSE\n" +
                        "                               ML.LEDGER_NAME\n" +
                        "                            END AS 'LEDGER_NAME',\n" +
                        "                            IFNULL(CASE\n" +
                        "                                     WHEN LB2.TRANS_MODE = 'DR' THEN\n" +
                        "                                      LB2.AMOUNT\n" +
                        "                                   END,\n" +
                        "                                   0) AS DR,\n" +
                        "                            IFNULL(ABS(CASE\n" +
                        "                                         WHEN LB2.TRANS_MODE = 'CR' THEN\n" +
                        "                                          -LB2.AMOUNT\n" +
                        "                                       END),\n" +
                        "                                   0) AS CR,\n" +
                        "                            CASE\n" +
                        "                              WHEN (SUM(CASE\n" +
                        "                                          WHEN LB2.TRANS_MODE = 'DR' THEN\n" +
                        "                                           LB2.AMOUNT\n" +
                        "                                          ELSE\n" +
                        "                                           -LB2.AMOUNT\n" +
                        "                                        END) >= 0) THEN\n" +
                        "                               'DR'\n" +
                        "                              ELSE\n" +
                        "                               'CR'\n" +
                        "                            END AS 'TRANSMODE'\n" +
                        "                       FROM MASTER_LEDGER AS ML\n" +
                        "                      INNER JOIN MASTER_LEDGER_GROUP AS LG\n" +
                        "                         ON ML.GROUP_ID = LG.GROUP_ID\n" +
                        "                      INNER JOIN (SELECT LB.BALANCE_DATE,\n" +
                        "                                        LB.PROJECT_ID,\n" +
                        "                                        LB.LEDGER_ID,\n" +
                        "                                        LB.AMOUNT,\n" +
                        "                                        LB.BRANCH_ID,\n" +
                        "                                        LB.TRANS_MODE\n" +
                        "                                   FROM LEDGER_BALANCE AS LB\n" +
                        "                                   LEFT JOIN (SELECT LBA.PROJECT_ID,\n" +
                        "                                                    LBA.LEDGER_ID,\n" +
                        "                                                    MAX(LBA.BALANCE_DATE) AS BAL_DATE\n" +
                        "                                               FROM LEDGER_BALANCE LBA\n" +
                        "                                              WHERE 1 = 1\n" +
                        "                                                AND LBA.BALANCE_DATE <\n" +
                        "                                                    ?BOOKS_BEGINNING_FROM\n" +
                        "                                              GROUP BY LBA.PROJECT_ID,\n" +
                        "                                                       LBA.LEDGER_ID) AS LB1\n" +
                        "                                     ON LB.LEDGER_ID = LB1.LEDGER_ID\n" +
                        "                                  WHERE LB.PROJECT_ID = LB1.PROJECT_ID\n" +
                        "                                 {   AND LB.BRANCH_ID IN (?BRANCH_OFFICE_ID) }\n" +
                        "                                    AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2\n" +
                        "                         ON ML.LEDGER_ID = LB2.LEDGER_ID\n" +
                        "                       LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                        "                         ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        "                       LEFT JOIN MASTER_BANK MB\n" +
                        "                         ON MBA.BANK_ID = MB.BANK_ID\n" +
                        "                      WHERE LG.GROUP_ID IN (12, 13, 14) -- AND LB2.TRANS_MODE=\"CR\"\n" +
                        "                        and ML.STATUS = 0\n" +
                        "                      GROUP BY LG.GROUP_ID,\n" +
                        "                               LG.GROUP_CODE,\n" +
                        "                               LG.LEDGER_GROUP,\n" +
                        "                               ML.LEDGER_NAME,\n" +
                        "                               LB2.PROJECT_ID) AS FNL\n" +
                        "              GROUP BY FNL.BRANCH_ID, FNL.PROJECT_ID) AS TOP\n" +
                        "    ON PB.BRANCH_ID = TOP.BRANCH_ID\n" +
                        "   AND PB.PROJECT_ID = TOP.PROJECT_ID\n" +
                        "  LEFT JOIN (SELECT FNL.BRANCH_ID,\n" +
                        "                    FNL.PROJECT_ID,\n" +
                        "                    SUM(FNL.DR) - SUM(FNL.CR) AS AMOUNT\n" +
                        "               FROM (SELECT ML.LEDGER_ID AS 'ID',\n" +
                        "                            LB2.PROJECT_ID,\n" +
                        "                            LB2.BRANCH_ID,\n" +
                        "                            LG.GROUP_ID,\n" +
                        "                            LG.GROUP_CODE,\n" +
                        "                            LG.LEDGER_GROUP,\n" +
                        "                            CASE\n" +
                        "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                        "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                        "                                      CONCAT(MB.BANK, ' - '),\n" +
                        "                                      MB.BRANCH)\n" +
                        "                              ELSE\n" +
                        "                               ML.LEDGER_NAME\n" +
                        "                            END AS 'LEDGER_NAME',\n" +
                        "                            IFNULL(CASE\n" +
                        "                                     WHEN LB2.TRANS_MODE = 'DR' THEN\n" +
                        "                                      LB2.AMOUNT\n" +
                        "                                   END,\n" +
                        "                                   0) AS DR,\n" +
                        "                            IFNULL(ABS(CASE\n" +
                        "                                         WHEN LB2.TRANS_MODE = 'CR' THEN\n" +
                        "                                          -LB2.AMOUNT\n" +
                        "                                       END),\n" +
                        "                                   0) AS CR,\n" +
                        "                            CASE\n" +
                        "                              WHEN (SUM(CASE\n" +
                        "                                          WHEN LB2.TRANS_MODE = 'DR' THEN\n" +
                        "                                           LB2.AMOUNT\n" +
                        "                                          ELSE\n" +
                        "                                           -LB2.AMOUNT\n" +
                        "                                        END) >= 0) THEN\n" +
                        "                               'DR'\n" +
                        "                              ELSE\n" +
                        "                               'CR'\n" +
                        "                            END AS 'TRANSMODE'\n" +
                        "                       FROM MASTER_LEDGER AS ML\n" +
                        "                      INNER JOIN MASTER_LEDGER_GROUP AS LG\n" +
                        "                         ON ML.GROUP_ID = LG.GROUP_ID\n" +
                        "                      INNER JOIN (SELECT LB.BALANCE_DATE,\n" +
                        "                                        LB.PROJECT_ID,\n" +
                        "                                        LB.LEDGER_ID,\n" +
                        "                                        LB.AMOUNT,\n" +
                        "                                        LB.BRANCH_ID,\n" +
                        "                                        LB.TRANS_MODE\n" +
                        "                                   FROM LEDGER_BALANCE AS LB\n" +
                        "                                   LEFT JOIN (SELECT LBA.PROJECT_ID,\n" +
                        "                                                    LBA.LEDGER_ID,\n" +
                        "                                                    MAX(LBA.BALANCE_DATE) AS BAL_DATE\n" +
                        "                                               FROM LEDGER_BALANCE LBA\n" +
                        "                                              WHERE 1 = 1\n" +
                        "                                                AND LBA.BALANCE_DATE BETWEEN ?BOOKS_BEGINNING_FROM AND \n" +
                        "                                                    ?DATE_CLOSED\n" +
                        "                                              GROUP BY LBA.PROJECT_ID,\n" +
                        "                                                       LBA.LEDGER_ID) AS LB1\n" +
                        "                                     ON LB.PROJECT_ID = LB1.PROJECT_ID\n" +
                        "                                    AND LB.LEDGER_ID = LB1.LEDGER_ID\n" +
                        "                                  WHERE LB.PROJECT_ID = LB1.PROJECT_ID\n" +
                        "                                  {  AND LB.BRANCH_ID IN (?BRANCH_OFFICE_ID) }\n" +
                        "                                    AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2\n" +
                        "                         ON ML.LEDGER_ID = LB2.LEDGER_ID\n" +
                        "                       LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                        "                         ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        "                       LEFT JOIN MASTER_BANK MB\n" +
                        "                         ON MBA.BANK_ID = MB.BANK_ID\n" +
                        "                      WHERE LG.GROUP_ID IN (12, 13, 14) -- AND LB2.TRANS_MODE='CR'\n" +
                        "                        and ML.STATUS = 0\n" +
                        "                      GROUP BY LG.GROUP_ID,\n" +
                        "                               LG.GROUP_CODE,\n" +
                        "                               LG.LEDGER_GROUP,\n" +
                        "                               ML.LEDGER_NAME,\n" +
                        "                               LB2.PROJECT_ID) AS FNL\n" +
                        "              GROUP BY FNL.BRANCH_ID, FNL.PROJECT_ID) AS TCB\n" +
                        "    ON PB.BRANCH_ID = TCB.BRANCH_ID\n" +
                        "   AND PB.PROJECT_ID = TCB.PROJECT_ID\n" +
                        "  LEFT JOIN (SELECT FRC.BRANCH_ID,\n" +
                        "                    FRC.PROJECT_ID,\n" +
                        "                    FRC.RC_AMOUNT AS RC_AMOUNT\n" +
                        "               FROM (SELECT TR.BRANCH_ID,\n" +
                        "                            TR.PROJECT_ID,\n" +
                        "                            SUM(TR.AMOUNT) AS RC_AMOUNT\n" +
                        "                       FROM (SELECT VM.VOUCHER_ID,\n" +
                        "                                    VM.VOUCHER_DATE,\n" +
                        "                                    VM.BRANCH_ID,\n" +
                        "                                    VM.PROJECT_ID,\n" +
                        "                                    VT.AMOUNT\n" +
                        "                               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        "                              INNER JOIN VOUCHER_TRANS AS VT\n" +
                        "                                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "                                AND VM.BRANCH_ID = VT.BRANCH_ID\n" +
                        "                              WHERE VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "                               { AND VM.BRANCH_ID = ?BRANCH_OFFICE_ID }\n" +
                        "                                AND VM.VOUCHER_TYPE ='RC'\n" +
                        "                              GROUP BY VT.VOUCHER_ID) TR\n" +
                        "                      GROUP BY TR.BRANCH_ID, TR.PROJECT_ID) AS FRC) AS RC\n" +
                        "    ON PB.PROJECT_ID = RC.PROJECT_ID\n" +
                        "   AND PB.BRANCH_ID = RC.BRANCH_ID\n" +
                        "  LEFT JOIN (SELECT FPY.BRANCH_ID,\n" +
                        "                    FPY.PROJECT_ID,\n" +
                        "                    FPY.PY_AMOUNT AS PY_AMOUNT\n" +
                        "               FROM (SELECT TP.BRANCH_ID,\n" +
                        "                            TP.PROJECT_ID,\n" +
                        "                            SUM(TP.AMOUNT) AS PY_AMOUNT\n" +
                        "                       FROM (SELECT VM.VOUCHER_ID,\n" +
                        "                                    VM.VOUCHER_DATE,\n" +
                        "                                    VM.BRANCH_ID,\n" +
                        "                                    VM.PROJECT_ID,\n" +
                        "                                    VT.AMOUNT\n" +
                        "                               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        "                              INNER JOIN VOUCHER_TRANS AS VT\n" +
                        "                                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "                                AND VM.BRANCH_ID = VT.BRANCH_ID\n" +
                        "                              WHERE VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "                               { AND VM.BRANCH_ID = ?BRANCH_OFFICE_ID }\n" +
                        "                                AND VM.VOUCHER_TYPE ='PY'\n" +
                        "                              GROUP BY VT.VOUCHER_ID) TP\n" +
                        "                      GROUP BY TP.BRANCH_ID, TP.PROJECT_ID) FPY) PY\n" +
                        "    ON PY.PROJECT_ID = PB.PROJECT_ID\n" +
                        "   AND PY.BRANCH_ID = PB.BRANCH_ID\n" +
                        " WHERE MP.DELETE_FLAG <> 1\n" +
                        " GROUP BY MP.PROJECT\n" +
                        " ORDER BY MP.PROJECT ASC;";


                        break;
                    }
                case SQLCommand.Project.DeleteProjectBranch:
                    {
                        query = "DELETE FROM PROJECT_BRANCH WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectCategoryLedger:
                    {
                        query = "DELETE FROM PROJECT_CATEGORY_LEDGER WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectCostCentre:
                    {
                        query = "DELETE FROM PROJECT_COSTCENTRE WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.DeleteProjectLedger:
                    {
                        query = "DELETE FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.ProjectBalance:
                    {
                        query = "SELECT COUNT(*) FROM LEDGER_BALANCE WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Project.ProjectCount:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_PROJECT";
                        break;
                    }
                case SQLCommand.Project.ProjectCategoryViseProjectCount:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_PROJECT WHERE PROJECT_CATEGORY_ID=?PROJECT_CATEGORY_ID";
                        break;
                    }
                case SQLCommand.Project.FetchBranchBalance:
                    {
                        query = " SELECT BO.BRANCH_OFFICE_NAME AS NAME  ,\n" +
                                        "       BRANCH_ID,\n" +
                                        "       SUM(OP_BALANCE) AS OPAMOUNT,\n" +
                                        "       SUM(RECEIPT) AS RC_AMOUNT,\n" +
                                        "       SUM(PAYMENT) AS PY_AMOUNT,\n" +
                                        "       SUM(CL_BALANCE) AS CLAMOUNT\n" +
                                        "\n" +
                                        "  FROM BRANCH_OFFICE BO\n" +
                                        "\n" +
                                        "  JOIN (SELECT MT.BRANCH_ID,\n" +
                                        "               IFNULL(SUM(VT.AMOUNT), 0) AS RECEIPT,\n" +
                                        "               0 AS PAYMENT,\n" +
                                        "               0 AS OP_BALANCE,\n" +
                                        "               0 AS CL_BALANCE\n" +
                                        "          FROM MASTER_LEDGER_GROUP LG\n" +
                                        "\n" +
                                        "          LEFT JOIN MASTER_LEDGER ML\n" +
                                        "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                        "          LEFT JOIN VOUCHER_TRANS VT\n" +
                                        "            ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                        "          LEFT JOIN VOUCHER_MASTER_TRANS MT\n" +
                                        "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                        "           AND MT.BRANCH_ID = VT.BRANCH_ID\n" +
                                        "            AND MT.LOCATION_ID=VT.LOCATION_ID\n" +
                                        "          LEFT JOIN MASTER_PROJECT MP\n" +
                                        "            ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                                        "          LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                                        "            ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                                        "         WHERE MT.VOUCHER_TYPE in ('RC', 'PY')\n" +
                                        "           AND VT.TRANS_MODE = 'CR'\n" +
                                        "           AND MT.STATUS = 1\n" +
                                        "           AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                                        "           AND LG.GROUP_ID NOT IN (12, 13)\n" +
                                        "         GROUP BY MT.BRANCH_ID\n" +
                                        "\n" +
                                        "        UNION ALL\n" +
                                        "\n" +
                                        "        SELECT MT.BRANCH_ID,\n" +
                                        "               0 AS RECEIPT,\n" +
                                        "               IFNULL(SUM(VT.AMOUNT), 0) AS PAYMENT,\n" +
                                        "               0 AS OP_BALANCE,\n" +
                                        "               0 AS CL_BALANCE\n" +
                                        "          FROM MASTER_LEDGER_GROUP LG\n" +
                                        "\n" +
                                        "          LEFT JOIN MASTER_LEDGER ML\n" +
                                        "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                        "          LEFT JOIN VOUCHER_TRANS VT\n" +
                                        "            ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                        "          LEFT JOIN VOUCHER_MASTER_TRANS MT\n" +
                                        "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                        "           AND MT.BRANCH_ID = VT.BRANCH_ID\n" +
                                        "            AND MT.LOCATION_ID=VT.LOCATION_ID\n" +
                                        "          LEFT JOIN MASTER_PROJECT MP\n" +
                                        "            ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                                        "          LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                                        "            ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                                        "         WHERE MT.VOUCHER_TYPE in ('RC', 'PY')\n" +
                                        "           AND VT.TRANS_MODE = 'DR'\n" +
                                        "           AND MT.STATUS = 1\n" +
                                        "           AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                                        "           AND LG.GROUP_ID NOT IN (12, 13)\n" +
                                        "         GROUP BY MT.BRANCH_ID\n" +
                                        "\n" +
                                        "        UNION ALL\n" +
                                        "\n" +
                                        "        SELECT LB2.BRANCH_ID,\n" +
                                        "               0 AS RECEIPT,\n" +
                                        "               0 AS PAYMENT,\n" +
                                        "               (SUM(CASE\n" +
                                        "                         WHEN LB2.TRANS_MODE = 'DR' THEN\n" +
                                        "                          LB2.AMOUNT\n" +
                                        "                         ELSE\n" +
                                        "                          -LB2.AMOUNT\n" +
                                        "                       END)) AS OP_BALANCE,\n" +
                                        "               0 AS CL_BALANCE\n" +
                                        "\n" +
                                        "          FROM MASTER_LEDGER AS ML\n" +
                                        "         INNER JOIN MASTER_LEDGER_GROUP AS LG\n" +
                                        "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                        "         INNER JOIN (SELECT LB.BALANCE_DATE,\n" +
                                        "                            LB.BRANCH_ID,\n" +
                                        "                            LB.PROJECT_ID,\n" +
                                        "                            LB.LEDGER_ID,\n" +
                                        "                            LB.AMOUNT,\n" +
                                        "                            LB.TRANS_MODE\n" +
                                        "                       FROM LEDGER_BALANCE AS LB\n" +
                                        "                       LEFT JOIN (SELECT LBA.BRANCH_ID,\n" +
                                        "                                        LBA.PROJECT_ID,\n" +
                                        "                                        LBA.LEDGER_ID,\n" +
                                        "                                        MAX(LBA.BALANCE_DATE) AS BAL_DATE\n" +
                                        "                                   FROM LEDGER_BALANCE LBA\n" +
                                        "                                  WHERE 1 = 1\n" +
                                        "                                    AND LBA.BALANCE_DATE <=\n" +
                                        "                                        ?BOOKS_BEGINNING_FROM\n" +
                                        "                                  GROUP BY LBA.BRANCH_ID,\n" +
                                        "                                           LBA.PROJECT_ID,\n" +
                                        "                                           LBA.LEDGER_ID) AS LB1\n" +
                                        "                         ON LB.BRANCH_ID = LB1.BRANCH_ID\n" +
                                        "                        AND LB.PROJECT_ID = LB1.PROJECT_ID\n" +
                                        "                        AND LB.LEDGER_ID = LB1.LEDGER_ID\n" +
                                        "                      WHERE LB.BALANCE_DATE = LB1.BAL_DATE) LB2\n" +
                                        "            ON ML.LEDGER_ID = LB2.LEDGER_ID\n" +
                                        "         WHERE 1 = 1\n" +
                                        "           AND LG.GROUP_ID IN (12, 13, 14)\n" +
                                        "         GROUP BY LB2.BRANCH_ID\n" +
                                        "\n" +
                                        "        UNION ALL\n" +
                                        "\n" +
                                        "        SELECT LB2.BRANCH_ID,\n" +
                                        "               0 AS RECEIPT,\n" +
                                        "               0 AS PAYMENT,\n" +
                                        "               0 AS OP_BALANCE,\n" +
                                        "               (SUM(CASE\n" +
                                        "                         WHEN LB2.TRANS_MODE = 'DR' THEN\n" +
                                        "                          LB2.AMOUNT\n" +
                                        "                         ELSE\n" +
                                        "                          -LB2.AMOUNT\n" +
                                        "                       END)) AS CL_BALANCE\n" +
                                        "\n" +
                                        "          FROM MASTER_LEDGER AS ML\n" +
                                        "         INNER JOIN MASTER_LEDGER_GROUP AS LG\n" +
                                        "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                        "         INNER JOIN (SELECT LB.BALANCE_DATE,\n" +
                                        "                            LB.BRANCH_ID,\n" +
                                        "                            LB.PROJECT_ID,\n" +
                                        "                            LB.LEDGER_ID,\n" +
                                        "                            LB.AMOUNT,\n" +
                                        "                            LB.TRANS_MODE\n" +
                                        "                       FROM LEDGER_BALANCE AS LB\n" +
                                        "                       LEFT JOIN (SELECT LBA.BRANCH_ID,\n" +
                                        "                                        LBA.PROJECT_ID,\n" +
                                        "                                        LBA.LEDGER_ID,\n" +
                                        "                                        MAX(LBA.BALANCE_DATE) AS BAL_DATE\n" +
                                        "                                   FROM LEDGER_BALANCE LBA\n" +
                                        "                                  WHERE 1 = 1\n" +
                                        "                                    AND LBA.BALANCE_DATE <=\n" +
                                        "                                        ?DATE_CLOSED\n" +
                                        "                                  GROUP BY LBA.BRANCH_ID,\n" +
                                        "                                           LBA.PROJECT_ID,\n" +
                                        "                                           LBA.LEDGER_ID) AS LB1\n" +
                                        "                         ON LB.BRANCH_ID = LB1.BRANCH_ID\n" +
                                        "                        AND LB.PROJECT_ID = LB1.PROJECT_ID\n" +
                                        "                        AND LB.LEDGER_ID = LB1.LEDGER_ID\n" +
                                        "                      WHERE LB.BALANCE_DATE = LB1.BAL_DATE) LB2\n" +
                                        "            ON ML.LEDGER_ID = LB2.LEDGER_ID\n" +
                                        "         WHERE 1 = 1\n" +
                                        "           AND LG.GROUP_ID IN (12, 13, 14)\n" +
                                        "         GROUP BY LB2.BRANCH_ID) AS T\n" +
                                        "    ON BO.BRANCH_OFFICE_ID = T.BRANCH_ID\n" +
                                        "{ AND BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE } \n" +
                                        " GROUP BY BRANCH_ID  ORDER BY BRANCH_OFFICE_NAME ASC";

                        break;
                    }
                case SQLCommand.Project.FetchProjectIdByProjectName:
                    {
                        query = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT=?PROJECT_NAME";
                        break;
                    }
                case SQLCommand.Project.FetchAllBranchProjects:
                    {
                        query = "SELECT MP.PROJECT, BO.BRANCH_OFFICE_CODE\n" +
                       "  FROM MASTER_PROJECT MP\n" +
                       " INNER JOIN MASTER_DIVISION AS MD\n" +
                       "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                       " INNER JOIN PROJECT_BRANCH PB\n" +
                       "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                       " INNER JOIN BRANCH_OFFICE BO\n" +
                       "    ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID\n" +
                       "   AND BO.STATUS = 2\n" +
                       " WHERE MP.DELETE_FLAG <> 1\n" +
                       " ORDER BY MP.PROJECT ASC;";
                        break;
                    }

            }

            return query;
        }
        #endregion Bank SQL
    }
}
