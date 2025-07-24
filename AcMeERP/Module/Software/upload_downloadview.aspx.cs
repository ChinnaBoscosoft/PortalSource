using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using Bosco.Model.UIModel;
using Bosco.Utility;
using AcMeERP.Base;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Software
{
    public partial class upload_downloadview : Base.UIBase
    {
        #region Properties
        
        #endregion

        #region Events

        private DataView SoftwareViewSource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        private string filename = "";
        private string contenttype = "";

        protected void Page_Init(object sender, EventArgs e)
        {
            targetPage = this.GetPageUrlByName(URLPages.SoftwareAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.SoftwareView;
            SetSoftwareViewSource();

            gvSoftware.RowCommand +=new GridViewCommandEventHandler(gvSoftware_RowCommand);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, "Upload", false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvSoftware.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvSoftware.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvSoftware.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                gvSoftware.SetTemplateColumn(ControlType.ImageButton, CommandMode.Download, this.rowIdColumn, "", null, "", CommandMode.Download.ToString());
            }
            else
            {
                if (this.CheckUserRights(RightsModule.Tools, RightsActivity.SoftwareUpload, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvSoftware.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Tools, RightsActivity.SoftwareModify, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvSoftware.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Tools, RightsActivity.SoftwareDelete, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvSoftware.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
                if (this.CheckUserRights(RightsModule.Tools, RightsActivity.SoftwareDownload, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvSoftware.SetTemplateColumn(ControlType.ImageButton, CommandMode.Download, this.rowIdColumn, "", null, "", CommandMode.Download.ToString());
                }
            }

            gvSoftware.HideColumn = this.hiddenColumn;
            gvSoftware.RowIdColumn = this.rowIdColumn;
            gvSoftware.DataSource = SoftwareViewSource;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.UploadDownload.UplaodDownloadViewPageTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.SoftwareDownload, base.LoginUser.LoginUserHeadOfficeCode==string.Empty?DataBaseType.Portal : DataBaseType.HeadOffice);
                //this.ShowLoadWaitPopUp();
            }
        }

        #endregion

        #region Methods

        #region Row Command Event - For Delete
        /// <summary>
        /// this event is to bind the values to each row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int VersionId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());
            new ErrorLog().WriteError("Portal Software Download Begins versionId::"+VersionId);
            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (VersionId > 0)
                {
                    using (SoftwareSystem software = new SoftwareSystem(VersionId))
                    {
                        resultArgs = software.DeleteSoftwareDetails(VersionId);

                        if (resultArgs.Success)
                        {
                            DeleteFiles(software.BUILDFILE_PHYSICAL);
                            if (!(string.IsNullOrEmpty(software.RELEASENOTES_PHYSICAL)))
                            {
                                DeleteFiles(software.RELEASENOTES_PHYSICAL);
                            }
                            this.Message = MessageCatalog.Message.SoftwareDeleted;
                            SetSoftwareViewSource();
                            gvSoftware.BindGrid(SoftwareViewSource);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }

            }
            else if (e.CommandName == CommandMode.Download.ToString())
            {
                //Download content
                using(SoftwareSystem software=new SoftwareSystem(VersionId))
                {
                    filename = software.BUILDFILE_PHYSICAL;
                    contenttype = software.CONTENT_TYPE;
                }
                try
                {
                    DownLoadFile();
                }
                catch (Exception ex)
                {
                    new ErrorLog().WriteError("Upload_downloadview.aspx.cs", "gvSoftware_RowCommand", ex.Message,"0");
                }
            }
        }

        #endregion

        private void SetSoftwareViewSource()
        {
            using (SoftwareSystem SoftwareSystem = new SoftwareSystem())
            {
                ResultArgs resultArgs = SoftwareSystem.FetchSoftwareDetails();

                if (resultArgs.Success)
                {
                    SoftwareViewSource = resultArgs.DataSource.Table.DefaultView;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = SoftwareSystem.AppSchema.Software.VERSION_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
        private void DeleteFiles(string filename)
        {
            string filePath = string.Empty;
            filePath = Server.MapPath(CommonMember.DOWNLOAD_PATH + filename);
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        private void DownLoadFile()
        {
            new ErrorLog().WriteError("Portal Software Download Begins");   
            byte[] bytes;
            bytes = File.ReadAllBytes(Server.MapPath(CommonMember.DOWNLOAD_PATH + filename));
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contenttype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename.Trim().Replace(" ","").Trim());
            Response.BinaryWrite(bytes);
            Response.Flush();
            //Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        


        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            using(SoftwareSystem software=new SoftwareSystem(id))
            {
                filename = software.BUILDFILE_PHYSICAL;
                contenttype = software.CONTENT_TYPE;
            }
            bytes = File.ReadAllBytes(Server.MapPath(CommonMember.DOWNLOAD_PATH + filename));
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contenttype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        #endregion
    }
}