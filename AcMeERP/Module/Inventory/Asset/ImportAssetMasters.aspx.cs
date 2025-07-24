using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Bosco.Model;

namespace AcMeERP.Module.Inventory.Asset
{
    public partial class ImportAssetMasters : Base.UIBase
    {
        #region Declaration

        DataSet dsAssetMasters = null;
        string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0";
        OleDbConnection connectionExcel;
        OleDbCommand cmdExcel;
        OleDbDataAdapter adpExcel;
        DataTable dtSheet;
        ResultArgs resultArgs = null;
        private static object objLock = new object();

        const string ASSET_TABLE_NAME = "Asset Item";
        string ASSET_PARENT_CLASS = "PARENT CLASS";
        string ASSET_SUB_CLASS = "SUB CLASS";
        string RETENTION_YRS = "RETENTION YRS";
        string DEPRECIATION_YRS = "DEPRECIATION YRS";
        string IS_INSURED = "IS INSURED";
        string IS_AMC = "IS AMC";
        string ASSET_ITEM = "ASSET ITEM";
        string ACCOUNT_LEDGER = "ACCOUNT LEDGER";
        string DEPRECIATION_LEDGER = "DEPRECIATION LEDGER";
        string DISPOSAL_LEDGER = "DISPOSAL LEDGER";
        string METHOD = "METHOD";
        string PREFIX = "PREFIX";
        string SUFFIX = "SUFFIX";
        string ASSETID = "ASSET ID";
        string ITEMID = "ITEM_ID";

        string INSURANCEAPPLICABLE = "Insurance Applicable?";
        string AMCAPPLICABLE = "AMC Applicable?";
        string RETENSIONYRS = "Retention Yrs";
        string DEPRECIATIONYRS = "Depreciation Yrs";

        private int UnitID
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["UnitID"].ToString());
            }
            set
            {
                ViewState["UnitID"] = value;
            }
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Asset.ImportAssetMastersPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.ImportAssetMasters, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.ShowLoadWaitPopUp(btnUpload);
                imgDownloadMastersTemplate.ToolTip = "Click to Download Asset Masters Template";
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
                                meoSummary.Text += "\n----------------------------------------------------";
                                meoSummary.Text += "\n------------Import Asset Masters Started------------";
                            if (ImportAssetMastersToDatabase())
                            {
                                meoSummary.Text += "\n------------Import Asset Masters Success------------";
                                meoSummary.Text += "\n------------Import Asset Masters Ended------------";
                                this.Message = "Asset Masters imported successfully";
                            }

                        }
                    }
                    else
                    {
                        new ErrorLog().WriteError("Import Asset Masters - " + "File name is empty");
                    }
                }
                else
                {
                    new ErrorLog().WriteError("Import Asset Masters - " + "File InValid");
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Asset Masters Exception - " + ex.Message);
            }
            finally
            {
                if (!string.IsNullOrEmpty(UploadFileDirectory))
                    File.Delete(UploadFileDirectory);
            }
        }

        protected void imgDownloadMastersTemplate_Click(object sender, EventArgs ed)
        {
            try
            {
                DownLoadFile();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Asset Masters Download Click -" + ex.Message);
            }

        }

        #endregion

        #region Methods

        private void RemoveEmptyRows(string tableName)
        {
            try
            {

                for (int i = 0; i <= dsAssetMasters.Tables[tableName].Rows.Count - 1; i++)
                {
                    string valuesarr = string.Empty;
                    List<object> lst = new List<object>(dsAssetMasters.Tables[tableName].Rows[i].ItemArray);
                    foreach (object s in lst)
                    {
                        valuesarr += s.ToString();
                    }
                    if (string.IsNullOrEmpty(valuesarr))
                    {
                        //Remove row here, this row do not have any value 
                        dsAssetMasters.Tables[tableName].Rows[i].Delete();
                        valuesarr = string.Empty;
                    }
                }
                dsAssetMasters.Tables[tableName].AcceptChanges();
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
                string FileName = "AcMEERP_Asset_Masters_Template" + DateTime.Now.ToString(DateFormatInfo.MySQLFormat.DateTime).ToString() + ".xlsx";
                byte[] bytes;
                bytes = File.ReadAllBytes(PagePath.AcMEERPAssetMastersFilePath);
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
            dsAssetMasters = new DataSet();
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
                string[] TableNames = { "Asset Item$" };
                foreach (DataRow dr in dtExcelSchema.Rows)
                {
                    string SheetName = dr["TABLE_NAME"].ToString();
                    SheetName = SheetName.Trim(new char[] { '\'' });

                    if (!SheetName.Equals(TableNames[Count]))
                    {
                        new ErrorLog().WriteError("Import Asset Masters ValidateFile() - " + SheetName.TrimEnd('$') + "Sheeting is Missing");
                        meoSummary.Text += "\n" + "File is invalid, please upload the correct file";
                        meoSummary.Text += "\n" + "-----------------------------------------------------------------------------------------";
                        Success = false;
                    }
                    else
                    {
                        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                        adpExcel.SelectCommand = cmdExcel;
                        adpExcel.Fill(dsAssetMasters.Tables.Add(SheetName.TrimEnd('$')));
                        RemoveEmptyRows(SheetName.TrimEnd('$'));
                    }
                    Count++;
                }
            }

            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Asset Masters ValidateFile() Exception - " + ex.Message);
            }
            finally { connectionExcel.Close(); }

            return Success;
        }

        private bool ImportAssetMastersToDatabase()
        {
            int parentAssetClassId = 0;
            int AssetClassId = 0;
            int AssetItemId = 0;
            bool _IsSuccess = true;
            if (dsAssetMasters != null && dsAssetMasters.Tables.Count > 0)
            {
                UnitID = GetUnitofMeasureId();
                if (dsAssetMasters.Tables[0] != null && dsAssetMasters.Tables[0].Rows.Count > 0)
                {
                    meoSummary.Text += "\n------------Import Asset Class and Asset Item Started------------";
                    foreach (DataRow drAssetItem in dsAssetMasters.Tables[ASSET_TABLE_NAME].Rows)
                    {
                        //Insert Asset Class
                        using (AssetClassSystem classSystem = new AssetClassSystem())
                        {
                            //Check Exists of Asset sub class
                            classSystem.AssetClass = drAssetItem[ASSET_PARENT_CLASS].ToString().Trim();
                            parentAssetClassId = AssetClassId = classSystem.FetchAssetClassIdByName();
                            if (parentAssetClassId > 0)
                            {
                                meoSummary.Text += "\nAsset Parent Class exists: " + parentAssetClassId + " " + drAssetItem[ASSET_PARENT_CLASS].ToString().Trim() + "";
                            }
                            else
                            { //Insert ParentClass in the asset_class table
                                classSystem.AssetClass = drAssetItem[ASSET_PARENT_CLASS].ToString().Trim();
                                classSystem.Depreciation = 0;
                                classSystem.ParentClassId = 1;//Primary
                                resultArgs = classSystem.SaveClassDetails();
                                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    AssetClassId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    meoSummary.Text += "\nAsset Parent Class Inserted: " + AssetClassId + " " + drAssetItem[ASSET_PARENT_CLASS].ToString().Trim() + "";
                                }
                            }
                            if (!string.IsNullOrEmpty(drAssetItem[ASSET_SUB_CLASS].ToString()))
                            {
                                //Check Exists of Asset sub class
                                classSystem.AssetClass = drAssetItem[ASSET_SUB_CLASS].ToString().Trim();
                                AssetClassId = classSystem.FetchAssetClassIdByName();
                                if (AssetClassId > 0)
                                {
                                    meoSummary.Text += "\nAsset Sub Class exists: " + AssetClassId + " " + drAssetItem[ASSET_SUB_CLASS].ToString().Trim() + "";
                                }
                                else
                                {   //Insert SubClass in the asset_class table
                                    classSystem.AssetClass = drAssetItem[ASSET_SUB_CLASS].ToString().Trim();
                                    classSystem.Depreciation = 0;
                                    classSystem.ParentClassId = parentAssetClassId > 0 ? parentAssetClassId : AssetClassId;//Primary
                                    resultArgs = classSystem.SaveClassDetails();
                                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                    {
                                        AssetClassId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                        meoSummary.Text += "\nAsset Sub Class Inserted: " + AssetClassId + " " + drAssetItem[ASSET_SUB_CLASS].ToString().Trim();
                                    }
                                }
                            }

                        }
                        //Insert Asset Items
                        using (AssetItemSystem itemSystem = new AssetItemSystem())
                        {
                            //Check Exists of Asset Item
                            itemSystem.Name = drAssetItem[ASSET_ITEM].ToString().Trim();
                            AssetItemId = itemSystem.FetchAssetItemIdByName();
                            if (AssetItemId > 0)
                            {
                                meoSummary.Text += "\nAsset Item exists : " + AssetItemId + " " + drAssetItem[ASSET_ITEM].ToString().Trim();
                            }
                            else
                            { //Insert ParentClass in the asset_class table
                                itemSystem.Name = drAssetItem[ASSET_ITEM].ToString().Trim();
                                itemSystem.AssetClassId = AssetClassId;
                                itemSystem.Prefix = drAssetItem[PREFIX].ToString();
                                itemSystem.Suffix = drAssetItem[SUFFIX].ToString();
                                itemSystem.Unit = UnitID;
                                itemSystem.DepreciationYrs = Member.NumberSet.ToInteger(drAssetItem[DEPRECIATION_YRS].ToString());
                                itemSystem.RetentionYrs = Member.NumberSet.ToInteger(drAssetItem[RETENSIONYRS].ToString());
                                itemSystem.AMCApplicable = drAssetItem[AMCAPPLICABLE].ToString() == YesNo.Yes.ToString() ? 1 : 0;
                                itemSystem.InsuranceApplicable = drAssetItem[INSURANCEAPPLICABLE].ToString() == YesNo.Yes.ToString() ? 1 : 0;
                                itemSystem.DepreciatonApplicable = Member.NumberSet.ToInteger(drAssetItem[DEPRECIATION_YRS].ToString()) > 0 ? 1 : 0;
                                resultArgs = itemSystem.SaveItemDetails();
                                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    meoSummary.Text += "\n Asset Item " + drAssetItem["Parent Class"].ToString().Trim() + " Inserted";
                                    AssetItemId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                }
                            }
                        }

                    }
                    meoSummary.Text += "\n------------Import Asset Class and Asset Item Ended------------";
                    _IsSuccess = true;
                }
                else
                {
                    meoSummary.Text += "\n------------Please Provide Valid Asset Class and Asset Items in the importing file------------";
                    _IsSuccess = false;
                }
            }
            return _IsSuccess;
        }

        private int GetUnitofMeasureId()
        {
            int UnitID = 0;
            using (AssetUnitOfMeassureSystem UnitOfMeasure = new AssetUnitOfMeassureSystem())
            {
                UnitOfMeasure.SYMBOL = FixedAssetDefaultUOM.Nos.ToString(); 
                UnitID = UnitOfMeasure.FetchUnitOfMeasureId();
                if (UnitID == 0)
                {
                    UnitOfMeasure.NAME = "Numbers";
                    UnitOfMeasure.SYMBOL = FixedAssetDefaultUOM.Nos.ToString(); 
                    resultArgs = UnitOfMeasure.SaveMeasureDetails();
                    if (resultArgs.Success)
                    {
                        UnitID = Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                    }
                }
            }
            return UnitID;
        }
        #endregion
    }
}