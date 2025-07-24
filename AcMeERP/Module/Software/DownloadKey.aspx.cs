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
 * Purpose          :This page allows the end user to download the latest license for their branch office
 *****************************************************************************************************/
using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.IO;
using DevExpress.Web.ASPxGridView;
using System.Collections.Generic;
using AcMeERP.DataSyncService;
using System.ServiceModel;
using Ionic.Zip;


namespace AcMeERP.Module.Software
{
    public partial class DownloadKey : Base.UIBase
    {
        #region Declaration
        string LICENCES = "Licenses/";
        ResultArgs resultArgs = null;
        private DataTable BranchDetail = null;
        private static object objLock = new object();

        #endregion

        #region Properties

        private DataTable Branch
        {
            set { ViewState[AppSchema.BranchOffice.TableName] = value; }
            get { return (DataTable)ViewState[AppSchema.BranchOffice.TableName]; }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Branch = null;
                this.PageTitle = MessageCatalog.Message.BranchOffice.DownloadKeyPageTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.DownloadLicenseKey, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                FetchBranchOfficeSource();
            }
        }


        protected void gvDownloadKey_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                lock (objLock)
                {
                    string branchOfficeCode = e.KeyValue.ToString();
                    if (!string.IsNullOrEmpty(branchOfficeCode))
                    {
                        using (LicenseSystem licenseSystem = new LicenseSystem())
                        {
                            string filename = string.Empty;
                            string keyFile = "";
                            string downLoadKeyFile = "";
                            resultArgs = licenseSystem.GenerateLicenseKeyFile(branchOfficeCode, out keyFile, out downLoadKeyFile);

                            if (resultArgs.Success)
                            {
                                filename = PagePath.AppFilePath + keyFile;

                                if (resultArgs.Success && File.Exists(filename))
                                {
                                    Response.AppendHeader("content-disposition", "attachment; filename=" + downLoadKeyFile);
                                    Response.ContentType = "text/xml";
                                    Response.TransmitFile(filename);
                                    Response.End();
                                }
                            }
                            else
                            {
                                this.Message = resultArgs.Message;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        protected void gvDownloadKey_Load(object sender, EventArgs e)
        {
            LoadGrid(Branch);
            if (!base.LoginUser.IsBranchOfficeUser)
            {
                ShowMultiDownload();
            }
        }
        #endregion

        #region Methods

        private void FetchBranchOfficeSource()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    DataBaseType ConnectTo = DataBaseType.Portal;
                    ResultArgs resultArgs = BranchOfficeSystem.FetchBranchforDownloadKey(ConnectTo);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        Branch = resultArgs.DataSource.Table;
                        LoadGrid(Branch);
                    }
                    else
                    {
                        Branch = resultArgs.DataSource.Table;
                        LoadGrid(Branch);
                        this.Message = resultArgs.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        private void LoadGrid(DataTable dtBranch)
        {
            try
            {
                gvDownloadKey.DataSource = dtBranch;
                gvDownloadKey.DataBind();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        private void ShowMultiDownload()
        {
            gvDownloadKey.Columns["colMultiCheckBox"].Visible = true;
        }

        #endregion

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {

                string FileType = "_licensekey.xml";
                string FileName = "Licenses.zip";
                string name = string.Empty;
                string branchOfficeCode = string.Empty;
                string headOfficeCode = string.Empty;
                string DirectoryPath = string.Empty;
                lock (objLock)
                {
                    List<object> lBranchId = new List<object>();
                    lBranchId = gvDownloadKey.GetSelectedFieldValues(this.AppSchema.BranchOffice.Branch_CodeColumn.ColumnName);
                    string[] path = new string[100];
                    headOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                    ClearAll();
                    foreach (object Id in lBranchId)
                    {
                        branchOfficeCode = Id.ToString();
                        if (!(string.IsNullOrEmpty(headOfficeCode) && string.IsNullOrEmpty(branchOfficeCode)))
                        {
                            DataTable dtLicense = new DataTable();
                            using (LicenseSystem licenseSystem = new LicenseSystem())
                            {
                                resultArgs = licenseSystem.GetBranchOfficeLicense(branchOfficeCode);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    dtLicense = resultArgs.DataSource.Table;
                                }

                                DataSet dsLicense = new DataSet("dsLicense");
                                dsLicense.Tables.Add(dtLicense);
                                if (dsLicense.Tables.Count > 0 && dtLicense != null)
                                {
                                    XMLConverter.WriteToXMLFile(dsLicense, PagePath.LicenseKeyFileName);
                                    byte[] bytes;
                                    name = CommonMember.DecryptValue(dsLicense.Tables[0].Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString());
                                    name = name.Replace(":", "_");
                                    DirectoryPath = PagePath.MultilicensekeySettingFileName + "Acme.erp License_" + name + FileType;
                                    bytes = File.ReadAllBytes(PagePath.LicenseKeyFileName);
                                    File.WriteAllBytes(DirectoryPath, bytes);
                                }
                            }
                        }
                    }
                    ZipFile zip = new ZipFile();
                    zip.AddDirectory(PagePath.AppFilePath + LICENCES);
                    zip.Save(PagePath.AppFilePath + LICENCES + FileName);
                    Response.AppendHeader("content-disposition", "attachment; filename=" + headOfficeCode + FileName);
                    Response.ContentType = "zip";
                    Response.TransmitFile(PagePath.AppFilePath + LICENCES + FileName);
                    Response.Flush();
                    System.IO.File.Delete(PagePath.AppFilePath + LICENCES + FileName);
                    ClearAll();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + "" + ex.StackTrace);
            }
        }

        private void ClearAll()
        {

            DirectoryInfo Paths = new DirectoryInfo(PagePath.AppFilePath + LICENCES);
            foreach (FileInfo file in Paths.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in Paths.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }

}
