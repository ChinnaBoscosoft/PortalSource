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

    public class LoginController : ApiController
    {
        //public string securityKey = ConfigurationManager.AppSettings["securityKey"].ToString();
        // POST api/<controller>
        // This post method will help for login validations
        //

        public List<ServiceResult> Post(HttpRequestMessage request)
        {
            // new ErrorLog().WriteMobileServiceError("Service Started");
            // new ErrorLog().WriteMobileServiceError("Received Data:"+request.ToString());
            AESEncryptionDecryption _Decrypt = new AESEncryptionDecryption();
            List<ServiceResult> objArray = new List<ServiceResult>();
            ServiceResult objResult = new ServiceResult();
            var loginjsonString = request.Content.ReadAsStringAsync().Result;
            // new ErrorLog().WriteMobileServiceError("Json String :" + loginjsonString);
            JArray array = JArray.Parse(loginjsonString);
            // new ErrorLog().WriteMobileServiceError("After arrya Parse :" + array.ToString());
            LoginData dtData = JsonConvert.DeserializeObject<LoginData>(array[0].ToString());
            string errorMessage=ValidateLoginData(dtData);
            if (string.IsNullOrEmpty(errorMessage))
            {
                switch (dtData.MobileRequest)
                {
                    case "login":
                        try
                        {
                            new ErrorLog().WriteMobileServiceError("Reached Login");
                            using (MobileServiceSystem mobileServiceSystem = new MobileServiceSystem())
                            {
                                //Connect to the Concern Head Office Database
                                mobileServiceSystem.HeadOfficeCode = dtData.HeadOfficeCode.Trim();
                                ResultArgs resultArgs = mobileServiceSystem.GetHeadOfficeDBConnection();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    mobileServiceSystem.UserName = dtData.Username.Trim(); //  dtData.Username; 
                                    mobileServiceSystem.Password = dtData.Password.Trim(); // dtData.Password;
                                    resultArgs = mobileServiceSystem.GetLoginDetails();
                                }

                                if (resultArgs.Success)
                                {
                                    objResult.Status = MessageCatalog.MobileService.SuccessStatus;
                                    objResult.ErrorCode = MessageCatalog.MobileService.SuccessErrorCode;
                                    objResult.Data.Add("UserBranchDetails", resultArgs.DataSource.TableSet);
                                }
                                else
                                {
                                    objResult.Status = MessageCatalog.MobileService.FalseStatus;
                                    objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                                    objResult.ErrorMessage = string.IsNullOrEmpty(resultArgs.Message) ? MessageCatalog.MobileService.InvalidLogin : resultArgs.Message;
                                }
                                new ErrorLog().WriteMobileServiceError("Login Service Request Completed");
                            }
                        }
                        catch (Exception ex)
                        {
                            objResult.Status = MessageCatalog.MobileService.FalseStatus; ;
                            objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                            objResult.ErrorMessage = "Error in Login";
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

        private string ValidateLoginData(LoginData dtData)
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
            return Message;
        }
    }

}