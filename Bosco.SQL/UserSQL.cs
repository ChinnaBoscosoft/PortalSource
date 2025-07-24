using System;
using System.Collections.Generic;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class UserSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.User).FullName)
            {
                query = GetUserSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetUserSQL()
        {
            string query = "";
            SQLCommand.User sqlCommandId = (SQLCommand.User)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.User.Add:
                    {
                        query = "INSERT INTO USER_INFO " +
                                 "(FIRSTNAME,LASTNAME, USER_NAME,PASSWORD,NAME,GENDER,ADDRESS,CONTACT_NO,EMAIL_ID,ROLE_ID,USER_PHOTO,NOTES)" +
                               " VALUES(?FIRSTNAME,?LASTNAME,?USER_NAME,?PASSWORD,?USER_NAME,?GENDER,?ADDRESS,?MOBILE_NO,?EMAIL_ID,?USER_TYPE,?USER_PHOTO,?NOTES);";
                        break;
                    }
                case SQLCommand.User.Update:
                    query = "UPDATE USER_INFO " +
                            "SET " +
                                    "FIRSTNAME=?FIRSTNAME," +
                                    "LASTNAME=?LASTNAME," +
                                    "USER_NAME=?USER_NAME," +
                                    "PASSWORD=?PASSWORD," +
                                    "NAME=?USER_NAME," +
                                    "GENDER=?GENDER," +
                                    "ADDRESS=?ADDRESS," +
                                    "CONTACT_NO=?MOBILE_NO," +
                                    "EMAIL_ID=?EMAIL_ID," +
                                    "ROLE_ID=?USER_TYPE," +
                                    "USER_PHOTO=?USER_PHOTO," +
                                    "NOTES=?NOTES " +
                                    "WHERE USER_ID=?USER_ID";
                    break;
                case SQLCommand.User.Delete:
                    {
                        query = "DELETE FROM USER_INFO WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.Fetch:
                    {
                        query = "SELECT USER_ID,FIRSTNAME,LASTNAME, USER_NAME,GENDER, NAME, PASSWORD, ADDRESS, CONTACT_NO AS MOBILE_NO,EMAIL_ID,ROLE_ID AS USER_TYPE,USER_PHOTO,NOTES" +
                                " FROM USER_INFO " +
                                "WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.FetchAll:
                    {
                        query = "SELECT USER_ID,FIRSTNAME,LASTNAME,USER_NAME,GENDER,PASSWORD,NAME,ADDRESS,CONTACT_NO,EMAIL_ID,USER_PHOTO," +
                                 "(SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS USERROLE" +
                                " FROM USER_INFO ORDER BY USER_NAME ASC;";
                        break;
                    }
                case SQLCommand.User.CheckOldPassword:
                    {
                        query = "SELECT USER_ID FROM USER_INFO WHERE PASSWORD=?PASSWORD AND  USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.FetchUserId:
                    {
                        query = "SELECT USER_ID FROM USER_INFO WHERE USER_NAME=?USER_NAME";
                        break;
                    }
                case SQLCommand.User.ResetPassword:
                    {
                        query = "UPDATE  USER_INFO SET PASSWORD=?PASSWORD WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.Authenticate:
                    {
                        query = "SELECT USER_ID, USER_NAME, NAME, FIRSTNAME, LASTNAME, ADDRESS, " +
                                "EMAIL_ID, CONTACT_NO, ROLE_ID, USERROLE, " +
                                "1 AS USER_TYPE, -1 AS BRANCH_ID, 1 AS ADMIN_USER, " +
                                "'server=192.168.1.7;database=acmepp;uid=app;pwd=app;pooling=false' AS DATABASE_NAME " +
                                "FROM USER_INFO UI " +
                                "LEFT JOIN USER_ROLE UR ON UI.ROLE_ID = UR.USERROLE_ID " +
                                "WHERE USER_NAME = ?USER_NAME " +
                                "AND PASSWORD = ?PASSWORD AND STATUS = ?STATUS;";
                        break;
                    }
            }

            return query;
        }

        #endregion User SQL
    }
}
