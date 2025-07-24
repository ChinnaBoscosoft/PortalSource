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
    public partial class GeneralateLoss : Report.Base.ReportBase
    {
        private DataTable data { get; set; }
        public double sumnext { get; set; }

        private bool isGBVerification
        {
            get
            {
                return (this.ReportProperties.ReportId == "RPT-078");
            }
        }

        DataTable dtLossConLedgerDetails = new DataTable();
        private DataTable dtPLLossConDetails
        {
            set
            {
                dtLossConLedgerDetails = value;
            }
            get
            {
                return dtLossConLedgerDetails;
            }
        }

        public GeneralateLoss()
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

        public void BindGeneralateLoss(DataTable dtGeneralateLoss, DataTable dtLossDetails)
        {
            if (dtGeneralateLoss != null)
            {
                dtPLLossConDetails = dtLossDetails;
                xrsubPLConLedgerDetails.Visible = isGBVerification;
                dtGeneralateLoss.TableName = "CongiregationProfitandLoss";
                this.DataSource = dtGeneralateLoss;
                this.DataMember = dtGeneralateLoss.TableName;
            }
        }

        //private ResultArgs GetReportSource()
        //{
        //    ResultArgs resultArgs = null;
        //    string sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.Loss);
        //    using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.Loss, DataBaseType.HeadOffice))
        //    {
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);

        //        if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
        //            dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
        //    }
        //    return resultArgs;
        //}
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

        private void xrConLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            ShowUnmappedLedgers(cell, false);
        }

        private void xrConParentGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            ShowUnmappedLedgers(cell, true);
        }

        private void ShowUnmappedLedgers(XRTableCell cell, bool isParent)
        {
            if (GetCurrentColumnValue(reportSetting1.CongiregationProfitandLoss.CON_LEDGER_NAMEColumn.ColumnName) != null)
            {
                string conledgername = cell.Text;
                if (String.IsNullOrEmpty(conledgername))
                {
                    cell.Text = (isParent ? "Unmapped Ledger Group" : "Unmapped Ledger");
                    if (isParent)
                    {
                        cell.Font = new Font(cell.Font, FontStyle.Italic | FontStyle.Bold);
                    }
                    else
                    {
                        cell.Font = new Font(cell.Font, FontStyle.Italic);
                    }
                }
                else
                {
                    cell.Font = isParent ? new Font(cell.Font, FontStyle.Bold) : new Font(cell.Font, FontStyle.Regular);
                }

                //Make bold generalte ledger group for verificaiton
                if (isGBVerification && !isParent)
                {
                    double amt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
                    cell.Font = new Font(cell.Font, FontStyle.Regular);
                    if (amt != 0)
                    {
                        cell.Font = new Font(cell.Font, FontStyle.Bold);
                    }
                }
            }

        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            cell.Text = cell.Text.ToUpper();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrsubPLConLedgerDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !isGBVerification;

            GeneralateBalanceSheetDetail LossConLedgerDetails = xrsubPLConLedgerDetails.ReportSource as GeneralateBalanceSheetDetail;
            if (xrsubPLConLedgerDetails.Visible && GetCurrentColumnValue("CON_LEDGER_ID") != null
                && dtPLLossConDetails != null && dtPLLossConDetails.Rows.Count > 0)
            {
                Int32 conledgerid = this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CON_LEDGER_ID").ToString());
                Int32 ProjectMainCategoryid = this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("PROJECT_CATOGORY_GROUP_ID").ToString());
                dtPLLossConDetails.DefaultView.RowFilter = "";
                dtPLLossConDetails.DefaultView.RowFilter = "PROJECT_CATOGORY_GROUP_ID = " + ProjectMainCategoryid + " AND CON_LEDGER_ID = " + conledgerid;
                DataTable dtGBConLedger = dtPLLossConDetails.DefaultView.ToTable();
                LossConLedgerDetails.BindGBConLedgerDetail(dtGBConLedger);
                LossConLedgerDetails.TitleColumnWidth = xrConLedgerName.WidthF;
                LossConLedgerDetails.AmountColumnWidth = xrConLedgerSum.WidthF;
                LossConLedgerDetails.FooterBorderWidth = xrConLedgerName.WidthF + xrConLedgerSum.WidthF;
                LossConLedgerDetails.HideGBConLedgerDetailHeaders();
            }
            else
            {
                LossConLedgerDetails.Visible = false;
            }
        }

        private void xrTable2_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("CON_LEDGER_NAME") != null)
            {
                XRTable tblledgername = sender as XRTable;
                tblledgername.BackColor = Color.Transparent;

                if (isGBVerification)
                {
                    tblledgername.BackColor = Color.DarkGray;
                }
            }
        }

        private void xrConLedgerSum_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Make bold generalte ledger group for verificaiton
            if (isGBVerification && GetCurrentColumnValue("AMOUNT") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                double amt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
                cell.Font = new Font(cell.Font, FontStyle.Regular);
                if (amt != 0)
                {
                    cell.Font = new Font(cell.Font, FontStyle.Bold);
                }
            }
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
