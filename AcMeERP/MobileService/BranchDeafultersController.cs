using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bosco.Model.UIModel.MobileService;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Bosco.Utility;
using System.Web;
using System.Data;

namespace AcMeERP.MobileService
{
    public class BranchDeafultersController : ApiController
    {
        private CommonMember member = new CommonMember();
        public List<ServiceResult> Post(HttpRequestMessage request)
        {
            List<ServiceResult> objArray = new List<ServiceResult>();

            AESEncryptionDecryption _Decrypt = new AESEncryptionDecryption();

            ServiceResult objResult = new ServiceResult();
            var loginjsonString = request.Content.ReadAsStringAsync().Result;
            // new ErrorLog().WriteMobileServiceError("Json String :" + loginjsonString);
            JArray array = JArray.Parse(loginjsonString);
            // new ErrorLog().WriteMobileServiceError("After arrya Parse :" + array.ToString());
            BranchDefaultersData dtData = JsonConvert.DeserializeObject<BranchDefaultersData>(array[0].ToString());
            string errorMessage = ValidateBranchDefaultersData(dtData);
            if (string.IsNullOrEmpty(errorMessage))
            {
                switch (dtData.MobileRequest)
                {
                    case "branchdefaulters":
                        try
                        {
                            new ErrorLog().WriteMobileServiceError("Reached Branch Defaulers Controller Controller");
                            using (MobileServiceSystem mobileServiceSystem = new MobileServiceSystem())
                            {
                                //Connect to the Concern Head Office Database
                                mobileServiceSystem.HeadOfficeCode = dtData.HeadOfficeCode.Trim();
                                ResultArgs resultArgs = mobileServiceSystem.GetHeadOfficeDBConnection();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    mobileServiceSystem.UserName = dtData.Username.Trim(); //  dtData.Username; 
                                    mobileServiceSystem.Password = dtData.Password.Trim(); // dtData.Password;
                                    resultArgs = mobileServiceSystem.AuthenticateUser();
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        DataTable dtUser = resultArgs.DataSource.Table;
                                        if (dtUser.Rows.Count > 0)
                                        {
                                            mobileServiceSystem.DateFrom = this.member.DateSet.ToDate(dtData.DateFrom.ToString(), false);
                                            mobileServiceSystem.DateTo = this.member.DateSet.ToDate(dtData.DateTo.ToString(), false);
                                            resultArgs = mobileServiceSystem.GetNonConformityBranches();
                                        }
                                        else
                                        {
                                            resultArgs.Message = MessageCatalog.MobileService.InvalidLogin;
                                        }
                                    }
                                }
                                if (resultArgs.Success)
                                {
                                    objResult.Status = MessageCatalog.MobileService.SuccessStatus;
                                    objResult.ErrorCode = MessageCatalog.MobileService.SuccessErrorCode;
                                    objResult.Data.Add(resultArgs.DataSource.Table.TableName, resultArgs.DataSource.Table);
                                }
                                else
                                {
                                    objResult.Status = MessageCatalog.MobileService.FalseStatus;
                                    objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                                    objResult.ErrorMessage = string.IsNullOrEmpty(resultArgs.Message) ? MessageCatalog.MobileService.ResultsNotFound : resultArgs.Message;
                                }
                                new ErrorLog().WriteMobileServiceError("Branch Defaulters Controller Service Request Completed");
                            }
                        }
                        catch (Exception ex)
                        {
                            objResult.Status = MessageCatalog.MobileService.FalseStatus;
                            objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                            objResult.ErrorMessage = "Error in Getting Branch Details";
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
        private string ValidateBranchDefaultersData(BranchDefaultersData dtData)
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
            else if (string.IsNullOrEmpty(dtData.Username))
            {
                Message = MessageCatalog.MobileService.UserNameEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.Password))
            {
                Message = MessageCatalog.MobileService.PasswordEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.DateFrom))
            {
                Message = MessageCatalog.MobileService.DateFromEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.DateTo))
            {
                Message = MessageCatalog.MobileService.DateToEmpty;
            }
            return Message;
        }

    }


}