/*  Class Name      : ExecutiveMemberSQL
 *  Purpose         : To have Manipulation query for Executive Member
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
    public class ExecutiveMemberSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.ExecutiveMembers).FullName)
            {
                query = GetExecuteMember();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQLScript
        /// <summary>
        /// Perform the action of Executive Member details
        /// </summary>
        /// <returns></returns>
        private string GetExecuteMember()
        {
            string query = "";
            SQLCommand.ExecutiveMembers sqlCommandId = (SQLCommand.ExecutiveMembers)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ExecutiveMembers.Add:
                    {
                        query = "INSERT INTO MASTER_EXECUTIVE_COMMITTEE ( " +
                             "EXECUTIVE, " +
                             "NAME, " +
                             "DATE_OF_BIRTH, " +
                             "RELIGION, " +
                             "ROLE, " +
                             "NATIONALITY, " +
                             "OCCUPATION, " +
                             "ASSOCIATION, " +
                             "OFFICE_BEARER, " +
                             "PLACE, " +
                             "STATE_ID, " +
                             "COUNTRY_ID, " +
                             "ADDRESS, " +
                             "PIN_CODE, " +
                             "PAN_SSN, " +
                             "PHONE, " +
                             "FAX, " +
                             "EMAIL, " +
                             "URL, " +
                             "DATE_OF_APPOINTMENT, " +
                             "DATE_OF_EXIT, " +
                             "NOTES) VALUES( " +
                             "?EXECUTIVE, " +
                             "?NAME, " +
                             "?DATE_OF_BIRTH, " +
                             "?RELIGION, " +
                             "?ROLE, " +
                             "?NATIONALITY, " +
                             "?OCCUPATION, " +
                             "?ASSOCIATION, " +
                             "?OFFICE_BEARER, " +
                             "?PLACE, " +
                             "?STATE_ID, " +
                             "?COUNTRY_ID, " +
                             "?ADDRESS, " +
                             "?PIN_CODE, " +
                             "?PAN_SSN, " +
                             "?PHONE, " +
                             "?FAX, " +
                             "?EMAIL, " +
                             "?URL, " +
                             "?DATE_OF_APPOINTMENT, " +
                             "?DATE_OF_EXIT, " +
                             "?NOTES) ";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.Update:
                    {
                        query = "UPDATE MASTER_EXECUTIVE_COMMITTEE SET " +
                                "EXECUTIVE =?EXECUTIVE, " +
                                "NAME =?NAME, " +
                                "DATE_OF_BIRTH =?DATE_OF_BIRTH, " +
                                "RELIGION =?RELIGION, " +
                                "ROLE =?ROLE, " +
                                "NATIONALITY =?NATIONALITY, " +
                                "OCCUPATION =?OCCUPATION, " +
                                "ASSOCIATION =?ASSOCIATION, " +
                                "OFFICE_BEARER =?OFFICE_BEARER, " +
                                "PLACE =?PLACE, " +
                                "STATE_ID =?STATE_ID, " +
                                "COUNTRY_ID =?COUNTRY_ID, " +
                                "ADDRESS =?ADDRESS, " +
                                "PIN_CODE =?PIN_CODE, " +
                                "PAN_SSN =?PAN_SSN, " +
                                "PHONE =?PHONE, " +
                                "FAX =?FAX, " +
                                "EMAIL =?EMAIL, " +
                                "URL =?URL, " +
                                "DATE_OF_APPOINTMENT =?DATE_OF_APPOINTMENT, " +
                                "DATE_OF_EXIT =?DATE_OF_EXIT, " +
                                "NOTES =?NOTES " +
                                "WHERE EXECUTIVE_ID=?EXECUTIVE_ID ";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.Delete:
                    {
                        query = "DELETE FROM MASTER_EXECUTIVE_COMMITTEE WHERE EXECUTIVE_ID=?EXECUTIVE_ID ";
                        break;
                    }

                case SQLCommand.ExecutiveMembers.Fetch:
                    {
                        query = "SELECT " +
                            "MEC.EXECUTIVE_ID, " +
                            "MEC.EXECUTIVE, " +
                            "MEC.NAME, " +
                            "DATE_FORMAT(MEC.DATE_OF_BIRTH,'%Y-%m-%d') AS DATE_OF_BIRTH, " +
                            "MEC.RELIGION, " +
                            "MEC.ROLE, " +
                            "MEC.NATIONALITY, " +
                            "MEC.OCCUPATION, " +
                            "MEC.ASSOCIATION, " +
                            "MEC.OFFICE_BEARER, " +
                            "MEC.PLACE, " +
                            "MEC.STATE_ID, " +
                            "MEC.COUNTRY_ID, " +
                            "MEC.ADDRESS, " +
                            "MEC.PIN_CODE, " +
                            "MEC.PAN_SSN, " +
                            "MEC.PHONE, " +
                            "MEC.FAX, " +
                            "MEC.EMAIL, " +
                            "MEC.URL, " +
                            "DATE_FORMAT(MEC.DATE_OF_APPOINTMENT,'%Y-%m-%d') AS DATE_OF_APPOINTMENT , " +
                            "DATE_FORMAT(MEC.DATE_OF_EXIT,'%Y-%m-%d') AS DATE_OF_EXIT, " +
                            "MEC.NOTES " +
                            "FROM " +
                            "MASTER_EXECUTIVE_COMMITTEE MEC " +
                            " WHERE EXECUTIVE_ID=?EXECUTIVE_ID";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.FetchAll:
                    {
                        query = "SELECT " +
                           "EXECUTIVE_ID, " +
                           "EXECUTIVE as Name, " +
                           "NAME as 'Name of the Father/Husband', " +
                           "Nationality , " +
                           "Occupation, " +
                           "MS.State, " +
                           "Country " +
                           "FROM " +
                           "MASTER_EXECUTIVE_COMMITTEE E " +
                           "LEFT JOIN COUNTRY C ON E.COUNTRY_ID=C.COUNTRY_ID " +
                           "LEFT JOIN STATE MS ON MS.STATE_ID=E.STATE_ID " +
                           " ORDER BY EXECUTIVE ASC";

                        break;
                    }
                case SQLCommand.ExecutiveMembers.MapGoverningMember:
                    {
                        query = "INSERT INTO EXECUTIVE_LEGAL_ENTITY(CUSTOMERID,EXECUTIVE_ID) VALUES(?CUSTOMERID,?EXECUTIVE_ID)";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.FetchGoverningMemberByLegalEntity:
                    {
                        query = "SELECT EC.EXECUTIVE_ID,EC.EXECUTIVE,EC.ROLE,\n" +
                              "DATE_FORMAT(EC.DATE_OF_APPOINTMENT,'%Y-%m-%d') AS 'Date of Joining' , " +
                             "DATE_FORMAT(EC.DATE_OF_EXIT,'%Y-%m-%d') AS 'Date of Exit', " +
                               "IF(ELE.CUSTOMERID=?CUSTOMERID,1,0) AS 'SELECT' FROM \n" +
                               "MASTER_EXECUTIVE_COMMITTEE EC  \n" +
                               "LEFT JOIN EXECUTIVE_LEGAL_ENTITY ELE \n" +
                               "ON EC.EXECUTIVE_ID=ELE.EXECUTIVE_ID AND ELE.CUSTOMERID=?CUSTOMERID GROUP BY EC.EXECUTIVE_ID";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.UnmapLegalEntitytoGoverningMember:
                    {
                        query = "DELETE FROM EXECUTIVE_LEGAL_ENTITY WHERE CUSTOMERID=?CUSTOMERID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
