using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetClassSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetClass).FullName)
            {
                query = GetClassSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetClassSQL()
        {
            string query = "";
            SQLCommand.AssetClass SqlcommandId = (SQLCommand.AssetClass)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetClass.Add:
                    {
                        query = "INSERT INTO ASSET_CLASS (" +
                                "ASSET_CLASS," +
                                "PARENT_CLASS_ID," +
                                "METHOD_ID," +
                                "DEP_PERCENTAGE)VALUES(" +
                                "?ASSET_CLASS, " +
                                "?PARENT_CLASS_ID," +
                                "?METHOD_ID," +
                                "?DEP_PERCENTAGE)";
                        break;
                    }
                case SQLCommand.AssetClass.Update:
                    {
                        query = "UPDATE ASSET_CLASS SET " +
                                "ASSET_CLASS=?ASSET_CLASS," +
                                "PARENT_CLASS_ID=?PARENT_CLASS_ID," +
                                "METHOD_ID=?METHOD_ID," +
                                "DEP_PERCENTAGE=?DEP_PERCENTAGE " +
                                "WHERE ASSET_CLASS_ID =?ASSET_CLASS_ID";
                        break;
                    }
                case SQLCommand.AssetClass.FetchAll:
                    {
                        query = "SELECT ASSET_CLASS_ID,\n" +
                                "       PARENT_CLASS_ID,\n" +
                                "       ASSET_CLASS,\n" +
                                "       ADM.DEP_METHOD,\n" +
                                "       DEP_PERCENTAGE\n" +
                                "  FROM ASSET_CLASS AC\n" +
                                " LEFT JOIN ASSET_DEP_METHOD ADM\n" +
                                "    ON AC.METHOD_ID = ADM.METHOD_ID\n" +
                                " WHERE ASSET_CLASS_ID NOT IN (1)\n" +
                                " ORDER BY AC.ASSET_CLASS_ID";
                        //"ORDER BY GROUP_NAME";
                        break;
                    }
                case SQLCommand.AssetClass.FetchSelectedClass:
                    {
                        query = "SELECT AC.PARENT_CLASS_ID,\n" +
                        "       AC.ASSET_CLASS_ID,\n" +
                        "       PARENT.ASSET_CLASS AS 'Parent Class',\n" +
                        "       AC.ASSET_CLASS AS 'Asset Class',\n" +
                        "       AI.ASSET_ITEM AS 'Asset Item',\n" +
                        "       AC.METHOD_ID,\n" +
                        "       ADM.DEP_METHOD,\n" +
                        "       AC.DEP_PERCENTAGE\n" +
                        "  FROM ASSET_CLASS AC\n" +
                        "  LEFT JOIN ASSET_DEP_METHOD ADM\n" +
                        "    ON AC.METHOD_ID = ADM.METHOD_ID\n" +
                        "  LEFT JOIN ASSET_ITEM AI\n" +
                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        "  INNER JOIN ASSET_CLASS PARENT\n" +
                        "    ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                        " WHERE AC.ASSET_CLASS_ID IN(?ASSET_CLASS_ID) AND AI.ITEM_ID >0 \n" +
                        " ORDER BY AC.ASSET_CLASS, AI.ASSET_ITEM ASC;";
                        break;
                    }
                case SQLCommand.AssetClass.Delete:
                    {
                        query = "DELETE FROM ASSET_CLASS WHERE ASSET_CLASS_ID=?ASSET_CLASS_ID OR PARENT_CLASS_ID =?ASSET_CLASS_ID";
                        break;
                    }
                case SQLCommand.AssetClass.FetchbyID:
                    {
                        query = "SELECT ASSET_CLASS_ID, ASSET_CLASS, PARENT_CLASS_ID, METHOD_ID, DEP_PERCENTAGE\n" +
                                "  FROM ASSET_CLASS\n" +
                                " WHERE ASSET_CLASS_ID = ?ASSET_CLASS_ID";
                        break;
                    }
                case SQLCommand.AssetClass.FetchAssetSubClassbyAssetParentId:
                    {

                        query = "SELECT ASSET_CLASS_ID, ASSET_CLASS AS 'Asset Sub Class', PARENT_CLASS_ID\n" +
                       "  FROM ASSET_CLASS\n" +
                       " WHERE FIND_IN_SET(ASSET_CLASS_ID, ?ASSET_CLASS_ID) > 0\n" +
                       "UNION\n" +
                       "SELECT ASSET_CLASS_ID, ASSET_CLASS AS 'Asset Sub Class', PARENT_CLASS_ID\n" +
                       "  FROM ASSET_CLASS\n" +
                       " WHERE FIND_IN_SET(PARENT_CLASS_ID, ?ASSET_CLASS_ID) > 0;";

                        break;
                    }
                case SQLCommand.AssetClass.FetchAssetClassIdByName:
                    {
                        query = "SELECT ASSET_CLASS_ID FROM ASSET_CLASS WHERE ASSET_CLASS=?ASSET_CLASS";
                        break;
                    }
                case SQLCommand.AssetClass.FetchClassNameByParentID:
                    {
                        query = "SELECT ASSET_CLASS_ID,PARENT_CLASS_ID,ASSET_CLASS FROM ASSET_CLASS WHERE  PARENT_CLASS_ID IN( " +
                                "   SELECT ASSET_CLASS_ID FROM ASSET_CLASS WHERE PARENT_CLASS_ID IN(1))";
                        ;
                        break;
                    }
                case SQLCommand.AssetClass.FetchDepreciationMethod:
                    {
                        query = "SELECT METHOD_ID, DEP_METHOD " +
                                "FROM ASSET_DEP_METHOD";
                        break;
                    }

            }
            return query;
        }
        #endregion
    }
}
