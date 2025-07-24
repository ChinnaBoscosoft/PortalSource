/* Class        : IDatabase.cs
 * Purpose      : Interface to SQLDataHandler
 * Author       : CS
 * Created on   : 23-Jun-2010
 */

using System;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.DAO
{
    public interface IDatabase : IDisposable
    {
        ResultArgs HasConnectionEstablished();
        ResultArgs Execute(DataManager sqlData, SQLType sqlType,  bool showmessage = false);
        ResultArgs Execute(DataManager sqlData, string SQLStatement, SQLType sqlType, bool showmessage = false);
        ResultArgs Fetch(DataManager dataManager, DataSource dataSourceType, string SQLStatement, SQLType sqlType, ref object dataSource);

        void BeginTransaction();
        void EndTransaction();
        //void CommitTransaction();
        void RollBackTransaction();
        string PreviousTransactionSource { get; }
        ExecutionMode TransExecutionMode { set; }
    }
}
