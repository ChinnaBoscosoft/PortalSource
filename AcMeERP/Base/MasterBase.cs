using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Text.RegularExpressions;
using Bosco.Model.Setting;

namespace AcMeERP.Base
{
    public class MasterBase : System.Web.UI.MasterPage
    {
        private string timeFrom = "";
        private string timeTo = "";
        private bool hideTime = false;

        private string headerTitle = "";
        private string headerTitle2 = "";
        private string pageTitle = "";
        private string message = "";
        private string provider = "";

        public MasterBase()
        {
            GlobalSetting objGlobal = new GlobalSetting();
            objGlobal.ApplySetting();
        }

        public virtual string TimeFrom
        {
            get { return timeFrom; }
            set { timeFrom = value; }
        }

        public virtual string TimeTo
        {
            get { return timeTo; }
            set { timeTo = value; }
        }

        public virtual bool ShowTimeFromTimeTo
        {
            get { return hideTime; }
            set { hideTime = value; }
        }

        public virtual string HeaderTitle
        {
            get { return headerTitle; }
            set { headerTitle = value; }
        }

        public virtual string HeaderTitle2
        {
            get { return headerTitle2; }
            set { headerTitle2 = value; }
        }

        public virtual string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; }
        }

        public virtual string Message
        {
            get { return message; }
            set { message = value; }
        }

        public virtual bool IsLookUpPage
        {
            set { IsLookUpPage = value; }
        }

        public virtual string SiteMenuProvider
        {
            set { provider = value; }
        }

        public virtual bool LockPage
        {
            set { bool pgLock = value; }
        }

        public UIBase ActivePage
        {
            get { return this.Page as UIBase; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public virtual string FocusControlIdFromMessagePopUp
        {
            get
            {
                string _focusControId = "";
                if (ViewState["FocusControId"] != null)
                {
                    _focusControId = ViewState["FocusControId"].ToString();
                }
                return _focusControId;
            }
            set { ViewState["FocusControId"] = value; }
        }


        protected void HandleSessionExpiry(int userId)
        {
            if (userId <= 0)
            {
                string sDefaultPage = ActivePage.GetPageUrlByName(URLPages.Default.ToString());
                Response.Redirect(sDefaultPage + "?msg=1", true);
            }
        }
        /// <summary>
        /// This function is used to check userrights for logged user.
        /// Check user indivual rights, if it is not eixts, check based on its user type rights
        /// Will redirect error page if Rights Type is PAGE and logged user doesn;t has rights
        /// Will return boolean value if Rights Type is TASK
        /// </summary>
        /// <param name="ModuleCode">RightsModule:Module Code</param>
        /// <param name="ActivityCode">ActivityCode:Activity Code</param>
        /// <param name="UserId">int:UserId</param>
        /// <param name="UserTypeId">int UserTypeId</param>        
        /// <returns>BOOL: True:has rights</returns>
        /// <remarks>It will return always true if looged user type is admin</remarks>
        public virtual bool CheckUserRights(RightsModule ModuleCode, RightsActivity ActivityCode, int UserTypeId, DataBaseType connectTo)
        {
            return CheckUserRights(ModuleCode, ActivityCode, UserTypeId, false, connectTo);
        }

        /// <summary>
        /// This function is used to check userrights for logged user.
        /// Check user indivual rights, if it not eixts, check based on its user type rights
        /// Will redirect error page if Rights Type is PAGE and logged user doesn;t has rights
        /// Will return boolean value if Rights Type is TASK
        /// </summary>
        /// <param name="ModuleCode">RightsModule:Module Code</param>
        /// <param name="ActivityCode">ActivityCode:Activity Code</param>
        /// <param name="UserId">int:UserId</param>
        /// <param name="UserTypeId">int UserTypeId</param>
        /// <param name="dontRedirect">Bool:Is Redirct to error page or not</param>
        /// <returns>BOOL: True:has rights</returns>
        /// <remarks>It will return always true if looged user type is admin</remarks>
        public virtual bool CheckUserRights(RightsModule ModuleCode, RightsActivity ActivityCode, int userRoleId, bool dontRedirect, DataBaseType connectTo)
        {
            bool IsAllowed = false;
            UserRightsType RightsType = UserRightsType.Page;
            if (this.ActivePage.LoginUser.IsPortalUser)
            {
                if ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin)
                {
                    IsAllowed = IsValidUserCheckUserRights(ModuleCode, ActivityCode, userRoleId, connectTo, IsAllowed, RightsType);
                }
                else if ((UserRole)userRoleId == UserRole.Admin)
                {
                    IsAllowed = true;
                }
            }
            else
            {
                if (((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin) &&
               ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.BranchAdmin))
                {
                    IsAllowed = IsValidUserCheckUserRights(ModuleCode, ActivityCode, userRoleId, connectTo, IsAllowed, RightsType);
                }
                else if (((UserRole)this.ActivePage.LoginUser.LoginUserRoleId == UserRole.Admin) ||
              ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId == UserRole.BranchAdmin))
                {
                    IsAllowed = true;
                }
            }
            if (!IsAllowed)
            {
                if (RightsType == UserRightsType.Page && !dontRedirect)
                {
                    string sErrorPage = ActivePage.GetPageUrlByName(URLPages.ErrorPage.ToString());
                    Response.Redirect(sErrorPage + "?msg=" + MessageCatalog.Message.RightsDenied, true);
                }
            }

            return IsAllowed;
        }
        private bool IsValidUserCheckUserRights(RightsModule ModuleCode, RightsActivity ActivityCode, int userRoleId, DataBaseType connectTo, bool IsAllowed, UserRightsType RightsType)
        {
            ResultArgs resultArg = new ResultArgs();

            //Check Loggedin User's and User type's Rights
            // First check User's rights, if not exists check rights for its user type
            //FOR PAGE : If there is no records and allowed false means, Redirect to Error page.
            //FOR TASK : If there is no records and allowed false means, Return False
            using (RightSystem rightsSystem = new RightSystem())
            {
                rightsSystem.ModuleCode = ModuleCode.ToString();
                rightsSystem.ActivityCode = ActivityCode.ToString();
                RightsType = rightsSystem.GetUserRightsType(connectTo);
                rightsSystem.RoleId = userRoleId;
                //Check in User Type
                resultArg = rightsSystem.GetUserRights(connectTo);
                if (resultArg.Success)
                {
                    DataView dvUserTypeRights = resultArg.DataSource.TableView;
                    if (dvUserTypeRights.Count > 0)
                    {
                        IsAllowed = dvUserTypeRights[0]["ALLOW"].ToString().Equals("1") ? true : false;
                    }
                }
            }
            return IsAllowed;
        }

        /// <summary>
        /// Method to predict the next code
        /// </summary>
        /// <param name="parCode"></param>
        /// <returns></returns>
        public static string CodePredictor(string parCode, DataTable dtCode)
        {
            parCode = parCode != string.Empty ? parCode : "000";
            string finalCode = "";
            string tempstr = "";
            string Code = string.Empty;
            try
            {
                string prefixCode = Regex.Match(parCode, @"^[A-Z|a-z]+").Value;
                string digitCode = Regex.Match(parCode, @"\d+").Value;
                string suffixCode = Regex.Match(parCode, @"[A-Z|a-z]+$").Value;
                int tempCode = Convert.ToInt32(digitCode) + 1;
                if (prefixCode.Length != parCode.Length)
                {
                    if (digitCode.Length != parCode.Length)
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                    else
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                }
                // To check the generated code is present already or not
                for (int i = 0; i < dtCode.Rows.Count && dtCode != null && dtCode.Rows.Count > 0; i++)
                {
                    if (finalCode.Equals(dtCode.Rows[i][0].ToString())) { finalCode = CodePredictor(finalCode, dtCode); i = 0; }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }

            return finalCode;
        }

        /// <summary>
        /// To add zero to string 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string AddZero(int length)
        {
            string tempstr = "";
            for (int i = 1; i <= length; i++)
                tempstr = tempstr + "0";
            return tempstr;
        }
        /// <summary>
        /// To count zeros present in the digitCode
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        private static int ZeroCount(string digit)
        {
            Regex reg = new Regex(@"0+");
            Match mat = reg.Match(digit);
            string tempstr = mat.Value;
            return tempstr.Length;
        }

    }
}