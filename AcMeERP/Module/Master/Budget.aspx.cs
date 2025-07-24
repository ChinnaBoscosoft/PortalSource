/*****************************************************************************************************
 * Created by       : chinna
 * Created On       : 9th June 2016
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

namespace AcMeERP.Module.Master
{
    public partial class Budget : Base.UIBase
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

        #endregion

        #region Properties
        private int branchid = 0;
        private int BranchId
        {
            set { branchid = value; }
            get { return branchid; }
        }

        /*private int projectid = 0;
        private int ProjectId
        {
            set { projectid = value; }
            get { return projectid; }
        }*/

        private string projectids = "0";
        private string ProjectIds
        {
            set { projectids = value; }
            get { return projectids; }
        }

        private int budgetId = 0;
        private int BudgetId
        {
            set { budgetId = value; }
            get { return budgetId; }
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
                    AllModifyApprovedAmount = false;
                }
                else
                {
                    lblBudgetStatus.Text = BudgetAction.Recommended.ToString();
                    btnApproved.Text = "Approve";
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

        private DataTable TempBudget
        {
            set { ViewState["TempBud"] = value; }
            get { return (DataTable)ViewState["TempBud"]; }
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

        double totalBudgetedRawProposedIncome = 0;
        public double TotalBudgetedRawProposedIncome
        {
            get
            {
                return totalBudgetedRawProposedIncome;
            }
            set
            {
                totalBudgetedRawProposedIncome = value;
            }
        }

        double totalProposedBudgetedExpenseordinory = 0;
        public double TotalProposedBudgetedExpenseOrdinary
        {
            get
            {
                return totalProposedBudgetedExpenseordinory;
            }
            set
            {
                totalProposedBudgetedExpenseordinory = value;
            }
        }

        double totalProposedBudgetedExpenseextraordinory = 0;
        public double TotalProposedBudgetedExpenseExtraOrdinary
        {
            get
            {
                return totalProposedBudgetedExpenseextraordinory;
            }
            set
            {
                totalProposedBudgetedExpenseextraordinory = value;
            }
        }

        double totalProposedBudgetedExpense = 0;
        public double TotalProposedBudgetedExpense
        {
            get
            {
                return totalProposedBudgetedExpense;
            }
            set
            {
                totalProposedBudgetedExpense = value;
            }
        }
        double totalbudgetedRawapprovedIncome = 0;
        public double TotalApprovedRawIncome
        {
            get
            {
                return totalbudgetedRawapprovedIncome;
            }
            set
            {
                totalbudgetedRawapprovedIncome = value;
            }
        }

        double totalapprovebudgetedExpenseordinary = 0;
        public double TotalApprovedBudgetedExpenseOrdinary
        {
            get
            {
                return totalapprovebudgetedExpenseordinary;
            }
            set
            {
                totalapprovebudgetedExpenseordinary = value;
            }
        }

        double totalapprovebudgetedExpenseExtraOrdinary = 0;
        public double TotalApprovedBudgetedExpenseExtraOrdinary
        {
            get
            {
                return totalapprovebudgetedExpenseExtraOrdinary;
            }
            set
            {
                totalapprovebudgetedExpenseExtraOrdinary = value;
            }
        }

        double totalapprovebudgetedExpenseonly = 0;
        public double TotalApprovedBudgetedExpensesOnly
        {
            get
            {
                return totalapprovebudgetedExpenseonly;
            }
            set
            {
                totalapprovebudgetedExpenseonly = value;
            }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnPrintPreview.Visible = btnDownload.Visible = false;
                lblPersonName.Visible = lblRole.Visible = false;
                lblBudgetNameCaption.Visible = lblDateFromCaption.Visible = lblDateToCaption.Visible = lblProjectNameCaption.Visible = false;

                LoadBranches();
                LoadBudget();
                //LoadProjects();
                LoadBudgetLedgerDetails();
                ShowLoadWaitPopUp(btnLoad);
                ShowLoadWaitPopUp(btnApproved);

                ShowLoadWaitPopUp(btnSave);

                lblBudgetName.Text = string.Empty;
                lblDateFrom.Text = string.Empty;
                lblDateTo.Text = string.Empty;
                lblBudgetStatus.Text = string.Empty;
                lblDetailCashBankBalance.Text = "";
                lblProjectName.Text = string.Empty;
                gvCashBankDetails.DataSource = null;

                lblPersonName.Text = this.LoginUser.LoginUserHeadOfficeInchargeName;
                lblRole.Text = this.LoginUser.LoginUserHeadOfficeInchargeDesignation;
                BindCashBankFDDetails();
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            lblBudgetNameCaption.Visible = lblDateFromCaption.Visible = lblDateToCaption.Visible = lblProjectNameCaption.Visible = true;

            LoadBudgetInformation();
            LoadBudgetLedgerDetails();
            BindCashBankFDDetails();
            btnPrintPreview.Visible = btnDownload.Visible = true;

            // ShowCMFNoteToProvince();
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
            string DateFrom = string.Empty;
            if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
            {
                DateFrom = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_FROM"].ToString());
                ProjectIds = BudgetInfo.Rows[0]["PROJECT_ID"].ToString();
            }

            //int proId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());

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
                    lblDetailCashBankBalance.Text = "<b>Opening Balance as on " + lblDateFrom.Text + "</b>";
                    //ShowCashBankRowDetailBalance(dtBalance);
                    gvCashBankDetails.Visible = true;
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
            lblDetailCashBankBalance.Text = "<b>Opening Balance as on " + lblDateFrom.Text + "</b>";
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

        private DataTable FetchAccounts(int GroupId)
        {
            ResultArgs resultArgs = new ResultArgs();
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                string BalanceDate = string.Empty;
                int BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                //int ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
                {
                    BalanceDate = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_FROM"].ToString());
                }
                voucherTransSystem.ProjectIDs = ProjectIds;
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
            //LoadProjects();
            LoadBudget();
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    UpdateGridValue();
        //    SaveBudget(false);
        //    this.Message = MessageCatalog.Message.SaveConformation;
        //}

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            if (ValidateBudgetDetails())
            {
                if (AllModifyApprovedAmount)//(btnApproved.Text == "Approved")
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
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateBudgetDetails())
            {
                if (AllModifyApprovedAmount)
                {
                    UpdateGridValue();
                    SaveBudget(true, 1);
                    this.Message = MessageCatalog.Message.SaveConformation;
                    BudgetStatus = BudgetStatus == BudgetAction.Approved ? BudgetAction.Approved : BudgetAction.Recommended;
                    btnSave.ClientSideEvents.Click = "function(s, e) { e.processOnServer = true; }";
                    LoadGrid(BudgetDetails);
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
                using (BudgetSystem budgetsystem = new BudgetSystem())
                {
                    BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    //ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                    BudgetId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                    gvBudgetView.UpdateEdit();
                    budgetsystem.dtBudgetLedgers = BudgetDetails;
                    budgetsystem.BudgetId = BudgetId;
                    //budgetsystem.ProjectId = ProjectId;
                    budgetsystem.ProjectIds = ProjectIds;
                    budgetsystem.BudgetAction = BudgetAction;
                    budgetsystem.BudgetActionValue = BudgetStatus == Bosco.Utility.BudgetAction.Approved ? 2 : BudgetStatusId;
                    resultArgs = budgetsystem.SaveBudgetDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (BudgetAction && BudgetStatusId != 1)
                        {
                            this.Message = "Budget is " + MessageCatalog.Message.ApproveConfirmation;

                            //Sending mail 
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
            bool isValid = true;
            //int SelectedProId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
            int SelectedBudId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
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
                cmbBudget.Focus();
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

        protected void gvBudgetView_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //Approved Amount will not be modified once budget is approved, but they wish they can use edit button option and modify
            GridViewDataColumn colApprovedAmount = gvBudgetView.Columns["APPROVED_CURRENT_YR"] as GridViewDataColumn;
            GridViewDataColumn colNarration = gvBudgetView.Columns["HO_NARRATION"] as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data)
            {
                if (gvBudgetView.FindRowCellTemplateControl(e.VisibleIndex, colApprovedAmount, "txtSpEdit") != null)
                {
                    ASPxSpinEdit txtApproved = gvBudgetView.FindRowCellTemplateControl(e.VisibleIndex, colApprovedAmount, "txtSpEdit") as ASPxSpinEdit;
                    ASPxTextBox txtNarration = gvBudgetView.FindRowCellTemplateControl(e.VisibleIndex, colNarration, "txtSpEditNarration") as ASPxTextBox;
                    txtApproved.Enabled = txtNarration.Enabled = AllModifyApprovedAmount;
                }
            }
        }

        protected void gvBudgetView_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //if (gvBudgetView.DataSource != null)
            //{
            ASPxGridView grid = sender as ASPxGridView;
            if (grid.GroupCount == 0)
                return;

            //if (e.RowType == GridViewRowType.Data && e.Row.Cells.Count > 1)
            //{
            //    e.Row.Cells[grid.GroupCount - 1].Visible = false;
            //    e.Row.Cells[grid.GroupCount].ColumnSpan = 2;
            //}
            //if (e.RowType == GridViewRowType.GroupFooter && e.Row.Cells.Count > 1)
            //{
            //    e.Row.Cells[grid.GroupCount - 1].Visible = false;
            //    e.Row.Cells[grid.GroupCount].ColumnSpan = 2;
            //}

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
                    GridViewDataColumn col = gvBudgetView.GetGroupedColumns()[level];

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
                            //if (e.Row.Cells[0].Text.Trim().ToUpper() == "CR")
                            //{
                            //    e.Row.Cells[0].Text = "Income Ledgers";
                            //}
                            //else if (e.Row.Cells[0].Text.Trim().ToUpper() == "DR")
                            //{
                            //    e.Row.Cells[0].Text = "Expenses Ledgers";
                            //}
                        }
                        else if (e.RowType == GridViewRowType.GroupFooter)
                        {
                            e.Row.Visible = false;
                            e.Row.Attributes["style"] = "display:none";
                        }
                    }
                    else if (col.FieldName == "GROUP_FOR_GRID")
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

                        if (rowlevel == 0 && e.Row.Cells.Count > 3)
                        {
                            e.Row.Cells[4].Text = budgettransmode.ToUpper() == "CR" ? "Income Ledgers Total" : "Expenses Ledgers Total";

                            if (e.Row.Cells[4].Text.Equals("Income Ledgers Total") || e.Row.Cells[4].Text.Equals("Expenses Ledgers Total"))
                            {
                                TableCell cell = new TableCell();
                                cell.Text = "";
                                e.Row.Cells.AddAt(5, cell);
                            }
                            //e.Row.Cells[4].Font.Bold = true;
                        }
                        else if (budgettransmode.ToUpper() == "CR" && rowlevel > 0)
                        {
                            e.Row.Visible = false;
                            e.Row.Attributes["style"] = "display:none";
                        }
                        else if (budgettransmode.ToUpper() == "DR" && rowlevel > 0)
                        {
                            string budgetgroup = drv["GROUP_FOR_GRID"].ToString();
                            string budgetsubgroup = drv["BUDGET_SUB_GROUP"].ToString();

                            e.Row.Visible = false;
                            e.Row.Attributes["style"] = "display:none";

                            /*if (String.IsNullOrEmpty(budgetgroup))
                            {
                                e.Row.Visible = false;
                                e.Row.Attributes["style"] = "display:none";
                            }
                            else if (string.IsNullOrEmpty(budgetsubgroup))
                            {
                                e.Row.Visible = false;
                                e.Row.Attributes["style"] = "display:none";
                            }*/
                        }
                    }
                }

                //else if (e.RowType == GridViewRowType.Data)
                //{
                //    e.Row.Cells[e.Row.Cells.Count - 1].ColumnSpan =1;
                //}
                //if (e.RowType == GridViewRowType.GroupFooter && e.Row.Cells.Count > 1)
                //{
                //    e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                //    e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;
                //}
                //}
            }
        }

        protected void gvBudgetView_Load(object sender, EventArgs e)
        {
            gvBudgetView.DataSource = BudgetDetails;
            gvBudgetView.DataBind();
        }

        #endregion

        #region Methods
        private void LoadBudgetLedgerDetails()
        {
            gvBudgetView.Settings.GroupFormat = "{1}";
            if (this.HeadOfficeCode.ToUpper() != "ABEBEN")
            {
                gvBudgetView.Settings.GroupFormat = "{1}";
            }

            btnApproved.Visible = false;
            btnSave.Visible = btnMove.Visible = false;
            try
            {
                BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                //ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                ProjectIds = "0";
                BudgetId = cmbBudget.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
                using (BudgetSystem budget = new BudgetSystem())
                {
                    if (BudgetId > 0)
                    {
                        budget.BranchId = BranchId;
                        //budget.ProjectId = ProjectId;
                        budget.BudgetId = BudgetId;
                        budget.BudgetTransMode = "DR";

                        if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
                        {
                            ProjectIds = BudgetInfo.Rows[0]["PROJECT_ID"].ToString();
                            budget.ProjectIds = BudgetInfo.Rows[0]["PROJECT_ID"].ToString();
                            budget.DateFrom = BudgetInfo.Rows[0]["DATE_FROM"].ToString();
                            budget.DateTo = BudgetInfo.Rows[0]["DATE_TO"].ToString();
                            BudgetStatus = (BudgetAction)this.Member.NumberSet.ToInteger(BudgetInfo.Rows[0][this.AppSchema.Budget.BUDGET_ACTIONColumn.ColumnName].ToString());
                        }
                        resultArgs = budget.FetchBudgetEdit();
                        if (resultArgs.Success)
                        {
                            //28/05/2020, to have Budget Income and Expense Ledgers
                            //BudgetDetails = resultArgs.DataSource.Table;

                            //For Expense Ledgers
                            DataTable dtExpenseLedgers = resultArgs.DataSource.Table;
                            BudgetDetails = dtExpenseLedgers;

                            //For Income Ledgers
                            //if (this.HeadOfficeCode.ToUpper() != "ABEBEN")
                            //{
                            //    //budget.BudgetTransMode = "CR";
                            //    //resultArgs = budget.FetchBudgetEdit();
                            //    //if (resultArgs.Success)
                            //    //{
                            //    //    DataTable dtIncomeLedgers = resultArgs.DataSource.Table;
                            //    //    BudgetDetails.Merge(dtIncomeLedgers);
                            //    //}
                            //}

                            //If Budget Status is recommanded, Show Approved amount as propsed amount in the grid

                            // command by chinna ( 13.01.2024)
                            // since it replaced into proposed to approved always if recommanded

                            //if (BudgetStatus == BudgetAction.Recommended)
                            //{
                            //    MovePropssedToApproved();
                            //}


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
                    else
                    {
                        gvBudgetView.DataSource = null;
                        gvBudgetView.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void ShowCMFNoteToProvince()
        {
            lblBudgetNote.Text = string.Empty;

            if (LoginUser.IS_CMF_CONGREGATION)
            {
                double ordinaryExprense = totalProposedBudgetedExpenseordinory;
                double extraOrdinaryExpense = totalProposedBudgetedExpenseextraordinory;
                double budgetexpense = TotalProposedBudgetedExpense;
                //  double amtDifference = ordinaryExprense - (totalBudgetedRawProposedIncome + totalOpeningBalance);

                double GrandApprovedBudgetedExpenses = (TotalApprovedBudgetedExpenseOrdinary + TotalApprovedBudgetedExpenseExtraOrdinary + TotalApprovedBudgetedExpensesOnly);
                double amtDifference = GrandApprovedBudgetedExpenses - (TotalApprovedRawIncome + totalOpeningBalance);
                if (amtDifference != 0)
                {
                    lblBudgetNote.Visible = true;
                    string notemsg = string.Empty;
                    notemsg = " " + System.Environment.NewLine;
                    notemsg = System.Environment.NewLine + "BUDGET NOTE: " + System.Environment.NewLine;
                    notemsg += "Opening Balance             : " + Member.NumberSet.ToNumber(totalOpeningBalance) + System.Environment.NewLine;
                    notemsg += "Budgeted Proposed Income    : " + Member.NumberSet.ToNumber(totalBudgetedRawProposedIncome) + System.Environment.NewLine;

                    //notemsg += "Budgeted Ordinary Expenses  : " + Member.NumberSet.ToNumber(ordinaryExprense) + System.Environment.NewLine + System.Environment.NewLine;
                    notemsg += "Budgeted Ordinary Expenses  : " + Member.NumberSet.ToNumber(ordinaryExprense) + System.Environment.NewLine;
                    notemsg += "Budgeted ExtraOrdinary Expenses  : " + Member.NumberSet.ToNumber(extraOrdinaryExpense) + System.Environment.NewLine;
                    notemsg += "Budgeted Expenses  : " + Member.NumberSet.ToNumber(budgetexpense) + System.Environment.NewLine;
                    notemsg += "Total Proposed Expenses  : " + Member.NumberSet.ToNumber(ordinaryExprense + extraOrdinaryExpense + budgetexpense) + System.Environment.NewLine + System.Environment.NewLine;

                    notemsg += "Budgeted Approved Income    : " + Member.NumberSet.ToNumber(TotalApprovedRawIncome) + System.Environment.NewLine;

                    notemsg += "Approved Ordinary Expenses  : " + Member.NumberSet.ToNumber(TotalApprovedBudgetedExpenseOrdinary) + System.Environment.NewLine;
                    notemsg += "Approved ExtraOrdinary Expenses  : " + Member.NumberSet.ToNumber(TotalApprovedBudgetedExpenseExtraOrdinary) + System.Environment.NewLine;
                    notemsg += "Budgeted Approved Expenses  : " + Member.NumberSet.ToNumber(TotalApprovedBudgetedExpensesOnly) + System.Environment.NewLine;
                    notemsg += "Total Approved Expenses  : " + Member.NumberSet.ToNumber(TotalApprovedBudgetedExpenseOrdinary + TotalApprovedBudgetedExpenseExtraOrdinary + TotalApprovedBudgetedExpensesOnly) + System.Environment.NewLine;
                    notemsg += System.Environment.NewLine;

                    if (amtDifference > 0)
                    {
                        notemsg += "Deficit contribute by Province : " + Member.NumberSet.ToNumber(Math.Abs(amtDifference));
                    }
                    else
                    {
                        notemsg += "Surplus transfer to Province : " + Member.NumberSet.ToNumber(Math.Abs(amtDifference));
                    }
                    notemsg += System.Environment.NewLine;

                    lblBudgetNote.Text = notemsg;
                    //    lblBudgetNote.Font = new Font(xrcellGrandTotal.Font.FontFamily, 12, FontStyle.Bold);
                    //lblBudgetNote. = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

                }
            }
        }

        private void LoadBranches()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranchByBudget(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
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
                                if (cmbBranch.Items.FindByValue(Session[base.DefaultBranchId].ToString()) != null)
                                {
                                    cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                                }
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

        //private void LoadProjects()
        //{
        //    try
        //    {
        //        using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
        //        {
        //            BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
        //            resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
        //            this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, AppSchema.Project.PROJECTColumn.ColumnName, AppSchema.Project.PROJECT_IDColumn.ColumnName, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Message = ex.Message;
        //    }
        //}

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
                        resultArgs = BranchOffiecSystem.FetchBudget(BranchId, datefrom, dateto);
                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            this.Member.ComboSet.BindCombo(cmbBudget, resultArgs.DataSource.Table, AppSchema.Budget.BUDGET_NAMEColumn.ColumnName, AppSchema.Budget.BUDGET_IDColumn.ColumnName, false);
                        }
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
            lblBudgetName.Text = string.Empty;
            lblDateFrom.Text = string.Empty;
            lblDateTo.Text = string.Empty;
            lblBudgetStatus.Text = string.Empty;
            lblDetailCashBankBalance.Text = string.Empty;
            lblProjectName.Text = string.Empty;
            gvCashBankDetails.DataSource = null;
            lblPersonName.Visible = lblRole.Visible = false;

            try
            {
                DataTable dtBudget = new DataTable();
                BranchId = cmbBranch.Items.Count == 0 || cmbBranch.SelectedItem == null ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                //ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                BudgetId = cmbBudget.Items.Count == 0 || cmbBudget.SelectedItem == null ? 0 : this.Member.NumberSet.ToInteger(cmbBudget.SelectedItem.Value.ToString());
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

                        if (LoginUser.IS_CMF_CONGREGATION)
                        {
                            gvBudgetView.Columns["colPROPOSED_CURRENT_YR"].Caption = String.Format("Proposed {0}", dePeriodFrom.Year.ToString());
                            gvBudgetView.Columns["colAPPROVED_CURRENT_YR"].Caption = string.Format("Approved {0}", dePeriodFrom.Year.ToString());
                            gvBudgetView.Columns["colAPPROVED_CURRENT_YR1"].Caption = string.Format("Approved {0}", dePeriodFrom.Year.ToString());
                            gvBudgetView.Columns["colActual"].Caption = String.Format("Actual {0}", (dePeriodFrom.Year - 1).ToString());
                            gvBudgetView.Columns["colAPPROVED_PREVIOUS_YR"].Caption = String.Format("Approved {0}", (dePeriodFrom.Year - 1).ToString());
                        }
                        else
                        {
                            gvBudgetView.Columns["colPROPOSED_CURRENT_YR"].Caption = String.Format("Proposed {0}", dePeriodFrom.Year.ToString() + "-" + dePeriodTo.Year.ToString());
                            gvBudgetView.Columns["colAPPROVED_CURRENT_YR"].Caption = string.Format("Approved {0}", dePeriodFrom.Year.ToString() + "-" + dePeriodTo.Year.ToString());
                            gvBudgetView.Columns["colAPPROVED_CURRENT_YR1"].Caption = string.Format("Approved {0}", dePeriodFrom.Year.ToString() + "-" + dePeriodTo.Year.ToString());
                            gvBudgetView.Columns["colActual"].Caption = String.Format("Actual {0}", (dePeriodFrom.Year - 1).ToString() + "-" + (dePeriodTo.Year - 1).ToString());
                            gvBudgetView.Columns["colAPPROVED_PREVIOUS_YR"].Caption = String.Format("Approved {0}", (dePeriodFrom.Year - 1).ToString() + "-" + (dePeriodTo.Year - 1).ToString());
                        }

                        lblBudgetName.Text = dtBudget.Rows[0][this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                        lblProjectName.Text = dtBudget.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        if (!string.IsNullOrEmpty(lblBudgetName.Text))
                        {
                            lblPersonName.Visible = lblRole.Visible = true;
                            lblDateFrom.Text = this.Member.DateSet.ToDate(dtBudget.Rows[0][this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString());
                            lblDateTo.Text = this.Member.DateSet.ToDate(dtBudget.Rows[0][this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString());
                            BudgetAction budetaction = (BudgetAction)this.Member.NumberSet.ToInteger(dtBudget.Rows[0][this.AppSchema.Budget.BUDGET_ACTIONColumn.ColumnName].ToString());
                            lblBudgetStatus.Text = budetaction.ToString();
                        }
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
            if (!dtBudget.Columns.Contains("GROUP_FOR_GRID"))
                dtBudget.Columns.Add("GROUP_FOR_GRID", typeof(string));

            foreach (DataRow row in dtBudget.Rows)
            {
                if (this.LoginUser.IS_BSG_CONGREGATION)
                    row["GROUP_FOR_GRID"] = row["LEDGER_GROUP"];
                else
                    row["GROUP_FOR_GRID"] = row["BUDGET_GROUP"];
            }
            try
            {
                DataTable IncomeSource = dtBudget.DefaultView.ToTable();
                //DataTable dtIncome = new DataTable();
                //DataTable dtExpense = new DataTable();

                totalBudgetedRawProposedIncome = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(PROPOSED_CURRENT_YR)", "BUDGET_TRANS_MODE='CR'").ToString());
                // TotalProposedBudgetedExpense = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(PROPOSED_CURRENT_YR)", "BUDGET_TRANS_MODE='DR'").ToString());
                string ordinaryexpense = "GROUP_FOR_GRID" + "='Ordinary Expenses'" + " AND BUDGET_TRANS_MODE='DR'";
                totalProposedBudgetedExpenseordinory = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(" + "PROPOSED_CURRENT_YR" + ")", ordinaryexpense).ToString());

                string Extraordinaryexpense = "GROUP_FOR_GRID" + "='Extraordinary Expenses'" + " AND BUDGET_TRANS_MODE='DR'";
                TotalProposedBudgetedExpenseExtraOrdinary = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(" + "PROPOSED_CURRENT_YR" + ")", Extraordinaryexpense).ToString());

                //string budgetexpensewithoutBGroup = "BUDGET_GROUP" + "<>'Ordinary Expenses' AND BUDGET_GROUP" + "<>'Extraordinary Expenses'";
                string budgetexpensewithoutBGroup = "GROUP_FOR_GRID=" + "''" + "AND BUDGET_TRANS_MODE='DR'";
                TotalProposedBudgetedExpense = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(" + "PROPOSED_CURRENT_YR" + ")", budgetexpensewithoutBGroup).ToString());

                TotalApprovedRawIncome = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(APPROVED_CURRENT_YR)", "BUDGET_TRANS_MODE='CR'").ToString());


                //TotalApprovedBudgetedExpense = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(APPROVED_CURRENT_YR)", "BUDGET_TRANS_MODE='DR'").ToString());
                TotalApprovedBudgetedExpenseOrdinary = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(" + "APPROVED_CURRENT_YR" + ")", ordinaryexpense).ToString());
                TotalApprovedBudgetedExpenseExtraOrdinary = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(" + "APPROVED_CURRENT_YR" + ")", Extraordinaryexpense).ToString());
                TotalApprovedBudgetedExpensesOnly = Member.NumberSet.ToDouble(dtBudget.Compute("SUM(" + "APPROVED_CURRENT_YR" + ")", budgetexpensewithoutBGroup).ToString());

                //IncomeSource.DefaultView.RowFilter = "BUDGET_TRANS_MODE='CR'";
                //dtIncome = IncomeSource.DefaultView.ToTable();
                //TotalBudgetedRawProposedIncome = Member.NumberSet.ToDouble(dtIncome.Compute("SUM(PROPOSED_CURRENT_YR)", string.Empty).ToString());
                //IncomeSource.DefaultView.RowFilter = string.Empty;
                //IncomeSource.AcceptChanges();

                //IncomeSource.DefaultView.RowFilter = "BUDGET_TRANS_MODE='DR'";
                //dtExpense = IncomeSource.DefaultView.ToTable();
                //string ordinaryexpense = "BUDGET_GROUP" + "='Ordinary Expenses'";
                //TotalProposedBudgetedExpense = Member.NumberSet.ToDouble(dtExpense.Compute("SUM(" + "PROPOSED_CURRENT_YR" + ")", ordinaryexpense).ToString());
                //IncomeSource.DefaultView.RowFilter = string.Empty;
                //IncomeSource.AcceptChanges();

                gvBudgetView.DataSource = IncomeSource; //dtBudget;
                gvBudgetView.DataBind();
                BudgetDetails = IncomeSource; //dtBudget;
                gvBudgetView.ExpandAll();

                //gvBudgetView.Settings.show
                //GridView.OptionsView.ShowIndicator = false;
                //GridViewFeaturesHelper.SetupGlobalGridViewBehavior(grid);
                //gvBudgetView.Settings. OptionsView.ShowIndicator = False

                BindCashBankFDDetails();
                ShowCMFNoteToProvince();
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
                GridViewDataColumn colApprovedAmount = gvBudgetView.Columns["APPROVED_CURRENT_YR"] as GridViewDataColumn;
                GridViewDataColumn colHONarration = gvBudgetView.Columns["HO_NARRATION"] as GridViewDataColumn;
                int visiblerowindex = 0;
                Int32 ledgerid = 0;
                string budgettransmode = string.Empty;

                gvBudgetView.UpdateEdit();
                DataTable dt = gvBudgetView.DataSource as DataTable;

                for (int i = 0; i < gvBudgetView.VisibleRowCount; i++)
                {
                    if (gvBudgetView.FindRowCellTemplateControl(i, colApprovedAmount, "txtSpEdit") != null)
                    {
                        decimal approvedamt = 0;
                        string HONarration = string.Empty;

                        //28/05/2020, to have Budget Income and Expense Ledgers
                        ledgerid = 0;
                        budgettransmode = string.Empty;
                        string[] strFieldNames = new string[] { "LEDGER_ID", "BUDGET_TRANS_MODE" };
                        DataTable dtUpdate = gvBudgetView.DataSource as DataTable;
                        if (gvBudgetView.GetRowValues(i, strFieldNames) != null)
                        {
                            object[] objUpdateLedgerInfo = gvBudgetView.GetRowValues(i, strFieldNames) as object[];
                            if (objUpdateLedgerInfo.GetLength(0) == 2)
                            {
                                ledgerid = base.LoginUser.NumberSet.ToInteger(objUpdateLedgerInfo.GetValue(0).ToString());
                                budgettransmode = objUpdateLedgerInfo.GetValue(1).ToString();
                            }
                        }

                        if (gvBudgetView.FindRowCellTemplateControl(i, colApprovedAmount, "txtSpEdit") != null)
                        {
                            ASPxSpinEdit txtApproved = gvBudgetView.FindRowCellTemplateControl(i, colApprovedAmount, "txtSpEdit") as ASPxSpinEdit;
                            approvedamt = base.LoginUser.NumberSet.ToDecimal(txtApproved.Text);
                        }

                        if (gvBudgetView.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") != null)
                        {
                            ASPxTextBox txtHONarration = gvBudgetView.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") as ASPxTextBox;
                            HONarration = txtHONarration.Text;
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
            objReportProperty.DateFrom = lblDateFrom.Text;
            objReportProperty.DateTo = lblDateTo.Text;

            objReportProperty.BudgetId = BudgetId.ToString();
            objReportProperty.Budget = BudgetId.ToString();
            objReportProperty.BranchOffice = BranchId.ToString();
            objReportProperty.Project = ProjectIds;
            objReportProperty.ReportTitle = "";

            if (BudgetInfo != null && BudgetInfo.Rows.Count > 0)
            {
                DateTime dePeriodFrom = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_FROM"].ToString(), false);
                DateTime dePeriodTo = this.Member.DateSet.ToDate(BudgetInfo.Rows[0]["DATE_TO"].ToString(), false);
            }

            objReportProperty.BudgetName = cmbBudget.SelectedItem == null ? string.Empty : cmbBudget.SelectedItem.Text;
            objReportProperty.Project = "";
            objReportProperty.BranchOfficeName = cmbBranch.SelectedItem == null ? string.Empty : cmbBranch.SelectedItem.Text;
            objReportProperty.SaveReportSetting();

            Session["REPORTPROPERTY"] = objReportProperty;
        }
        #endregion

    }
}