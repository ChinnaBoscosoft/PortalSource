using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Bosco.Model.UIModel.MobileService;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Configuration;
using System.Web;

namespace AcMeERP.MobileService
{
    public class AbstractController : ApiController
    {
        private CommonMember memberset = new CommonMember();
        public List<ServiceResult> Post(HttpRequestMessage request)
        {
            // new ErrorLog().WriteMobileServiceError("Service Started");
            // new ErrorLog().WriteMobileServiceError("Received Data:"+request.ToString());

            List<ServiceResult> objArray = new List<ServiceResult>();

            AESEncryptionDecryption _Decrypt = new AESEncryptionDecryption();

            ServiceResult objResult = new ServiceResult();
            var abstractjsonString = request.Content.ReadAsStringAsync().Result;
            // new ErrorLog().WriteMobileServiceError("Json String :" + loginjsonString);
            JArray array = JArray.Parse(abstractjsonString);
            // new ErrorLog().WriteMobileServiceError("After arrya Parse :" + array.ToString());
            AbstractData dtData = JsonConvert.DeserializeObject<AbstractData>(array[0].ToString());
            string errorMessage = ValidateAbstractData(dtData);
              if (string.IsNullOrEmpty(errorMessage))
              {
                  switch (dtData.MobileRequest)
                  {
                      case "abstract":
                          try
                          {
                              new ErrorLog().WriteMobileServiceError("Reached Abstract Controller");
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
                                      if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                      {
                                          //Get All Branches of the Head office
                                          mobileServiceSystem.BranchOfficeCode = dtData.BranchOfficeCode;
                                          mobileServiceSystem.ProjectName = dtData.ProjectName;
                                          mobileServiceSystem.DateFrom = this.memberset.DateSet.ToDate(dtData.DateFrom.ToString(), false);
                                          mobileServiceSystem.DateTo = this.memberset.DateSet.ToDate(dtData.DateTo.ToString(), false);
                                          resultArgs = mobileServiceSystem.GetAbstractDetail();
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
                                      objResult.Data.Add("Abstract", resultArgs.DataSource.TableSet);
                                  }
                                  else
                                  {
                                      objResult.Status = MessageCatalog.MobileService.FalseStatus;
                                      objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                                      objResult.ErrorMessage = string.IsNullOrEmpty(resultArgs.Message) ? MessageCatalog.MobileService.ResultsNotFound : resultArgs.Message;
                                  }
                                  new ErrorLog().WriteMobileServiceError("AbstractController Service Request Completed");
                              }
                          }
                          catch (Exception ex)
                          {
                              objResult.Status = MessageCatalog.MobileService.FalseStatus;
                              objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                              objResult.ErrorMessage = "Error Getting Abstract Details for the Branch";
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
        private string ValidateAbstractData(AbstractData dtData)
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
            else if (string.IsNullOrEmpty(dtData.DateFrom))
            {
                Message = MessageCatalog.MobileService.DateFromEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.DateTo))
            {
                Message = MessageCatalog.MobileService.DateToEmpty;
            }
            else if (string.IsNullOrEmpty(dtData.ProjectName))
            {
                Message = MessageCatalog.MobileService.ProjectNameEmpty;
            }
            return Message;
        }
    }
     
}