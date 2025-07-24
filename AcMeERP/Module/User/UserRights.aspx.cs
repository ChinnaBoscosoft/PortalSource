using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.User
{
    public partial class UserRights : Base.UIBase
    {
        private const string ROOT_NODE_TEXT = "User Rights";
        private const string ROOT_NODE_VALUE = "0";
        private static object objLock = new object();
        private const string ModuleCode = "ModuleCode";
        private const string ActivityCode = "ActivityCode";
        private const string Allow= "Allow";
        private DataTable UserRightsSource
        {
            get
            {
                if (ViewState["UserRightsSource"] == null)
                    return null;
                else
                    return (DataTable)ViewState["UserRightsSource"];
            }
            set { ViewState["UserRightsSource"] = value; }
        }
        DataView dvUserRights;
        public UserRightsMode UserRightsMode
        {
            get
            {
                if (ViewState["PageMode"] != null)
                    return (UserRightsMode)ViewState["PageMode"];
                else
                    return UserRightsMode.UserGroup;
            }
            set { ViewState["PageMode"] = value; }
        }
        public Int32 Id
        {
            get
            {
                if (ViewState["Id"] != null)
                    return this.Member.NumberSet.ToInteger(ViewState["Id"].ToString());
                else
                    return 0;
            }
            set { ViewState["Id"] = value; }
        }
        public Int32 ItemscountByModule
        {
            get
            {
                if (ViewState["ItemscountByModule"] != null)
                    return this.Member.NumberSet.ToInteger(ViewState["ItemscountByModule"].ToString());
                else
                    return 0;
            }
            set { ViewState["ItemscountByModule"] = value; }
        }
        public Int32 SelectedItemsByModule
        {
            get
            {
                if (ViewState["SelItemsByModule"] != null)
                    return this.Member.NumberSet.ToInteger(ViewState["SelItemsByModule"].ToString());

                else
                    return 0;
            }
            set { ViewState["SelItemsByModule"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Check user rights
                this.CheckUserRights(RightsModule.User, RightsActivity.UserRights, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.PageTitle = MessageCatalog.Message.User.UserRightsPageTitle;
                lblUserRights.Text = "User Rights";
                FillTreeView();
                CreateUserRightsTable();
                this.ShowLoadWaitPopUp(btnSaveRights);
            }
        }
        protected void tvUsers_SelectedNodeChanged(object sender, EventArgs e)
        {
            lock(objLock)
            {
                LoadUserRights();
            }
        }
        protected void dlUserRights_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //Bind activities list when moudles are getting loaded
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblModCode = e.Item.FindControl("lblModuleCode") as Label;
                CheckBox chkMdu = e.Item.FindControl("chkModule") as CheckBox;
                DataList dlActivitiesList = e.Item.FindControl("dlActivities") as DataList;
                string sModuleCode = lblModCode.Text;
                if (dvUserRights != null && dvUserRights.Count > 0)
                {
                    dvUserRights.RowFilter = "";
                    dvUserRights.RowFilter = "MODULE_CODE='" + sModuleCode + "'";

                    //Assign activies cout and seelected count
                    ItemscountByModule = dvUserRights.Count;
                    SelectedItemsByModule = 0;

                    dlActivitiesList.DataSource = dvUserRights;
                    dlActivitiesList.DataBind();

                }
            }
        }
        protected void dlActivities_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //when activity checkbox is getting loaded, 
            //select module checkbox, if all the activity check box selected
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkAct = e.Item.FindControl("chkActivity") as CheckBox;
                //count selected activity check boxes
                if (chkAct.Checked)
                {
                    SelectedItemsByModule++;
                }

                DataList dl = chkAct.NamingContainer.Parent as DataList;
                CheckBox chkMdu = dl.NamingContainer.FindControl("chkModule") as CheckBox;
                chkMdu.Checked = false;
                //select module check box if no of activites and selected actvities are equal
                if (ItemscountByModule == SelectedItemsByModule)
                {
                    chkMdu.Checked = true;
                }
            }
        }
        protected void chkActivity_CheckedChanged(object sender, EventArgs e)
        {
            lock (objLock)
            {
                //Update selected activity for user or type
                if (sender != null)
                {
                    CheckBox chkSelected = sender as CheckBox;
                    DataList dl = chkSelected.NamingContainer.Parent as DataList;
                    CheckBox chkMdu = dl.NamingContainer.FindControl("chkModule") as CheckBox;
                    Label lblModCode = chkSelected.NamingContainer.FindControl("lblModuleCode1") as Label;
                    Label lblActivityCode = chkSelected.NamingContainer.FindControl("lblActivityCode") as Label;

                    string sModuleCode = lblModCode.Text;
                    string sActivityCode = lblActivityCode.Text;
                    bool IsAllowed = chkSelected.Checked;
                    DataRow drRow = UserRightsSource.NewRow();
                    drRow[ModuleCode]=sModuleCode;
                    drRow[ActivityCode] = sActivityCode;
                    drRow[Allow] = IsAllowed;
                    UserRightsSource.Rows.Add(drRow);
                    UserRightsSource.AcceptChanges();
                   // UpdateRights(sModuleCode, sActivityCode, IsAllowed);

                    //If all activites selected, select its module check box
                    chkMdu.Checked = IsSelectedAllActivities(dl);
                }
            }
        }
        protected void chkModule_CheckedChanged(object sender, EventArgs e)
        {
            lock (objLock)
            {
                //Select all the activites for selected module and Update into DB for selected User or Type
                if (sender != null)
                {
                    CheckBox chkModuleSelected = sender as CheckBox;
                    DataList dlActivitiesList = chkModuleSelected.NamingContainer.FindControl("dlActivities") as DataList;
                    //Get all activites and mark as selected and update into DB for selected user or type
                    foreach (DataListItem dlItem in dlActivitiesList.Items)
                    {
                        CheckBox chkSelActivity = dlItem.FindControl("chkActivity") as CheckBox;
                        Label lblModCode = chkSelActivity.NamingContainer.FindControl("lblModuleCode1") as Label;
                        Label lblActivityCode = chkSelActivity.NamingContainer.FindControl("lblActivityCode") as Label;
                        string sModuleCode = lblModCode.Text;
                        string sActivityCode = lblActivityCode.Text;
                        bool IsAllowed = chkModuleSelected.Checked;
                        chkSelActivity.Checked = chkModuleSelected.Checked;

                        DataRow drRow = UserRightsSource.NewRow();
                        drRow[ModuleCode] = sModuleCode;
                        drRow[ActivityCode] = sActivityCode;
                        drRow[Allow] = IsAllowed;
                        UserRightsSource.Rows.Add(drRow);
                        UserRightsSource.AcceptChanges();
                        //Update rights for selected user or type
                       // UpdateRights(sModuleCode, sActivityCode, chkSelActivity.Checked);
                    }
                }
            }
        }

        /// <summary>
        /// Load Users and ites type in the Treeview
        /// </summary>
        private void FillTreeView()
        {
            string USER_ROLE_ID = this.AppSchema.UserRole.USERROLE_IDColumn.ColumnName;
            string USER_ROLE = this.AppSchema.UserRole.USERROLEColumn.ColumnName;

            using (RightSystem rightsSystem = new RightSystem())
            {
                ResultArgs resultArgs = null;
                if (base.LoginUser.IsHeadOfficeUser)
                {
                    rightsSystem.Accessibility = (int)Accessibility.HeadOffice;
                }
                else if (base.LoginUser.IsBranchOfficeUser)
                {
                    rightsSystem.Accessibility = (int)Accessibility.BranchOffice;
                }
                resultArgs = rightsSystem.GetUsersType(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                if (resultArgs.Success)
                {
                    DataView dvUsers = resultArgs.DataSource.TableView;
                    tvUsers.Nodes.Add(new TreeNode(ROOT_NODE_TEXT, ROOT_NODE_VALUE));
                    TreeNode root = tvUsers.Nodes[0];
                    TreeNode tvGrpNode = null; ;
                    string sGrp = "";

                    foreach (DataRowView dr in dvUsers)
                    {
                        if (sGrp != dr[USER_ROLE].ToString()) //Add user type as parent node
                        {
                            tvGrpNode = new TreeNode(dr[USER_ROLE].ToString(), dr[USER_ROLE_ID].ToString());
                            root.ChildNodes.Add(tvGrpNode);
                        }
                        sGrp = dr[USER_ROLE].ToString();
                    }

                    //Select first item in treeview list, if count is >0
                    if (root.ChildNodes.Count > 0)
                    {
                        tvUsers.Nodes[0].ChildNodes[0].Selected = true;
                        tvUsers.Nodes[0].ChildNodes[0].Expand();
                        LoadUserRights();
                    }

                }
                else
                {
                    this.Message = resultArgs.Message;
                }
            }
        }

        /// <summary>
        /// Load modules list in the datalist, and get userrights for selected type or user
        /// Get the All Activity rights for selected user or type. this rights will be used
        /// data list item data bound event and load module's activites
        /// </summary>
        private void LoadUserRights()
        {
            if (tvUsers.SelectedNode.Value != ROOT_NODE_VALUE)
            {
                if (tvUsers.SelectedNode.Parent.Value == ROOT_NODE_VALUE)
                {
                    UserRightsMode = UserRightsMode.UserGroup;
                    lblSelectedUser.Text = "User Role : " + tvUsers.SelectedNode.Text;
                    lblComment.Text = MessageCatalog.Message.User.UserRightsMap;
                }
                else
                {
                    UserRightsMode = UserRightsMode.User;
                    lblSelectedUser.Text = "User : " + tvUsers.SelectedNode.Text;
                    lblComment.Text = MessageCatalog.Message.User.UserRightsMap;
                }
                Id = this.Member.NumberSet.ToInteger(tvUsers.SelectedNode.Value);

                using (RightSystem rightsSystem = new RightSystem())
                {
                    ResultArgs resultArgs = null;
                    //Get all Activity rights for selected user or selected type
                    rightsSystem.RoleId = Id;
                    if (base.LoginUser.IsHeadOfficeUser)
                    {
                        rightsSystem.Accessibility = (int)Accessibility.HeadOffice;
                    }
                    else if (base.LoginUser.IsBranchOfficeUser)
                    {
                        rightsSystem.Accessibility = (int)Accessibility.BranchOffice;
                    }
                    resultArgs = rightsSystem.GetAllUserRights(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                        //Get unique modules list and bind in the datalist
                        dvUserRights = resultArgs.DataSource.Table.DefaultView;
                        resultArgs = rightsSystem.GetModulesList(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            dlUserRights.DataSource = resultArgs.DataSource.TableView;
                            dlUserRights.DataBind();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update Rights for selected user or type
        /// </summary>
        /// <param name="sModuleCode">String:ModuleCode of the activity</param>
        /// <param name="sActivityCode">String:ActivityCode of the activity</param>
        /// <param name="IsAllowed">Boolean: rights allowed or denied</param>
        private void UpdateRights(string sModuleCode, string sActivityCode, bool IsAllowed)
        {
            lock (objLock)
            {
                if (sModuleCode != "" && sActivityCode != "")
                {
                    using (RightSystem rightsSystem = new RightSystem())
                    {
                        ResultArgs resultArgs = null;
                        rightsSystem.RoleId = Id;
                        rightsSystem.ModuleCode = sModuleCode;
                        rightsSystem.ActivityCode = sActivityCode;
                        rightsSystem.Allow = IsAllowed;

                        resultArgs = rightsSystem.UpdateUserRights(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                        if (!resultArgs.Success)
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function is used to check whether selected all activites for a module.
        /// This will help to select its parent Module check box in the top grid
        /// </summary>
        /// <param name="dlActivities">DataList:Activities List for a module</param>
        /// <returns>Boolean:True:All Activites are selected, False: not all activites selected</returns>
        /// <remarks></remarks>
        private bool IsSelectedAllActivities(DataList dlActivities)
        {
            bool bRtn = true;
            foreach (DataListItem dlItem in dlActivities.Items)
            {
                CheckBox chkSelActivity = dlItem.FindControl("chkActivity") as CheckBox;
                if (!chkSelActivity.Checked)
                {
                    bRtn = false;
                    break;
                }
            }
            return bRtn;
        }
        protected void btnSaveRights_Click(object sender, EventArgs e)
        {
            lock (objLock)
            {
                try
                {
                    if (UserRightsSource != null && UserRightsSource.Rows.Count > 0)
                    {
                        foreach (DataRow  drrow in UserRightsSource.Rows)
                        {
                            UpdateRights(drrow[ModuleCode].ToString(), drrow[ActivityCode].ToString(),Convert.ToBoolean(drrow[Allow].ToString()));
                            
                        }
                        this.Message = MessageCatalog.Message.UserRightsSaved;
                    }
                }
                catch (Exception ex)
                {
                    this.Message = ex.Message;
                }
            }
        }
        private void CreateUserRightsTable()
        {
            UserRightsSource = new DataTable();
            UserRightsSource.Columns.Add(ModuleCode, typeof(string));
            UserRightsSource.Columns.Add(ActivityCode, typeof(string));
            UserRightsSource.Columns.Add(Allow, typeof(Boolean));
            UserRightsSource.AcceptChanges();
        }

    }
}