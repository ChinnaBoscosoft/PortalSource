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
 * Purpose          :This page allows the branch office user to download excel templates for uploading master data , asset master data ) to upload folder in the server
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Bosco.Utility;

namespace AcMeERP.Module.Software
{
    public partial class DownloadTemplates : Base.UIBase
    {
        #region Property
        string TemplatePath = @"~/Module/Software/Uploads/AcMEERPTemplate/";
        private string FolderPath { get; set; }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (base.LoginUser.IsPortalUser)
                {
                    divUpload.Visible = true;
                }
                this.PageTitle = "Acme.erp Templates";
                LoadModules();
                LoadTemplateFiles(); 
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }
        #endregion

        #region Methods

        private void UploadFile()
        {
            string FileName = UlcFileUpload.FileName;
            if (UlcFileUpload.IsValid)
            {
                if (!String.IsNullOrEmpty(FileName))
                {
                    string Module = cmbModule.SelectedItem.ToString();
                    FolderPath = Path.Combine(TemplatePath, Module + "/");
                    if (!string.IsNullOrEmpty(FolderPath))
                    {
                        string UploadDirectory = Server.MapPath(FolderPath) + FileName;
                        UlcFileUpload.SaveAs(UploadDirectory);
                        this.Message = MessageCatalog.Message.VoucherUploadSuccess;
                        LoadTemplateFiles();
                    }
                }
                else
                {
                    this.Message = MessageCatalog.Message.FileUploadFiledEmpty;
                }
            }
        }

        private void LoadModules()
        {
            DataView dvModuleList = this.Member.EnumSet.GetEnumDataSource(typeof(ModuleTemplate), Sorting.None);
            this.Member.ComboSet.BindCombo(cmbModule, dvModuleList.ToTable(), "Name", "Id", false);
        }

        private void LoadTemplateFiles()
        {
            try
            {
                DataTable dtFiles = new DataTable();
                dtFiles.Columns.Add("PhysicalFile");
                dtFiles.Columns.Add("Attachments");
                dtFiles.Columns.Add("Module");
                dtFiles.Columns.Add("Filesize");
                dtFiles.Columns.Add("UploadedOn");
                if (Directory.Exists(Server.MapPath(TemplatePath)))
                {
                    string[] dirPath = Directory.GetDirectories(Server.MapPath(TemplatePath));
                    foreach (string dir in dirPath)
                    {
                        string[] filesPath = Directory.GetFiles(dir);
                        foreach (string path in filesPath)
                        {
                            DataRow dr = dtFiles.NewRow();
                            dr["Module"] = dir.Substring(dir.LastIndexOf('\\') + 1);
                            dr["PhysicalFile"] = Bosco.Utility.CommonMember.DOWNLOAD_FOLDER + "AcMEERPTemplate/"
                                + dir.Substring(dir.LastIndexOf('\\') + 1) + "/" + Path.GetFileName(path);
                            dr["Attachments"] = Path.GetFileName(path).Replace(Path.GetExtension(path), string.Empty);
                            dr["Filesize"] = CommonMethod.GetFileSize(new FileInfo(path));
                            dr["UploadedOn"] = new FileInfo(path).CreationTimeUtc.Date.ToShortDateString();

                            dtFiles.Rows.Add(dr);
                        }
                    }
                    rptTemplates.DataSource = dtFiles;
                    rptTemplates.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        #endregion
    }
}