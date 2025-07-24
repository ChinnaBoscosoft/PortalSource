using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.Utility;

namespace Bosco.Utility.ConfigSetting
{
    public class UISettingProperty : TransProperty
    {
        private static DataView dvUISetting = null;
        private const string SettingNameField = "Name";
        private const string SettingValueField = "Value";

        private string GetUISettingInfo(string name)
        {
            string val = "";
            try
            {
                if (dvUISetting != null && dvUISetting.Count > 0)
                {
                    for (int i = 0; i < dvUISetting.Count; i++)
                    {
                        string record = dvUISetting[i][SettingNameField].ToString();
                        if (name == record)
                        {
                            val = dvUISetting[i][SettingValueField].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
           
            }
            return val;
        }

        public DataView UISettingInfo
        {
            set
            {
                UISettingProperty.dvUISetting = value;
            }
            get
            {
                return dvUISetting;
            }
        }

        public string UILanguage
        {
            get
            {
                return GetUISettingInfo(UserSetting.UILanguage.ToString());
            }
        }

        public string UIDateFormat
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIDateFormat.ToString());
            }
        }

        public string UIDateSeparator
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIDateSeparator.ToString());
            }
        }

        public string UIThemes
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIThemes.ToString());
            }
        }

        public string UITransClose
        {
            get
            {
                return GetUISettingInfo(UserSetting.UITransClose.ToString());
            }
        }

        public string UIVoucherPrint
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIPrintVoucher.ToString());
            }
        }
        public string ForeignBankAccount
        {
            get
            {
                return GetUISettingInfo(Setting.UIForeignBankAccount.ToString());
            }
        }
        public string UIProjSelection
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIProjSelection.ToString());
            }
        }
        #region IDisposable Members

        public override void Dispose()
        {
            //GC.Collect();
        }

        #endregion
    }
}
