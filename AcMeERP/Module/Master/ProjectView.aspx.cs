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
 * Purpose          :This page helps head office admin/user or branch office admin/user to view projects available by the role wise
 *****************************************************************************************************/
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.IO;

namespace AcMeERP.Module.Master
{
    public partial class ProjectView : Base.UIBase
    {

        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        private DataView ProjectViewResource = null;
        private DataTable ProjectSourcetoExport = null;
        private const string PROJECT_ID = "PROJECT_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Properties

        private int BranchId
        {
            set
            {
                ViewState["BranchId"] = value;
            }
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Project.ProjectViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                //this.ShowLoadWaitPopUp();
                gvProject.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.ProjectAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.ProjectView;
            BranchId = 0;
            if (base.LoginUser.IsBranchOfficeUser)
                FetchBranchId();
            SetProjectViewSource();

            gvProject.RowCommand += new GridViewCommandEventHandler(gvProject_RowCommand);
            gvProject.ExportClicked += new EventHandler(gvProject_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvProject.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvProject.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvProject.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvProject.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectEdit, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvProject.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvProject.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvProject.HideColumn = this.hiddenColumn;
            gvProject.RowIdColumn = this.rowIdColumn;
            gvProject.DataSource = ProjectViewResource;
        }

        protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ResultArgs resultArgs = new ResultArgs();
            int ProjectId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (ProjectId != 0)
                {
                    using (ProjectSystem ProjectSystem = new ProjectSystem())
                    {

                        resultArgs = ProjectSystem.DeleteProjectDetails(ProjectId, DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.ProjectDeleted;
                            SetProjectViewSource();
                            gvProject.BindGrid(ProjectViewResource);
                        }
                        else
                        {
                            //this.Message = MessageCatalog.Message.Project.DenyProjectDeletion;
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
        }

        protected void gvProject_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "ProjectSource" + DateTime.Now.Ticks.ToString();
                SetProjectViewSource();
                if (!ProjectSourcetoExport.Equals(null))
                {
                    ProjectSourcetoExport.Columns.Remove(PROJECT_ID);
                    CommonMethod.WriteExcelFile(ProjectSourcetoExport, fileName);
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

        #endregion

        #region Method

        #region Download Excel
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

        #endregion

        private void SetProjectViewSource()
        {
            using (ProjectSystem ProjectSystem = new ProjectSystem())
            {
                if (BranchId != 0)
                    ProjectSystem.BranchId = BranchId;
                //ResultArgs resultArgs = ProjectSystem.FetchProjects(DataBaseType.HeadOffice);
                ResultArgs resultArgs = ProjectSystem.FetchProjectsWithBranch(DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    ProjectViewResource = resultArgs.DataSource.Table.DefaultView;
                    ProjectSourcetoExport = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = ProjectSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }

        private void FetchBranchId()
        {
            try
            {
                using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                {
                    BranchOffice.BranchOfficeCode = base.LoginUser.LoginUserBranchOfficeCode;
                    ResultArgs resultArgs = BranchOffice.FetchBranch(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        BranchId = this.Member.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();

            }
        }
        #endregion

    }
}