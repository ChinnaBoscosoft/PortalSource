using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Configuration;

namespace Bosco.DAO.MySQL
{
    public class MySQLDataHandler : IDatabase
    {
        #region Member data declaration

        private MySqlConnection mySqlConnection = new MySqlConnection();
        private MySqlCommand mySqlCommand;
        private MySqlTransaction mySqlTransaction = null;

        private string rowUniqueParmName = "";
        private string transactionSource = "";
        private bool getRowUniqueId = false;
        private bool hasTransaction = false;
        private const string PARAMETER_DELIMITER = "?";

        private ExecutionMode executionMode = ExecutionMode.None;

        #endregion

        #region MySQL Data Types

        private MySqlDbType GetSQLFieldType(DataType fieldType)
        {
            switch (fieldType)
            {
                case DataType.Boolean:
                    { return MySqlDbType.Bit; }
                case DataType.Byte:
                    { return MySqlDbType.Byte; }
                case DataType.Char:
                    { return MySqlDbType.VarChar; }
                case DataType.DateTime:
                    { return MySqlDbType.DateTime; }
                case DataType.TimeSpan:
                    { return MySqlDbType.Timestamp; }
                case DataType.Double:
                case DataType.Decimal:
                case DataType.Single:
                    { return MySqlDbType.Decimal; }
                case DataType.Int16:
                case DataType.UInt16:
                    { return MySqlDbType.Int16; }
                case DataType.Int:
                case DataType.Int32:
                case DataType.UInt32:
                    { return MySqlDbType.Int32; }
                case DataType.Int64:
                case DataType.UInt64:
                    { return MySqlDbType.Int64; }
                case DataType.ByteArray:
                    { return MySqlDbType.Blob; }
                default:
                    { return MySqlDbType.VarChar; }
            }
        }

        #endregion

        #region IDatabase Members

        public string PreviousTransactionSource
        {
            get { return transactionSource; }

        }

        //08/01/2019, for budget sync
        public ExecutionMode TransExecutionMode
        {
            set
            {
                executionMode = value;
            }
        }

        private void Reset()
        {
            hasTransaction = false;
            executionMode = ExecutionMode.None;
            transactionSource = "";
        }

        #region Handling Connection

        public ResultArgs HasConnectionEstablished()
        {
            ResultArgs result = new ResultArgs();

            //Establish connection
            try
            {
                result.Success = EstablishConnection();
            }
            catch (Exception e)
            {
                result.Exception = e;
                new ErrorLog().WriteError("MySQLDataHandler", "HasConnectionEstablished", e.Message, new CommonMethod().GetExceptionline(e));
            }
            finally
            {
                CloseConnection(TransactionType.None);
            }

            return result;
        }

        private bool EstablishConnection()
        {
            if (mySqlConnection == null || mySqlConnection.State == ConnectionState.Closed)
            {
                if (mySqlConnection == null) mySqlConnection = new MySqlConnection();

                if (String.IsNullOrEmpty(mySqlConnection.ConnectionString))
                {
                    mySqlConnection.ConnectionString = ConfigurationHandler.Instance.ConnectionString;
                }

                mySqlConnection.Open();

                if (hasTransaction)
                {
                    try
                    {
                        mySqlTransaction = mySqlConnection.BeginTransaction();
                    }
                    catch (Exception e)
                    {
                        hasTransaction = false;
                        new ErrorLog().WriteError("MySQLDataHandler", "EstablishConnection", e.Message, new CommonMethod().GetExceptionline(e));
                    }
                }
            }

            return true;
        }

        private void CloseConnection(TransactionType transType)
        {
            if (mySqlConnection != null && mySqlConnection.State == ConnectionState.Open)
            {
                if (hasTransaction)
                {
                    if (transType == TransactionType.Commit)
                    {
                        mySqlTransaction.Commit();
                    }
                    else if (transType == TransactionType.Rollback)
                    {
                        mySqlTransaction.Rollback();
                    }
                }

                if (!hasTransaction || (hasTransaction && (transType == TransactionType.Commit || transType == TransactionType.Rollback)))
                {
                    mySqlConnection.Close();
                    mySqlConnection.Dispose();

                    if (mySqlCommand != null)
                    {
                        mySqlCommand.Dispose();
                        mySqlCommand = null;
                    }

                    mySqlConnection = null;
                    hasTransaction = false;
                }
            }
        }

        #endregion

        #region Handle Transaction

        /// <summary>
        /// Explicitly Invoke Transaction
        /// </summary>
        public void BeginTransaction()
        {
            // Verify Previous transaction is alive (Not properly close the transaction)
            if (hasTransaction)
            {
                //Previous transaction not ends properly
                //Write log (transactionSource)
                if (executionMode != ExecutionMode.None) { RollBackTransaction(); }
                new ResultArgs().Exception = new Exception("Previous BEGIN TRANSACTION was not ended properly (" + transactionSource + ")");
            }
            else
            {
                Reset();
                hasTransaction = true;

                StackTrace stackTrace = new StackTrace();

                if (stackTrace.FrameCount > 2)
                {
                    MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();
                    transactionSource += methodBase.ReflectedType.Name + "." + methodBase.Name + "()::" + DateTime.Now.ToString();
                    //transactionSource = AppDomain.CurrentDomain.ActivationContext.Form.GetType().Name + "::" + DateTime.Now.ToString();
                }
            }
        }

        /// <summary>
        /// End Transaction
        /// </summary>
        public void EndTransaction()
        {
            if (executionMode == ExecutionMode.Success)
            {
                CloseConnection(TransactionType.Commit);
            }
            else if (executionMode == ExecutionMode.Fail)
            {
                CloseConnection(TransactionType.Rollback);
            }
            else
            {
                CloseConnection(TransactionType.None);
            }

            Reset();
        }

        /// <summary>
        /// Commit transaction explicitly
        /// </summary>
        public void CommitTransaction()
        {
            CloseConnection(TransactionType.Commit);
            Reset();
        }

        /// <summary>
        /// Rollback transaction explicitly
        /// </summary>
        public void RollBackTransaction()
        {
            CloseConnection(TransactionType.Rollback);
            Reset();
        }

        #endregion

        #region Execute Methods

        public ResultArgs Execute(DataManager dataManager, SQLType sqlType, bool DontShowErrorMessage = false)
        {
            return Execute(dataManager, "", sqlType);
        }

        public ResultArgs Execute(DataManager dataManager, string SQLStatement, SQLType sqlType, bool DontShowErrorMessage = false)
        {
            ResultArgs result = new ResultArgs();
            SetSqlCommand(dataManager, SQLStatement, sqlType, result);
            bool hasError = false;

            try
            {
                if (result.Success)
                {
                    result.RowsAffected = mySqlCommand.ExecuteNonQuery();
                    result.RowsAffected = (result.RowsAffected < 0) ? 0 : result.RowsAffected;
                    SetRowUniqueIdentifierValue(result, mySqlCommand);
                }
            }
            catch (Exception e)
            {
                //Update Exception
                ParseException(result, e);
                result.Success = false;
                hasError = true;
                new ErrorLog().WriteError("MySQLDataHandler", "Execute", e.Message, mySqlCommand.CommandText, new CommonMethod().GetExceptionline(e));

            }
            finally
            {
                if (hasTransaction && hasError)
                {
                    //CloseConnection(TransactionType.Rollback);
                    executionMode = ExecutionMode.Fail;
                }
                else if (hasTransaction && !hasError && executionMode != ExecutionMode.Fail)
                {
                    executionMode = ExecutionMode.Success;
                }
                else
                {
                    CloseConnection(TransactionType.None);
                }
            }

            return result;
        }

        #endregion

        #region Fetch Methods

        public ResultArgs Fetch(DataManager dataManager, DataSource dataSourceType, string SQLStatement, SQLType sqlType, ref object dataSource)
        {
            ResultArgs result = new ResultArgs();
            MySqlDataAdapter mySqlAdapter;
            DataCommandArguments dataCommandArgs = dataManager.DataCommandArgs;

            SetSqlCommand(dataManager, SQLStatement, sqlType, result);
            dataSource = null;

            try
            {
                if (result.Success)
                {
                    switch (dataSourceType)
                    {
                        case DataSource.DataSet:
                            {
                                mySqlAdapter = new MySqlDataAdapter(mySqlCommand);
                                if (dataSource == null) dataSource = new DataSet();
                                result.RowsAffected = mySqlAdapter.Fill(dataSource as DataSet, dataCommandArgs.Name);
                                break;
                            }
                        case DataSource.DataView:
                            {
                                mySqlAdapter = new MySqlDataAdapter(mySqlCommand);
                                if (dataSource == null) dataSource = new DataTable(dataCommandArgs.Name);
                                result.RowsAffected = mySqlAdapter.Fill(dataSource as DataTable);
                                dataSource = ((DataTable)dataSource).DefaultView;
                                break;
                            }
                        case DataSource.DataReader:
                            {
                                dataSource = mySqlCommand.ExecuteReader();
                                result.RowsAffected = ((MySqlDataReader)dataSource).RecordsAffected;
                                break;
                            }
                        case DataSource.Scalar:
                            {
                                dataSource = mySqlCommand.ExecuteScalar();
                                if (dataSource != null)
                                {
                                    result.RowsAffected = 1;
                                }
                                else
                                {
                                    result.RowsAffected = 0;
                                    result.Success = true;
                                }

                                break;
                            }
                        default:
                            {
                                mySqlAdapter = new MySqlDataAdapter(mySqlCommand);
                                if (dataSource == null) dataSource = new DataTable(dataCommandArgs.Name);
                                result.RowsAffected = mySqlAdapter.Fill(dataSource as DataTable);
                                break;
                            }
                    }

                    SetRowUniqueIdentifierValue(result, mySqlCommand);
                }
            }
            catch (Exception e)
            {
                //Update Exception
                result.Exception = e;
                new ErrorLog().WriteError("MySQLDataHandler", "Fetch", e.Message, mySqlCommand.CommandText, new CommonMethod().GetExceptionline(e));
            }
            finally
            {
                if (hasTransaction && !result.Success)
                {
                    //CloseConnection(TransactionType.Rollback);
                    executionMode = ExecutionMode.Fail;
                }
                else if (hasTransaction && result.Success && executionMode != ExecutionMode.Fail)
                {
                    executionMode = ExecutionMode.Success;
                }
                else
                {
                    CloseConnection(TransactionType.None);
                }
            }

            //result.ShowMessage("Updated");
            if (result.Success)
            {
                result.DataSource.Data = dataSource;
            }

            return result;
        }

        #endregion

        #endregion

        #region Sql Command

        private void SetSqlCommand(DataManager dataManager, string SQLStatement, SQLType sqlType, ResultArgs result)
        {
            IDatabaseQuery sqlQueryHandler = new SQLQueryHandler();
            DataCommandArguments dataCommandArgs = dataManager.DataCommandArgs;
            DataParameters dataParameters = dataManager.Parameters;

            string query = "";
            string param = "";
            result.Success = true;

            try
            {
                //Establish connection
                EstablishConnection();

                //Get SQL script
                query = SQLStatement;
                if (string.IsNullOrEmpty(query)) { query = sqlQueryHandler.GetQuery(dataCommandArgs, ref sqlType); }
                query = ((SQLQueryHandler)sqlQueryHandler).ParseQuery(query, dataParameters, PARAMETER_DELIMITER);

                //Setup Command Object to execute the SQL
                mySqlCommand = new MySqlCommand(query, mySqlConnection);
                mySqlCommand.CommandType = (sqlType == SQLType.SQLStoredProcedure) ? CommandType.StoredProcedure : CommandType.Text;
                mySqlCommand.CommandTimeout = 0; // For Temporary Purpose we fixed timed out is 0, We need to fine tune the report Query

                //Set parameters
                if (dataParameters != null)
                {
                    foreach (IDataArguments itemValue in dataParameters)
                    {
                        param = GetParameter(query, itemValue.FieldName, sqlType);

                        if (itemValue.IsRowUniqueId)
                        {
                            rowUniqueParmName = param;
                            getRowUniqueId = true;
                        }

                        if (param != string.Empty)
                        {
                            //Convert Date, DateTime, Time, TimeStamp values to MySQL Format 
                            //to store MySQL standard datetime format in MySQL Database

                            if (itemValue.FieldValue != null)
                            {
                                switch (itemValue.FieldType)
                                {
                                    case DataType.Date:
                                        {
                                            itemValue.FieldValue = this.GetMySQLDateTime(itemValue.FieldValue.ToString(), DateDataType.Date);
                                            if (itemValue.FieldValue.ToString() == "") { itemValue.FieldValue = DBNull.Value; }
                                            break;
                                        }
                                    case DataType.DateTime:
                                        {
                                            itemValue.FieldValue = this.GetMySQLDateTime(itemValue.FieldValue.ToString(), DateDataType.DateTime);
                                            if (itemValue.FieldValue.ToString() == "") { itemValue.FieldValue = DBNull.Value; }
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                itemValue.FieldValue = DBNull.Value;
                            }

                            if (dataCommandArgs.IsDirectReplaceParameter)
                            {
                                query = ReplaceParameterValue(query, param, itemValue.FieldValue, sqlType, itemValue.FieldType);
                            }
                            else
                            {
                                MySqlDbType sqlDBType = GetSQLFieldType(itemValue.FieldType);
                                MySqlParameter mySqlParameter = mySqlCommand.Parameters.Add(param, sqlDBType);
                                mySqlParameter.Value = itemValue.FieldValue;

                                //Set Parameter direction (input/output parameter)
                                if (sqlType == SQLType.SQLStoredProcedure &&
                                    itemValue.ParameterType == SQLParameterType.Output)
                                {
                                    mySqlParameter.Direction = ParameterDirection.Output;
                                }
                            }
                        }
                    }
                }

                if (dataCommandArgs.IsDirectReplaceParameter)
                {
                    mySqlCommand.CommandText = query;
                }

                mySqlCommand.Transaction = mySqlTransaction;
                mySqlCommand.Prepare();
            }
            catch (Exception e)
            {
                //Update Exceptions
                result.Exception = e;
                new ErrorLog().WriteError("MySQLDataHandler", "SetSqlCommand", e.Message, mySqlCommand.CommandText, new CommonMethod().GetExceptionline(e));
            }
        }

        private string GetMySQLDateTime(string dateTime, DateDataType dateType)
        {
            string date = dateTime;
            string formatDateUpdate = "yyyy-MM-dd";
            string formatDateAndTimeUpdate = "yyyy-MM-dd HH:mm:ss";


            if (String.IsNullOrEmpty(date)) { date = ""; }
            date = date.Trim();

            if (date != "")
            {
                DateTime dt;

                try
                {
                    if (DateTime.TryParse(date, out dt))
                    {
                        dt = DateTime.Parse(date);

                        switch (dateType)
                        {
                            case DateDataType.Date:
                                {
                                    date = dt.ToString(formatDateUpdate);
                                    break;
                                }
                            case DateDataType.DateTime:
                                {
                                    date = dt.ToString(formatDateAndTimeUpdate);
                                    break;
                                }
                        }
                    }
                }
                catch (Exception e) { new ErrorLog().WriteError("MySQLDataHandler", "GetMySqlDateTime", e.Message, new CommonMethod().GetExceptionline(e)); }
            }

            return date;
        }

        #endregion

        #region Row Unique Identifier

        private void SetRowUniqueIdentifierValue(ResultArgs result, MySqlCommand mySqlCommand)
        {
            if (mySqlCommand.CommandType == CommandType.StoredProcedure)
            {
                MySqlParameterCollection mySqlParameterCollection = mySqlCommand.Parameters;
                string paramName = "";

                foreach (MySqlParameter mySqlParameter in mySqlParameterCollection)
                {
                    if (mySqlParameter.ParameterName == rowUniqueParmName)
                    {
                        paramName = RemoveParameterDelimiter(mySqlParameter.ParameterName);
                        result.RowUniqueIdCollection[paramName] = mySqlParameter.Value;
                        break;
                    }
                }
            }
            else
            {
                if (getRowUniqueId)
                {
                    string sQuery = "SELECT LAST_INSERT_ID()";
                    mySqlCommand.CommandText = sQuery;
                    mySqlCommand.CommandType = CommandType.Text;
                    result.RowUniqueId = mySqlCommand.ExecuteScalar().ToString();
                }
            }
        }
        #endregion

        #region Parse Exception

        private string GetParameter(string query, string fieldName, SQLType sqlType)
        {
            string param = PARAMETER_DELIMITER + fieldName;

            if (sqlType == SQLType.SQLStatic)
            {
                param = query.Contains(param) ? param : "";
            }
            return param;
        }

        private string RemoveParameterDelimiter(string parameterName)
        {
            string paramName = parameterName.Replace(PARAMETER_DELIMITER, "");
            return paramName;
        }

        private string ReplaceParameterValue(string query, string param, object paramValue, SQLType sqlType, DataType fieldType)
        {
            string sql = query;

            if (sqlType == SQLType.SQLStatic)
            {
                if (fieldType == DataType.Varchar || fieldType == DataType.String
                    || fieldType == DataType.Char || fieldType == DataType.Date
                    || fieldType == DataType.DateTime)
                {
                    if (paramValue == DBNull.Value || paramValue == null)
                    {
                        sql = query.Replace(param, "null");
                    }
                    else
                    {
                        sql = query.Replace(param, "'" + paramValue + "'");
                    }
                }
                else
                {
                    sql = query.Replace(param, paramValue.ToString());
                }
            }

            return sql;
        }

        private void ParseException(ResultArgs result, Exception e)
        {
            string errorMessage = e.Message;
            string message = errorMessage;

            int posStart = 0;
            int posEnd = 0;

            //Validation Message for Adding/Updating duplicate value
            posStart = errorMessage.ToLower().IndexOf("duplicate entry");

            if (posStart >= 0)
            {
                posStart = errorMessage.IndexOf("'");
                posEnd = errorMessage.LastIndexOf("'");

                if (posEnd >= posStart)
                {
                    message = "The Record is Available";//errorMessage.Substring(posStart, (posEnd - posStart + 1)) + " is available";
                    goto ENDLINE;
                }
            }

            //Validation Message for Adding/Updating duplicate value
            posStart = errorMessage.ToLower().IndexOf("cannot delete");

            if (posStart >= 0)
            {
                message = "Cannot Delete";
            }

            posStart = errorMessage.ToLower().IndexOf("Deadlock found");

            if (posStart >= 0)
            {
                result.IsDeadLock = true;
                message = "Other user is trying to save,try to save again.";
            }

        ENDLINE:
            ((ExceptionHandler)result.Exception).Add(e, message);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //GC.Collect();
        }

        #endregion
    }
}
