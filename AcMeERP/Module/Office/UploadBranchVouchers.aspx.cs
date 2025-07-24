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
 * Purpose          :This page allows the branch office admin/user to upload the voucher file generated in offline and see the datasync status of offilne and online uploaded vouchers
 *****************************************************************************************************/
using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.IO;
using System.ServiceModel;
using AcMeERP.DataSyncService;

namespace AcMeERP.Module.Office
{
    public partial class UploadBranchVouchers : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        private static object objLock = new object();
        private const string VOUCHER_TABLENAME = "VoucherMasters";
        private const string FD_VOUCHER_TABLENAME = "FD_Voucher_Master_Trans";
        #endregion

        #region properties

        private DataTable Status
        {
            get
            {
                return (DataTable)ViewState["Status"];
            }
            set
            {
                ViewState["Status"] = value;
            }
        }

        private string BranchOfficeCode
        {
            get
            {
                return ViewState["Branch_Office_id"].ToString();
            }
            set
            {
                ViewState["Branch_Office_id"] = value;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                this.PageTitle = MessageCatalog.Message.UploadVoucher.UploadVoucherPageTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.ShowLoadWaitPopUp(btnFileUpload);
                this.ShowLoadWaitPopUp(btnRefresh);
                this.ShowLoadWaitPopUp(btnSend);
                LoadStatus();
                this.SetControlFocus(UlcFileUpload);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            string LoginUserHeadOfficeCode = string.Empty;
            lock (objLock)
            {
                try
                {
                    LoginUserHeadOfficeCode = this.LoginUser.HeadOfficeCode;
                    DataSet dsVouchers = null;
                    string FileName = UlcFileUpload.FileName;
                    if (UlcFileUpload.IsValid)
                    {
                        if (!String.IsNullOrEmpty(FileName))
                        {
                            string UploadDirectory = Server.MapPath("~/AppFile/") + FileName;
                            UlcFileUpload.SaveAs(UploadDirectory);

                            new ErrorLog().WriteError("Begin Uploading Voucher and File is saved::" + FileName);

                            dsVouchers = XMLConverter.ConvertXMLToDataSet(UploadDirectory);
                            if (dsVouchers != null && dsVouchers.Tables.Count > 0 && (dsVouchers.Tables.Contains(VOUCHER_TABLENAME)
                            || dsVouchers.Tables.Contains(FD_VOUCHER_TABLENAME)))
                            {
                                new ErrorLog().WriteError("ConvertXMLToDataSet is Success");
                                resultArgs = IsValidOfficeCode(dsVouchers);
                                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    //DataSynchronizeService.DataSynchronizerClient objService = new DataSynchronizeService.DataSynchronizerClient();
                                    DataSynchronizer objService = new DataSynchronizer();
                                    string version= objService.GetAcmeERPProductVersion();
                                    resultArgs.Success = objService.UploadVoucher(CommonMethod.CompressData(dsVouchers));
                                    if (resultArgs.Success)
                                    {
                                        new ErrorLog().WriteError("Upload Voucher file via Web Service is Success");
                                        LoadStatus();
                                        this.Message = MessageCatalog.Message.VoucherUploadSuccess;
                                    }
                                    else
                                    {
                                        this.Message = resultArgs.Message;
                                    }
                                }
                                else
                                {
                                    this.Message = resultArgs.Message;
                                }
                                //Delete xml file from the AppFile once upload is finished in server.
                                File.Delete(UploadDirectory);

                            }
                            else
                            {
                                this.Message = MessageCatalog.Message.WebServiceMessage.InvalidVouchers;
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.FileUploadFiledEmpty;
                        }
                    }

                }
                catch (FaultException<DataSyncService.AcMeServiceException> ex)
                {
                    this.Message = ex.Detail.Message;
                }

                catch (Exception ex)
                {
                    new ErrorLog().WriteError("Error in Uploading Voucher::" + ex.StackTrace.ToString());
                    this.Message = ex.StackTrace.ToString();
                }
                finally
                {
                    base.HeadOfficeCode = this.LoginUser.HeadOfficeCode = LoginUserHeadOfficeCode;
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatus();
        }

        protected void gvSynchronizationStatus_Load(object sender, EventArgs e)
        {
            LoadStatusFromViewState();
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                BranchOfficeCode = gvSynchronizationStatus.GetRowValues(gvSynchronizationStatus.FocusedRowIndex, "BRANCH_OFFICE_CODE").ToString();
                DataSynchronizeService.DataSynchronizerClient objService = new DataSynchronizeService.DataSynchronizerClient();
                string branchMailId = objService.GetBranchMailAddress(BranchOfficeCode);
                string content = memContent.Text
                                       + "<br/><br/>"
                                       + "Thank You!<br />";
                //string content = "test"
                //                      + "<br/><br/>"
                //                       + "Thank You!<br />";
                resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(branchMailId),
                         CommonMethod.RemoveFirstValue(branchMailId),
                        "Requesting to upload branch Transaction", content,true);
                if (resultArgs.Success)
                {
                    this.Message = MessageCatalog.Message.FileUploading.SendMailSuccess;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

        }
        #endregion

        #region Methods

        private void LoadStatus()
        {
            try
            {
                using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
                {
                    if (base.LoginUser.IsHeadOfficeUser)
                    {
                        headOfficeSystem.HeadOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                    }
                    if (base.LoginUser.IsBranchOfficeUser)
                    {
                        headOfficeSystem.HeadOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                        headOfficeSystem.BranchOfficeCode = base.LoginUser.LoginUserBranchOfficeCode;
                        gvSynchronizationStatus.Columns["colMail"].Visible = false;
                    }

                    resultArgs = headOfficeSystem.FetchDataSyncStatus();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //gvSynchronizationStatus.DataSource = resultArgs.DataSource.Table;
                        //gvSynchronizationStatus.DataBind();                        
                        Status = resultArgs.DataSource.Table;
                        LoadGrid(Status);

                    }
                    else
                    {
                        Status = null;
                        LoadGrid(Status);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
        }


        /// <summary>
        /// This Method Validates the Office Code
        /// </summary>
        /// <param name="dsVouchers"></param>
        /// <returns></returns>
        private ResultArgs IsValidOfficeCode(DataSet dsVouchers)
        {
            try
            {
                if (dsVouchers != null && dsVouchers.Tables.Count > 0)
                {
                    DataTable dtValidOfficeData = dsVouchers.Tables["Header"];
                    DataRow drOffice = dtValidOfficeData.Rows[0];
                    string HeadOfficeCode = CommonMember.DecryptValue(drOffice["HEAD_OFFICE_CODE"].ToString());
                    string BranchOfficeCode = CommonMember.DecryptValue(drOffice["BRANCH_OFFICE_CODE"].ToString());

                    if (!(string.IsNullOrEmpty(BranchOfficeCode) && string.IsNullOrEmpty(HeadOfficeCode)))
                    {
                        using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
                        {
                            headOfficeSystem.HeadOfficeCode = HeadOfficeCode;
                            headOfficeSystem.BranchOfficeCode = BranchOfficeCode;
                            resultArgs = headOfficeSystem.FetchActiveOfficeInfo();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                if (base.LoginUser.IsHeadOfficeUser)
                                {
                                    if (base.LoginUser.LoginUserHeadOfficeCode.Equals(HeadOfficeCode))
                                    {
                                        resultArgs.Success = true;
                                    }
                                    else
                                    {
                                        resultArgs.Success = false;
                                        resultArgs.Message = MessageCatalog.Message.VoucherUploadXML;
                                    }
                                }
                                else if (base.LoginUser.IsBranchOfficeUser)
                                {
                                    if (base.LoginUser.LoginUserHeadOfficeCode.Equals(HeadOfficeCode) && base.LoginUser.LoginUserBranchOfficeCode.Equals(BranchOfficeCode))
                                    {
                                        resultArgs.Success = true;
                                    }
                                    else
                                    {
                                        resultArgs.Success = false;
                                        resultArgs.Message = MessageCatalog.Message.VoucherUploadXML;
                                    }
                                }
                                if (resultArgs.Success)
                                {
                                    new ErrorLog().WriteError("Office Code is valid: " + HeadOfficeCode + "::" + BranchOfficeCode);
                                }
                            }
                            else
                            {
                                new ErrorLog().WriteError("Invalid Office Code: " + HeadOfficeCode + "::" + BranchOfficeCode);
                                resultArgs.Message = MessageCatalog.Message.VoucherUploadXML;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private void LoadStatusFromViewState()
        {
            LoadGrid(Status);
        }
        private void LoadGrid(DataTable dtStatus)
        {
            try
            {
                gvSynchronizationStatus.DataSource = dtStatus;
                gvSynchronizationStatus.DataBind();
                if (LoginUser.IsHeadOfficeUser)
                {
                    gvSynchronizationStatus.Columns["colHeadOfficeCode"].Visible = false;
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