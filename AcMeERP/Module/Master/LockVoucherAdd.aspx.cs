using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.UIModel.Master;

namespace AcMeERP.Module.Master
{
    public partial class LockVoucherAdd : Base.UIBase
    {
        #region Property
        ResultArgs resultArgs = null;

        private int LockTransId
        {
            get
            {
                int locktransId = this.Member.NumberSet.ToInteger(this.RowId);
                return locktransId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }
        private int branchid = 0;
        private int BranchId
        {
            set { branchid = value; }
            get { return branchid; }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                LoadBranches();
                this.CheckUserRights(RightsModule.Data, RightsActivity.LockVoucherAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                hlkClose.PostBackUrl = this.ReturnUrl;
                if (LockTransId != 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                this.SetControlFocus(ddlBranch);
                this.ShowLoadWaitPopUp(btnSaveUser);
            }
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLockVouchers())
                {
                    ResultArgs resultArgs = null;
                    using (LockVoucherSystem LockVouchersystem = new LockVoucherSystem())
                    {
                        LockVouchersystem.LockTransId = LockTransId == 0 ? (int)AddNewRow.NewRow : LockTransId;

                        LockVouchersystem.BranchId = this.Member.NumberSet.ToInteger(ddlBranch.SelectedValue.ToString());
                        LockVouchersystem.ProjectId = this.Member.NumberSet.ToInteger(ddlProjects.SelectedValue.ToString());
                        LockVouchersystem.DateFrom = dteDateFrom.Date;
                        LockVouchersystem.DateTo = dteDateTo.Date;
                        LockVouchersystem.Password = CommonMember.EncryptValue(txtPassword.Text.Trim());
                        LockVouchersystem.Reasons = txtReasons.Text.Trim();
                        LockVouchersystem.PasswordHint = CommonMember.EncryptValue(txtPasswordHint.Text.Trim());
                        LockVouchersystem.LockByPortal = 1;

                        resultArgs = LockVouchersystem.SaveLockVoucher(DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = "Lock Voucher is Saved";
                            if (LockTransId == 0)
                            {
                                LockTransId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                ClearValues();
                            }
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }

        private bool ValidateLockVouchers()
        {
            bool isvalid = true;
            if (ddlBranch.SelectedValue == null)
            {
                this.Message = "Branch is empty";
                isvalid = false;
            }
            else if (ddlProjects.SelectedValue == null)
            {
                this.Message = "Project is empty";
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(dteDateFrom.Text))
            {
                this.Message = "Date From is empty";
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                this.Message = "Password is empty";
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtPasswordHint.Text))
            {
                this.Message = "Password Hint is empty";
                isvalid = false;
            }
            else if (dteDateFrom.Text != string.Empty && dteDateTo.Text != string.Empty)
            {
                if (this.Member.DateSet.ValidateFutureDate(this.Member.DateSet.ChangeDateFormat(dteDateFrom.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME)))
                {
                    if (!this.Member.DateSet.ValidateDate(this.Member.DateSet.ChangeDateFormat(dteDateFrom.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME), this.Member.DateSet.ChangeDateFormat(dteDateTo.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME)))
                    {
                        this.Message = MessageCatalog.Message.LockCloseDate;
                        dteDateTo.Text = string.Empty;
                        dteDateTo.Focus();
                        isvalid = false;
                    }
                }
                else
                {
                    this.Message = "Date From should not be less than the future Date";
                    dteDateFrom.Text = string.Empty;
                    dteDateFrom.Focus();
                    isvalid = false;
                }
            }
            return isvalid;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetControlFocus(ddlBranch);
            LockTransId = 0;
            ClearValues();
            SetPageTitle();
        }

        #endregion

        #region Methods
        private void AssignValuesToControls()
        {
            using (LockVoucherSystem LockvoucherSystem = new LockVoucherSystem(LockTransId, DataBaseType.HeadOffice))
            {
                if (!string.IsNullOrEmpty(LockvoucherSystem.BranchId.ToString()))
                    ddlBranch.SelectedValue = LockvoucherSystem.BranchId.ToString();
                LoadProjects();
                ddlProjects.SelectedValue = LockvoucherSystem.ProjectId.ToString();
                dteDateFrom.Text = ((LockvoucherSystem.DateFrom != DateTime.MinValue) ? LockvoucherSystem.DateFrom.ToString("dd/MM/yyyy") : string.Empty);
                dteDateTo.Text = ((LockvoucherSystem.DateTo != DateTime.MinValue) ? LockvoucherSystem.DateTo.ToString("dd/MM/yyyy") : string.Empty);
                txtPassword.Text = CommonMember.DecryptValue(LockvoucherSystem.Password).ToString();
                txtPasswordHint.Text = CommonMember.DecryptValue(LockvoucherSystem.PasswordHint).ToString(); ;
                txtReasons.Text = LockvoucherSystem.Reasons;
            }
        }

        private void ClearValues()
        {
            LockTransId = 0;
            dteDateFrom.Text = dteDateTo.Text = txtPassword.Text = txtPasswordHint.Text = string.Empty;
            ddlBranch.SelectedValue = ddlProjects.SelectedValue = "0";
            SetControlFocus(ddlBranch);
            SetPageTitle();
        }

        private void LoadBranches()
        {
            using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
            {
                resultArgs = BranchOffice.FetchBranch();
                this.Member.ComboSet.BindDataCombo(ddlBranch, resultArgs.DataSource.Table, this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName,
                    this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, true, CommonMember.SELECT);

            }
        }

        private void LoadProjects()
        {
            try
            {
                using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
                {
                    // BranchId = this.Member.NumberSet.ToInteger(ddlBranch.SelectedValue.ToString());
                    BranchId = this.Member.NumberSet.ToInteger(ddlBranch.SelectedValue);
                    if (LockTransId > 0)
                    {
                        resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
                    }
                    else
                    {
                        resultArgs = BranchOffiecSystem.FetchProjectBYBranch(BranchId);
                    }
                    this.Member.ComboSet.BindDataCombo(ddlProjects, resultArgs.DataSource.Table, AppSchema.Project.PROJECTColumn.ColumnName, AppSchema.Project.PROJECT_IDColumn.ColumnName, true, CommonMember.SELECT);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.LockVoucher.LockEditPageTitle : MessageCatalog.Message.LockVoucher.LockAddPageTitle));
        }
        #endregion

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProjects();
            SetControlFocus(ddlProjects);
        }
    }
}