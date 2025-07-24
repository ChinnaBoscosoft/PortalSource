using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Report.Base;
namespace Bosco.Report.ReportObject
{
    public partial class CashBankPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double Debitamt = 0;
        DataTable dtPayments = new DataTable();
        SettingProperty settings = new SettingProperty();
        double CashBankReceiptsAmt = 0;
        #endregion

        #region Construnctor
        public CashBankPayments()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            BindReport();
            base.ShowReport();
        }
        public override void VoucherPrint()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            this.HideDateRange = false;
            BindReport();
            this.Print();
        }
        #endregion

        #region Methods
        private void BindReport()
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                // this.ReportTitle = objReportProperty.ReportTitle;
                // this.ReportSubTitle = objReportProperty.ProjectTitle;

                setHeaderTitleAlignment();
                //  this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
                if (!string.IsNullOrEmpty(objReportProperty.ProjectTitle))
                {
                    string[] projectName = objReportProperty.ProjectTitle.Split('-');
                    xrlblInstitudeName.Text = projectName[0];
                }
                //xrlblInstituteName.Text =  objReportProperty.InstituteName;
                //   objReportProperty.ProjectId = objReportProperty.CashBankProjectId.ToString();
                xrlblInstituteName.Text = this.GetInstituteName();
                xrlblInstituteAddress.Text = objReportProperty.LegalAddress;

                resultArgs = BindCashBankPayments();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "CashBankPayments";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
            else
            {
                xrlblInstitudeName.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();
            }
        }
        public ResultArgs BindCashBankPayments()
        {
            //string CashBankPayments = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.CashBankVoucher);
            //using (DataManager dataManager = new DataManager())
            //{
            //    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
            //    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
            //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            //    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankPayments);
            //}
            //return resultArgs;
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchcashBankByVoucher);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CashBankVoucher.FetchcashBankByVoucher,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                this.ReportProperties.PrintCashBankVoucherId = string.Empty;
            }
            return resultArgs;
        }

        private DataTable UpdateRupeeInWord(DataTable dataSource)
        {
            string ruppeeInWords = "";
            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                if (!dataSource.Columns.Contains("RUPPEE_AMT"))
                    dataSource.Columns.Add("RUPPEE_AMT");

                foreach (DataRow drRecord in dataSource.Rows)
                {
                    if (drRecord != null)
                    {
                        if (!string.IsNullOrEmpty(drRecord["AMOUNT"].ToString()))
                        {
                            ruppeeInWords = ConvertRuppessInWord.GetRupeesToWord(drRecord["AMOUNT"].ToString());
                            drRecord["RUPPEE_AMT"] = ruppeeInWords;
                        }
                    }
                }
            }
            return dataSource;
        }

        #endregion
        private void xttblLedgerAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CashBankReceiptsAmt = this.ReportProperties.NumberSet.ToDouble(xttblLedgerAmount.Text);
            xttblLedgerAmount.Text = objReportProperty.NumberSet.ToCurrency(CashBankReceiptsAmt);
            if (CashBankReceiptsAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xttblLedgerAmount.Text = "";
            }
        }

        private void xrtblAmtInWords_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ConvertRuppessInWord.GetRupeesToWord(Debitamt.ToString());
            e.Handled = true;
        }

        private void xrtblAmtInWords_SummaryReset_1(object sender, EventArgs e)
        {
            Debitamt = 0;
        }

        private void xrtblAmtInWords_SummaryRowChanged_1(object sender, EventArgs e)
        {
            Debitamt += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
        }
    }
}
