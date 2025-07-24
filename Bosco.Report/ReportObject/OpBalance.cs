using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Report.Base;


namespace Bosco.Report.ReportObject
{
    public partial class OpBalance : Report.Base.ReportBase
    {
        //ResultArgs resultArgs = null;

        public OpBalance()
        {
            InitializeComponent();
            //this.AttachDrillDownToRecord(xrtblOpBalance, xrType,"GROUP_ID",ReportProperty.DrillDownType.GROUP_SUMMARY);
        }

        /*public override void ShowReport()
        {
           // BindCashBankOpBalance();
            base.ShowReport();
        }

        public ResultArgs FetchOpeningBalance(TransType transType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpeningBalance))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECTColumn, this.ReportProperties.Project);

                if (transType == TransType.RC)
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateTo);
                }

                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public void BindCashBankOpBalance(TransType transType)
        {
            this.dicSupressLedgerCode.Clear();
            //this.AddSuppresLedgerCode(xrtblOpBalance, xrlblOPCode);
            resultArgs = FetchOpeningBalance(transType);

            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                this.DataSource = resultArgs.DataSource.Table;
                this.DataMember = resultArgs.DataSource.Table.TableName;
            }
        }

        private void xrtblOpBalance_PreviewClick(object sender, PreviewMouseEventArgs e)
        {
            
            MessageRender.ShowMessage(sender.GetType().Name);
        }

        private void xrType_PreviewClick(object sender, PreviewMouseEventArgs e)
        {

        }

        private void xrtblOpBalance_PreviewDoubleClick(object sender, PreviewMouseEventArgs e)
        {

        }

        private void xrType_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Tag = GetCurrentRow();
        }*/
    }
}
