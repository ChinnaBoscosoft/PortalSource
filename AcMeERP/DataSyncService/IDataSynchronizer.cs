using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.ServiceModel.Activation;
using Bosco.Utility;
using AcMeERP.DataSyncService;
using System.ServiceModel.Web;
using System.IO;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataSynchronizer" in both code and config file together.
[ServiceContract(Name = "IDataSynchronizer")]
public interface IDataSynchronizer
{
    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    bool IsBranchExists(string headOfficeCode, string branchOfficeCode);

    [OperationContract]
    string GetHeadOfficeMailAddress(string headOfficeCode);

    [OperationContract]
    string GetBranchMailAddress(string branchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetBranchDetails(string headOfficeCode, string branchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetBranchDetailsByCredentials(string userName, string password);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetVoucherAmendments(string headOfficeCode, string branchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetLicenseDetails(string headOfficeCode, string branchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataSet GetMasterDetails(string headOfficeCode, string branchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataSet GetTDSMasterDetails(string headOfficeCode, string branchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetMismatchedProjects(string headOfficeCode, string branchOfficeCode, DataTable dtBOProjects);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetMismatchedLedgers(string headOfficeCode, string branchOfficeCode, DataTable dtBOLedgers);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    bool GetExportDatatoPortalExists(string headOfficeCode, string branchOfficeCode, DateTime dtFrom);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    bool UploadVoucher(byte[] Vouchers);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    bool UpdateDsyncStatus(string headOfficeCode, string branchOfficeCode, string FileName, string Project, string Location, string UploadedBy);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    //Returns Amendments and Datasync Status
    DataSet GetHeadOfficeMessages(string headOfficeCode, string branchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    //Returns Tickets
    bool PostTicket(string headOfficeCode, string branchOfficeCode, DataTable dtTicket);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    //Returns License is up-to-date
    bool IsLatestLicenseAvailable(string headOfficeCode, string branchOfficeCode, DateTime dtCurrentLicenseDate);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    //Returns acme.erp product version
    string GetAcmeERPProductVersion();

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    //returns the Acmeerp Server Current Date
    string GetCurrentServerDate();

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetLockVoucher(string HeadOfficeCode, string BranchOfficeCode);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable GetLockVoucherGraceDays(string HeadOfficeCode, string BranchOfficeCode, string BranchOfficeLocation);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataSet GetApprovedBudgetsDetails(string headOfficeCode, string branchOfficeCode, DateTime dFrom, DateTime dTo, Int32 budgettypeId, DataTable dtBudgetBOProjects);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    bool UploadBudgetsDetails(string headOfficeCode, string branchOfficeCode, DataSet dsBudgetDetails, bool sendMail = true);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    bool UpdateBranchLoggedHistory(string BranchOfficeCode, string HeadOfficeCode, string BranchOfficeName, string HeadOfficeName, string Location, DateTime LoggedDateTime, string LicenseKeyNumber, string Remarks);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    string UpdateSubLedgerVouchers(string BranchOfficeCode, string HeadOfficeCode, string Location, DateTime FrmDate, DateTime ToDate, DataTable dtSubLedgerVouchers);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    bool UpdateProjectClosedDate(string branchOfficeCode, string headOfficeCode, string location, string projectname, string projectcloseddate);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable RequestLocalCommunityKey(string licensekey, string hocode, string bocode, string location, string clientip, string clientmacaddress, string localcommunityuser);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    string GetLocalCommunityReceiptModuleRightStatus(string licensekey, string hocode, string bocode, string location, string clientip, string clientmacaddress);

    [OperationContract]
    [FaultContract(typeof(AcMeServiceException))]
    DataTable CheckVouchersInOtherProjectsOrDates(string headOfficeCode, string branchOfficeCode, string locationname, DateTime dFrom, DateTime dTo);


}
