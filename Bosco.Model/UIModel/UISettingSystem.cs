using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model
{
    public class UISettingSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        public UISettingSystem()
        {
        }

        public ResultArgs SaveUISettingDetails(DataTable dvSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                for (int i = 0; i < dvSetting.Rows.Count; i++)
                {
                    dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, dvSetting.Rows[i][1]);
                    dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, dvSetting.Rows[i][2]);
                    dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, dvSetting.Rows[i][3]);

                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        //Keep the setting Info into session
                        this.UISettingInfo = dvSetting.DefaultView;
                    }
                    else
                    {
                        this.UISettingInfo = null;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteUISettingDetails(int UId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.DeleteUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchUISettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.FetchUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn,UserID);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }
    }
}
