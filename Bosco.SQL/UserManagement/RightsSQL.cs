using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class RightsSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Rights).FullName)
            {
                query = GetRights();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetRights()
        {
            string query = "";
            SQLCommand.Rights sqlCommandId = (SQLCommand.Rights)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Rights.Delete:
                    {
                        query = @"DELETE FROM user_rights WHERE ROLE_ID=?ROLE_ID";
                        break;
                    }
                case SQLCommand.Rights.Fetch:
                    {
                        query = @" SELECT a.ACTIVITY_ID,MODULE,ACTIVITY,m.MODULE_ID,'TRUE' AS ISASSIGNED FROM master_activities a
                                    INNER JOIN master_module m ON a.MODULE_ID=m.MODULE_ID
                                     WHERE ACTIVITY_ID IN(SELECT ACTIVITY_ID FROM user_rights WHERE ROLE_ID=?ROLE_ID)

                                    UNION

                                     SELECT a.ACTIVITY_ID,MODULE,ACTIVITY,m.MODULE_ID,'FALSE' AS ISASSIGNED FROM master_activities a
                                    INNER JOIN master_module m ON a.MODULE_ID=m.MODULE_ID
                                     WHERE ACTIVITY_ID NOT IN(SELECT ACTIVITY_ID FROM user_rights WHERE ROLE_ID=?ROLE_ID);";
                        break;
                    }
                case SQLCommand.Rights.Add:
                    {
                        query = "INSERT INTO user_rights (ACTIVITY_ID,ROLE_ID) VALUES (?ACTIVITY_ID,?ROLE_ID)";
                        break;
                    }
                case SQLCommand.Rights.FetchRightsByRole:
                    {
                        query = "SELECT MM.MODULE,MA.ACTIVITY_CODE,MA.LEVEL,MA.SORT_ORDER FROM MASTER_MODULE MM LEFT JOIN MASTER_ACTIVITIES MA "+
                                "ON MA.MODULE_ID=MM.MODULE_ID "+
                                "LEFT JOIN USER_RIGHTS UR "+
                                "ON UR.ACTIVITY_ID=MA.ACTIVITY_ID "+
                                "WHERE UR.ROLE_ID=?ROLE_D";
                        break;
                    }
                case SQLCommand.Rights.FetchActivities: // for Rights Activities
                    {
                        query = "SELECT MODULE_CODE, MODULE, " +
                                "ACTIVITY_CODE, ACTIVITY " +
                                "FROM RIGHTS_ACTIVITIES WHERE ACCESSIBILITY IN (0,{ ?ACCESSIBILITY }) " +
                                "ORDER BY MODULE_ORDER, ACTIVITY_ORDER";
                        break;
                    }
                case SQLCommand.Rights.FetchModuleList: // for Rights Modules
                    {
                        query = "SELECT DISTINCT MODULE_CODE, MODULE, MODULE_ORDER " +
                                "FROM RIGHTS_ACTIVITIES WHERE ACCESSIBILITY IN (0,{ ?ACCESSIBILITY })" +
                                "ORDER BY MODULE_ORDER ";
                        break;
                    }
                case SQLCommand.Rights.FetchRightsType:
                    {
                        query = "SELECT TYPE " +
                                "FROM RIGHTS_ACTIVITIES " +
                                "WHERE MODULE_CODE=?MODULE_CODE AND ACTIVITY_CODE=?ACTIVITY_CODE AND ACCESSIBILITY IN (0,{ ?ACCESSIBILITY }) " +
                                "ORDER BY MODULE_ORDER ";
                        break;
                    }
                case SQLCommand.Rights.FetchAllRightsByUserGroup: // for User Group Rights
                    {
                        query = "SELECT T.MODULE_CODE,T.MODULE, T.ACTIVITY_CODE,T.ACTIVITY,T.ALLOW FROM (SELECT RA.MODULE_CODE, RA.MODULE, " +
                                "RA.ACTIVITY_CODE, RA.ACTIVITY,CASE WHEN IFNULL(RUG.ALLOW,0)=0 THEN 'false' ELSE 'true' END AS ALLOW,RA.ACCESSIBILITY " +
                                "FROM RIGHTS_ACTIVITIES RA " +
                                "LEFT JOIN RIGHTS_USER RUG ON RA.MODULE_CODE = RUG.MODULE_CODE " +
                                " AND RA.ACTIVITY_CODE = RUG.ACTIVITY_CODE AND " +
                                " RUG.ROLE_ID=?ROLE_ID" +
                                " ORDER BY RA.MODULE_ORDER, RA.ACTIVITY_ORDER) AS T "+
                                "WHERE T.ACCESSIBILITY IN (0,{ ?ACCESSIBILITY })";
                        break;
                    }
                case SQLCommand.Rights.FetchRoleType: // For Role
                    {
                        query = "SELECT USERROLE_ID , USERROLE " +
                                "FROM USER_ROLE { WHERE ACCESSIBILITY=?ACCESSIBILITY } " +
                                "ORDER BY USERROLE";
                        break;
                    }
                case SQLCommand.Rights.InsertRightsByUserRole:
                    {
                        query = "INSERT INTO RIGHTS_USER " +
                                "(MODULE_CODE, ACTIVITY_CODE, ROLE_ID, ALLOW) " +
                                "VALUES (?MODULE_CODE, ?ACTIVITY_CODE, ?ROLE_ID, ?ALLOW)";
                        break;
                    }
                case SQLCommand.Rights.CheckDuplicateUserRightsByUserRole:
                    {
                        query = "SELECT ROLE_ID FROM RIGHTS_USER " +
                                "WHERE MODULE_CODE=?MODULE_CODE AND ACTIVITY_CODE=?ACTIVITY_CODE AND " +
                                "ROLE_ID=?ROLE_ID";
                        break;
                    }
                case SQLCommand.Rights.DeleteRightsByUserRole:
                    {
                        query = "DELETE FROM RIGHTS_USER " +
                                "WHERE MODULE_CODE=?MODULE_CODE AND ACTIVITY_CODE=?ACTIVITY_CODE AND " +
                                " ROLE_ID=?ROLE_ID";
                        break;
                    }
                case SQLCommand.Rights.FetchUserRightsForMenu:    //User and its user types user rights
                    {
                        query = "SELECT RA.MODULE_CODE, RA.ACTIVITY_CODE, RA.TYPE, " +
                             "CASE WHEN IFNULL(RU.ALLOW,0)=0 THEN 'false' ELSE 'true' END AS USER_RIGHTS " +
                             "FROM RIGHTS_ACTIVITIES RA " +
                             "LEFT JOIN RIGHTS_USER RU ON (RA.MODULE_CODE=RU.MODULE_CODE " +
                             "AND RA.ACTIVITY_CODE = RU.ACTIVITY_CODE AND RU.ROLE_ID=?ROLE_ID AND "+
                             "RA.ACCESSIBILITY IN(0 { ,?ACCESSIBILITY) } )";
                        break;
                    }
                case SQLCommand.Rights.FetchRightsByUserRole: // for User Group Rights
                    {
                        query = "SELECT RUG.ROLE_ID, RUG.ALLOW " +
                                "FROM RIGHTS_USER RUG " +
                                "WHERE RUG.MODULE_CODE=?MODULE_CODE AND RUG.ACTIVITY_CODE=?ACTIVITY_CODE AND " +
                                "RUG.ROLE_ID=?ROLE_ID";
                        break;
                    }
            }

            return query;
        }

    }
}
