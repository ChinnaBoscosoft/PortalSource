using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using System.Web;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateActivityIEFA : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        private DataTable dtSource { get; set; }
        double SubGroupTotalAmountCredit = 0;
        double SubGroupTotalAmountDebit = 0;
        double groupDebitSum = 0;
        double groupCreditSum = 0;
        double TotalNetProfitIncome = 0;
        double TotalNetProfitExpense = 0;
        bool IsIncomeGrp = true;
        #endregion
        
        /// <summary>
        /// 14/01/2020, for FDCCSI, to keep Movement FD: NET Total
        /// </summary>
        public double NetTotalMovementActivity
        {
            get
            {
                double rtn = 0;
                if (HttpContext.Current.Session["NetTotalMovementActivity"] != null)
                {
                    rtn = UtilityMember.NumberSet.ToDouble(HttpContext.Current.Session["NetTotalMovementActivity"].ToString());
                }
                return rtn;
            }
            set
            {
                HttpContext.Current.Session["NetTotalMovementActivity"] = value;
            }
        }

        #region Constructor
        public GeneralateActivityIEFA()
        {
            this.SetLandscapeHeader = 723.25f;
            this.SetLandscapeFooter = 723.25f;
            InitializeComponent();
        }
        #endregion

        #region ShowReports
        public override void ShowReport()
        {
            groupDebitSum = 0;
            groupCreditSum = 0;
            TotalNetProfitIncome = 0;
            TotalNetProfitExpense = 0;
            NetTotalMovementActivity = 0;

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindSource();
            }

            base.ShowReport();
        }
        private void xrTableExpenseLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ExpenseLedgerAmt = this.ReportProperties.NumberSet.ToDouble(xrTableExpenseLedgerName.Text);
            if (ExpenseLedgerAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableExpenseLedgerName.Text = "";
            }
        }

        private void xrTableIncomeLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double IncomeLedgerAmt = this.ReportProperties.NumberSet.ToDouble(xrTableIncomeLedgerName.Text);
            if (IncomeLedgerAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableIncomeLedgerName.Text = "";
            }
        }

        private void xrTableFooterCongGroMain_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string MasterName = (GetCurrentColumnValue("MASTER_NAME") == null) ? string.Empty : "Total " + GetCurrentColumnValue("MASTER_NAME").ToString();
            xrTableFooterCongGroMain.Text = MasterName;

            if (xrTableFooterCongGroMain.Text.Equals("Total G1 Income - Fixed Asset"))
            {
                xrTableFooterCongGroMain.Text = "Total Income G1";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total G2 Expenditure - Fixed Asset"))
            {
                xrTableFooterCongGroMain.Text = "Total Expenditrue G2";
            }

            if (xrTableFooterCongGroMain.Text.Equals("Total H1 Income - Bonds & Securities"))
            {
                xrTableFooterCongGroMain.Text = "Total Income H1";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total H2 Expenditure - Bonds & Securities"))
            {
                xrTableFooterCongGroMain.Text = "Total Expenditure H2";
            }

            if (xrTableFooterCongGroMain.Text.Equals("Total I1 Income - Financing and Refunds"))
            {
                xrTableFooterCongGroMain.Text = "Total Income I1";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total I2 Expense - Financing and Refunds"))
            {
                xrTableFooterCongGroMain.Text = "Total Expenses I2";
            }

            if (xrTableFooterCongGroMain.Text.Equals("Total L1 Income - Commercial Activities"))
            {
                xrTableFooterCongGroMain.Text = "Total Income L1";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total L2 Expense - Commercial Activities"))
            {
                xrTableFooterCongGroMain.Text = "Total Expenses L2";
            }
        }

        private void xrTableFooterCongGrpSub_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string MasterName = (GetCurrentColumnValue("MASTER_PARENT") == null) ? string.Empty : GetCurrentColumnValue("MASTER_PARENT").ToString();
            xrTableFooterCongGrpSub.Text = "Total Cash Flow generated " + MasterName;

            if (xrTableFooterCongGrpSub.Text.Equals("Total Cash Flow generated G Fixed Asset"))
            {
                xrTableFooterCongGrpSub.Text = "Total Cash flow generated (G1-G2)";
            }
            if (xrTableFooterCongGrpSub.Text.Equals("Total Cash Flow generated H BONDS & SECURITIES"))
            {
                xrTableFooterCongGrpSub.Text = "Total Cash flow generated (H1-H2)";
            }
            if (xrTableFooterCongGrpSub.Text.Equals("Total Cash Flow generated I FINANCING AND REFUNDS"))
            {
                xrTableFooterCongGrpSub.Text = "Total Cash flow generated (I1-I2)";
            }
            if (xrTableFooterCongGrpSub.Text.Equals("Total Cash Flow generated L COMMERCIAL ACTIVITIES"))
            {
                xrTableFooterCongGrpSub.Text = "Total Cash flow generated (L1-L2)";
            }


        }
        private void xrTableCongGrpSub_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string MasterName = (GetCurrentColumnValue("MASTER_NAME") == null) ? string.Empty : GetCurrentColumnValue("MASTER_NAME").ToString();
            xrTableCongGrpSub.Text = MasterName;

            if (xrTableCongGrpSub.Text.Equals("G1 Income - Fixed Asset"))
            {
                xrTableCongGrpSub.Text = "G1 Income";
            }
            if (xrTableCongGrpSub.Text.Equals("G2 Expenditure - Fixed Asset"))
            {
                xrTableCongGrpSub.Text = "G2 Expenditure";
            }

            if (xrTableCongGrpSub.Text.Equals("H1 Income - Bonds & Securities"))
            {
                xrTableCongGrpSub.Text = "H1 Income";
            }
            if (xrTableCongGrpSub.Text.Equals("H2 Expenditure - Bonds & Securities"))
            {
                xrTableCongGrpSub.Text = "H2 Expenditure";
            }

            if (xrTableCongGrpSub.Text.Equals("I1 Income - Financing and Refunds"))
            {
                xrTableCongGrpSub.Text = "I1 Income";
            }
            if (xrTableCongGrpSub.Text.Equals("I2 Expense - Financing and Refunds"))
            {
                xrTableCongGrpSub.Text = "I2 Expenses";
            }

            if (xrTableCongGrpSub.Text.Equals("L1 Income - Commercial Activities"))
            {
                xrTableCongGrpSub.Text = "L1 Income";
            }
            if (xrTableCongGrpSub.Text.Equals("L2 Expense - Commercial Activities"))
            {
                xrTableCongGrpSub.Text = "L2 Expenses";
            }
        }
        #endregion

        #region Methods
        public void BindSource(bool fromMasterReport = false, DataTable dtDataSource=null)
        {
            SetReportTitle();
            this.SetLandscapeFooterDateWidth = 712.25f;

            if (fromMasterReport)
            {
                this.HideReportHeader = this.HidePageHeader = false;
                this.HideHeaderFully = true;
                this.HidePageFooter = false;
                xrlblYear.Text = "Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            }
            if (dtDataSource != null)
            {
                dtDataSource.DefaultView.RowFilter = "CON_LEDGER_CODE IN ('G','G1','G2','H','H1','H2','I','I1','I2','L','L1','L2')"; //Filter concern code for FA and Captial Fund;
                DataTable dtFAandCaptial = dtDataSource.DefaultView.ToTable();
                dtFAandCaptial.TableName = "CongiregationProfitandLoss";
                this.DataSource = dtFAandCaptial;
                this.DataMember = dtFAandCaptial.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlIncomeExpenditure = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpenseFA);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpenseFA, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlIncomeExpenditure);
            }
            return resultArgs;
        }

        #endregion


        private void xrTableFooterIncomeCongGroSub_SummaryRowChanged(object sender, EventArgs e)
        {
            groupDebitSum += (GetCurrentColumnValue("NXTRECEIPT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("NXTRECEIPT").ToString());
            groupCreditSum += (GetCurrentColumnValue("NXTPAYMENT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("NXTPAYMENT").ToString());
        }

        private void xrTableFooterIncomeCongGroSub_SummaryReset(object sender, EventArgs e)
        {
            groupDebitSum = groupCreditSum = 0;
        }

        private void xrTableFooterIncomeCongGroSub_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double Balance = 0;
            Balance = groupDebitSum - groupCreditSum;
            TotalNetProfitExpense += Balance;
            e.Result = Balance;
            e.Handled = true;

        }

        private void xrTableProfilLossIEFA_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalNetProfit = 0;
            TotalNetProfit = TotalNetProfitExpense;
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalNetProfit);
            e.Handled = true;
            NetTotalMovementActivity = TotalNetProfit;
        }

        

      
    }
}
