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
 * Purpose          :This page helps the head office admin user to map the projects to the branch office & by location wise also projects are mapped if the branch office 
 *                  has got multilocation concept
 *****************************************************************************************************/
using System;
using System.Collections.Generic;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

namespace AcMeERP.Module.Office
{
    public partial class ProjectMapping : Base.UIBase
    {
        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        private static object objLock = new object();
        private const string LOCATION_ID = "LOCATION_ID";
        private const string colLocation = "colLocation";
        private const string cmbLocation = "cmbLocation";
        private const string LocationName = "Location";
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
        private int BranchId
        {
            get
            {
                return string.IsNullOrEmpty((ViewState["BranchId"].ToString())) ? 0 :
                    this.Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
            }
            set
            {
                ViewState["BranchId"] = value;
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
                LoadBranch();
                LoadProjects();
                btnMapLocation.Visible = IsBranchHasLocations() ? true : false;
               // BindLocations();
                this.ShowLoadWaitPopUp(btnSaveMapping);
                this.ShowLoadWaitPopUp(bntSaveOnTop);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        //protected void cmbEditData_Init(object sender, EventArgs e)
        //{
        //    using (BranchLocationSystem locationSystem = new BranchLocationSystem())
        //    {
        //        locationSystem.BranchId = BranchId;
        //        resultArgs = locationSystem.FetchBranchLocationByBranchId(DataBaseType.HeadOffice);
        //        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 1)
        //        {
        //            gvProject.Columns[colLocation].Visible = true;
        //            ASPxComboBox editor = sender as ASPxComboBox;
        //            GridViewDataItemTemplateContainer container = editor.NamingContainer as GridViewDataItemTemplateContainer;
        //            editor.ValueType = typeof(System.Int32);
        //            editor.DataSource = resultArgs.DataSource.Table;
        //            editor.ValueField = LOCATION_ID;
        //            editor.TextField = LocationName;
        //            editor.DataBind();
        //            editor.DataBindItems();
        //            editor.SelectedIndex = 0;
        //            if (gvProject != null && gvProject.VisibleRowCount>0)
        //            {
        //                BindLocationSelected();
        //                //Mapped Project indicates, the project has transactions
        //                string value1 = gvProject.GetRowValues(container.VisibleIndex, "MAPPED_PROJECT").ToString();
        //                if (!string.IsNullOrEmpty(value1))
        //                {
        //                    //Disable the location combo if the project has transactions
        //                    editor.Enabled = false;
        //                }
        //            }
        //        }
        //    }
        //}
        protected void gvProject_Load(Object sender, EventArgs e)
        {
            try
            {
                //BindLocations();
                //if (IsPostBack)
                //{
                //    UpdateViewStateProjects();
                //}
                gvProject.DataSource = Projects;
                gvProject.DataBind();
              //  BindLocationSelected();
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
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
           // ShowLocation(true);
            BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
            btnMapLocation.Visible = IsBranchHasLocations() ? true : false;
            LoadProjects();
           // BindLocations();
        }
        protected void btnMapLocation_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/module/office/ProjectLocationMapping.aspx?BranchId=" + BranchId);
        }
        protected void gvProject_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            try
            {
                //To disable checkbox for the projects which have vouchers
                if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                {
                    string value1 = Convert.ToString(gvProject.GetRowValues(e.VisibleIndex, "MAPPED_PROJECT"));
                    if (!string.IsNullOrEmpty(value1))
                    {
                        e.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
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
                    List<object> lLocationId = new List<object>();
                    lProjectId = gvProject.GetSelectedFieldValues(this.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                    lLocationId = gvProject.GetSelectedFieldValues(this.AppSchema.BranchLocation.LOCATION_IDColumn.ColumnName);
                    using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                    {
                        resultArgs = BranchOffice.MapBranch(Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString()), lProjectId, lLocationId);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.ProjectMapping.ProjectMappingSaveConformation;
                            LoadProjects();
                            BindLocationSelected();
                            
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
                    resultArgs = BranchOffice.FetchProjectsbyBranch(BranchId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView ds = new DataView(resultArgs.DataSource.Table);
                        ds.Sort = "SELECT DESC";
                        Projects = ds.ToTable();
                        gvProject.DataSource = Projects;
                        gvProject.DataBind();
                        CheckSelectedProjects(Projects);
                    }
                    else
                    {
                        Projects = resultArgs.DataSource.Table;
                        gvProject.DataBind();
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

        public void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranch(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, "CODE", this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                        if (this.Member.NumberSet.ToInteger(Session[base.DefaultBranchId].ToString()) != 0)
                        {
                            cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                        }
                        BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    }
                    else
                    {
                        BranchId = 0;
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
                gvProject.Selection.UnselectAll();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (this.Member.NumberSet.ToInteger(dt.Rows[i]["SELECT"].ToString()) == 1)
                    {
                        gvProject.Selection.SelectRowByKey(dt.Rows[i]["PROJECT_ID"]);
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
                lProjectId = gvProject.GetSelectedFieldValues(this.AppSchema.Project.PROJECT_IDColumn.ColumnName);
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
                lProjectId = gvProject.GetSelectedFieldValues(this.AppSchema.Project.PROJECT_IDColumn.ColumnName);
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

        private void BindLocations()
        {
            using (BranchLocationSystem locationSystem = new BranchLocationSystem())
            {
                locationSystem.BranchId = BranchId;
                resultArgs = locationSystem.FetchBranchLocationByBranchId(DataBaseType.HeadOffice);
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 1)
                {
                    ShowLocation(true);
                }
                else
                {
                    ShowLocation(false);
                }

            }
        }

        private bool IsBranchHasLocations()
        {
            bool _hasLocation = false;
            using (BranchLocationSystem locationSystem = new BranchLocationSystem())
            {
                locationSystem.BranchId = BranchId;
                resultArgs = locationSystem.FetchBranchLocationByBranchId(DataBaseType.HeadOffice);
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 1)
                {
                    _hasLocation = true;
                }
            }
            return _hasLocation;
        }

        private void BindLocationSelected()
        {
            BindLocations();
            if (gvProject.Columns[colLocation].Visible)
            {
                for (int i = 0; i < gvProject.VisibleRowCount; i++)
                {
                    ASPxComboBox cmbLocation = ((ASPxComboBox)gvProject.FindRowCellTemplateControl(i, gvProject.Columns[colLocation] as GridViewDataColumn, "cmbLocation"));
                    if (cmbLocation != null)
                    {
                        if (Projects != null && Projects.Rows.Count > 0)
                        {
                            cmbLocation.SelectedIndex = this.Member.NumberSet.ToInteger(Projects.Rows[i][LOCATION_ID].ToString()) == 0 ? 0 : cmbLocation.Items.FindByValue(this.Member.NumberSet.ToInteger(Projects.Rows[i][LOCATION_ID].ToString())).Index;
                        }
                    }
                }
            }
        }

        private void ShowLocation(bool _flag)
        {
            gvProject.Columns[colLocation].Visible = _flag;
        }

        private void UpdateViewStateProjects()
        {
            if (gvProject.Columns[colLocation].Visible)
            {
                if (gvProject.VisibleRowCount > 0)
                {
                    for (int i = 0; i < gvProject.VisibleRowCount; i++)
                    {
                        ASPxComboBox cmbLocation = ((ASPxComboBox)gvProject.FindRowCellTemplateControl(i, gvProject.Columns["colLocation"] as GridViewDataColumn, "cmbLocation"));
                        if (cmbLocation != null)
                        {
                            int locationId = this.Member.NumberSet.ToInteger(cmbLocation.Value.ToString());
                            if (Projects != null && Projects.Rows.Count > 0)
                            {
                                Projects.Rows[i][LOCATION_ID] = locationId;
                                Projects.AcceptChanges();
                            }
                        }
                    }
                }
            }
        }
        #endregion

    }
}