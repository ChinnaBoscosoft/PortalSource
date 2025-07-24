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

namespace AcMeERP.Report
{
    public partial class ReportCriteria : Base.UIBase
    {
        #region Declaration
        Bosco.Utility.ConfigSetting.SettingProperty settingProperty = new Bosco.Utility.ConfigSetting.SettingProperty();
        private const string DateCaption = "Date As on";
        private const string SELECT = "SELECT";
        private const string SCONSTATEMENT = "Consolidated Statement";
        string sDateFrom = string.Empty;
        string sDateTo = string.Empty;
        #endregion

        #region Property

        public DataTable ProjectSelected { get; set; }
        public DataTable BankSelected { get; set; }
        public DataTable LedgerGroupSelected { get; set; }
        public DataTable LedgerSelected { get; set; }
        public DataTable BudgetSelected { get; set; }
        public DataTable CostCentreSelected { get; set; }

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
        private int FilterFlag = 0;

        private string ProjectId
        {
            set
            {
                ViewState["ProjectId"] = value;
            }
            get
            {
                if (ViewState["ProjectId"] == null)
                    return string.Empty;
                else
                    return (string)ViewState["ProjectId"];
            }
        }

        private string LedgerGroupId
        {
            set
            {
                ViewState["LedgerGroupId"] = value;
            }
            get
            {
                if (ViewState["LedgerGroupId"] == null)
                    return string.Empty;
                else
                    return (string)ViewState["LedgerGroupId"];
            }
        }

        private string BankId
        {
            set
            {
                ViewState["BankId"] = value;
            }
            get
            {
                if (ViewState["BankId"] == null)
                    return string.Empty;
                else
                    return (string)ViewState["BankId"];
            }
        }

        private string CostCentreId
        {
            set
            {
                ViewState["CostCentreId"] = value;
            }
            get
            {
                if (ViewState["CostCentreId"] == null)
                    return string.Empty;
                else
                    return (string)ViewState["CostCentreId"];
            }
        }


        private string LedgerId
        {
            set
            {
                ViewState["LedgerId"] = value;
            }
            get
            {
                if (ViewState["LedgerId"] == null)
                    return string.Empty;
                else
                    return (string)ViewState["LedgerId"];
            }
        }

        private DataTable ProjectSource
        {
            set { ViewState["ProjectSource"] = value; }

            get
            {
                if (ViewState["ProjectSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["ProjectSource"];
            }
        }

        private DataTable LedgerGroupSource
        {
            set { ViewState["LedgerGroupSource"] = value; }

            get
            {
                if (ViewState["LedgerGroupSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["LedgerGroupSource"];
            }
        }
        private DataTable LedgerSource
        {
            set
            {
                ViewState["LedgerSource"] = value;
            }
            get
            {
                if (ViewState["LedgerSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["LedgerSource"];
            }
        }

        private DataTable BankSource
        {
            set { ViewState["BankSource"] = value; }

            get
            {
                if (ViewState["BankSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["BankSource"];
            }
        }

        private DataTable BudgetSource
        {
            set { ViewState["BudgetSource"] = value; }
            get
            {
                if (ViewState["BudgetSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["BudgetSource"];
            }
        }

        private DataTable CostCentreSource
        {
            set { ViewState["CostCentreSource"] = value; }

            get
            {
                if (ViewState["CostCentreSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["CostCentreSource"];
            }
        }

        private DataTable ReportsCriteria
        {
            set
            {
                ViewState["ReportsCriteria"] = value;
            }
            get
            {
                if (ViewState["ReportsCriteria"] == null)
                    return null;
                else
                    return (DataTable)ViewState["ReportsCriteria"];
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TabDateProperties();
                ReportProperty.Current.ReportId = Request.QueryString["rid"];
                ProjectSource = BankSource = LedgerSource = LedgerGroupSource = CostCentreSource = ReportsCriteria = null;
                EnableTabs();
                EnableReportSetupProperties();
                ProjectId = LedgerGroupId = BankId = string.Empty;
            }

        }

        protected void chkProjectSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlag = 1;
            CheckBox chkSelectAll = (CheckBox)sender;
            string[] aCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
            for (int i = 0; i < aCriteria.Length; i++)
            {
                switch (aCriteria[i])
                {
                    case "BK":
                        SetBankAccountSource();
                        break;
                    case "BU":
                        SetBudgetSource();
                        break;
                }
            }

            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < ProjectSource.Rows.Count; i++)
                {
                    ProjectSource.Rows[i]["SELECT"] = "1";
                }
                ProjectId = string.Empty;
            }
            else
            {
                for (int i = 0; i < ProjectSource.Rows.Count; i++)
                {
                    ProjectSource.Rows[i]["SELECT"] = "0";
                }
                ProjectId = string.Empty;
            }
        }
        protected void chkBankSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlag = 1;
            CheckBox chkSelectAll = (CheckBox)sender;
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < BankSource.Rows.Count; i++)
                {
                    BankSource.Rows[i]["SELECT"] = "1";
                }
                BankId = string.Empty;
            }
            else
            {
                for (int i = 0; i < BankSource.Rows.Count; i++)
                {
                    BankSource.Rows[i]["SELECT"] = "0";
                }
                BankId = string.Empty;
            }
        }

        protected void chkLedgerSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlag = 1;
            CheckBox chkSelectAll = (CheckBox)sender;
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < LedgerSource.Rows.Count; i++)
                {
                    LedgerSource.Rows[i]["SELECT"] = "1";
                }
                LedgerId = string.Empty;
            }
            else
            {
                for (int i = 0; i < LedgerSource.Rows.Count; i++)
                {
                    LedgerSource.Rows[i]["SELECT"] = "0";
                }
                LedgerId = string.Empty;
            }
        }

        protected void chkProjectSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterFlag != 1)
            {
                string chkvalue = ((CheckBox)sender).ValidationGroup.ToString();
                CheckBox chkStatus = (CheckBox)sender;
                if (chkStatus.Checked)
                {
                    if (ProjectId != string.Empty)
                    {
                        ProjectId = ProjectId + "," + chkvalue;
                    }
                    else
                    {
                        ProjectId = chkvalue;
                    }
                }
                else
                {
                    ArrayList numbers = new ArrayList(ProjectId.Split(new char[] { ',' }));
                    if (numbers.Count > 1)
                    {
                        if (numbers.Contains(chkvalue))
                        {
                            numbers.RemoveAt(numbers.IndexOf(chkvalue));
                            numbers.TrimToSize();
                            ProjectId = InsertComma(numbers);
                        }
                    }
                    //else
                    //{
                    //    SetBankAccountSource();
                    //}
                }

                string[] aCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
                try
                {
                    for (int i = 0; i < aCriteria.Length; i++)
                    {
                        switch (aCriteria[i])
                        {
                            case "BK":
                                {
                                    if (ProjectId == null)
                                    {
                                        SetBankAccountSource();
                                    }
                                    DataTable dtBankInfo = new DataTable();
                                    dtBankInfo = FetchBankByProject(ProjectId);
                                    if (ReportProperty.Current.ReportId == "RPT-047")
                                    {
                                        using (Bosco.Report.Base.BalanceSystem bankbalanceSystem = new Bosco.Report.Base.BalanceSystem())
                                        {
                                            dtBankInfo = bankbalanceSystem.FetchFDByProjectId(ProjectId);
                                        }
                                    }
                                    gvBankAccount.DataSource = null;
                                    gvBankAccount.DataBind();
                                    gvBankAccount.DataSource = dtBankInfo;
                                    gvBankAccount.DataBind();
                                    break;
                                }
                            case "BU":
                                {
                                    if (ProjectId == null)
                                    {
                                        SetBudgetSource();
                                    }
                                    DataTable dtBudgetInfo = new DataTable();
                                    using (Bosco.Report.Base.BalanceSystem bankbalanceSystem = new Bosco.Report.Base.BalanceSystem())
                                    {
                                        dtBudgetInfo = bankbalanceSystem.FetchBudgetsByProjects(ProjectId);
                                    }
                                    gvBankAccount.DataSource = null;
                                    gvBankAccount.DataSource = dtBudgetInfo;
                                    gvBankAccount.DataBind();
                                    break;

                                    //            //        chkBankSelectAll.Checked = false;
                                    //DataTable dtProjectId = (DataTable)gvProject.DataSource;
                                    //DataTable dtBudgetId = (DataTable)gvBankAccount.DataSource;
                                    //if (dtProjectId != null && dtProjectId.Rows.Count != 0)
                                    //{
                                    //    var Selected = (from d in dtProjectId.AsEnumerable()
                                    //                    where ((d.Field<Int32?>("SELECT") == 1))
                                    //                    select d);
                                    //if (Selected.Count() != 0)
                                    //{
                                    //    DataTable dtBank = Selected.CopyToDataTable();
                                    //    foreach (DataRow dr in dtBank.Rows)
                                    //    {
                                    //        ProjectId += dr[0] + ",";
                                    //    }
                                    //    ProjectId = ProjectId.TrimEnd(',');
                                    //    using (Bosco.Report.Base.BalanceSystem bankBalanceSystem = new Bosco.Report.Base.BalanceSystem())
                                    //    {
                                    //        dtBudgetId = bankBalanceSystem.FetchBudgetsByProjects(ProjectId);
                                    //        dtBudgetId.Columns.Add("SELECT", typeof(Int32));
                                    //    }
                                    //    gvBankAccount.DataSource = dtBudgetId;
                                    //    //         gvBankAccount.RefreshDataSource();
                                    //}
                                    //else
                                    //{
                                    //    using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetNames))
                                    //    {
                                    //        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                                    //        if (resultArgs.Success)
                                    //        {
                                    //            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                    //            {
                                    //                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
                                    //                BudgetSelected = resultArgs.DataSource.Table;
                                    //                ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                    //                gvBankAccount.DataSource = null;
                                    //                gvBankAccount.DataSource = resultArgs.DataSource.Table;
                                    //                //               gvBankAccount.RefreshDataSource();
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //      }
                                    //      else
                                    //      {
                                    ////          SetBudgetSource();
                                    //      }

                                    // break;
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
        }
        protected void chkBankSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterFlag != 1)
            {
                string chkvalue = ((CheckBox)sender).ValidationGroup.ToString();
                CheckBox chkStatus = (CheckBox)sender;
                if (chkStatus.Checked)
                {
                    if (BankId != string.Empty)
                    {
                        BankId = BankId + "," + chkvalue;
                    }
                    else
                    {
                        BankId = chkvalue;
                    }
                }
                else
                {
                    ArrayList numbers = new ArrayList(BankId.Split(new char[] { ',' }));
                    if (numbers.Count > 1)
                    {
                        if (numbers.Contains(chkvalue))
                        {
                            numbers.RemoveAt(numbers.IndexOf(chkvalue));
                            numbers.TrimToSize();
                            BankId = InsertComma(numbers);
                        }
                    }

                }
            }

        }

        protected void chkLedgerSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterFlag != 1)
            {
                string chkvalue = ((CheckBox)sender).ValidationGroup.ToString();
                CheckBox chkStatus = (CheckBox)sender;
                if (chkStatus.Checked)
                {
                    if (LedgerId != string.Empty)
                    {
                        LedgerId = LedgerId + "," + chkvalue;
                    }
                    else
                    {
                        LedgerId = chkvalue;
                    }
                }
                else
                {
                    ArrayList numbers = new ArrayList(LedgerId.Split(new char[] { ',' }));
                    if (numbers.Count > 1)
                    {
                        if (numbers.Contains(chkvalue))
                        {
                            numbers.RemoveAt(numbers.IndexOf(chkvalue));
                            numbers.TrimToSize();
                            LedgerId = InsertComma(numbers);
                        }
                    }

                }
            }
        }

        protected void chkLedgerGroupSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlag = 1;
            CheckBox chkSelectAll = (CheckBox)sender;
            SetLedgerDetailSource();
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < LedgerGroupSource.Rows.Count; i++)
                {
                    LedgerGroupSource.Rows[i]["SELECT"] = "1";
                }
                LedgerGroupId = string.Empty;
            }
            else
            {
                for (int i = 0; i < LedgerGroupSource.Rows.Count; i++)
                {
                    LedgerGroupSource.Rows[i]["SELECT"] = "0";
                }
                LedgerGroupId = string.Empty;
            }
        }

        protected void ddlBranch_TextChanged(object sender, EventArgs e)
        {
            ddlBranch.Text = ddlBranch.SelectedItem.Value;
            ReportProperty.Current.BranchOffice = ddlBranch.SelectedItem.Value;
        }

        protected void chkLedgerGroupSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterFlag != 1)
            {
                string chkvalue = ((CheckBox)sender).ValidationGroup.ToString();
                CheckBox chkStatus = (CheckBox)sender;
                if (chkStatus.Checked)
                {
                    if (LedgerGroupId != string.Empty)
                        LedgerGroupId = LedgerGroupId + "," + chkvalue;
                    else
                        LedgerGroupId = chkvalue;
                }
                else
                {
                    ArrayList numbers = new ArrayList(LedgerGroupId.Split(new char[] { ',' }));
                    if (numbers.Count > 1)
                    {
                        if (numbers.Contains(chkvalue))
                        {
                            numbers.RemoveAt(numbers.IndexOf(chkvalue));
                            numbers.TrimToSize();
                            LedgerGroupId = InsertComma(numbers);
                        }
                    }
                    else
                    {
                        SetLedgerSource();
                    }
                }

                string[] aCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
                try
                {
                    for (int i = 0; i < aCriteria.Length; i++)
                    {
                        switch (aCriteria[i])
                        {
                            case "LG":
                                {
                                    DataTable dtLedgerInfo = new DataTable();
                                    if (gvLedger.DataSource == null)
                                    {
                                        SetLedgerDetailSource();
                                    }
                                    dtLedgerInfo = gvLedger.DataSource as DataTable;
                                    if (dtLedgerInfo != null && dtLedgerInfo.Rows.Count > 0)
                                    {
                                        DataView dtLedgerFilter = new DataView(dtLedgerInfo);
                                        dtLedgerFilter.RowFilter = "GROUP_ID IN(" + LedgerGroupId + ")";
                                        dtLedgerInfo = null;
                                        dtLedgerInfo = dtLedgerFilter.ToTable();
                                        //LedgerGroupSource = dtLedgerFilter.ToTable();
                                    }
                                    gvLedger.DataSource = null;
                                    gvLedger.DataBind();
                                    gvLedger.DataSource = dtLedgerInfo;
                                    gvLedger.DataBind();
                                    // LedgerGroupSource = dtLedgerInfo;
                                    break;
                                }

                        }
                    }
                }

                catch (Exception ex)
                {
                    this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
                }
                finally { }

            }
        }
        protected void chkCostCentreSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlag = 1;
            CheckBox chkSelectAll = (CheckBox)sender;
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < CostCentreSource.Rows.Count; i++)
                {
                    CostCentreSource.Rows[i]["SELECT"] = "1";
                }
                CostCentreId = string.Empty;
            }
            else
            {
                for (int i = 0; i < CostCentreSource.Rows.Count; i++)
                {
                    CostCentreSource.Rows[i]["SELECT"] = "0";
                }
                CostCentreId = string.Empty;
            }
        }
        protected void chkCostCentreSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterFlag != 1)
            {
                string[] aCriteria = ReportProperty.Current.ReportCriteria.Split('ÿ');
                try
                {
                    for (int i = 0; i < aCriteria.Length; i++)
                    {
                        switch (aCriteria[i])
                        {
                            case "CC":
                                {
                                    string chkvalue = ((CheckBox)sender).ValidationGroup.ToString();
                                    CheckBox chkStatus = (CheckBox)sender;
                                    if (chkStatus.Checked)
                                    {
                                        if (CostCentreId != string.Empty)
                                            CostCentreId = CostCentreId + "," + chkvalue;
                                        else
                                            CostCentreId = chkvalue;
                                    }
                                    else
                                    {
                                        ArrayList numbers = new ArrayList(CostCentreId.Split(new char[] { ',' }));
                                        if (numbers.Count > 1)
                                        {
                                            if (numbers.Contains(chkvalue))
                                            {
                                                numbers.RemoveAt(numbers.IndexOf(chkvalue));
                                                numbers.TrimToSize();
                                                CostCentreId = InsertComma(numbers);
                                            }
                                        }
                                        else
                                        {
                                            SetCostCentreSource();
                                        }
                                    }
                                    break;
                                }

                        }
                    }
                }

                catch (Exception ex)
                {
                    this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
                }
                finally { }
            }


        }
        protected void chkDateLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkvalue = (CheckBox)sender;
                string Value = chkvalue.Text;
                if (chkvalue.Checked)
                {
                    for (int i = 0; i < ReportsCriteria.Rows.Count; i++)
                    {
                        if (ReportsCriteria.Rows[i]["NAME"].Equals(Value))
                        {
                            ReportsCriteria.Rows[i]["SELECT"] = "1";
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ReportsCriteria.Rows.Count; i++)
                    {
                        if (ReportsCriteria.Rows[i]["NAME"].Equals(Value))
                        {
                            ReportsCriteria.Rows[i]["SELECT"] = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //string projectIds = SelectedProject();
            //string bankIds = SelectedBankDetails();
            //string ledgerGroupIds = SelectedLedgerGroup();
            //string ledgerIds = SelectedLedgerGroupDetails();
            //string costCenterIds = SelectedCostCentre();
            //Save Report Setting Properties
            try
            {
                DataTable dtCriteria = ReportsCriteria;
                string Criteria = ReportProperty.Current.ReportCriteria;
                string[] aCriteria = Criteria.Split('ÿ');
                for (int i = 0; i < aCriteria.Length; i++)
                {
                    if (aCriteria[i] == "DA")
                    {
                        ReportProperty.Current.DateAsOn = dtFrom.DateValue;
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
                            if (!string.IsNullOrEmpty(dtFrom.DateValue.Trim()) && !string.IsNullOrEmpty(dtTo.DateValue.Trim()))
                            {
                                //  if (DateFrom.DateTime > DateTo.DateTime)

                                if (!this.Member.DateSet.ValidateDate(this.Member.DateSet.ChangeDateFormat(dtFrom.DateValue, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME), this.Member.DateSet.ChangeDateFormat(dtTo.DateValue, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME)))
                                {
                                    DateTime date = Convert.ToDateTime(dtFrom.DateValue);
                                    dtFrom.DateValue = dtTo.DateValue;
                                    dtTo.DateValue = date.ToString();
                                    ReportProperty.Current.DateFrom = dtFrom.DateValue;
                                    ReportProperty.Current.DateTo = dtTo.DateValue;
                                    break;
                                }
                                else
                                {
                                    ReportProperty.Current.DateFrom = dtFrom.DateValue;
                                    ReportProperty.Current.DateTo = dtTo.DateValue;
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
                        case "PJ":
                            {
                                string ProjectId = SelectedProject();
                                if (!string.IsNullOrEmpty(ProjectId))
                                {
                                    Bosco.Report.Base.ReportProperty.Current.Project = ProjectId;
                                }
                                else
                                {
                                    this.Message = "A project must be selected.";
                                    //  xtbReportCriteria.SelectedTabPageIndex = 1;
                                    return;
                                }
                                break;
                            }
                        case "BK":
                            {
                                string LedgerId = SelectedBankDetails();

                                if (!string.IsNullOrEmpty(LedgerId))
                                {
                                    Bosco.Report.Base.ReportProperty.Current.Ledger = LedgerId;
                                }
                                else
                                {
                                    ReportProperty.Current.Ledger = "0";
                                }

                                break;
                            }
                        case "BU":
                            {
                                // string BudgetId = SelectedBudgetDetails();

                                //if (!string.IsNullOrEmpty(BudgetId))
                                //{
                                //    ReportProperty.Current.Budget = BudgetId;
                                //}
                                //else
                                //{
                                //    XtraMessageBox.Show("Budget Name must be selected.", "AcMe++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return;
                                //    //  ReportProperty.Current.Budget = "0";
                                //}
                                break;
                            }
                        case "LG":
                            {
                                string LedgerId = SelectedLedgerGroupDetails();
                                if (LedgerId != string.Empty)
                                {
                                    ReportProperty.Current.Ledger = LedgerId;
                                    string LedgerGroupId = SelectedLedgerGroup();
                                    if (!string.IsNullOrEmpty(LedgerGroupId))
                                    {
                                        ReportProperty.Current.LedgerGroup = LedgerGroupId;
                                    }
                                    else
                                    {
                                        ReportProperty.Current.LedgerGroup = "0";
                                    }
                                }
                                else
                                {
                                    //   this.Message("A Ledger must be selected!", "AcMe++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //   xtbReportCriteria.SelectedTabPageIndex = 2;
                                    return;
                                }
                                break;
                            }
                        case "CC":
                            {
                                string CostCentreId = SelectedCostCentre();
                                if (!string.IsNullOrEmpty(CostCentreId))
                                {
                                    ReportProperty.Current.CostCentre = CostCentreId;
                                }
                                else
                                {
                                    ReportProperty.Current.CostCentre = "0";
                                    //      xtbReportCriteria.SelectedTabPageIndex = 3;
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
                                    ReportProperty.Current.IncludeAllLedger = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "BL": //Set Values to Show By Ledgers
                                {
                                    ReportProperty.Current.ShowByLedger = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "BG": // Set Values to Ledger Groups.
                                {
                                    ReportProperty.Current.ShowByLedgerGroup = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "AB": // Set Values to Detailed Balance.
                                {
                                    ReportProperty.Current.ShowDetailedBalance = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "BA": // Include Bank Account Details. 
                                {
                                    ReportProperty.Current.IncludeBankAccount = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "DB": //Set Values to Daily Balance.
                                {
                                    ReportProperty.Current.ShowDailyBalance = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "BD": //Include Bank Details
                                {
                                    ReportProperty.Current.IncludeBankDetails = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "IK": //Set Values to Include In Kind.
                                {
                                    // ReportProperty.Current.IncludeInKind = rboInKind.SelectedIndex;
                                    break;
                                }
                            case "IJ": //Set Values to Include Journal
                                {
                                    ReportProperty.Current.IncludeJournal = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "GT": //Set Values to Group Total
                                {
                                    ReportProperty.Current.IncludeLedgerGroupTotal = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "AG": //Set Values to Attach Group
                                {
                                    ReportProperty.Current.IncludeLedgerGroup = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "AC": //Set Values to Attach Cost Centre
                                {
                                    ReportProperty.Current.IncludeCostCentre = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "MT": //Set Values to Month Wise Total
                                {
                                    ReportProperty.Current.ShowMonthTotal = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "AD": // Set Values to Donor Address
                                {
                                    ReportProperty.Current.ShowDonorAddress = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                            case "CD": // Set Values to Donor Category
                                {
                                    break;
                                }
                            case "IN": //include Narration
                                {
                                    ReportProperty.Current.IncludeNarration = this.Member.NumberSet.ToInteger(dtCriteria.Rows[i]["SELECT"].ToString());
                                    break;
                                }
                        }
                    }
                }
                SaveReportSetup();
                ReportProperty.Current.SaveReportSetting();
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
                ReportProperty.Current.TitleAlignment = ddlTitleAlignment.SelectedIndex;
                ReportProperty.Current.ShowLogo = chkShowReportLogo.Checked ? 1 : 0;
                ReportProperty.Current.ShowPageNumber = chkShowPageNumber.Checked ? 1 : 0;
                ReportProperty.Current.ShowLedgerCode = chkShowLedgerCode.Checked ? 1 : 0;
                ReportProperty.Current.ShowGroupCode = chkShowGroupCode.Checked ? 1 : 0;
                ReportProperty.Current.SortByLedger = ddlSortByLedger.SelectedIndex;
                ReportProperty.Current.SortByGroup = ddlSortByGroup.SelectedIndex;
                ReportProperty.Current.ShowHorizontalLine = chkHorizontalLine.Checked ? 1 : 0;
                ReportProperty.Current.ShowVerticalLine = chkVerticalLine.Checked ? 1 : 0;
                ReportProperty.Current.ShowTitles = chkShowTitle.Checked ? 1 : 0;
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomeLogin.aspx");
        }
        #endregion

        #region Methods

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
            BindBranch();
            if (ReportProperty.Current.ReportId != null)
            {
                AssignReportCriteria();//Load the current date for Date From and Date To Control.
                ddlSortByLedger.SelectedIndex = 0;
                ddlSortByGroup.SelectedIndex = 0;
                dtCriteria = ConstructTable();
                string value = ReportProperty.Current.ReportCriteria;
                string[] test = value.Split('ÿ');
                foreach (string s in test)
                {
                    switch (s)
                    {
                        case "DF": //Date From 
                            {
                                dtFrom.Visible = true;
                                break;
                            }
                        case "DT": //Date To
                            {
                                dtTo.Visible = true;
                                break;
                            }
                        case "DA": //Date As On 
                            {
                                lblDtTo.Visible = false;
                                dtFrom.Visible = true;
                                lblDtFrom.Text = DateCaption;
                                break;
                            }
                        case "AL": //Show All Ledgers.
                            {
                                if (ReportProperty.Current.IncludeAllLedger != 0)
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
                                if (ReportProperty.Current.ShowByLedger != 0)
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
                                if (ReportProperty.Current.ShowByLedgerGroup != 0)
                                {
                                    dtCriteria.Rows.Add(1, "BG", "Show By Ledger Group");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "BG", "Show By Ledger Group");
                                }
                                break;
                            }

                        case "DB": //Daily Balance
                            {
                                if (ReportProperty.Current.ShowDailyBalance != 0)
                                {
                                    dtCriteria.Rows.Add(1, "DB", "Show Daily Balance");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(0, "DB", "Show Daily Balance");
                                }
                                break;
                            }
                        case "BD": //Include Bank Details
                            {
                                if (ReportProperty.Current.IncludeBankDetails != 0)
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
                                if (ReportProperty.Current.IncludeBankAccount != 0)
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
                                if (ReportProperty.Current.IncludeLedgerGroupTotal != 0)
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
                                if (ReportProperty.Current.IncludeLedgerGroup != 0)
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
                                if (ReportProperty.Current.IncludeCostCentre != 0)
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
                                if (ReportProperty.Current.ShowMonthTotal != 0)
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
                                if (ReportProperty.Current.ShowDonorAddress != 0)
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
                                if (ReportProperty.Current.ShowDetailedBalance != 0)
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
                                if (ReportProperty.Current.IncludeNarration != 0)
                                {
                                    dtCriteria.Rows.Add(1, "IN", "Include Narration");
                                }
                                else
                                {
                                    dtCriteria.Rows.Add(1, "IN", "Include Narration");
                                }
                                break;
                            }
                        case "PJ": //Show Project tabs.
                            {
                                TabProject.Visible = true;
                                //if (!String.IsNullOrEmpty(ReportProperty.Current.Project) && ReportProperty.Current.Project != "0")
                                //{
                                //    AssignSelectedProject(); //Set project Source to the grid control.
                                //}
                                //else
                                //{

                                SetProjectSource();
                                //}
                                break;
                            }
                        case "BK": //Bank Account Details.
                            {
                                gvBankAccount.Visible = true;
                                divProject.Style["width"] = "50%";
                                divBank.Style["width"] = "48%";
                                //if (!string.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0")
                                //{
                                //    AssignSelectedBankDetails();
                                //}
                                //else
                                //{

                                SetBankAccountSource();

                                //}
                                break;
                            }
                        case "LG": //Show Ledgers With Groups.
                            {
                                TabLedger.Visible = true;

                                //if (!String.IsNullOrEmpty(ReportProperty.Current.Ledger) && ReportProperty.Current.Ledger != "0")
                                //{
                                //    //          AssignSelectedLedgerDetails();
                                //}
                                //else
                                //{
                                SetLedgerSource();
                                //}
                                break;
                            }
                        case "BU": // Get Budget Details.
                            {
                                divProject.Style["width"] = "50%";
                                divBank.Style["width"] = "48%";
                                gvBankAccount.Visible = true;
                                //    lcgBankAccount.Text = "Budget";
                                //if (!string.IsNullOrEmpty(ReportProperty.Current.Budget) && ReportProperty.Current.Budget != "0")
                                //{
                                //    AssignSelectedBudgetDetails();
                                //}
                                //else
                                //{
                                SetBudgetSource();
                                //}
                                break;
                            }
                        case "BR":
                            {
                                BindBranch();
                                break;
                            }
                        case "CC": //Show Cost Centre Details.
                            {
                                TabCostCentre.Visible = true;
                                if (!String.IsNullOrEmpty(ReportProperty.Current.CostCentre) && ReportProperty.Current.CostCentre != "0")
                                {
                                    //           AssignSelectedCostCentreDetails();
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
                gvDateLedger.DataSource = dtCriteria;
                gvDateLedger.DataBind();
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
                if (!String.IsNullOrEmpty(ReportProperty.Current.DateFrom) && !String.IsNullOrEmpty(ReportProperty.Current.DateTo))
                {
                    if (ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false) > ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateFrom, false))
                        dtFrom.DateValue = ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).ToString();
                    else
                    {
                        dtFrom.DateValue = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateFrom, false).ToString();
                        dtTo.DateValue = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateTo, false).ToString();
                    }

                }
                else if (!String.IsNullOrEmpty(ReportProperty.Current.DateAsOn))
                {
                    dtFrom.DateValue = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateAsOn, false).ToString();
                }
                else
                {
                    dtFrom.DateValue = ReportProperty.Current.DateSet.ToDate(settingProperty.BookBeginFrom, false).ToString();
                }
                if (!String.IsNullOrEmpty(ReportProperty.Current.ReportDate))
                {
                    ReportDate.DateValue = ReportProperty.Current.ReportDate;
                }
                else
                {
                    ReportDate.DateValue = ReportProperty.Current.DateSet.ToDate(DateTime.Today.ToString(), false).ToString();
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
            switch (ReportProperty.Current.ReportId)
            {
                case "RPT-007":
                case "RPT-008":
                case "RPT-009":
                case "RPT-010":
                case "RPT-013":
                    {
                        ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = false;
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
                        chkShowLedgerCode.Enabled = ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = false;
                        break;
                    }
                case "RPT-011":
                case "RPT-012":
                case "RPT-016":
                case "RPT-017":
                    {
                        ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = false;
                        break;
                    }
                case "RPT-027":
                case "RPT-028":
                    //case "RPT-029":
                    {
                        chkShowLedgerCode.Enabled = ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = true;
                        chkHorizontalLine.Enabled = false;
                        break;
                    }
                case "RPT-030":
                    {
                        chkShowLedgerCode.Enabled = ddlSortByLedger.Enabled = chkShowGroupCode.Enabled = ddlSortByGroup.Enabled = true;
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
            finally { }
        }
        private void BindBranch()
        {
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                ResultArgs resultArgs = new ResultArgs();
                resultArgs = headOfficeSystem.FetchBranchByHeadOffice(base.HeadOfficeCode, base.LoginUser.LoginUserHeadOfficeCode==string.Empty?DataBaseType.Portal : DataBaseType.HeadOffice);
                if (resultArgs.Success)
                {
                    this.Member.ComboSet.BindDataCombo(ddlBranch, resultArgs.DataSource.Table
                    , this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName
                    , this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName
                    , true, CommonMember.SELECT);
                }
            }
        }

        private void SetBudgetSource()
        {
            try
            {
                if (string.IsNullOrEmpty(ReportProperty.Current.Project) || ReportProperty.Current.Project == "0")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetNames, DataBaseType.HeadOffice))
                    {
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int64));
                                BudgetSelected = resultArgs.DataSource.Table;
                                ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                gvBankAccount.DataSource = null;
                                //  gvBankAccount.Caption = "Budget";
                                gvBankAccount.DataSource = resultArgs.DataSource.Table;
                                gvBankAccount.DataBind();
                            }
                        }
                    }
                }
                else
                {
                    DataTable dtBudgetDetails = null;
                    using (Bosco.Report.Base.BalanceSystem bankBalanceSystem = new Bosco.Report.Base.BalanceSystem())
                    {
                        dtBudgetDetails = bankBalanceSystem.FetchBudgetsByProjects(ReportProperty.Current.Project);
                        dtBudgetDetails.Columns.Add("SELECT", typeof(Int64));
                    }
                    gvBankAccount.DataSource = dtBudgetDetails;
                    BudgetSelected = dtBudgetDetails;

                }

                if (!string.IsNullOrEmpty(ReportProperty.Current.Budget) && ReportProperty.Current.Budget != "0")
                {
                    DataTable dtBank = (DataTable)gvBankAccount.DataSource;
                    string[] BudgetId = ReportProperty.Current.Budget.ToString().Split(',');
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
                            gvLedgerGroup.DataSource = resultArgs.DataSource.Table;
                            gvLedgerGroup.DataBind();
                        }
                        //if (ReportProperty.Current.Ledger =="0")
                        //{
                        SetLedgerDetailSource();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }

        private void SetLedgerDetailSource()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.SetLedgerDetailSource, DataBaseType.HeadOffice))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            LedgerSelected = resultArgs.DataSource.Table;
                            LedgerSource = resultArgs.DataSource.Table;
                            gvLedger.DataSource = resultArgs.DataSource.Table;
                            gvLedger.DataBind();
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

        private void SetBankAccountSource()
        {
            try
            {
                if (string.IsNullOrEmpty(ReportProperty.Current.Project) || ReportProperty.Current.Project == "0")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Bank.SelectAllBank, DataBaseType.HeadOffice))
                    {
                        ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                if (ReportProperty.Current.ReportId == "RPT-047")
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
                                    ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
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
                    DataTable dtBankDetails = FetchBankByProject(ReportProperty.Current.Project);
                    if (ReportProperty.Current.ReportId == "RPT-047")
                    {
                        using (Bosco.Report.Base.BalanceSystem bankbalanceSystem = new Bosco.Report.Base.BalanceSystem())
                        {
                            dtBankDetails = bankbalanceSystem.FetchFDByProjectId(ReportProperty.Current.Project);
                        }
                    }
                    gvBankAccount.DataSource = dtBankDetails;
                    BankSelected = dtBankDetails;
                    BankSource = dtBankDetails;
                }


            }
            catch (Exception ex)
            {
                this.Message = ex.Message + "\n" + ex.StackTrace.ToString();
            }
            finally { }
        }

        private DataTable FetchAllFixedDeposit()
        {
            using (DataManager data = new DataManager(SQLCommand.Bank.SelectAllFD, DataBaseType.HeadOffice))
            {
                ResultArgs resultArgs = data.FetchData(DataSource.DataTable);
                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
                BankSelected = resultArgs.DataSource.Table;
                ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                return resultArgs.DataSource.Table;
            }

        }

        private DataTable FetchBankByProject(string ProjectId)
        {
            DataTable dtBankDetails = new DataTable();
            try
            {
                using (Bosco.Report.Base.BalanceSystem bankBalanceSystem = new Bosco.Report.Base.BalanceSystem())
                {
                    dtBankDetails = bankBalanceSystem.FetchBankByProjectId(ProjectId);
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
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjects, DataBaseType.HeadOffice))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {

                            if (ReportProperty.Current.ReportId == "RPT-043")
                            {
                                DataTable dtForeignPro = FetchForeignProjects(resultArgs.DataSource.Table);
                                ProjectSelected = dtForeignPro;
                                ProjectSource = dtForeignPro;//set Project in ViewState
                                ReportProperty.Current.RecordCount = dtForeignPro.Rows.Count;
                                gvProject.DataSource = null;
                                gvProject.DataSource = dtForeignPro;
                                gvProject.DataBind();
                            }
                            else
                            {
                                ProjectSelected = resultArgs.DataSource.Table;
                                ProjectSource = resultArgs.DataSource.Table; ;//set Project in ViewState
                                ReportProperty.Current.RecordCount = resultArgs.DataSource.Table.Rows.Count;
                                gvProject.DataSource = null;
                                gvProject.DataSource = resultArgs.DataSource.Table;
                                gvProject.DataBind();
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
                        foreach (string pid in ProjectIds)
                        {
                            int i = 0;
                            for (i = 0; i < ProjectSource.Rows.Count; i++)
                            {
                                if (pid.Equals(ProjectSource.Rows[i]["PROJECT_ID"].ToString()))
                                {
                                    ProjectSource.Rows[i]["SELECT"] = "1";
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
                    var Selected = (from d in dtProject.AsEnumerable()
                                    where ((d.Field<Int64>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        ProjectSelected = Selected.CopyToDataTable();
                        if (ProjectSelected != null && ProjectSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in ProjectSelected.Rows)
                            {
                                selectedProjectId += dr[0] + ",";
                                ProjectName += dr[2].ToString() + ",";
                            }
                            selectedProjectId = selectedProjectId.TrimEnd(',');
                            ProjectName = ProjectName.TrimEnd(',');

                            if (ProjectName.Trim() != string.Empty)
                            {
                                if (ReportProperty.Current.RecordCount == ProjectSelected.Rows.Count)
                                {
                                    ReportProperty.Current.ProjectTitle = SCONSTATEMENT;
                                }
                                else
                                {
                                    ReportProperty.Current.ProjectTitle = ProjectName;
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
            return selectedProjectId;
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
                                    where ((d.Field<Int64?>(SELECT) == 1))
                                    select d);
                    var UnSelectedBank = (from d in dtBank.AsEnumerable()
                                          where ((d.Field<Int64?>(SELECT) != 1))
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
                    ReportProperty.Current.UnSelectedBankAccountId = UnSelectedAccountId;
                    ReportProperty.Current.Ledger = SelectedLedgerId;
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
                                    where ((d.Field<Int64?>(SELECT) == 1))
                                    select d);
                    var UnSelected = (from d in dtLedger.AsEnumerable()
                                      where ((d.Field<Int64?>(SELECT) != 1))
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
                    ReportProperty.Current.UnSelectedLedgerId = UnSelectedLedgerId;
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
                                    where ((d.Field<Int64>(SELECT) == 1))
                                    select d);
                    if (Selected.Count() > 0)
                    {
                        LedgerGroupSelected = Selected.CopyToDataTable();
                        if (LedgerGroupSelected != null && LedgerGroupSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in LedgerGroupSelected.Rows)
                            {
                                selectedLedgerGroupId += dr[0] + ",";
                                LedgerGroupName += dr[2].ToString() + ",";
                            }
                            selectedLedgerGroupId = selectedLedgerGroupId.TrimEnd(',');
                            LedgerGroupName = LedgerGroupName.TrimEnd(',');

                            if (LedgerGroupName.Trim() != string.Empty)
                            {
                                if (ReportProperty.Current.RecordCount == LedgerGroupSelected.Rows.Count)
                                {
                                    ReportProperty.Current.ProjectTitle = SCONSTATEMENT;
                                }
                                else
                                {
                                    ReportProperty.Current.ProjectTitle = LedgerGroupName;
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
            try
            {
                if (gvCostCentre.DataSource == null)
                {
                    if (!string.IsNullOrEmpty(CostCentreId) && CostCentreSource != null)
                    {
                        ArrayList costcentreids = new ArrayList(CostCentreId.Split(new char[] { ',' }));
                        foreach (string costcentreid in costcentreids)
                        {
                            for (int i = 0; i < CostCentreSource.Rows.Count; i++)
                            {
                                if (costcentreid.Equals(CostCentreSource.Rows[i]["COST_CENTRE_ID"].ToString()))
                                {
                                    CostCentreSource.Rows[i]["SELECT"] = "1";
                                    i = CostCentreSource.Rows.Count;
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
                    var Selected = (from d in dtCostCentre.AsEnumerable()
                                    where ((d.Field<Int64?>(SELECT) == 1))
                                    select d);

                    if (Selected.Count() > 0)
                    {
                        CostCentreSelected = Selected.CopyToDataTable();
                        if (CostCentreSelected != null && CostCentreSelected.Rows.Count != 0)
                        {
                            foreach (DataRow dr in CostCentreSelected.Rows)
                            {
                                SelectedCostCentre += dr[0] + ",";
                            }
                            SelectedCostCentre = SelectedCostCentre.TrimEnd(',');
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

        #endregion

        #region Commented


        protected void TabDateProperties()
        {
            dtFrom.DateValue = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtTo.DateValue = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            ReportDate.DateValue = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
        }

        #endregion

    }

}