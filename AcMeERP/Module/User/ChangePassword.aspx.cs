using System;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.User
{
    public partial class ChangePassword : Base.UIBase
    {
        #region Property
        private string Password
        {
            get;
            set;
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.User.ChangePasswordPageTitle;
                this.SetControlFocus(CurrentPassword);
                CancelPushButton.Visible = (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword) ? false : true;
                Menu mnuBar = (Menu)Master.FindControl("mnuTop");
                mnuBar.Visible = (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword) ? false : true;
                Notifydiv.Visible = (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword) ? true : false;
                this.ShowLoadWaitPopUp(ChangePasswordPushButton);
            }
        }
        protected void CurrentPassword_TextChanged(object sender, EventArgs e)
        {
            CheckCurrentPassword();
            Password = CurrentPassword.Text;
        }
        protected void ChangePasswordPushButton_click(object sender, EventArgs e)
        {
            try
            {
                ResultArgs resultArgs = null;
                if (CheckCurrentPassword())
                {
                    using (UserSystem userSystem = new UserSystem())
                    {
                        resultArgs = userSystem.ResetPassword(base.LoginUser.LoginUserId, CommonMember.EncryptValue(NewPassword.Text), (int)ResetPassword.ResetPassword, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            resultArgs = userSystem.AuthenticateUser(base.LoginUser.LoginUserName, CommonMember.EncryptValue(NewPassword.Text), base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                            string navUrl = this.GetPageUrlByName(URLPages.ChangePasswordSuccess.ToString());
                            Response.Redirect(navUrl, false);
                            Menu mnuBar = (Menu)Master.FindControl("mnuTop");
                            mnuBar.Visible = true;
                        }
                        if (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword)
                        {
                            string navUrl = this.GetPageUrlByName(URLPages.ChangePassword.ToString());
                            Response.Redirect(navUrl, false);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("ChangePassword", "ChangePassword.aspx", ex.StackTrace, "0");
                this.Message = ex.Message;

            }
            finally
            {
            }

        }
        protected void CancelPushButton_click(object sender, EventArgs e)
        {
            string navUrl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
            Response.Redirect(navUrl);
        }

        #endregion

        #region Methods

        private bool CheckCurrentPassword()
        {
            bool success = false;
            ResultArgs result = new ResultArgs();

            using (UserSystem usersystem = new UserSystem())
            {
                result = usersystem.CheckCurrentPassword(base.LoginUser.LoginUserId, CommonMember.EncryptValue(CurrentPassword.Text), base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
            }

            if (result.Success && result.RowsAffected > 0 && result != null)
            {
                success = true;
            }
            else
            {
               this.Message = MessageCatalog.Message.EnterCorrectPassword;
                success = false;
                SetControlFocus(CurrentPassword);
            }
            return success;
        }

        #endregion

    }
}
