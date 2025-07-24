using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using AcMeERP.Base;
using Bosco.Utility;

namespace AcMeERP.MasterPage
{
    public partial class HomeLoginMaster : Base.MasterBase
    {
        #region Declaration
        DataView dvUserRights = new DataView();
        #endregion

        public ScriptManager MasterScriptManager
        {
            get { return scmMain; }
        }

        public override string SiteMenuProvider
        {
            set
            {
                dsMenuTop.SiteMapProvider = value;
                LoadUserRights();
            }
        }

        public override string PageTitle
        {
            get { return ltTitle.Text; }
            set { ltTitle.Text = value; }
        }

        public override string TimeFrom
        {
            set
            {
                lblTimeFrom.Text = "Execution Start Time :" + value;
            }
        }

        public override string TimeTo
        {
            set
            {
                lblTimeTo.Text = " Execution End Time :" + value;
            }
        }

        public override bool ShowTimeFromTimeTo
        {
            set
            {
                lblTimeTo.Visible = value;
                lblTimeFrom.Visible = value;
            }
        }

        public override string Message
        {
            get { return lblMsg.Text; }
            set
            {
                lblMsg.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    divmsg.Style.Add("visibility", "hidden");
                }
                else
                {
                    divmsg.Style.Add("visibility", "visible");
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Message = string.Empty;
            LoadScripts();
            int userId = 0;
            if (!IsPostBack)
            {
                string userName = "";
                string headOfficeCode = "";

                if (this.ActivePage != null)
                {
                    userId = this.ActivePage.LoginUser.LoginUserId;
                    userName = this.ActivePage.LoginUser.LoginUserName;
                    headOfficeCode = this.ActivePage.HeadOfficeCode;
                    if (headOfficeCode == "") { headOfficeCode = URLPages.Portal.ToString(); }

                    if (userName != "") { userName = userName + "!"; }
                    lblUser.Text = userName;
                    if (headOfficeCode.Equals(URLPages.Portal.ToString()))
                    {
                        hlkLogout.NavigateUrl = "~/" + URLPages.Portal.ToString().ToLower();
                    }
                    else
                    {
                        hlkLogout.NavigateUrl = "~/" + headOfficeCode;
                    }

                    if (lblUser.Text == "")
                    {
                        hlkLogout.Text = " Login";
                        hlMyAcct.Visible = false;
                    }
                    else
                    {
                        hlkLogout.Text = " Logout";
                        hlMyAcct.Visible = true;
                    }
                }

                ltHeader.Text = string.IsNullOrEmpty(ActivePage.LoginUser.LoginUserHeadOfficeCode) ? PortalTitle.PortalName : ActivePage.LoginUser.LoginUserHeadOfficeName;
                //+ " - " + ActivePage.LoginUser.LoginUserHeadOfficeCode;
                HandleSessionExpiry(userId);
                lblMsg.Text = string.Empty;

            }
            else
            {
                userId = this.ActivePage == null ? 0 : this.ActivePage.LoginUser.LoginUserId;
                HandleSessionExpiry(userId);
            }

        }

        #region Methods
        private void LoadScripts()
        {
            HtmlGenericControl hgc = new HtmlGenericControl("script");

            hgc = new HtmlGenericControl("script");
            hgc.Attributes.Add("type", "text/javascript");
            hgc.Attributes.Add("src", ResolveClientUrl("~/Scripts/Validation.js"));
            head.Controls.Add(hgc);

            hgc = new HtmlGenericControl("script");
            hgc.Attributes.Add("type", "text/javascript");
            hgc.Attributes.Add("src", ResolveClientUrl("~/Scripts/jquery.js"));
            head.Controls.Add(hgc);

            hgc = new HtmlGenericControl("script");
            hgc.Attributes.Add("type", "text/javascript");
            hgc.Attributes.Add("src", ResolveClientUrl("~/Scripts/progress.js"));
            head.Controls.Add(hgc);

        }

        #endregion

        protected void mnuTop_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            //Assign Module and Activity code from the site map node custom properties
            // to concern menu's value.
            // This value will be used in DataBound event to check user rights.
            MenuItem mnuItem = e.Item;
            SiteMapNode smpNode = mnuItem.DataItem as SiteMapNode;
            string sModuleCode = smpNode["ModuleCode"];
            string sActivityCode = smpNode["ActivityCode"];
            string _target = smpNode["target"];
            mnuItem.Value = sModuleCode + Delimiter.AtCap + sActivityCode;
            if (!string.IsNullOrEmpty(_target))
            {
                mnuItem.Target = _target;
            }

            //02/03/2020, After User Rights Checking, show/hide few menu links based on Head office ---------------------
            string headOfficeCode = this.ActivePage.HeadOfficeCode;
            if (!string.IsNullOrEmpty(sActivityCode))
            {
                if (sActivityCode == "CommunityGeneralateReport")
                {

                }
                //1. Hide/Remove "Mysoure Budget" link other than Mysoure dioces Head office
                //Remove General Budget Aproval link for Mysore dioeces Head Office
                if ((headOfficeCode.ToUpper() != "DIOMYS" && sActivityCode.ToUpper() == "MYSOREAPPROVEBUDGET") ||
                   (headOfficeCode.ToUpper() == "DIOMYS" && sActivityCode.ToUpper() == "APPROVEBUDGET") ||
                    // (headOfficeCode.ToUpper() == "SDBINM" && sActivityCode.ToUpper() == "APPROVEBUDGET" || sActivityCode.ToUpper() == "BUDGET") ||
                   (headOfficeCode.ToUpper() == "SDBINM" && sActivityCode.ToUpper() == "BUDGET") ||
                   (headOfficeCode.ToUpper() == "DIOMYS" && sActivityCode.ToUpper() == "BUDGETDETAILS"))
                {
                    mnuItem.Parent.ChildItems.Remove(mnuItem);
                }

                //2. Hide/Remove "Annual Reports" link other than FDCCSI Head office
                if (headOfficeCode.ToUpper() != "FDCCSI" && sActivityCode.ToUpper() == "GENERALATEANNUALREPORTS")
                {
                    mnuItem.Parent.ChildItems.Remove(mnuItem);
                }

                if (headOfficeCode != string.Empty)
                {
                    //3. For except SDBINM and BOSCOCO (Demo)
                    if ((headOfficeCode.ToUpper().Substring(0, 3) != "SDB" && headOfficeCode.ToUpper().Substring(0, 3) != "BOS"))
                    {
                        //# Hide/Remove Mapping HO ledgers with Generlate ledgers based on Project Category Group for other than sdb congregation and Demo
                        //# Hide/Remove Generalate Mapping Asset Opening other than sdb Congregation and Demo
                        //# Hide/Remove Generalate Mapping settings other than sdb Congregation
                        //# Hide/Remove Remove Generalate Generalate Reports other than sdb Congregation

                        //For except SDBINM and BOSCOCO (Demo)
                        if (sActivityCode.ToUpper() == "PCGGENERALATELEDGERMAPPING" ||
                            sActivityCode.ToUpper() == "GENERALATEOPENING" ||
                            sActivityCode.ToUpper() == "SETTINGS" ||
                            sActivityCode.ToUpper() == "REPORTGENERALATE1" || sActivityCode.ToUpper() == "REPORTGENERALATE2")
                        {
                            mnuItem.Parent.ChildItems.Remove(mnuItem);
                        }
                        else if ((headOfficeCode.ToUpper() != "SDBINM") && sActivityCode.ToUpper() == "REQUESTRECEIPTMODULE")
                        { //Show Request receipt module 
                            mnuItem.Parent.ChildItems.Remove(mnuItem);
                        }
                    }

                    //4. For SDBINM & BoscoS Remove General Mapping HO ledgers with Generlate ledgers
                    // if ((headOfficeCode.ToUpper().Substring(0, 3) == "SDB" && headOfficeCode.ToUpper().Substring(0, 3) == "BOS") && sActivityCode.ToUpper() == "MAPGENERALATELEDGERS")
                    //    if ((headOfficeCode.ToUpper().Substring(0, 3) == "SDB" || headOfficeCode.ToUpper().Substring(0, 3) == "BOS") &&
                    // if ((headOfficeCode.ToUpper().Substring(0, 3) == "SDB") &&
                    //(sActivityCode.ToUpper() == "MAPGENERALATELEDGERS" || sActivityCode.ToUpper() == "GENERALATELEDGERBALANCE" || sActivityCode.ToUpper() == "SUBSIDY" || sActivityCode.ToUpper() == "CASHANDBANKBOOK"))
                    if ((headOfficeCode.ToUpper().Substring(0, 3) == "SDB") &&
                        (sActivityCode.ToUpper() == "MAPGENERALATELEDGERS" || sActivityCode.ToUpper() == "GENERALATELEDGERBALANCE" || sActivityCode.ToUpper() == "SUBSIDY" || sActivityCode.ToUpper() == "CASHANDBANKBOOK"))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }

                    //On 28/03/2023 - Temp for Chinna - Remove FMA - remove generlate ledger balance
                    if ((headOfficeCode.ToUpper().Substring(0, 3) == "FMA") &&
                       (sActivityCode.ToUpper() == "MAPGENERALATELEDGERS" || sActivityCode.ToUpper() == "GENERALATELEDGERBALANCE"))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }
                    else if ((headOfficeCode.ToUpper().Substring(0, 3) != "FMA") &&
                            sActivityCode.ToUpper() == "COMMUNITYGENERALATEREPORT") //Remove Community Generalate reports other than FMA
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }

                    // Sap to Show Receipts and Payment Reports
                    if ((headOfficeCode.ToUpper().Substring(0, 3) == "SAP") &&
                        (sActivityCode.ToUpper() == "ACCOUNTSRECEIPTSANDPAYMENTS"))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }
                    else if ((headOfficeCode.ToUpper().Substring(0, 3) != "SAP") &&
                        (sActivityCode.ToUpper() == "SAPACCOUNTSRECEIPTSANDPAYMENTS"))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }

                    // Sap to Show Balancesheet Reports
                    if ((headOfficeCode.ToUpper().Substring(0, 3) == "SAP") &&
                        (sActivityCode.ToUpper() == "ACCOUNTSBALANCESHEET"))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }
                    else if ((headOfficeCode.ToUpper().Substring(0, 3) != "SAP") &&
                        (sActivityCode.ToUpper() == "SAPACCOUNTSBALANCESHEET"))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }

                    if (headOfficeCode.ToUpper() == "FDCCSI" && sActivityCode.ToUpper() == "REPORTGENERALATE")
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }

                    else if ((headOfficeCode.ToUpper() != "SDBINM") &&
                        (sActivityCode.ToUpper() == "BUDGETNEWINM"))  // (sActivityCode.ToUpper() == "BUDGETINM")) 
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }

                    if ((headOfficeCode.ToUpper() != "SDBINM") &&
                       (sActivityCode.ToUpper() == "ACMEERPBACKUPOLD"))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }
                    //else if ((headOfficeCode.ToUpper() == "SDBINM") &&
                    //   (sActivityCode.ToUpper() == "BUDGET"))
                    //{
                    //    mnuItem.Parent.ChildItems.Remove(mnuItem);
                    //}

                    if (!headOfficeCode.Equals("SDBINM", StringComparison.OrdinalIgnoreCase) &&
                     (sActivityCode.Equals("BudgetINM", StringComparison.OrdinalIgnoreCase)))
                    {
                        mnuItem.Parent.ChildItems.Remove(mnuItem);
                    }
                }
            }
            //-----------------------------------------------------------------------------------------------------------
        }

        protected void mnuTop_DataBound(object sender, EventArgs e)
        {
            // Get each menu's Module and Activity Code Check its rights for logged user 
            // Remove menus based on the rights (First check user rights, it not exists
            // check its user type)
            //Get all the menus and check user rights for logged user and its user type
            if (this.ActivePage.LoginUser.IsPortalUser)
            {
                if ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin)
                {
                    ValidateAndConstructMenu();
                }
            }
            else
            {
                if (((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin) &&
                ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.BranchAdmin))
                {
                    ValidateAndConstructMenu();
                }
            }
        }

        private void ValidateAndConstructMenu()
        {
            //Get Root's(Home) node's child nodes and check its user rights.
            //Remove menus based on userrights
            for (int i = 0; i < mnuTop.Items.Count; i++)
            {
                MenuItem[] menuItemArray = GetMenusAsArray(mnuTop.Items[i]);

                foreach (MenuItem mnuParent in menuItemArray)
                {
                    CheckUserRights(mnuParent);
                }
            }
        }

        /// <summary>
        /// This is recurisive function, Check user rihgts for each parent and its child menus for logged user
        /// Menu's will be removed based on the user rights
        /// </summary>
        /// <param name="mnuItem">MenuItem:MenuItem to be checked userirhts</param>
        private void CheckUserRights(MenuItem mnuItem)
        {
            //It is recurisive function, if current node has child, again it will check all the childs
            // If looged user doesn't has rights, remove menu
            // If parent mode doesn;t has child, remove parent too
            //check Each parent's child menus and if not allowed for looged user, remove from the menu list
            int nChildCount = mnuItem.ChildItems.Count;
            //If current node
            if (nChildCount > 0)
            {
                MenuItem[] mnuItemArray = GetMenusAsArray(mnuItem);
                foreach (MenuItem mnu in mnuItemArray)
                {
                    CheckUserRights(mnu);
                }

                if (mnuItem.ChildItems.Count == 0 && !IsAllowedMenu(mnuItem.Value))
                {
                    mnuItem.Parent.ChildItems.Remove(mnuItem);
                }
            }
            else
            {
                if (!IsAllowedMenu(mnuItem.Value))
                {
                    mnuItem.Parent.ChildItems.Remove(mnuItem);
                }
            }
        }

        /// <summary>
        /// To Check user rights for given Modulecode and activitycode
        /// </summary>
        /// <param name="sModuleActivity">String:Module and Activitycode, It shoud be concatenate by @</param>
        /// <returns>Boolean:True:Allowed, Fase:Not Allowed</returns>
        private bool IsAllowedMenu(string sModuleActivity)
        {
            bool bRtn = false;
            //Check rights for Menus modue and activity code for logged user
            if (this.ActivePage.LoginUser.IsPortalUser)
            {
                if ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin)
                {
                    bRtn = IsValidUserAndAllowedMenu(sModuleActivity, bRtn);
                }
                else
                {
                    bRtn = true;
                }
            }
            else
            {
                if (((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin) &&
                ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.BranchAdmin))
                {
                    bRtn = IsValidUserAndAllowedMenu(sModuleActivity, bRtn);
                }
                else
                {
                    bRtn = true;
                }
            }
            return bRtn;
        }

        private bool IsValidUserAndAllowedMenu(string sModuleActivity, bool bRtn)
        {
            string sModuleCode = "";
            string sActivityCode = "";
            string[] sValue = sModuleActivity.Split(Delimiter.AtCap.ToCharArray());
            if (sValue.GetLength(0) > 0)
            {
                sModuleCode = sValue.GetValue(0).ToString();   //Module Code
                sActivityCode = sValue.GetValue(1).ToString(); //Activity Code

                using (RightSystem RightsSys = new RightSystem())
                {
                    if (sActivityCode != "")
                    {
                        dvUserRights.RowFilter = "MODULE_CODE" + "='" + sModuleCode + "'" +
                                       " AND " + "ACTIVITY_CODE" + "='" + sActivityCode + "'" +
                                       " AND " + "TYPE" + "=" + (int)UserRightsType.Page;
                        if (dvUserRights.Count > 0)
                        {
                            bool UserTypeRights = Convert.ToBoolean(dvUserRights[0]["USER_RIGHTS"].ToString());

                            //Check User Rights, It is not exists, check in UserType Rights
                            if (UserTypeRights)
                            {
                                bRtn = UserTypeRights;
                            }

                            if (!bRtn)
                            {
                                bRtn = isTaskAllowed(sModuleCode, sActivityCode);
                            }
                        }
                    }
                    else
                    {
                        bRtn = true;
                    }
                }
            }
            return bRtn;
        }

        /// <summary>
        /// This function is used to check task rights for given module and activity code
        /// It will check in the Page Refference Task column for given activity
        /// </summary>
        /// <param name="sModuleCode">string:ModuleCode</param>
        /// <param name="sActivityCode">string:ActivityCode</param>
        /// <returns>Boolean:True:Task is exists and rights given
        /// Fase: Task is not exists
        /// </returns>
        private bool isTaskAllowed(string sModuleCode, string sActivityCode)
        {
            bool isTaskExists = false;
            if (this.ActivePage.LoginUser.IsPortalUser)
            {
                if ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin)
                {

                    isTaskExists = IsValidUserAndTaskallowed(sModuleCode, sActivityCode, isTaskExists);
                }
                else
                {
                    isTaskExists = true;
                }
            }
            else
            {
                if (((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.Admin) &&
                   ((UserRole)this.ActivePage.LoginUser.LoginUserRoleId != UserRole.BranchAdmin))
                {
                    isTaskExists = IsValidUserAndTaskallowed(sModuleCode, sActivityCode, isTaskExists);
                }
                else
                {
                    isTaskExists = true;
                }
            }
            return isTaskExists;
        }

        private bool IsValidUserAndTaskallowed(string sModuleCode, string sActivityCode, bool isTaskExists)
        {
            if (sModuleCode != "" && sActivityCode != "")
            {
                using (RightSystem rightsSys = new RightSystem())
                {
                    dvUserRights.RowFilter = "MODULE_CODE" + "='" + sModuleCode + "'" +
                        " AND " + "TYPE" + "=" + (int)UserRightsType.Task;

                    foreach (DataRowView drv in dvUserRights)
                    {
                        bool UserTypeRights = Convert.ToBoolean(drv["USER_RIGHTS"].ToString());

                        //Check User Rights, It is not exists, check in UserType Rights
                        if (UserTypeRights)
                        {
                            isTaskExists = UserTypeRights;
                        }

                        if (isTaskExists)
                        {
                            break;
                        }
                    }
                }
            }
            return isTaskExists;
        }

        /// <summary>
        /// Get given Menu item's child iteams as menuitem Array
        /// </summary>
        /// <param name="mnuItem">MenuItem</param>
        /// <returns>MenuItem[]:Return MenuItems as Array</returns>
        private MenuItem[] GetMenusAsArray(MenuItem mnuItem)
        {
            MenuItem[] menuItemArray = new MenuItem[mnuItem.ChildItems.Count];
            mnuItem.ChildItems.CopyTo(menuItemArray, 0);
            return menuItemArray;
        }

        /// <summary>
        /// Get All Rights for given user and its type
        /// </summary>
        private void LoadUserRights()
        {
            //Get User Rights for logged user and its User Type
            using (RightSystem rightsSys = new RightSystem())
            {
                ResultArgs resultArgs = null;
                rightsSys.RoleId = this.ActivePage.LoginUser.LoginUserRoleId;
                //rightsSys.Accessibility = (int)Accessibility.Both;
                if (this.ActivePage.LoginUser.IsHeadOfficeUser)
                {
                    rightsSys.Accessibility = (int)Accessibility.HeadOffice;
                }
                else if (this.ActivePage.LoginUser.IsBranchOfficeUser)
                {
                    rightsSys.Accessibility = (int)Accessibility.BranchOffice;
                }
                resultArgs = rightsSys.GetUserRightsForMenu(this.ActivePage.LoginUser.HeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                if (resultArgs != null && resultArgs.Success)
                {
                    dvUserRights = resultArgs.DataSource.TableView;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }
            }
        }
    }
}
