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
using System.Web;

namespace AcMeERP.Module.Software
{
    public partial class acmeerp_branch_reports : Base.UIBase
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

        private string BranchReportBasePath
        {
            get
            {
                string FY = this.Member.DateSet.ToDate(base.LoginUser.YearFrom, false).Year.ToString();
                string BranchReportsPath = Path.Combine(PagePath.ApplicationPhysicalPath, @"Module\Software\Uploads\Acmeerp_Branch_Reports\", base.LoginUser.HeadOfficeCode);
                BranchReportsPath = Path.Combine(BranchReportsPath, FY);
                return BranchReportsPath;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            string branchrpttype = string.Empty;
            string pagetitle = MessageCatalog.Message.AcMEERPBranchReportTitle;
            if (Request.QueryString["typeid"] != null)
            {
                branchrpttype = Request.QueryString["typeid"].ToString();
                //For FMA Show all the branch reports as Generalate Reports
                if (branchrpttype == "1")
                {
                    pagetitle = "Community - Generalate Reports";
                }
            }

            if (!IsPostBack)
            {
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                Int32 yearfrm = this.Member.DateSet.ToDate(base.LoginUser.YearFrom, false).Year;
                Int32 yearto = this.Member.DateSet.ToDate(base.LoginUser.YearTo, false).Year;

                if (yearfrm == yearto )
                    this.PageTitle = pagetitle + " - " + yearfrm.ToString();
                else
                    this.PageTitle = pagetitle + " (" + yearfrm.ToString() + " - " + yearto.ToString() + ")";
            }
            ShowBranchReports();
            this.SetControlFocus(cmbBranch);
            ShowLoadWaitPopUp(btnFileUpload);
        }


        protected void gvDownloadBranchReports_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "downloadfile")
            {
                if (e.KeyValue != null && !string.IsNullOrEmpty(e.KeyValue.ToString()))
                {
                    string downloadfilename = e.KeyValue.ToString();
                    downloadfilename = Path.Combine(BranchReportBasePath, downloadfilename);
                    if (File.Exists(downloadfilename))
                    {
                        WriteDocumentToResponse(downloadfilename, Path.GetExtension(downloadfilename), true);
                    }
                }
            }
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
                                ShowBranchReports();
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

        protected void aspxBtnExcel_Click(object sender, EventArgs e)
        {
            XlsExportOptions xlsexport = new XlsExportOptions();
            xlsexport.SheetName = "Acmeerp_Branch_Reports";
            gvDownloadBranchReports.Columns["colDownloadMaster"].Visible = false;
            this.gridExport.WriteXlsToResponse("Acmeerp_Branch_Reports", true, xlsexport);
            gvDownloadBranchReports.Columns["BtnDownloadReportFile"].Visible = true;
        }

        protected void aspxBtnPdf_Click(object sender, EventArgs e)
        {
            this.gridExport.WritePdfToResponse("Acmeerp_Branch_Reports", true);
        }

        protected void rbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowBranchReports();
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

        private void ShowBranchReports()
        {
            try
            {
                DataTable dtAcmeerpBranchReports = new DataTable();
                dtAcmeerpBranchReports.Columns.Add("Branch", typeof(System.String));
                dtAcmeerpBranchReports.Columns.Add("Attachments", typeof(System.String));
                dtAcmeerpBranchReports.Columns.Add("Filesize", typeof(System.String));
                dtAcmeerpBranchReports.Columns.Add("UploadedOn", typeof(System.DateTime));
                string UploadedBranchReports = BranchReportBasePath;
                                
                if (Directory.Exists(UploadedBranchReports))
                {
                    string searchpattern = "*.*";
                    searchpattern = "*" + rbFileType.SelectedItem.Text ;
                    string[] filesPath = Directory.GetFiles(UploadedBranchReports, searchpattern );

                    string branchname = string.Empty;
                    foreach (string path in filesPath)
                    {
                        DataRow dr = dtAcmeerpBranchReports.NewRow();
                        string filename = Path.GetFileNameWithoutExtension(path);
                        string[] filenameparts = filename.Split('-');
                        if (filenameparts.Length > 0)
                        {
                            branchname = filenameparts.GetValue(0).ToString();
                        }
                        dr["Branch"] = branchname;
                        dr["Attachments"] = Path.GetFileName(path); //basepath + "/" + Path.GetFileName(path); 
                        dr["Filesize"] = CommonMethod.GetFileSize(new FileInfo(path));

                        DateTime dtUploadedTime = new FileInfo(path).LastAccessTime;
                        DateTime dtLastWriteTime = new FileInfo(path).LastWriteTime;
                        DateTime dtLastAccessTime = new FileInfo(path).LastAccessTime;

                        if (dtLastWriteTime > dtLastAccessTime)
                        {
                            dtUploadedTime = dtLastWriteTime;
                        }
                                                                        
                        dr["UploadedOn"] = this.Member.DateSet.ToDate(dtUploadedTime.ToString(), true);
                        dtAcmeerpBranchReports.Rows.Add(dr);
                    }
                    if (base.LoginUser.IsBranchOfficeUser)
                    {
                        DataView dvBackup = new DataView(dtAcmeerpBranchReports);
                        string branchdbfilter = this.LoginUser.LoginUserBranchOfficeCode.Substring(this.LoginUser.LoginUserBranchOfficeCode.Length - 6);
                        branchdbfilter = " - " + branchdbfilter + ".*";
                        dvBackup.RowFilter = "Attachments like '%" + branchdbfilter + "'";
                        dtAcmeerpBranchReports = dvBackup.ToTable();
                    }

                    if (dtAcmeerpBranchReports != null && dtAcmeerpBranchReports.Rows.Count > 0)
                    {
                        dtAcmeerpBranchReports.DefaultView.Sort = "UploadedOn DESC";
                        dtAcmeerpBranchReports = dtAcmeerpBranchReports.DefaultView.ToTable();
                    }
                    gvDownloadBranchReports.DataSource = dtAcmeerpBranchReports;
                    gvDownloadBranchReports.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
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

        /// <summary>
        /// Update Response content type based on export type
        /// </summary>
        /// <param name="documentData"></param>
        /// <param name="format"></param>
        /// <param name="isInline"></param>
        /// <param name="fileName"></param>
        private void WriteDocumentToResponse(string downloadfile, string format, bool isInline)
        {
            string contentType_Renamed;
            string disposition = isInline ? "inline" : "attachment";

            switch (format.ToLower())
            {
                case ".pdf":
                    {
                        contentType_Renamed = "application/pdf";
                        break;
                    }
                case ".xls":
                    {
                        contentType_Renamed = "application/vnd.ms-excel";
                        break;
                    }

                case ".xlsx":
                    {
                        contentType_Renamed = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;
                    }

                case ".mht":
                    {
                        contentType_Renamed = "message/rfc822";
                        break;
                    }

                case ".html":
                    {
                        contentType_Renamed = "text/html";
                        break;
                    }

                case ".txt":
                case ".csv":
                    {
                        contentType_Renamed = "text/plain";
                        break;
                    }

                case ".png":
                    {
                        contentType_Renamed = "image/png";
                        break;
                    }

                default:
                    {
                        contentType_Renamed = string.Format("application/{0}", format);
                        break;
                    }
            }

            Page.Response.Clear();
            //Page.Response.ContentType = "application/pdf";
            Page.Response.ContentType = contentType_Renamed;
            Page.Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}", disposition, Path.GetFileName(downloadfile)));
            Page.Response.WriteFile(downloadfile);
            Page.Response.End();
        }
        #endregion

       

    }
}