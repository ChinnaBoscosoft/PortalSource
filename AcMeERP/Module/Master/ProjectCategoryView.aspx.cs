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
 * Purpose          :This page helps head office admin/user or branch office admin/user to view the project category
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
    public partial class ProjectCategoryView : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        private DataView projectCategoryViewSource = null;
        private DataTable ProjectCategorySourceToExport = null;
        private const string PROJECT_CATEGORY_ID = "PROJECT_CATOGORY_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";

        #endregion

        #region Property
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

        public bool ShowITRGroupColumn
        {
            get
            {
                return ViewState["ITRGroup"] != null && (bool)ViewState["ITRGroup"];
            }
            set
            {
                ViewState["ITRGroup"] = value;
            }
        }
        #endregion
        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.ProjectCategoryAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.ProjectCategoryView;
            BranchId = 0;
            if (base.LoginUser.IsBranchOfficeUser)
                FetchBranchId();
            SetProjectCategoryViewSource();

            gvProjectCategory.RowCommand += new GridViewCommandEventHandler(gvProjCategory_RowCommand);
            gvProjectCategory.ExportClicked += new EventHandler(gvProjectCategory_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvProjectCategory.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvProjectCategory.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvProjectCategory.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectCategoryAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvProjectCategory.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectCategoryEdit, true,
                           base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvProjectCategory.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectCategoryDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvProjectCategory.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvProjectCategory.HideColumn = this.hiddenColumn;
            gvProjectCategory.RowIdColumn = this.rowIdColumn;
            gvProjectCategory.DataSource = projectCategoryViewSource;
            //this.ShowLoadWaitPopUp();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.ProjectCategory.ProjectCategoryViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectCategoryView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                //this.ShowLoadWaitPopUp();
                gvProjectCategory.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvProjectCategory_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "ProjectCategory" + DateTime.Now.Ticks.ToString();
                SetProjectCategoryViewSource();
                if (!ProjectCategorySourceToExport.Equals(null))
                {
                    ProjectCategorySourceToExport.Columns.Remove(PROJECT_CATEGORY_ID);
                    CommonMethod.WriteExcelFile(ProjectCategorySourceToExport, fileName);
                    DownLoadFile(fileName);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        /// <summary>
        /// Project Category Edit/Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvProjCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int projectCategoryId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (projectCategoryId > 0)
                {
                    using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                    {
                        resultArgs = projectCategorySystem.DeleteProjectCatogoryDetails(projectCategoryId, DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.ProjectCategoryDeleted;
                            SetProjectCategoryViewSource();
                            gvProjectCategory.BindGrid(projectCategoryViewSource);
                        }
                        else
                        {
                            this.Message = resultArgs.Message + MessageCatalog.Message.LedgerMapping.ProjectCategoryDelete;
                        }
                    }
                }

            }

        }

        #endregion

        #region Methods]

        #region Download Excel
        /// <summary>
        /// This method helps to export view data to excel
        /// </summary>
        /// <param name="fileName"></param>
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

        /// <summary>
        /// This method is to view the available project category in the Head office
        /// </summary>
        private void SetProjectCategoryViewSource() // ProjectCatogorySystem
        {
            using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
            {
                if (BranchId != 0)
                    projectCategorySystem.BranchId = BranchId;
                ResultArgs resultArgs = projectCategorySystem.FetchProjectCatogoryDetails(DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    DataTable dt = resultArgs.DataSource.Table;

                    if (HeadOfficeCode.ToUpper() != "SDBINM")
                    {
                        if (dt.Columns.Contains("ITRGroup"))
                        {
                            dt.Columns.Remove("ITRGroup");
                        }
                    }

                    projectCategoryViewSource = dt.DefaultView; //resultArgs.DataSource.Table.DefaultView;
                    ProjectCategorySourceToExport = dt;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = projectCategorySystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
        #endregion

    }
}