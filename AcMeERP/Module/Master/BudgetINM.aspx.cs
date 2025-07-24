/*****************************************************************************************************
 * Created by       : Chinna
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page helps head office admin/user or branch office admin/User to view the FD registers by Branch and Project wise
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using System.Data;
using System.Globalization;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using AcMEDSync.Model;
using Bosco.Report.ReportObject;
using Bosco.DAO.Schema;
using System.Web.UI;
using DevExpress.Web.ASPxGridView.Rendering;
using Bosco.Report.Base;
using System.Drawing;
using System.IO;

namespace AcMeERP.Module.Master
{
    public partial class BudgetINM : Base.UIBase
    {
        #region Declartion
        private ReportProperty objReportProperty = new ReportProperty();
        Bosco.Utility.ConfigSetting.SettingProperty objSettingProperty = new Bosco.Utility.ConfigSetting.SettingProperty();
        ResultArgs resultArgs = null;
        public const string ACTUAL = "ACTUAL";
        public const string PROPOSED_CURRENT_YR = "PROPOSED_CURRENT_YR";
        public const string APPROVED_CURRENT_YR = "APPROVED_CURRENT_YR";
        public const string APPROVED_PREVIOUS_YR = "APPROVED_PREVIOUS_YR";
        public const string LEDGER_ID = "LEDGER_ID";
        public const string NARRATION = "NARRATION";

        #endregion

        #region Properties
        private int branchid = 0;
        private int BranchId
        {
            set { branchid = value; }
            get { return branchid; }
        }

        private int projectid = 0;
        private int ProjectId
        {
            set { projectid = value; }
            get { return projectid; }
        }

        private string projectids = "0";
        private string ProjectIds
        {
            get
            {
                return ViewState["ProjectIds"].ToString();
            }
            set
            {
                ViewState["ProjectIds"] = value;
            }
        }

        private DateTime BudgetDateFrom
        {
            set
            {
                ViewState["BudgetDateFrom"] = value;
            }
            get
            {
                return (DateTime)ViewState["BudgetDateFrom"];
            }
        }

        private DateTime BudgetDateTo
        {
            set
            {
                ViewState["BudgetDateTo"] = value;
            }
            get
            {
                return (DateTime)ViewState["BudgetDateTo"];
            }
        }

        private int budgetId = 0;
        private int BudgetId
        {
            get
            {
                int budgetid = this.Member.NumberSet.ToInteger(this.RowId);
                return budgetid;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }
        private BudgetAction BudgetStatus
        {
            set
            {
                ViewState["BudgetAction"] = value;
                AllModifyApprovedAmount = true;
                //if (value == BudgetAction.Approved)
                //{
                //    lblBudgetStatus.Text = BudgetAction.Approved.ToString();
                //    btnApproved.Text = CommandMode.Edit.ToString();
                //    AllModifyApprovedAmount = false;
                //}
                //else
                //{
                //    lblBudgetStatus.Text = BudgetAction.Recommended.ToString();
                //    btnApproved.Text = "Approve";
                //    AllModifyApprovedAmount = true;
                //}
            }
            get
            {
                if (ViewState["BudgetAction"] != null)
                {
                    return (BudgetAction)ViewState["BudgetAction"];
                }
                else
                    return BudgetAction.Recommended;
            }
        }
        private DataTable BudgetInfo
        {
            set { ViewState["BudgetDate"] = value; }
            get { return (DataTable)ViewState["BudgetDate"]; }
        }
        private DataTable BudgetDetails
        {
            set { ViewState[AppSchema.Budget.TableName] = value; }
            get { return (DataTable)ViewState[AppSchema.Budget.TableName]; }
        }

        private bool AllModifyApprovedAmount
        {
            set { ViewState["AllModifyApprovedAmount"] = value; }
            get
            {
                if (ViewState["AllModifyApprovedAmount"] != null)
                {
                    return (bool)ViewState["AllModifyApprovedAmount"];
                }
                else
                    return false;
            }
        }

        double totalOpeningBalance = 0;
        public double TotalOpeningBalance
        {
            get
            {
                return totalOpeningBalance;
            }
            set
            {
                totalOpeningBalance = value;
            }
        }

        private string selectedfile;
        private string SelectedFileName
        {
            get { return ViewState["selectedfile"] as string; ; }
            set { ViewState["selectedfile"] = value; }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblPersonName.Visible = lblRole.Visible = false;
                if (!base.LoginUser.IsPortalUser)
                {
                    LoadBranches();
                    LoadProjects();
                    LoadBudgetType();
                    //LoadBudgetLedgerDetails();
                    ShowLoadWaitPopUp(btnLoad);
                    ShowLoadWaitPopUp(btnApproved);
                    //  this.CheckUserRights(RightsModule.Data, RightsActivity.BudgetAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    lblBudgetStatus.Text = string.Empty;
                    lblDetailCashBankBalance.Text = "";
                    //27/02/2024
                    gvCashBankDetails.DataSource = null;
                    lblPersonName.Text = this.LoginUser.LoginUserHeadOfficeInchargeName;
                    lblRole.Text = this.LoginUser.LoginUserHeadOfficeInchargeDesignation;
                    //BindCashBankFDDetails();
                    if (BudgetId != 0)
                    {
                        AssignValues();
                    }
                    SetDateFormat();
                    ShowLoadWaitPopUp(btnSave);
                    //btnSave.PostBackUrl = this.GetPageUrlByName(URLPages.BudgetView.ToString());
                }
            }
        }

        private void SetPageTitle()
        {
            this.PageTitle = (this.HasRowId ? "Budget(Add)" : "Budget(Edit)");
        }

        private void ClearValues()
        {
            txtBudget.Text = string.Empty;
            cmbBranch.SelectedItem.Value = "0"; // = cmbProjects.SelectedItem.Value
            SetControlFocus(cmbBranch);
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGridRecords();
        }

        private void LoadGridRecords()
        {
            LoadBudgetInformation();
            LoadBudgetLedgerDetails();
            BindCashBankFDDetails();
            btnPrintPreview.Visible = btnDownload.Visible = true;
        }

        protected string GetLiquidityGroupIds()
        {
            string groupIds = (int)FixedLedgerGroup.BankAccounts + "," +
                              (int)FixedLedgerGroup.Cash + "," +
                              (int)FixedLedgerGroup.FixedDeposit;
            return groupIds;
        }

        private void BindCashBankFDDetails()
        {
            if (IsValidApply())
            {
                string DateFrom = string.Empty;
                DateFrom = BudgetDateFrom.ToShortDateString();

                string groupIds = this.GetLiquidityGroupIds();
                resultArgs = GetBalance(DateFrom, ProjectIds, groupIds);
                gvCashBankDetails.Visible = false;
                if (resultArgs.Success)
                {
                    DataTable dtBalance = resultArgs.DataSource.Table;
                    if (dtBalance.Rows.Count > 0)
                    {
                        dtBalance.Columns.Add("order", typeof(System.Int32), "IIF(GROUP_ID = 13, 1, IIF(GROUP_ID = 12, 2, 3))");
                        dtBalance.DefaultView.Sort = "order";
                        dtBalance = dtBalance.DefaultView.ToTable();
                        double AmountDR = Member.NumberSet.ToDouble(dtBalance.Compute("SUM(AMOUNT_DR)", string.Empty).ToString());
                        double AmountCR = Member.NumberSet.ToDouble(dtBalance.Compute("SUM(AMOUNT_CR)", string.Empty).ToString());
                        totalOpeningBalance = AmountDR - AmountCR;
                    }
                    lblDetailCashBankBalance.Text = string.Empty;
                    gvCashBankDetails.DataSource = dtBalance;
                    gvCashBankDetails.DataBind();
                    if (dtBalance.Rows.Count > 0)
                    {
                        lblDetailCashBankBalance.Text = "<b>Opening Balance as on " + BudgetDateFrom.ToShortDateString() + "</b>"; //"<b>Opening Balance as on " + lblDateFrom.Text + "</b>";
                        //ShowCashBankRowDetailBalance(dtBalance);
                        gvCashBankDetails.Visible = true;
                    }
                }
            }
        }

        private ResultArgs GetBalance(string balDate, string projectIds, string groupIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance, DataBaseType.HeadOffice))
            {
                BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupIds);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, balDate);
                if (BranchId != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.BRANCH_IDColumn, BranchId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
            } return resultArgs;
        }

        private void ShowCashBankRowDetailBalance(DataTable dtCashBankBalance)
        {
            lblDetailCashBankBalance.Text = "<b>Opening Balance as on " + BudgetDateFrom.ToShortDateString() + "</b>"; // "<b>Opening Balance as on " + lblDateFrom.Text + "</b>";
            foreach (DataRow dr in dtCashBankBalance.Rows)
            {
                string ledgername = dr[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                double amt = this.Member.NumberSet.ToDouble(dr[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                if (dr[this.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString().ToUpper() == "CR")
                {
                    amt = -amt;
                }
                string LedgerDetails = "<b>" + ledgername + "</b> " + this.Member.NumberSet.ToNumber(amt);
                lblDetailCashBankBalance.Text += String.IsNullOrEmpty(lblDetailCashBankBalance.Text) ? LedgerDetails : "<br>" + LedgerDetails;
            }
        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
            LoadProjects();
        }

        protected void cmbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 30.05.2024
            //ProjectIds = cmbProjects.SelectedItem.Value.ToString();
            //ProjectId = this.Member.NumberSet.ToInteger(cmbProjects.SelectedItem.Value.ToString());
        }

        protected void cmbBudgetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDateFormat();
        }

        private void SetDateFormat()
        {
            BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
            if (BranchId > 0)
            {
                if (Member.NumberSet.ToInteger(cmbBudgetType.SelectedItem.Value.ToString()) == (int)BudgetType.BudgetByAnnualYear)
                {
                    ASPXDateFrom.Enabled = aspxDateTo.Enabled = false;
                    DateTime dt = Member.DateSet.ToDate(objSettingProperty.YearFrom, false);
                    aspxDateTo.Date = Member.DateSet.ToDate(objSettingProperty.YearTo, false);
                    ASPXDateFrom.Date = BudgetDateFrom = Member.DateSet.ToDate(objSettingProperty.YearFrom, false);
                    aspxDateTo.Date = BudgetDateTo = Member.DateSet.ToDate(objSettingProperty.YearTo, false);
                }
                else if (Member.NumberSet.ToInteger(cmbBudgetType.SelectedItem.Value.ToString()) == (int)BudgetType.BudgetAcademic)
                {
                    ASPXDateFrom.Date = BudgetDateFrom = new DateTime(Member.DateSet.ToDate(objSettingProperty.YearFrom, false).Year, 6, 1);
                    aspxDateTo.Date = BudgetDateTo = new DateTime(Member.DateSet.ToDate(objSettingProperty.YearTo, false).Year, 5, 31);
                    ASPXDateFrom.MinDate = Member.DateSet.ToDate(objSettingProperty.YearFrom, false);
                    aspxDateTo.MaxDate = Member.DateSet.ToDate(objSettingProperty.YearTo, false);
                }
                else if (Member.NumberSet.ToInteger(cmbBudgetType.SelectedItem.Value.ToString()) == (int)BudgetType.BudgetByCalendarYear)
                {
                    ASPXDateFrom.Date = BudgetDateFrom = new DateTime(Member.DateSet.ToDate(objSettingProperty.YearFrom, false).Year, 1, 1);
                    aspxDateTo.Date = BudgetDateTo = new DateTime(Member.DateSet.ToDate(objSettingProperty.YearFrom, false).Year, 12, 31);
                }
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            if (ValidateBudgetDetails())
            {
                if (AllModifyApprovedAmount)
                {
                    UpdateGridValue();
                    SaveBudget(true, 2);
                    this.Message = MessageCatalog.Message.ApproveConfirmation;
                    BudgetStatus = BudgetAction.Approved;
                    btnApproved.ClientSideEvents.Click = "function(s, e) { e.processOnServer = true; }";
                }
                else
                {
                    btnApproved.Text = "Approve";
                    AllModifyApprovedAmount = true;
                    btnApproved.ClientSideEvents.Click = "function(s, e) { e.processOnServer = confirm('Are you sure to Approve Budget?'); }";
                }
                LoadGrid(BudgetDetails);
                Response.Redirect(this.GetPageUrlByName(URLPages.BudgetView.ToString()));

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateBudgetDetails())
            {
                if (AllModifyApprovedAmount)
                {
                    // DataTable dt = (DataTable)gvBudgetAdd.DataSource;
                    UpdateGridValue();
                    SaveBudget(true, 1);
                    // this.Message = MessageCatalog.Message.SaveConformation;
                    BudgetStatus = BudgetAction.Recommended; //BudgetStatus == BudgetAction.Approved ? BudgetAction.Approved : BudgetAction.Recommended;
                    // btnSave.ClientSideEvents.Click = "function(s, e) { e.processOnServer = true; }";
                    LoadGrid(BudgetDetails);
                    Response.Redirect(this.GetPageUrlByName(URLPages.BudgetView.ToString()));
                }
                else
                {
                    this.Message = "Not able to edit the Amount. Make it Edit Mode";
                }
            }
        }

        private void SaveBudget(bool BudgetAction, int BudgetStatusId)
        {
            try
            {
                using (BudgetSystem BudgetSystem = new BudgetSystem())
                {
                    BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    //ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                    //BudgetId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                    gvBudgetAdd.UpdateEdit();
                    BudgetSystem.dtBudgetLedgers = BudgetDetails;
                    BudgetSystem.BudgetId = BudgetId == 0 ? (int)AddNewRow.NewRow : BudgetId;
                    ProjectIds = FetchSelectedValueCheckBoxList(AspxProjectList);
                    BudgetSystem.MultipleProjectId = ProjectIds;
                    BudgetSystem.ProjectIds = ProjectIds;
                    BudgetSystem.BudgetAction = BudgetAction;
                    BudgetSystem.BudgetName = txtBudget.Text;
                    BudgetSystem.DateFrom = ASPXDateFrom.Date.ToShortDateString();
                    BudgetSystem.DateTo = aspxDateTo.Date.ToShortDateString();
                    BudgetSystem.BudgetTypeId = this.Member.NumberSet.ToInteger(cmbBudgetType.SelectedItem.Value.ToString());
                    BudgetSystem.BudgetLevelId = 1;
                    BudgetSystem.monthwiseDistribution = 0;
                    BudgetSystem.BudgetActionValue = BudgetStatus == Bosco.Utility.BudgetAction.Approved ? 2 : BudgetStatusId;
                    BudgetSystem.BranchId = BranchId;
                    BudgetSystem.Status = chkIsActive.Checked ? 1 : 0;
                    BudgetSystem.FileName = FileUploadBrowse.FileName;
                    UploadFile();

                    resultArgs = BudgetSystem.SaveINMBudgetDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        //  if (BudgetAction && BudgetStatusId != 1)
                        //{
                        //this.Message = "Budget is " + MessageCatalog.Message.ApproveConfirmation;
                        this.Message = MessageCatalog.Message.SaveConformation;
                        if (BudgetId == 0)
                        {
                            BudgetId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        }
                        if (BudgetId > 0)
                        {
                            using (BranchOfficeSystem branchoffice = new BranchOfficeSystem())
                            {
                                string DateFrom = string.Empty;
                                string DateTo = string.Empty;
                                string BudgetName = string.Empty;
                                string ProjectName = string.Empty;
                                if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
                                {
                                    DateFrom = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_FROM"].ToString());
                                    DateTo = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_TO"].ToString());
                                    BudgetName = BudgetInfo.Rows[0]["BUDGET_NAME"].ToString();
                                    ProjectName = BudgetInfo.Rows[0]["PROJECT"].ToString();
                                }
                                // resultArgs = branchoffice.SendBudgetMail(BranchId, BudgetName, DateFrom, DateTo, ProjectName, Bosco.Utility.BudgetAction.Approved);
                                resultArgs = branchoffice.SendBudgetMail(BranchId, BudgetName, DateFrom, DateTo, ProjectName, Bosco.Utility.BudgetAction.Recommended);
                            }
                        }
                        //  }
                        //else
                        //{
                        //    this.Message = MessageCatalog.Message.SaveConformation;
                        //}
                    }
                    else
                    {
                        // if (resultArgs != null)
                        this.Message = resultArgs.Message;
                    }
                }
            }
            catch (Exception err)
            {
                this.Message = "Problem in saving Budget Data (" + err.Message + ")";
            }
        }

        private void AssignValues()
        {
            using (BudgetSystem budgetsystem = new BudgetSystem(BudgetId, DataBaseType.HeadOffice))
            {
                cmbBranch.Text = budgetsystem.BranchName;
                BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                txtBudget.Text = budgetsystem.BudgetName;
                BudgetId = budgetsystem.BudgetId;
                LoadProjects();
                ProjectIds = budgetsystem.MultipleProjectId.ToString();
                // cmbProjects.Text = budgetsystem.Project; // 30/05/2024
                ProjectId = budgetsystem.ProjectId;

                AssignValueCheckBoxList(AspxProjectList, ProjectIds);

                cmbBudgetType.Text = budgetsystem.BudgetTypeId == 3 ? this.Member.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByAnnualYear) : budgetsystem.BudgetTypeId == 6 ? this.Member.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetAcademic) : this.Member.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByCalendarYear);
                BudgetDateFrom = this.Member.DateSet.ToDate(budgetsystem.DateFrom, false);
                BudgetDateTo = this.Member.DateSet.ToDate(budgetsystem.DateTo, false);
                chkIsActive.Checked = budgetsystem.Status == 0 ? false : true;
                cmbBudgetType.Enabled = false;
                ViewState["selectedfile"] = lblFileName.Text = budgetsystem.FileName;

                LoadGridRecords();
            }
        }

        private bool ValidateBudgetDetails()
        {
            bool isValid = true;
            //int SelectedProId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
            // int SelectedBudId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
            int SelectedBudId = BudgetId; //cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
            int ProId = 0;
            int BudId = 0;
            if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
            {
                ProId = this.Member.NumberSet.ToInteger(BudgetInfo.Rows[0]["PROJECT_ID"].ToString());
                BudId = this.Member.NumberSet.ToInteger(BudgetInfo.Rows[0]["BUDGET_ID"].ToString());
            }
            if (!(SelectedBudId == BudId))
            {
                this.Message = "Mismtach selected Budget, click Go button to load selected Budget details";
                isValid = false;
            }
            else if (!isValidActiveBudget())
            {
                isValid = false;
            }
            else if (AspxProjectList.SelectedValues == null && AspxProjectList.SelectedValues.Count == 0)
            {
                isValid = false;
                this.Message = "Project is empty";
            }
            return isValid;
        }

        private bool isValidActiveBudget()
        {
            bool isValid = true;
            using (BudgetSystem budgetSystem = new BudgetSystem())
            {
                if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetMonth)
                {
                    if (budgetSystem.BudgetTypeId != (int)BudgetType.BudgetPeriod)
                    {
                        if (chkIsActive.Checked)
                        {
                            budgetSystem.MultipleProjectId = ProjectIds;
                            budgetSystem.DateFrom = ASPXDateFrom.Date.ToShortDateString();
                            budgetSystem.DateTo = aspxDateTo.Date.ToShortDateString();
                            int ActiveBudgetId = budgetSystem.CheckStatus();
                            if (!ActiveBudgetId.Equals(BudgetId))
                            {
                                if (!ActiveBudgetId.Equals(0))
                                {
                                    budgetSystem.Status = 0;
                                    chkIsActive.Checked = false;
                                    this.Message = "Active Budget is made for the Project already.Budget will be saved as Inactive Mode";
                                }
                            }
                        }
                    }
                }
            }
            return isValid;
        }

        private bool IsValidApply()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtBudget.Text))
            {
                this.Message = "Budget Name should not be empty. Please Enter it";
                isValid = false;
            }
            return isValid;
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            if (AllModifyApprovedAmount)
            {
                MovePropssedToApproved();
            }
            else
            {
                this.Message = "Can't Move to Proposed to Approved Amount. Make it Edit Mode";
            }
        }

        protected void gvBudgetAdd_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //Approved Amount will not be modified once budget is approved, but they wish they can use edit button option and modify
            GridViewDataColumn colApprovedAmount = gvBudgetAdd.Columns["APPROVED_CURRENT_YR"] as GridViewDataColumn;
            GridViewDataColumn colProposedAmount = gvBudgetAdd.Columns["PROPOSED_CURRENT_YR"] as GridViewDataColumn;
            GridViewDataColumn colNarration = gvBudgetAdd.Columns["HO_NARRATION"] as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data)
            {
                if (gvBudgetAdd.FindRowCellTemplateControl(e.VisibleIndex, colApprovedAmount, "txtSpEdit") != null)
                {
                    ASPxSpinEdit txtApproved = gvBudgetAdd.FindRowCellTemplateControl(e.VisibleIndex, colApprovedAmount, "txtSpEdit") as ASPxSpinEdit;
                    ASPxSpinEdit txtProposed = gvBudgetAdd.FindRowCellTemplateControl(e.VisibleIndex, colProposedAmount, "txtSpEditProposed") as ASPxSpinEdit;
                    ASPxTextBox txtNarration = gvBudgetAdd.FindRowCellTemplateControl(e.VisibleIndex, colNarration, "txtSpEditNarration") as ASPxTextBox;

                    if (base.LoginUser.IsBranchOfficeUser || base.LoginUser.IsBranchOfficeAdminUser)
                    {
                        txtApproved.Enabled = txtNarration.Enabled = false;
                    }
                    else
                    {
                        //txtApproved.Enabled = txtNarration.Enabled = txtProposed.Enabled = AllModifyApprovedAmount;
                        // txtApproved.Enabled = txtNarration.Enabled = true;
                        //  txtProposed.Enabled = false;
                    }
                }
            }
        }

        protected void gvBudgetAdd_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (grid.GroupCount == 0)
                return;

            if ((e.RowType == GridViewRowType.Data || e.RowType == GridViewRowType.Group || e.RowType == GridViewRowType.GroupFooter))
            {
                if (e.RowType == GridViewRowType.Data)
                {
                    e.Row.Cells[0].Visible = false;
                    if (e.Row.Cells.Count > 1)
                    {
                        e.Row.Cells[1].Visible = false;
                        e.Row.Cells[2].Visible = false;
                    }
                }

                if (e.RowType == GridViewRowType.Group)
                {
                    int level = ((GridViewTableGroupRow)e.Row).GroupLevel;
                    GridViewDataColumn col = gvBudgetAdd.GetGroupedColumns()[level];

                    if (level == 1)
                    {
                        e.Row.Cells[0].Visible = false;
                    }
                    else if (level == 2 && e.Row.Cells.Count > 1)
                    {
                        e.Row.Cells[0].Visible = false;
                        e.Row.Cells[1].Visible = false;
                    }

                    if (col.FieldName == "BUDGET_TRANS_MODE")
                    {
                        if (e.RowType == GridViewRowType.Group)
                        {
                            //e.Row.Visible = false;
                            //e.Row.Attributes["style"] = "display:none";
                        }
                        else if (e.RowType == GridViewRowType.GroupFooter)
                        {
                            e.Row.Visible = false;
                            e.Row.Attributes["style"] = "display:none";
                        }
                    }
                    else if (col.FieldName == "BUDGET_GROUP")
                    {
                        if (e.Row.Cells.Count > 1)
                        {
                            e.Row.Cells[1].Font.Size = 10;

                            if (String.IsNullOrEmpty(e.Row.Cells[1].Text))
                            {
                                e.Row.Visible = false;
                                e.Row.Attributes["style"] = "display:none";
                            }
                        }
                    }
                    else if (col.FieldName == "BUDGET_SUB_GROUP")
                    {
                        if (e.Row.Cells.Count > 2)
                        {
                            e.Row.Cells[2].Font.Size = 9;

                            if (String.IsNullOrEmpty(e.Row.Cells[2].Text))
                            {
                                e.Row.Visible = false;
                                e.Row.Attributes["style"] = "display:none";
                            }
                        }
                    }
                }
                else if (e.RowType == GridViewRowType.GroupFooter)
                {
                    e.Row.Cells[0].Visible = false;
                    if (e.Row.Cells.Count > 1)
                    {
                        e.Row.Cells[1].Visible = false;
                        e.Row.Cells[2].Visible = false;
                    }
                    if (grid.GetDataRow(e.VisibleIndex) != null)
                    {
                        DataRow drv = grid.GetDataRow(e.VisibleIndex);
                        int rowlevel = grid.GetRowLevel(e.VisibleIndex);
                        string budgettransmode = drv["BUDGET_TRANS_MODE"].ToString();
                        //  string budgettransmode = drv["BUDGET_NATURE"].ToString();
                        if (rowlevel == 0 && e.Row.Cells.Count > 3)
                        {
                            e.Row.Cells[4].Text = budgettransmode.ToUpper() == "CR" ? "Income Ledgers Total" : "Expenses Ledgers Total";
                            // e.Row.Cells[4].Text = budgettransmode.ToUpper() == "Income Ledgers" ? "Income Ledgers Total" : "Expenses Ledgers Total";
                            if (e.Row.Cells[4].Text.Equals("Income Ledgers Total") || e.Row.Cells[4].Text.Equals("Expenses Ledgers Total"))
                            {
                                TableCell cell = new TableCell();
                                cell.Text = "";
                                e.Row.Cells.AddAt(5, cell);
                            }
                        }
                        else if (budgettransmode.ToUpper() == "CR" && rowlevel > 0)
                        {
                            string budgetgroup = drv["BUDGET_GROUP"].ToString();
                            string budgetsubgroup = drv["BUDGET_SUB_GROUP"].ToString();

                            e.Row.Visible = false;
                            e.Row.Attributes["style"] = "display:none";
                        }
                        else if (budgettransmode.ToUpper() == "DR" && rowlevel > 0)
                        {
                            string budgetgroup = drv["BUDGET_GROUP"].ToString();
                            string budgetsubgroup = drv["BUDGET_SUB_GROUP"].ToString();

                            e.Row.Visible = false;
                            e.Row.Attributes["style"] = "display:none";
                        }
                    }
                }
            }
        }

        protected void gvBudgetView_Load(object sender, EventArgs e)
        {
            gvBudgetAdd.DataSource = BudgetDetails;
            gvBudgetAdd.DataBind();
        }

        #endregion

        #region Methods
        private void LoadBudgetLedgerDetails()
        {
            gvBudgetAdd.Settings.GroupFormat = "{1}";
            //btnApproved.Visible = false;
            // btnSave.Visible = btnMove.Visible = false;
            try
            {
                if (IsValidApply())
                {
                    BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    ProjectIds = FetchSelectedValueCheckBoxList(AspxProjectList);  // ProjectIds; 30/05/2024      // cmbProjects.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProjects.SelectedItem.Value.ToString());
                    BudgetId = BudgetId == 0 ? 0 : BudgetId;  //cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                    using (BudgetSystem budget = new BudgetSystem())
                    {
                        budget.BranchId = BranchId;
                        budget.ProjectIds = ProjectIds;
                        budget.BudgetId = BudgetId;
                        budget.BudgetTransMode = "DR";
                        budget.DateFrom = BudgetDateFrom.ToShortDateString();
                        budget.DateTo = BudgetDateTo.ToShortDateString();
                        BudgetStatus = BudgetAction.Created;
                        resultArgs = budget.FetchBudgetDetailsProposals();
                        if (resultArgs.Success)
                        {
                            DataTable dtExpenseLedgers = resultArgs.DataSource.Table;
                            BudgetDetails = dtExpenseLedgers;
                            btnApproved.Visible = btnSave.Visible = btnMove.Visible = (resultArgs.DataSource.Table.Rows.Count > 0);
                        }
                        else
                        {
                            BudgetDetails = resultArgs.DataSource.Table;
                            this.Message = "Problem in loading budget from portal (" + resultArgs.Message + ")";
                        }
                        LoadGrid(BudgetDetails);
                        SetReportPreviewPropterty();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBranches()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    BranchId = 0;
                    resultArgs = BranchOfficeSystem.FetchBranch();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, "CODE", this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                        if (cmbBranch.Items.Count > 0)
                        {
                            string branchid = string.Empty;
                            if (base.LoginUser.IsHeadOfficeUser || base.LoginUser.IsHeadOfficeAdminUser)
                            {
                                Session[base.DefaultBranchId] = cmbBranch.SelectedItem.Value == "All" ? "0" : cmbBranch.SelectedItem.Value;
                                BranchId = cmbBranch.SelectedItem.Value == "All" ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                            }
                            if (base.LoginUser.IsBranchOfficeUser || base.LoginUser.IsBranchOfficeAdminUser)
                            {
                                if (!string.IsNullOrEmpty(base.LoginUser.LoginUserBranchOfficeName))
                                {
                                    cmbBranch.Text = base.LoginUser.LoginUserBranchOfficeName;
                                    BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                                }
                                cmbBranch.Enabled = false;
                            }
                        }
                        else
                        {
                            BranchId = 0;
                            if (base.LoginUser.IsHeadOfficeUser)
                            {
                                BranchId = 0; ;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        private void LoadProjects()
        {
            using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
            {
                try
                {
                    resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        // this.Member.ComboSet.BindCombo(cmbProjects, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, false);// 30.05.2024
                        ProjectId = 0;
                        string projectid = string.Empty;
                        //for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                        //{
                        //    projectid += resultArgs.DataSource.Table.Rows[i]["PROJECT_ID"].ToString() + ",";
                        //} LoadLedgersInMultiCheckbox(cbchkInterAcFromList, dtAssetLiabilities, this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName,
                        // this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName);
                        //ProjectIds = projectid.TrimEnd(',');
                        // ProjectIds = cmbProjects.SelectedItem.Value.ToString(); // 30.05.2024

                        BindProjectCheckBoxList(AspxProjectList, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECT_IDColumn.ToString(), this.AppSchema.Project.PROJECTColumn.ToString());

                    }
                    else
                    {
                        // this.Member.ComboSet.BindCombo(cmbProjects, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, false);// 30.05.2024
                        ProjectId = 0;
                        ProjectIds = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    this.Message = ex.ToString();
                }
            }
        }

        private void BindProjectCheckBoxList(ASPxCheckBoxList ASPxCheckBoxList1, DataTable dtSource, string valueField, string Valuetext)
        {
            ASPxCheckBoxList1.ValueType = typeof(System.Int32);
            ASPxCheckBoxList1.ValueField = valueField;
            ASPxCheckBoxList1.TextField = Valuetext;
            ASPxCheckBoxList1.DataSource = dtSource;
            ASPxCheckBoxList1.DataBind();
        }

        /// <summary>
        /// Fetch the Selected values and update it
        /// </summary>
        /// <param name="chklist"></param>
        /// <param name="sValues"></param>
        private void AssignValueCheckBoxList(ASPxCheckBoxList chklist, string sValues)
        {
            if (!string.IsNullOrEmpty(sValues))
            {
                string[] selecteditems = sValues.Split(',');
                foreach (string value in selecteditems)
                {
                    Int32 rowvalue = this.Member.NumberSet.ToInteger(value);
                    if (chklist.Items.FindByValue(rowvalue) != null)
                    {
                        chklist.Items.FindByValue(rowvalue).Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Get the Selected Values
        /// </summary>
        /// <param name="chklist"></param>
        /// <returns></returns>
        private string FetchSelectedValueCheckBoxList(ASPxCheckBoxList chklist)
        {
            string rtn = string.Empty;
            if (chklist.SelectedValues != null && chklist.SelectedValues.Count > 0)
            {
                foreach (Int32 value in chklist.SelectedValues)
                {
                    rtn += value + ",";
                }
                rtn = rtn.TrimEnd(',');
            }
            return rtn;
        }

        private void LoadBudgetType()
        {
            BudgetType budgetType = new BudgetType();
            DataView dvbudget = this.Member.EnumSet.GetEnumDataSource(budgetType, Sorting.Ascending);
            if (dvbudget.Count > 0)
            {
                if (BudgetId > 0)
                    dvbudget.RowFilter = "Id in (3,4,6)";
                else
                    dvbudget.RowFilter = "Id in (3,4,6)";
                DataTable dtBudgetType = dvbudget.ToTable();
                string EnumValAnualYear = this.Member.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByAnnualYear);
                string EnumValCalenderYear = this.Member.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetByCalendarYear);

                //string EnumValBudgetMonth = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetMonth);
                //string EnumValBudgetPeriod = this.Member.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetPeriod);
                //dtBudgetType.Rows[0]["Name"] = EnumValAnualYear;
                //dtBudgetType.Rows[1]["Name"] = EnumValCalenderYear;
                // dtBudgetType.Rows[2]["Name"] = EnumValBudgetPeriod;

                string EnumValAcademicYear = this.Member.EnumSet.GetDescriptionFromEnumValue(BudgetType.BudgetAcademic);
                dtBudgetType.Rows[0]["Name"] = EnumValAcademicYear;
                dtBudgetType.Rows[1]["Name"] = EnumValAnualYear;
                dtBudgetType.Rows[2]["Name"] = EnumValCalenderYear;
                this.Member.ComboSet.BindCombo(cmbBudgetType, dtBudgetType, "Name", "Id", false);
                //  cmbBudgetType.SelectedIndex = 0;
                //  cmbBudgetType.Enabled = false;
                ASPXDateFrom.Enabled = aspxDateTo.Enabled = false;

                //glkpBudgetType.Properties.DataSource = dtBudgetType;
                //glkpBudgetType.Properties.DisplayMember = "Name";
                //glkpBudgetType.Properties.ValueMember = "Id";
                //glkpBudgetType.EditValue = glkpBudgetType.Properties.GetKeyValue(0);
            }
        }

        private void LoadBudget()
        {
            try
            {
                using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
                {
                    DateTime datefrom = new DateTime();
                    DateTime dateto = new DateTime();
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            datefrom = this.Member.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                            dateto = this.Member.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                        }
                        BranchId = cmbBranch.Items.Count == 0 || cmbBranch.SelectedItem == null ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                        //ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                        //resultArgs = BranchOffiecSystem.FetchBudget(BranchId, datefrom, dateto);
                        //if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        //{
                        //    this.Member.ComboSet.BindCombo(cmbBudget, resultArgs.DataSource.Table, AppSchema.Budget.BUDGET_NAMEColumn.ColumnName, AppSchema.Budget.BUDGET_IDColumn.ColumnName, false);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBudgetInformation()
        {
            //27/02/2024
            //lblBudgetName.Text = string.Empty;
            //lblDateFrom.Text = string.Empty;
            //lblDateTo.Text = string.Empty;
            lblBudgetStatus.Text = string.Empty;
            lblDetailCashBankBalance.Text = string.Empty;
            //27/02/2024
            //lblProjectName.Text = string.Empty;
            gvCashBankDetails.DataSource = null;
            lblPersonName.Visible = lblRole.Visible = false;
            try
            {
                DataTable dtBudget = new DataTable();
                BranchId = cmbBranch.Items.Count == 0 || cmbBranch.SelectedItem == null ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                //ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                BudgetId = BudgetId == 0 ? 0 : BudgetId;  //cmbBudget.Items.Count == 0 || cmbBudget.SelectedItem == null ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                using (BudgetSystem BudgetSystem = new BudgetSystem())
                {
                    dtBudget = BudgetInfo = BudgetSystem.FillBudetDetails(BudgetId);
                    if (dtBudget != null && dtBudget.Rows.Count > 0)
                    {
                        DateTime dePeriodFrom = this.Member.DateSet.ToDate(dtBudget.Rows[0]["DATE_FROM"].ToString(), false);
                        DateTime dePeriodTo = this.Member.DateSet.ToDate(dtBudget.Rows[0]["DATE_TO"].ToString(), false);

                        //string Previous = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).AddMonths(-1).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                        //string Current = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                        //string MonthCurrentYear = String.Format("{0}-{1}", Current, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).Year);
                        //string MonthPreviousYear = String.Format("{0}-{1}", Previous, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).AddMonths(-1).Year);

                        gvBudgetAdd.Columns["colPROPOSED_CURRENT_YR"].Caption = String.Format("Proposed {0}", dePeriodFrom.Year.ToString() + " " + dePeriodTo.Year.ToString());
                        gvBudgetAdd.Columns["colAPPROVED_CURRENT_YR"].Caption = string.Format("Approved {0}", dePeriodFrom.Year.ToString() + " " + dePeriodTo.Year.ToString());
                        gvBudgetAdd.Columns["colAPPROVED_CURRENT_YR1"].Caption = string.Format("Approved {0}", dePeriodFrom.Year.ToString() + " " + dePeriodTo.Year.ToString());
                        gvBudgetAdd.Columns["colActual"].Caption = String.Format("Actual {0}", (dePeriodFrom.Year - 1).ToString() + " " + (dePeriodTo.Year - 1).ToString());
                        gvBudgetAdd.Columns["colAPPROVED_PREVIOUS_YR"].Caption = String.Format("Approved {0}", (dePeriodFrom.Year - 1).ToString() + " " + (dePeriodTo.Year - 1).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadGrid(DataTable dtBudget)
        {
            try
            {
                DataTable IncomeSource = dtBudget.DefaultView.ToTable();
                gvBudgetAdd.DataSource = IncomeSource;
                gvBudgetAdd.DataBind();
                BudgetDetails = IncomeSource;
                gvBudgetAdd.ExpandAll();
                // BindCashBankFDDetails();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        private void UpdateGridValue()
        {
            try
            {
                GridViewDataColumn colLedger = gvBudgetAdd.Columns["LEDGER_ID"] as GridViewDataColumn;
                GridViewDataColumn colApprovedAmount = gvBudgetAdd.Columns["APPROVED_CURRENT_YR"] as GridViewDataColumn;
                GridViewDataColumn colProposedAmount = gvBudgetAdd.Columns["PROPOSED_CURRENT_YR"] as GridViewDataColumn;
                GridViewDataColumn colHONarration = gvBudgetAdd.Columns["HO_NARRATION"] as GridViewDataColumn;
                int visiblerowindex = 0;
                Int32 ledgerid = 0;
                string budgettransmode = string.Empty;

                gvBudgetAdd.UpdateEdit();
                DataTable dt = gvBudgetAdd.DataSource as DataTable;

                for (int i = 0; i < gvBudgetAdd.VisibleRowCount; i++)
                {
                    if (gvBudgetAdd.FindRowCellTemplateControl(i, colApprovedAmount, "txtSpEdit") != null)
                    {
                        decimal approvedamt = 0;
                        decimal proposedamt = 0;
                        string HONarration = string.Empty;
                        //28/05/2020, to have Budget Income and Expense Ledgers
                        ledgerid = 0;
                        budgettransmode = string.Empty;
                        string[] strFieldNames = new string[] { "LEDGER_ID", "BUDGET_TRANS_MODE" };
                        DataTable dtUpdate = gvBudgetAdd.DataSource as DataTable;
                        if (gvBudgetAdd.GetRowValues(i, strFieldNames) != null)
                        {
                            object[] objUpdateLedgerInfo = gvBudgetAdd.GetRowValues(i, strFieldNames) as object[];
                            if (objUpdateLedgerInfo.GetLength(0) == 2)
                            {
                                ledgerid = base.LoginUser.NumberSet.ToInteger(objUpdateLedgerInfo.GetValue(0).ToString());
                                budgettransmode = objUpdateLedgerInfo.GetValue(1).ToString();
                            }
                        }
                        if (gvBudgetAdd.FindRowCellTemplateControl(i, colApprovedAmount, "txtSpEdit") != null)
                        {
                            ASPxSpinEdit txtApproved = gvBudgetAdd.FindRowCellTemplateControl(i, colApprovedAmount, "txtSpEdit") as ASPxSpinEdit;
                            approvedamt = base.LoginUser.NumberSet.ToDecimal(txtApproved.Text);
                        }
                        if (gvBudgetAdd.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") != null)
                        {
                            ASPxTextBox txtHONarration = gvBudgetAdd.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") as ASPxTextBox;
                            HONarration = txtHONarration.Text;
                        }
                        if (gvBudgetAdd.FindRowCellTemplateControl(i, colProposedAmount, "txtSpEditProposed") != null)
                        {
                            ASPxSpinEdit txtProposed = gvBudgetAdd.FindRowCellTemplateControl(i, colProposedAmount, "txtSpEditProposed") as ASPxSpinEdit;
                            proposedamt = base.LoginUser.NumberSet.ToDecimal(txtProposed.Text);
                        }

                        //On 01/06/2020, to update into datatable
                        //if (BudgetDetails.Rows.Count > 0)
                        //{
                        //    BudgetDetails.Rows[i]["APPROVED_CURRENT_YR"] = approvedamt;
                        //    BudgetDetails.Rows[i]["HO_NARRATION"] = HONarration;
                        //    BudgetDetails.AcceptChanges();
                        //}

                        if (ledgerid > 0 && budgettransmode != "")
                        {
                            BudgetDetails.DefaultView.RowFilter = string.Empty;
                            BudgetDetails.DefaultView.RowFilter = "LEDGER_ID=" + ledgerid + " AND BUDGET_TRANS_MODE='" + budgettransmode + "'";
                            if (BudgetDetails.DefaultView.Count == 1)
                            {
                                BudgetDetails.DefaultView.BeginInit();
                                BudgetDetails.DefaultView[0]["APPROVED_CURRENT_YR"] = approvedamt;
                                BudgetDetails.DefaultView[0]["PROPOSED_CURRENT_YR"] = proposedamt;
                                BudgetDetails.DefaultView[0]["HO_NARRATION"] = HONarration;
                                BudgetDetails.DefaultView.EndInit();
                            }
                            BudgetDetails.DefaultView.RowFilter = string.Empty;
                        }
                    }
                    visiblerowindex++;
                }
                LoadGrid(BudgetDetails);
            }
            catch (Exception err)
            {
                this.Message = "Unable update in grid " + err.Message;
            }
        }

        private void MovePropssedToApproved()
        {
            DataTable dtSource = BudgetDetails;
            foreach (DataRow drROW in dtSource.Rows)
            {
                drROW["APPROVED_CURRENT_YR"] = this.Member.NumberSet.ToDecimal(drROW["PROPOSED_CURRENT_YR"].ToString());
                dtSource.AcceptChanges();
            }
            LoadGrid(dtSource);
        }

        private void SetReportPreviewPropterty()
        {
            //Assign Properties for Print Preview
            objReportProperty.ReportId = "RPT-048"; //Budget Details
            objReportProperty.DateFrom = string.Empty; // lblDateFrom.Text;
            objReportProperty.DateTo = string.Empty; // lblDateTo.Text;
            objReportProperty.BudgetId = BudgetId.ToString();
            objReportProperty.Budget = BudgetId.ToString();
            objReportProperty.BranchOffice = BranchId.ToString();
            objReportProperty.Project = ProjectIds;
            objReportProperty.ReportTitle = "";
            if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
            {
                DateTime dePeriodFrom = BudgetDateFrom;  //this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_FROM"].ToString(), false);
                DateTime dePeriodTo = BudgetDateTo; //this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_TO"].ToString(), false);
            }
            objReportProperty.BudgetName = string.Empty; //cmbBudget.SelectedItem == null ? string.Empty : cmbBudget.SelectedItem.Text;
            objReportProperty.Project = "";
            objReportProperty.BranchOfficeName = cmbBranch.SelectedItem == null ? string.Empty : cmbBranch.SelectedItem.Text;
            objReportProperty.SaveReportSetting();
            Session["REPORTPROPERTY"] = objReportProperty;
        }

        private void UploadFile()
        {
            string UploadFileDirectory = string.Empty;
            try
            {
                //string TemplatePath = @"D:\APP Source\APP Source\Portal\AcMeERP\Module\Software\Uploads\BudgetFiles\";
                string FileName = FileUploadBrowse.FileName;
                if (FileUploadBrowse.HasFile)
                {
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        UploadFileDirectory = Server.MapPath("~/Module/Software/Uploads/BudgetFiles/") + FileName;
                        FileUploadBrowse.SaveAs(UploadFileDirectory);
                        lblFileName.Text = "File Uploaded: " + FileName;
                        new ErrorLog().WriteError("Import Budget Files - " + "File is Saved");
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Budget Files:" + ex.Message);
            }
        }

        protected void btnDownLink_Click(object sender, EventArgs e)
        {
            // Specify the file name
            string excelFileName = ViewState["selectedfile"].ToString(); // Use null conditional operator to handle null ViewState
            string excelFilePath = Server.MapPath("~/Module/Software/Uploads/BudgetFiles/" + excelFileName);

            // Check if the file exists
            if (File.Exists(excelFilePath))
            {
                try
                {
                    // Set the appropriate headers for the response to initiate file download
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + excelFileName);
                    Response.TransmitFile(excelFilePath);
                    Response.Flush(); // Flush the response to ensure all content is sent to the client
                }
                catch (Exception ex)
                {
                    new ErrorLog().WriteError("Error occurred while downloading file: " + ex.Message);
                }
                finally
                {
                    Response.End(); // End the response
                }
            }
            else
            {
                new ErrorLog().WriteError("File does not exist: " + excelFilePath);
            }
        }
        #endregion
    }
}