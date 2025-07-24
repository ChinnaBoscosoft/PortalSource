using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class LedgerGroupSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.LedgerGroup).FullName)
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
            SQLCommand.LedgerGroup sqlCommandId = (SQLCommand.LedgerGroup)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.LedgerGroup.Add:
                    {
                        query = "INSERT INTO MASTER_LEDGER_GROUP ( " +
                                   "GROUP_CODE, " +
                                   "LEDGER_GROUP, " +
                                   "PARENT_GROUP_ID, " +
                                   "NATURE_ID, " +
                                   "MAIN_GROUP_ID,IMAGE_ID,SORT_ORDER ) VALUES " +
                                   "(?GROUP_CODE, " +
                                   "?LEDGER_GROUP, " +
                                   "?PARENT_GROUP_ID, " +
                                   "?NATURE_ID, " +
                                   "?MAIN_GROUP_ID,?IMAGE_ID,?SORT_ORDER )";
                        break;
                    }
                case SQLCommand.LedgerGroup.Update:
                    {
                        query = "UPDATE MASTER_LEDGER_GROUP SET " +
                                        "GROUP_ID = ?GROUP_ID, " +
                                        "GROUP_CODE =?GROUP_CODE, " +
                                        "LEDGER_GROUP =?LEDGER_GROUP, " +
                                        "PARENT_GROUP_ID=?PARENT_GROUP_ID, " +
                                        "NATURE_ID=?NATURE_ID, " +
                                        "MAIN_GROUP_ID=?MAIN_GROUP_ID,IMAGE_ID=?IMAGE_ID " +
                                        " WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.Delete:
                    {
                        query = "DELETE FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.Fetch:
                    {
                        query = "SELECT " +
                                   "GROUP_ID, " +
                                   "GROUP_CODE, " +
                                   "LEDGER_GROUP, " +
                                   "PARENT_GROUP_ID, " +
                                   "NATURE_ID, " +
                                   "MAIN_GROUP_ID, " +
                                   "IMAGE_ID " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP" +
                                   " WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchAll:
                    {
                        query = "SELECT " +
                                    "GROUP_ID , " +
                                    "LEDGER_GROUP AS 'Ledger Group' , " +
                                    "PARENT_GROUP_ID,IMAGE_ID " +
                                "FROM " +
                                    "MASTER_LEDGER_GROUP ORDER BY LEDGER_GROUP ASC";
                        break;
                    }

                case SQLCommand.LedgerGroup.FetchLedgerList:
                    {
                        //query = "SELECT ML.LEDGER_CODE,\n" +
                        //        "       ML.LEDGER_NAME AS 'LEDGER NAME'\n" +
                        //        "  FROM MASTER_LEDGER_GROUP MLG\n" +
                        //        " INNER JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                        //        " WHERE PARENT_GROUP_ID IN (?GROUP_ID)\n" +
                        //        "\n" +
                        //        "UNION\n" +
                        //        "SELECT GROUP_ID, '' AS 'LEDGER NAME'\n" +
                        //        "  FROM MASTER_LEDGER_GROUP\n" +
                        //        " WHERE PARENT_GROUP_ID IN\n" +
                        //        "       (SELECT GROUP_ID\n" +
                        //        "          FROM MASTER_LEDGER_GROUP MLG\n" +
                        //        "         WHERE MLG.PARENT_GROUP_ID IN (?GROUP_ID))\n" +
                        //        "\n" +
                        //        "UNION\n" +
                        //        "SELECT GROUP_ID, '' AS 'LEDGER_NAME'\n" +
                        //        "  FROM MASTER_LEDGER_GROUP MLG2\n" +
                        //        " WHERE MLG2.GROUP_ID IN (?GROUP_ID);";

                        query = "SELECT ML.LEDGER_CODE AS 'Ledger Code', ML.LEDGER_NAME AS 'Ledger Name'\n" +
                               "  FROM MASTER_LEDGER ML\n" +
                               "  JOIN (SELECT MLG.GROUP_ID, '' AS L\n" +
                               "          FROM MASTER_LEDGER_GROUP MLG\n" +
                               "         INNER JOIN MASTER_LEDGER ML\n" +
                               "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                               "         WHERE PARENT_GROUP_ID IN (?GROUP_ID)\n" +
                               "        UNION\n" +
                               "        SELECT GROUP_ID, '' AS L\n" +
                               "          FROM MASTER_LEDGER_GROUP\n" +
                               "         WHERE PARENT_GROUP_ID IN\n" +
                               "               (SELECT GROUP_ID\n" +
                               "                  FROM MASTER_LEDGER_GROUP MLG\n" +
                               "                 WHERE MLG.PARENT_GROUP_ID IN (?GROUP_ID))\n" +
                               "        UNION\n" +
                               "        SELECT GROUP_ID, '' AS L\n" +
                               "          FROM MASTER_LEDGER_GROUP MLG2\n" +
                               "         WHERE MLG2.GROUP_ID IN (?GROUP_ID)) AS T\n" +
                               "    ON T.GROUP_ID = ML.GROUP_ID ORDER BY ML.LEDGER_NAME;";

                        break;
                    }
                case SQLCommand.LedgerGroup.FetchByGroupId:
                    {
                        query = "SELECT GROUP_ID , LEDGER_GROUP AS 'Ledger Sub Group' , " +
                                    "PARENT_GROUP_ID,IMAGE_ID FROM MASTER_LEDGER_GROUP " +
                                "WHERE " +
                                "FIND_IN_SET(GROUP_ID,?LEDGER_GROUP) >0 " +
                                "UNION " +
                                "SELECT GROUP_ID , CONCAT(LEDGER_GROUP,CONCAT(' -',GROUP_CODE )) AS 'Ledger Sub Group' , " +
                                    "PARENT_GROUP_ID,IMAGE_ID FROM MASTER_LEDGER_GROUP " +
                                "WHERE " +
                                "FIND_IN_SET(PARENT_GROUP_ID,?LEDGER_GROUP)>0;";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchforLookup:
                    {
                        query = "SELECT " +
                                    "GROUP_ID , " +
                                    "LEDGER_GROUP  " +
                                "FROM " +
                                    "MASTER_LEDGER_GROUP WHERE GROUP_ID NOT IN (12,13,14) ORDER BY LEDGER_GROUP ASC "; //WHERE GROUP_ID NOT IN (1,2,3,4,12)
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchforLedgerLookup:
                    {
                        query = "SELECT " +
                                    "GROUP_ID , " +
                                    "NATURE_ID , " +
                                    "LEDGER_GROUP  " +
                                "FROM " +
                            //   "MASTER_LEDGER_GROUP WHERE GROUP_ID NOT IN (1,2,3,4,12,13)  ORDER BY LEDGER_GROUP ASC ";  
                                    "MASTER_LEDGER_GROUP WHERE GROUP_ID NOT IN (1,2,3,4,12)  ORDER BY LEDGER_GROUP ASC ";
                        //(14-FD Removed) Not to show Account Nature in the Ledger Lookup & Bank Account & FD , 12-bank, 13-cash, 14- FD allow FD ledger to be created in portal
                        break;
                    }
                case SQLCommand.LedgerGroup.GetGroupId:
                    {
                        query = "SELECT GROUP_ID,\n" +
                                "       GROUP_CODE,\n" +
                                "       LEDGER_GROUP,\n" +
                                "       PARENT_GROUP_ID,\n" +
                                "       NATURE_ID,\n" +
                                "       MAIN_GROUP_ID,\n" +
                                "       IMAGE_ID,\n" +
                                "       ACCESS_FLAG,\n" +
                                "       SORT_ORDER\n" +
                                "  FROM MASTER_LEDGER_GROUP\n" +
                                " WHERE LEDGER_GROUP = ?LEDGER_GROUP;";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchNatureId:
                    {
                        query = "SELECT " +
                                   "NATURE_ID " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP " +
                                   "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchValidateGroup:
                    {
                        query = "SELECT " +
                                   "NATURE_ID,PARENT_GROUP_ID " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP " +
                                   "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchAccessFlag:
                    {
                        query = "SELECT " +
                                   "ACCESS_FLAG " +
                               "FROM " +
                                   "MASTER_LEDGER_GROUP " +
                                   "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.UpdateImageIndex:
                    {
                        query = "UPDATE MASTER_LEDGER_GROUP " +
                                        "SET IMAGE_ID=0  " +
                                "WHERE GROUP_ID=?GROUP_ID ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchAccoutType:
                    {
                        query = "SELECT " +
                                      "ACCOUNT_TYPE_ID , " +
                                      "ACCOUNT_TYPE  " +
                                  "FROM " +
                                      "MASTER_ACCOUNT_TYPE ";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchLedgerGroupCodes:
                    {
                        query = "SELECT GROUP_CODE FROM MASTER_LEDGER_GROUP ORDER BY GROUP_ID DESC";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchFDLedger:
                    {
                        query = "SELECT GROUP_ID, LEDGER_GROUP FROM  MASTER_LEDGER_GROUP WHERE GROUP_ID IN (14) ORDER BY LEDGER_GROUP ASC";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchSubgroupById:
                    {
                        query = "SELECT GROUP_ID , LEDGER_GROUP AS 'Ledger Sub Group'," +
                               "PARENT_GROUP_ID,IMAGE_ID FROM MASTER_LEDGER_GROUP " +
                               "WHERE PARENT_GROUP_ID=(SELECT PARENT_GROUP_ID FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID)";
                        break;
                    }
                case SQLCommand.LedgerGroup.LedgerGroupFetchAll:
                    {
                        query = "SELECT lg.GROUP_CODE,lg.LEDGER_GROUP, " +
                               "t.ledger_group as ParentGroup,mn.NATURE,t1.ledger_group as MainGroup, " +
                               "lg.IMAGE_ID,lg.ACCESS_FLAG FROM master_ledger_group lg " +
                               "LEFT JOIN MASTER_NATURE mn on (mn.nature_id=lg.nature_id) " +
                               "INNER JOIN (SELECT GROUP_ID,LEDGER_GROUP FROM MASTER_LEDGER_GROUP WHERE PARENT_GROUP_ID) as t " +
                               "ON lg.parent_group_id=t.group_id " +
                               "INNER JOIN (SELECT GROUP_ID,LEDGER_GROUP FROM MASTER_LEDGER_GROUP WHERE PARENT_GROUP_ID) as t1 " +
                               "ON lg.main_group_id=t1.group_id";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchSortOrder:
                    {
                        query = "SELECT MAX(SORT_ORDER) AS SORT_ORDER FROM MASTER_LEDGER_GROUP WHERE PARENT_GROUP_ID=?PARENT_GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerGroup.FetchMainGroupSortOrder:
                    {
                        query = "SELECT SORT_ORDER FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID";
                        break;

                    }
                case SQLCommand.LedgerGroup.FetchLedgerGroupIdbyLedgerGroup:
                    {
                        query = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;

                    }
                case SQLCommand.LedgerGroup.FetchHeadOfficeLedgerGroup:
                    {
                        query = "SELECT 'All' AS 'Ledger Group',\n" +
   "       '' AS 'Parent Group' UNION (SELECT lg.LEDGER_GROUP AS 'Ledger Group',\n" +
   "                                        t.ledger_group as 'Parent Group'\n" +
   "\n" +
   "                                   FROM master_ledger_group lg\n" +
   "                                   LEFT JOIN MASTER_NATURE mn\n" +
   "                                     on (mn.nature_id = lg.nature_id)\n" +
   "                                  INNER JOIN (SELECT GROUP_ID, LEDGER_GROUP\n" +
   "                                               FROM MASTER_LEDGER_GROUP\n" +
   "                                              WHERE PARENT_GROUP_ID) as t\n" +
   "                                     ON lg.parent_group_id = t.group_id\n" +
   "                                  INNER JOIN (SELECT GROUP_ID, LEDGER_GROUP\n" +
   "                                               FROM MASTER_LEDGER_GROUP\n" +
   "                                              WHERE PARENT_GROUP_ID) as t1\n" +
   "                                     ON lg.main_group_id = t1.group_id\n" +
   "                                  ORDER BY LEDGER_GROUP);";
                        break;

                    }

            }
            return query;
        }
        #endregion User SQL
    }
}
