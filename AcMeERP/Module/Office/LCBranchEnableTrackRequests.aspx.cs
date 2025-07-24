using System;
using System.Collections.Generic;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.WebControls;

namespace AcMeERP.Module.Office
{
    public partial class LCBranchEnableTrackRequests : Base.UIBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        private static object objLock = new object();
        #endregion

        #region Properties

        private DataTable Branch
        {
            get
            {
                DataTable dtRtn = null;
                if (ViewState["Branch"] != null)
                {
                    dtRtn = (DataTable)ViewState["Branch"];
                }
                return dtRtn;
            }
            set
            {
                ViewState["Branch"] = value;
            }
        }


        private DataTable LCBranchEnableRequestsData
        {
            get
            {
                DataTable dtRtn = null;
                if (ViewState["LCBranchEnableRequests"] != null)
                {
                    dtRtn= (DataTable)ViewState["LCBranchEnableRequests"];
                }
                return dtRtn;
            }
            set
            {
                ViewState["LCBranchEnableRequests"] = value;
            }
        }

        private DataTable LCBranchGridBindData
        {
            get
            {
                DataTable dtRtn = null;
                if (ViewState["LCBranchGridBindData"] != null)
                {
                    dtRtn = (DataTable)ViewState["LCBranchGridBindData"];
                }
                return dtRtn;
            }
            set
            {
                ViewState["LCBranchGridBindData"] = value;
            }
        }

        private int BranchId
        {
            get
            {
                Int32 branchid = 0;
                if (ViewState["BranchId"] != null)
                {
                    branchid = this.Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
                }
                return branchid;
            }
            set
            {
                ViewState["BranchId"] = value;

                HeadOfficeCode = string.Empty;
                BranchOfficeCode = string.Empty;
                //Assign selected branch details 
                if (Branch != null)
                {
                    DataTable dtBranches = Branch.DefaultView.ToTable();
                    dtBranches.DefaultView.RowFilter = this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName + " = " + BranchId;
                    if (dtBranches.DefaultView.Count > 0)
                    {
                        HeadOfficeCode = dtBranches.DefaultView[0][this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                        BranchOfficeCode = dtBranches.DefaultView[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                    }
                }
            }
        }

        private string HeadOfficeCode
        {
            get
            {
                string rtn = string.Empty;
                if (ViewState["HeadOfficeCode"] != null)
                {
                    rtn = ViewState["HeadOfficeCode"].ToString();
                }

                return rtn;
            }
            set
            {
                ViewState["HeadOfficeCode"] = value;
            }
        }

        private string BranchOfficeCode
        {
            get
            {
                string rtn = string.Empty;
                if (ViewState["BranchOfficeCode"] != null)
                {
                    rtn = ViewState["BranchOfficeCode"].ToString();
                }

                return rtn;
            }
            set
            {
                ViewState["BranchOfficeCode"] = value;
            }
        }

        private string hcode = string.Empty;
        private string bcode = string.Empty;
        private string location = string.Empty;
        private string ipaddress = string.Empty;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.LCEnableTrackModules.LCEnableTrackModulesPageTitle;
                //Tmep: user branch creation user rights later we will have rights for enable receitps module
                this.CheckUserRights(RightsModule.Data, RightsActivity.BranchOfficeAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadBranch();
                
                LoadLCBranchClientEnableModuleRequests();
                //this.CheckUserRights(RightsModule.Tools, RightsActivity.MapProjectToBranch, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                
                this.ShowLoadWaitPopUp(btnSaveLCBranchRequest);
            }
        }

        protected void raReceiptModuleStatus_Init(object sender, EventArgs e)
        {
            if (sender != null)
            {
                GridViewDataItemTemplateContainer radioBtnLstcontainer = ((ASPxRadioButtonList)sender).NamingContainer as GridViewDataItemTemplateContainer;
                if (radioBtnLstcontainer != null)
                {
                    //GridViewDataItemTemplateContainer container = radioBtnLstcontainer.NamingContainer as GridViewDataItemTemplateContainer;
                    //radioBtnLstcontainer.ClientSideEvents.SelectedIndexChanged = String.Format("function (s, e) {{ cb.PerformCallback('{0}|' + s.SelectedIndexChanged()); }}", container.KeyValue);
                    //((ASPxRadioButtonList)sender).ClientSideEvents.SelectedIndexChanged = "function (s,e) {  alert(s.SelectedIndex.toString()); }";

                    var index = radioBtnLstcontainer.VisibleIndex;
                    var value = gvLCBranchEnableRequests.GetRowValues(radioBtnLstcontainer.VisibleIndex, this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName);
                    ((ASPxRadioButtonList)sender).SelectedIndex = Member.NumberSet.ToInteger(value.ToString());
                }
            }
        }
                

        protected void gvProject_Load(Object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    UpdateViewStateProjects();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally
            {
                
            }
        }

        protected void btnSaveLCBranchRequest_Click(object sender, EventArgs e)
        {
            SaveLcBranchRequests();
        }
              
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
            LoadLCBranchClientEnableModuleRequests();
            gvLCBranchEnableRequests.DataSource = null;

            if (LCBranchEnableRequestsData != null && LCBranchEnableRequestsData.Rows.Count > 0)
            {
                LCBranchGridBindData = LCBranchEnableRequestsData.DefaultView.ToTable();
                if ((!string.IsNullOrEmpty(HeadOfficeCode)) && (!string.IsNullOrEmpty(BranchOfficeCode)))
                {
                    //LCBranchGridBindData.DefaultView.RowFilter = AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName + " = '" + HeadOfficeCode + "' AND " +
                    //                                        AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName + " = '" + BranchOfficeCode + "'";

                    //On 30/01/2024, Show only requested and approved (don't show disabled)
                    LCBranchGridBindData.DefaultView.RowFilter = AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName + " = '" + HeadOfficeCode + "' AND " +
                                                            AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName + " = '" + BranchOfficeCode + "' AND  " +
                                                            AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName + " IN (1, 2)";


                    LCBranchGridBindData.DefaultView.Sort = AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName + "," +
                                                        AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName + " DESC";

                    LCBranchGridBindData = LCBranchGridBindData.DefaultView.ToTable();
                }
            }

            gvLCBranchEnableRequests.DataSource = LCBranchGridBindData;
            gvLCBranchEnableRequests.DataBind();
        }

        protected void gvProject_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            try
            {
                //To disable checkbox for the projects which have vouchers
                if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                {
                    string value1 = Convert.ToString(gvLCBranchEnableRequests.GetRowValues(e.VisibleIndex, "MAPPED_PROJECT"));
                    if (!string.IsNullOrEmpty(value1))
                    {
                        e.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        #endregion

        #region Methods

        private void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranch(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        Branch = resultArgs.DataSource.Table;
                        this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, "CODE", this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                        if (this.Member.NumberSet.ToInteger(Session[base.DefaultBranchId].ToString()) != 0)
                        {
                            //cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                            cmbBranch.SelectedIndex = 0;
                        }
                        BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                        if (Request.QueryString.Count > 0)
                        {
                            if (this.Member.NumberSet.ToInteger(Request.QueryString["BranchId"].ToString()) > 0)
                            {
                                BranchId = this.Member.NumberSet.ToInteger(Request.QueryString["BranchId"].ToString());
                                cmbBranch.SelectedIndex = cmbBranch.Items.IndexOfValue((Request.QueryString["BranchId"].ToString() as object));
                            }

                        }
                    }
                    else
                    {
                        BranchId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
        }

        private void SaveLcBranchRequests()
        {
            try
            {
                lock (objLock)
                {
                    using (LicenseSystem licensesystem = new LicenseSystem())
                    {
                        resultArgs = licensesystem.UpdateLCBranchModuleStatus(LCBranchGridBindData, BranchOfficeCode);
                        if (resultArgs.Success)
                        {
                            LoadLCBranchClientEnableModuleRequests();
                            this.Message = MessageCatalog.Message.LCEnableTrackModules.LCEnableTrackModulesUpdateConformation;
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.LCEnableTrackModules.LCEnableTrackModulesUpdateFailedConformation + "<br> (" + resultArgs.Message + ")";
                            LoadLCBranchClientEnableModuleRequests();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        public void LoadLCBranchClientEnableModuleRequests()
        {
            try
            {
                using (LicenseSystem licensesystem = new LicenseSystem())
                {
                    resultArgs = licensesystem.FetchLCBranchClientEnableModuleRequests();
                    if (resultArgs.Success && resultArgs.DataSource.Table!=null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable LcBrancList = resultArgs.DataSource.Table;
                        LCBranchEnableRequestsData = LcBrancList.DefaultView.ToTable();
                    }
                }

                gvLCBranchEnableRequests.DataSource = null;
                if (LCBranchEnableRequestsData != null && LCBranchEnableRequestsData.Rows.Count > 0)
                {
                    LCBranchGridBindData = LCBranchEnableRequestsData.DefaultView.ToTable();
                    if ((!string.IsNullOrEmpty(HeadOfficeCode)) && (!string.IsNullOrEmpty(BranchOfficeCode)))
                    {
                        //On 30/01/2024, Show only requested and approved (don't show disabled)
                        LCBranchGridBindData.DefaultView.RowFilter = AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName + " = '" + HeadOfficeCode + "' AND " +
                                                                AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName + " = '" + BranchOfficeCode + "' AND  " +
                                                                AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName + " IN (1, 2)";

                        LCBranchGridBindData.DefaultView.Sort = AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName + "," +
                                                            AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName + " DESC";

                        LCBranchGridBindData = LCBranchGridBindData.DefaultView.ToTable();
                    }
                }

                gvLCBranchEnableRequests.DataSource = null;
                gvLCBranchEnableRequests.DataSource = LCBranchGridBindData;
                gvLCBranchEnableRequests.DataBind();
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
        }

      

     
        private void UpdateViewStateProjects()
        {
            if (gvLCBranchEnableRequests.VisibleRowCount > 0)
            {
                for (int i = 0; i < gvLCBranchEnableRequests.VisibleRowCount; i++)
                {
                    ASPxRadioButtonList radReceiptModuleStatus = ((ASPxRadioButtonList)gvLCBranchEnableRequests.FindRowCellTemplateControl(i, gvLCBranchEnableRequests.Columns["colReceiptModuleAction"] as GridViewDataColumn, "raReceiptModuleStatus"));
                    if (radReceiptModuleStatus != null)
                    {
                        if (LCBranchGridBindData != null && LCBranchGridBindData.Rows.Count > 0)
                        {
                            Int32 actionLCBranchRequestStatus = (Int32)LCBranchModuleStatus.Disabled;
                            string actionLCBranchRequestStatusName = LCBranchModuleStatus.Disabled.ToString();

                            if (radReceiptModuleStatus.SelectedIndex >=0)
                            {
                                actionLCBranchRequestStatus = this.Member.NumberSet.ToInteger(radReceiptModuleStatus.SelectedIndex.ToString());

                                if (actionLCBranchRequestStatus == (Int32)LCBranchModuleStatus.Requested)
                                {
                                    actionLCBranchRequestStatusName = LCBranchModuleStatus.Requested.ToString();
                                }
                                else if (actionLCBranchRequestStatus == (Int32)LCBranchModuleStatus.Approved)
                                {
                                    actionLCBranchRequestStatusName = LCBranchModuleStatus.Approved.ToString();
                                }
                                else
                                {
                                    actionLCBranchRequestStatusName = LCBranchModuleStatus.Disabled.ToString();
                                }
                            }

                            LCBranchGridBindData.Rows[i][AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName] = actionLCBranchRequestStatus;
                            LCBranchGridBindData.Rows[i][AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUS_NAMEColumn.ColumnName] = actionLCBranchRequestStatusName;
                            LCBranchGridBindData.AcceptChanges();
                        }
                    }
                }
            }
        }

        protected void imgRefresh_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadLCBranchClientEnableModuleRequests();
        }

        #endregion

        protected void gvLCBranchEnableRequests_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                if (e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName) != null)
                {
                    //hcode != e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName).ToString().Trim() &&
                    //bcode != e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName).ToString().Trim() &&
                    Int32 developmenttype = this.Member.NumberSet.ToInteger(e.GetValue(AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName).ToString());

                    if (developmenttype == (Int32)(DeploymentType.ClientServer))
                    {
                        if (location != e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName).ToString().Trim() ||
                            ipaddress != e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName).ToString().Trim())
                        {
                            e.Row.BackColor = System.Drawing.Color.Cyan;

                            hcode = e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName).ToString().Trim();
                            bcode = e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName).ToString().Trim();
                            location = e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName).ToString().Trim();
                            ipaddress= e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName).ToString().Trim();
                        }
                    }
                    else
                    {
                        if (location != e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName).ToString().Trim())
                        {
                            e.Row.BackColor = System.Drawing.Color.Cyan;

                            hcode = e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName).ToString().Trim();
                            bcode = e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName).ToString().Trim();
                            location = e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName).ToString().Trim();
                            ipaddress = e.GetValue(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName).ToString().Trim();
                        }
                    }
                }
            }
        }

        protected void cb_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            String[] p = e.Parameter.Split('|');

            //MyObject obj = session.GetObjectByKey<MyObject>(Convert.ToInt32(p[0])); // get the record from the Session
            //obj.Active = Convert.ToBoolean(p[1]);
            //obj.Save();
        }

        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            using (LicenseSystem licensesystem = new LicenseSystem())
            {
                resultArgs = licensesystem.DeleteAllLCBranchModuleRequests(HeadOfficeCode);
                if (resultArgs.Success)
                {
                    this.Message = MessageCatalog.Message.LCEnableTrackModules.LCEnableTrackModulesSucessfullyDeletedAll;
                    if (LCBranchGridBindData != null)
                    {
                        LCBranchGridBindData.Rows.Clear();
                        LCBranchEnableRequestsData.Rows.Clear();
                    }
                }
                else
                {
                    this.Message = MessageCatalog.Message.LCEnableTrackModules.LCEnableTrackModulesFailedDeletedAll;
                }
                LoadLCBranchClientEnableModuleRequests();
            }
        }

        protected void btnClearCurrentBranch_Click(object sender, EventArgs e)
        {
            using (LicenseSystem licensesystem = new LicenseSystem())
            {
                resultArgs = licensesystem.DeleteLCBranchRequestsByBranch(HeadOfficeCode, BranchOfficeCode);
                if (resultArgs.Success)
                {
                    this.Message = MessageCatalog.Message.LCEnableTrackModules.LCEnableTrackModulesSucessfullyDeletedAll;
                    if (LCBranchGridBindData != null)
                    {
                        LCBranchGridBindData.Rows.Clear();
                        LCBranchEnableRequestsData.Rows.Clear();
                    }
                }
                else
                {
                    this.Message = MessageCatalog.Message.LCEnableTrackModules.LCEnableTrackModulesFailedDeletedAll;
                }
                LoadLCBranchClientEnableModuleRequests();
            }
        }

    }
}