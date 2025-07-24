using System;
using System.Collections.Generic;
using System.Text;

namespace Bosco.Utility.ConfigSetting
{
    public class SettingManager : ISettingManager
    {
        private static SettingManager settingManager = null;

        public static SettingManager Instance 
        { 
            get 
            {
                if (settingManager == null) { settingManager = new SettingManager(); }
                return settingManager; 
            } 
        }

        #region ISettingManager Members

        public string GetPageUrl(string pageName)
        {
            string pageUrl = String.Empty;

            if (!String.IsNullOrEmpty(pageName))
            {
                UrlSetting.ResourceManager.IgnoreCase = true;
                object objPageUrl = UrlSetting.ResourceManager.GetString(pageName);

                if (objPageUrl != null)
                {
                    pageUrl = objPageUrl.ToString();
                }
            }

            if (pageUrl != String.Empty) { pageUrl = PagePath.UIStartupPath + pageUrl; }
            return pageUrl;
        }

        #endregion
    }
}
