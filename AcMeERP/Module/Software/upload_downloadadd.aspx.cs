using System;
using System.Web.UI.WebControls;
using System.IO;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Net;
using System.Configuration;


namespace AcMeERP.Module.Software
{
    public partial class upload_downloadadd : Base.UIBase
    {

        #region Properties
        private int VersionID
        {
            get
            {
                int versionId = this.Member.NumberSet.ToInteger(this.RowId);
                return versionId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }
        private string ReleaseFileName
        {
            get
            {
                if (ViewState["ReleaseFileName"] != null)
                {
                    return ViewState["ReleaseFileName"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ReleaseFileName"] = value;
            }

        }
        private string BuildFileName
        {
            get
            {
                if (ViewState["BuildFileName"] != null)
                {
                    return ViewState["BuildFileName"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["BuildFileName"] = value;
            }
        }
        private string FileSize
        {
            get
            {
                if (ViewState["FileSize"] != null)
                {
                    return ViewState["FileSize"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["FileSize"] = value;
            }

        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.UploadDownload.UploadDownloadAddPageTitle;
                SetReleaseDate();
                SetControlFocus();
                this.CheckUserRights(RightsModule.Tools, RightsActivity.SoftwareUpload, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                hlkClose.PostBackUrl = this.ReturnUrl;
                if (VersionID > 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                else
                    dteReleaseDate.Date = DateTime.Now;

                this.ShowLoadWaitPopUp();
            }
            hfmode.Value = (this.Member.NumberSet.ToInteger(this.RowId) > 0) ? "1" : "0";


        }

        private void SetReleaseDate()
        {
            dteReleaseDate.Date = DateTime.Now;
            //teReleaseTime.Text = DateTime.Now.ToString(DateFormatInfo.Time12Format);
        }
        private string RenameFile(FileUpload fuUpControl)
        {
            string fileRename = string.Empty;
            fileRename = Path.GetFileNameWithoutExtension(fuUpControl.FileName) +
                DateTime.Now.ToString(DateFormatInfo.MySQLFormat.DateTimeLong).ToString()
                + Path.GetExtension(fuUpControl.FileName);
            return fileRename;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (SaveFile())
                    {
                        ResultArgs resultArgs = null;
                        using (SoftwareSystem softwareSystem = new SoftwareSystem())
                        {
                            softwareSystem.VersionId = VersionID == 0 ? (int)AddNewRow.NewRow : VersionID;
                            softwareSystem.DESC_OF_RELEASE = txtDescription.Text.Trim();
                            softwareSystem.TITLE = txtTitle.Text.Trim();
                            softwareSystem.RELEASE_VERSION = txtReleaseVersion.Text.Trim();
                            //softwareSystem.DATE_OF_RELEASE = Convert.ToDateTime(Member.DateSet.ToDateTime(dtReleaseDate.DateTimeValue.ToString(), DateFormatInfo.MySQLFormat.DateTimeAdd, true));
                            softwareSystem.DATE_OF_RELEASE = dteReleaseDate.Date;
                            softwareSystem.DATE_OF_UPLOAD = DateTime.Now;
                            softwareSystem.UPLOAD_TYPE = chkUploadType.Checked ? (int)FileUploadType.Prerequisite : (int)FileUploadType.Build;
                            softwareSystem.BUILDFILE_ACTUAL = spFileName.InnerHtml = hfFileName.Value;
                            softwareSystem.BUILDFILE_PHYSICAL = BuildFileName;
                            softwareSystem.RELEASENOTES_ACTUAL = spReleaseNoteName.InnerHtml = hfReleasefileName.Value;
                            softwareSystem.RELEASENOTES_PHYSICAL = ReleaseFileName;
                            softwareSystem.CONTENT_TYPE = fuUploadFile.PostedFile.ContentType;
                            softwareSystem.FILESIZE = FileSize;
                            resultArgs = softwareSystem.SaveSoftwareDetails();
                            if (resultArgs.Success)
                            {
                                this.Message = MessageCatalog.Message.FileUpload;
                                if (VersionID == 0)
                                {
                                    VersionID = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                }
                                AssignValuesToControls();
                            }
                            else
                            {
                                this.Message = fuUploadFile.FileName.ToString() + " is already available";

                            }
                        }
                    }
                    else
                    {
                        new ErrorLog().WriteError("File is not saved out of SaveFile()");
                    }
                }
                catch (Exception ex)
                {
                    new ErrorLog().WriteError("File Upload btnUpload click::" + ex.StackTrace.ToString());
                    this.Message = ex.Message;
                }
            }

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            clearvalues();
            SetReleaseDate();
            SetControlFocus();
        }
        #endregion

        #region Methods

        private bool SaveFile()
        {
            bool IsfileSaved = (VersionID > 0) ? true : false; //To allow update in the edit mode
            try
            {
                string _sDirectorypath = string.Empty;
                string sFileName = string.Empty;
                _sDirectorypath = Server.MapPath(CommonMember.UPLOAD_PATH);
                new ErrorLog().WriteError("File Upload Path SaveFile::" + _sDirectorypath);
                if (fuUploadFile.HasFile)
                {
                    if (!System.IO.Directory.Exists(_sDirectorypath))
                        System.IO.Directory.CreateDirectory(_sDirectorypath);
                    //Set readonly to false for file creation
                    var di = new DirectoryInfo(_sDirectorypath);
                    di.Attributes &= ~FileAttributes.ReadOnly;

                    //File.SetAttributes(_sDirectorypath, File.GetAttributes(_sDirectorypath) & ~FileAttributes.ReadOnly);

                    if (fuUploadFile.FileBytes.Length > 0)
                    {
                        new ErrorLog().WriteError("File Upload Path SaveFile Content::" + fuUploadFile.FileBytes.Length);

                        sFileName = RenameFile(fuUploadFile);
                        BuildFileName = sFileName;

                        //To check the file existence
                        if (!File.Exists(_sDirectorypath + sFileName))
                        {
                            //fuUploadFile.SaveAs(_sDirectorypath + sFileName);
                            UploadFileToFTP(fuUploadFile, sFileName);
                            FileInfo file = new FileInfo(_sDirectorypath + sFileName);
                            FileSize =CommonMethod.GetFileSize(file);
                            //delete the previous file in edit mode
                            if (VersionID > 0)
                            {
                                DeleteFiles(hfdelete.Value);
                            }
                            IsfileSaved = true;
                        }
                        else if (!IsfileSaved)
                        {
                            IsfileSaved = false;
                        }

                    }
                    if (fuUploadRelease.HasFile)
                    {
                        if (!System.IO.Directory.Exists(_sDirectorypath))
                            System.IO.Directory.CreateDirectory(_sDirectorypath);
                        //Set readonly to false for file creation
                        var dir = new DirectoryInfo(_sDirectorypath);
                        dir.Attributes &= ~FileAttributes.ReadOnly;

                        //File.SetAttributes(_sDirectorypath, File.GetAttributes(_sDirectorypath) & ~FileAttributes.ReadOnly);

                        if (fuUploadRelease.FileBytes.Length > 0)
                        {
                            new ErrorLog().WriteError("Portal File upload FileBytesLength::" + fuUploadRelease.FileBytes.Length);
                            sFileName = RenameFile(fuUploadRelease);
                            ReleaseFileName = sFileName;
                            //To check the file existence
                            if (!File.Exists(_sDirectorypath + sFileName))
                            {
                               // fuUploadRelease.SaveAs(_sDirectorypath + sFileName);
                                UploadFileToFTP(fuUploadRelease, sFileName);
                                //delete the previous file in edit mode
                                if (VersionID > 0)
                                {
                                    DeleteFiles(hfreleasedelete.Value);
                                }
                                IsfileSaved = true;
                            }
                            else if (!IsfileSaved)
                            {
                                IsfileSaved = false;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                IsfileSaved = false;
            }
            return IsfileSaved;
        }

        private void AssignValuesToControls()
        {
            using (SoftwareSystem softwareSystem = new SoftwareSystem(VersionID))
            {
                divfilename.Style.Add("display", "block");
                divReleaseName.Style.Add("display", "block");
                spFileName.InnerHtml = hfFileName.Value = softwareSystem.BUILDFILE_ACTUAL;
                spReleaseNoteName.InnerHtml = hfReleasefileName.Value = softwareSystem.RELEASENOTES_ACTUAL;
                txtTitle.Text = softwareSystem.TITLE;
                dteReleaseDate.Date = softwareSystem.DATE_OF_RELEASE;
                txtDescription.Text = softwareSystem.DESC_OF_RELEASE;
                txtReleaseVersion.Text = softwareSystem.RELEASE_VERSION;
                chkUploadType.Checked = softwareSystem.UPLOAD_TYPE == (int)FileUploadType.Prerequisite ? true : false;
                hfdelete.Value = softwareSystem.BUILDFILE_PHYSICAL;
                hfreleasedelete.Value = softwareSystem.RELEASENOTES_PHYSICAL;
                BuildFileName = softwareSystem.BUILDFILE_PHYSICAL;
                ReleaseFileName = softwareSystem.RELEASENOTES_PHYSICAL;
                FileSize = softwareSystem.FILESIZE;
                //dteReleaseDate.TimeValue = softwareSystem.RELEASETIME;
            }
        }

        private void SetControlFocus()
        {
            this.SetControlFocus(fuUploadFile);//Set the control focus
        }

        private void DeleteFiles(string filename)
        {
            string filePath = string.Empty;
            filePath = Server.MapPath(CommonMember.UPLOAD_PATH) + filename;
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        private void clearvalues()
        {
            divfilename.Style.Add("display", "none");
            divReleaseName.Style.Add("display", "none");
            txtReleaseVersion.Text = txtTitle.Text = txtDescription.Text = spFileName.InnerHtml = spReleaseNoteName.InnerHtml = string.Empty;
            hfmode.Value = hfdelete.Value = hfFileName.Value = hfReleasefileName.Value = string.Empty;
            chkUploadType.Checked = false;
            ReleaseFileName = BuildFileName = FileSize = string.Empty;
            VersionID = 0;
        }
        private bool UploadFileToFTP(FileUpload fuUpload,string fileName)
        {
            bool IsSuccess = false;
            try
            {
                string uploadUrl = ConfigurationManager.AppSettings["ftpURL"].ToString();
                string uploadFileName = fileName;
                Stream streamObj = fuUpload.PostedFile.InputStream;
                Byte[] buffer = new Byte[fuUpload.PostedFile.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uploadUrl);
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftp.Proxy = null;
                ftp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpUsername"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());

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
        #endregion
    }
}