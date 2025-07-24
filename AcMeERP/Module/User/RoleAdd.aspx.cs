using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;


namespace AcMeERP.Module.User
{
    public partial class RoleAdd : Base.UIBase
    {
        #region Property

        private int UserRoleID
        {
            get
            {
                int UserRoleId = this.Member.NumberSet.ToInteger(this.RowId);
                return UserRoleId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                SetControlFocus();
                hlkClose.PostBackUrl = this.ReturnUrl;
                if (UserRoleID > 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                this.ShowLoadWaitPopUp(btnSaveRole);
            }
        }
        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidRoleDetails())
                {
                    ResultArgs resultArgs = null;
                    using (UserRoleSystem userRoleSystem = new UserRoleSystem())
                    {
                        if (base.LoginUser.IsPortalUser)
                        {
                            userRoleSystem.Accessibility = (int)Accessibility.Both;
                        }
                        else if (base.LoginUser.IsHeadOfficeUser)
                        {
                            userRoleSystem.Accessibility = (int)Accessibility.HeadOffice;
                        }
                        else if (base.LoginUser.IsBranchOfficeUser)
                        {
                            userRoleSystem.Accessibility = (int)Accessibility.BranchOffice;
                        }
                        userRoleSystem.UserRoleId = UserRoleID == 0 ? (int)AddNewRow.NewRow : UserRoleID;
                        userRoleSystem.UserRoleName = txtRoleName.Text.Trim();
                        resultArgs = userRoleSystem.SaveUserRoles(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.RoleSaved;
                            if (UserRoleID == 0)
                            {
                                UserRoleID = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                ClearControls();
                            }
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
                this.Message = ex.Message;
            }
            finally { }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        #endregion

        #region Methods

        private void AssignValuesToControls()
        {
            using (UserRoleSystem roleSystem = new UserRoleSystem(UserRoleID, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
            {
                txtRoleName.Text = roleSystem.UserRoleName;
            }
        }
        private void ClearControls()
        {
            UserRoleID = 0;
            txtRoleName.Text = string.Empty;
            SetPageTitle();
            SetControlFocus(txtRoleName);
        }
        private bool ValidRoleDetails()
        {
            bool isvalid = true;
            if (string.IsNullOrEmpty(txtRoleName.Text))
            {
                this.Message = MessageCatalog.Message.Role.RoleNameFieldEmpty;
                isvalid = false;
            }
            return isvalid;
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.Role.RoleEditPageTitle : MessageCatalog.Message.Role.RoleAddPageTitle));
        }

        private void SetControlFocus()
        {
            this.SetControlFocus(txtRoleName);//Set the control focus
        }

        #endregion

    }
}