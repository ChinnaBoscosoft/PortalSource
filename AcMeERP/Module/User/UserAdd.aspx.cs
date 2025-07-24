using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.User
{
    public partial class UserAdd : Base.UIBase
    {
        private int UserId
        {
            get
            {
                int userId = this.Member.NumberSet.ToInteger(this.RowId);
                return userId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }
        private int EditRoleId = 0;
        private int RoleId
        {
            get
            {
                int roleId = ddlRole.Visible ? this.Member.NumberSet.ToInteger(ddlRole.SelectedValue) :
                     this.Member.NumberSet.ToInteger(ViewState["EditRoleId"].ToString());
                return roleId;
            }
            set
            {
                if (ddlRole.Visible)
                {
                    this.Member.ComboSet.SelectComboItem(ddlRole, value.ToString(), true);
                }
                else
                {

                    ViewState["EditRoleId"] = value;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                hlkClose.PostBackUrl = this.ReturnUrl;
                BindRole();//To bind Role Group followed by Role
                SetPageTitle();
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;

                if (UserId > 0)
                {
                    SetUserSource();
                    btnNew.Visible = false;
                    pnlLogin.Visible = false;
                }
                SetControlFocus();

                this.ShowLoadWaitPopUp(btnSaveUser);
            }
        }


        private void BindRole()
        {
            using (UserSystem userSystem = new UserSystem())
            {
                ResultArgs resultArgs = new ResultArgs();
                if (base.LoginUser.IsPortalUser)
                {
                    userSystem.Accessibility = (int)Accessibility.Both;
                }
                else if (base.LoginUser.IsHeadOfficeUser)
                {
                    userSystem.Accessibility = (int)Accessibility.HeadOffice;
                }
                else if (base.LoginUser.IsBranchOfficeUser)
                {
                    userSystem.Accessibility = (int)Accessibility.BranchOffice;
                }

                if (!base.LoginUser.IsHeadOfficeAdminUser)
                {
                    resultArgs = userSystem.FetchRoleListById(base.LoginUser.LoginUserRoleId, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                }
                else
                {
                    resultArgs = userSystem.FetchRoleListByIdWithAdmin(base.LoginUser.LoginUserRoleId, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                }
                if (resultArgs.Success)
                {
                    this.Member.ComboSet.BindDataCombo(ddlRole, resultArgs.DataSource.Table
                    , this.AppSchema.UserRole.USERROLEColumn.ColumnName
                    , this.AppSchema.UserRole.USERROLE_IDColumn.ColumnName
                    , true, CommonMember.SELECT);
                }
            }
        }


        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.User.UserEditPageTitle : MessageCatalog.Message.User.UserAddPageTitle));
        }

        private void SetControlFocus()
        {
            this.SetControlFocus(txtName);//Set the control focus
        }

        public void ClearValues()
        {
            UserId = RoleId = EditRoleId = 0;
            lblRole.Visible = false;
            ddlRole.Visible = cmpRole.Visible = true;
            txtUserName.Text = txtName.Text = txtAddress.Text = txtContact.Text = txtEmail.Text = txtnotes.Text = lblRole.Text = string.Empty;
            txtCountryCode.Text = CommonMember.CountryCode;
            //txtPlace.Text=txtCity.Text=string.Empty
            txtLastname.Text = string.Empty;
            SetPassword(txtPassword, "");
            SetPassword(txtConfirmPassword, "");
            SetPageTitle();
            this.SetControlFocus();
            checkusername.Visible = false;
            pnlLogin.Visible = true;
        }

        private void SetUserSource()
        {
            //Fill the controls with values
            using (UserSystem userSystem = new UserSystem())
            {
                ddlRole.Enabled = (UserId != 1); // Cannot change role for Admin User  

                if (UserId > 0)
                {
                    //Fill the controls with values
                    using (UserSystem usersys = new UserSystem(UserId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                    {
                        txtUserName.Text = usersys.UserName;
                        txtName.Text = usersys.FirstName;
                        txtLastname.Text = usersys.LastName;
                        SetPassword(txtPassword, CommonMember.DecryptValue(usersys.Password));
                        SetPassword(txtConfirmPassword, CommonMember.DecryptValue(usersys.Password));
                        if (base.LoginUser.IsHeadOfficeUser)
                        {
                            if ((UserRole)usersys.RoleId == UserRole.Admin || (UserRole)usersys.RoleId == UserRole.BranchAdmin)
                            {
                                lblRole.Text = ((UserRole)usersys.RoleId).ToString();
                                lblRole.Visible = true;
                                cmpRole.Visible = ddlRole.Visible = false;
                            }
                        }
                        RoleId = usersys.RoleId;
                        txtAddress.Text = usersys.Address;
                        txtnotes.Text = usersys.Notes;
                        txtContact.Text = usersys.MobileNo;
                        txtEmail.Text = usersys.Email;
                        txtCountryCode.Text = string.IsNullOrEmpty(usersys.CountryCode) ? "91" : usersys.CountryCode;
                    }
                }
            }
        }

        private void SetPassword(TextBox txtControl, string password)
        {
            txtControl.Attributes["value"] = password;
        }
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            if (!string.IsNullOrEmpty(userName))
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    ResultArgs resultArgs = null;
                    userSystem.UserName = userName;
                    resultArgs = userSystem.FetchUserDetailsByHeadOfficeCode(base.LoginUser.LoginUserHeadOfficeCode == "" ? DataBaseType.Portal : DataBaseType.HeadOffice);

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        checkusername.Visible = true;
                        lblStatus.Text = MessageCatalog.Message.UserNameAvailable;
                        this.SetControlFocus(txtUserName);
                    }
                    else
                    {
                        checkusername.Visible = false;
                        this.SetControlFocus(txtPassword);
                    }

                }
            }
            else
            {
                checkusername.Visible = false;
                this.SetControlFocus(txtPassword);
            }
        }


        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ResultArgs result = new ResultArgs();
                using (UserSystem usersys = new UserSystem())
                {
                    usersys.UserId = UserId == 0 ? (int)AddNewRow.NewRow : UserId;
                    usersys.FirstName = txtName.Text.Trim();
                    usersys.LastName = txtLastname.Text.Trim();
                    usersys.UserName = txtUserName.Text.Trim();
                    usersys.RoleId = RoleId;
                    usersys.Password = CommonMember.EncryptValue(txtPassword.Text.Trim());
                    usersys.Address = txtAddress.Text.Trim();
                    //usersys.City = txtCity.Text.Trim();
                    // usersys.Place = txtPlace.Text.Trim();
                    usersys.MobileNo = txtContact.Text.Trim();
                    usersys.Email = txtEmail.Text.Trim().ToLower();
                    usersys.Notes = txtnotes.Text.Trim();
                    usersys.status = (int)Status.Active;
                    usersys.Created_By = base.LoginUser.LoginUserId;
                    usersys.UserType = 0;//Need to change
                    usersys.CountryCode = txtCountryCode.Text.Trim();
                    if (base.LoginUser.IsPortalUser)
                    {
                        usersys.HeadOfficeCode = usersys.BranchCode = string.Empty;
                    }
                    else if (base.LoginUser.IsHeadOfficeUser)
                    {
                        usersys.HeadOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                        //Head Office User Head Office Code and Branch Office Code
                    }
                    else if (base.LoginUser.IsBranchOfficeUser)
                    {
                        usersys.HeadOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                        usersys.BranchCode = base.LoginUser.LoginUserBranchOfficeCode;
                    }
                    result = usersys.SaveUser(string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                    if (result.Success)
                    {
                        this.Message = MessageCatalog.Message.UserSaved;

                        if (UserId == 0)
                        {
                            UserId = this.Member.NumberSet.ToInteger(result.RowUniqueId.ToString());
                            ClearValues();
                        }
                    }
                    else
                        this.Message = result.Message;
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }
    }
}