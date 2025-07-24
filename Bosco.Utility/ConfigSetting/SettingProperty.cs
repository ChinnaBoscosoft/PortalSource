using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.Utility;
using System.Web;

namespace Bosco.Utility.ConfigSetting
{
    public class SettingProperty : UISettingProperty
    {
        //private static DataView dvSetting = null;
        public static DataView dvAccPeriod = null;
        private static DataView dvuserProjectInfo = null;
        private static string societyName = null;
        private static string institudeName = null;
        private static int UserRoleId = 0;
        private static string UserId = string.Empty;
        private static string noofNodes = null;
        private static string noOfModules = null;
        private static string address = null;
        private static string place = null;
        private static string pincode = null;
        private static string phone = null;
        private static string contactperson = null;
        private static string fax = null;
        private static string email = null;
        private static string url = null;
        private static string state = null;
        private static string country_info = null;

        private const string SettingNameField = "Name";
        private const string SettingValueField = "Value";

        private const string YearFromField = "YEAR_FROM";
        private const string YearToField = "YEAR_TO";
        private const string AccyearField = "ACC_YEAR_ID";
        private const string BookBeginningFromField = "BOOKS_BEGINNING_FROM";
        private const string ProjectIdField = "PROJECT_ID";
        private const string ProjectField = "PROJECT";
        private const string RecentVoucherDateField = "VOUCHER_DATE";
        public static string ActiveDatabaseName = "acperp";
        private static SettingProperty current = null;

        private static Int32 allowMultiCurrency = 0;

        public static string RestoreMultipleDBPath = @"C:\AcME_ERP\";
        public static string RestoreMultipleDBFileName = @"MultipleDB.xml";

        public string DigitGroupings = "3,2,2";
        public static string UILanguages = "en-US";
        public string Currencys = "र";
        public string DecimalPlacess = "2";
        public string DecimalSeparators = ".";
        public string GroupingSeparators = ",";
        public static string CurrencyPositivePatterns = "2";
        public static string CurrencyNegativePatterns = "9";
        public static string UIDateFormats = "dd/MM/yyyy";
        public static string UIDateSeparators = "/";
        //Temporary settings to enable/disable DataSyc, Localization and Mutiple database features-----------------------
        public static bool EnableDataSync = false;
        public static bool EnableLocalization = false;
        public static bool EnableMultipleDatabaseConnect = false;
        public static bool EnableTDS = false;
        public static bool EnablePayroll = false;
        public static bool EnableStack = false;
        public static bool EnableTallyMigration = false;
        public static bool EnableOrdinaryRestore = false;
        //public static bool EnableConnectDatabase = false;
        public static bool EnableLicenseKeyFile = true;
        //public static bool EnableMulitpleDatabaseRestore = false;
        public static bool next = false;

        private const string TDS_ON_FD_LEDGERID = "TDS_ON_FD_LEDGERIDS";
        private const string INTER_ACCOUNT_FROM_LEDGERIDS = "INTER_ACCOUNT_FROM_LEDGERIDS";
        private const string INTER_ACCOUNT_TO_LEDGERIDS = "INTER_ACCOUNT_TO_LEDGERIDS";
        private const string PROVINCE_FROM_LEDGERIDS = "PROVINCE_FROM_LEDGERIDS";
        private const string PROVINCE_TO_LEDGERIDS = "PROVINCE_TO_LEDGERIDS";
        private const string SHOW_BUDGET_LEDGER_ACTUAL_BALANCE = "SHOW_BUDGET_LEDGER_ACTUAL_BALANCE";
        private const string SHOW_BUDGET_LEDGER_SEPARATE_RECEIPT_PAYMENT_ACTUALBALANCE = "SHOW_BUDGET_LEDGER_SEPARATE_RECEIPT_PAYMENT_ACTUALBALANCE";

        //-----------------------------------------------------------------------------------------------------------------

        //public static DataTable dtLoginDB = new DataTable();
        //public static SettingProperty Current
        //{
        //    get
        //    {
        //        if (current == null) { current = new SettingProperty(); }
        //        return current;
        //    }
        //}

        private string GetSettingInfo(string name)
        {
            string val = "";

            try
            {
                DataView dvSetting = SettingInfo;
                if (dvSetting != null && dvSetting.Count > 0)
                {
                    for (int i = 0; i < dvSetting.Count; i++)
                    {
                        string record = dvSetting[i][SettingNameField].ToString();

                        if (name == record)
                        {
                            val = dvSetting[i][SettingValueField].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

            return val;
        }

        /// <summary>
        /// Set Setting Info as Dataview
        /// </summary>
        public DataView SettingInfo
        {
            set
            {
                HttpContext.Current.Session["dvSetting"] = value;
            }
            get
            {
                DataView dv = null;
                if (HttpContext.Current.Session["dvSetting"] != null)
                {
                    dv = HttpContext.Current.Session["dvSetting"] as DataView;
                }
                return dv;
            }
        }

        // 30/10/2024 - Chinna
        public string HeadOfficeCode
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[UserProperty.HeadOfficeCodeField] != null &&
                    HttpContext.Current.Session[UserProperty.HeadOfficeCodeField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[UserProperty.HeadOfficeCodeField].ToString();


                }
                return name;
            }
        }


        public string SocietyName
        {
            get { return societyName; }
            set { SettingProperty.societyName = value; }
        }

        public string InstituteName
        {
            get { return institudeName; }
            set { SettingProperty.institudeName = value; }
        }

        public string NoOfNodes
        {
            get { return noofNodes; }
            set { SettingProperty.noofNodes = value; }
        }

        public string NoOfModules
        {
            get { return noOfModules; }
            set { SettingProperty.noOfModules = value; }
        }
        public string Address
        {
            get { return address; }
            set { SettingProperty.address = value; }
        }
        public string Place
        {
            get { return place; }
            set { SettingProperty.place = value; }
        }
        public string PinCode
        {
            get { return pincode; }
            set { SettingProperty.pincode = value; }
        }
        public string ContactPerson
        {
            get { return contactperson; }
            set { SettingProperty.contactperson = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { SettingProperty.phone = value; }
        }
        public string CountryInfo
        {
            get { return country_info; }
            set { SettingProperty.country_info = value; }
        }
        public string State
        {
            get { return state; }
            set { SettingProperty.state = value; }
        }
        public string Fax
        {
            get { return fax; }
            set { SettingProperty.fax = value; }
        }
        public string Email
        {
            get { return email; }
            set { SettingProperty.email = value; }
        }
        public string URL
        {
            get { return url; }
            set { SettingProperty.url = value; }
        }

        public string Country
        {
            get
            {
                return GetSettingInfo(Setting.Country.ToString());
            }
        }
        public string ForeignBankAccount
        {
            get
            {
                return GetSettingInfo(Setting.UIForeignBankAccount.ToString());
            }

        }

        // 30/04/2024 - Chinna
        public string Currency
        {
            get
            {
                //return GetSettingInfo(Setting.Currency.ToString());
                if (this.HeadOfficeCode.ToUpper() == "SDBANN")
                {
                    Currencys = "KES";
                    DigitGroupings = "3,3,3";
                }
                return Currencys;
            }
        }

        public string CurrencyPosition
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyPosition.ToString());
            }
        }

        public string CurrencyPositivePattern
        {
            get
            {
                //  return GetSettingInfo(Setting.CurrencyPositivePattern.ToString());
                return CurrencyPositivePatterns;
            }
        }

        public string CurrencyNegativePattern
        {
            get
            {
                //return GetSettingInfo(Setting.CurrencyNegativePattern.ToString());
                return CurrencyNegativePatterns;
            }
        }

        public string CurrencyNegativeSign
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyNegativeSign.ToString());
            }
        }

        public string CurrencyCode
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyCode.ToString());
            }
        }

        public string CurrencyCodePosition
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyCodePosition.ToString());
            }
        }

        public string DigitGrouping
        {
            get
            {
                //return GetSettingInfo(Setting.DigitGrouping.ToString());
                return DigitGroupings;
            }
        }

        public string GroupingSeparator
        {
            get
            {
                //return GetSettingInfo(Setting.GroupingSeparator.ToString());
                return GroupingSeparators;
            }
        }

        public string DecimalPlaces
        {
            get
            {
                //return GetSettingInfo(Setting.DecimalPlaces.ToString());
                return DecimalPlacess;
            }
        }

        public string DecimalSeparator
        {
            get
            {
                //return GetSettingInfo(Setting.DecimalSeparator.ToString());
                return DecimalSeparators;
            }
        }

        public string HighNaturedAmt
        {
            get
            {
                return GetSettingInfo(Setting.HighNaturedAmt.ToString());
            }
        }

        public string TransEntryMethod
        {
            get
            {
                return GetSettingInfo(Setting.TransEntryMethod.ToString());
            }
        }

        public string Language
        {
            get
            {
                //return GetSettingInfo(Setting.UILanguage.ToString());
                return UILanguages;
            }
        }

        public string DateFormat
        {
            get
            {
                //  return GetSettingInfo(Setting.UIDateFormat.ToString());
                return UIDateFormats;
            }
        }

        public string DateSeparator
        {
            get
            {
                //return GetSettingInfo(Setting.UIDateSeparator.ToString());
                return UIDateSeparators;
            }
        }

        public string Themes
        {
            get
            {
                return GetSettingInfo(Setting.UIThemes.ToString());
            }
        }

        public string TransClose
        {
            get
            {
                return GetSettingInfo(Setting.UITransClose.ToString());
            }

        }

        public string ProjSelection
        {
            get
            {
                return GetSettingInfo(Setting.UIProjSelection.ToString());
            }
        }

        public string VoucherPrint
        {
            get
            {
                return GetSettingInfo(Setting.PrintVoucher.ToString());
            }
        }

        public string TransMode
        {
            get
            {
                return GetSettingInfo(Setting.UITransMode.ToString());
            }
        }



        public DataView AccPeriodInfo
        {
            set
            {
                SettingProperty.dvAccPeriod = value;
            }
        }

        public DataView UserProjectInfor
        {
            set
            {
                SettingProperty.dvuserProjectInfo = value;
            }
        }

        public string GetSettingValue(Setting setting)
        {
            string Rtn = "";
            Rtn = GetSettingInfo(setting.ToString());
            return Rtn;
        }

        private string GetAccPeriodInfo(string name)
        {
            string val = "";

            if (dvAccPeriod != null && dvAccPeriod.Count > 0)
            {
                val = dvAccPeriod[0][name].ToString();// dvAccPeriod[0][name].ToString();
            }
            return val;
        }

        private string GetUserProjectInfo(string name)
        {
            string val = "";

            if (dvuserProjectInfo != null && dvuserProjectInfo.Count > 0)
            {
                val = dvuserProjectInfo[0][name].ToString();// dvAccPeriod[0][name].ToString();
            }
            return val;
        }

        public string YearFrom
        {
            get
            {
                return GetAccPeriodInfo(YearFromField);
            }
        }

        public string YearTo
        {
            get
            {
                return GetAccPeriodInfo(YearToField);
            }
        }

        public string AccPeriodId
        {
            get
            {
                return GetAccPeriodInfo(AccyearField);
            }
        }

        public string BookBeginFrom
        {
            get
            {
                return GetAccPeriodInfo(BookBeginningFromField);
            }
        }

        public string UserProjectId
        {
            get
            {
                return GetUserProjectInfo(ProjectIdField);
            }
        }

        private static string hobconnectionString = string.Empty;
        public static string HOBConnectionString
        {
            set
            {
                hobconnectionString = value;
            }
            get
            {
                return hobconnectionString;
            }
        }

        public string RecentVoucherDate
        {
            get
            {
                return GetUserProjectInfo(RecentVoucherDateField);
            }
        }

        public string UserProject
        {
            get
            {
                return GetUserProjectInfo(ProjectField);
            }
        }

        //On 09/09/2024, To allow multi currency
        public Int32 AllowMultiCurrency
        {
            get
            {
                //On 28/10/2024, To enfource Multi currency for few
                if (allowMultiCurrency == 0 && (this.HeadOfficeCode.ToUpper() == "SDBANN" || this.HeadOfficeCode.ToUpper() == "SDBROMA"))
                {
                    allowMultiCurrency = 1;
                }
                return allowMultiCurrency;
            }
            set
            {
                allowMultiCurrency = value;
            }
        }


        /// <summary>
        /// This property is used in Report Filter Criteria form.
        /// </summary>
        public DateTime deDateFromValue;
        public DateTime AssignDateFrom
        {
            get
            {
                return deDateFromValue;
            }
            set
            {
                deDateFromValue = value;
            }
        }



        /// <summary>
        /// This property is used in Report Filter Criteria form.
        /// </summary>
        public DateTime deDateToValue;
        public DateTime AssignDateTo
        {
            get
            {
                return deDateToValue;
            }
            set
            {
                deDateToValue = value;
            }
        }

        public int RoleId
        {
            get
            {
                return UserRoleId;
            }
            set
            {
                SettingProperty.UserRoleId = value;
            }
        }


        public string LoginUserId
        {
            get
            {
                return UserId;
            }
            set
            {
                SettingProperty.UserId = value;
            }
        }


        public int TDSOnFDInterestLedgerId
        {
            get
            {
                int tdsOnFDInterestLedgerId = 0;
                if (HttpContext.Current.Session[TDS_ON_FD_LEDGERID] != null)
                {
                    tdsOnFDInterestLedgerId = NumberSet.ToInteger(HttpContext.Current.Session[TDS_ON_FD_LEDGERID].ToString());
                }

                return tdsOnFDInterestLedgerId;
            }
            set { HttpContext.Current.Session[TDS_ON_FD_LEDGERID] = value; }
        }

        public string InterAccountFromLedgerIds
        {
            get
            {
                string interaccountfromids = "0";

                if (HttpContext.Current.Session[INTER_ACCOUNT_FROM_LEDGERIDS] != null)
                {
                    interaccountfromids = HttpContext.Current.Session[INTER_ACCOUNT_FROM_LEDGERIDS].ToString();
                }

                if (string.IsNullOrEmpty(interaccountfromids))
                {
                    interaccountfromids = "0";
                }
                return interaccountfromids;
            }

            set { HttpContext.Current.Session[INTER_ACCOUNT_FROM_LEDGERIDS] = value; }
        }

        public string InterAccountToLedgerIds
        {
            get
            {
                string interaccounttoids = "0";

                if (HttpContext.Current.Session[INTER_ACCOUNT_TO_LEDGERIDS] != null)
                {
                    interaccounttoids = HttpContext.Current.Session[INTER_ACCOUNT_TO_LEDGERIDS].ToString();
                }

                if (string.IsNullOrEmpty(interaccounttoids))
                {
                    interaccounttoids = "0";
                }

                return interaccounttoids;
            }

            set { HttpContext.Current.Session[INTER_ACCOUNT_TO_LEDGERIDS] = value; }
        }

        public string ProvinceFromLedgerIds
        {
            get
            {
                string provinceFromledgerIds = "0";

                if (HttpContext.Current.Session[PROVINCE_FROM_LEDGERIDS] != null)
                {
                    provinceFromledgerIds = HttpContext.Current.Session[PROVINCE_FROM_LEDGERIDS].ToString();
                }

                if (string.IsNullOrEmpty(provinceFromledgerIds))
                {
                    provinceFromledgerIds = "0";
                }

                return provinceFromledgerIds;
            }

            set { HttpContext.Current.Session[PROVINCE_FROM_LEDGERIDS] = value; }
        }

        public string ProvinceToLedgerIds
        {
            get
            {
                string provincetoledgerIds = "0";

                if (HttpContext.Current.Session[PROVINCE_TO_LEDGERIDS] != null)
                {
                    provincetoledgerIds = HttpContext.Current.Session[PROVINCE_TO_LEDGERIDS].ToString();
                }

                if (string.IsNullOrEmpty(provincetoledgerIds))
                {
                    provincetoledgerIds = "0";
                }

                return provincetoledgerIds;
            }

            set { HttpContext.Current.Session[PROVINCE_TO_LEDGERIDS] = value; }
        }

        public string ShowBudgetLedgerActualBalance
        {
            get
            {
                string value = "0";

                if (HttpContext.Current.Session[SHOW_BUDGET_LEDGER_ACTUAL_BALANCE] != null)
                {
                    value = HttpContext.Current.Session[SHOW_BUDGET_LEDGER_ACTUAL_BALANCE].ToString();
                }

                if (string.IsNullOrEmpty(value))
                {
                    value = "0";
                }

                return value;
            }

            set { HttpContext.Current.Session[SHOW_BUDGET_LEDGER_ACTUAL_BALANCE] = value; }
        }

        public string ShowBudgetLedgerSeparateReceiptPaymentActualBalance
        {
            get
            {
                string value = "0";

                if (HttpContext.Current.Session[SHOW_BUDGET_LEDGER_SEPARATE_RECEIPT_PAYMENT_ACTUALBALANCE] != null)
                {
                    value = HttpContext.Current.Session[SHOW_BUDGET_LEDGER_SEPARATE_RECEIPT_PAYMENT_ACTUALBALANCE].ToString();
                }

                if (string.IsNullOrEmpty(value))
                {
                    value = "0";
                }

                return value;
            }

            set { HttpContext.Current.Session[SHOW_BUDGET_LEDGER_SEPARATE_RECEIPT_PAYMENT_ACTUALBALANCE] = value; }
        }

        #region IDisposable Members

        public override void Dispose()
        {
            //GC.Collect();
        }

        #endregion
    }
}
