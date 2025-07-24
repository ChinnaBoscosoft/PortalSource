using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraReports.UI.PivotGrid;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class AccountBalanceMulti : Report.Base.ReportBase
    {
        ResultArgs resultArgs = null;
        public double PeriodBalanceAmount { get; set; }

        public float LeftPosition 
        {
            set 
            {
                xrPGAccountBalanceMulti.LeftF = value;
            } 
        }

        public int GroupCodeColumnWidth
        {
            set
            {
      //          fieldGROUPCODE.Width = value;
            }
        }

        public int GroupNameColumnWidth
        {
            set
            {
                fieldLEDGERGROUP.Width = value;
            }
        }

        public int LedgerCodeColumnWidth
        {
            set
            {
      //          fieldLEDGERCODE.Width = value;
            }
        }

        public int LedgerNameColumnWidth
        {
            set
            {
                fieldLEDGERNAME.Width = value;
            }
        }

        public int AmountColumnWidth
        {
            set
            {
                fieldMONTHNAME.Width = value;
                fieldAMOUNT.Width = value;
            }
        }

        public bool ShowColumnHeader
        {
            set
            {
                xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders = value;
            }
        }

        DataTable dtBalGrantTotal = null;

        public DataTable GrantTotalBalance
        {
            get
            {
                return GetGrantTotalSource();
            }
        }

        public AccountBalanceMulti()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            base.ShowReport();
        }

        private ResultArgs GetBalance(string balDate, string projectIds, string groupIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public void BindBalance(bool isOpBalance)
        {
            string dateFrom = ReportProperties.DateFrom;
            string dateTo = ReportProperties.DateTo;
            string balDate = "";
            string projectIds = ReportProperties.Project;
            string groupIds = this.GetLiquidityGroupIds();
            DateTime monthYear;

            int month = 0;
            int year = 0;
            string monthName = "";
            string transMode = "";
            double amount = 0;

            if (dateTo == "") { dateTo = ReportProperties.DateAsOn; }
            DateTime date_from = DateTime.Parse(dateFrom);
            DateTime date_to = DateTime.Parse(dateTo);
            DateTime openingMonthYear;

            Dictionary<DateTime, DateTime> dicMonthYear = new Dictionary<DateTime, DateTime>();

            if (isOpBalance)
            {
                DateTime dateFr = DateTime.Parse(dateFrom);
                monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                dicMonthYear[monthYear] = dateFr.AddDays(-1);
                openingMonthYear = monthYear;

                while (true)
                {
                    dateFr = new DateTime(date_from.Year, date_from.Month, 1).AddMonths(1);

                    if (dateFr <= date_to)
                    {
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = dateFr.AddDays(-1);
                        date_from = dateFr;
                    }
                    else
                    {
                        //Opening Balance for Date of First Month for Balance Total Column
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = dicMonthYear[openingMonthYear];
                        break;
                    }
                }
            }
            else
            {
                DateTime dateFr = DateTime.Parse(dateFrom);

                while (true)
                {
                    dateFr = new DateTime(date_from.Year, date_from.Month, 1).AddMonths(1).AddDays(-1);

                    if (dateFr < date_to)
                    {
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = dateFr;
                        date_from = dateFr.AddDays(1);
                    }
                    else
                    {
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = date_to;

                        //Closing Balance for Date of Last Month for Balance Total Column
                        date_from = dateFr.AddDays(1);
                        dateFr = new DateTime(date_from.Year, date_from.Month, 1).AddMonths(1).AddDays(-1);
                        monthYear = new DateTime(dateFr.Year, dateFr.Month, 1);
                        dicMonthYear[monthYear] = date_to;
                        break;
                    }
                }
            }

            //Get Schema
            resultArgs = GetBalance(dateFrom, "0", "0");
            DataTable dtBalance = resultArgs.DataSource.Table;

            dtBalance.Columns.Add(reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName, typeof(DateTime));
            dtBalance.Columns.Add(reportSetting1.AccountBalance.YEARColumn.ColumnName, typeof(int));
            dtBalance.Columns.Add(reportSetting1.AccountBalance.MONTHColumn.ColumnName, typeof(int));
            dtBalance.Columns.Add(reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName, typeof(string));

            foreach (KeyValuePair<DateTime, DateTime> dateKey in dicMonthYear)
            {
                monthYear = dateKey.Key;
                balDate = dateKey.Value.ToShortDateString();
                month = monthYear.Month;
                year = monthYear.Year;
                monthName = monthYear.ToString("MMM") + "-" + monthYear.Year;

                //Fill Each Month Balance into 1 Table
                resultArgs = GetBalance(balDate, projectIds, groupIds);
                DataTable dtBalMonth = resultArgs.DataSource.Table;

                if (dtBalMonth != null)
                {
                    if (dtBalMonth.Rows.Count > 0)
                    {
                        foreach (DataRow drBalMonth in dtBalMonth.Rows)
                        {
                            transMode = drBalMonth[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                            amount = UtilityMember.NumberSet.ToDouble(drBalMonth[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                            if (transMode == TransactionMode.CR.ToString()) { amount = -amount; }

                            DataRow drBalance = dtBalance.NewRow();
                            drBalance[reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName] = monthYear;
                            drBalance[reportSetting1.AccountBalance.YEARColumn.ColumnName] = year;
                            drBalance[reportSetting1.AccountBalance.MONTHColumn.ColumnName] = month;
                            drBalance[reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName] = monthName;
                            drBalance[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName] = drBalMonth[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName];
                            drBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = amount;
                            drBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName] = transMode;
                            dtBalance.Rows.Add(drBalance);
                        }
                    }
                    else
                    {
                        DataRow drBalance = dtBalance.NewRow();
                        drBalance[reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName] = monthYear;
                        drBalance[reportSetting1.AccountBalance.YEARColumn.ColumnName] = year;
                        drBalance[reportSetting1.AccountBalance.MONTHColumn.ColumnName] = month;
                        drBalance[reportSetting1.AccountBalance.MONTH_NAMEColumn.ColumnName] = monthName;
                        drBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = 0;
                        dtBalance.Rows.Add(drBalance);
                    }
                }
            }

            dtBalance.AcceptChanges();
            DataView dvBalance = dtBalance.DefaultView;

            if (dvBalance != null)
            {
                dvBalance.Table.TableName = "AccountBalance";
                xrPGAccountBalanceMulti.DataSource = dvBalance;
                xrPGAccountBalanceMulti.DataMember = dvBalance.Table.TableName;
            }

            SetReportSetting();
        }

        private DataTable GetGrantTotalSource()
        {
            DataTable dtGrantTotal = new DataTable();
            dtGrantTotal.Columns.Add(reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName, typeof(string));
            dtGrantTotal.Columns.Add(reportSetting1.AccountBalance.MONTHColumn.ColumnName, typeof(int));
            dtGrantTotal.Columns.Add(reportSetting1.AccountBalance.AMOUNTColumn.ColumnName, typeof(double));
            
            object oTotVal = null;
            double totVal = 0;
            int row = xrPGAccountBalanceMulti.RowCount - 1;

            for (int col = 0; col < xrPGAccountBalanceMulti.ColumnCount; col++)
            {
                oTotVal = xrPGAccountBalanceMulti.GetCellValue(col, row);
                totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.AccountBalance.MONTHColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
            return dtGrantTotal;
        }

        private void xrPGAccountBalanceMulti_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            try
            {
                if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    if (e.Value1 != null && e.Value2 != null)
                    {
                        DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName).ToString());
                        DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.MONTH_YEARColumn.ColumnName).ToString());
                        e.Result = Comparer.Default.Compare(dt1, dt2);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception err)
            {
                string s = err.Message;
            }
        }

        private void xrPGAccountBalanceMulti_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total";
            }
        }

        private void xrPGAccountBalanceMulti_PrintFieldValue(object sender, CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                //if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name
                //    || e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                //{
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
           //     }
                 if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGAccountBalanceMulti.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    e.Appearance.ForeColor = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font;
                }

                if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    if (xrPGAccountBalanceMulti.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;

                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        e.Field.Options.ShowValues = false;
                    }
                }
            }
        }

        private void xrPGAccountBalanceMulti_PrintCell(object sender, CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                e.Appearance.ForeColor = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGAccountBalanceMulti.Styles.HeaderGroupLineStyle.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
            }
        }

        private void SetReportSetting()
        {
            int extendWidth = 0;

            //try { fieldGROUPCODE.Visible = true; }
            //catch { }
            try { fieldLEDGERGROUP.Visible = true; }
            catch { }
            //try { fieldLEDGERCODE.Visible = true; }
            //catch { }
            try { fieldLEDGERNAME.Visible = true; }
            catch { }

         //   fieldGROUPCODE.AreaIndex = 0;
            fieldLEDGERGROUP.AreaIndex = 1;
         //   fieldLEDGERCODE.AreaIndex = 2;
            fieldLEDGERNAME.AreaIndex = 3;

            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isShowBankDetail = (ReportProperties.ShowDetailedBalance == 1);

            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));
            
            if (isShowBankDetail) 
            { 
                isShowBankDetail = isLedgerVisible;
            }

            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);

            //Include / Exclude Code
            //try { fieldGROUPCODE.Visible = (isGroupCodeVisible || (isLedgerCodeVisible && !isShowBankDetail)); }
            //catch { }
            try { fieldLEDGERGROUP.Visible = (isGroupVisible || (isLedgerVisible && !isShowBankDetail)); }
            catch { }
            //try { fieldLEDGERCODE.Visible = (isShowBankDetail && isLedgerCodeVisible); }
            //catch { }
            try { fieldLEDGERNAME.Visible = isShowBankDetail; }
            catch { }

            if (isGroupVisible) { extendWidth = fieldLEDGERGROUP.Width; }
            if (isLedgerVisible) { extendWidth += fieldLEDGERNAME.Width; }
            //if (isGroupCodeVisible) { extendWidth += fieldGROUPCODE.Width; }
            //if (isLedgerCodeVisible) { extendWidth += fieldLEDGERCODE.Width; }

       //     if (fieldGROUPCODE.Visible) { extendWidth -= fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { extendWidth -= fieldLEDGERGROUP.Width; }
       //     if (fieldLEDGERCODE.Visible) { extendWidth -= fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { extendWidth -= fieldLEDGERNAME.Width; }

            if (fieldLEDGERGROUP.Visible)
            {
                fieldLEDGERGROUP.Width = fieldLEDGERGROUP.Width + extendWidth;
            }
            else
            {
                fieldLEDGERNAME.Width = fieldLEDGERNAME.Width + extendWidth;
            }

            //Hide Column Total
            xrPGAccountBalanceMulti.OptionsView.ShowColumnGrandTotalHeader = false;
            xrPGAccountBalanceMulti.OptionsView.ShowColumnGrandTotals = false;

            //Grid Lines
            if (isHorizontalLine)
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            }

            if (isVerticalLine)
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGAccountBalanceMulti.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            }
        }
    }
}
