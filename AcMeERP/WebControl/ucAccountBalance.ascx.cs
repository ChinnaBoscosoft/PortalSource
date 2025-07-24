/*****************************************************************************************************
 * Created by       : Amal Arockia Raj
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This user Control loads the Branch office Cash, Bank and FD Balance
 *                   For Sending mail and showing  the finance balance udpate the dll in dll folder (AcMEDSync.dll,Bosco.HOSQL.dll) and give reference to the Bosco.Report, Bosco.SQL,                      AcMEERP Project, Bosco.Model
 *****************************************************************************************************/
using System;
using AcMEDSync.Model;
using System.Data;
using Bosco.Model.Transaction;
using Bosco.Utility;

namespace AcMeERP.WebControl
{
    public partial class ucAccountBalance : System.Web.UI.UserControl
    {
        #region Properties
        public string BalanceCaption
        {
            set
            {
                lblBalance.Text = value;
            }
        }
        public string CashBalance
        {
            set
            {
                lblCashBalance.Text = value;
            }

        }
        public string BankBalance
        {
            set
            {
                lblBankBalance.Text = value;
            }
        }
        public string FDBalance
        {
            set
            {
                lblFdBalance.Text = value;
            }
        }

        public string ProjectIds
        {
            set
            {
                ViewState["ProjectIds"] = value;
            }
            get
            {
                return ViewState["ProjectIds"].ToString();
            }
        }

        public string BalanceDate
        {
            set
            {
                ViewState["BalanceDate"] = value;
            }
            get
            {
                return ViewState["BalanceDate"].ToString();
            }
        }

        public string BranchId
        {
            set
            {
                ViewState["BranchId"] = value;
            }
            get
            {
                return ViewState["BranchId"].ToString();
            }
        }

        public BalanceSystem.BalanceType BalanceType
        {
            set
            {
                ViewState["BalanceType"] = value;
            }
            get
            {
                return (BalanceSystem.BalanceType)ViewState["BalanceType"];
            }
        }
        #endregion

        #region Events

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        #endregion

        #region Methods

        private void BindCashBalance(DataTable dtCashBalance)
        {
            try
            {
                if (dtCashBalance != null && dtCashBalance.Rows.Count > 0)
                {
                    gvCashBalance.DataSource = dtCashBalance;
                    gvCashBalance.DataBind();
                }
                else
                {
                    gvCashBalance.DataSource = null;
                    gvCashBalance.DataBind();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
        }

        private void BindBankBalance(DataTable dtBankBalance)
        {
            if (dtBankBalance != null && dtBankBalance.Rows.Count > 0)
            {
                gvBankBalance.DataSource = dtBankBalance;
                gvBankBalance.DataBind();
            }
            else
            {
                gvBankBalance.DataSource = null;
                gvBankBalance.DataBind();
            }
        }

        private void BindFDBalance(DataTable dtFDBalance)
        {
            if (dtFDBalance != null && dtFDBalance.Rows.Count > 0)
            {
                gvFdBalance.DataSource = dtFDBalance;
                gvFdBalance.DataBind();
            }
            else
            {
                gvFdBalance.DataSource = null;
                gvFdBalance.DataBind();
            }
        }

        public void LoadBalance(string branchid, string projectid, string balancedate, BalanceSystem.BalanceType balancetype)
        {
            BranchId = branchid;
            ProjectIds = projectid;
            BalanceDate = balancedate;
            BalanceType = balancetype;
            LoadCashBalance();
            LoadBankBalance();
            LoadFDBalance();

        }
        private void LoadCashBalance()
        {
            try
            {
                DataTable dtCashAccounts = null;
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    BalanceProperty balanceProperty = new BalanceProperty();
                    balanceProperty = balanceSystem.GetCashBalance(BranchId, ProjectIds, BalanceDate, BalanceType);
                    if (balanceProperty.Result.Success && balanceProperty.Result.DataSource.Table.Rows.Count > 0)
                    {
                        CashBalance = balanceProperty.Amount.ToString("C") + " " + balanceProperty.TransMode;
                        dtCashAccounts = FetchAccounts((int)FixedLedgerGroup.Cash);
                        BindCashBalance(dtCashAccounts);
                    }
                    else
                    {
                        CashBalance = "0.00";
                        BindCashBalance(dtCashAccounts);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LoadBankBalance()
        {
            try
            {
                DataTable dtBankAccounts = null;
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    BalanceProperty balanceProperty = new BalanceProperty();
                    balanceProperty = balanceSystem.GetBankBalance(BranchId.ToString(), ProjectIds, BalanceDate, BalanceType);
                    if (balanceProperty.Result.Success && balanceProperty.Result.DataSource.Table.Rows.Count > 0)
                    {
                        BankBalance = balanceProperty.Amount.ToString("C") + " " + balanceProperty.TransMode;
                        dtBankAccounts = FetchAccounts((int)FixedLedgerGroup.BankAccounts);
                        //  BindBankBalance(dtBankAccounts);

                        using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                        {
                            ResultArgs result = voucherTransactionSystem.FetchTransBankBalance(BranchId, ProjectIds, BalanceDate);
                            DataTable dtFD = result.DataSource.Table;
                            BindBankBalance(dtFD);
                        }
                    }
                    else
                    {
                        BankBalance = "0.00";
                        BindBankBalance(dtBankAccounts);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadFDBalance()
        {
            try
            {
                DataTable dtFDAccounts = null;
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    BalanceProperty balanceProperty = new BalanceProperty();
                    balanceProperty = balanceSystem.GetFDBalance(BranchId.ToString(), ProjectIds, BalanceDate, BalanceType);
                    if (balanceProperty.Result.Success && balanceProperty.Result.DataSource.Table.Rows.Count > 0)
                    {
                        FDBalance = balanceProperty.Amount.ToString("C") + " " + balanceProperty.TransMode;
                        dtFDAccounts = FetchAccounts((int)FixedLedgerGroup.FixedDeposit);
                        using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                        {
                            ResultArgs result = voucherTransactionSystem.FetchTransFDBalance(BranchId, ProjectIds, BalanceDate);
                            DataTable dtFD = result.DataSource.Table;
                            BindFDBalance(dtFD);
                        }
                    }
                    else
                    {
                        FDBalance = "0.00";
                        BindFDBalance(dtFDAccounts);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable FetchAccounts(int GroupId)
        {
            ResultArgs resultArgs = new ResultArgs();
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectIDs = ProjectIds;
                voucherTransSystem.GroupId = GroupId;
                voucherTransSystem.BalanceDate = BalanceSystem.BalanceType.OpeningBalance == BalanceType ? Convert.ToDateTime(BalanceDate).AddDays(-1) : Convert.ToDateTime(BalanceDate);
                voucherTransSystem.BranchIds = BranchId;
                if (BalanceSystem.BalanceType.OpeningBalance == BalanceType)
                {
                    resultArgs = voucherTransSystem.FetchTransBalance();
                }
                else
                {
                    resultArgs = voucherTransSystem.FetchTransClosingBalance();
                }
            }
            return resultArgs.DataSource.Table;
        }

        #endregion
    }
}