/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 17/07/2015
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          : This page is to load report criteria for all the financial reports for mfs (Trial Balance, Balance sheet, Receipts and payments, Bank Journal and Day Book) as
 *                      defined in the ReportSetting.xml file in Bosco.Report.dll
 * *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Collections;
using Bosco.Model.UIModel;
using Bosco.Model.Setting;
using System.IO;
using DevExpress.XtraReports.Web;
using DevExpress.Web.ASPxCallback;

namespace AcMeERP.Report
{
    public partial class ReportCriteriaPortal : Base.UIBase, ICallbackEventHandler
    {
        #region Declaration
        string result = string.Empty;
        Bosco.Utility.ConfigSetting.SettingProperty settingProperty = new Bosco.Utility.ConfigSetting.SettingProperty();
        private const string DateCaption = "Date As on";
        private const string SELECT = "SELECT";
        private const string SCONSTATEMENT = "Consolidated Statement";
        string sDateFrom = string.Empty;
        string sDateTo = string.Empty;
        private static object objLoc = new object();
        #endregion

        #region Property

        private CommonMember utilityMember = null;
        private ReportProperty objReportProperty = new ReportProperty();
        Bosco.Utility.ConfigSetting.SettingProperty objSettingProperty = new Bosco.Utility.ConfigSetting.SettingProperty();

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }
        public DataTable ProjectSelected { get; set; }
        public DataTable BankSelected { get; set; }
        public DataTable LedgerGroupSelected { get; set; }
        public DataTable ConLedgerSelected { get; set; }

        public DataTable LedgerSelected { get; set; }
        public DataTable SubsidyLedgerSelected { get; set; }
        public DataTable ContributionLedgerSelected { get; set; }
        public DataTable BudgetSelected { get; set; }
        public DataTable CostCentreSelected { get; set; }
        public DataTable BranchSelected { get; set; }
        public DataTable SocietySelected { get; set; }

        private DataTable dtCriteria = new DataTable();
        private DataTable dtReportCriteria
        {
            get
            {
                return dtCriteria;
            }
            set
            {
                dtCriteria = value;
            }
        }

        private string ProjectId
        {
            set
            {
                Session["ProjectId"] = value;
            }
            get
            {
                if (Session["ProjectId"] == null)
                    return string.Empty;
                else
                    return (string)Session["ProjectId"];
            }
        }
        private string BudgetId
        {
            set
            {
                Session["BudgetId"] = value;
            }
            get
            {
                if (Session["BudgetId"] == null)
                    return string.Empty;
                else
                    return (string)Session["BudgetId"];
            }
        }
        private string CurrentLoginUserBranchCode
        {
            set
            {
                ViewState["CurrentLoginUserBranchCode"] = value;
            }
            get
            {
                if (ViewState["CurrentLoginUserBranchCode"] == null)
                    return string.Empty;
                else
                    return (string)ViewState["CurrentLoginUserBranchCode"];
            }
        }

        string yearfrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearfrom = settingProperty.YearFrom;
                return yearfrom;
            }
        }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }

        private string LedgerGroupId
        {
            get
            {
                if (Session["LedgerGroupId"] == null)
                    return string.Empty;
                else
                    return (string)Session["LedgerGroupId"];
            }
            set
            {
                Session["LedgerGroupId"] = value;
            }
        }

        private string BankId
        {
            get
            {
                if (Session["BankId"] == null)
                    return string.Empty;
                else
                    return (string)Session["BankId"];
            }
            set
            {
                Session["BankId"] = value;
            }
        }

        private string CostCentreId
        {
            get
            {
                if (Session["CostCentreId"] == null)
                    return string.Empty;
                else
                    return (string)Session["CostCentreId"];
            }
            set
            {
                Session["CostCentreId"] = value;
            }
        }

        private string ProjectCategoryId
        {
            get
            {
                if (Session["ProjectCategoryId"] == null)
                    return string.Empty;
                else
                    return (string)Session["ProjectCategoryId"];
            }
            set
            {
                Session["ProjectCategoryId"] = value;
            }
        }

        private string BranchCode
        {
            get
            {
                if (Session["BranchCode"] == null)
                    return string.Empty;
                else
                    return (string)Session["BranchCode"];
            }
            set
            {
                Session["BranchCode"] = value;
            }
        }
        private string SocietyCode
        {
            get
            {
                if (Session["SocietyCode"] == null)
                    return string.Empty;
                else
                    return (string)Session["SocietyCode"];
            }
            set
            {
                Session["SocietyCode"] = value;
            }
        }

        private string LedgerId
        {
            get
            {
                if (Session["LedgerId"] == null)
                    return string.Empty;
                else
                    return (string)Session["LedgerId"];
            }
            set
            {
                Session["LedgerId"] = value;
            }
        }

        private string ConLedgerId
        {
            get
            {
                if (Session["ConLedgerId"] == null)
                    return string.Empty;
                else
                    return (string)Session["ConLedgerId"];
            }
            set
            {
                Session["ConLedgerId"] = value;
            }
        }

        private int FDRegisterSelectedStatus
        {
            get
            {
                if (ViewState["FDRegisterSelectedStatus"] == null)
                    //1-All 2- Active and 3- Closed Fixed Deposit Register
                    return (Session["FDRegisterStatus"] != null) ? this.Member.NumberSet.ToInteger(Session["FDRegisterStatus"].ToString()) : 1;
                else
                    return (int)ViewState["FDRegisterSelectedStatus"];
            }
            set
            {
                ViewState["FDRegisterSelectedStatus"] = value;
            }
        }
        private DataTable ProjectSource
        {
            get
            {
                if (Session["ProjectSource"] == null)
                    return null;
                else
                    return (DataTable)Session["ProjectSource"];
            }
            set { Session["ProjectSource"] = value; }
        }
        private DataTable BranchSource
        {
            get
            {
                if (Session["BranchSource"] == null)
                    return null;
                else
                    return (DataTable)Session["BranchSource"];
            }
            set { Session["BranchSource"] = value; }
        }
        private DataTable SocietySource
        {
            get
            {
                if (Session["SocietySource"] == null)
                    return null;
                else
                    return (DataTable)Session["SocietySource"];
            }
            set { Session["SocietySource"] = value; }
        }
        private DataTable ProjectCategorySource
        {
            get
            {
                if (Session["ProjectCategorySource"] == null)
                    return null;
                else
                    return (DataTable)Session["ProjectCategorySource"];
            }
            set { Session["ProjectCategorySource"] = value; }
        }

        private DataTable LedgerGroupSource
        {
            get
            {
                if (Session["LedgerGroupSource"] == null)
                    return null;
                else
                    return (DataTable)Session["LedgerGroupSource"];
            }
            set { Session["LedgerGroupSource"] = value; }
        }

        private DataTable CongregationLedgeWithLedgerGroup
        {
            get
            {
                if (Session["ConLedgerWithLG"] == null)
                    return null;
                else
                    return (DataTable)Session["ConLedgerWithLG"];
            }
            set { Session["ConLedgerWithLG"] = value; }
        }

        private DataTable ConLedgerSource
        {
            get
            {
                if (Session["ConLedgerSource"] == null)
                    return null;
                else
                    return (DataTable)Session["ConLedgerSource"];
            }
            set { Session["ConLedgerSource"] = value; }
        }

        private DataTable LedgerSource
        {
            get
            {
                if (Session["LedgerSource"] == null)
                    return null;
                else
                    return (DataTable)Session["LedgerSource"];
            }
            set
            {
                Session["LedgerSource"] = value;
            }
        }

        private DataTable SubsidyLedgerSource
        {
            get
            {
                if (Session["SubsidyLedgerSource"] == null)
                    return null;
                else
                    return (DataTable)Session["SubsidyLedgerSource"];
            }
            set
            {
                Session["SubsidyLedgerSource"] = value;
            }
        }

        private DataTable ContributionLedgerSource
        {
            get
            {
                if (Session["ContributionLedgerSource"] == null)
                    return null;
                else
                    return (DataTable)Session["ContributionLedgerSource"];
            }
            set
            {
                Session["ContributionLedgerSource"] = value;
            }
        }

        private DataTable BankSource
        {
            get
            {
                if (Session["BankSource"] == null)
                    return null;
                else
                    return (DataTable)Session["BankSource"];
            }
            set { Session["BankSource"] = value; }
        }

        private DataTable BudgetSource
        {
            get
            {
                if (Session["BudgetSource"] == null)
                    return null;
                else
                    return (DataTable)Session["BudgetSource"];
            }
            set { Session["BudgetSource"] = value; }
        }

        private DataTable CostCentreSource
        {
            get
            {
                if (Session["CostCentreSource"] == null)
                    return null;
                else
                    return (DataTable)Session["CostCentreSource"];
            }
            set { Session["CostCentreSource"] = value; }
        }

        private DataTable ReportsCriteria
        {
            get
            {
                if (ViewState["ReportsCriteria"] == null)
                    return null;
                else
                    return (DataTable)ViewState["ReportsCriteria"];
            }
            set
            {
                ViewState["ReportsCriteria"] = value;
            }
        }

        private string Reportproperties
        {
            get
            {
                if (ViewState["Reportproperties"] == null)
                    return null;
                else
                    return (string)ViewState["Reportproperties"];
            }
            set
            {
                ViewState["Reportproperties"] = value;
            }
        }


        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            // objReportProperty.Society = "0";
            if (!IsPostBack)
            {
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                GlobalSetting objGlobal = new GlobalSetting();
                objGlobal.ApplySetting();
                TabDateProperties();
                objReportProperty.ReportId = Request.QueryString["rid"];
                ReportsCriteria = null;
                ProjectId = objReportProperty.Project;
                BudgetId = objReportProperty.Budget;
                BudgetId = objReportProperty.BudgetId;
                LedgerGroupId = objReportProperty.LedgerGroup;
                //if (!objReportProperty.ReportId.Equals("RPT-058"))
                //{
                //    LedgerGroupId = objReportProperty.LedgerGroup;
                //}
                //else
                //{
                //    objReportProperty.LedgerGroup = LedgerGroupId = string.Empty;
                //}

                //Disable Tabs // 30/05/2024
                //if (objReportProperty.IsSDBRomeReports)
                //{
                //    BranchCode = objReportProperty.BranchOffice = string.Empty;
                //    objReportProperty.Project = string.Empty;
                //}

                if ((this.LoginUser.IS_SAP_CONGREGATION && objReportProperty.ReportId == "RPT-183") || (this.LoginUser.IS_SAP_CONGREGATION && objReportProperty.ReportId == "RPT-184"))
                {
                    ddlCode.Visible = true;
                    lblCode.Visible = true;
                }

                //Assign the value from the session property of the report
                BranchCode = objReportProperty.BranchOffice;
                SocietyCode = objReportProperty.Society;
                CostCentreId = objReportProperty.CostCentre;
                objReportProperty.EnableDrillDown = true;
                EnableTabs();
                EnableReportSetupProperties();
                SetTabIndex();
                AssignValues();
                this.ShowLoadWaitPopUp(btnOk);
            }



            // This is to disble the Society for all the Reports (22.01.2020)
            TabSociety.Enabled = false;

            if (!string.IsNullOrEmpty(BranchCode) && BranchCode != "0")
            {
                TabSociety.Enabled = false;
            }
            //  else if ((!string.IsNullOrEmpty(SocietyCode) && SocietyCode != "0") && objReportProperty.IsSDBRomeReports) //30/05/2024
            else if ((!string.IsNullOrEmpty(SocietyCode) && SocietyCode != "0")) //
            {
                // Enable the branch for Rome Reports
                TabBranch.Enabled = true;
            }

            if (string.IsNullOrEmpty(BranchCode) && BranchCode == "0")
            {
                TabSociety.Enabled = true;
            }
            if (string.IsNullOrEmpty(SocietyCode) && SocietyCode == "0")
            {
                TabBranch.Enabled = true;
            }

            //On 29/01/2021, Enable always for Brach tab for this FDCCSI annaul reports 
            if (objReportProperty.ReportId == "RPT-170" || objReportProperty.ReportId == "RPT-175" || objReportProperty.ReportId == "RPT-176" ||
                objReportProperty.ReportId == "RPT-178" || objReportProperty.ReportId == "RPT-179")
            {
                TabBranch.Enabled = true;
            }

            //Enable always society tab for this Reports
            if (objReportProperty.ReportId == "RPT-001" || objReportProperty.ReportId == "RPT-002" || objReportProperty.ReportId == "RPT-003" ||
                objReportProperty.ReportId == "RPT-062" || objReportProperty.ReportId == "RPT-064" || objReportProperty.ReportId == "RPT-065" || objReportProperty.ReportId == "RPT-068" ||
                objReportProperty.ReportId == "RPT-028" || objReportProperty.ReportId == "RPT-063" || objReportProperty.ReportId == "RPT-027" || objReportProperty.ReportId == "RPT-183" ||
                objReportProperty.ReportId == "RPT-170" || objReportProperty.ReportId == "RPT-175" || objReportProperty.ReportId == "RPT-176" || objReportProperty.ReportId == "RPT-012" ||
                objReportProperty.ReportId == "RPT-077" || objReportProperty.ReportId == "RPT-078" || objReportProperty.ReportId == "RPT-079" ||
                objReportProperty.ReportId == "RPT-178" || objReportProperty.ReportId == "RPT-179" || objReportProperty.ReportId == "RPT-080" ||
                objReportProperty.ReportId == "RPT-030" || objReportProperty.ReportId == "RPT-031" || objReportProperty.ReportId == "RPT-190" || objReportProperty.ReportId == "RPT-060" || objReportProperty.ReportId == "RPT-185")
            {
                TabSociety.Enabled = true;
            }


            //Disable always society tab for this Reports
            if (objReportProperty.ReportId == "RPT-180")
            {
                TabSociety.Visible = false;
            }

            //set ActiveTab always Branch
            if (TabBranch.Visible)
            {
                TabReportCriteria.ActiveTabIndex = TabBranch.TabIndex;
            }
            //else if (objReportProperty.IsSDBRomeReports)//On 26/11/2019, For Rome reports // 30/05/2024
            //{
            //    TabReportCriteria.ActiveTabIndex = TabSociety.TabIndex;
            //}
            else
            {
                TabReportCriteria.ActiveTabIndex = TabReportSetup.TabIndex;
            }

            //Creating a reference of Client side Method, that is called after callback on server
            String cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg",
            "ReceiveServerData", "context");

            //Putting the reference in the method that will initiate Callback
            String callbackScript = "function CallServer(arg, context) {" +
            cbReference + "; }";

            //Registering the method
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
            "CallServer", callbackScript, true);
            if (objReportProperty.ReportId == "RPT-052")
            {
                objReportProperty.ProjectTitle = string.Empty;
                objReportProperty.BranchOfficeName = string.Empty;
                objReportProperty.CostCentreName = string.Empty;
                objReportProperty.BudgetName = string.Empty;
            }

            if (objReportProperty.ReportId == "RPT-046" || objReportProperty.ReportId == "RPT-048" || objReportProperty.ReportId == "RPT-169")
            {
                TabProject.Enabled = true;
            }
        }

        /// <summary>
        /// This method initiates the report criteria
        /// </summary>
        private void InitiateReportCriteria()
        {
            EnableTabs();
            EnableReportSetupProperties();
            SetTabIndex();
            AssignValues();
        }

        /// <summary>
        /// This method fired when ok button is clicked to assign criteria values to load the report in the report viewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {

            lock (objLoc)
            {
                if (IsValidCriteria())
                {
                    ClearFilterTextBoxes();
                    LoadReport(objReportProperty.ReportId);
                }
            }
        }

        /// <summary>
        /// This event is fired when Cancel button is clicked , navigates to HomeLogin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            string navUrl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
            Response.Redirect(navUrl, false);
        }
        /// <summary>
        /// This event is fired to get the selected ledgers from the ledger tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkLedgerSelect_CheckedChanged(object sender, EventArgs e)
        {
            //Single Ledger must be selected for monthly Ledger report summary and uncheck all the others
            if (objReportProperty.ReportId != null)
            {
                if (objReportProperty.ReportId.Equals("RPT-058"))
                {
                    CheckBox chk = (CheckBox)sender;
                    GridViewRow gv = (GridViewRow)chk.NamingContainer;
                    int rownumber = gv.RowIndex;

                    if (chk.Checked)
                    {
                        int i;
                        for (i = 0; i <= gvLedger.Rows.Count - 1; i++)
                        {
                            if (i != rownumber)
                            {
                                CheckBox chkcheckbox = ((CheckBox)(gvLedger.Rows[i].FindControl("chkLedgerSelect")));
                                chkcheckbox.Checked = false;
                            }
                        }
                    }
                }
            }
        }
        // Assign values to contorls from session
        private void AssignValues()
        {
            if (!string.IsNullOrEmpty(objReportProperty.DateFrom))
            {
                dteFrom.Date = this.Member.DateSet.ToDate(objReportProperty.DateFrom, true);

                if (objReportProperty.ReportId == "RPT-065" || objReportProperty.ReportId == "RPT-077" ||
                        objReportProperty.ReportId == "RPT-188" || objReportProperty.ReportId == "RPT-189")
                {
                    dteFrom.Date = UtilityMember.DateSet.ToDate(settingProperty.YearFrom, false);
                }

                if (objReportProperty.ReportId == "RPT-004" || objReportProperty.ReportId == "RPT-005" ||
                    objReportProperty.ReportId == "RPT-006")
                {
                    dteTo.Date = dteFrom.Date.AddMonths(12).AddDays(-1);
                }
                else
                {
                    dteTo.Date = this.Member.DateSet.ToDate(objReportProperty.DateTo, true);
                }
                // To hide the command for the Year we have fixed (#11/01/2023-Chinna)
                //if (objReportProperty.ReportId == "RPT-061")
                //{
                //    //Active Financial Year this values are stored in session in the HomeLogin.aspx
                //    objReportProperty.DateFrom = this.Member.DateSet.ToDate(Session["YEAR_FROM"].ToString(), true).ToShortDateString();
                //    objReportProperty.DateTo = this.Member.DateSet.ToDate(Session["YEAR_TO"].ToString(), true).ToShortDateString();
                //    dteFrom.Date = this.Member.DateSet.ToDate(Session["YEAR_FROM"].ToString(), true);
                //    dteTo.Date = this.Member.DateSet.ToDate(Session["YEAR_TO"].ToString(), true);
                //    dteFrom.MinDate = this.Member.DateSet.ToDate(Session["YEAR_FROM"].ToString(), true);
                //    dteTo.MaxDate = this.Member.DateSet.ToDate(Session["YEAR_TO"].ToString(), true);
                //}

                if (objReportProperty.ReportId == "RPT-031" || objReportProperty.ReportId == "RPT-184")
                {
                    dteFrom.Date = this.Member.DateSet.ToDate(objReportProperty.DateAsOn, true);
                }
            }
            if (TabBranch.Visible)
            {
                AssignBranchOffice();
            }
            if (TabSociety.Visible)
            {
                AssignSociety();
            }
            if (TabProjectCategory.Visible)
            {
                AssignProjectCategory();
            }
            if (TabProject.Visible)
            {
                AssignProject();
                if (gvBankAccount.Visible)
                {
                    AssignBank();
                }
            }
            if (TabLedger.Visible)
            {
                AssignLedgerGroup();
                AssignLedger();
            }
            if (TabCostCentre.Visible)
            {
                AssignCostCentre();
            }
            if (TabBudget.Visible)
            {
                AssignBudget();
            }

            //09/08/2021, For Congregation Ledger
            if (upCongregationLedger.Visible)
            {
                AssignCongregationLedger();
            }
            AssignReportSetUpValues();
        }

        private void AssignProject()
        {
            DataTable dtProject = null;
            if (!string.IsNullOrEmpty(objReportProperty.Project))
            {
                dtProject = gvProject.DataSource != null ? (DataTable)gvProject.DataSource : ProjectSource;
                string[] ProId = objReportProperty.Project.Split(',');
                for (int i = 0; i < ProId.Length; i++)
                {
                    if (dtProject != null && dtProject.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            if (dr["PROJECT_ID"].ToString() == ProId[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                ProjectId = objReportProperty.Project;

                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /*EnumerableRowCollection<DataRow> query =
               from project in dtProject.AsEnumerable()
               orderby project.Field<Int32>("SELECT") descending
               select project;
                DataView ProjectView = query.AsDataView();*/

                dtProject.DefaultView.Sort = "SELECT DESC";
                DataView ProjectView = dtProject.DefaultView;

                gvProject.DataSource = ProjectView.ToTable();
                gvProject.DataBind();
            }
        }

        private void AssignBudget()
        {
            DataTable dtBudget = null;
            if (!string.IsNullOrEmpty(objReportProperty.Budget))
            {
                dtBudget = gvBudget.DataSource != null ? (DataTable)gvBudget.DataSource : BudgetSource;
                string[] BudId = objReportProperty.Budget.Split(',');
                for (int i = 0; i < BudId.Length; i++)
                {
                    if (dtBudget != null && dtBudget.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtBudget.Rows)
                        {
                            if (dr["BUDGET_ID"].ToString() == BudId[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                BudgetId = objReportProperty.Budget;

                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /*EnumerableRowCollection<DataRow> query =
                from budget in dtBudget.AsEnumerable()
                orderby budget.Field<Int32>("SELECT") descending
                select budget;
                DataView budgetview = query.AsDataView();*/

                if (dtBudget != null)
                {
                    dtBudget.DefaultView.Sort = "SELECT DESC";
                    DataView budgetview = dtBudget.DefaultView;

                    gvBudget.DataSource = budgetview.ToTable();
                    gvBudget.DataBind();
                }
            }
        }

        private void AssignReportSetUpValues()
        {
            ddlConsolidate.SelectedValue = objReportProperty.ConsolidateStateMent.ToString();
            ddlTitleAlignment.SelectedIndex = objReportProperty.TitleAlignment;
            chkVerticalLine.Checked = objReportProperty.ShowVerticalLine == 1 ? true : false;
            chkHorizontalLine.Checked = objReportProperty.ShowHorizontalLine == 1 ? true : false;
            chkShowTitle.Checked = objReportProperty.ShowTitles == 1 ? true : false;
            chkShowReportLogo.Checked = objReportProperty.ShowLogo == 1 ? true : false;
            chkShowPageNumber.Checked = objReportProperty.ShowPageNumber == 1 ? true : false;
            chkShowreportDate.Checked = objReportProperty.ShowPrintDate == 1 ? true : false;
            chkShowLedgerCode.Checked = objReportProperty.ShowLedgerCode == 1 ? true : false;
        }

        private void AssignProjectCategory()
        {
            DataTable dtProjectCategory = null;
            if (!string.IsNullOrEmpty(ProjectCategoryId))
            {
                dtProjectCategory = gvProjectCategory.DataSource != null ? (DataTable)gvProjectCategory.DataSource : ProjectCategorySource;
                string[] ProCategoryId = ProjectCategoryId.Split(',');
                for (int i = 0; i < ProCategoryId.Length; i++)
                {
                    if (dtProjectCategory.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtProjectCategory.Rows)
                        {
                            if (dr["PROJECT_CATOGORY_ID"].ToString() == ProCategoryId[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                //   ProjectCategoryId = objReportProperty.ProjectCategory;

                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /*EnumerableRowCollection<DataRow> query =
                from projectCategory in dtProjectCategory.AsEnumerable()
                orderby projectCategory.Field<Int32>("SELECT") descending
                select projectCategory;
                DataView ProjectCategoryView = query.AsDataView();*/

                dtProjectCategory.DefaultView.Sort = "SELECT DESC";
                DataView ProjectCategoryView = dtProjectCategory.DefaultView;

                gvProjectCategory.DataSource = ProjectCategoryView.ToTable();
                gvProjectCategory.DataBind();
            }
        }

        private void AssignSociety()
        {
            DataTable dtSociety = null;
            if (!string.IsNullOrEmpty(objReportProperty.Society))
            {
                dtSociety = gvSociety.DataSource != null ? (DataTable)gvSociety.DataSource : SocietySource;
                string[] SocietyId = objReportProperty.Society.Split(',');
                for (int i = 0; i < SocietyId.Length; i++)
                {
                    if (dtSociety.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtSociety.Rows)
                        {
                            if (dr["CUSTOMERID"].ToString() == SocietyId[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }

                }
                SocietyCode = objReportProperty.Society;

                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /* EnumerableRowCollection<DataRow> query =
                    from society in dtSociety.AsEnumerable()
                   orderby society.Field<Int32>("SELECT") descending
                   select society;

                 DataView SocietyView = query.AsDataView();*/
                //---------------------------------------------------------------------------------

                //dtSociety.DefaultView.Sort = "SELECT DESC";
                dtSociety.DefaultView.Sort = "SELECT DESC, Society Name";
                DataView SocietyView = dtSociety.DefaultView;

                gvSociety.DataSource = SocietyView.ToTable();
                gvSociety.DataBind();
            }
        }

        private void AssignBank()
        {
            DataTable dtBank = null;
            if (!string.IsNullOrEmpty(objReportProperty.Ledger))
            {
                dtBank = gvBankAccount.DataSource != null ? (DataTable)gvBankAccount.DataSource : BankSource;
                string[] BankIds = objReportProperty.Ledger.Split(',');
                for (int i = 0; i < BankIds.Length; i++)
                {
                    if (dtBank.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtBank.Rows)
                        {
                            if (dr["LEDGER_ID"].ToString() == BankIds[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                //      BankId = objReportProperty.Ledger;

                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /* EnumerableRowCollection<DataRow> query =
                from bank in dtBank.AsEnumerable()
                orderby bank.Field<Int32>("SELECT") descending
                select bank;
                  DataView BankView = query.AsDataView();*/
                dtBank.DefaultView.Sort = "SELECT DESC";
                DataView BankView = dtBank.DefaultView;

                gvBankAccount.DataSource = BankView.ToTable();
                gvBankAccount.DataBind();
            }

        }

        private void AssignLedgerGroup(bool frmFilter = false)
        {
            // ErrorLog log = new ErrorLog();
            // log.WriteError("Assign1");

            DataTable dtLedgerGroup = null;
            if (!string.IsNullOrEmpty(objReportProperty.LedgerGroup) || frmFilter)
            {
                dtLedgerGroup = gvLedgerGroup.DataSource != null ? (DataTable)gvLedgerGroup.DataSource : LedgerGroupSource;
                if (dtLedgerGroup != null)
                {
                    string[] LedgerGroupIds;
                    if (frmFilter)
                    {
                        LedgerGroupIds = LedgerGroupId.Split(',');
                    }
                    else
                    {
                        LedgerGroupIds = objReportProperty.LedgerGroup.Split(',');
                    }

                    //Clear all selection --------------------------------------------------------------------
                    foreach (DataRow dr in dtLedgerGroup.Rows)
                    {
                        dr["SELECT"] = "0";
                    }
                    dtLedgerGroup.AcceptChanges();
                    //--------------------------------------------------------------------

                    //   log.WriteError("Assign2 - " + LedgerGroupIds.Length.ToString() + "--" + objReportProperty.LedgerGroup);
                    for (int i = 0; i < LedgerGroupIds.Length; i++)
                    {
                        if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtLedgerGroup.Rows)
                            {
                                if (dr["GROUP_ID"].ToString() == LedgerGroupIds[i].ToString())
                                    dr["SELECT"] = "1";
                            }
                            // log.WriteError("Assign3");
                        }
                    }
                    if (!frmFilter)
                    {
                        LedgerGroupId = objReportProperty.LedgerGroup;
                    }

                    //On 29/09/2020, Removed Linq--------------------------------------------------
                    /*EnumerableRowCollection<DataRow> query =
                     from ledgergp in dtLedgerGroup.AsEnumerable()
                      orderby ledgergp.Field<Int32>("SELECT") descending
                      select ledgergp;
                    DataView LedgerGroupView = query.AsDataView();*/
                    //-----------------------------------------------------------------------------
                    dtLedgerGroup.DefaultView.Sort = "SELECT DESC";
                    DataView LedgerGroupView = dtLedgerGroup.DefaultView;

                    gvLedgerGroup.DataSource = LedgerGroupView.ToTable();
                    gvLedgerGroup.DataBind();
                }
            }

        }

        private void AssignLedger()
        {
            DataTable dtLedger = null;
            if (!string.IsNullOrEmpty(objReportProperty.Ledger))
            {
                dtLedger = gvLedger.DataSource != null ? (DataTable)gvLedger.DataSource : LedgerSource;
                if (dtLedger == null)
                {
                    SetLedgerDetailSource();
                    dtLedger = gvLedger.DataSource != null ? (DataTable)gvLedger.DataSource : LedgerSource;
                }
                string[] LedgerIds = objReportProperty.Ledger.Split(',');
                //if (dtLedger != null)
                //{
                for (int i = 0; i < LedgerIds.Length; i++)
                {
                    if (dtLedger.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtLedger.Rows)
                        {
                            if (dr["LEDGER_ID"].ToString() == LedgerIds[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                //     }
                LedgerId = objReportProperty.Ledger;

                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /*EnumerableRowCollection<DataRow> query =
             from ledger in dtLedger.AsEnumerable()
             orderby ledger.Field<Int32>("SELECT") descending
             select ledger;
                DataView LedgerView = query.AsDataView();*/

                dtLedger.DefaultView.Sort = "SELECT DESC";
                DataView LedgerView = dtLedger.DefaultView;

                LedgerView.RowFilter = "GROUP_ID IN (" + LedgerGroupId + ")";
                gvLedger.DataSource = LedgerView.ToTable();
                gvLedger.DataBind();
                LedgerView.RowFilter = string.Empty;


            }
        }

        private void AssignCongregationLedger(bool frmFilter = false)
        {
            DataTable dtConLedger = null;
            if (!string.IsNullOrEmpty(objReportProperty.CongregationLedger) || frmFilter)
            {
                dtConLedger = gvCongregationLedger.DataSource != null ? (DataTable)gvCongregationLedger.DataSource : ConLedgerSource;
                if (dtConLedger == null)
                {
                    SetCongregationLedgerSource();
                    dtConLedger = gvCongregationLedger.DataSource != null ? (DataTable)gvCongregationLedger.DataSource : ConLedgerSource;
                }
                string[] ConLedgerIds;
                if (frmFilter)
                {
                    ConLedgerIds = ConLedgerId.Split(',');
                }
                else
                {
                    ConLedgerIds = objReportProperty.CongregationLedger.Split(',');
                }

                //Clear all selection --------------------------------------------------------------------
                foreach (DataRow dr in dtConLedger.Rows)
                {
                    dr["SELECT"] = "0";
                }
                dtConLedger.AcceptChanges();
                //----------------------------------------------------------------------------------------

                for (int i = 0; i < ConLedgerIds.Length; i++)
                {
                    if (dtConLedger.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtConLedger.Rows)
                        {
                            if (dr["CON_LEDGER_ID"].ToString() == ConLedgerIds[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }

                if (!frmFilter)
                {
                    ConLedgerId = objReportProperty.CongregationLedger;
                }



                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /*EnumerableRowCollection<DataRow> query =
                  from ledger in dtLedger.AsEnumerable()
                  orderby ledger.Field<Int32>("SELECT") descending
                  select ledger;
                  DataView LedgerView = query.AsDataView();*/
            }
            else
            {
                dtConLedger = gvCongregationLedger.DataSource != null ? (DataTable)gvCongregationLedger.DataSource : ConLedgerSource;
            }

            if (!string.IsNullOrEmpty(LedgerGroupId) && !LedgerGroupId.Equals("0"))
            {
                string affectedConLedgerId = GetSelectedConLedgerIdByLG();
                //dtConLedger.DefaultView.RowFilter = "SELECT = 1";
                dtConLedger.DefaultView.RowFilter = "CON_LEDGER_ID IN (" + affectedConLedgerId + ")";
            }

            if (dtConLedger != null)
            {
                dtConLedger.DefaultView.Sort = "SELECT DESC, CON_CODE";
                gvCongregationLedger.DataSource = dtConLedger;
                gvCongregationLedger.DataBind();
            }

        }

        private void AssignCostCentre()
        {
            DataTable dtCostCentre = null;
            if (!string.IsNullOrEmpty(objReportProperty.CostCentre))
            {
                dtCostCentre = gvCostCentre.DataSource != null ? (DataTable)gvCostCentre.DataSource : CostCentreSource;
                string[] CostCentId = objReportProperty.CostCentre.Split(',');
                for (int i = 0; i < CostCentId.Length; i++)
                {
                    if (dtCostCentre.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCostCentre.Rows)
                        {
                            if (dr["COST_CENTRE_ID"].ToString() == CostCentId[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }

                }
                CostCentreId = objReportProperty.CostCentre;
                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /*EnumerableRowCollection<DataRow> query =
                from costcenter in dtCostCentre.AsEnumerable()
                orderby costcenter.Field<Int32>("SELECT") descending
                select costcenter;
                DataView CostCenterView = query.AsDataView();*/

                dtCostCentre.DefaultView.Sort = "SELECT DESC";
                DataView CostCenterView = dtCostCentre.DefaultView;

                gvCostCentre.DataSource = CostCenterView.ToTable();
                gvCostCentre.DataBind();
            }

        }

        private void AssignBranchOffice()
        {
            DataTable dtBranchOffice = null;
            if (!string.IsNullOrEmpty(objReportProperty.BranchOffice))
            {
                dtBranchOffice = gvBranch.DataSource != null ? (DataTable)gvBranch.DataSource : BranchSource;
                string[] branId = objReportProperty.BranchOffice.Split(',');
                for (int i = 0; i < branId.Length; i++)
                {
                    if (dtBranchOffice.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtBranchOffice.Rows)
                        {
                            if (dr["BRANCH_OFFICE_ID"].ToString() == branId[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                BranchCode = objReportProperty.BranchOffice;

                //On 29/09/2020, Removed Linq--------------------------------------------------------
                /*EnumerableRowCollection<DataRow> query =
                from branchoffice in dtBranchOffice.AsEnumerable()
                orderby branchoffice.Field<Int64>("SELECT") descending
                select branchoffice;
                DataView BranchOfficeView = query.AsDataView();*/

                dtBranchOffice.DefaultView.Sort = "SELECT DESC";
                DataView BranchOfficeView = dtBranchOffice.DefaultView;

                gvBranch.DataSource = BranchOfficeView.ToTable();
                gvBranch.DataBind();
            }
        }

        private string SetBankCallback()
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');
            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "BK":
                            {
                                if (string.IsNullOrEmpty(ProjectId))
                                {
                                    SetBankAccountSource();
                                }
                                else
                                {
                                    DataTable dtBankInfo = new DataTable();
                                    dtBankInfo = FetchBankByProject(ProjectId);
                                    if (objReportProperty.ReportId == "RPT-047")
                                    {
                                        using (BankSystem bankSystem = new BankSystem())
                                        {

                                            dtBankInfo = bankSystem.FetchFDByProjectId(ProjectId, BranchCode);
                                        }
                                    }
                                    gvBankAccount.DataSource = null;
                                    gvBankAccount.DataBind();
                                    gvBankAccount.DataSource = dtBankInfo;
                                    BankSource = dtBankInfo;
                                    gvBankAccount.DataBind();
                                }
                                break;
                            }

                    }
                }
                StringWriter swBank = new StringWriter();
                HtmlTextWriter htwBank = new HtmlTextWriter(swBank);
                gvBankAccount.RenderControl(htwBank);
                result = "BK@" + swBank.ToString();
                htwBank.Flush();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetLedgerCallback()
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');
            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "LG":
                            {
                                if (objReportProperty.ReportId.Equals("RPT-053") || objReportProperty.ReportId.Equals("RPT-054") || objReportProperty.ReportId.Equals("RPT-055"))
                                {

                                }
                                else
                                {
                                    DataTable dtLedgerInfo = new DataTable();
                                    if (string.IsNullOrEmpty(LedgerGroupId) || LedgerGroupId.Equals("0"))
                                    {
                                        SetLedgerDetailSource();
                                    }
                                    dtLedgerInfo = gvLedger.DataSource != null ? gvLedger.DataSource as DataTable : LedgerSource;
                                    if (dtLedgerInfo != null && dtLedgerInfo.Rows.Count > 0 && !string.IsNullOrEmpty(LedgerGroupId) && !LedgerGroupId.Equals("0"))
                                    {
                                        DataView dtLedgerFilter = LedgerSource.DefaultView;
                                        dtLedgerFilter.RowFilter = "GROUP_ID IN (" + LedgerGroupId + ")";
                                        dtLedgerInfo = null;
                                        dtLedgerInfo = dtLedgerFilter.ToTable();
                                    }
                                    gvLedger.DataSource = null;
                                    gvLedger.DataBind();
                                    gvLedger.DataSource = dtLedgerInfo;
                                    gvLedger.DataBind();
                                }
                            }
                            break;
                    }
                }
                StringWriter swLedger = new StringWriter();
                HtmlTextWriter htwLedger = new HtmlTextWriter(swLedger);
                gvLedger.RenderControl(htwLedger);
                result = "LG@" + swLedger.ToString();
                htwLedger.Flush();

            }

            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
            return result;
        }

        private string SetSocietyCallback()
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');
            try
            {
                string findvalue = Array.Find(aCriteria, element => element.Equals("SY", StringComparison.Ordinal));
                if (findvalue == "SY")
                {
                    SetSocietySource();
                    StringWriter sw1 = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw1);
                    gvSociety.RenderControl(htw);
                    result = "SY@" + sw1.ToString();
                    htw.Flush();

                    //07/11/2019, To clear already assigned selected Society and Projects ---------------------------------------------------------
                    SocietyCode = string.Empty;
                    //-----------------------------------------------------------------------------------------------------------------------------
                }

                /* On 27/11/2019, to skip loop
                 * for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "SY":
                            SetSocietySource();
                            StringWriter sw1 = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw1);
                            gvSociety.RenderControl(htw);
                            result = "SY@" + sw1.ToString();
                            htw.Flush();

                            //07/11/2019, To clear already assigned selected Society and Projects ---------------------------------------------------------
                            SocietyCode = string.Empty;
                            //-----------------------------------------------------------------------------------------------------------------------------
                            break;
                    }
                }*/

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetBranchFilterCallback(string SearchText)
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');
            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "BR":
                            DataView dvBranchFilter = BranchSource.DefaultView;
                            if (!string.IsNullOrEmpty(SearchText))
                            {
                                dvBranchFilter.RowFilter = "BRANCH_OFFICE_NAME LIKE '*" + SearchText.Replace("'", "''") + "*'";
                            }
                            gvBranch.DataSource = null;
                            gvBranch.DataSource = dvBranchFilter.ToTable();
                            gvBranch.DataBind();

                            dvBranchFilter.RowFilter = string.Empty;

                            StringWriter sw1 = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw1);
                            gvBranch.RenderControl(htw);
                            result = "BRF@" + sw1.ToString();
                            lblBranchRecordCount.Text = "Records # :" + (gvBranch.DataSource as DataTable).Rows.Count.ToString();
                            htw.Flush();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetSocietyFilterCallback(string SearchText)
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');

            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "SY":
                            DataView dvSocietyFilter = SocietySource.DefaultView;

                            if (!string.IsNullOrEmpty(SearchText))
                            {
                                dvSocietyFilter.RowFilter = "SOCIETY_FILTER LIKE '*" + SearchText.Replace("'", "''") + "*'";
                            }
                            gvSociety.DataSource = null;
                            gvSociety.DataSource = dvSocietyFilter.ToTable();
                            gvSociety.DataBind();
                            dvSocietyFilter.RowFilter = string.Empty;

                            StringWriter sw1 = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw1);
                            gvSociety.RenderControl(htw);
                            result = "SYF@" + sw1.ToString();
                            htw.Flush();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetProjectFilterCallback(string SearchText)
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');

            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "PJ":
                            DataView dvProjectFilter = ProjectSource.DefaultView;

                            if (!string.IsNullOrEmpty(SearchText))
                            {
                                dvProjectFilter.RowFilter = "PROJECT_NAME LIKE '*" + SearchText.Replace("'", "''") + "*'";
                            }
                            gvProject.DataSource = null;
                            gvProject.DataSource = dvProjectFilter.ToTable();
                            gvProject.DataBind();
                            dvProjectFilter.RowFilter = string.Empty;

                            StringWriter sw1 = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw1);
                            gvProject.RenderControl(htw);
                            result = "PJF@" + sw1.ToString();
                            htw.Flush();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetFilterCallback(string SearchText)
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');

            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "BU":
                            DataView dvBudget = BudgetSource.DefaultView;

                            if (!string.IsNullOrEmpty(SearchText))
                            {
                                dvBudget.RowFilter = "BUDGET_NAME LIKE '*" + SearchText.Replace("'", "''") + "*'";
                            }
                            gvBudget.DataSource = null;
                            gvBudget.DataSource = dvBudget.ToTable();
                            gvBudget.DataBind();
                            dvBudget.RowFilter = string.Empty;

                            StringWriter sw1 = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw1);
                            gvBudget.RenderControl(htw);
                            result = "BUF@" + sw1.ToString();
                            htw.Flush();
                            break;
                        case "CL":
                            DataView dvConLedger = ConLedgerSource.DefaultView;

                            if (!string.IsNullOrEmpty(SearchText))
                            {
                                dvConLedger.RowFilter = "CON_LEDGER_NAME LIKE '*" + SearchText.Replace("'", "''") + "*'";
                            }

                            gvCongregationLedger.DataSource = null;
                            gvCongregationLedger.DataSource = dvConLedger.ToTable();
                            gvCongregationLedger.DataBind();
                            dvConLedger.RowFilter = string.Empty;

                            AssignCongregationLedger(true);

                            using (StringWriter sw2 = new StringWriter())
                            {
                                using (HtmlTextWriter htw2 = new HtmlTextWriter(sw2))
                                {
                                    gvCongregationLedger.RenderControl(htw2);
                                    result = "CL@" + sw2.ToString();
                                    htw2.Flush();
                                }
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }
        private string SetLedgerFilterCallback(string SearchText)
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');

            try
            {
                DataView dvLedgerFilter = LedgerSource.DefaultView;
                //On 30/09/2020, to retain selected group's ledger-------------------------------------------------
                if (dvLedgerFilter != null && !string.IsNullOrEmpty(LedgerGroupId) && LedgerGroupId != "0")
                {
                    dvLedgerFilter.RowFilter = "GROUP_ID IN (" + LedgerGroupId + ")";
                    dvLedgerFilter = dvLedgerFilter.ToTable().DefaultView;
                }
                //-------------------------------------------------------------------------------------------------

                if (!string.IsNullOrEmpty(SearchText))
                {
                    dvLedgerFilter.RowFilter = "LEDGER LIKE '*" + SearchText.Replace("'", "''") + "*'";
                }
                gvLedger.DataSource = null;
                gvLedger.DataSource = dvLedgerFilter.ToTable();
                gvLedger.DataBind();
                dvLedgerFilter.RowFilter = string.Empty;
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw1);
                gvLedger.RenderControl(htw);
                result = "LGF@" + sw1.ToString();
                htw.Flush();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetLedgerGroupFilterCallback(string SearchText)
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');
            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "LG":
                            DataView dvLedgerGroupFilter = LedgerGroupSource.DefaultView;
                            if (!string.IsNullOrEmpty(SearchText))
                            {
                                dvLedgerGroupFilter.RowFilter = "GROUP LIKE '*" + SearchText.Replace("'", "''") + "*'";
                            }
                            gvLedgerGroup.DataSource = null;
                            gvLedgerGroup.DataSource = dvLedgerGroupFilter.ToTable();
                            gvLedgerGroup.DataBind();
                            dvLedgerGroupFilter.RowFilter = string.Empty;

                            AssignLedgerGroup(true);

                            StringWriter sw1 = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw1);
                            gvLedgerGroup.RenderControl(htw);
                            result = "LGGF@" + sw1.ToString();
                            htw.Flush();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetCostCentreFilterCallback(string SearchText)
        {

            try
            {
                DataView dvCostCentreFilter = LedgerGroupSource.DefaultView;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    dvCostCentreFilter.RowFilter = "COST_CENTRE_NAME LIKE '*" + SearchText.Replace("'", "''") + "*'";
                }
                gvCostCentre.DataSource = null;
                gvCostCentre.DataSource = dvCostCentreFilter.ToTable();
                gvCostCentre.DataBind();
                dvCostCentreFilter.RowFilter = string.Empty;

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw1);
                gvCostCentre.RenderControl(htw);
                result = "CCF@" + sw1.ToString();
                htw.Flush();

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetBankAccountFilterCallback(string SearchText)
        {
            try
            {
                DataView dvBankAccountFilter = BankSource.DefaultView;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    dvBankAccountFilter.RowFilter = "BANK LIKE '*" + SearchText.Replace("'", "''") + "*'";
                }
                gvBankAccount.DataSource = null;
                gvBankAccount.DataSource = dvBankAccountFilter.ToTable();
                gvBankAccount.DataBind();
                dvBankAccountFilter.RowFilter = string.Empty;

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw1);
                gvBankAccount.RenderControl(htw);
                result = "BKF@" + sw1.ToString();
                htw.Flush();

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetProjectCategoryFilterCallback(string SearchText)
        {
            try
            {
                DataView dvProjectCategoryFilter = ProjectCategorySource.DefaultView;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    dvProjectCategoryFilter.RowFilter = "BANK LIKE '*" + SearchText.Replace("'", "''") + "*'";
                }
                gvProjectCategory.DataSource = null;
                gvProjectCategory.DataSource = dvProjectCategoryFilter.ToTable();
                gvProjectCategory.DataBind();
                dvProjectCategoryFilter.RowFilter = string.Empty;

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw1);
                gvProjectCategory.RenderControl(htw);
                result = "PCF@" + sw1.ToString();
                htw.Flush();

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetProjectCallback()
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');
            try
            {
                string PJfindvalue = Array.Find(aCriteria, element => element.Equals("PJ", StringComparison.Ordinal));
                string PCfindvalue = Array.Find(aCriteria, element => element.Equals("PC", StringComparison.Ordinal));

                if (PCfindvalue == null)
                {
                    ProjectCategoryId = "0";
                }
                if (PJfindvalue == "PJ")
                {
                    SetProjectSource();
                    StringWriter sw2 = new StringWriter();
                    HtmlTextWriter htw2 = new HtmlTextWriter(sw2);
                    gvProject.RenderControl(htw2);
                    result = "PJ@" + sw2.ToString();
                    htw2.Flush();

                    //07/11/2019, To clear already assigned selected Society and Projects ---------------------------------------------------------
                    ProjectId = string.Empty;
                    //-----------------------------------------------------------------------------------------------------------------------------
                }

                /*On 27/11/2019, to skip loop
                 * for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "PJ":
                            SetProjectSource();
                            StringWriter sw2 = new StringWriter();
                            HtmlTextWriter htw2 = new HtmlTextWriter(sw2);
                            gvProject.RenderControl(htw2);
                            result = "PJ@" + sw2.ToString();
                            htw2.Flush();

                            //07/11/2019, To clear already assigned selected Society and Projects ---------------------------------------------------------
                            ProjectId = string.Empty;
                            //-----------------------------------------------------------------------------------------------------------------------------
                            break;
                    }
                }*/

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetBudgetCallback()
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');
            try
            {
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    switch (aCriteria[i])
                    {
                        case "BU":
                            SetBudgetSource();
                            StringWriter sw2 = new StringWriter();
                            HtmlTextWriter htw2 = new HtmlTextWriter(sw2);
                            gvBudget.RenderControl(htw2);
                            result = "BU@" + sw2.ToString();
                            htw2.Flush();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }

        private string SetConLedgerCallback()
        {
            try
            {
                DataTable dtConLedgerInfo = new DataTable();
                if (string.IsNullOrEmpty(LedgerGroupId) || LedgerGroupId.Equals("0"))
                {
                    SetCongregationLedgerSource();
                }
                dtConLedgerInfo = gvCongregationLedger.DataSource != null ? gvCongregationLedger.DataSource as DataTable : ConLedgerSource;
                if (dtConLedgerInfo != null && dtConLedgerInfo.Rows.Count > 0 && !string.IsNullOrEmpty(LedgerGroupId) && !LedgerGroupId.Equals("0"))
                {
                    //LedgerGroupId = "110,187";
                    /*var arrayLedgerGrpIds = LedgerGroupId.Split(',');
                    string affectedConLedgerId = "";
                    foreach (string id in arrayLedgerGrpIds)
                    {
                        dtConLedgerInfo.DefaultView.RowFilter = "";
                        dtConLedgerInfo.DefaultView.RowFilter = "(PARENT_GROUP_ID LIKE '%,"  + id + ",%') OR (GROUP_ID LIKE '%," + id + ",%')";
                        foreach (DataRowView drv in dtConLedgerInfo.DefaultView)
                        {
                            affectedConLedgerId +=drv["CON_LEDGER_ID"].ToString() + ",";
                        }
                    }
                    affectedConLedgerId = affectedConLedgerId.TrimEnd(',');
                    */
                    dtConLedgerInfo.DefaultView.RowFilter = "";
                    string affectedConLedgerId = GetSelectedConLedgerIdByLG();
                    DataView dvConLedgerFilter = ConLedgerSource.DefaultView;
                    if (!string.IsNullOrEmpty(affectedConLedgerId))
                    {
                        dvConLedgerFilter.RowFilter = "CON_LEDGER_ID IN (" + affectedConLedgerId + ")";
                    }
                    else
                    {
                        dvConLedgerFilter.RowFilter = "CON_LEDGER_ID IN (0)";
                    }
                    dtConLedgerInfo = null;
                    dtConLedgerInfo = dvConLedgerFilter.ToTable();
                }
                gvCongregationLedger.DataSource = null;
                gvCongregationLedger.DataBind();
                dtConLedgerInfo.DefaultView.Sort = "CON_CODE";
                gvCongregationLedger.DataSource = dtConLedgerInfo;
                gvCongregationLedger.DataBind();

                //SetCongregationLedgerSource();
                StringWriter sw2 = new StringWriter();
                HtmlTextWriter htw2 = new HtmlTextWriter(sw2);
                gvCongregationLedger.RenderControl(htw2);
                result = "CL@" + sw2.ToString();
                htw2.Flush();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return result;
        }
        #endregion

        #region Methods
        private void LoadReport(string reportId)
        {
            if (!string.IsNullOrEmpty(reportId))
            {
                objReportProperty.ReportId = reportId;
                AssignReportValues();
                Session["REPORTPROPERTY"] = objReportProperty;
                Response.Redirect("~/Report/ReportViewer.aspx?rid=" + objReportProperty.ReportId.ToString(), false);
            }
        }
        private void SetTabIndex()
        {
            int j = 0;
            for (int i = 0; i < TabReportCriteria.Tabs.Count; i++)
            {
                if (TabReportCriteria.Tabs[i].Visible)
                {
                    TabReportCriteria.Tabs[i].TabIndex = (short)j;
                    j++;
                }
            }
        }
        private bool IsValidCriteria()
        {
            bool isValid = true;
            if (TabCostCentre.Visible)
            {
                if (string.IsNullOrEmpty(CostCentreId) || CostCentreId.Equals("0"))
                {
                    TabReportCriteria.ActiveTabIndex = TabCostCentre.TabIndex;
                    isValid = false;
                    this.Message = MessageCatalog.ReportMessage.REPORT_COSTCENTRE_EMPTY;
                }
            }
            if (TabLedger.Visible && isValid)
            {
                if (objReportProperty.ReportId.Equals("RPT-053") || objReportProperty.ReportId.Equals("RPT-053") || objReportProperty.ReportId.Equals("RPT-053"))
                {
                    if (string.IsNullOrEmpty(LedgerGroupId) || LedgerGroupId.Equals("0"))
                    {
                        TabReportCriteria.ActiveTabIndex = TabLedger.TabIndex;
                        isValid = false;
                        objReportProperty.Ledger = LedgerId;
                        this.Message = MessageCatalog.ReportMessage.SUBSIDY_LEDGER_EMPTY;
                    }
                    if (string.IsNullOrEmpty(LedgerId) || LedgerId.Equals("0"))
                    {
                        TabReportCriteria.ActiveTabIndex = TabLedger.TabIndex;
                        isValid = false;
                        objReportProperty.Ledger = LedgerId;
                        this.Message = MessageCatalog.ReportMessage.CONTRIBUTION_LEDGER_EMPTY;
                    }
                    if ((string.IsNullOrEmpty(LedgerGroupId) || LedgerGroupId.Equals("0")) && (string.IsNullOrEmpty(LedgerId) || LedgerId.Equals("0")))
                    {
                        TabReportCriteria.ActiveTabIndex = TabLedger.TabIndex;
                        isValid = false;
                        objReportProperty.Ledger = LedgerId;
                        this.Message = MessageCatalog.ReportMessage.SUBSIDY_CONTRIBUTION_LEDGER_EMPTY;
                    }
                }
                else if (objReportProperty.ReportId.Equals("RPT-058"))
                {
                    if (LedgerGroupId.Contains(","))
                    {
                        TabReportCriteria.ActiveTabIndex = TabLedger.TabIndex;
                        isValid = false;
                        objReportProperty.LedgerGroup = LedgerGroupId;
                        this.Message = "You can not select More than one Ledger Group";
                    }
                }
                else
                {
                    if ((string.IsNullOrEmpty(LedgerId) || LedgerId.Equals("0")) && gvLedger.Visible)
                    {
                        TabReportCriteria.ActiveTabIndex = 5; //TabLedger.TabIndex;
                        isValid = false;
                        objReportProperty.Ledger = LedgerId;
                        SetLedgerDetailSource();
                        SetLedgerSource();
                        this.Message = MessageCatalog.ReportMessage.REPORT_LEDGER_EMPTY;
                    }
                }
            }

            if (tabDate.Visible)
            {
                if (dteFrom.Value == null || dteTo.Value == null)
                {
                    TabDateProperties();
                    TabReportCriteria.ActiveTabIndex = tabDate.TabIndex;
                }

                int noOfMOnth = MonthDiff(this.dteFrom.Date, this.dteTo.Date);
                if (noOfMOnth != 11 && objReportProperty.ReportId.Equals("RPT-187"))
                {
                    TabReportCriteria.ActiveTabIndex = 0;
                    this.dteFrom.Focus();
                    this.dteTo.Focus();
                    this.Message = "Report date range should be upto one year (12 months)";
                    isValid = false;
                }
            }
            if (TabBranch.Visible && isValid)
            {
                if (string.IsNullOrEmpty(BranchCode) || BranchCode.Equals("0"))
                {
                    //if (objReportProperty.ReportId.Equals("RPT-050") || objReportProperty.ReportId.Equals("RPT-051"))
                    //{
                    //    isValid = false;
                    //    objReportProperty.BranchOffice = BranchCode;
                    //    SetBranchSource();
                    //    TabReportCriteria.ActiveTabIndex = TabBranch.TabIndex;
                    //    this.Message = "A Branch must be selected";
                    //}

                    // objReportProperty.Society = SocietyCode;
                    // SetSocietySource();


                    isValid = false;
                    objReportProperty.BranchOffice = BranchCode;
                    SetBranchSource();
                    TabReportCriteria.ActiveTabIndex = TabBranch.TabIndex;
                    this.Message = "A Branch must be selected";   //MessageCatalog.ReportMessage.BRANCH_ID_EMPTY;
                }
            }
            if (TabSociety.Enabled && isValid)
            {
                if (string.IsNullOrEmpty(SocietyCode) || SocietyCode.Equals("0"))
                {
                    isValid = false;
                    objReportProperty.Society = SocietyCode;
                    SetSocietySource();
                    TabReportCriteria.ActiveTabIndex = TabSociety.TabIndex;
                    this.Message = "A Legal Entity must be selected"; //"Legal entity is empty, ";
                    //this.Message = MessageCatalog.ReportMessage.BRANCH_ID_EMPTY;
                }
            }
            if (TabProject.Visible && isValid)
            {
                if (gvBankAccount.Visible)
                {
                    if (string.IsNullOrEmpty(BankId) || BankId.Equals("0")) //BankId
                    {
                        isValid = false;
                        objReportProperty.Ledger = BankId;
                        SetProjectSource();
                        SetBankAccountSource();
                        TabReportCriteria.ActiveTabIndex = TabProject.TabIndex;
                        this.Message = MessageCatalog.ReportMessage.REPORT_BANK_EMPTY;
                    }
                }
                if (string.IsNullOrEmpty(ProjectId) || ProjectId.Equals("0"))//ProjectId
                {
                    isValid = false;
                    objReportProperty.Project = ProjectId;
                    SetProjectSource();
                    TabReportCriteria.ActiveTabIndex = TabProject.TabIndex;
                    this.Message = "A Project must be selected";//MessageCatalog.ReportMessage.REPORT_PROJECT_EMPTY;
                }
            }

            //On q6/08/2021---------------
            //If Not invalid, reassign already selected con ledgers, to retain filter
            if (isValid == false && gvCongregationLedger.Visible)
            {
                AssignLedgerGroup(true);
                AssignCongregationLedger(true);
            }
            //---------------------------
            return isValid;
        }

        private int MonthDiff(DateTime date1, DateTime date2)
        {
            if (date1.Month < date2.Month)
            {
                return (date2.Year - date1.Year) * 12 + date2.Month - date1.Month;
            }
            else
            {
                return (date2.Year - date1.Year - 1) * 12 + date2.Month - date1.Month + 12;
            }
        }

        private void AssignReportValues()
        {
            try
            {
                DateLedgerCriteria();//Set DateLedger Selected values
                DataTable dtCriteria = ReportsCriteria;
                if (dtCriteria != null)
                {
                    if (string.IsNullOrEmpty(Reportproperties))
                        Reportproperties = objReportProperty.ReportCriteria;
                    string Criteria = (string.IsNullOrEmpty(objReportProperty.ReportCriteria)) ? Reportproperties : objReportProperty.ReportCriteria;
                    string[] aCriteria = Criteria.Split('ÿ');
                    for (int i = 0; i < aCriteria.Length; i++)
                    {
                        if (aCriteria[i] == "DA")
                        {
                            objReportProperty.DateAsOn = this.Member.DateSet.ToDate(dteFrom.Value.ToString(), false).ToString("dd/MM/yyyy");
                            break;
                        }
                        else
                        {
                            if (aCriteria[i] == "DF")
                            {
                                sDateFrom = aCriteria[i];
                            }
                            if (aCriteria[i] == "DT")
                            {
                                sDateTo = aCriteria[i];
                            }
                            if (!string.IsNullOrEmpty(sDateFrom) && !string.IsNullOrEmpty(sDateTo))
                            {
                                if (!string.IsNullOrEmpty(dteFrom.Text.Trim()) && !string.IsNullOrEmpty(dteTo.Text.Trim()))
                                {
                                    if (dteFrom.Date > dteTo.Date)
                                    {
                                        DateTime date = Convert.ToDateTime(dteFrom.Value.ToString());
                                        dteFrom.Value = dteTo.Value;
                                        dteTo.Value = date.ToString();
                                        objReportProperty.DateFrom = this.Member.DateSet.ToDate(dteFrom.Value.ToString(), false).ToShortDateString();
                                        objReportProperty.DateTo = this.Member.DateSet.ToDate(dteTo.Value.ToString(), false).ToShortDateString();
                                        break;
                                    }
                                    else
                                    {
                                        objReportProperty.DateFrom = this.Member.DateSet.ToDate(dteFrom.Value.ToString(), false).ToShortDateString();
                                        objReportProperty.DateTo = this.Member.DateSet.ToDate(dteTo.Value.ToString(), false).ToShortDateString();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < aCriteria.Length; i++)
                    {
                        switch (aCriteria[i])
                        {
                            case "BR":
                                {
                                    string BranchId = string.Empty; // chinna commanded for 02.09.2021 to have branch office Name
                                    BranchId = SelectedBranchCode();

                                    //if (base.LoginUser.IsBranchOfficeUser)
                                    //{
                                    //    //BranchId = CurrentLoginUserBranchCode;

                                    //    BranchId = SelectedBranchCode();

                                    //}
                                    //else
                                    //{
                                    //}

                                    if (!string.IsNullOrEmpty(BranchId))
                                    {
                                        objReportProperty.BranchOffice = BranchId;
                                    }
                                    else
                                    {
                                        objReportProperty.BranchOffice = "0";
                                    }
                                    break;
                                }
                            case "SY":
                                {
                                    string Societyid = SelectedSocietyCode();
                                    if (!string.IsNullOrEmpty(Societyid))
                                    {
                                        objReportProperty.Society = Societyid;
                                    }
                                    else
                                    {
                                        objReportProperty.Society = "0";
                                    }
                                    break;
                                }
                            case "PJ":
                                {
                                    string ProjectId = SelectedProject();
                                    if (!string.IsNullOrEmpty(ProjectId))
                                    {
                                        objReportProperty.Project = ProjectId;
                                    }
                                    break;
                                }
                            case "PC":
                                {
                                    string ProjectCategoryID = SelectedProjectCategory();
                                    if (!string.IsNullOrEmpty(ProjectCategoryID))
                                    {
                                        ProjectCategoryId = objReportProperty.ProjectCategory = ProjectCategoryID;
                                    }
                                    else
                                    {
                                        objReportProperty.ProjectCategory = "0";
                                    }
                                    break;
                                }
                            case "BK":
                                {
                                    string LedgerId = SelectedBankDetails();

                                    if (!string.IsNullOrEmpty(LedgerId))
                                    {
                                        objReportProperty.Ledger = LedgerId;
                                    }
                                    else
                                    {
                                        objReportProperty.Ledger = "0";
                                    }

                                    break;
                                }
                            case "BU":
                                {
                                    string BudgetId = SelectedBudgetDetails();

                                    if (!string.IsNullOrEmpty(BudgetId))
                                    {
                                        objReportProperty.Budget = BudgetId;
                                    }
                                    break;
                                }
                            case "CL":
                                {
                                    string conledgerids = SelectedCongregationDetails();
                                    objReportProperty.CongregationLedger = "";
                                    if (!string.IsNullOrEmpty(conledgerids))
                                    {
                                        objReportProperty.CongregationLedger = conledgerids;
                                    }
                                    break;
                                }
                            case "LG":
                                {
                                    string SelectedLedgerGroupId = SelectedLedgerGroup();
                                    string LedgerId = SelectedLedgerGroupDetails();
                                    objReportProperty.Ledger = LedgerId;
                                    if (!string.IsNullOrEmpty(SelectedLedgerGroupId))
                                    {
                                        objReportProperty.LedgerGroup = SelectedLedgerGroupId;
                                    }
                                    else
                                    {
                                        objReportProperty.LedgerGroup = "0";
                                    }
                                    break;
                                }
                            case "CC":
                                {
                                    string CostCentreId = SelectedCostCentre();
                                    if (!string.IsNullOrEmpty(CostCentreId))
                                    {
                                        objReportProperty.CostCentre = CostCentreId;
                                    }
                                    else
                                    {
                                        objReportProperty.CostCentre = "0";
                                        return;
                                    }
                                    break;
                                }
                            case "FD":
                                {
                                    if (FDRegisterSelectedStatus > 0)
                                    {
                                        objReportProperty.FDRegisterStatus = FDRegisterSelectedStatus;
                                    }
                                    else
                                    {
                                        objReportProperty.FDRegisterStatus = 0;
                                        return;
                                    }
                                    break;
                                }
                            case "SIA": //On 29/09/2020, for SDBINM Verification report Show Detail for Inter Account
                                {
                                    objReportProperty.ShowInterAccountDetails = 0;
                                    if (OptShowDetailInterAccount.Visible && OptShowDetailInterAccount.Checked)
                                    {
                                        objReportProperty.ShowInterAccountDetails = 1;
                                    }
                                    break;
                                }
                            case "SPC": //On 29/09/2020, for SDBINM Verification report Show Detail for Province From/To
                                {
                                    objReportProperty.ShowProvinceFromToContributionDetails = 0;
                                    if (OptShowDetailProvinceFromTo.Visible && OptShowDetailProvinceFromTo.Checked)
                                    {
                                        objReportProperty.ShowProvinceFromToContributionDetails = 1;
                                    }
                                    break;
                                }
                            case "CA":  //On 08/06/2023, for Show All Cash REPORTS
                                {
                                    objReportProperty.ShowAllCash = 0;
                                    if (chkFilterCashOnly.Visible && chkFilterCashOnly.Checked)
                                    {
                                        objReportProperty.ShowAllCash = 1;
                                    }
                                    break;
                                }
                        }
                    }
                    if (dtCriteria != null && dtCriteria.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtCriteria.Rows.Count; i++)
                        {
                            string criteria = dtCriteria.Rows[i]["CRITERIYA"].ToString();
                            switch (criteria)
                            {
                                case "AL": //Attach Values to show all Ledgers
                                    {
                                        objReportProperty.IncludeAllLedger = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "BL": //Set Values to Show By Ledgers
                                    {
                                        objReportProperty.ShowByLedger = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "SBCC":
                                    {
                                        objReportProperty.ShowByCostCentre = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "CT":
                                    {
                                        objReportProperty.ShowByCostCentreCategory = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "BUCC":
                                    {
                                        objReportProperty.BreakUpByCostCentre = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }

                                case "ID":
                                    {
                                        objReportProperty.IncludeDetailed = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }

                                case "BG": // Set Values to Ledger Groups.
                                    {
                                        objReportProperty.ShowByLedgerGroup = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "AB": // Set Values to Detailed Balance.
                                    {
                                        objReportProperty.ShowDetailedBalance = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "BA": // Include Bank Account Details. 
                                    {
                                        objReportProperty.IncludeBankAccount = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "DB": //Set Values to Daily Balance.
                                    {
                                        objReportProperty.ShowDailyBalance = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "BD": //Include Bank Details
                                    {
                                        objReportProperty.IncludeBankDetails = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "IK": //Set Values to Include In Kind.
                                    {
                                        // objReportProperty.IncludeInKind = rboInKind.SelectedIndex;
                                        break;
                                    }
                                case "IJ": //Set Values to Include Journal
                                    {
                                        objReportProperty.IncludeJournal = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "GT": //Set Values to Group Total
                                    {
                                        objReportProperty.IncludeLedgerGroupTotal = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "AG": //Set Values to Attach Group
                                    {
                                        objReportProperty.IncludeLedgerGroup = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "AC": //Set Values to Attach Cost Centre
                                    {
                                        objReportProperty.IncludeCostCentre = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "MT": //Set Values to Month Wise Total
                                    {
                                        objReportProperty.ShowMonthTotal = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "AD": // Set Values to Donor Address
                                    {
                                        objReportProperty.ShowDonorAddress = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "CD": // Set Values to Donor Category
                                    {
                                        break;
                                    }
                                case "IN": //include Narration
                                    {
                                        objReportProperty.IncludeNarration = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                                case "LS":
                                    {
                                        objReportProperty.LedgerSummary = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                        break;
                                    }
                            }
                        }
                    }
                }
                SaveReportSetup();
                objReportProperty.SaveReportSetting();

            }

            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }
        private void SaveReportSetup()
        {
            try
            {
                objReportProperty.TitleAlignment = ddlTitleAlignment.SelectedIndex;
                objReportProperty.ShowLogo = chkShowReportLogo.Checked ? 1 : 0;
                objReportProperty.ShowPageNumber = chkShowPageNumber.Checked ? 1 : 0;
                objReportProperty.ReportDate = dtreportdate.Date.ToString();
                objReportProperty.ShowLedgerCode = chkShowLedgerCode.Checked ? 1 : 0;
                objReportProperty.ReportCodeType = ddlCode.SelectedIndex.ToString();

                //  objReportProperty.DateAsOn = dteFrom.Date.ToString();

                //      objReportProperty.ShowLedgerCode = chkShowLedgerCode.Checked ? 1 : 0;
                //      objReportProperty.ShowGroupCode = chkShowGroupCode.Checked ? 1 : 0;
                //      objReportProperty.SortByLedger = ddlSortByLedger.SelectedIndex;
                //      objReportProperty.SortByGroup = ddlSortByGroup.SelectedIndex;
                objReportProperty.ConsolidateStateMent = this.Member.NumberSet.ToInteger(ddlConsolidate.SelectedValue);
                objReportProperty.ShowHorizontalLine = chkHorizontalLine.Checked ? 1 : 0;
                objReportProperty.ShowVerticalLine = chkVerticalLine.Checked ? 1 : 0;
                objReportProperty.ShowTitles = chkShowTitle.Checked ? 1 : 0;
                objReportProperty.ShowPrintDate = chkShowreportDate.Checked ? 1 : 0;
                objReportProperty.HeaderInstituteSocietyName = (rgbTitle.SelectedIndex == 0) ? 0 : 1;
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }
        private void DateLedgerCriteria()
        {
            if (gvDateLedger.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in gvDateLedger.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkDateLedger");
                    if (chk.Checked)
                    {
                        for (int i = 0; i < ReportsCriteria.Rows.Count; i++)
                        {
                            if (ReportsCriteria.Rows[i]["NAME"].Equals(chk.Text))
                            {
                                ReportsCriteria.Rows[i]["SELECT"] = "1";
                                ReportsCriteria.AcceptChanges();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ReportsCriteria.Rows.Count; i++)
                        {
                            if (ReportsCriteria.Rows[i]["NAME"].Equals(chk.Text))
                            {
                                ReportsCriteria.Rows[i]["SELECT"] = "0";
                                ReportsCriteria.AcceptChanges();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method enables the tabs based on the report Id sent in the query string from the ReportSetting.xml file
        /// </summary>
        private void EnableTabs()
        {
            if (objReportProperty.ReportId != null)
            {
                AssignReportCriteria();//Load the current date for Date From and Date To Control.
                dtCriteria = ConstructTable();
                TabSociety.Visible = false;
                TabBranch.Visible = false;
                tabDate.Visible = false;
                TabBudget.Visible = false;
                TabReportSetup.Visible = true;
                upCongregationLedger.Visible = false;
                lblVerificationTitle.Visible = OptShowDetailInterAccount.Visible = OptShowDetailProvinceFromTo.Visible = OptVerficationNone.Visible = chkFilterCashOnly.Visible = false;

                this.PageTitle = objReportProperty.ReportTitle;
                string criteria = objReportProperty.ReportCriteria;

                string[] test = criteria.Split('ÿ');
                foreach (string s in test)
                {
                    switch (s)
                    {

                        case "DF": //Date From 
                            {
                                tabDate.Visible = true;
                                dteFrom.Visible = true;
                                break;
                            }
                        case "DT": //Date To
                            {
                                tabDate.Visible = true;

                                if (objReportProperty.ReportId == "RPT-065" || objReportProperty.ReportId == "RPT-077" ||
                                    objReportProperty.ReportId == "RPT-188" || objReportProperty.ReportId == "RPT-189")
                                {
                                    dteFrom.Enabled = false;
                                    dteTo.Enabled = false;
                                    dteFrom.Date = UtilityMember.DateSet.ToDate(settingProperty.YearFrom, false);
                                }
                                else
                                {
                                    dteTo.Visible = true;
                                }

                                if (objReportProperty.ReportId == "RPT-004" || objReportProperty.ReportId == "RPT-005" || objReportProperty.ReportId == "RPT-006" ||
                                    objReportProperty.ReportId == "RPT-035" || objReportProperty.ReportId == "RPT-036" || objReportProperty.ReportId == "RPT-037" ||
                                    objReportProperty.ReportId == "RPT-051" || objReportProperty.ReportId == "RPT-065" || objReportProperty.ReportId == "RPT-077" ||
                                    objReportProperty.ReportId == "RPT-062" || objReportProperty.ReportId == "RPT-063" || objReportProperty.ReportId == "RPT-064" ||
                                    objReportProperty.ReportId == "RPT-068" || objReportProperty.ReportId == "RPT-077" || objReportProperty.ReportId == "RPT-078" ||
                                    objReportProperty.ReportId == "RPT-079" || objReportProperty.ReportId == "RPT-080" ||
                                    objReportProperty.ReportId == "RPT-050" || objReportProperty.ReportId == "RPT-188" || objReportProperty.ReportId == "RPT-189")
                                {
                                    dteTo.MaxDate = dteFrom.Date.AddMonths(12).AddDays(-1);
                                    dteTo.Date = dteFrom.Date.AddMonths(12).AddDays(-1);
                                }



                                break;
                            }
                        case "DA": //Date As On 
                            {
                                tabDate.Visible = true;
                                lblDtTo.Visible = false;
                                dteTo.Visible = false;
                                dteFrom.Visible = true;
                                lblDtFrom.Text = DateCaption;
                                break;
                            }
                        case "AL": //Show All Ledgers.
                            {
                                if (objReportProperty.IncludeAllLedger != 0)
                                {
                                    dtCriteria.Rows.Add(1, "AL", "Include All Ledgers");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "AL", "Include All Ledger");

                                }
                                break;
                            }

                        case "BL":
                            {
                                if (objReportProperty.ShowByLedger != 0)
                                {
                                    dtCriteria.Rows.Add(1, "BL", "Show By Ledger");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "BL", "Show By Ledger");
                                }
                                break;
                            }

                        case "BG": //Show Groups
                            {
                                if (objReportProperty.ShowByLedgerGroup != 0)
                                {
                                    dtCriteria.Rows.Add(1, "BG", "Show By Ledger Group");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "BG", "Show By Ledger Group");
                                }
                                break;
                            }
                        case "FD": //Daily Balance
                            {
                                if (objReportProperty.FDRegisterStatus != 0)
                                {
                                    dtCriteria.Rows.Add(1, "");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(1, "");
                                }
                                break;
                            }

                        case "DB": //Daily Balance
                            {
                                if (objReportProperty.ShowDailyBalance != 0)
                                {
                                    dtCriteria.Rows.Add(1, "DB", "Show Daily Balance");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "DB", "Show Daily Balance");
                                }
                                break;
                            }
                        case "SBCC": //Show By Cost Centre
                            {
                                if (objReportProperty.ShowByCostCentre != 0)
                                {
                                    dtCriteria.Rows.Add(1, "SBCC", "Show By Cost Centre");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "SBCC", "Show By Cost Centre");
                                }
                                break;
                            }
                        case "CT": //Show By Cost Centre Category
                            {
                                if (objReportProperty.ShowByCostCentreCategory != 0)
                                {
                                    dtCriteria.Rows.Add(1, "CT", "Show By Cost Centre Category");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "CT", "Show By Cost Centre Category");
                                }
                                break;
                            }

                        case "BUCC": //Break Up By Cost Centre
                            {
                                if (objReportProperty.BreakUpByCostCentre != 0)
                                {
                                    dtCriteria.Rows.Add(1, "BUCC", "Break Up By Cost Centre");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "BUCC", "Break Up By Cost Centre");
                                }
                                break;
                            }

                        case "ID":
                            {
                                string caption = "Show Detailed";
                                if (objReportProperty.ReportId == "RPT-065")
                                {
                                    caption = "Generalate Summary";
                                }

                                if (objReportProperty.IncludeDetailed != 0)
                                {
                                    dtCriteria.Rows.Add(1, "ID", caption);
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "ID", caption);
                                }
                                break;
                            }

                        case "BD": //Include Bank Details
                            {
                                if (objReportProperty.IncludeBankDetails != 0)
                                {
                                    dtCriteria.Rows.Add(1, "BD", "Include Bank Details");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "BD", "Include Bank Details");
                                }
                                break;
                            }
                        case "BA": //Include Bank Account Number.
                            {
                                if (objReportProperty.IncludeBankAccount != 0)
                                {
                                    dtCriteria.Rows.Add(1, "BA", "Include A/c No");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "BA", "Include A/c No");
                                }
                                break;
                            }
                        case "GT": //Group Total
                            {
                                if (objReportProperty.IncludeLedgerGroupTotal != 0)
                                {
                                    dtCriteria.Rows.Add(1, "GT", "Group Total");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "GT", "Group Total");
                                }
                                break;
                            }
                        case "AG": //Attach All Groups
                            {
                                if (objReportProperty.IncludeLedgerGroup != 0)
                                {
                                    dtCriteria.Rows.Add(1, "AG", "Attach Group");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "AG", "Attach Group");
                                }
                                break;
                            }
                        case "AC": //Attach Cost Centre
                            {
                                if (objReportProperty.IncludeCostCentre != 0)
                                {
                                    dtCriteria.Rows.Add(1, "AC", "Attach Cost Centre");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "AC", "Attach Cost Centre");
                                }
                                break;
                            }
                        case "MT": //Month Wise Total.
                            {
                                if (objReportProperty.ShowMonthTotal != 0)
                                {
                                    dtCriteria.Rows.Add(1, "MT", "Show Month Wise Total");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "MT", "Show Month Wise Total");
                                }
                                break;
                            }
                        case "AD": //Attach Donor Details
                            {
                                if (objReportProperty.ShowDonorAddress != 0)
                                {
                                    dtCriteria.Rows.Add(1, "AD", "Attach Donor Address");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "AD", "Attach Donor Address");
                                }
                                break;
                            }
                        case "AB": //Attach Detailed Balance
                            {
                                if (objReportProperty.ShowDetailedBalance != 0)
                                {
                                    dtCriteria.Rows.Add(1, "AB", "Include Detailed Balance");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "AB", "Include Detailed Balance");
                                }
                                break;
                            }
                        case "IN": //Attach Narration
                            {
                                if (objReportProperty.IncludeNarration != 0)
                                {
                                    dtCriteria.Rows.Add(1, "IN", "Include Narration");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(1, "IN", "Include Narration");
                                }
                                break;
                            }
                        case "LS":
                            {
                                if (objReportProperty.LedgerSummary != 0)
                                {
                                    dtCriteria.Rows.Add(1, "LS", "Show Ledger Summary");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "LS", "Show Ledger Summary");
                                }
                                break;
                            }
                        case "BR":// Show Branch tab
                            {
                                TabBranch.Visible = true;
                                //TabBranch.Visible = true;
                                SetBranchSource();
                                if (objReportProperty.ReportId == "RPT-050" || objReportProperty.ReportId == "RPT-051" || objReportProperty.ReportId.Equals("RPT-185"))
                                {
                                    objReportProperty.ProjectTitle = string.Empty;
                                }
                                break;
                            }
                        case "SY":// Show Society Tab
                            {
                                TabSociety.Visible = true;
                                SetSocietySource();
                                break;
                            }
                        case "PJ": //Show Project tabs.
                            {
                                TabProject.Visible = true;
                                SetProjectSource();
                                break;
                            }
                        case "PC":
                            {
                                TabProjectCategory.Visible = true;
                                SetProjectCategorySource();
                                break;
                            }
                        case "BK": //Bank Account Details.
                            {
                                divBankFilter.Visible = true;
                                gvBankAccount.Visible = true;
                                divProject.Style["width"] = "50%";
                                divBank.Style["width"] = "48%";
                                SetBankAccountSource();
                                break;
                            }
                        case "LG": //Show Ledgers With Groups.
                            {
                                TabLedger.Visible = true;
                                SetLedgerSource();
                                break;
                            }
                        case "CL": //Show Congregation Ledger.
                            {
                                TabLedger.HeaderText = "Congregation Group";
                                upCongregationLedger.Visible = true;
                                SetCongregationLedgerSource();
                                //On 10/08/2021, For Generlate Ledger Balance report - To Hide Ledger grid
                                if (objReportProperty.ReportId == "RPT-178")
                                {
                                    upLedger.Visible = false;
                                }
                                break;
                            }
                        case "BU": // Get Budget Details.
                            {
                                TabBudget.Visible = true;
                                SetBudgetSource();
                                break;
                            }

                        case "CC": //Show Cost Centre Details.
                            {
                                TabCostCentre.Visible = true;
                                if (!String.IsNullOrEmpty(objReportProperty.CostCentre) && objReportProperty.CostCentre != "0")
                                {
                                    SetCostCentreSource();
                                }
                                else
                                {
                                    SetCostCentreSource();
                                }
                                break;
                            }
                        case "SIA": //On 29/09/2020, for SDBINM Verification report Show Detail for Inter Account
                            {
                                lblVerificationTitle.Visible = OptShowDetailInterAccount.Visible = OptVerficationNone.Visible = true;
                                if (objReportProperty.ShowInterAccountDetails != 0)
                                {
                                    OptShowDetailInterAccount.Checked = true;
                                }
                                break;
                            }
                        case "SPC": //On 29/09/2020, for SDBINM Verification report Show Detail for Province From/To
                            {
                                lblVerificationTitle.Visible = OptShowDetailProvinceFromTo.Visible = OptVerficationNone.Visible = true;
                                if (objReportProperty.ShowProvinceFromToContributionDetails != 0)
                                {
                                    OptShowDetailProvinceFromTo.Checked = true;
                                }
                                break;
                            }
                        case "CA":
                            {
                                chkFilterCashOnly.Visible = true;
                                if (objReportProperty.ShowAllCash != 0)
                                {
                                    chkFilterCashOnly.Checked = true;
                                }
                                break;
                            }

                    }
                }
                ReportsCriteria = dtCriteria;
                if (dtCriteria.Rows.Count != 0)
                {
                    gvDateLedger.DataSource = dtCriteria;
                    gvDateLedger.DataBind();
                }
                else
                {
                    gvDateLedger.Visible = false;
                }

            }
        }

        /// <summary>
        /// Set Report Criteria for Date From and Date To
        /// </summary>
        private void AssignReportCriteria()
        {
            DateTime Today = DateTime.Now;
            try
            {
                if (!String.IsNullOrEmpty(objReportProperty.DateFrom) && !String.IsNullOrEmpty(objReportProperty.DateTo))
                {
                    if (objReportProperty.DateSet.ToDate(settingProperty.YearFrom, false) > objReportProperty.DateSet.ToDate(objReportProperty.DateFrom, false))
                        dteFrom.Date = objReportProperty.DateSet.ToDate(settingProperty.YearFrom, false);
                    else
                    {
                        dteFrom.Date = objReportProperty.DateSet.ToDate(objReportProperty.DateFrom, false);
                        dteTo.Date = objReportProperty.DateSet.ToDate(objReportProperty.DateTo, false);
                    }

                } // else is removed
                else if (!String.IsNullOrEmpty(objReportProperty.DateAsOn))
                {
                    dteFrom.Date = objReportProperty.DateSet.ToDate(objReportProperty.DateAsOn, false);
                }
                if (!String.IsNullOrEmpty(objReportProperty.ReportDate))
                {
                    dtreportdate.Date = objReportProperty.DateSet.ToDate(objReportProperty.ReportDate, false);
                }
                else
                {
                    dtreportdate.Date = objReportProperty.DateSet.ToDate(DateTime.Today.ToString(), false);
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
        }

        /// <summary>
        /// Enable report setup criteria based on the reports
        /// </summary>
        public void EnableReportSetupProperties()
        {
            switch (objReportProperty.ReportId)
            {
                case "RPT-007":
                case "RPT-008":
                case "RPT-009":
                case "RPT-010":
                case "RPT-013":
                    {
                        //             ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = false;
                        break;
                    }

                case "RPT-014":
                case "RPT-015":
                case "RPT-018": // Cash Flow
                case "RPT-019": // Bank Flow
                case "RPT-020": // FC Purpose
                case "RPT-021": // FC Purpose Donor Institutional
                case "RPT-022": // FC Purpose Donor Individual
                case "RPT-039": // FC Country. 
                    {
                        //             chkShowLedgerCode.Enabled = ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = false;
                        break;
                    }
                case "RPT-011":
                case "RPT-012":
                case "RPT-016":
                case "RPT-017":
                    {
                        //              ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = false;
                        break;
                    }
                case "RPT-027":
                case "RPT-028":
                    //case "RPT-029":
                    {
                        //               chkShowLedgerCode.Enabled = ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = true;
                        chkHorizontalLine.Enabled = false;
                        break;
                    }
                case "RPT-030":
                    {
                        //                chkShowLedgerCode.Enabled = ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = true;
                        chkHorizontalLine.Enabled = true;
                        break;
                    }
            }
        }

        private void SetCostCentreSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.SetCostCentreSource, DataBaseType.HeadOffice))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            CostCentreSelected = resultArgs.DataSource.Table;
                            CostCentreSource = resultArgs.DataSource.Table;
                            gvCostCentre.DataSource = resultArgs.DataSource.Table;
                            gvCostCentre.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { } //AssignCostCentre();
        }

        /// <summary>
        /// This method loadws the FDStatus as Active and Inactive
        /// </summary>
        /// <param name="ddlfd"></param>
        private void BindFDStatus(DropDownList ddlfd)
        {
            this.Member.ComboSet.BindEnum2DropDownList(ddlfd, typeof(FDStatus));
            if (FDRegisterSelectedStatus > 0)
            {
                ddlfd.SelectedValue = FDRegisterSelectedStatus.ToString();
            }
        }

        private void SetBranchSource()
        {
            try
            {
                using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    if (base.LoginUser.IsHeadOfficeUser)
                    {
                        resultArgs = headOfficeSystem.FetchBranchByHeadOffice(base.HeadOfficeCode, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ?
                            DataBaseType.Portal : DataBaseType.HeadOffice);
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            BranchSelected = resultArgs.DataSource.Table;
                            BranchSource = resultArgs.DataSource.Table;//set Branch in ViewState
                            objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gvBranch.DataSource = null;
                            gvBranch.DataSource = resultArgs.DataSource.Table;
                            gvBranch.DataBind();
                        }
                    }
                    else
                    {
                        resultArgs = headOfficeSystem.FetchBranchByHeadOffice(base.HeadOfficeCode, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ?
                               DataBaseType.Portal : DataBaseType.HeadOffice);

                        string branchCode = base.LoginUser.LoginUserBranchOfficeCode;
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            DataView dvBranch = resultArgs.DataSource.Table.DefaultView;
                            dvBranch.RowFilter = "BRANCH_OFFICE_CODE='" + branchCode + "'";
                            DataTable dtBranchFilter = new DataTable();
                            dtBranchFilter = dvBranch.ToTable();
                            if (dtBranchFilter.Rows.Count == 1)
                            {
                                dtBranchFilter.Rows[0]["SELECT"] = "1";
                                dtBranchFilter.AcceptChanges();
                                BranchCode = dtBranchFilter.Rows[0]["BRANCH_OFFICE_ID"].ToString();
                                CurrentLoginUserBranchCode = BranchCode;
                                objReportProperty.BranchOffice = BranchCode;
                                if (!string.IsNullOrEmpty(BranchCode))
                                {
                                    SetSocietySource();
                                    SetProjectSource();
                                    SetLedgerDetailSource();
                                }
                            }
                            gvBranch.DataSource = dtBranchFilter;
                            gvBranch.DataBind();
                            BranchSource = dtBranchFilter;//set Branch in ViewState
                            gvBranch.Enabled = false;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }//AssignBranchOffice();
        }

        private void SetSocietySource()
        {
            try
            {
                //  using (BalanceSystem balanceSystem = new BalanceSystem())
                using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    resultArgs = !string.IsNullOrEmpty(BranchCode) && BranchCode != "0" ? legalEntitySystem.FetchSocietybyBranch(BranchCode) : legalEntitySystem.FetchLegalEntity();

                    //On 17/11/2020
                    //if (objReportProperty.ReportId != null && objReportProperty.ReportId == "RPT-062" || objReportProperty.ReportId == "RPT-064" || objReportProperty.ReportId == "RPT-063" || objReportProperty.ReportId == "RPT-068" || objReportProperty.ReportId == "RPT-078")
                    //{
                    //    resultArgs = legalEntitySystem.FetchLegalEntity();
                    //}

                    // Commanded by Chinna for Enable the Branch 30/05/2024
                    //if (objReportProperty.IsSDBRomeReports)
                    //{
                    //    resultArgs = legalEntitySystem.FetchBranchAttachedSociety();
                    //}

                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.DefaultView.Sort = "Society Name";
                        SocietySelected = resultArgs.DataSource.Table;
                        SocietySource = resultArgs.DataSource.Table; //set Society in ViewState
                        objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                        gvSociety.DataSource = null;
                        gvSociety.DataSource = resultArgs.DataSource.Table;
                        gvSociety.DataBind();
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                }
            }

            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
        }

        private void SetBudgetSource()
        {
            try
            {
                if (string.IsNullOrEmpty(objReportProperty.ProjectId) || objReportProperty.ProjectId == "0")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetNames, DataBaseType.HeadOffice))
                    {
                        DateTime YearFrom = this.Member.DateSet.ToDate(objSettingProperty.YearFrom, false);
                        DateTime YearTo = this.Member.DateSet.ToDate(objSettingProperty.YearTo, false);

                        //if (!string.IsNullOrEmpty(objReportProperty.Budget))
                        //{
                        //    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, objReportProperty.Budget);
                        //}
                        dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                        dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);

                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                BudgetSelected = resultArgs.DataSource.Table;
                                BudgetSource = resultArgs.DataSource.Table;
                                objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                gvBudget.DataSource = null;
                                gvBudget.DataSource = resultArgs.DataSource.Table;
                                gvBudget.DataBind();
                            }
                        }
                    }
                }
                else
                {
                    DataTable dtBudgetDetails = null;
                    using (BudgetSystem budgetSystem = new BudgetSystem())
                    {
                        dtBudgetDetails = budgetSystem.FetchBudgetsByProjects(objReportProperty.ProjectId);
                    }
                    if (dtBudgetDetails != null)
                    {
                        BudgetSelected = dtBudgetDetails;
                        BudgetSource = dtBudgetDetails;
                        gvBudget.DataSource = null;
                        gvBudget.DataSource = dtBudgetDetails;
                        gvBudget.DataBind();
                    }

                }

                if (!string.IsNullOrEmpty(objReportProperty.Budget) && objReportProperty.Budget != "0")
                {
                    DataTable dtBudget = (DataTable)gvBudget.DataSource;
                    string[] BudgetId = objReportProperty.Budget.ToString().Split(',');
                    foreach (DataRow dr in dtBudget.Rows)
                    {
                        for (int i = 0; i < BudgetId.Length; i++)
                        {
                            if (dr["BUDGET_ID"].ToString() == BudgetId[i])
                                dr["SELECT"] = 1;
                        }
                    }
                    dtBudget.DefaultView.Sort = "SELECT DESC";
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }

        private void SetLedgerSource()
        {
            try
            {
                if (objReportProperty.ReportId.Equals("RPT-053") || objReportProperty.ReportId.Equals("RPT-054") || objReportProperty.ReportId.Equals("RPT-055"))
                {
                    using (MappingSystem mappingSystem = new MappingSystem())
                    {
                        ResultArgs resultArgs = mappingSystem.LoadMappedGeneralateLedgers();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            gvLedger.Columns[1].HeaderText = "Contribution Ledger";
                            var ContributionMappedLedgers = from row in resultArgs.DataSource.Table.AsEnumerable()
                                                            where row.Field<UInt32>("IS_SUBSIDY") != 0
                                                            select row;
                            if (ContributionMappedLedgers.Count() > 0)
                            {
                                gvLedger.DataSource = ContributionMappedLedgers.CopyToDataTable();
                                ContributionLedgerSource = ContributionMappedLedgers.CopyToDataTable();
                            }
                            else
                                gvLedger.DataSource = resultArgs.DataSource.Table.Clone();
                            gvLedger.DataBind();

                            DataTable dtMappledLedgers = resultArgs.DataSource.Table.Copy();
                            if (dtMappledLedgers != null)
                            {
                                if (dtMappledLedgers.Columns.Contains(this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName))
                                {
                                    //Rename  MAPPING_LEDGER_ID to GROUP_ID because of reusing the GroupId  
                                    dtMappledLedgers.Columns[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ColumnName = "GROUP_ID";
                                    dtMappledLedgers.Columns["LEDGER"].ColumnName = "GROUP";
                                    gvLedgerGroup.Columns[1].HeaderText = "Subsidy Ledger";
                                    var SubsidyMappedLedgers = from row in dtMappledLedgers.AsEnumerable()
                                                               where row.Field<UInt32>("IS_SUBSIDY") == 0
                                                               select row;
                                    if (SubsidyMappedLedgers.Count() > 0)
                                    {
                                        gvLedgerGroup.DataSource = SubsidyMappedLedgers.CopyToDataTable();
                                        SubsidyLedgerSource = SubsidyMappedLedgers.CopyToDataTable();
                                    }
                                    else
                                        gvLedgerGroup.DataSource = dtMappledLedgers.Clone();
                                    gvLedgerGroup.DataBind();
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.SetLedgerSource, DataBaseType.HeadOffice))
                    {
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                LedgerGroupSelected = resultArgs.DataSource.Table;
                                LedgerGroupSource = resultArgs.DataSource.Table;
                                gvLedger.DataSource = null;
                                gvLedgerGroup.DataSource = resultArgs.DataSource.Table;
                                gvLedgerGroup.DataBind();
                            }
                            if (string.IsNullOrEmpty(objReportProperty.Ledger) || objReportProperty.Ledger == "0")
                            {
                                SetLedgerDetailSource();
                            }
                            //if (objReportProperty.ReportId.Equals("RPT-058"))
                            //{   //Single ledger must be selected for monthly ledgerGroup report
                            //    ((CheckBox)(gvLedgerGroup.HeaderRow.FindControl("chkLedgerGroupSelectAll"))).Visible = false;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { } //AssignLedger();
        }


        /// <summary>
        /// On 09/08/2021, to show Congregation Report
        /// </summary>
        private void SetCongregationLedgerSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.SetCongregationLedgerSource, DataBaseType.HeadOffice))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            ConLedgerSelected = resultArgs.DataSource.Table;
                            ConLedgerSource = resultArgs.DataSource.Table;
                            gvCongregationLedger.DataSource = null;
                            gvCongregationLedger.DataSource = resultArgs.DataSource.Table;
                            gvCongregationLedger.DataBind();
                        }
                    }
                }

                //On 14/08/2021, to get Congregation Ledger with LG to filter concern ledger groups 
                //for selected LG
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchConLedgerIdByLedgerGroupId, DataBaseType.HeadOffice))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        CongregationLedgeWithLedgerGroup = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { } //AssignLedger();
        }

        private void SetLedgerDetailSource()
        {
            try
            {

                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.SetLedgerDetailSource, DataBaseType.HeadOffice))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtLegersource = resultArgs.DataSource.Table;
                            if (base.LoginUser.IsBranchOfficeUser)
                            {
                                using (DataManager dataManagerLedger = new DataManager(SQLCommand.LedgerBank.FetchLedgerDetailSourcebyBranch, DataBaseType.HeadOffice))
                                {
                                    if (!string.IsNullOrEmpty(BranchCode) && BranchCode != "0")
                                    {
                                        dataManagerLedger.Parameters.Add("BRANCH_ID", BranchCode);
                                        dataManagerLedger.DataCommandArgs.IsDirectReplaceParameter = true;
                                        resultArgs = dataManagerLedger.FetchData(DataSource.DataTable);
                                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                        {
                                            DataView dvLedgerSource = new DataView(dtLegersource);
                                            dvLedgerSource.RowFilter = "IS_BRANCH_LEDGER=0";
                                            dtLegersource = dvLedgerSource.ToTable();
                                            dtLegersource.Merge(resultArgs.DataSource.Table);
                                        }
                                    }

                                }
                            }
                            LedgerSelected = dtLegersource;
                            LedgerSource = dtLegersource;
                            gvLedger.DataSource = null;
                            gvLedger.DataSource = dtLegersource;
                            gvLedger.DataBind();

                            if (objReportProperty.ReportId.Equals("RPT-058"))
                            {   //Single ledger must be selected for monthly ledger report
                                ((CheckBox)(gvLedger.HeaderRow.FindControl("chkLedgerSelectAll"))).Visible = false;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { } //AssignLedgerGroup();
        }

        private void SetBankAccountSource()
        {
            try
            {
                if (string.IsNullOrEmpty(objReportProperty.Project) || objReportProperty.Project == "0")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Bank.SelectAllBank, DataBaseType.HeadOffice))
                    {
                        if (!string.IsNullOrEmpty(BranchCode) && BranchCode != "0")
                        {
                            dataManager.Parameters.Add("BRANCH_ID", BranchCode);
                        }
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                if (objReportProperty.ReportId == "RPT-047")
                                {
                                    DataTable dtFD = FetchAllFixedDeposit();
                                    BankSource = dtFD;
                                    gvBankAccount.DataSource = null;
                                    gvBankAccount.DataSource = dtFD;
                                    gvBankAccount.DataBind();
                                }
                                else
                                {
                                    BankSource = resultArgs.DataSource.Table;
                                    BankSelected = resultArgs.DataSource.Table;
                                    objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                    gvBankAccount.DataSource = null;
                                    gvBankAccount.DataSource = resultArgs.DataSource.Table;
                                    gvBankAccount.DataBind();
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataTable dtBankDetails = FetchBankByProject(objReportProperty.Project);
                    if (objReportProperty.ReportId == "RPT-047")
                    {
                        using (BankSystem bankSystem = new BankSystem())
                        {
                            dtBankDetails = bankSystem.FetchFDByProjectId(ProjectId, BranchCode);
                        }
                    }
                    gvBankAccount.DataSource = null;
                    gvBankAccount.DataBind();
                    gvBankAccount.DataSource = dtBankDetails;
                    gvBankAccount.DataBind();
                    BankSelected = dtBankDetails;
                    BankSource = dtBankDetails;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }//AssignBank();
        }

        /// <summary>
        /// This method fetches the All the FD Ledgers
        /// </summary>
        /// <returns></returns>
        private DataTable FetchAllFixedDeposit()
        {
            using (DataManager data = new DataManager(SQLCommand.Bank.SelectAllFD, DataBaseType.HeadOffice))
            {
                if (!string.IsNullOrEmpty(BranchCode) && BranchCode != "0")
                {
                    data.Parameters.Add("BRANCH_ID", BranchCode);
                }
                ResultArgs resultArgs = data.FetchData(DataSource.DataTable);
                BankSelected = resultArgs.DataSource.Table;
                objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                return resultArgs.DataSource.Table;
            }
        }

        private DataTable FetchBankByProject(string ProjectId)
        {
            DataTable dtBankDetails = new DataTable();
            try
            {
                using (BankSystem bankSystem = new BankSystem())
                {
                    dtBankDetails = bankSystem.FetchBankByProjectId(ProjectId, BranchCode);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally
            {
            }
            return dtBankDetails;
        }

        private void SetProjectSource()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    if (base.LoginUser.IsBranchOfficeUser && CurrentLoginUserBranchCode != "0" && !string.IsNullOrEmpty(CurrentLoginUserBranchCode))
                    {
                        resultArgs = !string.IsNullOrEmpty(SocietyCode) && SocietyCode != "0" ? projectSystem.FetchProjetBySociety(SocietyCode, CurrentLoginUserBranchCode, ProjectCategoryId) : projectSystem.FetchAllProjects(CurrentLoginUserBranchCode);
                    }
                    if (base.LoginUser.IsHeadOfficeUser)
                    {
                        resultArgs = !string.IsNullOrEmpty(SocietyCode) && SocietyCode != "0" ? projectSystem.FetchProjetBySociety(SocietyCode, BranchCode, ProjectCategoryId) : projectSystem.FetchAllProjects(BranchCode);
                    }
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        if (objReportProperty.ReportId.Equals("RPT-043") || objReportProperty.ReportId.Equals("RPT-054"))
                        {
                            DataTable dtForeignInfo = FetchForeignProjects(resultArgs.DataSource.Table);
                            ProjectSelected = dtForeignInfo;
                            ProjectSource = dtForeignInfo;
                            if (!string.IsNullOrEmpty(ProjectCategoryId) && ProjectCategoryId != "0")
                            {
                                DataView dv = ProjectSource.DefaultView;
                                dv.RowFilter = String.Format("PROJECT_CATEGORY_ID IN({0})", String.Format("'{0}'", String.Join("','", ProjectCategoryId.Split(','))));
                                ProjectSource = dv.ToTable();
                            }
                            objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gvProject.DataSource = null;
                            gvProject.DataSource = dtForeignInfo;
                            gvProject.DataBind();
                        }
                        else
                        {
                            ProjectSelected = resultArgs.DataSource.Table;
                            ProjectSource = resultArgs.DataSource.Table; ;//set Project in ViewState
                            if (!string.IsNullOrEmpty(ProjectCategoryId) && ProjectCategoryId != "0")
                            {
                                DataView dv = ProjectSource.DefaultView;
                                dv.RowFilter = String.Format("PROJECT_CATEGORY_ID IN({0})", String.Format("'{0}'", String.Join("','", ProjectCategoryId.Split(','))));
                                ProjectSource = dv.ToTable();
                            }
                            objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gvProject.DataSource = null;
                            gvProject.DataSource = ProjectSource;
                            gvProject.DataBind();
                        }
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { } //AssignProject();
        }

        private void SetProjectCategorySource()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    resultArgs = projectSystem.FetchProjectCategroy(DataBaseType.HeadOffice);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        ProjectCategorySource = resultArgs.DataSource.Table; //set Project Category in ViewState
                        objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                        gvProjectCategory.DataSource = null;
                        gvProjectCategory.DataSource = resultArgs.DataSource.Table;
                        gvProjectCategory.DataBind();
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                }
            }

            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }

        }

        /// <summary>
        /// This method filters the foreign projects of the branch office
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable FetchForeignProjects(DataTable dt)
        {
            DataTable dtForeign = dt;
            DataView dv = new DataView(dt);
            dv.RowFilter = "DIVISION='Foreign'";
            DataTable SelectedLedgers = dv.ToTable();
            return SelectedLedgers;

        }

        private DataTable ConstructTable()
        {
            DataTable dtTable = new DataTable();
            dtTable.Columns.Add(new DataColumn("SELECT", typeof(int)));
            dtTable.Columns.Add(new DataColumn("CRITERIYA", typeof(string)));
            dtTable.Columns.Add(new DataColumn("NAME", typeof(string)));
            return dtTable;
        }

        /// <summary>
        /// Get the selected project id 
        /// </summary>
        /// <returns></returns>
        private string SelectedProject()
        {
            string selectedProjectId = string.Empty;
            string ProjectName = string.Empty;
            try
            {
                if (gvProject.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(ProjectId) && ProjectSource != null)
                    {
                        ArrayList ProjectIds = new ArrayList(ProjectId.Split(new char[] { ',' }));
                        for (int Val = 0; Val < ProjectSource.Rows.Count; Val++)
                        {
                            ProjectSource.Rows[Val][SELECT] = "0";
                        }
                        foreach (string pid in ProjectIds)
                        {
                            int i = 0;
                            for (i = 0; i < ProjectSource.Rows.Count; i++)
                            {
                                if (pid.Equals(ProjectSource.Rows[i][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()))
                                {
                                    ProjectSource.Rows[i][SELECT] = "1";
                                    i = ProjectSource.Rows.Count;
                                }
                            }
                            ProjectSource.AcceptChanges();
                        }

                    }
                    gvProject.DataSource = null;
                    gvProject.DataSource = ProjectSource;
                    gvProject.DataBind();
                }

                DataTable dtProject = gvProject.DataSource as DataTable;
                if (dtProject != null && dtProject.Rows.Count != 0)
                {
                    DataView dvProject = dtProject.DefaultView;
                    if (dvProject != null && dvProject.Count > 0)
                    {
                        dvProject.RowFilter = "SELECT = 1";
                        foreach (DataRow dr in dvProject.ToTable().Rows)
                        {
                            selectedProjectId += dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                            ProjectName += dr[this.AppSchema.Project.PROJECT_NAMEColumn.ColumnName].ToString() + ", ";
                        }
                        selectedProjectId = selectedProjectId.TrimEnd(',');
                        ProjectName = ProjectName.TrimEnd(' ');

                        if (ProjectName.Trim() != string.Empty)
                        {
                            if (ProjectSource.Rows.Count == dvProject.ToTable().Rows.Count)
                            {
                                objReportProperty.ProjectTitle = ddlConsolidate.SelectedValue == "2" ? "  " + ProjectName.TrimEnd(',') : "  " + SCONSTATEMENT; // dvProject.ToTable().Rows.Count == 1
                            }
                            else
                            {
                                objReportProperty.ProjectTitle = ProjectName.TrimEnd(',');
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
            return selectedProjectId;
        }

        private string SelectedProjectCategory()
        {
            string selectedProjectCategoryId = string.Empty;
            string ProjectCategoryName = string.Empty;
            try
            {
                if (gvProjectCategory.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(ProjectCategoryId) && ProjectCategorySource != null)
                    {
                        ArrayList ProjectCategoryIds = new ArrayList(ProjectCategoryId.Split(new char[] { ',' }));
                        for (int Val = 0; Val < ProjectCategorySource.Rows.Count; Val++)
                        {
                            ProjectCategorySource.Rows[Val][SELECT] = "0";
                        }
                        foreach (string pid in ProjectCategoryIds)
                        {
                            int i = 0;
                            for (i = 0; i < ProjectCategorySource.Rows.Count; i++)
                            {
                                if (pid.Equals(ProjectCategorySource.Rows[i][this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName].ToString()))
                                {
                                    ProjectCategorySource.Rows[i][SELECT] = "1";
                                    i = ProjectCategorySource.Rows.Count;
                                }
                            }
                            ProjectCategorySource.AcceptChanges();
                        }

                    }
                    else
                    {
                        for (int Val = 0; Val < ProjectCategorySource.Rows.Count; Val++)
                        {
                            ProjectCategorySource.Rows[Val][SELECT] = "0";
                        }
                        ProjectCategorySource.AcceptChanges();
                    }
                    gvProjectCategory.DataSource = null;
                    gvProjectCategory.DataSource = ProjectCategorySource;
                    gvProjectCategory.DataBind();
                }

                DataTable dtProjectCategory = gvProjectCategory.DataSource as DataTable;
                if (dtProjectCategory != null && dtProjectCategory.Rows.Count != 0)
                {
                    DataView dvProjectCategory = dtProjectCategory.DefaultView;
                    if (dvProjectCategory != null && dvProjectCategory.Count > 0)
                    {
                        dvProjectCategory.RowFilter = "SELECT = 1";
                        foreach (DataRow dr in dvProjectCategory.ToTable().Rows)
                        {
                            selectedProjectCategoryId += dr[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName] + ",";
                            ProjectCategoryName += dr[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString() + ",";
                        }
                        selectedProjectCategoryId = selectedProjectCategoryId.TrimEnd(',');
                        ProjectCategoryName = ProjectCategoryName.TrimEnd(',');

                        if (ProjectCategoryName.Trim() != string.Empty)
                        {
                            if ((objReportProperty.ReportId == "RPT-051"))
                            {
                                objReportProperty.ProjectTitle = ProjectCategoryName != null ? ProjectCategoryName : string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
            return selectedProjectCategoryId;
        }

        /// <summary>
        /// get bank details id based on the selected bank accuont
        /// </summary>
        /// <returns></returns>
        private string SelectedBankDetails()
        {
            string selectedBankId = string.Empty;

            string selectedBankAccount = string.Empty;
            string UnSelectedAccountId = string.Empty;
            string SelectedLedgerId = string.Empty;
            try
            {

                if (gvBankAccount.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(BankId) && BankSource != null)
                    {
                        ArrayList bankIds = new ArrayList(BankId.Split(new char[] { ',' }));
                        for (int i = 0; i < BankSource.Rows.Count; i++)
                        {
                            BankSource.Rows[i]["SELECT"] = "0";
                        }
                        foreach (string bankid in bankIds)
                        {
                            int i = 0;
                            for (i = 0; i < BankSource.Rows.Count; i++)
                            {
                                if (bankid.Equals(BankSource.Rows[i]["BANK_ACCOUNT_ID"].ToString()))
                                {
                                    BankSource.Rows[i]["SELECT"] = "1";
                                    i = BankSource.Rows.Count;
                                }
                            }
                            BankSource.AcceptChanges();
                        }

                    }
                    gvBankAccount.DataSource = null;
                    gvBankAccount.DataSource = BankSource;
                    gvBankAccount.DataBind();
                }
                DataTable dtBank = gvBankAccount.DataSource as DataTable;
                if (dtBank != null && dtBank.Rows.Count != 0)
                {
                    //var Selected = (from d in dtBank.AsEnumerable()
                    //                where ((d.Field<Int32>(SELECT) == 1))
                    //                select d);
                    //var UnSelectedBank = (from d in dtBank.AsEnumerable()
                    //                      where ((d.Field<Int32>(SELECT) != 1))
                    //                      select d);
                    dtBank.DefaultView.RowFilter = "SELECT = 1";
                    //if (Selected.Count() > 0)
                    if (dtBank.DefaultView.Count > 0)
                    {
                        BankSelected = dtBank.DefaultView.ToTable(); //Selected.CopyToDataTable();
                        if (BankSelected != null && BankSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BankSelected.Rows)
                            {
                                selectedBankAccount += dr[0] + ",";
                                selectedBankId += dr[4] + ",";
                                SelectedLedgerId += dr[5] + ",";

                            }
                            selectedBankAccount = selectedBankAccount.TrimEnd(',');
                            selectedBankId = selectedBankId.TrimEnd(',');
                            SelectedLedgerId = SelectedLedgerId.TrimEnd(',');
                        }
                    }
                    dtBank.DefaultView.RowFilter = "SELECT != 1";
                    //if (UnSelectedBank.Count() > 0)
                    if (dtBank.DefaultView.Count > 0)
                    {
                        DataTable UnSelectedBankAccount = dtBank.DefaultView.ToTable(); //UnSelectedBank.CopyToDataTable();
                        if (UnSelectedBankAccount != null && UnSelectedBankAccount.Rows.Count != 0)
                        {
                            foreach (DataRow dr in UnSelectedBankAccount.Rows)
                            {
                                //  selectedBankAccount += dr[0] + ",";
                                UnSelectedAccountId += dr[4] + ",";
                            }
                            // selectedBankAccount = selectedBankAccount.TrimEnd(',');
                            UnSelectedAccountId = UnSelectedAccountId.TrimEnd(',');

                        }
                    }
                    objReportProperty.UnSelectedBankAccountId = UnSelectedAccountId;
                    objReportProperty.Ledger = SelectedLedgerId;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
            return SelectedLedgerId;
        }

        /// <summary>
        /// Get the selected group details id.
        /// </summary>
        /// <returns></returns>
        private string SelectedLedgerGroupDetails()
        {
            DataTable dtLedgerDetails = null;
            string UnSelectedLedgerId = string.Empty;
            string SelectedLedgerDetailsId = string.Empty;
            try
            {
                if (objReportProperty.ReportId.Equals("RPT-053") || objReportProperty.ReportId.Equals("RPT-054") || objReportProperty.ReportId.Equals("RPT-055"))
                {
                    //if (gvLedger.DataSource == null)
                    //{
                    if (!string.IsNullOrEmpty(LedgerId) && ContributionLedgerSource != null)
                    {
                        ArrayList ledgerIds = new ArrayList(LedgerId.Split(new char[] { ',' }));
                        for (int i = 0; i < ContributionLedgerSource.Rows.Count; i++)
                        {
                            ContributionLedgerSource.Rows[i]["SELECT"] = "0";
                        }
                        foreach (string ledgerid in ledgerIds)
                        {
                            int i = 0;
                            for (i = 0; i < ContributionLedgerSource.Rows.Count; i++)
                            {
                                if (ledgerid.Equals(ContributionLedgerSource.Rows[i]["LEDGER_ID"].ToString()))
                                {
                                    ContributionLedgerSource.Rows[i]["SELECT"] = "1";
                                    i = ContributionLedgerSource.Rows.Count;
                                }
                            }
                            ContributionLedgerSource.AcceptChanges();
                        }
                    }
                    gvLedger.DataSource = null;
                    gvLedger.DataSource = ContributionLedgerSource;
                    // gvLedger.DataBind();
                    //  }

                    DataTable dtGeneralateLedger = gvLedger.DataSource as DataTable;
                    if (dtGeneralateLedger != null && dtGeneralateLedger.Rows.Count != 0)
                    {
                        //var Selected = (from d in dtGeneralateLedger.AsEnumerable()
                        //                where ((d.Field<Int32?>(SELECT) == 1))
                        //                select d);
                        //var UnSelected = (from d in dtGeneralateLedger.AsEnumerable()
                        //                  where ((d.Field<Int32?>(SELECT) != 1))
                        //                  select d);
                        dtGeneralateLedger.DefaultView.RowFilter = "SELECT = 1";
                        //if (Selected.Count() > 0)
                        if (dtGeneralateLedger.DefaultView.Count > 0) ;
                        {
                            dtLedgerDetails = dtGeneralateLedger.DefaultView.ToTable();  //Selected.CopyToDataTable();
                            if (dtLedgerDetails != null && dtLedgerDetails.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dtLedgerDetails.Rows)
                                {
                                    SelectedLedgerDetailsId += dr["LEDGER_ID"] + ",";
                                }
                                SelectedLedgerDetailsId = SelectedLedgerDetailsId.TrimEnd(',');
                            }
                        }
                    }
                }
                else
                {
                    if (gvLedger.DataSource == null)
                    {
                        if (!string.IsNullOrEmpty(LedgerId) && LedgerSource != null)
                        {
                            ArrayList ledgerIds = new ArrayList(LedgerId.Split(new char[] { ',' }));
                            for (int i = 0; i < LedgerSource.Rows.Count; i++)
                            {
                                LedgerSource.Rows[i]["SELECT"] = "0";
                            }
                            foreach (string ledgerid in ledgerIds)
                            {
                                int i = 0;
                                for (i = 0; i < LedgerSource.Rows.Count; i++)
                                {
                                    if (ledgerid.Equals(LedgerSource.Rows[i]["LEDGER_ID"].ToString()))
                                    {
                                        LedgerSource.Rows[i]["SELECT"] = "1";
                                        i = LedgerSource.Rows.Count;
                                    }
                                }
                                LedgerSource.AcceptChanges();
                            }
                        }
                        gvLedger.DataSource = null;
                        gvLedger.DataSource = LedgerSource;
                        gvLedger.DataBind();
                    }

                    DataTable dtLedger = gvLedger.DataSource as DataTable;
                    if (dtLedger != null && dtLedger.Rows.Count != 0)
                    {
                        //var Selected = (from d in dtLedger.AsEnumerable()
                        //                where ((d.Field<Int32?>(SELECT) == 1))
                        //                select d);
                        //var UnSelected = (from d in dtLedger.AsEnumerable()
                        //                  where ((d.Field<Int32?>(SELECT) != 1))
                        //                  select d);
                        dtLedger.DefaultView.RowFilter = "SELECT = 1";
                        //if (Selected.Count() > 0)
                        if (dtLedger.DefaultView.Count > 0)
                        {
                            dtLedgerDetails = dtLedger.DefaultView.ToTable(); //Selected.CopyToDataTable();
                            if (dtLedgerDetails != null && dtLedgerDetails.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dtLedgerDetails.Rows)
                                {
                                    SelectedLedgerDetailsId += dr[0] + ",";
                                    objReportProperty.LedgerName = dr["LEDGER"].ToString();
                                    objReportProperty.LedgerGroupName = dr[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                                }
                                SelectedLedgerDetailsId = SelectedLedgerDetailsId.TrimEnd(',');
                            }
                        }

                        dtLedger.DefaultView.RowFilter = "SELECT != 1";
                        //if (UnSelected.Count() > 0)
                        if (dtLedger.DefaultView.Count > 0)
                        {
                            DataTable dtUnSelectedLedgerId = dtLedger.DefaultView.ToTable(); //UnSelected.CopyToDataTable();
                            if (dtUnSelectedLedgerId != null && dtUnSelectedLedgerId.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dtUnSelectedLedgerId.Rows)
                                {
                                    UnSelectedLedgerId += dr[0] + ",";
                                }
                                UnSelectedLedgerId = UnSelectedLedgerId.TrimEnd(',');
                            }
                        }
                        objReportProperty.UnSelectedLedgerId = UnSelectedLedgerId;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return SelectedLedgerDetailsId;
        }

        /// <summary>
        /// Get the selected ledger group id
        /// </summary>
        /// <returns></returns>
        private string SelectedLedgerGroup()
        {
            string selectedLedgerGroupId = string.Empty;
            string LedgerGroupName = string.Empty;
            try
            {
                if (objReportProperty.ReportId.Equals("RPT-053") || objReportProperty.ReportId.Equals("RPT-054") || objReportProperty.ReportId.Equals("RPT-055"))
                {
                    if (gvLedger.DataSource == null)
                    {
                        if (!string.IsNullOrEmpty(LedgerGroupId) && SubsidyLedgerSource != null)
                        {
                            ArrayList ledgerGroupIds = new ArrayList(LedgerGroupId.Split(new char[] { ',' }));
                            for (int i = 0; i < SubsidyLedgerSource.Rows.Count; i++)
                            {
                                SubsidyLedgerSource.Rows[i]["SELECT"] = "0";
                            }
                            foreach (string ledgerid in ledgerGroupIds)
                            {
                                int i = 0;
                                for (i = 0; i < SubsidyLedgerSource.Rows.Count; i++)
                                {
                                    if (ledgerid.Equals(SubsidyLedgerSource.Rows[i]["GROUP_ID"].ToString()))
                                    {
                                        SubsidyLedgerSource.Rows[i]["SELECT"] = "1";
                                        i = SubsidyLedgerSource.Rows.Count;
                                    }
                                }
                                SubsidyLedgerSource.AcceptChanges();
                            }
                        }
                        gvLedger.DataSource = null;
                        gvLedger.DataSource = SubsidyLedgerSource;
                        // gvLedger.DataBind();
                    }

                    DataTable dtGeneralateLedger = gvLedger.DataSource as DataTable;
                    if (dtGeneralateLedger != null && dtGeneralateLedger.Rows.Count != 0)
                    {
                        //var Selected = (from d in dtGeneralateLedger.AsEnumerable()
                        //                where ((d.Field<Int32?>(SELECT) == 1))
                        //                select d);
                        //var UnSelected = (from d in dtGeneralateLedger.AsEnumerable()
                        //                  where ((d.Field<Int32?>(SELECT) != 1))
                        //                  select d);
                        dtGeneralateLedger.DefaultView.RowFilter = "SELECT = 1";
                        //if (Selected.Count() > 0)
                        if (dtGeneralateLedger.DefaultView.Count > 0)
                        {
                            LedgerGroupSelected = dtGeneralateLedger.DefaultView.ToTable(); //Selected.CopyToDataTable();
                            if (LedgerGroupSelected != null && LedgerGroupSelected.Rows.Count != 0)
                            {
                                foreach (DataRow dr in LedgerGroupSelected.Rows)
                                {
                                    selectedLedgerGroupId += dr["GROUP_ID"] + ",";
                                }
                                selectedLedgerGroupId = selectedLedgerGroupId.TrimEnd(',');
                            }
                        }
                    }
                }
                else
                {
                    if (gvLedgerGroup.DataSource == null)
                    {
                        if (!string.IsNullOrEmpty(LedgerGroupId) && LedgerGroupSource != null)
                        {
                            //on 13/08/2021 To Clear aleady selected items (Temp Purpose ) ----------------------------
                            for (int i = 0; i < LedgerGroupSource.Rows.Count; i++)
                            {
                                LedgerGroupSource.Rows[i]["SELECT"] = "0";
                            }
                            LedgerGroupSource.AcceptChanges();
                            //-----------------------------------------------------------------------------------------------

                            ArrayList ProjectIds = new ArrayList(LedgerGroupId.Split(new char[] { ',' }));
                            foreach (string pid in ProjectIds)
                            {
                                int i = 0;
                                for (i = 0; i < LedgerGroupSource.Rows.Count; i++)
                                {
                                    if (pid.Equals(LedgerGroupSource.Rows[i]["GROUP_ID"].ToString()))
                                    {
                                        LedgerGroupSource.Rows[i]["SELECT"] = "1";
                                        i = LedgerGroupSource.Rows.Count;
                                    }
                                }
                                LedgerGroupSource.AcceptChanges();
                            }

                        }
                        gvLedgerGroup.DataSource = null;
                        gvLedgerGroup.DataSource = LedgerGroupSource;
                        gvLedgerGroup.DataBind();
                    }
                    DataTable dtLedgerGroup = gvLedgerGroup.DataSource as DataTable;
                    if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count != 0 && !string.IsNullOrEmpty(LedgerGroupId))
                    {
                        //var Selected = (from d in dtLedgerGroup.AsEnumerable()
                        //                where ((d.Field<Int32>(SELECT) == 1))
                        //                select d);
                        dtLedgerGroup.DefaultView.RowFilter = "SELECT = 1";
                        //if (Selected.Count() > 0)
                        if (dtLedgerGroup.DefaultView.Count > 0)
                        {
                            LedgerGroupSelected = dtLedgerGroup.DefaultView.ToTable(); //Selected.CopyToDataTable();
                            if (LedgerGroupSelected != null && LedgerGroupSelected.Rows.Count != 0)
                            {
                                foreach (DataRow dr in LedgerGroupSelected.Rows)
                                {
                                    selectedLedgerGroupId += dr[0] + ",";
                                    LedgerGroupName += dr[1].ToString() + ",";
                                }
                                selectedLedgerGroupId = selectedLedgerGroupId.TrimEnd(',');
                                LedgerGroupName = LedgerGroupName.TrimEnd(',');

                                if (LedgerGroupName.Trim() != string.Empty)
                                {
                                    if (LedgerGroupSource.Rows.Count == LedgerGroupSelected.Rows.Count)
                                    {
                                        objReportProperty.ProjectTitle = LedgerGroupSelected.Rows.Count == 1 ? LedgerGroupName : SCONSTATEMENT;
                                    }
                                    else
                                    {
                                        objReportProperty.ProjectTitle = LedgerGroupName;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
            return selectedLedgerGroupId;

        }


        /// <summary>
        /// Get the selected cost centre id.
        /// </summary>
        /// <returns></returns>
        private string SelectedCostCentre()
        {
            string SelectedCostCentre = string.Empty;
            string CostCentreName = string.Empty;
            try
            {
                if (gvCostCentre.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(CostCentreId) && CostCentreSource != null)
                    {
                        ArrayList costcentreids = new ArrayList(CostCentreId.Split(new char[] { ',' }));

                        for (int Val = 0; Val < CostCentreSource.Rows.Count; Val++)
                        {
                            CostCentreSource.Rows[Val][SELECT] = "0";
                        }
                        foreach (string costcentreid in costcentreids)
                        {
                            for (int i = 0; i < CostCentreSource.Rows.Count; i++)
                            {
                                if (costcentreid.Equals(CostCentreSource.Rows[i]["COST_CENTRE_ID"].ToString()))
                                {
                                    CostCentreSource.Rows[i]["SELECT"] = "1";
                                    //  i = CostCentreSource.Rows.Count;
                                }
                            }
                            CostCentreSource.AcceptChanges();
                        }
                    }
                    gvCostCentre.DataSource = null;
                    gvCostCentre.DataSource = CostCentreSource;
                    gvCostCentre.DataBind();
                }
                DataTable dtCostCentre = gvCostCentre.DataSource as DataTable;
                if (dtCostCentre != null && dtCostCentre.Rows.Count != 0)
                {

                    DataView dvCostcentre = dtCostCentre.DefaultView;
                    if (dvCostcentre != null && dvCostcentre.Count > 0)
                    {
                        dvCostcentre.RowFilter = "SELECT = 1";
                        foreach (DataRow dr in dvCostcentre.ToTable().Rows)
                        {
                            SelectedCostCentre += dr[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName] + ",";
                            CostCentreName += dr[this.AppSchema.CostCentre.COST_CENTREColumn.ColumnName].ToString() + ",";
                        }
                        SelectedCostCentre = SelectedCostCentre.TrimEnd(',');
                        CostCentreName = CostCentreName.TrimEnd(',');

                        if (CostCentreName.Trim() != string.Empty)
                        {
                            objReportProperty.CostCentreName = CostCentreName.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return SelectedCostCentre;
        }

        /// <summary>
        /// Get the selected Branch code
        /// </summary>
        /// <returns></returns>
        private string SelectedBranchCode()
        {
            string BranchName = string.Empty;
            string SelectedBranchId = string.Empty;
            string SelectedBranchCode = string.Empty;

            try
            {
                if (gvBranch.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(BranchCode) && BranchSource != null)
                    {
                        ArrayList branchcodes = new ArrayList(BranchCode.Split(new char[] { ',' }));
                        for (int Val = 0; Val < BranchSource.Rows.Count; Val++)
                        {
                            BranchSource.Rows[Val][SELECT] = "0";
                        }
                        foreach (string branchcode in branchcodes)
                        {
                            for (int i = 0; i < BranchSource.Rows.Count; i++)
                            {
                                if (branchcode.Equals(BranchSource.Rows[i]["BRANCH_OFFICE_ID"].ToString()))
                                {
                                    BranchSource.Rows[i]["SELECT"] = "1";
                                    i = BranchSource.Rows.Count;
                                }
                            }
                            BranchSource.AcceptChanges();
                        }
                    }
                    //        objReportProperty.BranchOfficeName = BranchName;

                    gvBranch.DataSource = null;
                    gvBranch.DataSource = BranchSource;
                    gvBranch.DataBind();
                }
                DataTable dtBranch = gvBranch.DataSource as DataTable;
                if (dtBranch != null && dtBranch.Rows.Count != 0)
                {
                    //On 29/09/2020, Removed Linq----------------------------------------------
                    /*var Selected = (from d in dtBranch.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);*/
                    //-------------------------------------------------------------------------

                    dtBranch.DefaultView.RowFilter = "SELECT = 1 ";
                    //if (Selected.Count() > 0)
                    if (dtBranch.DefaultView.Count > 0)
                    {
                        //BranchSelected = Selected.CopyToDataTable();
                        BranchSelected = dtBranch.DefaultView.ToTable();
                        if (BranchSelected != null && BranchSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BranchSelected.Rows)
                            {
                                SelectedBranchId += dr[0] + ",";
                                BranchName += dr[this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString() + ", ";
                                SelectedBranchCode += "'" + dr[this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString() + "',";
                            }
                            SelectedBranchId = SelectedBranchId.TrimEnd(',');
                            BranchName = BranchName.TrimEnd(' ');
                            SelectedBranchCode = SelectedBranchCode.TrimEnd(',');
                            if (BranchName.Trim() != string.Empty)
                            {
                                objReportProperty.BranchOfficeName = BranchName.TrimEnd(',');
                                objReportProperty.BranchOfficeCode = SelectedBranchCode;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return SelectedBranchId;
        }

        private string SelectedSocietyCode()
        {
            string SelectedSociety = string.Empty;
            string SelectedSocietyName = string.Empty;
            try
            {
                if (gvSociety.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(SocietyCode) && SocietySource != null)
                    {
                        ArrayList societyCode = new ArrayList(SocietyCode.Split(new char[] { ',' }));
                        for (int Val = 0; Val < SocietySource.Rows.Count; Val++)
                        {
                            SocietySource.Rows[Val]["SELECT"] = "0";
                        }
                        foreach (string scode in societyCode)
                        {
                            for (int i = 0; i < SocietySource.Rows.Count; i++)
                            {
                                if (scode.Equals(SocietySource.Rows[i]["CUSTOMERID"].ToString()))
                                {
                                    SocietySource.Rows[i]["SELECT"] = "1";
                                    i = SocietySource.Rows.Count;
                                }
                            }
                            SocietySource.AcceptChanges();
                        }
                    }
                    gvSociety.DataSource = null;
                    gvSociety.DataSource = SocietySource;
                    gvSociety.DataBind();
                }
                DataTable dtsociety = gvSociety.DataSource as DataTable;
                if (dtsociety != null && dtsociety.Rows.Count != 0)
                {
                    //On 29/09/2020, Removed Linq----------------------------------------------------
                    /*var Selected = (from d in dtsociety.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    objReportProperty.SelectedSocietyCount = Selected.Count();*/
                    dtsociety.DefaultView.RowFilter = "SELECT = 1";
                    objReportProperty.SelectedSocietyCount = dtsociety.DefaultView.Count;
                    //-------------------------------------------------------------------------------

                    //if (Selected.Count() > 0)
                    if (dtsociety.DefaultView.Count > 0)
                    {
                        //SocietySelected = Selected.CopyToDataTable();
                        SocietySelected = dtsociety.DefaultView.ToTable();
                        if (SocietySelected != null && SocietySelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in SocietySelected.Rows)
                            {
                                SelectedSociety += dr[0] + ",";
                                SelectedSocietyName += dr[1].ToString().Trim() + ",";
                            }
                            SelectedSociety = SelectedSociety.TrimEnd(',');
                            SelectedSocietyName = SelectedSocietyName.TrimEnd(',');
                        }
                    }
                }
                objReportProperty.SocietyName = SelectedSocietyName.TrimEnd(',');

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return SelectedSociety;
        }

        /// <summary>
        /// get bank details id based on the selected bank accuont
        /// </summary>
        /// <returns></returns>
        private string SelectedBudgetDetails()
        {
            string SelectedBudget = string.Empty;
            string SelectedBudgetName = string.Empty;
            try
            {
                if (gvBudget.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(BudgetId) && BudgetSource != null)
                    {
                        ArrayList budgetIds = new ArrayList(BudgetId.Split(new char[] { ',' }));
                        foreach (string budgetid in budgetIds)
                        {
                            int i = 0;
                            for (i = 0; i < BudgetSource.Rows.Count; i++)
                            {
                                if (budgetid.Equals(BudgetSource.Rows[i]["BUDGET_ID"].ToString()))
                                {
                                    BudgetSource.Rows[i]["SELECT"] = "1";
                                    i = BudgetSource.Rows.Count;
                                }
                            }
                            BudgetSource.AcceptChanges();
                        }

                    }
                    gvBudget.DataSource = null;
                    gvBudget.DataSource = BudgetSource;
                    gvBudget.DataBind();
                }

                DataTable dtBudgetDetails = gvBudget.DataSource as DataTable;
                if (dtBudgetDetails != null && dtBudgetDetails.Rows.Count != 0)
                {
                    DataView dvBudget = dtBudgetDetails.DefaultView;
                    if (dvBudget != null && dvBudget.Count > 0)
                    {
                        dvBudget.RowFilter = "SELECT = 1";
                        foreach (DataRow dr in dvBudget.ToTable().Rows)
                        {
                            SelectedBudget += dr[this.AppSchema.Budget.BUDGET_IDColumn.ColumnName] + ",";
                            SelectedBudgetName += dr[this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString() + ",";
                        }
                        SelectedBudget = SelectedBudget.TrimEnd(',');

                        SelectedBudgetName = SelectedBudgetName.TrimEnd(',');
                        if (SelectedBudgetName.Trim() != string.Empty)
                        {
                            objReportProperty.BudgetName = SelectedBudgetName.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return SelectedBudget;
        }

        /// <summary>
        /// On 14/08/2021, To get Congregation LedgerId based on selecetd LEdger Groups (LG)
        /// </summary>
        /// <returns></returns>
        private string GetSelectedConLedgerIdByLG()
        {
            string Rtn = "";
            try
            {
                if (!string.IsNullOrEmpty(LedgerGroupId) && !LedgerGroupId.Equals("0") && CongregationLedgeWithLedgerGroup != null)
                {
                    CongregationLedgeWithLedgerGroup.DefaultView.RowFilter = string.Empty;
                    CongregationLedgeWithLedgerGroup.DefaultView.RowFilter = "(PARENT_GROUP_ID IN (" + LedgerGroupId + ") OR GROUP_ID IN (" + LedgerGroupId + ") )";

                    DataTable dtConLedgerIds = CongregationLedgeWithLedgerGroup.DefaultView.ToTable(true, new string[] { "CON_LEDGER_ID" });
                    foreach (DataRow dr in dtConLedgerIds.Rows)
                    {
                        Rtn += dr["CON_LEDGER_ID"].ToString() + ",";
                    }
                    Rtn = Rtn.TrimEnd(',');
                    CongregationLedgeWithLedgerGroup.DefaultView.RowFilter = string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally
            {
                if (CongregationLedgeWithLedgerGroup != null)
                {
                    CongregationLedgeWithLedgerGroup.DefaultView.RowFilter = string.Empty;
                }

                if (string.IsNullOrEmpty(Rtn))
                {
                    Rtn = "0";
                }
            }

            return Rtn;
        }

        /// <summary>
        /// On 09/08/2021, to get selected congregation ledger details
        /// </summary>
        /// <returns></returns>
        private string SelectedCongregationDetails()
        {
            string SelectedConLedgers = string.Empty;
            string SelectedConLedgerName = string.Empty;
            try
            {
                if (gvCongregationLedger.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(ConLedgerId) && ConLedgerSource != null)
                    {
                        //on 13/08/2021 To Clear aleady selected items (Temp Purpose ) ----------------------------
                        for (int i = 0; i < ConLedgerSource.Rows.Count; i++)
                        {
                            ConLedgerSource.Rows[i]["SELECT"] = "0";
                        }
                        ConLedgerSource.AcceptChanges();
                        //-----------------------------------------------------------------------------------------------

                        ArrayList conledgerids = new ArrayList(ConLedgerId.Split(new char[] { ',' }));
                        foreach (string conledgerid in conledgerids)
                        {
                            int i = 0;
                            for (i = 0; i < ConLedgerSource.Rows.Count; i++)
                            {
                                if (conledgerid.Equals(ConLedgerSource.Rows[i]["CON_LEDGER_ID"].ToString()))
                                {
                                    ConLedgerSource.Rows[i]["SELECT"] = "1";
                                    i = ConLedgerSource.Rows.Count;
                                }
                            }
                            ConLedgerSource.AcceptChanges();
                        }
                    }
                    gvCongregationLedger.DataSource = null;
                    gvCongregationLedger.DataSource = ConLedgerSource;
                    gvCongregationLedger.DataBind();
                }

                DataTable dtCongregationLedger = gvCongregationLedger.DataSource as DataTable;
                if (dtCongregationLedger != null && dtCongregationLedger.Rows.Count != 0)
                {
                    DataView dvConLedger = dtCongregationLedger.DefaultView;
                    if (dvConLedger != null && dvConLedger.Count > 0 && !string.IsNullOrEmpty(ConLedgerId))
                    {
                        dvConLedger.RowFilter = "SELECT = 1";
                        foreach (DataRow dr in dvConLedger.ToTable().Rows)
                        {
                            SelectedConLedgers += dr[this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn.ColumnName] + ",";
                            SelectedConLedgerName += dr[this.AppSchema.CongregationLedger.CON_LEDGER_NAMEColumn.ColumnName].ToString() + ",";
                        }
                        SelectedConLedgers = SelectedConLedgers.TrimEnd(',');

                        SelectedConLedgerName = SelectedConLedgerName.TrimEnd(',');
                        if (SelectedConLedgerName.Trim() != string.Empty)
                        {
                            objReportProperty.CongregationLedger = SelectedConLedgers.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return SelectedConLedgers;
        }
        /// <summary>
        /// Clears the Filter Textboxes
        /// </summary>
        private void ClearFilterTextBoxes()
        {
            txtSearch.Text = txtSocietySearch.Text = txtProjectSearch.Text = txtProjectCategorySearch.Text = txtBudgetSearch.Text =
                txtBankAccountFilter.Text = txtLedgerGroupFilter.Text = txtLedgerSearch.Text = txtCostCentreFilter.Text = string.Empty;
        }

        #endregion

        #region SetDate

        /// <summary>
        /// Sets the Date fro the Date Tab
        /// </summary>
        protected void TabDateProperties()
        {
            dteFrom.Date = this.Member.DateSet.ToDate(objSettingProperty.YearFrom, false);
            dteTo.Date = dteFrom.Date.AddMonths(1).AddDays(-1);
            dtreportdate.Date = this.Member.DateSet.ToDate(DateTime.Now.Date.ToString(), true);
        }

        #endregion

        #region Callback
        /// <summary>
        /// Fired when callback is called
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void ReportCriteriaCallback_Callback(object source, CallbackEventArgs e)
        {
            AssignValues();
        }
        /// <summary>
        /// Gets the callback result
        /// </summary>
        /// <returns></returns>
        public string GetCallbackResult()
        {
            return result;
        }

        protected void ddlFD_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cmbedit = (DropDownList)sender;
            //this.objReportProperty.FDRegisterStatus = Convert.ToInt32(cmbedit.SelectedValue);
            FDRegisterSelectedStatus = Convert.ToInt32(cmbedit.SelectedValue);
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlFdRegister = (e.Row.FindControl("ddlFD") as DropDownList);
                CheckBox chkSelect = (e.Row.FindControl("chkDateLedger") as CheckBox);

                string criteria = objReportProperty.ReportCriteria;
                string[] test = criteria.Split('ÿ');
                foreach (string s in test)
                {
                    if (s.Equals("FD"))
                    {
                        ddlFdRegister.Visible = true;
                        chkSelect.Visible = false;
                    }
                }
                BindFDStatus(ddlFdRegister);
            }
        }

        /// <summary>
        /// this raised when checkbox is changed from all the tabs of report criteria as call back event  to filter the next tab value
        /// Based on the Branch selection, Society or Legal Entity  and Projects are filtered and shown in the criteria  tabs.
        /// </summary>
        /// <param name="eventArgument"></param>
        public void RaiseCallbackEvent(string eventArgument)
        {
            ArrayList criteria = new ArrayList(eventArgument.Split(new char[] { '@' }));
            eventArgument = criteria[0].ToString();
            string ids = criteria[1].ToString().TrimEnd(',');
            if (eventArgument == "BR")
            {
                BranchCode = ids;
                if (gvSociety.Visible)
                {
                    result = SetSocietyCallback();
                    result += "@" + SetProjectCallback();
                }
                else if (gvBudget.Visible)
                {
                    result = SetBudgetCallback();
                }
                else
                {
                    result = "";
                }

            }
            else if (eventArgument == "SY")//SY-Society
            {
                SocietyCode = ids;
                result = SetProjectCallback();
            }
            else if (eventArgument == "PJ")//PJ-Project
            {
                ProjectId = ids;
                if (gvBankAccount.Visible)
                {
                    result = SetBankCallback();
                }
                else if (gvBudget.Visible)
                {
                    result = SetBudgetCallback();
                }
                else
                {
                    result = "";
                }
            }
            //else if (eventArgument == "BU")
            //{
            //    BudgetId = ids;
            //    if (gvProject.Visible)
            //    {
            //        result = SetBudgetCallback();
            //    }
            //    else
            //    {
            //        result = "";
            //    }
            //}
            else if (eventArgument == "BK")//BK-Bank Account
            {
                BankId = ids;
                result = "";
            }
            else if (eventArgument == "LGG")//LGG-Ledger Group
            {
                LedgerGroupId = ids;
                result = "";
                if (gvLedger.Visible)
                {
                    result = SetLedgerCallback();
                }
                else if (gvCongregationLedger.Visible)
                {
                    result = SetConLedgerCallback();
                }
            }
            else if (eventArgument == "LG")//LG -Ledger Id
            {
                LedgerId = ids;
                result = "";
            }
            else if (eventArgument == "CC")//CC-Cost center
            {
                CostCentreId = ids;
                result = "";
            }
            else if (eventArgument == "PC")//PC-Project Category
            {
                ProjectCategoryId = ids;
                result = SetProjectCallback();
            }
            else if (eventArgument == "BRF" || eventArgument == "BRFR")//FR-refers to Filter -BRF-Branch Fitler
            {
                result = SetBranchFilterCallback(ids);
            }
            else if (eventArgument == "SYF" || eventArgument == "SYFR")//SYFR-Society Filter
            {
                result = SetSocietyFilterCallback(ids);
            }
            else if (eventArgument == "PJF" || eventArgument == "PJFR")//PJFR- Project Category Filter
            {
                result = SetProjectFilterCallback(ids);
            }
            else if (eventArgument == "BU")
            {
                BudgetId = ids;
                result = "";
                //result = SetBudgetFilterCallback(ids);
            }
            else if (eventArgument == "CL")
            {
                ConLedgerId = ids;
                result = "";
            }
            else if (eventArgument == "CLF" || eventArgument == "CLFR")
            {
                SetFilterCallback(ids);
            }
            else if (eventArgument == "LGGF" || eventArgument == "LGGFR")//LGGFR- Ledger Group Filter
            {
                result = SetLedgerGroupFilterCallback(ids);
            }
            else if (eventArgument == "LGF" || eventArgument == "LGFR")//LGFR- Ledger Filter
            {
                result = SetLedgerFilterCallback(ids);
            }
            else if (eventArgument == "CCF" || eventArgument == "CCFR")//CCFR- Cost Centre Filter
            {
                result = SetCostCentreFilterCallback(ids);
            }
            else if (eventArgument == "BKF" || eventArgument == "BKFR")// BKFR- Bank Account Filter
            {
                result = SetBankAccountFilterCallback(ids);
            }
            else if (eventArgument == "PCF" || eventArgument == "PCRF")// PCRF- Project Category Filter
            {
                result = SetProjectCategoryFilterCallback(ids);
            }
            else
            {
                result = "Call Back Error";
            }
        }
        #endregion
    }

}