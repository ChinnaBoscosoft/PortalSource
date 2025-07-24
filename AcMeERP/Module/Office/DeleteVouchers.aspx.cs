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
 * Purpose          :This page helps the portal admin to delete the vouchers and clear the opening balance for the branch office in case of wrong updation of vouchers from the                             Branch office
 *****************************************************************************************************/
using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using System.Data;

namespace AcMeERP.Module.Office
{
    public partial class DeleteVouchers : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs;

        #endregion

        #region Properties

        string HeadOfficeCode
        {
            get
            {
                return ddlHeadOffice.SelectedValue;
            }
            set
            {
                HeadOfficeCode = value;
            }
        }

        int BranchOfficeId
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ddlBranchOffice.SelectedValue);
            }
            set
            {
                BranchOfficeId = value;
            }
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CheckUserRights(RightsModule.Tools, RightsActivity.DeleteVoucher, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadHeadOffice();
                LoadBranchOffice();
                ShowLoadWaitPopUp(btnDeleteVoucher);
            }
        }

        protected void btnDeleteVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFields())
                {
                    base.HeadOfficeCode = ddlHeadOffice.SelectedValue;
                    using (VoucherTransactionSystem VoucherTransactionSystem = new VoucherTransactionSystem())
                    {
                        resultArgs = VoucherTransactionSystem.DeleteVoucherTrans(BranchOfficeId);
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.Message = "Voucher deletion is Success";
                        }
                        else
                        {
                            this.Message = "Vouchers are not available for the given date period";
                        }
                    }
                    base.HeadOfficeCode = "";
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        protected void ddlHeadOffcie_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.HeadOfficeCode = HeadOfficeCode;
            LoadBranchOffice();
        }

        protected void ddlBranchOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.HeadOfficeCode = HeadOfficeCode;
        }

        #endregion

        #region Methods

        private void LoadHeadOffice()
        {
            try
            {
                using (HeadOfficeSystem HeadOfficeSystem = new HeadOfficeSystem())
                {
                    resultArgs = HeadOfficeSystem.FetchHeadOffice();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlHeadOffice, resultArgs.DataSource.Table, this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName, AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName);
                        ddlHeadOffice.SelectedIndex = 0;
                    }
                    else
                    {
                        this.Member.ComboSet.BindDataCombo(ddlHeadOffice, resultArgs.DataSource.Table, this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName, AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        private void LoadBranchOffice()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBrachByHeadOffice();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlBranchOffice, resultArgs.DataSource.Table, AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName, AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName);
                        ddlBranchOffice.SelectedIndex = 0;
                    }
                    else
                    {
                        this.Member.ComboSet.BindDataCombo(ddlBranchOffice, resultArgs.DataSource.Table, AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName, AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;
            if (ddlHeadOffice.Items.Count == 0)
            {
                isValid = false;
                this.Message = "No Head Office is available";
            }
            else if (ddlBranchOffice.Items.Count == 0)
            {
                isValid = false;
                this.Message = "No Branch Office is available";
            }
            return isValid;
        }

        #endregion
    }
}