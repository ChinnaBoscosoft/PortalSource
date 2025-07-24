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
 * Purpose          :This page helps the head office admin user to import the new branch offices details to head office
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using System.IO;
using System.Data;
using System.Data.OleDb;
using Bosco.Model.UIModel;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Office
{
    public partial class ImportBranches : Base.UIBase
    {
        #region Declaration

        string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0";
        OleDbConnection connectionExcel;
        OleDbCommand cmdExcel;
        OleDbDataAdapter adpExcel;
        DataTable dtBranchOffice;
        DataTable dtBranchExecutive;
        ResultArgs resultArgs = new ResultArgs();
        string[] TableName = { "BranchOffice", "GoverningMember" };
        public const string SheetBranchOffice = "BranchOffice";
        public const string SheetGoverningMember = "GoverningMember";
        string CountryCode = string.Empty;
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = "Import Branches";
                imgDownloadBranchTempleate.ToolTip = "Click to download the branch office template";
                ShowLoadWaitPopUp(btnUpload);
            }
        }


        protected void imgDownloadBranchTempleate_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = "Branch_Template" + DateTime.Now.ToString(DateFormatInfo.MySQLFormat.DateTime).ToString() + ".xlsx";
                byte[] bytes;
                bytes = File.ReadAllBytes(PagePath.AcMEERPBranchTemplateFilePath);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                File.Delete(PagePath.AppFilePath + FileName);
                Response.End();

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Branches:" + ex.Message);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string UploadFileDirectory = string.Empty;
            try
            {
                string FileName = UlcFileUpload.FileName;
                if (UlcFileUpload.IsValid)
                {
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        UploadFileDirectory = Server.MapPath("~/AppFile/") + FileName;
                        UlcFileUpload.SaveAs(UploadFileDirectory);
                        new ErrorLog().WriteError("Import Branches - " + "File is Saved");
                        resultArgs = UploadData(UploadFileDirectory);
                        if (!resultArgs.Success)
                        {
                            new ErrorLog().WriteError(resultArgs.Message);
                            meoSummary.Text += "\n" + resultArgs.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Branches:" + ex.Message);

            }
        }

        #endregion

        #region Methods

        private ResultArgs UploadData(string FileName)
        {
            dtBranchOffice = new DataTable();
            dtBranchExecutive = new DataTable();
            try
            {
                conString = String.Format(conString, FileName);
                connectionExcel = new OleDbConnection(conString);
                cmdExcel = new OleDbCommand();
                adpExcel = new OleDbDataAdapter();
                cmdExcel.Connection = connectionExcel;
                connectionExcel.Open();
                DataTable dtExcelSchema = connectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dtExcelSchema.Rows.Count > 0)
                {
                    for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
                    {
                        switch (dtExcelSchema.Rows[i]["TABLE_NAME"].ToString().TrimEnd('$'))
                        {
                            case SheetBranchOffice:
                                cmdExcel.CommandText = "SELECT * From [" + SheetBranchOffice + "$]";
                                adpExcel.SelectCommand = cmdExcel;
                                adpExcel.Fill(dtBranchOffice);
                                dtBranchOffice = RemoveEmptyRows(dtBranchOffice);
                                resultArgs = UpdateBranchOfficeDetails();
                                break;
                            case SheetGoverningMember:
                                cmdExcel.CommandText = "SELECT * From [" + SheetGoverningMember + "$]";
                                adpExcel.SelectCommand = cmdExcel;
                                adpExcel.Fill(dtBranchExecutive);
                                dtBranchExecutive = RemoveEmptyRows(dtBranchExecutive);
                                resultArgs = UpdateGoverningDetails();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
            finally { connectionExcel.Close(); }
            return resultArgs;
        }

        private ResultArgs UpdateBranchOfficeDetails()
        {
            int RecordCount = 0;
            meoSummary.Text = "------------------------------------------ Notifications --------------------------------------------";
          //  meoSummary.Text += "\n" + "Notifications";
            foreach (DataRow BranchOffice in dtBranchOffice.Rows)
            {
                if (ValidateFields(BranchOffice))
                {
                    if (!(IsBranchOfficeCodeAvailable(BranchOffice["Head Office Code"].ToString() + BranchOffice["Code/UserName"].ToString())))
                    {
                        resultArgs = CreateBranch(BranchOffice);
                        if (resultArgs.Success)
                        {
                            new ErrorLog().WriteError(BranchOffice["Head Office Code"].ToString() + " is Created");
                            RecordCount++;
                        }
                        else
                        {
                            new ErrorLog().WriteError(BranchOffice["Head Office Code"].ToString() + " is not Created");
                        }
                    }
                    else
                    {
                        meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " is available ";
                        new ErrorLog().WriteError(BranchOffice["Code/UserName"].ToString() + " - " + "Code is available");
                        resultArgs.Message = BranchOffice["Code/UserName"].ToString() + " is available ";
                    }
                }
            }
            meoSummary.Text += RecordCount > 0 ? "\n" + "Branches are imported successfully" : "\n" + "Importing branches is failed";
            meoSummary.Text += "\n" + "--------------------------------- Import Branch Summary --------------------------------------";
            meoSummary.Text += "\n" + "Total Records          =" + dtBranchOffice.Rows.Count.ToString();
            meoSummary.Text += "\n" + "Created Branches    =" + RecordCount.ToString();
            meoSummary.Text += "\n" + "Skipped Branches    =" + (dtBranchOffice.Rows.Count - RecordCount).ToString();
            return resultArgs;
        }

        private ResultArgs UpdateGoverningDetails()
        {
            int RecordCount = 0;
            meoSummary.Text += "\n" + "------------------------------------ Governing Member ------------------------------------------";

            meoSummary.Text += "\n" + "Import Governing Member Started";
            foreach (DataRow BranchExecutive in dtBranchExecutive.Rows)
            {
                if (ValidateExecutiveFields(BranchExecutive))
                {
                    resultArgs = InsertGoverningMember(BranchExecutive);
                    if (resultArgs.Success)
                    {
                        new ErrorLog().WriteError(BranchExecutive["Executive Name"].ToString() + " is Created");
                        RecordCount++;
                    }
                    else
                    {
                        new ErrorLog().WriteError(BranchExecutive["Executive Name"].ToString() + " is not Created");
                    }
                }
            }
            meoSummary.Text += RecordCount > 0 ? "\n" + "Governing Members are imported successfully" : "\n" + "Importing branches is failed";
            meoSummary.Text += "\n" + "-------------------------------Import Governing Members Summary---------------------------";
            meoSummary.Text += "\n" + "Total Records                     =" + dtBranchExecutive.Rows.Count.ToString();
            meoSummary.Text += "\n" + "Created GoverningMember  =" + RecordCount.ToString();
            meoSummary.Text += "\n" + "Skipped GoverningMember  =" + (dtBranchExecutive.Rows.Count - RecordCount).ToString();
            meoSummary.Text += "\n" + "Import Governing Member Ended";
            meoSummary.Text += "\n" + "------------------------------------------------------------------------------------------------------";
            return resultArgs;

        }

        private DataTable RemoveEmptyRows(DataTable dtBranch)
        {
            try
            {

                for (int i = 0; i <= dtBranch.Rows.Count - 1; i++)
                {
                    string valuesarr = string.Empty;
                    List<object> lst = new List<object>(dtBranch.Rows[i].ItemArray);
                    foreach (object s in lst)
                    {
                        valuesarr += s.ToString();
                    }
                    if (string.IsNullOrEmpty(valuesarr))
                    {
                        //Remove row here, this row do not have any value 
                        dtBranch.Rows[i].Delete();
                        valuesarr = string.Empty;
                    }
                }
                dtBranch.AcceptChanges();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("RemoveEmptyRows::" + ex.Message);
            }
            return dtBranch;
        }

        private bool ValidateFields(DataRow BranchOffice)
        {
            int CountryId = 0;
            bool IsRecordValid = true;
            try
            {

                if (string.IsNullOrEmpty(BranchOffice["Code/UserName"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - Branch code is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchOffice["Head Office Code"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - Head office code is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchOffice["Name"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - E-Mail is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchOffice["Phone No"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - Phone No is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchOffice["Mobile No"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - Mobile No is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchOffice["Country"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - Country is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchOffice["State/Province"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - State/Province is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchOffice["Postal/Zip Code"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " - Postal/Zip Code is empty, this is skipped";
                }
                if (!string.IsNullOrEmpty(BranchOffice["Country"].ToString()))
                {
                    CountryId = FetchCountryId(BranchOffice["Country"].ToString());
                    if (CountryId == 0)
                    {
                        IsRecordValid = false;
                        meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " has mismatching country, this is skipped";
                    }
                }
                if (!string.IsNullOrEmpty(BranchOffice["State/Province"].ToString()))
                {
                    int StateId = 0;
                    StateId = FecthStateId(BranchOffice["State/Province"].ToString(), CountryId);
                    if (StateId == 0)
                    {
                        IsRecordValid = false;
                        meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " has mismatching State, this is skipped";
                    }
                }
                if (BranchOffice["Is Sub Branch"].ToString() == "Yes")
                {
                    if (string.IsNullOrEmpty(BranchOffice["Associated Branch Code"].ToString()))
                    {
                        IsRecordValid = false;
                        meoSummary.Text += "\n" + BranchOffice["Code/UserName"].ToString() + " associated branch is empty,this is skipped";
                    }
                }
            }


            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Branches ValidateFields() - " + ex.Message);
            }
            return IsRecordValid;
        }

        private bool ValidateExecutiveFields(DataRow BranchExecutive)
        {
            int CountryId = 0;
            bool IsRecordValid = true;
            try
            {
                if (string.IsNullOrEmpty(BranchExecutive["Executive Name"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - Governing Member name is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchExecutive["Date of Birth"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - Date of Birth is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchExecutive["Date of Joining"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - Date of Joining is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchExecutive["Nationality"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - Nationality is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchExecutive["Country"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - Country is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchExecutive["State"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - State is empty, this is skipped";
                }
                else if (string.IsNullOrEmpty(BranchExecutive["Address"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - Address is empty, this is skipped";
                }
                if (!string.IsNullOrEmpty(BranchExecutive["Country"].ToString()))
                {
                    CountryId = FetchCountryId(BranchExecutive["Country"].ToString());
                    if (CountryId == 0)
                    {
                        IsRecordValid = false;
                        meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " has mismatching country, this is skipped";
                    }
                }
                if (!string.IsNullOrEmpty(BranchExecutive["State"].ToString()))
                {
                    int StateId = 0;
                    StateId = FecthStateId(BranchExecutive["State"].ToString(), CountryId);
                    if (StateId == 0)
                    {
                        IsRecordValid = false;
                        meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " has mismatching State, this is skipped";
                    }
                }
                if (string.IsNullOrEmpty(BranchExecutive["Pincode"].ToString()))
                {
                    IsRecordValid = false;
                    meoSummary.Text += "\n" + BranchExecutive["Executive Name"].ToString() + " - PinCode is empty, this is skipped";
                }
            }

            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import GoverningMember ValidateFields() - " + ex.Message);
            }
            return IsRecordValid;
        }

        private bool IsBranchOfficeCodeAvailable(string branchOfficePartCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (BranchOfficeSystem officesystem = new BranchOfficeSystem())
                {
                    resultArgs = officesystem.BranchOfficeDetailsByCodeAvailable(branchOfficePartCode);
                }
                if (resultArgs.Success)
                {
                    DataTable dtoffice = resultArgs.DataSource.Table;
                    if (dtoffice != null)
                    {
                        if (dtoffice.Rows.Count == 1)
                        {
                            resultArgs.Success = true;
                        }
                        else
                        {
                            resultArgs.Success = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return resultArgs.Success;
        }

        private ResultArgs CreateBranch(DataRow drBranch)
        {
            bool IsSuccess = false;
            bool IsRecordValid = true;
            int BranchOfficeId = 0;
            string BranchOfficeCodeUpdate = string.Empty;
            try
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                {
                    //Storing the excel record values with branch office system properties.
                    branchOfficeSystem.BranchOfficeId = 0;
                    branchOfficeSystem.HeadOffice_Code = drBranch["Head Office Code"].ToString().ToLower().Trim();
                    branchOfficeSystem.BranchOfficeName = drBranch["Name"].ToString().Trim();
                    branchOfficeSystem.BranchOfficeCode = (drBranch["Head Office Code"].ToString().Trim() + drBranch["Code/UserName"].ToString().Trim()).ToLower();
                    branchOfficeSystem.BranchPartCode = drBranch["Code/UserName"].ToString().Trim().ToLower();
                    if (string.IsNullOrEmpty(drBranch["Deployment Model"].ToString()))
                    {
                        branchOfficeSystem.Deployment_Type = 0;
                    }
                    else
                    {
                        if (drBranch["Deployment Model"].ToString() == "Standalone")
                        {
                            branchOfficeSystem.Deployment_Type = 0;
                        }
                        else if (drBranch["Deployment Model"].ToString() == "Client-Server")
                        {
                            branchOfficeSystem.Deployment_Type = 1;
                        }
                    }

                    branchOfficeSystem.Country_Id = FetchCountryId(drBranch["Country"].ToString().Trim());
                    branchOfficeSystem.State_Id = FecthStateId(drBranch["State/Province"].ToString().Trim(), branchOfficeSystem.Country_Id);
                    branchOfficeSystem.City = drBranch["City"].ToString().Trim();

                    branchOfficeSystem.PinCode = drBranch["Postal/Zip Code"].ToString().Trim();
                    branchOfficeSystem.Address = drBranch["Address"].ToString().Trim();
                    branchOfficeSystem.PhoneNo = drBranch["Phone No"].ToString().Trim();
                    branchOfficeSystem.MobileNo = drBranch["Mobile No"].ToString().Trim();

                    branchOfficeSystem.BranchEmail = drBranch["E-Mail"].ToString().Trim();
                    branchOfficeSystem.CountryCode = string.Empty;

                    branchOfficeSystem.Status = 1;//1- Created in add
                    branchOfficeSystem.ModifiedDate = DateTime.Now;
                    branchOfficeSystem.ModifiedBy = base.LoginUser.LoginUserId;
                    branchOfficeSystem.CreatedDate = DateTime.Now;
                    branchOfficeSystem.CreatedBy = base.LoginUser.LoginUserId;

                    branchOfficeSystem.PersonIncharge = drBranch["Person Incharge"].ToString().Trim();
                    //branchOfficeSystem.IsSubBranch = rbtnIsSubbranch.Visible ? 1 : 0;//1-SubBranch-0-MainBranch
                    //branchOfficeSystem.AssociateBranchCode = rbtnIsSubbranch.Visible ? base.LoginUser.LoginUserBranchOfficeCode : string.Empty;
                    branchOfficeSystem.IsSubBranch = drBranch["Is Sub Branch"].ToString() == "Yes" ? 1 : 0;
                    branchOfficeSystem.AssociateBranchCode = drBranch["Associated Branch Code"].ToString().Trim();

                    if (!this.HasRowId)
                    {
                        LicenseSystem licenseSystem = new LicenseSystem();
                        branchOfficeSystem.BranchKeyCode = licenseSystem.GetNewNumber(NumberFormats.BranchKeyUniqueCode, "", "");
                    }
                    if (BranchOfficeId == 0)
                    {
                        BranchOfficeCodeUpdate = (drBranch["Head Office Code"].ToString().Trim() + drBranch["Code/UserName"].ToString().ToLower());
                    }
                    branchOfficeSystem.BranchOfficeCodeUpdate = BranchOfficeCodeUpdate;
                    resultArgs = branchOfficeSystem.SaveBranchOfficeDetails(DataBaseType.Portal);
                    if (resultArgs.Success)
                    {
                        ResultArgs HeadOfficeArs = null;  //To save details in the head office branch table
                        base.HeadOfficeCode = drBranch["Head Office Code"].ToString().Trim();

                        HeadOfficeArs = branchOfficeSystem.SaveBranchOfficeDetails(DataBaseType.HeadOffice);
                        if (HeadOfficeArs.Success)
                        {
                            ResultArgs mailResultArgs;
                            //   this.Message = MessageCatalog.Message.BranchOfficeSaved;
                            if (BranchOfficeId == 0)
                            {
                                string Name = "Branch Admin";
                                string Header = " Your Branch Office account with Acme.erp has been created successfully. Please wait for approval. " +
                                                "<br/>Once the account is approved you will get an email with the login details.";
                                string MainContent = "<b>Your account details available with us are as follows:</b><br/><br/>" +
                                                    "Branch Office Code: " + drBranch["Head Office Code"].ToString().Trim() + drBranch["Code/UserName"].ToString().ToLower() + "<br/>" +
                                                    "Branch Office Name: " + drBranch["Name"].ToString() + "<br/>" +
                                                    "Address: " + drBranch["Address"].ToString() + "," + drBranch["Country"].ToString() + "," + drBranch["State/Province"].ToString().Trim() + "," + drBranch["Postal/Zip Code"].ToString() + "<br/" +
                                                    "Mobile Number: " + drBranch["Mobile No"].ToString().Trim() + "<br/>" +
                                                    "Email: " + drBranch["E-Mail"].ToString().Trim() + "<br/>";

                                string content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);
                                mailResultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(drBranch["E-Mail"].ToString().Trim()),
                                CommonMethod.RemoveFirstValue(drBranch["E-Mail"].ToString().Trim()),
                                "Branch Office Created,Waiting for Approval", content,false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Branches CreateBranches() - " + ex.Message);
            }
            return resultArgs;
        }

        private ResultArgs InsertGoverningMember(DataRow drBranchExecutive)
        {
            try
            {
                using (ExecutiveMemberSystem executiveMemberSystem = new ExecutiveMemberSystem())
                {
                    // Storing the excel record values with executiveMemberSystem system properties.
                    executiveMemberSystem.ExecutiveId = 0;
                    executiveMemberSystem.ExecutiveName = drBranchExecutive["Executive Name"].ToString().ToLower().Trim();
                    executiveMemberSystem.FatherName = string.Empty.Trim();
                    executiveMemberSystem.DateOfBirth = drBranchExecutive["Date of Birth"].ToString().Trim();
                    executiveMemberSystem.Religion = drBranchExecutive["Religion"].ToString().Trim();
                    executiveMemberSystem.Role = string.Empty.Trim();
                    executiveMemberSystem.Nationality = drBranchExecutive["Nationality"].ToString().Trim();
                    executiveMemberSystem.Occupation = drBranchExecutive["Occupation"].ToString().Trim();
                    executiveMemberSystem.Association = string.Empty.Trim();
                    executiveMemberSystem.OfficeBearer = string.Empty.Trim();
                    executiveMemberSystem.Place = string.Empty.Trim();
                    executiveMemberSystem.CountryId = FetchCountryId(drBranchExecutive["Country"].ToString().Trim());
                    executiveMemberSystem.StateId = FecthStateId(drBranchExecutive["State"].ToString().Trim(), executiveMemberSystem.CountryId);
                    executiveMemberSystem.Address = drBranchExecutive["Address"].ToString().Trim();
                    executiveMemberSystem.PinCode = drBranchExecutive["Pincode"].ToString().Trim();
                    executiveMemberSystem.Pan_SSN = drBranchExecutive["Pan No"].ToString().Trim();
                    executiveMemberSystem.Phone = drBranchExecutive["Phone"].ToString().Trim();
                    executiveMemberSystem.Fax = string.Empty.Trim();
                    executiveMemberSystem.EMail = drBranchExecutive["Email"].ToString().Trim();
                    executiveMemberSystem.URL = string.Empty.Trim();
                    executiveMemberSystem.DateOfAppointment = drBranchExecutive["Date of Joining"].ToString().Trim();
                    executiveMemberSystem.DateOfExit = drBranchExecutive["Date of Exit"].ToString().Trim();
                    executiveMemberSystem.Notes = string.Empty.Trim();
                    resultArgs = executiveMemberSystem.SaveExecutiveMemberDetails(DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                      //  this.Message = "saved";
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Branches CreateBranches() - " + ex.Message);
            }
            return resultArgs;
        }

        private int FecthStateId(string State, int CountryId)
        {
            int StateId = 0;
            ResultArgs resultArgs = new ResultArgs();
            using (HeadOfficeSystem headSystem = new HeadOfficeSystem())
            {
                headSystem.Country_Id = CountryId;
                resultArgs = headSystem.FetchStateByCountry();
            }
            if (resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                DataView dvState = resultArgs.DataSource.Table.DefaultView;
                dvState.RowFilter = "STATE='" + State + "'";
                if (dvState.ToTable().Rows.Count > 0)
                {
                    StateId = this.Member.NumberSet.ToInteger(dvState.ToTable().Rows[0]["STATE_ID"].ToString());
                }
                dvState.RowFilter = "";
            }
            return StateId;
        }

        private int FetchCountryId(string CountryName)
        {
            ResultArgs resultArgs = new ResultArgs();
            int CountryId = 0;
            try
            {
                using (HeadOfficeSystem HeadSystem = new HeadOfficeSystem())
                {
                    resultArgs = HeadSystem.FetchCountry();
                }
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataView dvCountry = resultArgs.DataSource.Table.DefaultView;
                    dvCountry.RowFilter = "COUNTRY='" + CountryName + "'";
                    if (dvCountry.ToTable().Rows.Count > 0)
                    {
                        CountryId = this.Member.NumberSet.ToInteger(dvCountry.ToTable().Rows[0]["COUNTRY_ID"].ToString());
                        CountryCode = dvCountry.ToTable().Rows[0]["COUNTRY_CODE"].ToString();
                    }
                    dvCountry.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Branches FetchCountryId() - " + ex.Message);
            }
            return CountryId;
        }

        #endregion
    }
}