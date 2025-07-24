using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;

namespace AcMeERP.Module.User
{
    public partial class UserView : Base.UIBase
    {
        #region Decalarations

        CommonMember UtilityMember = new CommonMember();
        private DataView userViewSource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.UserAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.UserView;
            SetUserViewSource();

            gvUser.RowCommand += new GridViewCommandEventHandler(gvUser_RowCommand);
            gvUser.RowDataBound += new GridViewRowEventHandler(gvUser_RowDataBound);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;
           
            gvUser.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
            gvUser.SetTemplateColumn(ControlType.ImageButton, CommandMode.Status, this.rowIdColumn, "", null, "", CommandMode.Status.ToString());
            gvUser.SetTemplateColumn(ControlType.ImageButton, CommandMode.Reset, this.rowIdColumn, "", null, "", CommandMode.Reset.ToString());
            gvUser.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
            gvUser.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());

            gvUser.HideColumn = this.hiddenColumn;
            gvUser.RowIdColumn = this.rowIdColumn;
            gvUser.DataSource = userViewSource;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.User.UserViewPageTitle;
                this.CheckUserRights(RightsModule.User, RightsActivity.UserView, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.ShowLoadWaitPopUp();
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
        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            string[] rowIds = e.CommandArgument.ToString().Split(',');
            int userId = this.Member.NumberSet.ToInteger(rowIds[0]);
            this.RowId = userId.ToString();
            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (userId != 1) //Not an Admin User
                {
                    using (UserSystem userSystem = new UserSystem())
                    {
                        if (!rowIds[1].Equals("1"))
                        {
                            resultArgs = userSystem.DeleteUser(userId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                            if (resultArgs.Success)
                            {
                                this.Message = MessageCatalog.Message.UserDeleted;

                                SetUserViewSource();
                                gvUser.BindGrid(userViewSource);
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.NotActiveUserDelete;
                        }
                    }
                }
                else
                {
                    this.Message = "Cannot Delete Admin User";
                }
            }
            else if (e.CommandName == CommandMode.Status.ToString())
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    userSystem.status = rowIds[1].Equals("1") ? (int)Status.Inactive : (int)Status.Active;
                    resultArgs = userSystem.UpdateUserStatus(userId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {

                        this.Message = "User " + (userSystem.status > 0 ? "Activated" : "Deactivated");
                        SetUserViewSource();
                        gvUser.BindGrid(userViewSource);
                    }
                }
            }
            //To reset the password
            else if (e.CommandName == CommandMode.Reset.ToString())
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ResetPassword", "javascript:ShowDisplayPopUp();", true);
                using (UserSystem userSystem = new UserSystem())
                {
                    resultArgs = userSystem.ResetPassword(this.Member.NumberSet.ToInteger(this.RowId), CommonMember.EncryptValue(CommonMethod.GetRandomPassword()),
                        (int)ResetPassword.AutomaticPassword, base.LoginUser.IsPortalUser ? DataBaseType.Portal : DataBaseType.HeadOffice);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        this.Message = MessageCatalog.Message.ResetPasswordSuccess;
                        SendMail(this.Member.NumberSet.ToInteger(this.RowId), base.LoginUser.IsPortalUser ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    }
                    else
                        this.Message = resultArgs.Message;
                }
            }
        }

        #region Row Data Bound
        /// <summary>
        /// To Disable Edit and Delete for (Admin and Branch Office Admin)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlkEdit = (HyperLink)e.Row.FindControl("hlkEdit");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                ImageButton imgReset = (ImageButton)e.Row.FindControl("imgReset");
                if (imgStatus != null && imgDelete != null && hlkEdit != null && imgReset != null)
                {
                    string[] status = imgStatus.CommandArgument.Split(',');
                    if (status[1].Trim().Equals("0"))
                    {
                        imgStatus.ImageUrl = "~/App_Themes/MainTheme/images/activate.gif";
                        imgStatus.ToolTip = MessageCatalog.Message.ActivateToolTip;
                        imgStatus.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.Activate_Confirm + "');";
                    }
                    else
                    {
                        imgStatus.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.DeActivate_Confirm + "');";
                    }
                    imgReset.OnClientClick = "javascript:return confirm('Are you sure to reset the password?');";
                }
            }

        }
        #endregion

        #endregion

        private void SetUserViewSource()
        {
            using (UserSystem userSystem = new UserSystem())
            {
                userSystem.Created_By = base.LoginUser.LoginUserId;
                userSystem.BranchCode = base.LoginUser.LoginUserBranchOfficeCode;
                ResultArgs resultArgs = userSystem.FetchUserDetail(string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    userViewSource = resultArgs.DataSource.Table.DefaultView;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = userSystem.AppSchema.User.USER_IDColumn.ColumnName;
                if (base.LoginUser.IsPortalUser)
                {
                    this.hiddenColumn = this.rowIdColumn + "," + this.AppSchema.User.HEAD_OFFICEColumn.ColumnName + "," + this.AppSchema.User.BRANCH_OFFICEColumn.ColumnName;
                }
                else if (base.LoginUser.IsHeadOfficeUser)
                {
                    this.hiddenColumn = this.rowIdColumn + "," + this.AppSchema.User.HEAD_OFFICEColumn.ColumnName;
                }
                else
                {
                    this.hiddenColumn = this.rowIdColumn;
                }
            }
        }
        private bool SendMail(int UserId, DataBaseType connectTo)
        {
           
            bool IsMailSuccess = false;
            ResultArgs resultArgs = null;
            DataTable dtUserInfo = null;
            //Sending mail to user if admin changes their password
            try
            {
                using (UserSystem userSystem = new UserSystem(UserId))
                {
                    resultArgs = userSystem.FetchUserDetailsById(connectTo);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dtUserInfo = resultArgs.DataSource.Table;
                        string Name = " " + dtUserInfo.Rows[0][this.AppSchema.User.USER_NAMEColumn.ColumnName].ToString();
                        string Header = "Your Password has been reset successfully. You can login to the portal by using following details."
                                            + "<br/>";
                        string MainContent = "Username: <b>" + dtUserInfo.Rows[0][this.AppSchema.User.USER_NAMEColumn.ColumnName].ToString() + "</b><br/>"
                                            + "<br/> Password: " + CommonMember.DecryptValue(dtUserInfo.Rows[0][this.AppSchema.User.PASSWORDColumn.ColumnName].ToString())
                                            + "<br/><br/>";
                        string content = CommonMethod.GetMailTemplate(Header, MainContent, Name);
                        resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString()), CommonMethod.RemoveFirstValue(dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString()),
                            "Password Reset Success for Acme.erp Portal", content,false);
                        if (resultArgs.Success)
                        {
                            IsMailSuccess = true;
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
            return IsMailSuccess;
        }
    }
}