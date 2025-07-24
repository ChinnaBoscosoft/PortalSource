using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ReceiptsPayments : Bosco.Report.Base.ReportHeaderBase
    {
        SettingProperty settingProperty = new SettingProperty();

        #region Decelartion

        double ReceiptsAmt = 0;
        double PaymentsAmt = 0;
        double ReceiptOPAmt = 0;
        double PaymentClAmt = 0;
        #endregion

        #region Constructor
        public ReceiptsPayments()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            BindReceiptSource();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private ResultArgs SetReportReceiptSource()
        {
            ResultArgs resultArgs = null;
            string sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.ReceiptsPayments);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.ReceiptsPayments,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager,DAO.Data.DataSource.DataView, sqlReceipts);
            }
            return resultArgs;
        }

        public void BindReceiptSource()
        {
            this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            xrCapPaymentAmount.Text = this.SetCurrencyFormat("Amount");
            xrCapAmount.Text = this.SetCurrencyFormat("Amount");
            this.ReportSubTitle = objReportProperty.ProjectTitle;
            setHeaderTitleAlignment();
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                ResultArgs resultArgs = SetReportReceiptSource();
                DataView dvReceiptsPayments = resultArgs.DataSource.TableView;

                if (dvReceiptsPayments != null)
                {
                    dvReceiptsPayments.Table.TableName = "FinalReceiptsPayments";
                    this.DataSource = dvReceiptsPayments;
                    this.DataMember = dvReceiptsPayments.Table.TableName;
                }
                AccountBalance accountBalance = xrSubReports.ReportSource as AccountBalance;


                grpLedgerGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);

                if (grpLedgerGroup.Visible == false && grpLedger.Visible == false)
                {
                    grpLedger.Visible = true;
                }

                if (grpLedgerGroup.Visible)
                {
                    if (ReportProperties.SortByGroup == 1)
                    {
                        grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.FinalReceiptsPayments.LEDGER_GROUPColumn.ColumnName;
                        grpLedgerGroup.GroupFields[1].FieldName = reportSetting1.FinalReceiptsPayments.PAYMENT_LEDGER_NAMEColumn.ColumnName;
                    }
                    else
                    {
                        grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.FinalReceiptsPayments.LEDGER_GROUPColumn.ColumnName;
                        grpLedgerGroup.GroupFields[1].FieldName = reportSetting1.FinalReceiptsPayments.PAYMENT_GROUP_NAMEColumn.ColumnName;
                    }
                }
                if (grpLedger.Visible)
                {
                    if (ReportProperties.SortByLedger == 1)
                    {
                        grpLedger.GroupFields[0].FieldName = reportSetting1.FinalReceiptsPayments.LEDGER_NAMEColumn.ColumnName;
                    }
                    else
                    {
                        grpLedger.GroupFields[0].FieldName = reportSetting1.FinalReceiptsPayments.SORT_IDColumn.ColumnName;
                    }
                }
                accountBalance.BindBalance(true, false);
                ReceiptOPAmt = accountBalance.PeriodBalanceAmount;

                AccountBalance accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalance;

                accountClosingBalance.BindBalance(false, false);
                PaymentClAmt = accountClosingBalance.PeriodBalanceAmount;
            }
        }
        #endregion

        #region Events
        private void xrReceiptAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ReceiptsAmt + ReceiptOPAmt;
            e.Handled = true;
        }

        private void xrReceiptAmt_SummaryReset(object sender, EventArgs e)
        {
            ReceiptsAmt = PaymentsAmt = 0;
        }

        private void xrReceiptAmt_SummaryRowChanged(object sender, EventArgs e)
        {
            ReceiptsAmt += this.ReportProperties.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
            PaymentsAmt += this.ReportProperties.NumberSet.ToDouble(GetCurrentColumnValue("PAYMENT_AMOUNT").ToString());
        }
        private void xrPaymentAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PaymentsAmt + PaymentClAmt;
            e.Handled = true;
        }
        #endregion
    }
}


