using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateBalanceSheetLiabilities : Report.Base.ReportBase
    {
        private DataTable data { get; set; }
        public double sumnext { get; set; }

        public GeneralateBalanceSheetLiabilities()
        {
            InitializeComponent();
        }

        #region Methods
        //public void FetchLoss()
        //{
        //    ResultArgs resultArgs = GetReportSource();
        //    if (resultArgs.Success)
        //    {
        //        data = resultArgs.DataSource.Table;
        //        DataView dvLedgers = resultArgs.DataSource.Table.AsDataView();

        //        if (dvLedgers != null)
        //        {
        //            dvLedgers.Table.AcceptChanges();
        //            sumnext = UtilityMember.NumberSet.ToDouble(data.Compute("SUM(NXTPAYMENT)", "").ToString());
        //            dvLedgers.Table.TableName = "CongiregationProfitandLoss";
        //            this.DataSource = dvLedgers;
        //            this.DataMember = dvLedgers.Table.TableName;
        //        }
        //    }

        //}

        public void BindGeneralateLoss(DataTable dtGeneralateLoss)
        {
            if (dtGeneralateLoss != null)
            {
                dtGeneralateLoss.TableName = "CongiregationProfitandLoss";
                this.DataSource = dtGeneralateLoss;
                this.DataMember = dtGeneralateLoss.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.Loss);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.Loss, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);

                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
            }
            return resultArgs;
        }
        #endregion

        private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //int mainid = 0;
            //int ledgerid = 0;

            //mainid = GetCurrentColumnValue(reportSetting1.CongiregationProfitandLoss.CON_MAIN_PARENT_IDColumn.ColumnName) == null ? 0
            //    : UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.CongiregationProfitandLoss.CON_MAIN_PARENT_IDColumn.ColumnName).ToString());

            //ledgerid = GetCurrentColumnValue(reportSetting1.CongiregationProfitandLoss.CON_LEDGER_IDColumn.ColumnName) == null ? 0
            //    : UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.CongiregationProfitandLoss.CON_LEDGER_IDColumn.ColumnName).ToString());

            //if (mainid == ledgerid)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    e.Cancel = false;
            //}
        }

        //private void xrGrpSum_PrintOnPage(object sender, PrintOnPageEventArgs e)
        //{
        //    XRTableCell cell = sender as XRTableCell;
        //    if (cell!=null)
        //    {
        //        double grpSum = UtilityMember.NumberSet.ToDouble(cell.Text);
        //        cell.Text = UtilityMember.NumberSet.ToNumber( Math.Abs(grpSum));
        //    }
        //}

        //private void xrConLedgerSum_PrintOnPage(object sender, PrintOnPageEventArgs e)
        //{
        //    XRTableCell cell = sender as XRTableCell;
        //    if (cell != null)
        //    {
        //        double ledgerSum = UtilityMember.NumberSet.ToDouble(cell.Text);
        //        cell.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(ledgerSum));
        //    }
        //}
    }
}
