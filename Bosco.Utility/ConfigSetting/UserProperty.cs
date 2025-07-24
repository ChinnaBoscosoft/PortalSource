/*  Class Name      : UserProperty
 *  Purpose         : Define/access user info for currently logged in user
 *  Author          : CS
 *  Created on      : 8-Jul-2010
 */

using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using Bosco.Utility;
using System.Drawing;

namespace Bosco.Utility.ConfigSetting
{
    public class UserProperty : SettingProperty
    {
        private const string UserIdField = "USER_ID";
        private const string UserNameField = "USER_NAME";
        private const string UserFullNameField = "NAME";
        private const string UserFirstName = "FIRSTNAME";
        private const string UserLastName = "LASTNAME";
        private const string UserAddressField = "ADDRESS";
        private const string RoleIdField = "ROLE_ID";
        private const string UserRole = "USERROLE";
        private const string UserMobileNoField = "CONTACT_NO";
        private const string UserEmailIdField = "EMAIL_ID";
        private const string UserTypeField = "USER_TYPE";
        private const string PasswordStatus = "PASSWORD_STATUS";
        private const string UserAdminField = "ADMIN_USER";
        private const string BranchCodeField = "BRANCH_CODE";
        private const string BranchNameField = "BRANCH_OFFICE_NAME";

        public const string HeadOfficeCodeField = "HEAD_OFFICE_CODE";
        private const string HeadOfficeNameField = "HEAD_OFFICE_NAME";
        private const string UserHeadOfficeCodeField = "USER_HEAD_OFFICE_CODE";
        private const string UserHeadOfficeNameField = "USER_HEAD_OFFICE_NAME";
        private const string HeadOfficeDatabaseConnectionField = "DATABASE_CONNECTION";

        private const string UserHeadOfficeInchargeNameField = "USER_HEAD_OFFICE_INCHARGE_NAME";
        private const string UserHeadOfficeInchargeDesignationField = "USER_HEAD_OFFICE_INCHARGE_DESIGNATION";


        private object objUserLock = new object();
        private string GetUserInfo(string name)
        {
            string val = "";

            lock (objUserLock)
            {
                Dictionary<string, string> dicUser = HttpContext.Current.Session[SessionMemeber.UserInfo]
                                                     as Dictionary<string, string>;

                if (dicUser != null && dicUser.Count > 0)
                {
                    val = dicUser[name];
                }
            }

            return val;
        }

        public DataView UserInfo
        {
            set
            {
                lock (objUserLock)
                {
                    Dictionary<string, string> dicUser = new Dictionary<string, string>();
                    DataView dvUser = value;

                    if (dvUser != null)
                    {
                        foreach (DataColumn dcUser in dvUser.Table.Columns)
                        {
                            dicUser[dcUser.ColumnName] = dvUser[0][dcUser.ColumnName].ToString();
                        }

                        HttpContext.Current.Session[SessionMemeber.UserInfo] = dicUser;
                        dvUser.Table.Clear();
                        dvUser.Dispose();
                    }
                    else
                    {
                        HttpContext.Current.Session.Remove(SessionMemeber.UserInfo);
                    }
                }
            }
        }


        /// <summary>
        /// Get logged in User Id
        /// </summary>
        public int LoginUserId
        {
            get
            {
                string user_Id = GetUserInfo(UserIdField);
                if (user_Id == "") user_Id = "0";
                int userId = 0;
                int.TryParse(user_Id, out userId);
                return userId;
            }
        }

        /// <summary>
        /// Get logged in Password Status
        /// </summary>
        public int LoginUserPasswordStatus
        {
            get
            {
                string user_PasswordStatus = GetUserInfo(PasswordStatus);
                if (user_PasswordStatus == "") user_PasswordStatus = "0";
                int userPasswordStatus = 0;
                int.TryParse(user_PasswordStatus, out userPasswordStatus);
                return userPasswordStatus;
            }
        }
        /// <summary>
        /// Get User is Logged in
        /// </summary>
        public bool HasLoginUser
        {
            get
            {
                int userId = LoginUserId;
                return (userId > 0);
            }
        }

        /// <summary>
        /// Get Name of the logged in user
        /// </summary>
        public string LoginUserName
        {
            get
            {
                return GetUserInfo(UserNameField);
            }
        }

        /// <summary>
        /// Get Full Name of the logged in user
        /// </summary>
        public string LoginUserFullName
        {
            get { return GetUserInfo(UserFullNameField); }
        }

        /// <summary>
        /// Get Address of the logged in user
        /// </summary>
        public string LoginUserAddress
        {
            get { return GetUserInfo(UserAddressField); }
        }

        /// <summary>
        /// Get Mobile No of the logged in user
        /// </summary>
        public string LoginUserMobileNo
        {
            get { return GetUserInfo(UserMobileNoField); }
        }

        /// <summary>
        /// Get logged in user Email Id
        /// </summary>
        public string LoginUserEmailId
        {
            get { return GetUserInfo(UserEmailIdField); }
        }

        public string GetUserRole
        {
            get { return GetUserInfo(UserRole); }
        }

        public string FirstName
        {
            get { return GetUserInfo(UserFirstName); }
        }

        public string LastName
        {
            get { return GetUserInfo(UserLastName); }
        }

        public int LoginUserRoleId
        {
            get
            {
                int roleId = 0;
                string roleNum = GetUserInfo(RoleIdField);
                int.TryParse(roleNum, out roleId);
                return roleId;
            }
        }

        public string LoginUserHeadOfficeCode
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[UserHeadOfficeCodeField] != null &&
                    HttpContext.Current.Session[UserHeadOfficeCodeField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[UserHeadOfficeCodeField].ToString();
                }

                return name;
            }

            set
            {
                HttpContext.Current.Session[UserHeadOfficeCodeField] = value;
                HeadOfficeCode = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }

        public string LoginUserHeadOfficeName
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[UserHeadOfficeNameField] != null &&
                    HttpContext.Current.Session[UserHeadOfficeNameField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[UserHeadOfficeNameField].ToString();
                }

                return name;
            }

            set
            {
                HttpContext.Current.Session[UserHeadOfficeNameField] = value;
                HeadOfficeName = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }

        public string LoginUserHeadOfficeInchargeName
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[UserHeadOfficeInchargeNameField] != null &&
                    HttpContext.Current.Session[UserHeadOfficeInchargeNameField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[UserHeadOfficeInchargeNameField].ToString();
                }

                return name;
            }

            set
            {
                HttpContext.Current.Session[UserHeadOfficeInchargeNameField] = value;
                HeadOfficeInchargeName = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }


        public string LoginUserHeadOfficeInchargeDesignation
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[UserHeadOfficeInchargeDesignationField] != null &&
                    HttpContext.Current.Session[UserHeadOfficeInchargeDesignationField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[UserHeadOfficeInchargeDesignationField].ToString();
                }

                return name;
            }

            set
            {
                HttpContext.Current.Session[UserHeadOfficeInchargeDesignationField] = value;
                HeadOfficeInchargeDesignation = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }

        /// <summary>
        /// Get logged in user type
        /// </summary>
        public UserType UserType
        {
            get
            {
                UserType userType = UserType.Portal;
                string user_Type = GetUserInfo(UserTypeField);
                if (user_Type != "") { userType = (UserType)this.EnumSet.GetEnumItemType(typeof(UserType), user_Type); }
                return userType;
            }
        }

        public bool IsAdminUser
        {
            get
            {
                bool isAdminUser = false;
                string user_admin = GetUserInfo(UserTypeField);

                if (user_admin != "")
                {
                    int adminUser = 0;
                    int.TryParse(user_admin, out adminUser);
                    isAdminUser = (adminUser == 1);
                }
                return isAdminUser;
            }
        }

        //Keep Headoffice code in Session for Connectiong to Concerned HO DB
        public string HeadOfficeCode
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[HeadOfficeCodeField] != null &&
                    HttpContext.Current.Session[HeadOfficeCodeField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[HeadOfficeCodeField].ToString();


                }
                return name;
            }

            set
            {
                HttpContext.Current.Session[HeadOfficeCodeField] = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }

        public bool IS_CMF_CONGREGATION
        {
            get
            {
                return (HeadOfficeCode.ToUpper().Substring(0, 3) == "CMF");
            }
        }

        public bool IS_ABEBEN_DIOCESE
        {
            get { return HeadOfficeCode.ToUpper() == "ABEBEN"; }
        }

        public bool IS_DIOMYS_DIOCESE
        {
            get { return HeadOfficeCode.ToUpper() == "DIOMYS"; }
        }

        public bool IS_CMFCHE_CONGREGATION
        {
            get
            {
                return (HeadOfficeCode.ToUpper() == "CMFCHE");
            }
        }

        public bool IS_BSG_CONGREGATION
        {
            get
            {
                return (HeadOfficeCode.ToUpper().Substring(0, 3) == "BSG");
            }
        }

        public bool IS_SAP_CONGREGATION
        {
            get
            {
                return (HeadOfficeCode.ToUpper().Substring(0, 3) == "SAP");
            }
        }

        public bool IS_SDBINM_CONGREGATION
        {
            get
            {
                return (HeadOfficeCode.ToUpper() == "SDBINM");
            }
        }

        /// <summary>
        /// On 29/06/2019, For Mumbai Province, In Budget, They need to maintain Province Help amount separately in Budget.
        /// It should be added into Budget income and should not be affected Finance
        /// </summary>
        public string BUDGET_HO_HELP_AMOUNT_CAPTION
        {
            get
            {
                return "Province Help";
            }
        }

        /// <summary>
        /// On 29/06/2019, For Mumbai Province, In Budget, They need to maintain Province Help amount separately in Budget.
        /// It should be added into Budget income and should not be affected Finance
        /// </summary>
        public bool ENABLE_BUDGET_HO_HELP_AMOUNT
        {
            get
            {
                return HeadOfficeCode.ToUpper() == "SDBINB";
            }
        }

        public string HeadOfficeName
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[HeadOfficeNameField] != null &&
                    HttpContext.Current.Session[HeadOfficeNameField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[HeadOfficeNameField].ToString();
                }
                return name;
            }

            set
            {
                HttpContext.Current.Session[HeadOfficeNameField] = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }

        public string HeadOfficeInchargeName
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[UserHeadOfficeInchargeNameField] != null)
                {
                    name = HttpContext.Current.Session[UserHeadOfficeInchargeNameField].ToString();
                }
                return name;
            }

            set
            {
                HttpContext.Current.Session[UserHeadOfficeInchargeNameField] = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }

        public string HeadOfficeInchargeDesignation
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[UserHeadOfficeInchargeDesignationField] != null)
                {
                    name = HttpContext.Current.Session[UserHeadOfficeInchargeDesignationField].ToString();
                }
                return name;
            }

            set
            {
                HttpContext.Current.Session[UserHeadOfficeInchargeDesignationField] = value;

                if (String.IsNullOrEmpty(value))
                {
                    HeadOfficeDBConnection = "";
                }
            }
        }

        public string HeadOfficeDBConnection
        {
            get
            {
                string name = "";

                if (HttpContext.Current.Session[HeadOfficeDatabaseConnectionField] != null &&
                    HttpContext.Current.Session[HeadOfficeDatabaseConnectionField].ToString() != String.Empty)
                {
                    name = HttpContext.Current.Session[HeadOfficeDatabaseConnectionField].ToString();
                }
                return name;
            }

            set { HttpContext.Current.Session[HeadOfficeDatabaseConnectionField] = value; }
        }

        public string LoginUserBranchOfficeCode
        {
            get
            {
                string branchCode = GetUserInfo(BranchCodeField);
                return branchCode;
            }
        }
        public string LoginUserBranchOfficeName
        {
            get
            {
                string branchName = GetUserInfo(BranchNameField);
                return branchName;
            }
        }

        public bool IsHeadOfficeUser
        {
            get
            {
                string headOfficeCode = LoginUserHeadOfficeCode;
                string branchCode = LoginUserBranchOfficeCode;
                return (headOfficeCode != "" && branchCode == "");
            }
        }

        public bool IsBranchOfficeUser
        {
            get
            {
                string headOfficeCode = LoginUserHeadOfficeCode;
                string branchCode = LoginUserBranchOfficeCode;
                return (headOfficeCode != "" && branchCode != "");
            }
        }

        public bool IsHeadOfficeAdminUser
        {
            get
            {
                string headOfficeCode = LoginUserHeadOfficeCode;
                string branchCode = LoginUserBranchOfficeCode;
                int roleId = LoginUserRoleId;
                return (headOfficeCode != "" && branchCode == "" && roleId == (int)UserType.HeadOffice);
            }
        }

        public bool IsHeadOfficeUserRights
        {
            get
            {
                bool Rtn = (IsHeadOfficeAdminUser && LoginUserName != LoginUserHeadOfficeCode);
                return Rtn;
            }
        }

        public bool IsBranchOfficeAdminUser
        {
            get
            {
                string branchCode = LoginUserBranchOfficeCode;
                int roleId = LoginUserRoleId;
                return (branchCode != "" && roleId == (int)UserType.BranchOffice);

            }
        }
        private bool isportaluser = false;
        public bool IsPortalUser
        {
            get
            {
                string headOfficeCode = LoginUserHeadOfficeCode;
                string branchCode = LoginUserBranchOfficeCode;
                return (headOfficeCode == "" && branchCode == "");
                //  return headOfficeCode.ToLower() == "portal" ? true : false;
            }
        }

        public bool IsPortalAdminUser
        {
            get
            {
                //string headOfficeCode = HeadOfficeCode;
                //string branchCode = BranchOfficeCode;
                //return (headOfficeCode != "" && branchCode == "");
                return true;
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
