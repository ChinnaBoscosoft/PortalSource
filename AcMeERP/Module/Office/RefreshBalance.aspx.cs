/*****************************************************************************************************
 * Created by       : Arockia Raj
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page helps the admin to refresh branch office balance and vouchers by date wise
 *****************************************************************************************************/
using System;
using Bosco.Utility;
using Bosco.Model.UIModel;
using AcMEDSync.Model;

namespace AcMeERP.Module.Office
{
    public partial class RefreshBalance : Base.UIBase
    {
        #region Properties
        private static object objLock = new object();
        ResultArgs resultArgs = null;
        private static string CODE = "CODE";
        CommonMember UtilityMember = new CommonMember();

        private int branchid = 0;
        private int BranchId
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["branchid"].ToString());
            }
            set
            {
                branchid = value;
                ViewState["branchid"] = branchid;
            }
        }

        private int projectid = 0;
        private int ProjectId
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["projectid"].ToString());
            }
            set
            {
                projectid = value;
                ViewState["projectid"] = projectid;
            }
        }

        private string ProjectIds
        {
            get
            {
                return ViewState["ProjectIds"].ToString();
            }
            set
            {
                ViewState["ProjectIds"] = value;
            }
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             //   SetPageTitle();
                hlkClose.PostBackUrl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
                dteFrom.Date =Session["YEAR_FROM"]!=null?this.UtilityMember.DateSet.ToDate(Session["YEAR_FROM"].ToString(),false): DateTime.Now.Date;
                LoadBranch();
                LoadProjects();
                this.ShowLoadWaitPopUp(btnRefresh);
            }
        }
        protected void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
        }
        protected void cmbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchId = this.UtilityMember.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
            LoadProjects();
        }
        protected void btnRefreshBalance_OnClick(object sender, EventArgs e)
        {
            lock (objLock)
            {
                try
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        balanceSystem.ProjectId = ProjectId;
                        balanceSystem.VoucherDate = dteFrom.Date.ToShortDateString();
                        resultArgs = balanceSystem.UpdateBulkTransBalance(BranchId,true);
                        if (resultArgs.Success)
                        {
                            this.Message = "Balance Updated Successfully";
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }

                    }

                }
                catch (Exception ex)
                {
                    this.Message = ex.Message;
                    
                }
            }
        }

        #endregion

        #region Methods

        private void SetPageTitle()
        {
            this.PageTitle = "Balance Refresh";
        }

        private void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    BranchId = 0;
                    resultArgs = BranchOfficeSystem.FetchBranch();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranches, resultArgs.DataSource.Table, CODE, this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                        if (cmbBranches.Items.Count > 0)
                        {
                            string branchid = string.Empty;
                            if (base.LoginUser.IsHeadOfficeUser || base.LoginUser.IsHeadOfficeAdminUser)
                            {
                                Session[base.DefaultBranchId] =cmbBranches.SelectedItem.Value;
                                BranchId =this.UtilityMember.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
                            }
                            if (base.LoginUser.IsBranchOfficeUser || base.LoginUser.IsBranchOfficeAdminUser)
                            {
                                if (!string.IsNullOrEmpty(base.LoginUser.LoginUserBranchOfficeName))
                                {
                                    cmbBranches.Text = base.LoginUser.LoginUserBranchOfficeName;
                                    BranchId = this.Member.NumberSet.ToInteger(cmbBranches.SelectedItem.Value.ToString());
                                }
                                cmbBranches.Enabled = false;
                            }
                        }
                        else
                        {
                            BranchId = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }

        private void LoadProjects()
        {
            using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
            {
                try
                {
                    resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, true);
                        ProjectId = 0;
                        string projectid = string.Empty;
                        for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                        {
                            projectid += resultArgs.DataSource.Table.Rows[i]["PROJECT_ID"].ToString() + ",";
                        }
                        ProjectIds = projectid.TrimEnd(',');
                    }
                    else
                    {
                        this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, true);
                        ProjectId = 0;
                        ProjectIds = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    this.Message = ex.ToString();
                }
            }
        }
        #endregion




    }
}