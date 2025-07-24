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
 * Purpose          :This page helps the portal admin to view all the head office available in acme.erp and communicate credentials and make active or inactive based
 *                     on the client need.
 *****************************************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using Bosco.Model;
using System.Configuration;
using System.Web.Routing;
using System.IO;

namespace AcMeERP.Module.Office
{
    public partial class HeadOfficeView : Base.UIBase
    {
        private DataView headOfficeViewSource = null;
        private DataTable HeadOfficeSourceToExportExcel = null;
        private const string HEAD_OFFICE_ID = "HEAD_OFFICE_ID";
        private const string STATUS = "STATUS";
        CommonMember UtilityMember = new CommonMember();

        private string rowIdColumn = "";
        private string HOCodeColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";

        #region Property
        private string RandonPassword
        {
            get;
            set;
        }
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
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.HeadOfficeAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.HeadOfficeView;
            SetHeadOfficeViewSource();

            gvHeadOffice.RowCommand += new GridViewCommandEventHandler(gvHeadOffice_RowCommand);
            gvHeadOffice.RowDataBound += new GridViewRowEventHandler(gvHeadOffice_RowDataBound);
            gvHeadOffice.ExportClicked += new EventHandler(gvHeadOffice_ExportClicked);
            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvHeadOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Status, this.HOCodeColumn, "", null, "", CommandMode.Status.ToString());
                gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.DB, this.HOCodeColumn, "", null, "", CommandMode.DB.ToString());
                gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Email, this.HOCodeColumn, "", null, "", CommandMode.Email.ToString());
                gvHeadOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.HOCodeColumn, "", null, "", CommandMode.Delete.ToString());

            }
            else
            {
                if (this.CheckUserRights(RightsModule.Office, RightsActivity.HeadOfficeAdd, true,
                    base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvHeadOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Office, RightsActivity.HeadOffficeApprove, true,
                    base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Status, this.HOCodeColumn, "", null, "", CommandMode.Status.ToString());
                }
                if (this.CheckUserRights(RightsModule.Office, RightsActivity.UpdateDatbaseConnection, true,
                    base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.DB, this.HOCodeColumn, "", null, "", CommandMode.DB.ToString());
                }
                if (this.CheckUserRights(RightsModule.Office, RightsActivity.CommunicateLoginInfo, true,
                   base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Email, this.HOCodeColumn, "", null, "", CommandMode.Email.ToString());
                }
                if (this.CheckUserRights(RightsModule.Office, RightsActivity.HeadOfficeEdit, true,
                   base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvHeadOffice.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Office, RightsActivity.HeadOfficeDelete, true,
                   base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvHeadOffice.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.HOCodeColumn, "", null, "", CommandMode.Delete.ToString());
                }

            }

            gvHeadOffice.HideColumn = this.hiddenColumn;
            gvHeadOffice.RowIdColumn = this.rowIdColumn;
            gvHeadOffice.DataSource = headOfficeViewSource;

        }

        protected void gvHeadOffice_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "HeadOffice" + DateTime.Now.Ticks.ToString();
                SetHeadOfficeViewSource();
                if (!HeadOfficeSourceToExportExcel.Equals(null))
                {
                    HeadOfficeSourceToExportExcel.Columns.Remove(HEAD_OFFICE_ID);
                    HeadOfficeSourceToExportExcel.Columns.Remove(STATUS);

                    CommonMethod.WriteExcelFile(HeadOfficeSourceToExportExcel, fileName);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.HeadOffice.HeadOfficeViewPageTitel;
                this.CheckUserRights(RightsModule.Office, RightsActivity.HeadOfficeView,
                base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                // this.ShowLoadWaitPopUp();
                gvHeadOffice.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        #region Row Command Event - For Delete
        /// <summary>
        /// this event is to bind the values to each row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvHeadOffice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ResultArgs resultArgs = new ResultArgs();
                string[] status_code = e.CommandArgument.ToString().Trim().Split(',');
                new ErrorLog().WriteError("Head Office Status Code Length:" + status_code.Length);
                //Index 0 - Status 1 - Head office code
                if (status_code.Length == 2)
                {
                    string headOfficeCode = status_code[1];
                    int status = this.Member.NumberSet.ToInteger(status_code[0]);

                    if (e.CommandName == CommandMode.Delete.ToString())
                    {
                        if (!string.IsNullOrEmpty(headOfficeCode))
                        {
                            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
                            {
                                base.HeadOfficeCode = headOfficeCode;
                                resultArgs = headOfficeSystem.DeleteHeadOfficeDetails(DataBaseType.HeadOffice, headOfficeCode);

                                if (resultArgs.Success)
                                {
                                    resultArgs = headOfficeSystem.DeleteHeadOfficeDetails(DataBaseType.Portal, headOfficeCode);
                                    if (resultArgs.Success)
                                    {
                                        this.Message = MessageCatalog.Message.HeadOfficeDeleted;

                                        SetHeadOfficeViewSource();
                                        gvHeadOffice.BindGrid(headOfficeViewSource);
                                    }
                                    else
                                        this.Message = resultArgs.Message;
                                }
                                else
                                    this.Message = resultArgs.Message;
                            }
                        }

                    }
                    else if (e.CommandName == CommandMode.DB.ToString())
                    {
                        ViewState["HOCODE"] = headOfficeCode;
                        new ErrorLog().WriteError("Head Office Database Updation:" + headOfficeCode);
                        AssignDatabseValuesToControls();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateDB", "javascript:ShowDisplayPopUp()", true);
                        //ClearDBValues();
                    }
                    else if (e.CommandName == CommandMode.Status.ToString())
                    {

                        this.Message = string.Empty;
                        int usercreatedstatus = 0; //To update the user created staus
                        new ErrorLog().WriteError("Head Office Status:" + headOfficeCode);
                        if (!string.IsNullOrEmpty(headOfficeCode))
                        {
                            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
                            {
                                if (status == (int)OfficeStatus.Activated) //check if head office has got active branches
                                {
                                    resultArgs = headOfficeSystem.FetchBranchByHeadOffice(headOfficeCode);
                                    if ((resultArgs.Success && resultArgs.RowsAffected > 0))
                                    {
                                        this.Message = MessageCatalog.Message.HeadOfficeNotDeactivate;
                                        return;
                                    }
                                }
                                headOfficeSystem.Status = (status == (int)OfficeStatus.Activated) ? (int)OfficeStatus.DeActivated : (int)OfficeStatus.Activated;
                                //To assign staus value based on the current status Ex Cur- 1- Created - Change into Activate -2 3-Deactivate
                                headOfficeSystem.HeadOfficeCode = headOfficeCode;
                                resultArgs = headOfficeSystem.UpdateOfficeStatus(DataBaseType.Portal);
                                if (resultArgs.Success)
                                {
                                    base.HeadOfficeCode = headOfficeCode;
                                    resultArgs = headOfficeSystem.UpdateOfficeStatus(DataBaseType.HeadOffice);
                                    new ErrorLog().WriteError("Head Office DB Connection Changed:" + base.HeadOfficeCode);
                                    if (resultArgs.Success)
                                    {

                                        using (AccouingPeriodSystem accountingPeriodSystem = new AccouingPeriodSystem())
                                        {
                                            DateTime dtfromDate;
                                            DateTime dtToDate;
                                            int AccountYearType = headOfficeSystem.FetchAccountingYearType(base.HeadOfficeCode);

                                            if (AccountYearType == (int)AccountingYearType.FinancialYear)
                                            {                                              
                                                dtfromDate = new DateTime(this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Year, 4, 1);
                                                if (DateTime.Now.Month <= 3)
                                                {
                                                    dtfromDate = dtfromDate.AddYears(-1);
                                                }
                                                dtToDate = dtfromDate.AddYears(1).AddDays(-1);
                                                accountingPeriodSystem.InsertAndActivateAccYear(dtfromDate.ToString(), dtToDate.ToString());
                                            }
                                            else
                                            {
                                                dtfromDate = new DateTime(DateTime.Now.Year, 1, 1);
                                                dtToDate = new DateTime(DateTime.Now.Year, 12, 31);
                                                accountingPeriodSystem.InsertAndActivateAccYear(dtfromDate.ToString(), dtToDate.ToString());
                                            }
                                        }

                                        //Push head office admin details to user info table of head office data base and send mail to head office admin with login credentials
                                        if (status == (int)OfficeStatus.Created)
                                        {
                                            new ErrorLog().WriteError("Head Office Status created:" + status);
                                            if (SaveHeadOfficeAdminUserDetails(headOfficeCode))
                                            {
                                                //add newly created head office for routing..
                                                RouteTable.Routes.MapPageRoute(headOfficeCode, headOfficeCode, "~/Account/portal/Default.aspx");

                                                usercreatedstatus = (int)UserCreatedStatus.UserCreated;
                                                //Sending mail to head office admin

                                                string Name = "Admin";
                                                string Header = "Your Head Office account with Acme.erp has been activated successfully. You can login to the portal with the following details."
                                                                    + "<br/> Kindly change the password on first login by opeining Change password option under user menu.<br/>";
                                                string MainContent = "You can login to the portal using the below <br/><br/> URL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: " + ConfigurationManager.AppSettings["AcMESite"].ToString()
                                                                    + "login.php" + "<br /><br /> Head Office Code: " + headOfficeCode + "<br/><br/>"
                                                                    + "Username: " + headOfficeCode + "<br/><br/>"
                                                                    + "Password: " + RandonPassword + "<br/><br/> By logging into the portal you can <br/> <br/>"
                                                                    + "1.	Download Latest version of Acme.erp<br/>"
                                                                    + "2.	Download Master details<br/>"
                                                                    + "3.	Download License<br/>"
                                                                    + "4.	Upload vouchers entered<br/>"
                                                                    + "5.	Submit Support requests and more…<br/>";
                                                string content = CommonMethod.GetMailTemplate(Header, MainContent, Name);
                                                resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(EmailId.Trim()),
                                                    CommonMethod.RemoveFirstValue(EmailId.Trim()), "Head Office Activation Success", content,false);
                                                if (resultArgs.Success)
                                                {
                                                    //Update user created status in both database
                                                    usercreatedstatus = (int)UserCreatedStatus.UserCommunicated;
                                                    this.Message = MessageCatalog.Message.UserCommunicated + " and ";
                                                    new ErrorLog().WriteError("User Credentails Mail is sent and communicated");
                                                }
                                                else
                                                {
                                                    this.Message = MessageCatalog.Message.UserNotCommunicated + " and ";
                                                }
                                                headOfficeSystem.UserCreatedStatus = usercreatedstatus;
                                                headOfficeSystem.HeadOfficeCode = headOfficeCode;
                                                resultArgs = headOfficeSystem.UpdateUserCreatedStatus(DataBaseType.Portal);
                                                if (resultArgs.Success)
                                                {
                                                    //update in head office database
                                                    base.HeadOfficeCode = headOfficeCode;
                                                    resultArgs = headOfficeSystem.UpdateUserCreatedStatus(DataBaseType.HeadOffice);
                                                    //if (resultArgs.Success)
                                                    //Write it in log
                                                }

                                            }

                                        }
                                        this.Message += (status == (int)OfficeStatus.DeActivated || status == (int)OfficeStatus.Created) ?
                                        MessageCatalog.Message.HeadofficeActivated : MessageCatalog.Message.HeadofficeDeactivated;
                                        SetHeadOfficeViewSource();
                                        gvHeadOffice.BindGrid(headOfficeViewSource);

                                        //send mail to head office admin if head office is either activate or deactivate
                                        if (status != (int)OfficeStatus.Created)
                                        {
                                            SendHeadOfficeApproveStatus(headOfficeCode, (status == (int)OfficeStatus.DeActivated || status == (int)OfficeStatus.Created) ?
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
                        if (!string.IsNullOrEmpty(headOfficeCode))
                        {
                            if (SendMailToHeadOfficeAdmin(headOfficeCode))
                            {
                                this.Message = MessageCatalog.Message.HeadOfficeLoginInfo;
                            }
                            else
                            {
                                this.Message = MessageCatalog.Message.MailSendingFailure;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// <summary>
        /// Send Head Office Login Details to Head Office Admin to login.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <returns></returns>
        private bool SendMailToHeadOfficeAdmin(string headOfficeCode)
        {
            int UserCommunicationStatus = 0;
            bool IsMailSent = false;
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                ResultArgs resultArgs = null;
                resultArgs = headOfficeSystem.HeadOfficeDetailsByCode(headOfficeCode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataTable dtHeadOffice = resultArgs.DataSource.Table;
                    UserCommunicationStatus = this.Member.NumberSet.ToInteger(dtHeadOffice.Rows[0][this.AppSchema.HeadOffice.USER_CREATED_STATUSColumn.ColumnName].ToString());
                }
                //if user is not communicated ,send auto mail to Head office Admin to login.
                if (UserCommunicationStatus == 0)  //User is not created but head office is created.
                {
                    if (SaveHeadOfficeAdminUserDetails(headOfficeCode))
                    {
                        UserCommunicationStatus = (int)UserCreatedStatus.UserCreated;
                    }
                }
                if (UserCommunicationStatus == (int)UserCreatedStatus.UserCreated || UserCommunicationStatus == (int)UserCreatedStatus.UserCommunicated)
                {
                    DataTable dtUserInfo = null;
                    using (UserSystem userSystem = new UserSystem())
                    {
                        base.HeadOfficeCode = headOfficeCode;
                        userSystem.UserName = headOfficeCode;
                        resultArgs = userSystem.FetchUserDetailsByHeadOfficeCode(DataBaseType.HeadOffice);
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dtUserInfo = resultArgs.DataSource.Table;
                        }

                        //Sending mail to head office admin

                        string Name = "Admin";
                        string Header = "Your Head Office account with Acme.erp has been activated successfully. You can login to the portal with the following details."
                                            + "<br/> You will be asked to change the password when you login for the first time.<br/>";
                        string MainContent = "<b>You can login to the portal using the below,</b> <br/><br/> URL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: <b>" + ConfigurationManager.AppSettings["AcMESite"].ToString()
                                            + headOfficeCode + "</b><br /> Username: <b>" + headOfficeCode + "</b><br/>"
                                            + "Password: " + CommonMember.DecryptValue(dtUserInfo.Rows[0][this.AppSchema.User.PASSWORDColumn.ColumnName].ToString()) + "<br/><br/> <b>By logging into the portal you can</b> <br/> <br/>"
                                            + "1.	Download Latest version of Acme.erp<br/>"
                                            + "2.	Download Master details<br/>"
                                            + "3.	Download License<br/>"
                                            + "4.	Upload vouchers entered<br/>"
                                            + "5.	Submit Support requests and more…<br/>";
                        string content = CommonMethod.GetMailTemplate(Header, MainContent, Name);
                        resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString()),
                            CommonMethod.RemoveFirstValue(dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString()),
                            "Head Office Activation Success", content,false);
                        if (resultArgs.Success)
                        {
                            //Update user created status in both database
                            UserCommunicationStatus = (int)UserCreatedStatus.UserCreated;
                            IsMailSent = true;
                        }
                        headOfficeSystem.UserCreatedStatus = UserCommunicationStatus;
                        headOfficeSystem.HeadOfficeCode = headOfficeCode;
                        resultArgs = headOfficeSystem.UpdateUserCreatedStatus(DataBaseType.Portal);
                        if (resultArgs.Success)
                        {
                            //update in head office database
                            base.HeadOfficeCode = headOfficeCode;
                            resultArgs = headOfficeSystem.UpdateUserCreatedStatus(DataBaseType.HeadOffice);
                            if (resultArgs.Success)
                                //Write it in log
                                new ErrorLog().WriteError("HeadOfficeAdd.aspx.cs", "btnSaveHeadOffice_Click", "UserCreatedstatus is updated in user info table", "0");
                        }

                    }
                }
            }
            return IsMailSent;
        }

        #endregion

        #region Row Data Bound
        /// <summary>
        /// To Disable Edit and Delete for (Admin and Branch Office Admin)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvHeadOffice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlkEdit = (HyperLink)e.Row.FindControl("hlkEdit");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                ImageButton imgUpdateDB = (ImageButton)e.Row.FindControl("imgDB");
                ImageButton imgEmail = (ImageButton)e.Row.FindControl("imgEmail");
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
                }
                else
                {
                    if (imgStatus != null)
                    {
                        imgStatus.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.DeActivate_Confirm + "');";
                    }
                }
                if (imgUpdateDB != null)
                {
                    imgUpdateDB.ToolTip = MessageCatalog.Message.UpdateDBToolTip;
                    imgUpdateDB.OnClientClick = "javascript:return confirm('Are sure to update the database connection?');";
                }

            }

        }
        #endregion
        /// <summary>
        /// Save Head Office Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        protected void btnSaveDB_Click(object sender, EventArgs s)
        {
            if (!string.IsNullOrEmpty(ViewState["HOCODE"].ToString()))
            {
                using (HeadOfficeSystem hoSystem = new HeadOfficeSystem())
                {
                    ResultArgs resultargs;
                    hoSystem.DBName = txtDBName.Text.Trim();
                    hoSystem.DBPassword = txtDBPassword.Text.Trim();
                    hoSystem.DBUsername = txtDBUserName.Text.Trim();
                    hoSystem.HostName = txtHostName.Text.Trim();
                    hoSystem.HeadOfficeCode = ViewState["HOCODE"].ToString();
                    resultargs = hoSystem.UpdateDatabase();
                    if (resultargs.Success)
                    {
                        this.Message = MessageCatalog.Message.DatabaseInfoUpdated;
                    }
                }
            }
        }

        private void ImportDataFromExcel()
        {
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                ResultArgs resulArgs = headOfficeSystem.SaveHeadOfficeDetails(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                if (resulArgs.Success)
                {
                    this.Message = "Head Office details have been saved successfully.";
                }
            }
        }
        public void HeadOfficeSourceToExport()
        {
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                ResultArgs resulArgs = headOfficeSystem.FetchHeadOfficeToExport(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                if (resulArgs.Success)
                {
                    HeadOfficeSourceToExportExcel = resulArgs.DataSource.Table;
                }
            }
        }

        /// <summary>
        /// Fetch Head office Details
        /// </summary>
        private void SetHeadOfficeViewSource()
        {
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                ResultArgs resultArgs = headOfficeSystem.FetchHeadOfficeDetails(string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    headOfficeViewSource = resultArgs.DataSource.Table.DefaultView;
                    HeadOfficeSourceToExportExcel = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = headOfficeSystem.AppSchema.HeadOffice.HEAD_OFFICE_IDColumn.ColumnName;
                this.HOCodeColumn = headOfficeSystem.AppSchema.HeadOffice.STATUSColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn + "," + headOfficeSystem.AppSchema.HeadOffice.STATUSColumn.ColumnName;
            }
        }

        /// <summary>
        /// This is to save user details in the head office data base
        /// </summary>
        /// <param name="hocode"></param>
        /// <returns></returns>
        private bool SaveHeadOfficeAdminUserDetails(string hocode)
        {
            bool isUserCreated = false;
            ResultArgs result = null;
            DataTable dtUserInfo = new DataTable();
            try
            {

                using (HeadOfficeSystem hosystem = new HeadOfficeSystem())
                {
                    hosystem.HeadOfficeCode = hocode;
                    result = hosystem.FetchHeadOfficeAdmin(DataBaseType.Portal);
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
                            usersys.FirstName = dtUserInfo.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString().Trim(); ;
                            usersys.UserName = dtUserInfo.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString().Trim();
                            usersys.RoleId = (int)UserRole.Admin;
                            usersys.UserType = (int)UserRole.Admin;
                            RandonPassword = CommonMethod.GetRandomPassword();
                            usersys.Password = CommonMember.EncryptValue(RandonPassword);
                            usersys.MobileNo = dtUserInfo.Rows[0][this.AppSchema.HeadOffice.MOBILE_NOColumn.ColumnName].ToString().Trim();
                            usersys.Email = EmailId = dtUserInfo.Rows[0][this.AppSchema.HeadOffice.ORG_MAIL_IDColumn.ColumnName].ToString().Trim().ToLower();
                            usersys.status = (int)Status.Active;
                            usersys.PasswordStatus = (int)ResetPassword.AutomaticPassword;
                            usersys.HeadOfficeCode = hocode;
                            base.HeadOfficeCode = hocode;
                            result = usersys.SaveUser(DataBaseType.HeadOffice);
                            new ErrorLog().WriteError("Head Office User Created");
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

        private void ClearDBValues()
        {
            txtDBName.Text = txtDBUserName.Text = txtDBUserName.Text = string.Empty;
            txtDBPassword.Attributes["value"] = string.Empty;
        }
        /// <summary>
        /// This method fech and assigns the database connectivity details of each head office.
        /// </summary>
        private void AssignDatabseValuesToControls()
        {
            ResultArgs resultarg;
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                headOfficeSystem.HeadOfficeCode = ViewState["HOCODE"].ToString();
                resultarg = headOfficeSystem.FetchHeadOfficeDBDetails();
            }
            if (resultarg.Success)
            {
                DataTable dtdb = resultarg.DataSource.Table;
                if (dtdb != null)
                {
                    if (dtdb.Rows.Count > 0)
                    {
                        txtHostName.Text = dtdb.Rows[0][this.AppSchema.HeadOffice.HOST_NAMEColumn.ColumnName].ToString();
                        txtDBName.Text = dtdb.Rows[0][this.AppSchema.HeadOffice.DB_NAMEColumn.ColumnName].ToString();
                        txtDBUserName.Text = dtdb.Rows[0][this.AppSchema.HeadOffice.USERNAMEColumn.ColumnName].ToString();
                        txtDBPassword.Attributes["value"] = dtdb.Rows[0][this.AppSchema.HeadOffice.PASSWORDColumn.ColumnName].ToString();
                    }
                }
            }
        }
        /// <summary>
        /// Send Mail to Head office if Headoffice Status is changed.
        /// </summary>
        /// <param name="headofficeCode"></param>
        /// <param name="status"></param>
        private void SendHeadOfficeApproveStatus(string headofficeCode, string status)
        {
            using (HeadOfficeSystem headofficeSystem = new HeadOfficeSystem())
            {
                new ErrorLog().WriteError("Send Office Approved Status in SendHeadOfficeApproveStatus ");
                ResultArgs resultArgs = null;
                DataTable dtHeadOfficeInfo = null;
                resultArgs = headofficeSystem.HeadOfficeDetailsByCode(headofficeCode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtHeadOfficeInfo = resultArgs.DataSource.Table;
                    //Sending mail to head office admin
                    using (SettingSystem automail = new SettingSystem())
                    {
                        string Name = "Admin";
                        string Header = "Your Head Office is " + status + " successfully.";
                        string MainContent = "Your Head Office Code is: " + headofficeCode + "<br/>";

                        string content = CommonMethod.GetMailTemplate(Header, MainContent, Name);

                        resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(dtHeadOfficeInfo.Rows[0][this.AppSchema.HeadOffice.ORG_MAIL_IDColumn.ColumnName].ToString()),
                            CommonMethod.RemoveFirstValue(dtHeadOfficeInfo.Rows[0][this.AppSchema.HeadOffice.ORG_MAIL_IDColumn.ColumnName].ToString()),
                            "Head Office Activation Status", content,true);
                        if (resultArgs.Success)
                        {
                            new ErrorLog().WriteError("HeadOfficeView.aspx.cs", "SendHeadOfficeApproveStatus", "Status Updated Mail is sent", "0");
                        }
                    }
                }
            }
        }

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