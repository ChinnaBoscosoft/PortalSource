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
 * Purpose          :This page helps head office admin/user or branch office admin/User to view the FD registers by Branch and Project wise
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using System.Data;

namespace AcMeERP.Module.Master
{
    public partial class FDRegisters : Base.UIBase
    {
        #region Declartion

        ResultArgs resultArgs = null;

        #endregion

        #region Properties
        private int branchid = 0;
        private int BranchId
        {
            set { branchid = value; }
            get { return branchid; }
        }
        private int projectid = 0;
        private int ProjectId
        {
            set { projectid = value; }
            get { return projectid; }
        }
        private DateTime BalanceDate
        {
            get { return this.Member.DateSet.ToDate(ViewState["BalanceDate"].ToString(), false); }
            set { ViewState["BalanceDate"] = value; }
        }
        private DataTable FdRegister
        {
            set { ViewState[AppSchema.FDRegisters.TableName] = value; }
            get { return (DataTable)ViewState[AppSchema.FDRegisters.TableName]; }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FdRegister = null;
                LoadBranches();
                LoadProjects();
                LoadDate();
                LoadFdRegister();
                ShowLoadWaitPopUp(btnLoad);
            }

        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadFdRegister();
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProjects();
            LoadDate();
        }

        protected void gvFDRegistersView_Load(object sender, EventArgs e)
        {
            LoadFdRegister();
        }
        #endregion

        #region Methods
        private void LoadFdRegister()
        {
            try
            {
                BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                using (FDAccountSystem FdAccount = new FDAccountSystem())
                {
                    FdAccount.BranchId = BranchId;
                    FdAccount.ProjectId = ProjectId;
                    FdAccount.DateFrom = dteDateFrom.Date;
                    FdAccount.DateTo = dteDateTo.Date;
                    resultArgs = FdAccount.FetchFDRegistersView();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        FdRegister = resultArgs.DataSource.Table;
                    else
                        FdRegister = resultArgs.DataSource.Table;
                    LoadGrid(FdRegister);

                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBranches()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranch(DataBaseType.HeadOffice);
                    this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName, AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                    if (base.LoginUser.IsBranchOfficeUser)
                    {
                        cmbBranch.Text = base.LoginUser.LoginUserBranchOfficeName;
                        cmbBranch.Enabled = false;
                    }
                    else if (base.LoginUser.IsHeadOfficeUser)
                    {
                        if (this.Member.NumberSet.ToInteger(Session[base.DefaultBranchId].ToString()) > 0)
                        {
                            cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadProjects()
        {
            try
            {
                using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
                {
                    BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
                    this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, AppSchema.Project.PROJECTColumn.ColumnName, AppSchema.Project.PROJECT_IDColumn.ColumnName, true);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadDate()
        {
            try
            {
                BranchId = cmbBranch.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                ProjectId = cmbProject.Items.Count == 0 ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                using (VoucherTransactionSystem VoucherMasterSystem = new VoucherTransactionSystem())
                {
                    BalanceDate = VoucherMasterSystem.FetchOPBalanceDate(BranchId, projectid);
                }
                using (FDAccountSystem FDAccount = new FDAccountSystem())
                {
                    FDAccount.BranchId = BranchId;
                    resultArgs = FDAccount.FetchDate();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DateTime dateTo = this.Member.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["MIN_DATE"].ToString(), false);
                        dteDateFrom.Date = dateTo.Date.AddDays(1 - (dateTo.Date.Day));
                        dteDateTo.Date = this.Member.DateSet.ToDate(dteDateFrom.Date.ToString(), false).AddMonths(1).AddSeconds(-1);
                    }
                    else
                    {
                        dteDateFrom.Date = this.Member.DateSet.ToDate(DateTime.Now.AddDays(1 - DateTime.Today.Day).ToString(), false);
                        dteDateTo.Date = this.Member.DateSet.ToDate(dteDateFrom.Date.ToString(), false).AddMonths(1).AddSeconds(-1);
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        private void LoadGrid(DataTable dtFdRegister)
        {
            try
            {
                gvFDRegistersView.DataSource = dtFdRegister;
                gvFDRegistersView.DataBind();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }
        #endregion
    }
}