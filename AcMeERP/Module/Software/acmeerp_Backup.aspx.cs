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

namespace AcMeERP.Module.Software
{
    public partial class acmeerp_Backup : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;
        private static object objLock = new object();
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
                this.PageTitle = MessageCatalog.Message.AcMEERPBackupTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                //LoadBranch();
                //ShowBackup();
                //this.SetControlFocus(cmbBranch);
                //ShowLoadWaitPopUp(btnFileUpload);
            }
            ShowBackup();
            this.SetControlFocus(cmbBranch);
            ShowLoadWaitPopUp(btnFileUpload);
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            lock (objLock)
            {
                try
                {
                    string FileName = string.Empty;
                    string Extension = string.Empty;
                    if (!string.IsNullOrEmpty(cmbBranch.Text))
                    {
                        FileName = cmbBranch.SelectedItem.Text;
                        if (UlcFileUpload.IsValid)
                        {
                            Extension = Path.GetExtension(UlcFileUpload.PostedFile.FileName);
                            if (UploadFTP(UlcFileUpload, FileName + Extension, base.LoginUser.HeadOfficeCode))
                            {
                                this.Message = MessageCatalog.Message.FileUpload;
                                ShowBackup();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    new ErrorLog().WriteError(ex.Message);
                }
            }
        }

        #endregion

        #region Methods

        private void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranch(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, "CODE", this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
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
        private void ShowBackup()
        {
            try
            {
                DataTable dtBackup = new DataTable();
                dtBackup.Columns.Add("PhysicalFile");
                dtBackup.Columns.Add("Attachments");
                dtBackup.Columns.Add("Filesize");
                dtBackup.Columns.Add("UploadedOn", typeof(System.DateTime));
                //string dbuloadDBpath = @"C:\Inetpub\vhosts\acmeerp.org\httpdocs\Module\Software\Uploads\AcMEERPBackup\";
                //string dbuloadDBpath = @"D:\PLESKVHOST\vhosts\acmeerp.org\httpdocs\Module\Software\Uploads\AcMEERPBackup\";

                string dbuloadDBpath = @"C:\Inetpub\vhosts\acmeerp.org\httpdocs\Module\Software\Uploads\AcMEERPBackup\";
                //string dbuloadDBpath = @"D:\APP Source\APP Source\Portal\AcMeERP\Module\Software\Uploads\AcMEERPBackup\";

                //dbuloadDBpath = Server.MapPath("~/Module/Software/Uploads/AcMEERPBackup/");

                //if (Directory.Exists(Server.MapPath("~/Module/Software/Uploads/AcMEERPBackup/") + base.LoginUser.HeadOfficeCode))
                if (Directory.Exists(dbuloadDBpath + base.LoginUser.HeadOfficeCode) && base.LoginUser.HeadOfficeCode.ToUpper() != "SMSMMS")
                {
                    string[] filesPath = Directory.GetFiles(dbuloadDBpath + base.LoginUser.HeadOfficeCode, "*.gz");
                    foreach (string path in filesPath)
                    {
                        DataRow dr = dtBackup.NewRow();
                        dr["PhysicalFile"] = "https://www.acmeerp.org/Module/Software/" + Bosco.Utility.CommonMember.DOWNLOAD_FOLDER + "AcMEERPBackup/" + base.LoginUser.HeadOfficeCode + "/" + Path.GetFileName(path);
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
                        //dr["UploadedOn"] = this.Member.DateSet.ToDate(dtUploadedDateTime.ToString(),true); working fine
                        dr["UploadedOn"] = this.Member.DateSet.ToDate(dtUploadedTime.ToString(), true);
                        dtBackup.Rows.Add(dr);
                    }

                    if ((base.LoginUser.IsHeadOfficeAdminUser || base.LoginUser.IsAdminUser) && base.LoginUser.IsHeadOfficeUserRights == false)
                    {
                        if (dtBackup != null && dtBackup.Rows.Count > 0)
                        {
                            dtBackup.DefaultView.Sort = "UploadedOn DESC";
                            dtBackup = dtBackup.DefaultView.ToTable();
                        }
                    }
                    else if (base.LoginUser.IsBranchOfficeAdminUser)
                    {
                        DataView dvBackup = new DataView(dtBackup);
                        string branchdbfilter = this.LoginUser.LoginUserBranchOfficeCode.Substring(this.LoginUser.LoginUserBranchOfficeCode.Length - 6);
                        branchdbfilter = " - " + branchdbfilter + ".sql";
                        dvBackup.RowFilter = "Attachments like '%" + branchdbfilter + "'";
                        dtBackup = dvBackup.ToTable();
                    }
                    else if (base.LoginUser.IsHeadOfficeUser)
                    {
                        DataView dvBackup = new DataView(dtBackup);
                        string branchdbfilter = GetBranchCode();
                        branchdbfilter = " - " + branchdbfilter + ".sql";
                        dvBackup.RowFilter = "Attachments like '%" + branchdbfilter + "'";
                        dtBackup = dvBackup.ToTable();
                    }

                    //if (dtBackup != null && dtBackup.Rows.Count > 0)
                    //{
                    //    dtBackup.DefaultView.Sort = "UploadedOn DESC";
                    //    dtBackup = dtBackup.DefaultView.ToTable();
                    //}

                    //if (base.LoginUser.IsBranchOfficeUser)
                    //{
                    //    DataView dvBackup = new DataView(dtBackup);
                    //    //dvBackup.RowFilter = "Attachments='" + base.LoginUser.LoginUserBranchOfficeName + "'";
                    //    //dtBackup.Clear();
                    //    string branchdbfilter = this.LoginUser.LoginUserBranchOfficeCode.Substring(this.LoginUser.LoginUserBranchOfficeCode.Length - 6);
                    //    branchdbfilter = " - " + branchdbfilter + ".sql";
                    //    dvBackup.RowFilter = "Attachments like '%" + branchdbfilter + "'";
                    //    dtBackup = dvBackup.ToTable();
                    //}
                    //else if (base.LoginUser.IsHeadOfficeAdminUser || base.LoginUser.IsBranchOfficeAdminUser || base.LoginUser.IsAdminUser || base.LoginUser.IsHeadOfficeUserRights == false)
                    //{
                    //}

                    //if (dtBackup != null && dtBackup.Rows.Count > 0)
                    //{
                    //    dtBackup.DefaultView.Sort = "UploadedOn DESC";
                    //    dtBackup = dtBackup.DefaultView.ToTable();
                    //}
                    gvDownloadBackup.DataSource = dtBackup;
                    gvDownloadBackup.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

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

        private bool UploadFTP(ASPxUploadControl fuUpload, string fileName, string headOfficeCode)
        {
            bool IsSuccess = false;
            string uploadUrl = string.Empty;
            try
            {
                uploadUrl = ConfigurationManager.AppSettings["ftpURL"].ToString() + "AcMEERPBackup";
                if (DoesFtpDirectoryExist(uploadUrl))
                {
                    createDirectory(uploadUrl);
                }
                uploadUrl += "/" + headOfficeCode + "/";
                if (DoesFtpDirectoryExist(uploadUrl))
                {

                    createDirectory(uploadUrl);
                }
                string uploadFileName = fileName;
                Stream streamObj = fuUpload.PostedFile.InputStream;
                Byte[] buffer = new Byte[fuUpload.PostedFile.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                string ftpUrl = string.Format("{0}/{1}", uploadUrl, uploadFileName);
                FtpWebRequest requestObj = FtpWebRequest.Create(ftpUrl) as FtpWebRequest;
                requestObj.KeepAlive = true;
                requestObj.UseBinary = true;
                requestObj.Proxy = null;
                requestObj.Timeout = 90000;
                requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                requestObj.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpUsername"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());
                Stream requestStream = requestObj.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
                requestObj = null;
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally
            {
            }
            return IsSuccess;
        }

        /* Create a New Directory on the FTP Server */
        public bool createDirectory(string newDirectory)
        {
            bool IsSuccess = false;
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest(newDirectory);
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                /* Establish Return Communication with the FTP Server */
                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    /* Resource Cleanup */
                    ftpResponse.Close();
                    ftpRequest = null;
                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                this.Message = ex.Message;
            }
            return IsSuccess;
        }

        private FtpWebRequest CreateAnFTPwebrequest(string remoteFile)
        {
            string remotefilepath = Path.Combine(ConfigurationManager.AppSettings["ftpURL"].ToString(), remoteFile);
            /* Create an FTP Request */
            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(remotefilepath);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpUsername"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());
            /* When in doubt, use these options */
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = false;
            ftpRequest.Proxy = null;
            ftpRequest.UseBinary = false;
            ftpRequest.Timeout = 90000;
            return ftpRequest;
        }

        /* Does FTP Directory exist*/
        public bool DoesFtpDirectoryExist(string dirPath)
        {
            try
            {
                FtpWebRequest request = CreateAnFTPwebrequest(dirPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        /* Does FTP file exist */
        public bool CheckIfFileExistsOnServer(string filePath)
        {
            try
            {
                FtpWebRequest request = CreateAnFTPwebrequest(filePath);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }
        #endregion

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
    }
}