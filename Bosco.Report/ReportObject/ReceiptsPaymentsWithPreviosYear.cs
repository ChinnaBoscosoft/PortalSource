using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ReceiptsPaymentsWithPreviosYear : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ReceiptsPaymentsWithPreviosYear()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            ReceiptsPaymentsWithPreviosYearReport();
            base.ShowReport();
        }
        private void ReceiptsPaymentsWithPreviosYearReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                this.ReportTitle = objReportProperty.ReportTitle;
                this.ReportSubTitle = objReportProperty.ProjectTitle;
                this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                xrCapReceiptPrevious.Text = xrCapPayPrevious.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.PREVIOUS);
                xrCapReceiptAmount.Text = xrCapPayAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);
                DataTable dtReceiptPaymentWithPrevious = GetReportSource();
                if (dtReceiptPaymentWithPrevious != null)
                {
                    this.DataSource = dtReceiptPaymentWithPrevious;
                    this.DataMember = dtReceiptPaymentWithPrevious.TableName;
                }
            }
            else
                ShowReportFilterDialog();
        }

        private DataTable GetReportSource()
        {
            try
            {
                string QueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ReceiptPaymentWithPrevious);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.ReceiptPaymentWithPrevious,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, QueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }
        #endregion
    }
}
