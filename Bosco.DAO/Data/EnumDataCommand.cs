/*  Class Name      : EnumDataCommand.cs
 *  Purpose         : Enum Data type for Data Level Access
 *  Author          : CS
 *  Created on      : 15-Jul-2010
 */

namespace Bosco.DAO.Data
{


    public enum TransactionType
    {
        None,
        Commit,
        Rollback
    }

    public enum ExecutionMode
    {
        None,
        Success,
        Fail
    }

    public enum DataType
    {
        Boolean,
        Byte,
        Char,
        Date,
        DateTime, 
        Decimal, 
        Double,
        Int,
        Int16,
        Int32,
        Int64,
        SByte,
        Single, 
        String,
        TimeSpan, 
        UInt16,
        UInt32,
        UInt64,
        ByteArray,
        Varchar,
        None
    }

    public enum DataSource
    {
        DataSet,
        DataReader,
        DataTable,
        DataView,
        Scalar
    }

    public enum DataBaseType
    {
        Portal,
        HeadOffice
    }

    public enum ProjectCatogoryGroup
    {
        Primary = 1,
        CommunityActivities = 2,
        Formations = 3,
        ExtraordinaryActivities =4,
        OtherActivities=5
    }

    public enum SQLType
    {
        SQLStatic,
        SQLDynamic,
        SQLStoredProcedure
    }

    public enum SQLParameterType
    {
        Input,
        Output
    }

    public enum SQLConstraint
    {
        AutoIncrement,
        PrimaryKey,
        UniqueKey,
        None
    }
    public enum SQLAdapterType
    {
        SQL,
        HOSQL
    }
}