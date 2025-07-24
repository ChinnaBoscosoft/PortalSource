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
 * Purpose          :This page allows the branch office to upload their branch office database backup to portal server.
 *****************************************************************************************************/
using System;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Configuration;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.XtraPrinting;
using Ionic.Zip;
using System.Linq;
using DevExpress.Web.ASPxGridView;


namespace AcMeERP.Module.Software
{
    public partial class acmeerp_Backup2015 : Base.UIBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        public DataTable dtSourceBackup = new DataTable();
        private static object objLock = new object();
        // string dbuloadDBpath = @"D:\APP Source\NewPortal\AcMeERPPortal\AcMeERP\Module\Software\Uploads\AcMEERPBackupSDBINM\sdbinm\";
        string dbuloadDBpath = @"C:\Inetpub\vhosts\acmeerp.org\httpdocs\Module\Software\Uploads\AcMEERPBackupSDBINM\sdbinm\";
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

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.AcMEERPBackupTitleold;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadBackupData();
                LoadBranch();
            }

            if (ViewState["BackupData"] != null)
            {
                dtSourceBackup = (DataTable)ViewState["BackupData"];
                BindBackupGrid();
            }
        }

        private void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranchbyLocations(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, "BNAME_CODE", "BRANCH_OFFICE_ID", false);
                        if (LoginUser.IsHeadOfficeUser)
                        {
                            if (this.Member.NumberSet.ToInteger(Session[base.DefaultBranchId].ToString()) != 0)
                            {
                                cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                            }
                        }
                        else if (LoginUser.IsBranchOfficeUser)
                        {
                            cmbBranch.Text = LoginUser.LoginUserBranchOfficeName;
                            cmbBranch.Enabled = false;
                        }
                        BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    }
                    else
                    {
                        BranchId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
        }

        /// <summary>
        /// Load : Back up
        /// </summary>
        private void LoadBackupData()
        {
            try
            {
                if (ViewState["BackupData"] != null)
                {
                    dtSourceBackup = (DataTable)ViewState["BackupData"];
                    //return;
                }
                else
                {

                    dtSourceBackup = new DataTable();

                    using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                    {
                        resultArgs = branchOfficeSystem.FetchBranchbyLocations(
                            string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode)
                                ? DataBaseType.Portal
                                : DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtSourceBackup = resultArgs.DataSource.Table;
                        }
                    }

                    if (!dtSourceBackup.Columns.Contains("Attachments"))
                    {
                        dtSourceBackup.Columns["BNAME_CODE"].ColumnName = "Attachments";
                    }

                    dtSourceBackup.Columns.Add("PhysicalFile", typeof(string));
                    dtSourceBackup.Columns.Add("Filesize", typeof(string));
                    dtSourceBackup.Columns.Add("UploadedOn", typeof(DateTime));

                    LoadBackupFiles();
                }

                //if ((base.LoginUser.IsHeadOfficeAdminUser || base.LoginUser.IsBranchOfficeAdminUser || base.LoginUser.IsAdminUser) && base.LoginUser.IsHeadOfficeUserRights == false)
                if ((base.LoginUser.IsHeadOfficeAdminUser || base.LoginUser.IsAdminUser) && base.LoginUser.IsHeadOfficeUserRights == false)
                {
                    if (dtSourceBackup != null && dtSourceBackup.Rows.Count > 0)
                    {
                        dtSourceBackup.DefaultView.Sort = "UploadedOn DESC";
                        dtSourceBackup = dtSourceBackup.DefaultView.ToTable();
                    }
                }
                else if (base.LoginUser.IsBranchOfficeAdminUser)
                {
                    DataView dvBackup = new DataView(dtSourceBackup);
                    string branchdbfilter = this.LoginUser.LoginUserBranchOfficeCode.Substring(this.LoginUser.LoginUserBranchOfficeCode.Length - 6);
                    branchdbfilter = "-" + branchdbfilter;
                    branchdbfilter = branchdbfilter;
                    dvBackup.RowFilter = "Attachments like '%" + branchdbfilter + "'";
                    dtSourceBackup = dvBackup.ToTable();
                }
                else if (base.LoginUser.IsHeadOfficeUser)
                {
                    DataView dvBackup = new DataView(dtSourceBackup);
                    string branchdbfilter = GetBranchCode();
                    branchdbfilter = " - " + branchdbfilter;
                    dvBackup.RowFilter = "Attachments like '%" + branchdbfilter + "'";
                    dtSourceBackup = dvBackup.ToTable();
                }
                ViewState["BackupData"] = dtSourceBackup;
                BindBackupGrid();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBackupFiles()
        {
            try
            {
                DataTable dtBackup = new DataTable();
                dtBackup.Columns.Add("PhysicalFile", typeof(string));
                dtBackup.Columns.Add("Attachments", typeof(string));
                dtBackup.Columns.Add("Filesize", typeof(string));
                dtBackup.Columns.Add("UploadedOn", typeof(DateTime));

                //string dbuloadDBpath = @"D:\APP Source\NewPortal\AcMeERPPortal\AcMeERP\Module\Software\Uploads\AcMEERPBackupSDBINM\";
                string dbuloadDBpath = @"C:\Inetpub\vhosts\acmeerp.org\httpdocs\Module\Software\Uploads\AcMEERPBackupSDBINM\";
                string userBackupPath = Path.Combine(dbuloadDBpath, base.LoginUser.HeadOfficeCode);

                if (!Directory.Exists(userBackupPath))
                {
                    this.Message = "Backup directory not found: " + userBackupPath;
                    return;
                }

                string[] filesPath = Directory.GetFiles(userBackupPath, "*.zip");
                foreach (string path in filesPath)
                {
                    DataRow dr = dtBackup.NewRow();
                    FileInfo fileInfo = new FileInfo(path);

                    dr["PhysicalFile"] = "https://www.acmeerp.org/Module/Software/" + Bosco.Utility.CommonMember.DOWNLOAD_FOLDER + "AcMEERPBackupSDBINM/" + base.LoginUser.HeadOfficeCode + "/" + Path.GetFileName(path);
                    dr["Attachments"] = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    dr["Filesize"] = CommonMethod.GetFileSize(fileInfo);

                    // Use LastWriteTime for better accuracy
                    dr["UploadedOn"] = this.Member.DateSet.ToDate(fileInfo.LastWriteTime.ToString(), true);

                    dtBackup.Rows.Add(dr);
                }

                foreach (DataRow sourceRow in dtSourceBackup.Rows)
                {
                    string attachmentName = sourceRow["Attachments"].ToString();

                    string escapedAttachmentName = attachmentName.Replace("'", "''");

                    DataRow[] matchingRows = dtBackup.Select("Attachments = '" + escapedAttachmentName + "'");

                    if (matchingRows.Length > 0)
                    {
                        DataRow backupRow = matchingRows[0]; // Get first match
                        sourceRow["PhysicalFile"] = backupRow["PhysicalFile"];
                        sourceRow["Filesize"] = backupRow["Filesize"];
                        sourceRow["UploadedOn"] = backupRow["UploadedOn"];
                    }
                    else
                    {
                        // Assign default values if no backup is found
                        sourceRow["PhysicalFile"] = string.Empty;
                        sourceRow["Filesize"] = string.Empty;
                        sourceRow["UploadedOn"] = DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = "Error in LoadBackupFiles: " + ex.Message;
            }
        }

        protected void aspxUploadBrowsefile_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {
                if (e.UploadedFile.IsValid)
                {
                    // Get Selected Branch from ComboBox
                    string selectedBranchCode = cmbBranch.Text != null ? cmbBranch.Text : "";
                    if (string.IsNullOrEmpty(selectedBranchCode))
                    {
                        this.Message = "Error: Please select a branch before uploading.";
                        return;
                    }

                    // Get File Name & Path
                    string fileName = Path.GetFileName(e.UploadedFile.FileName);
                    string fileExtension = Path.GetExtension(fileName);

                    // Allow only .zip and .sql files
                    if (!fileExtension.Equals(".zip", StringComparison.OrdinalIgnoreCase) &&
                        !fileExtension.Equals(".sql", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Message = "Invalid file type. Please upload .zip or .sql files only.";
                        return;
                    }

                    // ✅ Define File Paths (Save as `selectedBranchCode.zip`)
                    string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                    string zipFileName = selectedBranchCode + ".zip";  // 🔹 File is always named after the branch
                    string zipFilePath = Path.Combine(dbuloadDBpath, zipFileName);

                    // ✅ Save File Temporarily
                    e.UploadedFile.SaveAs(tempFilePath);

                    // ✅ Create ZIP File with Selected Branch Name
                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.AddFile(tempFilePath, "");  // Add the uploaded file
                        zip.Save(zipFilePath);          // Save as `{selectedBranchCode}.zip`
                    }

                    File.Delete(tempFilePath); // Remove Temp File

                    // Get File Size
                    FileInfo fileInfo = new FileInfo(zipFilePath);
                    string formattedSize = CommonMethod.GetFileSize(new FileInfo(zipFilePath));
                    DateTime uploadedTime = fileInfo.LastWriteTime;

                    // Update DataTable (Grid Source)
                    DataRow[] matchingRows = dtSourceBackup.Select(string.Format("Attachments = '{0}' ", selectedBranchCode));

                    if (matchingRows.Length > 0)
                    {
                        // Update the first matching row
                        DataRow rowToUpdate = matchingRows[0];
                        rowToUpdate["PhysicalFile"] = zipFilePath;
                        rowToUpdate["Filesize"] = formattedSize;
                        rowToUpdate["UploadedOn"] = uploadedTime;
                    }
                    else
                    {
                        // If no matching record, add a new row
                        DataRow newRow = dtSourceBackup.NewRow();
                        newRow["Attachments"] = selectedBranchCode;
                        newRow["PhysicalFile"] = zipFilePath;
                        newRow["Filesize"] = formattedSize;
                        newRow["UploadedOn"] = uploadedTime;
                        dtSourceBackup.Rows.Add(newRow);
                    }

                    // Store in ViewState and Bind Grid
                    ViewState["BackupData"] = dtSourceBackup;
                    // e.CallbackData = "File uploaded and assigned successfully.";
                    this.Message = "File uploaded and assigned successfully.";
                }
                else
                {
                    this.Message = "File upload failed.";
                }
            }
            catch (Exception ex)
            {
                this.Message = "Error: " + ex.Message;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedBranchCode = cmbBranch.Text != null ? cmbBranch.Text : "";
                if (string.IsNullOrEmpty(selectedBranchCode))
                {
                    this.Message = "Please select a branch before uploading.";
                    return;
                }

                // Ensure Backup Data Exists
                if (ViewState["BackupData"] != null)
                {
                    dtSourceBackup = (DataTable)ViewState["BackupData"];
                }
                else
                {
                    this.Message = "No backup data found.";
                    return;
                }

                // Find matching row in the DataTable
                DataRow[] matchingRows = dtSourceBackup.Select(string.Format("Attachments = '{0}'", selectedBranchCode));
                if (matchingRows.Length == 0)
                {
                    this.Message = "No matching backup found for the selected branch.";
                    return;
                }

                if (dtSourceBackup != null && dtSourceBackup.Rows.Count > 0)
                {
                    dtSourceBackup.DefaultView.Sort = "UploadedOn DESC";
                    dtSourceBackup = dtSourceBackup.DefaultView.ToTable();
                }

                ViewState["BackupData"] = dtSourceBackup;

                // LoadBackupData();

                BindBackupGrid();
                LoadBranch();

                this.Message = "Backup data updated successfully.";

            }
            catch (Exception ex)
            {
                this.Message = "Error: " + ex.Message;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBackupData();
            LoadBranch();
        }

        /// <summary>
        /// Rows to fetch Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvDownloadBackup_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
        {
            //string dbuloadDBpath = @"C:\Inetpub\vhosts\acmeerp.org\httpdocs\Module\Software\Uploads\AcMEERPBackupSDBINM\sdbinm\";
            if (e.CommandArgs.CommandName == "downloadfile")
            {
                if (e.KeyValue != null && !string.IsNullOrEmpty(e.KeyValue.ToString()))
                {
                    string downloadfilename = e.KeyValue.ToString();
                    downloadfilename = downloadfilename + ".zip";
                    downloadfilename = Path.Combine(dbuloadDBpath, downloadfilename);

                    if (!File.Exists(downloadfilename))
                    {
                        this.Message = "File not found: " + downloadfilename;
                        return;
                    }

                    string disposition = "attachment";

                    // Clear response and set headers
                    Page.Response.Clear();
                    Page.Response.ContentType = "application/zip";

                    string originalFileName = Path.GetFileName(downloadfilename);

                    // **Sanitize filename (Remove problematic characters like semicolons, commas, etc.)**
                    string safeFileName = originalFileName.Replace(",", "").Replace(";", "").Replace("\"", "").Trim();

                    // Add only ONE Content-Disposition header
                    Page.Response.AddHeader("Content-Disposition", string.Format("{0}; filename=\"{1}\"", disposition, safeFileName));

                    // Disable caching
                    Page.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                    Page.Response.Cache.SetNoStore();
                    Page.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

                    // Transmit the file
                    Page.Response.TransmitFile(downloadfilename);
                    Page.Response.Flush();
                    Page.Response.End(); // Ensures immediate response termination

                    //Page.Response.Clear();
                    ////Page.Response.ContentType = "application/pdf";
                    //Page.Response.ContentType = contentType_Renamed;
                    //Page.Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}", disposition, Path.GetFileName(downloadfile)));
                    //Page.Response.WriteFile(downloadfile);
                    //Page.Response.End();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindBackupGrid()
        {
            if (dtSourceBackup == null || dtSourceBackup.Rows.Count == 0)
            {
                gvDownloadBackup.DataSource = null;
            }
            else
            {
                gvDownloadBackup.DataSource = dtSourceBackup;
                gvDownloadBackup.DataBind();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update Response content type based on export type
        /// </summary>
        /// <param name="documentData"></param>
        /// <param name="format"></param>
        /// <param name="isInline"></param>
        /// <param name="fileName"></param>
        /// <summary>
        /// This is to get Branch Code
        /// </summary>
        /// <returns></returns>
        public string GetBranchCode()
        {
            string rtn = string.Empty;
            using (DataManager dataManager = new DataManager(Bosco.DAO.Schema.SQLCommand.BranchOffice.FetchBranchforKeyDownloadByUserId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUser.LoginUserId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                rtn = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_CODE"].ToString();
                rtn = rtn.Replace(",", "','");
                rtn = rtn.Substring(1, rtn.Length - 3);
            }
            return rtn;
        }
        #endregion

    }
}