using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AddressBookSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AddressBook).FullName)
            {
                query = GetAddressBookSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetAddressBookSQL()
        {
            {
                string query = "";
                SQLCommand.AddressBook sqlCommandId = (SQLCommand.AddressBook)(this.dataCommandArgs.SQLCommandId);

                switch (sqlCommandId)
                {
                    case SQLCommand.AddressBook.Add:
                        {
                            query = "INSERT INTO MASTER_DONAUD ( " +
                                   "NAME, " +
                                   "TYPE, " +
                                   "PLACE," +
                                   "COUNTRY_ID," +
                                   "PINCODE," +
                                   "PHONE," +
                                   "FAX," +
                                   "EMAIL," +
                                  "IDENTITYKEY," +
                                   "URL," +
                                   "FCDONOR," +
                                   "STATE," +
                                   "ADDRESS ) VALUES( " +
                                   "?NAME, " +
                                   "?TYPE, " +
                                   "?PLACE," +
                                   "?COUNTRY_ID," +
                                   "?PINCODE," +
                                   "?PHONE," +
                                   "?FAX," +
                                   "?EMAIL," +
                                  "?IDENTITYKEY," +
                                   "?URL," +
                                   "?FCDONOR," +
                                   "?STATE," +
                                   "?ADDRESS)";
                            break;
                        }

                    case SQLCommand.AddressBook.Update:
                        {
                            query = "UPDATE MASTER_DONAUD SET " +
                                        "NAME = ?NAME, " +
                                        "TYPE =?TYPE, " +
                                        "PLACE=?PLACE, " +
                                        "COUNTRY_ID=?COUNTRY_ID," +
                                        "PINCODE=?PINCODE," +
                                        "PHONE=?PHONE, " +
                                        "FAX=?FAX, " +
                                        "EMAIL=?EMAIL, " +
                                        "IDENTITYKEY=?IDENTITYKEY," +
                                        "URL=?URL, " +
                                        "FCDONOR=?FCDONOR, " +
                                        "STATE=?STATE, " +
                                        "ADDRESS=?ADDRESS " +
                                        "WHERE DONAUD_ID=?DONAUD_ID ";
                            break;
                        }

                    case SQLCommand.AddressBook.Delete:
                        {
                            query = "DELETE FROM MASTER_DONAUD WHERE DONAUD_ID=?DONAUD_ID";
                            break;
                        }

                    case SQLCommand.AddressBook.Fetch:
                        {
                            query = "SELECT " +
                                    "NAME, " +
                                    "TYPE, " +
                                    "PLACE," +
                                    "COUNTRY_ID," +
                                    "PINCODE," +
                                    "PHONE," +
                                    "FAX," +
                                    "EMAIL," +
                                    "IDENTITYKEY,"+
                                    "URL," +
                                    "FCDONOR," +
                                    "STATE," +
                                    "ADDRESS " +
                                "FROM " +
                                    "MASTER_DONAUD " +
                                    " WHERE DONAUD_ID=?DONAUD_ID ";
                            break;
                        }

                    case SQLCommand.AddressBook.FetchDonor:
                        {
                            query = "SELECT " +
                                    "DONAUD_ID, " +
                                    "NAME, " +
                                    "PLACE," +
                                    "COUNTRY," +
                                    "PHONE," +
                                    "STATE," +
                                    "ADDRESS," +
                                    "PINCODE," +
                                    "FAX," +
                                    "EMAIL," +
                                    "IDENTITYKEY," +
                                    "URL " +
                                "FROM " +
                                    "MASTER_DONAUD A " +
                                    " INNER JOIN MASTER_COUNTRY C ON A.COUNTRY_ID=C.COUNTRY_ID " +
                                    " WHERE IDENTITYKEY=0" +
                                    " ORDER BY NAME ASC";
                            break;
                        }

                    case SQLCommand.AddressBook.FetchAuditor:
                        {
                            query = "SELECT " +
                                    "DONAUD_ID, " +
                                    "NAME, " +
                                    "PLACE," +
                                    "COUNTRY," +
                                    "PHONE," +
                                    "STATE," +
                                    "ADDRESS," +
                                    "PINCODE," +
                                    "FAX," +
                                    "EMAIL," +
                                    "IDENTITYKEY," +
                                    "URL " +
                                "FROM " +
                                    "MASTER_DONAUD A " +
                                    " INNER JOIN MASTER_COUNTRY C ON A.COUNTRY_ID=C.COUNTRY_ID " +
                                    " WHERE IDENTITYKEY=1 " +
                                    " ORDER BY NAME ASC";
                            break;
                        }

                    case SQLCommand.AddressBook.FetchOthers:
                        {
                            query = "SELECT " +
                                    "DONAUD_ID, " +
                                    "NAME, " +
                                    "PLACE," +
                                    "COUNTRY," +
                                    "PHONE," +
                                    "STATE," +
                                    "ADDRESS," +
                                    "PINCODE," +
                                    "FAX," +
                                    "EMAIL," +
                                    "IDENTITYKEY," +
                                    "URL " +
                                "FROM " +
                                    "MASTER_DONAUD A " +
                                    " INNER JOIN MASTER_COUNTRY C ON A.COUNTRY_ID=C.COUNTRY_ID " +
                                    " WHERE Type=2 " +
                                    " ORDER BY NAME ASC";
                            break;
                        }

                    case SQLCommand.AddressBook.FetchAll:
                        {
                            query = "SELECT " +
                                    "DONAUD_ID, " +
                                    "NAME, " +
                                    "PLACE, " +
                                    "COUNTRY, " +
                                    "PINCODE, " +
                                    "PHONE, " +
                                    "FAX, " +
                                    "EMAIL, " +
                                   "IDENTITYKEY," +
                                    "URL, " +
                                    "STATE, " +
                                    "ADDRESS " +
                                "FROM " +
                                   "MASTER_DONAUD A " +
                                    " INNER JOIN MASTER_COUNTRY C ON A.COUNTRY_ID=C.COUNTRY_ID " +
                                   " ORDER BY NAME ASC";
                            break;
                        }
                }
                return query;
            }
        }
        #endregion
    }
}
