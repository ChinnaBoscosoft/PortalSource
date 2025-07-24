using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class InKindArticleSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.InKindArticle).FullName)
            {
                query = GetInKindArticleSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the inkind article details.
        /// </summary>
        /// <returns></returns>
        private string GetInKindArticleSQL()
        {
            string query = "";
            SQLCommand.InKindArticle sqlCommandId = (SQLCommand.InKindArticle)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.InKindArticle.Add:
                    {
                        query = "INSERT INTO MASTER_INKIND_ARTICLE ( " +
                               "ABBREVATION, " +
                               "ARTICLE, " +
                               "OP_QUANTITY, " +
                               "OP_VALUE, " +
                               "NOTES ) VALUES( " +
                               "?ABBREVATION, " +
                               "?ARTICLE, " +
                               "?OP_QUANTITY," +
                               "?OP_VALUE," +
                               "?NOTES)";
                        break;
                    }
                case SQLCommand.InKindArticle.Update:
                    {
                        query = "UPDATE MASTER_INKIND_ARTICLE SET " +
                                    "ABBREVATION = ?ABBREVATION, " +
                                    "ARTICLE =?ARTICLE, " +
                                    "OP_QUANTITY =?OP_QUANTITY, " +
                                    "OP_VALUE=?OP_VALUE, " +
                                    "NOTES=?NOTES " +
                                    " WHERE ARTICLE_ID=?ARTICLE_ID ";
                        break;
                    }
                case SQLCommand.InKindArticle.Delete:
                    {
                        query = "DELETE FROM MASTER_INKIND_ARTICLE WHERE ARTICLE_ID=?ARTICLE_ID";
                        break;
                    }
                case SQLCommand.InKindArticle.Fetch:
                    {
                        query = "SELECT " +
                                "ARTICLE_ID, " +
                                "ABBREVATION, " +
                                "ARTICLE, " +
                                "OP_QUANTITY, " +
                                "OP_VALUE, " +
                                "NOTES " +
                            "FROM " +
                                "MASTER_INKIND_ARTICLE " +
                                " WHERE ARTICLE_ID=?ARTICLE_ID ";
                        break;
                    }
                case SQLCommand.InKindArticle.FetchAll:
                    {
                        query = "SELECT " +
                               "ARTICLE_ID, " +
                               "ABBREVATION, " +
                               "ARTICLE, " +
                               "OP_QUANTITY, " +
                               "OP_VALUE, " +
                               "NOTES " +
                           "FROM " +
                               "MASTER_INKIND_ARTICLE ORDER BY ABBREVATION ASC ";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}

