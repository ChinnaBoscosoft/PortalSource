using System;
using System.Data;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreCashBankBook : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int count = 0;
        int ccCount = 0;
        #endregion

        #region Constructor
        public CostCentreCashBankBook()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblBindSource, xrCosBank,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false);
            this.AttachDrillDownToRecord(xrtblBindSource, xrCosPayments,
                new ArrayList { this.ReportParameters.PAY_VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            count = 0;
            ccCount = 0;
            CashAndBankBook();
            base.ShowReport();
        }

        private void CashAndBankBook()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                setHeaderTitleAlignment();
                SetReportTitle();
                this.SetLandscapeHeader = 1145.25f;
                this.SetLandscapeFooter = 1145.25f;
                this.SetLandscapeFooterDateWidth = 979.00f;
                // this.SetLandscapeCostCentreWidth = 1145.25f;

                grpCostCentreNameHeader.Visible = (ReportProperties.ShowByCostCentre == 1);
                grpCCBreakup.Visible = (ReportProperties.BreakByCostCentre == 1) ? true : false;

                if (grpCostCentreNameHeader.Visible != true)
                {
                    this.CosCenterName = objReportProperty.CostCentreName;
                }
                else
                {
                    this.CosCenterName = string.Empty;
                }

                // To show by costcentre starts
                if (this.ReportProperties.ShowByCostCentreCategory == 1 && this.ReportProperties.ShowByCostCentre == 1)
                {
                    grpCostCentreNameHeader.Visible = grpCostCentreCategoryName.Visible = Detail.Visible = ReportFooter.Visible = true;
                    grpCostCategoryName.Visible = false;
                }
                else if (this.ReportProperties.ShowByCostCentre == 1)
                {
                    grpCostCentreCategoryName.Visible = grpCostCategoryName.Visible = false;
                    grpCostCentreNameHeader.Visible = Detail.Visible = ReportFooter.Visible = true;
                }
                else if (this.ReportProperties.ShowByCostCentreCategory == 1)
                {
                    grpCostCategoryName.Visible = true;
                    Detail.Visible = ReportFooter.Visible = grpCostCentreCategoryName.Visible = grpCostCategoryName.Visible == true ? false : true;
                }
                else
                {
                    grpCostCentreCategoryName.Visible = grpCostCategoryName.Visible = false;
                    Detail.Visible = ReportFooter.Visible = true;
                }
                // To show by costcentre ends

                //this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                prCLBankBalance.Visible = prCLCashBalance.Visible = prOPCashBalance.Visible = prOPBankBalance.Visible = false;
                resultArgs = GetReportSource();
                DataView dvCashBankBook = resultArgs.DataSource.TableView;
                if (dvCashBankBook != null)
                {
                    dvCashBankBook.Table.TableName = "CashBankBook";
                    this.DataSource = dvCashBankBook;
                    this.DataMember = dvCashBankBook.Table.TableName;
                }
            }
            SetReportSetup();

            if (this.ReportProperties.IncludeNarration == 1)
            {

            }
            else
            {
                Detail.HeightF = 25.0f;
            }

            // To show by Date starts
            if (this.ReportProperties.ShowByCostCentre == 0)
                grpCostCentreNameHeader.GroupFields[0].FieldName = string.Empty;
            else
                grpCostCentreNameHeader.GroupFields[0].FieldName = "COST_CENTRE_NAME";

            if (this.ReportProperties.ShowByCostCentreCategory == 0)
            {
                grpCostCentreCategoryName.GroupFields[0].FieldName = string.Empty;
                grpCostCategoryName.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpCostCentreCategoryName.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
                grpCostCategoryName.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
            }
            // To show by Date ends

        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterCashBankBook);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CostCentre.CostCenterCashBankBook, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankBookQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCosCapReceiptCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCosCapReceiptCode.Tag != null && xrCosCapReceiptCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCosCapReceiptCode.Tag.ToString());
            }
            else
            {
                xrCosCapReceiptCode.Tag = xrCosCapReceiptCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            grpCostCentreNameHeader.Visible = (ReportProperties.ShowByCostCentre == 1);
            if (grpCostCentreNameHeader.Visible != true)
            {
                this.CosCenterName = objReportProperty.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;

            }
            xrCosCapReceiptCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCosPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCosCashCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell6.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell23.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell16.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell20.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            // xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;
            SetReportBorder();
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            xrCCBreakup = AlignTotalTable(xrCCBreakup);
        }

        public override XRTable AlignTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignGrandTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                if (count == 1)
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                                else if (count == trow.Cells.Count)
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    tcell.Borders = BorderSide.Bottom;
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignHeaderTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        private XRTable AlignCCCategoryTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (ReportProperties.IncludeNarration != 1)
                        {
                            if (ReportProperties.ShowByCostCentre == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                        }
                        else
                        {
                            if (ReportProperties.ShowByCostCentre == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        private XRTable AlignCostCentreTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (ccCount == 1)
                        {
                            if (ReportProperties.IncludeNarration == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                        }
                        else
                        {
                            if (ReportProperties.IncludeNarration == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ccCount == 1)
                        {
                            if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left;
                                if (count == trow.Cells.Count)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                            }
                        }
                        else
                        {
                            if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                                if (count == trow.Cells.Count)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }

                            else
                            {
                                tcell.Borders = BorderSide.Bottom;
                            }
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        public override XRTable AlignCashBankBookTable(XRTable table, string bankNarration, string cashNarration, int count)
        {
            int rowcount = 0;

            foreach (XRTableRow row in table.Rows)
            {
                int cellcount = 0;
                ++rowcount;
                if (rowcount == 2 && ReportProperties.IncludeNarration != 1)
                {
                    row.Visible = false;
                }
                else if (bankNarration == string.Empty && cashNarration == string.Empty && ReportProperties.IncludeNarration == 1 && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
                foreach (XRTableCell cell in row)
                {
                    ++cellcount;
                    if (ReportProperties.IncludeNarration != 1 && rowcount == 1)
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = BorderSide.Right | BorderSide.Left | BorderSide.Bottom;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 8)
                                    cell.Borders = BorderSide.None;
                                else
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 8)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 8)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                        }
                        else
                        {
                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }

                    }
                    else
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 8)
                                            cell.Borders = BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 8)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                }

                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                else if (ReportProperties.ShowLedgerCode != 1)
                                {
                                    if (cellcount == 3 || cellcount == 8)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                }
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                                else if (bankNarration != string.Empty || cashNarration != string.Empty)
                                {
                                    if (cellcount == row.Cells.Count)
                                    {
                                        cell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                                    }
                                    else
                                    {
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                    }
                                }
                                else
                                    if (cellcount == row.Cells.Count)
                                    {
                                        cell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                                    }
                                    else
                                    {
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | BorderSide.Top;
                                    }
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }

                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                    cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
                cellcount = 0;
            }
            return table;
        }



        #endregion

        #region Events

        private void xrCosPayment_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPayment.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPayment.Text = "";
            }
        }

        private void xrCosPay_Cash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPay_Cash.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPay_Cash.Text = "";
            }
        }

        private void xrCosPaymentCash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPaymentCash.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPaymentCash.Text = "";
            }
        }

        private void xrCosPaymentBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPaymentBank.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPaymentBank.Text = "";
            }
        }



        private void xrtblBindSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            xrtblBindSource = AlignCashBankBookTable(xrtblBindSource, Narration, Narration_Pay, count);
        }

        private void xrtblCCName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ccCount++;
            xrtblCCName = AlignCostCentreTable(xrtblCCName);
        }

        #endregion


    }
}
