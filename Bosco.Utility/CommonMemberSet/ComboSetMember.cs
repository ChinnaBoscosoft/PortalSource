/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace Bosco.Utility.CommonMemberSet
{
    #region Combo Set

    public class ComboSetMember
    {
        public bool SelectComboItem(System.Web.UI.WebControls.DropDownList dropDownCombo, string value, bool isValue)
        {
            bool isItem = false;

            if (isValue)
            {
                System.Web.UI.WebControls.ListItem item = dropDownCombo.Items.FindByValue(value);
                if (item != null)
                {
                    dropDownCombo.SelectedIndex = -1;
                    item.Selected = true;
                    isItem = true;
                }
            }
            else
            {
                System.Web.UI.WebControls.ListItem item = dropDownCombo.Items.FindByText(value);
                if (item != null)
                {
                    dropDownCombo.SelectedIndex = -1;
                    item.Selected = true;
                    isItem = true;
                }
            }

            return isItem;
        }

        public void RemoveComboItem(System.Web.UI.WebControls.DropDownList dropDownCombo, string value, bool isValue)
        {
            if (isValue)
            {
                ListItem item = dropDownCombo.Items.FindByValue(value);
                dropDownCombo.Items.Remove(item);
            }
            else
            {
                ListItem item = dropDownCombo.Items.FindByText(value);
                dropDownCombo.Items.Remove(item);
            }
        }

        public void BindDataCombo(System.Web.UI.WebControls.DropDownList dropDownCombo, object dataSource, string listField, string valueField)
        {
            BindDataCombo(dropDownCombo, dataSource, listField, valueField, false, "");
        }

        public void BindDataCombo(System.Web.UI.WebControls.DropDownList dropDownCombo, object dataSource, string listField, string valueField, bool isAddEmptyItem, string emptyItemName)
        {
            dropDownCombo.DataSource = dataSource;
            dropDownCombo.DataTextField = listField;
            dropDownCombo.DataValueField = valueField;
            dropDownCombo.DataBind();

            if (isAddEmptyItem)
            {
                System.Web.UI.WebControls.ListItem lstSelect = new System.Web.UI.WebControls.ListItem(emptyItemName, "0");
                dropDownCombo.Items.Insert(0, lstSelect);
            }
        }

        public void BindDataList(System.Web.UI.WebControls.ListBox dropDownList, object dataSource, string listField, string valueField)
        {
            dropDownList.DataSource = dataSource;
            dropDownList.DataTextField = listField;
            dropDownList.DataValueField = valueField;
            dropDownList.DataBind();
        }
        public void BindEnum2DropDownList(DropDownList dropDownList, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);

            for (int i = 0; i <= names.Length - 1; i++)
            {
                dropDownList.Items.Add(new ListItem(names[i], Convert.ToInt32(values.GetValue(i)).ToString()));
            }
        }

        public void BindCombo(ASPxComboBox ComboBox, DataTable Source, string dispalyField, string valueField,bool IsAddItem)
        {
            ComboBox.TextField = dispalyField;
            ComboBox.ValueField = valueField;
            ComboBox.DataSource = Source;
            ComboBox.DataBind();
            if (IsAddItem)
            {
                ComboBox.Items.Insert(0, new ListEditItem("All"));
            }
            ComboBox.SelectedIndex = 0;
        }

        public void FillDataCheckBoxList(System.Web.UI.WebControls.CheckBoxList chkBoxList, object dataSource, string listField, string valueField)
        {
            chkBoxList.DataSource = dataSource;
            chkBoxList.DataTextField = listField;
            chkBoxList.DataValueField = valueField;
            chkBoxList.DataBind();
        }

    #endregion
    }
}
