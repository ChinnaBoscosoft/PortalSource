/* Class        : SQLInterfaceBase.cs
 * Purpose      : Base Class to SQLInterface contains properties set at UI level
 * Author       : CS
 * Created on   : 19-Jul-2010
 */


using System;
using System.Reflection;
using System.Data;
using System.Collections;
using System.Collections.Generic;

//using Bosco.Utility.ConfigSetting;

namespace Bosco.DAO.Data
{
    public class DataParameters
    {
        private List<IDataArguments> dataItem = new List<IDataArguments>();
        private bool hasRowUniqueIdAsParamater = false;
        private bool isParamRowUniqueId = false;

        #region SQL parameter Collection

        public void Add(object field)
        {
            this.Add(field, "", 0, DataType.None, SQLConstraint.None, SQLParameterType.Input);
        }

        public void Add(object field, SQLParameterType parameterType)
        {
            this.Add(field, "", 0, DataType.None, SQLConstraint.None, parameterType);
        }

        public void Add(object field, bool isRowUniqueId)
        {
            this.Add(field, isRowUniqueId, SQLParameterType.Input);
        }

        public void Add(object field, bool isRowUniqueId, SQLParameterType parameterType)
        {
            hasRowUniqueIdAsParamater = true;
            isParamRowUniqueId = isRowUniqueId;
            
            this.Add(field, "", 0, DataType.None, SQLConstraint.None, parameterType);
            
            hasRowUniqueIdAsParamater = false;
            isParamRowUniqueId = false;
        }

        public void Add(object field, object fieldValue)
        {
            this.Add(field, fieldValue, 0, DataType.None, SQLConstraint.None, SQLParameterType.Input);
        }

        public void Add(object field, object fieldValue, SQLParameterType parameterType)
        {
            this.Add(field, fieldValue, 0, DataType.None, SQLConstraint.None, parameterType);
        }

        public void Add(object field, object fieldValue, bool isRowUniqueId)
        {
            this.Add(field, fieldValue, isRowUniqueId, SQLParameterType.Input);
        }

        public void Add(object field, object fieldValue, bool isRowUniqueId, SQLParameterType parameterType)
        {
            hasRowUniqueIdAsParamater = true;
            isParamRowUniqueId = isRowUniqueId;

            this.Add(field, fieldValue, 0, DataType.None, SQLConstraint.None, parameterType);

            hasRowUniqueIdAsParamater = false;
            isParamRowUniqueId = false;
        }

        public void Add(object field, object fieldValue, DataType fieldType)
        {
            this.Add(field, fieldValue, 0, fieldType, SQLConstraint.None, SQLParameterType.Input);
        }

        public void Add(object field, object fieldValue, DataType fieldType, SQLParameterType parameterType)
        {
            this.Add(field, fieldValue, 0, fieldType, SQLConstraint.None, parameterType);
        }

        public void Add(object field, object fieldValue, int fieldSize, DataType fieldType)
        {
            this.Add(field, fieldValue, fieldSize, fieldType, SQLConstraint.None, SQLParameterType.Input);
        }

        public void Add(object field, object fieldValue, int fieldSize, DataType fieldType, SQLParameterType parameterType)
        {
            this.Add(field, fieldValue, fieldSize, fieldType, SQLConstraint.None, parameterType);
        }

        private void Add(object field, object fieldValue, int fieldSize, DataType fieldType, SQLConstraint sqlConstraint)
        {
            this.Add(field, fieldValue, fieldSize, fieldType, sqlConstraint, SQLParameterType.Input);
        }

        private void Add(object field, object fieldValue, int fieldSize, DataType fieldType, 
            SQLConstraint sqlConstraint, SQLParameterType parameterType)
        {
            string fieldName = "";
            bool isRowUniqueId = false;

            if (field.GetType() == typeof(DataColumn))
            {
                DataColumn dcField = field as DataColumn;
                fieldType = GetFieldType(dcField);
                fieldName = dcField.ColumnName;
                fieldSize = dcField.MaxLength;
                isRowUniqueId = dcField.Unique;
            }
            else
            {
                fieldName = field.ToString();
            }

            if (hasRowUniqueIdAsParamater) { isRowUniqueId = isParamRowUniqueId; }

            DataArguments dataArguments = new DataArguments(fieldName, fieldValue);
            if (fieldType != DataType.None) dataArguments.FieldType = fieldType;
            dataArguments.FieldSize = fieldSize;
            dataArguments.IsRowUniqueId = isRowUniqueId;
            dataArguments.SQLConstraint = sqlConstraint;
            dataArguments.ParameterType = parameterType;
            dataItem.Add(dataArguments);
        }
        #endregion

        #region Parameter Data Types

        private DataType GetFieldType(DataColumn dcField)
        {
            DataType fieldType = DataType.None;
            string dataType = dcField.DataType.Name;

            switch (dataType)
            {
                case "Boolean":
                    fieldType = DataType.Boolean;
                    break;
                case "Byte":
                    fieldType = DataType.Byte;
                    break;
                case "Char":
                    fieldType = DataType.Char;
                    break;
                case "DateTime":
                    fieldType = DataType.DateTime;
                    break;
                case "Decimal":
                    fieldType = DataType.Decimal;
                    break;
                case "Double":
                    fieldType = DataType.Double;
                    break;
                case "Int16":
                    fieldType = DataType.Int16;
                    break;
                case "Int32":
                    fieldType = DataType.Int32;
                    break;
                case "Int64":
                    fieldType = DataType.Int64;
                    break;
                case "SByte":
                    fieldType = DataType.SByte;
                    break;
                case "Single":
                    fieldType = DataType.Single;
                    break;
                case "String":
                    fieldType = DataType.String;
                    break;
                case "TimeSpan":
                    fieldType = DataType.TimeSpan;
                    break;
                case "UInt16":
                    fieldType = DataType.UInt16;
                    break;
                case "UInt32":
                    fieldType = DataType.UInt32;
                    break;
                case "UInt64":
                    fieldType = DataType.UInt64;
                    break;
                case "Byte[]":
                    fieldType = DataType.ByteArray;
                    break;
               
            }

            return fieldType;
        }

        #endregion

        #region IEnumerable Collection

        public void RemoveItem(int index)
        {
            try
            {
                dataItem.RemoveAt(index);
            }
            catch { }
        }

        public bool RemoveItem(string fieldName)
        {
            bool isRemoved = false;

            foreach (IDataArguments item in this)
            {
                if (item.FieldName == fieldName)
                {
                    isRemoved = dataItem.Remove(item);
                    break;
                }
            }

            return isRemoved;
        }

        public IDataArguments this[int index]
        {
            get { return dataItem[index]; }
        }

        public IDataArguments GetItem(int index)
        {
            return dataItem[index];
        }

        public IDataArguments GetItem(string fieldName)
        {
            IDataArguments itemValue = null;

            foreach (IDataArguments item in this)
            {
                if (item.FieldName == fieldName)
                {
                    itemValue = item;
                    break;
                }
            }
            return itemValue;
        }

        public void Clear()
        {
            dataItem.Clear();
        }

        public int Count
        {
            get { return dataItem.Count; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return dataItem.GetEnumerator();
        }

        #endregion
    }
}
