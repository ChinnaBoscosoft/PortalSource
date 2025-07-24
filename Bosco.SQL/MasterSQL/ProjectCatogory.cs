using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace Bosco.SQL
{
    public class ProjectCatogory : IDatabaseQuery
    {
        #region ISQLServerQueryMembers
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ProjectCatogory).FullName)
            {
                query = GetProjectCatogorySQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetProjectCatogorySQL()
        {
            string query = "";
            SQLCommand.ProjectCatogory sqlCommandId = (SQLCommand.ProjectCatogory)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ProjectCatogory.Add:
                    {
                        query = "INSERT INTO MASTER_PROJECT_CATOGORY ( " +
                            " PROJECT_CATOGORY_ID, " +
                            "PROJECT_CATOGORY_NAME ,PROJECT_CATOGORY_GROUP_ID,PROJECT_CATOGORY_ITRGROUP_ID) VALUES( " + // ,PROJECT_CATOGORY_ITRGROUP_ID
                            "?PROJECT_CATOGORY_ID, " +
                            "?PROJECT_CATOGORY_NAME ,?PROJECT_CATOGORY_GROUP_ID,?PROJECT_CATOGORY_ITRGROUP_ID)";  // ,?PROJECT_CATOGORY_ITRGROUP_ID
                        break;
                    }
                case SQLCommand.ProjectCatogory.Update:
                    {
                        query = "UPDATE MASTER_PROJECT_CATOGORY SET " +
                            "PROJECT_CATOGORY_NAME =?PROJECT_CATOGORY_NAME , PROJECT_CATOGORY_GROUP_ID =?PROJECT_CATOGORY_GROUP_ID , PROJECT_CATOGORY_ITRGROUP_ID =?PROJECT_CATOGORY_ITRGROUP_ID " + //, PROJECT_CATOGORY_ITRGROUP_ID =?PROJECT_CATOGORY_ITRGROUP_ID
                            "WHERE PROJECT_CATOGORY_ID = ?PROJECT_CATOGORY_ID";

                        break;
                    }
                case SQLCommand.ProjectCatogory.Delete:
                    {
                        query = "DELETE FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_ID =?PROJECT_CATOGORY_ID";
                        break;
                    }
                case SQLCommand.ProjectCatogory.Fetch:
                    {
                        query = "SELECT " +
                        "PROJECT_CATOGORY_ID, " +
                        "PROJECT_CATOGORY_NAME, " +
                        "PROJECT_CATOGORY_GROUP_ID," +
                        "PROJECT_CATOGORY_ITRGROUP_ID " +
                        " FROM " +
                        "MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_ID=?PROJECT_CATOGORY_ID";
                        break;
                    }
                case SQLCommand.ProjectCatogory.FetchITR:
                    {
                        query = "select PROJECT_CATOGORY_ITRGROUP_ID, PROJECT_CATOGORY_ITRGROUP from master_project_catogory_ITRGroup";
                        break;
                    }
                case SQLCommand.ProjectCatogory.FetchByName:
                    {
                        query = "SELECT " +
                        "PROJECT_CATOGORY_ID, " +
                        "PROJECT_CATOGORY_NAME " +
                        "FROM " +
                        "MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_NAME IN (?PROJECT_CATOGORY_NAME)";
                        break;
                    }
                case SQLCommand.ProjectCatogory.FetchAll:
                    {
                        //query = "SELECT " +
                        //    "PROJECT_CATOGORY_ID, " +
                        //    "PROJECT_CATOGORY_NAME AS 'Name', " +
                        //    "PROJECT_CATOGORY_GROUP AS 'group' " +
                        //    "FROM " +
                        //    "MASTER_PROJECT_CATOGORY MPC LEFT JOIN MASTER_PROJECT_CATOGORY_GROUP MPCG " +
                        //    " ON MPC.PROJECT_CATOGORY_GROUP_ID = MPCG.PROJECT_CATOGORY_GROUP_ID ORDER BY PROJECT_CATOGORY_NAME ASC ";

                        query = @"SELECT
                            PROJECT_CATOGORY_ID,
                            PROJECT_CATOGORY_NAME AS 'Name',
                            PROJECT_CATOGORY_GROUP AS 'group',
                            PROJECT_CATOGORY_ITRGROUP AS 'ITRGroup'
                            FROM
                            MASTER_PROJECT_CATOGORY MPC LEFT JOIN MASTER_PROJECT_CATOGORY_GROUP MPCG
                            ON MPC.PROJECT_CATOGORY_GROUP_ID = MPCG.PROJECT_CATOGORY_GROUP_ID
                            LEFT JOIN master_project_catogory_ITRGroup MPCI ON MPCI.PROJECT_CATOGORY_ITRGROUP_ID=MPC.PROJECT_CATOGORY_ITRGROUP_ID
                            LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_CATEGORY_ID=MPC.PROJECT_CATOGORY_ID
                            LEFT JOIN PROJECT_BRANCH PB ON MP.PROJECT_ID = PB.PROJECT_ID
                            LEFT JOIN BRANCH_LOCATION BL ON BL.LOCATION_ID = PB.LOCATION_ID
                            LEFT JOIN BRANCH_OFFICE BO ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID
                            { WHERE PB.BRANCH_ID =?BRANCH_OFFICE_ID } GROUP BY PROJECT_CATOGORY_NAME ORDER BY PROJECT_CATOGORY_NAME ASC";


                        break;
                    }
                case SQLCommand.ProjectCatogory.ProjectCategoryFetchAll:
                    {
                        query = "SELECT " +
                            "PROJECT_CATOGORY_NAME " +
                            "FROM " +
                            "MASTER_PROJECT_CATOGORY";
                        break;
                    }
                case SQLCommand.ProjectCatogory.MapProjectCategorytoLedger:
                    {
                        query = "INSERT INTO PROJECT_CATEGORY_LEDGER(PROJECT_CATEGORY_ID,LEDGER_ID) VALUES(?PROJECT_CATOGORY_ID,?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.ProjectCatogory.UnmapProjectCategorytoLedger:
                    {
                        query = "DELETE FROM PROJECT_CATEGORY_LEDGER WHERE PROJECT_CATEGORY_ID=?PROJECT_CATEGORY_ID";
                        break;
                    }
                case SQLCommand.ProjectCatogory.ProjectCategoryCount:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_PROJECT_CATOGORY";
                        break;
                    }
                case SQLCommand.ProjectCatogory.CreatUpdateDefaultLedgerDetails:
                    {
                        query = @"INSERT INTO PROJECT_CATEGORY_LEDGER (PROJECT_CATEGORY_ID, LEDGER_ID)
                                    SELECT MPC.PROJECT_CATOGORY_ID, ML.LEDGER_ID FROM MASTER_LEDGER AS ML,
                                    MASTER_PROJECT_CATOGORY AS MPC WHERE ML.ACCESS_FLAG =2 AND PROJECT_CATOGORY_ID =?PROJECT_CATOGORY_ID
                                    ON DUPLICATE KEY UPDATE PROJECT_CATEGORY_ID=MPC.PROJECT_CATOGORY_ID,LEDGER_ID =ML.LEDGER_ID";
                        break;
                    }

            }
            return query;

        }
        #endregion

    }
}
