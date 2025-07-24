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
 * Purpose          :This page helps the portal admin to generate the license key details for each active branch office whenever needed for each financial year
 *****************************************************************************************************/
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Configuration;
using System.Data;

namespace AcMeERP.Module.Office
{
    public partial class GenerateLicenseKey : Base.UIBase
    {
        #region Property

        CommonMember UtilityMember = new CommonMember();
        private const string ID = "Id";
        private const string NAME = "Name";
        private const string DESCRIPTION = "Description";
        private static object objLock = new object();
        private int BranchOfficeId
        {
            get
            {
                int branchOfficeId = this.Member.NumberSet.ToInteger(this.RowId);
                return branchOfficeId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }
        private string BranchKeyCode
        {
            get
            {
                string branchKeyCode = string.Empty;

                branchKeyCode = ViewState["BranchKeyCode"] != null ? ViewState["BranchKeyCode"].ToString() : string.Empty;
                return branchKeyCode;

            }
            set
            {
                ViewState["BranchKeyCode"] = value;
            }
        }

        private int LicenseId
        {
            get
            {
                int LicenseId = 0;

                LicenseId = ViewState["LicenseId"] != null ? this.Member.NumberSet.ToInteger(ViewState["LicenseId"].ToString()) : 0;
                return LicenseId;

            }
            set
            {
                ViewState["LicenseId"] = value;
            }
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                divParish.Visible = false;
                LoadParish();
                SetPageTitle();
                SetControlFocus();
                bindModuleItems();
                BindLinceseReports();
                hlkClose.PostBackUrl = this.ReturnUrl;
                this.CheckUserRights(RightsModule.Office, RightsActivity.CreateLicenseKey, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                DateTime dtYFrom = new DateTime(this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Year, 4, 1);
                DateTime dtYTo = dtYFrom.AddYears(1).AddDays(-1);
                if (this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false) < dtYTo)
                {
                    if (this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Month <= 3)
                    {
                        dtYearFrom.Date = dtYFrom.AddYears(-1);
                    }
                    else
                    {
                        dtYearFrom.Date = dtYFrom;
                    }
                }
                else
                {
                    dtYearFrom.Date = dtYFrom;
                }

                dtYearFrom.Enabled = false;
                dtYearTo.Date = dtYTo;
                if (BranchOfficeId > 0)
                {
                    AssignValuesToControls();
                }
                this.ShowLoadWaitPopUp(btnSaveLicense);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void btnSaveLicense_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Page.IsValid)
                //{
                lock (objLock)
                {
                    ResultArgs resultArgs = null;

                    using (LicenseSystem licenseSystem = new LicenseSystem())
                    {
                        licenseSystem.BRANCH_ID = BranchOfficeId;
                        licenseSystem.BRANCH_KEY_CODE = BranchKeyCode;
                        licenseSystem.LICENSE_KEY_NUMBER = licenseSystem.GetNewNumber(NumberFormats.LicenseIdentificationNumber, "", BranchKeyCode);
                        licenseSystem.LICENSE_QUANTITY = string.IsNullOrEmpty(txtNoofCode.Text.Trim()) ? 0 : this.Member.NumberSet.ToInteger(txtNoofCode.Text.Trim());
                        licenseSystem.LICENSE_COST = string.IsNullOrEmpty(txtLicenseCost.Text.Trim()) ? 0 : this.Member.NumberSet.ToDouble(txtLicenseCost.Text.Trim());
                        licenseSystem.IS_LICENSE_MODEL = chkLicenseModel.Checked ? 1 : 0;
                        licenseSystem.IS_MULTILOCATION = chkMultilocation.Checked ? 1 : 0;
                        licenseSystem.ENABLE_PORTAL = chkPortal.Checked ? 1 : 0;
                        licenseSystem.LOCK_MASTER = chkLockMaster.Checked ? 1 : 0;
                        licenseSystem.MAP_LEDGER = chkMapLedger.Checked ? 1 : 0;
                        licenseSystem.ALLOW_MULTI_CURRENCY = chkAllowMultiCurrency.Checked ? 1 : 0;
                        licenseSystem.ATTACH_VOUCHERS_FILES = chkAttachVouchersFiles.Checked ? 1 : 0;
                        licenseSystem.MODULE_ITEM = GetSelectedModule();
                        licenseSystem.LICENSE_REPORT_ITEM = GetSelectedlicReports();
                        licenseSystem.Parish_Code = divParish.Visible == true ? ddlParish.SelectedValue.Trim() : string.Empty;
                        licenseSystem.INSTITUTE_NAME = txtInstituteName.Text.Trim();
                        licenseSystem.THIRDPARTY_MODE = txtthirdpartymode.Text.Trim();
                        licenseSystem.THIRDPARTY_URL = txtthirdpartyurl.Text.Trim();
                        licenseSystem.YEAR_FROM = dtYearFrom.Date;
                        licenseSystem.YEAR_TO = dtYearTo.Date;
                        licenseSystem.LOGIN_URL = txtURL.Text.Trim();
                        licenseSystem.ACCESS_MULTI_DB = chkAccessToMultiDB.Checked ? 1 : 0;

                        licenseSystem.APPROVE_BUDUGET_PORTAL = chkBudgetApprovalbyPortal.Checked ? 1 : 0;
                        licenseSystem.APPROVE_BUDUGET_EXCEL = chkBudgetApprovalbyExcel.Checked ? 1 : 0;

                        //For Temp
                        //if (licenseSystem.APPROVE_BUDUGET_PORTAL == 1 && licenseSystem.APPROVE_BUDUGET_EXCEL == 1)
                        //{
                        //    licenseSystem.APPROVE_BUDUGET_PORTAL = 1;
                        //    licenseSystem.APPROVE_BUDUGET_EXCEL = 0;
                        //}

                        licenseSystem.IS_TWO_MONTH_BUDGET = chkBudgetTwoMonth.Checked ? 1 : 0;
                        licenseSystem.AUTOMATIC_BACKUP_PORTAL = chkAutomaticBackup.Checked ? 1 : 0;
                        resultArgs = licenseSystem.SaveLicenseDetails();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            LicenseId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            this.Message = MessageCatalog.Message.LicenseGenerated;
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally
            {
            }
        }

        protected void chkModuleItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in chkModuleItems.Items)
            {
                if (item.Selected)
                {
                    string i = item.Value;
                    if (i == "7")
                        divParish.Visible = true;
                    else
                        divParish.Visible = false;
                }
            }
        }

        #endregion

        #region Methods

        private DateTime CalculateYearTo()
        {
            DateTime accDateTo = new DateTime(dtYearFrom.Date.Year, 4, 1);
            return accDateTo.Date.AddYears(1).AddDays(-1);
        }

        private void SetPageTitle()
        {
            this.PageTitle = MessageCatalog.Message.GenerateLicenseKey.GenerateLicenseKeyPageTitle;
        }

        private void SetControlFocus()
        {
            SetControlFocus(txtBOName);
        }

        private void AssignValuesToControls()
        {
            try
            {
                using (LicenseSystem licenseSystem = new LicenseSystem())
                {
                    ResultArgs resultArgs = licenseSystem.GetBranchOfficeLicenseDetails(BranchOfficeId);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        DataTable dtLicenseInfo = resultArgs.DataSource.Table;
                        BranchKeyCode = dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.BRANCH_KEY_CODEColumn.ColumnName].ToString();
                        txtBOfficeCode.Text = dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                        txtHeadOfficeCode.Text = dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                        txtBOName.Text = dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString();
                        txtInstituteName.Text = dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.InstituteNameColumn.ColumnName].ToString();
                        txtThirdParty.Text = dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.THIRDPARTY_CODEColumn.ColumnName].ToString();
                        txtthirdpartymode.Text = dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.THIRDPARTY_MODEColumn.ColumnName].ToString();
                        txtthirdpartyurl.Text = dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.THIRDPARTY_URLColumn.ColumnName].ToString();
                        txtNoofCode.Text = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                        [this.AppSchema.Branch_License.LICENSE_QUANTITYColumn.ColumnName].ToString()) > 0 ?
                        dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LICENSE_QUANTITYColumn.ColumnName].ToString() : string.Empty;
                        txtLicenseCost.Text = this.Member.NumberSet.ToDouble(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LICENSE_COSTColumn.ColumnName].ToString()) > 0 ? dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LICENSE_COSTColumn.ColumnName].ToString() : string.Empty;
                        chkMultilocation.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                            [this.AppSchema.Branch_License.IS_MULTILOCATIONColumn.ColumnName].ToString()) > 0 ? true : false;
                        chkLicenseModel.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                            [this.AppSchema.Branch_License.IS_LICENSE_MODELColumn.ColumnName].ToString()) > 0 ? true : false;
                        chkLockMaster.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                            [this.AppSchema.Branch_License.LOCK_MASTERColumn.ColumnName].ToString()) > 0 ? true : false;
                        chkMapLedger.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                            [this.AppSchema.Branch_License.MAP_LEDGERColumn.ColumnName].ToString()) > 0 ? true : false;

                        chkAllowMultiCurrency.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                          [this.AppSchema.Branch_License.ALLOW_MULTI_CURRENCYColumn.ColumnName].ToString()) > 0 ? true : false;

                        chkAttachVouchersFiles.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                          [this.AppSchema.Branch_License.ATTACH_VOUCHERS_FILESColumn.ColumnName].ToString()) > 0 ? true : false;

                        chkPortal.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                            [this.AppSchema.Branch_License.ENABLE_PORTALColumn.ColumnName].ToString()) > 0 ? true : false;
                        txtURL.Text = dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LOGIN_URLColumn.ColumnName].ToString();
                        chkAccessToMultiDB.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                            [this.AppSchema.Branch_License.AccessToMultiDBColumn.ColumnName].ToString()) > 0 ? true : false;
                        chkBudgetApprovalbyPortal.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                           [this.AppSchema.Branch_License.APPROVE_BUDGET_BY_PORTALColumn.ColumnName].ToString()) > 0 ? true : false;
                        chkBudgetApprovalbyExcel.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                           [this.AppSchema.Branch_License.APPROVE_BUDGET_BY_EXCELColumn.ColumnName].ToString()) > 0 ? true : false;
                        chkBudgetTwoMonth.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                           [this.AppSchema.Branch_License.IS_TWO_MONTH_BUDGETColumn.ColumnName].ToString()) > 0 ? true : false;
                        chkAutomaticBackup.Checked = this.Member.NumberSet.ToInteger(dtLicenseInfo.Rows[0]
                         [this.AppSchema.Branch_License.AUTOMATIC_BACKUP_PORTALColumn.ColumnName].ToString()) > 0 ? true : false;
                        SetSelectedModuleItem(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.MODULE_ITEMColumn.ColumnName].ToString());
                        SetSelectedLicenseReportItem(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.ENABLE_REPORTSColumn.ColumnName].ToString());
                        ddlParish.SelectedValue = dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.CRISTO_PARISH_CODEColumn.ColumnName].ToString();

                        dtYearTo.Date = this.Member.DateSet.ToDate(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.YEAR_TOColumn.ColumnName].ToString(), false);
                        if (dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName].ToString().Equals(DeploymentType.Standalone.ToString()))
                        {
                            radbtnlstDeployment.Items[0].Selected = true;
                        }
                        else
                        {
                            radbtnlstDeployment.Items[1].Selected = true;
                        }
                    }
                    else if (resultArgs.Success && resultArgs.RowsAffected == 0)
                    {
                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem(BranchOfficeId, DataBaseType.Portal))
                        {
                            txtBOfficeCode.Text = branchOfficeSystem.BranchOfficeCode;
                            txtBOName.Text = branchOfficeSystem.BranchOfficeName;
                            txtHeadOfficeCode.Text = branchOfficeSystem.HeadOffice_Code;
                            radbtnlstDeployment.SelectedValue = branchOfficeSystem.Deployment_Type.ToString();
                            txtURL.Text = ConfigurationManager.AppSettings["AcMESite"] + branchOfficeSystem.HeadOffice_Code;
                            BranchKeyCode = branchOfficeSystem.BranchKeyCode;
                        }
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                    using (BranchOfficeSystem branchSystem = new BranchOfficeSystem())
                    {
                        branchSystem.BranchOfficeCode = txtBOfficeCode.Text.Trim();
                        if (branchSystem.IsSubBranchInfo())
                        {
                            divPortal.Visible = false;
                            chkMultilocation.Visible = chkPortal.Visible = false;
                            chkMapLedger.Visible = chkLockMaster.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

        }

        private void bindModuleItems()
        {
            DataTable dtModuleList = GetModuleDataSource();

            if (dtModuleList != null)
            {
                this.Member.ComboSet.FillDataCheckBoxList(chkModuleItems, dtModuleList, DESCRIPTION, ID);
            }
        }

        private void BindLinceseReports()
        {
            DataTable dtLicenseReportList = GetLicenseReportDataSource();

            if (dtLicenseReportList != null)
            {
                this.Member.ComboSet.FillDataCheckBoxList(chkLicenseReport, dtLicenseReportList, DESCRIPTION, ID);
            }
        }

        private void SetSelectedModuleItem(string moduleItems)
        {
            string[] splitModule = moduleItems.Split(Delimiter.Comma.ToCharArray());
            foreach (string module in splitModule)
            {
                ListItem li = chkModuleItems.Items.FindByText(GetModuleDescriptionByName(module));
                if (li != null)
                {
                    li.Selected = true;
                    if (li.Text == "Cristo")
                    {
                        divParish.Visible = true;
                    }
                    else
                        divParish.Visible = false;
                }
            }
        }

        private void SetSelectedLicenseReportItem(string LicReport)
        {
            string[] splitLicReports = LicReport.Split(Delimiter.AtCap.ToCharArray());
            foreach (string licReports in splitLicReports)
            {
                ListItem li = chkLicenseReport.Items.FindByText(GetLicReportDescriptionByName(licReports));
                if (li != null)
                {
                    li.Selected = true;
                }
            }
        }

        private void UnSelectModuleItem()
        {
            foreach (ListItem li in chkModuleItems.Items)
            {
                li.Selected = false;
            }
        }

        private string GetSelectedModule()
        {
            string moduleItem = "";

            foreach (ListItem li in chkModuleItems.Items)
            {
                if (li.Selected)
                {
                    moduleItem += ((moduleItem != "") ? Delimiter.Comma : "") + GetModuleNameByDescription(li.Text);
                }
            }
            return moduleItem;
        }

        private string GetSelectedlicReports()
        {
            string licReports = "";

            foreach (ListItem li in chkLicenseReport.Items)
            {
                if (li.Selected)
                {
                    licReports += ((licReports != "") ? Delimiter.AtCap : "") + GetLicenseReportNameByDescription(li.Text);
                }
            }
            return licReports;
        }

        private DataTable GetModuleDataSource()
        {
            DataTable dtModuleList = new DataTable("ModuleList");
            dtModuleList.Columns.Add(ID, typeof(int));
            dtModuleList.Columns.Add(NAME, typeof(string));
            dtModuleList.Columns.Add(DESCRIPTION, typeof(string));

            ModuleList moduleList = new ModuleList();
            DataView dvModuleDescriptionList = this.Member.EnumSet.GetEnumDataSourceDescriptionfromEnumType(moduleList, Sorting.None);
            DataView dvModuleNameList = this.Member.EnumSet.GetEnumDataSource(moduleList, Sorting.None);
            if (dvModuleNameList != null && dvModuleDescriptionList != null)
            {
                for (int i = 0; i < dvModuleNameList.Table.Rows.Count; i++)
                {
                    DataRow drRow = dtModuleList.NewRow();
                    drRow[ID] = this.Member.NumberSet.ToInteger(dvModuleDescriptionList.Table.Rows[i][ID].ToString());
                    drRow[DESCRIPTION] = dvModuleDescriptionList.Table.Rows[i][NAME].ToString();
                    drRow[NAME] = dvModuleNameList.Table.Rows[i][NAME].ToString();
                    dtModuleList.Rows.Add(drRow);
                    dtModuleList.AcceptChanges();
                }
            }
            return dtModuleList;
        }

        private DataTable GetLicenseReportDataSource()
        {
            DataTable dtLicenseReport = new DataTable("LicenseReports");
            dtLicenseReport.Columns.Add(ID, typeof(int));
            dtLicenseReport.Columns.Add(NAME, typeof(string));
            dtLicenseReport.Columns.Add(DESCRIPTION, typeof(string));

            LincenseReportList LicReports = new LincenseReportList();
            DataView dvModuleDescriptionList = this.Member.EnumSet.GetEnumDataSourceDescriptionfromEnumType(LicReports, Sorting.None);
            DataView dvModuleNameList = this.Member.EnumSet.GetEnumDataSource(LicReports, Sorting.None);
            if (dvModuleNameList != null && dvModuleDescriptionList != null)
            {
                for (int i = 0; i < dvModuleNameList.Table.Rows.Count; i++)
                {
                    DataRow drRow = dtLicenseReport.NewRow();
                    drRow[ID] = this.Member.NumberSet.ToInteger(dvModuleDescriptionList.Table.Rows[i][ID].ToString());
                    drRow[DESCRIPTION] = dvModuleDescriptionList.Table.Rows[i][NAME].ToString();
                    drRow[NAME] = dvModuleNameList.Table.Rows[i][NAME].ToString();
                    dtLicenseReport.Rows.Add(drRow);
                    dtLicenseReport.AcceptChanges();
                }
            }
            return dtLicenseReport;
        }

        private string GetModuleNameByDescription(string description)
        {
            string moduleName = string.Empty;
            if (!string.IsNullOrEmpty(description))
            {
                DataTable dtModuleSource = GetModuleDataSource();
                for (int i = 0; i < dtModuleSource.Rows.Count; i++)
                {
                    if (dtModuleSource.Rows[i][DESCRIPTION].ToString().Equals(description.Trim()))
                    {
                        moduleName = dtModuleSource.Rows[i][NAME].ToString();
                    }
                }
            }
            return moduleName;
        }

        private string GetLicenseReportNameByDescription(string description)
        {
            string LicReport = string.Empty;
            if (!string.IsNullOrEmpty(description))
            {
                DataTable dtModuleSource = GetLicenseReportDataSource();
                for (int i = 0; i < dtModuleSource.Rows.Count; i++)
                {
                    if (dtModuleSource.Rows[i][DESCRIPTION].ToString().Equals(description.Trim()))
                    {
                        LicReport = dtModuleSource.Rows[i][NAME].ToString().Replace("_", "-");
                    }
                }
            }
            return LicReport;
        }
        private string GetModuleDescriptionByName(string name)
        {
            string moduleDesc = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                DataTable dtModuleSource = GetModuleDataSource();
                for (int i = 0; i < dtModuleSource.Rows.Count; i++)
                {
                    if (dtModuleSource.Rows[i][NAME].ToString().Equals(name.Trim()))
                    {
                        moduleDesc = dtModuleSource.Rows[i][DESCRIPTION].ToString();
                    }
                }
            }
            return moduleDesc;
        }

        private string GetLicReportDescriptionByName(string name)
        {
            string licReportdescription = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                DataTable dtLicReportSource = GetLicenseReportDataSource();
                for (int i = 0; i < dtLicReportSource.Rows.Count; i++)
                {
                    if (dtLicReportSource.Rows[i][NAME].ToString().Equals(name.Trim().Replace("-", "_")))
                    {
                        licReportdescription = dtLicReportSource.Rows[i][DESCRIPTION].ToString();
                    }
                }
            }
            return licReportdescription;
        }

        private void LoadParish()
        {
            using (HeadOfficeSystem headSystem = new HeadOfficeSystem())
            {
                ResultArgs resultArgs = new ResultArgs();
                DataTable dtParish = new DataTable();
                dtParish = ConstructParish();
                if (dtParish != null && dtParish.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindDataCombo(ddlParish, dtParish, "PARISH_NAME", "PARISH_CODE");
                    ddlParish.SelectedIndex = 0;
                    // , this.AppSchema.State.STATEColumn.ColumnName
                    // , this.AppSchema.State.STATE_IDColumn.ColumnName
                    // , true, CommonMember.SELECT);
                }
            }
        }

        private DataTable ConstructParish()
        {
            DataTable dtconstruct = new DataTable();
            dtconstruct.Columns.Add("PARISH_ID", typeof(int));
            dtconstruct.Columns.Add("PARISH_CODE", typeof(string));
            dtconstruct.Columns.Add("PARISH_NAME", typeof(string));
            dtconstruct.Rows.Add(1, "205/VEL", "Our Lady of Assumption Cathedral");
            dtconstruct.Rows.Add(2, "203/SAN", "St Thomas Cathedral (Basilica)");
            dtconstruct.Rows.Add(3, "203/VYA", "Our Lady of Consolation");

            return dtconstruct;
        }

        #endregion
    }
}