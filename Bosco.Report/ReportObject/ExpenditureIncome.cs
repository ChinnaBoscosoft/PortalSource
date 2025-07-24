using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;
namespace Bosco.Report.ReportObject
{
    public partial class ExpenditureIncome : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Declaration
        double ExpenditureAmount = 0;
        double IncomeAmount = 0;
        double ExcessIncome = 0;
        double ExcessExpenditure = 0;
        private string EXCESSOFINCOME = "Excess Of Expenditure Over Income";
        private string EXCESSOFEXPENDITURE = "Excess of Income Over Expenditure";
        SettingProperty settingProperty = new SettingProperty();
        #endregion

        #region Constructor
        public ExpenditureIncome()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindExpenditureIncomeSource();
        }
        #endregion

        #region Method
        private void BindExpenditureIncomeSource()
        {
            try
            {
                // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                xrCapExpenditureAmt.Text = this.SetCurrencyFormat(xrCapExpenditureAmt.Text);
                xrCapIncomeAmt.Text = this.SetCurrencyFormat(xrCapIncomeAmt.Text);
                // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                // this.ReportTitle = ReportProperty.Current.ReportTitle;
                setHeaderTitleAlignment();
                SetReportTitle();
                // this.ReportTitle = this.ReportProperties.ReportTitle;
                Payments PaymentsLedger = xrSubPayments.ReportSource as Payments;
                PaymentsLedger.HidePaymentReportHeader();
                Receipts receiptsLedger = xrSubReceipts.ReportSource as Receipts;
                receiptsLedger.HideReceiptReportHeader();
                if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                                || this.ReportProperties.Project == "0")
                {
                    ShowReportFilterDialog();
                }
                else
                {
                    BindSource();
                    base.ShowReport();
                }

                xtTblHeadTable = AlignHeaderTable(xtTblHeadTable);
                xrTblExpense = AlignExpenseTable(xrTblExpense);
                xrTblTotal = AlignTotalTable(xrTblTotal);


            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            SetReportSetup();
        }

        public XRTable AlignExpenseTable(XRTable table)
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
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
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
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
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
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            }

                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCapExpenditureCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapExpenditureCode.Tag != null && xrCapExpenditureCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapExpenditureCode.Tag.ToString());
            }
            else
            {
                xrCapExpenditureCode.Tag = xrCapExpenditureCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapExpenditureCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCapIncomeCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrlLine1.ForeColor = xrlLine2.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
        }

        private void SetExpenseTableBorders()
        {
            foreach (XRTableRow trow in xrTblExpense.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }

        }

        private void SetTotalTableBorders()
        {
            foreach (XRTableRow trow in xrTblTotal.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
        }

        private void BindSource()
        {
            //this.ReportProperties.Society = "0";
            ResultArgs IncomeResultArgs = new ResultArgs();
            ResultArgs ExpenseResultArgs = new ResultArgs();
            ResultArgs FinalIEResultArg = new ResultArgs();

            Payments PaymentsLedger = xrSubPayments.ReportSource as Payments;
            PaymentsLedger.HidePaymentReportHeader();

            Receipts receiptsLedger = xrSubReceipts.ReportSource as Receipts;
            receiptsLedger.HideReceiptReportHeader();

            this.AttachDrillDownToSubReport(PaymentsLedger);
            this.AttachDrillDownToSubReport(receiptsLedger);

            string sqlFinalIE = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalIncomeExpenditure);
            object sqlCommandId = string.Empty;
            using (DataManager dataManager = new DataManager(sqlCommandId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.settingProperty.BookBeginFrom);

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, ReportProperties.ShowByLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, ReportProperties.ShowByLedgerGroup);

                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                FinalIEResultArg = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlFinalIE);
            }

            if (FinalIEResultArg.Success)
            {
                DataTable dtFinalIE = FinalIEResultArg.DataSource.Table;
                dtFinalIE.DefaultView.RowFilter = "AMOUNT< 0";
                DataTable dtExpenseLedgers = dtFinalIE.DefaultView.ToTable();
                dtExpenseLedgers.Columns.Add("PAYMENTAMT", dtExpenseLedgers.Columns["AMOUNT"].DataType, "AMOUNT * -1"); //Remove Negative Symbol
                ExpenseResultArgs.Success = true;
                ExpenseResultArgs.DataSource.Data = dtExpenseLedgers;
                PaymentsLedger.SortByLedgerorGroup();

                dtFinalIE.DefaultView.RowFilter = string.Empty;
                dtFinalIE.DefaultView.RowFilter = "AMOUNT > 0";
                DataTable dtIncomeLedgers = dtFinalIE.DefaultView.ToTable();
                dtIncomeLedgers.Columns["AMOUNT"].ColumnName = "RECEIPTAMT";
                IncomeResultArgs.Success = true;
                IncomeResultArgs.DataSource.Data = dtIncomeLedgers;
                receiptsLedger.SortByLedgerorGroup();

                PaymentsLedger.BindExpenseSource(ExpenseResultArgs, TransType.EP);
                receiptsLedger.BindIncomeSource(IncomeResultArgs, TransType.IC);

                IncomeAmount = receiptsLedger.ReceiptAmount;
                ExpenditureAmount = PaymentsLedger.PaymentAmout;

                xrTableCell12.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(ExpenditureAmount.ToString())).ToString();
                xrTableCell14.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(IncomeAmount.ToString())).ToString();
            }

            // setting Expenditure table column width while showledgercode is set 1
            if (ReportProperties.ShowLedgerCode == 1)
            {
                PaymentsLedger.CodeColumnWidth = xrCapExpenditureCode.WidthF;
                PaymentsLedger.NameColumnWidth = xrCapExpenditureName.WidthF;
                PaymentsLedger.AmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }
            else
            {
                PaymentsLedger.CodeColumnWidth = 0;
                PaymentsLedger.NameColumnWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF - 2;
                PaymentsLedger.AmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }

            // setting Expenditure table column width while showgroupcode is set 1
            if (ReportProperties.ShowGroupCode == 1)
            {
                PaymentsLedger.GroupCodeColumnWidth = xrCapExpenditureCode.WidthF;
                PaymentsLedger.GroupNameColumnWidth = xrCapExpenditureName.WidthF;
                PaymentsLedger.GroupAmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }
            else
            {
                PaymentsLedger.GroupCodeColumnWidth = 0;
                PaymentsLedger.GroupNameColumnWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF - 2;
                PaymentsLedger.GroupAmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }

            PaymentsLedger.CategoryNameWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF + xrCapExpenditureAmt.WidthF;
            
            // setting income table column width while showledgercode is set 1
            if (ReportProperties.ShowLedgerCode == 1)
            {
                if (ReportProperties.ShowByLedger == 1)
                {
                    receiptsLedger.CodeColumnWidth = xrCapExpenditureCode.WidthF - 2;
                    receiptsLedger.NameColumnWidth = xrCapExpenditureName.WidthF + 2;
                    receiptsLedger.AmountColumnWidth = xrCapIncomeAmt.WidthF + 1;
                }
                else
                {
                    receiptsLedger.CodeColumnWidth = xrCapExpenditureCode.WidthF - 2;
                    receiptsLedger.NameColumnWidth = xrCapExpenditureName.WidthF + 2;
                    receiptsLedger.AmountColumnWidth = xrCapIncomeAmt.WidthF + 1;
                }
            }
            else
            {
                receiptsLedger.CodeColumnWidth = 0;
                receiptsLedger.NameColumnWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF - 2;
                receiptsLedger.AmountColumnWidth = xrCapIncomeAmt.WidthF;
            }

            // setting income table column width while showgroupcode is set 1
            if (ReportProperties.ShowGroupCode == 1)
            {
                receiptsLedger.GroupCodeColumnWidth = xrCapExpenditureCode.WidthF - 2;
                receiptsLedger.GroupNameColumnWidth = xrCapExpenditureName.WidthF + 2;
                receiptsLedger.GroupAmountColumnWidth = xrCapIncomeAmt.WidthF + 1;
            }
            else
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    receiptsLedger.GroupCodeColumnWidth = 0;
                    receiptsLedger.GroupNameColumnWidth = xrCapIncomeCode.WidthF + xrCapIncomeLedgerName.WidthF - 4;
                    receiptsLedger.GroupAmountColumnWidth = xrCapIncomeAmt.WidthF;
                }
                else
                {
                    receiptsLedger.GroupCodeColumnWidth = 0;
                    receiptsLedger.GroupNameColumnWidth = xrCapIncomeCode.WidthF + xrCapIncomeLedgerName.WidthF - 2;
                    receiptsLedger.GroupAmountColumnWidth = xrCapIncomeAmt.WidthF;
                }
            }

            receiptsLedger.CostCentreCategoryNameWidth = xrCapIncomeCode.WidthF + xrCapIncomeLedgerName.WidthF + xrCapIncomeAmt.WidthF;
            //SplashScreenManager.CloseForm();
            //base.ShowReport();


            xtTblHeadTable = HeadingTableBorder(xtTblHeadTable, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            // to align xrTblExpense
            SetExpenseTableBorders();
            SetTotalTableBorders();


        }


        #endregion

        #region Events
        private void xrPaymentsSumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExpenditureAmount != IncomeAmount)
            {
                if (ExpenditureAmount < IncomeAmount)
                {
                    ExcessIncome = IncomeAmount - ExpenditureAmount;
                    e.Result = ExpenditureAmount + ExcessIncome;
                    e.Handled = true;
                }
                else
                {
                    e.Result = ExpenditureAmount;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = ExpenditureAmount;
                e.Handled = true;
            }
        }

        private void xrReceiptsSumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExpenditureAmount != IncomeAmount)
            {
                if (ExpenditureAmount > IncomeAmount)
                {
                    ExcessExpenditure = ExpenditureAmount - IncomeAmount;
                    e.Result = IncomeAmount + ExcessExpenditure;
                    e.Handled = true;
                }
                else
                {
                    e.Result = IncomeAmount;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = IncomeAmount;
                e.Handled = true;
            }
        }

        private void xrTableCell5_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExpenditureAmount != IncomeAmount)
            {
                if (ExpenditureAmount > IncomeAmount)
                {
                    ExcessExpenditure = ExpenditureAmount - IncomeAmount;
                    e.Result = EXCESSOFINCOME;
                    e.Handled = true;
                }
                else
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrTableCell6_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExpenditureAmount != IncomeAmount)
            {
                if (ExpenditureAmount > IncomeAmount)
                {
                    ExcessExpenditure = ExpenditureAmount - IncomeAmount;
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessExpenditure);
                    e.Handled = true;
                }
                else
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

        }

        private void xrTableCell2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExpenditureAmount != IncomeAmount)
            {
                if (ExpenditureAmount < IncomeAmount)
                {
                    ExcessIncome = IncomeAmount - ExpenditureAmount;
                    e.Result = EXCESSOFEXPENDITURE;
                    e.Handled = true;
                }
                else
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrTableCell1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExpenditureAmount != IncomeAmount)
            {
                if (ExpenditureAmount < IncomeAmount)
                {
                    ExcessIncome = IncomeAmount - ExpenditureAmount;
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessIncome);
                    e.Handled = true;
                }
                else
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        #endregion
    }
}
