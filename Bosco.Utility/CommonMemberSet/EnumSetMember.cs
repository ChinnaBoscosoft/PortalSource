/*  Class Name      : EnumType
 *  Purpose         : Enum type Utilities
 *  Author          : CS
 *  Created on      : 25-Jul-2010
 */

using System;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace Bosco.Utility.CommonMemberSet
{
    public class EnumSetMember
    {
        public Type GetEnumType(string assemblyName, string classTypeName, string enumType)
        {
            Type type = null;
            string typeName = "";

            try
            {
                //Identify type of identifier within a class 
                typeName = classTypeName + "+" + enumType + ", " + assemblyName;
                type = GetEnumType(typeName);

                //Identify type of identifier within a namespace
                if (type == null)
                {
                    typeName = classTypeName + "." + enumType + ", " + assemblyName;
                    type = GetEnumType(typeName);
                }
            }
            catch (Exception e)
            {
                new ExceptionHandler(e, true);
            }

            return type;
        }

        public Type GetEnumType(string enumType)
        {
            Type type = null;

            try
            {
                type = Type.GetType(enumType, false, true);
            }
            catch (Exception e)
            {
                new ExceptionHandler(e, true);
            }

            return type;
        }

        public Enum GetEnumItemType(string enumType, string enumItem)
        {
            Type type = GetEnumType(enumType);
            return GetEnumItemType(type, enumItem);
        }

        public Enum GetEnumItemType(string assemblyName, string classTypeName, string enumType, string enumItem)
        {
            Type type = GetEnumType(assemblyName, classTypeName, enumType);
            return GetEnumItemType(type, enumItem);
        }

        public Enum GetEnumItemType(Type enumType, string enumItem)
        {
            Enum enumTypeItem = null;

            if (enumType != null)
            {
                try
                {
                    enumTypeItem = (Enum)Enum.Parse(enumType, enumItem, true);
                }
                catch (Exception e)
                {
                    new ExceptionHandler(e, true);
                }
            }

            return enumTypeItem;
        }

        public string GetEnumItemNameByValue(Type enumType, int value)
        {
            string enumName = "";

            if (enumType != null)
            {
                try
                {
                    enumName = Enum.GetName(enumType, value);
                }
                catch (Exception e)
                {
                    new ExceptionHandler(e, true);
                }
            }

            return enumName;
        }



        public string GetDescriptionFromEnumValue(Enum enumValue)
        {
            string enumDescription = "";
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    enumDescription = ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumDescription;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName">Name of dll</param>
        /// <param name="partialNamespace">Namespace followed by the assembly name</param>
        /// <param name="enumType">Enumerated Type name</param>
        /// <returns></returns>

        public DataView GetEnumDataSource(string enumType)
        {
            return GetEnumDataSource(enumType, Sorting.None);
        }

        public DataView GetEnumDataSource(string enumType, Sorting dataSort)
        {
            Type type = GetEnumType(enumType);
            return GetEnumDataSource(type, dataSort);
        }

        public DataView GetEnumDataSource(Enum enumType, Sorting dataSort)
        {
            Type type = enumType.GetType();
            return GetEnumDataSource(type, dataSort);
        }

        public DataView GetEnumDataSource(string assemblyName, string classTypeName, string enumType)
        {
            Type type = GetEnumType(assemblyName, classTypeName, enumType);
            return GetEnumDataSource(type, Sorting.None);
        }

        public DataView GetEnumDataSource(string assemblyName, string classTypeName, string enumType, Sorting dataSort)
        {
            Type type = GetEnumType(assemblyName, classTypeName, enumType);
            return GetEnumDataSource(type, dataSort);
        }

        public DataView GetEnumDataSource(Type enumType)
        {
            return GetEnumDataSource(enumType, Sorting.None);
        }

        public DataView GetEnumDataSource(Type enumType, Sorting dataSort)
        {
            DataView dvEnumSource = null;
            DataRow drEnumSource = null;
            EnumTypeSchema.EnumTypeDataTable dtEnumSource = new EnumTypeSchema.EnumTypeDataTable();

            if (enumType != null)
            {
                try
                {
                    string[] enumNames = Enum.GetNames(enumType);
                    int enumValue = 0;

                    foreach (string enumName in enumNames)
                    {
                        enumValue = (int)Enum.Parse(enumType, enumName, true);

                        drEnumSource = dtEnumSource.NewRow();
                        drEnumSource[dtEnumSource.IdColumn.ColumnName] = enumValue;
                        drEnumSource[dtEnumSource.NameColumn.ColumnName] = enumName;
                        dtEnumSource.Rows.Add(drEnumSource);
                    }

                    dtEnumSource.AcceptChanges();
                    dvEnumSource = dtEnumSource.DefaultView;

                    if (dataSort == Sorting.Ascending || dataSort == Sorting.Descending)
                    {

                        if (dataSort == Sorting.Descending)
                        {
                            dvEnumSource.Sort = dtEnumSource.NameColumn.ColumnName + " DESC";
                        }
                        else
                        {
                            dvEnumSource.Sort = dtEnumSource.NameColumn.ColumnName;
                        }
                    }
                }
                catch (Exception e)
                {
                    new ExceptionHandler(e, true);
                }
            }

            return dvEnumSource;
        }

        public DataView GetStringListDataSource(string stringList, string delimiter)
        {
            DataView dvSource = null;
            DataRow drSource = null;
            EnumTypeSchema.EnumTypeDataTable dtSource = new EnumTypeSchema.EnumTypeDataTable();

            if (stringList != "")
            {
                try
                {
                    string[] aStringList = stringList.Split(delimiter.ToCharArray());
                    int value = 0;

                    foreach (string item in aStringList)
                    {
                        drSource = dtSource.NewRow();
                        drSource[dtSource.IdColumn.ColumnName] = ++value;
                        drSource[dtSource.NameColumn.ColumnName] = item;
                        dtSource.Rows.Add(drSource);
                    }

                    dtSource.AcceptChanges();
                    dvSource = dtSource.DefaultView;
                }
                catch (Exception e)
                {
                    new ExceptionHandler(e, true);
                }
            }

            return dvSource;
        }

        public DataView GetEnumDataSourceDescriptionfromEnumType(Enum enumType, Sorting dataSort)
        {
            DataView dvEnumSource = null;
            DataRow drEnumSource = null;
            EnumTypeSchema.EnumTypeDataTable dtEnumSource = new EnumTypeSchema.EnumTypeDataTable();

            if (enumType != null)
            {
                try
                {
                    int enumValue = 0;
                    int i = 0;
                    int count=GetEnumDataSource(enumType.GetType()).Table.Rows.Count;
                    string[] descs = new string[count];
                    string[] names = enumType.GetType().GetEnumNames();
                    foreach (string name in names)
                    {
                        FieldInfo fi = enumType.GetType().GetField(name);
                        object[] da = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        foreach (DescriptionAttribute ds in da)
                        {
                            descs[i] = ds.Description;
                            i++;
                        }
                    }
                    foreach (string description in descs)
                    {
                        drEnumSource = dtEnumSource.NewRow();
                        drEnumSource[dtEnumSource.IdColumn.ColumnName] = enumValue;
                        drEnumSource[dtEnumSource.NameColumn.ColumnName] = description;
                        dtEnumSource.Rows.Add(drEnumSource);
                        enumValue++;
                    }

                    if (dataSort == Sorting.Ascending || dataSort == Sorting.Descending)
                    {

                        if (dataSort == Sorting.Descending)
                        {
                            dvEnumSource.Sort = dtEnumSource.NameColumn.ColumnName + " DESC";
                        }
                        else
                        {
                            dvEnumSource.Sort = dtEnumSource.NameColumn.ColumnName;
                        }
                    }
                    dtEnumSource.AcceptChanges();
                    dvEnumSource = dtEnumSource.DefaultView;

                }
                catch (Exception e)
                {
                    new ExceptionHandler(e, true);
                }
            }

            return dvEnumSource;
        }
    }
}
