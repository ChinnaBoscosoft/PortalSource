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
    public partial class BranchUserMapping : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        private static object objLock = new object();

        #endregion

        #region Properties

        private DataTable Branch
        {
            get
            {
                return (DataTable)ViewState["Branch"];
            }
            set
            {
                ViewState["Branch"] = value;
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
                this.PageTitle = MessageCatalog.Message.ProjectMapping.BranchMappingPageTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.MapProjectToBranch, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadUsers();
                LoadBranches();
                this.ShowLoadWaitPopUp(btnSaveMapping);
                this.ShowLoadWaitPopUp(bntSaveOnTop);

                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvUserBranch_Load(Object sender, EventArgs e)
        {
            try
            {
                gvUserBranch.DataSource = Branch;
                gvUserBranch.DataBind();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        protected void btnSaveMapping_Click(object sender, EventArgs e)
        {
            MapBranch();
        }

        protected void bntSaveOnTop_Click(object sender, EventArgs e)
        {
            MapBranch();
        }
        protected void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserId = this.Member.NumberSet.ToInteger(cmbUser.SelectedItem.Value.ToString());
            LoadBranches();
        }
        #endregion

        #region Methods

        private void MapBranch()
        {
            try
            {
                lock (objLock)
                {
                    List<object> lBranchId = new List<object>();
                    lBranchId = gvUserBranch.GetSelectedFieldValues(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName);
                    using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                    {
                        resultArgs = BranchOffice.MapBranchByUser(Member.NumberSet.ToInteger(cmbUser.SelectedItem.Value.ToString()), lBranchId);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.ProjectMapping.BranchMappingSaveConformation;
                            LoadBranches();
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.ProjectMapping.BranchMappingSavingFailedConformation;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        public void LoadBranches()
        {
            try
            {
                using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                {
                    if (base.LoginUser.IsHeadOfficeAdminUser)
                    {
                        resultArgs = BranchOffice.FetchBranchByHeadOfficeUsers(UserId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    }
                    else if (base.LoginUser.IsBranchOfficeAdminUser)
                    {
                        resultArgs = BranchOffice.FetchBranchByBranchOfficeUsers(UserId, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    }
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView ds = new DataView(resultArgs.DataSource.Table);
                        ds.Sort = "SELECT DESC";
                        Branch = ds.ToTable();
                        gvUserBranch.DataSource = Branch;
                        gvUserBranch.DataBind();
                        CheckSelectedBranches(Branch);
                    }
                    else
                    {
                        Branch = resultArgs.DataSource.Table;
                        gvUserBranch.DataBind();
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
                    ResultArgs resultArgs = userSystem.FetchUsersForBranchMapping(string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbUser, resultArgs.DataSource.Table, this.AppSchema.User.USER_NAMEColumn.ColumnName, this.AppSchema.User.USER_IDColumn.ColumnName, false);
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

        private void CheckSelectedBranches(DataTable dt)
        {
            try
            {
                gvUserBranch.Selection.UnselectAll();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (this.Member.NumberSet.ToInteger(dt.Rows[i]["SELECT"].ToString()) == 1)
                    {
                        gvUserBranch.Selection.SelectRowByKey(dt.Rows[i]["BRANCH_OFFICE_ID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private DataTable GetSelectedBranch(DataTable dt)
        {
            try
            {
                List<object> lBranchId = new List<object>();
                lBranchId = gvUserBranch.GetSelectedFieldValues(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName);
                foreach (object BranchId in lBranchId)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.Member.NumberSet.ToInteger(dt.Rows[i]["BRANCH_OFFICE_ID"].ToString()) == this.Member.NumberSet.ToInteger(BranchId.ToString()))
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
            foreach (DataRow dr in Branch.Rows)
            {
                dr["SELECT"] = 0;
            }
        }

        private void UpdateBranch()
        {
            DataTable dt = Branch;
            try
            {
                List<object> lBranchId = new List<object>();
                lBranchId = gvUserBranch.GetSelectedFieldValues(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName);
                UnSelectAll();
                if (lBranchId.Count != 0)
                {

                    foreach (object value in lBranchId)
                    {
                        foreach (object branchid in lBranchId)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr[this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName].ToString() == branchid.ToString())
                                {
                                    dr["SELECT"] = 1;
                                    break;
                                }
                            }
                        }
                    }
                    Branch = dt;
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