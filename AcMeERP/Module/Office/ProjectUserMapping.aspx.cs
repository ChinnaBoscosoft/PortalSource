/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          : This page helps the branch office admin to map the projects to the branch office user to provide access to the particular project
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Office
{
    public partial class ProjectUserMapping : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        private static object objLock = new object();

        #endregion

        #region Properties

        private DataTable Projects
        {
            get
            {
                return (DataTable)ViewState["Projects"];
            }
            set
            {
                ViewState["Projects"] = value;
            }
        }
        private int UserId
        {
            get
            {
                return string.IsNullOrEmpty((ViewState["UserId"].ToString())) ? 0 :
                    this.Member.NumberSet.ToInteger(ViewState["UserId"].ToString());
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }

        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                this.PageTitle = MessageCatalog.Message.ProjectMapping.ProjectMappingPageTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.MapProjectToBranch, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadUsers();
                LoadProjects();
                this.ShowLoadWaitPopUp(btnSaveMapping);
                this.ShowLoadWaitPopUp(bntSaveOnTop);

                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvUserProject_Load(Object sender, EventArgs e)
        {
            try
            {
                gvUserProject.DataSource = Projects;
                gvUserProject.DataBind();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        protected void btnSaveMapping_Click(object sender, EventArgs e)
        {
            MapProjects();
        }

        protected void bntSaveOnTop_Click(object sender, EventArgs e)
        {
            MapProjects();
        }
        protected void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserId = this.Member.NumberSet.ToInteger(cmbUser.SelectedItem.Value.ToString());
            LoadProjects();
        }
        #endregion

        #region Methods

        private void MapProjects()
        {
            try
            {
                lock (objLock)
                {
                    List<object> lProjectId = new List<object>();
                    lProjectId = gvUserProject.GetSelectedFieldValues(this.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                    using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                    {
                        resultArgs = BranchOffice.MapProjectByUser(Member.NumberSet.ToInteger(cmbUser.SelectedItem.Value.ToString()), lProjectId);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.ProjectMapping.ProjectMappingSaveConformation;
                            LoadProjects();
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.ProjectMapping.ProjectMappingSavingFailedConformation;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        public void LoadProjects()
        {
            try
            {
                using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                {
                    if (base.LoginUser.IsHeadOfficeAdminUser)
                    {
                        resultArgs = BranchOffice.FetchProjectsByHeadOfficeUsers(UserId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    }
                    else if(base.LoginUser.IsBranchOfficeAdminUser)
                    {
                        resultArgs = BranchOffice.FetchProjectsByBranchOfficeUsers(UserId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    }
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView ds = new DataView(resultArgs.DataSource.Table);
                        ds.Sort = "SELECT DESC";
                        Projects = ds.ToTable();
                        gvUserProject.DataSource = Projects;
                        gvUserProject.DataBind();
                        CheckSelectedProjects(Projects);
                    }
                    else
                    {
                        Projects = resultArgs.DataSource.Table;
                        gvUserProject.DataBind();
                        btnSaveMapping.Visible = false;
                        bntSaveOnTop.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        public void LoadUsers()
        {
            try
            {

                using (UserSystem userSystem = new UserSystem())
                {
                    ResultArgs resultArgs = userSystem.FetchUsersForProjectMapping(string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                   if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbUser, resultArgs.DataSource.Table, this.AppSchema.User.USER_NAMEColumn.ColumnName, this.AppSchema.User.USER_IDColumn.ColumnName,false);
                        cmbUser.SelectedIndex = 0;
                        UserId = this.Member.NumberSet.ToInteger(cmbUser.SelectedItem.Value.ToString());
                    }
                    else
                    {
                        UserId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
        }

        private void CheckSelectedProjects(DataTable dt)
        {
            try
            {
                gvUserProject.Selection.UnselectAll();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (this.Member.NumberSet.ToInteger(dt.Rows[i]["SELECT"].ToString()) == 1)
                    {
                        gvUserProject.Selection.SelectRowByKey(dt.Rows[i]["PROJECT_ID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private DataTable GetSelectedProjects(DataTable dt)
        {
            try
            {
                List<object> lProjectId = new List<object>();
                lProjectId = gvUserProject.GetSelectedFieldValues(this.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                foreach (object ProjectId in lProjectId)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.Member.NumberSet.ToInteger(dt.Rows[i]["PROJECT_ID"].ToString()) == this.Member.NumberSet.ToInteger(ProjectId.ToString()))
                            dt.Rows[i]["SELECT"] = 1;
                        else
                            dt.Rows[i]["SELECT"] = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return dt;
        }

        private void UnSelectAll()
        {
            foreach (DataRow dr in Projects.Rows)
            {
                dr["SELECT"] = 0;
            }
        }

        private void UpdateProject()
        {
            DataTable dt = Projects;
            try
            {
                List<object> lProjectId = new List<object>();
                lProjectId = gvUserProject.GetSelectedFieldValues(this.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                UnSelectAll();
                if (lProjectId.Count != 0)
                {

                    foreach (object value in lProjectId)
                    {
                        foreach (object projectid in lProjectId)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString() == projectid.ToString())
                                {
                                    dr["SELECT"] = 1;
                                    break;
                                }
                            }
                        }
                    }
                    Projects = dt;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

        }
        
        #endregion
       
    }
}