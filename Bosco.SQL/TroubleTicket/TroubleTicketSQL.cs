using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class TroubleTicketSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.TroubleTicket).FullName)
            {
                query = GetTroubleTicketSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the software details.
        /// </summary>
        /// <returns></returns>
        private string GetTroubleTicketSQL()
        {
            string query = "";
            SQLCommand.TroubleTicket sqlCommandId = (SQLCommand.TroubleTicket)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TroubleTicket.Add:
                    {
                        query = "INSERT INTO TROUBLE_TICKET (HEAD_OFFICE_CODE,BRANCH_OFFICE_CODE,SUBJECT,DESCRIPTION,PRIORITY,\n" +
                               "POSTED_DATE,COMPLETED_DATE,ATTACH_FILE_NAME,POSTED_BY,REPLIED_TICKET_ID,USER_NAME,PHYSICAL_FILE_NAME,STATUS)\n" +
                               "VALUES(?HEAD_OFFICE_CODE,?BRANCH_OFFICE_CODE,?SUBJECT,?DESCRIPTION,?PRIORITY,?POSTED_DATE,\n" +
                               "?COMPLETED_DATE,?ATTACH_FILE_NAME,?POSTED_BY,?REPLIED_TICKET_ID,?USER_NAME,?PHYSICAL_FILE_NAME,?STATUS)";
                        break;
                    }
                case SQLCommand.TroubleTicket.Update:
                    {
                        query = "UPDATE TROUBLE_TICKET SET HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE,BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE,\n" +
                                "SUBJECT=?SUBJECT,DESCRIPTION=?DESCRIPTION,PRIORITY=?PRIORITY,POSTED_DATE=?POSTED_DATE,\n" +
                                "COMPLETED_DATE=?COMPLETED_DATE,ATTACH_FILE_NAME=?ATTACH_FILE_NAME,POSTED_BY=?POSTED_BY,\n" +
                                "REPLIED_TICKET_ID=?REPLIED_TICKET_ID,USER_NAME=?USER_NAME,PHYSICAL_FILE_NAME=?PHYSICAL_FILE_NAME WHERE TICKET_ID=?TICKET_ID";
                        break;
                    }
                case SQLCommand.TroubleTicket.FetchAll:
                    {
                        query = "SELECT TICKET_ID,HEAD_OFFICE_CODE,BRANCH_OFFICE_CODE,SUBJECT, " +
                                "CASE WHEN STATUS=1 THEN 'Posted'" +
                                "WHEN STATUS=2 THEN 'Completed'" +
                                "WHEN STATUS=3 THEN 'Inprogress'" +
                                "WHEN STATUS=4 THEN 'Clarification'" +
                                "END as STATUS," +
                               "DESCRIPTION,CASE WHEN PRIORITY=1 THEN 'High' " +
                               "WHEN PRIORITY=2 THEN 'Medium' ELSE 'Low' END AS PRIORITY, " +
                               "DATE_FORMAT(POSTED_DATE,'%b %d %Y %h:%i %p') AS POSTED_DATE, " +
                               "DATE_FORMAT(COMPLETED_DATE,'%b %d %Y %h:%i %p') AS COMPLETED_DATE, " +
                               "ATTACH_FILE_NAME,POSTED_BY,PHYSICAL_FILE_NAME, " +
                               "REPLIED_TICKET_ID,USER_NAME FROM TROUBLE_TICKET WHERE REPLIED_TICKET_ID=0 " +
                              "{ AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } { AND BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE } ORDER BY TICKET_ID DESC";
                        break;
                    }
                case SQLCommand.TroubleTicket.Delete:
                    {
                        query = "DELETE FROM TROUBLE_TICKET WHERE TICKET_ID=?TICKET_ID";
                        break;
                    }
                case SQLCommand.TroubleTicket.FetchTicketById:
                    {
                        query = "SELECT HEAD_OFFICE_CODE, BRANCH_OFFICE_CODE, SUBJECT, DESCRIPTION,\n" +
                                "PRIORITY, POSTED_DATE, COMPLETED_DATE, ATTACH_FILE_NAME,POSTED_BY\n" +
                                "POSTED_BY, REPLIED_TICKET_ID, USER_NAME, PHYSICAL_FILE_NAME\n" +
                                "FROM TROUBLE_TICKET WHERE TICKET_ID=?TICKET_ID;";
                        break;
                    }
                case SQLCommand.TroubleTicket.UpdateStatus:
                    {
                        query = "UPDATE TROUBLE_TICKET SET STATUS=?STATUS WHERE TICKET_ID=?TICKET_ID";
                        break;
                    }
                case SQLCommand.TroubleTicket.FetchReplies:
                    {
                        query = @"SELECT TICKET_ID,REPLIED_TICKET_ID, DATE_FORMAT(POSTED_DATE,'%b %d %Y %h:%i %p') AS REPLY_DATE,SUBJECT,DESCRIPTION,USER_NAME FROM TROUBLE_TICKET 
                                    WHERE REPLIED_TICKET_ID=?REPLIED_TICKET_ID";
                        break;
                    }
                case SQLCommand.TroubleTicket.FetchUserDetailsByBOCode:
                    {
                        query = "SELECT USER_ID, USER_NAME\n" +
                                "  FROM USER_INFO\n" +
                                " WHERE BRANCH_CODE = ?BRANCH_OFFICE_CODE \n" +
                                "   AND HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.TroubleTicket.FetchTicketsByBranch:
                    {
                        //query = "SELECT tt.TICKET_ID,\n" +
                        //        "       tt.REPLIED_TICKET_ID,\n" +
                        //        "       DATE_FORMAT(tt.POSTED_DATE, '%b %d %Y %h:%i %p') AS REPLY_DATE,\n" +
                        //        "       tt.SUBJECT,\n" +
                        //        "       tt.DESCRIPTION,\n" +
                        //        "       tt.USER_NAME\n" +
                        //        "  FROM TROUBLE_TICKET as tt\n" +
                        //        "  join (SELECT TICKET_ID,\n" +
                        //        "               HEAD_OFFICE_CODE,\n" +
                        //        "               BRANCH_OFFICE_CODE,\n" +
                        //        "               SUBJECT,\n" +
                        //        "               CASE\n" +
                        //        "                 WHEN STATUS = 1 THEN\n" +
                        //        "                  'Posted'\n" +
                        //        "                 WHEN STATUS = 2 THEN\n" +
                        //        "                  'Completed'\n" +
                        //        "                 WHEN STATUS = 3 THEN\n" +
                        //        "                  'Inprogress'\n" +
                        //        "                 WHEN STATUS = 4 THEN\n" +
                        //        "                  'Clarification'\n" +
                        //        "               END as STATUS,\n" +
                        //        "               DESCRIPTION,\n" +
                        //        "               CASE\n" +
                        //        "                 WHEN PRIORITY = 1 THEN\n" +
                        //        "                  'High'\n" +
                        //        "                 WHEN PRIORITY = 2 THEN\n" +
                        //        "                  'Medium'\n" +
                        //        "                 ELSE\n" +
                        //        "                  'Low'\n" +
                        //        "               END AS PRIORITY,\n" +
                        //        "               DATE_FORMAT(POSTED_DATE, '%b %d %Y %h:%i %p') AS POSTED_DATE,\n" +
                        //        "               DATE_FORMAT(COMPLETED_DATE, '%b %d %Y %h:%i %p') AS COMPLETED_DATE,\n" +
                        //        "               ATTACH_FILE_NAME,\n" +
                        //        "               POSTED_BY,\n" +
                        //        "               PHYSICAL_FILE_NAME,\n" +
                        //        "               REPLIED_TICKET_ID,\n" +
                        //        "               USER_NAME\n" +
                        //        "          FROM TROUBLE_TICKET\n" +
                        //        "         WHERE REPLIED_TICKET_ID = 0\n" +
                        //        "           AND HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE\n" +
                        //        "           AND BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE\n" +
                        //        "         ORDER BY TICKET_ID DESC) as t\n" +
                        //        "    on tt.REPLIED_TICKET_ID = t.ticket_id\n" +
                        //        "    or tt.ticket_id = t.ticket_id";

                        //query = "SELECT TICKET_ID,\n" +
                        //        "       SUBJECT,\n" +
                        //        "       DESCRIPTION,\n" +
                        //        "       PRIORITY,\n" +
                        //        "       POSTED_DATE,\n" +
                        //        "       COMPLETED_DATE,\n" +
                        //        "       ATTACH_FILE_NAME,\n" +
                        //        "       POSTED_BY,\n" +
                        //        "       REPLIED_TICKET_ID,\n" +
                        //        "       USER_NAME,\n" +
                        //        "       PHYSICAL_FILE_NAME,\n" +
                        //        "       STATUS\n" +
                        //        "  FROM TROUBLE_TICKET WHERE HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE\n" + 
                        //        "           AND BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE";


                        query = "SELECT tt.TICKET_ID,\n" +
                                 "       tt.REPLIED_TICKET_ID,\n" +
                                 "       tt.POSTED_DATE,TT.COMPLETED_DATE,TT.PRIORITY,TT.ATTACH_FILE_NAME,TT.POSTED_BY,\n" +
                                 "       tt.SUBJECT,TT.PHYSICAL_FILE_NAME,TT.STATUS,\n" +
                                 "       tt.DESCRIPTION,\n" +
                                 "       tt.USER_NAME\n" +
                                 "  FROM TROUBLE_TICKET as tt\n" +
                                 "  join (SELECT TICKET_ID,\n" +
                                 "               HEAD_OFFICE_CODE,\n" +
                                 "               BRANCH_OFFICE_CODE,\n" +
                                 "               SUBJECT,\n" +
                                 "               CASE\n" +
                                 "                 WHEN STATUS = 1 THEN\n" +
                                 "                  'Posted'\n" +
                                 "                 WHEN STATUS = 2 THEN\n" +
                                 "                  'Completed'\n" +
                                 "                 WHEN STATUS = 3 THEN\n" +
                                 "                  'Inprogress'\n" +
                                 "                 WHEN STATUS = 4 THEN\n" +
                                 "                  'Clarification'\n" +
                                 "               END as STATUS,\n" +
                                 "               DESCRIPTION,\n" +
                                 "               CASE\n" +
                                 "                 WHEN PRIORITY = 1 THEN\n" +
                                 "                  'High'\n" +
                                 "                 WHEN PRIORITY = 2 THEN\n" +
                                 "                  'Medium'\n" +
                                 "                 ELSE\n" +
                                 "                  'Low'\n" +
                                 "               END AS PRIORITY,\n" +
                                 "               DATE_FORMAT(POSTED_DATE, '%b %d %Y %h:%i %p') AS POSTED_DATE,\n" +
                                 "               DATE_FORMAT(COMPLETED_DATE, '%b %d %Y %h:%i %p') AS COMPLETED_DATE,\n" +
                                 "               ATTACH_FILE_NAME,\n" +
                                 "               POSTED_BY,\n" +
                                 "               PHYSICAL_FILE_NAME,\n" +
                                 "               REPLIED_TICKET_ID,\n" +
                                 "               USER_NAME\n" +
                                 "          FROM TROUBLE_TICKET\n" +
                                 "         WHERE REPLIED_TICKET_ID = 0\n" +
                                 "           AND HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE\n" +
                                 "           AND BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE\n" +
                                 "         ORDER BY TICKET_ID DESC) as t\n" +
                                 "    on tt.REPLIED_TICKET_ID = t.ticket_id \n" +
                                 "    or tt.ticket_id = t.ticket_id";
                        break;
                    }

            }
            return query;
        }
        #endregion TroubleTicket SQL

    }
}
