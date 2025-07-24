using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bosco.Model;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.DAO.Schema;

namespace AcMeERP.Module.Inventory.Asset
{
    public partial class AssetClassView : Base.UIBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion
        #region Properties

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

        private string AssetClassIDs
        {
            get
            {
                if (ViewState["AssetClassIDs"] == null)
                    return "0";
                else
                    return ViewState["AssetClassIDs"].ToString();
            }
            set { ViewState["AssetClassIDs"] = value; }
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

        private DataView AssetItemSource
        {
            get;
            set;
        }

        private int AssetClassIdPass
        {
            get
            {
                int AssetClassIdPass = this.Member.NumberSet.ToInteger(trlAssetClass.SelectedValue);
                return AssetClassIdPass;
            }
            set
            {
                this.AssetClassIdPass = 0;
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

        #endregion
        #region Events
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTreeViewControl();
            }
            trlAssetClass.ImageSet = TreeViewImageSet.Arrows;
            trlAssetClassChild.ImageSet = TreeViewImageSet.Arrows;

            if (trlAssetClass.Nodes.Count > 0)
            {
                this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerGroupView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                trlAssetClass.Nodes[0].Selected = true;
                NodeSelection = true;
                trlAssetClass_SelectedNodeChanged(sender, e);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.LoginUser.IsAdminUser)
                {
                    btnAddClass.Visible = btnEditClass.Visible = btnDeleteClass.Visible = false;
                    divLedgerGroup.Visible = false;
                }
                if (trlAssetClass.Nodes.Count > 0)
                {
                    if (trlAssetClass.Nodes[0].Selected)
                    {
                        NodeSelection = true;
                    }
                }

                gvAssetClassView.SettingsPager.PageSizeItemSettings.Visible = false;
                this.PageTitle = MessageCatalog.Message.Asset.AssetClassViewPageTitle;
                this.ShowLoadWaitPopUp(btnAddClass);

            }
            trlAssetClass.ExpandDepth = 1;
            trlAssetClassChild.ExpandAll();

        }
        protected void btnSaveAssetClass_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (ValidateAssetClass())
                {
                    try
                    {
                        using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                        {
                            assetClassSystem.AssetClass = txtClassName.Text.Trim();
                            assetClassSystem.Depreciation = this.Member.NumberSet.ToDouble(txtDepPercent.Text.Trim());
                            assetClassSystem.ParentClassId = this.Member.NumberSet.ToInteger(ddlParentClass.SelectedValue.ToString());
                            assetClassSystem.Method = this.Member.NumberSet.ToInteger(ddlDepreciation.SelectedValue.ToString());
                            assetClassSystem.AssetClassId = (UpdationMode == "Add") ? (int)AddNewRow.NewRow : AssetClassIdPass;
                            resultArgs = assetClassSystem.SaveClassDetails();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                if (UpdationMode == "Add")
                                {
                                    ClearOldValues();
                                }
                                lblMessage.Text = MessageCatalog.Message.Asset.AssetClassSaved;

                                GenerateTreeview();
                                if (trlAssetClass.Nodes.Count > 0)
                                {
                                    trlAssetClass.Nodes[0].Selected = true;
                                    trlAssetClass_SelectedNodeChanged(sender, e);
                                }
                                ShowAssetClasspopup(true);
                            }
                            else
                            {
                                lblMessage.Text = resultArgs.Message;
                                ShowAssetClasspopup(true);
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

        // Popup Window Events (Asset Class Add, Edit and Delete)
        protected void btnAddClass_Click(object sender, EventArgs e)
        {
            if (NodeSelection == true)
            {
                btnNew.Visible = true;
                UpdationMode = "Add";
                ClearValues();
                ClearOldValues();
                LoadAssetClass();
                LoadDepreciationMethods();
                imagePopupTitle.InnerText = MessageCatalog.Message.Asset.AssetClassAdd;
                SetControlFocus(ddlParentClass);
                ShowAssetClasspopup(true);
                
            }
            else
            {
                this.Message = MessageCatalog.Message.Asset.SelectParentClass;
            }
        }

        protected void btnEditClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (NodeSelection == true)
                {
                    UpdationMode = "Edit";
                    btnNew.Visible = false;
                    LoadAssetClass();
                    LoadDepreciationMethods();
                    ddlParentClass.Enabled = false;
                    lblMessage.Text = "";
                    AssignAssetClassDetails();
                    imagePopupTitle.InnerText = MessageCatalog.Message.Asset.AssetClassEdit;
                    SetControlFocus(ddlParentClass);
                    ShowAssetClasspopup(false);
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
        protected void btnDeleteClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (NodeSelection == true)
                {
                    if (trlAssetClass.SelectedNode.ChildNodes.Count == 0)
                    {
                        using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                        {
                            assetClassSystem.AssetClassId =AssetClassIdPass;
                            resultArgs = assetClassSystem.DeleteClassDetails();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                GenerateTreeview();
                                if (trlAssetClass.Nodes.Count > 0)
                                {
                                    trlAssetClass.Nodes[0].Selected = true;
                                    trlAssetClass_SelectedNodeChanged(sender, e);
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
            }
            catch (Exception Ex)
            {
                this.Message = Ex.Message;
            }
            finally { }
        }
        protected void trlAssetClass_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (trlAssetClass.SelectedNode.Depth == 0)
            {
                trlAssetClass.CollapseAll();  //Collapse all nodes
                trlAssetClass.SelectedNode.Expand(); //Expand the selected node
            }
            NodeSelection = true;
            trlAssetClassChild.Visible = true;
            DataTable dtAssetClass = new DataTable();
            string nodeList = string.Empty;
            string nodeAssetList = string.Empty;
            try
            {
                if (trlAssetClass.SelectedNode.ChildNodes.Count > 0)
                {
                    int tmpparent = 0;
                    foreach (TreeNode child in trlAssetClass.SelectedNode.ChildNodes)
                    {
                        nodeList += child.Value + ",";
                        nodeAssetList += child.Value + ",";
                        if (tmpparent == 0)
                        {
                            nodeAssetList += child.Value + ",";
                            tmpparent++;
                        }
                    }
                }
                else
                {
                    nodeList = trlAssetClass.SelectedValue;
                    nodeAssetList = trlAssetClass.SelectedValue;
                }
                if (!string.IsNullOrEmpty(nodeList))
                {
                    trlAssetClassChild.Nodes.Clear();
                    dtAssetClass = LoadAssetSubClassList(nodeList.TrimEnd(','));
                    DataRow[] Rows = dtAssetClass.Select("PARENT_CLASS_ID=" + trlAssetClass.SelectedValue + " OR ASSET_CLASS_ID =" + trlAssetClass.SelectedValue);
                    for (int i = 0; i < Rows.Length; i++)
                    {
                        TreeNode root = new TreeNode(dtAssetClass.Rows[i]["Asset Sub Class"].ToString(), dtAssetClass.Rows[i]["ASSET_CLASS_ID"].ToString());
                        CreateSubCategoryNode(root, dtAssetClass);
                        trlAssetClassChild.Nodes.Add(root);
                    }
                    if (trlAssetClassChild.Nodes.Count > 0)
                    {
                        trlAssetClassChild.Nodes[0].Selected = true;
                        trlAssetClassChild_SelectedNodeChanged(sender, e);

                    }
                    else
                    {
                        AssetClassIDs = nodeList.TrimEnd(',');
                        LoadAssetItemList();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void trlAssetClassChild_SelectedNodeChanged(object sender, EventArgs e)
        {
            AssetClassIDs = trlAssetClassChild.SelectedValue + "," + trlAssetClass.SelectedValue;
            LoadAssetItemList();
        }
        protected void gvAssetClassView_PageIndexChanged(object sender, EventArgs e)
        {
            trlAssetClassChild_SelectedNodeChanged(sender, e);
        }

        #endregion
        #region Methods

        private void BindTreeViewControl()
        {
            try
            {
                DataTable dtAssetClass = FetchAssetClass();
                DataRow[] Rows = dtAssetClass.Select("PARENT_CLASS_ID=1"); // Get all parents Class Nodes
               // DataRow[] Rows = dtAssetClass.Select(); // Get all parents Class Nodes
                for (int i = 0; i < Rows.Length; i++)
                {
                    TreeNode root = new TreeNode(Rows[i][this.appSchema.AppSchema.AssetClass.ASSET_CLASSColumn.ColumnName].ToString(), Rows[i][this.appSchema.AppSchema.AssetClass.ASSET_CLASS_IDColumn.ColumnName].ToString());
                    CreateNode(root, dtAssetClass);
                    trlAssetClass.Nodes.Add(root);
                }
                trlAssetClass.CollapseAll();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private DataTable FetchAssetClass()
        {
            DataTable dtAssetClass = new DataTable();
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchClassDetails();
                    if (resultArgs.Success)
                    {
                        dtAssetClass = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return dtAssetClass;
        }
        private void CreateNode(TreeNode node, DataTable Dt)
        {
            DataRow[] Rows = Dt.Select("PARENT_CLASS_ID=" + node.Value + " AND ASSET_CLASS_ID <>" + node.Value);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode Childnode = new TreeNode(Rows[i][this.appSchema.AppSchema.AssetClass.ASSET_CLASSColumn.ColumnName].ToString(), Rows[i][this.appSchema.AppSchema.AssetClass.ASSET_CLASS_IDColumn.ColumnName].ToString());
                node.ChildNodes.Add(Childnode);
                CreateNode(Childnode, Dt);
            }
        }

        private void CreateSubCategoryNode(TreeNode node, DataTable dt)
        {
            DataRow[] Rows = dt.Select("PARENT_CLASS_ID=" + node.Value + " AND ASSET_CLASS_ID <> " + node.Value);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode Childnode = new TreeNode(Rows[i]["Asset Sub Class"].ToString(), Rows[i][this.appSchema.AppSchema.AssetClass.ASSET_CLASS_IDColumn.ColumnName].ToString());
                node.ChildNodes.Add(Childnode);
                CreateSubCategoryNode(Childnode, dt);
            }
        }
        private void LoadAssetClass()
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchParentClassName();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlParentClass, resultArgs.DataSource.Table, assetClassSystem.AppSchema.AssetClass.ASSET_CLASSColumn.ColumnName, assetClassSystem.AppSchema.AssetClass.ASSET_CLASS_IDColumn.ColumnName);
                        if (UpdationMode == "Add")
                            ddlParentClass.SelectedValue = ddlParentClass.SelectedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        public void LoadDepreciationMethods()
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchAssetDepreciationMethods();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlDepreciation, resultArgs.DataSource.Table, assetClassSystem.AppSchema.AssetClass.DEP_METHODColumn.ColumnName, assetClassSystem.AppSchema.AssetClass.METHOD_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void AssignAssetClassDetails()
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem(AssetClassIdPass))
                {
                    ddlParentClass.SelectedValue = assetClassSystem.ParentClassId.ToString();
                    ddlDepreciation.SelectedValue = assetClassSystem.Method.ToString();
                    txtDepPercent.Text = assetClassSystem.Depreciation.ToString();
                    txtClassName.Text = assetClassSystem.AssetClass.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        private void ClearValues()
        {
            //Clear all source trlAssetClass,trChild, and gridview
            trlAssetClass.Nodes.Clear();
            trlAssetClassChild.Nodes.Clear();
            BindTreeViewControl();
        }

        private void GenerateTreeview()
        {
            //Clear all source trlAssetClass,trChild, and gridview
            trlAssetClass.Nodes.Clear();
            trlAssetClassChild.Nodes.Clear();
            BindTreeViewControl();
        }


        private void ClearOldValues()
        {
            txtClassName.Text = txtDepPercent.Text = string.Empty;
            ddlParentClass.SelectedIndex = 0;
            ddlDepreciation.SelectedIndex = 0;
            SetControlFocus(ddlParentClass);
            lblMessage.Text = "";
            UpdationMode = "Add";
            LoadAssetClass();
            ShowAssetClasspopup(true);
        }

        private void ShowAssetClasspopup(bool isAddMode)
        {
            if (isAddMode)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AssetClassAdd", "javascript:ShowDisplayPopUp()", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AssetClassEdit", "javascript:ShowDisplayPopUp()", true);
            }
        }

        private DataTable LoadAssetSubClassList(string aClassIDs)
        {
            DataTable dtAssetclass = new DataTable();
            try
            {
                using (AssetClassSystem assetclassSystem = new AssetClassSystem())
                {
                    trlAssetClassChild.DataSource = null;
                    assetclassSystem.AssetClassIds = aClassIDs;
                    resultArgs = assetclassSystem.FetchAssetSubClassByAssetParentId();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAssetclass = resultArgs.DataSource.Table;
                        trlAssetClassChild.ExpandAll();
                    }

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally { }
            return dtAssetclass;
        }

        private void LoadAssetItemList()
        {
            try
            {
                if (!string.IsNullOrEmpty(AssetClassIDs) && AssetClassIDs != "0")
                {
                    using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                    {
                        gvAssetClassView.DataSource = null;
                        assetClassSystem.AssetClassIds =AssetClassIDs.ToString();
                        resultArgs = assetClassSystem.FetchSelectedClassDetails();
                        if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            AssetItemSource = resultArgs.DataSource.Table.DefaultView;
                            NewDataSource = AssetItemSource.ToTable();
                            gvAssetClassView.DataSource = AssetItemSource;
                            gvAssetClassView.DataBind();
                        }
                        else
                        {
                            gvAssetClassView.DataSource = resultArgs.DataSource.Table.DefaultView;
                            gvAssetClassView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }
        private bool ValidateAssetClass()
        {
            bool IsAssetClassValid = true;

            if ((ddlParentClass.SelectedValue==null))
            {
                this.Message = MessageCatalog.Message.Asset.ParentClassisrequired;
                IsAssetClassValid = false;
                ddlParentClass.Focus();
            }
            if (string.IsNullOrEmpty(txtClassName.Text))
            {
                this.Message = MessageCatalog.Message.Asset.ClassNameisrequired;
                IsAssetClassValid = false;
                txtClassName.Focus();
            }
            return IsAssetClassValid;
        }
        #endregion

    }
}