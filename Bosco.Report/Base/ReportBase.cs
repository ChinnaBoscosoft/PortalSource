/*  Class Name      : ReportBase.cs
 *  Purpose         : Base Report Object
 *  Author          : CS
 *  Created on      : 12-Nov-2013
 */

using System;
using System.Collections.Generic;
using Bosco.Utility;

using Bosco.DAO.Data;
using Bosco.Report.SQL;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections;
using DevExpress.XtraPrinting;
using AcMEDSync.Model;
using System.Web;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Schema;
using System.ComponentModel;

namespace Bosco.Report.Base
{
    public class ReportBase : DevExpress.XtraReports.UI.XtraReport
    {
        private UserProperty userProperty = null;
        private CommonMember utilityMember = null;
        private ReportSQL reportSQL = null;
        private ReportSetting.ReportParameterDataTable reportParameters = null;
        private ReportSetting.LedgerDataTable reportLedgers = null;
        private ReportSetting.BranchReportsDataTable reportBranchProject = null;
        private ReportBankSQL reportBankSQL = null;
        private ReportFinalAccounts reportFinalAccounts = null;
        private GeneralateReports generalateReports = null;
        private ReportCostCenter reportCostCentre = null;
        private ReportMasters reportMasters = null;
        private ReportCashBankVoucher reportCashBankVoucher = null;
        private ReportForeginContribution reportForeginContribution = null;
        private ReportBudgetVariance reportBudget = null;
        private ReportProperty objReportProperty = new ReportProperty();
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private string reportId = "";
        private XRControlStyle styleInstitute;
        private XRControlStyle styleReportTitle;
        private XRControlStyle styleReportSubTitle;
        private XRControlStyle styleDateInfo;
        private XRControlStyle stylePageInfo;
        private XRControlStyle styleEvenRow;
        private XRControlStyle styleGroupRow;
        private XRControlStyle styleRow;
        private XRControlStyle styleColumnHeader;
        private XRControlStyle styleTotalRow;


        public event EventHandler<EventDrillDownArgs> ReportDrillDown;

        private bool isDrillDownMode = false;
        private XRControlStyle styleRowSmall;
        private XRControlStyle styleTotalRowSmall;
        private XRControlStyle styleColumnHeaderSmall;

        /// <summary>
        /// Adding collection of Keys and Values of Tables and Cells.
        /// </summary>
        private Dictionary<XRTable, XRTableCell> dicSupressLedgerCode = new Dictionary<XRTable, XRTableCell>();

        /// <summary>
        /// Report Values for Conditionals.
        /// </summary>
        public enum ReportValue
        {
            Remove = 0,
            Add = 1
        }

        public enum TransType
        {
            RC,
            PY,
            IC,
            EP,
            CRC,
            CPY,
            CINC,
            CEXP,
            JN

        }
        public enum TransMode
        {
            CR,
            DR
        }

        public enum BankType
        {
            Cleared,
            Uncleared,
            Realized,
            Unrealized
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportBase));
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.styleInstitute = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportSubTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleDateInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.stylePageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleEvenRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeader = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeaderSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            resources.ApplyResources(this.topMarginBand1, "topMarginBand1");
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            resources.ApplyResources(this.detailBand1, "detailBand1");
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            resources.ApplyResources(this.bottomMarginBand1, "bottomMarginBand1");
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // styleInstitute
            // 
            this.styleInstitute.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleInstitute.Name = "styleInstitute";
            this.styleInstitute.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportTitle
            // 
            this.styleReportTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportTitle.Name = "styleReportTitle";
            this.styleReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportSubTitle
            // 
            this.styleReportSubTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportSubTitle.Name = "styleReportSubTitle";
            this.styleReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportSubTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleDateInfo
            // 
            this.styleDateInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleDateInfo.Name = "styleDateInfo";
            this.styleDateInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.styleDateInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // stylePageInfo
            // 
            this.stylePageInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stylePageInfo.Name = "stylePageInfo";
            this.stylePageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.stylePageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleEvenRow
            // 
            this.styleEvenRow.BackColor = System.Drawing.Color.White;
            this.styleEvenRow.BorderColor = System.Drawing.Color.Silver;
            this.styleEvenRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleEvenRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleEvenRow.BorderWidth = 1F;
            this.styleEvenRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleEvenRow.Name = "styleEvenRow";
            this.styleEvenRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleGroupRow
            // 
            this.styleGroupRow.BackColor = System.Drawing.Color.Linen;
            this.styleGroupRow.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleGroupRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.styleGroupRow.BorderWidth = 1F;
            this.styleGroupRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRow.Name = "styleGroupRow";
            this.styleGroupRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleRow
            // 
            this.styleRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRow.BorderWidth = 1F;
            this.styleRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRow.Name = "styleRow";
            this.styleRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleColumnHeader
            // 
            this.styleColumnHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeader.BorderWidth = 1F;
            this.styleColumnHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeader.Name = "styleColumnHeader";
            this.styleColumnHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleTotalRow
            // 
            this.styleTotalRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRow.BorderWidth = 1F;
            this.styleTotalRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRow.Name = "styleTotalRow";
            this.styleTotalRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleRowSmall
            // 
            this.styleRowSmall.BackColor = System.Drawing.Color.Empty;
            this.styleRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRowSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRowSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRowSmall.BorderWidth = 1F;
            this.styleRowSmall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRowSmall.Name = "styleRowSmall";
            this.styleRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRowSmall.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleTotalRowSmall
            // 
            this.styleTotalRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRowSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRowSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRowSmall.BorderWidth = 1F;
            this.styleTotalRowSmall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRowSmall.Name = "styleTotalRowSmall";
            this.styleTotalRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleColumnHeaderSmall
            // 
            this.styleColumnHeaderSmall.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeaderSmall.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeaderSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeaderSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeaderSmall.BorderWidth = 1F;
            this.styleColumnHeaderSmall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeaderSmall.Name = "styleColumnHeaderSmall";
            this.styleColumnHeaderSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // ReportBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleInstitute,
            this.styleReportTitle,
            this.styleReportSubTitle,
            this.styleDateInfo,
            this.stylePageInfo,
            this.styleEvenRow,
            this.styleGroupRow,
            this.styleRow,
            this.styleColumnHeader,
            this.styleTotalRow,
            this.styleRowSmall,
            this.styleTotalRowSmall,
            this.styleColumnHeaderSmall});
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        public ReportBase()
        {

        }

        public UserProperty LoginUser
        {
            get
            {
                if (userProperty == null) { userProperty = new UserProperty(); }
                return userProperty;
            }
        }

        public bool IsDrillDownMode
        {
            get
            {
                return (HttpContext.Current.Session["DrillDownProperties"] != null &&
                      ((Dictionary<string, object>)HttpContext.Current.Session["DrillDownProperties"]).Count > 1);
            }
        }


        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        protected ReportProperty ReportProperties
        {
            get
            {
                return objReportProperty.Current;
            }
        }

        protected ReportSetting.ReportParameterDataTable ReportParameters
        {
            get
            {
                if (reportParameters == null) { reportParameters = new ReportSetting.ReportParameterDataTable(); }
                return reportParameters;
            }
        }

        protected ReportSetting.LedgerDataTable LedgerParameters
        {
            get
            {
                if (reportLedgers == null) { reportLedgers = new ReportSetting.LedgerDataTable(); }
                return reportLedgers;
            }
        }

        protected ReportSetting.BranchReportsDataTable BranchProjectParameters
        {
            get
            {
                if (reportBranchProject == null)
                {
                    reportBranchProject = new ReportSetting.BranchReportsDataTable();
                }
                return reportBranchProject;
            }
        }

        protected string GetReportSQL(ReportSQLCommand.Report queryId)
        {
            if (reportSQL == null) { reportSQL = new ReportSQL(); }
            string sql = reportSQL.GetReportSQL(queryId);
            return sql;
        }
        protected string GetMasterSQL(ReportSQLCommand.Masters queryId)
        {
            if (reportMasters == null) { reportMasters = new ReportMasters(); }
            string sql = reportMasters.GetReportSQL(queryId);
            return sql;
        }

        protected string GetBankReportSQL(ReportSQLCommand.BankReport queryId)
        {
            if (reportBankSQL == null) { reportBankSQL = new ReportBankSQL(); }
            string sql = reportBankSQL.GetReportSQL(queryId);
            return sql;
        }

        protected string GetFinalAccountsReportSQL(ReportSQLCommand.FinalAccounts queryId)
        {
            if (reportFinalAccounts == null) { reportFinalAccounts = new ReportFinalAccounts(); }
            string sql = reportFinalAccounts.GetReportSQL(queryId);
            return sql;
        }

        protected string GetGeneralateReportSQL(ReportSQLCommand.Generalate queryId)
        {
            if (generalateReports == null) { generalateReports = new GeneralateReports(); }
            string sql = generalateReports.GetGeneralateSQL(queryId);
            return sql;
        }

        protected string GetReportCostCentre(ReportSQLCommand.CostCentre queryId)
        {
            if (reportCostCentre == null) { reportCostCentre = new ReportCostCenter(); }
            string sql = reportCostCentre.GetCostCenterSQL(queryId);
            return sql;
        }

        protected string GetReportCashBankVoucher(ReportSQLCommand.CashBankVoucher queryId)
        {
            if (reportCashBankVoucher == null) { reportCashBankVoucher = new ReportCashBankVoucher(); }
            string sql = reportCashBankVoucher.GetCashBankSQL(queryId);
            return sql;
        }

        protected string GetReportForeginContribution(ReportSQLCommand.ForeginContribution queryId)
        {
            if (reportForeginContribution == null) { reportForeginContribution = new ReportForeginContribution(); }
            string sql = reportForeginContribution.GetReportSQL(queryId);
            return sql;
        }

        protected string GetBudgetvariance(ReportSQLCommand.BudgetVariance queryId)
        {
            if (reportBudget == null) { reportBudget = new ReportBudgetVariance(); }
            string sql = reportBudget.GetReportSQL(queryId);
            return sql;
        }

        public string ReportId
        {
            get { return reportId; }
            set { this.reportId = value; }
        }

        public void ShowReportFilterDialog()
        {
            this.ShowReport();
        }

        public void ShowFiancialReportFilterDialog()
        {
            this.ShowReport();
        }

        public virtual void ShowReport()
        {
            this.CreateDocument(true);
        }

        public virtual void VoucherPrint()
        {
        }

        protected string GetLiquidityGroupIds()
        {
            string groupIds = (int)FixedLedgerGroup.BankAccounts + "," +
                              (int)FixedLedgerGroup.Cash + "," +
                              (int)FixedLedgerGroup.FixedDeposit;
            return groupIds;
        }

        protected string GetProgressiveDate(string dateValue)
        {
            string prgDate = dateValue;
            string sqlAcYear = this.GetReportSQL(SQL.ReportSQLCommand.Report.AccountYear);

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Report.AccountYear, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlAcYear);


                DataTable dtAcYear = resultArgs.DataSource.Table;

                if (dtAcYear != null && dtAcYear.Rows.Count > 0)
                {
                    prgDate = dtAcYear.Rows[0][this.ReportParameters.YEAR_FROMColumn.ColumnName].ToString();
                    prgDate = DateTime.Parse(prgDate).ToShortDateString();
                }
            }

            return prgDate;

        }

        /// <summary>
        /// Add or Remove the Ledger code based on the XRTable and XRTableCell
        /// </summary>
        /// <param name="xrTable"></param>
        /// <param name="xrTblCell"></param>
        public void AddSuppresLedgerCode(XRTable xrTable, XRTableCell xrTblCell)
        {

            if (objReportProperty.ShowLedgerCode == 0 && xrTable.Rows.FirstRow.Cells.IndexOf(xrTblCell) >= 0)
            {
                xrTable.Rows.FirstRow.Cells.Remove(xrTblCell);
            }
            else if (objReportProperty.ShowLedgerCode == 1 && xrTable.Rows.FirstRow.Cells.IndexOf(xrTblCell) <= 0)
            {
                xrTable.Rows.FirstRow.InsertCell(xrTblCell, 0);
                xrTblCell.WidthF = objReportProperty.NumberSet.ToInteger(objReportProperty.SetWithForCode.ToString());
            }
            dicSupressLedgerCode.Add(xrTable, xrTblCell);
        }

        public bool AddHorizontalLine()
        {
            return objReportProperty.ShowHorizontalLine != 0 ? true : false;
        }

        public bool AddNarration()
        {
            return objReportProperty.IncludeNarration != 0 ? true : false;
        }

        public bool AddVerticalLine()
        {
            return objReportProperty.ShowVerticalLine != 0 ? true : false;
        }

        public bool AddReportTitles()
        {
            return objReportProperty.ShowTitles != 0 ? true : false;
        }
        //To allign ordinary tables
        public XRTable SetBorders(XRTable table, int HorizontalLine, int VerticalLine)
        {

            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (HorizontalLine == 1)
                    {
                        if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = System.Drawing.Color.Gainsboro;
                }
            }
            return table;
        }
        // To allign heading tables
        public XRTable SetHeadingTableBorder(XRTable table, int HorizontalLine, int VerticalLine)
        {

            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (HorizontalLine == 1)
                    {
                        if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = System.Drawing.Color.DarkGray;
                }
            }
            return table;
        }

        // to aling cash bank book report table
        public virtual XRTable AlignCashBankBookTable(XRTable table, string bankNarration, string cashNarration, int count)
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
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
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
                                if (count == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                else if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
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
        // To allign table which inclues narration
        public XRTable AlignTable(XRTable table, string bankNarration, string cashNarration, int count)
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
                            if (count == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                            else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            if (cellcount == 6)
                                cellcount = 1;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (count == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            if (cellcount == 6)
                                cellcount = 1;
                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                            if (cellcount == 6)
                                cellcount = 1;
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
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                }
                                else if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
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
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            if (cellcount == 6)
                            {
                                cellcount = 1;
                            }
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                else if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            if (cellcount == 6)
                            {
                                cellcount = 1;
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
                            if (cellcount == 6)
                                cellcount = 1;
                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                    cell.BorderColor = System.Drawing.Color.Gainsboro;
                }
            }
            return table;
        }

        public virtual XRTable AlignClosingBalance(XRTable table)
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
                            if (ReportProperties.ShowDetailedBalance == 1)
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (ReportProperties.ShowDetailedBalance == 1)
                            {
                                if (count == 3 || count == 9)
                                    tcell.Borders = BorderSide.None;
                                else
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                            else
                            {
                                if (count == 3 || count == 9)
                                    tcell.Borders = BorderSide.None;
                                else
                                    tcell.Borders = BorderSide.Right;
                            }
                        else
                            if (ReportProperties.ShowDetailedBalance == 1)
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Right;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
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
                            if (count == 3 || count == 9)
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
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        // to align header tables
        public virtual XRTable AlignHeaderTable(XRTable table)
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
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
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

        public virtual XRTable AlignOpeningBalanceTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        // to align content tables - chinnaa
        public virtual XRTable AlignContentTable(XRTable table)
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
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
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
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (count == 1)
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
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }


        public virtual XRTable AlignGroupTable(XRTable table)
        {
            return AlignGroupTable(table, false);
        }
        // to align group tables
        public virtual XRTable AlignGroupTable(XRTable table, bool IsBorderAll = false)
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
                        if (IsBorderAll)
                        {
                            tcell.Borders = BorderSide.All;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
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

        // to align total tables
        public virtual XRTable AlignTotalTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        // to align grand total tables
        public virtual XRTable AlignGrandTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;

        }

        public XRTable SetLedgerGroupeBorders(XRTable table)
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
                            //if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            //{
                            //    tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            //}
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                            //if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            //{
                            //    tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            //}
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }

            return table;
        }

        public XRTable SetTableBorder(XRTable table)
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
                            //if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            //{
                            //    tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            //}
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        // For only Receipts and Payments & Income and Expenditure Heeader table 
        public XRTable HeadingTableBorder(XRTable table, int HorizontalLine, int VerticalLine)
        {

            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
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
                    else if (HorizontalLine == 1)
                    {
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (VerticalLine == 1)
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

        public XRTable SetGrandTotalTableBorders(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
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
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public XRTable SetBalanceTableBorders(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | BorderSide.Right | BorderSide.Top;
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
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public string GetBankNamesByProject()
        {
            ResultArgs resultArgs = new ResultArgs();
            string rtn = string.Empty;
            string projectid = objReportProperty.Current.ProjectId;
            string bankclosedDate = objReportProperty.Current.DateFrom;
            if (!string.IsNullOrEmpty(objReportProperty.Current.DateFrom))
            {
                bankclosedDate = UtilityMember.DateSet.ToDate(objReportProperty.Current.DateFrom, false).AddMonths(-1).ToShortDateString();
            }

            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankByProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(ReportParameters.PROJECT_IDColumn, projectid);
                if (!string.IsNullOrEmpty(bankclosedDate))
                {
                    dataManager.Parameters.Add(ReportParameters.DATE_CLOSEDColumn, bankclosedDate);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBankNames = resultArgs.DataSource.Table;
                    foreach (DataRow dr in dtBankNames.Rows)
                    {
                        rtn += dr[ReportParameters.BANKColumn.ColumnName].ToString() + ",";
                    }
                    rtn = rtn.TrimEnd(',');
                }
            }

            return rtn;
        }

        #region Drill-Down Methods
        /// <summary>
        /// This method is used to attach report drill down event to subreports with in the main report
        /// </summary>
        /// <param name="rptSubReport"></param>
        public void AttachDrillDownToSubReport(ReportBase rptSubReport)
        {
            if (ReportDrillDown != null)
            {
                rptSubReport.ReportDrillDown += new EventHandler<EventDrillDownArgs>(ReportDrillDown);
            }
        }

        /// <summary>
        /// This method is used to attach BeforePrint, double-click event to all the fields of the record
        /// </summary>
        /// <param name="xrRptTable"></param>
        /// <param name="xrLinkField"></param>
        /// <summary>
        /// This method is used to attach BeforePrint, double-click event to all the fields of the record
        /// </summary>
        /// <param name="xrRptTable"></param>
        /// <param name="xrLinkField"></param>
        public void AttachDrillDownToRecord(XRTable xrRptTable, XRTableCell xrLinkField,
                                   ArrayList arylnkFields, DrillDownType lnkDrillDownType, bool bAttachAllFields, string VoucherType = "", bool hasNarration = false)
        {
            xrLinkField.BeforePrint += new System.Drawing.Printing.PrintEventHandler(RptField_BeforePrint);
            if (bAttachAllFields)
            {
                foreach (XRTableCell RptField in xrRptTable.Rows.FirstRow.Cells)
                {
                    RptField.PreviewDoubleClick += new PreviewMouseEventHandler(RptField_PreviewDoubleClick);
                }
            }
            if (hasNarration)
            {
                // ReportProperties.IncludeNarration = 1;
            }
            string drilldownsource = string.Empty;
            foreach (string linkField in arylnkFields)
            {
                drilldownsource += (drilldownsource != string.Empty ? Delimiter.Mew : "") + lnkDrillDownType.ToString() + Delimiter.ECap + linkField + Delimiter.ECap + VoucherType;
            }

            if (drilldownsource != string.Empty)
            {
                xrLinkField.PreviewDoubleClick += new PreviewMouseEventHandler(RptField_PreviewDoubleClick);
                xrLinkField.PreviewMouseMove += new PreviewMouseEventHandler(xrLinkField_PreviewMouseMove);
                xrLinkField.Target = drilldownsource;
            }
        }

        void xrLinkField_PreviewMouseMove(object sender, PreviewMouseEventArgs e)
        {
            //Cursor.Current = Cursors.Hand;
        }

        /// <summary>
        /// This evenet is used to hold current printing record to link field of the record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RptField_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Tag = GetCurrentRow();
        }

        /// <summary>
        /// This method is used to catch user's drill-down event on the record,
        /// and assing which drill report whould be shown(drill-downed) and drill information into EventDrillDownArgs
        /// and triggerd ReportDrillDown event to ReportViewer Usercontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RptField_PreviewDoubleClick(object sender, PreviewMouseEventArgs e)
        {
            if (e.Brick.Value != null && e.Brick.Value.GetType() == typeof(DataRowView))
            {
                if (ReportDrillDown != null)
                {
                    DataRowView dataRow = e.Brick.Value as DataRowView;
                    XRTableRow xrtblRecord = ((XRTableCell)sender).Row;
                    DrillDown(xrtblRecord, ((XRTableCell)sender), dataRow);
                }
            }
        }

        /// <summary>
        /// This method is used to catch user's drill-down event on the record,
        /// and assing which drill report whould be shown(drill-downed) and drill information into EventDrillDownArgs
        /// and triggerd ReportDrillDown event to ReportViewer Usercontrol
        /// </summary>
        /// <param name="xrtblDrillRecord"></param>
        /// <param name="dataDrillDataRow"></param>
        private void DrillDown(XRTableRow xrtblDrillRecord, XRTableCell xrTblCell, DataRowView dataDrillDataRow)
        {
            try
            {
                Dictionary<string, object> dicDrillDownProperties = new Dictionary<string, object>();
                DrillDownType ddtypeLinkType = DrillDownType.DRILL_DOWN;
                dicDrillDownProperties.Add("DrillDownLink", ddtypeLinkType.ToString());

                string DrillToRptId = UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.DRILL_DOWN);
                if (isValidDrillDownRecord(xrtblDrillRecord) && dataDrillDataRow != null)
                {
                    string[] drilldownItmes = xrTblCell.Target.Split(Delimiter.Mew.ToCharArray());
                    foreach (string drilldownItem in drilldownItmes)
                    {
                        string[] sDrillDownLink = drilldownItem.Split(Delimiter.ECap.ToCharArray());
                        if (sDrillDownLink.Length >= 2)
                        {
                            string sLinkField = sDrillDownLink.GetValue(1).ToString();
                            string sVoucherSubTypeField = sDrillDownLink.GetValue(2).ToString();
                            string sLinkFieldValue = string.Empty;
                            string sVoucherType = string.Empty;
                            if (sLinkField == this.ReportParameters.COST_CENTRE_IDColumn.ColumnName)
                            {
                                sLinkFieldValue = this.ReportProperties.CostCentre;
                                sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                            }
                            else if (sLinkField == this.ReportParameters.DATE_AS_ONColumn.ColumnName)
                            {
                                sLinkFieldValue = this.ReportProperties.DateAsOn;
                                sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                            }
                            else
                            {
                                sLinkFieldValue = dataDrillDataRow[sLinkField].ToString();
                                sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                            }

                            ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), sDrillDownLink.GetValue(0).ToString());
                            if ((ddtypeLinkType == DrillDownType.DRILL_DOWN ||
                                ddtypeLinkType == DrillDownType.LEDGER_VOUCHER) && sVoucherType == ledgerSubType.GN.ToString())
                            {
                                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dataDrillDataRow["PARTICULAR_TYPE"].ToString());
                            }
                            else if (sVoucherType == ledgerSubType.FD.ToString())
                            {
                                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), "FD_VOUCHER");
                            }

                            DrillToRptId = UtilityMember.EnumSet.GetDescriptionFromEnumValue(ddtypeLinkType);
                            dicDrillDownProperties["DrillDownLink"] = ddtypeLinkType.ToString();
                            dicDrillDownProperties.Add(sLinkField, sLinkFieldValue);
                            if (!string.IsNullOrEmpty(sVoucherSubTypeField) && !string.IsNullOrEmpty(sVoucherType) && sLinkField != this.ReportParameters.DATE_AS_ONColumn.ColumnName)
                            {
                                dicDrillDownProperties.Add(sVoucherSubTypeField, sVoucherType);
                            }
                        }
                    }

                    //Define DrillDown properties
                    if (dicDrillDownProperties.Count > 1)
                    {
                        EventDrillDownArgs eventdrilldownArg = new EventDrillDownArgs(ddtypeLinkType, DrillToRptId, dicDrillDownProperties);
                        ReportDrillDown(this, eventdrilldownArg);
                    }
                }
            }
            catch (Exception Err)
            {
                //MessageBox.Show(Err.Message)
            }
        }

        /// <summary>
        /// check whether, xratable record row has drilldown link information 
        /// </summary>
        /// <param name="xrtblRecord"></param>
        /// <returns></returns>
        private bool isValidDrillDownRecord(XRTableRow xrtblRecord)
        {
            bool bValid = false;
            foreach (XRTableCell xrTblCell in xrtblRecord.Cells)
            {
                if (xrTblCell.Target != string.Empty)
                {
                    bValid = true;
                    break;
                }
            }
            return bValid;
        }
        #endregion

        #region Get Balances
        public Double GetBalance(string ProjectId, string RptDate, BalanceSystem.LiquidBalanceGroup BalanceGroup,
            BalanceSystem.BalanceType BalanceType)
        {
            double rtnBalance = 0;
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                AcMEDSync.Model.BalanceProperty balanceProperty = null;
                switch (BalanceGroup)
                {
                    case BalanceSystem.LiquidBalanceGroup.CashBalance:
                        balanceProperty = balanceSystem.GetCashBalance(this.ReportProperties.BranchOffice, ProjectId,
                                    RptDate, BalanceType);

                        break;
                    case BalanceSystem.LiquidBalanceGroup.BankBalance:
                        balanceProperty = balanceSystem.GetBankBalance(this.ReportProperties.BranchOffice, ProjectId,
                                    "0", RptDate, BalanceType);

                        break;
                    case BalanceSystem.LiquidBalanceGroup.FDBalance:
                        balanceProperty = balanceSystem.GetFDBalance(this.ReportProperties.BranchOffice, ProjectId,
                                    RptDate, BalanceType);
                        break;
                }

                if (balanceProperty != null && balanceProperty.Result.Success)
                {
                    if (balanceProperty.TransMode == TransactionMode.CR.ToString())
                    {
                        rtnBalance = -UtilityMember.NumberSet.ToDouble(balanceProperty.Amount.ToString());
                    }
                    else
                    {
                        rtnBalance = balanceProperty.Amount;
                    }

                }
            }
            return rtnBalance;
        }

        public ResultArgs GetBalanceDetail(bool isOpening, string balDate, string projectIds, string groupIds)
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);

                if (isOpening)
                {
                    //For Opening Balance, Finding Balance Date
                    DateTime dateBalance = DateTime.Parse(balDate).AddDays(-1);
                    balDate = dateBalance.ToShortDateString();
                }

                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                string bankcloseddate = balDate;
                if (!string.IsNullOrEmpty(bankcloseddate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, bankcloseddate);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;

            //ResultArgs resultArgs = new ResultArgs();
            //using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance))
            //{
            //    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
            //    dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);

            //    if (isOpening)
            //    {
            //        //For Opening Balance, Finding Balance Date
            //        DateTime dateBalance = DateTime.Parse(balDate).AddDays(-1);
            //        balDate = dateBalance.ToShortDateString();
            //    }

            //    dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

            //    //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            //    string bankcloseddate = balDate;
            //    if (!string.IsNullOrEmpty(bankcloseddate))
            //    {
            //        dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, bankcloseddate);
            //    }

            //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            //    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            //}
            //return resultArgs;
        }

        #endregion

    }

    /// <summary>
    /// This class is used to hold drill-down information
    /// send through drilldown event to ui
    /// </summary>
    public class EventDrillDownArgs : EventArgs
    {
        private string drillDownRpt = string.Empty;
        private DrillDownType drillDownType = DrillDownType.DRILL_DOWN;
        private Dictionary<string, object> dicDrillDownProperties = new Dictionary<string, object>();

        public EventDrillDownArgs(DrillDownType ddType, string ddRptId, Dictionary<string, object> dicDDProperties)
        {
            drillDownType = ddType;
            this.drillDownRpt = ddRptId;
            this.dicDrillDownProperties = dicDDProperties;
        }

        public DrillDownType DrillDownType
        {
            get
            {
                return drillDownType;
            }
        }

        public string DrillDownRpt
        {
            get
            {
                return drillDownRpt;
            }
        }

        public Dictionary<string, object> DrillDownProperties
        {
            get
            {
                return dicDrillDownProperties;
            }
        }
    }

    public class ReportCommandHandler : ICommandHandler
    {

        private XtraReport Rpt;

        public ReportCommandHandler(XtraReport xrRpt)
        {
            this.Rpt = xrRpt;
        }

        public virtual void HandleCommand(PrintingSystemCommand command,
    object[] args, IPrintControl control, ref bool handled)
        {
            if (!CanHandleCommand(command, control)) return;

            if (Rpt.FindControl("tcLedgerName", false) != null)
                Rpt.FindControl("tcLedgerName", false).Visible = false;

            handled = true;
        }

        public virtual bool CanHandleCommand(PrintingSystemCommand command, IPrintControl control)
        {
            return command == PrintingSystemCommand.Print || command == PrintingSystemCommand.PrintDirect;
        }

    }
}
