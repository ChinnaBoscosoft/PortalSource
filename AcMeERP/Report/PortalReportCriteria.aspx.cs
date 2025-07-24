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
    public partial class PortalReportCriteria : Base.UIBase, ICallbackEventHandler
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
        public EventDrillDownArgs ReportDrillDown = null;
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
        public DataTable LedgerSelected { get; set; }
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

            if (!IsPostBack)
            {
                GlobalSetting objGlobal = new GlobalSetting();
                objGlobal.ApplySetting();
                TabDateProperties();
                objReportProperty.ReportId = Request.QueryString["rid"];
                ReportsCriteria = null;
                ProjectId = objReportProperty.Project;
                LedgerGroupId = objReportProperty.LedgerGroup;
                LedgerId = objReportProperty.Ledger;
                BranchCode = objReportProperty.BranchOffice;
                SocietyCode = objReportProperty.Society;
                CostCentreId = objReportProperty.CostCentre;
                if (Session["DillDownLinks"] == null)
                {
                    Session["DillDownLinks"] = new Stack<EventDrillDownArgs>();
                }
                //Showpopup if hdva contains value true
                if (Request.QueryString["hdva"] == null)
                {
                    InitiateReportCriteria();
                    InitialDrillDownProperties(objReportProperty.ReportId);
                    //Clear Previous Report Cache value before loading New Report
                    Page.Session["RPTCACHE"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Report Criteria", "javascript:ShowDisplayPopUp()", true);
                }
                else if (Request.QueryString.AllKeys.Contains("DrillBack"))
                {
                    DrillDownTarget(GetRecentDrillDown());
                }
                else
                {
                    LoadReport(objReportProperty.ReportId);
                }

                this.ShowLoadWaitPopUp(btnOk);
            }
            //Disable Tabs
            if (!string.IsNullOrEmpty(BranchCode) && BranchCode != "0")
            {
                TabSociety.Enabled = false;
            }
            else if (!string.IsNullOrEmpty(SocietyCode) && SocietyCode != "0")
            {
                TabBranch.Enabled = false;
            }
            if (string.IsNullOrEmpty(BranchCode) && BranchCode == "0")
            {
                TabSociety.Enabled = true;
            }
            if (string.IsNullOrEmpty(SocietyCode) && SocietyCode == "0")
            {
                TabBranch.Enabled = true;
            }
            //set ActiveTab always Branch
            TabReportCriteria.ActiveTabIndex = TabBranch.TabIndex;

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
            }

            
        }

        private void InitiateReportCriteria()
        {
            EnableTabs();
            EnableReportSetupProperties();
            SetTabIndex();
            AssignValues();
        }

        private void ShowDrillDownInToolbar()
        {
            ReportToolbarButton drillButton = new ReportToolbarButton();
            drillButton.Name = "DrillDown";
            drillButton.ImageUrl = "~/App_Themes/MainTheme/images/Decrease.png";
            drillButton.ToolTip = "Drill-Down/Drill-Back";
            dvReportViewer.ToolbarItems.Insert(0, drillButton);
        }

        private void ShowCriteriaCustomiseInToolbar()
        {
            ReportToolbarButton rtbCustomise = new ReportToolbarButton();
            rtbCustomise.Name = "Customization";
            rtbCustomise.ImageUrl = "~/App_Themes/MainTheme/images/Report.png";
            rtbCustomise.ToolTip = "Customise";
            dvReportViewer.ToolbarItems.Insert(0, rtbCustomise);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            lock (objLoc)
            {
                if (IsValidCriteria())
                {
                    ClearFilterTextBoxes();
                    dvReportViewer.ToolbarItems.RemoveAt(1);
                    LoadReport(objReportProperty.ReportId);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Criteria Popup", "javascript:ShowDisplayPopUp()", true);
                }
            }
        }

        // Assign values to contorls from session
        private void AssignValues()
        {
            if (!string.IsNullOrEmpty(objReportProperty.DateFrom))
            {
                dteFrom.Date = this.Member.DateSet.ToDate(objReportProperty.DateFrom, true);
                if (objReportProperty.ReportId == "RPT-004" || objReportProperty.ReportId == "RPT-005" || objReportProperty.ReportId == "RPT-006")
                {
                    dteTo.Date = dteFrom.Date.AddMonths(12).AddDays(-1);
                }
                else
                {
                    dteTo.Date = this.Member.DateSet.ToDate(objReportProperty.DateTo, true);
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
                    if (dtProject.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            if (dr["PROJECT_ID"].ToString() == ProId[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                ProjectId = objReportProperty.Project;
                EnumerableRowCollection<DataRow> query =
               from project in dtProject.AsEnumerable()
               orderby project.Field<Int32>("SELECT") descending
               select project;

                DataView ProjectView = query.AsDataView();
                gvProject.DataSource = ProjectView.ToTable();
                gvProject.DataBind();
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

                EnumerableRowCollection<DataRow> query =
              from society in dtSociety.AsEnumerable()
              orderby society.Field<Int32>("SELECT") descending
              select society;

                DataView SocietyView = query.AsDataView();
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

                EnumerableRowCollection<DataRow> query =
              from bank in dtBank.AsEnumerable()
              orderby bank.Field<Int32>("SELECT") descending
              select bank;

                DataView BankView = query.AsDataView();
                gvBankAccount.DataSource = BankView.ToTable();
                gvBankAccount.DataBind();
            }

        }

        private void AssignLedgerGroup()
        {
            DataTable dtLedgerGroup = null;
            if (!string.IsNullOrEmpty(objReportProperty.LedgerGroup))
            {
                dtLedgerGroup = gvLedgerGroup.DataSource != null ? (DataTable)gvLedgerGroup.DataSource : LedgerGroupSource;
                string[] LedgerGroupIds = objReportProperty.LedgerGroup.Split(',');
                for (int i = 0; i < LedgerGroupIds.Length; i++)
                {
                    if (dtLedgerGroup.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtLedgerGroup.Rows)
                        {
                            if (dr["GROUP_ID"].ToString() == LedgerGroupIds[i].ToString())
                                dr["SELECT"] = "1";
                        }
                    }
                }
                LedgerGroupId = objReportProperty.LedgerGroup;
                EnumerableRowCollection<DataRow> query =
              from ledgergp in dtLedgerGroup.AsEnumerable()
              orderby ledgergp.Field<Int32>("SELECT") descending
              select ledgergp;

                DataView LedgerGroupView = query.AsDataView();
                gvLedgerGroup.DataSource = LedgerGroupView.ToTable();
                gvLedgerGroup.DataBind();
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
                EnumerableRowCollection<DataRow> query =
             from ledger in dtLedger.AsEnumerable()
             orderby ledger.Field<Int32>("SELECT") descending
             select ledger;

                DataView LedgerView = query.AsDataView();
                gvLedger.DataSource = LedgerView.ToTable();
                gvLedger.DataBind();

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
                EnumerableRowCollection<DataRow> query =
            from costcenter in dtCostCentre.AsEnumerable()
            orderby costcenter.Field<Int32>("SELECT") descending
            select costcenter;

                DataView CostCenterView = query.AsDataView();
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
                EnumerableRowCollection<DataRow> query =
            from branchoffice in dtBranchOffice.AsEnumerable()
            orderby branchoffice.Field<Int32>("SELECT") descending
            select branchoffice;

                DataView BranchOfficeView = query.AsDataView();
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
                                break;
                            }

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
                for (int i = 0; i < aCriteria.Length; i++)
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
                                dvBranchFilter.RowFilter = "BRANCH_OFFICE_NAME LIKE '*" + SearchText + "*'";
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
                                dvSocietyFilter.RowFilter = "SOCIETY_FILTER LIKE '*" + SearchText + "*'";
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
                                dvProjectFilter.RowFilter = "PROJECT_NAME LIKE '*" + SearchText + "*'";
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

        private string SetLedgerFilterCallback(string SearchText)
        {
            string[] aCriteria = objReportProperty.ReportCriteria.Split('ÿ');

            try
            {
                DataView dvLedgerFilter = LedgerSource.DefaultView;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    dvLedgerFilter.RowFilter = "LEDGER LIKE '*" + SearchText + "*'";
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
                                dvLedgerGroupFilter.RowFilter = "GROUP LIKE '*" + SearchText + "*'";
                            }
                            gvLedgerGroup.DataSource = null;
                            gvLedgerGroup.DataSource = dvLedgerGroupFilter.ToTable();
                            gvLedgerGroup.DataBind();
                            dvLedgerGroupFilter.RowFilter = string.Empty;

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
                    dvCostCentreFilter.RowFilter = "COST_CENTRE_NAME LIKE '*" + SearchText + "*'";
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
                    dvBankAccountFilter.RowFilter = "BANK LIKE '*" + SearchText + "*'";
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
                    dvProjectCategoryFilter.RowFilter = "BANK LIKE '*" + SearchText + "*'";
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
                for (int i = 0; i < aCriteria.Length; i++)
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

        #endregion

        #region Methods
        private void LoadReport(string reportId)
        {
            if (!string.IsNullOrEmpty(reportId))
            {
                if (Request.QueryString["DrillDownType"] != null)
                {
                    dvReportViewer.ToolbarItems.RemoveAt(0);
                    ShowDrillDownInToolbar();
                    AttachDrillDownProperties();
                }
                objReportProperty.ReportId = reportId;
                AssignReportValues();
                string reportAssemblyType = objReportProperty.ReportAssembly;
                //new ErrorLog().WriteError("ReportId::" + objReportProperty.ReportId.ToString() + "Assembly Type::" + reportAssemblyType.ToString());
                Page.Session["RPTCACHE"] = null;
                ReportBase report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                dvReportViewer.Report = report;
                report.ShowReport();
                upReport.Update();
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
            if (TabLedger.Visible)
            {
                if (string.IsNullOrEmpty(LedgerId) || LedgerId.Equals("0"))
                {
                    TabReportCriteria.ActiveTabIndex = TabLedger.TabIndex;
                    isValid = false;
                    objReportProperty.Ledger = LedgerId;
                    SetLedgerDetailSource();
                    SetLedgerSource();
                    this.Message = MessageCatalog.ReportMessage.REPORT_LEDGER_EMPTY;

                }

            }
            if (tabDate.Visible)
            {
                if (dteFrom.Value == null || dteTo.Value == null)
                {
                    TabDateProperties();
                    TabReportCriteria.ActiveTabIndex = tabDate.TabIndex;
                }
            }
            if (TabBranch.Visible)
            {
                if (string.IsNullOrEmpty(BranchCode) || BranchCode.Equals("0"))
                {
                    if (objReportProperty.ReportId.Equals("RPT-050") || objReportProperty.ReportId.Equals("RPT-051") || objReportProperty.ReportId.Equals("RPT-185"))
                    {
                        isValid = false;
                        objReportProperty.BranchOffice = BranchCode;
                        SetBranchSource();
                        TabReportCriteria.ActiveTabIndex = TabBranch.TabIndex;
                        this.Message = MessageCatalog.ReportMessage.BRANCH_ID_EMPTY;
                    }
                }
            }
            if (TabProject.Visible)
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
                    this.Message = MessageCatalog.ReportMessage.REPORT_PROJECT_EMPTY;
                }
            }
            return isValid;
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
                                    string BranchId = string.Empty;
                                    if (base.LoginUser.IsBranchOfficeUser)
                                    {
                                        BranchId = CurrentLoginUserBranchCode;//Branch Id
                                    }
                                    else
                                    {
                                        BranchId = SelectedBranchCode();
                                    }
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
                                case "BUCC":
                                    {
                                        objReportProperty.BreakUpByCostCentre = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
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
                //      objReportProperty.ShowLedgerCode = chkShowLedgerCode.Checked ? 1 : 0;
                //      objReportProperty.ShowGroupCode = chkShowGroupCode.Checked ? 1 : 0;
                //      objReportProperty.SortByLedger = ddlSortByLedger.SelectedIndex;
                //      objReportProperty.SortByGroup = ddlSortByGroup.SelectedIndex;
                objReportProperty.ShowHorizontalLine = chkHorizontalLine.Checked ? 1 : 0;
                objReportProperty.ShowVerticalLine = chkVerticalLine.Checked ? 1 : 0;
                objReportProperty.ShowTitles = chkShowTitle.Checked ? 1 : 0;
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
        private string InsertComma(ArrayList list)
        {
            string Constructedstring = string.Empty;
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Constructedstring = Constructedstring + "," + list[i];
                }
                Constructedstring = Constructedstring.Remove(0, 1);

            }
            return Constructedstring;
        }

        private void EnableTabs()
        {
            if (objReportProperty.ReportId != null)
            {
                AssignReportCriteria();//Load the current date for Date From and Date To Control.
                //ddlSortByLedger.SelectedIndex = 0;
                //ddlSortByGroup.SelectedIndex = 0;
                dtCriteria = ConstructTable();
                TabSociety.Visible = false;
                TabBranch.Visible = false;
                lblReport.Text = objReportProperty.ReportTitle;
                string criteria = objReportProperty.ReportCriteria;

                string[] test = criteria.Split('ÿ');
                foreach (string s in test)
                {
                    switch (s)
                    {

                        case "DF": //Date From 
                            {
                                dteFrom.Visible = true;
                                break;
                            }
                        case "DT": //Date To
                            {
                                if (objReportProperty.ReportId == "RPT-004" || objReportProperty.ReportId == "RPT-005" || objReportProperty.ReportId == "RPT-006" || objReportProperty.ReportId == "RPT-035" || objReportProperty.ReportId == "RPT-036" || objReportProperty.ReportId == "RPT-037" || objReportProperty.ReportId == "RPT-051")
                                {
                                    dteTo.MaxDate = dteFrom.Date.AddMonths(12).AddDays(-1);
                                    dteTo.Date = dteFrom.Date.AddMonths(12).AddDays(-1);
                                }
                                dteTo.Visible = true;
                                break;
                            }
                        case "DA": //Date As On 
                            {
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
                                    dtCriteria.Rows.Add(1, "MT", "Show Month Total");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "MT", "Show Month Total");
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
                        case "BU": // Get Budget Details.
                            {
                                divProject.Style["width"] = "50%";
                                divBank.Style["width"] = "48%";
                                gvBankAccount.Visible = true;
                                gvBankAccount.Columns[1].HeaderText = "Budget";
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

                }
                else if (!String.IsNullOrEmpty(objReportProperty.DateAsOn))
                {
                    dteFrom.Date = objReportProperty.DateSet.ToDate(objReportProperty.DateAsOn, false);
                }
                //else
                //{
                //    dteFrom.Date = objReportProperty.DateSet.ToDate(settingProperty.BookBeginFrom, false);
                //}
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
            finally { AssignCostCentre(); }
        }

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
                                    if (TabLedger.Visible)
                                    {
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
            }

            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { AssignBranchOffice(); }
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
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        SocietySelected = resultArgs.DataSource.Table;
                        SocietySource = resultArgs.DataSource.Table; ;//set Society in ViewState
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
            finally { AssignSociety(); }
        }

        private void SetBudgetSource()
        {
            try
            {
                if (string.IsNullOrEmpty(objReportProperty.Project) || objReportProperty.Project == "0")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetNames, DataBaseType.HeadOffice))
                    {
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                BudgetSelected = resultArgs.DataSource.Table;
                                BankSource = resultArgs.DataSource.Table;
                                objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                gvBankAccount.DataSource = null;
                                gvBankAccount.DataSource = resultArgs.DataSource.Table;
                                gvBankAccount.DataBind();
                            }
                        }
                    }
                }
                else
                {
                    DataTable dtBudgetDetails = null;
                    using (BudgetSystem budgetSystem = new BudgetSystem())
                    {
                        dtBudgetDetails = budgetSystem.FetchBudgetsByProjects(objReportProperty.Project);
                    }
                    gvBankAccount.DataSource = dtBudgetDetails;
                    BudgetSelected = dtBudgetDetails;
                    BankSource = dtBudgetDetails;

                }

                if (!string.IsNullOrEmpty(objReportProperty.Budget) && objReportProperty.Budget != "0")
                {
                    DataTable dtBank = (DataTable)gvBankAccount.DataSource;
                    string[] BudgetId = objReportProperty.Budget.ToString().Split(',');
                    foreach (DataRow dr in dtBank.Rows)
                    {
                        for (int i = 0; i < BudgetId.Length; i++)
                        {
                            if (dr["BANK_ID"].ToString() == BudgetId[i])
                                dr["SELECT"] = 1;
                        }
                    }
                    dtBank.DefaultView.Sort = "SELECT DESC";
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
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { AssignLedger(); }
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
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { AssignLedgerGroup(); }
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
            finally { AssignBank(); }
        }

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
                        resultArgs = !string.IsNullOrEmpty(SocietyCode) && SocietyCode != "0" ? projectSystem.FetchProjetBySociety(SocietyCode, CurrentLoginUserBranchCode,ProjectCategoryId) : projectSystem.FetchAllProjects(CurrentLoginUserBranchCode);
                    }
                    if (base.LoginUser.IsHeadOfficeUser)
                    {
                        resultArgs = !string.IsNullOrEmpty(SocietyCode) && SocietyCode != "0" ? projectSystem.FetchProjetBySociety(SocietyCode, BranchCode, ProjectCategoryId) : projectSystem.FetchAllProjects(BranchCode);
                    }
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        if (objReportProperty.ReportId == "RPT-043")
                        {
                            DataTable dtForeignInfo = FetchForeignProjects(resultArgs.DataSource.Table);
                            ProjectSelected = dtForeignInfo;
                            ProjectSource = dtForeignInfo;
                            objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gvProject.DataSource = null;
                            gvProject.DataSource = dtForeignInfo;
                            gvProject.DataBind();
                        }
                        else
                        {
                            ProjectSelected = resultArgs.DataSource.Table;
                            ProjectSource = resultArgs.DataSource.Table; ;//set Project in ViewState
                            objReportProperty.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                            gvProject.DataSource = null;
                            gvProject.DataSource = resultArgs.DataSource.Table;
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
            finally { AssignProject(); }
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
                        ProjectCategorySource = resultArgs.DataSource.Table; ;//set Project Category in ViewState
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
                            //if (ProjectSource.Rows.Count == dvProject.ToTable().Rows.Count)
                            //{
                            //    objReportProperty.ProjectTitle = dvProject.ToTable().Rows.Count == 1 ? "  " + ProjectName : "  " + SCONSTATEMENT;
                            //}
                            //else
                            //{

                            objReportProperty.ProjectTitle = ProjectName.TrimEnd(',');
                            // }
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
                    //gvProjectCategory.DataSource = null;
                    //gvProjectCategory.DataSource = ProjectCategorySource;
                    //gvProjectCategory.DataBind();
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
                    var Selected = (from d in dtBank.AsEnumerable()
                                    where ((d.Field<Int32>(SELECT) == 1))
                                    select d);
                    var UnSelectedBank = (from d in dtBank.AsEnumerable()
                                          where ((d.Field<Int32>(SELECT) != 1))
                                          select d);

                    if (Selected.Count() > 0)
                    {
                        BankSelected = Selected.CopyToDataTable();
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
                    if (UnSelectedBank.Count() > 0)
                    {
                        DataTable UnSelectedBankAccount = UnSelectedBank.CopyToDataTable();
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
                    var Selected = (from d in dtLedger.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    var UnSelected = (from d in dtLedger.AsEnumerable()
                                      where ((d.Field<Int32?>(SELECT) != 1))
                                      select d);
                    if (Selected.Count() > 0)
                    {
                        dtLedgerDetails = Selected.CopyToDataTable();
                        if (dtLedgerDetails != null && dtLedgerDetails.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dtLedgerDetails.Rows)
                            {
                                SelectedLedgerDetailsId += dr[0] + ",";
                            }
                            SelectedLedgerDetailsId = SelectedLedgerDetailsId.TrimEnd(',');
                        }
                    }
                    if (UnSelected.Count() > 0)
                    {
                        DataTable dtUnSelectedLedgerId = UnSelected.CopyToDataTable();
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
                if (gvLedgerGroup.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(LedgerGroupId) && LedgerGroupSource != null)
                    {
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
                if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count != 0)
                {
                    var Selected = (from d in dtLedgerGroup.AsEnumerable()
                                    where ((d.Field<Int32>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        LedgerGroupSelected = Selected.CopyToDataTable();
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
                    var Selected = (from d in dtBranch.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() > 0)
                    {
                        BranchSelected = Selected.CopyToDataTable();
                        if (BranchSelected != null && BranchSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BranchSelected.Rows)
                            {
                                SelectedBranchCode += dr[0] + ",";
                                BranchName += dr[this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString() + ", ";
                            }
                            SelectedBranchCode = SelectedBranchCode.TrimEnd(',');
                            BranchName = BranchName.TrimEnd(' ');
                            if (BranchName.Trim() != string.Empty)
                            {
                                objReportProperty.BranchOfficeName = BranchName.TrimEnd(',');
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
            return SelectedBranchCode;
        }

        private string SelectedSocietyCode()
        {
            string SelectedSociety = string.Empty;
            try
            {
                if (gvSociety.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(SocietyCode) && SocietySource != null)
                    {
                        ArrayList societyCode = new ArrayList(SocietyCode.Split(new char[] { ',' }));
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
                    var Selected = (from d in dtsociety.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() > 0)
                    {
                        SocietySelected = Selected.CopyToDataTable();
                        if (SocietySelected != null && SocietySelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in SocietySelected.Rows)
                            {
                                SelectedSociety += dr[0] + ",";
                            }
                            SelectedSociety = SelectedSociety.TrimEnd(',');
                        }
                    }
                }
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
            string BudgetId = string.Empty;
            string UnSelectedAccountId = string.Empty;
            try
            {
                if (gvBankAccount.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(BankId) && BankSource != null)
                    {
                        ArrayList bankIds = new ArrayList(BankId.Split(new char[] { ',' }));
                        foreach (string bankid in bankIds)
                        {
                            int i = 0;
                            for (i = 0; i < BankSource.Rows.Count; i++)
                            {
                                if (bankid.Equals(BankSource.Rows[i]["BANK_ID"].ToString()))
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
                    var Selected = (from d in dtBank.AsEnumerable()
                                    where ((d.Field<Int32?>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        BudgetSelected = Selected.CopyToDataTable();
                        if (BudgetSelected != null && BudgetSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in BudgetSelected.Rows)
                            {
                                BudgetId += dr[0] + ",";
                            }
                            BudgetId = BudgetId.TrimEnd(',');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
            return BudgetId;
        }

        private void ClearFilterTextBoxes()
        {
            txtSearch.Text = txtSocietySearch.Text = txtProjectSearch.Text = txtProjectCategorySearch.Text =
                txtBankAccountFilter.Text = txtLedgerGroupFilter.Text = txtLedgerSearch.Text = txtCostCentreFilter.Text = string.Empty;
        }

        #endregion

        #region SetDate

        protected void TabDateProperties()
        {
            dteFrom.Date = this.Member.DateSet.ToDate(objSettingProperty.YearFrom, false);
            dteTo.Date = dteFrom.Date.AddMonths(1).AddDays(-1);
            dtreportdate.Date = this.Member.DateSet.ToDate(DateTime.Now.Date.ToString(), true);
        }

        #endregion

        #region Callback
        protected void ReportCriteriaCallback_Callback(object source, CallbackEventArgs e)
        {
            AssignValues();
        }
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
                else
                {
                    result = "";
                }
            }
            else if (eventArgument == "SY")
            {
                SocietyCode = ids;
                result = SetProjectCallback();
            }
            else if (eventArgument == "PJ")
            {
                ProjectId = ids;
                if (gvBankAccount.Visible)
                {
                    result = SetBankCallback();
                }
                else
                {
                    result = "";
                }
            }
            else if (eventArgument == "BK")
            {
                BankId = ids;
                result = "";
            }
            else if (eventArgument == "LGG")
            {
                LedgerGroupId = ids;
                result = SetLedgerCallback();
            }
            else if (eventArgument == "LG")
            {
                LedgerId = ids;
                result = "";
            }
            else if (eventArgument == "CC")
            {
                CostCentreId = ids;
                result = "";
            }
            else if (eventArgument == "PC")
            {
                ProjectCategoryId = ids;
                result = "";
            }
            else if (eventArgument == "BRF" || eventArgument == "BRFR")
            {
                result = SetBranchFilterCallback(ids);
            }
            else if (eventArgument == "SYF" || eventArgument == "SYFR")
            {
                result = SetSocietyFilterCallback(ids);
            }
            else if (eventArgument == "PJF" || eventArgument == "PJFR")
            {
                result = SetProjectFilterCallback(ids);
            }
            else if (eventArgument == "LGGF" || eventArgument == "LGGFR")
            {
                result = SetLedgerGroupFilterCallback(ids);
            }
            else if (eventArgument == "LGF" || eventArgument == "LGFR")
            {
                result = SetLedgerFilterCallback(ids);
            }
            else if (eventArgument == "CCF" || eventArgument == "CCFR")
            {
                result = SetCostCentreFilterCallback(ids);
            }
            else if (eventArgument == "BKF" || eventArgument == "BKFR")
            {
                result = SetBankAccountFilterCallback(ids);
            }
            else if (eventArgument == "PCF" || eventArgument == "PCRF")
            {
                result = SetProjectCategoryFilterCallback(ids);
            }
            else
            {
                result = "Call Back Error";
            }
        }

        #endregion

        #region Report Caching
        protected void dvReportViewer_Unload(object sender, EventArgs e)
        {
            dvReportViewer.Report = null;
            upReport.Update();
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

        #region Drill-Down Methods

        private void InitialDrillDownProperties(string RptId)
        {
            if (RptId != string.Empty)
            {
                ClearDrillDown();
                //Add base report into the collection for drill down, when it gets loaded in the viewer
                EventDrillDownArgs baseRepotEventArug = new EventDrillDownArgs(DrillDownType.BASE_REPORT, RptId, new Dictionary<string, object>());
                AddDrillDown(baseRepotEventArug);
            }
        }

        private void AddDrillDown(EventDrillDownArgs eventDrillDown)
        {
            if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Count == 0)
            {
                ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Push(eventDrillDown);
            }
            else
            {
                if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]) != null)
                {
                    EventDrillDownArgs rteventValue = ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Peek();
                    if (rteventValue.DrillDownRpt != eventDrillDown.DrillDownRpt)
                    {
                        ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Push(eventDrillDown);
                    }
                }
            }
        }

        private EventDrillDownArgs GetRecentDrillDown()
        {
            EventDrillDownArgs RtneventDrill = null;
            if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Count > 1)
            {
                ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Pop();
                RtneventDrill = ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Peek();
                if (RtneventDrill.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    objReportProperty.ReportId = RtneventDrill.DrillDownRpt;
                    InitiateReportCriteria();
                }
                else
                {
                    dvReportViewer.ToolbarItems.RemoveAt(0);
                    ShowDrillDownInToolbar();
                    objReportProperty.ReportId = RtneventDrill.DrillDownRpt;
                }
            }
            return RtneventDrill;
        }

        private void DrillDownProperty()
        {
            try
            {
                Dictionary<string, object> dicDrillDownProperties = new Dictionary<string, object>();
                DrillDownType ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), Request.QueryString["DrillDownType"].ToString());
                dicDrillDownProperties.Add("DrillDownLink", ddtypeLinkType.ToString());
                ArrayList drilldownItmes = new ArrayList();
                string DrillToRptId = Request.QueryString["rid"].ToString();
                string sLinkField = Request.QueryString["FNAME"] != null ? Request.QueryString["FNAME"].ToString() : string.Empty;
                drilldownItmes.Add(sLinkField);
                if (Request.QueryString["PNAME"] != null)
                {
                    sLinkField = Request.QueryString["PNAME"].ToString();
                    drilldownItmes.Add(sLinkField);
                }
                string sVoucherSubTypeField = string.Empty;
                string sLinkFieldValue = string.Empty;
                string sVoucherType = string.Empty;
                sLinkFieldValue = Request.QueryString["FVALUE"] != null ? Request.QueryString["FVALUE"].ToString() : string.Empty;
                sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;

                foreach (string drilldownItem in drilldownItmes)
                {
                    sLinkField = drilldownItem;
                    if (sLinkField == "COST_CENTRE_ID")
                    {
                        sLinkFieldValue = this.objReportProperty.CostCentre;
                        sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;
                    }
                    else if (sLinkField == "DATE_AS_ON")
                    {
                        sLinkFieldValue = this.objReportProperty.DateAsOn;
                        sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;
                    }
                    else
                    {
                        sLinkFieldValue = Request.QueryString["FVALUE"] != null ? Request.QueryString["FVALUE"].ToString() : string.Empty;
                        sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;
                    }
                    ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), Request.QueryString["DrillDownType"].ToString());
                    if ((ddtypeLinkType == DrillDownType.DRILL_DOWN ||
                        ddtypeLinkType == DrillDownType.LEDGER_VOUCHER) && sVoucherType == ledgerSubType.GN.ToString())
                    {
                        ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), Request.QueryString["PARTICULAR_TYPE"].ToString());
                    }
                    else if (sVoucherType == ledgerSubType.FD.ToString())
                    {
                        ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), "FD_VOUCHER");
                    }

                    DrillToRptId = Request.QueryString["rid"].ToString();
                    dicDrillDownProperties["DrillDownLink"] = Request.QueryString["DrillDownType"].ToString();
                    dicDrillDownProperties.Add(sLinkField, sLinkFieldValue);
                    if (!string.IsNullOrEmpty(sVoucherSubTypeField) && !string.IsNullOrEmpty(sVoucherType) && sLinkField != "DATE_AS_ON")
                    {
                        dicDrillDownProperties.Add(sVoucherSubTypeField, sVoucherType);
                    }

                }
                //Define DrillDown properties
                if (dicDrillDownProperties.Count > 1)
                {
                    ReportDrillDown = new EventDrillDownArgs(ddtypeLinkType, DrillToRptId, dicDrillDownProperties);
                    AddDrillDown(ReportDrillDown);
                    this.objReportProperty.DrillDownProperties = dicDrillDownProperties;
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Attaching DrillDownProperty::" + ex.Message);
            }
        }

        private void AttachDrillDownProperties()
        {
            DrillDownProperty();
        }

        private void ClearDrillDown()
        {
            if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]) != null)
            {
                ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Clear();
                if (objReportProperty.DrillDownProperties != null)
                {
                    objReportProperty.DrillDownProperties.Clear();
                    HttpContext.Current.Session["DrillDownProperties"] = null;
                }
            }
        }

        /// <summary>
        /// This method used to load drilldown/ledger/end transaction screen based on the event triggered 
        /// when user clicks
        /// </summary>
        /// <param name="eDrilldownevent"></param>
        private bool DrillDownTarget(EventDrillDownArgs eDrilldownevent)
        {
            bool bSucessDrillDown = false;
            if (eDrilldownevent != null && eDrilldownevent.DrillDownRpt != string.Empty)
            {
                // Load Drill-Down Report for selected Group
                switch (eDrilldownevent.DrillDownType)
                {
                    case DrillDownType.BASE_REPORT:
                    case DrillDownType.GROUP_SUMMARY:
                    case DrillDownType.GROUP_SUMMARY_RECEIPTS:
                    case DrillDownType.GROUP_SUMMARY_PAYMENTS:
                    case DrillDownType.LEDGER_SUMMARY:
                    case DrillDownType.LEDGER_SUMMARY_RECEIPTS:
                    case DrillDownType.LEDGER_SUMMARY_PAYMENTS:
                    case DrillDownType.LEDGER_CASH:
                    case DrillDownType.LEDGER_BANK:
                        objReportProperty.DrillDownProperties = eDrilldownevent.DrillDownProperties;
                        LoadReport(eDrilldownevent.DrillDownRpt);
                        bSucessDrillDown = true;
                        break;
                }
            }
            return bSucessDrillDown;
        }

        #endregion
    }

}