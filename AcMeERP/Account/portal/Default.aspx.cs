/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 21st May 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          : This page is to login to the portal as different users of Head office Admin/Users , Branch office Admin/Users and portal admin and portal users
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Setting;

namespace AcMeERP.Account.Portal
{
    public partial class Default : Base.UIBase
    {
        private bool isLogoutValue = false;

        string PostedHeadofficeCode = string.Empty;
        string UserName = string.Empty;
        string Password = string.Empty;
        public const string hocode = "hocode";
        public const string username = "username";
        public const string password = "password";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResultArgs resultArgs = null;
                SetLogout();
                Uri uri = new Uri(Request.Url.ToString());
                string rawUrl = Request.RawUrl.ToString();
                string segment = Request.Url.Segments[Request.Url.Segments.Count() - 1].ToString();

                resultArgs = this.IsDatabaseServerConnected();
                if (resultArgs != null && resultArgs.Success)
                {
                    GlobalSetting objGlobal = new GlobalSetting();
                    objGlobal.ApplySetting();

                    string[] PostDataValu = Request.Form.AllKeys;
                    if (PostDataValu.Length == 0 && Session["LoginMode"] == null || Session["LoginMode"] == "0")
                    {
                        if (rawUrl.ToLower().Contains(URLPages.Portal.ToString().ToLower()) && segment.ToLower().Contains(URLPages.Portal.ToString().ToLower()))
                        {
                            this.PageTitle = "Login ( " + URLPages.Portal.ToString().ToUpper() + " )";
                        }
                        else if (!string.IsNullOrEmpty(rawUrl))
                        {
                            string OfficeCode = segment.Trim(Convert.ToChar(Delimiter.forwardSlash.ToString()));
                            if (IsHeadOfficeCodeAvailable(OfficeCode))
                            {
                                this.PageTitle = "Login ( " + OfficeCode.ToUpper() + " )";
                                this.LoginUser.LoginUserHeadOfficeCode = OfficeCode.ToLower();
                                base.HeadOfficeCode = this.LoginUser.HeadOfficeCode;//Sets the Head office code as database connection from the url
                                //To set head office name  in the header
                                HeadOfficeNameFromCode();
                            }
                            else
                            {
                                // Remove commandline and url localhost and update it

                                string navUrl = this.GetPageUrlByName(URLPages.Default.ToString());
                                Response.Redirect(navUrl, true);

                                // Local Test
                                //   Response.Redirect("http://localhost/portal/login.html");


                            }
                        }
                    }
                    else
                    {
                        string[] PostDataValues = Request.Form.AllKeys;


                        string HeadofficeCode = string.Empty;
                        string UserName = string.Empty;
                        string Password = string.Empty;

                        if (PostDataValues.Length != 0)
                        {
                            HeadofficeCode = Request.Form[hocode].ToString();
                            UserName = Request.Form[username].ToString();
                            Password = Request.Form[password].ToString();
                        }
                        if (!(string.IsNullOrEmpty(HeadofficeCode) &&
                            string.IsNullOrEmpty(UserName) &&
                            string.IsNullOrEmpty(Password)))
                        {
                            if (IsHeadOfficeCodeAvailable(HeadofficeCode))
                            {
                                this.LoginUser.LoginUserHeadOfficeCode = HeadofficeCode.ToLower();
                                base.HeadOfficeCode = HeadofficeCode.ToLower();
                                Session["LoginMode"] = "1";

                                resultArgs = SignIn(UserName, Password);
                                if (!resultArgs.Success)
                                {
                                    //  Response.Redirect("https://www.acmeerp.org/login.php?msg=3", true); // Invalid User Name and Password
                                    // Response.Redirect("https://localhost/acptest/login.html?msg=3", true);
                                    //  Response.Redirect("https://staging.acmeerp.org/login.html?msg=3", true); // Invalid User Name and Password
                                    Response.Redirect("https://acmeerp.org/login.html?msg=3", true); // Invalid User Name and Password
                                }
                                else
                                {
                                    string retUrl = URLPages.Default.ToString(NumberFormatInfo.Number);
                                    //if (this.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword)
                                    //{
                                    //    string navUrl = this.GetPageUrlByName(URLPages.ChangePassword.ToString());
                                    //    Response.Redirect(navUrl, false);
                                    //}
                                    //else
                                    //{
                                    if (!base.LoginUser.IsPortalUser)
                                    {
                                        ApplyAccPeriod();
                                    }

                                    //Assin default ledger ids for report and process
                                    //base.AssignLedgerIDs();

                                    //Get Head Office Global Settings----------------------------
                                    base.AssignSetting();
                                    //-----------------------------------------------------------

                                    string navUrl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
                                    Response.Redirect(navUrl, true);
                                    // }
                                }
                            }
                            else if (HeadofficeCode == "portal")
                            {
                                Session["LoginMode"] = "1";
                                resultArgs = SignIn(UserName, Password);
                                if (!resultArgs.Success)
                                {
                                    //Response.Redirect("https://www.acmeerp.org/login.php?msg=3", true); // Invalid User Name and Password
                                    // Response.Redirect("https://localhost/acptest/login.html?msg=3", true);
                                    // Response.Redirect("https://staging.acmeerp.org/login.html?msg=3", true);
                                    Response.Redirect("https://acmeerp.org/login.html?msg=3", true);
                                }
                                else
                                {
                                    string retUrl = URLPages.Default.ToString(NumberFormatInfo.Number);
                                    //if (this.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword)
                                    //{
                                    //    string navUrl = this.GetPageUrlByName(URLPages.ChangePassword.ToString());
                                    //    Response.Redirect(navUrl, false);
                                    //}
                                    //else
                                    //{
                                    string navUrl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
                                    Response.Redirect(navUrl, true);
                                    // }
                                }
                            }
                            else
                            {
                                // Response.Redirect("https://localhost/acptest/login.html?msg=3", true);// Invalid User Name and Password
                                //  Response.Redirect("https://www.acmeerp.org/login.php?msg=3", true);
                                // Response.Redirect("https://staging.acmeerp.org/login.html?msg=3", true);
                                Response.Redirect("https://acmeerp.org/login.html?msg=3", true);
                            }
                        }
                        //else if (Session["LoginMode"] != "0")
                        //{
                        //    Response.Redirect("https://www.acmeerp.org/login.php?msg=2", true);   //Log out
                        //}
                    }

                    if (IsLogout)
                    {
                        bool logOut = false;
                        this.LoginUser.UserInfo = null;
                        if (Session["LoginMode"] != null && Session["LoginMode"] != "0")
                        {
                            logOut = true;
                        }

                        Session.Remove("LoginMode");
                        Session.Remove("ReportProperty");
                        Session.Remove("ProjectId");
                        Session.Remove("LedgerGroupId");
                        Session.Remove("BankId");
                        Session.Remove("BranchCode");
                        Session.Remove("SocietyCode");
                        Session.Remove("LedgerId");
                        Session.Remove("RPTCACHE");
                        Session.Remove("YEAR_FROM"); //Financial Year From
                        Session.Remove("YEAR_TO"); //Financial Year To
                        ClearReportSession();
                        if (logOut)
                            // Response.Redirect("https://www.acmeerp.org/login.php?msg=2", true);
                            // Response.Redirect("https://localhost/acptest/login.html?msg=2", true);
                            // Response.Redirect("https://staging.acmeerp.org/login.html?msg=2", true);
                            Response.Redirect("https://acmeerp.org/login.html?msg=2", true);
                        else
                            //Response.Redirect("https://www.acmeerp.org/login.php", true);
                            // Response.Redirect("https://staging.acmeerp.org/login.html", true);
                            Response.Redirect("https://acmeerp.org/login.html", true);
                        //Response.Redirect("https://localhost/acptest/login.html", true);
                    }

                    if (Request.QueryString[QueryStringMemeber.Message] != null)
                    {
                        this.Message = Request.QueryString[QueryStringMemeber.Message].ToString();
                    }

                    this.SetControlFocus(txtUserName);
                    this.FocusControlIdFromMessagePopUp = txtUserName.ClientID;
                    // this.ShowLoadWaitPopUp();

                    //to Hide Login link in default page
                    Master.FindControl("divinfo").Visible = false;
                    Master.FindControl("divspace").Visible = true;
                }
                else
                {
                    //If DB server is not connected, show message
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Server is under the maintenance. sorry for the inconvenience.')", true);
                    //  Response.Redirect("https://www.acmeerp.org/login.php?msg=4", true);
                    // Response.Redirect("https://acmeerp.org/login.html?msg=4", true);
                    Response.Redirect("https://acmeerp.org/login.html?msg=4", true);
                }
            }
        }

        private void SetLogout()
        {
            this.LoginUser.UserInfo = null;
            this.LoginUser.LoginUserHeadOfficeCode = "";
            base.HeadOfficeCode = "";//HeadOffice
            this.SetMenuProvider();
            isLogoutValue = true;//Logout by User (Session)
        }
        /// <summary>
        /// This method fetches active financial year 
        /// </summary>

        private void ApplyAccPeriod()
        {
            try
            {
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    ResultArgs resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                    if (resultArgs.Success)
                    {
                        Bosco.Utility.ConfigSetting.SettingProperty.dvAccPeriod = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        /// <summary>
        /// This method is to login to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            using (UserSystem userSystem = new UserSystem())
            {
                ResultArgs resultArgs = userSystem.AuthenticateUser(txtUserName.Text.Trim(), CommonMember.EncryptValue(txtPassword.Text.Trim()),
                     base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                if (resultArgs == null || !resultArgs.Success)
                {
                    this.Message = (resultArgs == null ? "Server is under the maintenance. sorry for the inconvenience." : resultArgs.Message);
                    SetControlFocus(txtUserName);
                }
                else
                {
                    string retUrl = URLPages.Default.ToString(NumberFormatInfo.Number);
                    if (this.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword)
                    {
                        string navUrl = this.GetPageUrlByName(URLPages.ChangePassword.ToString());
                        Response.Redirect(navUrl, false);
                    }
                    else
                    {
                        if (!base.LoginUser.IsPortalUser)
                        {
                            ApplyAccPeriod();
                        }
                        string navUrl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
                        Response.Redirect(navUrl, true);
                    }

                }
            }
        }

        private ResultArgs SignIn(string userName = "", string password = "")
        {
            ResultArgs resultArgs = new ResultArgs();
            using (UserSystem userSystem = new UserSystem())
            {
                resultArgs = userSystem.AuthenticateUser(userName, CommonMember.EncryptValue(password),
                    base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
            }
            return resultArgs;
        }

        /// <summary>
        /// Method to set header dynamically
        /// </summary>
        private void HeadOfficeNameFromCode()
        {
            ResultArgs resultArgs;
            using (HeadOfficeSystem officesystem = new HeadOfficeSystem())
            {
                resultArgs = officesystem.FetchLoginUserHeadOfficeDetails(this.LoginUser.LoginUserHeadOfficeCode);
            }
            if (resultArgs.Success)
            {
                DataTable dtoffice = resultArgs.DataSource.Table;
                if (dtoffice != null)
                {
                    if (dtoffice.Rows.Count == 1)
                    {
                        this.LoginUser.LoginUserHeadOfficeName = dtoffice.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName].ToString();
                        HttpContext.Current.Session["HeadOfficeInformation"] = dtoffice;
                        if (HttpContext.Current.Session["HeadOfficeInformation"] == null)
                        {
                            new ErrorLog().WriteError("HeadOfficeInformation is empty for reportbase purpose");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This method checks whether Head Office Code is available.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <returns></returns>
        private bool IsHeadOfficeCodeAvailable(string headOfficeCode)
        {
            ResultArgs resultArgs = null;
            using (HeadOfficeSystem officesystem = new HeadOfficeSystem())
            {
                resultArgs = officesystem.HeadOfficeDetailsByCode(headOfficeCode);
            }
            if (resultArgs != null && resultArgs.Success)
            {
                DataTable dtoffice = resultArgs.DataSource.Table;
                if (dtoffice != null)
                {
                    if (dtoffice.Rows.Count == 1)
                    {
                        this.LoginUser.LoginUserHeadOfficeName = dtoffice.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName].ToString();
                        resultArgs.Success = true;
                    }
                    else
                    {
                        resultArgs.Success = false;
                    }
                }

            }
            return resultArgs.Success;
        }
        private bool IsLogout
        {
            get
            {
                return isLogoutValue;
            }
            set
            {
                isLogoutValue = value;
            }
        }
        /// <summary>
        /// This method clears the session value when user logsout of the system.
        /// </summary>
        private void ClearReportSession()
        {
            Session.Remove("reportId");
            Session.Remove("dvReportSettingInfo");
            Session.Remove("ReportGroup");
            Session.Remove("ReportName");
            Session.Remove("ReportTitle");
            Session.Remove("ReportDescription");
            Session.Remove("ReportAssembly");
            Session.Remove("AccounYear");
            Session.Remove("DateFrom");
            Session.Remove("DateTo");
            Session.Remove("DateAsOn");
            Session.Remove("IncludeAllLedger");
            Session.Remove("ShowByLedger");
            Session.Remove("ShowByLedgerGroup");
            Session.Remove("ShowDailyBalance");
            Session.Remove("ShowByCostCentre");
            Session.Remove("BreakUpByCostCentre");
            Session.Remove("IncludeJournal");
            Session.Remove("IncludeInKind");
            Session.Remove("IncludeLedgerGroupTotal");
            Session.Remove("IncludeLedgerGroup");
            Session.Remove("IncludeCostCentre");
            Session.Remove("ShowMonthTotal");
            Session.Remove("ShowDonorAddress");
            Session.Remove("ShowDonorCategory");
            Session.Remove("IncludeBankAccount");
            Session.Remove("IncludeBankDetails");
            Session.Remove("ShowDetailedBalance");
            Session.Remove("Project");
            Session.Remove("ProjectId");
            Session.Remove("BankAccount");
            Session.Remove("InstituteName");
            Session.Remove("SocietyName");
            Session.Remove("LegalAddress");
            Session.Remove("LedgalEntityId");
            Session.Remove("ShowTitleSocietyName");
            Session.Remove("FDAccountID");
            Session.Remove("FDAccount");
            Session.Remove("BudgetId");
            Session.Remove("Budget");
            Session.Remove("BankAccountId");
            Session.Remove("Ledger");
            Session.Remove("LedgerGroup	");
            Session.Remove("CostCentre");
            Session.Remove("Narration");
            Session.Remove("ReportCriteria");
            Session.Remove("ProjectTitle");
            Session.Remove("CostCentreName");
            Session.Remove("VoucherType");
            Session.Remove("TitleAlignment");
            Session.Remove("Count");
            Session.Remove("BankAccountName");
            Session.Remove("ReportDate");
            Session.Remove("ShowLedgerCode");
            Session.Remove("ShowGroupCode");
            Session.Remove("SortByLedger");
            Session.Remove("SortByGroup");
            Session.Remove("IncludeNarration");
            Session.Remove("ShowHorizontalLine");
            Session.Remove("ShowVerticalLine");
            Session.Remove("ShowTitles");
            Session.Remove("ShowLogo");
            Session.Remove("ShowPageNumber");
            Session.Remove("ShowPrintDate");
            Session.Remove("SetWithForCode");
            Session.Remove("RecordCount");
            Session.Remove("HeaderInstituteSocietyName");
            Session.Remove("SelectedProjectCount");
            Session.Remove("CashBankVoucher");
            Session.Remove("BranchOffice");
            Session.Remove("Society");
            Session.Remove("dtLedgerEntity");
            Session.Remove("ShowProjectsinFooter");
            Session.Remove("DrillDownProperties");
            Session.Remove("ProjectSource");
            Session.Remove("BankSource");
            Session.Remove("LedgerSource");
            Session.Remove("LedgerGroupSource");
            Session.Remove("BranchSource");
            Session.Remove("LedgerSource");
            Session.Remove("SocietySource");
            Session.Remove("CostCentreSource");
            Session.Remove("CostCentreId");
            Session.Remove("DillDownLinks");
            Session.Remove("FDRegisterStatus");
            Session.Remove(base.DefaultBranchId);
            Session.Remove("ProjectCategoryId");
            Session.Remove("ProjectCategory");
            Session.Remove("ProjectCategorySource");
            Session.Remove("REPORTPROPERTY");
            Session.Remove("EnableDrillDown");
        }
    }
}
