using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.IO;
using Bosco.Model.UIModel.Master;


namespace AcMeERP.Module.Master
{
    public partial class GoverningMemberView : Base.UIBase
    {

        #region Properties

        CommonMember UtilityMember = new CommonMember();
        private DataView GoverningMemberViewResource = null;
        private DataTable GoverningMemberViewSourceToExport = null;

        private const string EXECUTIVE_ID = "EXECUTIVE_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.GoverningMemberAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.GoverningMemberView;
            SetGoverningMemberViewSource();

            gvGMemberView.RowCommand += new GridViewCommandEventHandler(gvGMemberView_RowCommand);
            gvGMemberView.ExportClicked += new EventHandler(gvGMemberView_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvGMemberView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvGMemberView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvGMemberView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.GoverningMemberAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvGMemberView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.GoverningMemberEdit, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvGMemberView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.GoverningMemberDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvGMemberView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvGMemberView.HideColumn = this.hiddenColumn;
            gvGMemberView.RowIdColumn = this.rowIdColumn;
            gvGMemberView.DataSource = GoverningMemberViewResource;
        }

        protected void gvGMemberView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int executiveId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (executiveId > 0)
                {
                    using (ExecutiveMemberSystem executiveSystem = new ExecutiveMemberSystem())
                    {

                        resultArgs = executiveSystem.DeleteExecuteMember(executiveId, DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.GoverningMember.GoverningMemberDelete;
                            SetGoverningMemberViewSource();
                            gvGMemberView.BindGrid(GoverningMemberViewResource);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
        }

        protected void gvGMemberView_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "Governing Members" + DateTime.Now.Ticks.ToString();
                SetGoverningMemberViewSource();
                if (!GoverningMemberViewSourceToExport.Equals(null))
                {
                    GoverningMemberViewSourceToExport.Columns.Remove(EXECUTIVE_ID);
                    CommonMethod.WriteExcelFile(GoverningMemberViewSourceToExport, fileName);
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
                this.PageTitle = MessageCatalog.Message.GoverningMember.GoverningMemberPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.GoverningMemberView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                gvGMemberView.ShowExport = true;
                //this.ShowLoadWaitPopUp();
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }
        #endregion

        #region Methods
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
        private void SetGoverningMemberViewSource()
        {

            using (ExecutiveMemberSystem executiveSystem = new ExecutiveMemberSystem())
            {
                ResultArgs resultArgs = executiveSystem.FetchExecutiveMemberDetails(DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    GoverningMemberViewResource = resultArgs.DataSource.Table.DefaultView;
                    GoverningMemberViewSourceToExport = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = executiveSystem.AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
        #endregion


    }
}