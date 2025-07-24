using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CostCenterCashJournal : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        int count = 0;

        public CostCenterCashJournal()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrtblDetails, xrCosParticulars,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false);
        }

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindCostCenterCashJournal();
            }

            // To show by Date starts
            if (this.ReportProperties.ShowByCostCentre == 0)
                grpCostCenterName.GroupFields[0].FieldName = string.Empty;
            else
                grpCostCenterName.GroupFields[0].FieldName = "COST_CENTRE_NAME";

            if (this.ReportProperties.ShowByCostCentreCategory == 0)
            {
                grpCostcentreCategoryName.GroupFields[0].FieldName = string.Empty;
                grpCostCategoryName.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpCostcentreCategoryName.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
                grpCostCategoryName.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
            }
            // To show by Date ends

            base.ShowReport();
        }
        #endregion

        #region Methods

        private void BindCostCenterCashJournal()
        {
            // this.ReportTitle = ReportProperty.Current.ReportTitle;
            // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            SetReportTitle();

            // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            xrCapReceipts.Text = this.SetCurrencyFormat(xrCapReceipts.Text);
            xrCapPayments.Text = this.SetCurrencyFormat(xrCapPayments.Text);

            grpCCBreakup.Visible = (ReportProperties.BreakByCostCentre == 1) ? true : false;

            // To show by costcentre starts
            if (this.ReportProperties.ShowByCostCentreCategory == 1 && this.ReportProperties.ShowByCostCentre == 1)
            {
                grpCostCenterName.Visible = grpCostCategoryName.Visible = Detail.Visible = ReportFooter.Visible = true;
                grpCostcentreCategoryName.Visible = false;
            }
            else if (this.ReportProperties.ShowByCostCentre == 1)
            {
                grpCostcentreCategoryName.Visible = grpCostCategoryName.Visible = false;
                grpCostCenterName.Visible = Detail.Visible = ReportFooter.Visible = true;
            }
            else if (this.ReportProperties.ShowByCostCentreCategory == 1)
            {
                grpCostcentreCategoryName.Visible = true;
                Detail.Visible = ReportFooter.Visible = grpCostCategoryName.Visible = grpCostcentreCategoryName.Visible == true ? false : true;
            }
            else
            {
                grpCostcentreCategoryName.Visible = grpCostCategoryName.Visible = false;
                Detail.Visible = ReportFooter.Visible = true;
            }
            // To show by costcentre ends


            ResultArgs resultArgs = GetReportSource();
            DataTable dtReceipt = resultArgs.DataSource.Table;

            if (dtReceipt != null)
            {
                dtReceipt.TableName = "CashBankJournal";
                this.DataSource = dtReceipt;
                this.DataMember = dtReceipt.TableName;
            }
            SetReportSetup();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CostCentreCashJournal = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterCashJournal);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CostCentre.CostCenterCashJournal, DataBaseType.HeadOffice))
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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CostCentreCashJournal);
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
            float actualCodeWidth = xrCosCapCashLedgerCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCosCapCashLedgerCode.Tag != null && xrCosCapCashLedgerCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCosCapCashLedgerCode.Tag.ToString());
            }
            else
            {
                xrCosCapCashLedgerCode.Tag = xrCosCapCashLedgerCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            grpCostCenterName.Visible = (ReportProperties.ShowByCostCentre == 1);
            if (grpCostCenterName.Visible != true)
            {
                this.CosCenterName = objReportProperty.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;
            }
            xrCosCapCashLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCashLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCashCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell3.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            // xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;

            SetReportBorder();

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
                            if (count == 3 || count == 9)
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
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                if (count == 1)
                                    tcell.Borders = BorderSide.All;
                                else
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right | BorderSide.Top;
                        }
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;

                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Bottom | BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Bottom | BorderSide.Right;
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
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = BorderSide.None;
                                else
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            else
                                if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
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
                                        if (cellcount == 3 || cellcount == 9)
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
                                        if (cellcount == 3 || cellcount == 9)
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
                                    if (cellcount == 3 || cellcount == 9)
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
                                if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.None;
                                else if (cellcount == 1)
                                    cell.Borders = BorderSide.Left | BorderSide.Bottom;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                                else
                                    cell.Borders = BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Bottom | BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.Bottom;
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
                                    else
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
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
        private void SetReportBorder()
        {
            xttblHeaderCaption = AlignHeaderTable(xttblHeaderCaption);
            xttblGrandTotal = AlignGrandTotalTable(xttblGrandTotal);
            xrCCBreakup = AlignTotalTable(xrCCBreakup);
            xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            xrtblCCName = AlignCostCentreTable(xrtblCCName);
        }

        #endregion Methods

        #region Events

        private void xrCosCenterCashReceipts_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosCenterCashReceipts.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosCenterCashReceipts.Text = "";
            }
        }

        private void xrCashPayments_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCashPayments.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCashPayments.Text = "";
            }
        }


        private void xrtblDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrtblDetails = AlignCashBankBookTable(xrtblDetails, Narration, string.Empty, count);
        }

        #endregion Events

    }
}
