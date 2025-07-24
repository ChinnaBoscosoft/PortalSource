using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.UIModel
{
    public class UserSystem : SystemBase
    {
        #region Declaration
        ApplicationSchema.UserDataTable dtUser = null;
        ResultArgs resultArgs = null;
        #endregion

        #region Properties

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public new string FirstName { get; set; }
        public new string LastName { get; set; }
        public int status { get; set; }
        public int PasswordStatus { get; set; }
        public string HeadOfficeCode { get; set; }
        public string BranchCode { get; set; }
        public string tblname { get; set; }
        public string UserStatus { get; set; }
        public string UserRole { get; set; }
        public int UserType { get; set; }
        public int Created_By { get; set; }
        public int Accessibility { get; set; }
        public DataTable UserNames { get; set; }
        public string CountryCode { get; set; }
        #endregion

        #region Constructor

        public UserSystem()
        {
            dtUser = this.AppSchema.User;
        }

        public UserSystem(int userId)
        {
            this.UserId = userId;
            FillUserProperties(DataBaseType.Portal);
        }

        public UserSystem(int userId, DataBaseType connectTo)
        {
            this.UserId = userId;
            FillUserProperties(connectTo);
        }

        #endregion

        #region Methods

        public ResultArgs AuthenticateUser(string userName, string passWord)
        {
            return AuthenticateUser(userName, passWord, DataBaseType.Portal);
        }

        public ResultArgs AuthenticateUser(string userName, string passWord, DataBaseType connectTo)
        {
            ResultArgs resultArgs = null;
            DataView dvUser = null;
            try
            {

                using (DataManager dataManager = new DataManager(SQLCommand.User.Authenticate, connectTo))
                {
                    //Check Encrypted pwd
                    dataManager.Parameters.Add(dtUser.USER_NAMEColumn, userName);
                    dataManager.Parameters.Add(dtUser.PASSWORDColumn, EncryptValue(passWord));
                    dataManager.Parameters.Add(dtUser.STATUSColumn, (int)Utility.Status.Active);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataView);

                    if (resultArgs != null && resultArgs.Success)
                    {
                        //Check Raw pwd
                        dvUser = resultArgs.DataSource.TableView;
                        if (dvUser != null && dvUser.Count == 0)
                        {
                            dataManager.Parameters.Clear();
                            dataManager.Parameters.Add(dtUser.USER_NAMEColumn, userName);
                            dataManager.Parameters.Add(dtUser.PASSWORDColumn, passWord);
                            dataManager.Parameters.Add(dtUser.STATUSColumn, (int)Utility.Status.Active);
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dataManager.FetchData(DataSource.DataView);
                        }
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        dvUser = resultArgs.DataSource.TableView;
                        resultArgs.Success = (dvUser != null && dvUser.Count == 1);

                        if (resultArgs.Success)
                        {
                            this.UserInfo = dvUser;
                        }
                        else
                        {
                            this.UserInfo = null;
                            new ErrorLog().WriteError("User login:" + resultArgs.Message.ToString() + "User table count:" + dvUser.Count);
                            resultArgs.Message = MessageCatalog.Message.Invalid_User;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        public ResultArgs GetUserSource()
        {
            return GetUserSource(DataBaseType.Portal);
        }

        public ResultArgs GetUserSource(DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchAll, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.User.CREATEDBYColumn, Created_By);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }

        public ResultArgs GetUserSource(int userId)
        {
            return GetUserSource(userId, DataBaseType.Portal);
        }

        public ResultArgs GetUserSource(int userId, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.Fetch, connectTo))
            {
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }

        public ResultArgs ResetPassword(int userId, string passWord, int passwordStatus)
        {
            return ResetPassword(userId, passWord, passwordStatus, DataBaseType.Portal);
        }

        public ResultArgs ResetPassword(int userId, string passWord, int passwordStatus, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.ResetPassword, connectTo))
            {
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, passWord);
                dataManager.Parameters.Add(dtUser.PASSWORD_STATUSColumn, passwordStatus);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }
        public ResultArgs ResetPassword(string username, string passWord, int passwordStatus, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.ResetPasswordByUserName, connectTo))
            {
                dataManager.Parameters.Add(dtUser.USER_NAMEColumn, username);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, passWord);
                dataManager.Parameters.Add(dtUser.PASSWORD_STATUSColumn, passwordStatus);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs CheckCurrentPassword(int userId, string passWord)
        {
            return CheckCurrentPassword(userId, passWord, DataBaseType.Portal);
        }

        public ResultArgs CheckCurrentPassword(int userId, string passWord, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.CheckOldPassword, connectTo))
            {
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, passWord);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }

        public ResultArgs FetchUserId(string userName)
        {
            return FetchUserId(userName, DataBaseType.Portal);
        }

        public ResultArgs FetchUserId(string userName, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchUserId, connectTo))
            {
                dataManager.Parameters.Add(dtUser.USER_NAMEColumn, userName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs SaveUser()
        {
            return SaveUser(DataBaseType.Portal);
        }

        public ResultArgs SaveUser(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((UserId.Equals(0) ?
                SQLCommand.User.Add : SQLCommand.User.Update), connectTo))
            {
                dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                dataManager.Parameters.Add(AppSchema.User.FIRSTNAMEColumn, FirstName);
                dataManager.Parameters.Add(AppSchema.User.LASTNAMEColumn, LastName);
                dataManager.Parameters.Add(AppSchema.User.USER_NAMEColumn, UserName);
                dataManager.Parameters.Add(AppSchema.User.PASSWORDColumn, Password);
                dataManager.Parameters.Add(AppSchema.User.ADDRESSColumn, Address);
                dataManager.Parameters.Add(AppSchema.User.CONTACT_NOColumn, MobileNo);
                dataManager.Parameters.Add(AppSchema.User.CITYColumn, City);
                dataManager.Parameters.Add(AppSchema.User.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(AppSchema.User.PLACEColumn, Place);
                dataManager.Parameters.Add(AppSchema.User.EMAIL_IDColumn, Email);
                dataManager.Parameters.Add(AppSchema.User.ROLE_IDColumn, RoleId);
                dataManager.Parameters.Add(AppSchema.User.NOTESColumn, Notes);
                dataManager.Parameters.Add(AppSchema.User.STATUSColumn, status);
                dataManager.Parameters.Add(AppSchema.User.CREATEDBYColumn, Created_By);
                dataManager.Parameters.Add(AppSchema.User.USER_TYPEColumn, UserType);
                dataManager.Parameters.Add(AppSchema.User.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(AppSchema.User.BRANCH_CODEColumn, BranchCode);

                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs UpdateProfile(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.Profile, connectTo))
            {
                dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                dataManager.Parameters.Add(AppSchema.User.FIRSTNAMEColumn, FirstName);
                dataManager.Parameters.Add(AppSchema.User.LASTNAMEColumn, LastName);
                dataManager.Parameters.Add(AppSchema.User.ADDRESSColumn, Address);
                dataManager.Parameters.Add(AppSchema.User.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(AppSchema.User.CONTACT_NOColumn, MobileNo);
                dataManager.Parameters.Add(AppSchema.User.CITYColumn, City);
                dataManager.Parameters.Add(AppSchema.User.PLACEColumn, Place);
                dataManager.Parameters.Add(AppSchema.User.EMAIL_IDColumn, Email);
                dataManager.Parameters.Add(AppSchema.User.NOTESColumn, Notes);

                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs DeleteUser()
        {
            return DeleteUser(UserId, DataBaseType.Portal);
        }

        public ResultArgs DeleteUser(DataBaseType connectTo)
        {
            return DeleteUser(UserId, connectTo);
        }

        public ResultArgs DeleteUser(int userId)
        {
            return DeleteUser(userId, DataBaseType.Portal);
        }


        public ResultArgs DeleteUser(int userId, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.Delete, connectTo))
            {
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs UpdateUserStatus(int userId)
        {
            return UpdateUserStatus(userId, DataBaseType.Portal);
        }


        public ResultArgs UpdateUserStatus(int userId, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.UpdateUserStatus, connectTo))
            {
                dataManager.Parameters.Add(dtUser.STATUSColumn, status);
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs FetchUserDetail()
        {
            return FetchUserDetail(DataBaseType.Portal);
        }

        public ResultArgs FetchUserDetail(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(base.IsHeadOfficeUser ? SQLCommand.User.FetchAllByHeadOfficeUser : SQLCommand.User.FetchAll, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.User.CREATEDBYColumn, Created_By);
                dataManager.Parameters.Add(this.AppSchema.User.BRANCH_CODEColumn, BranchCode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, this.LoginUserHeadOfficeCode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPortalUserDetail(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchAllCreatedBy, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchHOUserDetail(object CommandId)
        {
            using (DataManager dataManager = new DataManager(CommandId, DataBaseType.HeadOffice))
            {
                if (!base.IsAdminUser)
                {
                    dataManager.Parameters.Add(AppSchema.User.CREATEDBYColumn, Created_By);
                }
                dataManager.Parameters.Add(AppSchema.User.ROLE_IDColumn, RoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchUsersForProjectMapping(DataBaseType connectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(base.IsHeadOfficeAdminUser ? SQLCommand.User.FetchHeadOfficeUsers : SQLCommand.User.FetchBranchOfficeUsers, connectTo))
                {
                    if (base.IsBranchOfficeAdminUser)
                    {
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                    }
                    dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    using (DataManager dataManager = new DataManager(base.IsHeadOfficeAdminUser ? SQLCommand.User.FetchHeadOfficeUsersAdmin : SQLCommand.User.FetchBranchOfficeUsersAdmin, connectTo))
                    {
                        if (base.IsBranchOfficeAdminUser)
                        {
                            dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                        }
                        dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUserId);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchUsersForBranchMapping(DataBaseType connectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(base.IsHeadOfficeAdminUser ? SQLCommand.User.FetchHeadOfficeUsers : SQLCommand.User.FetchBranchOfficeUsers, connectTo))
                {
                    if (base.IsBranchOfficeAdminUser)
                    {
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                    }
                    dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    using (DataManager dataManager = new DataManager(base.IsHeadOfficeAdminUser ? SQLCommand.User.FetchHeadOfficeUsersAdmin : SQLCommand.User.FetchBranchOfficeUsersAdmin, connectTo))
                    {
                        if (base.IsBranchOfficeAdminUser)
                        {
                            dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                        }
                        dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUserId);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        private void FillUserProperties(DataBaseType connectTo)
        {
            resultArgs = FetchUserDetailsById(connectTo);
            DataTable dtUserEdit = resultArgs.DataSource.Table;

            if (dtUserEdit != null && dtUserEdit.Rows.Count > 0)
            {
                FirstName = dtUserEdit.Rows[0][AppSchema.User.FIRSTNAMEColumn.ColumnName].ToString();
                LastName = dtUserEdit.Rows[0][AppSchema.User.LASTNAMEColumn.ColumnName].ToString();
                this.UserId = NumberSet.ToInteger(dtUserEdit.Rows[0][AppSchema.User.USER_IDColumn.ColumnName].ToString());
                UserName = dtUserEdit.Rows[0][AppSchema.User.USER_NAMEColumn.ColumnName].ToString();
                Password = dtUserEdit.Rows[0][AppSchema.User.PASSWORDColumn.ColumnName].ToString();
                RoleId = NumberSet.ToInteger(dtUserEdit.Rows[0][AppSchema.User.ROLE_IDColumn.ColumnName].ToString());
                Address = dtUserEdit.Rows[0][AppSchema.User.ADDRESSColumn.ColumnName].ToString();
                MobileNo = dtUserEdit.Rows[0][AppSchema.User.CONTACT_NOColumn.ColumnName].ToString();
                Email = dtUserEdit.Rows[0][AppSchema.User.EMAIL_IDColumn.ColumnName].ToString();
                Place = dtUserEdit.Rows[0][AppSchema.User.PLACEColumn.ColumnName].ToString();
                City = dtUserEdit.Rows[0][AppSchema.User.CITYColumn.ColumnName].ToString();
                Notes = dtUserEdit.Rows[0][AppSchema.User.NOTESColumn.ColumnName].ToString();
                status = NumberSet.ToInteger(dtUserEdit.Rows[0][AppSchema.User.STATUSColumn.ColumnName].ToString());
                HeadOfficeCode = dtUserEdit.Rows[0][AppSchema.User.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                BranchCode = dtUserEdit.Rows[0][AppSchema.User.BRANCH_CODEColumn.ColumnName].ToString();
                UserRole = dtUserEdit.Rows[0][AppSchema.User.USERROLEColumn.ColumnName].ToString();
                UserStatus = dtUserEdit.Rows[0][AppSchema.User.USERSTATUSColumn.ColumnName].ToString();
                CountryCode = dtUserEdit.Rows[0][AppSchema.User.COUNTRY_CODEColumn.ColumnName].ToString();
            }
        }

        public ResultArgs FetchUserDetailsById()
        {
            return FetchUserDetailsById(DataBaseType.Portal);
        }

        public ResultArgs FetchUserDetailsById(DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.Fetch, connectTo))
            {
                dataMember.Parameters.Add(this.AppSchema.User.USER_IDColumn, this.UserId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchUserDetailsByHeadOfficeCode(DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.FetchUserByHeadOffice, connectTo))
            {
                dataMember.Parameters.Add(this.AppSchema.User.USER_NAMEColumn, UserName);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        /// <summary>
        /// This is to bind user role to combo box
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchUserRoleList()
        {
            return FetchUserRoleList(DataBaseType.Portal);
        }

        public ResultArgs FetchUserRoleList(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.FetchUserRole, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRoleListById(int roleId)
        {
            return FetchRoleListById(roleId, DataBaseType.Portal);
        }

        public ResultArgs FetchRoleListById(int roleId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.FetchRole, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, roleId);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRoleListByIdWithAdmin(int roleId)
        {
            return FetchRoleListByIdWithAdmin(roleId, DataBaseType.Portal);
        }

        public ResultArgs FetchRoleListByIdWithAdmin(int roleId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.FetchRoleByAdmin, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, roleId);
                dataManager.Parameters.Add(this.AppSchema.UserRights.ACCESSIBILITYColumn, Accessibility);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRoleById(int roleId)
        {
            return FetchRoleById(roleId, DataBaseType.Portal);
        }
        public ResultArgs FetchRoleById(int roleId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.FetchRoleByRoleId, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, roleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string FetchAdminEmail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchAdminEmail, DataBaseType.Portal))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        #endregion
    }
}
