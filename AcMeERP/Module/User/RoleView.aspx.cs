using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;

namespace AcMeERP.Module.User
{
    public partial class RoleView : Base.UIBase
    {
        CommonMember UtilityMember = new CommonMember();
        private DataView roleViewSource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.RoleAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.RoleView;
            SetUserRoleViewSource();

            gvRole.RowCommand += new GridViewCommandEventHandler(gvRole_RowCommand);
            gvRole.RowDataBound += new GridViewRowEventHandler(gvRole_RowDataBound);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            gvRole.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
            gvRole.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
            gvRole.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());

            gvRole.HideColumn = this.hiddenColumn;
            gvRole.RowIdColumn = this.rowIdColumn;
            gvRole.DataSource = roleViewSource;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Role.RoleViewPageTitle;
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
        protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int roleId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (roleId > 0)
                {
                    using (UserRoleSystem roleSystem = new UserRoleSystem())
                    {
                        resultArgs = roleSystem.DeleteUserRole(roleId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.UserRoleDeleted;
                            SetUserRoleViewSource();
                            gvRole.BindGrid(roleViewSource);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }

            }
        }

        #endregion

        #region Row Data Bound
        /// <summary>
        /// To Disable Edit and Delete for (Admin and Branch Office Admin)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlkEdit = (HyperLink)e.Row.FindControl("hlkEdit");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                if (imgDelete != null && hlkEdit != null)
                {
                    if (e.Row.Cells[0].Text.Equals("Admin") || e.Row.Cells[0].Text.Equals("Branch Admin"))
                    {
                        hlkEdit.Enabled = false;
                        imgDelete.Enabled = false;
                        hlkEdit.ImageUrl = "~/App_Themes/MainTheme/images/GridEdit.png";
                        imgDelete.ImageUrl = "~/App_Themes/MainTheme/images/DeleteDisable.png";
                    }
                }

            }

        }
        #endregion

        private void SetUserRoleViewSource()
        {
            using (UserRoleSystem roleSystem = new UserRoleSystem())
            {
                if (base.LoginUser.IsHeadOfficeUser)
                {
                    roleSystem.Accessibility = (int)Accessibility.HeadOffice;
                }
                else if (base.LoginUser.IsBranchOfficeUser)
                {
                    roleSystem.Accessibility = (int)Accessibility.BranchOffice;
                }
                ResultArgs resultArgs = roleSystem.FetchUserRoleDetails(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    roleViewSource = resultArgs.DataSource.Table.DefaultView;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = roleSystem.AppSchema.UserRole.USERROLE_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
    }
}