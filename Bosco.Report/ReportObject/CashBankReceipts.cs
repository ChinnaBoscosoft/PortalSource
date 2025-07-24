using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
namespace Bosco.Report.ReportObject
{
    public partial class CashBankReceipts : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        double Creditamt = 0;
        double CashBankReceiptsAmt = 0;
        #endregion

        #region Constructor
        public CashBankReceipts()
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
            BindReport();
            this.Print();
        }
        #endregion

        #region Method
        private void BindReport()
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                // this.ReportTitle = objReportProperty.ReportTitle;
                //  this.ReportSubTitle = objReportProperty.ProjectTitle;
                setHeaderTitleAlignment();

                if (!string.IsNullOrEmpty(objReportProperty.ProjectTitle))
                {
                    string[] projectName = objReportProperty.ProjectTitle.Split('-');
                    xrlblReceiptInsName.Text = projectName[0];
                }
                //xrlblInstituteName.Text =  objReportProperty.InstituteName;
                //   objReportProperty.ProjectId = objReportProperty.CashBankProjectId.ToString();
                xrlblInsName.Text = (string.IsNullOrEmpty(this.GetInstituteName())?settings.InstituteName: this.GetInstituteName());
                xrlblInsAddress.Text = (string.IsNullOrEmpty(objReportProperty.LegalAddress) ? settings.Address : objReportProperty.LegalAddress);


                //this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
                // xrlblReceiptInsName.Text = settings.InstituteName;
                resultArgs = BindCashBankReceipts();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "CashBankReceipts";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
            else
            {
                xrlblReceiptInsName.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();
            }
        }
        public ResultArgs BindCashBankReceipts()
        {
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchcashBankByVoucher);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CashBankVoucher.FetchcashBankByVoucher,DataBaseType.HeadOffice))
            {
                //dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                this.ReportProperties.PrintCashBankVoucherId = string.Empty;

            }
            return resultArgs;
        }


        #endregion

        private void xrtblAmtInWords_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ConvertRuppessInWord.GetRupeesToWord(Creditamt.ToString());
            e.Handled = true;
        }

        private void xrtblAmtInWords_SummaryReset(object sender, EventArgs e)
        {
            Creditamt = 0;
        }

        private void xrtblAmtInWords_SummaryRowChanged(object sender, EventArgs e)
        {
            Creditamt += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
        }

        private void xrtblProFundCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CashBankReceiptsAmt = this.ReportProperties.NumberSet.ToDouble(xrtblProFundCode.Text);
            xrtblProFundCode.Text= objReportProperty.NumberSet.ToCurrency(CashBankReceiptsAmt);
            if (CashBankReceiptsAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrtblProFundCode.Text = "";
            }
        }
    }
}
