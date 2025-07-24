using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.UIModel
{
    public class MasterRightsSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public MasterRightsSystem()
        {

        }
        #endregion

        #region Properties
        public string MasterName { get; set; }
        public int AccessRights { get; set; }
        #endregion

        #region Methods

        public int MasterRights()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterRights.FetchByMasterName))
            {
                dataManager.Parameters.Add(this.AppSchema.MasterRights.MASTER_NAMEColumn, MasterName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion
    }
}
