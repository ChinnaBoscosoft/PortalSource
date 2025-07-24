/* Class        : DataArguments.cs
 * Purpose      : Send SQL Parameter collection properties to SQLDataHandler
 * Author       : CS
 * Created on   : 22-Jul-2010
 */

using System;

namespace Bosco.DAO.Data
{
    public class DataArguments : IDataArguments
    {
        private string fieldName = "";
        private int fieldSize = 0;
        private object fieldValue = null;
        private bool isRowUniqueId = false;
        private SQLParameterType parameterType = SQLParameterType.Input;
        private DataType fieldType;
        private SQLConstraint sqlConstraint;

        public DataArguments() { }

        public DataArguments(string fieldName, object fieldValue)
        {
            this.fieldName = fieldName;
            this.fieldValue = fieldValue;
        }

        #region IDataArguments Members

        public virtual string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public virtual int FieldSize
        {
            get { return fieldSize; }
            set { fieldSize = value; }
        }

        public virtual object FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        public virtual DataType FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        public virtual bool IsRowUniqueId
        {
            get { return isRowUniqueId; }
            set { isRowUniqueId = value; }
        }

        public virtual SQLParameterType ParameterType
        {
            get { return parameterType; }
            set { parameterType = value; }
        }

        public SQLConstraint SQLConstraint
        {
            get { return sqlConstraint; }
            set { sqlConstraint = value; }
        }

        #endregion
    }
}
