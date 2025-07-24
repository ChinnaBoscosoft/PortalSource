using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string Query = string.Empty;
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Voucher).FullName)
            {
                Query = VoucherSQLManipulition();
            }

            sqlType = this.sqlType;
            return Query;
        }
        #endregion

        #region SQL Script
        private string VoucherSQLManipulition()
        {
            string Query = string.Empty;
            SQLCommand.Voucher sqlQueryType = (SQLCommand.Voucher)(this.dataCommandArgs.SQLCommandId);
            switch (sqlQueryType)
            {
                #region Insert Query
                case SQLCommand.Voucher.Add:
                    Query = "INSERT INTO MASTER_VOUCHER" +
                            "(" +
                                 "VOUCHER_NAME," +
                                 "VOUCHER_TYPE," +
                                 "VOUCHER_METHOD," +
                                 "PREFIX_CHAR," +
                                 "SUFFIX_CHAR," +
                                 "STARTING_NUMBER," +
                                 "NUMBERICAL_WITH," +
                                 "PREFIX_WITH_ZERO," +
                                 "MONTH," +
                                 "DURATION," +
                                 "ALLOW_DUPLICATE," +
                                 "NOTE" +
                            ")" +
                            " VALUES" +
                            "(" +
                                 "?VOUCHER_NAME," +
                                 "?VOUCHER_TYPE," +
                                 "?VOUCHER_METHOD," +
                                 "?PREFIX_CHAR," +
                                 "?SUFFIX_CHAR," +
                                 "?STARTING_NUMBER," +
                                 "?NUMBERICAL_WITH," +
                                 "?PREFIX_WITH_ZERO," +
                                 "?MONTH," +
                                 "?DURATION," +
                                 "?ALLOW_DUPLICATE," +
                                 "?NOTE" +
                            ");";
                    break;
                #endregion

                #region Updat Query
                case SQLCommand.Voucher.Update:
                    Query = "UPDATE MASTER_VOUCHER " +
                                  "SET " +
                                        "VOUCHER_NAME=?VOUCHER_NAME," +
                                        "VOUCHER_TYPE=?VOUCHER_TYPE," +
                                        "VOUCHER_METHOD= ?VOUCHER_METHOD," +
                                        "PREFIX_CHAR=?PREFIX_CHAR," +
                                        "SUFFIX_CHAR=?SUFFIX_CHAR," +
                                        "STARTING_NUMBER=?STARTING_NUMBER," +
                                        "NUMBERICAL_WITH=?NUMBERICAL_WITH," +
                                        "PREFIX_WITH_ZERO=?PREFIX_WITH_ZERO," +
                                        "MONTH=?MONTH," +
                                        "DURATION=?DURATION," +
                                        "ALLOW_DUPLICATE=?ALLOW_DUPLICATE," +
                                        "NOTE=?NOTE " +
                                 "WHERE VOUCHER_ID=?VOUCHER_ID;";
                    break;
                #endregion

                #region Delete Query
                case SQLCommand.Voucher.Delete:
                    Query = "DELETE FROM MASTER_VOUCHER WHERE VOUCHER_ID=?VOUCHER_ID";
                    break;
                #endregion

                #region Fetch Query
                case SQLCommand.Voucher.FetchByVoucherId:
                    Query = "SELECT "
                                     + "VOUCHER_ID,"
                                     + "VOUCHER_NAME,"
                                     + "VOUCHER_TYPE,"
                                    + "VOUCHER_METHOD,"
                                    + "PREFIX_CHAR,"
                                    + "SUFFIX_CHAR,"
                                    + "STARTING_NUMBER,"
                                    + "NUMBERICAL_WITH,"
                                    + "PREFIX_WITH_ZERO,"
                                    + "MONTH,"
                                    + "DURATION,"
                                    + "ALLOW_DUPLICATE,"
                                    + "NOTE"
                             + " FROM MASTER_VOUCHER"
                                  + " WHERE VOUCHER_ID=?VOUCHER_ID";
                    break;
                case SQLCommand.Voucher.FetchAll:
                    Query = "SELECT " +
                                    "VOUCHER_ID," +
                                     "VOUCHER_NAME," +
                                     " CASE" +
                                        " WHEN" +
                                               " VOUCHER_TYPE=1 THEN 'Receipts'" +
                                         " WHEN" +
                                                " VOUCHER_TYPE=2 THEN 'Payments'" +
                                         " WHEN" +
                                                " VOUCHER_TYPE=3 THEN 'Contra'" +
                                         " ELSE" +
                                                 " 'Journal'" +
                                        " END  AS 'VOUCHER_TYPE'," +
                                      " CASE" +
                                         " WHEN" +
                                                 " VOUCHER_METHOD=1 THEN 'Automatic'" +
                                         " ELSE" +
                                                 " 'Manual'" +
                                     "END AS  VOUCHER_METHOD," +
                                    " PREFIX_CHAR," +
                                     "SUFFIX_CHAR," +
                                     "STARTING_NUMBER," +
                                     "NUMBERICAL_WITH," +
                                     "PREFIX_WITH_ZERO," +
                                     "MONTH" +
                                  " FROM MASTER_VOUCHER" +
                                  " ORDER BY VOUCHER_NAME ASC;";
                    break;
                case SQLCommand.Voucher.FetchVoucherNumberFormat:
                    {
                        Query = "SELECT" +
                            // " CONCAT(PREFIX_CHAR,CONCAT('#',STARTING_NUMBER ),CONCAT('#',SUFFIX_CHAR)) AS 'VOUCHER_NUMBER' " +
                                " PREFIX_CHAR,STARTING_NUMBER,SUFFIX_CHAR,NUMBERICAL_WITH,PREFIX_WITH_ZERO " +
                                " FROM MASTER_VOUCHER AS MV " +
                                " INNER JOIN PROJECT_VOUCHER AS MPV ON " +
                                " MV.VOUCHER_ID=MPV.VOUCHER_ID " +
                                " WHERE MPV.PROJECT_ID=?PROJECT_ID " +
                                " AND MV.VOUCHER_TYPE=?VOUCHER_TYPE";
                        break;
                    }
                case SQLCommand.Voucher.UpdateLastVoucherNumber:
                    {
                        Query = " UPDATE VOUCHER_NUMBER_FORMAT " +
                                    " SET " +
                                    " NUMBER_FORMAT_ID=?NUMBER_FORMAT_ID, " +
                                    " LAST_VOUCHER_NUMBER=?LAST_VOUCHER_NUMBER, " +
                                    " RUNNING_NUMBER=?RUNNING_NUMBER, " +
                                    " NUMBER_FORMAT=?NUMBER_FORMAT " +
                                    " WHERE NUMBER_ID=?NUMBER_ID ";

                        break;
                    }
                case SQLCommand.Voucher.InsertVoucherNumber:
                    {
                        Query = " INSERT INTO VOUCHER_NUMBER_FORMAT (NUMBER_FORMAT_ID, " +
                                              " LAST_VOUCHER_NUMBER, " +
                                              " RUNNING_NUMBER, " +
                                              " NUMBER_FORMAT ) VALUES" +
                                              " (?NUMBER_FORMAT_ID,  " +
                                              " ?LAST_VOUCHER_NUMBER , " +
                                              " ?RUNNING_NUMBER,  " +
                                              " ?NUMBER_FORMAT ) ";

                        break;
                    }
                case SQLCommand.Voucher.FetchVoucherNumberFormatExist:
                    {
                        Query = "SELECT " +
                                  " NUMBER_ID, " +
                                  " NUMBER_FORMAT_ID, " +
                                  " LAST_VOUCHER_NUMBER, " +
                                  " RUNNING_NUMBER, " +
                                  " NUMBER_FORMAT " +
                                  " FROM VOUCHER_NUMBER_FORMAT WHERE NUMBER_FORMAT_ID=?NUMBER_FORMAT_ID";
                        break;
                    }
                #endregion
            }
            return Query;
        }

        #endregion
    }
}