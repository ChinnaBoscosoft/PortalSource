using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BranchInvestments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor

        public BranchInvestments()
        {
            InitializeComponent();

        }

        #endregion

        #region Variables
        public int Flag = 0;
        #endregion

        #region ShowReport


        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
                SetReportBorder();
            }
            else
            {
                BindReport();
                base.ShowReport();
            }
        }

        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindReport()
        {
            this.SetLandscapeHeader = 1060.25f;
            this.SetLandscapeFooter = 1060.25f;
            this.SetLandscapeFooterDateWidth = 900.00f;
            this.ReportTitle = objReportProperty.ReportTitle;
            setHeaderTitleAlignment();
            SetReportTitle();
            this.SetReportDate = this.ReportProperties.ReportDate;
            DateTime dtfrom = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false);
            DateTime dtto = UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false);
            this.ReportBranchName = string.Empty;
            this.ReportSubTitle = string.Empty;
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + dtfrom.AddYears(-5).ToShortDateString() + " - " + this.ReportProperties.DateTo;

            xrCellY1CapAmount.Text = "FY " + dtfrom.ToString("yyyy") + " - " + dtto.ToString("yy");
            xrCellY2CapAmount.Text = "FY " + dtfrom.AddYears(-1).ToString("yyyy") + " - " + dtto.AddYears(-1).ToString("yy");
            xrCellY3CapAmount.Text = "FY " + dtfrom.AddYears(-2).ToString("yyyy") + " - " + dtto.AddYears(-2).ToString("yy");
            xrCellY4CapAmount.Text = "FY " + dtfrom.AddYears(-3).ToString("yyyy") + " - " + dtto.AddYears(-3).ToString("yy");
            xrCellY5CapAmount.Text = "FY " + dtfrom.AddYears(-4).ToString("yyyy") + " - " + dtto.AddYears(-4).ToString("yy");
            xrCellY6CapAmount.Text = "FY " + dtfrom.AddYears(-5).ToString("yyyy") + " - " + dtto.AddYears(-5).ToString("yy");

            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtPayments= resultArgs.DataSource.Table;
                if (dtPayments != null)
                {
                    string TotalPayment = reportSetting1.BranchInvestment.FY1_PAYMENT_AMOUNTColumn.ColumnName + " + " +
                                          reportSetting1.BranchInvestment.FY2_PAYMENT_AMOUNTColumn.ColumnName + " + " +
                                          reportSetting1.BranchInvestment.FY3_PAYMENT_AMOUNTColumn.ColumnName + " + " +
                                          reportSetting1.BranchInvestment.FY4_PAYMENT_AMOUNTColumn.ColumnName + " + " +
                                          reportSetting1.BranchInvestment.FY5_PAYMENT_AMOUNTColumn.ColumnName + " + " +
                                          reportSetting1.BranchInvestment.FY6_PAYMENT_AMOUNTColumn.ColumnName;

                    DataColumn dcTotalPayment = new DataColumn(reportSetting1.BranchInvestment.TOTAL_PAYMENT_AMOUNTColumn.ColumnName, typeof(System.Double), TotalPayment);
                    dtPayments.Columns.Add(dcTotalPayment);

                    dtPayments.TableName = "MonthlyAbstract";
                    this.DataSource = dtPayments;
                    this.DataMember = dtPayments.TableName;
                }
            }

            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            this.ReportProperties.Project = this.GetBranchProjectIds(this.ReportProperties.BranchOffice);
            string sqlBranchFixedAssetInvestments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BranchFixedAssetsInvestments);
            //string liquidityGroupIds = this.GetLiquidityGroupIds();
            
            Int32 FixedAssetId = this.GetGroupId("Fixed Assets");
            string FixedAssetLedgerIds = this.GetLedgerIds(FixedAssetId);

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.BranchFixedAssetsInvestments, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, FixedAssetId);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, FixedAssetLedgerIds);
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlBranchFixedAssetInvestments);
            }

            return resultArgs;
        }

        private void SetReportSetting(AccountBalance accountBalance)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            SetReportBorder();

            tcCapAmountPeriod.Text = this.SetCurrencyFormat(tcCapAmountPeriod.Text);
            tcCapAmountProgress.Text = this.SetCurrencyFormat(tcCapAmountProgress.Text);

            //Attach / Detach all ledgers
            //dvPayment.RowFilter = "";
            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    dvPayment.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            //}

            //if (dvPayment.Count == 0)
            //{
            //    DataRowView drvPayment = dvPayment.AddNew();
            //    drvPayment.BeginEdit();
            //    drvPayment[reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName] = 0;
            //    drvPayment[reportSetting1.MonthlyAbstract.AMOUNT_PROGRESSIVEColumn.ColumnName] = 0;
            //    drvPayment[reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName] = 1;
            //    drvPayment.EndEdit();
            //}

            //Include / Exclude Code
            if (tcCapCode.Tag != null && tcCapCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcCapCode.Tag.ToString());
            }
            else
            {
                tcCapCode.Tag = tcCapCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcCapCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            

            //Include / Exclude Ledger group or Ledger
            
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        
        }

        private void SetReportBorder()
        {
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrTblData = AlignContentTable(xrTblData);
            xrTblTotal = AlignContentTable(xrTblTotal);
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
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Top;
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
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

       

        #endregion

     

       
       
    }
}
