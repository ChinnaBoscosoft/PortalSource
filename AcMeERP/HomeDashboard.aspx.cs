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
 * Purpose          :This page loads all the branch office datastatus, branchwise balance and project wise balance(Detail balance)
 *****************************************************************************************************/
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.Web.ASPxGridView;
using Bosco.Model.Transaction;
using DevExpress.Web.ASPxPivotGrid;
using System.Collections;

namespace AcMeERP
{
    public partial class HomeDashboard : Base.UIBase
    {

        #region Declaration

        ResultArgs resultArgs;
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
        private DataTable DataSynSource
        {
            get
            {
                return (DataTable)ViewState["DataSynSource"];
            }
            set
            {
                ViewState["DataSynSource"] = value;
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

        #endregion


        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!base.LoginUser.IsPortalUser)
                {
                    BranchId = 0;
                    BranchIds = "0"; ProjectIds = "0";
                    dteProjectBalDate.Date = DateTime.Now.Date;
                    divHoDashBoard.Visible = true;
                    Session[base.DefaultBranchId] = 0;
                    //  LoadBranchRecPaymentAmount();
                    LoadDate();
                    this.ShowLoadWaitPopUp(btnGo);
                    this.ShowLoadWaitPopUp(btnProjectGo);
                }
            }
            if (!base.LoginUser.IsPortalUser)
            {
                ltrlACYear.Text = FinancialYear;
                LoadDataSynStatus();
                //pvtDataSynStatus.CollapseAllRows();
            }
        }

        protected void gvProjects_BeforePerformDataSelect(object sender, EventArgs e)
        {
            int branchId = 0;
            ASPxGridView grid = sender as ASPxGridView;
            try
            {
                using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
                {
                    branchId = Member.NumberSet.ToInteger(grid.GetMasterRowKeyValue().ToString());

                    resultArgs = BranchOffiecSystem.FetchProjects(branchId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        grid.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            // LoadBranchRecPaymentAmount();
        }
        protected void gvBranchDetail_Load(object sender, EventArgs e)
        {
            //if (TabReportCriteria.ActiveTab.HeaderText.Equals("Branch"))
            //{
            // LoadBranchRecPaymentAmount();
            // }
        }
        #endregion

        #region Methods


        private void LoadBranchStatus()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchBranchBalance(dteBalanceDate.Date.ToString(), dteBalanceDate.Date.ToString());
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gvBranchDetail.DataSource = resultArgs.DataSource.Table;
                        gvBranchDetail.DataBind();
                    }
                    else
                    {
                        gvBranchDetail.DataSource = null;
                        gvBranchDetail.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
        }

        private void LoadBranchRecPaymentAmount()
        {
            try
            {
                DateTime dtfromDate;

                using (VoucherTransactionSystem vouchertransactionsystem = new VoucherTransactionSystem())
                {
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dtfromDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                        }
                        else
                        {
                            dtfromDate = new DateTime(this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Year, 4, 1);
                        }
                        using (ProjectSystem projectSystem = new ProjectSystem())
                        {
                            resultArgs = projectSystem.FetchDashboardBranchDetails(dtfromDate, dteBalanceDate.Date);
                            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                gvBranchDetail.DataSource = resultArgs.DataSource.Table;
                                gvBranchDetail.DataBind();
                            }
                            else
                            {
                                gvBranchDetail.DataSource = null;
                                gvBranchDetail.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
        }


        private void LoadDate()
        {
            DateTime dtfromDate;
            DateTime dtToDate;

            using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
            {
                resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtfromDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                    dtToDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                }
                else
                {
                    dtfromDate = new DateTime(this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Year, 4, 1);
                    dtToDate = dtfromDate.AddYears(1).AddDays(-1);

                }
                dteBalanceDate.MinDate = dtfromDate;
                dteBalanceDate.MaxDate = dteBalanceDate.Date = dtToDate;
            }
        }

        #endregion

        #region DashBoard

        #region Project Tab

        private void LoadProjectsbyBranch()
        {
            try
            {
                BalanceDate = DateTime.Now;
                using (ProjectSystem projectSystem = new ProjectSystem())
                {

                    projectSystem.BranchId = cmbBranches.Items.Count > 0 ? this.Member.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString()) : 0;
                    using (VoucherTransactionSystem VoucherMasterSystem = new VoucherTransactionSystem())
                    {
                        BalanceDate = VoucherMasterSystem.FetchOPBalanceDate(BranchId, 0);
                    }
                    projectSystem.AccountDate = new DateTime(dteProjectBalDate.Date.Year, 3, 31);
                    if (BalanceDate == projectSystem.AccountDate.AddDays(-1))
                    {
                        projectSystem.AccountDate = BalanceDate;
                    }
                    if (dteProjectBalDate.Date.Month <= 3)
                    {
                        projectSystem.AccountDate = projectSystem.AccountDate.AddYears(-1);
                    }
                    DateTime DateFrom = new DateTime(projectSystem.AccountDate.Year, 4, 1);
                    resultArgs = projectSystem.FetchProjectsByBranch(DateFrom.ToString(), dteProjectBalDate.Date.ToString());
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gvProjects.DataSource = Projects = resultArgs.DataSource.Table;
                        gvProjects.DataBind();
                        LoadProjects(Projects.DefaultView);
                    }
                    else
                    {
                        gvProjects.DataSource = Projects = null;
                        gvProjects.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
        }

        private void LoadProjects(DataView dvProject)
        {
            try
            {
                if (BranchId > 0)
                {
                    dvProject.RowFilter = "BRANCH_ID=" + BranchId + "";
                    gvProjects.DataSource = dvProject.ToTable();
                    gvProjects.DataBind();
                    dvProject.RowFilter = "";
                }
                else
                {
                    DataTable dtProject = dvProject.ToTable().AsEnumerable().GroupBy(r => r.Field<UInt32>("PROJECT_ID")).Select(g => g.First()).CopyToDataTable();
                    gvProjects.DataSource = dtProject;
                    gvProjects.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBranchCombo()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    BranchId = 0;
                    resultArgs = BranchOfficeSystem.FetchBranch();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranches, resultArgs.DataSource.Table, CODE, this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, true);
                        if (cmbBranches.Items.Count > 0)
                        {
                            string branchid = string.Empty;
                            if (base.LoginUser.IsHeadOfficeUser)
                            {
                                Session[base.DefaultBranchId] = cmbBranches.SelectedItem.Value == "All" ? "0" : cmbBranches.SelectedItem.Value;
                                BranchId = cmbBranches.SelectedItem.Value == "All" ? 0 : this.UtilityMember.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
                            }
                            if (base.LoginUser.IsBranchOfficeUser)
                            {
                                if (!string.IsNullOrEmpty(base.LoginUser.LoginUserBranchOfficeName))
                                {
                                    cmbBranches.Text = base.LoginUser.LoginUserBranchOfficeName;
                                    BranchId =
                                        this.Member.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
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

        protected void gvProjects_Load(object sender, EventArgs e)
        {
            //if (TabDashBoard.ActiveTab.HeaderText.Equals("Project"))
            //{
            //  LoadProjectsbyBranch();
            //}
        }

        protected void cmbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session[base.DefaultBranchId] = cmbBranches.SelectedItem.Value == "All" ? "0" : cmbBranches.SelectedItem.Value;
            BranchId = cmbBranches.SelectedItem.Value == "All" ? 0 : this.UtilityMember.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
            //  LoadProjectsbyBranch();
        }
        protected void btnProjectGo_Click(object sender, EventArgs e)
        {
            //LoadProjectsbyBranch();
        }

        #region DashBoard

        private void LoadDataSynStatus()
        {
            try
            {
                DateTime dtfromDate;
                DateTime dtToDate;

                using (VoucherTransactionSystem vouchertransactionsystem = new VoucherTransactionSystem())
                {
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dtfromDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                            dtToDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                            string financialYear = "Financial year (" + dtfromDate.ToString("dd-MM-yyyy") + " to " + dtToDate.ToString("dd-MM-yyyy") + ")";
                            FinancialYear = ltrlACYear.Text = financialYear;
                        }
                        else
                        {
                            dtfromDate = new DateTime(this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Year, 4, 1);
                            dtToDate = dtfromDate.AddYears(1).AddDays(-1);
                        }
                    }
                    vouchertransactionsystem.dtdsDateFrom = this.Member.DateSet.ToDate(dtfromDate.ToString(), false);
                    vouchertransactionsystem.dtdsDateTo = this.Member.DateSet.ToDate(dtToDate.ToString(), false);
                    if (base.LoginUser.IsBranchOfficeUser)
                    {
                        vouchertransactionsystem.BranchOfficeCode = this.LoginUser.LoginUserBranchOfficeCode;
                    }
                    //resultArgs = vouchertransactionsystem.FetchDataSynStatus();
                    resultArgs = vouchertransactionsystem.FetchDataSynStatusProjectWise();
                    if (resultArgs.Success)
                    {
                        pvtDataSynStatus.DataSource = resultArgs.DataSource.Table;
                        pvtDataSynStatus.DataBind();
                        DataSynSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void pvtDataSynStatus_FieldValueDisplayText(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total";
            }
        }
        //protected void pvtDataSynStatus_Load(object sender, EventArgs e)
        //{
        //    if (!base.LoginUser.IsPortalUser)
        //    {
        //        if (DataSynSource == null)
        //        {
        //            LoadDataSynStatus();
        //        }
        //        else
        //        {
        //            ltrlACYear.Text = FinancialYear;
        //            pvtDataSynStatus.DataSource = DataSynSource;
        //            pvtDataSynStatus.DataBind();
        //        }
        //    }
        //}
        protected void pvtDataSynStatus_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
        {
            if (e.ColumnValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal
                     || e.ColumnValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                if (e.SummaryValue != null)
                {
                    e.CellStyle.BackColor = System.Drawing.Color.Chocolate;
                    e.CellStyle.Font.Bold = true;
                }
            if (e.ColumnValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value ||
                e.RowValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value) return;

            if (e.Value != null)
            {
                if (e.RowIndex % 2 == 0)
                {
                    e.CellStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
                }
                else
                {
                    e.CellStyle.BackColor = System.Drawing.Color.AliceBlue;
                }
                if (this.Member.NumberSet.ToInteger(e.Value.ToString()) > 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Green;
                    e.CellStyle.Font.Bold = true;
                    e.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Red;
                    e.CellStyle.Font.Bold = true;
                    e.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }

        protected void pvtDataSynStatus_HtmlFieldValuePrepared(object sender, DevExpress.Web.ASPxPivotGrid.PivotHtmlFieldValuePreparedEventArgs e)
        {
            if (!e.IsColumn)
            {

                if (e.Field.FieldName == "BRANCH_OFFICE_NAME")
                {
                    e.Cell.BackColor = System.Drawing.Color.BurlyWood;
                }
                else
                {
                    e.Cell.BackColor = System.Drawing.Color.BlanchedAlmond;
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
        #endregion
        #endregion
        protected void TabDashBoard_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabDashBoard.ActiveTab.HeaderText.Equals("Project"))
            {
                LoadBranchCombo();
                dteProjectBalDate.Date = DateTime.Now.Date;
                //   LoadProjectsbyBranch();
            }
        }
        #endregion
    }
}