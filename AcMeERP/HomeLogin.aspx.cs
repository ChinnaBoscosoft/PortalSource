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
 * Purpose          :This page loads the currently logined head office/Branch office datasync datastatus count by month wise for the active financial year
 *                      Loads the receipts and payments for the selected period or default three months , current month and previous months 
 *****************************************************************************************************/
using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using DevExpress.Web.ASPxPivotGrid;
using System.Collections;
using Bosco.Report.Base;
using AcMEDSync.Model;
using DevExpress.XtraReports.Web;
using System.IO;
using DevExpress.Web.ASPxGridView;

namespace AcMeERP
{
    public partial class HomeLogin : Base.UIBase
    {
        #region Declaration

        private static object objLock = new object();
        ResultArgs resultArgs = null;
        private static string CODE = "CODE";
        CommonMember UtilityMember = new CommonMember();

        #endregion

        #region Propety
        private int branchid = 0;
        private int BranchId
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["branchid"].ToString());
            }
            set
            {
                branchid = value;
                ViewState["branchid"] = branchid;
            }
        }

        private int projectid = 0;
        private int ProjectId
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["projectid"].ToString());
            }
            set
            {
                projectid = value;
                ViewState["projectid"] = projectid;
            }
        }

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
        private string BranchIds
        {
            get
            {
                return ViewState["BranchIds"].ToString();
            }
            set
            {
                ViewState["BranchIds"] = value;
            }
        }

        private DateTime BalanceDate
        {
            get
            {
                return this.Member.DateSet.ToDate(ViewState["BalanceDate"].ToString(), false);
            }
            set
            {
                ViewState["BalanceDate"] = value;
            }
        }

        private DataTable Projects
        {
            set
            {
                ViewState["Projects"] = value;
            }
            get
            {
                return (DataTable)ViewState["Projects"];
            }
        }

        private string FinancialYear
        {
            set
            {
                ViewState["FinancialYear"] = value;
            }
            get
            {
                return (string)ViewState["FinancialYear"];
            }
        }

        private DateTime YearFrom
        {
            set
            {
                ViewState["YearFrom"] = value;
            }
            get
            {
                return (DateTime)ViewState["YearFrom"];
            }
        }
        private DateTime YearTo
        {
            set
            {
                ViewState["YearTo"] = value;
            }
            get
            {
                return (DateTime)ViewState["YearTo"];
            }
        }

        private DateTime DateFrom
        {
            set
            {
                ViewState["DateFrom"] = value;
            }
            get
            {
                return (DateTime)ViewState["DateFrom"];
            }
        }
        private DateTime DateTo
        {
            set
            {
                ViewState["DateTo"] = value;
            }
            get
            {
                return (DateTime)ViewState["DateTo"];
            }
        }

        private DataTable dtgvMorethanoneBranchSystem
        {
            set
            {
                ViewState["dtgvMorethanoneBranchSystem"] = value;
            }
            get
            {
                return (DataTable)ViewState["dtgvMorethanoneBranchSystem"];
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                if (base.LoginUser.IsPortalUser)
                {
                    divDashBoard.Visible = true;
                }
                if (base.LoginUser.IsPortalUser)
                {
                    divHeadHeader.Attributes.Add("class", "accordion-item opened");
                    divBranchHeader.Attributes.Add("class", "accordion-item opened");
                    divExpireLicenseHeader.Attributes.Add("class", "accordion-item opened");
                    divSoftHeader.Attributes.Add("class", "accordion-item opened");
                    divMorethanoneBranchSystemHeader.Attributes.Add("class", "accordion-item opened");

                    showHeadOfficeApprove();
                    showBranchOfficeApprove();

                    //showRenewalBranches(30);
                    //showBranchUsingMorethanOneSystemUsingAcmeerp(); //On 12/05/2022
                    gvBranchesRenewal.Visible = false;
                    gvMorethanoneBranchSystem.Visible = false;

                    divgvprojects.Visible = false;
                    divprojects.Visible = false;
                    GetNewUpdates();
                    this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                }
                if (!base.LoginUser.IsPortalUser)
                {
                    BranchId = 0;
                    BranchIds = "0"; ProjectIds = "0";
                    divHoDashBoard.Visible = true;
                    Session[base.DefaultBranchId] = 0;
                    SetDate();
                    LoadBranch();
                    LoadProjects();
                    GetActiveFinancialYear();

                    //On 28/02/2024, To skip loding all data when login-----------------------------------------
                    uiClosingBalance.Visible = false;
                    pvtDataSynStatus.Visible = false;
                    //LoadDataSynStatusProjectWise();
                    //LoadNonConformityBranches();
                    //LoadSumaryData();
                    //-------------------------------------------------------------------------------------------

                    this.ShowLoadWaitPopUp(btnSendMail);
                    this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                }

                //13/11/2019, to skip twice loading repot -------------------------------
                if (!base.LoginUser.IsPortalUser)
                {
                    // Monthly Reciepts and Payment
                    //LoadMonthlyReceiptPayment();
                }
                //-----------------------------------------------------------------------
            }
            if (!base.LoginUser.IsPortalUser)
            {
                // Monthly Reciepts and Payment
                //LoadMonthlyReceiptPayment();
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
            this.ShowTimeFromTimeTo = true;
        }

        protected void cmbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchId = this.UtilityMember.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
            LoadProjects();
            //BindDashboard();
        }
        protected void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
            //BindDashboard();
        }

        protected void btnViewStatus_Click(object sender, EventArgs e)
        {
            BranchId = this.UtilityMember.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
            ProjectId = this.UtilityMember.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
            BindDashboard();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            lock (objLock)
            {
                BindDashboard(true);
            }
        }

        /// <summary>
        /// This method loads the default Dashboard data.
        /// </summary>
        private void BindDashboard(bool loadRPReport = false)
        {
            //lblMonth.Visible = false;
            DateFrom = CommonMethod.FirstDayOfMonthFromDateTime(this.UtilityMember.DateSet.ToDate(dteFrom.Value.ToString(), false));
            DateTo = CommonMethod.LastDayOfMonthFromDateTime(this.UtilityMember.DateSet.ToDate(dteTo.Value.ToString(), false));
            //lblMonth.Text = "Receipts & Payments from " + DateFrom.ToString("MMM") + " " + DateFrom.Year.ToString() + " to " + DateTo.ToString("MMM") + " " + DateTo.Year.ToString();
            //lblProject.Text = cmbProject.Text.Trim();
            GetActiveFinancialYear();
            LoadDataSynStatusProjectWise();
            LoadNonConformityBranches();
            LoadSumaryData();
            if (loadRPReport)
            {
                LoadMonthlyReceiptPayment();
            }

            uiClosingBalance.Visible = true;
            pvtDataSynStatus.Visible = true;
        }
        #region DataStatus
        protected void pvtDataSynStatus_FieldValueDisplayText(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total";
            }
        }
        protected void pvtDataSynStatus_Load(object sender, EventArgs e)
        {
            //14_11_2019, to avaoid unnessary loading------
            //if (!base.LoginUser.IsPortalUser)
            //{
            //    LoadDataSynStatusProjectWise();
            //}
            //-----------------------------------------
        }
        protected void pvtDataSynStatus_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
        {
            if (e.ColumnValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value ||
                e.RowValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value) return;
            if (e.Value != null)
            {
                if (this.Member.NumberSet.ToInteger(e.Value.ToString()) > 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Green;
                    e.CellStyle.Font.Bold = true;
                    e.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Red;
                    e.CellStyle.BackColor = System.Drawing.Color.Red;
                    e.CellStyle.Font.Bold = true;
                    e.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }

        protected void pvtDataSynStatus_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "MONTH_NAME")
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "MONTH_NAME").ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "MONTH_NAME").ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }

        }

        protected void gvMorethanoneBranchSystemDetails_BeforePerformDataSelect(object sender, EventArgs e)
        {
            string hcode = string.Empty;
            string bcode = string.Empty;

            if (dtgvMorethanoneBranchSystem != null)
            {
                try
                {
                    ASPxGridView grid = sender as ASPxGridView;
                    if (!string.IsNullOrEmpty(grid.GetMasterRowKeyValue().ToString()))
                    {
                        string[] ValueIds = grid.GetMasterRowKeyValue().ToString().Split(',');
                        if (ValueIds != null)
                        {
                            //0-index-head office code, 1-branch office code
                            //hcode = "SDBINM";// ValueIds[0].ToString();
                            bcode = ValueIds[0].ToString();
                        }
                    }
                    DataTable dtgvMorethanoneBranchSystemDetails = dtgvMorethanoneBranchSystem.DefaultView.ToTable();
                    //this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName + " = '" + hcode + "' AND " +
                    string filter = this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName + " = '" + bcode + "'";
                    dtgvMorethanoneBranchSystemDetails.DefaultView.RowFilter = filter;
                    //dtgvMorethanoneBranchSystemDetails.DefaultView.Sort = "REMARKS, LOGGED_ON DESC";
                    dtgvMorethanoneBranchSystemDetails.DefaultView.Sort = "LICENSE_KEY_NUMBER DESC, LOGGED_ON DESC, REMARKS";

                    if (dtgvMorethanoneBranchSystemDetails.DefaultView.Count > 0)
                    {
                        grid.DataSource = dtgvMorethanoneBranchSystemDetails.DefaultView.ToTable();
                    }
                }
                catch (Exception ex)
                {
                    string exception = ex.ToString();
                    this.Message = ex.Message;
                }
            }
        }

        protected void aspxBtnExcel_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.XlsExportOptions xlsexport = new DevExpress.XtraPrinting.XlsExportOptions();
            xlsexport.SheetName = "Acmeerp_Branch_Logged_History";
            this.gridExportBranchLocationLoggedHistory.WriteXlsxToResponse("Acmeerp_Branch__Location_Logged_History", true);
        }

        #endregion
        #endregion

        #region Methods

        private bool GetNewUpdates()
        {
            DataTable dtNewUpdates;
            bool IsSoftwareAvailable = false;
            using (SoftwareSystem softwareSystem = new SoftwareSystem())
            {
                ResultArgs resultArgs = softwareSystem.FetchSoftwareDetailsByCurrentMonth();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    IsSoftwareAvailable = true;
                    dtNewUpdates = resultArgs.DataSource.Table;
                    dlNewUpdates.DataSource = dtNewUpdates;
                    dlNewUpdates.DataBind();
                }
            }
            return IsSoftwareAvailable;
        }
        /// <summary>
        /// Fetch Head Office Pending for approve
        /// </summary>
        private void showHeadOfficeApprove()
        {
            DataTable dtHeadOffice = null;
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                ResultArgs resultArgs = null;
                resultArgs = headOfficeSystem.FetchHeadOfficeToBeApproved(DataBaseType.Portal);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtHeadOffice = resultArgs.DataSource.Table;
                    dlHeadOffice.DataSource = dtHeadOffice;
                    dlHeadOffice.DataBind();
                }
            }
        }

        /// <summary>
        /// This method lists out all the newly created Branch office(s) to the Head office admin to approve  
        /// </summary>
        private void showBranchOfficeApprove()
        {
            DataTable dtBranchOffice = null;
            string headOffice = base.LoginUser.LoginUserHeadOfficeCode;
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                ResultArgs resultArgs = null;
                if (base.LoginUser.IsHeadOfficeUser)
                {
                    branchOfficeSystem.HeadOffice_Code = base.LoginUser.LoginUserHeadOfficeCode;
                    resultArgs = branchOfficeSystem.FetchBranchOfficeApprovedByHeadoffice(DataBaseType.Portal);

                }
                if (base.LoginUser.IsPortalUser)
                {
                    resultArgs = branchOfficeSystem.FetchBranchOfficeToBeApproved(DataBaseType.Portal);
                }
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtBranchOffice = resultArgs.DataSource.Table;
                    dlBranchOffice.DataSource = dtBranchOffice;
                    dlBranchOffice.DataBind();
                }
            }
        }

        /// <summary>
        /// On 02/12/2020, To show Renewal Branches for given Days
        /// 
        /// *************************** EXCEPT SDB CONGREGATIONS ***************************
        /// </summary>
        private void showRenewalBranches(int NextNoOfDays)
        {
            gvBranchesRenewal.Visible = true;
            DataTable dtRenewalBranchOffice = null;
            string headOffice = base.LoginUser.LoginUserHeadOfficeCode;

            //Skip All SDB HOs, Bosco Demo and License generated before 01/04/2017
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                ResultArgs resultArgs = null;
                if (base.LoginUser.IsHeadOfficeUser)
                {
                    branchOfficeSystem.HeadOffice_Code = base.LoginUser.LoginUserHeadOfficeCode;
                    resultArgs = branchOfficeSystem.FetchRenewalBranchOfficeByDays(DataBaseType.Portal);
                }
                else if (base.LoginUser.IsPortalUser)
                {
                    resultArgs = branchOfficeSystem.FetchRenewalBranchOfficeByDays(DataBaseType.Portal);
                }
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtRenewalBranchOffice = resultArgs.DataSource.Table;
                    gvBranchesRenewal.DataSource = dtRenewalBranchOffice;
                    gvBranchesRenewal.DataBind();
                }
            }
        }


        /// <summary>
        /// On 12/05/2022, Show more than one branch system using Acmerp in Local Communities
        /// </summary>
        /// <param name="NextNoOfDays"></param>
        private void showBranchUsingMorethanOneSystemUsingAcmeerp()
        {
            gvMorethanoneBranchSystem.Visible = true;
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                ResultArgs resultArgs = null;
                resultArgs = branchOfficeSystem.FetchBranchHistoryMoreThanOneSystem(DataBaseType.Portal, false);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtgvMorethanoneBranchSystem = resultArgs.DataSource.Table;
                    showBranchUsingMorethanOneSystemUsingAcmeerpExtracted();
                }
            }
        }


        private void showBranchUsingMorethanOneSystemUsingAcmeerpExtracted()
        {
            if (dtgvMorethanoneBranchSystem != null)
            {
                DataTable dtMaster = dtgvMorethanoneBranchSystem.DefaultView.ToTable(true,
                                                new string[] { "BRANCH_OFFICE_CODE", "HEAD_OFFICE_CODE", "BRANCH_OFFICE_NAME", "HEAD_OFFICE_NAME", "LOCATION_NAME", "IPs" });
                //dtMaster.DefaultView.Sort = "IPs DESC, HEAD_OFFICE_NAME, BRANCH_OFFICE_NAME, LOCATION_NAME";
                dtMaster.DefaultView.Sort = "HEAD_OFFICE_NAME, BRANCH_OFFICE_NAME, LOCATION_NAME, IPs DESC";
                gvMorethanoneBranchSystem.DataSource = dtMaster;
                gvMorethanoneBranchSystem.DataBind();
            }
        }


        private void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    BranchId = 0;
                    resultArgs = BranchOfficeSystem.FetchBranch();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranches, resultArgs.DataSource.Table, CODE, this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                        if (cmbBranches.Items.Count > 0)
                        {
                            string branchid = string.Empty;
                            if (base.LoginUser.IsHeadOfficeUser || base.LoginUser.IsHeadOfficeAdminUser)
                            {
                                Session[base.DefaultBranchId] = cmbBranches.SelectedItem.Value == "All" ? "0" : cmbBranches.SelectedItem.Value;
                                BranchId = cmbBranches.SelectedItem.Value == "All" ? 0 : this.UtilityMember.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
                            }
                            if (base.LoginUser.IsBranchOfficeUser || base.LoginUser.IsBranchOfficeAdminUser)
                            {
                                if (!string.IsNullOrEmpty(base.LoginUser.LoginUserBranchOfficeName))
                                {
                                    cmbBranches.Text = base.LoginUser.LoginUserBranchOfficeName;
                                    BranchId = this.Member.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
                                }
                                cmbBranches.Enabled = false;
                            }
                        }
                        else
                        {
                            BranchId = 0;
                            if (base.LoginUser.IsHeadOfficeUser)
                            {
                                BranchIds = "0";
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
                        this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, true);
                        ProjectId = 0;
                        string projectid = string.Empty;
                        for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                        {
                            projectid += resultArgs.DataSource.Table.Rows[i]["PROJECT_ID"].ToString() + ",";
                        }
                        ProjectIds = projectid.TrimEnd(',');
                    }
                    else
                    {
                        this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, false);
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
        /// <summary>
        /// This method fetches the active financial year
        /// </summary>
        private void GetActiveFinancialYear()
        {
            try
            {
                DateTime dtfromDate = DateTime.Now;
                DateTime dtToDate = DateTime.Now;

                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dtfromDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                        dtToDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                        string financialYear = "Financial year (" + dtfromDate.ToString("dd-MM-yyyy") + " to " + dtToDate.ToString("dd-MM-yyyy") + ")";
                        FinancialYear = financialYear;
                        //FinancialYear = ltrlACYear.Text = financialYear;
                    }
                    else
                    {
                        dtfromDate = new DateTime(this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Year, 4, 1);
                        dtToDate = dtfromDate.AddYears(1).AddDays(-1);
                    }
                    //Active Financial Year
                    Session["YEAR_FROM"] = dtfromDate;
                    Session["YEAR_TO"] = dtToDate;
                    //DateFrom = dtfromDate;
                    //DateTo = dtToDate;
                    YearFrom = dtfromDate;
                    YearTo = dtToDate;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        /// <summary>
        /// This method loads the active dashboard filtering criteria
        /// </summary>
        private void SetDate()
        {
            DateTime dtFromDate = DateTime.Now.AddMonths(-2);
            DateTime dtToDate = dtFromDate.AddMonths(2);
            DateFrom = dteFrom.Date = CommonMethod.FirstDayOfMonthFromDateTime(dtFromDate.Date);
            DateTo = dteTo.Date = CommonMethod.LastDayOfMonthFromDateTime(dtToDate.Date);
            //lblMonth.Text = "Receipts & Payments from " + DateFrom.ToString("MMM") + " " + DateFrom.Year.ToString() + " to " + DateTo.ToString("MMM") + " " + DateTo.Year.ToString();
            //lblProject.Text = cmbProject.Text.Trim();
        }

        private void LoadDataSynStatusProjectWise()
        {
            try
            {
                using (VoucherTransactionSystem vouchertransactionsystem = new VoucherTransactionSystem())
                {
                    vouchertransactionsystem.dtdsDateFrom = this.Member.DateSet.ToDate(YearFrom.ToString(), false);
                    vouchertransactionsystem.dtdsDateTo = this.Member.DateSet.ToDate(YearTo.ToString(), false);
                    vouchertransactionsystem.BranchId = BranchId;

                    if (base.LoginUser.IsBranchOfficeUser)
                    {
                        vouchertransactionsystem.BranchOfficeCode = this.LoginUser.LoginUserBranchOfficeCode;
                    }
                    resultArgs = vouchertransactionsystem.FetchDataSynStatusProjectWise();
                    if (resultArgs.Success)
                    {
                        pvtDataSynStatus.DataSource = resultArgs.DataSource.Table;
                        pvtDataSynStatus.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadMonthlyReceiptPayment()
        {
            dvReportViewer.Visible = true;
            if (!string.IsNullOrEmpty(ProjectIds) || ProjectId > 0)
            {
                ReportProperty objReportProperty = new ReportProperty();
                Page.Session["RPTCACHE"] = null;
                objReportProperty.ReportId = "RPT-027";
                objReportProperty.Project = ProjectId.ToString().Equals("0") ? ProjectIds : ProjectId.ToString();
                objReportProperty.DateFrom = this.UtilityMember.DateSet.ToDate(DateFrom.Date.ToString(), false).ToShortDateString();
                objReportProperty.DateTo = this.UtilityMember.DateSet.ToDate(DateTo.Date.ToString(), false).ToShortDateString();
                objReportProperty.ShowLogo = 0;
                objReportProperty.ShowLedgerCode = 1;
                objReportProperty.ShowGroupCode = 0;
                objReportProperty.ShowHorizontalLine = 0;
                objReportProperty.ShowByLedger = 1;
                objReportProperty.ShowVerticalLine = 0;
                objReportProperty.EnableDrillDown = false;
                objReportProperty.BranchOffice = BranchId.ToString();
                objReportProperty.BranchOfficeName = cmbBranches.SelectedItem == null ? string.Empty : cmbBranches.SelectedItem.Text;
                string reportAssemblyType = objReportProperty.ReportAssembly;
                ReportHeaderBase report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportHeaderBase;
                report.HideReportHeader = false;
                report.ShowReport();
                dvReportViewer.Report = report;
                Page.Session["RPTCACHE"] = null;
            }
        }

        private void LoadNonConformityBranches()
        {
            try
            {
                using (VoucherTransactionSystem vouchertransactionsystem = new VoucherTransactionSystem())
                {
                    //vouchertransactionsystem.dtdsDateFrom = this.Member.DateSet.ToDate(YearFrom.ToString(), false);
                    //vouchertransactionsystem.dtdsDateTo = this.Member.DateSet.ToDate(YearTo.ToString(), false);
                    vouchertransactionsystem.dtdsDateFrom = this.Member.DateSet.ToDate(dteFrom.Value.ToString(), false);
                    vouchertransactionsystem.dtdsDateTo = this.Member.DateSet.ToDate(dteTo.Value.ToString(), false);

                    vouchertransactionsystem.BranchId = BranchId;

                    if (base.LoginUser.IsBranchOfficeUser)
                    {
                        vouchertransactionsystem.BranchOfficeCode = this.LoginUser.LoginUserBranchOfficeCode;
                    }
                    resultArgs = vouchertransactionsystem.FetchNonConformityBranches();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        if (!IsPostBack)
                        {
                            hlkConformity.Text += "(" + resultArgs.DataSource.Table.Rows.Count + ")";
                            lblPopTitle.Text += "(" + resultArgs.DataSource.Table.Rows.Count + ")";
                        }
                        gvNonBranch.DataSource = null;
                        gvNonBranch.DataSource = resultArgs.DataSource.Table;
                        gvNonBranch.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        public void LoadSumaryData()
        {
            using (AccouingPeriodSystem accountSystem = new AccouingPeriodSystem())
            {
                string dtRecentVoucherDate = DateTime.Now.ToShortDateString();
                accountSystem.YearFrom = YearFrom.ToShortDateString();
                accountSystem.YearTo = YearTo.ToShortDateString();
                accountSystem.BranchId = BranchId;
                accountSystem.ProjectId = ProjectId;
                resultArgs = accountSystem.FetchRecentVoucherDate();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                {
                    DataTable dtProject = resultArgs.DataSource.Table;
                    foreach (DataRow dr in dtProject.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString()))
                        {
                            dtRecentVoucherDate = dr[AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                        }
                    }
                }
                uiClosingBalance.LoadBalance(BranchId.ToString(), ProjectId == 0 ? ProjectIds : ProjectId.ToString(), dtRecentVoucherDate.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                //uiClosingBalance.BalanceCaption = this.Member.DateSet.ToDate(dtRecentVoucherDate, false).DayOfWeek + ", " +
                //    this.Member.DateSet.ToDate(dtRecentVoucherDate, false).ToString("dd MMM yyyy");
                uiClosingBalance.BalanceCaption = "Balance as on " + this.Member.DateSet.ToDate(dtRecentVoucherDate);
            }
        }

        private void UncheckAllNonBranch()
        {
            try
            {
                if (gvNonBranch != null && gvNonBranch.Rows.Count > 0)
                {
                    string strmailIds = string.Empty;
                    foreach (GridViewRow gvrow in gvNonBranch.Rows)
                    {
                        CheckBox chkAll = (CheckBox)gvrow.FindControl("chkMsgAll");

                        CheckBox chk = (CheckBox)gvrow.FindControl("chkMessage");

                        if (chkAll != null && chkAll.Checked)
                        {
                            chkAll.Checked = false;
                        }
                        if (chk != null && chk.Checked)
                        {
                            chk.Checked = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + " UncheckAllNonBranch Dashboard Non Conforming Branches");
            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                lock (objLock)
                {
                    if (!string.IsNullOrEmpty(txtMsg.Text))
                    {
                        if (gvNonBranch != null && gvNonBranch.Rows.Count > 0)
                        {
                            string strmailIds = string.Empty;
                            foreach (GridViewRow gvrow in gvNonBranch.Rows)
                            {
                                CheckBox chk = (CheckBox)gvrow.FindControl("chkMessage");

                                if (chk != null & chk.Checked)
                                {
                                    string branchOfficeName = gvrow.Cells[2].Text.Trim();
                                    string Name = "Branch Admin" + " (" + gvrow.Cells[5].Text.Trim() + ")";
                                    string Header = "Regarding Data Upload to the Portal.<br/>";
                                    string MainContent = "<b>" + txtMsg.Text.Trim() + "</b>";

                                    string content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);

                                    resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(gvrow.Cells[4].Text), CommonMethod.RemoveFirstValue(gvrow.Cells[4].Text),
                                        "Please upload the Branch Office Data to Portal", content, false);
                                    if (!resultArgs.Success)
                                    {
                                        this.Message = resultArgs.Message.ToString();
                                    }
                                    else
                                    {
                                        this.Message = "Mail has been communicated to the Concern Branch Office to updload Data to Portal";
                                    }
                                }
                                strmailIds = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        this.Message = "Please Select Non Conformity Branches & Provide message to send email";
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + " btnSendMail_Click Dashboard Non Conforming Branches");
                this.Message = ex.Message;
            }
            finally
            {
                UncheckAllNonBranch();
            }

        }

        protected void gvMorethanoneBranchSystem_Load(object sender, EventArgs e)
        {
            showBranchUsingMorethanOneSystemUsingAcmeerpExtracted();
        }
        #endregion

        #region Report Caching
        /// <summary>
        /// This event fired when reportviewer is unloaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvReportViewer_Unload(object sender, EventArgs e)
        {
            dvReportViewer.Report = null;
        }
        //Report Caching -For Perfomance
        protected void dvReportViewer_CacheReportDocument(object sender, CacheReportDocumentEventArgs e)
        {
            Page.Session["RPTCACHE"] = e.SaveDocumentToMemoryStream();
        }
        protected void dvReportViewer_RestoreReportDocumentFromCache(object sender, RestoreReportDocumentFromCacheEventArgs e)
        {
            Stream stream = Page.Session["RPTCACHE"] as Stream;
            if (stream != null)
                e.RestoreDocumentFromStream(stream);
        }
        #endregion

        protected void aspxBtnMorethanOneRefreshBranch_Click(object sender, EventArgs e)
        {
            showBranchUsingMorethanOneSystemUsingAcmeerp();
        }

        protected void aspxBtnLicenseRenewal_Click(object sender, EventArgs e)
        {
            showRenewalBranches(30);
        }
    }

}
