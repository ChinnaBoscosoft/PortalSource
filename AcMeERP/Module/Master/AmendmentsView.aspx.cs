using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace AcMeERP.Module.Master
{

    public partial class AmendmentsView : Base.UIBase
    {
        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs;
        private static string CODE = "CODE";

        #endregion

        #region Properties


        private string BranchOfficeCode
        {
            set { ViewState["BranchOfficeCode"] = value; }
            get { return ViewState["BranchOfficeCode"].ToString(); }
        }


        private int BranchId
        {
            set { ViewState[AppSchema.Amendments.BRANCH_OFFICE_CODEColumn.ColumnName] = value; }
            get { return this.Member.NumberSet.ToInteger(ViewState[AppSchema.Amendments.BRANCH_OFFICE_CODEColumn.ColumnName].ToString()); }
        }

        private string VoucherId
        {
            set { ViewState[AppSchema.Amendments.VOUCHER_IDColumn.ColumnName] = value; }
            get { return ViewState[AppSchema.Amendments.VOUCHER_IDColumn.ColumnName].ToString(); }
        }

        private DataTable AmendmentHistory
        {
            set { ViewState[AppSchema.Amendments.TableName] = value; }
            get { return (DataTable)ViewState[AppSchema.Amendments.TableName]; }
        }

        private DataTable Branch
        {
            set { ViewState[AppSchema.BranchOffice.TableName] = value; }
            get { return (DataTable)ViewState[AppSchema.BranchOffice.TableName]; }
        }


        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                AmendmentHistory = null;
                Branch = null;
                this.CheckUserRights(RightsModule.Data, RightsActivity.AmendmentView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ?
                   DataBaseType.Portal : DataBaseType.HeadOffice);
                this.PageTitle = MessageCatalog.Message.Amendment.AmendmentViewPageTitle;
                BranchOfficeCode = string.Empty;
                LoadBranch();
                LoadAmendmentHistory();
                this.ShowLoadWaitPopUp();
                this.SetControlFocus(cmbBranch);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvAmendmentHistory_Load(object sender, EventArgs e)
        {
            LoadGrid(AmendmentHistory);
        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                DataView dvBranch = Branch.DefaultView;
                dvBranch.RowFilter = AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName + " IN (" + BranchId + ")";
                BranchOfficeCode = dvBranch.ToTable().Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                dvBranch.RowFilter = string.Empty;
                LoadAmendmentHistory();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        #endregion

        #region Methods

        private void LoadAmendmentHistory()
        {
            try
            {
                using (AmendmentSystem amendmentSystem = new AmendmentSystem())
                {
                    amendmentSystem.BranchOfficeCode = BranchOfficeCode;
                    resultArgs = amendmentSystem.FetchAmendmentHistory();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        AmendmentHistory = resultArgs.DataSource.Table;
                    else
                        AmendmentHistory = resultArgs.DataSource.Table;
                    LoadGrid(AmendmentHistory);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranch(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, CODE, this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                        Branch = resultArgs.DataSource.Table;
                        if (!base.LoginUser.IsHeadOfficeUser)
                        {
                            if (!string.IsNullOrEmpty(base.LoginUser.LoginUserBranchOfficeCode))
                            {
                                cmbBranch.Text = base.LoginUser.LoginUserBranchOfficeName;
                                BranchId = Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                                BranchOfficeCode = base.LoginUser.LoginUserBranchOfficeCode;
                                cmbBranch.Enabled = false;
                            }
                        }
                        else
                        {
                            if (this.Member.NumberSet.ToInteger(Session[base.DefaultBranchId].ToString()) != 0)
                            {
                                cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                            }
                            BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                DataView dvAmendements = resultArgs.DataSource.Table.DefaultView;
                                dvAmendements.RowFilter = AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn + " IN (" + BranchId + ")";
                                BranchOfficeCode = dvAmendements.ToTable().Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                                dvAmendements.RowFilter = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        BranchId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadGrid(DataTable dtAmendments)
        {
            try
            {
                gvAmendmentHistory.DataSource = dtAmendments;
                gvAmendmentHistory.DataBind();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        #endregion
    }
}