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
    public class BranchDataStatusController : ApiController
    {
        public List<ServiceResult> Post(HttpRequestMessage request)
        {
            List<ServiceResult> objArray = new List<ServiceResult>();

            AESEncryptionDecryption _Decrypt = new AESEncryptionDecryption();

            ServiceResult objResult = new ServiceResult();
            var loginjsonString = request.Content.ReadAsStringAsync().Result;
            // new ErrorLog().WriteMobileServiceError("Json String :" + loginjsonString);
            JArray array = JArray.Parse(loginjsonString);
            // new ErrorLog().WriteMobileServiceError("After arrya Parse :" + array.ToString());
            BranchStatusData dtData = JsonConvert.DeserializeObject<BranchStatusData>(array[0].ToString());

            switch (dtData.MobileRequest)
            {
                case "branchdatastatus":
                    try
                    {
                        new ErrorLog().WriteMobileServiceError("Reached BranchDataStatus Controller");
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
                                        mobileServiceSystem.BranchOfficeCode = dtData.BranchOfficeCode.Trim();
                                        mobileServiceSystem.ProjectName = dtData.ProjectName;
                                        resultArgs = mobileServiceSystem.GetDataSynStatusProjectWise();
                                    }
                                    else
                                    {
                                        resultArgs.Message = MessageCatalog.MobileService.InvalidLogin;
                                    }
                                }

                                new ErrorLog().WriteMobileServiceError("BranchDataStatus Controller Service Request Completed");
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
                        }

                    }
                    catch (Exception ex)
                    {
                        objResult.Status = MessageCatalog.MobileService.FalseStatus;
                        objResult.ErrorCode = MessageCatalog.MobileService.InvalidLoginErrorCode;
                        objResult.ErrorMessage = "Error in Getting Branch Data Status";
                        new ErrorLog().WriteMobileServiceError(ex.Message);
                    }
                    break;

            }
            objArray.Add(objResult);
            return objArray;
        }
    }
}