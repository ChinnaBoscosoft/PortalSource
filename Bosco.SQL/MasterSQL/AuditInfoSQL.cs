using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
namespace Bosco.SQL
{
   public class AuditInfoSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AuditInfo).FullName)
            {
                query = GetAuditInfoSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion 
        public string GetAuditInfoSQL()
        {
            string query = "";
            SQLCommand.AuditInfo SqlCommand = (SQLCommand.AuditInfo)(this.dataCommandArgs.SQLCommandId);
            switch (SqlCommand)
            {
                case SQLCommand.AuditInfo.Add:
                    {
                        query = "INSERT INTO MASTER_AUDITING_INFO ( " +
                               "PROJECT_ID, " +
                               "AUDIT_BEGIN, " +
                               "AUDIT_END, " +
                               "AUDITED_ON, " +
                               "AUDIT_TYPE, " +
                               "DONAUD_ID, " +
                               "NOTES ) VALUES( " +
                               "?PROJECT_ID, " +
                               "?AUDIT_BEGIN, " +
                               "?AUDIT_END, " +
                               "?AUDITED_ON, " +
                               "?AUDIT_TYPE, " +
                               "?DONAUD_ID, " +
                               "?NOTES) ";
                        break;

                    }
                case SQLCommand.AuditInfo.Update:
                    {
                        query = "UPDATE MASTER_AUDITING_INFO SET " +
                                  "PROJECT_ID =?PROJECT_ID, " +
                                  "AUDIT_BEGIN =?AUDIT_BEGIN, " +
                                  "AUDIT_END =?AUDIT_END, " +
                                   "AUDITED_ON =?AUDITED_ON, " +
                                  "AUDIT_TYPE=?AUDIT_TYPE, " +
                                  "DONAUD_ID=?DONAUD_ID, " +
                                  "NOTES=?NOTES " +
                                  " WHERE AUDIT_INFO_ID=?AUDIT_INFO_ID ";

                        break;
                    }
                case SQLCommand.AuditInfo.Delete:
                    {
                        query = "DELETE FROM MASTER_AUDITING_INFO WHERE AUDIT_INFO_ID=?AUDIT_INFO_ID ";
                        break;
                    }
                case SQLCommand.AuditInfo.Fetch:
                    {
                        query = "SELECT " +
                               "AUDIT_INFO_ID, " +
                               "PROJECT_ID, " +
                               "AUDIT_BEGIN, " +
                               "AUDIT_END, " +
                               "AUDITED_ON, " +
                               "AUDIT_TYPE, " +
                               "DONAUD_ID, " +
                               "NOTES " +
                               "FROM " +
                               "MASTER_AUDITING_INFO WHERE AUDIT_INFO_ID=?AUDIT_INFO_ID";
                        break;
                    }
                case SQLCommand.AuditInfo.FetchAll:
                    {
                        query= "SELECT " +
                               "AUDIT_INFO_ID, " +
                               "CONCAT(P.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',  " +
                               "AUDIT_BEGIN, " +
                               "AUDIT_END, " +
                               "AUDITED_ON, " +
                               "AUDIT_TYPE, " +
                               "NAME, " +
                               "A.NOTES " +
                               "FROM " +
                               " MASTER_AUDITING_INFO A " + " INNER JOIN MASTER_PROJECT P ON " +
                               " A.PROJECT_ID=P.PROJECT_ID " +
                               " INNER JOIN MASTER_DONAUD D ON " +
                               " A.DONAUD_ID=D.DONAUD_ID LEFT JOIN MASTER_DIVISION MD ON P.DIVISION_ID=MD.DIVISION_ID ORDER BY PROJECT ASC;";
                        break;
                    }
            }
            return query;
        }
    }
}
