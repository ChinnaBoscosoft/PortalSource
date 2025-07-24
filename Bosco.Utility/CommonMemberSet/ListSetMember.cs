/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace Bosco.Utility.CommonMemberSet
{
    #region List Set

    public class ListSetMember
    {
        /*public string GetCheckListItem(ListBox listBox,
            SelectionType selectType, string delimiter, bool isValue, out int count)
        {
            string selectedIds = "";
            count = 0;

            foreach (object item in listBox.Items)
            {
                if (selectType == SelectionType.All || (selectType == SelectionType.Selected && item.Selected == true))
                {
                    if (isValue)
                    {
                        selectedIds += ((selectedIds != "") ? delimiter : "") + item.Value;
                    }
                    else
                    {
                        selectedIds += ((selectedIds != "") ? delimiter + " " : "") + item.Text;
                    }
                    count++;
                }
                else if (selectType == SelectionType.Deselected && item.Selected == false)
                {
                    if (isValue)
                    {
                        selectedIds += ((selectedIds != "") ? delimiter : "") + item.Value;
                    }
                    else
                    {
                        selectedIds += ((selectedIds != "") ? delimiter + " " : "") + item.Text;
                    }
                    count++;
                }
            }
            return selectedIds;
        }

        public void SelectCheckListItem(System.Web.UI.WebControls.CheckBoxList chkListBox,
            string selectedItems, string delimiter, bool isValue)
        {
            string[] aSelectedItems = selectedItems.Split(delimiter.ToCharArray());

            foreach (string item in aSelectedItems)
            {

                if (isValue && chkListBox.Items.FindByValue(item) != null)
                {
                    chkListBox.Items.FindByValue(item).Selected = true;
                }
                else if (chkListBox.Items.FindByText(item) != null)
                {
                    chkListBox.Items.FindByText(item).Selected = true;
                }
            }
        }

        public bool SelectListItem(System.Web.UI.WebControls.ListBox listBox, string value, bool isValue)
        {
            bool isItem = false;

            if (isValue)
            {
                System.Web.UI.WebControls.ListItem item = listBox.Items.FindByValue(value);
                if (item != null)
                {
                    listBox.SelectedIndex = -1;
                    item.Selected = true;
                    isItem = true;
                }
            }
            else
            {
                System.Web.UI.WebControls.ListItem item = listBox.Items.FindByText(value);
                if (item != null)
                {
                    listBox.SelectedIndex = -1;
                    item.Selected = true;
                    isItem = true;
                }
            }

            return isItem;
        }

        public void BindDataList(System.Web.UI.WebControls.ListBox listBox, object dataSource, string listField, string valueField)
        {
            BindDataList(listBox, dataSource, listField, valueField, false);
        }

        public void BindDataList(System.Web.UI.WebControls.ListBox listBox, object dataSource, string listField, string valueField, bool isAllNeeded)
        {
            listBox.DataSource = dataSource;
            listBox.DataTextField = listField;
            listBox.DataValueField = valueField;
            listBox.DataBind();

            if (isAllNeeded)
            {
                System.Web.UI.WebControls.ListItem lstSelect = new System.Web.UI.WebControls.ListItem(DefaultItemBase.AllWithLine, "0");
                listBox.Items.Insert(0, lstSelect);
            }
        }

        public void BindDataCheckList(System.Web.UI.WebControls.CheckBoxList checkBoxList, object dataSource, string listField, string valueField)
        {
            checkBoxList.DataSource = dataSource;
            checkBoxList.DataTextField = listField;
            checkBoxList.DataValueField = valueField;
            checkBoxList.DataBind();

        }*/
    }

    #endregion
}
