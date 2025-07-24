using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;


namespace Bosco.SQL
{
    public class LedgerProfileSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.LedgerProfile).FullName)
            {
                query = GetLedgerProfile();
            }

            sqlType = this.sqlType;
            return query;
        }
        private string GetLedgerProfile()
        {
            string query = "";
            SQLCommand.LedgerProfile sqlCommandId = (SQLCommand.LedgerProfile)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.LedgerProfile.Add:
                    {
                        query = "INSERT INTO TDS_CREDTIORS_PROFILE\n" +
                                   "   (NAME,\n" +
                                   "   ADDRESS,\n" +
                                   "   DEDUTEE_TYPE_ID,\n" +
                                   "   NATURE_OF_PAYMENT_ID,\n" +
                                   "   STATE_ID,\n" +
                                   "   PIN_CODE,\n" +
                                   "   CONTACT_NUMBER,\n" +
                                   "   EMAIL,\n" +
                                   "   LEDGER_ID,\n" +
                                   "   PAN_NUMBER,\n" +
                                   "   COUNTRY_ID)\n" +
                                   "VALUES\n" +
                                   "   (?NAME,\n" +
                                   "   ?ADDRESS,\n" +
                                   "   ?DEDUTEE_TYPE_ID,\n" +
                                   "   ?NATURE_OF_PAYMENT_ID,\n" +
                                   "   ?STATE_ID,\n" +
                                   "   ?PIN_CODE,\n" +
                                   "   ?CONTACT_NUMBER,\n" +
                                   "   ?EMAIL,\n" +
                                   "   ?LEDGER_ID,\n" +
                                   "   ?PAN_NUMBER,\n" +
                                   "   ?COUNTRY_ID)";
                        break;
                    }
                case SQLCommand.LedgerProfile.Delete:
                    {
                        query = "DELETE FROM TDS_CREDTIORS_PROFILE WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerProfile.Update:
                    {
                        query = "UPDATE TDS_CREDTIORS_PROFILE\n" +
                                    "   SET NAME                = ?NAME,\n" +
                                    "       DEDUTEE_TYPE_ID     = ?DEDUTEE_TYPE_ID,\n" +
                                    "       NATURE_OF_PAYMENT_ID=?NATURE_OF_PAYMENT_ID,\n" +
                                    "       ADDRESS             = ?ADDRESS,\n" +
                                    "       STATE_ID            = ?STATE_ID,\n" +
                                    "       PIN_CODE            = ?PIN_CODE,\n" +
                                    "       CONTACT_NUMBER      = ?CONTACT_NUMBER,\n" +
                                    "       EMAIL               = ?EMAIL,\n" +
                                    "       LEDGER_ID           = ?LEDGER_ID,\n" +
                                    "       PAN_NUMBER          = ?PAN_NUMBER,\n" +
                                    "       COUNTRY_ID          = ?COUNTRY_ID,\n" +
                                    " WHERE CREDITORS_PROFILE_ID = ?CREDITORS_PROFILE_ID";
                        break;
                    }

                case SQLCommand.LedgerProfile.Fetch:
                    {
                        query = "SELECT CREDITORS_PROFILE_ID,\n" +
                                "       NAME,\n" +
                                "       ADDRESS,\n" +
                                "       STATE_ID,\n" +
                                "       COUNTRY_ID,\n" +
                                "       PIN_CODE,\n" +
                                "       CONTACT_NUMBER,\n" +
                                "       EMAIL,\n" +
                                "       LEDGER_ID,\n" +
                                "       PAN_NUMBER,\n" +
                                "  FROM TDS_CREDTIORS_PROFILE\n" +
                                " WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }

            }
            return query;
        }

        #endregion LedgerProfileSQL

    }
}
