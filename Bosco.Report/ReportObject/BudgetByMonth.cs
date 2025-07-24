using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Schema;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetByMonth : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        Int32 LedgerGroupId = 0;
        #endregion

        #region Constructor

        public BudgetByMonth()
        {
            InitializeComponent();

            //fieldMONTHNAME.Appearance.FieldHeader.Font = new Font(fieldMONTHNAME.Appearance.FieldHeader.Font.FontFamily, 9);
        }

        #endregion

        #region Show Reports

        public override void ShowReport()
        {
            FetchBudgetByMonth();
        }

        #endregion

        #region Methods

        public void FetchBudgetByMonth()
        {
            this.SetLandscapeHeader = Detail.WidthF;
            this.SetLandscapeFooter = Detail.WidthF;
            this.SetLandscapeFooterDateWidth = Detail.WidthF;

            this.SetTitleWidth(xrPGBudgetByMonth.WidthF);
            setHeaderTitleAlignment();

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget))
            {
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                ShowReportFilterDialog();
            }
            else
            {
                FetchBudgetMonth();
                base.ShowReport();
            }
        }

        private void FetchBudgetMonth()
        {

            SetReportTitle();
            AssignBudgetDateRangeTitle();
            this.BudgetName = objReportProperty.BudgetName;

            SetProperPapersize();
            setHeaderTitleAlignment();
            string budgetmonth = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetExpenditure);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BudgetVariance.BudgetExpenditure, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);

                //  dataManager.Parameters.Add(this.ReportParameters.FDREGISTERSTATUSColumn, this.ReportProperties.FDRegisterStatus);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetmonth);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DataTable dtMonthlyBudget = resultArgs.DataSource.Table;
                    xrPGBudgetByMonth.DataSource = dtMonthlyBudget;
                    xrPGBudgetByMonth.DataMember = dtMonthlyBudget.TableName;

                    DataTable dtMonths = dtMonthlyBudget.DefaultView.ToTable(true, new string[] { "DATE_FROM", "DATE_TO", "MONTH_NAME" });
                    DataTable dtMonthOpening = dtMonthlyBudget.Clone();
                    AttachCashBankDetails(dtMonthOpening, dtMonths);

                    xrPGMonthOpeningBalance.DataSource = dtMonthOpening;
                    xrPGMonthOpeningBalance.DataMember = dtMonthOpening.TableName;
                }
            }
        }

        private DataTable AttachCashBankDetails(DataTable dtBudgetMonthOpening, DataTable dtMonths)
        {
            foreach (DataRow drMonths in dtMonths.Rows)
            {
                DateTime openingdatefrom = this.UtilityMember.DateSet.ToDate(drMonths["DATE_FROM"].ToString(), false);
                DateTime openingdateto = this.UtilityMember.DateSet.ToDate(drMonths["DATE_TO"].ToString(), false);
                string MonthName = drMonths["MONTH_NAME"].ToString();

                //1. Attach Cash Ledger
                ResultArgs result = this.GetBalanceDetail(true, openingdatefrom.ToShortDateString(), this.ReportProperties.Project, ((int)FixedLedgerGroup.Cash).ToString());
                if (result.Success)
                {
                    DataTable dtBalanceDetails = result.DataSource.Table;

                    AppendCashBankFDRow(dtBudgetMonthOpening, dtBalanceDetails, openingdatefrom.ToShortDateString(), openingdateto.ToShortDateString(), MonthName);
                }

                //2. Attach Bank Ledger
                result = this.GetBalanceDetail(true, openingdatefrom.ToShortDateString(), this.ReportProperties.Project, ((int)FixedLedgerGroup.BankAccounts).ToString());
                if (result.Success)
                {
                    DataTable dtBalanceDetails = result.DataSource.Table;
                    AppendCashBankFDRow(dtBudgetMonthOpening, dtBalanceDetails, openingdatefrom.ToShortDateString(), openingdateto.ToShortDateString(), MonthName);
                }

                //AppendCashBankFDEmptyRow(dtBudgetByMonth, openingdatefrom.ToShortDateString(), openingdateto.ToShortDateString(), MonthName);
            }
            return dtBudgetMonthOpening;
        }

        /// <summary>
        /// Add detail cash/Bank/fd ledger details into budget ledgers
        /// </summary>
        /// <param name="dtIncomeBudget"></param>
        /// <param name="dtCashBankDetail"></param>
        /// <param name="isOpeningbalance"></param>
        private void AppendCashBankFDRow(DataTable dtBudgetExpense, DataTable dtCashBankDetail, string datefrom, string dateto, string monthname)
        {
            foreach (DataRow dr in dtCashBankDetail.Rows)
            {
                Int32 cashbankledgerid = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                Int32 groupid = UtilityMember.NumberSet.ToInteger(dr["GROUP_ID"].ToString());
                DataRow drCashBank = dtBudgetExpense.NewRow();
                drCashBank["GROUP_ID"] = groupid;
                drCashBank["MONTH_NAME"] = monthname;
                drCashBank["DATE_FROM"] = datefrom;
                drCashBank["DATE_TO"] = dateto;
                drCashBank["NATURE_ID"] = (int)Natures.Assert;
                drCashBank["NATURE"] = "Asset";
                drCashBank["LEDGER_ID"] = cashbankledgerid;
                drCashBank["ACCESS_FLAG"] = "2";
                drCashBank["LEDGER_CODE"] = (groupid == (Int32)FixedLedgerGroup.Cash ? " " : dr["LEDGER_CODE"].ToString().Trim());
                drCashBank["LEDGER_NAME"] = dr["LEDGER_NAME"].ToString().Trim();
                drCashBank["LEDGER_GROUP"] = dr["LEDGER_GROUP"].ToString().Trim();
                drCashBank["BUDGET_TRANS_MODE"] = string.Empty;
                double balanceamt = this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                if (dr["TRANS_MODE"].ToString().ToUpper() == "CR")
                    drCashBank["AMOUNT"] = -balanceamt;
                else
                    drCashBank["AMOUNT"] = balanceamt;

                dtBudgetExpense.DefaultView.Table.Rows.Add(drCashBank);
                dtBudgetExpense.DefaultView.Table.AcceptChanges();
            }
        }

        private double FetchOPBalance(string Date, int LedgerId)
        {
            double value = 100;
            Bosco.Report.Base.BalanceProperty balancePropery;
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                //balancePropery = balanceSystem.GetBalance(this.ReportProperties.Project, LedgerId, Date, BalanceSystem.BalanceType.ClosingBalance);
            }
            return value;//balancePropery.Amount;
        }

        private void xrPGBudgetByMonth_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldMONTHNAME.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.Value1.ToString());
                    DateTime dt2 = DateTime.Parse(e.Value2.ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGBudgetByMonth_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            //if (e.ColumnField.Name == fieldMONTHNAME.Name)
            //{
            //    //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
            //    if (e.RowField==null)
            //    {
            //        e.Appearance.Font = new System.Drawing.Font(fieldMONTHNAME.Appearance.FieldHeader.Font, FontStyle.Bold);
            //    }
            //}

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGBudgetByMonth.Styles.HeaderGroupLineStyle.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
                e.Appearance.Font = xrPGBudgetByMonth.Styles.CellStyle.Font;
            }

            if (e.RowField == null)
            {
                e.Appearance.Font = xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font;

                ////On 25/02/2019
                //if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.GrandTotalCell)
                //{
                //    if (xrPGBudgetByMonth.DataSource != null && !string.IsNullOrEmpty(e.ColumnValue.Text))
                //    {
                //        DataTable dtReportSource = (DataTable)xrPGBudgetByMonth.DataSource;
                //        string monthname = e.ColumnValue.Text;
                //        object RealSumWithoutCasBank = dtReportSource.Compute("SUM(AMOUNT)",
                //            "GROUP_ID NOT IN (" + (Int32)FixedLedgerGroup.Cash + "," + (Int32)FixedLedgerGroup.BankAccounts + ") AND MONTH_NAME='" + monthname + "'");
                //        if (RealSumWithoutCasBank != null)
                //        {
                //            double GrandTotalWithoutCasBank = this.UtilityMember.NumberSet.ToDouble(RealSumWithoutCasBank.ToString());
                //            e.Brick.Text = this.UtilityMember.NumberSet.ToNumber(GrandTotalWithoutCasBank).ToString();
                //        }
                //    }
                //}
            }
        }

        private void xrPGBudgetByMonth_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            //if (e.Field != null && e.Field.Name == fieldMONTHNAME.Name)
            //{
            //    e.Appearance.Font = new System.Drawing.Font(fieldMONTHNAME.Appearance.FieldHeader.Font, FontStyle.Bold);
            //}

            //if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            //{
            //    e.Appearance.Font = new System.Drawing.Font(fieldMONTHNAME.Appearance.FieldHeader.Font, FontStyle.Bold);
            //}
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    e.Appearance.Font = xrPGBudgetByMonth.Styles.CellStyle.Font;
                    /*string ledgername = e.Value.ToString();
                    Int32 grpId = this.FetchLedgerGroupId(ledgername);
                    if (grpId > 0 && (grpId != (Int32)FixedLedgerGroup.Cash || grpId != (Int32)FixedLedgerGroup.BankAccounts))
                    {
                        e.Appearance.Font = new System.Drawing.Font(xrPGBudgetByMonth.Styles.CellStyle.Font, FontStyle.Bold);
                    }*/
                }
                else if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGBudgetByMonth.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGBudgetByMonth.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGBudgetByMonth.Styles.HeaderGroupLineStyle.Font;
                }
            }
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.Brick.Text = "Total";
                e.Appearance.Font = xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font;
            }
        }
        #endregion

        #region events

        private void xrPGMonthOpeningBalance_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldCBMONTHNAME.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.Value1.ToString());
                    DateTime dt2 = DateTime.Parse(e.Value2.ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void SetProperPapersize()
        {
            //bool isLedgerCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            //fieldLEDGERCODE.Visible = isLedgerCodeVisible;
            xrPGMonthOpeningBalance.OptionsView.ShowRowHeaders = false;
            xrPGMonthOpeningBalance.OptionsView.ShowColumnGrandTotalHeader = false;
            xrPGMonthOpeningBalance.OptionsView.ShowColumnGrandTotals = false;


            if (this.ReportProperties.Budget.Split(',').Length > 9)
            {
                this.Landscape = true;
                fieldLEDGERCODE.Width = fieldCBLEDGERCODE.Width = 35;
                fieldLEDGERNAME.Width = fieldCBLEDGERNAME.Width = 145;
                fieldMONTHNAME.Width = fieldAMOUNT.Width = fieldCBMONTHNAME.Width = fieldCBAMOUNT.Width = 75;
                xrPGBudgetByMonth.Styles.CellStyle.Font = new Font(xrPGBudgetByMonth.Styles.CellStyle.Font.FontFamily, 8);
                xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font = new Font(xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font.FontFamily, 8, FontStyle.Bold);
                xrPGMonthOpeningBalance.Styles.CellStyle.Font = new Font(xrPGBudgetByMonth.Styles.CellStyle.Font.FontFamily, 8);
                xrPGMonthOpeningBalance.Styles.FieldHeaderStyle.Font = new Font(xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font.FontFamily, 8, FontStyle.Bold);

                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 10;
                this.SetTitleWidth(this.PageWidth - 10);
            }
            else if (this.ReportProperties.Budget.Split(',').Length <= 3)
            {
                this.Landscape = false;
                fieldLEDGERCODE.Width = fieldCBLEDGERCODE.Width = 50;
                fieldLEDGERNAME.Width = fieldCBLEDGERNAME.Width = 300;
                fieldMONTHNAME.Width = fieldAMOUNT.Width = fieldCBMONTHNAME.Width = fieldCBAMOUNT.Width = 125;
                xrPGBudgetByMonth.Styles.CellStyle.Font = new Font(xrPGBudgetByMonth.Styles.CellStyle.Font.FontFamily, 10);
                xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font = new Font(xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font.FontFamily, 10, FontStyle.Bold);
                xrPGMonthOpeningBalance.Styles.CellStyle.Font = new Font(xrPGBudgetByMonth.Styles.CellStyle.Font.FontFamily, 10);
                xrPGMonthOpeningBalance.Styles.FieldHeaderStyle.Font = new Font(xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font.FontFamily, 10, FontStyle.Bold);

                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 10;
                this.SetTitleWidth(this.PageWidth - 10);
            }
            else if (this.ReportProperties.Budget.Split(',').Length <= 9)
            {
                this.Landscape = true;
                fieldLEDGERCODE.Width = fieldCBLEDGERCODE.Width = 35;
                fieldLEDGERNAME.Width = fieldCBLEDGERNAME.Width = 175;
                fieldMONTHNAME.Width = fieldAMOUNT.Width = fieldCBMONTHNAME.Width = fieldCBAMOUNT.Width = 90;
                xrPGBudgetByMonth.Styles.CellStyle.Font = new Font(xrPGBudgetByMonth.Styles.CellStyle.Font.FontFamily, 9);
                xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font = new Font(xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font.FontFamily, 9, FontStyle.Bold);
                xrPGMonthOpeningBalance.Styles.CellStyle.Font = new Font(xrPGBudgetByMonth.Styles.CellStyle.Font.FontFamily, 9);
                xrPGMonthOpeningBalance.Styles.FieldHeaderStyle.Font = new Font(xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font.FontFamily, 9, FontStyle.Bold);

                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 10;
                this.SetTitleWidth(this.PageWidth - 10);
            }

        }

        private void xrPGMonthOpeningBalance_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            //if (e.ColumnField.Name == fieldMONTHNAME.Name)
            //{
            //    //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
            //    if (e.RowField==null)
            //    {
            //        e.Appearance.Font = new System.Drawing.Font(fieldMONTHNAME.Appearance.FieldHeader.Font, FontStyle.Bold);
            //    }
            //}

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGBudgetByMonth.Styles.HeaderGroupLineStyle.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
                e.Appearance.Font = xrPGBudgetByMonth.Styles.CellStyle.Font;
            }

            if (e.RowField == null)
            {
                e.Appearance.Font = xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font;
            }
        }

        private void xrPGMonthOpeningBalance_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            //if (e.Field != null && e.Field.Name == fieldMONTHNAME.Name)
            //{
            //    e.Appearance.Font = new System.Drawing.Font(fieldMONTHNAME.Appearance.FieldHeader.Font, FontStyle.Bold);
            //}

            //if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            //{
            //    e.Appearance.Font = new System.Drawing.Font(fieldMONTHNAME.Appearance.FieldHeader.Font, FontStyle.Bold);
            //}
            if (e.Field != null)
            {
                if (e.Field.Name == fieldCBLEDGERCODE.Name || e.Field.Name == fieldCBLEDGERNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    e.Appearance.Font = xrPGBudgetByMonth.Styles.CellStyle.Font;
                    /*string ledgername = e.Value.ToString();
                    Int32 grpId = this.FetchLedgerGroupId(ledgername);
                    if (grpId > 0 && (grpId != (Int32)FixedLedgerGroup.Cash || grpId != (Int32)FixedLedgerGroup.BankAccounts))
                    {
                        e.Appearance.Font = new System.Drawing.Font(xrPGBudgetByMonth.Styles.CellStyle.Font, FontStyle.Bold);
                    }*/
                }
                else if (e.Field.Name == fieldCBMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGBudgetByMonth.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGBudgetByMonth.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font;

                    if (xrPGMonthOpeningBalance.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;

                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        //e.Field.Options.ShowValues = false;
                    }
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGBudgetByMonth.Styles.HeaderGroupLineStyle.Font;
                }

            }
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.Brick.Text = "Total";
                e.Appearance.Font = xrPGBudgetByMonth.Styles.FieldHeaderStyle.Font;
            }
        }
        #endregion
    }
}
