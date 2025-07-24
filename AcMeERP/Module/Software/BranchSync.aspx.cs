using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using System.IO;
using AcMeERP.Base;
using Bosco.Model.UIModel;
using Bosco.DAO.Data;
using System.ServiceModel;

namespace AcMeERP.Module.Software
{
    public partial class BranchSync : System.Web.UI.Page
    {
        AcMeERP.Base.UIBase objBase = new UIBase();

        private string HeadOfficeCode
        {
            set
            {
                ViewState["HeadOfficeCode"] = value;
            }
            get
            {
                return (ViewState["HeadOfficeCode"] != null ? ViewState["HeadOfficeCode"].ToString() : string.Empty);
            }
        }

        private string BranchOfficeCode
        {
            set
            {
                ViewState["BranchOfficeCode"] = value;
            }
            get
            {
                return (ViewState["BranchOfficeCode"] != null ? ViewState["BranchOfficeCode"].ToString() : string.Empty);
            }
        }

        private string BranchLocation
        {
            set
            {
                ViewState["BranchLocation"] = value;
            }
            get
            {
                return (ViewState["BranchLocation"] != null ? ViewState["BranchLocation"].ToString() : string.Empty);
            }
        }

        private string CurrentFY
        {
            set
            {
                ViewState["CurrentFY"] = value;
            }
            get
            {
                return (ViewState["CurrentFY"] != null ? ViewState["CurrentFY"].ToString() : string.Empty);
            }
        }

        private string BranchAction
        {
            set
            {
                ViewState["BranchAction"] = value;
            }
            get
            {
                return (ViewState["BranchAction"] != null ? ViewState["BranchAction"].ToString() : string.Empty);
            }
        }

        private string FileName
        {
            set
            {
                ViewState["FileName"] = value;
            }
            get
            {
                return (ViewState["FileName"] != null ? ViewState["FileName"].ToString() : string.Empty);
            }
        }

        private string FileDescription
        {
            set
            {
                ViewState["FileDescription"] = value;
            }
            get
            {
                return (ViewState["FileDescription"] != null ? ViewState["FileDescription"].ToString() : string.Empty);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            result.Message = "Uploader is not yet initialised";
            
            string uploadPath = string.Empty;
            try
            {
                if (Request.Headers["HeadOfficeCode"] != null && Request.Headers["BranchOfficeCode"] != null && Request.Headers["BranchLocation"] != null  &&
                    Request.Headers["BranchAction"] != null)
                {
                    HeadOfficeCode = Request.Headers["HeadOfficeCode"].ToString();
                    BranchOfficeCode = Request.Headers["BranchOfficeCode"].ToString();
                    BranchLocation = Request.Headers["BranchLocation"].ToString();
                    BranchAction = Request.Headers["BranchAction"].ToString();
                    
                    result = FillBranchDetails(HeadOfficeCode, HeadOfficeCode + BranchOfficeCode);
                }
                
                if (Request.Headers["FileName"] != null && Request.Headers["FileDescription"] != null)
                {
                    FileName = Request.Headers["FileName"].ToString();
                    FileDescription = Request.Headers["FileDescription"].ToString();
                    CurrentFY = Request.Headers["CurrentFY"].ToString();
                }

                if (result.Success)
                {
                    //Stream stream = Request.GetBufferlessInputStream();
                    uploadPath = getUploadPath();
                    if (!string.IsNullOrEmpty(uploadPath) && Request.InputStream != null)
                    {
                        Stream stream = Request.InputStream;
                        string path = Path.Combine(uploadPath, FileName);
                        using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
                        {
                            stream.CopyTo(outputFileStream);
                        }

                        //For Branch Voucher Files - Update into Voucher File detials against concern voucher
                        if (BranchAction == BranchUploadAction.BranchVoucherAttachFiles.ToString())
                        {

                        }

                        /*foreach (string f in Request.Files.AllKeys)
                        {
                            HttpPostedFile file = Request.Files[f];
                            file.SaveAs(Path.Combine(uploadPath, file.FileName));
                            break;
                        }*/
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            finally
            {
                HttpContext.Current.Response.Clear();
                if (!result.Success)
                {
                    HttpContext.Current.Response.Write(result.Message);
                }                
                HttpContext.Current.Response.End();
            }
        }

        private string getUploadPath()
        {
            string rtn = string.Empty;
            string UploadPath = Path.Combine(PagePath.ApplicationPhysicalPath, @"Module\Software\Uploads\Acmeerp_Bramp_Temp_Files\", HeadOfficeCode);
            BranchUploadAction action = (BranchUploadAction)Enum.Parse(typeof(BranchUploadAction), BranchAction, true);

            switch (action)
            {
                case BranchUploadAction.BranchReport:
                    { //1. For FY Based uploading Branch reports
                        UploadPath = Path.Combine(PagePath.ApplicationPhysicalPath, @"Module\Software\Uploads\Acmeerp_Branch_Reports\", HeadOfficeCode);
                        rtn = Path.Combine(UploadPath, CurrentFY);
                        break;
                    }
                case BranchUploadAction.BranchVouchers:
                    { //2. For uploading Branch Voucher to data sycn path
                        rtn = Path.Combine(PagePath.ApplicationPhysicalPath, @"Module\Software\Uploads\AcMEERP_Vouchers");
                        break;
                    }
                case BranchUploadAction.BranchDatabase:
                    {  //3. For uploading Branch databack up
                        rtn = Path.Combine(PagePath.ApplicationPhysicalPath, @"Module\Software\Uploads\AcMEERPBackup\", HeadOfficeCode);
                        break;
                    }
                case BranchUploadAction.BranchVoucherAttachFiles:
                    {
                        //4. For uploading voucher files
                        rtn = Path.Combine(PagePath.ApplicationPhysicalPath, @"Module\Software\Uploads\Acmeerp_Branch_Voucher_Files\", HeadOfficeCode);
                        rtn = Path.Combine(UploadPath, CurrentFY);
                        break;
                    }
                default:
                    {
                        UploadPath = Path.Combine(PagePath.ApplicationPhysicalPath, @"Module\Software\Uploads\Acmeerp_Bramp_Temp_Files\", HeadOfficeCode);
                        break;
                    }
            }

            return rtn;
        }
        
        private ResultArgs FillBranchDetails(string headofficecode, string branchcode)
        {
            ResultArgs resultArgs = new ResultArgs();

            try
            {
                if (!(string.IsNullOrEmpty(headofficecode)) && !(string.IsNullOrEmpty(headofficecode)))
                {
                    resultArgs = IsBranchExists(headofficecode.Trim(), branchcode.Trim());
                    if (resultArgs.Success)
                    {
                        resultArgs.Success = true;
                        /*objBase.HeadOfficeCode = HeadOfficeCode;//To Connect Head Office Database
                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = HeadOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = BranchOfficeCode;
                            Int32 branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, BranchOfficeCode);
                            if (branchOfficeId > 0)
                            {
                                resultArgs.Success = true;
                            }
                            else
                            {
                                resultArgs.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                            }
                        }*/
                    }
                    else
                    {
                        resultArgs.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    }
                }
                else
                {
                    resultArgs.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }
            finally
            {
                
            }

            return resultArgs;
        }

        public ResultArgs IsBranchExists(string headOfficeCode, string branchOfficeCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            
            try
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = branchOfficeSystem.IsBranchExists(headOfficeCode, branchOfficeCode);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0 && resultArgs.DataSource != null)
                    {
                        resultArgs.Success = true;
                    }
                    else
                    {
                        resultArgs.Message = !string.IsNullOrEmpty(resultArgs.Message) ? resultArgs.Message : MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    }
                }
            }
            catch (Exception er)
            {
                resultArgs.Message = !string.IsNullOrEmpty(resultArgs.Message) ? resultArgs.Message : MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
            }
            return resultArgs;
        }
    }
}