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
 * Purpose          :This page helps the head office admin user to import the head office master data from excel
 *****************************************************************************************************/
using System;
using System.Web;
using Bosco.Utility;
using System.Data;
using System.Data.OleDb;
using Bosco.Model.UIModel;
using Bosco.DAO.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AcMeERP.Module.Office
{
    public partial class ImportMasters : Base.UIBase
    {
        #region Declaration

        DataSet dsMasters;
        string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0";
        OleDbConnection connectionExcel;
        OleDbCommand cmdExcel;
        OleDbDataAdapter adpExcel;
        DataTable dtSheet;
        ResultArgs resultArgs = null;
        public int SortOrder = 0;
        private bool isLegalEntityAvailable = false;
        private bool isProjectCategoryAvailable = false;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.ImportMastersPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.ImportMasterData, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.ShowLoadWaitPopUp(btnUpload);
                imgDownloadMastersTempleate.ToolTip = "Click to Download Masters Template";
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
                        new ErrorLog().WriteError("Import Masters - " + "File is Saved");
                        if (ValidateFile(UploadFileDirectory))
                        {
                            meoSummary.Text = "-----------------------------------------------------------------------------------------";
                            meoSummary.Text += "\n" + "Import Masters Started";
                            meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";

                            meoSummary.Text += "\n" + "Importing Legal Entities is Started";
                            if (ImportLegalEntity())
                            {
                                meoSummary.Text += dsMasters.Tables["Legal Entity"].Rows.Count > 0 ? "\n" + "Legal Entity  is Imported Successfully" : "\n" + "Legal Entity has no records";
                                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                meoSummary.Text += "\n" + "Importing Project Category is Started";
                                if (ImportProjectCategroy())
                                {
                                    meoSummary.Text += dsMasters.Tables["Project Category"].Rows.Count > 0 ? "\n" + "Project Categories are Imported Successfully" : "\n" + "Project Category has no records";
                                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                    meoSummary.Text += "\n" + "Importing Projects is Started .... ";
                                    if (ImportProject())
                                    {
                                        meoSummary.Text += dsMasters.Tables["Project"].Rows.Count > 0 ? "\n" + "Projects are Imported Successfully" : "\n" + "Project has no records";
                                        meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                    }
                                    else
                                    {
                                        meoSummary.Text += "\n" + "Importing Projects is Failed";
                                        meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                    }
                                }
                                else
                                {
                                    meoSummary.Text += "\n" + "Importing Project Category is Failed";
                                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                }
                            }
                            else
                            {
                                meoSummary.Text += "\n" + "Importing Legal Entities is Failed.";
                                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                            }

                            meoSummary.Text += "\n" + "Importing Ledger Groups is Started";
                            if (ImportLedgerGroup())
                            {
                                meoSummary.Text += dsMasters.Tables["Ledger Group"].Rows.Count - 31 > 0 ? "\n" + "Ledger Groups are Imported Successfully" : "\n" + "Ledger Group has no records";
                                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                meoSummary.Text += "\n" + " Importing Ledgers is Started";
                                if (ImportLedger())
                                {
                                    meoSummary.Text += dsMasters.Tables["Ledger"].Rows.Count > 0 ? "\n" + "Ledgers are Imported Successfully" : "\n" + "Ledger has no records";
                                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                    meoSummary.Text += "\n" + "Importing Masters is Completed Successfully";
                                }
                                else
                                {
                                    meoSummary.Text += "\n" + "Importing Ledgers is Failed";
                                    meoSummary.Text += "\n" + "Importing Masters is Stopped";
                                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                                }
                            }
                            else
                            {
                                meoSummary.Text += "\n" + "Importing Ledger Groups is Failed";
                                meoSummary.Text += "\n" + "Importing Masters is Stopped";
                                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                            }

                        }
                    }
                    else
                    {
                        new ErrorLog().WriteError("Import Masters - " + "File name is empty");
                    }
                }
                else
                {
                    new ErrorLog().WriteError("Import Masters - " + "File InValid");
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters Exception - " + ex.Message);
            }
            finally
            {
                if (!string.IsNullOrEmpty(UploadFileDirectory))
                    File.Delete(UploadFileDirectory);
            }
        }

        protected void imgDownloadMastersTempleate_Click(object sender, EventArgs ed)
        {
            try
            {
                DownLoadFile();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters Download Click -" + ex.Message);
            }

        }

        #endregion

        #region Methods

        private void RemoveEmptyRows(string tableName)
        {
            try
            {

                for (int i = 0; i <= dsMasters.Tables[tableName].Rows.Count - 1; i++)
                {
                    string valuesarr = string.Empty;
                    List<object> lst = new List<object>(dsMasters.Tables[tableName].Rows[i].ItemArray);
                    foreach (object s in lst)
                    {
                        valuesarr += s.ToString();
                    }
                    if (string.IsNullOrEmpty(valuesarr))
                    {
                        //Remove row here, this row do not have any value 
                        dsMasters.Tables[tableName].Rows[i].Delete();
                        valuesarr = string.Empty;
                    }
                }
                dsMasters.Tables[tableName].AcceptChanges();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("RemoveEmptyRows::" + ex.Message);
            }
        }

        private void DownLoadFile()
        {
            try
            {
                string FileName = "Masters_Template" + DateTime.Now.ToString(DateFormatInfo.MySQLFormat.DateTime).ToString() + ".xlsx";
                byte[] bytes;
                bytes = File.ReadAllBytes(PagePath.AcMEERPMastersFilePath);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                System.IO.File.Delete(PagePath.AppFilePath + FileName);
                Response.End();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private bool ValidateFile(string FileName)
        {
            dsMasters = new DataSet();
            dtSheet = new DataTable();
            bool Success = true;
            try
            {
                conString = String.Format(conString, FileName);
                connectionExcel = new OleDbConnection(conString);
                cmdExcel = new OleDbCommand();
                adpExcel = new OleDbDataAdapter();
                cmdExcel.Connection = connectionExcel;
                connectionExcel.Open();
                DataTable dtExcelSchema = connectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                int Count = 0;
                string[] TableNames = { "Ledger Group$", "Ledger$", "Legal Entity$", "Project Category$", "Project$" };


                foreach (DataRow dr in dtExcelSchema.Rows)
                {

                    string SheetName = dr["TABLE_NAME"].ToString();
                    SheetName = SheetName.Trim(new char[] { '\'' });

                    if (!dr["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                    {
                        if (!SheetName.Equals(TableNames[Count]))
                        {
                            new ErrorLog().WriteError("Import Masters ValidateFile() - " + SheetName.TrimEnd('$') + "Sheeting is Missing");
                            meoSummary.Text += "\n" + "File is invalid";
                            meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                            Success = false;
                        }
                        else
                        {
                            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                            adpExcel.SelectCommand = cmdExcel;
                            adpExcel.Fill(dsMasters.Tables.Add(SheetName.TrimEnd('$')));
                            RemoveEmptyRows(SheetName.TrimEnd('$'));
                        }
                        Count++;
                    }
                }
            }

            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters ValidateFile() Exception - " + ex.Message);
            }
            finally { connectionExcel.Close(); }

            return Success;
        }

        #region Legal Entity

        private bool ImportLegalEntity()
        {
            bool Success = true;
            int RecordCount = 0;
            int ExistingRecordCount = 0;
            int Count = 0;
            try
            {
                if (dsMasters.Tables["Legal Entity"] != null && dsMasters.Tables["Legal Entity"].Rows.Count > 0)
                {
                    foreach (DataRow drLegalEntity in dsMasters.Tables["Legal Entity"].Rows)
                    {
                        Count++;
                        if (drLegalEntity != null)
                        {
                            if (ValidateLegalEntityFields(drLegalEntity))
                            {
                                using (LegalEntitySystem LegalEntitySystem = new LegalEntitySystem())
                                {
                                    LegalEntitySystem.CustomerId = 0;
                                    LegalEntitySystem.SocietyName = drLegalEntity["Society Name"].ToString().Trim();
                                    LegalEntitySystem.ContactPerson = drLegalEntity["Contact Person"].ToString().Trim();
                                    LegalEntitySystem.Address = drLegalEntity["Address"].ToString().Trim();
                                    LegalEntitySystem.Place = drLegalEntity["Place"].ToString().Trim();
                                    LegalEntitySystem.State = drLegalEntity["State/Province"].ToString().Trim();
                                    LegalEntitySystem.Country = drLegalEntity["Country"].ToString().Trim();
                                    LegalEntitySystem.Pincode = drLegalEntity["Pincode"].ToString().Trim();
                                    LegalEntitySystem.Phone = drLegalEntity["Phone"].ToString().Trim().Replace('-', ' ');
                                    LegalEntitySystem.Fax = drLegalEntity["Fax"].ToString().Trim();
                                    LegalEntitySystem.EMail = drLegalEntity["Email"].ToString().Trim();
                                    LegalEntitySystem.URL = drLegalEntity["Url"].ToString().Trim();
                                    LegalEntitySystem.RegNo = drLegalEntity["Society/Reg No"].ToString().Trim();
                                    if (!string.IsNullOrEmpty(drLegalEntity["Reg Date"].ToString().Trim()))
                                    {
                                        LegalEntitySystem.RegDate = this.Member.DateSet.ToDate(drLegalEntity["Reg Date"].ToString(), false);
                                    }
                                    if (!string.IsNullOrEmpty(drLegalEntity["Prior Permission Date"].ToString().Trim()))
                                    {
                                        LegalEntitySystem.PermissionDate = this.Member.DateSet.ToDate(drLegalEntity["Prior Permission Date"].ToString(), false);
                                    }
                                    LegalEntitySystem.PermissionNo = drLegalEntity["Prior Permission No"].ToString().Trim();
                                    LegalEntitySystem.A12No = drLegalEntity["12A No"].ToString().Trim();
                                    LegalEntitySystem.PANNo = drLegalEntity["PAN No"].ToString().Trim();
                                    LegalEntitySystem.GIRNo = drLegalEntity["GIR No"].ToString().Trim();
                                    LegalEntitySystem.TANNo = drLegalEntity["TAN No"].ToString().Trim();
                                    LegalEntitySystem.FCRINo = drLegalEntity["FCRA No"].ToString().Trim();
                                    if (!string.IsNullOrEmpty(drLegalEntity["FCRA Reg Date"].ToString().Trim()))
                                    {
                                        LegalEntitySystem.FCRIRegisterDate = this.Member.DateSet.ToDate(drLegalEntity["FCRA Reg Date"].ToString(), false);
                                    }

                                    LegalEntitySystem.G80No = drLegalEntity["80G No"].ToString().Trim();

                                    if (drLegalEntity["Association Nature"].ToString().Equals(Association.Others.ToString().Trim()))
                                    {
                                        LegalEntitySystem.AssociationOther = drLegalEntity["Association Nature Others"].ToString().Trim();
                                    }
                                    LegalEntitySystem.AssoicationNature = GetSelectedNatureofAssociation(drLegalEntity["Association Nature"].ToString().Trim());
                                    LegalEntitySystem.Denomination = GetDenomination(drLegalEntity["Denomination"].ToString().Trim());
                                    if (drLegalEntity["Denomination"].ToString() == Association.Others.ToString().Trim())
                                    {
                                        LegalEntitySystem.DenominationOther = drLegalEntity["Denomination Others"].ToString().Trim();
                                    }
                                    resultArgs = LegalEntitySystem.SaveLegalEntityDetails(DataBaseType.HeadOffice);
                                    if (!resultArgs.Success)
                                    {
                                        new ErrorLog().WriteError(drLegalEntity["Society Name"].ToString().Trim() + "-" + " - " + resultArgs.Message);
                                        if (resultArgs.Message == "The Record is Available")
                                            ExistingRecordCount++;
                                    }
                                    else
                                    {
                                        RecordCount++;
                                    }
                                    new ErrorLog().WriteError("Import Masters ImportLegalEntity() - " + drLegalEntity["Society Name"].ToString().Trim() + resultArgs.Exception.ToString());
                                }
                            }
                            else
                            {
                                new ErrorLog().WriteError("Import Masters ImportLegalEntity() - " + drLegalEntity["Society Name"].ToString() + "Validation Fails");
                            }
                        }
                    }
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                    meoSummary.Text += "\n" + "------------------------------Legal Entity Record Summary-------------------------";
                    meoSummary.Text += "\n" + "Total Records=" + dsMasters.Tables["Legal Entity"].Rows.Count.ToString();
                    meoSummary.Text += "\n" + "Created Legal Entities=" + RecordCount.ToString();
                    meoSummary.Text += "\n" + "Existing Legal Entities=" + ExistingRecordCount.ToString();
                    meoSummary.Text += "\n" + "Skipped Legal Entities=" + (dsMasters.Tables["Legal Entity"].Rows.Count - (RecordCount + ExistingRecordCount)).ToString();
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                }
                else
                {
                    new ErrorLog().WriteError("Import Masters ImportLegalEntity() - " + "Legal Entity Table has No Records");
                    using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                    {
                        if (legalEntitySystem.GetCount() < 0)
                            Success = false;
                        else

                            Success = true;
                    }
                }

            }
            catch (Exception ex)
            {
                meoSummary.Text += "\n" + "Some exception has occured,Importing Legal Entities is stopped";
                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                new ErrorLog().WriteError("Import Masters ImportLegalEntity() Exception - " + ex.Message);
                Success = false;

            }

            return Success;
        }

        private bool ValidateLegalEntityFields(DataRow LegalEntity)
        {
            bool Success = true;

            try
            {
                if (!string.IsNullOrEmpty(LegalEntity["Society Name"].ToString().Trim()))
                {
                    if (string.IsNullOrEmpty(LegalEntity["Society Name"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + "- Society Name is empty, this is skipped";
                    }
                    else if (string.IsNullOrEmpty(LegalEntity["Country"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has no Country, this is skipped";
                    }
                    else if (string.IsNullOrEmpty(LegalEntity["State/Province"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has no State/Province, this is skipped";
                    }
                    else if (string.IsNullOrEmpty(LegalEntity["Society/Reg No"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has no Society Reg No, this is skipped";
                    }
                    else if (string.IsNullOrEmpty(LegalEntity["Association Nature"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has no Association Nature, this is skipped";
                    }
                    else if (string.IsNullOrEmpty(LegalEntity["Denomination"].ToString().Trim()))
                    {
                        if (LegalEntity["Association Nature"].ToString().Trim().Equals(Association.Religious.ToString()))
                        {
                            Success = false;
                            meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has no Denomination, this is skipped";
                        }
                    }
                    if (string.IsNullOrEmpty(GetSelectedNatureofAssociation(LegalEntity["Association Nature"].ToString().Trim())))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has mismatching Association Nature, this is skipped";
                    }
                    if (!string.IsNullOrEmpty(LegalEntity["Denomination"].ToString()))
                    {
                        int Did = 0;
                        Did = GetDenomination(LegalEntity["Denomination"].ToString());
                        if (Did == 0)
                        {
                            Success = false;
                            meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has mismatching Denomination, this is skipped";
                        }
                    }
                    if (string.IsNullOrEmpty(LegalEntity["Association Nature Others"].ToString().Trim()))
                    {
                        if (LegalEntity["Association Nature"].ToString().Trim().Equals(Association.Others.ToString()))
                        {
                            Success = false;
                            meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has no Association Nature Others, this is skipped";
                        }
                    }
                    if (string.IsNullOrEmpty(LegalEntity["Denomination Others"].ToString().Trim()))
                    {
                        if (LegalEntity["Denomination"].ToString().Trim().Equals(Denomination.Others.ToString()))
                        {
                            Success = false;
                            meoSummary.Text += "\n" + LegalEntity["Society Name"].ToString() + "- has no Denomination Others, this is skipped";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters ValidateLegalEntityFields() - " + ex.Message);
            }

            return Success;
        }

        private string GetSelectedNatureofAssociation(string AssociationNature)
        {
            string[] Values = AssociationNature.Split(',');
            string Selected = string.Empty;
            foreach (string Val in Values)
            {
                if (AssociationNature.Equals(Association.Cultural.ToString()))
                {
                    Selected += (int)Association.Cultural + ",";
                }
                else if (AssociationNature.Equals(Association.Economic.ToString()))
                {
                    Selected += (int)Association.Economic + ",";
                }
                else if (AssociationNature.Equals(Association.Educational.ToString()))
                {
                    Selected += (int)Association.Educational + ",";
                }
                else if (AssociationNature.Equals(Association.Religious.ToString()))
                {
                    Selected += (int)Association.Religious + ",";
                }
                if (AssociationNature.Equals(Association.Social.ToString()))
                {
                    Selected += (int)Association.Social + ",";
                }
                if (AssociationNature.Equals(Association.Others.ToString()))
                {
                    Selected += (int)Association.Others + ",";
                }
            }
            Selected = Selected.TrimEnd(',');
            return Selected;
        }

        private int GetDenomination(string denomination)
        {
            int DenominationValue = 0;
            if (denomination.Equals(Denomination.Hindu.ToString()))
            {
                DenominationValue = (int)Denomination.Hindu;
            }
            else if (denomination.Equals(Denomination.Buddhist.ToString()))
            {
                DenominationValue = (int)Denomination.Buddhist;
            }
            else if (denomination.Equals(Denomination.Christian.ToString()))
            {
                DenominationValue = (int)Denomination.Christian;
            }
            else if (denomination.Equals(Denomination.Muslim.ToString()))
            {
                DenominationValue = (int)Denomination.Muslim;
            }
            else if (denomination.Equals(Denomination.Sikh.ToString()))
            {
                DenominationValue = (int)Denomination.Sikh;
            }
            else if (denomination.Equals(Denomination.Others.ToString()))
            {
                DenominationValue = (int)Denomination.Others;
            }
            return DenominationValue;
        }

        #endregion

        #region Project Category

        private bool ImportProjectCategroy()
        {
            bool Success = true;
            int RecordCount = 0;
            int ExistingRecordCount = 0;
            try
            {
                if (dsMasters.Tables["Project Category"] != null && dsMasters.Tables["Project Category"].Rows.Count > 0)
                {
                    foreach (DataRow drProjectCategory in dsMasters.Tables["Project Category"].Rows)
                    {
                        if (ValidateProjectCategory(drProjectCategory))
                        {
                            using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                            {
                                projectCategorySystem.ProjectCatogoryId = 0;
                                projectCategorySystem.ProjectCatogoryName = drProjectCategory["Project Category"].ToString().Trim();
                                resultArgs = projectCategorySystem.SaveProjectCatogoryDetails(DataBaseType.HeadOffice);
                                if (!resultArgs.Success)
                                {
                                    if (resultArgs.Message == "The Record is Available")
                                        ExistingRecordCount++;
                                    new ErrorLog().WriteError(drProjectCategory["Project Category"].ToString() + " - " + resultArgs.Message);
                                }
                                else
                                {
                                    RecordCount++;
                                }
                                new ErrorLog().WriteError("Import Masters ImportProjectCategroy() - " + drProjectCategory["Project Category"].ToString() + resultArgs.Exception.ToString());
                            }
                        }
                        else
                        {
                            new ErrorLog().WriteError("Import Masters ImportProjectCategroy() - " + drProjectCategory["Project Category"].ToString() + "Validation Fails");
                        }

                    }
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                    meoSummary.Text += "\n" + "------------------------------Project Category Record Summary-------------------";
                    meoSummary.Text += "\n" + "Total Records=" + dsMasters.Tables["Project Category"].Rows.Count.ToString();
                    meoSummary.Text += "\n" + "Created Project Categories=" + RecordCount.ToString();
                    meoSummary.Text += "\n" + "Existing Project Categories=" + ExistingRecordCount.ToString();
                    meoSummary.Text += "\n" + "Skipped Project Categories=" + (dsMasters.Tables["Project Category"].Rows.Count - (RecordCount + ExistingRecordCount)).ToString();
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                }
                else
                {
                    new ErrorLog().WriteError("Import Masters ImportProjectCategroy() - " + "Project Category has no valid data");
                    using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                    {
                        if (projectCategorySystem.GetCount() < 0)
                            Success = false;
                        else
                            Success = true;
                    }
                }

            }
            catch (Exception ex)
            {
                meoSummary.Text += "\n" + "Some exception has occured,Importing Project Categories is stopped";
                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                new ErrorLog().WriteError("Import Masters ImportProjectCategroy() - " + ex.Message);
                Success = false;
            }
            return Success;
        }

        private bool ValidateProjectCategory(DataRow drProjectCategory)
        {
            bool Success = true;

            try
            {
                if (string.IsNullOrEmpty(drProjectCategory["Project Category"].ToString().Trim()))
                {
                    Success = false;
                    meoSummary.Text += "\n" + "Project Category has no valid data";
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("ValidateProjectCategory:" + ex.Message);
            }
            return Success;
        }
        #endregion

        #region Project

        private bool ImportProject()
        {
            bool Success = true;
            int RecordCount = 0;
            int ExistingRecordCount = 0;
            try
            {
                if (dsMasters.Tables["Project"] != null && dsMasters.Tables["Project"].Rows.Count > 0)
                {
                    foreach (DataRow drProject in dsMasters.Tables["Project"].Rows)
                    {
                        if (ValidateProjectFields(drProject))
                        {
                            using (ProjectSystem projectSystem = new ProjectSystem())
                            {
                                projectSystem.ProjectId = 0;
                                projectSystem.ProjectCode = drProject["Code"].ToString().Trim();
                                projectSystem.SocietyId = GetSocietyId(drProject["Society Name"].ToString().Trim());
                                projectSystem.ProjectName = drProject["Project"].ToString().Trim();
                                projectSystem.ProjectCategroyId = GetProjectCategoryId(drProject["Project Category"].ToString().Trim());
                                projectSystem.Description = drProject["Description"].ToString().Trim();
                                projectSystem.StartedOn = this.Member.DateSet.ToDate(drProject["Started On"].ToString().Trim(), false);
                                if (!string.IsNullOrEmpty(drProject["Closed On"].ToString().Trim()))
                                {
                                    projectSystem.Closed_On = this.Member.DateSet.ToDate(drProject["Closed On"].ToString().Trim(), false);
                                }
                                projectSystem.DivisionId = GetDivisionId(drProject["Division"].ToString().Trim());
                                projectSystem.Address = drProject["Address"].ToString().Trim();
                                resultArgs = projectSystem.SaveProject(DataBaseType.HeadOffice);
                                if (!resultArgs.Success)
                                {
                                    new ErrorLog().WriteError(drProject["Project"].ToString() + " - " + resultArgs.Message);
                                    if (resultArgs.Message == "The Record is Available")
                                        ExistingRecordCount++;
                                }
                                else
                                {
                                    RecordCount++;
                                }

                                new ErrorLog().WriteError("Import Masters ImportProject() - " + drProject["Project"].ToString() + resultArgs.Exception.ToString());
                            }
                        }
                        else
                        {
                            new ErrorLog().WriteError("Import Masters ImportProject() - " + drProject["Project"].ToString() + "Validation Fails");
                            // meoSummary.Text += "\n" + drProject["Project"].ToString() + "- Validation Fails";
                        }
                    }
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                    meoSummary.Text += "\n" + "------------------------------Project Record Summary----------------------------";
                    meoSummary.Text += "\n" + "Total Records=" + dsMasters.Tables["Project"].Rows.Count.ToString();
                    meoSummary.Text += "\n" + "Created Projects=" + RecordCount.ToString();
                    meoSummary.Text += "\n" + "Existing Projects=" + ExistingRecordCount.ToString();
                    meoSummary.Text += "\n" + "Skipped Projects=" + (dsMasters.Tables["Project"].Rows.Count - (RecordCount + ExistingRecordCount)).ToString();
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";

                }
                else
                {
                    new ErrorLog().WriteError("Import Masters ImportProject() - " + "Project has no records");
                }
            }
            catch (Exception ex)
            {
                meoSummary.Text += "\n" + "Some exception has occured,Importing projects is stopped";
                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                new ErrorLog().WriteError("Import Masters ImportProject() - " + ex.Message);
                Success = false;
            }
            return Success;
        }

        private bool ValidateProjectFields(DataRow drProject)
        {
            bool Success = true;
            if (!string.IsNullOrEmpty(drProject["Project"].ToString().Trim()))
            {
                if (string.IsNullOrEmpty(drProject["Project"].ToString().Trim()))
                {
                    Success = false;
                    meoSummary.Text += "\n" + drProject["Project"].ToString().Trim() + "-" + "has no Project, this is skipped";
                }
                else if (string.IsNullOrEmpty(drProject["Society Name"].ToString().Trim()))
                {
                    Success = false;
                    meoSummary.Text += "\n" + drProject["Project"].ToString().Trim() + "-" + "has no Society Name, this is skipped";
                }
                else if (string.IsNullOrEmpty(drProject["Project Category"].ToString().Trim()))
                {
                    Success = false;
                    meoSummary.Text += "\n" + drProject["Project"].ToString().Trim() + "-" + "has no Project Category, this is skipped";
                }

                else if (string.IsNullOrEmpty(drProject["Started On"].ToString().Trim()))
                {
                    Success = false;
                    meoSummary.Text += "\n" + drProject["Project"].ToString().Trim() + "-" + "has no Started On, this is skipped";
                }
                else if (string.IsNullOrEmpty(drProject["Division"].ToString().Trim()))
                {
                    Success = false;
                    meoSummary.Text += "\n" + drProject["Project"].ToString().Trim() + "-" + "has no Division,this is skipped";
                }

                if (!string.IsNullOrEmpty(drProject["Project Category"].ToString().Trim()))
                {
                    int PCId = GetProjectCategoryId(drProject["Project Category"].ToString().Trim());
                    if (PCId == 0)
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drProject["Project"].ToString().Trim() + "-" + " has mismatching Project Category, this is skipped";
                    }
                }
                if (!string.IsNullOrEmpty(drProject["Society Name"].ToString().Trim()))
                {
                    int SId = GetSocietyId(drProject["Society Name"].ToString().Trim());
                    if (SId == 0)
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drProject["Project"].ToString() + "(" + " " + drProject["Society Name"].ToString().Trim() + " " + ")" + "-" + "has mismatching Society Name, this is skipped";
                    }
                }
                if (!string.IsNullOrEmpty(drProject["Division"].ToString().Trim()))
                {
                    int DId = 0;
                    DId = GetDivisionId(drProject["Division"].ToString().Trim());
                    if (DId == 0)
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drProject["Project"].ToString().Trim() + "-" + "has mismatching Division, this is skipped";
                    }
                }

            }
            return Success;
        }

        private int GetSocietyId(string Society)
        {
            int SocietyId = 0;
            using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
            {
                legalEntitySystem.SocietyName = Society;
                resultArgs = legalEntitySystem.FetchCustomerIdBySociety();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    SocietyId = this.Member.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.LegalEntity.CUSTOMERIDColumn.ColumnName].ToString());
                else
                    SocietyId = 0;
            }
            return SocietyId;
        }

        private int GetProjectCategoryId(string ProjectCategory)
        {
            int ProjectCategoryId = 0;
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchProjectCategroy(DataBaseType.HeadOffice);
                    DataView dvProjectCategory = resultArgs.DataSource.Table.DefaultView;
                    dvProjectCategory.RowFilter = "PROJECT_CATOGORY_NAME='" + ProjectCategory + "'";
                    if (dvProjectCategory.ToTable().Rows.Count > 0)
                    {
                        ProjectCategoryId = this.Member.NumberSet.ToInteger(dvProjectCategory.ToTable().Rows[0]["PROJECT_CATOGORY_ID"].ToString());
                    }
                    dvProjectCategory.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters GetProjectCategoryId() - " + ex.Message);
            }
            return ProjectCategoryId;
        }

        private int GetDivisionId(string ProjectDivision)
        {
            int DivisionId = 0;
            if (Division.Local.ToString().Equals(ProjectDivision))
            {
                DivisionId = (int)Division.Local;
            }
            else if (Division.Foreign.ToString().Equals(ProjectDivision))
            {
                DivisionId = (int)Division.Foreign;
            }
            return DivisionId;
        }


        #endregion Project

        #region LedgerGroup

        private bool ImportLedgerGroup()
        {
            bool Success = true;
            int RecordCount = 0;
            int ExistingRecordCount = 0;
            DataTable dtSkippedLedgerGroups;
            try
            {
                if (dsMasters.Tables["Ledger Group"] != null && dsMasters.Tables["Ledger Group"].Rows.Count > 0)
                {
                    for (int i = 0; i < dsMasters.Tables["Ledger Group"].Rows.Count; i++)
                    {
                        if (ValidateLedgerGroupFields(dsMasters.Tables["Ledger Group"].Rows[i]))
                        {
                            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                            {
                                ledgerSystem.Abbrevation = dsMasters.Tables["Ledger Group"].Rows[i]["Code"].ToString().Trim().ToUpper();
                                ledgerSystem.Group = dsMasters.Tables["Ledger Group"].Rows[i]["Name"].ToString().Trim();
                                ledgerSystem.ParentGroupId = GetLedgerGroupId(dsMasters.Tables["Ledger Group"].Rows[i]["Parent Group Name"].ToString().Trim());
                                ledgerSystem.NatureId = ledgerSystem.GetNatureId(ledgerSystem.ParentGroupId);
                                ledgerSystem.MainGroupId = ledgerSystem.ParentGroupId;
                                ledgerSystem.GroupId = 0;
                                ledgerSystem.ImageId = 1;
                                ledgerSystem.SortOrder = SortOrder = ledgerSystem.FetchSortOrder();
                                if (SortOrder != 0 && ledgerSystem.ParentGroupId.Equals(ledgerSystem.NatureId))
                                {
                                    ledgerSystem.SortOrder = GenerateSortOrder(SortOrder);
                                }
                                else if (!ledgerSystem.ParentGroupId.Equals(ledgerSystem.NatureId))
                                {
                                    if (SortOrder == 0)
                                    {
                                        SortOrder = ledgerSystem.FetchMainGroupSortOrder();
                                    }
                                    ledgerSystem.SortOrder = GenerateSortOrderSquence(SortOrder);
                                }
                                resultArgs = ledgerSystem.SaveLedgerGroupDetails(DataBaseType.HeadOffice);

                                if (!resultArgs.Success)
                                {
                                    new ErrorLog().WriteError(dsMasters.Tables["Ledger Group"].Rows[i]["Name"].ToString() + " - " + resultArgs.Message);
                                    if (resultArgs.Message == "The Record is Available")
                                        ExistingRecordCount++;
                                }
                                else
                                {
                                    RecordCount++;
                                }
                                new ErrorLog().WriteError("Import Masters ImportLedgerGroup()  - " + dsMasters.Tables["Ledger Group"].Rows[i]["Name"].ToString() + resultArgs.Exception.ToString());
                            }
                        }
                        else
                        {
                            new ErrorLog().WriteError("Import Masters ImportLedgerGroup()  - " + dsMasters.Tables["Ledger Group"].Rows[i]["Name"].ToString() + "Validation Fails");

                            dtSkippedLedgerGroups = new DataTable();

                            dtSkippedLedgerGroups.Columns.Add("Name", typeof(string));
                            dtSkippedLedgerGroups.Columns.Add("Code", typeof(string));
                            dtSkippedLedgerGroups.Columns.Add("Parent Group Name", typeof(string));
                            dtSkippedLedgerGroups.Columns.Add("Nature", typeof(string));
                            DataRow dr = dsMasters.Tables["Ledger Group"].Rows[i];
                            dtSkippedLedgerGroups.ImportRow(dr);
                        }
                    }
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                    meoSummary.Text += "\n" + "------------------------------Ledger Group Record Summary-----------------------";
                    meoSummary.Text += "\n" + "Total Records=" + (dsMasters.Tables["Ledger Group"].Rows.Count) as String;
                    meoSummary.Text += "\n" + "Created Ledger Groups=" + RecordCount.ToString();
                    meoSummary.Text += "\n" + "Existing Ledger Groups=" + ExistingRecordCount.ToString();
                    meoSummary.Text += "\n" + "Skipped Ledger Groups=" + ((dsMasters.Tables["Ledger Group"].Rows.Count) - (RecordCount + ExistingRecordCount)).ToString();
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";

                }
                else
                {
                    new ErrorLog().WriteError("Import Masters ImportLedgerGroup() - " + "Ledger Group Table had No Records");
                }

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters ImportLedgerGroup() - " + ex.Message);
                meoSummary.Text += "\n" + "Some excepton has occured,importing ledger groups is stopped";
                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
            }
            return Success;
        }

        private bool ValidateLedgerGroupFields(DataRow drLedgerGroup)
        {
            bool Success = true;
            try
            {
                if (!string.IsNullOrEmpty(drLedgerGroup["Name"].ToString().Trim()))
                {
                    if (string.IsNullOrEmpty(drLedgerGroup["Name"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drLedgerGroup["Name"].ToString().Trim() + "-" + "has no Ledger Name, this is skipped";
                    }
                    if (string.IsNullOrEmpty(drLedgerGroup["Parent Group Name"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drLedgerGroup["Name"].ToString().Trim() + "-" + "has no Parent Group Name, this is skipped";
                    }

                    else if (string.IsNullOrEmpty(drLedgerGroup["Nature"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drLedgerGroup["Name"].ToString().Trim() + "-" + "has no Nature, this is skipped";
                    }

                    if (!string.IsNullOrEmpty(drLedgerGroup["Parent Group Name"].ToString().Trim()))
                    {
                        int GId = GetLedgerGroupId(drLedgerGroup["Parent Group Name"].ToString().Trim());
                        if (GId == 0)
                        {
                            Success = false;
                            meoSummary.Text += "\n" + drLedgerGroup["Name"].ToString().Trim() + "-" + "given Parent Group Name is not available,this is skipped";
                        }
                        else
                        {
                            if (!ValidateGroupLevel(GId))
                            {
                                Success = false;
                                meoSummary.Text += "\n" + drLedgerGroup["Name"].ToString() + "-" + "not able to create third level of Group, this is skipped";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters ValidateLedgerGroupFields() -" + ex.Message);
            }

            return Success;
        }

        private int GenerateSortOrder(int GetNatureSortOrder)
        {
            SortOrder = GetNatureSortOrder + 100;
            return SortOrder;
        }

        private int GenerateSortOrderSquence(int GetSortOrder)
        {
            SortOrder = GetSortOrder + 1;
            return SortOrder;
        }

        private bool ValidateGroupLevel(int GroupId)
        {
            bool IsGroupLevel = true;
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                resultArgs = ledgerSystem.ValidateGroupId(GroupId, DataBaseType.HeadOffice);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    if (resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ColumnName].ToString() != resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString())
                    {
                        IsGroupLevel = false;
                    }
                }
            }
            return IsGroupLevel;
        }

        private int GetLedgerGroupId(string GroupName)
        {

            int GroupId = 0;
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.LoadLedgerGroupSource(DataBaseType.HeadOffice);

                    DataView dvLedgerGroup = resultArgs.DataSource.Table.DefaultView;
                    dvLedgerGroup.RowFilter = "LEDGER_GROUP='" + GroupName + "'";

                    if (dvLedgerGroup.ToTable().Rows.Count > 0)
                    {
                        GroupId = this.Member.NumberSet.ToInteger(dvLedgerGroup.ToTable().Rows[0]["GROUP_ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters GetLedgerGroupId()-" + ex.Message);
            }
            return GroupId;
        }


        #endregion LedgerGroup

        #region Ledger

        private bool ImportLedger()
        {
            bool Success = true;
            int RecordCount = 0;
            int ExistingRecordCount = 0;
            try
            {
                if (dsMasters.Tables["Ledger"] != null && dsMasters.Tables["Ledger"].Rows.Count > 0)
                {
                    foreach (DataRow drLedger in dsMasters.Tables["Ledger"].Rows)
                    {
                        if (ValidateLedgerFields(drLedger))
                        {
                            using (LedgerSystem ledgerSystem = new LedgerSystem())
                            {
                                ledgerSystem.LedgerCode = drLedger["Code"].ToString().Trim();
                                ledgerSystem.LedgerName = drLedger["Name"].ToString().Trim();
                                ledgerSystem.GroupId = GetGroupId(drLedger["Ledger Group"].ToString().Trim());
                                //ledgerSystem.IsCostCentre = (chkIncludeCostCenter.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                                //ledgerSystem.IsBankInterestLedger = (chkBankInterestLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                                //ledgerSystem.IsTDSApplicable = (chkIsTDSApplicable.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                                ledgerSystem.IsCostCentre = 0;
                                ledgerSystem.IsFDInterestLedger = 0;
                                ledgerSystem.IsTDSApplicable = 0;
                                ledgerSystem.IsInKindLedger = 0;
                                ledgerSystem.IsDepriciationLedger = 0;
                                ledgerSystem.IsAssetGainLedger = 0;
                                ledgerSystem.IsAssetLossLedger = 0;
                                ledgerSystem.LedgerType = GetLedgerType(drLedger["Ledger Type"].ToString().Trim());
                                ledgerSystem.LedgerSubType = ledgerSubType.GN.ToString().Trim();
                                ledgerSystem.LedgerId = 0;
                                ledgerSystem.LedgerNotes = drLedger["Notes"].ToString().Trim();
                                ledgerSystem.SortId = this.Member.NumberSet.ToInteger(ledgerSubType.GN.ToString().Trim());// (int)LedgerSortOrder.Bank;
                                ledgerSystem.FDType = ledgerSubType.GN.ToString().Trim();

                                ledgerSystem.LedgerProfileName = drLedger["Ledger Profile Name"].ToString().Trim();
                                ledgerSystem.LedgerProfileAddress = drLedger["Ledger Profile Address"].ToString().Trim();
                                ledgerSystem.LedgerProfileEmail = drLedger["Ledger Profile Email"].ToString().Trim();
                                ledgerSystem.LedgerProfileContactNo = drLedger["Ledger Profile Contact No"].ToString().Trim();
                                ledgerSystem.LedgerProfilePanNo = drLedger["Ledger Profile PAN No"].ToString().Trim();
                                ledgerSystem.LedgerProfilePincode = drLedger["Ledger Profile PIN Code"].ToString().Trim();
                                if (ledgerSystem.IsTDSApplicable != 0)
                                {
                                    //ledgerSystem.NatureOfPaymentId = this.Member.NumberSet.ToInteger(ddlNatureDeductee.SelectedValue.ToString());
                                    ledgerSystem.NatureOfPaymentId = 0;
                                }
                                ledgerSystem.LedgerProfileCountryId = GetCounrtyId(drLedger["Ledger Profile Country"].ToString().Trim());
                                ledgerSystem.LedgerProfileStateId = GetStateId(ledgerSystem.LedgerProfileCountryId, drLedger["Ledger Profile State"].ToString().Trim());
                                resultArgs = ledgerSystem.SaveLedger(DataBaseType.HeadOffice);
                                if (!resultArgs.Success)
                                {
                                    new ErrorLog().WriteError(drLedger["Name"].ToString() + " - " + resultArgs.Message);
                                    if (resultArgs.Message == "The Record is Available")
                                        ExistingRecordCount++;
                                }
                                else
                                {
                                    RecordCount++;
                                }
                                new ErrorLog().WriteError("Import Masters ImportLedgers() - " + drLedger["Name"].ToString() + resultArgs.Exception.ToString());
                            }
                        }
                        else
                        {
                            new ErrorLog().WriteError("Import Masters ImportLedgers() - " + drLedger["Name"].ToString() + "Validation Fails");
                        }
                    }
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                    meoSummary.Text += "\n" + "------------------------------Ledger Record Summary------------------------------";
                    meoSummary.Text += "\n" + "Total Records=" + dsMasters.Tables["Ledger"].Rows.Count.ToString();
                    meoSummary.Text += "\n" + "Created Ledgers=" + RecordCount.ToString();
                    meoSummary.Text += "\n" + "Existing Ledgers=" + ExistingRecordCount.ToString();
                    meoSummary.Text += "\n" + "Skipped Ledgers=" + (dsMasters.Tables["Ledger"].Rows.Count - (RecordCount + ExistingRecordCount)).ToString();
                    meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                }
                else
                {
                    new ErrorLog().WriteError("Import Masters ImportLedgers() - " + "No Records Available");
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters ImportLedger() - " + ex.Message);
                Success = false;
                meoSummary.Text += "\n" + "Some exception has occured,importing ledgers is stopped";
                meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
            }
            return Success;
        }

        private bool ValidateLedgerFields(DataRow drLedger)
        {
            bool Success = true;
            try
            {
                if (!string.IsNullOrEmpty(drLedger["Name"].ToString().Trim()))
                {
                    if (string.IsNullOrEmpty(drLedger["Name"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + "Ledger Name is empty, this is skipped";
                    }
                    else if (string.IsNullOrEmpty(drLedger["Ledger Group"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drLedger["Name"].ToString() + "- has no Ledger Group, this is skipped";
                    }
                    else if (string.IsNullOrEmpty(drLedger["Ledger Type"].ToString().Trim()))
                    {
                        Success = false;
                        meoSummary.Text += "\n" + drLedger["Name"].ToString() + "- has no Ledger Type, this is skipped";
                    }
                    if (!string.IsNullOrEmpty(drLedger["Ledger Group"].ToString().Trim()))
                    {
                        int GId = 0;
                        GId = GetGroupId(drLedger["Ledger Group"].ToString().Trim());
                        if (GId == 0)
                        {
                            Success = false;
                            meoSummary.Text += "\n" + drLedger["Name"].ToString() + "- given Ledger Group is not available, this is skipped";
                        }

                    }
                    if (!string.IsNullOrEmpty(drLedger["Ledger Type"].ToString().Trim()))
                    {
                        string ledgerType = string.Empty;
                        ledgerType = drLedger["Ledger Type"].ToString().Trim();
                        if (ledgerType != LedgerType.General.ToString() && ledgerType != LedgerType.InKind.ToString())
                        {
                            Success = false;
                            meoSummary.Text += "\n" + drLedger["Name"].ToString() + "- given Ledger Type is not available, this is skipped";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("ValidateLedgerFields:" + ex.Message);
            }
            return Success;

        }

        private string GetLedgerType(string ledgerType)
        {
            if (ledgerType == LedgerType.General.ToString())
                ledgerType = ledgerSubType.GN.ToString();
            else
                ledgerType = ledgerSubType.IK.ToString();
            return ledgerType;
        }

        private int GetGroupId(string Group)
        {
            int GroupId = 0;
            try
            {
                using (LedgerGroupSystem LedgerGroupSystem = new LedgerGroupSystem())
                {
                    // resultArgs = LedgerGroupSystem.LoadLedgerGroupforLedgerLoodkup(ledgerSubType.GN, DataBaseType.HeadOffice);
                    resultArgs = LedgerGroupSystem.GetLedgerGroupId(Group, DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        GroupId = this.Member.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["GROUP_ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Ledger GetGroupId() - " + ex.Message);
            }
            return GroupId;
        }

        private int GetCounrtyId(string Country)
        {
            int CountryId = 0;
            try
            {
                using (HeadOfficeSystem HeadSystem = new HeadOfficeSystem())
                {
                    resultArgs = HeadSystem.FetchCountry();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvCountry = resultArgs.DataSource.Table.DefaultView;
                        dvCountry.RowFilter = "COUNTRY='" + Country + "'";
                        CountryId = this.Member.NumberSet.ToInteger(dvCountry.ToTable().Rows[0]["COUNTRY_ID"].ToString());

                        dvCountry.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters GetCountryId() - " + ex.Message);
            }
            return CountryId;
        }

        private int GetStateId(int CountryId, string State)
        {
            int StateId = 0;
            try
            {
                using (HeadOfficeSystem headSystem = new HeadOfficeSystem())
                {
                    headSystem.Country_Id = CountryId;
                    resultArgs = headSystem.FetchStateByCountry();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvState = resultArgs.DataSource.Table.DefaultView;
                        dvState.RowFilter = "STATE='" + State + "'";
                        StateId = this.Member.NumberSet.ToInteger(dvState.ToTable().Rows[0]["STATE_ID"].ToString());
                        dvState.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Masters GetStateId() - " + ex.Message);
            }
            return StateId;
        }

        #endregion

        #endregion
    }
}