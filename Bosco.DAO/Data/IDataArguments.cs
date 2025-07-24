/* Class        : IDataArguments.cs
 * Purpose      : Interface to DataArguments class
 * Author       : CS
 * Created on   : 18-Jul-2010
 */

using System;

namespace Bosco.DAO.Data
{
    public interface IDataArguments
    {
        string FieldName { get; set; }
        int FieldSize { get; set; }
        object FieldValue { get; set; }
        DataType FieldType { get; set; }
        bool IsRowUniqueId { get; set; }
        SQLParameterType ParameterType { get; set; }
    }
}
