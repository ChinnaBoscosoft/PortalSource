using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.SQL
{
    public class BudgetSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Budget).FullName)
            {
                query = GetBudgetSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        private string GetBudgetSQL()
        {
            string query = "";
            SQLCommand.Budget sqlCommandId = (SQLCommand.Budget)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Budget.FetchById:
                    {
                        query = @"SELECT BM.BUDGET_ID,BUDGET_NAME,
                          BUDGET_TYPE_ID,
                          BUDGET_LEVEL_ID,
                          GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_ID,
                          GROUP_CONCAT(MP.PROJECT) AS PROJECT,
                          DATE_FROM,
                          DATE_TO,
                          REMARKS, BUDGET_ACTION,
                          BM.IS_ACTIVE,
                          BO.BRANCH_OFFICE_NAME,
                          BM.BRANCH_ID
                      FROM BUDGET_MASTER BM
                      LEFT JOIN BUDGET_PROJECT BP
                        ON BP.BUDGET_ID = BM.BUDGET_ID
                     INNER JOIN MASTER_PROJECT MP
                        ON BP.PROJECT_ID = MP.PROJECT_ID
                      INNER JOIN BRANCH_OFFICE BO
                        ON BO.BRANCH_OFFICE_ID = BM.BRANCH_ID
                     WHERE BM.BUDGET_ID IN (?BUDGET_ID);";
                        break;
                    }
                case SQLCommand.Budget.SDBFetchById:
                    {
                        query = @"SELECT BM.BUDGET_ID,BUDGET_NAME,
                          BUDGET_TYPE_ID,
                          BUDGET_LEVEL_ID,
                          GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_ID,
                          GROUP_CONCAT(MP.PROJECT) AS PROJECT,
                          DATE_FROM,
                          DATE_TO,
                          REMARKS, BUDGET_ACTION,
                          BM.IS_ACTIVE,
                          BO.BRANCH_OFFICE_NAME,
                          BM.BRANCH_ID,
                          BM.FILE_NAME
                      FROM BUDGET_MASTER BM
                      LEFT JOIN BUDGET_PROJECT BP
                        ON BP.BUDGET_ID = BM.BUDGET_ID
                     INNER JOIN MASTER_PROJECT MP
                        ON BP.PROJECT_ID = MP.PROJECT_ID
                      INNER JOIN BRANCH_OFFICE BO
                        ON BO.BRANCH_OFFICE_ID = BM.BRANCH_ID
                     WHERE BM.BUDGET_ID IN (?BUDGET_ID);";
                        break;
                    }
                case SQLCommand.Budget.FetchByStatisticId:
                    {
                        query = @"SELECT * FROM BUDGET_STATISTICS_DETAIL WHERE BUDGET_ID =?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchAll:
                    { // by aldrin
                        query = @"SELECT BM.BUDGET_ID,
                                       BUDGET_NAME,
                                       MP.PROJECT,
                                       BUDGET_TYPE_ID,
                                       BP.PROJECT_ID,
                                       DATE_FROM,
                                       DATE_TO,
                                       REMARKS,
                                       IS_ACTIVE,
                                       CASE
                                         WHEN IS_ACTIVE = 0 THEN
                                          'Inactive'
                                         ELSE
                                          'Active'
                                       END AS STATUS
                                  FROM BUDGET_MASTER BM
                                   INNER JOIN BUDGET_PROJECT BP
                                     ON BP.BUDGET_ID= BM.BUDGET_ID
                                  INNER JOIN BUDGET_LEDGER BL 
                                   ON BM.BUDGET_ID = BL.BUDGET_ID
                                 INNER JOIN MASTER_PROJECT MP
                                    ON BP.PROJECT_ID = MP.PROJECT_ID  {AND BP.PROJECT_ID=?PROJECT_ID}
                                     WHERE BUDGET_TYPE_ID<>3
                                      AND DATE_FROM >= ?YEAR_FROM 
                                      AND DATE_TO <= ?YEAR_TO 
                                       { AND DATE_FROM <= ?DATE_FROM }
                                   ORDER BY STATUS,BUDGET_NAME ASC;";

                        break;
                    }

                case SQLCommand.Budget.FetchBudget:
                    {
                        query = @" SELECT BM.BRANCH_ID,BM.BUDGET_ID,BUDGET_NAME AS 'Budget Name',MP.PROJECT, DATE_FORMAT(DATE_FROM, '%d/%m/%Y') as 'Date From',DATE_FORMAT(DATE_TO,'%d/%m/%Y') AS 'Date To',
                                       REMARKS,
                                       CASE
                                         WHEN IS_ACTIVE = 0 THEN
                                          'Inactive'
                                         ELSE
                                          'Active'
                                       END AS STATUS
                                  FROM BUDGET_MASTER BM
                                   INNER JOIN BUDGET_PROJECT BP
                                     ON BP.BUDGET_ID= BM.BUDGET_ID
                                  INNER JOIN BUDGET_LEDGER BL
                                   ON BM.BUDGET_ID = BL.BUDGET_ID
                                 INNER JOIN MASTER_PROJECT MP
                                    ON BP.PROJECT_ID = MP.PROJECT_ID { WHERE BM.BRANCH_ID IN (?BRANCH_OFFICE_ID) } GROUP BY BUDGET_NAME,PROJECT
                                   ORDER BY STATUS,BUDGET_NAME ASC;";
                        break;
                    }
                case SQLCommand.Budget.AddNewBudgetFetchLedger:
                    {
                        query = @"SELECT ML.LEDGER_CODE,MONTH(MONTH_YEAR) AS MONTH,0.00 AS TOTAL,
                                           DATE_FORMAT(MONTH_YEAR, '%b - %Y') AS DURATION,
                                           MLG.LEDGER_GROUP,
                                           ML.LEDGER_ID,
                                           ML.LEDGER_NAME,
                                           ML.LEDGER_CODE,
                                           if(MONTH(MONTH_YEAR) = 1,
                                              0.00,
                                              if(MONTH(MONTH_YEAR) = 2,
                                                 0.00,
                                                 if(MONTH(MONTH_YEAR) = 3,
                                                    0.00,
                                                    if(MONTH(MONTH_YEAR) = 4,
                                                       0.00,
                                                       if(MONTH(MONTH_YEAR) = 5,
                                                          0.00,
                                                          if(MONTH(MONTH_YEAR) = 6,
                                                             0.00,
                                                             if(MONTH(MONTH_YEAR) = 7,
                                                                0.00,
                                                                if(MONTH(MONTH_YEAR) = 8,
                                                                   0.00,
                                                                   if(MONTH(MONTH_YEAR) = 9,
                                                                      0.00,
                                                                      if(MONTH(MONTH_YEAR) = 10,
                                                                         0.00,
                                                                         if(MONTH(MONTH_YEAR) = 11,
                                                                            0.00,
                                                                            if(MONTH(MONTH_YEAR) = 12, 0.00, 0.00)))))))))))) as AMOUNT,
                                           IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE
                                      FROM (SELECT (?DATE_FROM - INTERVAL
                                                    DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,
                                                   NO_OF_MONTH
                                              FROM (SELECT @rownum := @rownum + 1 AS NO_OF_MONTH
                                                      FROM (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4

                                                            ) AS T1,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4

                                                            ) AS T2,

                                                           (SELECT @rownum := -1) AS T0) D1) D2
                                      LEFT JOIN PROJECT_LEDGER PL
                                      INNER JOIN MASTER_LEDGER ML
                                      INNER JOIN MASTER_LEDGER_GROUP MLG
                                          ON ML.GROUP_ID = MLG.GROUP_ID ON ML.LEDGER_ID = PL.LEDGER_ID
                                          AND ML.GROUP_ID NOT IN (12,13,14) AND PL.PROJECT_ID=?PROJECT_ID
                                     WHERE D2.MONTH_YEAR <= ?DATE_TO;";
                        break;
                    }
                case SQLCommand.Budget.FetchRecentBudgetList:
                    {
                        query = "SELECT BUDGET_ID  FROM BUDGET_MASTER BM WHERE PROJECT_ID = ?PROJECT_ID ORDER BY BM.BUDGET_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.Budget.FetchBudgetNames:
                    {
                        //query = "SELECT BM.BUDGET_ID   AS BANK_ID,\n" +
                        //        "       DATE_FROM,\n" +
                        //        //"       YEAR_FROM,\n" +
                        //        "       DATE_TO,\n" +
                        //        "       BM.BUDGET_NAME AS BANK,\n" +
                        //        "       BM.PROJECT_ID\n" +
                        //        "  FROM BUDGET_MASTER BM\n" +
                        //        " INNER JOIN MASTER_PROJECT MP\n" +
                        //        "    ON MP.PROJECT_ID = BM.PROJECT_ID\n" +
                        //        " INNER JOIN ACCOUNTING_YEAR AY\n" +
                        //        "    ON DATE_FROM BETWEEN YEAR_FROM AND YEAR_TO\n" +
                        //        " WHERE AY.STATUS = 1 AND IS_ACTIVE = 1\n" +
                        //    //"   AND BM.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //        " GROUP BY BANK_ID\n" +
                        //        " ORDER BY BANK;";

                        query = "SELECT B.BUDGET_ID AS BUDGET_ID,\n" +
                                "       B.DATE_FROM,\n" +
                                "       B.DATE_TO,\n" +
                                "       B.BUDGET_NAME AS BUDGET_NAME,B.BUDGET_TYPE,\n" +
                                "       GROUP_CONCAT(B.PROJECT_ID) PROJECT_ID,\n" +
                                "       GROUP_CONCAT(B.PROJECT) PROJECT, B.IS_ACTIVE,0 AS 'SELECT'\n" +
                                " FROM (SELECT BM.BUDGET_ID,\n" +
                                    "       DATE_FROM,\n" +
                                    "       DATE_TO,\n" +
                            //"       CONCAT(BM.BUDGET_NAME, CONCAT('- ', CASE WHEN BM.IS_ACTIVE = 0 THEN 'Inactive' ELSE 'Active' END)) BUDGET_NAME ,\n" +
                                    "       BM.BUDGET_NAME,\n" +
                                    "       BT.BUDGET_TYPE,\n" +
                                    "       BP.PROJECT_ID PROJECT_ID,\n" +
                                    "       CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS PROJECT, BM.IS_ACTIVE\n" +
                                    "  FROM BUDGET_MASTER BM\n" +
                                    " INNER JOIN BUDGET_PROJECT BP\n" +
                                    "    ON BP.BUDGET_ID = BM.BUDGET_ID \n" +
                                    " INNER JOIN MASTER_PROJECT MP\n" +
                                    "    ON MP.PROJECT_ID = BP.PROJECT_ID\n" +
                                    " INNER JOIN MASTER_DIVISION MD ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                            // " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID = UP.PROJECT_ID\n" +
                            //" INNER JOIN ACCOUNTING_YEAR AY\n" +
                            //"    ON DATE_FROM BETWEEN YEAR_FROM AND YEAR_TO\n" +
                                    " INNER JOIN BUDGET_TYPE BT\n" +
                                    "    ON BT.BUDGET_TYPE_ID = BM.BUDGET_TYPE_ID\n" +
                            //  " WHERE IS_ACTIVE = 1 AND ((DATE_FROM >= ?DATE_FROM AND DATE_TO <=?DATE_TO) OR \n" +
                                    " WHERE IS_ACTIVE = 1 AND ((DATE_FROM >= ?YEAR_FROM AND DATE_TO <=?YEAR_TO) OR \n" +
                                    " (BM.BUDGET_TYPE_ID = " + (int)BudgetType.BudgetByCalendarYear + " AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <=?YEAR_TO)) AND \n" +
                            //" BP.PROJECT_ID IN (?PROJECT_ID) AND \n" +
                                    " MP.DELETE_FLAG<>1 { AND UP.ROLE_ID=?USERROLE_ID }\n" +
                                    " GROUP BY MP.PROJECT, BM.BUDGET_ID) B\n" +
                            //" WHERE B.PROJECT_ID IN (?PROJECT_ID)\n" +
                            // "WHERE B.BUDGET_ID IN (SELECT BUDGET_ID FROM BUDGET_PROJECT WHERE PROJECT_ID IN (?PROJECT_ID))\n" +
                                    " { WHERE B.BUDGET_ID IN (?BUDGET_ID) }\n" +
                                    " GROUP BY B.BUDGET_ID ORDER BY B.IS_ACTIVE DESC, B.DATE_FROM, B.BUDGET_NAME;";
                        break;
                    }
                case SQLCommand.Budget.FetchBudgetByProject:
                    {
                        query = query = "SELECT BM.BUDGET_ID   AS BUDGET_ID, 0 AS 'SELECT', \n" +
                                "       DATE_FROM,\n" +
                                "       DATE_TO,\n" +
                                "       BM.BUDGET_NAME AS BUDGET_NAME,\n" +
                                "       BP.PROJECT_ID\n" +
                                "  FROM BUDGET_MASTER BM\n" +
                                "   INNER JOIN BUDGET_PROJECT BP\n" +
                                "  ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = BP.PROJECT_ID\n" +
                                " WHERE DATE_FROM >=?DATE_FROM AND DATE_TO<=?DATE_TO \n" +
                                " { AND BM.IS_ACTIVE = ?IS_ACTIVE} \n" +
                                "  AND BP.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " GROUP BY BUDGET_ID\n" +
                                " ORDER BY BUDGET_NAME";
                        break;
                    }
                case SQLCommand.Budget.AddPeriod:
                    {
                        query = "INSERT INTO BUDGET_MASTER\n" +
                                 "  (BUDGET_NAME, BUDGET_TYPE_ID, BUDGET_LEVEL_ID,IS_MONTH_WISE,DATE_FROM,DATE_TO, IS_ACTIVE ,REMARKS)\n" +
                                 "VALUES\n" +
                                 "  (?BUDGET_NAME, ?BUDGET_TYPE_ID, ?BUDGET_LEVEL_ID,?IS_MONTH_WISE, ?DATE_FROM, ?DATE_TO,?IS_ACTIVE,?REMARKS)";
                        break;
                    }
                case SQLCommand.Budget.AddAnnual:
                    {
                        query = "INSERT INTO BUDGET_MASTER\n" +
                                 "  (BUDGET_NAME, BUDGET_TYPE_ID, BUDGET_LEVEL_ID,IS_MONTH_WISE,DATE_FROM,DATE_TO, IS_ACTIVE,BUDGET_ACTION,REMARKS, BRANCH_ID,FILE_NAME)\n" +
                                 "VALUES\n" +
                                 "  (?BUDGET_NAME, ?BUDGET_TYPE_ID, ?BUDGET_LEVEL_ID, ?IS_MONTH_WISE, ?DATE_FROM, ?DATE_TO,?IS_ACTIVE,?BUDGET_ACTION,?REMARKS,?BRANCH_ID, ?FILE_NAME)";

                        //query = "INSERT INTO BUDGET_MASTER\n" +
                        //         "  (BUDGET_NAME, BUDGET_TYPE_ID, BUDGET_LEVEL_ID,IS_MONTH_WISE,DATE_FROM,DATE_TO, HO_HELP_PROPOSED_AMOUNT, HO_HELP_APPROVED_AMOUNT, IS_ACTIVE, BUDGET_ACTION)\n" +
                        //         "VALUES\n" +
                        //         "  (?BUDGET_NAME, ?BUDGET_TYPE_ID, ?BUDGET_LEVEL_ID,?IS_MONTH_WISE, ?DATE_FROM, ?DATE_TO,?HO_HELP_PROPOSED_AMOUNT, ?HO_HELP_APPROVED_AMOUNT, ?IS_ACTIVE, ?BUDGET_ACTION)";
                        break;
                    }
                case SQLCommand.Budget.CheckStatus:
                    {
                        // query = "SELECT BM.BUDGET_ID FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID WHERE PROJECT_ID IN (?PROJECT_ID) AND IS_ACTIVE =TRUE AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <= ?YEAR_TO { AND BUDGET_TYPE_ID =?BUDGET_TYPE_ID};";
                        // query = "SELECT BM.BUDGET_ID FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID WHERE PROJECT_ID IN (?PROJECT_ID) AND IS_ACTIVE =TRUE AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <= ?YEAR_TO { AND BUDGET_TYPE_ID =?BUDGET_TYPE_ID};";
                        query = "SELECT BM.BUDGET_ID FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID WHERE PROJECT_ID IN (?PROJECT_ID) AND IS_ACTIVE =TRUE AND (DATE_FROM BETWEEN ?YEAR_FROM AND ?YEAR_TO OR DATE_TO BETWEEN ?YEAR_FROM AND ?YEAR_TO) { AND BUDGET_TYPE_ID =?BUDGET_TYPE_ID};";
                        break;
                    }
                case SQLCommand.Budget.Update:
                    {
                        query = " UPDATE BUDGET_MASTER\n" +
                                " SET BUDGET_ACTION = ?BUDGET_ACTION\n" +
                                " WHERE BUDGET_ID = ?BUDGET_ID";
                        break;
                    }

                case SQLCommand.Budget.UpdateOnline:
                    {
                        query = " UPDATE BUDGET_MASTER\n" +
                                "   SET BUDGET_NAME    = ?BUDGET_NAME,\n" +
                                "       BUDGET_TYPE_ID = ?BUDGET_TYPE_ID,\n" +
                                "       BUDGET_LEVEL_ID = ?BUDGET_LEVEL_ID,\n" +
                                "       IS_MONTH_WISE  = ?IS_MONTH_WISE,\n" +
                                "       DATE_FROM      = ?DATE_FROM,\n" +
                                "       DATE_TO        = ?DATE_TO,\n" +
                                "       IS_ACTIVE      = ?IS_ACTIVE,\n" +
                                "       REMARKS=?REMARKS,\n" +
                                "       BUDGET_ACTION=?BUDGET_ACTION,\n" +
                                 "      FILE_NAME=?FILE_NAME\n" +
                                " WHERE BUDGET_ID = ?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.Delete:
                    {
                        query = "DELETE FROM BUDGET_MASTER WHERE BUDGET_ID = ?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.DeleteBudgetLedgerById:
                    {
                        query = "DELETE FROM BUDGET_LEDGER WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.DeleteBudgetProjectById:
                    {
                        query = "DELETE FROM BUDGET_PROJECT WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.DeleteBudgetStatisticsDetails:
                    {
                        query = "DELETE FROM BUDGET_STATISTICS_DETAIL WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.DeleteAllotFund:
                    {
                        query = "DELETE FROM ALLOT_FUND WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.BudgetLedgerAdd:
                    {
                        query = "INSERT INTO BUDGET_LEDGER\n" +
                                "   (BUDGET_ID,LEDGER_ID,AMOUNT,TRANS_MODE)\n" +
                                "VALUES\n" +
                                "   (?BUDGET_ID,?LEDGER_ID,?AMOUNT,?TRANS_MODE)";
                        break;
                    }
                case SQLCommand.Budget.BudgetLedgerUpdate:
                    {
                        // 10.01.2019
                        query = "UPDATE BUDGET_LEDGER SET APPROVED_AMOUNT =?APPROVED_AMOUNT, HO_NARRATION =?HO_NARRATION\n" +
                                "WHERE BUDGET_ID = ?BUDGET_ID AND LEDGER_ID = ?LEDGER_ID AND TRANS_MODE=?BUDGET_TRANS_MODE";
                        break;
                    }
                case SQLCommand.Budget.BudgetOnlineLedgerUpdate:
                    {
                        query = "INSERT INTO BUDGET_LEDGER\n" +
                                "   (BUDGET_ID,LEDGER_ID,PROPOSED_AMOUNT,APPROVED_AMOUNT,TRANS_MODE,HO_NARRATION)\n" +
                                "VALUES\n" +
                                "   (?BUDGET_ID,?LEDGER_ID,?PROPOSED_AMOUNT,?APPROVED_AMOUNT,?TRANS_MODE,?HO_NARRATION)";
                        break;
                    }
                case SQLCommand.Budget.BudgetSubLedgerUpdate:
                    {
                        query = "UPDATE BUDGET_SUB_LEDGER SET APPROVED_AMOUNT =?APPROVED_AMOUNT, HO_NARRATION =?HO_NARRATION WHERE BUDGET_ID = ?BUDGET_ID AND LEDGER_ID = ?LEDGER_ID AND SUB_LEDGER_ID = ?SUB_LEDGER_ID";
                        break;
                    }
                case SQLCommand.Budget.AnnualBudgetProjectAdd:
                    {
                        query = "INSERT INTO BUDGET_PROJECT (BUDGET_ID,PROJECT_ID) VALUES (?BUDGET_ID,?PROJECT_ID)";
                        break;
                    }
                case SQLCommand.Budget.AddStatisticDetails:
                    {
                        query = "INSERT INTO BUDGET_STATISTICS_DETAIL (BUDGET_ID,STATISTICS_TYPE_ID,TOTAL_COUNT) VALUES (?BUDGET_ID,?STATISTICS_TYPE_ID,?TOTAL_COUNT)";
                        break;
                    }
                case SQLCommand.Budget.FetchMappedLedgers:
                    {
                        query = "SELECT ML.LEDGER_CODE,\n" +
                                "       PL.PROJECT_ID,\n" +
                                "       MP.PROJECT,\n" +
                                "       PL.LEDGER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,\n" +
                                "       0 AS AMOUNT\n" +
                                "  FROM project_ledger PL\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                            // "   AND MLG.NATURE_ID IN (1, 2)\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = PL.PROJECT_ID\n" +
                                "   AND PL.PROJECT_ID = ?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Budget.ChangeStatusToInActive:
                    {
                        query = "UPDATE BUDGET_MASTER SET IS_ACTIVE=0 WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchBudgetBalance:
                    {
                        //query = "SELECT BM.BUDGET_ID, BL.LEDGER_ID, BL.AMOUNT, BL.TRANS_MODE\n" +
                        //      "  FROM BUDGET_MASTER BM\n" +
                        //      " INNER JOIN BUDGET_LEDGER BL\n" +
                        //      "    ON BL.BUDGET_ID = BM.BUDGET_ID\n" +
                        //      "   AND BL.LEDGER_ID = ?LEDGER_ID\n" +
                        //      "   AND BM.PROJECT_ID = ?PROJECT_ID\n" +
                        //      "   AND BM.IS_ACTIVE = 1;";
                        //break;

                        query = "SELECT LEDGER_ID,\n" +
                                "       IF(MONTH(?DATE_FROM) = 1,\n" +
                                "          MONTH1,\n" +
                                "          IF(MONTH(?DATE_FROM) = 2,\n" +
                                "             MONTH2,\n" +
                                "             IF(MONTH(?DATE_FROM) = 3,\n" +
                                "                MONTH3,\n" +
                                "                IF(MONTH(?DATE_FROM) = 4,\n" +
                                "                   MONTH4,\n" +
                                "                   IF(MONTH(?DATE_FROM) = 5,\n" +
                                "                      MONTH5,\n" +
                                "                      IF(MONTH(?DATE_FROM) = 6,\n" +
                                "                         MONTH6,\n" +
                                "                         IF(MONTH(?DATE_FROM) = 7,\n" +
                                "                            MONTH7,\n" +
                                "                            IF(MONTH(?DATE_FROM) = 8,\n" +
                                "                               MONTH8,\n" +
                                "                               IF(MONTH(?DATE_FROM) = 9,\n" +
                                "                                  MONTH9,\n" +
                                "                                  IF(MONTH(?DATE_FROM) = 10,\n" +
                                "                                     MONTH10,\n" +
                                "                                     IF(MONTH(?DATE_FROM) = 11,\n" +
                                "                                        MONTH11,\n" +
                                "                                        IF(MONTH(?DATE_FROM) = 12,\n" +
                                "                                           MONTH12,\n" +
                                "                                           0.00)))))))))))) AS AMOUNT\n" +
                                "\n" +
                                "  FROM allot_fund AF\n" +
                                "  LEFT JOIN budget_master BM\n" +
                                "    ON AF.BUDGET_ID = BM.BUDGET_ID\n" +
                                " WHERE AF.LEDGER_ID = ?LEDGER_ID\n" +
                                "   AND IS_ACTIVE = TRUE\n" +
                                "   AND BM.PROJECT_ID = PROJECT_ID\n" +
                                " GROUP BY MONTH(?DATE_FROM);";
                        break;
                    }
                case SQLCommand.Budget.CheckBudgetByDate:
                    {
                        query = " SELECT *\n" +
                                "  FROM BUDGET_MASTER BM\n" +
                                " INNER JOIN BUDGET_PROJECT BP\n" +
                                " ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                " WHERE BP.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND BM.IS_ACTIVE = 1\n" +
                                "   AND ?VOUCHER_DATE BETWEEN BM.DATE_FROM AND BM.DATE_TO;";

                        //chinna trans
                        //query = " SELECT *\n" +
                        //        "  FROM BUDGET_MASTER BM\n" +
                        //        " WHERE BM.PROJECT_ID = ?PROJECT_ID\n" +
                        //        "   AND BM.IS_ACTIVE = 1\n" +
                        //        "   AND ?VOUCHER_DATE BETWEEN BM.DATE_FROM AND BM.DATE_TO;";
                        break;
                    }
                case SQLCommand.Budget.FetchbyBudgetProject:
                    {
                        query = "SELECT GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_ID\n" +
                                 "FROM BUDGET_MASTER BM\n" +
                                 "INNER JOIN BUDGET_PROJECT BP\n" +
                                 "ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                 "WHERE BP.BUDGET_ID = ?BUDGET_ID\n" +
                                 "AND BM.IS_ACTIVE = 1\n" +
                                 " AND ?VOUCHER_DATE BETWEEN BM.DATE_FROM AND BM.DATE_TO;";
                        break;
                    }
                case SQLCommand.Budget.FetchBudgetProjectforLookup:
                    {
                        query = "SELECT " +
                                    "MP.PROJECT_ID," +
                                    "MP.DATE_STARTED," +
                                    "MP.DATE_CLOSED," +
                                    "MD.DIVISION_ID,MP.CONTRIBUTION_ID," +
                                    "MP.PROJECT AS PROJECT_NAME," +
                                    "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE ,MP.CUSTOMERID " +
                                "FROM " +
                                    " MASTER_PROJECT MP " +
                                    " INNER JOIN MASTER_DIVISION MD ON " +
                                    " MP.DIVISION_ID=MD.DIVISION_ID " +
                                    " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID=UP.PROJECT_ID " +
                                    " WHERE MP.DELETE_FLAG<>1 { AND UP.ROLE_ID=?USERROLE_ID } " +
                                    " {AND (MP.DATE_CLOSED IS NULL OR MP.DATE_CLOSED >= ?DATE_CLOSED) } " + //On 28/06/2018, This property is used to skip Projects which is closed on or equal to this date
                                    " GROUP BY MP.PROJECT ASC ";
                        break;
                    }
                case SQLCommand.Budget.FetchLastBudgetMonth:
                    {
                        query = "SELECT DATE_FROM AS NEXT_DATE\n" +
                           "FROM BUDGET_MASTER BM\n" +
                          "INNER JOIN BUDGET_PROJECT BP\n" +
                             "ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                            "AND BP.PROJECT_ID IN (?PROJECT_ID)\n" +
                            " AND DATE_FROM >= ?YEAR_FROM\n" +
                            " AND DATE_TO <= ?YEAR_TO\n" +
                          "ORDER BY DATE_TO DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.Budget.FetchProjectforBudget:
                    {
                        query = "SELECT GROUP_CONCAT(PROJECT_ID) AS PROJECT FROM BUDGET_PROJECT WHERE BUDGET_ID IN (?BUDGET_ID)";
                        break;
                    }
                case SQLCommand.Budget.AddAllotFund:
                    {
                        query = "INSERT INTO ALLOT_FUND " +
                              "(BUDGET_ID, " +
                               "LEDGER_ID, " +
                               "MONTH1, " +
                               "MONTH2, " +
                               "MONTH3, " +
                               "MONTH4, " +
                               "MONTH5, " +
                               "MONTH6, " +
                               "MONTH7, " +
                               "MONTH8, " +
                               "MONTH9, " +
                               "MONTH10, " +
                               "MONTH11, " +
                               "MONTH12) " +
                            "VALUES " +
                              "(?BUDGET_ID, " +
                               "?LEDGER_ID," +
                               " ?MONTH1, " +
                               "?MONTH2, " +
                               "?MONTH3, " +
                               "?MONTH4, " +
                               "?MONTH5, " +
                               "?MONTH6, " +
                               "?MONTH7, " +
                               "?MONTH8, " +
                               "?MONTH9, " +
                               "?MONTH10, " +
                               "?MONTH11, " +
                               "?MONTH12)";
                        break;
                    }
                case SQLCommand.Budget.UpdateAllotFund:
                    {
                        query = "UPDATE ALLOT_FUND SET " +
                                        "MONTH1 =?MONTH1, " +
                                        "MONTH2=?MONTH2, " +
                                        "MONTH3=?MONTH3, " +
                                        "MONTH4=?MONTH4, " +
                                        "MONTH5=?MONTH5, " +
                                        "MONTH6=?MONTH6, " +
                                        "MONTH7=?MONTH7, " +
                                        "MONTH8=?MONTH8, " +
                                        "MONTH9=?MONTH9, " +
                                        "MONTH10=?MONTH10, " +
                                        "MONTH11=?MONTH11, " +
                                        "MONTH12=?MONTH12 " +
                                        "WHERE LEDGER_ID=?LEDGER_ID  AND BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchAllotFund:
                    {
                        query = "SELECT " +
                                    "BUDGET_ID," +
                                    "LEDGER_ID," +
                                    "MONTH1, " +
                                    "MONTH2, " +
                                    "MONTH3, " +
                                    "MONTH4, " +
                                    "MONTH5, " +
                                    "MONTH6, " +
                                    "MONTH7, " +
                                    "MONTH8, " +
                                    "MONTH9, " +
                                    "MONTH10, " +
                                    "MONTH11, " +
                                    "MONTH12 " +
                                    "FROM ALLOT_FUND WHERE LEDGER_ID=?LEDGER_ID AND BUDGET_ID=?BUDGET_ID;";
                        break;
                    }
                case SQLCommand.Budget.GetLedgerExist:
                    {
                        query = "SELECT COUNT(*) AS COUNT FROM ALLOT_FUND WHERE LEDGER_ID=?LEDGER_ID AND BUDGET_ID=?BUDGET_ID;";
                        break;
                    }

                case SQLCommand.Budget.BudgetLoad:
                    {
                        query = @"SELECT MONTH(MONTH_YEAR) AS MONTH,IF(BL.AMOUNT IS NULL,0,BL.AMOUNT) AS TOTAL,
                                           IF(BL.AMOUNT IS NULL,0,BL.AMOUNT) AS PROECTED_AMOUNT,
                                           IF(T.AMOUNT IS NULL,0,T.AMOUNT) AS ACTUAL_AMOUNT,
                                           DATE_FORMAT(MONTH_YEAR, '%b - %Y') AS DURATION,
                                           MLG.LEDGER_GROUP,MLG.NATURE_ID,NATURE,
                                           ML.LEDGER_ID,
                                           ML.LEDGER_NAME,
                                           ML.LEDGER_CODE,
                                           IF(MONTH(MONTH_YEAR) = 1 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                              MONTH1,
                                              IF(MONTH(MONTH_YEAR) = 2 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                 MONTH2,
                                                 IF(MONTH(MONTH_YEAR) = 3 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                    MONTH3,
                                                    IF(MONTH(MONTH_YEAR) = 4 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                       MONTH4,
                                                       IF(MONTH(MONTH_YEAR) = 5 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                          MONTH5,
                                                          IF(MONTH(MONTH_YEAR) = 6 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                             MONTH6,
                                                             IF(MONTH(MONTH_YEAR) = 7 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                MONTH7,
                                                                IF(MONTH(MONTH_YEAR) = 8 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                   MONTH8,
                                                                   IF(MONTH(MONTH_YEAR) = 9 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                      MONTH9,
                                                                      IF(MONTH(MONTH_YEAR) = 10 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                         MONTH10,
                                                                         IF(MONTH(MONTH_YEAR) = 11 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                            MONTH11,
                                                                            IF(MONTH(MONTH_YEAR) = 12 AND AL.LEDGER_ID=BL.LEDGER_ID, MONTH12, 0.00)))))))))))) AS AMOUNT,
                                           IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE
                                      FROM (SELECT (?DATE_FROM - INTERVAL
                                                    DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,
                                                   NO_OF_MONTH
                                              FROM (SELECT @rownum := @rownum + 1 AS NO_OF_MONTH
                                                      FROM (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4

                                                            ) AS T1,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4

                                                            ) AS T2,

                                                           (SELECT @rownum := -1) AS T0) D1) D2
                                      LEFT JOIN PROJECT_LEDGER PL
                                      INNER JOIN MASTER_LEDGER ML
                                      INNER JOIN MASTER_LEDGER_GROUP MLG
                                          ON ML.GROUP_ID = MLG.GROUP_ID ON ML.LEDGER_ID = PL.LEDGER_ID
                                          AND ML.GROUP_ID NOT IN (12,13,14) AND PL.PROJECT_ID=?PROJECT_ID
                                          INNER JOIN MASTER_NATURE MN
                                          ON MLG.NATURE_ID=MN.NATURE_ID
                                           LEFT JOIN BUDGET_LEDGER BL ON PL.LEDGER_ID=BL.LEDGER_ID AND BUDGET_ID=?BUDGET_ID --  Budget Id
                                           LEFT JOIN ALLOT_FUND AL
                                         ON AL.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                      LEFT JOIN BUDGET_MASTER BM
                                        ON BM.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                      LEFT JOIN (SELECT LB.PROJECT_ID,
                                                    BL.BUDGET_ID,
                                                    LB.LEDGER_ID,
                                                    BL.AMOUNT AS PROECTED_AMOUNT,
                                                    IFNULL(MAX(LB.AMOUNT), 0) AS AMOUNT
                                               FROM LEDGER_BALANCE LB
             
                                               LEFT JOIN BUDGET_MASTER BM
                                                 ON BM.PROJECT_ID = LB.PROJECT_ID
             
                                               LEFT JOIN BUDGET_LEDGER BL
                                                 ON BL.LEDGER_ID = LB.LEDGER_ID
             
                                               WHERE LB.PROJECT_ID IN (?PROJECT_ID) AND BM.BUDGET_ID=?PREVIOUS_BUDGET_ID
                                                    
                                                AND LB.BALANCE_DATE <=?DATE_TO
                                              GROUP BY LB.PROJECT_ID, LB.LEDGER_ID) AS T
                                          ON T.LEDGER_ID = ML.LEDGER_ID
                                     WHERE D2.MONTH_YEAR <=?DATE_TO ORDER BY NATURE_ID;";
                        break;
                    }
                case SQLCommand.Budget.GetRandomMonth:
                    {
                        query = @"SELECT MONTH_YEAR,DATE_FORMAT(MONTH_YEAR,'%b - %Y') AS DURATION,AMOUNT AS TOTAL,
                                           IF(MONTH(MONTH_YEAR) = 1 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                              MONTH1,
                                              IF(MONTH(MONTH_YEAR) = 2 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                 MONTH2,
                                                 IF(MONTH(MONTH_YEAR) = 3 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                    MONTH3,
                                                    IF(MONTH(MONTH_YEAR) = 4 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                       MONTH4,
                                                       IF(MONTH(MONTH_YEAR) = 5 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                          MONTH5,
                                                          IF(MONTH(MONTH_YEAR) = 6 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                             MONTH6,
                                                             IF(MONTH(MONTH_YEAR) = 7 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                MONTH7,
                                                                IF(MONTH(MONTH_YEAR) = 8 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                   MONTH8,
                                                                   IF(MONTH(MONTH_YEAR) = 9 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                      MONTH9,
                                                                      IF(MONTH(MONTH_YEAR) = 10 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                         MONTH10,
                                                                         IF(MONTH(MONTH_YEAR) = 11 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                            MONTH11,
                                                                            IF(MONTH(MONTH_YEAR) = 12 AND AL.LEDGER_ID=BL.LEDGER_ID, MONTH12, 0.00)))))))))))) AS AMOUNT,
                                           IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                                           YEAR(MONTH_YEAR) AS 'YEAR',
                                           MONTH(MONTH_YEAR) AS 'MONTH',
                                           ML.LEDGER_CODE,
                                           MLG.LEDGER_GROUP,
                                           BL.BUDGET_ID,
                                           BL.LEDGER_ID,
                                           ML.LEDGER_NAME
                                      FROM (SELECT (?DATE_FROM - INTERVAL
                                                    DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,
                                                   NO_OF_MONTH
                                              FROM (SELECT @ROWNUM := @ROWNUM + 1 AS NO_OF_MONTH
                                                      FROM (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                                                            ) AS T1,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                                                            ) AS T2,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                        
                                                            ) AS T3,
                                                           (SELECT @ROWNUM := -1) AS T0) D1) D2
                                      LEFT JOIN BUDGET_LEDGER BL
                                     INNER JOIN MASTER_LEDGER ML
                                     INNER JOIN MASTER_LEDGER_GROUP MLG
                                        ON ML.GROUP_ID = MLG.GROUP_ID ON ML.LEDGER_ID = BL.LEDGER_ID AND ML.GROUP_ID NOT IN (12,13,14)
                                      LEFT JOIN ALLOT_FUND AL
                                        ON AL.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                      LEFT JOIN BUDGET_MASTER BM
                                        ON BM.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                       AND BM.DATE_FROM <= ?DATE_FROM
                                       AND BM.DATE_TO >= ?DATE_TO
                                       AND YEAR(D2.MONTH_YEAR) = YEAR(BM.DATE_FROM)
                                       AND MONTH(D2.MONTH_YEAR) = MONTH(BM.DATE_FROM)
                                     WHERE BL.BUDGET_ID = ?BUDGET_ID
                                       AND D2.MONTH_YEAR <= ?DATE_TO;";
                        break;
                    }
                //Modified by Carmel Raj M on July-04-2015

                //Changes: This query is executed based on the budget type.
                //Budget Type = Annual Budget (BUDGET_TYPE_ID =3) 
                //Budget Type = Period Budget (BUDGET_TYPE_ID = 2)
                case SQLCommand.Budget.FetchBudgetAmount:
                    {
                        query = "SELECT IF(BUDGET_TYPE_ID = ?BUDGET_TYPE_ID, BL.LEDGER_ID, AF.LEDGER_ID) AS LEDGER_ID,\n" + //= 3
                                "       BUDGET_TYPE_ID,BL.TRANS_MODE,\n" +
                            //"       IF(BUDGET_TYPE_ID = ?BUDGET_TYPE_ID,\n" + //3
                            //"          BL.APPROVED_AMOUNT,BL.APPROVED_AMOUNT) AS AMOUNT\n" +
                                "  IF(BUDGET_TYPE_ID = ?BUDGET_TYPE_ID AND ?BUDGET_TYPE_ID = " + (int)BudgetType.BudgetMonth + ",\n" +
                                "  SUM(IF(MONTH(?DATE_FROM)=MONTH(BM.DATE_FROM),BL.APPROVED_AMOUNT,0)),BL.APPROVED_AMOUNT) AS AMOUNT\n" +
                                "  FROM BUDGET_MASTER BM\n" +
                                "  LEFT JOIN BUDGET_LEDGER BL\n" +
                                "    ON BM.BUDGET_ID = BL.BUDGET_ID\n" +
                                "    AND BL.LEDGER_ID IN (?LEDGER_ID)\n" +
                                "     LEFT JOIN BUDGET_PROJECT BP\n" +
                                "    ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                "  LEFT JOIN ALLOT_FUND AF\n" +
                                "    ON AF.BUDGET_ID = BM.BUDGET_ID\n" +
                                " WHERE (CASE\n" +
                                "         WHEN BUDGET_TYPE_ID = 2 THEN\n" +
                                "          AF.LEDGER_ID = ?LEDGER_ID\n" +
                                "         ELSE\n" +
                                "          BL.LEDGER_ID = ?LEDGER_ID\n" +
                                "       END)\n" +
                                "   AND IS_ACTIVE = TRUE\n" +
                                "   AND BP.PROJECT_ID = ?PROJECT_ID AND BL.APPROVED_AMOUNT <> '' AND BL.TRANS_MODE = ?TRANS_MODE\n" +
                                " AND DATE_FROM BETWEEN ?YEAR_FROM AND ?YEAR_TO\n" +
                                " GROUP BY MONTH(?DATE_FROM);";
                        break;
                    }
                case SQLCommand.Budget.ImportBudget:
                    {
                        query = @"SELECT MONTH_YEAR,DATE_FORMAT(MONTH_YEAR,'%b - %Y') AS DURATION,ROUND(BL.AMOUNT*?PERCENTAGE/100) AS TOTAL,0  AS PROECTED_AMOUNT,0 AS ACTUAL_AMOUNT,
                                            ROUND((IF(MONTH(MONTH_YEAR) = 1 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                              MONTH1,
                                              IF(MONTH(MONTH_YEAR) = 2 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                 MONTH2,
                                                 IF(MONTH(MONTH_YEAR) = 3 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                    MONTH3,
                                                    IF(MONTH(MONTH_YEAR) = 4 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                       MONTH4,
                                                       IF(MONTH(MONTH_YEAR) = 5 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                          MONTH5,
                                                          IF(MONTH(MONTH_YEAR) = 6 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                             MONTH6,
                                                             IF(MONTH(MONTH_YEAR) = 7 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                MONTH7,
                                                                IF(MONTH(MONTH_YEAR) = 8 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                   MONTH8,
                                                                   IF(MONTH(MONTH_YEAR) = 9 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                      MONTH9,
                                                                      IF(MONTH(MONTH_YEAR) = 10 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                         MONTH10,
                                                                         IF(MONTH(MONTH_YEAR) = 11 AND AL.LEDGER_ID=BL.LEDGER_ID,
                                                                            MONTH11,
                                                                            IF(MONTH(MONTH_YEAR) = 12 AND AL.LEDGER_ID=BL.LEDGER_ID, MONTH12, 0.00))))))))))))*?PERCENTAGE)/100) AS AMOUNT,
                                           IF(MLG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                                           YEAR(MONTH_YEAR) AS 'YEAR',
                                           MONTH(MONTH_YEAR) AS 'MONTH',
                                           ML.LEDGER_CODE,
                                           MLG.LEDGER_GROUP,
                                           BL.BUDGET_ID,
                                           BL.LEDGER_ID,
                                           ML.LEDGER_NAME
                                      FROM (SELECT (?DATE_FROM - INTERVAL
                                                    DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,
                                                   NO_OF_MONTH
                                              FROM (SELECT @ROWNUM := @ROWNUM + 1 AS NO_OF_MONTH
                                                      FROM (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                                                            ) AS T1,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                                                            ) AS T2,
                                                           (SELECT 1 UNION
                                                                    SELECT 2 UNION
                                                                            SELECT 3 UNION
                                                                                    SELECT 4
                                                
                        
                                                            ) AS T3,
                                                           (SELECT @ROWNUM := -1) AS T0) D1) D2
                                      LEFT JOIN BUDGET_LEDGER BL
                                     INNER JOIN MASTER_LEDGER ML
                                     INNER JOIN MASTER_LEDGER_GROUP MLG
                                        ON ML.GROUP_ID = MLG.GROUP_ID ON ML.LEDGER_ID = BL.LEDGER_ID
                                      LEFT JOIN ALLOT_FUND AL
                                        ON AL.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                      LEFT JOIN BUDGET_MASTER BM
                                        ON BM.BUDGET_ID = BL.BUDGET_ID AND AL.LEDGER_ID=BL.LEDGER_ID
                                       AND BM.DATE_FROM <= ?DATE_FROM
                                       AND BM.DATE_TO >= ?DATE_TO
                                       AND YEAR(D2.MONTH_YEAR) = YEAR(BM.DATE_FROM)
                                       AND MONTH(D2.MONTH_YEAR) = MONTH(BM.DATE_FROM)
                                     WHERE BL.BUDGET_ID = ?BUDGET_ID
                                       AND D2.MONTH_YEAR <= ?DATE_TO;";
                        break;
                    }
                #region Annual Budget
                // chinna 05.05.2018
                case SQLCommand.Budget.AnnualBudgetFetchAdd:
                    {
                        query = "SELECT MLG.GROUP_ID,\n" +
                                "       MLG.NATURE_ID,\n" +
                                "       Ml.LEDGER_ID,\n" +
                                 "       TRANS_MODE,\n" +
                                "       GROUP_CONCAT(PROJECT_ID) AS PROJECT_ID,\n" +
                                "       Ml.LEDGER_CODE,\n" +
                                "       Ml.LEDGER_NAME,\n" +
                                "         CASE\n" +
                                "               WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                                "                           'Recurring Expenses'\n" +
                                "               WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                                "                            'Non - Recurring Expenses'\n" +
                                "                    ELSE ''\n" +
                                "                    END AS BUDGET_GROUP,\n" +
                                "                  CASE\n" +
                                "                    WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                                "                           'Regular Expenses'\n" +
                                "                    WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                                "                            'Non - Regular Expenses'\n" +
                                "                            ELSE ''\n" +
                                "               END AS BUDGET_SUB_GROUP,\n" +
                                "       MLG.LEDGER_GROUP,\n" +
                                "       MNN.NATURE,\n" +
                                "       IFNULL(APPROVED_INCOME_PREVIOUS_YR,0.00) AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                                "       0.00 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                                "       0.00 AS APPROVED_INCOME_CURRENT_YR,\n" +
                                "       IFNULL(AMOUNT_CR, 0.00) AS AMOUNT_CR,\n" +
                                "       IFNULL(AMOUNT_DR, 0.00) AS AMOUNT_DR,\n" +
                                "       IFNULL(ACTUAL_INCOME, 0.00) AS ACTUAL_INCOME,\n" +
                                 "      IFNULL(NARRATION, '') AS NARRATION\n" +
                                "       FROM(\n" +
                                "SELECT GROUP_ID,\n" +
                                "       NATURE_ID,\n" +
                                "       LEDGER_ID,\n" +
                                "       TRANS_MODE,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       BUDGET_GROUP,\n" +
                                "       BUDGET_SUB_GROUP,\n" +
                                "       NARRATION,\n" +
                                "       LEDGER_GROUP,\n" +
                                "       APPROVED_INCOME_PREVIOUS_YR,\n" +
                                "       PROPOSED_INCOME_CURRENT_YR,\n" +
                                "       APPROVED_INCOME_CURRENT_YR,\n" +
                                "      SUM(AMOUNT_CR) AS AMOUNT_CR,\n" +
                                "      SUM(AMOUNT_DR) AS AMOUNT_DR,\n" +
                                "       SUM(ACTUAL_INCOME) AS ACTUAL_INCOME\n" +
                                "  FROM (SELECT ML.GROUP_ID,\n" +
                                "               NATURE_ID,\n" +
                                "               ML.LEDGER_ID,\n" +
                                "               TT.TRANS_MODE,\n" +
                                "               ML.LEDGER_CODE,\n" +
                                "               ML.LEDGER_NAME,\n" +
                                "                 CASE\n" +
                                "               WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                                "                   'Recurring Expenses'\n" +
                                "               WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                                "                    'Non - Recurring Expenses'\n" +
                                "                    ELSE ''\n" +
                                "                END AS BUDGET_GROUP,\n" +
                                "                 CASE\n" +
                                "               WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                                "                   'Regular Expenses'\n" +
                                "               WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                                "                    'Non - Regular Expenses'\n" +
                                "                    ELSE ''\n" +
                                "                END AS BUDGET_SUB_GROUP,\n" +
                                "               ' ' AS NARRATION,\n" +
                                "               IF(MLG.NATURE_ID = 1,\n" +
                                "                  'INCOMES',\n" +
                                "                  IF(MLG.NATURE_ID = 2,\n" +
                                "                     'EXPENSES',\n" +
                                "                     IF(MLG.NATURE_ID = 3, 'ASSETS', 'LIABILITIES'))) AS LEDGER_GROUP,\n" +
                                "               0.00 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                                "               IF(TT.APPROVED_AMOUNT IS NULL, 0.00, TT.APPROVED_AMOUNT) AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                                "                0.00 AS APPROVED_INCOME_CURRENT_YR,\n" +
                                "                 0.00 AS AMOUNT_CR,\n" +
                                "                 0.00 AS AMOUNT_DR,\n" +
                                "               0.00 AS ACTUAL_INCOME\n" +
                                "          FROM MASTER_LEDGER ML\n" +
                                "         INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                "         INNER JOIN BUDGET_LEDGER BL\n" +
                                "            ON BL.LEDGER_ID = ML.LEDGER_ID\n" +
                                "          LEFT JOIN (SELECT LEDGER_ID, APPROVED_AMOUNT, T.BUDGET_ID,TRANS_MODE,PROJECT_ID\n" +
                                "                      FROM (SELECT BM.BUDGET_ID  -- ,PROJECT_ID\n" +
                                "                              FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID \n" +
                                "                             WHERE DATE_FROM <= ?YEAR_FROM\n" +
                                "                             AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID AND PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                             ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T\n" +
                                "                      INNER JOIN BUDGET_PROJECT BP\n" +
                                "                      ON BP.BUDGET_ID = T.BUDGET_ID\n" +
                                "                      LEFT JOIN BUDGET_LEDGER BL\n" +
                                "                        ON T.BUDGET_ID = BL.BUDGET_ID\n" +
                                "                     GROUP BY LEDGER_ID,TRANS_MODE) AS TT\n" +
                                "            ON BL.LEDGER_ID = TT.LEDGER_ID  WHERE TT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "         GROUP BY BL.LEDGER_ID  -- ,TT.TRANS_MODE\n" +
                                "        UNION ALL\n" +
                                "        SELECT GROUP_ID,\n" +
                                "               NATURE_ID,\n" +
                                "               LEDGER_ID,\n" +
                                "               TRANS_MODE,\n" +
                                "               LEDGER_CODE,\n" +
                                "               LEDGER_NAME,\n" +
                                "                 BUDGET_GROUP,\n" +
                                "                 BUDGET_SUB_GROUP,\n" +
                                "               ' ' AS NARRATION,\n" +
                                "               LEDGER_GROUP,\n" +
                                "               0.00 AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                                "               0.00 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                                "               0.00 AS APPROVED_INCOME_CURRENT_YR,\n" +
                                "                 SUM(AMOUNT_CR) AS AMOUNT_CR,\n" +
                                "                 SUM(AMOUNT_DR) AS AMOUNT_DR,\n" +
                            //"               IF(SUM(AMOUNT_DR) > SUM(AMOUNT_CR),\n" +
                            //"                  SUM(AMOUNT_DR) - SUM(AMOUNT_CR),\n" +
                            // "                  SUM(AMOUNT_CR) - SUM(AMOUNT_DR)) AS ACTUAL_INCOME\n" +
                                "               CASE WHEN NATURE_ID IN (1,2) THEN\n" +
                                "               IF(NATURE_ID = 1, SUM(AMOUNT_CR) - SUM(AMOUNT_DR), SUM(AMOUNT_DR) - SUM(AMOUNT_CR))\n" +
                                "               ELSE\n" +
                                "               IF(SUM(AMOUNT_DR) >0,SUM(AMOUNT_DR),SUM(AMOUNT_CR)) END AS ACTUAL_INCOME\n" +
                                "          FROM (SELECT ML.GROUP_ID,\n" +
                                "                       VMT.PROJECT_ID,\n" +
                                "                       VT.VOUCHER_ID,\n" +
                                "                       LEDGER_NAME,\n" +
                                "                     CASE\n" +
                                "                     WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                                "                          'Recurring Expenses'\n" +
                                "                     WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                                "                          'NON - Recurring Expenses'\n" +
                                "                     ELSE ''\n" +
                                "                     END AS BUDGET_GROUP,\n" +
                                "                     CASE\n" +
                                "                        WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                                "                           'Regular Expenses'\n" +
                                "                        WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                                "                           'Non - Regular Expenses'\n" +
                                "                        ELSE ''\n" +
                                "                      END AS BUDGET_SUB_GROUP,\n" +
                                "                       LEDGER_CODE,\n" +
                                "                       VT.LEDGER_ID,\n" +
                                "                       VT.TRANS_MODE,\n" +
                                "                       LEDGER_GROUP,\n" +
                                "                       NATURE_ID,\n" +
                                "                       (CASE\n" +
                                "                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                                "                          VT.AMOUNT\n" +
                                "                         ELSE\n" +
                                "                          0\n" +
                                "                       END) AS AMOUNT_DR,\n" +
                                "\n" +
                                "                       (CASE\n" +
                                "                         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                                "                          VT.AMOUNT\n" +
                                "                         ELSE\n" +
                                "                          0\n" +
                                "                       END) AS AMOUNT_CR\n" +
                                                  "FROM VOUCHER_TRANS VT\n" +
                                "                 INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "                    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                "                   LEFT JOIN BUDGET_PROJECT BP\n" +
                                "                    ON BP.PROJECT_ID = VMT.PROJECT_ID\n" +
                                "                --  LEFT JOIN BUDGET_MASTER BM\n" +
                                "                  --   ON VMT.PROJECT_ID = BM.PROJECT_ID\n" +
                                "                 INNER JOIN MASTER_LEDGER ML\n" +
                                "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "                 INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "                    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                "                 LEFT JOIN BUDGET_LEDGER BL\n" +
                                "                    ON ML.LEDGER_ID=BL.LEDGER_ID\n" +
                                "                 WHERE VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                   AND VMT.STATUS = 1\n" +
                                "                   AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND\n" +
                                "                       ?YEAR_TO\n" +
                                "                 GROUP BY VT.LEDGER_ID, VOUCHER_ID,TRANS_MODE) AS T\n" +
                                "         GROUP BY PROJECT_ID, LEDGER_ID,TRANS_MODE) AS TT\n" +    //,IF(NATURE_ID IN (3,4),TRANS_MODE,'CR')) AS TT\n" +
                                " GROUP BY LEDGER_ID) AS TS1\n" +
                                " RIGHT JOIN PROJECT_LEDGER PL\n" +
                                "    ON PL.LEDGER_ID = TS1.LEDGER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                "  LEFT JOIN MASTER_NATURE MNN\n" +
                                "   ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                                " WHERE PL.PROJECT_ID IN (?PROJECT_ID) AND MLG.NATURE_ID IN (1,2, 3, 4)\n" +
                                " AND ML.GROUP_ID NOT IN (12, 13, 14) AND ML.ACCESS_FLAG NOT IN (2)\n" +
                                " GROUP BY PL.LEDGER_ID  --  ,TRANS_MODE\n" +
                                " ORDER BY LEDGER_CODE ASC,ACTUAL_INCOME, PROPOSED_INCOME_CURRENT_YR";

                        // Commanded on chinna 31.05.2018
                        //query = "SELECT MLG.GROUP_ID,\n" +
                        //       "       MLG.NATURE_ID,\n" +
                        //       "       Ml.LEDGER_ID,\n" +
                        //        "       TRANS_MODE,\n" +
                        //       "       GROUP_CONCAT(PROJECT_ID) AS PROJECT_ID,\n" +
                        //       "       Ml.LEDGER_CODE,\n" +
                        //       "       Ml.LEDGER_NAME,\n" +
                        //       "         CASE\n" +
                        //       "               WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //       "                           'Recurring Expenses'\n" +
                        //       "               WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //       "                            'Non - Recurring Expenses'\n" +
                        //       "                    ELSE ''\n" +
                        //       "                    END AS BUDGET_GROUP,\n" +
                        //       "                  CASE\n" +
                        //       "                    WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //       "                           'Regular Expenses'\n" +
                        //       "                    WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //       "                            'Non - Regular Expenses'\n" +
                        //       "                            ELSE ''\n" +
                        //       "               END AS BUDGET_SUB_GROUP,\n" +
                        //       "       MLG.LEDGER_GROUP,\n" +
                        //       "       MNN.NATURE,\n" +
                        //       "       IFNULL(APPROVED_INCOME_PREVIOUS_YR,0.00) AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //       "       0.00 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                        //       "       0.00 AS APPROVED_INCOME_CURRENT_YR,\n" +
                        //       "       IFNULL(ACTUAL_INCOME, 0.00) AS ACTUAL_INCOME,\n" +
                        //        "      IFNULL(NARRATION, '') AS NARRATION\n" +
                        //       "       FROM(\n" +
                        //       "SELECT GROUP_ID,\n" +
                        //       "       NATURE_ID,\n" +
                        //       "       LEDGER_ID,\n" +
                        //       "       TRANS_MODE,\n" +
                        //       "       LEDGER_CODE,\n" +
                        //       "       LEDGER_NAME,\n" +
                        //       "       BUDGET_GROUP,\n" +
                        //       "       BUDGET_SUB_GROUP,\n" +
                        //       "       NARRATION,\n" +
                        //       "       LEDGER_GROUP,\n" +
                        //       "       APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //       "       PROPOSED_INCOME_CURRENT_YR,\n" +
                        //       "       APPROVED_INCOME_CURRENT_YR,\n" +
                        //       "       SUM(ACTUAL_INCOME) AS ACTUAL_INCOME\n" +
                        //       "  FROM (SELECT ML.GROUP_ID,\n" +
                        //       "               NATURE_ID,\n" +
                        //       "               ML.LEDGER_ID,\n" +
                        //       "               TT.TRANS_MODE,\n" +
                        //       "               ML.LEDGER_CODE,\n" +
                        //       "               ML.LEDGER_NAME,\n" +
                        //       "                 CASE\n" +
                        //       "               WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //       "                   'Recurring Expenses'\n" +
                        //       "               WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //       "                    'Non - Recurring Expenses'\n" +
                        //       "                    ELSE ''\n" +
                        //       "                END AS BUDGET_GROUP,\n" +
                        //       "                 CASE\n" +
                        //       "               WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //       "                   'Regular Expenses'\n" +
                        //       "               WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //       "                    'Non - Regular Expenses'\n" +
                        //       "                    ELSE ''\n" +
                        //       "                END AS BUDGET_SUB_GROUP,\n" +
                        //       "               ' ' AS NARRATION,\n" +
                        //       "               IF(MLG.NATURE_ID = 1,\n" +
                        //       "                  'INCOMES',\n" +
                        //       "                  IF(MLG.NATURE_ID = 2,\n" +
                        //       "                     'EXPENSES',\n" +
                        //       "                     IF(MLG.NATURE_ID = 3, 'ASSETS', 'LIABILITIES'))) AS LEDGER_GROUP,\n" +
                        //       "               0.00 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                        //       "               IF(TT.APPROVED_AMOUNT IS NULL, 0.00, TT.APPROVED_AMOUNT) AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //       "                0.00 AS APPROVED_INCOME_CURRENT_YR,\n" +
                        //       "               0.00 AS ACTUAL_INCOME\n" +
                        //       "          FROM MASTER_LEDGER ML\n" +
                        //       "         INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //       "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //       "         INNER JOIN BUDGET_LEDGER BL\n" +
                        //       "            ON BL.LEDGER_ID = ML.LEDGER_ID\n" +
                        //       "          LEFT JOIN (SELECT LEDGER_ID, APPROVED_AMOUNT, T.BUDGET_ID,TRANS_MODE,PROJECT_ID\n" +
                        //       "                      FROM (SELECT BM.BUDGET_ID  -- ,PROJECT_ID\n" +
                        //       "                              FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID \n" +
                        //       "                             WHERE DATE_FROM <= ?YEAR_FROM\n" +
                        //       "                             AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID AND PROJECT_ID IN (?PROJECT_ID)\n" +
                        //       "                             ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T\n" +
                        //       "                      INNER JOIN BUDGET_PROJECT BP\n" +
                        //       "                      ON BP.BUDGET_ID = T.BUDGET_ID\n" +
                        //       "                      LEFT JOIN BUDGET_LEDGER BL\n" +
                        //       "                        ON T.BUDGET_ID = BL.BUDGET_ID\n" +
                        //       "                     GROUP BY LEDGER_ID,TRANS_MODE) AS TT\n" +
                        //       "            ON BL.LEDGER_ID = TT.LEDGER_ID  WHERE TT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //       "         GROUP BY BL.LEDGER_ID  -- ,TT.TRANS_MODE\n" +
                        //       "        UNION ALL\n" +
                        //       "        SELECT GROUP_ID,\n" +
                        //       "               NATURE_ID,\n" +
                        //       "               LEDGER_ID,\n" +
                        //       "               TRANS_MODE,\n" +
                        //       "               LEDGER_CODE,\n" +
                        //       "               LEDGER_NAME,\n" +
                        //       "                 BUDGET_GROUP,\n" +
                        //       "                 BUDGET_SUB_GROUP,\n" +
                        //       "               ' ' AS NARRATION,\n" +
                        //       "               LEDGER_GROUP,\n" +
                        //       "               0.00 AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //       "               0.00 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                        //       "               0.00 AS APPROVED_INCOME_CURRENT_YR,\n" +
                        //       "               IF(SUM(AMOUNT_DR) > SUM(AMOUNT_CR),\n" +
                        //       "                  SUM(AMOUNT_DR) - SUM(AMOUNT_CR),\n" +
                        //       "                  SUM(AMOUNT_CR) - SUM(AMOUNT_DR)) AS ACTUAL_INCOME\n" +
                        //       "          FROM (SELECT ML.GROUP_ID,\n" +
                        //       "                       VMT.PROJECT_ID,\n" +
                        //       "                       VT.VOUCHER_ID,\n" +
                        //       "                       LEDGER_NAME,\n" +
                        //       "                     CASE\n" +
                        //       "                     WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //       "                          'Recurring Expenses'\n" +
                        //       "                     WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //       "                          'NON - Recurring Expenses'\n" +
                        //       "                     ELSE ''\n" +
                        //       "                     END AS BUDGET_GROUP,\n" +
                        //       "                     CASE\n" +
                        //       "                        WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //       "                           'Regular Expenses'\n" +
                        //       "                        WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //       "                           'Non - Regular Expenses'\n" +
                        //       "                        ELSE ''\n" +
                        //       "                      END AS BUDGET_SUB_GROUP,\n" +
                        //       "                       LEDGER_CODE,\n" +
                        //       "                       VT.LEDGER_ID,\n" +
                        //       "                       VT.TRANS_MODE,\n" +
                        //       "                       LEDGER_GROUP,\n" +
                        //       "                       NATURE_ID,\n" +
                        //       "                       (CASE\n" +
                        //       "                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        //       "                          VT.AMOUNT\n" +
                        //       "                         ELSE\n" +
                        //       "                          0\n" +
                        //       "                       END) AS AMOUNT_DR,\n" +
                        //       "\n" +
                        //       "                       (CASE\n" +
                        //       "                         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        //       "                          VT.AMOUNT\n" +
                        //       "                         ELSE\n" +
                        //       "                          0\n" +
                        //       "                       END) AS AMOUNT_CR\n" +
                        //                         "FROM VOUCHER_TRANS VT\n" +
                        //       "                 INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //       "                    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //       "                   LEFT JOIN BUDGET_PROJECT BP\n" +
                        //       "                    ON BP.PROJECT_ID = VMT.PROJECT_ID\n" +
                        //       "                --  LEFT JOIN BUDGET_MASTER BM\n" +
                        //       "                  --   ON VMT.PROJECT_ID = BM.PROJECT_ID\n" +
                        //       "                 INNER JOIN MASTER_LEDGER ML\n" +
                        //       "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        //       "                 INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //       "                    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //       "                 LEFT JOIN BUDGET_LEDGER BL\n" +
                        //       "                    ON ML.LEDGER_ID=BL.LEDGER_ID\n" +
                        //       "                 WHERE VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //       "                   AND VMT.STATUS = 1\n" +
                        //       "                   AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND\n" +
                        //       "                       ?YEAR_TO\n" +
                        //       "                 GROUP BY VT.LEDGER_ID, VOUCHER_ID) AS T\n" +
                        //       "         GROUP BY PROJECT_ID, LEDGER_ID) AS TT\n" +
                        //       " GROUP BY LEDGER_ID,TRANS_MODE) AS TS1\n" +
                        //       " RIGHT JOIN PROJECT_LEDGER PL\n" +
                        //       "    ON PL.LEDGER_ID = TS1.LEDGER_ID\n" +
                        //       " INNER JOIN MASTER_LEDGER ML\n" +
                        //       "    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                        //       "  LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //       "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //       "  LEFT JOIN MASTER_NATURE MNN\n" +
                        //       "   ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        //       " WHERE PL.PROJECT_ID IN (?PROJECT_ID) AND MLG.NATURE_ID IN (1,2, 3, 4)\n" +
                        //       " AND ML.GROUP_ID NOT IN (12, 13, 14) AND ML.ACCESS_FLAG NOT IN (2)\n" +
                        //       " GROUP BY PL.LEDGER_ID -- ,TRANS_MODE\n" +
                        //       " ORDER BY LEDGER_CODE ASC,ACTUAL_INCOME, PROPOSED_INCOME_CURRENT_YR";
                        break;
                    }

                // chinna 05.05.2018
                case SQLCommand.Budget.AnnualBudgetFetchEdit:
                    {
                        query = "SELECT GROUP_ID,\n" +
                        "       NATURE_ID,\n" +
                        "       NATURE,\n" +
                        "       LEDGER_ID,\n" +
                        "       ACCESS_FLAG,\n" +
                        "       LEDGER_CODE,\n" +
                        "       LEDGER_NAME,\n" +
                        "       BUDGET_GROUP,\n" +
                        "       BUDGET_SUB_GROUP,\n" +
                        "       NARRATION,\n" +
                        "       LEDGER_GROUP,\n" +
                        "       TRANS_MODE,\n" +
                        "       PROPOSED_INCOME_CURRENT_YR_CR,\n" +
                        "       PROPOSED_INCOME_CURRENT_YR_DR,\n" +
                        "       APPROVED_INCOME_CURRENT_YR_CR,\n" +
                        "       APPROVED_INCOME_CURRENT_YR_DR,\n" +
                        "       APPROVED_INCOME_PREVIOUS_YR_CR,\n" +
                        "       APPROVED_INCOME_PREVIOUS_YR_DR,\n" +
                        "       SUM(AMOUNT_CR) AS AMOUNT_CR,\n" +
                        "       SUM(AMOUNT_DR) AS AMOUNT_DR,\n" +
                        "       SUM(ACTUAL_INCOME) AS ACTUAL_INCOME\n" +
                        "  FROM (SELECT GROUP_ID,\n" +
                        "               NATURE_ID,\n" +
                        "               NATURE,\n" +
                        "               PL.LEDGER_ID,\n" +
                        "               PL.ACCESS_FLAG,\n" +
                        "               PL.LEDGER_CODE,\n" +
                        "               PL.LEDGER_NAME,\n" +
                        "               PL.BUDGET_GROUP,\n" +
                        "               PL.BUDGET_SUB_GROUP,\n" +
                        "               NARRATION,\n" +
                        "               PL.LEDGER_GROUP,\n" +
                        "               CUR_BL.TRANS_MODE,\n" +
                        "               SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.PROPOSED_AMOUNT, 0)) AS PROPOSED_INCOME_CURRENT_YR_CR,\n" +
                        "               SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.PROPOSED_AMOUNT, 0)) AS PROPOSED_INCOME_CURRENT_YR_DR,\n" +
                        "               SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_CURRENT_YR_CR,\n" +
                        "               SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_CURRENT_YR_DR,\n" +
                        "               SUM(IF(PRE_BL.TRANS_MODE = 'CR', PRE_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_PREVIOUS_YR_CR,\n" +
                        "               SUM(IF(PRE_BL.TRANS_MODE = 'DR', PRE_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_PREVIOUS_YR_DR,\n" +
                        "               0 AS AMOUNT_CR,\n" +
                        "               0 AS AMOUNT_DR,\n" +
                        "               0 AS ACTUAL_INCOME\n" +
                        "          FROM (SELECT PL.PROJECT_ID,\n" +
                        "                       ML.GROUP_ID,\n" +
                        "                       MLG.NATURE_ID,\n" +
                        "                       MNN.NATURE,\n" +
                        "                       ML.LEDGER_ID,\n" +
                        "                       ML.ACCESS_FLAG,\n" +
                        "                       ML.LEDGER_CODE,\n" +
                        "                       ML.LEDGER_NAME,\n" +
                        "                       CASE\n" +
                        "                         WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        "                          'Recurring Expenses'\n" +
                        "                         WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        "                          'Non - Recurring Expenses'\n" +
                        "                         ELSE\n" +
                        "                          ''\n" +
                        "                       END AS BUDGET_GROUP,\n" +
                        "                       CASE\n" +
                        "                         WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        "                          'Regular Expenses'\n" +
                        "                         WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        "                          'Non - Regular Expenses'\n" +
                        "                         ELSE\n" +
                        "                          ''\n" +
                        "                       END AS BUDGET_SUB_GROUP,\n" +
                        "\n" +
                        "                       IF(MLG.NATURE_ID = 1,\n" +
                        "                          'Incomes',\n" +
                        "                          IF(MLG.NATURE_ID = 2,\n" +
                        "                             'Expenses',\n" +
                        "                             IF(MLG.NATURE_ID = 3, 'Assets', 'Liabilities'))) AS LEDGER_GROUP\n" +
                        "                  FROM MASTER_LEDGER ML\n" +
                        "                 INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "                    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        "                 INNER JOIN MASTER_NATURE MNN\n" +
                        "                    ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        "                 INNER JOIN PROJECT_LEDGER PL\n" +
                        "                    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                        "                 WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "                 GROUP BY ML.LEDGER_ID) AS PL\n" +
                        "          LEFT JOIN (SELECT BL.LEDGER_ID,\n" +
                        "                           BL.NARRATION,\n" +
                        "                           BL.TRANS_MODE,\n" +
                        "                           BL.PROPOSED_AMOUNT,\n" +
                        "                           BL.APPROVED_AMOUNT\n" +
                        "                      FROM BUDGET_LEDGER BL\n" +
                        "                     WHERE BL.BUDGET_ID IN (?BUDGET_ID)) AS CUR_BL\n" +
                        "            ON CUR_BL.LEDGER_ID = PL.LEDGER_ID\n" +
                        "          LEFT JOIN (SELECT LEDGER_ID,\n" +
                        "                           PROPOSED_AMOUNT,\n" +
                        "                           APPROVED_AMOUNT,\n" +
                        "                           T.BUDGET_ID,\n" +
                        "                           TRANS_MODE\n" +
                        "                      FROM (SELECT BUDGET_ID\n" +
                        "                              FROM BUDGET_MASTER BM\n" +
                        "                             WHERE DATE_FROM <=?YEAR_FROM\n" +
                        "                               AND DATE_TO >=?YEAR_TO\n" +
                        "                             ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T\n" +
                        "                      LEFT JOIN BUDGET_LEDGER BL\n" +
                        "                        ON T.BUDGET_ID = BL.BUDGET_ID) AS PRE_BL\n" +
                        "            ON PRE_BL.LEDGER_ID = PL.LEDGER_ID\n" +
                        "         WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "         GROUP BY PL.LEDGER_ID\n" +
                        "\n" +
                        "        UNION ALL\n" +
                        "\n" +
                        "        SELECT GROUP_ID,\n" +
                        "               NATURE_ID,\n" +
                        "               NATURE,\n" +
                        "               LEDGER_ID,\n" +
                        "               ACCESS_FLAG,\n" +
                        "               LEDGER_CODE,\n" +
                        "               LEDGER_NAME,\n" +
                        "               BUDGET_GROUP,\n" +
                        "               BUDGET_SUB_GROUP,\n" +
                        "               ' ' AS NARRATION,\n" +
                        "               LEDGER_GROUP,\n" +
                        "               TRANS_MODE AS TRANS_MODE,\n" +
                        "               0 AS PROPOSED_INCOME_CURRENT_YR_CR,\n" +
                        "               0 AS PROPOSED_INCOME_CURRENT_YR_DR,\n" +
                        "               0 AS APPROVED_INCOME_CURRENT_YR_CR,\n" +
                        "               0 AS APPROVED_INCOME_CURRENT_YR_DR,\n" +
                        "               0 AS APPROVED_INCOME_PREVIOUS_YR_CR,\n" +
                        "               0 AS APPROVED_INCOME_PREVIOUS_YR_DR,\n" +
                        "               SUM(AMOUNT_CR) AS AMOUNT_CR,\n" +
                        "               SUM(AMOUNT_DR) AS AMOUNT_DR,\n" +
                        "               CASE\n" +
                        "                 WHEN NATURE_ID IN (1, 2) THEN\n" +
                        "                  IF(NATURE_ID = 1,\n" +
                        "                     SUM(AMOUNT_CR) - SUM(AMOUNT_DR),\n" +
                        "                     SUM(AMOUNT_DR) - SUM(AMOUNT_CR))\n" +
                        "                 ELSE\n" +
                        "                  IF(SUM(AMOUNT_DR) > 0, SUM(AMOUNT_DR), SUM(AMOUNT_CR))\n" +
                        "               END AS ACTUAL_INCOME\n" +
                        "          FROM (SELECT ML.GROUP_ID,\n" +
                        "                       MLG.NATURE_ID,\n" +
                        "                       VMT.PROJECT_ID,\n" +
                        "                       VT.VOUCHER_ID,\n" +
                        "                       MNN.NATURE,\n" +
                        "                       LEDGER_NAME,\n" +
                        "                       CASE\n" +
                        "                         WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        "                          'Recurring Expenses'\n" +
                        "                         WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        "                          'Non - Recurring Expenses'\n" +
                        "                         ELSE\n" +
                        "                          ''\n" +
                        "                       END AS BUDGET_GROUP,\n" +
                        "                       CASE\n" +
                        "                         WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        "                          'Regular Expenses'\n" +
                        "                         WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        "                          'Non - Regular Expenses'\n" +
                        "                         ELSE\n" +
                        "                          ''\n" +
                        "                       END AS BUDGET_SUB_GROUP,\n" +
                        "                       LEDGER_CODE,\n" +
                        "                       IF(MLG.NATURE_ID = 1,\n" +
                        "                          'Incomes',\n" +
                        "                          IF(MLG.NATURE_ID = 2,\n" +
                        "                             'Expenses',\n" +
                        "                             IF(MLG.NATURE_ID = 3, 'Assets', 'Liabilities'))) AS LEDGER_GROUP,\n" +
                        "                       VT.TRANS_MODE,\n" +
                        "                       VT.LEDGER_ID,\n" +
                        "                       ML.ACCESS_FLAG,\n" +
                        "                       (CASE\n" +
                        "                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        "                          (VT.AMOUNT)\n" +
                        "                         ELSE\n" +
                        "                          0\n" +
                        "                       END) AS AMOUNT_DR,\n" +
                        "\n" +
                        "                       (CASE\n" +
                        "                         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        "                          (VT.AMOUNT)\n" +
                        "                         ELSE\n" +
                        "                          0\n" +
                        "                       END) AS AMOUNT_CR,\n" +
                        "\n" +
                        "                       (CASE\n" +
                        "                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        "                          VT.AMOUNT\n" +
                        "                         ELSE\n" +
                        "                          -VT.AMOUNT\n" +
                        "                       END) AS AMOUNT\n" +
                        "                  FROM VOUCHER_TRANS VT\n" +
                        "                 INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        "                    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "                  LEFT JOIN BUDGET_PROJECT BP\n" +
                        "                    ON BP.PROJECT_ID = VMT.PROJECT_ID\n" +
                        "                 INNER JOIN MASTER_LEDGER ML\n" +
                        "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "                 INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "                    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        "                  LEFT JOIN MASTER_NATURE MNN\n" +
                        "                    ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        "                  LEFT JOIN BUDGET_LEDGER BL\n" +
                        "                    ON ML.LEDGER_ID = BL.LEDGER_ID\n" +
                        "                 WHERE VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "                   AND VMT.STATUS = 1\n" +
                        "                   AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND\n" +
                        "                        ?YEAR_TO\n" +
                        "                 GROUP BY VT.LEDGER_ID, VOUCHER_ID) AS T\n" +
                        "         GROUP BY PROJECT_ID, LEDGER_ID) AS TT\n" +
                        " WHERE ACCESS_FLAG NOT IN (2)\n" +
                        " GROUP BY TT.LEDGER_ID\n" +
                        " ORDER BY LEDGER_CODE ASC,\n" +
                        "          PROPOSED_INCOME_CURRENT_YR_CR,\n" +
                        "          PROPOSED_INCOME_CURRENT_YR_DR,\n" +
                        "          ACTUAL_INCOME";

                        // commanded on 04.06.2018 at 11 AM

                        //query = "SELECT GROUP_ID,NATURE_ID,NATURE,\n" +
                        //           "       LEDGER_ID,\n" +
                        //           "       ACCESS_FLAG,\n" +
                        //           "       LEDGER_CODE,\n" +
                        //           "       LEDGER_NAME,\n" +
                        //           "       BUDGET_GROUP,\n" +
                        //           "       BUDGET_SUB_GROUP,\n" +
                        //           "       NARRATION,\n" +
                        //           "       LEDGER_GROUP,\n" +
                        //           "       TRANS_MODE,\n" +
                        //           "       PROPOSED_INCOME_CURRENT_YR,\n" +
                        //           "       PROPOSED_INCOME_PREVIOUS_YR,\n" +
                        //           "       APPROVED_INCOME_CURRENT_YR,\n" +
                        //           "       APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //           "       SUM(AMOUNT_CR) AS AMOUNT_CR,\n" +
                        //           "       SUM(AMOUNT_DR) AS AMOUNT_DR,\n" +
                        //           "       SUM(ACTUAL_INCOME) AS ACTUAL_INCOME\n" +
                        //           "  FROM (SELECT ML.GROUP_ID,MLG.NATURE_ID,MNN.NATURE,\n" +
                        //           "               ML.LEDGER_ID,\n" +
                        //           "               ML.ACCESS_FLAG,\n" +
                        //           "               ML.LEDGER_CODE,\n" +
                        //           "               ML.LEDGER_NAME,\n" +
                        //           "          CASE\n" +
                        //           "          WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //           "           'Recurring Expenses'\n" +
                        //           "          WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //           "           'Non - Recurring Expenses'\n" +
                        //           "          ELSE ''\n" +
                        //           "          END AS BUDGET_GROUP,\n" +
                        //           "          CASE\n" +
                        //           "            WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //           "               'Regular Expenses'\n" +
                        //           "            WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //           "               'Non - Regular Expenses'\n" +
                        //           "            ELSE ''\n" +
                        //           "          END AS BUDGET_SUB_GROUP,\n" +
                        //           "               NARRATION,\n" +
                        //           "               IF(MLG.NATURE_ID = 1,\n" +
                        //           "                  'Incomes',\n" +
                        //           "                  IF(MLG.NATURE_ID = 2,\n" +
                        //           "                     'Expenses',\n" +
                        //           "                     IF(MLG.NATURE_ID = 3, 'Assets', 'Liabilities'))) AS LEDGER_GROUP, BL.TRANS_MODE,\n" +
                        //           "               IF(BL.PROPOSED_AMOUNT IS NULL, 0.00, BL.PROPOSED_AMOUNT) AS PROPOSED_INCOME_CURRENT_YR,\n" +
                        //           "               IF(TT.PROPOSED_AMOUNT IS NULL, 0.00, TT.PROPOSED_AMOUNT) AS PROPOSED_INCOME_PREVIOUS_YR,\n" +
                        //           "                IF(BL.APPROVED_AMOUNT IS NULL, 0.00, BL.APPROVED_AMOUNT) AS APPROVED_INCOME_CURRENT_YR,\n" +
                        //           "                IF(TT.APPROVED_AMOUNT IS NULL, 0.00, TT.APPROVED_AMOUNT) AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //           "                0 AS AMOUNT_CR,\n" +
                        //           "                0 AS AMOUNT_DR,\n" +
                        //           "               0 AS ACTUAL_INCOME\n" +
                        //           "          FROM MASTER_LEDGER ML\n" +
                        //           "         INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //           "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //           "         INNER JOIN MASTER_NATURE MNN\n" +
                        //           "                ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        //           "         INNER JOIN PROJECT_LEDGER PL\n" +
                        //           "            ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                        //           "         LEFT JOIN (SELECT BL.LEDGER_ID,BL.NARRATION,BL.TRANS_MODE,BL.PROPOSED_AMOUNT,BL.APPROVED_AMOUNT FROM BUDGET_LEDGER BL WHERE BL.BUDGET_ID IN (?BUDGET_ID)) AS BL\n" +
                        //           "            ON BL.LEDGER_ID = PL.LEDGER_ID\n" +
                        //           "          LEFT JOIN (SELECT LEDGER_ID, PROPOSED_AMOUNT,APPROVED_AMOUNT, T.BUDGET_ID,TRANS_MODE,PROJECT_ID\n" +
                        //           "                      FROM (SELECT BUDGET_ID\n" +
                        //           "                              FROM BUDGET_MASTER BM\n" +
                        //           "                             WHERE DATE_FROM <= ?YEAR_FROM and DATE_TO >= ?YEAR_TO\n" +
                        //           "                             ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T\n" +
                        //           "                    INNER JOIN BUDGET_PROJECT BP\n" +
                        //           "                      ON BP.BUDGET_ID = T.BUDGET_ID\n" +
                        //           "                      LEFT JOIN BUDGET_LEDGER BL\n" +
                        //           "                        ON T.BUDGET_ID = BL.BUDGET_ID\n" +
                        //           "          WHERE BP.PROJECT_ID IN (?PROJECT_ID) AND BP.BUDGET_ID = T.BUDGET_ID\n" +
                        //           "                     GROUP BY LEDGER_ID,TRANS_MODE) AS TT\n" +
                        //           "            ON BL.LEDGER_ID = TT.LEDGER_ID\n" +
                        //           "            AND BL.TRANS_MODE = TT.TRANS_MODE\n" +
                        //           "            WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //           "         GROUP BY ML.LEDGER_ID   -- ,TRANS_MODE\n" +
                        //           "\n" +
                        //           "        UNION ALL\n" +
                        //           "\n" +
                        //           "        SELECT GROUP_ID,NATURE_ID,NATURE,\n" +
                        //           "               LEDGER_ID,\n" +
                        //           "              ACCESS_FLAG,\n" +
                        //           "               LEDGER_CODE,\n" +
                        //           "               LEDGER_NAME,\n" +
                        //           "                 BUDGET_GROUP,\n" +
                        //           "                 BUDGET_SUB_GROUP,\n" +
                        //           "               ' ' AS NARRATION,\n" +
                        //           "               LEDGER_GROUP,\n" +
                        //           "               TRANS_MODE AS TRANS_MODE,\n" +
                        //           "               0 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                        //           "               0 AS PROPOSED_INCOME_PREVIOUS_YR,\n" +
                        //           "               0 AS APPROVED_INCOME_CURRENT_YR,\n" +
                        //           "               0 AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //           "           SUM(AMOUNT_CR) AS AMOUNT_CR,\n" +
                        //           "           SUM(AMOUNT_DR) AS AMOUNT_DR,\n" +
                        //           "         CASE WHEN NATURE_ID IN (1,2) THEN\n" +
                        //           "         IF(NATURE_ID = 1, SUM(AMOUNT_CR) - SUM(AMOUNT_DR), SUM(AMOUNT_DR) - SUM(AMOUNT_CR))\n" +
                        //           "         ELSE\n" +
                        //           "         IF(SUM(AMOUNT_DR) >0,SUM(AMOUNT_DR),SUM(AMOUNT_CR)) END AS ACTUAL_INCOME\n" +
                        //           "          FROM (SELECT ML.GROUP_ID,MLG.NATURE_ID,VMT.PROJECT_ID,VT.VOUCHER_ID,MNN.NATURE,\n" +
                        //           "                       LEDGER_NAME,\n" +
                        //           "              CASE\n" +
                        //           "              WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //           "               'Recurring Expenses'\n" +
                        //           "              WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //           "               'Non - Recurring Expenses'\n" +
                        //           "              ELSE ''\n" +
                        //           "              END AS BUDGET_GROUP,\n" +
                        //           "              CASE\n" +
                        //           "                 WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //           "                   'Regular Expenses'\n" +
                        //           "                 WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //           "                   'Non - Regular Expenses'\n" +
                        //           "                 ELSE ''\n" +
                        //           "          END AS BUDGET_SUB_GROUP,\n" +
                        //           "                       LEDGER_CODE,\n" +
                        //           "                       IF(MLG.NATURE_ID = 1,\n" +
                        //           "                          'Incomes',\n" +
                        //           "                          IF(MLG.NATURE_ID = 2,\n" +
                        //           "                             'Expenses',\n" +
                        //           "                             IF(MLG.NATURE_ID = 3, 'Assets', 'Liabilities'))) AS LEDGER_GROUP, VT.TRANS_MODE,\n" +
                        //           "                       VT.LEDGER_ID, ML.ACCESS_FLAG,\n" +
                        //           "                       (CASE\n" +
                        //           "                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        //           "                          (VT.AMOUNT)\n" +
                        //           "                         ELSE\n" +
                        //           "                          0\n" +
                        //           "                       END) AS AMOUNT_DR,\n" +
                        //           "\n" +
                        //           "                       (CASE\n" +
                        //           "                         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        //           "                          (VT.AMOUNT)\n" +
                        //           "                         ELSE\n" +
                        //           "                          0\n" +
                        //           "                       END) AS AMOUNT_CR,\n" +
                        //           "\n" +
                        //           "                       (CASE\n" +
                        //           "                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        //           "                          VT.AMOUNT\n" +
                        //           "                         ELSE\n" +
                        //           "                          -VT.AMOUNT\n" +
                        //           "                       END) AS AMOUNT\n" +
                        //           "                  FROM VOUCHER_TRANS VT\n" +
                        //           "          INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //           "             ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //           "            LEFT JOIN BUDGET_PROJECT BP\n" +
                        //           "             ON BP.PROJECT_ID = VMT.PROJECT_ID\n" +
                        //           "          INNER JOIN MASTER_LEDGER ML\n" +
                        //           "             ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        //           "          INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //           "             ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //           "          LEFT JOIN MASTER_NATURE MNN\n" +
                        //           "            ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        //           "          LEFT JOIN BUDGET_LEDGER BL\n" +
                        //           "             ON ML.LEDGER_ID=BL.LEDGER_ID\n" +
                        //           "          WHERE VMT.PROJECT_ID IN (9,10)\n" +
                        //           "            AND VMT.STATUS = 1\n" +
                        //           "              AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND\n" +
                        //           "                  ?YEAR_TO\n" +
                        //           "                 GROUP BY VT.LEDGER_ID,VOUCHER_ID) AS T\n" +
                        //           "         GROUP BY PROJECT_ID,LEDGER_ID) AS TT WHERE ACCESS_FLAG NOT IN (2)\n" +
                        //           " GROUP BY TT.LEDGER_ID -- ,TRANS_MODE \n " +
                        //           " ORDER BY LEDGER_CODE ASC,PROPOSED_INCOME_CURRENT_YR,ACTUAL_INCOME";

                        // changes done on 01.06.2017
                        //query = "SELECT GROUP_ID,NATURE_ID,NATURE,\n" +
                        //"       LEDGER_ID,\n" +
                        //"       ACCESS_FLAG,\n" +
                        //"       LEDGER_CODE,\n" +
                        //"       LEDGER_NAME,\n" +
                        //"       BUDGET_GROUP,\n" +
                        //"       BUDGET_SUB_GROUP,\n" +
                        //"       NARRATION,\n" +
                        //"       LEDGER_GROUP,\n" +
                        //"       TRANS_MODE,\n" +
                        //"       PROPOSED_INCOME_CURRENT_YR,\n" +
                        //"       PROPOSED_INCOME_PREVIOUS_YR,\n" +
                        //"       APPROVED_INCOME_CURRENT_YR,\n" +
                        //"       APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //"       SUM(ACTUAL_INCOME) AS AMOUNT\n" +
                        //"  FROM (SELECT ML.GROUP_ID,MLG.NATURE_ID,MNN.NATURE,\n" +
                        //"               ML.LEDGER_ID,\n" +
                        //"               ML.ACCESS_FLAG,\n" +
                        //"               ML.LEDGER_CODE,\n" +
                        //"               ML.LEDGER_NAME,\n" +
                        //"          CASE\n" +
                        //"          WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //"           'Recurring Expenses'\n" +
                        //"          WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //"           'Non - Recurring Expenses'\n" +
                        //"          ELSE ''\n" +
                        //"          END AS BUDGET_GROUP,\n" +
                        //"          CASE\n" +
                        //"            WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //"               'Regular Expenses'\n" +
                        //"            WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //"               'Non - Regular Expenses'\n" +
                        //"            ELSE ''\n" +
                        //"          END AS BUDGET_SUB_GROUP,\n" +
                        //"               NARRATION,\n" +
                        //"               IF(MLG.NATURE_ID = 1,\n" +
                        //"                  'Incomes',\n" +
                        //"                  IF(MLG.NATURE_ID = 2,\n" +
                        //"                     'Expenses',\n" +
                        //"                     IF(MLG.NATURE_ID = 3, 'Assets', 'Liabilities'))) AS LEDGER_GROUP, BL.TRANS_MODE,\n" +
                        //"               IF(BL.PROPOSED_AMOUNT IS NULL, 0.00, BL.PROPOSED_AMOUNT) AS PROPOSED_INCOME_CURRENT_YR,\n" +
                        //"               IF(TT.PROPOSED_AMOUNT IS NULL, 0.00, TT.PROPOSED_AMOUNT) AS PROPOSED_INCOME_PREVIOUS_YR,\n" +
                        //"                IF(BL.APPROVED_AMOUNT IS NULL, 0.00, BL.APPROVED_AMOUNT) AS APPROVED_INCOME_CURRENT_YR,\n" +
                        //"                IF(TT.APPROVED_AMOUNT IS NULL, 0.00, TT.APPROVED_AMOUNT) AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //"               0 AS ACTUAL_INCOME\n" +
                        //"          FROM MASTER_LEDGER ML\n" +
                        //"         INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //"            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //"         INNER JOIN MASTER_NATURE MNN\n" +
                        //"                ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        //"         INNER JOIN PROJECT_LEDGER PL\n" +
                        //"            ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                        //"         LEFT JOIN (SELECT BL.LEDGER_ID,BL.NARRATION,BL.TRANS_MODE,BL.PROPOSED_AMOUNT,BL.APPROVED_AMOUNT FROM BUDGET_LEDGER BL WHERE BL.BUDGET_ID IN (?BUDGET_ID)) AS BL\n" +
                        //"            ON BL.LEDGER_ID = PL.LEDGER_ID\n" +
                        //"          LEFT JOIN (SELECT LEDGER_ID, PROPOSED_AMOUNT,APPROVED_AMOUNT, T.BUDGET_ID,TRANS_MODE,PROJECT_ID\n" +
                        //"                      FROM (SELECT BUDGET_ID\n" +
                        //"                              FROM BUDGET_MASTER BM\n" +
                        //"                             WHERE DATE_FROM <= ?YEAR_FROM and DATE_TO >= ?YEAR_TO\n" +
                        //"                             ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T\n" +
                        //"                    INNER JOIN BUDGET_PROJECT BP\n" +
                        //"                      ON BP.BUDGET_ID = T.BUDGET_ID\n" +
                        //"                      LEFT JOIN BUDGET_LEDGER BL\n" +
                        //"                        ON T.BUDGET_ID = BL.BUDGET_ID\n" +
                        //"          WHERE BP.PROJECT_ID IN (?PROJECT_ID) AND BP.BUDGET_ID = T.BUDGET_ID\n" +
                        //"                     GROUP BY LEDGER_ID,TRANS_MODE) AS TT\n" +
                        //"            ON BL.LEDGER_ID = TT.LEDGER_ID\n" +
                        //"            AND BL.TRANS_MODE = TT.TRANS_MODE\n" +
                        //"            WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //"         GROUP BY ML.LEDGER_ID  -- ,TRANS_MODE\n" +
                        //"\n" +
                        //"        UNION ALL\n" +
                        //"\n" +
                        //"        SELECT GROUP_ID,NATURE_ID,NATURE,\n" +
                        //"               LEDGER_ID,\n" +
                        //"              ACCESS_FLAG,\n" +
                        //"               LEDGER_CODE,\n" +
                        //"               LEDGER_NAME,\n" +
                        //"                 BUDGET_GROUP,\n" +
                        //"                 BUDGET_SUB_GROUP,\n" +
                        //"               ' ' AS NARRATION,\n" +
                        //"               LEDGER_GROUP,\n" +
                        //"               TRANS_MODE AS TRANS_MODE,\n" +
                        //"               0 AS PROPOSED_INCOME_CURRENT_YR,\n" +
                        //"               0 AS PROPOSED_INCOME_PREVIOUS_YR,\n" +
                        //"               0 AS APPROVED_INCOME_CURRENT_YR,\n" +
                        //"               0 AS APPROVED_INCOME_PREVIOUS_YR,\n" +
                        //"               IF(AMOUNT_DR > AMOUNT_CR,\n" +
                        //"                  AMOUNT_DR - AMOUNT_CR,\n" +
                        //"                  AMOUNT_CR - AMOUNT_DR) AS ACTUAL_INCOME\n" +
                        //"          FROM (SELECT ML.GROUP_ID,MLG.NATURE_ID,MNN.NATURE,\n" +
                        //"                       LEDGER_NAME,\n" +
                        //"              CASE\n" +
                        //"              WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //"               'Recurring Expenses'\n" +
                        //"              WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //"               'Non - Recurring Expenses'\n" +
                        //"              ELSE ''\n" +
                        //"              END AS BUDGET_GROUP,\n" +
                        //"              CASE\n" +
                        //"                 WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //"                   'Regular Expenses'\n" +
                        //"                 WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //"                   'Non - Regular Expenses'\n" +
                        //"                 ELSE ''\n" +
                        //"          END AS BUDGET_SUB_GROUP,\n" +
                        //"                       LEDGER_CODE,\n" +
                        //"                       IF(MLG.NATURE_ID = 1,\n" +
                        //"                          'Incomes',\n" +
                        //"                          IF(MLG.NATURE_ID = 2,\n" +
                        //"                             'Expenses',\n" +
                        //"                             IF(MLG.NATURE_ID = 3, 'Assets', 'Liabilities'))) AS LEDGER_GROUP, VT.TRANS_MODE,\n" +
                        //"                       VT.LEDGER_ID, ML.ACCESS_FLAG,\n" +
                        //"                       (CASE\n" +
                        //"                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        //"                          SUM(VT.AMOUNT)\n" +
                        //"                         ELSE\n" +
                        //"                          0\n" +
                        //"                       END) AS AMOUNT_DR,\n" +
                        //"\n" +
                        //"                       (CASE\n" +
                        //"                         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        //"                          SUM(VT.AMOUNT)\n" +
                        //"                         ELSE\n" +
                        //"                          0\n" +
                        //"                       END) AS AMOUNT_CR,\n" +
                        //"\n" +
                        //"                       (CASE\n" +
                        //"                         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        //"                          VT.AMOUNT\n" +
                        //"                         ELSE\n" +
                        //"                          -VT.AMOUNT\n" +
                        //"                       END) AS AMOUNT\n" +
                        //"                  FROM BUDGET_LEDGER BL\n" +
                        //"                 INNER JOIN MASTER_LEDGER ML\n" +
                        //"                    ON BL.LEDGER_ID = ML.LEDGER_ID\n" +
                        //"                 INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //"                    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //"                 INNER JOIN VOUCHER_TRANS VT\n" +
                        //"                    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //"                 INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //"                    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //"                 LEFT JOIN MASTER_NATURE MNN\n" +
                        //"               ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        //"                 WHERE VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //"                   AND VMT.STATUS = 1\n" +
                        //"                   AND BL.BUDGET_ID = ?BUDGET_ID\n" +
                        //"                   AND ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                        //"                   AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND\n" +
                        //"                       ?YEAR_TO\n" +
                        //"                 GROUP BY LEDGER_ID) AS T\n" +
                        //"         GROUP BY LEDGER_ID) AS TT WHERE ACCESS_FLAG NOT IN (2)\n" +
                        //" GROUP BY TT.LEDGER_ID -- ,TRANS_MODE \n " +
                        //" ORDER BY LEDGER_CODE ASC,PROPOSED_INCOME_CURRENT_YR,ACTUAL_INCOME";
                        break;
                    }
                case SQLCommand.Budget.BudgetAddEditDetails:
                    {
                        query = "SELECT GROUP_ID,\n" +
                            "       NATURE_ID,\n" +
                            "       NATURE,\n" +
                            "       PL.LEDGER_ID,\n" +
                            "       PL.ACCESS_FLAG,\n" +
                            "       PL.LEDGER_CODE,\n" +
                            "       PL.LEDGER_NAME,\n" +
                            "       PL.BUDGET_GROUP, PL.BUDGET_GROUP_SORT_ID,\n" +
                            "       ''  AS BUDGET_SUB_GROUP,  -- PL.BUDGET_SUB_GROUP,\n" +
                            "       PL.LEDGER_GROUP,\n" + // 24/04/2025 CHINNA UPPER
                            "       UPPER(CUR_BL.TRANS_MODE) AS BUDGET_TRANS_MODE, IF(CUR_BL.TRANS_MODE='CR',' Income', 'Expenses') AS BUDGET_NATURE,\n" + // Income Ledgers', 'Expenses Ledgers'
                            "     SUM(IFNULL(CUR_BL.PROPOSED_AMOUNT,0)) AS PROPOSED_CURRENT_YR,\n" +
                            "\n" +
                            " --       CASE\n" +
                            " --        WHEN NATURE_ID IN (1, 3, 4) AND 'CR' = ?BUDGET_TRANS_MODE THEN\n" +
                            " --         SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.APPROVED_AMOUNT, 0))\n" +
                            " --        WHEN NATURE_ID IN (2, 3, 4) AND 'DR' = ?BUDGET_TRANS_MODE THEN\n" +
                            " --         SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.APPROVED_AMOUNT, 0))\n" +
                            " --      END AS APPROVED_CURRENT_YR,\n" +
                            "      SUM(IFNULL(CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_CURRENT_YR,\n" +
                            "\n" +
                            " --      CASE\n" +
                            " --        WHEN NATURE_ID IN (1, 3, 4) AND 'CR' = ?BUDGET_TRANS_MODE THEN\n" +
                            " --         SUM(IF(PRE_BL.TRANS_MODE = 'CR', PRE_BL.APPROVED_AMOUNT, 0))\n" +
                            " --        WHEN NATURE_ID IN (2, 3, 4) AND 'DR' = ?BUDGET_TRANS_MODE THEN\n" +
                            " --         SUM(IF(PRE_BL.TRANS_MODE = 'DR', PRE_BL.APPROVED_AMOUNT, 0))\n" +
                            " --      END AS APPROVED_PREVIOUS_YR,\n" +
                            "     SUM(IFNULL(PRE_BL.APPROVED_AMOUNT, 0)) AS APPROVED_PREVIOUS_YR,\n" +
                            "\n" +
                            "       CASE\n" +
                            "         WHEN NATURE_ID IN (1, 2) THEN\n" +
                            "          IF(NATURE_ID = 1,\n" +
                            "             IFNULL(AMOUNT_CR, 0) - IFNULL(AMOUNT_DR, 0),\n" +
                            "             IFNULL(AMOUNT_DR, 0) - IFNULL(AMOUNT_CR, 0))\n" +
                            // 23/04/2025, *Chinna
                            "  --       ELSE\n" +
                            "  --      IF('CR' = ?BUDGET_TRANS_MODE, IFNULL(ACTUAL.AMOUNT_CR, 0),IFNULL(ACTUAL.AMOUNT_DR, 0))\n" +
                            " WHEN NATURE_ID IN (3, 4) THEN \n" +
                            "  CASE \n" +
                            "  WHEN PL.TRANS_MODE = 'DR' THEN IFNULL(AMOUNT_DR, 0) \n" +
                            "  WHEN PL.TRANS_MODE = 'CR' THEN IFNULL(AMOUNT_CR, 0)\n" +
                            "  ELSE 0 END END AS ACTUAL,\n" +
                            "       CUR_BL.NARRATION,\n" +
                            "       CUR_BL.HO_NARRATION\n" +
                            "-- SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.PROPOSED_AMOUNT, 0)) AS PROPOSED_INCOME_CURRENT_YR_CR,\n" +
                            "-- SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.PROPOSED_AMOUNT, 0)) AS PROPOSED_INCOME_CURRENT_YR_DR,\n" +
                            "-- SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_CURRENT_YR_CR,\n" +
                            "-- SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_CURRENT_YR_DR,\n" +
                            "\n" +
                            "  FROM (SELECT PL.PROJECT_ID,\n" +
                            "               ML.GROUP_ID,\n" +
                            "               MLG.NATURE_ID,\n" +
                            "               MNN.NATURE,\n" +
                            "               ML.LEDGER_ID,\n" +
                            "               ML.ACCESS_FLAG,\n" +
                            "               ML.LEDGER_CODE,\n" +
                            "               ML.LEDGER_NAME,\n" +
                            "               ML.BUDGET_GROUP_ID,\n" +
                            "               ML.BUDGET_SUB_GROUP_ID,\n" +
                            "               IFNULL(BG.BUDGET_GROUP, 'Others') AS BUDGET_GROUP,\n" +
                            "               IFNULL(BG.BUDGET_GROUP_SORT_ID, '7') AS BUDGET_GROUP_SORT_ID,\n" +
                            "               IFNULL(BSG.BUDGET_SUB_GROUP, '') AS BUDGET_SUB_GROUP,\n" +
                            "               BL.TRANS_MODE,\n" +
                            "               MLG.LEDGER_GROUP\n" +
                            "          FROM MASTER_LEDGER ML\n" +
                            "         INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                            "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                            "         INNER JOIN MASTER_NATURE MNN\n" +
                            "            ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                            "         INNER JOIN PROJECT_LEDGER PL\n" +
                            "            ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                            "         LEFT JOIN BUDGET_GROUP BG ON BG.BUDGET_GROUP_ID = ML.BUDGET_GROUP_ID\n" +
                            "         LEFT JOIN BUDGET_SUB_GROUP BSG ON BSG.BUDGET_SUB_GROUP_ID = ML.BUDGET_SUB_GROUP_ID\n" +
                            "         INNER JOIN BUDGET_LEDGER BL" +
                            "            ON BL.LEDGER_ID = PL.LEDGER_ID AND BL.BUDGET_ID =?BUDGET_ID" +
                            "         WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                            "         GROUP BY ML.LEDGER_ID, BL.TRANS_MODE) AS PL\n" +
                            "  LEFT JOIN (SELECT BL.LEDGER_ID,\n" +
                            "                    BL.NARRATION, BL.HO_NARRATION,\n" +
                            "                    BL.TRANS_MODE,\n" +
                            "                    BL.PROPOSED_AMOUNT,\n" +
                            "                    BL.APPROVED_AMOUNT\n" +
                            "               FROM BUDGET_LEDGER BL\n" +
                            "              WHERE BL.BUDGET_ID IN (?BUDGET_ID) ) AS CUR_BL\n" +
                            "    ON CUR_BL.LEDGER_ID = PL.LEDGER_ID AND CUR_BL.TRANS_MODE = PL.TRANS_MODE\n" +
                            //"  LEFT JOIN (SELECT BL.LEDGER_ID, BL.NARRATION,BL.HO_NARRATION, BL.TRANS_MODE\n" +
                            //"               FROM BUDGET_LEDGER BL\n" +
                            //"              WHERE BL.BUDGET_ID IN (?BUDGET_ID)\n" +
                            //"                AND BL.TRANS_MODE = ?BUDGET_TRANS_MODE) AS CUR_NAR\n" +
                            //"    ON CUR_NAR.LEDGER_ID = PL.LEDGER_ID\n" +
                            "  LEFT JOIN (SELECT LEDGER_ID,\n" +
                            "                    PROPOSED_AMOUNT,\n" +
                            "                    APPROVED_AMOUNT,\n" +
                            "                    T.BUDGET_ID,\n" +
                            "                    TRANS_MODE\n" +
                            "               FROM (SELECT BM.BUDGET_ID,BM.DATE_FROM,BM.DATE_TO\n" +
                            "                       FROM BUDGET_MASTER BM\n" +
                            // This is to get the desc based on the project
                            "             INNER JOIN BUDGET_PROJECT BP\n" +
                            "               ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                            "                      WHERE DATE_FROM <=?YEAR_FROM AND DATE_TO>=?YEAR_TO AND BP.PROJECT_ID IN (?PROJECT_ID)\n" +
                            "                      ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T\n" +
                            "               LEFT JOIN BUDGET_LEDGER BL\n" +
                            "               ON T.BUDGET_ID = BL.BUDGET_ID\n" +
                            "    INNER JOIN BUDGET_PROJECT MP ON MP.BUDGET_ID = BL.BUDGET_ID WHERE MP.PROJECT_ID IN (?PROJECT_ID) GROUP BY BL.LEDGER_ID, BL.TRANS_MODE) AS PRE_BL\n" +
                            "    ON PRE_BL.LEDGER_ID = PL.LEDGER_ID AND PRE_BL.TRANS_MODE = PL.TRANS_MODE\n" +
                            "  LEFT JOIN (SELECT VT.LEDGER_ID,\n" +
                            "                    SUM(IF(VT.TRANS_MODE = 'DR', VT.AMOUNT, 0)) AS AMOUNT_DR,\n" +
                            "                    SUM(IF(VT.TRANS_MODE = 'CR', VT.AMOUNT, 0)) AS AMOUNT_CR\n" +
                            "               FROM VOUCHER_TRANS VT\n" +
                            "              INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                            "                 ON VT.BRANCH_ID = VMT.BRANCH_ID AND VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                            "                   AND VT.LOCATION_ID = VMT.LOCATION_ID\n" +
                            "              WHERE VMT.BRANCH_ID IN (?BRANCH_ID) AND VMT.PROJECT_ID IN (?PROJECT_ID) { AND VMT.VOUCHER_TYPE NOT IN (?VOUCHER_TYPE) }\n" +
                            "                AND VMT.STATUS = 1\n" +
                            "                AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND\n" +
                            "                    ?YEAR_TO\n" +
                            "              GROUP BY VT.LEDGER_ID) AS ACTUAL\n" +
                            "    ON ACTUAL.LEDGER_ID = PL.LEDGER_ID\n" +
                            " WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                            "   -- AND IF('CR' = ?BUDGET_TRANS_MODE, NATURE_ID IN (1, 3, 4), NATURE_ID IN (2, 3, 4))\n" +
                            "   AND PL.ACCESS_FLAG NOT IN (2) AND GROUP_ID NOT IN(12,13,14)\n" +
                            " GROUP BY PL.LEDGER_ID, CUR_BL.TRANS_MODE ORDER BY PL.LEDGER_GROUP, PL.LEDGER_CODE";

                        //query = "SELECT GROUP_ID,\n" +
                        //   "       NATURE_ID,\n" +
                        //   "       NATURE,\n" +
                        //   "       PL.LEDGER_ID,\n" +
                        //   "       PL.ACCESS_FLAG,\n" +
                        //   "       PL.LEDGER_CODE,\n" +
                        //   "       PL.LEDGER_NAME,\n" +
                        //   "       PL.BUDGET_GROUP,\n" +
                        //   "       PL.BUDGET_SUB_GROUP,\n" +
                        //   "       PL.LEDGER_GROUP,\n" +
                        //   "       CUR_BL.TRANS_MODE AS BUDGET_TRANS_MODE, IF(CUR_BL.TRANS_MODE='CR',' Income Ledgers', 'Expenses Ledgers') AS BUDGET_NATURE,\n" +
                        //   "     SUM(IFNULL(CUR_BL.PROPOSED_AMOUNT,0)) AS PROPOSED_CURRENT_YR,\n" +
                        //   "\n" +
                        //   " --       CASE\n" +
                        //   " --        WHEN NATURE_ID IN (1, 3, 4) AND 'CR' = ?BUDGET_TRANS_MODE THEN\n" +
                        //   " --         SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.APPROVED_AMOUNT, 0))\n" +
                        //   " --        WHEN NATURE_ID IN (2, 3, 4) AND 'DR' = ?BUDGET_TRANS_MODE THEN\n" +
                        //   " --         SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.APPROVED_AMOUNT, 0))\n" +
                        //   " --      END AS APPROVED_CURRENT_YR,\n" +
                        //   "      SUM(IFNULL(CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_CURRENT_YR,\n" +
                        //   "\n" +
                        //   " --      CASE\n" +
                        //   " --        WHEN NATURE_ID IN (1, 3, 4) AND 'CR' = ?BUDGET_TRANS_MODE THEN\n" +
                        //   " --         SUM(IF(PRE_BL.TRANS_MODE = 'CR', PRE_BL.APPROVED_AMOUNT, 0))\n" +
                        //   " --        WHEN NATURE_ID IN (2, 3, 4) AND 'DR' = ?BUDGET_TRANS_MODE THEN\n" +
                        //   " --         SUM(IF(PRE_BL.TRANS_MODE = 'DR', PRE_BL.APPROVED_AMOUNT, 0))\n" +
                        //   " --      END AS APPROVED_PREVIOUS_YR,\n" +
                        //   "     SUM(IFNULL(PRE_BL.APPROVED_AMOUNT, 0)) AS APPROVED_PREVIOUS_YR,\n" +
                        //   "\n" +
                        //   "       CASE\n" +
                        //   "         WHEN NATURE_ID IN (1, 2) THEN\n" +
                        //   "          IF(NATURE_ID = 1,\n" +
                        //   "             IFNULL(AMOUNT_CR, 0) - IFNULL(AMOUNT_DR, 0),\n" +
                        //   "             IFNULL(AMOUNT_DR, 0) - IFNULL(AMOUNT_CR, 0))\n" +
                        //   "         ELSE\n" +
                        //   "      IF('CR' = ?BUDGET_TRANS_MODE, IFNULL(ACTUAL.AMOUNT_CR, 0),IFNULL(ACTUAL.AMOUNT_DR, 0))\n" +
                        //   "       END AS ACTUAL,\n" +
                        //   "       CUR_BL.NARRATION,\n" +
                        //   "       CUR_BL.HO_NARRATION\n" +
                        //   "-- SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.PROPOSED_AMOUNT, 0)) AS PROPOSED_INCOME_CURRENT_YR_CR,\n" +
                        //   "-- SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.PROPOSED_AMOUNT, 0)) AS PROPOSED_INCOME_CURRENT_YR_DR,\n" +
                        //   "-- SUM(IF(CUR_BL.TRANS_MODE = 'CR', CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_CURRENT_YR_CR,\n" +
                        //   "-- SUM(IF(CUR_BL.TRANS_MODE = 'DR', CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_INCOME_CURRENT_YR_DR,\n" +
                        //   "\n" +
                        //   "  FROM (SELECT PL.PROJECT_ID,\n" +
                        //   "               ML.GROUP_ID,\n" +
                        //   "               MLG.NATURE_ID,\n" +
                        //   "               MNN.NATURE,\n" +
                        //   "               ML.LEDGER_ID,\n" +
                        //   "               ML.ACCESS_FLAG,\n" +
                        //   "               ML.LEDGER_CODE,\n" +
                        //   "               ML.LEDGER_NAME, BL.TRANS_MODE,\n" +
                        //   "               CASE\n" +
                        //   "                 WHEN ML.BUDGET_GROUP_ID = 1 THEN\n" +
                        //   "                  'Recurring Expenses'\n" +
                        //   "                 WHEN ML.BUDGET_GROUP_ID = 2 THEN\n" +
                        //   "                  'Non - Recurring Expenses'\n" +
                        //   "                 ELSE\n" +
                        //   "                  ''\n" +
                        //   "               END AS BUDGET_GROUP,\n" +
                        //   "               CASE\n" +
                        //   "                 WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN\n" +
                        //   "                  '   Regular Expenses'\n" +
                        //   "                 WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN\n" +
                        //   "                  '   Non - Regular Expenses'\n" +
                        //   "                 ELSE\n" +
                        //   "                  ''\n" +
                        //   "               END AS BUDGET_SUB_GROUP, MLG.LEDGER_GROUP\n" +
                        //    //"               IF(MLG.NATURE_ID = 1,\n" +
                        //    //"                  'Incomes',\n" +
                        //    //"                  IF(MLG.NATURE_ID = 2,\n" +
                        //    //"                     'Expenses',\n" +
                        //    //"                     IF(MLG.NATURE_ID = 3, 'Assets', 'Liabilities'))) AS LEDGER_GROUP\n" +
                        //   "          FROM MASTER_LEDGER ML\n" +
                        //   "         INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //   "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //   "         INNER JOIN MASTER_NATURE MNN\n" +
                        //   "            ON MNN.NATURE_ID = MLG.NATURE_ID\n" +
                        //   "         INNER JOIN PROJECT_LEDGER PL\n" +
                        //   "            ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                        //   "         INNER JOIN BUDGET_LEDGER BL" +
                        //   "            ON BL.LEDGER_ID = PL.LEDGER_ID AND BL.BUDGET_ID =?BUDGET_ID" +
                        //   "         WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //   "         GROUP BY ML.LEDGER_ID, BL.TRANS_MODE) AS PL\n" +
                        //   "  LEFT JOIN (SELECT BL.LEDGER_ID,\n" +
                        //   "                    BL.NARRATION, BL.HO_NARRATION,\n" +
                        //   "                    BL.TRANS_MODE,\n" +
                        //   "                    BL.PROPOSED_AMOUNT,\n" +
                        //   "                    BL.APPROVED_AMOUNT\n" +
                        //   "               FROM BUDGET_LEDGER BL\n" +
                        //   "              WHERE BL.BUDGET_ID IN (?BUDGET_ID) ) AS CUR_BL\n" +
                        //   "    ON CUR_BL.LEDGER_ID = PL.LEDGER_ID AND CUR_BL.TRANS_MODE = PL.TRANS_MODE\n" +
                        //    //"  LEFT JOIN (SELECT BL.LEDGER_ID, BL.NARRATION,BL.HO_NARRATION, BL.TRANS_MODE\n" +
                        //    //"               FROM BUDGET_LEDGER BL\n" +
                        //    //"              WHERE BL.BUDGET_ID IN (?BUDGET_ID)\n" +
                        //    //"                AND BL.TRANS_MODE = ?BUDGET_TRANS_MODE) AS CUR_NAR\n" +
                        //    //"    ON CUR_NAR.LEDGER_ID = PL.LEDGER_ID\n" +
                        //   "  LEFT JOIN (SELECT LEDGER_ID,\n" +
                        //   "                    PROPOSED_AMOUNT,\n" +
                        //   "                    APPROVED_AMOUNT,\n" +
                        //   "                    T.BUDGET_ID,\n" +
                        //   "                    TRANS_MODE\n" +
                        //   "               FROM (SELECT BM.BUDGET_ID,BM.DATE_FROM,BM.DATE_TO\n" +
                        //   "                       FROM BUDGET_MASTER BM\n" +
                        //    // This is to get the desc based on the project
                        //   "             INNER JOIN BUDGET_PROJECT BP\n" +
                        //   "               ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                        //   "                      WHERE DATE_FROM <=?YEAR_FROM AND DATE_TO>=?YEAR_TO AND BP.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //   "                      ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T\n" +
                        //   "               LEFT JOIN BUDGET_LEDGER BL\n" +
                        //   "               ON T.BUDGET_ID = BL.BUDGET_ID\n" +
                        //   "    INNER JOIN BUDGET_PROJECT MP ON MP.BUDGET_ID = BL.BUDGET_ID WHERE MP.PROJECT_ID IN (?PROJECT_ID) GROUP BY BL.LEDGER_ID, BL.TRANS_MODE) AS PRE_BL\n" +
                        //   "    ON PRE_BL.LEDGER_ID = PL.LEDGER_ID AND PRE_BL.TRANS_MODE = PL.TRANS_MODE\n" +
                        //   "  LEFT JOIN (SELECT VT.LEDGER_ID,\n" +
                        //   "                    SUM(IF(VT.TRANS_MODE = 'DR', VT.AMOUNT, 0)) AS AMOUNT_DR,\n" +
                        //   "                    SUM(IF(VT.TRANS_MODE = 'CR', VT.AMOUNT, 0)) AS AMOUNT_CR\n" +
                        //   "               FROM VOUCHER_TRANS VT\n" +
                        //   "              INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //   "                 ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //   "              WHERE VMT.BRANCH_ID IN (?BRANCH_ID) AND VMT.PROJECT_ID IN (?PROJECT_ID) { AND VMT.VOUCHER_TYPE NOT IN (?VOUCHER_TYPE) }\n" +
                        //   "                AND VMT.STATUS = 1\n" +
                        //   "                AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND\n" +
                        //   "                    ?YEAR_TO\n" +
                        //   "              GROUP BY VT.LEDGER_ID) AS ACTUAL\n" +
                        //   "    ON ACTUAL.LEDGER_ID = PL.LEDGER_ID\n" +
                        //   " WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //   "   -- AND IF('CR' = ?BUDGET_TRANS_MODE, NATURE_ID IN (1, 3, 4), NATURE_ID IN (2, 3, 4))\n" +
                        //   "   AND PL.ACCESS_FLAG NOT IN (2) AND GROUP_ID NOT IN(12,13,14)\n" +
                        //   " GROUP BY PL.LEDGER_ID, CUR_BL.TRANS_MODE ORDER BY PL.LEDGER_CODE";

                        break;
                    }

                case SQLCommand.Budget.BudgetAddEditDetailsProposals:
                    {
                        query = @"SELECT GROUP_ID,
                                       NATURE_ID,
                                       NATURE,
                                       PL.LEDGER_ID,
                                       PL.ACCESS_FLAG,
                                       PL.LEDGER_CODE,
                                       PL.LEDGER_NAME,
                                       PL.BUDGET_GROUP,
                                       PL.BUDGET_SUB_GROUP,
                                       PL.LEDGER_GROUP,
                                       PL.TRANS_MODE AS BUDGET_TRANS_MODE, IF(PL.TRANS_MODE='CR',' Income Ledgers', 'Expenses Ledgers') AS BUDGET_NATURE,
                                      -- CUR_BL.TRANS_MODE AS BUDGET_TRANS_MODE, IF(NATURE_ID IN (1,3,4),' Income Ledgers', 'Expenses Ledgers') AS BUDGET_NATURE,
                                     SUM(IFNULL(CUR_BL.PROPOSED_AMOUNT,0)) AS PROPOSED_CURRENT_YR,

                                      SUM(IFNULL(CUR_BL.APPROVED_AMOUNT, 0)) AS APPROVED_CURRENT_YR,

                                     SUM(IFNULL(PRE_BL.APPROVED_AMOUNT, 0)) AS APPROVED_PREVIOUS_YR,

                                       CASE
                                         WHEN NATURE_ID IN (1, 2) THEN
                                          IF(NATURE_ID = 1,
                                             IFNULL(AMOUNT_CR, 0) - IFNULL(AMOUNT_DR, 0),
                                             IFNULL(AMOUNT_DR, 0) - IFNULL(AMOUNT_CR, 0))
                                         ELSE
                                      IF(PL.TRANS_MODE='CR', IFNULL(ACTUAL.AMOUNT_CR, 0),IFNULL(ACTUAL.AMOUNT_DR, 0))
                                       END AS ACTUAL,
                                       CUR_BL.NARRATION,
                                       CUR_BL.HO_NARRATION
                                  FROM (SELECT MP.PROJECT_ID,
                                               ML.GROUP_ID,
                                               MLG.NATURE_ID,
                                               MNN.NATURE,
                                               ML.LEDGER_ID,
                                               ML.ACCESS_FLAG,
                                               ML.LEDGER_CODE,
                                               ML.LEDGER_NAME,
                                               ML.BUDGET_GROUP_ID,
                                               ML.BUDGET_SUB_GROUP_ID,
                                               IFNULL(BG.BUDGET_GROUP, '') AS BUDGET_GROUP,
                                               IFNULL(BSG.BUDGET_SUB_GROUP, '') AS BUDGET_SUB_GROUP,
                                               IF(MLG.NATURE_ID=2,'DR','CR') AS  TRANS_MODE,
                                               MLG.LEDGER_GROUP
                                          FROM MASTER_LEDGER ML
                                         INNER JOIN MASTER_LEDGER_GROUP MLG
                                            ON ML.GROUP_ID = MLG.GROUP_ID
                                         INNER JOIN MASTER_NATURE MNN
                                            ON MNN.NATURE_ID = MLG.NATURE_ID
                                        INNER JOIN PROJECT_CATEGORY_LEDGER PCL
                                            ON PCL.LEDGER_ID = ML.LEDGER_ID
                                         INNER JOIN MASTER_PROJECT MP
                                            ON MP.PROJECT_CATEGORY_ID = PCL.PROJECT_CATEGORY_ID
                                         LEFT JOIN BUDGET_GROUP BG ON BG.BUDGET_GROUP_ID = ML.BUDGET_GROUP_ID
                                         LEFT JOIN BUDGET_SUB_GROUP BSG ON BSG.BUDGET_SUB_GROUP_ID = ML.BUDGET_SUB_GROUP_ID
                                        --  LEFT JOIN BUDGET_LEDGER BL ON BL.LEDGER_ID = PCL.LEDGER_ID 
                                        WHERE MP.PROJECT_ID IN (?PROJECT_ID)
                                        --  GROUP BY ML.LEDGER_ID,BL.TRANS_MODE
                                        GROUP BY ML.LEDGER_ID
                                     UNION ALL
                                     SELECT MP.PROJECT_ID,
                                               ML.GROUP_ID,
                                               MLG.NATURE_ID,
                                               MNN.NATURE,
                                               ML.LEDGER_ID,
                                               ML.ACCESS_FLAG,
                                               ML.LEDGER_CODE,
                                               ML.LEDGER_NAME,
                                               ML.BUDGET_GROUP_ID,
                                               ML.BUDGET_SUB_GROUP_ID,
                                               IFNULL(BG.BUDGET_GROUP, '') AS BUDGET_GROUP,
                                               IFNULL(BSG.BUDGET_SUB_GROUP, '') AS BUDGET_SUB_GROUP,
                                               'DR' AS TRANS_MODE,
                                               MLG.LEDGER_GROUP
                                          FROM MASTER_LEDGER ML
                                         INNER JOIN MASTER_LEDGER_GROUP MLG
                                            ON ML.GROUP_ID = MLG.GROUP_ID
                                         INNER JOIN MASTER_NATURE MNN
                                            ON MNN.NATURE_ID = MLG.NATURE_ID
                                        INNER JOIN PROJECT_CATEGORY_LEDGER PCL
                                            ON PCL.LEDGER_ID = ML.LEDGER_ID
                                         INNER JOIN MASTER_PROJECT MP
                                            ON MP.PROJECT_CATEGORY_ID = PCL.PROJECT_CATEGORY_ID
                                         LEFT JOIN BUDGET_GROUP BG ON BG.BUDGET_GROUP_ID = ML.BUDGET_GROUP_ID
                                         LEFT JOIN BUDGET_SUB_GROUP BSG ON BSG.BUDGET_SUB_GROUP_ID = ML.BUDGET_SUB_GROUP_ID
                                         LEFT JOIN BUDGET_LEDGER BL ON BL.LEDGER_ID = PCL.LEDGER_ID WHERE MLG.NATURE_ID IN (3,4) AND MP.PROJECT_ID IN (?PROJECT_ID)
                                         -- GROUP BY ML.LEDGER_ID, BL.TRANS_MODE) AS PL
                                         GROUP BY ML.LEDGER_ID) AS PL
                                  LEFT JOIN (SELECT BL.LEDGER_ID,
                                                    BL.NARRATION, BL.HO_NARRATION,
                                                    BL.TRANS_MODE,
                                                    BL.PROPOSED_AMOUNT,
                                                    BL.APPROVED_AMOUNT
                                               FROM BUDGET_LEDGER BL LEFT JOIN BUDGET_MASTER BM ON BM.BUDGET_ID = BL.BUDGET_ID
                                              WHERE BL.BUDGET_ID IN (?BUDGET_ID)) AS CUR_BL
                                    ON CUR_BL.LEDGER_ID = PL.LEDGER_ID AND CUR_BL.TRANS_MODE = PL.TRANS_MODE
                                  LEFT JOIN (SELECT LEDGER_ID,
                                                    PROPOSED_AMOUNT,
                                                    APPROVED_AMOUNT,
                                                    T.BUDGET_ID,
                                                    TRANS_MODE
                                               FROM (SELECT BM.BUDGET_ID,BM.DATE_FROM,BM.DATE_TO
                                                       FROM BUDGET_MASTER BM
                                             INNER JOIN BUDGET_PROJECT BP
                                               ON BP.BUDGET_ID = BM.BUDGET_ID
                                                      WHERE DATE_FROM <=?YEAR_FROM AND DATE_TO>=?YEAR_TO AND BP.PROJECT_ID IN (?PROJECT_ID)
                                                      ORDER BY DATE_FROM, DATE_TO DESC LIMIT 1) AS T
                                               LEFT JOIN BUDGET_LEDGER BL
                                               ON T.BUDGET_ID = BL.BUDGET_ID
                                    INNER JOIN BUDGET_PROJECT MP ON MP.BUDGET_ID = BL.BUDGET_ID WHERE MP.PROJECT_ID IN (?PROJECT_ID) GROUP BY BL.LEDGER_ID, BL.TRANS_MODE) AS PRE_BL
                                    ON PRE_BL.LEDGER_ID = PL.LEDGER_ID AND PRE_BL.TRANS_MODE = PL.TRANS_MODE
                                  LEFT JOIN (SELECT VT.LEDGER_ID,
                                                    SUM(IF(VT.TRANS_MODE = 'DR', VT.AMOUNT, 0)) AS AMOUNT_DR,
                                                    SUM(IF(VT.TRANS_MODE = 'CR', VT.AMOUNT, 0)) AS AMOUNT_CR
                                               FROM VOUCHER_TRANS VT
                                              INNER JOIN VOUCHER_MASTER_TRANS VMT
                                                 ON VT.BRANCH_ID = VMT.BRANCH_ID AND VT.VOUCHER_ID = VMT.VOUCHER_ID
                                                   AND VT.LOCATION_ID = VMT.LOCATION_ID
                                              WHERE VMT.BRANCH_ID IN (87) AND VMT.PROJECT_ID IN (?PROJECT_ID)  { AND VMT.VOUCHER_TYPE NOT IN (?VOUCHER_TYPE) }
                                                AND VMT.STATUS = 1
                                                AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND
                                                    ?YEAR_TO
                                              GROUP BY VT.LEDGER_ID) AS ACTUAL
                                    ON ACTUAL.LEDGER_ID = PL.LEDGER_ID
                                 WHERE PL.PROJECT_ID IN (?PROJECT_ID)
                                   -- AND IF('CR' = 'DR', NATURE_ID IN (1, 3, 4), NATURE_ID IN (2, 3, 4))
                                    AND PL.ACCESS_FLAG NOT IN (2) AND GROUP_ID NOT IN(12,13,14)
                                    GROUP BY PL.LEDGER_ID, PL.TRANS_MODE
                                    ORDER BY PL.LEDGER_GROUP, PL.LEDGER_CODE";
                        break;
                    }

                case SQLCommand.Budget.BudgetMysoreDetails:
                    {
                        query = @"SELECT ML.SORT_ID,
                                       PL.PROJECT_ID,
                                       ML.GROUP_ID,
                                       MLG.NATURE_ID,
                                       MNN.NATURE,
                                       ML.LEDGER_ID,
                                       0 AS SUB_LEDGER_ID,
                                       ML.ACCESS_FLAG,
                                       'DR' AS BUDGET_TRANS_MODE,
                                       ML.LEDGER_CODE,
                                       ML.LEDGER_NAME AS MAIN_LEDGER_NAME,
                                       ML.LEDGER_NAME,
                                       0 AS 'SELECT',
                                       IF(IFNULL(M1.BUDGET_ID || M2.BUDGET_ID, 0) = 0, 0, 1) AS IS_ALLOTED,
                                       IF(ML.BUDGET_GROUP_ID = 2, 0, 1) AS REC_NONREC,
                                       CAST(CONCAT(ML.LEDGER_ID, '.', 0) as DECIMAL(15, 2)) AS BIND_LEDGER_ID,
                                       0 As ActionDelete,
                                       0 as ActionInsert,
                                       0 AS IS_SUB_LEDGER,
                                       '' AS SUB_LEDGER_NAME,
                                       '' AS NEW_SUB_LEDGER_NAME, 0.0 AS NEW_SUB_LEDGER_AMOUNT, '' AS NEW_SUB_LEDGER_NARRATION, '' AS TMP_SUB_LEDGER_ID,
                                       MLG.LEDGER_GROUP,
                                       IFNULL(M1.PROPOSED_AMOUNT, 0) AS PROPOSED_CURRENT_YR,
                                       IFNULL(M1.APPROVED_AMOUNT, 0) AS APPROVED_CURRENT_YR,
                                       IFNULL(PREV_BUDGET.PROPOSED_AMOUNT, 0) AS PREV_PROPOSED_AMOUNT,
                                       IFNULL(PREV_BUDGET.APPROVED_AMOUNT, 0) AS APPROVED_PREVIOUS_YR,
                                       IFNULL(PREV_ACTUAL.PREV_ACTUAL_SPENT, 0) - IFNULL(PREV_SUB_LEDGER_ACTUAL.AMOUNT,0) AS ACTUAL,
                                       IFNULL(PREV_BUDGET.APPROVED_AMOUNT, 0) - (IFNULL(PREV_ACTUAL.PREV_ACTUAL_SPENT, 0)-IFNULL(PREV_SUB_LEDGER_ACTUAL.AMOUNT,0)) AS BALANCE,
                                       IFNULL(M2.PROPOSED_AMOUNT, 0) AS M2_PROPOSED_AMOUNT,
                                       IFNULL(M2.APPROVED_AMOUNT, 0) AS M2_APPROVED_AMOUNT,
                                       IFNULL(M1.NARRATION, '') AS NARRATION,
                                       IFNULL(M1.HO_NARRATION, '') AS HO_NARRATION,
                                       CASE
                                         WHEN ML.BUDGET_GROUP_ID = 1 THEN
                                          'Recurring Expenses'
                                         WHEN ML.BUDGET_GROUP_ID = 2 THEN
                                          'Non - Recurring Expenses'
                                         ELSE
                                          ''
                                       END AS BUDGET_GROUP,
                                       CASE
                                         WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN
                                          'Regular Expenses'
                                         WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN
                                          'Non - Regular Expenses'
                                         ELSE
                                          ''
                                       END AS BUDGET_SUB_GROUP,
                                       ML.BUDGET_GROUP_ID,
                                       ML.BUDGET_SUB_GROUP_ID
                                  FROM MASTER_LEDGER ML
                                 INNER JOIN MASTER_LEDGER_GROUP MLG
                                    ON ML.GROUP_ID = MLG.GROUP_ID
                                 INNER JOIN MASTER_NATURE MNN
                                    ON MNN.NATURE_ID = MLG.NATURE_ID
                                 INNER JOIN PROJECT_LEDGER PL
                                    ON PL.LEDGER_ID = ML.LEDGER_ID
                                 INNER JOIN PROJECT_BUDGET_LEDGER PBL
                                    ON PBL.LEDGER_ID = PL.LEDGER_ID
                                   AND PBL.PROJECT_ID = PL.PROJECT_ID
                                  LEFT JOIN (SELECT BM.BUDGET_ID,
                                                    BL.LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    BL.PROPOSED_AMOUNT,
                                                    BL.APPROVED_AMOUNT,
                                                    BL.NARRATION,
                                                    Bl.HO_NARRATION,
                                                    0 AS SUB_LEDGER_ALONE
                                               FROM BUDGET_MASTER BM
                                               LEFT JOIN BUDGET_LEDGER BL
                                                 ON BM.BUDGET_ID = BL.BUDGET_ID
                                              WHERE BM.BUDGET_ID IN (?MONTH1_BUDGET_ID)
                                                AND BL.TRANS_MODE = ?BUDGET_TRANS_MODE
                                             -- Un Budgetd Legers (Sub Ledgers alone)
                                             UNION ALL
                                             SELECT BM.BUDGET_ID,
                                                    BSL.LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    0 AS PROPOSED_AMOUNT,
                                                    0 AS APPROVED_AMOUNT,
                                                    '' AS NARRATION,
                                                    '' AS HO_NARRATION,
                                                    1 AS SUB_LEDGER_ALONE
                                               FROM BUDGET_MASTER BM
                                              INNER JOIN BUDGET_SUB_LEDGER BSL
                                                 ON BM.BUDGET_ID = BSL.BUDGET_ID
                                               LEFT JOIN BUDGET_LEDGER BL
                                                 ON BL.BUDGET_ID = BM.BUDGET_ID
                                                AND BL.LEDGER_ID = BSL.LEDGER_ID
                                              WHERE BM.BUDGET_ID IN (?MONTH1_BUDGET_ID)
                                                AND BSL.TRANS_MODE = ?BUDGET_TRANS_MODE
                                                AND BL.LEDGER_ID IS NULL
                                              GROUP BY LEDGER_ID) AS M1
                                    ON M1.LEDGER_ID = ML.LEDGER_ID
                                  LEFT JOIN (SELECT BM.BUDGET_ID,
                                                    BL.LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    BL.PROPOSED_AMOUNT,
                                                    BL.APPROVED_AMOUNT,
                                                    BL.NARRATION,
                                                    Bl.HO_NARRATION,
                                                    0 AS SUB_LEDGER_ALONE
                                               FROM BUDGET_MASTER BM
                                               LEFT JOIN BUDGET_LEDGER BL
                                                 ON BM.BUDGET_ID = BL.BUDGET_ID
                                              WHERE BM.BUDGET_ID IN (?MONTH2_BUDGET_ID)
                                                AND BL.TRANS_MODE = ?BUDGET_TRANS_MODE
                                             -- Un Budgetd Legers (Sub Ledgers alone)
                                             UNION ALL
                                             SELECT BM.BUDGET_ID,
                                                    BSL.LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    0 AS PROPOSED_AMOUNT,
                                                    0 AS APPROVED_AMOUNT,
                                                    '' AS NARRATION,
                                                    '' AS HO_NARRATION,
                                                    1 AS SUB_LEDGER_ALONE
                                               FROM BUDGET_MASTER BM
                                              inner JOIN BUDGET_SUB_LEDGER BSL
                                                 ON BM.BUDGET_ID = BSL.BUDGET_ID
                                               LEFT JOIN BUDGET_LEDGER BL
                                                 ON BL.BUDGET_ID = BM.BUDGET_ID
                                                AND BL.LEDGER_ID = BSL.LEDGER_ID
                                              WHERE BM.BUDGET_ID IN (?MONTH2_BUDGET_ID)
                                                AND BSL.TRANS_MODE = ?BUDGET_TRANS_MODE
                                                AND BL.LEDGER_ID IS NULL
                                              GROUP BY LEDGER_ID) AS M2
                                    ON M2.LEDGER_ID = ML.LEDGER_ID
                                  LEFT JOIN (SELECT BM.BUDGET_ID,
                                                    BL.LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    BL.PROPOSED_AMOUNT,
                                                    BL.APPROVED_AMOUNT,
                                                    BL.NARRATION
                                               FROM BUDGET_MASTER BM
                                               LEFT JOIN BUDGET_LEDGER BL
                                                 ON BM.BUDGET_ID = BL.BUDGET_ID
                                              WHERE BM.BUDGET_ID IN (?PREVIOUS_BUDGET_ID)
                                                AND BL.TRANS_MODE = ?BUDGET_TRANS_MODE) AS PREV_BUDGET
                                    ON PREV_BUDGET.LEDGER_ID = ML.LEDGER_ID
                                  LEFT JOIN (SELECT VT.LEDGER_ID, SUM(VT.AMOUNT) AS PREV_ACTUAL_SPENT
                                               FROM VOUCHER_TRANS VT
                                              INNER JOIN VOUCHER_MASTER_TRANS VMT
                                                 ON VT.VOUCHER_ID = VMT.VOUCHER_ID
                                              WHERE VMT.PROJECT_ID IN (?PROJECT_ID)
                                                AND (VT.TRANS_MODE = ?BUDGET_TRANS_MODE AND
                                                    VMT.VOUCHER_TYPE IN ('RC', 'PY'))
                                                AND VMT.STATUS = 1 AND VMT.BRANCH_ID IN (?BRANCH_ID)
                                                AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO
                                              GROUP BY VT.LEDGER_ID) AS PREV_ACTUAL
                                    ON PREV_ACTUAL.LEDGER_ID = ML.LEDGER_ID
                                   LEFT JOIN (SELECT VT.LEDGER_ID, SUM(VSL.AMOUNT) AS AMOUNT
                                               FROM VOUCHER_TRANS VT
                                              INNER JOIN VOUCHER_MASTER_TRANS VMT
                                                 ON VT.VOUCHER_ID = VMT.VOUCHER_ID
                                              INNER JOIN VOUCHER_SUB_LEDGER_TRANS VSL
                                                 ON VSL.LEDGER_ID = VT.LEDGER_ID AND VSL.VOUCHER_ID=VT.VOUCHER_ID
                                              WHERE VMT.PROJECT_ID IN (?PROJECT_ID)
                                                AND (VT.TRANS_MODE = 'DR' AND
                                                    VMT.VOUCHER_TYPE IN ('RC', 'PY'))
                                                AND VMT.STATUS = 1 AND VMT.BRANCH_ID IN (?BRANCH_ID)
                                                AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO
                                              GROUP BY VT.LEDGER_ID) AS PREV_SUB_LEDGER_ACTUAL   
                                        ON PREV_SUB_LEDGER_ACTUAL.LEDGER_ID = ML.LEDGER_ID
                                 WHERE PL.PROJECT_ID IN (?PROJECT_ID) AND ML.GROUP_ID NOT IN(12,13,14) AND ((M1.PROPOSED_AMOUNT>0 OR M1.APPROVED_AMOUNT>0) OR (M2.PROPOSED_AMOUNT>0 OR M2.APPROVED_AMOUNT>0) OR M1.SUB_LEDGER_ALONE=1 OR M2.SUB_LEDGER_ALONE=1 OR BUDGET_GROUP_ID=1)
                                 GROUP BY ML.LEDGER_ID
                                UNION ALL
                                SELECT ML.SORT_ID,
                                       PL.PROJECT_ID,
                                       ML.GROUP_ID,
                                       MLG.NATURE_ID,
                                       MNN.NATURE,
                                       ML.LEDGER_ID,
                                       MSL.SUB_LEDGER_ID,
                                       ML.ACCESS_FLAG,
                                       'DR' AS BUDGET_TRANS_MODE,
                                       ML.LEDGER_CODE,
                                       ML.LEDGER_NAME AS MAIN_LEDGER_NAME,
                                       -- CONCAT('  ->', MSL.SUB_LEDGER_NAME, CONCAT(' - ', ML.LEDGER_NAME)) AS LEDGER_NAME,
                                       CONCAT('  ', MSL.SUB_LEDGER_NAME) AS LEDGER_NAME,
                                       0 AS 'SELECT',
                                       IF(IFNULL(M1.BUDGET_ID || M2.BUDGET_ID, 0) = 0, 0, 1) AS IS_ALLOTED,
                                       IF(ML.BUDGET_GROUP_ID = 2, 0, 1) AS REC_NONREC,
                                       CAST(CONCAT(ML.LEDGER_ID, '.', MSL.SUB_LEDGER_ID) as DECIMAL(15, 2)) AS BIND_LEDGER_ID,
                                       0 As ActionDelete,
                                       0 as ActionInsert,
                                       1 AS IS_SUB_LEDGER,
                                       MSL.SUB_LEDGER_NAME,
                                       '' AS NEW_SUB_LEDGER_NAME, 0.0 AS NEW_SUB_LEDGER_AMOUNT, '' AS NEW_SUB_LEDGER_NARRATION, '' AS TMP_SUB_LEDGER_ID,
                                       MLG.LEDGER_GROUP,
                                       IFNULL(M1.PROPOSED_AMOUNT, 0) AS PROPOSED_CURRENT_YR,
                                       IFNULL(M1.APPROVED_AMOUNT, 0) AS APPROVED_CURRENT_YR,
                                       IFNULL(PREV_BUDGET.PROPOSED_AMOUNT, 0) AS PREV_PROPOSED_AMOUNT,
                                       IFNULL(PREV_BUDGET.APPROVED_AMOUNT, 0) AS APPROVED_PREVIOUS_YR,
                                       IFNULL(PREV_ACTUAL.PREV_ACTUAL_SPENT, 0) AS ACTUAL,
                                       IFNULL(PREV_BUDGET.APPROVED_AMOUNT, 0) - IFNULL(PREV_ACTUAL.PREV_ACTUAL_SPENT, 0) AS BALANCE,
                                       IFNULL(M2.PROPOSED_AMOUNT, 0) AS M2_PROPOSED_AMOUNT,
                                       IFNULL(M2.APPROVED_AMOUNT, 0) AS M2_APPROVED_AMOUNT,
                                       IFNULL(M1.NARRATION, '') AS NARRATION,
                                       IFNULL(M1.HO_NARRATION, '') AS HO_NARRATION,
                                       CASE
                                         WHEN ML.BUDGET_GROUP_ID = 1 THEN
                                          'Recurring Expenses'
                                         WHEN ML.BUDGET_GROUP_ID = 2 THEN
                                          'Non - Recurring Expenses'
                                         ELSE
                                          ''
                                       END AS BUDGET_GROUP,
                                       CASE
                                         WHEN ML.BUDGET_SUB_GROUP_ID = 1 THEN
                                          'Regular Expenses'
                                         WHEN ML.BUDGET_SUB_GROUP_ID = 2 THEN
                                          'Non - Regular Expenses'
                                         ELSE
                                          ''
                                       END AS BUDGET_SUB_GROUP,
                                       ML.BUDGET_GROUP_ID,
                                       ML.BUDGET_SUB_GROUP_ID
                                  FROM MASTER_LEDGER ML
                                 INNER JOIN MASTER_LEDGER_GROUP MLG
                                    ON ML.GROUP_ID = MLG.GROUP_ID
                                 INNER JOIN MASTER_NATURE MNN
                                    ON MNN.NATURE_ID = MLG.NATURE_ID
                                 INNER JOIN PROJECT_LEDGER PL
                                    ON PL.LEDGER_ID = ML.LEDGER_ID
                                 INNER JOIN PROJECT_BUDGET_LEDGER PBL
                                    ON PBL.LEDGER_ID = PL.LEDGER_ID
                                   AND PBL.PROJECT_ID = PL.PROJECT_ID
                                 INNER JOIN LEDGER_SUB_LEDGER LSL
                                    ON LSL.LEDGER_ID = ML.LEDGER_ID
                                 INNER JOIN MASTER_SUB_LEDGER MSL
                                    ON MSL.SUB_LEDGER_ID = LSL.SUB_LEDGER_ID
                                  LEFT JOIN (SELECT BM.BUDGET_ID,
                                                    BSL.LEDGER_ID,
                                                    BSL.SUB_LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    BSL.PROPOSED_AMOUNT,
                                                    BSL.APPROVED_AMOUNT,
                                                    NARRATION,
                                                    HO_NARRATION
                                               FROM BUDGET_MASTER BM
                                               LEFT JOIN BUDGET_SUB_LEDGER BSL
                                                 ON BSL.BUDGET_ID = BM.BUDGET_ID
                                             -- LEFT JOIN BUDGET_LEDGER BL ON BM.BUDGET_ID = BL.BUDGET_ID
                                              WHERE BM.BUDGET_ID IN (?MONTH1_BUDGET_ID)
                                                AND BSL.TRANS_MODE = ?BUDGET_TRANS_MODE) AS M1
                                    ON M1.LEDGER_ID = ML.LEDGER_ID
                                   AND M1.SUB_LEDGER_ID = MSL.SUB_LEDGER_ID
                                  LEFT JOIN (SELECT BM.BUDGET_ID,
                                                    BSL.LEDGER_ID,
                                                    BSL.SUB_LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    BSL.PROPOSED_AMOUNT,
                                                    BSL.APPROVED_AMOUNT,
                                                    NARRATION,
                                                    HO_NARRATION
                                               FROM BUDGET_MASTER BM
                                             -- LEFT JOIN BUDGET_LEDGER BL ON BM.BUDGET_ID = BL.BUDGET_ID
                                               LEFT JOIN BUDGET_SUB_LEDGER BSL
                                                 ON BSL.BUDGET_ID = BM.BUDGET_ID
                                              WHERE BM.BUDGET_ID IN (?MONTH2_BUDGET_ID)
                                                AND BSL.TRANS_MODE = ?BUDGET_TRANS_MODE) AS M2
                                    ON M2.LEDGER_ID = ML.LEDGER_ID
                                   AND M2.SUB_LEDGER_ID = MSL.SUB_LEDGER_ID
                                  LEFT JOIN (SELECT BM.BUDGET_ID,
                                                    BSL.LEDGER_ID,
                                                    BSL.SUB_LEDGER_ID,
                                                    BM.DATE_FROM,
                                                    BM.DATE_TO,
                                                    BSL.PROPOSED_AMOUNT,
                                                    BSL.APPROVED_AMOUNT,
                                                    NARRATION
                                               FROM BUDGET_MASTER BM
                                             -- LEFT JOIN BUDGET_LEDGER BL ON BM.BUDGET_ID = BL.BUDGET_ID
                                               LEFT JOIN BUDGET_SUB_LEDGER BSL
                                                 ON BSL.BUDGET_ID = BSL.BUDGET_ID
                                              WHERE BM.BUDGET_ID IN (?PREVIOUS_BUDGET_ID)
                                                AND BSL.TRANS_MODE = ?BUDGET_TRANS_MODE) AS PREV_BUDGET
                                    ON PREV_BUDGET.LEDGER_ID = ML.LEDGER_ID
                                   AND PREV_BUDGET.SUB_LEDGER_ID = MSL.SUB_LEDGER_ID
                                  LEFT JOIN (SELECT VT.LEDGER_ID,
                                                    VSL.SUB_LEDGER_ID,
                                                    SUM(VSL.AMOUNT) AS PREV_ACTUAL_SPENT
                                               FROM VOUCHER_TRANS VT
                                              INNER JOIN VOUCHER_MASTER_TRANS VMT
                                                 ON VT.VOUCHER_ID = VMT.VOUCHER_ID
                                              INNER JOIN VOUCHER_SUB_LEDGER_TRANS VSL
                                                 ON VSL.LEDGER_ID = VT.LEDGER_ID AND VSL.VOUCHER_ID=VT.VOUCHER_ID
                                              WHERE VMT.PROJECT_ID IN (?PROJECT_ID)
                                                AND (VT.TRANS_MODE = ?BUDGET_TRANS_MODE AND
                                                    VMT.VOUCHER_TYPE IN ('RC', 'PY'))
                                                AND VMT.STATUS = 1 AND VMT.BRANCH_ID IN (?BRANCH_ID)
                                                AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO
                                              GROUP BY VT.LEDGER_ID, VSL.SUB_LEDGER_ID) AS PREV_ACTUAL
                                    ON PREV_ACTUAL.LEDGER_ID = ML.LEDGER_ID
                                   AND PREV_ACTUAL.SUB_LEDGER_ID = MSL.SUB_LEDGER_ID
                                 WHERE PL.PROJECT_ID IN (?PROJECT_ID) AND ML.GROUP_ID NOT IN(12,13,14) AND ((M1.PROPOSED_AMOUNT>0 OR M1.APPROVED_AMOUNT>0) OR (M2.PROPOSED_AMOUNT>0 OR M2.APPROVED_AMOUNT>0))
                                 GROUP BY ML.LEDGER_ID,MSL.SUB_LEDGER_ID";
                        break;
                    }
                case SQLCommand.Budget.AnnualBudgetFetch:
                    {
                        //query = "SELECT BUDGET_ID,\n" +
                        //        "       BUDGET_NAME,\n" +
                        //        "       MP.PROJECT,\n" +
                        //        "       MP.PROJECT_ID,\n" +
                        //        "       BUDGET_TYPE_ID,\n" +
                        //        "       BM.PROJECT_ID,\n" +
                        //        "       DATE_FROM,\n" +
                        //        "       DATE_TO,\n" +
                        //        "       REMARKS,\n" +
                        //        "       IS_ACTIVE,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN IS_ACTIVE = 0 THEN\n" +
                        //        "          'Inactive'\n" +
                        //        "         ELSE\n" +
                        //        "          'Active'\n" +
                        //        "       END AS STATUS\n" +
                        //        "  FROM BUDGET_MASTER BM\n" +
                        //        " INNER JOIN MASTER_PROJECT MP\n" +
                        //        "    ON BM.PROJECT_ID = MP.PROJECT_ID \n" +
                        //        " WHERE BUDGET_TYPE_ID = 3\n" +
                        //        "   AND DATE_FROM >= ?YEAR_FROM\n" +
                        //        "   AND DATE_TO <= ?YEAR_TO\n" +
                        //        " ORDER BY STATUS, BUDGET_NAME ASC;";

                        query = "SELECT BM.BUDGET_ID,\n" +
                                "       BUDGET_NAME,\n" +
                                "       BLL.BUDGET_LEVEL_NAME,\n" +
                                "       GROUP_CONCAT(MP.PROJECT) AS PROJECT,\n" +
                                "       GROUP_CONCAT(MP.PROJECT_ID) AS PROJECT_ID,\n" +
                                "       BM.BUDGET_TYPE_ID,\n" +
                                "       BUDGET_TYPE,\n" +
                            //"       BP.PROJECT_ID,\n" +
                                "       DATE_FROM,\n" +
                                "       DATE_TO,\n" +
                                "       REMARKS,\n" +
                                "       IS_ACTIVE,\n" +
                                "       CASE\n" +
                                "         WHEN IS_ACTIVE = 0 THEN\n" +
                                "          'INACTIVE'\n" +
                                "         ELSE\n" +
                                "          'ACTIVE'\n" +
                                "       END AS STATUS\n" +
                                "  FROM BUDGET_MASTER BM\n" +
                                "  INNER JOIN BUDGET_PROJECT BP\n" +
                                "    ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                " LEFT JOIN BUDGET_LEVEL BLL\n" +
                                " ON BLL.BUDGET_LEVEL_ID = BM.BUDGET_LEVEL_ID\n" +
                                "  LEFT JOIN BUDGET_TYPE BT\n" +
                                "    ON BT.BUDGET_TYPE_ID = BM.BUDGET_TYPE_ID\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON BP.PROJECT_ID = MP.PROJECT_ID\n" +
                                " LEFT JOIN USER_PROJECT UP ON MP.PROJECT_ID = UP.PROJECT_ID\n" +
                                " WHERE BM.BUDGET_TYPE_ID IN (2,3, 4,5)\n" +
                            // "   AND DATE_FROM >= ?YEAR_FROM\n" +
                            // "   AND DATE_TO <= ?YEAR_TO\n" +
                                " AND ((DATE_FROM >= ?DATE_FROM AND DATE_TO <=?DATE_TO) OR \n" +
                                " (BM.BUDGET_TYPE_ID = " + (int)BudgetType.BudgetByCalendarYear + " AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <=?YEAR_TO)) { AND UP.ROLE_ID=?USERROLE_ID }  \n" +
                                " GROUP BY BUDGET_ID ORDER BY STATUS, DATE_FROM, BUDGET_NAME ASC";

                        break;
                    }
                case SQLCommand.Budget.AnnualBudgetProject:
                    {
                        query = "SELECT COUNT(BP.PROJECT_ID) AS PROJECT_ID,GROUP_CONCAT(MP.PROJECT) AS PROJECT FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                " INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = BP.PROJECT_ID\n" +
                                " WHERE BP.PROJECT_ID IN (?PROJECT_ID) " +
                               "AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID AND DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO";

                        //query = "SELECT COUNT(BP.PROJECT_ID) FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID WHERE BP.PROJECT_ID IN (?PROJECT_ID) " +
                        //        "AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID AND DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO";

                        //query = "SELECT COUNT(PROJECT_ID) FROM BUDGET_MASTER WHERE PROJECT_ID=?PROJECT_ID AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID  AND DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO";
                        break;
                    }
                case SQLCommand.Budget.CalendarYearBudget:
                    {
                        //query = "SELECT DATE_FROM,DATE_TO,COUNT(BUDGET_ID) AS COUNT FROM BUDGET_MASTER WHERE PROJECT_ID=?PROJECT_ID AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID AND DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO";
                        // query = "SELECT DATE_FROM,DATE_TO,COUNT(BM.BUDGET_ID) AS COUNT FROM BUDGET_MASTER BM INNER JOIN BUDGET_LEDGER BL ON BL.BUDGET_ID = BM.BUDGET_ID WHERE PROJECT_ID=?PROJECT_ID AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID AND DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO";
                        query = "SELECT DATE_FROM,DATE_TO,COUNT(BM.BUDGET_ID) AS COUNT FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID WHERE PROJECT_ID=?PROJECT_ID AND BUDGET_TYPE_ID=?BUDGET_TYPE_ID AND DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO";

                        break;
                    }
                case SQLCommand.Budget.InsertBudgetCostCentreDetails:
                    {
                        query = "INSERT INTO BUDGET_COSTCENTER\n" +
                              "   (BUDGET_ID, LEDGER_ID, COST_CENTRE_ID, AMOUNT, SEQUENCE_NO, COST_CENTRE_TABLE)\n" +
                              "VALUES\n" +
                              "   (?BUDGET_ID, ?LEDGER_ID, ?COST_CENTRE_ID, ?AMOUNT, ?SEQUENCE_NO, ?COST_CENTRE_TABLE)";
                        break;
                    }
                case SQLCommand.Budget.DeleteBudgetCCdetailsByBudgetId:
                    {
                        query = "DELETE FROM BUDGET_COSTCENTER WHERE BUDGET_ID=?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchCostCentreByLedger:
                    {
                        query = "SELECT BCC.BUDGET_ID,\n" +
                        "       BCC.LEDGER_ID,\n" +
                        "       BCC.COST_CENTRE_ID,\n" +
                        "       BCC.AMOUNT,\n" +
                        "       BCC.SEQUENCE_NO,\n" +
                        "       BCC.COST_CENTRE_TABLE\n" +
                        "  FROM BUDGET_COSTCENTER BCC\n" +
                        " INNER JOIN MASTER_COST_CENTRE MC\n" +
                        "    ON BCC.COST_CENTRE_ID = MC.COST_CENTRE_ID\n" +
                        " WHERE BCC.BUDGET_ID = ?BUDGET_ID\n" +
                        "   AND LEDGER_ID = ?LEDGER_ID\n" +
                        "   AND COST_CENTRE_TABLE = ?COST_CENTRE_TABLE;";

                        break;
                    }
                case SQLCommand.Budget.CheckForBudgetEntry:
                    {
                        query = "SELECT COUNT(LEDGER_ID),\n" +
                                "       BM.BUDGET_ID,\n" +
                                "       BUDGET_TYPE_ID,\n" +
                                "    --    PROJECT_ID,\n" +
                                "       DATE_FROM,\n" +
                                "       DATE_TO,\n" +
                                "       IS_ACTIVE\n" +
                                "  FROM BUDGET_MASTER BM\n" +
                                " INNER JOIN BUDGET_LEDGER BL\n" +
                                "    ON BM.BUDGET_ID = BL.BUDGET_ID\n" +
                                "   AND PROPOSED_AMOUNT <> 0\n" +
                                "   AND BM.BUDGET_ID = ?BUDGET_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchBudgetIdByDateRangeProject:
                    {
                        query = "SELECT BM.BUDGET_ID\n" +
                                "FROM BUDGET_MASTER BM\n" +
                                "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                "WHERE BM.IS_ACTIVE = 1 AND BM.DATE_FROM=?DATE_FROM AND BM.DATE_TO=?DATE_TO AND BM.BRANCH_ID =?BRANCH_OFFICE_ID\n" +
                                "AND BP.PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.Budget.FetchBranchCodebyBranchId:
                    {
                        query = @"SELECT BRANCH_OFFICE_CODE FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_ID =?BRANCH_ID";
                        break;
                    }
                case SQLCommand.Budget.FetchOneTwoMonthStatus:
                    {
                        query = "SELECT IS_TWO_MONTH_BUDGET FROM BRANCH_LICENSE AS BL INNER JOIN BRANCH_OFFICE AS BM ON BM.BRANCH_OFFICE_ID= BL.BRANCH_ID\n" +
                                 " WHERE BM.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE ORDER BY LICENSE_ID DESC LIMIT 1";
                        break;
                    }
                #endregion
            }
            return query;
        }
        #endregion Bank SQL
        #endregion
    }
}
