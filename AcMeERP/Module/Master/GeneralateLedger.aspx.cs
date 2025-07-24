using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.DAO.Data;
using System.Data;
using DevExpress.XtraEditors.Filtering;

namespace AcMeERP.Module.Master
{
    public partial class GeneralateLedger : Base.UIBase
    {
        ResultArgs resultArgs = null;

        #region Decleration

        private string LedgerIDs
        {
            get
            {
                if (ViewState["LedgerIDs"] == null)
                    return "0";
                else
                    return (string)ViewState["LedgerIDs"];
            }
            set { ViewState["LedgerIDs"] = value; }
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

        private DataTable NodesData
        {
            get
            {
                if (ViewState["NodesData"] == null)
                    return null;
                else
                    return (DataTable)ViewState["NodesData"];
            }
            set
            {
                ViewState["NodesData"] = value;
            }
        }

        private DataView LedgerSource
        {
            get;
            set;
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

        private int LedgerId
        {
            get
            {
                int LedgerI = this.Member.NumberSet.ToInteger(trlGeneralateLedger.SelectedValue);
                return LedgerI;
            }
            set
            {
                this.LedgerId = 0;
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTreeViewControl();
            }
            trlGeneralateLedger.ImageSet = TreeViewImageSet.Arrows;
            //trlChild.ImageSet = TreeViewImageSet.Arrows;
            //LedgerGroupRights();//CheckUserRights
            if (trlGeneralateLedger.Nodes.Count > 0)
            {
                //this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerGroupView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                trlGeneralateLedger.Nodes[0].Selected = true;
                NodeSelection = true;
                trlGeneralateLedger_SelectedNodeChanged(sender, e);
            }

            btnAddLedger.Enabled = btnEditLedger.Enabled = btnDeleteLedger.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.LoginUser.IsAdminUser)
                {
                    btnAddLedger.Visible = btnEditLedger.Visible = btnDeleteLedger.Visible = false;
                    divLedger.Visible = false;
                }
                if (trlGeneralateLedger.Nodes.Count > 0)
                {
                    if (trlGeneralateLedger.Nodes[0].Selected)
                    {
                        NodeSelection = true;
                    }
                }

                btnAddLedger.Enabled = btnEditLedger.Enabled = btnDeleteLedger.Enabled = false;
                gvView.SettingsPager.PageSizeItemSettings.Visible = false;
                this.PageTitle = MessageCatalog.Message.GeneralateLedger.GeneralateLedgerViewPageTitle;
                this.ShowLoadWaitPopUp(btnAddLedger);

            }
            trlGeneralateLedger.ExpandDepth = 1;
            //trlChild.ExpandAll();
        }

        protected void btnAddLedger_Click(object sender, EventArgs e)
        {
            //if (NodeSelection == true)
            //{


            btnNew.Visible = true;
            UpdationMode = "Add";
            ClearValues();
            LoadLedgerParent();
            //LoadLedgerGroupCodes();
            imagePopupTitle.InnerText = MessageCatalog.Message.GeneralateLedgerAdd;
            SetControlFocus(ddlParent);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralateLedgerAdd", "javascript:ShowDisplayPopUp()", true);
            //}
            //else
            //{
            //    //this.Message = MessageCatalog.Message.SelectLedgerType;
            //}
        }

        protected void btnEditLedger_Click(object sender, EventArgs e)
        {
            try
            {
                //if (NodeSelection == true)
                //{
                UpdationMode = "Edit";
                btnNew.Visible = false;
                LoadLedgerParent();
                //LoadLedgerGroupCodes();
                ddlParent.Enabled = false;
                lblMessage.Text = "";
                AssignGenerateLedgerDetails();
                imagePopupTitle.InnerText = MessageCatalog.Message.GeneralateLedgerEdit;
                SetControlFocus(ddlParent);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralateLedgerEdit", "javascript:ShowDisplayPopUp()", true);
                //}
                //else
                //{
                //    //this.Message = MessageCatalog.Message.SelectLedgerType;
                //}
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void btnDeleteLedger_Click(object sender, EventArgs e)
        {
            try
            {
                //if (NodeSelection == true)
                //{
                //if ((ValidateGroupDelete()))
                //{
                if (trlGeneralateLedger.SelectedNode.ChildNodes.Count == 0)
                {
                    //if (LedgerHasMapped())
                    //{
                    using (GeneralateSystem GeneralateSystem = new GeneralateSystem())
                    {
                        resultArgs = GeneralateSystem.DeleteLedger(LedgerId, DataBaseType.HeadOffice);
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            GenerateTreeview();
                            if (trlGeneralateLedger.Nodes.Count > 0)
                            {
                                trlGeneralateLedger.Nodes[0].Selected = true;
                                trlGeneralateLedger_SelectedNodeChanged(sender, e);
                            }
                            this.Message = MessageCatalog.Message.GeneralateLedgerDeleteSccessful;
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.GeneralateLedgerNotDelete;
                        }
                    }
                    //}
                }
                else
                {
                    this.Message = MessageCatalog.Message.GeneralateLedgerNotDelete;
                }
                //}
                //else
                //{
                //    this.Message = MessageCatalog.Message.LedgerGroupNatureNotDelete;
                //}
                //}
            }
            catch (Exception Ex)
            {
                this.Message = Ex.Message;
            }
            finally { }
        }

        protected void trlGeneralateLedger_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (trlGeneralateLedger.SelectedNode.Depth == 0)
            {
                trlGeneralateLedger.CollapseAll();  //Collapse all nodes
                trlGeneralateLedger.SelectedNode.Expand(); //Expand the selected node
            }
            NodeSelection = true;
            //trlChild.Visible = true;
            DataTable dtLedgerSubGroup = new DataTable();
            string nodeList = string.Empty;
            string nodeLedgerList = string.Empty;
            try
            {
                if (trlGeneralateLedger.SelectedNode.ChildNodes.Count > 0)
                {
                    int tmpparent = 0;
                    foreach (TreeNode child in trlGeneralateLedger.SelectedNode.ChildNodes)
                    {
                        nodeList += child.Parent.Value + ",";
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
                    nodeList = trlGeneralateLedger.SelectedValue;
                    nodeLedgerList = trlGeneralateLedger.SelectedValue;
                }
                //if (!string.IsNullOrEmpty(nodeList))
                //{
                //    trlChild.Nodes.Clear();
                //    dtLedgerSubGroup = LoadGenerateLedgerList(nodeList.TrimEnd(','));
                //    DataRow[] Rows = dtLedgerSubGroup.Select("CON_PARENT_LEDGER_ID=" + trlGeneralateLedger.SelectedValue + " OR CON_LEDGER_ID =" + trlGeneralateLedger.SelectedValue);
                //    for (int i = 0; i < Rows.Length; i++)
                //    {
                //        TreeNode root = new TreeNode(dtLedgerSubGroup.Rows[i]["Ledger Sub Group"].ToString(), dtLedgerSubGroup.Rows[i]["CON_LEDGER_ID"].ToString());
                //        CreateSubCategoryNode(root, dtLedgerSubGroup);
                //        trlChild.Nodes.Add(root);
                //    }
                //    if (trlChild.Nodes.Count > 0)
                //    {
                //        trlChild.Nodes[0].Selected = true;
                //        trlChild_SelectedNodeChanged(sender, e);

                //    }
                //else
                //{
                LedgerIDs = nodeList.TrimEnd(',');
                LoadLedgerList();

                lblGeneralateLedgerGroup.Text = trlGeneralateLedger.SelectedNode.Text.ToString() + " - " + "Province Ledgers Mapped";

                //}
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void trlChild_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int MainParentId = 0;
                try
                {
                    using (GeneralateSystem generalateSystem = new GeneralateSystem())
                    {
                        generalateSystem.GeneralateLedgerCode = txtCode.Text.Trim().ToUpper();
                        generalateSystem.GeneralateLedgerName = txtLedgerName.Text.Trim();
                        generalateSystem.GeneralateParentLedgerId = this.Member.NumberSet.ToInteger(ddlParent.SelectedValue.ToString());
                        DataTable dtNode = NodesData;

                        if (dtNode != null)
                        {
                            DataView dvGroup = dtNode.AsDataView();
                            dvGroup.RowFilter = "CON_LEDGER_ID = " + generalateSystem.GeneralateParentLedgerId;
                            if (dvGroup != null)
                            {
                                foreach (DataRow drvItem in dvGroup.ToTable().Rows)
                                {
                                    MainParentId = generalateSystem.MainParent = this.Member.NumberSet.ToInteger(drvItem["CON_MAIN_PARENT_ID"].ToString());
                                }
                            }
                        }
                        generalateSystem.MainParent = MainParentId; //this.Member.NumberSet.ToInteger(ddlParent.SelectedValue.ToString());
                        generalateSystem.GeneralateLedgerId = (UpdationMode == "Add") ? (int)AddNewRow.NewRow : LedgerId;

                        resultArgs = generalateSystem.SaveGeneralateLedger(DataBaseType.HeadOffice);
                    }
                    if (resultArgs.Success)
                    {
                        if (this.Member.NumberSet.ToInteger(ddlParent.SelectedValue.ToString()) == 0)
                        {
                            using (GeneralateSystem generalate = new GeneralateSystem())
                            {
                                generalate.GeneralateLedgerCode = txtCode.Text.Trim().ToUpper();
                                generalate.GeneralateLedgerName = txtLedgerName.Text.Trim();
                                generalate.GeneralateParentLedgerId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                generalate.GeneralateLedgerId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                generalate.MainParent = MainParentId > 0 ? MainParentId : this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                resultArgs = generalate.SaveGeneralateLedger(DataBaseType.HeadOffice);
                            }
                        }
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            if (UpdationMode == "Add")
                            {
                                ClearOldValues();
                            }
                            lblMessage.Text = MessageCatalog.Message.GeneralateLedgerSaved;

                            GenerateTreeview();
                            if (trlGeneralateLedger.Nodes.Count > 0)
                            {
                                trlGeneralateLedger.Nodes[0].Selected = true;
                                trlGeneralateLedger_SelectedNodeChanged(sender, e);
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "GenerateLedgerAdd", "javascript:ShowDisplayPopUp()", true);
                        }
                        else
                        {
                            lblMessage.Text = resultArgs.Message;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "GenerateLedgerAdd", "javascript:ShowDisplayPopUp()", true);
                        }
                    }
                    else
                    {
                        lblMessage.Text = resultArgs.Message;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "GenerateLedgerAdd", "javascript:ShowDisplayPopUp()", true);
                    }
                }
                catch (Exception Ex)
                {
                    lblMessage.Text = Ex.Message;
                    //BindTreeViewControl();
                }
                finally
                {
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearOldValues();
            lblMessage.Text = "";
        }


        private void ClearValues()
        {
            txtCode.Text = txtLedgerName.Text = string.Empty;
            lblMessage.Text = "";
            ddlParent.Enabled = true;
        }

        private void ClearOldValues()
        {
            txtLedgerName.Text = txtCode.Text = string.Empty;
            ddlParent.SelectedIndex = 0;
            SetControlFocus(ddlParent);
            UpdationMode = "Add";
            LoadLedgerParent();
            //LoadLedgerGroupCodes();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralateLedgerAdd", "javascript:ShowDisplayPopUp()", true);
        }

        private void LoadLedgerParent()
        {
            using (GeneralateSystem generalateSystem = new GeneralateSystem())
            {
                resultArgs = generalateSystem.FetchAllGeneralateParentLedgers(DataBaseType.HeadOffice);
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    NodesData = resultArgs.DataSource.Table;
                    this.Member.ComboSet.BindDataCombo(ddlParent, resultArgs.DataSource.Table, this.AppSchema.CongregationLedger.CON_LEDGER_NAMEColumn.ColumnName,
                        this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn.ColumnName, true, CommonMember.SELECT);
                }
                //if (UpdationMode == "Add")
                //    if (ddlParent.Items.Count >= Member.NumberSet.ToInteger(trlGeneralateLedger.SelectedValue.ToString()))
                //        ddlParent.SelectedValue = trlGeneralateLedger.SelectedValue.ToString();
                else
                {
                    NodesData = resultArgs.DataSource.Table;
                    this.Member.ComboSet.BindDataCombo(ddlParent, resultArgs.DataSource.Table, this.AppSchema.CongregationLedger.CON_LEDGER_NAMEColumn.ColumnName,
                        this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn.ColumnName, true, CommonMember.SELECT);
                    ddlParent.SelectedIndex = 0;
                }
            }
        }

        private void AssignGenerateLedgerDetails()
        {
            using (GeneralateSystem generalateSystem = new GeneralateSystem(LedgerId))
            {
                txtCode.Text = generalateSystem.GeneralateLedgerCode;
                txtLedgerName.Text = generalateSystem.GeneralateLedgerName;
                ddlParent.SelectedValue = generalateSystem.GeneralateParentLedgerId.ToString();
            }
        }

        private void GenerateTreeview()
        {
            //Clear all source trlGeneralateLedger,trChile, and gridview
            trlGeneralateLedger.Nodes.Clear();
            //trlChild.Nodes.Clear();
            BindTreeViewControl();
        }

        private void BindTreeViewControl()
        {
            try
            {
                using (GeneralateSystem generalatesystem = new GeneralateSystem())
                {
                    resultArgs = generalatesystem.FetchAllGeneralateLedgers(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //NodesData = resultArgs.DataSource.Table;
                        DataTable dt = resultArgs.DataSource.Table;


                        // DataRow[] Rows = dt.Select("CON_PARENT_LEDGER_ID=CON_LEDGER_ID", "CON_LEDGER_CODE"); // Get all parents nodes

                        DataRow[] Rows = dt.Select("CON_PARENT_LEDGER_ID=CON_LEDGER_ID"); // Get all parents nodes
                        for (int i = 0; i < Rows.Length; i++)
                        {
                            TreeNode root = new TreeNode(Rows[i]["CON_LEDGER_NAME"].ToString(), Rows[i]["CON_LEDGER_ID"].ToString());
                            CreateNode(root, dt);
                            trlGeneralateLedger.Nodes.Add(root);
                        }
                        trlGeneralateLedger.CollapseAll();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void LoadLedgerList()
        {
            try
            {
                if (!string.IsNullOrEmpty(LedgerIDs) && LedgerIDs != "0")
                {
                    using (GeneralateSystem GeneralateSystem = new GeneralateSystem())
                    {
                        gvView.DataSource = null;
                        GeneralateSystem.GeneralateLedgerIds = LedgerIDs;
                        resultArgs = GeneralateSystem.GetLedgerList(DataBaseType.HeadOffice);
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

        private void CreateNode(TreeNode node, DataTable Dt)
        {
            DataRow[] Rows = Dt.Select("CON_PARENT_LEDGER_ID=" + node.Value + " AND CON_LEDGER_ID <> " + node.Value);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode Childnode = new TreeNode(Rows[i]["CON_LEDGER_NAME"].ToString(), Rows[i]["CON_LEDGER_ID"].ToString());
                node.ChildNodes.Add(Childnode);
                CreateNode(Childnode, Dt);
            }
        }

        protected void gvView_PageIndexChanged(object sender, EventArgs e)
        {
            trlGeneralateLedger_SelectedNodeChanged(sender, e);
        }

        //public bool LedgerHasMapped()
        //{
        //    bool HasMapped = true;
        //    using (GeneralateSystem generalatesystem = new GeneralateSystem())
        //    {
        //        resultArgs = generalatesystem.FetchmappedLedgers(DataBaseType.HeadOffice);
        //        if (resultArgs.Success)
        //        {
        //            if (resultArgs.RowsAffected > 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmation", "javascript:ShowConfirmationMessage()", true);
        //            }
        //        }
        //    }
        //    HasMapped = hdnValue.Value != "" ? Convert.ToBoolean(hdnValue.Value) : false;
        //    hdnValue.Value = null;
        //    return HasMapped;
        //}
    }
}