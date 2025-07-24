using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class SoftwareSQL:IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Software).FullName)
            {
                query = GetSoftwareSQL();
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
        private string GetSoftwareSQL()
        {
            string query = "";
            SQLCommand.Software sqlCommandId = (SQLCommand.Software)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Software.Add:
                    {
                        query = "INSERT INTO SOFTWARE (DATE_OF_RELEASE,DESC_OF_RELEASE,TITLE,DATE_Of_UPLOAD,BUILDFILE_ACTUAL,BUILDFILE_PHYSICAL,CONTENT_TYPE, "+
                                 "RELEASENOTES_ACTUAL,RELEASENOTES_PHYSICAL,UPLOAD_TYPE,RELEASE_VERSION,FILESIZE) "+
                                 "VALUES(?DATE_OF_RELEASE,?DESC_OF_RELEASE,?TITLE,?DATE_Of_UPLOAD,?BUILDFILE_ACTUAL,?BUILDFILE_PHYSICAL,?CONTENT_TYPE, "+
                                 "?RELEASENOTES_ACTUAL,?RELEASENOTES_PHYSICAL,?UPLOAD_TYPE,?RELEASE_VERSION,?FILESIZE)";
                        break;
                    }
                case SQLCommand.Software.Update:
                    {
                        query = "UPDATE SOFTWARE SET DATE_OF_RELEASE =?DATE_OF_RELEASE,  DESC_OF_RELEASE =?DESC_OF_RELEASE, "+
                                "TITLE =?TITLE,RELEASE_VERSION=?RELEASE_VERSION, BUILDFILE_ACTUAL=?BUILDFILE_ACTUAL, " +
                                "BUILDFILE_PHYSICAL=?BUILDFILE_PHYSICAL,CONTENT_TYPE=?CONTENT_TYPE,DATE_Of_UPLOAD=?DATE_Of_UPLOAD, UPLOAD_TYPE=?UPLOAD_TYPE, "  +
                                "RELEASENOTES_ACTUAL=?RELEASENOTES_ACTUAL,RELEASENOTES_PHYSICAL=?RELEASENOTES_PHYSICAL,FILESIZE=?FILESIZE " +
                                "WHERE VERSION_ID=?VERSION_ID";
                        break;
                    }
                case SQLCommand.Software.Delete:
                    {
                        query = "DELETE FROM  SOFTWARE WHERE VERSION_ID=?VERSION_ID";
                        break;
                    }
                case SQLCommand.Software.FetchAll:
                    {
                        query = "SELECT VERSION_ID,date_format( DATE_OF_RELEASE,'%d/%m/%Y %h:%i %p') AS 'Released on', " +
                                "DESC_OF_RELEASE as 'Description', "+
                                "RELEASE_VERSION as 'Version',TITLE AS 'Title', "+
                                "CASE WHEN UPLOAD_TYPE=0 THEN 'SetUp' ELSE 'Prerequisite' END AS 'Type', "+
                                "BUILDFILE_ACTUAL AS 'Attachments', "+
                                "RELEASENOTES_ACTUAL AS 'Release Note' "+
                                "FROM SOFTWARE ORDER BY DATE_OF_RELEASE DESC";
                        break;
                    }
                case SQLCommand.Software.FetchByCurrentMonth:
                    {
                        query = "SELECT date_format( DATE_OF_RELEASE,'%d/%m/%Y %h:%i %p') AS 'Released on', " +
                               "DESC_OF_RELEASE as 'Description', "+
                               "RELEASE_VERSION as 'Version',TITLE AS 'Title', "+
                               "CASE WHEN UPLOAD_TYPE=0 THEN 'SetUp' ELSE 'Prerequisite' END AS 'Type', "+
                               "BUILDFILE_ACTUAL AS 'Attachments', "+
                               "RELEASENOTES_ACTUAL AS 'Release Note' " +
                               "FROM SOFTWARE WHERE month(DATE_Of_UPLOAD)=month(current_date()) ORDER BY DATE_OF_RELEASE DESC LIMIT 5";
                        break;
                    }
                case SQLCommand.Software.FetchByType:
                    {
                        query = "SELECT VERSION_ID,date_format( DATE_OF_RELEASE,'%d/%m/%Y %h:%i %p') AS 'Released on', " +
                                "DESC_OF_RELEASE as 'Description', " +
                                "RELEASE_VERSION as 'Version',TITLE AS 'Title', " +
                                "CASE WHEN UPLOAD_TYPE=0 THEN 'SetUp' ELSE 'Prerequisite' END AS 'Type', " +
                                "BUILDFILE_ACTUAL AS 'Attachments', " +
                                "BUILDFILE_PHYSICAL AS 'PhysicalFile', "+
                                "RELEASENOTES_PHYSICAL AS 'PhysicalRelease', "+
                                "RELEASENOTES_ACTUAL AS 'Release Note',FILESIZE,CONTENT_TYPE AS Ctype  " +
                                "FROM SOFTWARE WHERE UPLOAD_TYPE=?UPLOAD_TYPE ORDER BY DATE_OF_RELEASE DESC";
                        break;
                    }
                case SQLCommand.Software.Download:
                    {
                        query = "SELECT VERSION_ID,date_format( DATE_OF_RELEASE,'%d/%m/%Y') AS DATE_OF_RELEASE,date_format( DATE_OF_RELEASE,'%r') AS RELEASETIME, " +
                                "date_format( DATE_Of_UPLOAD,'%d/%m/%Y') AS DATE_Of_UPLOAD,DESC_OF_RELEASE,TITLE, " +
                                "BUILDFILE_PHYSICAL,CONTENT_TYPE,RELEASENOTES_ACTUAL,RELEASE_VERSION,FILESIZE, " +
                                "RELEASENOTES_PHYSICAL,BUILDFILE_ACTUAL,UPLOAD_TYPE FROM SOFTWARE WHERE VERSION_ID=?VERSION_ID";
                        break;
                    }
                case SQLCommand.Software.DeleteLog:
                    {
                        query = "DELETE FROM DATASYNC_TASK WHERE STATUS IN (?STATUS)";
                        break;
                    }
                    
            }
            return query;
        }
        #endregion HeadOffice SQL
    }
}
