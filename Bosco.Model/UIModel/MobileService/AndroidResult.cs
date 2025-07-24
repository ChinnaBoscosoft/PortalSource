using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bosco.Model.UIModel.MobileService
{
    public enum MobileRequest
    {
        login,
        branch,
        branchdatastatus,
        branchdeafulters,
        branchabstract,
        project,
        ledger,
        checkbalance,
        headofficeprofile
    }

    public class ServiceResult
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public Dictionary<string, object> Data = new Dictionary<string, object>();
    }

    public class AbstractData
    {
        public string MobileRequest { get; set; }//branchabstract
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BranchOfficeCode { get; set; }
        public string ProjectName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }

    public class CheckBalanceData
    {
        public string MobileRequest { get; set; }//checkbalance
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BranchOfficeCode { get; set; }
        public string ProjectName { get; set; }
        public string DateAsOn { get; set; }
    }

    public class LoginData
    {
        public string MobileRequest { get; set; }//login
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class BranchData
    {
        public string MobileRequest { get; set; }//branch
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class BranchStatusData
    {
        public string MobileRequest { get; set; }//branchdatastatus
        public string HeadOfficeCode { get; set; }
        public string BranchOfficeCode { get; set; }
        public string ProjectName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class BranchDefaultersData
    {
        public string MobileRequest { get; set; }//branchdeafulters
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }

    public class ProjectData
    {
        public string MobileRequest { get; set; }//project
        public string HeadOfficeCode { get; set; }
        public string BranchOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LedgerData
    {
        public string MobileRequest { get; set; }//ledger
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LedgerSummaryData
    {
        public string MobileRequest { get; set; }//ledgersummary
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LedgerName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }

    public class HeadOfficeProfileData
    {
        public string MobileRequest { get; set; }//headofficeprofile
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class EmailData
    {
        public string MobileRequest { get; set; }//email
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MailIds { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    public class SMSData
    {
        public string MobileRequest { get; set; }//sms
        public string HeadOfficeCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNumber{ get; set; }
        public string Message { get; set; }
    }
}
