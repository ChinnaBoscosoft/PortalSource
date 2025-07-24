using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
        #endregion

        #region Properties
        public int RoleId { get; set; }
        public string ModuleCode { get; set; }
        public string Module { get; set; }
        public string ActivityCode { get; set; }
        public string Activity { get; set; }
        public int ModuleOrder { get; set; }
        public int ActivityPrder { get; set; }
        public bool Allow { get; set; }
        public int Type { get; set; }
        public int Accessibility { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchRightsbyRoleId(int UserRoleId)
        {
            return FetchRightsbyRoleId(UserRoleId, DataBaseType.Portal);

        }

        /// <summary>
        /// This is to fetch the user rights for the selected role
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public ResultArgs FetchRightsbyRoleId(int UserRoleId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.rights.ROLE_IDColumn, UserRoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;

        }


        public ResultArgs DeleteRoleRights()
        {
            return DeleteRoleRights(DataBaseType.Portal);
        }

        /// <summary>
        /// This is to delete the available user rights for the selected user role to insert newly
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteRoleRights(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.rights.ROLE_IDColumn, RoleId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        public ResultArgs GetAllUserRights()
        {
            return GetAllUserRights(DataBaseType.Portal);

        }

        public ResultArgs GetAllUserRights(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.FetchAllRightsByUserGroup, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ROLE_IDColumn, RoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;

        }

        public ResultArgs GetModulesList()
        {
            return GetModulesList(DataBaseType.Portal);
        }
        public ResultArgs GetModulesList(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.FetchModuleList, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }
        public ResultArgs GetActivitiesList()
        {
            return GetActivitiesList(DataBaseType.Portal);
        }

        public ResultArgs GetActivitiesList(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.FetchActivities, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public ResultArgs GetUsersType()
        {
            return GetUsersType(DataBaseType.Portal);
        }

        public ResultArgs GetUsersType(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.FetchRoleType, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public ResultArgs UpdateUserRights(DataBaseType connectTo)
        {
            object sqlCommand = null;
            bool IsRightsExists = IsUserRightsExists(connectTo);

            //For User Role
            if (Allow && !IsRightsExists)
                sqlCommand = SQLCommand.Rights.InsertRightsByUserRole; //Insert User Role Rights
            else if (!Allow && IsRightsExists)
                sqlCommand = SQLCommand.Rights.DeleteRightsByUserRole; //Delete User Role Rights
            else
                sqlCommand = SQLCommand.Rights.InsertRightsByUserRole;

            using (DataManager dataManager = new DataManager(sqlCommand, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ROLE_IDColumn, RoleId);
                dataManager.Parameters.Add(this.AppSchema.UserRights.MODULE_CODEColumn, ModuleCode);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACTIVITY_CODEColumn, ActivityCode);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ALLOWColumn, (Allow ? 1 : 0));
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        /// <summary>
        /// Check Rights exists for given user or type for Moduel and activity code
        /// </summary>
        /// <param name="uRights">UserRightsMode:UserType or User</param>
        /// <param name="sModuleCode">String:ModuleCode</param>
        /// <param name="sActivityCode">String:ActivityCode</param>
        /// <param name="Id">Int:UserId or UserTypeId</param>
        /// <returns></returns>
        private bool IsUserRightsExists(DataBaseType connectTo)
        {
            bool bRtn = true;

            using (DataManager dataManager = new DataManager(SQLCommand.Rights.CheckDuplicateUserRightsByUserRole, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ROLE_IDColumn, RoleId);
                dataManager.Parameters.Add(this.AppSchema.UserRights.MODULE_CODEColumn, ModuleCode);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACTIVITY_CODEColumn, ActivityCode);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataView);
                if (resultArgs.Success)
                {
                    bRtn = (resultArgs.RowsAffected > 0);
                }
            }
            return bRtn;
        }

        /// <summary>
        /// Get all Rights for User and Type for each module and activity code
        /// This data source will be used for menus to verify valid or not.
        /// </summary>
        /// <param name="UserId">int:UserId</param>
        /// <param name="UserTypeid">int:UserTypeid</param>
        /// <returns>ResultArgs:With its properties</returns>
        public ResultArgs GetUserRightsForMenu(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.FetchUserRightsForMenu, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ROLE_IDColumn, RoleId);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }

        /// <summary>
        /// Get User Rights Type (PAGE, TASK) for given module and activity code
        /// </summary>
        /// <param name="ModuleCode">String:ModuleCode</param>
        /// <param name="ActivityCode">String:ActivityCode</param>
        /// <returns>UserRightsType:PAGE OR TASK</returns>
        public UserRightsType GetUserRightsType(DataBaseType connectTo)
        {
            UserRightsType RightsType = UserRightsType.Page;

            using (DataManager dataManager = new DataManager(SQLCommand.Rights.FetchRightsType, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                dataManager.Parameters.Add(this.AppSchema.UserRights.MODULE_CODEColumn, ModuleCode);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACTIVITY_CODEColumn, ActivityCode);
                resultArgs = dataManager.FetchData(DataSource.DataView);
                if (resultArgs.Success)
                {
                    DataView dv = resultArgs.DataSource.TableView;
                    if (dv.Count > 0)
                    {
                        RightsType = dv[0][this.AppSchema.UserRights.TYPEColumn.ColumnName].ToString().Equals("0") ? UserRightsType.Page : UserRightsType.Task;
                    }
                }
            }
            return RightsType;
        }

        /// <summary>
        /// Get Rights for given User or User type and Module and activity code
        /// </summary>
        /// <param name="uRights">UserRightsMode:UserType or User</param>
        /// <param name="ModuleCode">String:Module Code of the activity</param>
        /// <param name="ActivityCode">String:ActivityCode</param>
        /// <param name="Id">Int:UserId or UserTypeId</param>
        /// <returns>ResultArgs:with its properties</returns>
        public ResultArgs GetUserRights(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Rights.FetchRightsByUserRole, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.ROLE_IDColumn, RoleId);
                dataManager.Parameters.Add(this.AppSchema.UserRights.MODULE_CODEColumn, ModuleCode);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACTIVITY_CODEColumn, ActivityCode);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }
        #endregion
    }
}
