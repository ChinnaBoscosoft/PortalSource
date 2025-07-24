using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility.ConfigSetting;
using System.Threading;
using System.Globalization;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Setting
{
    public class GlobalSetting : SystemBase, ISetting
    {
        ResultArgs resultArgs = null;
        public void ApplySetting()
        {
            try
            {
                SettingProperty setting = new SettingProperty();

                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

                culture.NumberFormat.CurrencySymbol = setting.Currency;
                //Currency Symbol,Code,Position

                culture.NumberFormat.CurrencyPositivePattern = this.NumberSet.ToInteger(setting.CurrencyPositivePattern);
                culture.NumberFormat.CurrencyNegativePattern = this.NumberSet.ToInteger(setting.CurrencyNegativePattern);

                //for Currency  x
                if (setting.DigitGrouping != "0,0,0")
                {
                    culture.NumberFormat.CurrencyGroupSizes = setting.DigitGrouping.Split(',').Select(n => this.NumberSet.ToInteger(n)).ToArray();
                }
                culture.NumberFormat.CurrencyGroupSeparator = setting.GroupingSeparator;
                culture.NumberFormat.CurrencyDecimalDigits = this.NumberSet.ToInteger(setting.DecimalPlaces);
                culture.NumberFormat.CurrencyDecimalSeparator = setting.DecimalSeparator;

                //for Numbers
                if (setting.DigitGrouping != "0,0,0")
                {
                    culture.NumberFormat.NumberGroupSizes = setting.DigitGrouping.Split(',').Select(n => this.NumberSet.ToInteger(n)).ToArray();
                }
                culture.NumberFormat.NumberGroupSeparator = setting.GroupingSeparator;
                culture.NumberFormat.NumberDecimalDigits = this.NumberSet.ToInteger(setting.DecimalPlaces);
                culture.NumberFormat.NumberDecimalSeparator = setting.DecimalSeparator;

                //Thread.CurrentThread.CurrentCulture.fdf
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                //Thread.CurrentThread.CurrentCulture.NumberFormat = nformat;

                DateTimeFormatInfo date = new DateTimeFormatInfo();
                date.ShortDatePattern = setting.DateFormat;
                date.DateSeparator = setting.DateSeparator;

                Thread.CurrentThread.CurrentCulture.DateTimeFormat = date;
                //Date format and date separator


                //Get Inter Account/Province Contribution  -----------------------------
                InterAccountFromLedgerIds = "0";
                InterAccountToLedgerIds = "0";
                ProvinceFromLedgerIds = "0";
                ProvinceToLedgerIds = "0";
                if (SettingInfo != null && SettingInfo.Count > 0)
                {
                    InterAccountFromLedgerIds = GetSettingValue(Utility.Setting.InterAccountFromLedger);
                    InterAccountToLedgerIds = GetSettingValue(Utility.Setting.InterAccountToLedger);
                    ProvinceFromLedgerIds = GetSettingValue(Utility.Setting.ProvinceContributionFromLedger);
                    ProvinceToLedgerIds = GetSettingValue(Utility.Setting.ProvinceContributionToLedger);
                }
                //----------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        public ResultArgs SaveSetting(DataTable dtSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdate, DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();

                string SettingName = "";
                string Value = "";

                DataTable dtSet = dtSetting;

                if (dtSet != null)
                {
                    foreach (DataRow drSetting in dtSet.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.Setting.NameColumn.ColumnName].ToString();
                        Value = drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString();

                        dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, SettingName);
                        dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, Value);

                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success) { break; }
                        else
                            dataManager.Parameters.Clear();
                    }

                    if (resultArgs.Success)
                    {
                        this.SettingInfo = dtSetting.DefaultView;
                    }
                    else
                    {
                        this.SettingInfo = null;
                    }
                }
                dataManager.EndTransaction();

            }
            return resultArgs;
        }

        public ResultArgs FetchSettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.FetchUI, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserID);

                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public ResultArgs UpdateAccountingPeriod(string AccPeriodId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.UpdateStatus, DataBaseType.HeadOffice))
            {
                dataManager.Database = dataManager.Database;
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

    }
}
