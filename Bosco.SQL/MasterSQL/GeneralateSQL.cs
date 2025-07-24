using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class GeneralateSQL : IDatabaseQuery
    {
        #region ISQLedgerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.CongregationLedger).FullName)
            {
                query = GetLedgerGroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion


        #region SQL Script
        private string GetLedgerGroupSQL()
        {
            string query = "";
            SQLCommand.CongregationLedger sqlCommandId = (SQLCommand.CongregationLedger)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.CongregationLedger.Add:
                    {
                        query = "INSERT INTO CONGREGATION_LEDGER\n" +
                                "  (CON_LEDGER_CODE, CON_LEDGER_NAME, CON_PARENT_LEDGER_ID,CON_MAIN_PARENT_ID)\n" +
                                "VALUES\n" +
                                "  (?CON_LEDGER_CODE, ?CON_LEDGER_NAME, ?CON_PARENT_LEDGER_ID, ?CON_MAIN_PARENT_ID)";
                        break;
                    }

                case SQLCommand.CongregationLedger.Update:
                    {
                        query = "UPDATE CONGREGATION_LEDGER\n" +
                                "   SET CON_LEDGER_ID        = ?CON_LEDGER_ID,\n" +
                                "       CON_LEDGER_CODE      = ?CON_LEDGER_CODE,\n" +
                                "       CON_LEDGER_NAME      = ?CON_LEDGER_NAME,\n" +
                                "       CON_PARENT_LEDGER_ID = ?CON_PARENT_LEDGER_ID,\n" +
                                "       CON_MAIN_PARENT_ID   = ?CON_MAIN_PARENT_ID\n" +
                                " WHERE CON_LEDGER_ID = ?CON_LEDGER_ID";
                        break;
                    }

                case SQLCommand.CongregationLedger.Delete:
                    {
                        query = "DELETE FROM CONGREGATION_LEDGER WHERE CON_LEDGER_ID=?CON_LEDGER_ID";
                        break;
                    }

                case SQLCommand.CongregationLedger.DeleteMappedLedgers:
                    {
                        query = "DELETE FROM CONGREGATION_LEDGER_MAP WHERE CON_LEDGER_ID=?CON_LEDGER_ID";
                        break;
                    }



                case SQLCommand.CongregationLedger.FetchAll:
                    {
                        //query = "SELECT CON_LEDGER_ID,\n" +
                        //        "       CON_LEDGER_CODE,\n" +
                        //        "       CONCAT(CON_LEDGER_NAME,' (',CONCAT(CON_LEDGER_CODE,')')) AS CON_LEDGER_NAME,\n" +
                        //        "       CON_PARENT_LEDGER_ID\n" +
                        //        "  FROM CONGREGATION_LEDGER ORDER BY CON_LEDGER_CODE";

                        query = "SELECT CON_LEDGER_ID,\n" +
                               "       CON_LEDGER_CODE,\n" +
                               "     CONCAT(CON_LEDGER_NAME, IF(CON_LEDGER_CODE IS NULL, '', CONCAT(' (',CONCAT(CON_LEDGER_CODE,')')))) AS CON_LEDGER_NAME,\n" +
                               "       CON_PARENT_LEDGER_ID\n" +
                               "  FROM CONGREGATION_LEDGER ORDER BY CON_LEDGER_CODE";
                        break;
                    }

                case SQLCommand.CongregationLedger.FetchAllChildLedgers:
                    {
                        query = "SELECT CON_LEDGER_ID,\n" +
                                "       CON_LEDGER_CODE,\n" +
                                "       CON_LEDGER_NAME,\n" +
                                "       CON_PARENT_LEDGER_ID\n" +
                                "  FROM CONGREGATION_LEDGER WHERE CON_LEDGER_ID NOT IN (CON_MAIN_PARENT_ID)";
                        break;
                    }
                case SQLCommand.CongregationLedger.FetchMappedLedgers:
                    {
                        query = "SELECT COUNT(CON_LEDGER_ID) FROM CONGREGATION_LEDGER_MAP WHERE CON_LEDGER_ID=?CON_LEDGER_ID";
                        break;
                    }

                case SQLCommand.CongregationLedger.FetchParentLedgers:// This fetches only the ledgers which does not have any child ledger to that.
                    {
                        // Commanded by chinna to show all the generalate created Ledgers
                        //query = "SELECT CON_LEDGER_ID,\n" +
                        //        "       CON_LEDGER_CODE,\n" +
                        //        "       CON_LEDGER_NAME,\n" +
                        //        "       CON_PARENT_LEDGER_ID\n" +
                        //        "  FROM CONGREGATION_LEDGER\n" +
                        //        " WHERE CON_LEDGER_ID NOT IN\n" +
                        //        "       (SELECT CON_PARENT_LEDGER_ID\n" +
                        //        "          FROM CONGREGATION_LEDGER\n" +
                        //        "         WHERE CON_LEDGER_ID <> CON_PARENT_LEDGER_ID);";

                        //query = "SELECT CON_LEDGER_ID,\n" +
                        //       "       CON_LEDGER_CODE,\n" +
                        //       "      CONCAT(CON_LEDGER_CODE,'  ',CON_LEDGER_NAME) AS CON_LEDGER_NAME,\n" +
                        //       "       CON_PARENT_LEDGER_ID\n" +
                        //       "FROM CONGREGATION_LEDGER \n" +
                        //       "WHERE (CON_LEDGER_ID <> CON_PARENT_LEDGER_ID) OR CON_LEDGER_CODE IN ('A.2','A.3','A.5','B.1','B.4','C.6','C.7','C.8', 'C.9', 'D.6', 'D.7')\n" +
                        //       "ORDER BY CON_LEDGER_CODE, CON_LEDGER_NAME";

                        query = "SELECT CON_LEDGER_ID,\n" +
                               "       CON_LEDGER_CODE,\n" +
                               "     CONCAT(IFNULL(CON_LEDGER_CODE,''),'  ',CON_LEDGER_NAME) AS CON_LEDGER_NAME,\n" +
                               "     CON_PARENT_LEDGER_ID\n" +
                               "FROM CONGREGATION_LEDGER \n" +
                               "WHERE (CON_LEDGER_ID <> CON_PARENT_LEDGER_ID) OR CON_LEDGER_CODE IN ('A.2','A.3','A.5','B.1','B.4','C.6','C.7','C.8', 'C.9', 'D.6', 'D.7')\n" +
                               "ORDER BY CON_LEDGER_CODE, CON_LEDGER_NAME";

                        break;
                    }
                case SQLCommand.CongregationLedger.FetchGroupedParentLedgers:
                    {
                        query = "SELECT CON_LEDGER_ID,\n" +
                             "       CON_LEDGER_CODE,\n" +
                             "      CONCAT(CON_LEDGER_NAME,'  ', CON_LEDGER_CODE) AS CON_LEDGER_NAME,\n" +
                             "       CON_PARENT_LEDGER_ID\n" +
                             "  FROM CONGREGATION_LEDGER;";
                        break;
                    }
                case SQLCommand.CongregationLedger.FetchProjectCategoryLedgers:
                    {
                        //query = "SELECT MPCG.PROJECT_CATOGORY_GROUP_ID, PROJECT_CATOGORY_GROUP FROM MASTER_PROJECT_CATOGORY_GROUP AS MPCG \n" +
                        //          "INNER JOIN MASTER_PROJECT_CATOGORY AS MPC ON MPC.PROJECT_CATOGORY_GROUP_ID = MPCG.PROJECT_CATOGORY_GROUP_ID\n" +
                        //          "GROUP BY MPCG.PROJECT_CATOGORY_GROUP_ID;";
                        query = "SELECT PROJECT_CATOGORY_GROUP_ID, PROJECT_CATOGORY_GROUP FROM MASTER_PROJECT_CATOGORY_GROUP \n" +
                           " GROUP BY PROJECT_CATOGORY_GROUP_ID;";
                        break;
                    }
                case SQLCommand.CongregationLedger.FetchProjectCategorybyGroupedProjectCategory:
                    {
                        query = "SELECT GROUP_CONCAT(PROJECT_CATOGORY_ID) AS PROJECT_CATOGORY_ID FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_GROUP_ID IN \n" +
                         "(SELECT PROJECT_CATOGORY_GROUP_ID FROM MASTER_PROJECT_CATOGORY_GROUP WHERE PROJECT_CATOGORY_GROUP_ID IN (?PROJECT_CATOGORY_GROUP_ID));";
                        break;
                    }

                case SQLCommand.CongregationLedger.FetchAllParents: // while mapping the con Ledger id = con parent Id i replaced con ledger id = con parnet Ledger id (14.06.2017)
                    {
                        query = "SELECT CL1.CON_LEDGER_ID, CL1.CON_LEDGER_NAME,CL2.CON_MAIN_PARENT_ID\n" +
                                "  FROM CONGREGATION_LEDGER CL1\n" +
                                " INNER JOIN CONGREGATION_LEDGER CL2\n" +
                                "    ON CL1.CON_LEDGER_ID = CL2.CON_MAIN_PARENT_ID\n" +
                                " GROUP BY CL1.CON_LEDGER_ID";
                        break;
                    }

                case SQLCommand.CongregationLedger.FetchById:
                    {
                        query = "SELECT CON_LEDGER_ID,\n" +
                                "       CON_LEDGER_CODE,\n" +
                                "       CON_LEDGER_NAME,\n" +
                                "       CON_PARENT_LEDGER_ID\n" +
                                "  FROM CONGREGATION_LEDGER\n" +
                                " WHERE CON_LEDGER_ID = ?CON_LEDGER_ID";
                        ;
                        break;
                    }

                case SQLCommand.CongregationLedger.FetchLedgerList:
                    {
                        query = "SELECT T.CON_LEDGER_ID,\n" +
                                "       LEDGER_ID,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME,\n" +
                                "       T.CON_LEDGER_NAME,\n" +
                                "       CON_LEDGER_CODE\n" +
                                "  FROM CONGREGATION_LEDGER CL\n" +
                                "  JOIN (SELECT LM.CON_LEDGER_ID,\n" +
                                "               LEDGER_NAME,\n" +
                                "               LEDGER_CODE,\n" +
                                "               CON_LEDGER_NAME,\n" +
                                "               ML.LEDGER_ID,\n" +
                                "               CON_PARENT_LEDGER_ID,\n" +
                                "               CON_MAIN_PARENT_ID\n" +
                                "          FROM CONGREGATION_LEDGER_MAP LM\n" +
                                "          LEFT JOIN CONGREGATION_LEDGER L\n" +
                                "            ON LM.CON_LEDGER_ID = L.CON_LEDGER_ID\n" +
                                "          LEFT JOIN MASTER_LEDGER ML\n" +
                                "            ON LM.LEDGER_ID = ML.LEDGER_ID) AS T\n" +
                                "    ON T.CON_PARENT_LEDGER_ID = CL.CON_PARENT_LEDGER_ID\n" +
                                "   AND T.CON_MAIN_PARENT_ID IN (?CON_LEDGER_ID)\n" +
                                "    OR T.CON_PARENT_LEDGER_ID IN (?CON_LEDGER_ID)\n" +
                                "    OR T.CON_LEDGER_ID IN (?CON_LEDGER_ID)\n" +
                                " GROUP BY LEDGER_ID;";

                        //query = "SELECT T.CON_LEDGER_ID,\n" +
                        //        "       LEDGER_ID,\n" +
                        //        "       LEDGER_CODE,\n" +
                        //        "       LEDGER_NAME,\n" +
                        //        "       CON_LEDGER_NAME,\n" +
                        //        "       CON_LEDGER_CODE\n" +
                        //        "  FROM CONGREGATION_LEDGER CL\n" +
                        //        "  JOIN (SELECT LM.CON_LEDGER_ID, LEDGER_NAME, LEDGER_CODE, ML.LEDGER_ID\n" +
                        //        "          FROM CONGREGATION_LEDGER_MAP LM\n" +
                        //        "         INNER JOIN MASTER_LEDGER ML\n" +
                        //        "            ON LM.LEDGER_ID = ML.LEDGER_ID) AS T\n" +
                        //        "    ON T.CON_LEDGER_ID = CL.CON_LEDGER_ID\n" +
                        //        "   AND T.CON_PARENT_LEDGER_ID IN (?CON_LEDGER_ID)";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
