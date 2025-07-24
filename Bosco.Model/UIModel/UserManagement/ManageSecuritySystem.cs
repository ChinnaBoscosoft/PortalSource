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
    public class ManageSecuritySystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchUserRoles()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManageSecurity.FetchUserRole))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs ManageSecurity()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManageSecurity.Fetch))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs SaveManageSecurity()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManageSecurity.Edit))
            {
                dataManager.Parameters.Add(this.AppSchema.User.ROLE_IDColumn,UserRoleId);
                dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, UserId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}
