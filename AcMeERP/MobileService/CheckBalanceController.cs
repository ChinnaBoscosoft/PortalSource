using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bosco.Model.UIModel.MobileService;
using System.Web;
using Bosco.Utility;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AcMeERP.MobileService
{
    public class CheckBalanceController : ApiController
    {
        private CommonMember memberset = new CommonMember();
        public List<ServiceResult> Post(HttpRequestMessage request)
        {
            List<ServiceResult> objArray = new List<ServiceResult>();

            AESEncryptionDecryption _Decrypt = new AESEncryptionDecryption();

            ServiceResult objResult = new ServiceResult();
            var abstractjsonString = request.Content.ReadAsStringAsync().Result;
            // new ErrorLog().WriteMobileServiceError("Json String :" + loginjsonString);
            JArray array = JArray.Parse(abstractjsonString);
            // new ErrorLog().WriteMobileServiceError("After arrya Parse :" + array.ToString());
            CheckBalanceData dtData = JsonConvert.DeserializeObject<CheckBalanceData>(array[0].ToString());
            string errorMessage = ValidateCheckBalanceData(dtData);
             if (string.IsNullOrEmpty(errorMessage))
             {
                 switch (dtData.MobileRequest)
                 {
                     case "checkbalance":
                         try
                         {
                             new ErrorLog().WriteMobileServiceError("Reached CheckBalance Controller");
                             using (MobileServiceSystem mobileServiceSystem = new MobileServiceSystem())
                             {
                                 //Connect to the Concern Head Office Database
                                 mobileServiceSystem.HeadOfficeCode = dtData.HeadOfficeCode.Trim();
                                 ResultArgs resultArgs = mobileServiceSystem.GetHeadOfficeDBConnection();
                                 if (resultArgs != null && resultArgs.Success)
                                 {
                                     mobileServiceSystem.UserName = dtData.Username.Trim();
                                     mobileServiceSystem.Password = dtData.Password.Trim();
                                     resultArgs = mobileServiceSystem.AuthenticateUser();
                                     if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                     {
                                         mobileServiceSystem.BranchOfficeCode = dtData.BranchOfficeCode;
                                         mobileServiceSystem.ProjectName = dtData.ProjectName;
                                         mobileServiceSystem.DateTo = mobileServiceSystem.DateAsOn = this.memberset.DateSet.ToDate(dtData.DateAsOn.ToString(), false);
                                         resultArgs = mobileServiceSystem.GetCheckBalanceDetails();
                                     }
                                     else
                                     {
                                         resultArgs.Message = MessageCatalog.MobileService.InvalidLogin;
                                     }

                                 }
                                 if (resultArgs.Success)
                                 {
                                     objResult.Status = MessageCatalog.MobileService.SuccessStatus;
                                     objResult.ErrorCode = MessageCatalog.MobileService.SuccessErrorCode;
                                     objResult.Data.Add("CheckBalance", resultArgs.DataSource.TableSet);
                                 }
                                 else
                                 {
                                     objResult.Status = MessageCatalog.MobileService.FalseStatus;
                                     objResult.ErrorCode = MessageCatalog.MobileService.NotFoundErrorCode;
                                     objResult.ErrorMessage = string.IsNullOrEmpty(resultArgs.Message) ? MessageCatalog.MobileService.ResultsNotFound : resultArgs.Message;
                                 }
                                 new ErrorLog().WriteMobileServiceError("CheckBalance Controller Service Request Completed");
                             }
                         }
                         catch (Exception ex)
                         {
                             objResult.Status = MessageCatalog.MobileService.FalseStatus;
                             objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                             objResult.ErrorMessage = "Error in getting CheckBalance for the Branch";
                             new ErrorLog().WriteMobileServiceError(ex.Message);
                         }
                         break;
                 }
             }
             else
             {
                 objResult.Status = MessageCatalog.MobileService.FalseStatus;
                 objResult.ErrorCode = MessageCatalog.MobileService.EmptyErrorCode;
                 objResult.ErrorMessage = errorMessage;
             }
            objArray.Add(objResult);
            return objArray;
        }
        private string ValidateCheckBalanceData(CheckBalanceData dtData)
        {
            string Message = string.Empty;
            if (string.IsNullOrEmpty(dtData.MobileRequest))
            {
                Message = MessageCatalog.MobileService.MobileRequestEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.HeadOfficeCode))
            {
                Message = MessageCatalog.MobileService.HeadOfficeCodeEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.BranchOfficeCode))
            {
                Message = MessageCatalog.MobileService.BranchOfficeCodeEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.Username))
            {
                Message = MessageCatalog.MobileService.UserNameEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.Password))
            {
                Message = MessageCatalog.MobileService.PasswordEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.DateAsOn))
            {
                Message = MessageCatalog.MobileService.DateAsOnEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.ProjectName))
            {
                Message = MessageCatalog.MobileService.ProjectNameEmpty;
            }
            return Message;
        }
    }
}