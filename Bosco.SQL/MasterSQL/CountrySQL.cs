/*  Class Name      : CountrySQL
 *  Purpose         : To have Manipulation query for Country
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
namespace Bosco.SQL
{
    public class CountrySQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Country).FullName)
            {
                query = GetCountrySQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetCountrySQL()
        {
            string query = "";
            SQLCommand.Country sqlCommandId = (SQLCommand.Country)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.Country.Add:
                    {
                        query = "INSERT INTO MASTER_COUNTRY ( " +
                               "COUNTRY, " +
                               "COUNTRY_CODE, " +
                                "CURRENCY_CODE, " +
                               "CURRENCY_SYMBOL, " +
                               "CURRENCY_NAME ) VALUES( " +
                               "?COUNTRY, " +
                               "?COUNTRY_CODE, " +
                                "?CURRENCY_CODE, " +
                               "?CURRENCY_SYMBOL, " +
                               "?CURRENCY_NAME) ";

                        break;
                    }
                case SQLCommand.Country.Update:
                    {
                        query = "UPDATE MASTER_COUNTRY SET " +
                                    "COUNTRY =?COUNTRY, " +
                                    "COUNTRY_CODE =?COUNTRY_CODE, " +
                                     "CURRENCY_CODE =?CURRENCY_CODE, " +
                                    "CURRENCY_SYMBOL=?CURRENCY_SYMBOL, " +
                                    "CURRENCY_NAME=?CURRENCY_NAME " +
                                    "WHERE COUNTRY_ID=?COUNTRY_ID ";

                        break;
                    }

                case SQLCommand.Country.Delete:
                    {
                        query = "DELETE FROM MASTER_COUNTRY WHERE COUNTRY_ID=?COUNTRY_ID ";
                        break;
                    }

                case SQLCommand.Country.Fetch:
                    {
                        query = "SELECT " +
                               "COUNTRY_ID, " +
                               "COUNTRY, " +
                               "COUNTRY_CODE, " +
                                "CURRENCY_CODE, " +
                               "CURRENCY_SYMBOL, " +
                               "CURRENCY_NAME " +

                               "FROM " +
                               "MASTER_COUNTRY WHERE COUNTRY_ID=?COUNTRY_ID";
                        break;
                    }
                case SQLCommand.Country.FetchAll:
                    {

                        query = "SELECT COUNTRY_ID,\n" +
                                "       COUNTRY AS 'Country',\n" +
                                "       COUNTRY_CODE AS 'Country Code',\n" +
                                "       CURRENCY_CODE AS 'Currency Code',\n" +
                                "       CURRENCY_SYMBOL AS 'Symbol',\n" +
                                "       CURRENCY_NAME AS 'Currency Name',\n" +
                                "\n" +
                                "       CONCAT(' ( ',\n" +
                                "              CURRENCY_SYMBOL,\n" +
                                "              ':',\n" +
                                "              IFNULL(CURRENCY_CODE, ''),\n" +
                                "              ' ) ',\n" +
                                "              ' - ',\n" +
                                "              IFNULL(CURRENCY_NAME, '')) AS CURRENCY\n" +
                            // "       CONCAT(' ( ', CURRENCY_SYMBOL, ' ) ') AS CUR\n" + 
                                "\n" +
                                "  FROM MASTER_COUNTRY\n" +
                            // " WHERE CURRENCY_SYMBOL <> \"\"\n" + 
                                " ORDER BY COUNTRY ASC";

                        break;
                    }
                case SQLCommand.Country.FetchCountryList:
                    {
                        query = "SELECT " +
                               "COUNTRY_ID, " +
                               "COUNTRY " +
                           "FROM " +
                               "MASTER_COUNTRY ORDER BY COUNTRY ASC";
                        break;
                    }

                case SQLCommand.Country.FetchCountryCodeList:
                    {
                        query = "SELECT " +
                            "COUNTRY_ID, " +
                            "COUNTRY_CODE " +
                            "FROM " +
                            "MASTER_COUNTRY ORDER BY COUNTRY_CODE";
                        break;
                    }
                case SQLCommand.Country.FetchCurrencySymbolsList:
                    {
                        query = "SELECT " +
                             "COUNTRY_ID, " +
                             "CURRENCY_SYMBOL " +
                             "FROM " +
                             "MASTER_COUNTRY " +
                             "WHERE CURRENCY_SYMBOL <> ''" +
                             "ORDER BY CURRENCY_SYMBOL";
                        break;
                    }
                case SQLCommand.Country.FetchCurrencyCodeList:
                    {
                        query = "SELECT " +
                             "COUNTRY_ID, " +
                             "CURRENCY_CODE " +
                             "FROM " +
                             "MASTER_COUNTRY ORDER BY CURRENCY_CODE";
                        break;
                    }
                case SQLCommand.Country.FetchCurrencyNameList:
                    {
                        query = "SELECT " +
                               "COUNTRY_ID, " +
                               "CURRENCY_NAME " +
                           "FROM " +
                               "COUNTRY ORDER BY CURRENCY_NAME ASC";
                        break;
                    }
            }
            return query;

        }
        #endregion
    }
}

