using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.User
{
    public partial class UserProfile : Base.UIBase
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                hlkClose.PostBackUrl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
                // this.PageTitle = "My Profile";
                if (base.LoginUser.LoginUserId > 0)
                {
                    SetUserSource();
                }
                SetControlFocus();
                
            }
            //Lock User to change Password if AutoPassword is set to User
            CancelPushButton.Visible = (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword) ? false : true;
            Menu mnuBar = (Menu)Master.FindControl("mnuTop");
            mnuBar.Visible = (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword) ? false : true;
            Notifydiv.Visible = (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword) ? true : false;
            this.ShowLoadWaitPopUp(btnSaveUser);
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ResultArgs result = new ResultArgs();
                using (UserSystem usersys = new UserSystem())
                {
                    usersys.UserId = base.LoginUser.LoginUserId;
                    usersys.FirstName = txtName.Text.Trim();
                    usersys.LastName = txtLastname.Text.Trim();
                    usersys.Address = txtAddress.Text.Trim();
                    //usersys.City = txtCity.Text.Trim();
                    //usersys.Place = txtPlace.Text.Trim();
                    usersys.MobileNo = txtContact.Text.Trim();
                    usersys.Email = txtEmail.Text.Trim().ToLower();
                    usersys.Notes = txtnotes.Text.Trim();
                    usersys.CountryCode = !string.IsNullOrEmpty(txtCountryCode.Text.Trim()) ? txtCountryCode.Text.Trim() : string.Empty;
                    result = usersys.UpdateProfile(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                    if (result.Success)
                    {
                        this.Message = MessageCatalog.Message.ProfileUpdated;
                        SetUserSource();
                    }
                    else
                        this.Message = result.Message;
                }
            }
        }
        protected void profile_Click(object sender, EventArgs e)
        {
            Profilediv.Visible = true;
            changePwddiv.Visible = false;
            SetControlFocus();
        }
        protected void changepwd_Click(object sender, EventArgs e)
        {
            Profilediv.Visible = false;
            changePwddiv.Visible = true;
            this.SetControlFocus(CurrentPassword);
        }
        #endregion

        #region Methods
        private void SetControlFocus()
        {
            // this.SetControlFocus(txtName);//Set the control focus
        }

        private void SetUserSource()
        {
            //Fill the controls with values
            using (UserSystem userSystem = new UserSystem())
            {

                if (base.LoginUser.LoginUserId > 0)
                {
                    //Fill the controls with values
                    using (UserSystem usersys = new UserSystem(base.LoginUser.LoginUserId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                    {
                        #region label
                        //ltFirstName.Text = usersys.FirstName;
                        //ltLastName.Text = usersys.LastName;
                        //ltrAddress.Text = usersys.Address;
                        //ltrNote.Text = usersys.Notes;
                        //ltrMobileNo.Text = usersys.MobileNo;
                        //ltrEmail.Text = usersys.Email;
                        //ltrRole.Text = usersys.UserRole;
                        //ltrStatus.Text = usersys.UserStatus;
                        #endregion

                        txtName.Text = usersys.FirstName;
                        txtLastname.Text = usersys.LastName;
                        txtAddress.Text = usersys.Address;
                        txtnotes.Text = usersys.Notes;
                        txtContact.Text = usersys.MobileNo;
                        txtEmail.Text = usersys.Email;
                        ltRole.Text = usersys.UserRole;
                        ltStatus.Text = usersys.UserStatus;
                        txtCountryCode.Text = string.IsNullOrEmpty(usersys.CountryCode.ToString()) ? "91" : usersys.CountryCode.ToString();
                    }
                }
            }
        }


        #endregion

        #region changepassword
        protected void ChangePasswordPushButton_click(object sender, EventArgs e)
        {
            ResultArgs resultArgs = null;
            using (UserSystem userSystem = new UserSystem())
            {

                if (CheckCurrentPassword())
                {
                    resultArgs = userSystem.ResetPassword(base.LoginUser.LoginUserId, CommonMember.EncryptValue(NewPassword.Text), (int)ResetPassword.ResetPassword, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                    if (resultArgs.Success)
                    {
                        resultArgs = userSystem.AuthenticateUser(base.LoginUser.LoginUserName, CommonMember.EncryptValue(NewPassword.Text), 
                            base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                        string navUrl = this.GetPageUrlByName(URLPages.ChangePasswordSuccess.ToString());
                        Response.Redirect(navUrl, false);

                        //Lock User to change Password if AutoPassword is set to User
                        Menu mnuBar = (Menu)Master.FindControl("mnuTop");
                        mnuBar.Visible = true;
                    }
                    //Lock User to change Password if AutoPassword is set to User
                    if (base.LoginUser.LoginUserPasswordStatus == (int)ResetPassword.AutomaticPassword)
                    {
                        string retUrl = URLPages.Default.ToString(NumberFormatInfo.Number);
                        string navUrl = this.GetPageUrlByName(URLPages.ChangePassword.ToString());
                        Response.Redirect(navUrl);
                    }
                }
            }

        }
        protected void CancelPushButton_click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomeLogin.aspx");
        }



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
            }
            return success;
        }


        #endregion
        #endregion
    }
}