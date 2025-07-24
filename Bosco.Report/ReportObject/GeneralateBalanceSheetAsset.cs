using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateBalanceSheetAsset : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        double CashBankTotalCL = 0;
        double CashBankTotalOpening = 0;
        double InventoryCL = 0;
        double InventoryOpening = 0;
        #endregion

        #region Constructor
        public GeneralateBalanceSheetAsset()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        string yearFrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearFrom = settingProperty.YearFrom;
                return yearFrom;
            }
        }
        public double TotalAssets { get; set; }
        public double OPTotalAssets { get; set; }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }

        private DataTable gbConLedgerRptSourceDetails = new DataTable();
        private DataTable GBConLedgerRptSourceDetails
        {
            get
            {
                return gbConLedgerRptSourceDetails;
            }
            set
            {
                gbConLedgerRptSourceDetails = value;
            }
        }

        private bool IsGBVerificatinReport
        {
            get
            {
                return (this.ReportProperties.ReportId == "RPT-077");
            }
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            base.ShowReport();

        }
        #endregion

        #region Events
        private void xrOpeningAssetAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningAsset = this.ReportProperties.NumberSet.ToDouble(xrOpeningAssetAmount.Text);
            if (OpeningAsset != 0)
            {
                //e.Cancel = false;
            }
            else
            {
                //xrOpeningAssetAmount.Text = "";
            }
        }

        private void grpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    //e.Cancel = true;
                }
            }
        }

        #endregion

        #region Methods
        public void HideBalanceSheetAssetCapHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindBalanceSheetAsset(DataTable dtAssetLedgers, DataTable dtGBAssetDetails)
        {
            try
            {
                GBConLedgerRptSourceDetails = dtGBAssetDetails;

                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                DateTime dtDateFrom = Convert.ToDateTime(YearFrom);
                string YearFromReducing = dtDateFrom.AddDays(-1).ToShortDateString();

                this.SetLandscapeHeader = 1030.25f;
                this.SetLandscapeFooter = 1030.25f;
                this.SetLandscapeFooterDateWidth = 860.00f;

                SetReportTitle();
                setHeaderTitleAlignment();
                if (dtAssetLedgers != null)
                {
                    TotalAssets = this.UtilityMember.NumberSet.ToDouble(dtAssetLedgers.Compute("SUM(AMOUNT)", string.Empty).ToString());
                    OPTotalAssets = this.UtilityMember.NumberSet.ToDouble(dtAssetLedgers.Compute("SUM(OP_AMOUNT)", string.Empty).ToString());

                    CashBankTotalCL = this.UtilityMember.NumberSet.ToDouble(dtAssetLedgers.Compute("SUM(AMOUNT)", "PARENT_CON_CODE IN ('A.4.1')").ToString());
                    CashBankTotalOpening = this.UtilityMember.NumberSet.ToDouble(dtAssetLedgers.Compute("SUM(OP_AMOUNT)", "PARENT_CON_CODE IN ('A.4.1')").ToString());

                    InventoryCL = this.UtilityMember.NumberSet.ToDouble(dtAssetLedgers.Compute("SUM(AMOUNT)", "PARENT_CON_CODE IN ('A.4.3')").ToString());
                    InventoryOpening = this.UtilityMember.NumberSet.ToDouble(dtAssetLedgers.Compute("SUM(OP_AMOUNT)", "PARENT_CON_CODE IN ('A.4.3')").ToString());

                    dtAssetLedgers.TableName = "BalanceSheet";
                    this.DataSource = dtAssetLedgers;
                    this.DataMember = dtAssetLedgers.TableName;
                }
                SortByLedgerorGroup();
                xrTblAssetLedgerName = AlignContentTable(xrTblAssetLedgerName);
                                
                Detail.Visible = (dtAssetLedgers.Rows.Count > 0);

                //For Con Ledger Details
                this.ReportProperties.ShowDetailedBalance = IsGBVerificatinReport ? 1 : 0;
                xrsubGBConLedgerDetails.Visible = (this.ReportProperties.ShowDetailedBalance == 1);

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        private void SortByLedgerorGroup()
        {

            if (grpMaster.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpMaster.SortingSummary.Enabled = true;
                    grpMaster.SortingSummary.FieldName = "CON_CODE";
                    grpMaster.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpMaster.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpMaster.SortingSummary.Enabled = true;
                    grpMaster.SortingSummary.FieldName = "CON_CODE";
                    grpMaster.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpMaster.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
            //if (grpConLedger.Visible)
            //{
            //    if (this.ReportProperties.SortByLedger == 0)
            //    {
            //        grpConLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpConLedger.SortingSummary.FieldName = "SORT_ORDER";
            //            grpConLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //        }
            //        else
            //        {
            //            grpConLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //        }
            //        grpConLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpConLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //    else
            //    {
            //        grpConLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpConLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
            //            grpConLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        else
            //        {
            //            grpConLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        grpConLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpConLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //}
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
                if (IsGBVerificatinReport && !isParent)
                {
                    double amt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
                    string condledgerParetnsCode = GetCurrentColumnValue("PARENT_CON_CODE").ToString();
                    cell.Font = new Font(cell.Font, FontStyle.Regular);
                    if (condledgerParetnsCode != "A.4.1" && amt != 0)
                    {
                        cell.Font = new Font(cell.Font, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion

        private void tcAssetGrpGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            ShowUnmappedLedgers(cell, true);
        }

        private void tcAssetLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            ShowUnmappedLedgers(cell, false);
        }

        private void xrsubGBConLedgerDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = true;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GeneralateBalanceSheetDetail AssetConLedgerDetails = xrsubGBConLedgerDetails.ReportSource as GeneralateBalanceSheetDetail;
            if (xrsubGBConLedgerDetails.Visible && GetCurrentColumnValue("CON_LEDGER_ID") != null
                && GBConLedgerRptSourceDetails != null && GBConLedgerRptSourceDetails.Rows.Count > 0)
            {
                Int32 conledgerid = this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CON_LEDGER_ID").ToString());
                GBConLedgerRptSourceDetails.DefaultView.RowFilter = "";
                GBConLedgerRptSourceDetails.DefaultView.RowFilter = "CON_LEDGER_ID = " + conledgerid;
                DataTable dtGBConLedger = GBConLedgerRptSourceDetails.DefaultView.ToTable();
                AssetConLedgerDetails.BindGBConLedgerDetail(dtGBConLedger);
                AssetConLedgerDetails.TitleColumnWidth = tcAssetGrpGroupName.WidthF + xrOpeningTotalAssetAmount.WidthF;
                AssetConLedgerDetails.AmountColumnWidth = xrtblTransDebit.WidthF;
                AssetConLedgerDetails.FooterBorderWidth = tcAssetGrpGroupName.WidthF + xrOpeningTotalAssetAmount.WidthF + xrtblTransDebit.WidthF;
                AssetConLedgerDetails.HideGBConLedgerDetailHeaders();
            }
            else
            {
                AssetConLedgerDetails.Visible = false;
            }
        }

        private void xrOpeningTotalAssetAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrOpeningTotalAssetAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("PARENT_CON_CODE") != null)
            {
                string condledgerParetnsCode = GetCurrentColumnValue("PARENT_CON_CODE").ToString();

                if (condledgerParetnsCode.ToUpper() == "A.4")
                {
                    double wkfundTotal = 0;
                    for (int i = 0; i <= e.CalculatedValues.Count - 1; i++)
                    {
                        wkfundTotal += UtilityMember.NumberSet.ToDouble(e.CalculatedValues[i].ToString());
                    }
                    wkfundTotal += (CashBankTotalOpening + InventoryOpening);
                    e.Result = wkfundTotal;
                    e.Handled = true;
                }
            }
        }

        private void xrtblTransDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("PARENT_CON_CODE") != null)
            {
                string condledgerParetnsCode = GetCurrentColumnValue("PARENT_CON_CODE").ToString();

                if (condledgerParetnsCode.ToUpper() == "A.4")
                {
                    double wkfundTotal = 0;
                    for (int i = 0; i <= e.CalculatedValues.Count - 1; i++)
                    {
                        wkfundTotal += UtilityMember.NumberSet.ToDouble(e.CalculatedValues[i].ToString());
                    }
                    wkfundTotal += (CashBankTotalCL + InventoryCL);
                    e.Result = wkfundTotal;
                    e.Handled = true;
                }
            }
        }

        private void xrsubGBConLedgerDetails_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!IsGBVerificatinReport)
            {
                e.Cancel = true;
            }
            else
            {
                //if (GetCurrentColumnValue("PARENT_CON_CODE") != null)
                //{
                //    string condledgerParetnsCode = GetCurrentColumnValue("PARENT_CON_CODE").ToString();

                //    if (condledgerParetnsCode.ToUpper() == "A.4.1")
                //    {
                //        e.Cancel = true;
                //    }
                //}
            }
        }

        private void tcAssetLedgerAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Make bold generalte ledger group for verificaiton
            XRTableCell cell = sender as XRTableCell;
            if (IsGBVerificatinReport )
            {
                double amt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
                string condledgerParetnsCode = GetCurrentColumnValue("PARENT_CON_CODE").ToString();
                cell.Font = new Font(cell.Font, FontStyle.Regular);
                if (condledgerParetnsCode != "A.4.1" && amt != 0)
                {
                    cell.Font = new Font(cell.Font, FontStyle.Bold);
                }
            }
        }

        private void xrTblAssetLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PARENT_CON_CODE") != null)
            {
                XRTable tblledgername = sender as XRTable;
                string condledgerParetnsCode = GetCurrentColumnValue("PARENT_CON_CODE").ToString();
                double amt =  UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
                tblledgername.BackColor = Color.Transparent;
                if (condledgerParetnsCode.ToUpper() != "A.4.1" && amt!=0)
                {
                    if (IsGBVerificatinReport)
                    {
                        tblledgername.BackColor = Color.DarkGray;
                    }
                }
            }
        }
    }
}

