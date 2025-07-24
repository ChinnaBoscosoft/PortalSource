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
 * Purpose          :This page allows the end user to download the updated masters for their branch office as xml file
 *****************************************************************************************************/
using System;
using System.Web;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.IO;
using DevExpress.Web.ASPxGridView;
using System.ServiceModel;
using AcMeERP.DataSyncService;
using System.Collections.Generic;
using Ionic.Zip;

namespace AcMeERP.Module.Software
{
    public partial class DownloadMasters : Base.UIBase
    {
        #region Declaration
        string MASTER = "Masters/";
        ResultArgs resultArgs = null;
        private DataTable BranchDetail;
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
                this.PageTitle = MessageCatalog.Message.BranchOffice.DownloadMasterPageTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.DownloadMasterData, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                FetchBranchOfficeSource();
            }
        }

        protected void gvDownloadMaster_Load(object sender, EventArgs e)
        {
            LoadGrid(Branch);
            if (!base.LoginUser.IsBranchOfficeUser)
            {
                ShowMultiDownload();
            }
        }

        protected void gvDownloadMaster_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                lock (objLock)
                {
                    string branchOfficeCode = e.KeyValue.ToString();
                    string headOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                    if (!(string.IsNullOrEmpty(headOfficeCode) && string.IsNullOrEmpty(branchOfficeCode)))
                    {
                        DataSynchronizer objService = new DataSynchronizer();
                        DataSet dsMasters = objService.GetMasterDetails(headOfficeCode, branchOfficeCode);
                        if (dsMasters.Tables.Count > 0 && dsMasters != null)
                        {
                            XMLConverter.WriteToXMLFile(dsMasters,
                                 PagePath.MasterSettingFileName);
                            DownLoadFile(branchOfficeCode);
                        }
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                this.Message = ex.Detail.Message;
            }
        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string FileType = "_Masters.xml";
                string FileName = "Master.zip";
                string name = string.Empty;
                string branchOfficeCode = string.Empty;
                string headOfficeCode = string.Empty;
                string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                lock (objLock)
                {
                    List<object> lBranchId = new List<object>();
                    lBranchId = gvDownloadMaster.GetSelectedFieldValues(this.AppSchema.BranchOffice.Branch_CodeColumn.ColumnName);
                    string[] path = new string[100];
                    headOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                    ClearAll();
                    foreach (object Id in lBranchId)
                    {
                        branchOfficeCode = Id.ToString();
                        if (!(string.IsNullOrEmpty(headOfficeCode) && string.IsNullOrEmpty(branchOfficeCode)))
                        {
                            DataSynchronizer objService = new DataSynchronizer();
                            DataSet dsMasters = objService.GetMasterDetails(headOfficeCode, branchOfficeCode);
                            if (dsMasters.Tables.Count > 0 && dsMasters != null)
                            {
                                XMLConverter.WriteToXMLFile(dsMasters, PagePath.MasterSettingFileName);
                                byte[] bytes;
                                name = GetBranchOfficeName(CommonMember.DecryptValue(dsMasters.Tables["Header"].Rows[0][this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString()), CommonMember.DecryptValue(dsMasters.Tables["Header"].Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString()));
                                bytes = File.ReadAllBytes(PagePath.MasterSettingFileName);
                                DirectoryPath = PagePath.MultiMasterSettingFileName + "Acme.erp_" + name + FileType;
                                File.WriteAllBytes(DirectoryPath, bytes);
                            }
                        }
                    }
                    ZipFile zip = new ZipFile();
                    zip.AddDirectory(PagePath.AppFilePath + MASTER);
                    zip.Save(PagePath.AppFilePath + MASTER + FileName);
                    Response.AppendHeader("content-disposition", "attachment; filename=" + headOfficeCode + FileName);
                    Response.ContentType = "zip";
                    Response.TransmitFile(PagePath.AppFilePath + MASTER + FileName);
                    Response.Flush();
                    System.IO.File.Delete(PagePath.AppFilePath + MASTER + FileName);
                    ClearAll();
                    Response.End();
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + "" + ex.StackTrace);
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
                    DataBaseType ConnectTo = (base.LoginUser.IsPortalUser) ? DataBaseType.Portal : DataBaseType.HeadOffice;
                    ResultArgs resultArgs = BranchOfficeSystem.FetchActiveBranch(ConnectTo);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        Branch = resultArgs.DataSource.Table;
                        LoadGrid(Branch);
                    }
                    else
                    {
                        BranchDetail = resultArgs.DataSource.Table;
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

        private void DownLoadFile(string branchOfficeCode)
        {
            byte[] bytes;
            bytes = File.ReadAllBytes(PagePath.MasterSettingFileName);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=master_" + branchOfficeCode +
            DateTime.Now.ToString(DateFormatInfo.MySQLFormat.DateTime).ToString() + ".xml");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        private void LoadGrid(DataTable dtBranch)
        {
            try
            {
                gvDownloadMaster.DataSource = dtBranch;
                gvDownloadMaster.DataBind();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }

        }

        private void ClearAll()
        {
            DirectoryInfo Paths = new DirectoryInfo(PagePath.AppFilePath + MASTER);
            foreach (FileInfo file in Paths.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in Paths.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void ShowMultiDownload()
        {
            gvDownloadMaster.Columns["colMultiCheckBox"].Visible = true;
        }

        private string GetBranchOfficeName(string headOfficeCode, string branchOfficeCode)
        {
            string branchOfficeName = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(headOfficeCode) && !string.IsNullOrEmpty(branchOfficeCode))
                {
                    DataSynchronizeService.DataSynchronizerClient objDsync = new DataSynchronizeService.DataSynchronizerClient();
                    DataTable dtBranchInfo = objDsync.GetBranchDetails(headOfficeCode, branchOfficeCode);
                    if (dtBranchInfo != null && dtBranchInfo.Rows.Count > 0)
                    {
                        branchOfficeName = dtBranchInfo.Rows[0]["Branch Name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return branchOfficeName;
        }
        #endregion

    }
}