/****************************************************************************************************************************
 * Purpose       : This page is to manage branch office view.
 * Created Date  : 26 April 2014
 * Modified Date : 
 * **************************************************************************************************************************/

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.Data;
using Bosco.Model;
using System.ServiceModel;


namespace AcMeERP.Module.Office
{
    public partial class BranchOfficeView : Base.UIBase
    {
        #region Declaration
        private DataView BranchOfficeViewSource = null;
        public DataTable BranchSourceToExport = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        private string licenseTargetPage = "";
        private const string OFFICE_CODE = "Office Code";
        private const string BRANCH_CODE = "Branch Code";
        private const string BRANCH_NAME = "Branch Name";
        private const string DEPLOYMENT_TYPE = "Deployment Type";
        private const string ADDRESS = "Address";
        private const string COUNTRY = "Country";
        private const string PHONE_NO = "Phone No";
        private const string MOBILE_NO = "Mobile No";
        private const string STATE = "State";
        private const string PINCODE = "PinCode";
        private const string OFFICE_STATUS = "Office Status";
        private string BOCodeColumn = "";
        CommonMember UtilityMember = new CommonMember();
        #endregion

        #region Property
        private string RandonPassword
        {
            get;
            set;
        }

        //Branch Admin MailId
        private string EmailId
        {
            get
            {
                string MailId = string.Empty;
                if (ViewState["EmailId"].ToString() != string.Empty)
                {
                    MailId = ViewState["EmailId"].ToString();
                }
                return MailId;

            }
            set { ViewState["EmailId"] = value; }
        }

        //Branch Admin Username
        private string BranchPartCode
        {
            get
            {
                string BranchPartCode = string.Empty;
                if (ViewState["BranchPartCode"].ToString() != string.Empty)
                {
                    BranchPartCode = ViewState["BranchPartCode"].ToString();
                }
                return BranchPartCode;

            }
            set { ViewState["BranchPartCode"] = value; }
        }
        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                targetPage = this.GetPageUrlByName(URLPages.BranchOfficeAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.BranchOfficeView;
                licenseTargetPage = this.GetPageUrlByName(URLPages.GenerateLicenseKey.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.BranchOfficeView;
                SetBranchOfficeViewSource();

                gvBranchOffice.RowCommand += new GridViewCommandEventHandler(gvBranchOffice_RowCommand);
                gvBranchOffice.RowDataBound += new GridViewRowEventHandler(gvBranchOffice_RowDataBound);
                gvBranchOffice.ExportClicked += new EventHandler(gvBranchOffice_ExportClicked);
                gvBranchOffice.DownloadBranchTemplate += new EventHandler(gvBranchOffice_DownloadBranchTemplate);
                LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
                linkUrl.ShowModelWindow = false;

                LinkUrlColumn licenseLinkUrl = new LinkUrlColumn(this.licenseTargetPage, "Generate License Key", false);
                licenseLinkUrl.ShowModelWindow = false;
                if (this.LoginUser.IsAdminUser)
                {
                    gvBranchOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                    if (this.LoginUser.IsPortalUser)
                    {
                        gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Status, this.BOCodeColumn, "", null, "", CommandMode.Status.ToString());
                    }
                    gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Email, this.BOCodeColumn, "", null, "", CommandMode.Email.ToString());
                    if (this.LoginUser.IsPortalUser)
                    {
                        gvBranchOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.License, this.rowIdColumn, "", licenseLinkUrl, "", CommandMode.License.ToString());
                    }
                    gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.View, this.BOCodeColumn, "", null, "", CommandMode.View.ToString());
                    gvBranchOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                    gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.BOCodeColumn, "", null, "", CommandMode.Delete.ToString());

                }
                else if (base.LoginUser.IsHeadOfficeUser)
                {
                    if (this.CheckUserRights(RightsModule.Office, RightsActivity.BranchOfficeAdd, true,
                        base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                    {
                        gvBranchOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                    }
                    if (this.CheckUserRights(RightsModule.Office, RightsActivity.BranchOfficeApprove, true,
                        base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                    {
                        gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Status, this.BOCodeColumn, "", linkUrl, "", CommandMode.Status.ToString());
                    }
                    if (this.CheckUserRights(RightsModule.Office, RightsActivity.CommunicateLoginInfo, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                    {
                        gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Email, this.BOCodeColumn, "", null, "", CommandMode.Email.ToString());
                    }
                    if (this.CheckUserRights(RightsModule.Office, RightsActivity.BranchOfficeEdit, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                    {
                        gvBranchOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                    }
                    if (this.CheckUserRights(RightsModule.Office, RightsActivity.BranchOfficeDelete, true,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                    {
                        gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.BOCodeColumn, "", null, "", CommandMode.Delete.ToString());
                    }
                }
                else if (base.LoginUser.IsBranchOfficeUser)
                {
                    //Create SubBranches- Sub Centre if the branch is multilocated.
                    if ((int)UserRole.BranchAdmin == base.LoginUser.LoginUserRoleId)
                    {
                        using (LicenseSystem licenseSystem = new LicenseSystem())
                        {
                            licenseSystem.BranchCode = base.LoginUser.LoginUserBranchOfficeCode;
                            if (licenseSystem.IsBranchMultilocated() && (int)UserRole.BranchAdmin == base.LoginUser.LoginUserRoleId)
                            {
                                gvBranchOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                               //gvBranchOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                               //gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.BOCodeColumn, "", null, "", CommandMode.Delete.ToString());
                            }
                        }
                    }

                    gvBranchOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.View, this.BOCodeColumn, "", null, "", CommandMode.View.ToString());
                }
                gvBranchOffice.HideColumn = this.hiddenColumn;
                gvBranchOffice.RowIdColumn = this.rowIdColumn;
                gvBranchOffice.DataSource = BranchOfficeViewSource;
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.BranchOffice.BranchOfficeViewPageTitle;
                this.CheckUserRights(RightsModule.Office, RightsActivity.BranchOfficeView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                gvBranchOffice.EnableSort = true;
                //Head Office and Portal User can export data
                if (!base.LoginUser.IsBranchOfficeUser)
                {
                    gvBranchOffice.ShowExport = true;
                }
                //this.ShowLoadWaitPopUp();
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        #endregion

        #region Row Command Event - For Delete
        /// <summary>
        /// this event is to bind the values to each row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBranchOffice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                this.Message = string.Empty;
                ResultArgs resultArgs = new ResultArgs();
                string[] status_code = e.CommandArgument.ToString().Trim().Split(',');
                //Index 0 - Status 1 - Branch office code -2 Head Office Code -3-Issubbranch
                if (status_code.Length == 4)
                {
                    string headOfficeCode = status_code[2];
                    string branchOfficeCode = status_code[1];
                    int isSubBranch = this.Member.NumberSet.ToInteger(status_code[3].ToString());
                    int status = this.Member.NumberSet.ToInteger(status_code[0]);
                    new ErrorLog().WriteError("HeadOffice Code: " + headOfficeCode + "BranchOfficeCode: " + branchOfficeCode);
                    if (e.CommandName == CommandMode.View.ToString())
                    {
                        using (BranchOfficeSystem branchSystem = new BranchOfficeSystem())
                        {
                            branchSystem.BranchOfficeCode = branchOfficeCode.Trim();
                            resultArgs = branchSystem.FetchBranchOfficeView(DataBaseType.Portal);
                        }
                        if (resultArgs.Success)
                        {
                            DataTable dtBranch = resultArgs.DataSource.Table;
                            if (dtBranch != null)
                            {
                                if (dtBranch.Rows.Count > 0)
                                {
                                    ltHeadOfficeCode.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                                    ltBranchOfficeCode.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                                    ltBranchOfficeName.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString();
                                    ltAddress.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.ADDRESSColumn.ColumnName].ToString();
                                    ltCity.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.CITYColumn.ColumnName].ToString();
                                    ltCountry.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.COUNTRYColumn.ColumnName].ToString();
                                    ltState.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.STATEColumn.ColumnName].ToString();
                                    ltPincode.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.PINCODEColumn.ColumnName].ToString();
                                    ltPhoneNo.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.PHONE_NOColumn.ColumnName].ToString();
                                    ltMobileNo.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.MOBILE_NOColumn.ColumnName].ToString();
                                    ltMailId.Text = dtBranch.Rows[0][this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName].ToString();
                                    if (base.LoginUser.IsHeadOfficeUser)
                                    {
                                        divHeadOfficeCode.Visible = false;
                                    }
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "BranchOfficeView", "javascript:ShowDisplayPopUp()", true);
                    }
                    else if (e.CommandName == CommandMode.Delete.ToString())
                    {
                        if (!string.IsNullOrEmpty(branchOfficeCode))
                        {
                            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                            {
                                base.HeadOfficeCode = headOfficeCode.Trim();
                                resultArgs = branchOfficeSystem.DeleteBranchOfficeDetails(DataBaseType.HeadOffice, branchOfficeCode);

                                if (resultArgs.Success)
                                {
                                    int BranchId = 0;
                                    BranchId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.Portal, branchOfficeCode);
                                    if (BranchId > 0)
                                    {
                                        resultArgs = branchOfficeSystem.DeleteBranchLicenseDetails(DataBaseType.Portal, BranchId);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = branchOfficeSystem.DeleteBranchOfficeDetails(DataBaseType.Portal, branchOfficeCode);
                                            if (resultArgs.Success)
                                            {
                                                this.Message = MessageCatalog.Message.BranchOfficeDeleted;
                                                SetBranchOfficeViewSource();
                                                gvBranchOffice.BindGrid(BranchOfficeViewSource);
                                            }
                                            else
                                                this.Message = resultArgs.Message;
                                        }
                                    }
                                }
                                else
                                    this.Message = resultArgs.Message;
                            }
                        }
                    }
                    else if (e.CommandName == CommandMode.Status.ToString())
                    {
                        this.Message = string.Empty;
                        int usercreatedstatus = 0; //To update the user created staus
                        if (!string.IsNullOrEmpty(branchOfficeCode))
                        {
                            new ErrorLog().WriteError("Inside Status Change branchOfficeCode: " + branchOfficeCode);
                            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                            {
                                branchOfficeSystem.Status = (status == (int)OfficeStatus.Activated) ? (int)OfficeStatus.DeActivated : (int)OfficeStatus.Activated;
                                new ErrorLog().WriteError("Inside Status Change branchOfficeSystem.Status: " + branchOfficeSystem.Status);
                                //To assign staus value based on the current status Ex Cur- 1- Created - Change into Activate -2 3-Deactivate
                                branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                                resultArgs = branchOfficeSystem.UpdateOfficeStatus(DataBaseType.Portal);
                                if (resultArgs.Success)
                                {
                                    base.HeadOfficeCode = headOfficeCode;
                                    resultArgs = branchOfficeSystem.UpdateOfficeStatus(DataBaseType.HeadOffice);
                                    if (resultArgs.Success)
                                    {
                                        new ErrorLog().WriteError("Inside Status Change headOfficeCode: " + headOfficeCode);
                                        //Push head office admin details to user info table of head office data base and send mail to head office admin with login credentials
                                        if (status == (int)OfficeStatus.Created && isSubBranch == 0)
                                        {
                                            if (SaveBranchOfficeAdminUserDetails(branchOfficeCode, headOfficeCode))
                                            {
                                                usercreatedstatus = (int)UserCreatedStatus.UserCreated;
                                                //Sending mail to head office admin
                                                string branchOfficeName = GetBranchOfficeName(headOfficeCode, headOfficeCode + BranchPartCode);
                                                string Name = "Branch Admin";
                                                string Header = "Your Branch Office account with Acme.erp has been activated successfully. You can login to the portal with following details."
                                                                    + "<br/>";
                                                string MainContent = "Head Office Code: " + headOfficeCode + "<br/><br />" + 
                                                                    "Branch Name:" + branchOfficeName + "<br /><br /> URL:" + ConfigurationManager.AppSettings["AcMESite"].ToString()
                                                                     + "login.php" + "<br /><br /> Username: " + BranchPartCode + "<br/>"
                                                                    + "<br/> Password: " + RandonPassword + "<br/>";

                                                string content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);
                                                resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(EmailId.Trim()),
                                                    CommonMethod.RemoveFirstValue(EmailId.Trim()), "Branch Office Activation Success", content,true);
                                                if (resultArgs.Success)
                                                {
                                                    //Update user created status in both data base
                                                    usercreatedstatus = (int)UserCreatedStatus.UserCommunicated;
                                                    this.Message = MessageCatalog.Message.UserCommunicated + " and ";
                                                }
                                                else
                                                {
                                                    this.Message = MessageCatalog.Message.UserNotCommunicated + " and ";
                                                }
                                                branchOfficeSystem.UserCreatedStatus = usercreatedstatus;
                                                branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                                                resultArgs = branchOfficeSystem.UpdateUserCreatedStatus(DataBaseType.Portal);
                                                if (resultArgs.Success)
                                                {
                                                    //update in head office database
                                                    base.HeadOfficeCode = headOfficeCode;
                                                    resultArgs = branchOfficeSystem.UpdateUserCreatedStatus(DataBaseType.HeadOffice);
                                                    //if (resultArgs.Success)
                                                    //Write it in log

                                                }
                                                else
                                                {
                                                    new ErrorLog().WriteError("Inside Send Mail UpdateUserCreatedStatus: " + usercreatedstatus);
                                                }

                                            }

                                        }
                                        this.Message += (status == (int)OfficeStatus.DeActivated || status == (int)OfficeStatus.Created) ?
                                             MessageCatalog.Message.BranchOfficeActivated : MessageCatalog.Message.BranchOfficeDeactivated;

                                        SetBranchOfficeViewSource();
                                        gvBranchOffice.BindGrid(BranchOfficeViewSource);
                                        if (status != (int)OfficeStatus.Created && isSubBranch == 0)
                                        {
                                            SendBranchOfficeApproveStatus(branchOfficeCode, (status == (int)OfficeStatus.DeActivated || status == (int)OfficeStatus.Created) ?
                                                    OfficeStatus.Activated.ToString() : OfficeStatus.DeActivated.ToString());
                                        }
                                    }
                                    else
                                    {
                                        this.Message = resultArgs.Message;
                                    }
                                }
                                else
                                {
                                    this.Message = resultArgs.Message;
                                }
                            }
                        }
                    }
                    else if (e.CommandName == CommandMode.Email.ToString())
                    {
                        if (!(string.IsNullOrEmpty(branchOfficeCode) && string.IsNullOrEmpty(headOfficeCode)) && isSubBranch == 0)
                        {
                            if (SendMailToBranchOfficeAdmin(headOfficeCode, branchOfficeCode))
                            {
                                this.Message = MessageCatalog.Message.BranchOfficeLoginInfo;
                            }
                            else
                            {
                                this.Message = MessageCatalog.Message.MailSendingFailure;
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.MailCommunicationSubBranch;
                        }
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                this.Message = ex.Detail.Message;
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Branch Office view Activity " + ex.Message);
                this.Message = ex.Message;
            }
            finally
            {
                //To set the current login(Portal or Head Office)
                if (base.LoginUser.IsPortalUser)
                {
                    base.HeadOfficeCode = DataBaseType.Portal.ToString();
                }
            }
        }

        #endregion

        #region Row Data Bound Event
        protected void gvBranchOffice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                HyperLink hlkLicense = (HyperLink)e.Row.FindControl("hlkLicense");
                ImageButton imgEmail = (ImageButton)e.Row.FindControl("imgEmail");

                // Status cell value is 4 in Branch Office GridView
                if (e.Row.Cells[4].Text.Trim().Equals(OfficeStatus.Created.ToString()) || e.Row.Cells[4].Text.Trim().Equals(OfficeStatus.DeActivated.ToString()))
                {
                    if (imgStatus != null)
                    {
                        imgStatus.ImageUrl = "~/App_Themes/MainTheme/images/activate.gif";
                        imgStatus.ToolTip = MessageCatalog.Message.ActivateToolTip;
                        imgStatus.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.Activate_Confirm + "');";
                    }
                    if (imgEmail != null)
                    {
                        imgEmail.Enabled = false;//Disable mailing if office is created or inactive mode.
                    }
                    if (base.LoginUser.IsPortalUser)
                    {
                        if (hlkLicense != null)
                        {
                            hlkLicense.ImageUrl = "~/App_Themes/MainTheme/images/licenseGray.png";
                            hlkLicense.Enabled = false;
                        }
                    }
                }
                else
                {
                    if (imgStatus != null)
                    {
                        imgStatus.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.DeActivate_Confirm + "');";
                    }
                }

                if (base.LoginUser.IsPortalUser)
                {
                    if (imgStatus != null)
                    {
                        this.ShowLoadWaitPopUp(imgStatus);
                    }
                }
                if (!base.LoginUser.IsBranchOfficeUser)
                {
                    if (imgEmail != null)
                    {
                        this.ShowLoadWaitPopUp(imgEmail);
                    }
                }
            }
        }

        protected void gvBranchOffice_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "BranchOffice" + DateTime.Now.Ticks.ToString();
                SelectAllBranchToExport();
                if (!BranchSourceToExport.Equals(null))
                {
                    CommonMethod.WriteExcelFile(BranchSourceToExport, fileName);
                    DownLoadFile(fileName);
                }
                else
                {
                    this.Message = MessageCatalog.Message.NoRecordToExport;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void gvBranchOffice_DownloadBranchTemplate(object sender, EventArgs e)
        {
            try
            {
                string fileName = "BranchOfficeTemplate" + DateTime.Now.Ticks.ToString();
                DataTable dtBranchTemplate = CreateBranchTemplate();
                CommonMethod.WriteExcelFile(dtBranchTemplate, fileName);
                DownLoadFile(fileName);
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private DataTable CreateBranchTemplate()
        {
            DataTable dtBranchTemplate = new DataTable();

            dtBranchTemplate.Columns.Add("HeaddOffice", typeof(string));
            dtBranchTemplate.Columns.Add("Code/UserName", typeof(string));
            dtBranchTemplate.Columns.Add("Name", typeof(string));
            dtBranchTemplate.Columns.Add("Deployment Model", typeof(string));
            dtBranchTemplate.Columns.Add("Person Incharge", typeof(string));
            dtBranchTemplate.Columns.Add("E-Mail", typeof(string));
            dtBranchTemplate.Columns.Add("Phone No", typeof(string));
            dtBranchTemplate.Columns.Add("Mobile No", typeof(string));
            dtBranchTemplate.Columns.Add("Address", typeof(string));
            dtBranchTemplate.Columns.Add("City", typeof(string));
            dtBranchTemplate.Columns.Add("Country", typeof(string));
            dtBranchTemplate.Columns.Add("State/Province", typeof(string));
            dtBranchTemplate.Columns.Add("Postal/Zip Code", typeof(string));

            dtBranchTemplate.TableName = "BranchTemplate";
            return dtBranchTemplate;

        }

        #endregion

        #region Methods
        /// <summary>
        /// This is to save user details in the head office data base
        /// </summary>
        /// <param name="hocode"></param>
        /// <returns></returns>
        private bool SaveBranchOfficeAdminUserDetails(string bocode, string headOfficeCode)
        {
            bool isUserCreated = false;
            ResultArgs result = null;
            DataTable dtUserInfo = new DataTable();
            try
            {

                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                {
                    branchOfficeSystem.BranchOfficeCode = bocode;
                    result = branchOfficeSystem.FetchBranchOfficeAdmin(DataBaseType.Portal);
                    if (result.Success)
                    {
                        dtUserInfo = result.DataSource.Table;
                    }
                }

                if (dtUserInfo != null)
                {
                    if (dtUserInfo.Rows.Count > 0)
                    {
                        using (UserSystem usersys = new UserSystem())
                        {
                            usersys.UserId = (int)AddNewRow.NewRow;
                            usersys.FirstName = dtUserInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString().Trim();
                            usersys.UserName = BranchPartCode = dtUserInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn.ColumnName].ToString().Trim();
                            usersys.RoleId = (int)UserRole.BranchAdmin;
                            usersys.UserType = (int)UserRole.BranchAdmin;
                            RandonPassword = CommonMethod.GetRandomPassword();
                            usersys.Password = CommonMember.EncryptValue(RandonPassword);
                            usersys.MobileNo = dtUserInfo.Rows[0][this.AppSchema.BranchOffice.MOBILE_NOColumn.ColumnName].ToString().Trim();
                            usersys.Email = EmailId = dtUserInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName].ToString().Trim().ToLower();
                            usersys.status = (int)Status.Active;
                            usersys.PasswordStatus = (int)ResetPassword.AutomaticPassword;
                            usersys.BranchCode = bocode;
                            usersys.HeadOfficeCode = headOfficeCode;

                            base.HeadOfficeCode = headOfficeCode;
                            result = usersys.SaveUser(DataBaseType.HeadOffice);
                        }
                        isUserCreated = result.Success;
                        if (result.Message.Equals(MessageCatalog.Message.RecordExistence))
                        {
                            isUserCreated = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                isUserCreated = false;
            }
            return isUserCreated;

        }

        /// <summary>
        /// Fetch Branch Office Details
        /// </summary>
        /// 
        public void SelectAllBranchToExport()
        {
            try
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                {
                    ResultArgs resultArgs = branchOfficeSystem.FetchAllBranchsToExport(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                        BranchSourceToExport = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        private void SetBranchOfficeViewSource()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {

                    ResultArgs resultArgs = BranchOfficeSystem.FetchBranchOfficeDetails(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                    if (resultArgs.Success)
                    {
                        BranchOfficeViewSource = resultArgs.DataSource.Table.DefaultView;
                        BranchSourceToExport = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }

                    this.rowIdColumn = BranchOfficeSystem.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName;
                    this.BOCodeColumn = BranchOfficeSystem.AppSchema.BranchOffice.STATUSColumn.ColumnName;
                    if (this.LoginUser.IsPortalUser)
                    {
                        this.hiddenColumn = this.rowIdColumn + "," + BranchOfficeSystem.AppSchema.BranchOffice.STATUSColumn.ColumnName;
                    }
                    else
                    {
                        this.hiddenColumn = this.rowIdColumn + "," + "H.O Code" + "," +
                            BranchOfficeSystem.AppSchema.BranchOffice.STATUSColumn.ColumnName;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        /// <summary>
        /// Send Mail if Branch Office Admin Login credentials fails.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        private bool SendMailToBranchOfficeAdmin(string headOfficeCode, string branchOfficeCode)
        {
            int UserCommunicationStatus = 0;
            bool IsMailSent = false;
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                ResultArgs resultArgs = null;
                branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                resultArgs = branchOfficeSystem.BranchOfficeDetailsByCode(branchOfficeCode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataTable dtBranchOffice = resultArgs.DataSource.Table;
                    UserCommunicationStatus = this.Member.NumberSet.ToInteger(dtBranchOffice.Rows[0][this.AppSchema.BranchOffice.USER_CREATED_STATUSColumn.ColumnName].ToString());
                    //Branch Admin Username
                    BranchPartCode = dtBranchOffice.Rows[0][this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn.ColumnName].ToString();
                }
                //if user is not communicated ,send auto mail to Head office Admin to login.
                if (UserCommunicationStatus == 0)  //User is not created but head office is created.
                {
                    if (SaveBranchOfficeAdminUserDetails(branchOfficeCode, headOfficeCode))
                    {
                        UserCommunicationStatus = (int)UserCreatedStatus.UserCreated;
                    }
                }//User is created or commnicated
                if (UserCommunicationStatus == (int)UserCreatedStatus.UserCreated || UserCommunicationStatus == (int)UserCreatedStatus.UserCommunicated)
                {
                    DataTable dtUserInfo = null;
                    using (UserSystem userSystem = new UserSystem())
                    {
                        base.HeadOfficeCode = headOfficeCode;
                        userSystem.UserName = BranchPartCode;
                        resultArgs = userSystem.FetchUserDetailsByHeadOfficeCode(DataBaseType.HeadOffice);
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dtUserInfo = resultArgs.DataSource.Table;
                        }
                        //Sending mail to Branch office admin
                        using (SettingSystem automail = new SettingSystem())
                        {
                            string branchOfficeName = GetBranchOfficeName(headOfficeCode, headOfficeCode + BranchPartCode);
                            string Name = "Branch Admin" + " (" + branchOfficeName + ")";
                            string Header = "Your Branch Office account with Acme.erp has been activated successfully. You can login to the portal with the following details."
                                                + "<br/> You will be asked to change the password when you login for the first time.<br/>";
                            string MainContent = "<b>You can login to the portal using the below,</b> <br/><br/> URL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: <b>" + ConfigurationManager.AppSettings["AcMESite"].ToString() + headOfficeCode + "</b><br /> Username: <b>"
                                                + BranchPartCode + "</b><br/>"
                                                + "Password: "
                                                + CommonMember.DecryptValue(dtUserInfo.Rows[0][this.AppSchema.User.PASSWORDColumn.ColumnName].ToString())
                                                + "<br/><br/><b>By logging into the portal you can</b> <br/> <br/>"
                                                + "1.	Download Latest version of Acme.erp<br/>"
                                                + "2.	Download Master details<br/>"
                                                + "3.	Download License<br/>"
                                                + "4.	Upload vouchers entered<br/>"
                                                + "5.	Submit Support requests and more…<br/>";

                            string content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);

                            resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString()),
                                 CommonMethod.RemoveFirstValue(dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString()),
                                "Branch Office Activation Success", content,false);
                            if (resultArgs.Success)
                            {
                                //Update user created status in both database
                                UserCommunicationStatus = (int)UserCreatedStatus.UserCommunicated;
                                IsMailSent = true;
                            }
                            branchOfficeSystem.UserCreatedStatus = UserCommunicationStatus;
                            branchOfficeSystem.HeadOffice_Code = headOfficeCode;
                            resultArgs = branchOfficeSystem.UpdateUserCreatedStatus(DataBaseType.Portal);
                            if (resultArgs.Success)
                            {
                                //update in head office database
                                base.HeadOfficeCode = headOfficeCode;
                                resultArgs = branchOfficeSystem.UpdateUserCreatedStatus(DataBaseType.HeadOffice);
                                if (resultArgs.Success)
                                //Write it in log
                                {
                                    new ErrorLog().WriteError("BranchOfficeAdd.aspx.cs", "btnSaveBranchOffice_Click", resultArgs.Message, "0");
                                }

                            }
                        }
                    }
                }
            }
            return IsMailSent;
        }
        /// <summary>
        /// Send if Branch Office Status is changed
        /// </summary>
        /// <param name="branchOfficeCode"></param>
        /// <param name="status"></param>
        private void SendBranchOfficeApproveStatus(string branchOfficeCode, string status)
        {
            using (BranchOfficeSystem branchofficeSystem = new BranchOfficeSystem())
            {
                ResultArgs resultArgs = null;
                DataTable dtBranchOfficeInfo = null;
                resultArgs = branchofficeSystem.BranchOfficeDetailsByCode(branchOfficeCode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtBranchOfficeInfo = resultArgs.DataSource.Table;
                    //Sending mail to head office admin
                    using (SettingSystem automail = new SettingSystem())
                    {
                        string branchOfficeName = dtBranchOfficeInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString();
                        string Name = "Branch Admin" + "(" + branchOfficeName + ")";
                        string Header = "Your Branch Office is " + status + " successfully.";
                        string MainContent = "Your Branch Office Code is: " + branchOfficeCode + "<br/>";

                        string content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);

                        resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(dtBranchOfficeInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName].ToString()),
                            CommonMethod.RemoveFirstValue(dtBranchOfficeInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName].ToString()),
                            "Branch Office Activation Status", content,true);
                        if (resultArgs.Success)
                        {
                        }
                    }
                }
            }
        }

        private string GetBranchOfficeName(string headOfficeCode, string branchOfficeCode)
        {
            string branchOfficeName = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(headOfficeCode) && !string.IsNullOrEmpty(branchOfficeCode))
                {
                    DataSynchronizeService.DataSynchronizerClient objDsync = new DataSynchronizeService.DataSynchronizerClient();
                    DataTable dtBranchInfo = objDsync.GetBranchDetails(headOfficeCode, branchOfficeCode);
                    if (dtBranchInfo != null && dtBranchInfo.Rows.Count > 0)
                    {
                        branchOfficeName = dtBranchInfo.Rows[0]["Branch Name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return branchOfficeName;
        }
        #endregion

        #region Download Excel
        private void DownLoadFile(string fileName)
        {
            try
            {
                byte[] bytes;
                bytes = File.ReadAllBytes(PagePath.AppFilePath + fileName + ".xlsx");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/xlsx";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".xlsx");
                Response.BinaryWrite(bytes);
                Response.Flush();
                System.IO.File.Delete(PagePath.AppFilePath + fileName + ".xlsx");
                Response.End();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        #endregion
    }
}