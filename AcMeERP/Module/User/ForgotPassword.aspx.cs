using System;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model;

namespace AcMeERP.Module.User
{
    public partial class ForgotPassword : Base.UIBase
    {
        #region Delcaration
        ResultArgs resultArgs = null;
        #endregion

        #region Property

        //Stores the security Code in session which is sent to user via Mail/SMS
        private int SecurityCode
        {
            get
            {
                int securityCode = 0;
                if (!string.IsNullOrEmpty(Session["SecurityCode"].ToString()))
                {
                    securityCode = this.Member.NumberSet.ToInteger(Session["SecurityCode"].ToString());
                }
                return securityCode;
            }
            set { Session["SecurityCode"] = value; }
        }
        private DataTable UserInfo
        {
            get
            {
                DataTable dtUserInfo = null;
                if (ViewState["dtUserInfo"] != null)
                {
                    dtUserInfo = ViewState["dtUserInfo"] as DataTable;
                }
                return dtUserInfo;
            }

            set
            {
                ViewState["dtUserInfo"] = value;
            }
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.User.ForgotPasswordPageTitle;
                this.SetControlFocus(txtUserName);
                this.ShowLoadWaitPopUp();
                SecurityCode = 0;
                this.SetControlFocus(txtUserName);
                txtUserName.Text = string.Empty;
                UserInfo = null;
            }
        }
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    base.HeadOfficeCode = string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal.ToString() : base.LoginUser.LoginUserHeadOfficeCode;
                    resultArgs = userSystem.FetchUserId(txtUserName.Text, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        UserInfo = resultArgs.DataSource.Table;
                        pnlConfirmationmsg.Visible = true;
                        txtUserName.Enabled = false;
                    }
                    else if (resultArgs.RowsAffected == 0)
                    {
                        this.Message = MessageCatalog.Message.EnterCorrectUserName;
                    }
                }
            }
        }
        protected void imgbtnRefresh_Click(object sender, EventArgs e)
        {
            txtConfirmationCode.Enabled = true;
            txtConfirmationCode.Text = string.Empty;
            SendSecurityCode();
        }
        protected void rblConfirmation_SelectedIndexChanged(object sender, EventArgs e)
        {
            SendSecurityCode();
        }

        private void SendSecurityCode()
        {
            SecurityCode = CommonMethod.GetSecurityKey();
            if (SecurityCode != 0 && SecurityCode.ToString().Length == 4)
            {
                if (SendConfirmationCode(this.Member.NumberSet.ToInteger(rblConfirmation.SelectedValue) == (int)ConfirmationMode.SMS ? ConfirmationMode.SMS : ConfirmationMode.Email))
                {
                    pnlConfirmationCode.Visible = true;
                }
                else
                {
                    this.Message = MessageCatalog.Message.EnterCorrectConfirmationCode;
                }
            }
        }
        protected void txtConfirmationCode_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtConfirmationCode.Text.Trim())))
            {
                if (SecurityCode== this.Member.NumberSet.ToInteger(txtConfirmationCode.Text.Trim()))
                {
                    txtConfirmationCode.Enabled = false;
                    pnlResetPassword.Visible = true;
                    pnlbutton.Visible = true;
                }
            }
        }
        protected void SetPasswordButton_click(object sender, EventArgs e)
        {
            using (UserSystem userSystem = new UserSystem())
            {
                if (!(string.IsNullOrEmpty(txtUserName.Text.Trim())))
                {
                    base.HeadOfficeCode = string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal.ToString() : base.LoginUser.LoginUserHeadOfficeCode;
                    resultArgs = userSystem.ResetPassword(txtUserName.Text.Trim(), CommonMember.EncryptValue(NewPassword.Text), (int)ResetPassword.ResetPassword,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                }

                if (resultArgs.Success)
                {
                    this.Message = MessageCatalog.Message.ChangedPassword;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }
            }


        }
        protected void CancelPushButton_click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomeLogin.aspx");
        }

        #endregion

        #region Method
        private bool SendConfirmationCode(ConfirmationMode cfMode)
        {
            bool isCodeSent = false;

            if (cfMode == ConfirmationMode.SMS)
            {
                using (SettingSystem autoSMS = new SettingSystem())
               {
                   resultArgs= autoSMS.SendSMS(UserInfo.Rows[0][this.AppSchema.User.CONTACT_NOColumn.ColumnName].ToString(), SecurityCode.ToString());
                   if (resultArgs.Success)
                   {
                       isCodeSent = true;
                   }
                   else
                   {
                       this.Message = resultArgs.Message;
                   }
               }
            }
            else if(cfMode==ConfirmationMode.Email)
            {
                 using (SettingSystem automail = new SettingSystem())
                    {
                        string content = "Confirmation Code to recover your password :"+SecurityCode
                                            + "<br/><br/>"
                                            + "Thank You!<br />";
                        resultArgs = automail.SendEmail(UserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString(),
                            "", "AcMeERP Portal Confirmation Code", content);
                        if (resultArgs.Success)
                        {
                            isCodeSent = true;
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                 }
            }
            return isCodeSent;
        }
        #endregion
    }
}