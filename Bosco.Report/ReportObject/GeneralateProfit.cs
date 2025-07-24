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
    public partial class GeneralateProfit : Report.Base.ReportBase
    {
        private DataTable data { get; set; }
        public double sumnxt { get; set; }

        private bool isGBVerification
        {
            get
            {
                return (this.ReportProperties.ReportId == "RPT-078");
            }
        }

        DataTable dtIncomeConLedgerDetails = new DataTable();
        private DataTable dtPLIncomeConDetails
        {
            set
            {
                dtIncomeConLedgerDetails = value;
            }
            get
            {
                return dtIncomeConLedgerDetails;
            }
        }


        public GeneralateProfit()
        {
            InitializeComponent();
        }

        #region Methods
        //public void FetchProfit()
        //{
        //    ResultArgs resultArgs = GetReportSource();
        //    if (resultArgs.Success && resultArgs.DataSource != null)
        //    {
        //        DataView dvLedgers = resultArgs.DataSource.Table.AsDataView();
        //        data = resultArgs.DataSource.Table;
        //        if (dvLedgers != null)
        //        {
        //            sumnxt = UtilityMember.NumberSet.ToDouble(data.Compute("SUM(NXTRECEIPT)", "").ToString());
        //            dvLedgers.Table.TableName = "CongiregationProfitandLoss";
        //            this.DataSource = dvLedgers;
        //            this.DataMember = dvLedgers.Table.TableName;
        //        }
        //    }
        //}

        public void BindGeneralateProfit(DataTable dtGeneralateProfit, DataTable dtIncomeDetails)
        {
            if (dtGeneralateProfit != null)
            {
                dtPLIncomeConDetails = dtIncomeDetails;
                xrsubPLConLedgerDetails.Visible = isGBVerification;

                xrConLedgerName.Font = new System.Drawing.Font(xrConLedgerName.Font, FontStyle.Regular);
                dtGeneralateProfit.TableName = "CongiregationProfitandLoss";
                this.DataSource = dtGeneralateProfit;
                this.DataMember = dtGeneralateProfit.TableName;
            }
        }

        //private ResultArgs GetReportSource()
        //{
        //    ResultArgs resultArgs = null;
        //    string sqlProfit = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.Profit);
        //    using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.Profit, DataBaseType.HeadOffice))
        //    {
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);

        //        if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
        //            dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

        //        resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlProfit);
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

        private void grpCongregatoion_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void xrLedgerSum_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Make bold generalte ledger group for verificaiton
            if (isGBVerification && GetCurrentColumnValue("AMOUNT")!=null)
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

        private void xrsubPLConLedgerDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !isGBVerification;

            GeneralateBalanceSheetDetail LossConLedgerDetails = xrsubPLConLedgerDetails.ReportSource as GeneralateBalanceSheetDetail;
            if (xrsubPLConLedgerDetails.Visible && GetCurrentColumnValue("CON_LEDGER_ID") != null
                && dtPLIncomeConDetails != null && dtPLIncomeConDetails.Rows.Count > 0)
            {
                Int32 conledgerid = this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CON_LEDGER_ID").ToString());
                Int32 ProjectMainCategoryid = this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("PROJECT_CATOGORY_GROUP_ID").ToString());
                dtPLIncomeConDetails.DefaultView.RowFilter = "";
                dtPLIncomeConDetails.DefaultView.RowFilter = "PROJECT_CATOGORY_GROUP_ID = " + ProjectMainCategoryid + " AND CON_LEDGER_ID = " + conledgerid;
                DataTable dtGBConLedger = dtPLIncomeConDetails.DefaultView.ToTable();
                LossConLedgerDetails.BindGBConLedgerDetail(dtGBConLedger);
                LossConLedgerDetails.TitleColumnWidth = xrConLedgerName.WidthF;
                LossConLedgerDetails.AmountColumnWidth = xrLedgerSum.WidthF;
                LossConLedgerDetails.FooterBorderWidth = xrConLedgerName.WidthF + xrLedgerSum.WidthF;
                LossConLedgerDetails.HideGBConLedgerDetailHeaders();
            }
            else
            {
                LossConLedgerDetails.Visible = false;
            }
        }
    }
}
