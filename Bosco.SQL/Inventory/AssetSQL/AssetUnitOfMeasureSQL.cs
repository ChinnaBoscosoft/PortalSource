using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AssetUnitOfMeasureSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetUnitOfMeasure).FullName)
            {
                query = GetLocationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Location details.
        /// </summary>
        /// <returns></returns>
        private string GetLocationSQL()
        {
            string query = "";
            SQLCommand.AssetUnitOfMeasure sqlCommandId = (SQLCommand.AssetUnitOfMeasure)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.AssetUnitOfMeasure.Add:
                    {
                        query = "INSERT INTO UOM\n" +
                                "  (SYMBOL,\n" +
                                "   NAME)\n" +
                                "VALUES\n" +
                                "   (?SYMBOL,\n" +
                                "   ?NAME)";
                        break;
                    }
                case SQLCommand.AssetUnitOfMeasure.Update:
                    {
                        query = "UPDATE UOM SET \n" +
                                "                           SYMBOL = ?SYMBOL,\n" +
                                "                           NAME = ?NAME\n" +
                                " WHERE UOM_ID = ?UOM_ID";
                        break;
                    }
                case SQLCommand.AssetUnitOfMeasure.Delete:
                    {
                        query = "DELETE FROM UOM WHERE UOM_ID = ?UOM_ID";

                        break;
                    }
                case SQLCommand.AssetUnitOfMeasure.Fetch:
                    {
                        query = "SELECT UOM_ID,\n" +
                                "       SYMBOL,\n" +
                                "       NAME\n" +
                                "  FROM UOM\n" +
                                " WHERE UOM_ID = ?UOM_ID";

                        break;
                    }
                case SQLCommand.AssetUnitOfMeasure.FetchAll:
                    {
                        query = "SELECT UOM_ID,\n" +
                                "       SYMBOL,\n" +
                                "       NAME\n" +
                                "  FROM UOM";
                        break;
                    }

                case SQLCommand.AssetUnitOfMeasure.FetchForFirstUnit:
                    {
                        query = "SELECT UOM_ID, SYMBOL FROM UOM WHERE SYMBOL <> ''";
                        break;
                    }

                case SQLCommand.AssetUnitOfMeasure.FetchUnitOfMeasureId:
                    {
                        query = "SELECT UOM_ID FROM UOM WHERE SYMBOL=?SYMBOL";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}