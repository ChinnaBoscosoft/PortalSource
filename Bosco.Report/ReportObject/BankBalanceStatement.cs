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
using DevExpress.XtraSplashScreen;
namespace Bosco.Report.ReportObject
{
    public partial class BankBalanceStatement : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        string transMode = "";
        double crbalanceAmt = 0;
        double drbalanceAmt = 0;
        double crTotalAmt = 0;
        double drTotalAmt = 0;
        #endregion

        #region Constructor
        public BankBalanceStatement()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrtblBankBalance, xrBankAccount,
                new ArrayList { this.ReportParameters.LEDGER_IDColumn.ColumnName, this.ReportParameters.DATE_AS_ONColumn.ColumnName },
                DrillDownType.LEDGER_SUMMARY, false);
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindBankBalanceStatement();
        }

        #endregion

        #region Method
        private void BindBankBalanceStatement()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateAsOn)
                && !string.IsNullOrEmpty(this.ReportProperties.Project)
                && this.ReportProperties.Ledger != "0" && !string.IsNullOrEmpty(this.ReportProperties.Ledger))
            {
                crbalanceAmt = crTotalAmt = drbalanceAmt = drTotalAmt = 0;
                xrCapAmount.Text = this.SetCurrencyFormat(xrCapAmount.Text);
                SetReportTitle();
                // this.ReportTitle = ReportProperty.Current.ReportTitle;
                setHeaderTitleAlignment();
                // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                resultArgs = GetReportSource();
                DataView dvBankReconciliation = resultArgs.DataSource.TableView;
                if (dvBankReconciliation != null && dvBankReconciliation.Count != 0)
                {
                    foreach (DataRowView drvBalance in dvBankReconciliation)
                    {
                        drvBalance.BeginEdit();
                        transMode = drvBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();

                        if (transMode == TransactionMode.CR.ToString())
                        {
                            crbalanceAmt = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                            crTotalAmt += crbalanceAmt;
                        }
                        else
                        {
                            drbalanceAmt = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                            drTotalAmt += drbalanceAmt;
                        }
                        drvBalance.EndEdit();
                    }

                }
                if (dvBankReconciliation != null && dvBankReconciliation.Count != 0)
                {
                    dvBankReconciliation.Table.TableName = "BankBalanceStatement";
                    this.DataSource = dvBankReconciliation;
                    this.DataMember = dvBankReconciliation.Table.TableName;
                }

                base.ShowReport();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string BankBalanceStatements = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankBalanceStatement);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.BankBalanceStatement, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, "0");
                    }
                    DateTime dteBankbalance = this.ReportProperties.DateSet.ToDate(this.ReportProperties.DateAsOn, false);

                    if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }

                    string BankBalance = dteBankbalance.ToShortDateString();
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, BankBalance);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BankBalanceStatements);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblBankBalance = AlignContentTable(xrtblBankBalance);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

        }
        #endregion

        private void xrAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = "";
            e.Result = (drTotalAmt > crTotalAmt) ? drTotalAmt - crTotalAmt : crTotalAmt - drTotalAmt;
            e.Handled = true;
        }

        private void xrGrandTransMode_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = drTotalAmt > crTotalAmt ? MessageCatalog.ReportCommonTitle.DR : MessageCatalog.ReportCommonTitle.CR;
            e.Handled = true;
        }

        private void xrTotalAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double bankBalanceStatement = this.ReportProperties.NumberSet.ToDouble(xrTotalAmount.Text);
            if (bankBalanceStatement != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTotalAmount.Text = "";
                xrTransMode.Text = "";
            }
        }
    }
}
