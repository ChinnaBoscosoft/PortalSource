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
                                 "(FIRSTNAME,LASTNAME,CREATEDBY,USER_NAME,PASSWORD,ADDRESS,CONTACT_NO,EMAIL_ID,ROLE_ID,NOTES,STATUS,PLACE,CITY,USER_TYPE,BRANCH_CODE,HEAD_OFFICE_CODE,COUNTRY_CODE)" +
                               " VALUES(?FIRSTNAME,?LASTNAME,?CREATEDBY,?USER_NAME,?PASSWORD,?ADDRESS,?CONTACT_NO,?EMAIL_ID,?ROLE_ID,?NOTES,?STATUS,?PLACE,?CITY,?USER_TYPE,?BRANCH_CODE,?HEAD_OFFICE_CODE,?COUNTRY_CODE);";
                        break;
                    }
                case SQLCommand.User.Update:
                    {
                        query = "UPDATE USER_INFO " +
                                "SET " +
                                        "FIRSTNAME=?FIRSTNAME," +
                                        "LASTNAME=?LASTNAME," +
                                        "CREATEDBY=?CREATEDBY," +
                                        "USER_NAME=?USER_NAME," +
                                        "ADDRESS=?ADDRESS," +
                                        "CONTACT_NO=?CONTACT_NO," +
                                        "EMAIL_ID=?EMAIL_ID," +
                                        "ROLE_ID=?ROLE_ID," +
                                        "NOTES=?NOTES ," +
                                        "PLACE=?PLACE, " +
                                        "CITY=?CITY, " +
                                        "COUNTRY_CODE=?COUNTRY_CODE " +
                                        "WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.Profile:
                    {
                        query = "UPDATE USER_INFO " +
                                "SET " +
                                        "FIRSTNAME=?FIRSTNAME," +
                                        "LASTNAME=?LASTNAME," +
                                        "ADDRESS=?ADDRESS," +
                                        "CONTACT_NO=?CONTACT_NO," +
                                        "EMAIL_ID=?EMAIL_ID," +
                                        "NOTES=?NOTES ," +
                                        "PLACE=?PLACE, " +
                                        "COUNTRY_CODE=?COUNTRY_CODE, " +
                                        "CITY=?CITY " +
                                        "WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.UpdateUserStatus:
                    {
                        query = "UPDATE USER_INFO " +
                                "SET " +
                                "STATUS=?STATUS " +
                                "WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.Delete:
                    {
                        query = "DELETE FROM USER_INFO WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.Fetch:
                    {
                        query = "SELECT USER_ID,FIRSTNAME,LASTNAME, USER_NAME, PASSWORD, ADDRESS, " +
                                "CONTACT_NO,EMAIL_ID,ROLE_ID,NOTES,STATUS,PLACE,CITY,HEAD_OFFICE_CODE,BRANCH_CODE," +
                                "CASE WHEN STATUS=1 THEN 'Active' ELSE 'Inactive' END AS USERSTATUS,(SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS " +
                                "'USERROLE',COUNTRY_CODE FROM USER_INFO " +
                                "WHERE USER_ID=?USER_ID";
                        break;
                    }

                case SQLCommand.User.FetchAllHeadOffice:
                    {
                        query = @"SELECT concat(cast(USER_ID as char),',',cast(STATUS as char)) AS USER_ID,concat(Firstname,' ',LASTNAME) As Name,USER_NAME As Username,
                                    (SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS 'User Role',Head_office_code as 'Head Office',Branch_code as 'Branch Office',
                                    Address,CONTACT_NO as 'Contact No',EMAIL_ID as 'Email',CASE WHEN STATUS=1 THEN 'Active' ELSE 'Inactive' END AS Status
                                    FROM USER_INFO WHERE ROLE_ID=?ROLE_ID {and CREATEDBY=?CREATEDBY} ORDER BY USER_NAME ASC";
                        break;
                    }
                case SQLCommand.User.FetchAllCreatedBy:
                    {
                        query = @"SELECT concat(cast(USER_ID as char),',',cast(STATUS as char)) AS USER_ID,concat(Firstname,' ',LASTNAME) As Name,USER_NAME As Username,
                                    (SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS 'User Role',
                                     Address,CONTACT_NO as 'Contact No',EMAIL_ID as 'Email',CASE WHEN STATUS=1 THEN 'Active' ELSE 'Inactive' END AS Status
                                     FROM USER_INFO WHERE CREATEDBY=?CREATEDBY ORDER BY USER_NAME ASC";
                        break;
                    }
                case SQLCommand.User.FetchAll:
                    {
                        query = @"SELECT Branch_code as 'Branch Office', concat(cast(USER_ID as char),',',cast(STATUS as char)) AS USER_ID,concat(Firstname,' ',LASTNAME) As Name,USER_NAME As Username,
                                    (SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS 'Role',Head_office_code as 'Head Office',
                                    CONTACT_NO as 'Contact No',replace(EMAIL_ID,',',', ') as 'Email',CASE WHEN STATUS=1 THEN 'Active' ELSE 'Inactive' END AS Status
                                    FROM USER_INFO  WHERE CREATEDBY IN(?CREATEDBY) { AND BRANCH_CODE=?BRANCH_CODE } ORDER BY USER_NAME ASC";
                        break;
                    }
                case SQLCommand.User.FetchAllByHeadOfficeUser:
                    {
                        query = @"SELECT Branch_code as 'Branch Office', concat(cast(USER_ID as char),',',cast(STATUS as char)) AS USER_ID,concat(Firstname,' ',LASTNAME) As Name,USER_NAME As Username,
                                    (SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS 'Role',Head_office_code as 'Head Office',
                                    CONTACT_NO as 'Contact No',replace(EMAIL_ID,',',', ') as 'Email',CASE WHEN STATUS=1 THEN 'Active' ELSE 'Inactive' END AS Status
                                    FROM USER_INFO WHERE CREATEDBY IN(0 {, ?CREATEDBY } ) AND USER_NAME <> ?HEAD_OFFICE_CODE  ORDER BY USER_NAME ASC"; // AND ROLE_ID<>1
                        break;
                    }
                case SQLCommand.User.CheckOldPassword:
                    {
                        query = "SELECT USER_ID FROM USER_INFO WHERE PASSWORD=?PASSWORD AND  USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.FetchUserId:
                    {
                        query = "SELECT USER_ID,CONTACT_NO,EMAIL_ID FROM USER_INFO WHERE USER_NAME=?USER_NAME AND STATUS<>0";
                        break;
                    }
                case SQLCommand.User.ResetPassword:
                    {
                        query = "UPDATE  USER_INFO SET PASSWORD=?PASSWORD,PASSWORD_STATUS=?PASSWORD_STATUS WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.ResetPasswordByUserName:
                    {
                        query = "UPDATE  USER_INFO SET PASSWORD=?PASSWORD,PASSWORD_STATUS=?PASSWORD_STATUS WHERE USER_NAME=?USER_NAME";
                        break;
                    }
                case SQLCommand.User.Authenticate:
                    {
                        query = "SELECT UI.USER_ID, UI.USER_NAME, UI.FIRSTNAME, UI.LASTNAME, UI.ADDRESS,UI.PASSWORD, " +
                                "UI.EMAIL_ID, UI.CONTACT_NO, UI.ROLE_ID, UR.USERROLE, " +
                                "UI.USER_TYPE, UI.BRANCH_CODE,UI.HEAD_OFFICE_CODE, 1 AS ADMIN_USER,UI.PASSWORD_STATUS,BR.BRANCH_OFFICE_NAME, " +
                                "'server=192.168.1.7;database=acmepp;uid=app;pwd=app;pooling=false' AS DATABASE_NAME " +
                                "FROM USER_INFO UI " +
                                "LEFT JOIN USER_ROLE UR ON UI.ROLE_ID = UR.USERROLE_ID " +
                                "LEFT JOIN BRANCH_OFFICE BR ON UI.BRANCH_CODE=BR.BRANCH_OFFICE_CODE " +
                                "WHERE UI.USER_NAME = ?USER_NAME " +
                                "AND UI.PASSWORD = ?PASSWORD AND UI.STATUS = ?STATUS;";
                        break;
                    }
                case SQLCommand.User.FetchUserByHeadOffice:
                    {
                        query = "SELECT USER_NAME,PASSWORD,EMAIL_ID,HEAD_OFFICE_CODE FROM USER_INFO WHERE USER_NAME=?USER_NAME";
                        break;
                    }
                case SQLCommand.User.FetchAdminEmail:
                    {
                        query = "SELECT EMAIL_ID FROM USER_INFO WHERE ROLE_ID=1";
                        break;
                    }
                case SQLCommand.User.FetchHeadOfficeUsers:
                    {
                        query = "SELECT USER_ID, USER_NAME FROM USER_INFO WHERE ROLE_ID NOT IN (2) AND USER_NAME <> ?HEAD_OFFICE_CODE AND BRANCH_CODE IS NULL AND STATUS=1";
                        break;
                    }
                case SQLCommand.User.FetchHeadOfficeUsersAdmin:
                    {
                        //query = "SELECT USER_ID, USER_NAME FROM USER_INFO WHERE ROLE_ID NOT IN (1,2) AND BRANCH_CODE IS NULL AND STATUS=1";
                        query = "SELECT USER_ID, USER_NAME FROM USER_INFO WHERE USER_ID = ?USER_ID AND BRANCH_CODE IS NULL AND STATUS=1";
                        break;
                    }
                case SQLCommand.User.FetchBranchOfficeUsers:
                    {
                        query = "SELECT USER_ID, USER_NAME FROM USER_INFO WHERE ROLE_ID NOT IN (1,2) AND HEAD_OFFICE_CODE IS NOT NULL \n" +
                                "AND BRANCH_CODE IS NOT NULL AND STATUS=1 AND BRANCH_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.User.FetchBranchOfficeUsersAdmin:
                    {
                        query = "SELECT USER_ID, USER_NAME FROM USER_INFO WHERE ROLE_ID NOT IN (1,2) AND HEAD_OFFICE_CODE IS NOT NULL \n" +
                                "AND BRANCH_CODE IS NOT NULL AND STATUS=1 AND BRANCH_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }

            }

            return query;
        }

        #endregion User SQL
    }
}
