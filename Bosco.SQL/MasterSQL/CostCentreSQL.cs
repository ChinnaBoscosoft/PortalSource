using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class CostCentreSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.CostCentre).FullName)
            {
                query = GetCostCentreSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the cost centre details.
        /// </summary>
        /// <returns></returns>
        private string GetCostCentreSQL()
        {
            string query = "";
            SQLCommand.CostCentre sqlCommandId = (SQLCommand.CostCentre)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.CostCentre.Add:
                    {
                        query = "INSERT INTO MASTER_COST_CENTRE ( " +
                               "ABBREVATION, " +
                               "COST_CENTRE_NAME, " +
                               "NOTES) VALUES( " +
                               "?ABBREVATION, " +
                               "?COST_CENTRE_NAME, " +
                               "?NOTES )";
                        break;
                    }
                case SQLCommand.CostCentre.Update:
                    {
                        query = "UPDATE MASTER_COST_CENTRE SET " +
                                    "ABBREVATION = ?ABBREVATION, " +
                                    "COST_CENTRE_NAME=?COST_CENTRE_NAME ," +
                                    "NOTES=?NOTES " +
                                    "WHERE COST_CENTRE_ID=?COST_CENTRE_ID ";
                        break;
                    }
                case SQLCommand.CostCentre.Delete:
                    {
                        query = "DELETE FROM MASTER_COST_CENTRE WHERE COST_CENTRE_ID=?COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.CostCentre.Fetch:
                    {
                        query = "SELECT " +
                                "COST_CENTRE_ID, " +
                                "ABBREVATION, " +
                                "COST_CENTRE_NAME, " +
                                "NOTES " +
                            "FROM " +
                                "MASTER_COST_CENTRE " +
                                " WHERE COST_CENTRE_ID=?COST_CENTRE_ID ";
                        break;
                    }
                case SQLCommand.CostCentre.FetchAll:
                    {
                        query = "SELECT " +
                                "COST_CENTRE_ID, " +
                                "ABBREVATION, " +
                                "COST_CENTRE_NAME, " +
                                "NOTES " +
                            "FROM " +
                                "MASTER_COST_CENTRE " +
                                " ORDER BY COST_CENTRE_NAME ASC";
                        break;
                    }
                case SQLCommand.CostCentre.FetchforLookup:
                    {
                        query = "SELECT MS.COST_CENTRE_ID, MS.ABBREVATION, MS.COST_CENTRE_NAME, MS.NOTES\n" +
                        "  FROM PROJECT_COSTCENTRE PCC\n" +
                        " INNER JOIN MASTER_COST_CENTRE MS\n" +
                        "    ON PCC.COST_CENTRE_ID = MS.COST_CENTRE_ID\n" +
                        " WHERE PCC.PROJECT_ID = ?PROJECT_ID\n" +
                        " ORDER BY ABBREVATION ASC;";
                        break;
                    }
                case SQLCommand.CostCentre.SetCostCentreSource:
                    {
                        query = "SELECT P.PROJECT_ID,M.COST_CENTRE_ID,CONCAT(M.COST_CENTRE_NAME, ' (', MCC.COST_CENTRE_CATEGORY_NAME, ')') AS COST_CENTRE_NAME, "+
                                "M.COST_CENTRE_NAME AS COST_CENTRE,0 AS 'SELECT'  "+
                                "FROM MASTER_COST_CENTRE M LEFT JOIN PROJECT_COSTCENTRE P "+
                                "ON M.COST_CENTRE_ID=P.COST_CENTRE_ID "+
                                "INNER JOIN COSTCATEGORY_COSTCENTRE CCA "+
                                 "ON M.COST_CENTRE_ID = CCA.COST_CENTRE_ID "+
                                "INNER JOIN MASTER_COST_CENTRE_CATEGORY MCC "+
                                 "ON MCC.COST_CENTRECATEGORY_ID = CCA.COST_CATEGORY_ID "+
                                 "GROUP BY M.COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.CostCentre.FetchCostCentreCodes:
                    {
                        query = "SELECT ABBREVATION FROM MASTER_COST_CENTRE ORDER BY COST_CENTRE_ID DESC";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}
