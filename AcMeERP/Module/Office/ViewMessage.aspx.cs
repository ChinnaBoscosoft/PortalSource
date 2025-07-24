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
 * Purpose          :This page allows the branch office user to view the posted broad casted message and single branch office message
 *****************************************************************************************************/
using System;

using System.Web.UI.WebControls;
using Bosco.Utility;
using System.Data;
using AcMeERP.Base;
using Bosco.Model.UIModel;

namespace AcMeERP.Module.Office
{
    public partial class ViewMessage : Base.UIBase
    {
        #region Properties

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        private DataView MessageViewSource = null;
        private string targetPage = "";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string branchIdColumn = "";
        private string branchofficecodeColumn = "";

        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.SendMessage.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.ViewMessage;
            SetViewMessageSource();

            gvMessageView.RowCommand += new GridViewCommandEventHandler(gvMessageView_RowCommand);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlComposeCaption, false);
            linkUrl.ShowModelWindow = false;

            if (base.LoginUser.IsHeadOfficeAdminUser)
            {
                gvMessageView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, null, linkUrl);
                gvMessageView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Resend, this.rowIdColumn, "", linkUrl, "", CommandMode.Resend.ToString());
                // gvMessageView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            gvMessageView.HideColumn = this.hiddenColumn;
            gvMessageView.RowIdColumn = this.rowIdColumn;
            gvMessageView.DataSource = MessageViewSource;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.BranchLocation.MessageView;
                this.ShowLoadWaitPopUp();
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvMessageView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int roleId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (roleId > 0)
                {
                    using (BranchOfficeSystem branchLocationSystem = new BranchOfficeSystem())
                    {
                        //resultArgs = branchLocationSystem.DeleteLocation(roleId);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.BranchLocationDeleted;
                            SetViewMessageSource();
                            gvMessageView.BindGrid(MessageViewSource);
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

        #region Methods
        private void SetViewMessageSource()
        {
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                resultArgs = branchOfficeSystem.FetchAllMessageDetail();
                if (resultArgs != null && resultArgs.Success)
                {
                    MessageViewSource = resultArgs.DataSource.Table.DefaultView;
                }
                this.rowIdColumn = this.AppSchema.SendMessage.IDColumn.ColumnName;
                this.branchIdColumn = this.AppSchema.SendMessage.BRANCH_IDColumn.ColumnName;
                this.branchofficecodeColumn = this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn + "," + this.branchofficecodeColumn + "," + this.branchIdColumn;
            }
        }
        #endregion

    }
}