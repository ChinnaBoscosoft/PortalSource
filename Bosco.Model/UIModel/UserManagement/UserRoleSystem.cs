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
    public class UserRoleSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Property
        public int Accessibility { get; set; }
        #endregion
        #region Constructor
        public UserRoleSystem()
        {

        }

        public UserRoleSystem(int userRoleId)
        {
            FillSaveUserRole(userRoleId);
        }
        public UserRoleSystem(int userRoleId,DataBaseType connectTo)
        {
            FillSaveUserRole(userRoleId, connectTo);
        }

        #endregion 

        #region Properties
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchUserRoleDetails()
        {
           return FetchUserRoleDetails(DataBaseType.Portal);
        }
        public ResultArgs FetchUserRoleDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.FetchAll,connectTo))
            {
                if (Accessibility > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveUserRoles()
        {
            return SaveUserRoles(DataBaseType.Portal);
        }
        public ResultArgs SaveUserRoles(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((UserRoleId == 0 ? SQLCommand.UserRole.Add : SQLCommand.UserRole.Edit), connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, UserRoleId);
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLEColumn, UserRoleName);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillSaveUserRole(int UserRoleId)
        {
            return FillSaveUserRole(UserRoleId, DataBaseType.Portal);
        }
        public ResultArgs FillSaveUserRole(int UserRoleId, DataBaseType connectTo)
        {
            resultArgs = FetchUserDetailsbyId(UserRoleId, connectTo);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                UserRoleName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.UserRole.USERROLEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteUserRole(int UserRoleID)
        {
            return DeleteUserRole(UserRoleID, DataBaseType.Portal);
        }
        
        public ResultArgs DeleteUserRole(int UserRoleID,DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, UserRoleID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchUserDetailsbyId(int UserRoleId)
        {
            return FetchUserDetailsbyId(UserRoleId, DataBaseType.Portal);
        }
        public ResultArgs FetchUserDetailsbyId(int UserRoleId,DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, UserRoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
              
        }
        #endregion
    }
}
