using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
namespace Bosco.Report.ReportObject
{
    public partial class BankReconciliation : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double UnrealizedAmt = 0;
        double UnClearedAmt = 0;
        #endregion

        #region Constructor
        public BankReconciliation()
        {
            InitializeComponent();
            /*this.AttachDrillDownToRecord(xrTblRecord, xrParticulars,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, this.ReportParameters.DATE_AS_ONColumn.ColumnName },
                DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");*/
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindBankReconciliationStatement();
            base.ShowReport();
        }
        #endregion

        #region Method
        public void BindBankReconciliationStatement()
        {
            xrCurrentBankBalance.Text = string.Empty;
            if (!string.IsNullOrEmpty(this.ReportProperties.DateAsOn)
                && !string.IsNullOrEmpty(this.ReportProperties.Project)
                && this.ReportProperties.Ledger != "0")
            {
                //this.ReportTitle = objReportProperty.ReportTitle;
                // this.ReportSubTitle = objReportProperty.ProjectTitle;
                setHeaderTitleAlignment();
                SetReportTitle();
                this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                xrCapUnrealized.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.UNREALIZED);
                xrCapUnCleared.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.UNCLEARED);
                resultArgs = GetReportSource();
                DataView dvBankReconciliation = resultArgs.DataSource.TableView;
                if (dvBankReconciliation != null && dvBankReconciliation.Count != 0)
                {
                    UnrealizedAmt = objReportProperty.NumberSet.ToDouble(dvBankReconciliation.ToTable().Compute("SUM(UNREALISED)", "").ToString());
                    UnClearedAmt = objReportProperty.NumberSet.ToDouble(dvBankReconciliation.ToTable().Compute("SUM(UNCLEARED)", "").ToString());
                    dvBankReconciliation.Table.TableName = "BankReconciliationStatement";
                    this.DataSource = dvBankReconciliation;
                    this.DataMember = dvBankReconciliation.Table.TableName;

                    //xrCurrentBankBalance.Text = objReportProperty.NumberSet.ToNumber(this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateAsOn, BalanceSystem.LiquidBalanceGroup.BankBalance,
                    //               BalanceSystem.BalanceType.ClosingBalance));

                    //double BankAmt = objReportProperty.NumberSet.ToDouble(xrCurrentBankBalance.Text);
                    //xrBankUnrealizedAmt.Text = objReportProperty.NumberSet.ToNumber(BankAmt + UnrealizedAmt);

                    //double BankTotalAmt = objReportProperty.NumberSet.ToDouble(xrBankUnrealizedAmt.Text);
                    //xrBankFinalAmt.Text = objReportProperty.NumberSet.ToNumber(BankTotalAmt - UnClearedAmt);
                }
                // resultArgs = GetBankClosingBalance();
                DataTable dtBankClosing = GetBankClosingBalance();
                double amount = 0;
                if (dtBankClosing != null && dtBankClosing.Rows.Count != 0)
                {
                    foreach (DataRow dr in dtBankClosing.Rows)
                    {
                        amount = objReportProperty.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                    }
                }
                string BankClosingBalance = amount.ToString();
                //xrCurrentBankBalance.Text = objReportProperty.NumberSet.ToNumber(this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateAsOn, BalanceSystem.LiquidBalanceGroup.BankBalance,
                //                   BalanceSystem.BalanceType.ClosingBalance));
                resultArgs = GetBRSCleared();
                DataView dvBRSCleared = resultArgs.DataSource.TableView;
                double reconciled = 0;
                double cleared = 0;
                if (dvBRSCleared != null && dvBRSCleared.Table.Rows.Count != 0)
                {
                    reconciled = objReportProperty.NumberSet.ToDouble(dvBRSCleared.ToTable().Compute("SUM(REALISED)", "").ToString());
                    cleared = objReportProperty.NumberSet.ToDouble(dvBRSCleared.ToTable().Compute("SUM(CLEARED)", "").ToString());
                }
                double temp =this.UtilityMember.NumberSet.ToDouble( BankClosingBalance) - UnrealizedAmt;
                double Balance = temp + UnClearedAmt;
                xrCurrentBankBalance.Text = objReportProperty.NumberSet.ToNumber(Balance);
                double BankAmt = objReportProperty.NumberSet.ToDouble(xrCurrentBankBalance.Text);
                xrBankUnrealizedAmt.Text = objReportProperty.NumberSet.ToNumber(BankAmt + UnrealizedAmt);

                double BankTotalAmt = objReportProperty.NumberSet.ToDouble(xrBankUnrealizedAmt.Text);
                xrBankFinalAmt.Text = objReportProperty.NumberSet.ToNumber(BankTotalAmt - UnClearedAmt);
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportSetup();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatement);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.BankReconcilationStatement,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.LedgalEntityId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BRStatement);
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }


        private ResultArgs GetBRSCleared()
        {
            try
            {
                string BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatementCleared);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.BankReconcilationStatementCleared,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.LedgalEntityId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BRStatement);
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private DataTable GetBankClosingBalance()
        {
            string BankClosingBalance = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankCurrentClosingBalance);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.BankCurrentClosingBalance,DataBaseType.HeadOffice)) 
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.LedgalEntityId);
                if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, "0");
                }
                DateTime dteClosingDate = this.ReportProperties.DateSet.ToDate(this.ReportProperties.DateAsOn, false);
                //string ClosingDate = dteClosingDate.AddDays(-1).ToShortDateString();
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, dteClosingDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankClosingBalance);
            }
            return resultArgs.DataSource.Table;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCapcode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapcode.Tag != null && xrCapcode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapcode.Tag.ToString());
            }
            else
            {
                xrCapcode.Tag = xrCapcode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapcode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            // xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;
            SetReportBorder();
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrTblRecord = SetBorders(xrTblRecord, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblGrandTotal = SetBorders(xrtblGrandTotal, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrTable = SetBorders(xrTable, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
        }
        #endregion

        private void xrUnRealized_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrUnRealized.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrUnRealized.Text = "";
            }
        }

        private void xrUncleared_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrUncleared.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrUncleared.Text = "";
            }
        }
    }
}
