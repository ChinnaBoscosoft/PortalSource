using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Model.UIModel
{
    public class LicenseSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        public const string MatchValue = "MATCH_VALUE";
        public const string MatchLength = "MATCH_LENGTH";
        public const string LikeValue = "LIKE_VALUE";
        public const string RunningNo = "RUNNING_NUMBER";
        public const string RunningNoLength = "RUNNING_NO_LENGTH";
        #endregion

        #region Constructor
        public LicenseSystem()
        {
        }
        #endregion

        #region License Property
        public int BRANCH_ID { get; set; }
        public string LICENSE_KEY_NUMBER { get; set; }
        public int LICENSE_QUANTITY { get; set; }
        public double LICENSE_COST { get; set; }
        public DateTime YEAR_FROM { get; set; }
        public DateTime YEAR_TO { get; set; }
        public string INSTITUTE_NAME { get; set; }
        public string SOCIETY_NAME { get; set; }
        public int IS_MULTILOCATION { get; set; }
        public int ENABLE_PORTAL { get; set; }
        public string MODULE_ITEM { get; set; }
        public string LICENSE_REPORT_ITEM { get; set; }
        public string LOGIN_URL { get; set; }
        public int IS_LICENSE_MODEL { get; set; }
        public string BRANCH_KEY_CODE { get; set; }
        public int ACCESS_MULTI_DB { get; set; }
        public int APPROVE_BUDUGET_PORTAL { get; set; }
        public int APPROVE_BUDUGET_EXCEL { get; set; }
        public int IS_TWO_MONTH_BUDGET { get; set; }
        public int AUTOMATIC_BACKUP_PORTAL { get; set; }
        public string BranchCode { get; set; }
        public int LOCK_MASTER { get; set; }
        public int ALLOW_MULTI_CURRENCY { get; set; }
        public int ATTACH_VOUCHERS_FILES { get; set; }
        public int MAP_LEDGER { get; set; }
        public string Parish_Code { get; set; }
        public string ThirdParty_Code { get; set; }
        public string THIRDPARTY_MODE { get; set; }
        public string THIRDPARTY_URL { get; set; }
        #endregion

        #region Methods
        public string GetNewNumber(NumberFormats numberFormatId, string dateValue, string customerCode, string bocode = "", DataManager dm = null)
        {
            string formatedValue = "";
            string runningNumberLenth = "0";
            dateValue = (dateValue == "") ? this.DateSet.GetDateToday() : dateValue;

            switch (numberFormatId)
            {
                case NumberFormats.BranchKeyUniqueCode:
                    {
                        formatedValue = ReplaceNumFormatValues(LicenseUtility.BranchKeyUniqueCode, dateValue);
                        runningNumberLenth = LicenseUtility.LicenseRunningNoLength.ToString();
                        break;
                    }
                case NumberFormats.LicenseIdentificationNumber:
                    {
                        formatedValue = customerCode;
                        runningNumberLenth = LicenseUtility.LicenseIdentificationRunningNoLength.ToString();
                        break;
                    }
                case NumberFormats.LC_Branch_Enable_Request_IdentificationNumber:
                    {
                        formatedValue = ReplaceNumFormatValues(LicenseUtility.BranchKeyUniqueCode, dateValue); ;
                        runningNumberLenth = LicenseUtility.LicenseRunningNoLength.ToString();
                        break;
                    }
            }
            return GetNumber(numberFormatId, formatedValue, runningNumberLenth, bocode, dm);
        }

        public string ReplaceNumFormatValues(string sFormat, string dateValue)
        {
            sFormat = DateTime.Parse(dateValue).ToString(sFormat.Replace('Y', 'y').Replace('D', 'd').Replace('m', 'M'));
            return sFormat;
        }

        private string GetNumber(NumberFormats NumberFormatId, string FormatedValue, string RunningNoLength, string bocode = "", DataManager dm = null)
        {
            DataView dvNoFormat = new DataView();
            DataManager dataManagerArgs = null;
            string returnValue = "";

            switch (NumberFormatId)
            {
                case NumberFormats.BranchKeyUniqueCode:
                    {
                        dataManagerArgs = new DataManager(SQLCommand.License.NewBranchUniqueCodeFetch, DataBaseType.Portal);
                        break;
                    }
                case NumberFormats.LicenseIdentificationNumber:
                    {
                        dataManagerArgs = new DataManager(SQLCommand.License.NewLicenseIdentificationNumberFetch, DataBaseType.Portal);
                        break;
                    }
                case NumberFormats.LC_Branch_Enable_Request_IdentificationNumber:
                    {
                        dataManagerArgs = new DataManager(SQLCommand.License.NewLCBranchEnableRequestIdentificationNumberFetch, DataBaseType.HeadOffice);
                        dataManagerArgs.Parameters.Add(AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn, bocode);
                        if (dm != null) dataManagerArgs.Database = dm.Database;
                        break;
                    }
            }

            int length = FormatedValue.Length + int.Parse(RunningNoLength);

            dataManagerArgs.Parameters.Add(MatchValue, FormatedValue, DataType.Varchar);
            dataManagerArgs.Parameters.Add(MatchLength, (FormatedValue.Length + 1).ToString(), DataType.Varchar);
            dataManagerArgs.Parameters.Add(RunningNo, RunningNoLength, DataType.Varchar);
            dataManagerArgs.Parameters.Add(LikeValue, FormatedValue + "%", DataType.Varchar);

            resultArgs = dataManagerArgs.FetchData(dataManagerArgs, DataSource.DataView);
            if (!resultArgs.Success | resultArgs.RowsAffected < 1)
                return returnValue;
            dvNoFormat = resultArgs.DataSource.TableView;
            returnValue = dvNoFormat[0][0].ToString();
            return returnValue;
        }

        public string GetRandomLicenseNumber()
        {
            string licenseNumber = "";
            object dataSource = null;
            string currentDate = this.ReplaceNumFormatValues(LicenseUtility.BranchKeyUniqueCode, this.DateSet.GetDateToday());
            Random rand = new Random();
            int number = rand.Next(0, Int32.MaxValue);
            licenseNumber = currentDate + number.ToString();
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.IsLicenseNoExist))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.LICENSE_KEY_NUMBERColumn, licenseNumber);
                resultArgs = dataManagerArgs.FetchData(DataSource.Scalar, ref dataSource);
                if (resultArgs.Success && resultArgs.RowsAffected == 1 && dataSource.ToString() != "0")
                {
                    licenseNumber = GetRandomLicenseNumber();
                }
            }
            return licenseNumber;
        }

        public ResultArgs SaveLicenseDetails()
        {
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.Add))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.LICENSE_IDColumn, true);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.BRANCH_IDColumn, BRANCH_ID);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.BRANCH_KEY_CODEColumn, BRANCH_KEY_CODE);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.LICENSE_KEY_NUMBERColumn, LICENSE_KEY_NUMBER);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.LICENSE_COSTColumn, LICENSE_COST);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.LICENSE_QUANTITYColumn, LICENSE_QUANTITY);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.YEAR_FROMColumn, YEAR_FROM);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.YEAR_TOColumn, YEAR_TO);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.KEY_GENERATED_DATEColumn, DateTime.Now);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.INSTITUTE_NAMEColumn, INSTITUTE_NAME);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.IS_MULTILOCATIONColumn, IS_MULTILOCATION);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.LOGIN_URLColumn, LOGIN_URL);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.ENABLE_PORTALColumn, ENABLE_PORTAL);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.USER_IDColumn, base.LoginUserId);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.MODULE_ITEMColumn, MODULE_ITEM);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.ENABLE_REPORTSColumn, LICENSE_REPORT_ITEM);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.CRISTO_PARISH_CODEColumn, Parish_Code);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.THIRDPARTY_MODEColumn, THIRDPARTY_MODE);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.THIRDPARTY_URLColumn, THIRDPARTY_URL);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.IS_LICENSE_MODELColumn, IS_LICENSE_MODEL);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.ACCESS_MULTI_DBColumn, ACCESS_MULTI_DB);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.APPROVE_BUDGET_BY_PORTALColumn, APPROVE_BUDUGET_PORTAL);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.APPROVE_BUDGET_BY_EXCELColumn, APPROVE_BUDUGET_EXCEL);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.IS_TWO_MONTH_BUDGETColumn, IS_TWO_MONTH_BUDGET);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.AUTOMATIC_BACKUP_PORTALColumn, AUTOMATIC_BACKUP_PORTAL);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.LOCK_MASTERColumn, LOCK_MASTER);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.MAP_LEDGERColumn, MAP_LEDGER);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.ALLOW_MULTI_CURRENCYColumn, ALLOW_MULTI_CURRENCY);
                dataManagerArgs.Parameters.Add(this.AppSchema.Branch_License.ATTACH_VOUCHERS_FILESColumn, ATTACH_VOUCHERS_FILES);
                resultArgs = dataManagerArgs.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs GenerateLicenseKeyFile(string branchOfficeCode, out string keyName, out string downKeyName)
        {
            string licenseNo = "";
            keyName = "";
            downKeyName = "";
            BranchCode = branchOfficeCode;
            resultArgs = GetBranchOfficeLicense(branchOfficeCode);
            if (resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                DataTable dtLicenseEncrypt = resultArgs.DataSource.Table;
                //Generate License key file
                keyName = AppFile.LicenseKeyName;
                downKeyName = "AcMEERPLicense.xml";

                //dll Conversion
                //downKeyName = "Acme.erpLicense.acp";

                //downKeyName = licenseNo.Remove(10);
                //downKeyName += DateTime.Now.Date.ToString("yyyyMMdd");
                //downKeyName += ".xml";
                if (dtLicenseEncrypt.Rows.Count > 0)
                {
                    dtLicenseEncrypt.WriteXml(PagePath.LicenseKeyFileName);
                    //dll conversion
                    // time being is commanded
                    //   resultArgs = CommonMember.CreateLicenceKey(CommonMember.GetXMLAsString());
                }
                else
                {
                    resultArgs.Message = "License Key is not available " + resultArgs.Message;
                    resultArgs.Success = false;
                }
            }
            else
            {
                resultArgs.Message = "License Key is not available " + resultArgs.Message;
                resultArgs.Success = false;
            }
            return resultArgs;

        }
        public ResultArgs GetBranchOfficeLicense(string branchOfficeCode)
        {
            string locationName = string.Empty;
            using (BranchLocationSystem locationSystem = new BranchLocationSystem())
            {
                resultArgs = locationSystem.FetchBranchLocationByBranch(DataBaseType.HeadOffice, branchOfficeCode);
                if (resultArgs != null && resultArgs.Success)
                {
                    locationName = resultArgs.DataSource.Data.ToString();
                }
            }
            return GetBranchOfficeLicense(branchOfficeCode, locationName);
        }

        public ResultArgs GetBranchOfficeLicense(string branchOfficeCode, string locationName)
        {
            string licenseNo = "";
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.LicenseDetailsByBranchCodeFetch))
            {
                BranchCode = branchOfficeCode;
                dataManagerArgs.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchCode);
                dataManagerArgs.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManagerArgs.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataTable dtLicenseInfo = resultArgs.DataSource.Table;
                    dtLicenseInfo.TableName = "LicenseKey";
                    DataTable dtLicenseEncrypt = dtLicenseInfo.Clone();
                    if (dtLicenseEncrypt.Rows.Count == 0)
                    {
                        DataRow drLicense = dtLicenseEncrypt.NewRow();
                        drLicense[this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.EMAILColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.EMAILColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.ADDRESSColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.ADDRESSColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.COUNTRYColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.COUNTRYColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.STATEColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.STATEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.YEAR_FROMColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.YEAR_FROMColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.YEAR_TOColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.YEAR_TOColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.BRANCH_KEY_CODEColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.BRANCH_KEY_CODEColumn.ColumnName].ToString());
                        licenseNo = dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LICENSE_KEY_NUMBERColumn.ColumnName].ToString();
                        drLicense[this.AppSchema.Branch_License.LICENSE_KEY_NUMBERColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LICENSE_KEY_NUMBERColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.NoOfNodesColumn.ColumnName]
                         = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.NoOfNodesColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.URLColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.URLColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.IS_LICENSE_MODELColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.IS_LICENSE_MODELColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.LOCK_MASTERColumn.ColumnName]
                       = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LOCK_MASTERColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.MAP_LEDGERColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.MAP_LEDGERColumn.ColumnName].ToString());

                        drLicense[this.AppSchema.Branch_License.ALLOW_MULTI_CURRENCYColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.ALLOW_MULTI_CURRENCYColumn.ColumnName].ToString());

                        drLicense[this.AppSchema.Branch_License.ATTACH_VOUCHERS_FILESColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.ATTACH_VOUCHERS_FILESColumn.ColumnName].ToString());

                        drLicense[this.AppSchema.Branch_License.IS_MULTILOCATIONColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.IS_MULTILOCATIONColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.ENABLE_PORTALColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.ENABLE_PORTALColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.KEY_GENERATED_DATEColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.KEY_GENERATED_DATEColumn.ColumnName].ToString());

                        drLicense[this.AppSchema.Branch_License.NoOfModulesColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.NoOfModulesColumn.ColumnName].ToString());

                        drLicense[this.AppSchema.Branch_License.ENABLE_REPORTSColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.ENABLE_REPORTSColumn.ColumnName].ToString());

                        drLicense[this.AppSchema.Branch_License.SocietyNameColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.SocietyNameColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.InstituteNameColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.InstituteNameColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.PINCODEColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.PINCODEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.PHONEColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.PHONEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.PLACEColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.PLACEColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.BranchOffice.MOBILE_NOColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.MOBILE_NOColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.AccessToMultiDBColumn.ColumnName]
                            //= CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.AccessToMultiDBColumn.ColumnName].ToString());
                        = (dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.AccessToMultiDBColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.APPROVE_BUDGET_BY_PORTALColumn.ColumnName]
                        = CommonMember.EncryptValue((dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.APPROVE_BUDGET_BY_PORTALColumn.ColumnName].ToString()));
                        drLicense[this.AppSchema.Branch_License.APPROVE_BUDGET_BY_EXCELColumn.ColumnName]
                        = CommonMember.EncryptValue((dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.APPROVE_BUDGET_BY_EXCELColumn.ColumnName].ToString()));
                        drLicense[this.AppSchema.Branch_License.IS_TWO_MONTH_BUDGETColumn.ColumnName]
                        = (dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.IS_TWO_MONTH_BUDGETColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.AUTOMATIC_BACKUP_PORTALColumn.ColumnName]
                        = (dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.AUTOMATIC_BACKUP_PORTALColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.CONTACTPERSONColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.CONTACTPERSONColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.FAXColumn.ColumnName]
                        = CommonMember.EncryptValue(dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.FAXColumn.ColumnName].ToString());
                        drLicense[this.AppSchema.Branch_License.LOCATIONColumn.ColumnName]
                        = CommonMember.EncryptValue((string.IsNullOrEmpty(locationName) ?
                        dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.LOCATIONColumn.ColumnName].ToString() : locationName));
                        drLicense[this.AppSchema.Branch_License.CRISTO_PARISH_CODEColumn.ColumnName]
                        = CommonMember.EncryptValue((string.IsNullOrEmpty(Parish_Code) ?
                        dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.CRISTO_PARISH_CODEColumn.ColumnName].ToString() : Parish_Code));

                        drLicense[this.AppSchema.BranchOffice.THIRDPARTY_CODEColumn.ColumnName]
                            = CommonMember.EncryptValue((string.IsNullOrEmpty(ThirdParty_Code) ?
                            dtLicenseInfo.Rows[0][this.AppSchema.BranchOffice.THIRDPARTY_CODEColumn.ColumnName].ToString() : ThirdParty_Code));

                        drLicense[this.AppSchema.Branch_License.THIRDPARTY_MODEColumn.ColumnName]
                            = CommonMember.EncryptValue((string.IsNullOrEmpty(THIRDPARTY_MODE) ?
                            dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.THIRDPARTY_MODEColumn.ColumnName].ToString() : THIRDPARTY_MODE));

                        drLicense[this.AppSchema.Branch_License.THIRDPARTY_URLColumn.ColumnName]
                         = CommonMember.EncryptValue((string.IsNullOrEmpty(THIRDPARTY_URL) ?
                         dtLicenseInfo.Rows[0][this.AppSchema.Branch_License.THIRDPARTY_URLColumn.ColumnName].ToString() : THIRDPARTY_URL));

                        dtLicenseEncrypt.Rows.Add(drLicense);
                        dtLicenseEncrypt.AcceptChanges();
                        resultArgs.DataSource.Data = dtLicenseEncrypt;
                        resultArgs.Success = true;
                    }
                }
                else
                {
                    resultArgs.Message = "License is not purchased for your branch office";
                    resultArgs.Success = false;
                }
                return resultArgs;
            }
        }

        public string GetBranchLocation(string branchOfficeCode)
        {
            string locationName = string.Empty;
            using (BranchLocationSystem locationSystem = new BranchLocationSystem())
            {
                resultArgs = locationSystem.FetchBranchLocationByBranch(DataBaseType.HeadOffice, branchOfficeCode);
                if (resultArgs != null && resultArgs.Success)
                {
                    locationName = resultArgs.DataSource.Data.ToString();
                }
            }
            return locationName;
        }
        public bool IsBranchMultilocated()
        {
            bool isMultiLocation = false;
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.LicenseDetailsByBranchCodeFetch))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchCode);
                resultArgs = dataManagerArgs.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Branch_License.IS_MULTILOCATIONColumn.ColumnName].ToString().Equals("1"))
                    {
                        isMultiLocation = true;
                    }
                }
            }
            return isMultiLocation;
        }

        public ResultArgs GetBranchOfficeLicenseDetails(int BranchOfficeId)
        {
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.LicenseDetailsByBranchIdFetch))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchOfficeId);
                dataManagerArgs.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManagerArgs.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 17/01/2022, To get Local community branch client details
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchLCBranchClientEnableModuleRequests()
        {
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.FetchLCBranchClientEnableModuleRequests, DataBaseType.HeadOffice))
            {
                resultArgs = dataManagerArgs.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 18/01/2022, Is LC request to enable Receipt Module
        /// </summary>
        /// <param name="licensecode"></param>
        /// <param name="branchcode"></param>
        /// <param name="headofficecode"></param>
        /// <param name="location"></param>
        /// <param name="ipaddress"></param>
        /// <param name="macaddress"></param>
        /// <returns></returns>
        public ResultArgs FetchLCBranchClientEnableModuleRequestsByBranch(string headofficecode, string branchcode, string location)
        {
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.FetchLCBranchClientEnableModuleRequestsByBranch, DataBaseType.HeadOffice))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn, headofficecode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn, branchcode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn, location);
                dataManagerArgs.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManagerArgs.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// 17/01/2022, check given local branch details already available 
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsExistsLCBranchClientEnableModuleRequestsByBranchRequestCode(string licensebranchrequestcode, string headofficecode, string branchcode, string location)
        {
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.IsExistsLCBranchClientEnableModuleRequestsByBranchRequestCode, DataBaseType.HeadOffice))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn, licensebranchrequestcode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn, branchcode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn, headofficecode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn, location);
                //dataManagerArgs.Parameters.Add(this.AppSchema.LCBranchClientDetails.LC_CLIENT_IP_ADDRESSColumn, ipaddress);
                //dataManagerArgs.Parameters.Add(this.AppSchema.LCBranchClientDetails.LC_CLIENT_MAC_ADDRESSColumn, macaddress);
                dataManagerArgs.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManagerArgs.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// 12/03/2022, To update branch local community client details
        /// </summary>
        /// <returns></returns>
        public ResultArgs RequestLCBranchClientEnableModuleRequests(string licensekeynumber, string headofficecode, string branchcode, string location,
                        string ipaddress, string macaddress, string localcommunityuser)
        {
            bool beginTrans = false;
            using (DataManager dataManager = new DataManager(SQLCommand.License.RequestLCBranchClientEnableModuleRequests, DataBaseType.HeadOffice))
            {
                try
                {
                    dataManager.BeginTransaction();
                    beginTrans = true;
                    DateTime createdon = DateSet.ToDate(DateTime.Today.Date.ToShortDateString(), false);
                    string lcRequestEnableCode = GetNewNumber(NumberFormats.LC_Branch_Enable_Request_IdentificationNumber, "", "", branchcode, dataManager);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn, lcRequestEnableCode);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LICENSE_KEY_NUMBERColumn, licensekeynumber);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn, branchcode);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn, headofficecode);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_NAMEColumn, branchcode);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_NAMEColumn, headofficecode);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn, location);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn, ipaddress);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn, macaddress);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUESTED_ONColumn, createdon);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUESTED_BYColumn, localcommunityuser);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.PORTAL_UPDATED_ONColumn, null);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.PORTAL_UPDATED_BYColumn, string.Empty);
                    dataManager.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.REMARKSColumn, string.Empty);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();

                    //If inserted properly, return unique geneated code for concern branch
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs.ReturnValue = lcRequestEnableCode;
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.Message;
                }
                finally
                {
                    if (beginTrans)
                    {
                        if (resultArgs != null && resultArgs.Success)
                        {
                            dataManager.EndTransaction();
                        }
                        else
                        {
                            dataManager.RollBackTransaction();
                        }
                    }
                }
            }


            return resultArgs;
        }



        /// <summary>
        /// 17/01/2022, To update branch local community client details
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateLCBranchModuleStatus(string lcbranchrequestcode, string headofficecode, string branchcode, string location, LCBranchModuleStatus ReceiptModuleStatus)
        {
            DateTime modifiedon = DateSet.ToDate(DateTime.Today.Date.ToShortDateString(), false);
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.UpdateLCBranchReceiptModuleStatus, DataBaseType.HeadOffice))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn, lcbranchrequestcode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn, headofficecode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn, branchcode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn, location);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn, (Int32)ReceiptModuleStatus);

                dataManagerArgs.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_DATEColumn, modifiedon);
                dataManagerArgs.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_BYColumn, this.LoginUserName);
                dataManagerArgs.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManagerArgs.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLCBranchModuleStatus(DataTable dtLCBranchRequests, string BOCode)
        {
            string condition = string.Empty;
            string conditionCondition = string.Empty;
            string msg = string.Empty;
            string ipAddress = string.Empty;
            string macAddress = string.Empty;
            Int32 depolymenttype = (Int32)DeploymentType.Standalone;
            List<string> isMailAlreadysend = new List<string>();

            resultArgs = new ResultArgs();
            if (dtLCBranchRequests != null && dtLCBranchRequests.Rows.Count > 0)
            {
                dtLCBranchRequests.DefaultView.RowFilter = this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName + " = '" + BOCode + "'";
                DataTable dtBORequest = dtLCBranchRequests.DefaultView.ToTable();

                if (dtBORequest.Rows.Count > 0)
                {
                    //---------------------------------------------------------
                    //1. Check all requests should be in single status (should not be mixed). For client server, it will be bsaed on IP Address
                    string[] uniquebranches = new string[] { "LC_HEAD_OFFICE_CODE", "LC_BRANCH_OFFICE_CODE", "LC_BRANCH_LOCATION" };
                    DataTable dtValidation = dtBORequest.DefaultView.ToTable();
                    DataTable dtListofBO = dtBORequest.DefaultView.ToTable(true, uniquebranches);
                    dtValidation.DefaultView.RowFilter = string.Empty;
                    foreach (DataRow dr in dtListofBO.Rows)
                    {
                        string hocode = dr[this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                        string branchcode = dr[this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                        string location = dr[this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName].ToString();

                        if (dtValidation.DefaultView.Count > 0)
                        {
                            dtValidation.DefaultView.RowFilter = string.Empty;
                            dtValidation.DefaultView.RowFilter = "LC_HEAD_OFFICE_CODE = '" + hocode + "' AND LC_BRANCH_OFFICE_CODE ='" + branchcode + "' AND LC_BRANCH_LOCATION = '" + location + "'";
                            depolymenttype = NumberSet.ToInteger(dtValidation.DefaultView[0][this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName].ToString());
                        }

                        if (depolymenttype == (Int32)DeploymentType.ClientServer)
                        {   //For Client Server System (All the requests from one system for client server should be one status (should not be mixed like few are disabled, few are approved)
                            /*string[] uniquebranchesClientServerIPAddress = new string[] { "LC_HEAD_OFFICE_CODE", "LC_BRANCH_OFFICE_CODE", "LC_BRANCH_LOCATION", "LC_BRANCH_CLIENT_IP_ADDRESS", "LC_BRANCH_CLIENT_MAC_ADDRESS" };
                            DataTable dtListofBOClientServerIPAddress = dtValidation.DefaultView.ToTable(true, uniquebranchesClientServerIPAddress);
                            resultArgs.Success = true;
                            foreach (DataRow drIpAddress in dtListofBOClientServerIPAddress.Rows)
                            {
                                resultArgs = CheckIndividualSystemRequests(dtValidation.DefaultView.ToTable());
                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }*/

                            resultArgs.Success = true;
                        }
                        else
                        {   //For Standalone System (All the requests from one branch for sandalone should be one status (should not be mixed like few are disabled, few are approved)
                            resultArgs = CheckIndividualSystemRequests(dtValidation.DefaultView.ToTable());
                        }

                        if (!resultArgs.Success)
                        {
                            resultArgs.Message += " (BO:" + branchcode + ", Location: " + location + ") ";
                            break;
                        }
                    }
                    //-----------------------------------------------------------


                    if (resultArgs.Success)
                    {
                        foreach (DataRow dr in dtBORequest.Rows)
                        {
                            string lcbranchrequestcode = dr[this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName].ToString();
                            string hocode = dr[this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                            string branchcode = dr[this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                            string location = dr[this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName].ToString();
                            string requestedby = dr[this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUESTED_BYColumn.ColumnName].ToString();
                            Int32 receiptmoduleRequest = NumberSet.ToInteger(dr[this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName].ToString());
                            LCBranchModuleStatus lcbranchmoduleStatus = LCBranchModuleStatus.Disabled;
                            if (receiptmoduleRequest == (Int32)LCBranchModuleStatus.Requested)
                            {
                                lcbranchmoduleStatus = LCBranchModuleStatus.Requested;
                            }
                            else if (receiptmoduleRequest == (Int32)LCBranchModuleStatus.Approved)
                            {
                                lcbranchmoduleStatus = LCBranchModuleStatus.Approved;
                            }
                            resultArgs = UpdateLCBranchModuleStatus(lcbranchrequestcode, hocode, branchcode, location, lcbranchmoduleStatus);

                            if (lcbranchmoduleStatus == LCBranchModuleStatus.Approved && !isMailAlreadysend.Contains(location))
                            {
                                SendModuleRequestAndApproved(branchcode, location, requestedby, LCBranchModuleStatus.Approved);
                                isMailAlreadysend.Add(location);
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "No Branch Local community Requests : " + BOCode;
                }
            }
            else
            {
                resultArgs.Message = "No Branch Local community Requests :" + BOCode;
            }

            return resultArgs;
        }


        /// <summary>
        /// This method is used to check Individual system request
        /// Recent Request alone can be changed, all the previous request must be disabled
        /// For Client Server : All the requests from one system for client server should be one status (should not be mixed like few are disabled, few are approved)
        /// For Standalone : All the requests from one branch for sandalone should be one status (should not be mixed like few are disabled, few are approved)
        /// 
        /// </summary>
        /// <param name="dtLCIndividualSystemRequests"></param>
        /// <returns></returns>
        public ResultArgs CheckIndividualSystemRequests(DataTable dtLCIndividualSystemRequests)
        {
            ResultArgs result = new ResultArgs();
            string lcrequestcode = string.Empty;
            if (dtLCIndividualSystemRequests.Rows.Count > 0)
            {
                dtLCIndividualSystemRequests.DefaultView.Sort = this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName + " DESC";
                if (dtLCIndividualSystemRequests.DefaultView.Count > 0)
                {
                    lcrequestcode = dtLCIndividualSystemRequests.DefaultView[0][this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName].ToString();
                    dtLCIndividualSystemRequests.DefaultView.RowFilter = "LC_BRANCH_REQUEST_CODE <> '" + lcrequestcode + "'";
                    dtLCIndividualSystemRequests = dtLCIndividualSystemRequests.DefaultView.ToTable();
                }

                //On 04/05/2022, all the previous requests must be disabled
                //DataTable dtLCBranchStatus = dtLCIndividualSystemRequests.DefaultView.ToTable(true, new string[] { this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName });
                //if (dtLCBranchStatus.Rows.Count > 1)
                dtLCIndividualSystemRequests.DefaultView.RowFilter = this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName + " IN (" + (int)LCBranchModuleStatus.Requested
                                                            + "," + (int)LCBranchModuleStatus.Approved + ")";
                if (dtLCIndividualSystemRequests.DefaultView.Count > 0)
                {
                    result.Message = "All the previous request must be disabled";
                }
                else
                {
                    result.Success = true;
                }
            }
            return result;
        }


        public ResultArgs SendModuleRequestAndApproved(string branccode, string location, string requestedby, LCBranchModuleStatus lcbrancstauts)
        {
            DataTable dtProjects = new DataTable();
            ResultArgs result = new ResultArgs();
            using (BranchOfficeSystem branchsystem = new BranchOfficeSystem())
            {
                result = branchsystem.FetchMailIdByBranchCode(branccode);
            }
            if (result.Success)
            {
                DataTable dtBranchMailInfo = result.DataSource.Table;
                if (dtBranchMailInfo.Rows.Count > 0)
                {
                    string HoCode = dtBranchMailInfo.Rows[0]["HEAD_OFFICE_CODE"].ToString();
                    string branchname = dtBranchMailInfo.Rows[0]["BRANCH"].ToString();
                    string MailId = dtBranchMailInfo.Rows[0]["MAIL_ID"].ToString();
                    //MailId = "anthonyselvam87@gmail.com"; 
                    //MailId = "anthonyselvam87@gmail.com, Kali@boscoits.com, alex@boscosofttech.com, chinna@boscosofttech.com, anand@boscoits.com, baskar@boscoits.com";
                    //string bcc = "Kali@boscoits.com, alex@boscosofttech.com, chinna@boscosofttech.com, alwar@boscoits.com";
                    //MailId = "inmeconomer@gmail.com";
                    MailId = "financemanager@sdbchennai.org";
                    string bcc = "anthonyselvam87@gmail.com, alex@boscosofttech.com";

                    string Subject = "Acme.erp : Local Branch Request - " + branccode + " (" + location + ")";
                    string budgetmsg = "The above Local Branch has requested to enable Receipt Module.";
                    if (lcbrancstauts == LCBranchModuleStatus.Approved)
                    {
                        Subject = "Acme.erp : Your Branch Request has been approved by Province Economer Office - " + branccode + " (" + location + ")";
                        budgetmsg = "The above Local Branch has been approved by Province Economer Office, You can make Receipt Voucher in Acme.erp";
                    }

                    string MainContent = "<b>Local Branch Request details</b>"
                                              + "<br />"
                                              + "Branch Office Name   : " + branchname + " <br />"
                                              + "Branch Location      : " + location + " <br />"
                                              + "Requested by         : " + requestedby + " <br />"
                                              + "<br />"
                                              + "<b>" + budgetmsg + "</b>";

                    //result = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(MailId), "", Subject, MainContent, true);
                    result = AcMEDSync.Common.SendEmail(MailId, bcc, Subject, MainContent, true);
                }
                else
                {
                    result.Message = "Module rights mail information is not found";
                }
            }
            return result;
        }


        /// <summary>
        /// On 25/04/2022, to clear all local branch requests
        /// </summary>
        /// <param name="headofficecode"></param>
        /// <param name="branchcode"></param>
        /// <returns></returns>
        public ResultArgs DeleteAllLCBranchModuleRequests(string headofficecode)
        {
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.DeleteAllLCBranchRequests, DataBaseType.HeadOffice))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn, headofficecode);
                dataManagerArgs.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManagerArgs.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteLCBranchRequestsByBranch(string headofficecode, string branchcode)
        {
            using (DataManager dataManagerArgs = new DataManager(SQLCommand.License.DeleteLCBranchRequestsByBranch, DataBaseType.HeadOffice))
            {
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn, headofficecode);
                dataManagerArgs.Parameters.Add(this.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn, branchcode);
                dataManagerArgs.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManagerArgs.UpdateData();
            }
            return resultArgs;
        }

        #endregion
    }
}
