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
    public class UISetting : SystemBase, ISetting
    {
        ResultArgs resultArgs = null;
        public void ApplySetting()
        {
            try
            {
                UISettingProperty setting = new UISettingProperty();
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setting.UILanguage);

                //Date format and date separator
                DateTimeFormatInfo date = new DateTimeFormatInfo();
                date.ShortDatePattern = setting.UIDateFormat;
                date.DateSeparator = setting.UIDateSeparator;

                Thread.CurrentThread.CurrentCulture.DateTimeFormat = date;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        public ResultArgs SaveSetting(DataTable dtSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                dataManager.BeginTransaction();

                string SettingName = "";
                string Value = "";
                int UserId = 0;

                DataTable dtUISetting = dtSetting;
                if (dtUISetting != null)
                {
                    foreach (DataRow drSetting in dtUISetting.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.Setting.NameColumn.ColumnName].ToString();
                        Value = drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString();
                        UserId = this.NumberSet.ToInteger(drSetting["UserId"].ToString());

                        dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, SettingName);
                        dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, Value);
                        dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserId);

                        resultArgs = dataManager.UpdateData();

                        if (!resultArgs.Success) { break; }
                        else { dataManager.Parameters.Clear(); }
                    }

                    if (resultArgs.Success)
                    {
                        this.UISettingInfo = dtUISetting.DefaultView;
                    }
                    else
                    {
                        this.UISettingInfo = null;
                    }
                }
                dataManager.EndTransaction();
            }

            return resultArgs;
        }

        public ResultArgs FetchSettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.FetchUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserID);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        private ResultArgs SaveSettingDetails(string settingName, string value, int userId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, settingName);
                dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, value);
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, userId);

                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }
    }
}
