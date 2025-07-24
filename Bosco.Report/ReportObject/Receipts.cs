using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using System.Data;
using DevExpress.XtraPrinting;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class Receipts : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public Receipts()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrtblReceiptGroup, xrGroupName,
                    new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_RECEIPTS, false);
            this.AttachDrillDownToRecord(xrTableReceipt, xrLedgerName,
                new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY, false, "", true);
        }

        #endregion

        #region Decelaration
        public double ReceiptAmount { get; set; }
        private int CountReceipt = 0;
        SettingProperty settingPropery = new SettingProperty();
        #endregion

        #region Show Reports
        public override void ShowReport()
        {

            BindReceiptSource(TransType.RC);
            base.ShowReport();
        }
        #endregion

        #region Properties
        public float CodeColumnWidth
        {
            set
            {
                xrLedgerCode.WidthF = value;

            }
        }
        public float GroupCodeColumnWidth
        {
            set
            { xrGroupCode.WidthF = value; }
        }

        public float NameColumnWidth
        {
            set
            {
                xrLedgerName.WidthF = value;
            }
        }
        public float GroupNameColumnWidth
        {
            set { xrGroupName.WidthF = value; }
        }

        /// <summary>
        /// Enabled by chinna at 30.07.2021
        /// </summary>
        /// 
        public float ParentGroupCodeColumnWidth
        {
            set { xrParentGroupCode.WidthF = value; }
        }

        public float ParentGroupNameColumnWidth
        {
            set { xrParentgroupName.WidthF = value; }
        }

        public float ParentGroupAmountColumnWidth
        {
            set { xrParentGroupAmount.WidthF = value; }
        }

        public float AmountColumnWidth
        {
            set
            {
                xrLedgerAmt.WidthF = value;
            }
        }
        public float GroupAmountColumnWidth
        {
            set { xrGroupAmt.WidthF = value; }
        }
        private bool hideCostcentre = false;
        public bool HideCostCentreReceipts
        {
            get { return hideCostcentre; }
            set { hideCostcentre = value; }
        }

        public float CostCentreCategoryNameWidth
        {
            set { xrCostCentreCategoryName.WidthF = value; }
        }

        public bool PaymentCostCentreNameVisible
        {
            set { xrPaymentCostCentreName.Visible = value; }
        }

        public float CostCentreWidth { set { xrtblCellCostcentreName.WidthF = value; } }

        public float CostCategoryAmountWidth
        {
            get { return xrcellCCCAmount.WidthF; }
            set { xrcellCCCAmount.WidthF = value; }
        }

        #endregion

        #region Methods
        public void HideReceiptReportHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        private ResultArgs SetReportReceiptSource(TransType transType)
        {
            this.ReportProperties.ShowGroupCode = (LoginUser.IS_CMFCHE_CONGREGATION) ? 1 : 0;

            ResultArgs resultArgs = null;
            string sqlReceipts = string.Empty;
            object sqlCommandId = string.Empty;
            if (transType == TransType.RC)
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Receipts);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Receipts;
                if (objReportProperty.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CRC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreReceipts);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentreReceipts;
                if (objReportProperty.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else if (transType == TransType.CINC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreIncome);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentreIncome;
                if (objReportProperty.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Income);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Income;
            }
            using (DataManager dataManager = new DataManager(sqlCommandId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.settingPropery.BookBeginFrom);

                int LedgerPaddingRequired = (ReportProperties.ShowByLedger == 1) ? 1 : 0; //(ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0; //(ReportProperties.ShowGroupCode) ? 1 : 0; //(ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");

                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceipts);
            }
            return resultArgs;
        }

        public void BindReceiptSource(TransType transType)
        {
            ReceiptAmount = 0;
            ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtReceipts = resultArgs.DataSource.Table;

            /* if (transType.Equals(TransType.RC))
             {
                 DataTable dtJournal = GetJournalSource();
                 if (dtJournal != null && dtJournal.Rows.Count > 0)
                 {
                     foreach (DataRow dr in dtJournal.Rows)
                     {
                         int LedgerId = objReportProperty.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                         decimal Amount = objReportProperty.NumberSet.ToDecimal(dr["RECEIPTAMT"].ToString());

                         foreach (DataRow drReceipt in dtReceipts.Rows)
                         {
                             if (LedgerId.Equals(this.UtilityMember.NumberSet.ToInteger(drReceipt["LEDGER_ID"].ToString())))
                             {
                                 decimal AmountPeriod = objReportProperty.NumberSet.ToDecimal(drReceipt["RECEIPTAMT"].ToString());
                                 drReceipt.SetField("RECEIPTAMT", Amount + AmountPeriod);
                                 CountReceipt++;
                                 break;
                             }
                         }

                     }

                     if (CountReceipt == 0)
                     {
                         DataTable dtReceiptEmptyTable = ConstructReceiptTable();
                         dtReceiptEmptyTable = dtJournal.DefaultView.ToTable(false, new string[] { "LEDGER_ID", "GROUP_ID", "GROUP_CODE", "LEDGER_GROUP", "LEDGER_CODE", "LEDGER_NAME", "RECEIPTAMT" });
                         //  DataTable DT = dtReceiptEmptyTable.AsEnumerable().Select("LEDGER_ID, GROUP_ID, GROUP_CODE, LEDGER_GROUP, LEDGER_CODE, LEDGER_NAME, SUM(RECEIPTAMT) AS RECEIPTAMT  GROUP BY LEDGER_ID").CopyToDataTable();
                         double Recptamt = this.UtilityMember.NumberSet.ToDouble(dtReceiptEmptyTable.Compute("SUM(RECEIPTAMT)", "").ToString());
                         if (Recptamt > 0)
                         {
                             dtReceipts.Merge(dtReceiptEmptyTable);
                         }

                         //DataTable dtLedgerAmount = dtReceiptEmptyTable.AsEnumerable().GroupBy(r => r.Field<UInt32?>("LEDGER_ID")).Select(g => g.First()).CopyToDataTable();
                     }
                 }
             }*/

            if (transType == TransType.IC || transType == TransType.RC || transType == TransType.CRC || transType == TransType.CINC)
            {
                if (dtReceipts != null && dtReceipts.Rows.Count != 0)
                {
                    ReceiptAmount = this.UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(RECEIPTAMT)", "").ToString());
                }
            }

            if (dtReceipts != null)
            {
                dtReceipts.TableName = "Receipts";
                this.DataSource = dtReceipts;
                this.DataMember = dtReceipts.TableName;
            }

            SetReportSetting();
            SetReportBorder();
            SortByLedgerorGroup();
        }

        private DataTable GetJournalSource()
        {
            ResultArgs resultArgs = null;
            string sqlReceiptJournal = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalReceiptJournal);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceiptJournal);
            }
            return resultArgs.DataSource.Table;
        }

        private void SetReportSetting()
        {
            float actualCodeWidth = xrGroupCode.WidthF;
            bool isCapCodeVisible = true;
            bool isGroupCodeVisible = false;

            //Include / Exclude Code
            if (xrGroupCode.Tag != null && xrGroupCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrGroupCode.Tag.ToString());
            }
            else
            {
                xrGroupCode.Tag = xrGroupCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            xrParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpReceiptGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpReceiptLedger.Visible = ReportProperties.ShowByLedger == 1 || ReportProperties.ShowByCostCentre == 1;
            grpCostCentreNameReceipts.Visible = (ReportProperties.ShowByCostCentre == 1);
            grpCCBreakup.Visible = (ReportProperties.BreakByCostCentre == 1);
            grpcostCenterCategory.Visible = ReportProperties.ShowByCostCentreCategory == 1;


            if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")
            {
                if (ReportProperties.ShowByCostCentreCategory == 1)
                    grpcostCenterCategory.Visible = true;
                else
                    grpcostCenterCategory.Visible = false;
            }

            if (grpCostCentreNameReceipts.Visible == true)
            {
                this.CosCenterName = objReportProperty.CostCentreName;
                HideCostCentreReceipts = true;
            }
            else
            {
                this.CosCenterName = string.Empty;
                HideCostCentreReceipts = false;
            }

            grpParentGroup.GroupFields[0].FieldName = "";
            grpReceiptGroup.GroupFields[0].FieldName = "";
            grpReceiptLedger.GroupFields[0].FieldName = "";

            if (grpReceiptGroup.Visible == false && grpReceiptLedger.Visible == false)
            {
                // This code add by Amal
                if (ReportProperties.ReportId != "RPT-041" && ReportProperties.ReportId != "RPT-049")
                    grpReceiptLedger.Visible = true;
            }

            if (grpParentGroup.Visible)
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName;
                }
                else
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName;
                }
            }

            if (grpReceiptGroup.Visible)
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpReceiptGroup.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpReceiptGroup.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName;
                }
            }
            if (grpReceiptLedger.Visible)
            {
                if (ReportProperties.ShowByLedger == 1)
                {
                    grpReceiptLedger.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpReceiptLedger.GroupFields[0].FieldName = reportSetting1.Receipts.LEDGER_NAMEColumn.ColumnName;
                }
            }

            xrParentGroupCode.Visible = xrGroupCode.Visible = (this.LoginUser.IS_CMFCHE_CONGREGATION) ? true : false;

        }

        private void SetReportBorder()
        {
            xrParentGroup = AlignGroupTable(xrParentGroup);
            xrtblReceiptGroup = AlignGroupTable(xrtblReceiptGroup);
            xrTableReceipt = AlignContentTable(xrTableReceipt);
            xrPaymentCostCentreName = AlignCostCentreTable(xrPaymentCostCentreName);
            xrTblCostCentreCategoryName = AlignCCCategoryTable(xrTblCostCentreCategoryName);
            if (ReportProperties.ReportId == "RPT-028" || ReportProperties.ReportId == "RPT-034")
                xrTblCostCentreCategoryName.Visible = false;
            xrCCBreakup = AlignContentTable(xrCCBreakup);
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

        public override XRTable AlignContentTable(XRTable table)
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
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = BorderSide.Left;
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
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
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
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
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
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

        public override XRTable AlignGroupTable(XRTable table)
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
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Right;
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


        public ResultArgs GetIncomeSource(TransType transType)
        {

            ResultArgs resultArgs = null;
            string sqlReceipts = string.Empty;
            object sqlCommandId = string.Empty;
            if (transType == TransType.RC)
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Receipts);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Receipts;
                //if (objReportProperty.ShowGroupCode != 0)
                //{
                //    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                //}
            }
            else if (transType == TransType.CRC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreReceipts);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentreReceipts;
                if (objReportProperty.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else if (transType == TransType.CINC)
            {
                sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreIncome);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentreIncome;
                if (objReportProperty.ShowGroupCode != 0)
                {
                    xrGroupAmt.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else
            {
                sqlReceipts = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Income);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Income;
            }
            using (DataManager dataManager = new DataManager(sqlCommandId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.settingPropery.BookBeginFrom);

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
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceipts);
            }
            return resultArgs;

        }

        public void BindIncomeSource(ResultArgs resultArgs, TransType transType)
        {
            ReceiptAmount = 0;
            //ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtReceipts = resultArgs.DataSource.Table;

            if (transType == TransType.IC || transType == TransType.RC || transType == TransType.CRC || transType == TransType.CINC)
            {
                if (dtReceipts != null && dtReceipts.Rows.Count != 0)
                {
                    ReceiptAmount = this.UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(RECEIPTAMT)", "").ToString());
                }
            }


            if (dtReceipts != null)
            {
                dtReceipts.TableName = "Receipts";
                this.DataSource = dtReceipts;
                this.DataMember = dtReceipts.TableName;
            }

            SetReportBorder();
            SetReportSetting();
            SortByLedgerorGroup();
        }

        public void SetCostCategoryTableBorder()
        {
            xrTblCostCentreCategoryName = SetHeadingTableBorder(xrTblCostCentreCategoryName, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
        }

        private DataTable ConstructReceiptTable()
        {
            DataTable dtReceipts = new DataTable();
            dtReceipts.Columns.Add("LEDGER_ID", typeof(UInt32));
            dtReceipts.Columns.Add("GROUP_ID", typeof(UInt32));
            dtReceipts.Columns.Add("GROUP_CODE", typeof(string));
            dtReceipts.Columns.Add("LEDGER_GROUP", typeof(string));
            dtReceipts.Columns.Add("LEDGER_CODE", typeof(string));
            dtReceipts.Columns.Add("LEDGER_NAME", typeof(string));
            dtReceipts.Columns.Add("RECEIPTAMT", typeof(decimal));
            return dtReceipts;
        }

        public void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            if (grpReceiptGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpReceiptGroup.SortingSummary.Enabled = true;
                    grpReceiptGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                    grpReceiptGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpReceiptGroup.SortingSummary.Enabled = true;
                    grpReceiptGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpReceiptGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
            if (grpReceiptLedger.Visible)
            {
                if (this.ReportProperties.SortByLedger == 0)
                {
                    grpReceiptLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    else
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    grpReceiptLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpReceiptLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    else
                    {
                        grpReceiptLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    grpReceiptLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpReceiptLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            //if (grpcostCenterCategory.Visible)
            //{
            //    grpcostCenterCategory.SortingSummary.Enabled = true;
            //    grpcostCenterCategory.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            //    grpcostCenterCategory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpcostCenterCategory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (grpCostCentreNameReceipts.Visible)
            //{
            //    grpcostCenterCategory.SortingSummary.Enabled = true;
            //    grpcostCenterCategory.SortingSummary.FieldName = "COST_CENTRE_NAME";
            //    grpcostCenterCategory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpcostCenterCategory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
        }

        #endregion

        private void xrLedgerAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrLedgerAmt.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrLedgerAmt.Text = "";
            }
        }

        private void grpReceiptGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.Receipts.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName) != null
                    ? GetCurrentColumnValue(reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    e.Cancel = true;
                }
            }
        }

        private void xrParentgroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("GROUP_ID").ToString()) : 0;

            if (this.ReportProperties.ShowByLedger == 1 || this.ReportProperties.ShowByLedgerGroup == 1)
            {
                if (GroupId == 1 || GroupId == 2 || GroupId == 3 || GroupId == 4)
                {
                    xrParentgroupName.Padding = new PaddingInfo(3, 3, 3, 3);
                }
                else
                {
                    xrParentgroupName.Padding = new PaddingInfo(3, 3, 3, 3);
                }
            }
            else
            {
                xrParentgroupName.Padding = new PaddingInfo(3, 3, 3, 3);
            }
        }

        private void xrGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("PARENT_GROUP_ID").ToString()) : 0;
            // To be repalced by chinna reg parent group id
            int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("GROUP_ID").ToString()) : 0;
            string GroupName = this.GetCurrentColumnValue("LEDGER_GROUP") != null ? this.GetCurrentColumnValue("LEDGER_GROUP").ToString() : string.Empty;

            if (this.ReportProperties.ShowByLedger == 1 || this.ReportProperties.ShowByLedgerGroup == 1)
            {
                if (GroupId == 1 || GroupId == 2 || GroupId == 3 || GroupId == 4)
                {
                    xrGroupName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                }
                else
                {
                    xrGroupName.Padding = new PaddingInfo(7, 3, 3, 3);
                }
            }
            else
            {
                xrGroupName.Padding = new PaddingInfo(3, 3, 3, 3);
            }
        }

        private void xrLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("GROUP_ID").ToString()) : 0;

            if (this.ReportProperties.ShowByLedger == 1 || this.ReportProperties.ShowByLedgerGroup == 1)
            {
                if (GroupId == 1 || GroupId == 2 || GroupId == 3 || GroupId == 4)
                {
                    xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                }
                else
                {
                    xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(12, 3, 3, 3);
                }
            }
            else
            {
                xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
            }
        }
    }
}
