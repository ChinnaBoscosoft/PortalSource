using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Configuration;

namespace Bosco.Model
{
    public class SystemBase : UserProperty
    {
        private string rowIdColumn = "";
        private string hideColumns = "";
        private string columnOrder = "";
        private AppSchemaSet.ApplicationSchemaSet appSchema = null;

        public SystemBase()
        {
            appSchema = new AppSchemaSet().AppSchema;
        }

        public AppSchemaSet.ApplicationSchemaSet AppSchema
        {
            get { return appSchema; }
        }

        public string RowIdColumn
        {
            get { return rowIdColumn; }
            set { rowIdColumn = value; }
        }

        public string HideColumns
        {
            get { return hideColumns; }
            set { hideColumns = value; }
        }

        public string ColumnOrder
        {
            get { return columnOrder; }
            set { columnOrder = value; }
        }

        /// <summary>
        /// On 22/03/2018, Check Acmeerp DB server is connected or not
        /// if not connected, mail will be sent to defaul acmeerp id and (Alex and Chinna)
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsDatabaseServerConnected()
        {
            bool Connected = false;
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (HeadOfficeSystem HOsystem = new HeadOfficeSystem())
                {
                    resultArgs = HOsystem.FetchHeadOffice();
                    if (resultArgs !=null && resultArgs.Success )
                    {
                        Connected = true;
                    }
                }
            }
            catch(Exception err)
            {
                resultArgs.Message = "Database Server is not connected";
            }

            if (!Connected)
            {
                using (SettingSystem settingsystem = new SettingSystem())
                {
                    //Fix default Alex, Chinna
                    settingsystem.SendEmail("alex@boscosofttech.com", "lourdu@boscoits.com,chinna@boscosofttech.com",
                            "Acme.erp Database Server is down", "Acme.erp is not able to connect Database Server. Check MySQL Service.");
                }
            }
            return resultArgs;
        }


        #region IDisposable Members

        public override void Dispose()
        {
            if (appSchema != null)
            {
                appSchema = null;
                base.Dispose();
            }
        }

        #endregion
    }
}
