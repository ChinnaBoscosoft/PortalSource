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
    public class RightSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor

        public RightSystem()
        {

        }

        public RightSystem(int moduleid)
        {
            FillSaveModule(moduleid);
        }

        #endregion

        #region Properties
        public int RightsId { get; set; }
        public string RoleId { get; set; }
        public int ActivitiesId { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchModuleDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Module.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveModuleDetails()
        {
            using (DataManager dataManager = new DataManager(RightsId == 0 ? SQLCommand.UserRole.Add : SQLCommand.UserRole.Edit))
            {
                dataManager.Parameters.Add(this.AppSchema.Module.MODULE_IDColumn, RightsId);
                dataManager.Parameters.Add(this.AppSchema.Module.MODULEColumn, RoleId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillSaveModule(int ModuleId)
        {
            resultArgs = FetchModuleDetailsbyId(ModuleId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                RoleId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Module.MODULEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteModule(int UserRoleID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Module.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.Module.MODULE_IDColumn, RightsId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchModuleDetailsbyId(int ModuleId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Module.MODULE_IDColumn, ModuleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

    }
}
