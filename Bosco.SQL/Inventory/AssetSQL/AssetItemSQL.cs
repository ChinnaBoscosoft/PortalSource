using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetItemSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetItem).FullName)
            {
                query = GetgroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetgroupSQL()
        {
            string query = "";
            SQLCommand.AssetItem SqlcommandId = (SQLCommand.AssetItem)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetItem.Add:
                    {
                        query = "INSERT INTO ASSET_ITEM\n" +
                                "  (ASSET_CLASS_ID,\n" +
                                "   DEPRECIATION_LEDGER_ID,\n" +
                                "   DISPOSAL_LEDGER_ID,\n" +
                                "   ACCOUNT_LEDGER_ID,\n" +
                                "   ASSET_ITEM,\n" +
                                "   UOM_ID,\n" +
                                "   PREFIX,\n" +
                                "   SUFFIX,\n" +
                                "   RETENTION_YRS,\n" +
                                "   DEPRECIATION_YRS,\n" +
                                "   IS_INSURANCE,\n" +
                                "   IS_AMC,\n" +
                                "   IS_ASSET_DEPRECIATION,\n" +
                                "   STARTING_NO,ASSET_MODE)\n" +
                                "VALUES\n" +
                                "  (?ASSET_CLASS_ID,\n" +
                                "   ?DEPRECIATION_LEDGER_ID,\n" +
                                "   ?DISPOSAL_LEDGER_ID,\n" +
                                "   ?ACCOUNT_LEDGER_ID,\n" +
                                "   ?ASSET_ITEM,\n" +
                                "   ?UOM_ID,\n" +
                                "   ?PREFIX,\n" +
                                "   ?SUFFIX,\n" +
                                "   ?RETENTION_YRS,\n" +
                                "   ?DEPRECIATION_YRS,\n" +
                                "   ?IS_INSURANCE,\n" +
                                "   ?IS_AMC,\n" +
                                "   ?IS_ASSET_DEPRECIATION,\n" +
                                "   ?STARTING_NO,?ASSET_MODE);";
                        break;
                    }

                case SQLCommand.AssetItem.Delete:
                    {
                        query = "DELETE FROM ASSET_ITEM WHERE ITEM_ID=?ITEM_ID";
                        break;
                    }

                case SQLCommand.AssetItem.FetchAll:
                    {
                        query = "SELECT AI.ITEM_ID,\n" +
                        "       AG.ASSET_CLASS_ID,\n" +
                        "       PARENT.ASSET_CLASS AS PARENT_CLASS,\n" +
                        "       AG.ASSET_CLASS AS ASSET_GROUP,\n" +
                        "       AI.ASSET_ITEM AS ASSET_NAME,\n" +
                        "       -- AG.ASSET_CLASS,\n" +
                        "       CONCAT(AG.ASSET_CLASS, ' - ', ASSET_ITEM) AS ASSET_ITEM,\n" +
                        "       AU.SYMBOL,\n" +
                        "       -- AI.ASSET_ITEM,\n" +
                        "       CONCAT(PREFIX, SUFFIX) AS PREFIXSUFFIX,\n" +
                        "       PREFIX,\n" +
                        "       SUFFIX,\n" +
                        "       RETENTION_YRS,\n" +
                        "       DEPRECIATION_YRS,\n" +
                        "       IS_INSURANCE,\n" +
                        "       IS_AMC,\n" +
                        "       STARTING_NO,\n" +
                        "       --       ML.LEDGER_ID AS DEPRECIATION_LEDGER_ID,\n" +
                        "       --       DP.LEDGER_ID AS DISPOSAL_LEDGER_ID,\n" +
                        "       AL.LEDGER_ID AS ACCOUNT_LEDGER_ID,\n" +
                        "       --       ML.LEDGER_NAME AS DEPRECIATION_LEDGER,\n" +
                        "       --       DP.LEDGER_NAME AS DISPOSAL_LEDGER,\n" +
                        "       AL.LEDGER_NAME  AS ACCOUNT_LEDGER,\n" +
                        "       AID.LOCATION_ID\n" +
                        "  FROM ASSET_ITEM AI\n" +
                        "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        "  LEFT JOIN ASSET_LOCATION SL\n" +
                        "    ON AID.LOCATION_ID = SL.LOCATION_ID\n" +
                        " INNER JOIN ASSET_CLASS AG\n" +
                        "    ON AG.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        " INNER JOIN UOM AU\n" +
                        "    ON AU.UOM_ID = AI.UOM_ID\n" +
                        "-- INNER JOIN MASTER_LEDGER ML\n" +
                        "--    ON ML.LEDGER_ID = AI.DEPRECIATION_LEDGER_ID\n" +
                        "-- INNER JOIN MASTER_LEDGER DP\n" +
                        "--    ON DP.LEDGER_ID = AI.DISPOSAL_LEDGER_ID\n" +
                        " INNER JOIN MASTER_LEDGER AL\n" +
                        "    ON AL.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                        "  INNER JOIN ASSET_CLASS PARENT\n" +
                        "    ON AG.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                        " GROUP BY AI.ITEM_ID, AG.ASSET_CLASS_ID\n" +
                        " ORDER BY  AG.ASSET_CLASS,AI.ASSET_ITEM ASC;";


                        break;
                    }

                case SQLCommand.AssetItem.Update:
                    {
                        query = "UPDATE ASSET_ITEM\n" +
                                "   SET ASSET_CLASS_ID         = ?ASSET_CLASS_ID,\n" +
                                "       DEPRECIATION_LEDGER_ID = ?DEPRECIATION_LEDGER_ID,\n" +
                                "       DISPOSAL_LEDGER_ID     = ?DISPOSAL_LEDGER_ID,\n" +
                                "       ACCOUNT_LEDGER_ID      = ?ACCOUNT_LEDGER_ID,\n" +
                                "       ASSET_ITEM             = ?ASSET_ITEM,\n" +
                                "       UOM_ID                = ?UOM_ID,\n" +
                                "       PREFIX                 = ?PREFIX,\n" +
                                "       SUFFIX                 = ?SUFFIX,\n" +
                                "       RETENTION_YRS          = ?RETENTION_YRS,\n" +
                                "       DEPRECIATION_YRS       = ?DEPRECIATION_YRS,\n" +
                                "       IS_INSURANCE              = ?IS_INSURANCE,\n" +
                                "       IS_AMC                    = ?IS_AMC,\n" +
                                "       IS_ASSET_DEPRECIATION    =?IS_ASSET_DEPRECIATION,\n" +
                                "       STARTING_NO            = ?STARTING_NO,\n" +
                                "       ASSET_MODE             =?ASSET_MODE\n" +
                                " WHERE ITEM_ID = ?ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.Fetch:
                    {
                        query = "SELECT ITEM_ID,\n" +
                                    //"       ML.LEDGER_NAME AS LEDGER,\n" +
                                    "       AI.ASSET_CLASS_ID,\n" +
                                    "       AC.ASSET_CLASS,\n" +
                                    "       DEPRECIATION_LEDGER_ID,\n" +
                                    "       DISPOSAL_LEDGER_ID,\n" +
                                    "       ACCOUNT_LEDGER_ID,\n" +
                                    "       ASSET_ITEM,\n" +
                                    "       AI.UOM_ID,\n" +
                                    "       SYMBOL,\n" +
                                    "       PREFIX,\n" +
                                    "       SUFFIX,\n" +
                                    "       RETENTION_YRS,\n" +
                                    "       DEPRECIATION_YRS,\n" +
                                    "       IS_INSURANCE,\n" +
                                    "       IS_AMC,\n" +
                                    "       IS_ASSET_DEPRECIATION,\n" +
                                    "       ASSET_MODE,\n" +
                                    "       STARTING_NO,ASSET_MODE\n" +
                                    "  FROM ASSET_ITEM AI\n" +
                                    " INNER JOIN ASSET_CLASS AC\n" +
                                    "    ON AI.ASSET_CLASS_ID=AC.ASSET_CLASS_ID\n" +
                                    //" INNER JOIN MASTER_LEDGER ML\n" +
                                    //"    ON ML.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                                    " LEFT JOIN UOM\n" +
                                    "    ON UOM.UOM_ID=AI.UOM_ID\n" +
                                    " WHERE ITEM_ID = ?ITEM_ID";
                        break;
                    }

                case SQLCommand.AssetItem.FetchAllAssetItems:
                    {
                        query = "SELECT AI.ITEM_ID,PARENT.ASSET_CLASS AS 'Parent Class', AI.ASSET_ITEM AS 'Asset Item', AC.ASSET_CLASS AS 'Asset Class'\n" +
                        "  FROM ASSET_ITEM AI\n" +
                        " INNER JOIN ASSET_CLASS AC\n" +
                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        "  LEFT JOIN ASSET_CLASS PARENT\n" +
                        "    ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                        " GROUP BY AI.ASSET_ITEM\n" +
                        " ORDER BY AC.ASSET_CLASS, AI.ASSET_ITEM ASC;";


                        break;
                    }
                case SQLCommand.AssetItem.FetchAssetItemIdByName:
                    {
                        query = "SELECT ITEM_ID FROM ASSET_ITEM WHERE ASSET_ITEM=?ASSET_ITEM";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
