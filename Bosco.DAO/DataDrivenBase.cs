/* Class        : DataDrivenBase.cs
 * Purpose      : Mediator between UI and Data Access (execute/retrive data source)
 * Author       : CS
 * Created on   : 24-Jun-2010
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Configuration;

namespace Bosco.DAO
{
    public abstract class DataDrivenBase : IDisposable
    {
        private IDatabase database = null;
        private object dataSourceOptional = null;
        private const string SESSION_DB = "SESSION_DB"; //for Web App

        #region Properties for Get/Set Database for Transaction Begin till end

        public IDatabase Database
        {
            get { return database; }
            set { database = value; }
        }

        private IDatabase SessionDB
        {
            get
            {
                //get database for web app
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[SESSION_DB] != null)
                {
                    database = HttpContext.Current.Session[SESSION_DB] as IDatabase;
                }

                return database;
            }
            set
            {
                //set database for web app
                HttpContext.Current.Session[SESSION_DB] = value;
                if (value == null)
                {
                    HttpContext.Current.Session.Remove(SESSION_DB);
                }
            }
        }

        public DataDrivenBase() 
        {
            database = SessionDB;

            if (database == null)
            {
                database = DatabaseFactory.Instance.CurrentDatabase();
            }
        }

        #endregion

        #region DataDriven Members

        /// <summary>
        /// Get the Database connection status
        /// </summary>
        public virtual ResultArgs HasDatabaseConnectionEstablished
        {
            get
            {
                ResultArgs resultArgs = database.HasConnectionEstablished();
                return resultArgs;
            }
        }

        #region Update Process

        //Update Data
        public virtual ResultArgs UpdateData(DataManager dataManager, bool DontShowErrorMessage = false)
        {
            ResultArgs resultArgs = UpdateData(dataManager, "", SQLType.SQLStatic, DontShowErrorMessage);
            return resultArgs;
        }

        public virtual ResultArgs UpdateData(DataManager dataManager, string SQLStatement, bool DontShowErrorMessage = false)
        {
            ResultArgs resultArgs = UpdateData(dataManager, SQLStatement, SQLType.SQLStatic, DontShowErrorMessage);
            return resultArgs;
        }

        public virtual ResultArgs UpdateData(DataManager dataManager, string SQLStatement, SQLType sqlType, bool DontShowErrorMessage = false)
        {
            ResultArgs resultArgs = null;
            if (SessionDB != null) { database = SessionDB; }
            SetConnection();

            try
            {
                resultArgs = database.Execute(dataManager, SQLStatement, sqlType, DontShowErrorMessage);
                HandleNullReference(resultArgs, null);
            }
            catch (Exception e)
            {
                EndTransaction();
                HandleNullReference(resultArgs, e);
            }

            return resultArgs;
        }

        #endregion

        #region Fetching Process

        //Fetching Data
        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, "", ref dataSourceOptional);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, ref object dataSource)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, "", ref dataSource);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, SQLStatement, ref dataSourceOptional);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement, SQLType sqlType)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, SQLStatement, sqlType, ref dataSourceOptional);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement, ref object dataSource)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, SQLStatement, SQLType.SQLStatic, ref dataSource);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement,
            SQLType sqlType, ref object dataSource)
        {
            ResultArgs resultArgs = null;

            if (SessionDB != null) { database = SessionDB; }
            SetConnection();

            try
            {
                resultArgs = database.Fetch(dataManager, dataSourceType, SQLStatement, sqlType, ref dataSource);
            }
            catch (Exception e)
            {
                EndTransaction();
                HandleNullReference(resultArgs, e);
            }

            return resultArgs;
        }

        #endregion

        #region Handle Transaction

        public virtual void BeginTransaction()
        {
            if (SessionDB != null)
            {
                database = SessionDB;
            }

            SessionDB = database;
            database.BeginTransaction();
        }

        //08/01/2019, for budget sync
        public virtual ExecutionMode TransExecutionMode
        {
            set { database.TransExecutionMode = value; }
        }

        /// <summary>
        /// force to Rollback  //08/01/2019, for budget sync
        /// </summary>
        public virtual void RollBackTransaction()
        {
            database = SessionDB;
            database.RollBackTransaction();
            SessionDB = null;
        }

        public virtual void EndTransaction()
        {
            database = SessionDB;
            database.EndTransaction();
            SessionDB = null;
        }

        #endregion

        #endregion

        private void HandleNullReference(ResultArgs resultArgs, Exception e)
        {
            if (resultArgs == null) 
            { 
                resultArgs = new ResultArgs();
                resultArgs.Exception = new Exception("Object is Null");
            }

            if (e != null) { resultArgs.Exception = e; }
        }

        private void SetConnection()
        {
            DataManager dm = (DataManager)this;
            ConfigurationHandler.Instance.ConnectionString = "";

            if (dm.DataCommandArgs.ActiveDatabaseType == DataBaseType.HeadOffice)
            {
                using (Utility.ConfigSetting.UserProperty userProperty = new Utility.ConfigSetting.UserProperty())
                {
                    ConfigurationHandler.Instance.ConnectionString =userProperty.HeadOfficeDBConnection;
                }
            }
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            if (dataSourceOptional != null)
            {
                dataSourceOptional = null;
            }

            GC.SuppressFinalize(true);
        }

        #endregion
    }
}
