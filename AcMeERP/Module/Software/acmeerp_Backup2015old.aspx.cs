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
    public partial class acmeerp_Backup2015old : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;
        public DataTable dtSourceBackup = new DataTable();
        private static object objLock = new object();
        string dbuloadDBpath = @"D:\APP Source\NewPortal\AcMeERPPortal\AcMeERP\Module\Software\Uploads\AcMEERPBackupSDBINM\sdbinm\";
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
            if (!IsPostBack || gvDownloadBackup.IsCallback)
            {
                this.PageTitle = MessageCatalog.Message.AcMEERPBackupTitleold;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadBackupData();
            }

            if (ViewState["BackupData"] != null)
            {
                dtSourceBackup = (DataTable)ViewState["BackupData"];
                BindBackupGrid();
            }
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

            //if (e.CommandArgs.CommandName == "UploadFile")
            //{
            //    // Get the Attachments value directly from e.KeyValue
            //    // Get the row index of the clicked button
            //    int rowIndex = e.VisibleIndex;

            //    // Retrieve the Attachments column value for the selected row
            //    object attachmentValue = gvDownloadBackup.GetRowValues(rowIndex, "Attachments");
            //    string attachmentName = attachmentValue != null ? attachmentValue.ToString() : string.Empty;

            //    if (string.IsNullOrEmpty(attachmentName))
            //    {
            //        this.Message = "Attachment name is missing.";
            //        return;
            //    }

            //    // Proceed with file upload and processing
            //    var uploadControl = gvDownloadBackup.FindRowCellTemplateControl(e.VisibleIndex, null, "uploadControl") as ASPxUploadControl;
            //    if (uploadControl != null && uploadControl.HasFile)
            //    {
            //        string fileExtension = Path.GetExtension(uploadControl.FileName);
            //        string tempFilePath = Path.Combine(Path.GetTempPath(), attachmentName + fileExtension);
            //        string zipFileName = attachmentName + ".zip";
            //        string zipFilePath = Path.Combine(dbuloadDBpath, zipFileName);

            //        uploadControl.SaveAs(tempFilePath);

            //        using (ZipFile zip = new ZipFile())
            //        {
            //            zip.AddFile(tempFilePath, "");
            //            zip.Save(zipFilePath);
            //        }

            //        File.Delete(tempFilePath);
            //        this.Message = "File uploaded and saved as ZIP successfully!";
            //    }
            //    else
            //    {
            //        this.Message = "Please select a file to upload.";
            //    }
            //    ShowBackup();
            //}
        }

        /// <summary>
        /// file browse files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {
                if (e.UploadedFile.IsValid)
                {
                    ASPxUploadControl uploadControl = sender as ASPxUploadControl;

                    if (uploadControl == null)
                    {
                        e.CallbackData = "Error: Upload control not found.";
                        return;
                    }

                    // Ensure a row is selected
                    int rowIndex = gvDownloadBackup.FocusedRowIndex;
                    if (rowIndex < 0)
                    {
                        e.CallbackData = "Error: No row selected.";
                        return;
                    }

                    // Retrieve the Attachments column value
                    string attachmentName = gvDownloadBackup.GetRowValues(rowIndex, "Attachments") as string;
                    if (string.IsNullOrEmpty(attachmentName))
                    {
                        e.CallbackData = "Error: Attachment name is missing.";
                        return;
                    }

                    string fileName = Path.GetFileName(e.UploadedFile.FileName);
                    string fileExtension = Path.GetExtension(fileName);

                    if (fileExtension.ToLower() != ".zip" && fileExtension.ToLower() != ".sql")
                    {
                        e.CallbackData = "Invalid file type. Please upload .zip or .sql files only.";
                        return;
                    }

                    string tempFilePath = Path.Combine(Path.GetTempPath(), attachmentName + fileExtension);
                    string zipFileName = attachmentName + ".zip";
                    string zipFilePath = Path.Combine(dbuloadDBpath, zipFileName);

                    e.UploadedFile.SaveAs(tempFilePath);

                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.AddFile(tempFilePath, "");
                        zip.Save(zipFilePath);
                    }

                    File.Delete(tempFilePath);
                    e.CallbackData = "File uploaded and saved as ZIP successfully!";

                    FileInfo fileInfo = new FileInfo(zipFilePath);
                    long fileSizeInBytes = fileInfo.Length;
                    string formattedSize = (fileSizeInBytes / 1024) + " KB";

                    DataTable dtUpdatedBackup = ViewState["BackupData"] as DataTable;
                    if (dtUpdatedBackup != null)
                    {
                        foreach (DataRow row in dtUpdatedBackup.Rows)
                        {
                            if (row["Attachments"].ToString() == attachmentName)
                            {
                                row["Filesize"] = formattedSize;
                                row["UploadedOn"] = DateTime.Now;
                                break;
                            }
                        }
                        ViewState["BackupData"] = dtUpdatedBackup;
                    }

                    uploadControl.JSProperties["cpRefreshGrid"] = true;
                }
                else
                {
                    e.CallbackData = "File upload failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                e.CallbackData = "Error: " + ex.Message;
            }
        }

        protected void aspxBtnExcel_Click(object sender, EventArgs e)
        {
            XlsExportOptions xlsexport = new XlsExportOptions();
            xlsexport.SheetName = "Acmeerp_BO_Backup";
            gvDownloadBackup.Columns["colDownloadMaster"].Visible = false;
            this.gridExport.WriteXlsToResponse("Acmeerp_BO_Backup", true, xlsexport);
            gvDownloadBackup.Columns["colDownloadMaster"].Visible = true;
        }

        protected void aspxBtnPdf_Click(object sender, EventArgs e)
        {
            this.gridExport.WritePdfToResponse("Acmeerp_BO_Backup", true);
        }

        #endregion

        #region Methods

        /// <summary>
        // Bind it to the grid control
        /// </summary>
        private void ShowBackup()
        {
            try
            {
                if (ViewState["BackupData"] != null)
                {
                    dtSourceBackup = (DataTable)ViewState["BackupData"];
                }
                else
                {
                    try
                    {
                        using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                        {
                            resultArgs = BranchOfficeSystem.FetchBranchbyLocations(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                dtSourceBackup = resultArgs.DataSource.Table;
                                dtSourceBackup = dtSourceBackup.AsEnumerable().Take(2).CopyToDataTable();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        resultArgs.Message = ex.Message;
                    }
                    // Rename column BNAME_CODE to Attachments
                    dtSourceBackup.Columns["BNAME_CODE"].ColumnName = "Attachments";
                    // Add new columns to dtSourceBackup
                    dtSourceBackup.Columns.Add("PhysicalFile");
                    dtSourceBackup.Columns.Add("Filesize");
                    dtSourceBackup.Columns.Add("UploadedOn", typeof(DateTime));
                }

                // directory
                DataTable dtBackup = new DataTable();
                dtBackup.Columns.Add("PhysicalFile");
                dtBackup.Columns.Add("Attachments");
                dtBackup.Columns.Add("Filesize");
                dtBackup.Columns.Add("UploadedOn", typeof(System.DateTime));
                //string dbuloadDBpath = @"C:\Inetpub\vhosts\acmeerp.org\httpdocs\Module\Software\Uploads\AcMEERPBackupSDBINM\";
                //dbuloadDBpath = @"C:\Inetpub\vhosts\acmeerp.org\staging.acmeerp.org\Module\Software\Uploads\AcMEERPBackupSDBINM\";
                string dbuloadDBpath = @"D:\APP Source\APP Source\Portal\AcMeERP\Module\Software\Uploads\AcMEERPBackupSDBINM\";
                dbuloadDBpath = @"D:\APP Source\NewPortal\AcMeERPPortal\AcMeERP\Module\Software\Uploads\AcMEERPBackupSDBINM\";
                if (Directory.Exists(dbuloadDBpath + base.LoginUser.HeadOfficeCode) && base.LoginUser.HeadOfficeCode.ToUpper() == "SDBINM")
                {
                    string[] filesPath = Directory.GetFiles(dbuloadDBpath + base.LoginUser.HeadOfficeCode, "*.zip");
                    foreach (string path in filesPath)
                    {
                        string values = string.Empty;
                        DataRow dr = dtBackup.NewRow();
                        dr["PhysicalFile"] = "https://www.acmeerp.org/Module/Software/" + Bosco.Utility.CommonMember.DOWNLOAD_FOLDER + "AcMEERPBackupSDBINM/" + base.LoginUser.HeadOfficeCode + "/" + Path.GetFileName(path);
                        dr["Attachments"] = Path.GetFileName(path).Replace(Path.GetExtension(path), string.Empty);
                        dr["Filesize"] = CommonMethod.GetFileSize(new FileInfo(path));

                        //dr["UploadedOn"] = new FileInfo(path).CreationTimeUtc;
                        DateTime dtUploadedTime = new FileInfo(path).LastAccessTime;
                        DateTime dtLastWriteTime = new FileInfo(path).LastWriteTime;
                        DateTime dtLastAccessTime = new FileInfo(path).LastAccessTime;
                        if (dtLastWriteTime > dtLastAccessTime)
                        {
                            dtUploadedTime = dtLastWriteTime;
                        }
                        //dr["UploadedOn"] = this.Member.DateSet.ToTime(dtUploadedDateTime.ToString(), "dd/MM/yyyy h:mm:ss tt") + " :: " + dtUploadedDateTime.ToLocalTime().ToString(); //this.Member.DateSet.ToDate(dtUploadedDateTime.ToString(), true);// this.Member.DateSet.ToDateTime(dtUploadedDateTime.ToString(), "dd/mm/yyyy h:mm:ss tt", true);
                        dr["UploadedOn"] = this.Member.DateSet.ToDate(dtUploadedTime.ToString(), true);
                        dtBackup.Rows.Add(dr);
                    }

                    // **Use Dictionary for Fast Lookups**
                    // **Update dtSourceBackup Based on dtBackup**
                    foreach (DataRow sourceRow in dtSourceBackup.Rows)
                    {
                        string attachmentName = sourceRow["Attachments"].ToString();
                        DataRow[] matchingRows = dtBackup.Select("Attachments = '" + attachmentName + "'");

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
                            sourceRow["PhysicalFile"] = DBNull.Value;
                            sourceRow["Filesize"] = "1 KB"; // Default size
                            sourceRow["UploadedOn"] = DBNull.Value;
                        }
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
                        dtBackup = dvBackup.ToTable();
                    }
                    else if (base.LoginUser.IsHeadOfficeUser)
                    {
                        DataView dvBackup = new DataView(dtSourceBackup);
                        string branchdbfilter = GetBranchCode();
                        branchdbfilter = " - " + branchdbfilter;
                        dvBackup.RowFilter = "Attachments like '%" + branchdbfilter + "'";
                        dtBackup = dvBackup.ToTable();
                    }

                    gvDownloadBackup.DataSource = dtSourceBackup;
                    gvDownloadBackup.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBackupData()
        {
            try
            {
                if (ViewState["BackupData"] != null)
                {
                    dtSourceBackup = (DataTable)ViewState["BackupData"];
                    return;
                }

                dtSourceBackup = new DataTable(); // ✅ Always initialize to prevent null issues.

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

                string dbuloadDBpath = @"D:\APP Source\NewPortal\AcMeERPPortal\AcMeERP\Module\Software\Uploads\AcMEERPBackupSDBINM\";
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

                if (dtSourceBackup == null)
                {
                    this.Message = "Source backup data is null.";
                    return;
                }

                foreach (DataRow sourceRow in dtSourceBackup.Rows)
                {
                    string attachmentName = sourceRow["Attachments"].ToString();

                    // **Escape Single Quotes in the Attachment Name**
                    string escapedAttachmentName = attachmentName.Replace("'", "''");

                    // **Use the Escaped Name in the DataTable.Select() Query**
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
                        sourceRow["Filesize"] = "1 KB";
                        sourceRow["UploadedOn"] = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = "Error in LoadBackupFiles: " + ex.Message;
            }
        }

        private void BindBackupGrid()
        {
            if (dtSourceBackup == null || dtSourceBackup.Rows.Count == 0)
            {
                gvDownloadBackup.DataSource = null;
            }
            else
            {
                gvDownloadBackup.DataSource = dtSourceBackup;
            }

            if (!gvDownloadBackup.IsCallback)  // ✅ Prevent double binding issues
            {
                gvDownloadBackup.DataBind();
            }
        }


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