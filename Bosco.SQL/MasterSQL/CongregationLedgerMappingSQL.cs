using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class CongregationLedgerMappingSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.CongregationMapping).FullName)
            {
                query = GetCongregationLedgerMappingSQL();
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
        private string GetCongregationLedgerMappingSQL()
        {
            string query = "";
            SQLCommand.CongregationMapping sqlCommandId = (SQLCommand.CongregationMapping)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.CongregationMapping.FetchLedgerByCongregationLedger:
                    {
                        query = "SELECT ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "      CONCAT(IFNULL(GROUP_CODE,''), ' - ', MLG.LEDGER_GROUP) AS LEDGER_GROUP,MLG.NATURE_ID,\n" +
                                "       CASE WHEN MLG.NATURE_ID = 1 THEN 'Incomes'\n" +
                                "    WHEN MLG.NATURE_ID =2 THEN 'Expenses'\n" +
                                "    WHEN MLG.NATURE_ID =3 THEN 'Assets'\n" +
                                "   WHEN MLG.NATURE_ID =4 THEN 'Liabilities' END NATURE,\n" +
                                "       IF(CLM.CON_LEDGER_ID = ?CON_LEDGER_ID, 1, 0) AS 'SELECT'\n" +
                                "  FROM MASTER_LEDGER AS ML\n" +
                                "  LEFT JOIN CONGREGATION_LEDGER_MAP AS CLM\n" +
                                "    ON ML.LEDGER_ID = CLM.LEDGER_ID\n" +
                                "   AND ML.ACCESS_FLAG = 0\n" +
                                "  LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                                "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                " WHERE ML.GROUP_ID NOT IN (12, 13)\n" +
                                "   AND ML.IS_BRANCH_LEDGER = 0\n" +
                                " AND (CLM.CON_LEDGER_ID IS NULL OR CLM.CON_LEDGER_ID =?CON_LEDGER_ID);";

                        //query = "SELECT ML.LEDGER_ID,\n" +
                        //      "       ML.LEDGER_NAME,\n" +
                        //      "       MLG.LEDGER_GROUP,MLG.NATURE_ID,\n" +
                        //      "       CASE WHEN MLG.NATURE_ID = 1 THEN 'Incomes'\n" +
                        //      "    WHEN MLG.NATURE_ID =2 THEN 'Expenses'\n" +
                        //      "    WHEN MLG.NATURE_ID =3 THEN 'Assets'\n" +
                        //      "   WHEN MLG.NATURE_ID =4 THEN 'Liabilities' END NATURE,\n" +
                        //      "       IF(CLM.CON_LEDGER_ID = ?CON_LEDGER_ID, 1, 0) AS 'SELECT'\n" +
                        //      "  FROM MASTER_LEDGER AS ML\n" +
                        //      "  LEFT JOIN CONGREGATION_LEDGER_MAP AS CLM\n" +
                        //      "    ON ML.LEDGER_ID = CLM.LEDGER_ID\n" +
                        //      "   AND ML.ACCESS_FLAG = 0\n" +
                        //      "  LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //      "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        //      " WHERE ML.GROUP_ID NOT IN (12, 13)\n" +
                        //      "   AND ML.IS_BRANCH_LEDGER = 0\n" +
                        //      " AND (CLM.CON_LEDGER_ID IS NULL OR CLM.CON_LEDGER_ID =?CON_LEDGER_ID);";

                        break;
                    }
                case SQLCommand.CongregationMapping.FetchLedgerByProjectCategoryLedger:
                    {
                        // if there is community Leder as we fixed as Id 2, By default load all the IEAL Ledger otherwise by default filter the AL Ledgers
                        // chinna 22.10.2020 (ML.LEDGER_ID NOT IN (20, 135,249))
                        query = @" SELECT MLG.NATURE_ID,ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME, MLG.LEDGER_GROUP,CLM.CON_LEDGER_ID,CL.CON_LEDGER_NAME,
                                    IF(CLM.PROJECT_CATOGORY_GROUP_ID IN (?PROJECT_CATOGORY_GROUP_ID),1,0) AS 'SELECT'
                                    FROM MASTER_LEDGER AS ML
                                    INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID=ML.GROUP_ID
                                    LEFT JOIN CONGREGATION_LEDGER_MAP AS CLM ON ML.LEDGER_ID = CLM.LEDGER_ID AND CLM.LEDGER_ID = ML.LEDGER_ID AND CLM.PROJECT_CATOGORY_GROUP_ID IN (?PROJECT_CATOGORY_GROUP_ID)
                                    LEFT JOIN CONGREGATION_LEDGER AS CL ON CL.CON_LEDGER_ID = CLM.CON_LEDGER_ID
                                    WHERE ML.GROUP_ID NOT IN (12, 13, 14)
                                    AND ML.IS_BRANCH_LEDGER=0 AND ML.LEDGER_ID NOT IN (?INTER_AC_FROM_TRANSFER_ID, ?INTER_AC_TO_TRANSFER_ID, ?CONTRIBUTION_FROM_PROVINCE_ID, ?CONTRIBUTION_TO_PROVINCE_ID)
                                    AND IF(?PROJECT_CATOGORY_GROUP_ID = 2,  MLG.NATURE_ID IN (1,2,3,4), MLG.NATURE_ID IN (1,2))
                                    GROUP BY LEDGER_ID ORDER BY MLG.NATURE_ID,ML.LEDGER_CODE,ML.LEDGER_NAME;";

                        //                        query = @" SELECT MLG.NATURE_ID,ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME, MLG.LEDGER_GROUP,CLM.CON_LEDGER_ID,CL.CON_LEDGER_NAME,
                        //                                    IF(CLM.PROJECT_CATOGORY_GROUP_ID IN (?PROJECT_CATOGORY_GROUP_ID),1,0) AS 'SELECT'
                        //                                    FROM MASTER_LEDGER AS ML
                        //                                    INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID=ML.GROUP_ID
                        //                                    LEFT JOIN CONGREGATION_LEDGER_MAP AS CLM ON ML.LEDGER_ID = CLM.LEDGER_ID AND CLM.LEDGER_ID = ML.LEDGER_ID AND CLM.PROJECT_CATOGORY_GROUP_ID IN (?PROJECT_CATOGORY_GROUP_ID)
                        //                                    LEFT JOIN CONGREGATION_LEDGER AS CL ON CL.CON_LEDGER_ID = CLM.CON_LEDGER_ID
                        //                                    WHERE ML.ACCESS_FLAG=0 AND ML.GROUP_ID NOT IN (12, 13, 14)
                        //                                    AND ML.IS_BRANCH_LEDGER=0 AND ML.LEDGER_ID NOT IN (20, 135,249)
                        //                                    GROUP BY LEDGER_ID ORDER BY MLG.NATURE_ID,ML.LEDGER_CODE,ML.LEDGER_NAME;";

                        //query = "SELECT MLG.NATURE_ID,ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME, MLG.LEDGER_GROUP,CLM.CON_LEDGER_ID,CL.CON_LEDGER_NAME, IF(CLM.PROJECT_CATOGORY_GROUP_ID IN (?PROJECT_CATOGORY_GROUP_ID),1,0) AS 'SELECT'\n" +
                        //           "FROM MASTER_LEDGER AS ML\n" +
                        //           "INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID=ML.GROUP_ID\n" +
                        //           "INNER JOIN PROJECT_CATEGORY_LEDGER AS PCL ON ML.LEDGER_ID = PCL.LEDGER_ID AND PCL.PROJECT_CATEGORY_ID IN (?PROJECT_CATOGORY_ID)\n" +
                        //           "LEFT JOIN CONGREGATION_LEDGER_MAP AS CLM ON ML.LEDGER_ID = CLM.LEDGER_ID AND CLM.LEDGER_ID = PCL.LEDGER_ID AND CLM.PROJECT_CATOGORY_GROUP_ID IN (?PROJECT_CATOGORY_GROUP_ID)\n" +
                        //           "LEFT JOIN CONGREGATION_LEDGER AS CL ON CL.CON_LEDGER_ID = CLM.CON_LEDGER_ID\n" +
                        //           "WHERE ML.ACCESS_FLAG=0 AND ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                        //           "AND PCL.PROJECT_CATEGORY_ID IN (?PROJECT_CATOGORY_ID) AND ML.IS_BRANCH_LEDGER=0\n" +
                        //           "GROUP BY LEDGER_ID ORDER BY MLG.NATURE_ID,ML.LEDGER_CODE,ML.LEDGER_NAME;";

                        break;
                    }
                case SQLCommand.CongregationMapping.MapCongregationLedger:
                    {
                        query = "INSERT INTO CONGREGATION_LEDGER_MAP(CON_LEDGER_ID, LEDGER_ID)VALUES(?CON_LEDGER_ID, ?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.CongregationMapping.MapProjectCatogoryCongregationLedger:
                    {
                        query = "INSERT INTO CONGREGATION_LEDGER_MAP(LEDGER_ID,CON_LEDGER_ID,PROJECT_CATOGORY_GROUP_ID)VALUES(?LEDGER_ID,?CON_LEDGER_ID,?PROJECT_CATOGORY_GROUP_ID);";
                        break;
                    }
                case SQLCommand.CongregationMapping.DeleteMappedLedger:
                    {
                        query = "DELETE FROM CONGREGATION_LEDGER_MAP WHERE CON_LEDGER_ID = ?CON_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.CongregationMapping.DeleteGroupedProjectCatogoryLedgers:
                    {
                        query = "DELETE FROM CONGREGATION_LEDGER_MAP WHERE PROJECT_CATOGORY_GROUP_ID = ?PROJECT_CATOGORY_GROUP_ID";
                        break;
                    }
                case SQLCommand.CongregationMapping.DeleteIndividualMappedLedger:
                    {
                        query = "DELETE FROM CONGREGATION_LEDGER_MAP WHERE LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.CongregationMapping.CheckingMappedCount:
                    {
                        query = "SELECT CON_LEDGER_ID, LEDGER_ID\n" +
                                "  FROM CONGREGATION_LEDGER_MAP\n" +
                                " WHERE LEDGER_ID IN (?LEDGER_IDS)\n" +
                                " GROUP BY LEDGER_ID\n" +
                                "HAVING COUNT(LEDGER_ID) > 0;";
                        break;
                    }
                case SQLCommand.CongregationMapping.CheckingSameNature:
                    {
                        query = "SELECT NATURE_ID\n" +
                                "  FROM MASTER_LEDGER_GROUP\n" +
                                " WHERE GROUP_ID IN (\n" +
                                "\n" +
                                "                    SELECT GROUP_ID\n" +
                                "                      FROM MASTER_LEDGER\n" +
                                "                     WHERE LEDGER_ID IN (\n" +
                                "\n" +
                                "                                         SELECT LEDGER_ID\n" +
                                "                                           FROM CONGREGATION_LEDGER_MAP\n" +
                                "                                          WHERE CON_LEDGER_ID IN\n" +
                                "                                                (SELECT CON_LEDGER_ID\n" +
                                "                                                   FROM CONGREGATION_LEDGER\n" +
                                "                                                  WHERE CON_MAIN_PARENT_ID =\n" +
                                "                                                        (SELECT CON_MAIN_PARENT_ID\n" +
                                "                                                           FROM CONGREGATION_LEDGER\n" +
                                "                                                          WHERE CON_LEDGER_ID = ?CON_LEDGER_ID))))\n" +
                                " GROUP BY NATURE_ID;";
                        break;
                    }
            }

            return query;
        }
        #endregion SQL
    }
}
