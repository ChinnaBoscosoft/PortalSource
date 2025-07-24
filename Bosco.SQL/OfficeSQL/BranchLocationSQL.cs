using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class BranchLocationSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.BranchLocation).FullName)
            {
                query = GetBranchLocationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Branch Location details.
        /// </summary>
        /// <returns></returns>
        private string GetBranchLocationSQL()
        {
            string query = "";
            SQLCommand.BranchLocation sqlCommandId = (SQLCommand.BranchLocation)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.BranchLocation.Add:
                    {
                        query = "INSERT INTO BRANCH_LOCATION(BRANCH_ID,LOCATION_NAME)VALUES(?BRANCH_ID,?LOCATION_NAME);";
                        break;
                    }
                case SQLCommand.BranchLocation.Update:
                    {
                        query = "UPDATE BRANCH_LOCATION\n" +
                                "   SET BRANCH_ID = ?BRANCH_ID, LOCATION_NAME = ?LOCATION_NAME\n" +
                                " WHERE LOCATION_ID = ?LOCATION_ID;";
                        break;
                    }
                case SQLCommand.BranchLocation.Delete:
                    {
                        query = "DELETE FROM BRANCH_LOCATION WHERE LOCATION_ID=?LOCATION_ID";
                        break;
                    }
                case SQLCommand.BranchLocation.Fetch:
                    {
                        query = "SELECT LOCATION_ID,BRANCH_ID,LOCATION_NAME FROM BRANCH_LOCATION WHERE LOCATION_ID=?LOCATION_ID";
                        break;
                    }
                case SQLCommand.BranchLocation.FetchAll:
                    {
                        query = "SELECT LOCATION_ID, LOCATION_NAME AS Location, BRANCH_OFFICE_NAME as Branch\n" +
                                "  FROM BRANCH_LOCATION BL\n" +
                                " INNER JOIN BRANCH_OFFICE BO\n" +
                                "    ON BL.BRANCH_ID = BO.BRANCH_OFFICE_ID { WHERE BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }";
                        break;
                    }
                // ON BL.BRANCH_ID = BO.BRANCH_OFFICE_ID WHERE (BL.BRANCH_ID=0)  OR BRANCH_ID=?BRANCH_ID ORDER BY LOCATION_ID;
                case SQLCommand.BranchLocation.FetchLocationbyBranch:
                    {
                        query = "SELECT LOCATION_ID, LOCATION_NAME AS Location, BRANCH_OFFICE_NAME as Branch\n" +
                                "  FROM BRANCH_LOCATION BL\n" +
                                " LEFT JOIN BRANCH_OFFICE BO\n" +
                                "    ON BL.BRANCH_ID = BO.BRANCH_OFFICE_ID WHERE (BL.BRANCH_ID=0 AND BL.LOCATION_NAME ='PRIMARY')  OR BRANCH_ID=?BRANCH_ID ORDER BY LOCATION_ID ";
                        break;
                    }
                case SQLCommand.BranchLocation.FetchBranchLocation:
                    {
                        query = "SELECT GROUP_CONCAT(LOCATION_NAME) as Location\n " +
                              "FROM BRANCH_LOCATION BL\n " +
                              "LEFT JOIN BRANCH_OFFICE BO\n " +
                              "ON BL.BRANCH_ID = BO.BRANCH_OFFICE_ID OR BL.BRANCH_ID=0  WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.BranchLocation.FetchLocationbyBranchLocation:
                    {
                        query = "SELECT LOCATION_ID FROM BRANCH_LOCATION WHERE BRANCH_ID=? BRANCH_ID AND LOCATION_NAME = ?LOCATION_NAME";
                        break;
                    }
            }

            return query;
        }

        #endregion Branch_Location SQL
    }
}
