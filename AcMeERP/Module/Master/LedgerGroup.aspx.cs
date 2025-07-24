/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page helps head office admin/Branch office admin to view the available HO Ledger Group and its concern Ledgers
 *****************************************************************************************************/
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Master
{
    public partial class LedgerGroup : Base.UIBase
    {
        #region Variables

        ResultArgs resultArgs = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        public int SortOrder = 0;

        #endregion

        #region Declaration
        private string GroupIDs
        {
            get
            {
                if (ViewState["GroupIDs"] == null)
                    return "0";
                else
                    return (string)ViewState["GroupIDs"];
            }
            set { ViewState["GroupIDs"] = value; }
        }

        private DataTable NewDataSource
        {
            get
            {
                if (ViewState["NewDataSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["NewDataSource"];
            }
            set
            {
                ViewState["NewDataSource"] = value;
            }
        }

        private DataView LedgerSource
        {
            get;
            set;
        }

        private int LedgerIdPass
        {
            get
            {
                int LedgerIdPass = this.Member.NumberSet.ToInteger(trlLedgerGroup.SelectedValue);
                return LedgerIdPass;
            }
            set
            {
                this.LedgerIdPass = 0;
            }
        }

        private string UpdationMode
        {
            set
            {
                ViewState["UpdationMode"] = value;
            }
            get
            {
                if (ViewState["UpdationMode"] == null)
                    return "Add";
                else
                    return (string)ViewState["UpdationMode"];
            }
        }

        private bool NodeSelection
        {
            set
            {
                ViewState["NodeSelection"] = value;
            }
            get
            {
                if (ViewState["NodeSelection"] == null)
                    return false;
                else
                    return (bool)ViewState["NodeSelection"];
            }
        }

        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTreeViewControl();
            }
            trlLedgerGroup.ImageSet = TreeViewImageSet.Arrows;
            trlChild.ImageSet = TreeViewImageSet.Arrows;
            LedgerGroupRights();//CheckUserRights
            if (trlLedgerGroup.Nodes.Count > 0)
            {
                this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerGroupView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                trlLedgerGroup.Nodes[0].Selected = true;
                NodeSelection = true;
                trlLedgerGroup_SelectedNodeChanged(sender, e);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // if(!(base.LoginUser.IsAdminUser)) 
                if ((!(base.LoginUser.IsAdminUser)) && (!(base.LoginUser.IsHeadOfficeUser)))
                {
                    btnAddGroup.Visible = btnEditGroup.Visible = btnDeleteGroup.Visible = false;
                    divLedgerGroup.Visible = false;
                }
                if (trlLedgerGroup.Nodes.Count > 0)
                {
                    if (trlLedgerGroup.Nodes[0].Selected)
                    {
                        NodeSelection = true;
                    }
                }

                gvView.SettingsPager.PageSizeItemSettings.Visible = false;
                this.PageTitle = MessageCatalog.Message.LedgerGroup.LedgerGroupViewPageTitle;
                this.ShowLoadWaitPopUp(btnAddGroup);

            }
            trlLedgerGroup.ExpandDepth = 1;
            trlChild.ExpandAll();

        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (NodeSelection == true)
            {
                btnNew.Visible = true;
                UpdationMode = "Add";
                ClearValues();
                LoadLedgerGroup();
                //LoadLedgerGroupCodes();
                imagePopupTitle.InnerText = MessageCatalog.Message.LedgerGroupAdd;
                SetControlFocus(ddlgroup);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LedgerGroupAdd", "javascript:ShowDisplayPopUp()", true);
            }
            else
            {
                this.Message = MessageCatalog.Message.SelectLedgerType;
            }
        }

        protected void btnEditGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (NodeSelection == true)
                {
                    UpdationMode = "Edit";
                    btnNew.Visible = false;
                    LoadLedgerGroup();
                    //LoadLedgerGroupCodes();
                    ddlgroup.Enabled = false;
                    lblMessage.Text = "";
                    AssignLedgerGroupDetails();
                    imagePopupTitle.InnerText = MessageCatalog.Message.LedgerGroupEdit;
                    SetControlFocus(ddlgroup);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LedgerGroupEdit", "javascript:ShowDisplayPopUp()", true);
                }
                else
                {
                    this.Message = MessageCatalog.Message.SelectLedgerType;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (NodeSelection == true)
                {
                    if ((ValidateGroupDelete()))
                    {
                        if (trlLedgerGroup.SelectedNode.ChildNodes.Count == 0)
                        {
                            using (LedgerGroupSystem LedgerSystem = new LedgerGroupSystem())
                            {
                                resultArgs = LedgerSystem.DeleteLedgerGroup(LedgerIdPass, DataBaseType.HeadOffice);
                                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    GenerateTreeview();
                                    if (trlLedgerGroup.Nodes.Count > 0)
                                    {
                                        trlLedgerGroup.Nodes[0].Selected = true;
                                        trlLedgerGroup_SelectedNodeChanged(sender, e);
                                    }
                                    this.Message = MessageCatalog.Message.LedgerGroupDeleteSccessful;
                                }
                                else
                                {
                                    this.Message = MessageCatalog.Message.LedgerGroupNotDelete;
                                }
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.LedgerGroupNotDelete;
                        }
                    }
                    else
                    {
                        this.Message = MessageCatalog.Message.LedgerGroupNatureNotDelete;
                    }
                }
            }
            catch (Exception Ex)
            {
                this.Message = Ex.Message;
            }
            finally { }
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (ValidateGroupDetails())
                {
                    try
                    {
                        using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                        {
                            ledgerSystem.Abbrevation = txtCode.Text.Trim().ToUpper();
                            ledgerSystem.Group = txtLedgerName.Text.Trim();
                            ledgerSystem.ParentGroupId = this.Member.NumberSet.ToInteger(ddlgroup.SelectedValue.ToString());
                            ledgerSystem.NatureId = ledgerSystem.GetNatureId(this.Member.NumberSet.ToInteger(ddlgroup.SelectedValue.ToString()));
                            ledgerSystem.MainGroupId = this.Member.NumberSet.ToInteger(ddlgroup.SelectedValue.ToString());
                            ledgerSystem.GroupId = (UpdationMode == "Add") ? (int)AddNewRow.NewRow : LedgerIdPass;
                            ledgerSystem.ImageId = 1;
                            ledgerSystem.SortOrder = SortOrder = ledgerSystem.FetchSortOrder();
                            if (SortOrder != 0 && ledgerSystem.ParentGroupId.Equals(ledgerSystem.NatureId))
                            {
                                ledgerSystem.SortOrder = GenerateSortOrder(SortOrder);
                            }
                            else if (!ledgerSystem.ParentGroupId.Equals(ledgerSystem.NatureId))
                            {
                                if (SortOrder == 0)
                                {
                                    SortOrder = ledgerSystem.FetchMainGroupSortOrder();
                                }
                                ledgerSystem.SortOrder = GenerateSortOrderSquence(SortOrder);
                            }
                            resultArgs = ledgerSystem.SaveLedgerGroupDetails(DataBaseType.HeadOffice);
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                if (UpdationMode == "Add")
                                {
                                    ClearOldValues();
                                }
                                lblMessage.Text = MessageCatalog.Message.LedgerGroupSaved;

                                GenerateTreeview();
                                if (trlLedgerGroup.Nodes.Count > 0)
                                {
                                    trlLedgerGroup.Nodes[0].Selected = true;
                                    trlLedgerGroup_SelectedNodeChanged(sender, e);
                                }
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "LedgerGroupAdd", "javascript:ShowDisplayPopUp()", true);
                            }
                            else
                            {
                                lblMessage.Text = resultArgs.Message;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "LedgerGroupAdd", "javascript:ShowDisplayPopUp()", true);
                            }

                        }
                    }
                    catch (Exception Ex)
                    {
                        lblMessage.Text = Ex.Message;
                    }
                    finally
                    {
                    }

                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearOldValues();
            lblMessage.Text = "";
        }

        protected void trlLedgerGroup_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (trlLedgerGroup.SelectedNode.Depth == 0)
            {
                trlLedgerGroup.CollapseAll();  //Collapse all nodes
                trlLedgerGroup.SelectedNode.Expand(); //Expand the selected node
            }
            NodeSelection = true;
            trlChild.Visible = true;
            DataTable dtLedgerSubGroup = new DataTable();
            string nodeList = string.Empty;
            string nodeLedgerList = string.Empty;
            try
            {
                if (trlLedgerGroup.SelectedNode.ChildNodes.Count > 0)
                {
                    int tmpparent = 0;
                    foreach (TreeNode child in trlLedgerGroup.SelectedNode.ChildNodes)
                    {
                        nodeList += child.Value + ",";
                        nodeLedgerList += child.Value + ",";
                        if (tmpparent == 0)
                        {
                            nodeLedgerList += child.Value + ",";
                            tmpparent++;
                        }
                    }
                }
                else
                {
                    nodeList = trlLedgerGroup.SelectedValue;
                    nodeLedgerList = trlLedgerGroup.SelectedValue;
                }
                if (!string.IsNullOrEmpty(nodeList))
                {
                    trlChild.Nodes.Clear();
                    dtLedgerSubGroup = LoadLedgerGroupList(nodeList.TrimEnd(','));
                    DataRow[] Rows = dtLedgerSubGroup.Select("PARENT_GROUP_ID=" + trlLedgerGroup.SelectedValue + " OR GROUP_ID =" + trlLedgerGroup.SelectedValue);
                    for (int i = 0; i < Rows.Length; i++)
                    {
                        TreeNode root = new TreeNode(dtLedgerSubGroup.Rows[i]["Ledger Sub Group"].ToString(), dtLedgerSubGroup.Rows[i]["GROUP_ID"].ToString());
                        CreateSubCategoryNode(root, dtLedgerSubGroup);
                        trlChild.Nodes.Add(root);
                    }
                    if (trlChild.Nodes.Count > 0)
                    {
                        trlChild.Nodes[0].Selected = true;
                        trlChild_SelectedNodeChanged(sender, e);

                    }
                    else
                    {
                        GroupIDs = nodeList.TrimEnd(',');
                        LoadLedgerList();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void trlChild_SelectedNodeChanged(object sender, EventArgs e)
        {
            GroupIDs = trlChild.SelectedValue;
            LoadLedgerList();
        }

        protected void gvView_PageIndexChanged(object sender, EventArgs e)
        {
            trlChild_SelectedNodeChanged(sender, e);
            //   gvView.DataSource = NewDataSource;
        }

        #endregion

        #region Methods
        private void CreateNode(TreeNode node, DataTable Dt)
        {
            DataRow[] Rows = Dt.Select("PARENT_GROUP_ID=" + node.Value + " AND GROUP_ID <> " + node.Value);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode Childnode = new TreeNode(Rows[i]["Ledger Group"].ToString(), Rows[i]["GROUP_ID"].ToString());
                node.ChildNodes.Add(Childnode);
                CreateNode(Childnode, Dt);
            }
        }

        private void ClearOldValues()
        {
            txtLedgerName.Text = txtCode.Text = string.Empty;
            ddlgroup.SelectedIndex = 0;
            SetControlFocus(ddlgroup);
            UpdationMode = "Add";
            LoadLedgerGroup();
            //LoadLedgerGroupCodes();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LedgerGroupAdd", "javascript:ShowDisplayPopUp()", true);
        }

        private void CreateSubCategoryNode(TreeNode node, DataTable dt)
        {
            DataRow[] Rows = dt.Select("PARENT_GROUP_ID=" + node.Value + " AND GROUP_ID <> " + node.Value);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode Childnode = new TreeNode(Rows[i]["Ledger Sub Group"].ToString(), Rows[i]["GROUP_ID"].ToString());
                node.ChildNodes.Add(Childnode);
                CreateSubCategoryNode(Childnode, dt);
            }
        }

        private int GenerateSortOrderSquence(int GetSortOrder)
        {
            SortOrder = GetSortOrder + 1;
            return SortOrder;
        }

        private int GenerateSortOrder(int GetNatureSortOrder)
        {
            SortOrder = GetNatureSortOrder + 100;
            return SortOrder;
        }
        private void FetchLedgerGroup()
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.GetLedgerGroupSource(DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {

                        DataTable dtLedgerGroup = resultArgs.DataSource.Table;
                        trlLedgerGroup.DataSource = (IHierarchicalEnumerable)dtLedgerGroup;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally { }
        }

        private void BindTreeViewControl()
        {
            try
            {
                DataTable dt = Fetch();

                DataRow[] Rows = dt.Select("group_id=parent_group_id"); // Get all parents nodes
                for (int i = 0; i < Rows.Length; i++)
                {
                    TreeNode root = new TreeNode(Rows[i]["Ledger Group"].ToString(), Rows[i]["GROUP_ID"].ToString());
                    CreateNode(root, dt);
                    trlLedgerGroup.Nodes.Add(root);
                }
                trlLedgerGroup.CollapseAll();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private DataTable Fetch()
        {
            DataTable dtLedgerGroup = new DataTable();
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.GetLedgerGroupSource(DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                        dtLedgerGroup = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally { }
            return dtLedgerGroup;
        }

        private DataTable LoadLedgerGroupList(string GroupIds)
        {
            DataTable dtLedgerGroup = new DataTable();
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    trlChild.DataSource = null;
                    ledgerSystem.GroupIds = GroupIds;
                    resultArgs = ledgerSystem.GetLedgerGroupByIdList(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtLedgerGroup = resultArgs.DataSource.Table;
                        trlChild.ExpandAll();
                    }

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally { }
            return dtLedgerGroup;
        }

        private void LoadLedgerList()
        {
            try
            {
                if (!string.IsNullOrEmpty(GroupIDs) && GroupIDs != "0")
                {
                    using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                    {
                        gvView.DataSource = null;
                        ledgerSystem.GroupIds = GroupIDs;
                        resultArgs = ledgerSystem.GetLedgerList(DataBaseType.HeadOffice);
                        if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            LedgerSource = resultArgs.DataSource.Table.DefaultView;
                            NewDataSource = LedgerSource.ToTable();
                            gvView.DataSource = LedgerSource;
                            gvView.DataBind();
                        }
                        else
                        {
                            gvView.DataSource = resultArgs.DataSource.Table.DefaultView;
                            gvView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally { }
        }

        private void LoadLedgerGroup()
        {
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                resultArgs = ledgerSystem.LoadLedgerGroupSource(DataBaseType.HeadOffice);
                this.Member.ComboSet.BindDataCombo(ddlgroup, resultArgs.DataSource.Table, this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName,
                    this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName, true, CommonMember.SELECT);
                if (UpdationMode == "Add")
                    ddlgroup.SelectedValue = trlLedgerGroup.SelectedValue.ToString();
            }
        }

        //private void LoadLedgerGroupCodes()
        //{
        //    using (LedgerGroupSystem ledgerGroupSystem = new LedgerGroupSystem())
        //    {
        //        resultArgs = ledgerGroupSystem.FecthLedgerGroupCodes(DataBaseType.HeadOffice);
        //        //this.Member.ComboSet.BindDataCombo(ddlUsedCodes, resultArgs.DataSource.Table, this.AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName,
        //        //    this.AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName, false, "");
        //        if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        {
        //            this.Member.ComboSet.BindDataList(lvUsedCodes, resultArgs.DataSource.Table, AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName, AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName);
        //            if (UpdationMode == "Edit" || UpdationMode == "Add")
        //            {
        //                lvUsedCodes.SelectedIndex = 0;
        //                txtCode.Text = Base.MasterBase.CodePredictor(lvUsedCodes.Text, resultArgs.DataSource.Table);
        //            }
        //        }
        //    }
        //}

        private void AssignLedgerGroupDetails()
        {

            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem(LedgerIdPass))
            {
                txtCode.Text = ledgerSystem.Abbrevation;
                txtLedgerName.Text = ledgerSystem.Group;
                ddlgroup.SelectedValue = ledgerSystem.ParentGroupId.ToString();
            }
        }

        private bool ValidateGroupDelete()
        {
            bool IsGroupVaild = true;
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                int AcFlag = ledgerSystem.GetAccessFlag(LedgerIdPass, DataBaseType.HeadOffice);
                if (AcFlag == (int)AccessFlag.Readonly || AcFlag == (int)AccessFlag.Editable)
                {
                    IsGroupVaild = false;
                }
            }
            return IsGroupVaild;
        }

        private bool ValidateGroupDetails()
        {
            bool IsGroudValid = true;

            if (!ValidateGroupLevel())
            {
                this.Message = MessageCatalog.Message.LedgerGroupNextlevel;
                IsGroudValid = false;
                ddlgroup.Focus();
            }
            return IsGroudValid;
        }

        private bool ValidateGroupLevel()
        {
            bool IsGroupLevel = true;
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                resultArgs = ledgerSystem.ValidateGroupId(this.Member.NumberSet.ToInteger(ddlgroup.SelectedValue.ToString()), DataBaseType.HeadOffice);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    if (resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ColumnName].ToString() != resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString())
                    {
                        IsGroupLevel = false;
                    }
                }
            }
            return IsGroupLevel;
        }

        private void ClearValues()
        {
            txtCode.Text = txtLedgerName.Text = string.Empty;
            lblMessage.Text = "";
            ddlgroup.Enabled = true;
        }

        private void GenerateTreeview()
        {
            //Clear all source trlLedgerGroup,trChile, and gridview
            trlLedgerGroup.Nodes.Clear();
            trlChild.Nodes.Clear();
            BindTreeViewControl();
        }

        private void LedgerGroupRights()
        {
            if (this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerGroupAdd, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
            {
                btnAddGroup.Visible = true;
            }
            if (this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerGroupEdit, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
            {
                btnEditGroup.Visible = true;
            }
            if (this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerGroupDelete, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
            {
                btnDeleteGroup.Visible = true;
            }
        }

        #endregion

    }
}