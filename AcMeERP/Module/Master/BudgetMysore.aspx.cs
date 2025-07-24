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
using Bosco.Report.Base;
using AcMeERP.MasterPage;

namespace AcMeERP.Module.Master
{
    public partial class BudgetMysore : Base.UIBase
    {
        #region Declartion
        //For Print
        private ReportProperty objReportProperty = new ReportProperty();

        ResultArgs resultArgs = null;
        public const string ACTUAL = "ACTUAL";
        public const string PROPOSED_CURRENT_YR = "PROPOSED_CURRENT_YR";
        public const string APPROVED_CURRENT_YR = "APPROVED_CURRENT_YR";
        public const string APPROVED_PREVIOUS_YR = "APPROVED_PREVIOUS_YR";
        public const string LEDGER_ID = "LEDGER_ID";
        public const string NARRATION = "NARRATION";

        int RecLedgersSerialNo = 0;
        int RecAlphabetSerialNo = 0;
        private string[] FixedRecAlphabetLedgers = { "Net Amount", "Professional Tax", "LIC" };
        private string[] FixedRecESICMainLedgers = { "Management ESIC", "Employee ESIC" };
        #endregion

        #region Properties

        private bool IsTwoMonthBudget
        {
            set { ViewState["IsTwoMonthBudget"] = value; }
            get
            {
                bool rtn = false;
                rtn = ViewState["IsTwoMonthBudget"] == null ? false : Convert.ToBoolean(ViewState["IsTwoMonthBudget"].ToString());
                return rtn;
            }
        }

        private int BranchId
        {
            set { ViewState["BranchId"] = value; }
            get
            {
                Int32 rtn = 0;
                rtn = ViewState["BranchId"] == null ? 0 : this.Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
                return rtn;
            }
        }

        private int ProjectId
        {
            set { ViewState["ProjectId"] = value; }
            get
            {
                Int32 rtn = 0;
                rtn = ViewState["ProjectId"] == null ? 0 : this.Member.NumberSet.ToInteger(ViewState["ProjectId"].ToString());
                return rtn;
            }
        }

        private int BudgetId
        {
            set { ViewState["BudgetId"] = value; }
            get
            {
                Int32 rtn = 0;
                rtn = ViewState["BudgetId"] == null ? 0 : this.Member.NumberSet.ToInteger(ViewState["BudgetId"].ToString());
                return rtn;
            }
        }

        private int M1BudgetId
        {
            set { ViewState["M1BudgetId"] = value; }
            get
            {
                Int32 rtn = 0;
                rtn = ViewState["M1BudgetId"] == null ? 0 : this.Member.NumberSet.ToInteger(ViewState["M1BudgetId"].ToString());
                return rtn;
            }
        }

        private int M2BudgetId
        {
            set { ViewState["M2BudgetId"] = value; }
            get
            {
                Int32 rtn = 0;
                rtn = ViewState["M2BudgetId"] == null ? 0 : this.Member.NumberSet.ToInteger(ViewState["M2BudgetId"].ToString());
                return rtn;
            }
        }

        private string BudgetDateFrom
        {
            set { ViewState["BudgetDateFrom"] = value; }
            get
            {
                string rtn = string.Empty;
                rtn = ViewState["BudgetDateFrom"] == null ? string.Empty : ViewState["BudgetDateFrom"].ToString();
                return rtn;
            }
        }

        private string BudgetDateTo
        {
            set { ViewState["BudgetDateTo"] = value; }
            get
            {
                string rtn = string.Empty;
                rtn = ViewState["BudgetDateTo"] == null ? string.Empty : ViewState["BudgetDateTo"].ToString();
                return rtn;
            }
        }

        private string NextBudgetDateFrom
        {
            set { ViewState["NextBudgetDateFrom"] = value; }
            get
            {
                string rtn = string.Empty;
                rtn = ViewState["NextBudgetDateFrom"] == null ? string.Empty : ViewState["NextBudgetDateFrom"].ToString();
                return rtn;
            }
        }

        private string NextBudgetDateTo
        {
            set { ViewState["NextBudgetDateFrom"] = value; }
            get
            {
                string rtn = string.Empty;
                rtn = ViewState["NextBudgetDateFrom"] == null ? string.Empty : ViewState["NextBudgetDateFrom"].ToString();
                return rtn;
            }
        }

        private string PrevBudgetDateFrom
        {
            set { ViewState["PrevBudgetDateFrom"] = value; }
            get
            {
                string rtn = string.Empty;
                rtn = ViewState["PrevBudgetDateFrom"] == null ? string.Empty : ViewState["PrevBudgetDateFrom"].ToString();
                return rtn;
            }
        }

        private string PrevBudgetDateTo
        {
            set { ViewState["PrevBudgetDateTo"] = value; }
            get
            {
                string rtn = string.Empty;
                rtn = ViewState["PrevBudgetDateTo"] == null ? string.Empty : ViewState["PrevBudgetDateTo"].ToString();
                return rtn;
            }
        }

        private BudgetAction BudgetStatus
        {
            set
            {
                ViewState["BudgetAction"] = value;
                if (value == BudgetAction.Approved)
                {
                    lblBudgetStatus.Text = BudgetAction.Approved.ToString();
                    btnApproved.Text = CommandMode.Edit.ToString();
                    btnSave.Enabled = false;
                    AllModifyApprovedAmount = false;
                }
                else
                {
                    lblBudgetStatus.Text = BudgetAction.Recommended.ToString();
                    btnApproved.Text = "Approved";
                    btnSave.Enabled = true;
                    AllModifyApprovedAmount = true;
                }
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

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //For ABE, hide move, save buttons
            btnSave.Visible = btnMove.Visible = false;
            lblPersonName.Visible = lblRole.Visible = false;

            if (!IsPostBack)
            {
                LoadBranches();
                LoadProjects();
                LoadBudgetLedgerDetails();
                ShowLoadWaitPopUp(btnLoad);
                ShowLoadWaitPopUp(btnSave);
                ShowLoadWaitPopUp(btnApproved);

                lblBudgetName.Text = string.Empty;
                lblDateFrom.Text = string.Empty;
                lblDateTo.Text = string.Empty;
                lblBudgetStatus.Text = string.Empty;
                //lblDetailCashBankBalance.Text = string.Empty;
                //gvCashBankDetails.DataSource = null;

                lblPersonName.Text = this.LoginUser.LoginUserHeadOfficeInchargeName;
                lblRole.Text = this.LoginUser.LoginUserHeadOfficeInchargeDesignation;
                //BindCashBankFDDetails();
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadBudgetInformation();
            LoadBudgetLedgerDetails();
            //BindCashBankFDDetails();
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            MovePropssedToApproved();
        }

        protected void gvBudgetView_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //Approved Amount will not be modified once budget is approved, but they wish they can use edit button option and modify
            if (gvBudgetView.DataSource != null)
            {
                GridViewDataColumn colM1ApprovedAmount = gvBudgetView.Columns["APPROVED_CURRENT_YR"] as GridViewDataColumn;
                GridViewDataColumn colM2ApprovedAmount = gvBudgetView.Columns["colM2APPROVED_AMOUNT"] as GridViewDataColumn;
                GridViewDataColumn colNarration = gvBudgetView.Columns["HO_NARRATION"] as GridViewDataColumn;


                if (e.RowType == GridViewRowType.Data)
                {
                    if (gvBudgetView.FindRowCellTemplateControl(e.VisibleIndex, colM1ApprovedAmount, "txtM1ApprovedAmount") != null)
                    {
                        ASPxSpinEdit txtM1Approved = gvBudgetView.FindRowCellTemplateControl(e.VisibleIndex, colM1ApprovedAmount, "txtM1ApprovedAmount") as ASPxSpinEdit;
                        ASPxTextBox txtNarration = gvBudgetView.FindRowCellTemplateControl(e.VisibleIndex, colNarration, "txtSpEditNarration") as ASPxTextBox;
                        ASPxSpinEdit txtM2Approved = null;
                        txtM1Approved.Enabled = txtNarration.Enabled = AllModifyApprovedAmount;

                        if (IsTwoMonthBudget)
                        {
                            txtM2Approved = gvBudgetView.FindRowCellTemplateControl(e.VisibleIndex, colM2ApprovedAmount, "txtM2ApprovedAmount") as ASPxSpinEdit;
                            txtM2Approved.Enabled = AllModifyApprovedAmount;
                        }

                        string ledgername = gvBudgetView.GetRowValues(e.VisibleIndex, new String[] { "LEDGER_NAME" }).ToString();
                        if (ledgername.Trim().ToUpper() == "SALARY" || ledgername.Trim().ToUpper() == "B. P.F :" || ledgername.Trim().ToUpper() == "C. ESIC :")
                        {
                            txtM1Approved.CssClass = "hideIndentColumn";
                            txtNarration.CssClass = "hideIndentColumn";

                            if (IsTwoMonthBudget && txtM2Approved != null)
                            {
                                txtM2Approved.CssClass = "hideIndentColumn";
                            }
                        }
                    }
                }
            }
        }

        private DataTable FetchAccounts(int GroupId)
        {
            ResultArgs resultArgs = new ResultArgs();
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                string BalanceDate = string.Empty;
                int BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                int ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
                {
                    BalanceDate = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_FROM"].ToString());
                }
                voucherTransSystem.ProjectIDs = ProjectId.ToString();
                voucherTransSystem.GroupId = GroupId;
                BalanceSystem.BalanceType BalanceType = BalanceSystem.BalanceType.OpeningBalance;
                voucherTransSystem.BalanceDate = BalanceSystem.BalanceType.OpeningBalance == BalanceType ? Convert.ToDateTime(BalanceDate).AddDays(-1) : Convert.ToDateTime(BalanceDate);
                voucherTransSystem.BranchIds = BranchId.ToString();
                if (BalanceSystem.BalanceType.OpeningBalance == BalanceType)
                {
                    resultArgs = voucherTransSystem.FetchTransBalance();
                }
                else
                {
                    resultArgs = voucherTransSystem.FetchTransClosingBalance();
                }
            }
            return resultArgs.DataSource.Table;
        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsTwoMonthBudget = FetchOneMonthTwoMonthStatus();
            LoadProjects();
            LoadBudget();
        }

        protected void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvBudgetView.DataSource = null;
            gvBudgetView.DataBind();
            ClearFooter();
            LoadBudget();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateGridValue();
            SaveBudget(false);
            this.Message = MessageCatalog.Message.SaveConformation;
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            if (ValidateBudgetDetails())
            {
                if (btnApproved.Text == "Approved")
                {
                    UpdateGridValue();
                    SaveBudget(true);
                    this.Message = MessageCatalog.Message.ApproveConfirmation;
                    BudgetStatus = BudgetAction.Approved;
                    btnApproved.ClientSideEvents.Click = "function(s, e) { e.processOnServer = true; }";
                }
                else
                {
                    btnApproved.Text = "Approved";
                    AllModifyApprovedAmount = true;
                    btnApproved.ClientSideEvents.Click = "function(s, e) { e.processOnServer = confirm('Are you sure to Approve Budget?'); }";
                }
                LoadGrid();
            }
        }

        private void SaveBudget(bool BudgetAction)
        {
            try
            {
                using (BudgetSystem budgetsystem = new BudgetSystem())
                {
                    //BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    //ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                    //BudgetId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                    //gvBudgetView.UpdateEdit();

                    BudgetDetails.DefaultView.RowFilter = "IS_SUB_lEDGER=0";
                    budgetsystem.dtBudgetLedgers = BudgetDetails.DefaultView.ToTable();
                    BudgetDetails.DefaultView.RowFilter = "";

                    BudgetDetails.DefaultView.RowFilter = "IS_SUB_lEDGER=1";
                    budgetsystem.dtBudgetSubLedgers = BudgetDetails.DefaultView.ToTable();
                    BudgetDetails.DefaultView.RowFilter = "";

                    budgetsystem.BudgetId = budgetsystem.Month1BudgetId = BudgetId;
                    budgetsystem.Month2BudgetId = M2BudgetId;
                    budgetsystem.ProjectId = ProjectId;
                    budgetsystem.BudgetAction = BudgetAction;
                    budgetsystem.IsTwoMonthBudget = IsTwoMonthBudget;
                    resultArgs = budgetsystem.SaveBudgetDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (BudgetAction)
                        {
                            this.Message = MessageCatalog.Message.ApproveConfirmation;

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
                                resultArgs = branchoffice.SendBudgetMail(BranchId, BudgetName, DateFrom, DateTo, ProjectName, Bosco.Utility.BudgetAction.Approved);
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.SaveConformation;
                        }
                    }
                    else
                    {
                        if (resultArgs != null)
                            this.Message = resultArgs.Message;
                    }
                }
            }
            catch (Exception err)
            {
                this.Message = "Problem in saving Budget Data (" + err.Message + ")";
            }
        }

        private bool ValidateBudgetDetails()
        {
            bool isValid = false;
            int SelectedProId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
            //int SelectedBudId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());

            if (IsTwoMonthBudget)
            {
                string selectedbudgets = cmbBudget.SelectedItem.Value.ToString();
                string[] monthsbudget = selectedbudgets.Split(',');
                if (monthsbudget.Length == 2)
                {
                    //BudgetId = M1BudgetId = this.Member.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString());
                    //M2BudgetId = this.Member.NumberSet.ToInteger(monthsbudget.GetValue(1).ToString());
                    isValid = (M1BudgetId == this.Member.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString()) &&
                               M2BudgetId == this.Member.NumberSet.ToInteger(monthsbudget.GetValue(1).ToString()));
                }
            }
            else
            {
                Int32 id = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                isValid = (M1BudgetId == id);
                //BudgetId = M1BudgetId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
            }

            //int ProId = 0;
            //int BudId = 0;
            //if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
            //{
            //    ProId = this.Member.NumberSet.ToInteger(BudgetInfo.Rows[0]["PROJECT_ID"].ToString());
            //    BudId = this.Member.NumberSet.ToInteger(BudgetInfo.Rows[0]["BUDGET_ID"].ToString());
            //}

            if (!isValid)
            {
                this.Message = "Mismatched the Budget details, Click on Go button and check";
                cmbBudget.Focus();
                isValid = false;
            }
            return isValid;
        }

        protected void gvBudgetView_Load(object sender, EventArgs e)
        {
            gvBudgetView.DataSource = BudgetDetails;
            gvBudgetView.DataBind();
        }

        protected void gvBudgetView_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (gvBudgetView.DataSource != null)
            {
                ASPxGridView grid = sender as ASPxGridView;
                if (grid.GroupCount == 0)
                    return;

                if ((e.RowType == GridViewRowType.Header ||
                    e.RowType == GridViewRowType.Group || e.RowType == GridViewRowType.GroupFooter)
                    && e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[grid.GroupCount - 1].CssClass = "hideIndentColumn";

                    if (e.RowType == GridViewRowType.Group)
                    {
                        if (e.Row.Cells[1].Text.Trim().ToUpper() == "1")
                        {
                            e.Row.Cells[1].Text = "Recurring Expenses";
                        }
                        else if (e.Row.Cells[1].Text.Trim().ToUpper() == "2")
                        {
                            e.Row.Cells[1].Text = "Non-Recurring Expenses";
                        }
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[1].Font.Bold = true;
                        e.Row.Cells[1].Font.Size = 14;
                    }
                }
            }
        }

        protected void gvBudgetView_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (gvBudgetView.DataSource != null)
            {
                Int32 budgetgrpId = this.Member.NumberSet.ToInteger(gvBudgetView.GetRowValues(e.VisibleIndex, new String[] { "BUDGET_GROUP_ID" }).ToString());
                Int32 budgetsubgrpId = this.Member.NumberSet.ToInteger(gvBudgetView.GetRowValues(e.VisibleIndex, new String[] { "BUDGET_SUB_GROUP_ID" }).ToString());
                Int32 issubledger = this.Member.NumberSet.ToInteger(gvBudgetView.GetRowValues(e.VisibleIndex, new String[] { "IS_SUB_LEDGER" }).ToString());
                string mainledgername = gvBudgetView.GetRowValues(e.VisibleIndex, new String[] { "MAIN_LEDGER_NAME" }).ToString();
                string ledgername = gvBudgetView.GetRowValues(e.VisibleIndex, new String[] { "LEDGER_NAME" }).ToString();

                if (e.CellValue != null && e.DataColumn.FieldName.ToUpper() == "LEDGER_NAME")
                {
                    if (ledgername.Trim().ToUpper() == "SALARY" || ledgername.Trim().ToUpper() == "B. P.F :" || ledgername.Trim().ToUpper() == "C. ESIC :")
                    {
                        e.Cell.Font.Bold = true;
                    }
                    else if (budgetgrpId == 2 && issubledger == 1)
                    {
                        e.Cell.Text = "&nbsp;&nbsp;" + ledgername;
                    }
                    else
                    {
                        bool AlphaLedgersExists = Array.IndexOf(FixedRecAlphabetLedgers, e.CellValue.ToString()) >= 0;
                        if (AlphaLedgersExists)
                        {
                            e.Cell.Text = getAlphabetSerialNo() + ". " + ledgername;
                        }
                    }
                }
                else if (e.CellValue != null && e.DataColumn.FieldName.ToUpper() == "LEDGER_CODE")
                {
                    bool AlphaLedgersExists = Array.IndexOf(FixedRecAlphabetLedgers, ledgername) >= 0;
                    bool ESICLedgersExists = Array.IndexOf(FixedRecESICMainLedgers, ledgername) >= 0;
                    if ((budgetgrpId == 1 && budgetsubgrpId > 2 && !AlphaLedgersExists && !ESICLedgersExists) || (ledgername.Trim().ToUpper() == "SALARY"))//Skip Salary/PF group
                    {
                        RecLedgersSerialNo++;
                        e.Cell.Text = RecLedgersSerialNo.ToString();
                    }
                    else if (budgetgrpId == 2 && issubledger == 0)
                    {
                        RecLedgersSerialNo++;
                        e.Cell.Text = RecLedgersSerialNo.ToString();
                    }
                    else
                    {
                        e.Cell.Text = string.Empty;
                    }

                }
                else if (e.CellValue != null && e.DataColumn.FieldName.ToUpper() == "APPROVED_PREVIOUS_YR" || e.DataColumn.FieldName.ToUpper() == "ACTUAL" ||
                        e.DataColumn.FieldName.ToUpper() == "BALANCE" || e.DataColumn.FieldName.ToUpper() == "PROPOSED_CURRENT_YR" ||
                        e.DataColumn.FieldName.ToUpper() == "M2_PROPOSED_AMOUNT" || e.DataColumn.FieldName.ToUpper() == "M2_APPROVED_AMOUNT")
                {
                    if (ledgername.Trim().ToUpper() == "SALARY" || ledgername.Trim().ToUpper() == "B. P.F :" || ledgername.Trim().ToUpper() == "C. ESIC :")
                    {
                        e.Cell.Text = string.Empty;
                    }
                }
            }
        }

        #endregion

        #region Methods
        private void LoadBudgetLedgerDetails()
        {
            Session["REPORTPROPERTY"] = null;
            btnApproved.Visible = false;

            try
            {
                BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());

                if (IsTwoMonthBudget)
                {
                    if (cmbBudget.Items.Count > 0)
                    {
                        string selectedbudgets = cmbBudget.SelectedItem.Value.ToString();
                        string[] monthsbudget = selectedbudgets.Split(',');
                        if (monthsbudget.Length == 2)
                        {
                            BudgetId = M1BudgetId = this.Member.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString());
                            M2BudgetId = this.Member.NumberSet.ToInteger(monthsbudget.GetValue(1).ToString());
                        }
                    }
                }
                else
                {
                    BudgetId = M1BudgetId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                }

                PrevBudgetDateFrom = NextBudgetDateFrom = BudgetDateFrom = string.Empty;
                PrevBudgetDateTo = NextBudgetDateTo = BudgetDateTo = string.Empty;
                using (BudgetSystem budget = new BudgetSystem())
                {
                    if (BudgetId > 0 && ProjectId > 0)
                    {
                        budget.BranchId = BranchId;
                        budget.ProjectId = ProjectId;
                        //budget.BudgetId = budget.Month1BudgetId = BudgetId;
                        budget.BudgetId = budget.Month1BudgetId = M1BudgetId;
                        budget.Month2BudgetId = M2BudgetId;
                        budget.BudgetTransMode = "DR";

                        if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
                        {
                            BudgetDateFrom = budget.DateFrom = BudgetInfo.Rows[0]["DATE_FROM"].ToString();
                            BudgetDateTo = budget.DateTo = BudgetInfo.Rows[0]["DATE_TO"].ToString();

                            PrevBudgetDateFrom = this.Member.DateSet.ToDate(BudgetDateFrom, false).AddMonths(-1).ToShortDateString();
                            PrevBudgetDateTo = this.Member.DateSet.ToDate(PrevBudgetDateFrom, false).AddMonths(1).AddDays(-1).ToShortDateString();

                            //Assign Month dates (Month2)
                            NextBudgetDateFrom = this.Member.DateSet.ToDate(BudgetDateFrom, false).AddMonths(1).ToShortDateString();
                            NextBudgetDateTo = this.Member.DateSet.ToDate(NextBudgetDateFrom, false).AddMonths(1).AddDays(-1).ToShortDateString();

                            BudgetStatus = (BudgetAction)this.Member.NumberSet.ToInteger(BudgetInfo.Rows[0][this.AppSchema.Budget.BUDGET_ACTIONColumn.ColumnName].ToString());
                        }
                        resultArgs = budget.FetchMysoreBudgetEdit();
                        if (resultArgs.Success)
                        {
                            RecLedgersSerialNo = 0;
                            RecAlphabetSerialNo = 0;
                            BudgetDetails = resultArgs.DataSource.Table;
                            AttachRecurringGroup(BudgetDetails);

                            //If Budget Status is recommanded, Show Approved amount as propsed amount in the grid
                            if (BudgetStatus == BudgetAction.Recommended)
                            {
                                MovePropssedToApproved();
                            }
                            btnApproved.Visible = (resultArgs.DataSource.Table.Rows.Count > 0);
                        }
                        else
                        {
                            BudgetDetails = resultArgs.DataSource.Table;
                            this.Message = "Problem in loading budget from portal (" + resultArgs.Message + ")";
                        }
                        LoadGrid();
                        SetReportPreviewPropterty();
                        SetTitle();
                    }
                    else
                    {
                        gvBudgetView.DataSource = null;
                        gvBudgetView.DataBind();
                        ClearFooter();
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
                    resultArgs = BranchOfficeSystem.FetchBranch(DataBaseType.HeadOffice);
                    this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName, AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                    if (base.LoginUser.IsBranchOfficeUser)
                    {
                        cmbBranch.Text = base.LoginUser.LoginUserBranchOfficeName;
                        cmbBranch.Enabled = false;
                    }
                    else if (base.LoginUser.IsHeadOfficeUser)
                    {
                        if (this.Member.NumberSet.ToInteger(Session[base.DefaultBranchId].ToString()) > 0)
                        {
                            cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            SetTitle();
        }

        private void LoadProjects()
        {
            try
            {
                using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
                {
                    gvBudgetView.DataSource = null;
                    BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
                    this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, AppSchema.Project.PROJECTColumn.ColumnName, AppSchema.Project.PROJECT_IDColumn.ColumnName, false);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            SetTitle();
        }

        private void LoadBudget()
        {
            try
            {
                using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
                {
                    BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                    resultArgs = BranchOffiecSystem.FetchBudget(BranchId, ProjectId, IsTwoMonthBudget);
                    this.Member.ComboSet.BindCombo(cmbBudget, resultArgs.DataSource.Table, AppSchema.Budget.BUDGET_NAMEColumn.ColumnName, AppSchema.Budget.BUDGET_IDColumn.ColumnName, false);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            SetTitle();
        }

        private void LoadBudgetInformation()
        {
            lblBudgetName.Text = string.Empty;
            lblDateFrom.Text = string.Empty;
            lblDateTo.Text = string.Empty;
            lblBudgetStatus.Text = string.Empty;
            //lblDetailCashBankBalance.Text = string.Empty;
            // gvCashBankDetails.DataSource = null;
            lblPersonName.Visible = lblRole.Visible = false;

            try
            {
                DataTable dtBudget = new DataTable();
                BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                BudgetId = M1BudgetId = M2BudgetId = 0;

                gvBudgetView.Columns["colM2PROPOSED_AMOUNT"].Visible = false;
                gvBudgetView.Columns["colM2APPROVED_AMOUNT"].Visible = false;
                if (IsTwoMonthBudget)
                {
                    if (cmbBudget.Items.Count > 0)
                    {
                        string selectedbudgets = cmbBudget.SelectedItem.Value.ToString();
                        string[] monthsbudget = selectedbudgets.Split(',');
                        if (monthsbudget.Length == 2)
                        {
                            BudgetId = M1BudgetId = this.Member.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString());
                            M2BudgetId = this.Member.NumberSet.ToInteger(monthsbudget.GetValue(1).ToString());

                            gvBudgetView.Columns["colM2PROPOSED_AMOUNT"].Visible = true;
                            gvBudgetView.Columns["colM2APPROVED_AMOUNT"].Visible = true;
                        }
                    }
                }
                else
                {
                    BudgetId = M1BudgetId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                }

                if (BudgetId > 0 && ProjectId > 0)
                {
                    using (BudgetSystem BudgetSystem = new BudgetSystem())
                    {
                        dtBudget = BudgetInfo = BudgetSystem.FillBudetDetails(BudgetId);
                        if (dtBudget != null && dtBudget.Rows.Count > 0)
                        {
                            DateTime dePeriodFrom = this.Member.DateSet.ToDate(dtBudget.Rows[0]["DATE_FROM"].ToString(), false);
                            DateTime dePeriodTo = this.Member.DateSet.ToDate(dtBudget.Rows[0]["DATE_TO"].ToString(), false);
                            string Previous = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).AddMonths(-1).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                            string Current = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                            string NextMonth = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).AddMonths(1).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);

                            string MonthPreviousYear = String.Format("{0}-{1}", Previous, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).AddMonths(-1).Year);
                            string MonthCurrentYear = String.Format("{0}-{1}", Current, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).Year);
                            string MonthNextYear = String.Format("{0}-{1}", NextMonth, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).AddMonths(1).Year);

                            gvBudgetView.Columns["colPROPOSED_CURRENT_YR"].Caption = String.Format("Proposed {0}", MonthCurrentYear);
                            gvBudgetView.Columns["colAPPROVED_CURRENT_YR"].Caption = string.Format("Approved {0}", MonthCurrentYear);
                            //gvBudgetView.Columns["colAPPROVED_CURRENT_YR1"].Caption = string.Format("Approved {0}", MonthCurrentYear);
                            gvBudgetView.Columns["colActual"].Caption = String.Format("Actual {0}", MonthPreviousYear);
                            gvBudgetView.Columns["colAPPROVED_PREVIOUS_YR"].Caption = String.Format("Approved {0}", MonthPreviousYear);
                            gvBudgetView.Columns["BALANCE"].Caption = String.Format("Balance {0}", MonthPreviousYear);
                            gvBudgetView.Columns["colM2PROPOSED_AMOUNT"].Caption = String.Format("Proposed {0}", MonthNextYear);
                            gvBudgetView.Columns["colM2APPROVED_AMOUNT"].Caption = String.Format("Approved {0}", MonthNextYear);

                            lblBudgetName.Text = dtBudget.Rows[0][this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(lblBudgetName.Text))
                            {
                                lblPersonName.Visible = lblRole.Visible = true;
                                lblDateFrom.Text = this.Member.DateSet.ToDate(dtBudget.Rows[0][this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString());
                                lblDateTo.Text = this.Member.DateSet.ToDate(dtBudget.Rows[0][this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString());
                                if (IsTwoMonthBudget)
                                    lblDateTo.Text = this.Member.DateSet.ToDate(dtBudget.Rows[0][this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString(), true).AddMonths(2).AddDays(-1).ToShortDateString();
                                BudgetAction budetaction = (BudgetAction)this.Member.NumberSet.ToInteger(dtBudget.Rows[0][this.AppSchema.Budget.BUDGET_ACTIONColumn.ColumnName].ToString());
                                lblBudgetStatus.Text = budetaction.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadGrid()
        {
            try
            {
                BudgetDetails.DefaultView.Sort = "BUDGET_GROUP_ID, SORT_ID, MAIN_LEDGER_NAME, SUB_LEDGER_NAME";
                gvBudgetView.DataSource = BudgetDetails.DefaultView.ToTable();
                gvBudgetView.DataBind();
                gvBudgetView.ExpandAll();
                BudgetDetails = BudgetDetails.DefaultView.ToTable();
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
                GridViewDataColumn colLedger = gvBudgetView.Columns["LEDGER_ID"] as GridViewDataColumn;
                GridViewDataColumn colSubLedger = gvBudgetView.Columns["SUB_LEDGER_ID"] as GridViewDataColumn;
                GridViewDataColumn colM1ApprovedAmount = gvBudgetView.Columns["APPROVED_CURRENT_YR"] as GridViewDataColumn;
                GridViewDataColumn colM2ApprovedAmount = gvBudgetView.Columns["M2_APPROVED_AMOUNT"] as GridViewDataColumn;
                GridViewDataColumn colHONarration = gvBudgetView.Columns["HO_NARRATION"] as GridViewDataColumn;
                decimal M1ApprovedAmt = 0;
                decimal M2ApprovedAmt = 0;
                string HONarration = string.Empty;
                Int32 LedgerId = 0;
                Int32 SubLedgerId = 0;
                for (int i = 0; i < gvBudgetView.VisibleRowCount; i++)
                {
                    object value = gvBudgetView.GetRowValues(i, new String[] { "LEDGER_ID" });
                    LedgerId = 0;
                    if (value != null)
                    {
                        LedgerId = this.Member.NumberSet.ToInteger(value.ToString());
                    }

                    SubLedgerId = 0;
                    value = gvBudgetView.GetRowValues(i, new String[] { "SUB_LEDGER_ID" });
                    if (value != null)
                    {
                        SubLedgerId = this.Member.NumberSet.ToInteger(value.ToString());
                    }

                    if (gvBudgetView.FindRowCellTemplateControl(i, colM1ApprovedAmount, "txtM1ApprovedAmount") != null)
                    {
                        ASPxSpinEdit txtM1Approved = gvBudgetView.FindRowCellTemplateControl(i, colM1ApprovedAmount, "txtM1ApprovedAmount") as ASPxSpinEdit;
                        M1ApprovedAmt = base.LoginUser.NumberSet.ToDecimal(txtM1Approved.Text);
                    }

                    if (gvBudgetView.FindRowCellTemplateControl(i, colM2ApprovedAmount, "txtM2ApprovedAmount") != null)
                    {
                        ASPxSpinEdit txtM2Approved = gvBudgetView.FindRowCellTemplateControl(i, colM2ApprovedAmount, "txtM2ApprovedAmount") as ASPxSpinEdit;
                        M2ApprovedAmt = base.LoginUser.NumberSet.ToDecimal(txtM2Approved.Text);
                    }

                    if (gvBudgetView.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") != null)
                    {
                        ASPxTextBox txtHONarration = gvBudgetView.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") as ASPxTextBox;
                        HONarration = txtHONarration.Text;
                    }

                    if (LedgerId > 0)
                    {
                        BudgetDetails.DefaultView.RowFilter = "LEDGER_ID=" + LedgerId + " AND SUB_LEDGER_ID=" + SubLedgerId;
                        if (BudgetDetails.DefaultView.Count == 1)
                        {
                            BudgetDetails.DefaultView[0]["APPROVED_CURRENT_YR"] = M1ApprovedAmt;
                            BudgetDetails.DefaultView[0]["M2_APPROVED_AMOUNT"] = M2ApprovedAmt;
                            BudgetDetails.DefaultView[0]["HO_NARRATION"] = HONarration;
                            BudgetDetails.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                BudgetDetails.DefaultView.RowFilter = "";
                this.Message = "Unable update in grid " + err.Message;
            }
            BudgetDetails.DefaultView.RowFilter = "";
            LoadGrid();
        }

        private void AttachRecurringGroup(DataTable dtBindData)
        {
            //Int32 pfGrpOrder = getGroupNumber(1);
            //Int32 esicGrpOrder =pfGrpOrder + getGroupNumber(2);

            DataRow dr = dtBindData.NewRow();
            dr["BUDGET_GROUP_ID"] = 1;
            dr["MAIN_LEDGER_NAME"] = "  Salary";
            dr["LEDGER_NAME"] = "  Salary";
            dr["SORT_ID"] = "0";
            dtBindData.Rows.Add(dr);

            dr = dtBindData.NewRow();
            dr["BUDGET_GROUP_ID"] = 1;
            dr["MAIN_LEDGER_NAME"] = "  b. P.F :";
            dr["LEDGER_NAME"] = "  b. P.F :";
            dr["SORT_ID"] = "2";
            dtBindData.Rows.Add(dr);

            dr = dtBindData.NewRow();
            dr["BUDGET_GROUP_ID"] = 1;
            dr["MAIN_LEDGER_NAME"] = "  c. ESIC :";
            dr["LEDGER_NAME"] = "  c. ESIC :";
            dr["SORT_ID"] = "5";
            dtBindData.Rows.Add(dr);
        }


        /// <summary>
        /// get SalaryGrp, PF Grp record numbers
        /// 
        /// grpnumber=1- Salary
        /// grpnumber=2 - PF
        /// grpnumber =3 ESIC
        /// </summary>
        /// <returns></returns>
        private int getGroupNumber(int grpnumber)
        {
            Int32 rtn = grpnumber;
            BudgetDetails.DefaultView.RowFilter = string.Empty;
            try
            {
                if (BudgetDetails != null && BudgetDetails.Rows.Count > 0)
                {
                    BudgetDetails.DefaultView.RowFilter = "BUDGET_SUB_GROUP_ID=" + grpnumber;
                    rtn = BudgetDetails.DefaultView.Count;
                }
            }
            catch (Exception err)
            {
                BudgetDetails.DefaultView.RowFilter = string.Empty; ;
            }
            BudgetDetails.DefaultView.RowFilter = string.Empty;

            return (rtn + 1);
        }

        private string getAlphabetSerialNo()
        {
            string rtn = string.Empty;

            try
            {
                int pos = (RecAlphabetSerialNo == 1 || RecAlphabetSerialNo == 2) ? RecAlphabetSerialNo + 2 : RecAlphabetSerialNo;
                string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                rtn = alphabetArray.GetValue(pos).ToString();
                RecAlphabetSerialNo++;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn.ToLower();
        }

        private void MovePropssedToApproved()
        {
            DataTable dtSource = BudgetDetails;
            foreach (DataRow drROW in dtSource.Rows)
            {
                drROW["APPROVED_CURRENT_YR"] = this.Member.NumberSet.ToDecimal(drROW["PROPOSED_CURRENT_YR"].ToString());
                drROW["M2_APPROVED_AMOUNT"] = this.Member.NumberSet.ToDecimal(drROW["M2_PROPOSED_AMOUNT"].ToString());
                dtSource.AcceptChanges();
            }
            LoadGrid();
        }

        private void SetReportPreviewPropterty()
        {
            //Assign Properties for Print Preview
            objReportProperty.ReportId = "RPT-171"; //Budget Approval By Month
            objReportProperty.IsTwoMonthBudget = IsTwoMonthBudget;
            objReportProperty.DateFrom = BudgetDateFrom;

            if (IsTwoMonthBudget)
            {
                objReportProperty.DateTo = NextBudgetDateTo;
            }
            else
            {
                objReportProperty.DateTo = BudgetDateTo;
            }

            objReportProperty.PrevDateFrom = this.Member.DateSet.ToDate(PrevBudgetDateFrom, "yyyy-MM-dd");
            objReportProperty.PrevDateTo = this.Member.DateSet.ToDate(PrevBudgetDateTo, "yyyy-MM-dd"); ;
            objReportProperty.ProjectId = ProjectId.ToString();
            objReportProperty.BudgetId = BudgetId.ToString() + "," + M2BudgetId.ToString();
            objReportProperty.Budget = BudgetId.ToString() + "," + M2BudgetId.ToString();
            objReportProperty.BranchOffice = BranchId.ToString();
            objReportProperty.ReportTitle = "";

            if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
            {
                DateTime dePeriodFrom = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_FROM"].ToString(), false);
                DateTime dePeriodTo = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_TO"].ToString(), false);
                string Previous = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).AddMonths(-1).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                string Current = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                string NextMonth = new DateTime(this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Year, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).AddMonths(1).Month, this.Member.DateSet.ToDate(dePeriodFrom.ToString(), false).Day).ToString("MMM", CultureInfo.InvariantCulture);

                string MonthPreviousYear = String.Format("{0}-{1}", Previous, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).AddMonths(-1).Year);
                string MonthCurrentYear = String.Format("{0}-{1}", Current, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).Year);
                string MonthNextYear = String.Format("{0}-{1}", NextMonth, Member.DateSet.ToDate(dePeriodFrom.ToString(), true).AddMonths(1).Year);

                objReportProperty.BudgetPrevDateCaption = MonthPreviousYear;
                objReportProperty.BudgetM1PropsedDateCaption = MonthCurrentYear;
                objReportProperty.BudgetM2PropsedDateCaption = MonthNextYear;
            }

            objReportProperty.BudgetName = cmbBudget.SelectedItem == null ? string.Empty : cmbBudget.SelectedItem.Text;
            objReportProperty.Project = cmbProject.SelectedItem == null ? string.Empty : cmbProject.SelectedItem.Text;
            objReportProperty.BranchOfficeName = cmbBranch.SelectedItem == null ? string.Empty : cmbBranch.SelectedItem.Text;
            objReportProperty.SaveReportSetting();

            Session["REPORTPROPERTY"] = objReportProperty;
        }

        private void SetTitle()
        {
            lblSocietyName.Text = lblSocietyAddress.Text = lblReportTitle.Text = string.Empty;
            lblMonth.Text = lblBranchName.Text = lblProject.Text = string.Empty;
            btnPrint.Visible = btnDownload.Visible = btnPrintPreview.Visible = false;

            //Set Society name as Title
            if (ProjectId > 0 && BudgetId > 0 && !string.IsNullOrEmpty(BudgetDateFrom) && !string.IsNullOrEmpty(BudgetDateTo))
            {
                using (LegalEntitySystem legalsys = new LegalEntitySystem())
                {
                    ResultArgs result = legalsys.FetchSocietyByProject(ProjectId.ToString());
                    if (result.Success && result != null)
                    {
                        DataTable dtLegalEntity = result.DataSource.Table as DataTable;
                        if (dtLegalEntity.Rows.Count > 0)
                        {
                            lblSocietyName.Text = dtLegalEntity.Rows[0]["SOCIETYNAME"].ToString().Trim(); //Mysore Diocesan Educational Society";
                            lblSocietyAddress.Text = dtLegalEntity.Rows[0]["ADDRESS"].ToString().Trim();

                            lblReportTitle.Text = "Monthly Budget and Expenses";
                            lblMonth.Text = "Month : Budget for the month of " + this.Member.DateSet.ToDate(BudgetDateFrom, "MMM yyyy");
                            lblBranchName.Text = "Institute : " + (cmbBranch.SelectedItem == null ? string.Empty : cmbBranch.SelectedItem.Text);
                            lblProject.Text = cmbProject.SelectedItem == null ? string.Empty : cmbProject.SelectedItem.Text;

                            string banknames = GetBankNamesByProject();
                            if (!string.IsNullOrEmpty(banknames))
                            {
                                lblProject.Text = lblProject.Text + " (" + banknames + ")";
                            }
                        }
                    }
                }

                btnPrintPreview.Visible = btnDownload.Visible = true;
            }
            SetFooterBalance();
        }

        private void SetFooterBalance()
        {
            ClearFooter();

            //Set Society name as Title
            if (ProjectId > 0 && BudgetId > 0 && !string.IsNullOrEmpty(BudgetDateFrom) && !string.IsNullOrEmpty(BudgetDateTo))
            {
                double TotalPrevBudgetedAmt = 0;
                double TotalPrevBudgetActual = 0;
                double TotalPreviousBudgetDiffBalance = 0;

                double StatementBankBalance = 0;
                double NotMatrilzedAmountInBank = 0;
                double StatementCBBankBalance = 0;

                double UnRealizedAmt = 0;
                double UnClearedAmt = 0;

                double TotalM1PropsedAmount = 0;
                double TotalM2PropsedAmount = 0;

                lblOPCaption.Visible = lblBanKBalancePerBankCaption.Visible = lblBankBalanceCBCaption.Visible = true;
                lblBRSCaption1.Visible = lblBRSCaption.Visible = lblTotalBudgetCaption.Visible = lblBalanceCaption.Visible = true;
                lblMainDifferenceCaption.Visible = lblTotalBudgetCaption.Visible = lblBalanceCaption.Visible = lblDifferenceCaption.Visible = true;

                using (BalanceSystem1 balance = new BalanceSystem1())
                {
                    //Get Bank Closing Balance
                    Bosco.Report.Base.BalanceProperty bankclBalance = balance.GetBankBalance(ProjectId.ToString(), BudgetDateTo, BalanceSystem1.BalanceType.ClosingBalance, "");
                    if (bankclBalance.Result.Success)
                    {
                        StatementCBBankBalance = bankclBalance.Amount;
                    }

                    //Get BRS
                    ResultArgs resultargs = GetBRSStatement();
                    if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtBRSList = resultargs.DataSource.Table;

                        UnRealizedAmt = this.Member.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnRealised)", "").ToString());
                        UnClearedAmt = this.Member.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnCleared)", "").ToString());
                    }

                    //# Bank Balance
                    StatementBankBalance = 0;
                    StatementBankBalance = StatementCBBankBalance - UnRealizedAmt;
                    StatementBankBalance += UnClearedAmt;

                    NotMatrilzedAmountInBank = 0;
                    NotMatrilzedAmountInBank = UnRealizedAmt + UnClearedAmt;

                    if (BudgetDetails != null && BudgetDetails.Rows.Count > 0)
                    {
                        object oAmount = BudgetDetails.Compute("SUM(APPROVED_PREVIOUS_YR)", string.Empty);
                        TotalPrevBudgetedAmt = this.Member.NumberSet.ToDouble(oAmount.ToString());

                        oAmount = BudgetDetails.Compute("SUM(ACTUAL)", string.Empty);
                        TotalPrevBudgetActual = this.Member.NumberSet.ToDouble(oAmount.ToString());

                        oAmount = BudgetDetails.Compute("SUM(BALANCE)", string.Empty);
                        TotalPreviousBudgetDiffBalance = this.Member.NumberSet.ToDouble(oAmount.ToString());

                        oAmount = BudgetDetails.Compute("SUM(PROPOSED_CURRENT_YR)", string.Empty);
                        TotalM1PropsedAmount = this.Member.NumberSet.ToDouble(oAmount.ToString());

                        if (IsTwoMonthBudget)
                        {
                            oAmount = BudgetDetails.Compute("SUM(M2_PROPOSED_AMOUNT)", string.Empty);
                            TotalM2PropsedAmount = this.Member.NumberSet.ToDouble(oAmount.ToString());
                        }
                    }

                    lblOP.Text = this.Member.NumberSet.ToNumber(TotalPrevBudgetedAmt);
                    lblBanKBalancePerBank.Text = this.Member.NumberSet.ToNumber(StatementBankBalance);
                    lblBankBalanceCB.Text = this.Member.NumberSet.ToNumber(StatementCBBankBalance);
                    lblBRS1.Text = this.Member.NumberSet.ToNumber(NotMatrilzedAmountInBank);
                    lblTotalBudget.Text = this.Member.NumberSet.ToNumber(TotalM1PropsedAmount + TotalM2PropsedAmount);
                    lblBalance.Text = this.Member.NumberSet.ToNumber(TotalPreviousBudgetDiffBalance);
                    lblMainDifference.Text = this.Member.NumberSet.ToNumber((TotalM1PropsedAmount + TotalM2PropsedAmount) - (TotalPrevBudgetedAmt - TotalPrevBudgetActual));

                    if (IsTwoMonthBudget)
                    {
                        lblTotalBudgetCaption.Text = "Total Budget of " + this.Member.DateSet.ToDate(BudgetDateFrom, "MMM") + " & " + this.Member.DateSet.ToDate(NextBudgetDateTo, "MMM yyyy");
                    }
                    else
                    {
                        lblTotalBudgetCaption.Text = "Total Budget of " + this.Member.DateSet.ToDate(BudgetDateFrom, "MMM yyyy");
                    }

                    lblBalanceCaption.Text = "Balance from Previous Month Budget " + this.Member.DateSet.ToDate(PrevBudgetDateFrom, "MMM yyyy");


                }
            }
        }

        private void ClearFooter()
        {
            lblOP.Text = lblBanKBalancePerBank.Text = lblBankBalanceCB.Text = string.Empty;
            lblBRS1.Text = lblTotalBudget.Text = lblBalance.Text = string.Empty;
            lblMainDifference.Text = lblTotalBudgetCaption.Text = lblBalanceCaption.Text = lblDifference.Text = string.Empty;

            lblOPCaption.Visible = lblBanKBalancePerBankCaption.Visible = lblBankBalanceCBCaption.Visible = false;
            lblBRSCaption1.Visible = lblBRSCaption.Visible = lblTotalBudgetCaption.Visible = lblBalanceCaption.Visible = false;
            lblMainDifferenceCaption.Visible = lblTotalBudgetCaption.Visible = lblBalanceCaption.Visible = lblDifferenceCaption.Visible = false;
        }

        private string GetBankNamesByProject()
        {
            ResultArgs resultArgs = new ResultArgs();
            string rtn = string.Empty;
            string bankclosedDate = BudgetDateFrom;

            if (ProjectId > 0 && BudgetId > 0 && !string.IsNullOrEmpty(BudgetDateFrom) && !string.IsNullOrEmpty(BudgetDateTo))
            {
                if (!string.IsNullOrEmpty(BudgetDateFrom))
                {
                    bankclosedDate = this.Member.DateSet.ToDate(BudgetDateFrom, false).AddMonths(-1).ToShortDateString();
                }

                using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankByProject, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    if (!string.IsNullOrEmpty(bankclosedDate))
                    {
                        dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, bankclosedDate);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtBankNames = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtBankNames.Rows)
                        {
                            rtn += dr[this.AppSchema.Bank.BANKColumn.ColumnName].ToString() + ",";
                        }
                        rtn = rtn.TrimEnd(',');
                    }
                }
            }

            return rtn;
        }

        private ResultArgs GetBRSStatement()
        {
            ResultArgs resultargs = new ResultArgs();

            //# Get BRS details still previous budget date to
            if (ProjectId > 0 && BudgetId > 0 && !string.IsNullOrEmpty(BudgetDateFrom) && !string.IsNullOrEmpty(BudgetDateTo))
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRSByMaterialized, DataBaseType.HeadOffice))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add("DATE_AS_ON", this.Member.DateSet.ToDate(PrevBudgetDateTo, false), DataType.DateTime);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                }
            }
            return resultargs;
        }

        private bool FetchOneMonthTwoMonthStatus()
        {
            bool rtn = false;
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                budgetsystem.BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                rtn = budgetsystem.isOneTwoMonthBudgetstatus();
            }
            return rtn;
        }

        #endregion

    }
}