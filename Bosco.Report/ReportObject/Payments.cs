using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using System.Data;
using DevExpress.XtraPrinting;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class Payments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public Payments()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblReceiptGroup, xrPaymentGroupName,
                    new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrtblPaymentLedger, xrPaymentLedgerName,
                new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false, "", true);
        }

        #endregion

        #region Decelaration
        public double PaymentAmout { get; set; }
        private int Rowcount = 0;
        private int GroupRowCount = 0;
        SettingProperty settingProperty = new SettingProperty();

        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            Rowcount = 0;
            GroupRowCount = 0;
            BindPaymentSource(TransType.PY);
            base.ShowReport();


        }
        #endregion

        #region Properties
        public float CodeColumnWidth
        {
            set
            {
                xrPaymentLedgerCode.WidthF = value;
            }
            get
            {
                return xrPaymentGroupCode.WidthF;
            }
        }
        public float GroupCodeColumnWidth
        {
            set { xrPaymentGroupCode.WidthF = value; }

        }
        public float ParentGroupCodeColumnWidth
        {
            set { tcParentGroupCode.WidthF = value; }
        }
        public float NameColumnWidth
        {
            set
            {

                xrPaymentLedgerName.WidthF = value;
            }
            get
            {
                return xrPaymentGroupName.WidthF;
            }
        }
        public float GroupNameColumnWidth
        {
            set
            {
                xrPaymentGroupName.WidthF = value;

            }
        }

        public float ParentGroupColumWidth
        {
            set { xrParentGroupName.WidthF = value; }
        }

        public float CategoryNameWidth
        {
            set
            {
                xrCostCentreCategoryName.WidthF = value;
            }
        }

        public float CostCentreWidth { set { xrCelCostCentreName.WidthF = value; } }

        public float AmountColumnWidth
        {
            set
            {
                xrPayAmt.WidthF = value;
            }
            get
            {
                return xrgrpPayAmt.WidthF;
            }
        }
        public float GroupAmountColumnWidth
        {
            set { xrgrpPayAmt.WidthF = value; }

        }

        public float ParentGroupAmountColumnWidth
        {
            set { xrgrpParentAmount.WidthF = value; }
        }

        private bool hideccpayments = false;
        public bool HideCostCentrePayments
        {
            get { return hideccpayments; }
            set { hideccpayments = value; }

        }
        public float CostCategoryAmountWidth
        {
            get { return xrCellCCCAmount.WidthF; }
            set { xrCellCCCAmount.WidthF = value; }
        }

        public bool CostCentreNameVisible
        {
            set { xrtblCostcenterName.Visible = value; }
        }

        #endregion

        #region Methods
        private ResultArgs SetReportReceiptSource(TransType transType)
        {
            this.ReportProperties.ShowGroupCode = this.LoginUser.IS_CMFCHE_CONGREGATION ? 1 : 0;

            ResultArgs resultArgs = null;
            string sqlPayments = string.Empty;
            object sqlCommandId = string.Empty;
            if (transType == TransType.PY)
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Payments);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Payments;
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (objReportProperty.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CPY)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentrePayments);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentrePayments;
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (objReportProperty.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CEXP)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreExpenditure);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentreExpenditure;
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (objReportProperty.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Expenditure);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Expenditure;
                if (objReportProperty.Current.ShowGroupCode == 0)
                {
                    xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            using (DataManager dataManager = new DataManager(sqlCommandId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.settingProperty.BookBeginFrom);

                //   int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                //   int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                int LedgerPaddingRequired = (ReportProperties.ShowByLedger == 1) ? 1 : 0;     // (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0; //(ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlPayments);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {

            xrtblParentGroup = AlignGroupTable(xrtblParentGroup);
            xrtblReceiptGroup = AlignGroupTable(xrtblReceiptGroup);
            xrtblPaymentLedger = AlignContentTable(xrtblPaymentLedger);

            xrtblCostcenterName = AlignCostCentreTable(xrtblCostcenterName);
            tblCostCentreCategoryName = AlignCCCategoryTable(tblCostCentreCategoryName);
            if (ReportProperties.ReportId == "RPT-028" || ReportProperties.ReportId == "RPT-034")
                tblCostCentreCategoryName.Visible = false;
            xrCCBreakup = AlignContentTable(xrCCBreakup);
        }

        private XRTable AlignContentTable(XRTable table, int rCount)
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
                        if (rCount == 1)
                        {
                            if (count == 1)
                            {
                                if (ReportProperties.ReportId == "RPT-028")//Income and Expenditure
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom; ;
                                    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")// Cost Centre Receipts and Payments
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom; ;
                                    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }

                                else
                                {
                                    tcell.Borders = BorderSide.All;
                                    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                            }
                            else
                            {
                                if (ReportProperties.ReportId == "RPT-028")//Income and Expenditure
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                                else if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")// Cost Centre Receipts and Payments
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    if (count == 2)
                                    {
                                        tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                                    }
                                    else
                                    {
                                        tcell.Borders = BorderSide.All;
                                    }
                                }
                            }
                        }
                        else
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

        private XRTable AlignGroupTable(XRTable table, int rCount)
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
                        if (rCount == 1)
                        {
                            if (ReportProperties.ReportId == "RPT-028")
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Left | BorderSide.Right;
                                    if (count == 1 && ReportProperties.ShowGroupCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                                }
                            }
                            else if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Left | BorderSide.Right;
                                    if (count == 1 && ReportProperties.ShowGroupCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right;
                                    if (count == 1 && ReportProperties.ShowGroupCode != 1)
                                    {
                                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                    }
                                }
                                else
                                {
                                    if (count == 2)
                                    {
                                        if (ReportProperties.ShowByLedger != 1)
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top | BorderSide.Bottom;
                                        }
                                        else
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                        }
                                    }
                                    else
                                    {
                                        if (ReportProperties.ShowByLedger != 1)
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top | BorderSide.Bottom;
                                        }
                                        else
                                        {
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                        }
                                    }
                                }
                            }
                        }
                        else
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

        public void BindPaymentSource(TransType transType)
        {

            Rowcount = 0;
            GroupRowCount = 0;
            PaymentAmout = 0;
            ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtPayments = resultArgs.DataSource.Table;

            //On 13/08/2018, to show TDS on FD interest for accumulate interest
            /*if (transType == TransType.PY)
            {
                ResultArgs resultarg = GetJournalTDSonFDInterestAmount();
                if (resultarg != null && resultarg.Success)
                {
                    DataTable dtTDSonFDInterest = resultarg.DataSource.Table;
                    if (dtTDSonFDInterest != null && dtTDSonFDInterest.Rows.Count > 0)
                    {
                        //On 22/05/2019, to fix ledger grouping --------------------------------------------------------
                        //dtPayments.Merge(dtTDSonFDInterest);
                        foreach (DataRow drTDS in dtTDSonFDInterest.Rows)
                        {
                            int LedgerId = UtilityMember.NumberSet.ToInteger(drTDS["LEDGER_ID"].ToString());
                            decimal TDSAmount = UtilityMember.NumberSet.ToDecimal(drTDS["PAYMENTAMT"].ToString());
                            dtPayments.DefaultView.RowFilter = "LEDGER_ID=" + LedgerId;
                            if (dtPayments.DefaultView.Count > 0)
                            {
                                decimal PaymentAmt = UtilityMember.NumberSet.ToDecimal(dtPayments.DefaultView[0]["PAYMENTAMT"].ToString());
                                dtPayments.DefaultView[0].BeginEdit();
                                dtPayments.DefaultView[0]["PAYMENTAMT"] = (PaymentAmt + TDSAmount);
                                dtPayments.DefaultView[0].EndEdit();
                            }
                            else
                            {
                                DataRow dr = dtPayments.NewRow();
                                dr["LEDGER_ID"] = drTDS["LEDGER_ID"];
                                dr["GROUP_ID"] = drTDS["GROUP_ID"];
                                dr["GROUP_CODE"] = drTDS["GROUP_CODE"];
                                dr["SORT_ORDER"] = drTDS["SORT_ORDER"];
                                dr["PARENT_GROUP"] = drTDS["PARENT_GROUP"];
                                dr["LEDGER_GROUP"] = drTDS["LEDGER_GROUP"];
                                dr["LEDGER_NAME"] = drTDS["LEDGER_NAME"];
                                dr["LEDGER_CODE"] = drTDS["LEDGER_CODE"];
                                dr["PAYMENTAMT"] = drTDS["PAYMENTAMT"];
                                dtPayments.Rows.Add(dr);
                            }
                            dtPayments.DefaultView.RowFilter = string.Empty;
                        }
                        //-----------------------------------------------------------------------------------------------
                    }
                }
            }*/

            if (transType == TransType.EP || transType == TransType.PY || transType == TransType.CPY || transType == TransType.CEXP)
            {
                if (dtPayments != null && dtPayments.Rows.Count != 0)
                {
                    PaymentAmout = this.UtilityMember.NumberSet.ToDouble(dtPayments.Compute("SUM(PAYMENTAMT)", "").ToString());
                }
            }

            if (dtPayments != null)
            {
                dtPayments.TableName = "Payments";
                this.DataSource = dtPayments;
                this.DataMember = dtPayments.TableName;
            }


            SetReportSetting();
            SetReportBorder();
            SortByLedgerorGroup();
        }

        public void HidePaymentReportHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        private void SetReportSetting()
        {
            float actualCodeWidth = xrPaymentGroupCode.WidthF;
            bool isCapCodeVisible = true;
            bool isCapGroupVisible = false;
            //Include / Exclude Code
            if (xrPaymentGroupCode.Tag != null && xrPaymentGroupCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrPaymentGroupCode.Tag.ToString());
            }
            else
            {
                xrPaymentGroupCode.Tag = xrPaymentGroupCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrPaymentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrPaymentLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpPaymentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpPaymentLedger.Visible = ReportProperties.ShowByLedger == 1 || ReportProperties.ShowByCostCentre == 1;
            grpCostCentreNamePayments.Visible = (ReportProperties.ShowByCostCentre == 1);
            grpCCBreakup.Visible = (ReportProperties.BreakByCostCentre == 1);
            grpcostCentreCategory.Visible = ReportProperties.ShowByCostCentreCategory == 1;


            if (ReportProperties.ReportId == "RPT-041" || ReportProperties.ReportId == "RPT-049")
            {
                if (ReportProperties.ShowByCostCentreCategory == 1)
                    grpcostCentreCategory.Visible = true;
                else
                    grpcostCentreCategory.Visible = false;
            }

            if (grpCostCentreNamePayments.Visible == true)
            {
                this.CosCenterName = objReportProperty.CostCentreName;
                HideCostCentrePayments = true;
            }
            else
            {
                this.CosCenterName = string.Empty;
                HideCostCentrePayments = false;
            }
            if (grpPaymentGroup.Visible == false && grpPaymentLedger.Visible == false)
            {
                // This code add by Amal
                if (ReportProperties.ReportId != "RPT-041" && ReportProperties.ReportId != "RPT-049")
                    grpPaymentLedger.Visible = true;
            }

            //done by alwar on 18/12/2015 (for Temporary) for sorting issue
            grpcostCentreCategory.GroupFields[0].FieldName = "";
            grpCostCentreNamePayments.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
            grpPaymentGroup.GroupFields[0].FieldName = "";
            grpPaymentLedger.GroupFields[0].FieldName = "";

            if (grpcostCentreCategory.Visible)
            {
                grpcostCentreCategory.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
            }

            if (grpCostCentreNamePayments.Visible)
            {
                grpCostCentreNamePayments.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_NAMEColumn.ColumnName;
            }

            if (grpParentGroup.Visible)
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName;
                }
                else
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName;
                }
            }

            if (grpPaymentGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpPaymentGroup.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;
                }
            }
            if (grpPaymentLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpPaymentLedger.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpPaymentLedger.GroupFields[0].FieldName = reportSetting1.Payments.LEDGER_NAMEColumn.ColumnName;
                }
            }

            tcParentGroupCode.Visible = xrPaymentGroupCode.Visible = this.LoginUser.IS_CMFCHE_CONGREGATION ? true : false;

        }

        public ResultArgs GetExpenseReportSource(TransType transType)
        {
            ResultArgs resultArgs = null;
            string sqlPayments = string.Empty;
            object sqlCommandId = string.Empty;
            if (transType == TransType.PY)
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Payments);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Payments;
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                //if (objReportProperty.ShowGroupCode == 0)
                //{
                //    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                //}
            }
            else if (transType == TransType.CPY)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentrePayments);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentrePayments;
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (objReportProperty.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
            else if (transType == TransType.CEXP)
            {
                sqlPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreExpenditure);
                sqlCommandId = SQL.ReportSQLCommand.CostCentre.CostCentreExpenditure;
                xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                if (objReportProperty.ShowGroupCode == 0)
                {
                    xrPaymentGroupCode.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }

            else
            {
                sqlPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.Expenditure);
                sqlCommandId = SQL.ReportSQLCommand.FinalAccounts.Expenditure;
                if (objReportProperty.ShowGroupCode == 0)
                {
                    xrgrpPayAmt.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                }
            }
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
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlPayments);
            }
            return resultArgs;
        }

        public void BindExpenseSource(ResultArgs resultArgs, TransType transType)
        {
            PaymentAmout = 0;
            //ResultArgs resultArgs = SetReportReceiptSource(transType);
            DataTable dtPayments = resultArgs.DataSource.Table;

            if (transType == TransType.EP || transType == TransType.PY || transType == TransType.CPY || transType == TransType.CEXP)
            {
                if (dtPayments != null && dtPayments.Rows.Count != 0)
                {
                    PaymentAmout = this.UtilityMember.NumberSet.ToDouble(dtPayments.Compute("SUM(PAYMENTAMT)", "").ToString());
                }
            }

            if (dtPayments != null)
            {
                dtPayments.TableName = "Payments";
                this.DataSource = dtPayments;
                this.DataMember = dtPayments.TableName;
            }
            Rowcount = 0;
            GroupRowCount = 0;
            SetReportBorder();
            SetReportSetting();
            SortByLedgerorGroup();
        }

        public void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER"; // GROUP_CODE
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

            if (grpPaymentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpPaymentGroup.SortingSummary.Enabled = true;
                    grpPaymentGroup.SortingSummary.FieldName = "SORT_ORDER"; // GROUP_CODE
                    grpPaymentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpPaymentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpPaymentGroup.SortingSummary.Enabled = true;
                    grpPaymentGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpPaymentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpPaymentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
            if (grpPaymentLedger.Visible)
            {
                if (this.ReportProperties.SortByLedger == 0)
                {
                    grpPaymentLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpPaymentLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                        grpPaymentLedger.SortingSummary.FieldName = "LEDGER_CODE";  // LEDGER_CODE
                    }
                    else
                    {
                        grpPaymentLedger.SortingSummary.FieldName = "LEDGER_CODE";  // LEDGER_CODE
                    }
                    grpPaymentLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpPaymentLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpPaymentLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpPaymentLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_NAME
                        grpPaymentLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    else
                    {
                        grpPaymentLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    grpPaymentLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpPaymentLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            //if (grpcostCentreCategory.Visible)
            //{
            //    grpcostCentreCategory.SortingSummary.Enabled = true;
            //    grpcostCentreCategory.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            //    grpcostCentreCategory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpcostCentreCategory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (grpCostCentreNamePayments.Visible)
            //{
            //    grpCostCentreNamePayments.SortingSummary.Enabled = true;
            //    grpCostCentreNamePayments.SortingSummary.FieldName = "COST_CENTRE_NAME";
            //    grpCostCentreNamePayments.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpCostCentreNamePayments.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
        }

        #endregion

        #region Events

        private void xrPayAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double PaymentAmt = this.ReportProperties.NumberSet.ToDouble(xrPayAmt.Text);
            if (PaymentAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrPayAmt.Text = "";
            }
        }

        private void xrtblPaymentLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Rowcount++;
            AlignContentTable(xrtblPaymentLedger, Rowcount);
        }

        private void xrtblReceiptGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GroupRowCount++;
            AlignGroupTable(xrtblReceiptGroup, GroupRowCount);
        }

        #endregion

        private void grpParentGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void grpPaymentGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    e.Cancel = true;
                }
            }
        }
        /// <summary>
        /// On 13/08/2018, to show TDS on FD interest for accumulate interest
        /// We show FD renewal accumulated jounral entry interest amount in receipt side
        /// After adding TDS entry along with FD interest, for Accumulated interest TDS amount should be added with Payment side
        /// 
        /// this method will retrn entries which are made on TDS on FD intererest ledger while renewing accumulated intrest
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetJournalTDSonFDInterestAmount()
        {
            ResultArgs resultArgs = null;
            string sqlReceiptJournal = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FetchTDSOnFDInterest);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceiptJournal);
            }
            return resultArgs;
        }

        private void xrPaymentLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("GROUP_ID").ToString()) : 0;

            if (this.ReportProperties.ShowByLedger == 1 || this.ReportProperties.ShowByLedgerGroup == 1)
            {
                if (GroupId == 1 || GroupId == 2 || GroupId == 3 || GroupId == 4)
                {
                    xrPaymentLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                }
                else
                {
                    xrPaymentLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(12, 3, 3, 3);
                }
            }
            else
            {
                xrPaymentLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
            }
        }

        private void xrPaymentGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("PARENT_GROUP_ID").ToString()) : 0;
            // Chinna to be Replaced ( Parent Group details)
            int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("GROUP_ID").ToString()) : 0;
            string GroupName = this.GetCurrentColumnValue("LEDGER_GROUP") != null ? this.GetCurrentColumnValue("LEDGER_GROUP").ToString() : string.Empty;

            if (this.ReportProperties.ShowByLedger == 1 || this.ReportProperties.ShowByLedgerGroup == 1)
            {
                if (GroupId == 1 || GroupId == 2 || GroupId == 3 || GroupId == 4)
                {
                    xrPaymentGroupName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                }
                else
                {
                    xrPaymentGroupName.Padding = new PaddingInfo(7, 3, 3, 3);
                }
            }
            else
            {
                xrPaymentGroupName.Padding = new PaddingInfo(3, 3, 3, 3);
            }
        }

        private void xrParentGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int GroupId = this.GetCurrentColumnValue("GROUP_ID") != null ? this.UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("GROUP_ID").ToString()) : 0;

            if (this.ReportProperties.ShowByLedger == 1 || this.ReportProperties.ShowByLedgerGroup == 1)
            {
                if (GroupId == 1 || GroupId == 2 || GroupId == 3 || GroupId == 4)
                {
                    xrParentGroupName.Padding = new PaddingInfo(3, 3, 3, 3);
                }
                else
                {
                    xrParentGroupName.Padding = new PaddingInfo(3, 3, 3, 3);
                }
            }
            else
            {
                xrParentGroupName.Padding = new PaddingInfo(3, 3, 3, 3);
            }
        }
    }
}
