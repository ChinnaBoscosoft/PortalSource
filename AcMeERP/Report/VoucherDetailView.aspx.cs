using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel.Master;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using DevExpress.Web.ASPxGridView;
using System.Data;

namespace AcMeERP.Report
{
    public partial class VoucherDetailView : System.Web.UI.Page
    {
        #region Declartion

        ResultArgs resultArgs = null;
        DataTable dtTranasaction = new DataTable();
        #endregion

        #region Properties
       
        private int BranchID
        {
            set { ViewState["branchID"] = value; }
            get { return Convert.ToInt32(ViewState["branchID"].ToString()); }
        }
        private int LocationId
        {
            set { ViewState["LocationId"] = value; }
            get { return Convert.ToInt32(ViewState["LocationId"].ToString()); }
        }
        private int VoucherId
        {
            set { ViewState["voucherId"] = value; }
            get { return Convert.ToInt32(ViewState["voucherId"].ToString()); }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VoucherId = Request.QueryString["VoucherId"] == null ? 0 : Convert.ToInt32(Request.QueryString["VoucherId"].ToString());
                BranchID = Request.QueryString["BranchId"] == null ? 0 : Convert.ToInt32(Request.QueryString["BranchId"].ToString());
                LocationId = 0;
                LoadVoucherDetails();
            }
        }

        protected void gvVoucherTrans_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;
                int VoucherId = Convert.ToInt16(grid.GetMasterRowKeyValue().ToString());
                dtTranasaction = LoadTransaction(VoucherId, BranchID,0);
                if (dtTranasaction.Rows.Count > 0)
                {
                    grid.DataSource = dtTranasaction;
                    grid.ExpandAll();
                }
                else
                {
                    grid.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Report Drill Down Voucher view screen " + ex.Message);

            }
        }

        protected void lbtnAmendment_click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/module/master/AmendmentsAdd.aspx?1=" + VoucherId + "&2=" + BranchID +"&3="+LocationId);
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Voucher Detailview screen" + ex.Message);
            }
        }

        #endregion

        #region Methods

        private void LoadVoucherDetails()
        {
            try
            {
                using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                {
                    voucherTransSystem.VoucherId = VoucherId;
                    voucherTransSystem.BranchId = BranchID;
                    voucherTransSystem.LocationId = LocationId;
                    resultArgs = voucherTransSystem.FetchVoucherMasterById();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        gvMasterVoucher.DataSource = resultArgs.DataSource.Table;
                        gvMasterVoucher.DataBind();
                        gvMasterVoucher.DetailRows.ExpandAllRows();
                        lblProjectName.Text = resultArgs.DataSource.Table.Rows[0]["PROJECT"].ToString();
                        lblBranchName.Text = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_NAME"].ToString();
                        lblVoucherDate.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_DATE"].ToString();
                        lblVoucherType.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_TYPE"].ToString();
                        lblVoucherNo.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_NO"].ToString();
                        txtNAddress.Text = resultArgs.DataSource.Table.Rows[0]["NAME_ADDRESS"].ToString();
                        txtNarration.Text = resultArgs.DataSource.Table.Rows[0]["NARRATION"].ToString();
                    }
                    else
                    {
                        gvMasterVoucher.DataSource = null;
                        gvMasterVoucher.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Drill Down Voucher view screen " + ex.Message);
            }
        }

        private DataTable LoadTransaction(int VoucherId, int BranchId,int locationId)
        {
            try
            {
                using (VoucherTransactionSystem VoucherTransaction = new VoucherTransactionSystem())
                {
                    resultArgs = VoucherTransaction.FetchTransactions(VoucherId, BranchId, locationId);
                    if (!resultArgs.Success)
                    {
                        new ErrorLog().WriteError("Drill Down Voucher view screen " + resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Voucher view screen " + ex.Message);
            }
            return resultArgs.DataSource.Table;
        }

        #endregion
    }
}