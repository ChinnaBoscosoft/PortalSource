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
 * Purpose          :This page helps the HO admin to maintain the default FC purpose and sent all the purpose as master data to the branch office.
 *****************************************************************************************************/
using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
namespace AcMeERP.Module.Master
{
    public partial class FCPurposeView : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        private DataView ProjectViewResource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.FCPurpose.FCPurposeViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.FCPurposeView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                // this.ShowLoadWaitPopUp();
                gvPurpose.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.FCPurposeAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.FCPurposeView;
            SetProjectViewSource();
            gvPurpose.RowCommand += new GridViewCommandEventHandler(gvPurpose_RowCommand);
            // gvPurpose.HideColumn = this.hiddenColumn;
            //gvPurpose.RowIdColumn = this.rowIdColumn;
            //gvPurpose.DataSource = ProjectViewResource;

            gvPurpose.RowCommand += new GridViewCommandEventHandler(gvPurpose_RowCommand);
            //gvPurpose.ExportClicked += new EventHandler(gv);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvPurpose.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvPurpose.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvPurpose.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.FCPurposeAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvPurpose.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.FCPurposeEdit, true,
                           base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvPurpose.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.FCPurposeDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvPurpose.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvPurpose.HideColumn = this.hiddenColumn;
            gvPurpose.RowIdColumn = this.rowIdColumn;
            gvPurpose.DataSource = ProjectViewResource;
        }

        protected void gvPurpose_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ResultArgs resultArgs = new ResultArgs();
            int PurposeId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (PurposeId != 0)
                {
                    using (PurposeSystem PurposeSystem = new PurposeSystem())
                    {
                        resultArgs = PurposeSystem.DeletePurposeDetails(PurposeId, DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.FCPurpose.FCPurposeDeleteConformation;
                            SetProjectViewSource();
                            gvPurpose.BindGrid(ProjectViewResource);
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.Delete_failure;
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void SetProjectViewSource()
        {
            using (PurposeSystem PurposeSystem = new PurposeSystem())
            {
                ResultArgs resultArgs = PurposeSystem.FetchPurposeDetails(DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    ProjectViewResource = resultArgs.DataSource.Table.DefaultView;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = this.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }

        #endregion
    }
}