using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class HeadOfficeSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.HeadOffice).FullName)
            {
                query = GetHeadOfficeSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetHeadOfficeSQL()
        {
            string query = "";
            SQLCommand.HeadOffice sqlCommandId = (SQLCommand.HeadOffice)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.HeadOffice.Add:
                    {
                        query = "INSERT INTO HEAD_OFFICE ( " +
                               "HEAD_OFFICE_CODE, " +
                               "HEAD_OFFICE_NAME, " +
                               "TYPE_ID, " +
                               "BELONGSTO," +
                               "DESIGNATION," +
                               "ORG_MAIL_ID," +
                               "INCHARGE_NAME," +
                               "ADDRESS," +
                               "MOBILE_NO," +
                               "PHONE_NO," +
                               "COUNTRY_ID," +
                               "STATE_ID," +
                               "PINCODE," +
                               "SR_INCHARGE_NAME," +
                               "SR_MOBILE_NO," +
                               "SR_PHONE_NO," +
                               "SR_MAIL_ID," +
                               "CITY," +
                               "HOST_NAME," +
                               "DB_NAME," +
                               "USERNAME," +
                               "PASSWORD," +
                               "STATUS," +
                               "CREATED_DATE," +
                               "CREATED_BY," +
                               "MODIFIED_DATE," +
                               "MODIFIED_BY," +
                               "COUNTRY_CODE," +
                               "USER_CREATED_STATUS," +
                               " ACCOUNTING_YEAR_TYPE ) VALUES( " +
                               "?HEAD_OFFICE_CODE, " +
                               "?HEAD_OFFICE_NAME, " +
                               "?TYPE_ID, " +
                               "?BELONGSTO," +
                               "?DESIGNATION," +
                               "?ORG_MAIL_ID," +
                               "?INCHARGE_NAME," +
                               "?ADDRESS," +
                               "?MOBILE_NO," +
                               "?PHONE_NO," +
                               "?COUNTRY_ID," +
                               "?STATE_ID," +
                               "?PINCODE," +
                               "?SR_INCHARGE_NAME," +
                               "?SR_MOBILE_NO," +
                               "?SR_PHONE_NO," +
                               "?SR_MAIL_ID," +
                               "?CITY," +
                               "?HOST_NAME," +
                               "?DB_NAME," +
                               "?USERNAME," +
                               "?PASSWORD," +
                               "?STATUS," +
                               "?CREATED_DATE," +
                               "?CREATED_BY," +
                               "?MODIFIED_DATE," +
                               "?MODIFIED_BY," +
                               "?COUNTRY_CODE," +
                               "?USER_CREATED_STATUS,?ACCOUNTING_YEAR_TYPE )";
                        break;
                    }
                case SQLCommand.HeadOffice.Update:
                    {
                        query = "UPDATE HEAD_OFFICE SET " +
                                "HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE, " +
                               "HEAD_OFFICE_NAME=?HEAD_OFFICE_NAME, " +
                               "TYPE_ID=?TYPE_ID, " +
                               "BELONGSTO=?BELONGSTO," +
                               "DESIGNATION=?DESIGNATION," +
                               "ORG_MAIL_ID=?ORG_MAIL_ID," +
                               "INCHARGE_NAME=?INCHARGE_NAME," +
                               "ADDRESS=?ADDRESS," +
                               "MOBILE_NO=?MOBILE_NO," +
                               "PHONE_NO=?PHONE_NO," +
                               "COUNTRY_ID=?COUNTRY_ID," +
                               "STATE_ID=?STATE_ID," +
                               "PINCODE=?PINCODE," +
                               "SR_INCHARGE_NAME=?SR_INCHARGE_NAME," +
                               "SR_MOBILE_NO=?SR_MOBILE_NO," +
                               "SR_PHONE_NO=?SR_PHONE_NO," +
                               "SR_MAIL_ID=?SR_MAIL_ID," +
                               "CITY=?CITY," +
                               "COUNTRY_CODE=?COUNTRY_CODE," +
                               "MODIFIED_DATE=?MODIFIED_DATE," +
                               "MODIFIED_BY=?MODIFIED_BY,ACCOUNTING_YEAR_TYPE=?ACCOUNTING_YEAR_TYPE " +
                               "WHERE  HEAD_OFFICE_CODE =?CODE";
                        break;
                    }
                case SQLCommand.HeadOffice.Delete:
                    {
                        query = "DELETE FROM  HEAD_OFFICE WHERE HEAD_OFFICE_CODE = ?CODE";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchAllHeadOffice:
                    {
                        query = "SELECT HEAD_OFFICE_CODE,HEAD_OFFICE_NAME,STATUS,CREATED_DATE FROM HEAD_OFFICE WHERE STATUS=1";
                        break;
                    }
                case SQLCommand.HeadOffice.Fetch:
                    {
                        query = "SELECT " +
                                "HEAD_OFFICE_CODE, " +
                                   "HEAD_OFFICE_NAME, " +
                                   "TYPE_ID, " +
                                   "BELONGSTO," +
                                   "DESIGNATION," +
                                   "ORG_MAIL_ID," +
                                   "INCHARGE_NAME," +
                                   "ADDRESS," +
                                   "MOBILE_NO," +
                                   "COUNTRY_CODE," +
                                   "PHONE_NO," +
                                   "ADDRESS," +
                                   "COUNTRY_ID," +
                                   "STATE_ID," +
                                   "PINCODE," +
                                   "SR_INCHARGE_NAME," +
                                   "SR_MOBILE_NO," +
                                   "SR_MAIL_ID," +
                                   "STATUS," +
                                   "SR_PHONE_NO,USER_CREATED_STATUS," +
                                   "CITY,ACCOUNTING_YEAR_TYPE" +
                                   " FROM " +
                                   "HEAD_OFFICE  WHERE HEAD_OFFICE_ID=?HEAD_OFFICE_ID  ";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchByCode:
                    {
                        query = "SELECT " +
                                "HEAD_OFFICE_CODE, " +
                                   "HEAD_OFFICE_NAME, " +
                                   "TYPE_ID, " +
                                   "BELONGSTO," +
                                   "DESIGNATION," +
                                   "ORG_MAIL_ID," +
                                   "INCHARGE_NAME," +
                                   "ADDRESS," +
                                   "MOBILE_NO," +
                                   "COUNTRY_CODE," +
                                   "PHONE_NO," +
                                   "COUNTRY_ID," +
                                   "STATE_ID," +
                                   "PINCODE," +
                                   "SR_INCHARGE_NAME," +
                                   "SR_MOBILE_NO," +
                                   "SR_MAIL_ID," +
                                   "STATUS," +
                                   "SR_PHONE_NO,USER_CREATED_STATUS," +
                                   "CITY" +
                                   " FROM " +
                                   "HEAD_OFFICE  WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE ";
                        break;
                    }
                case SQLCommand.HeadOffice.LoginUserHeadOffice:
                    {
                        query = "SELECT HEAD_OFFICE_ID,HEAD_OFFICE_CODE ,HEAD_OFFICE_NAME, " +
                               "TYPE, BELONGSTO,ADDRESS,COUNTRY,STATE,PINCODE " +
                               "FROM HEAD_OFFICE HO " +
                               "INNER JOIN head_OFFICE_TYPE HOT ON HO.TYPE_ID=HOT.TYPE_ID " +
                               "INNER JOIN country c on c.country_id=ho.country_id " +
                               "INNER JOIN state s on s.state_id=ho.state_id " +
                               "WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchAll:
                    {
                        //TRIM(BOTH ',' FROM concat_ws(',',address, Country,state,Pincode)) as Address,
                        query = @"SELECT HEAD_OFFICE_ID,HEAD_OFFICE_CODE AS 'Code',head_office_name as 'Name',
                                      BelongsTo,Type,CONCAT(cast(STATUS as char),',',HEAD_OFFICE_CODE) AS Status,
                                     case when Status=1 then 'Created' else case when status=2 then 'Activated' else 'DeActivated' end end as 'H.O Status' from HEAD_OFFICE HO
                                     INNER JOIN head_OFFICE_TYPE HOT ON HO.TYPE_ID=HOT.TYPE_ID
                                     INNER JOIN country c on c.country_id=ho.country_id
                                     INNER JOIN state s on s.state_id=ho.state_id  
                                     group by Head_office_id order by status ASC ,head_office_code asc";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchAllToExport:
                    {
                        query = @"SELECT HEAD_OFFICE_CODE AS 'Office Code',HEAD_OFFICE_NAME as 'Office Name',
                                TYPE, BELONGSTO,
                                case when Status=1 then 'Created' else case when status=2 then 'Activated' else 'DeActivated' end end as 'Office Status' from HEAD_OFFICE HO
                                INNER JOIN head_OFFICE_TYPE HOT ON HO.TYPE_ID=HOT.TYPE_ID
                                INNER JOIN country c on c.country_id=ho.country_id
                                INNER JOIN state s on s.state_id=ho.state_id
                                group by Head_office_id order by status ASC ,head_office_code asc;";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchActiveHeadOffice:
                    {
                        query = @"SELECT HEAD_OFFICE_ID,lower(HEAD_OFFICE_CODE) as HEAD_OFFICE_CODE  FROM HEAD_OFFICE WHERE STATUS=2";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchDatabase:
                    {
                        query = "SELECT HEAD_OFFICE_ID, HEAD_OFFICE_CODE, " +
                                "HEAD_OFFICE_NAME, HOST_NAME, DB_NAME, USERNAME, PASSWORD, " +
                                "CONCAT('server=', HOST_NAME, ';database=', DB_NAME, " +
                                "';uid=', USERNAME, ';pwd=', PASSWORD, ';pooling=false') AS DB_CONNECTION " +
                                "FROM HEAD_OFFICE " +
                                "WHERE HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE";

                        break;
                    }
                case SQLCommand.HeadOffice.FetchBranch:
                    {
                        query = "SELECT BO.BRANCH_OFFICE_ID,lower(BO.BRANCH_OFFICE_CODE) AS BRANCH_OFFICE_CODE , " +
                                "BO.BRANCH_OFFICE_NAME,0 AS 'SELECT' FROM " +
                                "HEAD_OFFICE HO LEFT JOIN BRANCH_OFFICE BO ON BO.HEAD_OFFICE_CODE=HO.HEAD_OFFICE_CODE " +
                                "WHERE HO.HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE AND BO.STATUS=2 AND BO.IS_SUBBRANCH=0 ORDER BY BO.BRANCH_OFFICE_NAME ASC";
                        break;
                    }

                case SQLCommand.HeadOffice.FetchBranchByUser:
                    {
                        query = "SELECT BO.BRANCH_OFFICE_ID,lower(BO.BRANCH_OFFICE_CODE) AS BRANCH_OFFICE_CODE , " +
                                "BO.BRANCH_OFFICE_NAME,0 AS 'SELECT' FROM " +
                                "HEAD_OFFICE HO LEFT JOIN BRANCH_OFFICE BO ON BO.HEAD_OFFICE_CODE=HO.HEAD_OFFICE_CODE " +
                                " INNER JOIN BRANCH_USER BU ON BU.BRANCH_ID = BO.BRANCH_OFFICE_ID " +
                                "WHERE HO.HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE AND BO.STATUS=2 AND BO.IS_SUBBRANCH=0 AND BU.USER_ID =?USER_ID ORDER BY BO.BRANCH_OFFICE_NAME ASC";
                        break;
                    }

                case SQLCommand.HeadOffice.FetchBranchDetails:
                    {
                        query = "SELECT lower(BO.BRANCH_OFFICE_CODE) AS BRANCH_OFFICE_CODE,\n" +
                                "       BO.BRANCH_OFFICE_NAME,\n" +
                                "       BO.BRANCH_EMAIL_ID,\n" +
                                "       BO.INCHARGE_NAME,\n" +
                                "       BO.ADDRESS,\n" +
                                "       BO.MOBILE_NO,\n" +
                                "       BO.PHONE_NO,\n" +
                                "       BO.PINCODE\n" +
                                "  FROM HEAD_OFFICE HO\n" +
                                "  LEFT JOIN BRANCH_OFFICE BO\n" +
                                "    ON BO.HEAD_OFFICE_CODE = HO.HEAD_OFFICE_CODE\n" +
                                " WHERE HO.HEAD_OFFICE_CODE =?HEAD_OFFICE_CODE\n" +
                                "   AND BO.STATUS = 2\n" +
                                "   AND BO.IS_SUBBRANCH = 0\n" +
                                " ORDER BY BO.BRANCH_OFFICE_NAME ASC;";

                        break;
                    }
                case SQLCommand.HeadOffice.FetchCountry:
                    {
                        query = @"select COUNTRY_ID,COUNTRY_CODE,COUNTRY from country ORDER BY COUNTRY";

                        break;
                    }
                case SQLCommand.HeadOffice.FetchState:
                    {
                        query = @"SELECT STATE_ID,STATE,STATE_CODE FROM STATE WHERE COUNTRY_ID=?COUNTRY_ID ORDER BY STATE";

                        break;
                    }
                case SQLCommand.HeadOffice.FecthType:
                    {
                        query = @"SELECT TYPE_ID,TYPE FROM HEAD_OFFICE_TYPE ORDER BY TYPE";

                        break;
                    }
                case SQLCommand.HeadOffice.Status:
                    {
                        query = @"UPDATE HEAD_OFFICE SET STATUS=?STATUS WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";

                        break;
                    }
                case SQLCommand.HeadOffice.FetchHOAUser:
                    {
                        query = @"SELECT HEAD_OFFICE_CODE,ORG_MAIL_ID,MOBILE_NO FROM head_office WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE;";

                        break;
                    }
                case SQLCommand.HeadOffice.UpdateDB:
                    {
                        query = @"UPDATE HEAD_OFFICE SET HOST_NAME=?HOST_NAME,DB_NAME=?DB_NAME,USERNAME=?USERNAME,PASSWORD=?PASSWORD WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";

                        break;
                    }
                case SQLCommand.HeadOffice.UpdateUserStatus:
                    {
                        query = @"UPDATE HEAD_OFFICE SET USER_CREATED_STATUS=?USER_CREATED_STATUS WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";

                        break;
                    }

                case SQLCommand.HeadOffice.FetchMailId:
                    {
                        query = @"SELECT CONCAT(BO.BRANCH_EMAIL_ID,',',HO.ORG_MAIL_ID) AS MailAddress FROM BRANCH_OFFICE AS BO
                                  INNER JOIN HEAD_OFFICE AS HO ON
                                  BO.HEAD_OFFICE_CODE =HO.HEAD_OFFICE_CODE
                                  WHERE BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE AND HO.HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE AND BO.STATUS=2 AND HO.STATUS=2";
                        break;
                    }

                case SQLCommand.HeadOffice.FetchBranchOfficeMailId:
                    {
                        query = "SELECT BRANCH_EMAIL_ID FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }

                case SQLCommand.HeadOffice.FetchHeadOfficeMailId:
                    {
                        query = "SELECT ORG_MAIL_ID FROM HEAD_OFFICE WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchMainContent:
                    {
                        query = "SELECT MAIL_TYPE,SUBJECT,CONTENT FROM MAIL_INFO";
                        break;
                    }
                case SQLCommand.HeadOffice.ScheduleDataSycnTask:
                    {
                        query = "INSERT INTO DATASYNC_TASK(HEAD_OFFICE_ID, BRANCH_OFFICE_ID,  " +
                                "UPLOADED_ON, XML_FILENAME, STATUS, REMARKS,PROJECT, LOCATION, UPLOADED_BY) VALUES(?HEAD_OFFICE_ID, " +
                                "?BRANCH_OFFICE_ID, NOW(), ?XML_FILENAME, ?STATUS, ?REMARKS,?PROJECT, ?LOCATION, ?UPLOADED_BY)";
                        break;
                    }
                case SQLCommand.HeadOffice.GetActiveOfficeInfo:
                    {
                        query = "SELECT HO.HEAD_OFFICE_ID,BO.BRANCH_OFFICE_ID FROM BRANCH_OFFICE AS BO " +
                                "INNER JOIN HEAD_OFFICE AS HO ON " +
                                "BO.HEAD_OFFICE_CODE =HO.HEAD_OFFICE_CODE " +
                                 "WHERE BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE AND HO.HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE AND BO.STATUS=2 AND HO.STATUS=2";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchDataSyncStatus:
                    {
                        query = "SELECT DST.HEAD_OFFICE_ID,\n" +
                                "       HF.HEAD_OFFICE_CODE,\n" +
                                "       DST.BRANCH_OFFICE_ID,\n" +
                                "       BF.BRANCH_OFFICE_CODE, BF.BRANCH_OFFICE_NAME,\n" +
                                "       DATE_FORMAT(DST.UPLOADED_ON,'%d/%m/%Y %h:%i %p') AS UPLOADED_ON,\n" +
                                "       DATE_FORMAT(DST.STARTED_ON,'%d/%m/%Y %h:%i %p') AS STARTED_ON,\n" +
                                "       DATE_FORMAT(DST.COMPLETED_ON,'%d/%m/%Y %h:%i %p') AS COMPLETED_ON,\n" +
                                "       DST.XML_FILENAME,\n" +
                                "       DSS.ID,\n" +
                                "       DATE_FORMAT(DST.TRANS_DATE_FROM,'%d/%m/%Y') AS DATE_FROM,\n" +
                                "       DATE_FORMAT(DST.TRANS_DATE_TO,'%d/%m/%Y') AS DATE_TO,\n" +
                                "       DST.PROJECT, DST.LOCATION, DST.UPLOADED_BY,\n" +
                                "       DSS.STATUS,DST.REMARKS\n" +
                                "  FROM DATASYNC_TASK AS DST\n" +
                                " INNER JOIN HEAD_OFFICE AS HF\n" +
                                "    ON DST.HEAD_OFFICE_ID = HF.HEAD_OFFICE_ID\n" +
                                " {AND HF.HEAD_OFFICE_CODE =?HEAD_OFFICE_CODE }\n" +
                                " INNER JOIN BRANCH_OFFICE AS BF\n" +
                                "    ON DST.BRANCH_OFFICE_ID = BF.BRANCH_OFFICE_ID\n" +
                                " {AND BF.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }\n" +
                                " INNER JOIN DATASYNC_STATUS AS DSS\n" +
                                "    ON DST.STATUS = DSS.ID \n" +
                                " ORDER BY DST.ID DESC";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchHeadOfficeforCombo:
                    {
                        query = "SELECT HEAD_OFFICE_ID,HEAD_OFFICE_CODE,CONCAT(HEAD_OFFICE_CODE,CONCAT(' - ',HEAD_OFFICE_NAME)) AS HEAD_OFFICE_NAME FROM HEAD_OFFICE WHERE STATUS=2";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchAccountingYearType:
                    {
                        query = "SELECT ACCOUNTING_YEAR_TYPE FROM HEAD_OFFICE WHERE HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.HeadOffice.FetchDataSyncMessage:
                    {
                        query = "SELECT UPLOADED_ON,\n" +
                                "       STARTED_ON,\n" +
                                "       COMPLETED_ON,\n" +
                                "       TRANS_DATE_FROM,\n" +
                                "       TRANS_DATE_TO,\n" +
                                "       REMARKS,\n" +
                                "       PROJECT,\n" +
                                "       CASE\n" +
                                "         WHEN STATUS = 1 THEN\n" +
                                "          'RECEIVED'\n" +
                                "         WHEN STATUS = 2 THEN\n" +
                                "          'INPROGRESS'\n" +
                                "         WHEN STATUS = 3 THEN\n" +
                                "          'CLOSED'\n" +
                                "         WHEN STATUS = 4 THEN\n" +
                                "          'FAILED'\n" +
                                "       END AS STATUS\n" +
                                "  FROM DATASYNC_TASK\n" +
                                " WHERE BRANCH_OFFICE_ID =\n" +
                                "       (SELECT BRANCH_OFFICE_ID\n" +
                                "          FROM BRANCH_OFFICE\n" +
                                "         WHERE BRANCH_OFFICE_CODE =?BRANCH_OFFICE_CODE)";
                        break;
                    }
            }
            return query;
        }
        #endregion HeadOffice SQL
    }
}
