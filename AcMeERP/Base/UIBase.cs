using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using DevExpress.Web.ASPxEditors;
using Bosco.Model.Setting;
using System.Data.OleDb;
using Bosco.Model.UIModel;

namespace AcMeERP.Base
{
    public class UIBase : System.Web.UI.Page
    {
        private UserProperty userProperty = null;
        private CommonMember member = null;
        private AppSchemaSet.ApplicationSchemaSet appSchema = null;
        public string DefaultBranchId = "DefaultBranchId";

        public UIBase()
        {

        }

        public CommonMember Member
        {
            get
            {
                if (member == null) { member = new CommonMember(); }
                return member;
            }
        }
        protected override void InitializeCulture()
        {
            GlobalSetting objGlobal = new GlobalSetting();
            objGlobal.ApplySetting();
            base.InitializeCulture();
        }

        public AppSchemaSet.ApplicationSchemaSet AppSchema
        {
            get
            {
                if (appSchema == null) { appSchema = new AppSchemaSet().AppSchema; }
                return appSchema;
            }
        }

        public UserProperty LoginUser
        {
            get
            {
                if (userProperty == null) { userProperty = new UserProperty(); }
                return userProperty;
            }
        }

        private string LoginMode
        {
            get
            {
                if (Session["LoginMode"] == null)
                    return "0";
                else
                    return (string)Session["LoginMode"];
            }
            set
            {
                Session["LoginMode"] = value;
            }
        }

        public string PageTitle
        {
            get
            {
                string pageTitle = String.Empty;
                MasterBase master = this.Master as MasterBase;
                if (master != null) { pageTitle = master.PageTitle; }
                return pageTitle;
            }
            set
            {
                MasterBase master = this.Master as MasterBase;
                if (master != null) { master.PageTitle = value; }

            }
        }

        public string TimeFrom
        {
            get
            {
                string timeFrom = String.Empty;
                MasterBase master = this.Master as MasterBase;
                if (master != null) { timeFrom = master.TimeFrom; }
                return timeFrom;
            }
            set
            {
                MasterBase master = this.Master as MasterBase;
                if (master != null) { master.TimeFrom = value; }

            }
        }

        public string TimeTo
        {
            get
            {
                string timeTo = String.Empty;
                MasterBase master = this.Master as MasterBase;
                if (master != null) { timeTo = master.TimeTo; }
                return timeTo;
            }
            set
            {
                MasterBase master = this.Master as MasterBase;
                if (master != null) { master.TimeTo = value; }
            }
        }

        public bool ShowTimeFromTimeTo
        {
            get
            {
                bool timeTo = false;
                MasterBase master = this.Master as MasterBase;
                if (master != null) { timeTo = master.ShowTimeFromTimeTo; }
                return timeTo;
            }
            set
            {
                MasterBase master = this.Master as MasterBase;
                if (master != null) { master.ShowTimeFromTimeTo = value; }
            }
        }

        public string Message
        {
            get
            {
                string message = String.Empty;
                MasterBase master = this.Master as MasterBase;
                if (master != null)
                {
                    message = master.Message;
                }
                return message;
            }
            set
            {
                MasterBase master = this.Master as MasterBase;
                if (master != null) { master.Message = value; }
                ScrollTop();
            }
        }

        public string RowId
        {
            get
            {
                string row_Id = "0";

                if (ViewState[QueryStringMemeber.Id] != null &&
                    ViewState[QueryStringMemeber.Id].ToString() != String.Empty)
                {
                    row_Id = ViewState[QueryStringMemeber.Id].ToString();
                }
                return row_Id;
            }
            set { ViewState[QueryStringMemeber.Id] = value; }
        }

        public string RowId1
        {
            get
            {
                if (ViewState[QueryStringMemeber.Id1] != null &&
                    ViewState[QueryStringMemeber.Id1].ToString() != "")
                {
                    return ViewState[QueryStringMemeber.Id1].ToString();
                }
                else
                {
                    return "0";
                }
            }
            set { ViewState[QueryStringMemeber.Id1] = value; }
        }

        public string RowId2
        {
            get
            {
                if (ViewState[QueryStringMemeber.Id2] != null &&
                    ViewState[QueryStringMemeber.Id2].ToString() != "")
                {
                    return ViewState[QueryStringMemeber.Id2].ToString();
                }
                else
                {
                    return "0";
                }
            }
            set { ViewState[QueryStringMemeber.Id2] = value; }
        }

        public bool HasRowId
        {
            get { return (RowId != "0"); }
        }

        public string PageName()
        {
            string pageName = Request.AppRelativeCurrentExecutionFilePath;
            string[] aUrlSegment = this.Request.Url.Segments;

            if (aUrlSegment.Length > 0)
            {
                pageName = aUrlSegment[aUrlSegment.Length - 1].ToLower();
            }

            return pageName;
        }

        public bool HasReturnUrl
        {
            get { return (Request.QueryString[QueryStringMemeber.ReturnUrl] != null); }
        }

        public string ReturnUrl
        {
            get
            {
                string pageId = "0";
                string retUrl = String.Empty;

                if (Request.QueryString[QueryStringMemeber.ReturnUrl] != null)
                {
                    pageId = Request.QueryString[QueryStringMemeber.ReturnUrl].ToString();
                }

                retUrl = this.GetPageUrlById(pageId);
                return retUrl;
            }
        }

        //Patch for Google Chrome/Safari Browser for Menu Navigation using Meny control
        protected override void OnPreInit(EventArgs e)
        {
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
            // All webpages displaying an ASP.NET menu control must inherit this class.
            //Chrome/Safari
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Page.ClientTarget = "uplevel";
            }

            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (this.Context != null && this.Context.Session != null)
                {
                    if (Request.QueryString[QueryStringMemeber.Id] != null)
                    {
                        RowId = Request.QueryString[QueryStringMemeber.Id].ToString();
                    }
                }
            }
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                SetMenuProvider();

                try
                {
                    if (!IsPostBack)
                    {
                        this.Title = ApplicationTitle.Name;

                        if (Request.QueryString[QueryStringMemeber.Id] != null)
                        {
                            if (Request.QueryString[QueryStringMemeber.Id].ToString().Contains(","))
                            {
                                string[] ids = Request.QueryString[QueryStringMemeber.Id].ToString().Split(',');
                                RowId = ids[0];
                            }
                            else
                            {
                                RowId = Request.QueryString[QueryStringMemeber.Id].ToString();
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    this.Message = err.Message;
                }
                finally
                {
                    base.OnLoad(e);
                }
            }
        }

        public string FocusControlIdFromMessagePopUp
        {
            set
            {
                MasterBase master = this.Master as MasterBase;
                if (master != null) { master.FocusControlIdFromMessagePopUp = value; }
            }
        }

        public void ShowLoadWaitPopUp()
        {
            this.Page.Form.Attributes.Add("onsubmit", "javascript:return showProgress();");
        }

        public void ShowLoadWaitPopUp(Button btnAction)
        {
            btnAction.Attributes.Add("onclick", "javascript:return showProgress();");
        }

        public void ShowLoadWaitPopUp(ImageButton btnAction)
        {
            btnAction.Attributes.Add("onclick", "javascript:return showProgress();");
        }
        public void ShowLoadWaitPopUp(ASPxButton btnAction)
        {
            btnAction.Attributes.Add("onclick", "javascript:return showProgress();");
        }

        public void ScrollTop()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Scroll", "$.scrollTop();", true);
        }

        //to focus the control added by 
        public void SetControlFocus(Control controlName)
        {
            ScriptManager.GetCurrent(this).SetFocus(controlName);
        }

        public string GetPageUrlByName(string pageId)
        {
            string pageUrl = SettingManager.Instance.GetPageUrl(pageId);
            return pageUrl;
        }

        public string GetPageUrlById(string pageId)
        {
            int page_id = this.Member.NumberSet.ToInteger(pageId);
            string pageName = this.Member.EnumSet.GetEnumItemNameByValue(typeof(URLPages), page_id);

            string pageUrl = SettingManager.Instance.GetPageUrl(pageName);
            return pageUrl;
        }

        public void SetMenuProvider()
        {
            SiteMenuProvider menuprovider = SiteMenuProvider.HomeMenuProvider;
            UserType userType = UserType.Portal;

            if (this.LoginUser.IsHeadOfficeUser)
            {
                userType = UserType.HeadOffice;
            }
            else if (this.LoginUser.IsBranchOfficeUser)
            {
                userType = UserType.BranchOffice;
            }

            //UserType userType = this.LoginUser.IsHeadOfficeUser ? UserType.HeadOffice : UserType.Portal; //LoginUser.UserType;

            if (LoginUser.HasLoginUser)
            {
                if (userType == UserType.Portal)
                {
                    menuprovider = SiteMenuProvider.SiteAdminMenuProvider;
                }
                else if (userType == UserType.HeadOffice)
                {
                    menuprovider = SiteMenuProvider.HeadOfficeMenuProvider;
                }
                else if (userType == UserType.BranchOffice)
                {
                    menuprovider = SiteMenuProvider.BranchOfficeMenuProvider;
                }
            }

            MasterBase master = this.Master as MasterBase;
            if (master != null) { master.SiteMenuProvider = menuprovider.ToString(); }
        }

        public string HeadOfficeCode
        {
            get
            {
                return this.LoginUser.HeadOfficeCode;
            }

            set
            {
                this.LoginUser.HeadOfficeCode = value;

                if (String.IsNullOrEmpty(value))
                {
                    this.LoginUser.HeadOfficeDBConnection = "";
                }
                else
                {
                    this.LoginUser.HeadOfficeDBConnection = "";

                    using (Bosco.Model.SettingSystem settingSystem = new Bosco.Model.SettingSystem())
                    {
                        string hoConnectionString = settingSystem.GetHeadOfficeDBConnection(value);
                        // command by chinna 25.01.2018
                        //hoConnectionString = "server=localhost;port=3320;database=diomys;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=localhost;port=3320;database=sdbinm;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=localhost;port=3320;database=sdbinmlive;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=localhost;port=3320;database=diomys;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=192.168.1.172;port=3306;database=SDBINM;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=192.168.1.172;port=3306;database=SDBTLS;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=192.168.1.172;port=3306;database=sdbinm;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=192.168.1.172;port=3306;database=cmfche;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=localhost;port=3320;database=sdbinm;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = hoConnectionString.Replace("localhost", "192.168.1.172");

                        //hoConnectionString = "server=192.168.1.172;port=3306;database=boscos;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=192.168.1.172;port=3306;database=sdbinm;uid=root;pwd=acperproot;pooling=false";
                        //hoConnectionString = "server=localhost;port=3320;database=sdbtls;uid=root;pwd=acperproot;pooling=false";
                        this.LoginUser.HeadOfficeDBConnection = hoConnectionString;
                    }
                }
            }
        }

        /// <summary>
        /// Check Userrights for given ModuleCode and ActivityCode for logged user
        /// It will verify based on type of Activity(PAGE or TASK).
        /// If Activity Type is PAGE and not valid rights, it will redirect to error page
        /// If Activity Type is TASK, it will return based on the rights
        /// </summary>
        /// <param name="ModuleCode">String:Module Code of the activity</param>
        /// <param name="ActivityCode">String:Activity Code of the activity</param>
        /// </param>
        /// <returns>Boolean:True:Valid Rights, False:not valid rights if Activity type is TASK</returns>
        public bool CheckUserRights(RightsModule ModuleCode, RightsActivity ActivityCode, DataBaseType connectTo)
        {
            MasterBase master = this.Master as MasterBase;
            bool hasRights = false;
            if (master != null)
            {
                hasRights = CheckUserRights(ModuleCode, ActivityCode, false, connectTo);
            }

            return hasRights;
        }

        /// <summary>
        /// Check Userrights for given ModuleCode and ActivityCode for logged user
        /// It will verify based on type of activity(PAGE or TASK)
        /// </summary>
        /// <param name="ModuleCode">String:Module Code of the activity</param>
        /// <param name="ActivityCode">String:Activity Code of the activity</param>
        /// <param name="dontRedirect">boolean:Whether redirect to error page 
        /// if Activity Type is PAGE and doesn't has valid rights.
        /// </param>
        /// <returns>Boolean:True:Valid Rights, False:not valid rights</returns>
        public bool CheckUserRights(RightsModule ModuleCode, RightsActivity ActivityCode, bool dontRedirect, DataBaseType connectTo)
        {
            MasterBase master = this.Master as MasterBase;
            bool hasRights = false;
            if (master != null)
            {
                hasRights = master.CheckUserRights(ModuleCode, ActivityCode, this.LoginUser.LoginUserRoleId, dontRedirect, connectTo);
            }

            return hasRights;
        }

        //For downloading file
        public virtual DataTable ReadData(String FileName)
        {
            DataTable dt = new DataTable();
            try
            {
                string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0'";
                if (!string.IsNullOrEmpty(FileName))
                {
                    conStr = String.Format(conStr, FileName);
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dtExcelSchema;
                    cmdExcel.Connection = connExcel;
                    connExcel.Open();
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[1][2].ToString();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dt);
                    connExcel.Close();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// On 22/03/2018, Check Acmeerp DB server is connected or not
        /// if not connected, mail will be sent to defaul acmeerp id and (Alex and Chinna)
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsDatabaseServerConnected()
        {
            ResultArgs resultArgs = new ResultArgs();
            using (Bosco.Model.SettingSystem settingSystem = new Bosco.Model.SettingSystem())
            {
                resultArgs = settingSystem.IsDatabaseServerConnected();
            }
            return resultArgs;
        }

        //public void AssignLedgerIDs()
        //{
        //    string Ledger_Name_InterACTransfer = "Inter Account Transfer";
        //    string Ledger_Name_ContributionFromProvince = "Contribution from Province";
        //    string Ledger_Name_ContributionToProvince = "Contribution to Province";

        //    userProperty.InterAccountFromLedgerIds = GetLedgerId(Ledger_Name_InterACTransfer);
        //    userProperty.InterAccountToLedgerIds = userProperty.InterAccountFromLedgerIds;
        //    userProperty.ProvinceFromLedgerIds = GetLedgerId(Ledger_Name_ContributionFromProvince);
        //    userProperty.ProvinceToLedgerIds = GetLedgerId(Ledger_Name_ContributionToProvince);
        //}

        public void AssignSetting()
        {
            try
            {
                ISetting isetting = new GlobalSetting();
                ResultArgs resultArgs = isetting.FetchSettingDetails(1);

                if (resultArgs.Success && resultArgs.DataSource.TableView != null && resultArgs.DataSource.TableView.Count > 0)
                {
                    DataView dvSetting = resultArgs.DataSource.TableView;
                    this.LoginUser.SettingInfo = dvSetting;
                    isetting.ApplySetting();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private int GetLedgerId(string ledgername)
        {
            Int32 rntvalue = 0;
            ResultArgs resultArgs = new ResultArgs();
            using (LedgerSystem ledgersys = new LedgerSystem())
            {
                rntvalue = ledgersys.GetLegerId(ledgername);
            }

            return rntvalue;
        }

        //public virtual DataSet ReadMasters(string FileName)
        //{
        //    DataSet Masters = new DataSet();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(FileName))
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new ErrorLog().WriteError("Import Masters from ReadMasters Method Exception  - " + ex.Message);
        //    }
        //}
    }
}
