using System;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;


namespace Bosco.Report.ReportObject
{
    public partial class MonthlyGNAbstractReceiptsPayments : ReportHeaderBase
    {
        double JVAffectedAmount = 0;
        public MonthlyGNAbstractReceiptsPayments()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
        }
        public override void ShowReport()
        {
            LoadMonthlyAbstractReport();
            base.ShowReport();
        }

        private void LoadMonthlyAbstractReport()
        {
            SetReportTitle();
            this.SetLandscapeFooterDateWidth = 505.25f;
            setHeaderTitleAlignment();
            MonthlyGNAbstractReceipts montlyAbstractReceipts = xrSubreportMonthlyReceipts.ReportSource as MonthlyGNAbstractReceipts;
            MonthlyGNAbstractPayments montlyAbstractPayments = xrSubreportMontlyPayments.ReportSource as MonthlyGNAbstractPayments;
            GeneralateJournalVoucherReport JVVouchers = xrSubreportJV.ReportSource as GeneralateJournalVoucherReport;


            //this.AttachDrillDownToSubReport(montlyAbstractReceipts);
            //this.AttachDrillDownToSubReport(montlyAbstractPayments);
            montlyAbstractReceipts.HideReportHeaderFooter();
            montlyAbstractPayments.HideReportHeaderFooter();
            JVVouchers.HideReportHeaderFooter();

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                       || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                       || this.ReportProperties.Project == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                //Get Projects for selected projects ---------------------
                //this.ReportProperties.Project = this.GetSocietyProjectIds();
                this.ReportProperties.Project = this.GetProjectIds(this.ReportProperties.Society, this.ReportProperties.BranchOffice);
                //--------------------------------------------------------

                //ResultArgs result = GetReportSource(TransType.RC);
                ResultArgs result = GetReportSource();
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtRP = result.DataSource.Table;

                    //For Receipts
                    dtRP.DefaultView.RowFilter = string.Empty;
                    dtRP.DefaultView.RowFilter = "AMOUNT_CR > 0";
                    DataTable dtReceipts = dtRP.DefaultView.ToTable();
                    dtReceipts.Columns["AMOUNT_CR"].ColumnName = "AMOUNT_PERIOD";
                    dtRP.DefaultView.RowFilter = string.Empty;
                    //DataTable dtReceipts = result.DataSource.Table;

                    //For Payments
                    //result = GetReportSource(TransType.PY);
                    //DataTable dtPayments = result.DataSource.Table;
                    dtRP.DefaultView.RowFilter = string.Empty;
                    dtRP.DefaultView.RowFilter = "AMOUNT_DR > 0";
                    DataTable dtPayments = dtRP.DefaultView.ToTable();
                    dtPayments.Columns["AMOUNT_DR"].ColumnName = "AMOUNT_PERIOD";
                    dtRP.DefaultView.RowFilter = string.Empty;

                    //For Journal
                    dtRP.DefaultView.RowFilter = string.Empty;
                    dtRP.DefaultView.RowFilter = "AMOUNT_JV_CR > 0 OR AMOUNT_JV_DR >0";
                    DataTable dtJournalVouchers = dtRP.DefaultView.ToTable();
                    dtJournalVouchers.Columns["AMOUNT_JV_CR"].ColumnName = "CREDIT_AMOUNT";
                    dtJournalVouchers.Columns["AMOUNT_JV_DR"].ColumnName = "DEBIT_AMOUNT";
                    dtJournalVouchers.Columns["JV_AMOUNT"].ColumnName = "AMOUNT";
                    dtJournalVouchers.Columns["LEDGER_GROUP"].ColumnName = "CON_LEDGER_NAME";
                    dtRP.DefaultView.RowFilter = string.Empty;


                    if (result.Success && result.DataSource.Table != null)
                    {
                        //double totalreceitps = UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(AMOUNT_PERIOD)", string.Empty).ToString());
                        //double totalpayments = UtilityMember.NumberSet.ToDouble(dtPayments.Compute("SUM(AMOUNT_PERIOD)", string.Empty).ToString());

                        ////double diffamount = 0;
                        //if (ReportProperties.ShowDetailedBalance == 1)
                        //{
                        //    if (!String.IsNullOrEmpty(this.ReportProperties.Project))
                        //    {
                        //        ResultArgs resultbalance = this.GetBalanceDetail(true, this.ReportProperties.DateFrom, this.ReportProperties.Project, "12,13,14");
                        //        if (resultbalance.Success && resultbalance.DataSource.Table != null)
                        //        {
                        //            DataTable dtOpBalance = resultbalance.DataSource.Table;
                        //            dtOpBalance.Columns.Add("FINAL_AMOUNT", dtOpBalance.Columns["AMOUNT"].DataType, "IIF(TRANS_MODE='CR', AMOUNT * -1, AMOUNT)");
                        //            double cashbankfd = UtilityMember.NumberSet.ToDouble(dtOpBalance.Compute("SUM(FINAL_AMOUNT)", string.Empty).ToString());
                        //            totalreceitps = totalreceitps + cashbankfd;
                        //        }

                        //        resultbalance = this.GetBalanceDetail(false, this.ReportProperties.DateTo, this.ReportProperties.Project, "12,13,14");
                        //        if (resultbalance.Success && resultbalance.DataSource.Table != null)
                        //        {
                        //            DataTable dtCLBalance = resultbalance.DataSource.Table;
                        //            dtCLBalance.Columns.Add("FINAL_AMOUNT", dtCLBalance.Columns["AMOUNT"].DataType, "IIF(TRANS_MODE='CR', AMOUNT * -1, AMOUNT)");
                        //            double cashbankfd = UtilityMember.NumberSet.ToDouble(dtCLBalance.Compute("SUM(FINAL_AMOUNT)", string.Empty).ToString());
                        //            totalpayments = totalpayments + cashbankfd;
                        //        }
                        //    }

                        //    diffamount = (totalreceitps - totalpayments);
                        //}

                        montlyAbstractReceipts.BindReceiptSource(dtReceipts);
                        montlyAbstractPayments.TotalReceiptAmout = montlyAbstractReceipts.TotalReceiptAmount;
                        montlyAbstractPayments.BindPaymentSource(dtPayments);

                        JVVouchers.BindJournalSource(dtJournalVouchers);
                        JVAffectedAmount = JVVouchers.TotalAmount;
                    }
                }
            }
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
        }

        private ResultArgs GetReportSource() //TransType transtype
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateAbstract);
            //string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateAbstract, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                //dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, transtype.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);

                //if (transtype == TransType.RC)
                //    dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                //else
                //    dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.DR.ToString());

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);
                //if (transtype == TransType.RC)
                //    dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                //else
                //    dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.DR.ToString());

                //if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                //}
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);

                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }

        private void xrSubreportJV_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (JVAffectedAmount == 0);
        }
    }
}
