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
    public partial class GeneralateBalanceSheetLiability : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        SettingProperty settingProperty = new SettingProperty();
        #endregion

        #region Constructor
        public GeneralateBalanceSheetLiability()
        {
            InitializeComponent();
        }
        #endregion

        #region Property

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

        string yearFrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearFrom = settingProperty.YearFrom;
                return yearFrom;
            }
        }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }
        public double TotalLiabilities { get; set; }
        public double OpTotalLiabilities { get; set; }

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

        #endregion

        #region Methods
        public void HideBalanceSheetLiabilityHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindBalanceSheetLiability(DataTable dtLiabilityLedgers, DataTable dtGBLiabilityDetails)
        {
            try
            {
                GBConLedgerRptSourceDetails = dtGBLiabilityDetails;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;

                DateTime dtDateFrom = Convert.ToDateTime(YearFrom);
                string YearFromReducing = dtDateFrom.AddDays(-1).ToShortDateString();

                this.SetLandscapeHeader = 1030.25f;
                this.SetLandscapeFooter = 1030.25f;
                this.SetLandscapeFooterDateWidth = 860.00f;

                SetReportTitle();
                setHeaderTitleAlignment();

                if (dtLiabilityLedgers != null)
                {
                    TotalLiabilities = this.UtilityMember.NumberSet.ToDouble(dtLiabilityLedgers.Compute("SUM(AMOUNT)", string.Empty).ToString());
                    OpTotalLiabilities = this.UtilityMember.NumberSet.ToDouble(dtLiabilityLedgers.Compute("SUM(OP_AMOUNT)", string.Empty).ToString());
                    dtLiabilityLedgers.TableName = "BalanceSheet";
                    this.DataSource = dtLiabilityLedgers;
                    this.DataMember = dtLiabilityLedgers.TableName;

                    //For Con Ledger Details
                    xrsubGBConLedgerDetails.Visible = IsGBVerificatinReport;

                }

                SortByLedgerorGroup();

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

        private void tcLiabilityGrpGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            ShowUnmappedLedgers(cell, true);
        }

        private void tcLiabilityLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            ShowUnmappedLedgers(cell, false);
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
                //AssetConLedgerDetails.Visible = (dtGBConLedger.Rows.Count > 0);
                AssetConLedgerDetails.BindGBConLedgerDetail(dtGBConLedger);
                AssetConLedgerDetails.TitleColumnWidth = tcLiabilityLedgerName.WidthF + xrLiabilityOpening.WidthF;
                AssetConLedgerDetails.AmountColumnWidth = xrtcLiabilityLedgerAmt.WidthF;
                AssetConLedgerDetails.FooterBorderWidth = tcLiabilityLedgerName.WidthF + xrLiabilityOpening.WidthF + xrtcLiabilityLedgerAmt.WidthF;
                AssetConLedgerDetails.HideGBConLedgerDetailHeaders();

            }
            else
            {
                AssetConLedgerDetails.Visible = false;
            }
        }

        private void xrsubGBConLedgerDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !IsGBVerificatinReport;
        }

        private void tcNewLiabilityGrpGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            ShowUnmappedLedgers(cell, true);
        }

        private void xrtcLiabilityLedgerAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Make bold generalte ledger group for verificaiton
            XRTableCell cell = sender as XRTableCell;
            if (GetCurrentColumnValue("PARENT_CON_CODE") != null)
            {
                if (IsGBVerificatinReport)
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

        private void xrTblLiabilityLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PARENT_CON_CODE") != null)
            {
                XRTable tblledgername = sender as XRTable;
                string condledgerParetnsCode = GetCurrentColumnValue("PARENT_CON_CODE").ToString();
                double amt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
                tblledgername.BackColor = Color.Transparent;
                if (amt != 0)
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