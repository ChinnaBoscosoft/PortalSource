using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

using AcMeERP.Base;
using Bosco.Utility;
using AcMeERP.Module.Office;


namespace AcMeERP.WebControl
{
    public partial class GridViewControl : System.Web.UI.UserControl
    {
        public event GridViewCommandEventHandler RowCommand;
        public event GridViewRowEventHandler RowDataBound;
        public event GridViewSelectEventHandler RowSelecting;
        public event GridViewEditEventHandler RowEditing;
        public event GridViewDeleteEventHandler RowDeleting;
        public event EventHandler ExportClicked;
        public event EventHandler DownloadBranchTemplate;
        public event EventHandler RowAdd;
        public event EventHandler FindSort;
        public event EventHandler RaiseError;

        private List<TemplateColumnCollection> templateColumnCollection = new List<TemplateColumnCollection>();
        private Dictionary<string, string> filterColumnList = new Dictionary<string, string>();
        private object dataSource = null;

        private bool hasRowIdColumn = false;
        private bool showAdd = true;
        private bool enableSort = true;
        private bool showExport = false;

        private string sortColumn = "";
        private string hideColumn = "";
        private string[] ahideColumn;
        private string rowIdColumn = "";
        private string defaultRowFilter = "";
        private string columnOrder = "";
        private string[] acolumnOrder;


        /// <summary>
        /// Class to set additional column filter for grid data
        /// </summary>

        private ExternalFilter externalFilter;
        private bool hasExternalFilter = false;

        public ExternalFilter ExternalFilterItem
        {
            get { return this.externalFilter; }
            set { this.externalFilter = value; }
        }

        private int customPageSize = 0;

        public int CustomPageSize
        {
            set { customPageSize = value; }
            get { return customPageSize; }
        }

        public string ColumnOrder
        {
            get { return columnOrder; }
            set
            {
                columnOrder = value;
                acolumnOrder = columnOrder.Split(',');
            }
        }
       
        public bool HideToolBar
        {
            set { toolBar.Visible = !value; }
            get { return toolBar.Visible; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmltw = new HtmlTextWriter(sw);
            base.Render(htmltw);
            StringBuilder sb = sw.GetStringBuilder();
            sb = sb.Replace(Delimiter.NewLine, Delimiter.NewLineTag);
            writer.Write(sb);
        }

        int currentRowIndex = 0;
        public string GetRowId(string dataKeyFieldName)
        {
            string rowId = GridViewUserControl.DataKeys[currentRowIndex][dataKeyFieldName].ToString();
            return rowId;
        }

        public GridViewRowCollection Rows
        {
            get
            {
                return GridViewUserControl.Rows;
            }
        }

        public object DataSource
        {
            set
            {
                this.dataSource = value;
                SetDefaultRowFilter();
                SetBoundedColumn();

                if (!IsPostBack)
                {
                    FillExternalFilterData();
                    FillFilterData();
                    BindGridSource();
                }
            }
        }

        public void BindGrid(object dataSource)
        {
            this.dataSource = dataSource;
            SetDefaultRowFilter();
            BindGridSource();
        }

        public void BindGrid()
        {
            BindGridSource();
        }

        private void BindGridSource()
        {

            SetBinderCriteria();
            GridViewUserControl.DataSource = this.dataSource;
            GridViewUserControl.PageSize = this.PageSize;
            GridViewUserControl.DataBind();
            if (GridViewUserControl.AllowPaging)
            {
                FillPageCombo();
            }
        }


        private void SetDefaultRowFilter()
        {
            if (dataSource != null && dataSource.GetType().Name == typeof(DataView).Name)
            {
                defaultRowFilter = ((DataView)dataSource).RowFilter;
            }
        }

        private int PageSize
        {
            get
            {
                if (customPageSize == 0)
                {
                    int pagesize = 0;
                    if (pagesize == 0) { pagesize = 20; }
                    return pagesize;
                }
                else
                {
                    return customPageSize;
                }
            }
        }

        public bool ShowAdd
        {
            get { return showAdd; }
            set
            {
                showAdd = value;
                lbAdd.Visible = showAdd;
            }
        }
        public bool ShowExport
        {
            get { return showExport; }
            set
            {
                showExport = value;
                imgExport.Visible = showExport;
            }
        }

        public bool EnableSort
        {
            get { return enableSort; }
            set
            {
                enableSort = value;
            }
        }

        public string RowIdColumn
        {
            get { return rowIdColumn; }
            set
            {
                rowIdColumn = value;
                string[] aRowIdColumn = rowIdColumn.Split(Delimiter.AtCap.ToCharArray());
                GridViewUserControl.DataKeyNames = aRowIdColumn;
            }
        }

        /// <summary>
        /// get/set Comma separated column name
        /// </summary>
        public string HideColumn
        {
            get { return hideColumn; }
            set
            {
                hideColumn = value;
                ahideColumn = hideColumn.Split(',');
            }
        }

        private void RaiseFindSortEvent()
        {
            if (FindSort != null)
            {
                FindSort(this, new EventArgs());
            }
        }

        //Overloading Methods
        public void SetTemplateColumn(ControlType type, CommandMode commandMode, string rowIdField)
        {
            SetTemplateColumn(type, commandMode, rowIdField, "", null, "", "", null);
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode, string headerText, ScriptColumn scriptColumn)
        {
            SetTemplateColumn(type, commandMode, "", "", null, "", headerText, scriptColumn);
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode,
            string rowIdField, string boundField)
        {
            SetTemplateColumn(type, commandMode, rowIdField, boundField, null, "", "", null);
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode,
            string rowIdField, LinkUrlColumn linkUrlColumn)
        {
            SetTemplateColumn(type, commandMode, rowIdField, "", linkUrlColumn, "", "", null);
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode,
            string rowIdField, string boundField, LinkUrlColumn linkUrlColumn)
        {
            SetTemplateColumn(type, commandMode, rowIdField, boundField, linkUrlColumn, "", "", null);
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode,
               string rowIdField, string boundField, LinkUrlColumn linkUrlColumn, string imageUrl)
        {
            SetTemplateColumn(type, commandMode, rowIdField, boundField, linkUrlColumn, imageUrl, "", null);
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode,
            string rowIdField, string boundField, LinkUrlColumn linkUrlColumn,
            string imageUrl, string headerText)
        {
            SetTemplateColumn(type, commandMode, rowIdField, boundField, linkUrlColumn, imageUrl, headerText, null);
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode,
            string rowIdField, string boundField, LinkUrlColumn linkUrlColumn,
            string imageUrl, string headerText, ScriptColumn scriptColumn)
        {
            this.rowIdColumn = rowIdField;

            templateColumnCollection.Add(new TemplateColumnCollection(type, commandMode,
                rowIdField, boundField, linkUrlColumn, imageUrl, headerText, scriptColumn));
        }

        public void SetTemplateColumn(ControlType type, CommandMode commandMode,
            string rowIdField, string conditionalField, string conditionalValue,
            LinkUrlColumn linkUrlColumn, LinkUrlColumn alternateLinkUrlColumn, string headerText)
        {
            this.rowIdColumn = rowIdField;

            templateColumnCollection.Add(new TemplateColumnCollection(type, commandMode,
                rowIdField, conditionalField, conditionalValue, linkUrlColumn, alternateLinkUrlColumn, headerText));
        }

        protected void cboPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboPage.SelectedItem.Text.Equals("All"))
            {
                GridViewUserControl.AllowPaging = true;
                SetPageIndex(cboPage.SelectedIndex);
            }
            else
            {
                GridViewUserControl.AllowPaging = false;
                BindGridSource();
            }
        }

        protected void cboFilterExternal_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridSource();
        }

        private void SetPageIndex(int pageIndex)
        {
            RaiseFindSortEvent();
            GridViewUserControl.PageIndex = pageIndex;
            BindGridSource();
        }

        protected void GridViewUserControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SetPageIndex(e.NewPageIndex);
        }

        protected void GridViewUserControl_Sorting(object sender, GridViewSortEventArgs e)
        {
            RaiseFindSortEvent();
            sortColumn = e.SortExpression;
            BindGridSource();
        }

        protected void GridViewUserControl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int idx = GridViewUserControl.SelectedValue .RowIndex;

            if (e.CommandSource.GetType() == typeof(ImageButton))
            {
                GridViewRow gvRow = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                currentRowIndex = gvRow.RowIndex;
            }
           
            if (RowCommand != null)
            {
                RowCommand(sender, e);

                if (e.CommandName == Bosco.Utility.CommandMode.Edit.ToString())
                {
                    BindGridSource();
                }
            }
        }

        protected void GridViewUserControl_RowSelecting(object sender, GridViewSelectEventArgs e)
        {
            if (RowSelecting != null)
            {
                RowSelecting(sender, e);
            }
        }

        protected void GridViewUserControl_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (RowEditing != null)
            {
                RowEditing(sender, e);
            }
        }

        protected void GridViewUserControl_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (RowDeleting != null)
            {
                RowDeleting(sender, e);
            }
        }
        
        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (RowAdd != null)
            {
                RowAdd(sender, e);
            }
            else
            {
                BindGridSource();
            }
        }

        protected void imgFilterGo_Click(object sender, ImageClickEventArgs e)
        {
            RaiseFindSortEvent();
            BindGridSource();
        }

        /*protected void txtFilter_TextChanged(object sender, EventArgs e)
        {
            RaiseFindSortEvent(); 
            BindGridSource();
        }*/

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            RaiseFindSortEvent();
            txtFilter.Text = "";

            if (cboFilter.Items.Count > 0)
            {
                cboFilter.SelectedIndex = 0;
            }
            else
            {
                cboFilter.SelectedIndex = -1;
            }
            BindGridSource();
        }

        private void SetBoundedColumn()
        {
            if (dataSource == null)
            {
                if (!showAdd)
                {
                    this.lbAdd.Visible = false;
                }
                return;
            }

            DataView dvSource = dataSource as DataView;
            if (dvSource == null)
            {
                dataSource = null;
                return;
            }

            //Set column Order
            if (columnOrder != "" && acolumnOrder.Length > 0)
            {
                for (int i = 0; i < acolumnOrder.Length; i++)
                {
                    if (dvSource.Table.Columns.Contains(acolumnOrder[i]))
                    {
                        dvSource.Table.Columns[acolumnOrder[i]].SetOrdinal(i);
                    }
                }
            }

            DataColumnCollection columnGrid = dvSource.Table.Columns;
            GridViewUserControl.AutoGenerateColumns = false;
            filterColumnList.Clear();

            foreach (DataColumn dc in columnGrid)
            {
                bool showColumn = true;
                BoundField boundField = new BoundField();

                boundField.HeaderText = dc.Caption;
                boundField.DataField = dc.ColumnName;
                if (!EnableSort)
                    boundField.SortExpression = "";
                else
                    boundField.SortExpression = dc.ColumnName;

                if (isNumericType(dc.DataType.FullName))
                {
                    boundField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    boundField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }

                if (ahideColumn != null)
                {

                    for (int i = 0; i < ahideColumn.Length; i++)
                    {
                        if (ahideColumn[i].ToLower() == dc.ColumnName.ToLower())
                        {
                            showColumn = false;
                            break;
                        }
                    }
                }

                if (showColumn)
                {
                    GridViewUserControl.Columns.Add(boundField);
                    filterColumnList[dc.ColumnName] = dc.Caption;
                }

                if (!hasRowIdColumn)
                {
                    hasRowIdColumn = (rowIdColumn.ToLower() == dc.ColumnName.ToLower());
                }
            }

            if (!hasRowIdColumn)
            {
                rowIdColumn = "";
            }

            AddTemplateColumn();
        }

        private void AddTemplateColumn()
        {
            TemplateField templateField;
            Unit unit = new Unit(25, UnitType.Pixel);
            int index = 0;
            lbAdd.Visible = false;
            hlkAdd.Visible = false;

            foreach (TemplateColumnCollection tcCollection in templateColumnCollection)
            {
                //Set target page url for Add control
                if (tcCollection.CommandMode == CommandMode.Add && tcCollection.LinkUrlColumn != null)
                {
                    if (tcCollection.ControlType == ControlType.HyperLink)
                    {
                        hlkAdd.Visible = true;
                        hlkAdd.Target = "_self";
                        hlkAdd.CssClass = "button";
                        //hlkAdd.Width = new Unit(150);
                        hlkAdd.Height = new Unit(16);
                        hlkAdd.Text = tcCollection.LinkUrlColumn.LinkUrlCaption;
                        if (tcCollection.LinkUrlColumn.ShowModelWindow == true) { hlkAdd.Target = "_blank"; }
                        hlkAdd.NavigateUrl = tcCollection.LinkUrlColumn.LinkUrlPage;
                    }
                    else
                    {
                        lbAdd.Visible = true;

                        bool retVal = (tcCollection.LinkUrlColumn.ShowModelWindow == true);

                        lbAdd.OnClientClick = Bosco.Utility.CommonMemberSet.ClientScriptSetMember.GetOpenWindowScript(tcCollection.LinkUrlColumn.LinkUrlPage,
                            "0", tcCollection.LinkUrlColumn.Height, tcCollection.LinkUrlColumn.Width,
                            tcCollection.LinkUrlColumn.Left, tcCollection.LinkUrlColumn.Top, retVal, tcCollection.LinkUrlColumn.ShowModelWindow);
                        lbAdd.CommandArgument = "0";
                        lbAdd.CommandName = Bosco.Utility.CommandMode.Add.ToString();
                    }
                }
                else
                {
                    //Add template column like Edit, Delete, View, ....
                    templateField = new GridViewTemplateField(tcCollection);
                    GridViewUserControl.Columns.Add(templateField);

                    //Set column style for width, align of the grid
                    index = (GridViewUserControl.Columns.Count - 1);
                    GridViewUserControl.Columns[index].HeaderStyle.Width = unit;
                    GridViewUserControl.Columns[index].HeaderStyle.CssClass = "padleft5px";
                    GridViewUserControl.Columns[index].ItemStyle.Width = unit;
                    GridViewUserControl.Columns[index].ItemStyle.CssClass = "padleft5px";
                    GridViewUserControl.Columns[index].ItemStyle.Wrap = true;
                   
                    GridViewUserControl.Columns[index].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    GridViewUserControl.Columns[index].ItemStyle.HorizontalAlign = HorizontalAlign.Left;

                    if (tcCollection.CommandMode == Bosco.Utility.CommandMode.Edit ||
                        tcCollection.CommandMode == Bosco.Utility.CommandMode.Delete || tcCollection.CommandMode == Bosco.Utility.CommandMode.License || 
                        tcCollection.CommandMode == Bosco.Utility.CommandMode.Download || tcCollection.CommandMode == Bosco.Utility.CommandMode.Status || 
                        tcCollection.CommandMode == Bosco.Utility.CommandMode.Email || tcCollection.CommandMode == Bosco.Utility.CommandMode.Data ||
                        tcCollection.CommandMode == Bosco.Utility.CommandMode.DB || tcCollection.CommandMode == Bosco.Utility.CommandMode.Select
                        || tcCollection.CommandMode == Bosco.Utility.CommandMode.View || tcCollection.CommandMode == Bosco.Utility.CommandMode.Reset
                        || tcCollection.CommandMode == Bosco.Utility.CommandMode.Key || tcCollection.CommandMode == Bosco.Utility.CommandMode.Resend)
                    {
                        //GridViewUserControl.Columns[index].HeaderStyle.CssClass = "nonprintable";
                        //GridViewUserControl.Columns[index].ItemStyle.CssClass = "nonprintable";
                        GridViewUserControl.Columns[index].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        GridViewUserControl.Columns[index].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        private void FillFilterData()
        {
            txtFilter.Text = "";
            cboFilter.Items.Clear();

            if (filterColumnList.Count == 0) return;

            foreach (KeyValuePair<string, string> filterKey in filterColumnList)
            {
                ListItem item = new ListItem(filterKey.Value, filterKey.Key);
                cboFilter.Items.Add(item);
            }
        }

        private void FillExternalFilterData()
        {
            hasExternalFilter = (externalFilter != null);
            ltFilterExternal.Visible = hasExternalFilter;
            cboFilterExternal.Visible = hasExternalFilter;

            if (!hasExternalFilter) { return; }
            ltFilterExternal.Text = externalFilter.FilterCaption;

            UIBase page = this.Page as UIBase;
            if (page != null)
            {
                string defaultVal = "";
                if (externalFilter.BindEmptyListItem) { defaultVal = DefaultItem.AllWithLine; }

                page.Member.ComboSet.BindDataCombo(cboFilterExternal, externalFilter.FilterSource,
                                          externalFilter.ListField, externalFilter.ValueField,
                                          externalFilter.BindEmptyListItem, defaultVal);
            }

            /*if (cboFilterExternal.Items.Count == 0)
            {
                for (int i = 0; i <= externalFilter.FilterList.GetUpperBound(0); i++)
                {
                    ListItem listItem = new ListItem(externalFilter.FilterList[i, 0], externalFilter.FilterList[i, 1]);
                    cboFilterExternal.Items.Add(listItem);
                }
            }*/
        }

        private void FillPageCombo()
        {
            int pageCount = GridViewUserControl.PageCount;
            int itemCount = cboPage.Items.Count;
            cboPage.Items.Clear();

           
            for (int i = 0; i < pageCount; i++)
            {
                cboPage.Items.Add("Page " + (i + 1) + " / " + pageCount);
            }
            if (cboPage.Items.Count > 0)
            {
                cboPage.SelectedIndex = GridViewUserControl.PageIndex;
            }

            if (pageCount > 1)
            {
                cboPage.Items.Add("All");
            }

            DataView dvSource = this.dataSource as DataView;
            int recordCount = 0;

            if (dvSource != null) { recordCount = dvSource.Count; }
            ltRecord.Text = "Records # : " + recordCount.ToString();
        }

        public void SetComboFirstItem()
        {
            this.cboFilter.SelectedIndex = 0;
        }

        public void ClearFilterText()
        {
            this.txtFilter.Text = "";
        }

        public void SetFilterTextBoxFocus()
        {
            this.txtFilter.Focus();
        }

        private void SetBinderCriteria()
        {
            if (dataSource == null) return;

            bool hasFilter = false;
            string filterColumn = "";
            string filterItem = "";
            string rowFilter = "";

            if (cboFilter.SelectedValue != null)
            {
                filterColumn = cboFilter.SelectedValue;
                filterItem = txtFilter.Text.Trim();
                hasFilter = (filterColumn != "" && filterItem != "");
            }

            DataView dv = dataSource as DataView;
            if (hasFilter) rowFilter = GetRowFilterItem(dv, filterColumn, filterItem, false);

            //Add external filter
            if (hasExternalFilter || cboFilterExternal.SelectedItem != null)
            {
                if (!externalFilter.BindEmptyListItem || (externalFilter.BindEmptyListItem && cboFilterExternal.SelectedIndex > 0))
                {
                    filterColumn = externalFilter.FieldNameToFilter;
                    filterItem = ((externalFilter.ItemToFilter == ExternalFilter.FilterItem.ByListField) ?
                                   cboFilterExternal.SelectedItem.Text : cboFilterExternal.SelectedValue);

                    if (filterItem != string.Empty && filterItem != "0")
                    {
                        rowFilter += ((rowFilter != "") ? " AND " : "") + GetRowFilterItem(dv, filterColumn, filterItem, true);
                    }
                    hasFilter = true;
                }
            }
            try
            {
                dv.RowFilter = "";
            }
            catch { }


            if (defaultRowFilter != "")
            {
                rowFilter += ((rowFilter != "") ? " AND " : "") + defaultRowFilter;
                hasFilter = true;
            }

            //set row filter Item
            if (hasFilter)
            {
                dv.RowFilter = rowFilter;
            }

            //Sort item
            if (sortColumn != "")
            {
                try
                {
                    dv.Sort = sortColumn; //+" " + e.SortDirection.ToString();
                }
                catch { }
            }
        }

        private string GetRowFilterItem(DataView dv, string filterColumn, string filterItem, bool filterExact)
        {
            string filter = "";
            DataTable dt = dv.Table;
            string filterColumnType = dt.Columns[filterColumn].DataType.FullName;
            //To filter by Special Characters

            filterItem = ((UIBase)this.Page).Member.StringSet.RowFilterText(filterItem);

            if (dt.Columns[filterColumn].DataType.FullName == "System.String")
            {
                if (filterExact)
                {
                    filter = "[" + filterColumn + "] = '" + filterItem + "'";
                }
                else
                {
                    filter = "[" + filterColumn + "] LIKE '*" + filterItem + "*'";
                }
            }
            else if (dt.Columns[filterColumn].DataType.FullName == "System.DateTime")
            {
                filter = "[" + filterColumn + "] = #" + filterItem + "#";
            }
            else if (isNumericType(filterColumnType))
            {
                try
                {
                    filter = "[" + filterColumn + "] = " + double.Parse(filterItem);
                }
                catch
                {
                    filter = "";
                    if (RaiseError != null) RaiseError(this, new EventArgs());
                }
            }
            else
            {
                filter = "[" + filterColumn + "] = " + filterItem;
            }

            return filter;
        }

        private bool isNumericType(string typeName)
        {
            bool isNumber = false;

            isNumber = typeName == "System.Int16" | typeName == "System.Int32" |
                     typeName == "System.Int64" | typeName == "System.Double" |
                     typeName == "System.Decimal" | typeName == "System.UInt32";

            return isNumber;
        }

        //It is for Additional custom filter
        public class ExternalFilter
        {
            private object filterSource;
            private string fieldNameToFilter = "";
            private string listField = "";
            private string valueField = "";
            private string filterCaption = "";
            private bool bindEmptyListItem = false;
            private FilterItem filterItem = FilterItem.ByListField;

            public enum FilterItem
            {
                ByListField = 0,
                ByValueField = 1
            }

            /// <summary>
            /// List Item, List Value
            /// </summary>
            public object FilterSource
            {
                get { return this.filterSource; }
                set { this.filterSource = value; }
            }

            public FilterItem ItemToFilter
            {
                get { return this.filterItem; }
                set { this.filterItem = value; }
            }

            public string FieldNameToFilter
            {
                get { return this.fieldNameToFilter; }
                set { this.fieldNameToFilter = value; }
            }

            public string ListField
            {
                get { return this.listField; }
                set { this.listField = value; }
            }

            public string ValueField
            {
                get { return this.valueField; }
                set { this.valueField = value; }
            }

            public string FilterCaption
            {
                get { return this.filterCaption; }
                set { this.filterCaption = value; }
            }

            public bool BindEmptyListItem
            {
                get { return this.bindEmptyListItem; }
                set { this.bindEmptyListItem = value; }
            }

            /// <summary>
            /// filterList[list item, list value]
            /// </summary>
            /// <param name="filterList">list item, list value to display in the combo</param>
            /// <param name="fieldName">name of the to be filtered</param>
            public ExternalFilter(object filterSource, string fieldNameToFilter, string listField,
                                  string valueField, string filterCaption, FilterItem itemToFilter,
                                  bool bindEmptyListItem)
            {
                this.filterSource = filterSource;
                this.fieldNameToFilter = fieldNameToFilter;
                this.listField = listField;
                this.valueField = valueField;
                this.filterCaption = filterCaption;
                this.filterItem = itemToFilter;
                this.bindEmptyListItem = bindEmptyListItem;
            }
        }

        protected void GridViewUserControl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridViewUserControl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (RowDataBound != null)
            {
                RowDataBound(sender, e);
            }
        }

        protected void imgExport_Click(object sender, ImageClickEventArgs e)
        {
            if (ExportClicked != null)
            {
                ExportClicked(sender, e);
            }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        
        }
       
    }
}