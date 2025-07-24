using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace AcMeERP.Module.Master
{
    public partial class OpeningBalanceView : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;

        #endregion

        #region Properties
        private int project = 0;
        private int Project
        {
            get
            {
                project = cmbProject.SelectedItem.Value.ToString() == "All" ? 0 : Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                return project;
                // return ViewState["Projects"].ToString();
            }
            set
            {
                //ViewState["Projects"] = value;
                project = value;
            }
        }

        private int BranchId
        {
            get
            {
                return Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
            }
            set
            {
                ViewState["BranchId"] = value;
            }

        }

        private DataTable LedgerOPBalance
        {
            get
            {
                return (DataTable)ViewState["LedgerOPBalance"];
            }
            set
            {
                ViewState["LedgerOPBalance"] = value;
            }
        }



        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                this.PageTitle = MessageCatalog.Message.Vouchers.LedgerOpenBalanceTitle;
               // this.CheckUserRights(RightsModule.Data, RightsActivity.OpeningBalanceView, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadBranch();
                LoadProjects();
                LoadOPBalance();
                this.ShowLoadWaitPopUp(btnLoad);
                this.ShowTimeFromTimeTo = true;
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchId = Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
            LoadProjects();
        }

        protected void gvOpeningBalanceView_Load(object sender, EventArgs e)
        {
            LoadOPBalanceFromViewState();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadOPBalance();
        }
        #endregion

        #region Methods

        private void LoadBranch()
        {
            using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
            {
                resultArgs = BranchOfficeSystem.FetchBranch(DataBaseType.HeadOffice);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName, this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                    if (!base.LoginUser.IsBranchOfficeUser)
                    {
                        BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    }
                    else
                    {
                        cmbBranch.SelectedItem.Text = base.LoginUser.LoginUserBranchOfficeCode;
                        cmbBranch.Enabled = false;
                        BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    }
                }
                else
                {
                    BranchId = 0;
                }
            }
        }

        private void LoadProjects()
        {
            using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
            {
                try
                {
                    string projectid = string.Empty;
                    resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, false);
                        Project = 0;
                    }
                    else
                    {
                        Project = 0;
                    }
                }
                catch (Exception ex)
                {
                    this.Message = ex.ToString();
                }
            }
        }
        private void LoadOPBalance()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.ProjectId = Project;
                mappingSystem.BranchId = BranchId;
                resultArgs = mappingSystem.FetchMappedLedgers();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gvOpeningBalanceView.DataSource = resultArgs.DataSource.Table;
                    gvOpeningBalanceView.DataBind();
                    LedgerOPBalance = resultArgs.DataSource.Table;
                }
                else
                {
                    gvOpeningBalanceView.DataSource = null;
                    gvOpeningBalanceView.DataBind();
                    LedgerOPBalance = null;
                }
            }
        }

        private void LoadOPBalanceFromViewState()
        {
            gvOpeningBalanceView.DataSource = LedgerOPBalance;
            gvOpeningBalanceView.DataBind();
        }
        #endregion
    }
}