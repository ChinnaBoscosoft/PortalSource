using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class TrialBalance : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        string dtClosingDateRange = string.Empty;
        string dtClosingDateRangeYear = string.Empty;
        int GroupNumber = 0;
        double OPDebit = 0;
        double OPCredit = 0;
        double CurTransDebit = 0;
        double CurTransCredit = 0;
        double ClosingBalanceDebit = 0;
        double ClosingBalanceCredit = 0;
        double TotalCashClosingBalanceDebit = 0;
        double TotalCashClosingBalanceCredit = 0;
        double TotalBankClosingBalanceDebit = 0;
        double TotalBankClosingBalanceCredit = 0;
        double TotalFDClosingBalanceDebit = 0;
        double TotalFDClosingBalanceCredit = 0;
        double TotalOpeningCashAmount = 0;
        double TotalOpeningBankAmount = 0;
        double TotalOpeningFixedDeposit = 0;
        double AllTotalOpeningLedgerDebitBalance = 0;
        double AllTotalOpeningLedgerCreditBalance = 0;
        double CashDebitCurrentTotal = 0;
        double CashCreditCurrentTotal = 0;
        double BankAccountCreditTotal = 0;
        double BankAccountDebitTotal = 0;
        double FixedDepositCreditTotal = 0;
        double FixedDepositDebitTotal = 0;
        double SumCurTransDebit = 0;
        double SumCurTransCredit = 0;
        double SumofClDebit = 0;
        double SumofClCredit = 0;
        double SumClosingBalanceDebit = 0;
        double SumClosingBalanceCredit = 0;
        double IEDebit = 0;
        double IECredit = 0;
        double TotalOpeningCashBankFD = 0;
        double TotalOpeningCashBankFDCredit = 0;
        double OpCashDebit = 0;
        double OpCashCredit = 0;
        double OpBankDebit = 0;
        double OpBankCredit = 0;
        double OpFDDebit = 0;
        double OpFDCredit = 0;
        double IncomeAmt = 0;
        double ExpenceAmt = 0;
        int CurrentFinancialYear = 0;
        double IncomeExpenditureAmountPrevious = 0;
        string DateRangeDFReducing = string.Empty;
        string GroupCode = string.Empty;
        string PrevGroupCode = string.Empty;
        int GroupCodeNumber = 0;
        double GrpClosingDebit = 0.0;
        double GrpClosingCredit = 0.0;
        bool isCapLedgerCodeVisible;
        bool isCapGroupCodeVisible;

        double ExcessDebitAmount;
        double ExcessCreditAmount;
        #endregion

        #region Constructor
        public TrialBalance()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrTblGroup, xrtblClCurGrpDebit,
                    new ArrayList { reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrTblGroup, xrtblClCurGrpCredit,
                    new ArrayList { reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_RECEIPTS, false);

            this.AttachDrillDownToRecord(xrTblLedger, xrCurTransDebit,
                    new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrTblLedger, xrCurTransCredit,
                    new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY_RECEIPTS, false);
        }
        #endregion

        #region Property
        string yearfrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearfrom = settingProperty.YearFrom;
                return yearfrom;
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
        DataTable dtAssignTrialBalance = null;
        public DataTable dtTrialBalance
        {
            get
            {
                return dtAssignTrialBalance;
            }
            set
            {
                dtAssignTrialBalance = value;
            }
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            SumofClCredit = 0;
            SumofClDebit = 0;
            BindTrialBalance();
            xrtblClosingBalanceCredit.Text = xrtblClosingBalanceDebit.Text = string.Empty;
            OPDebit = 0;
            OPCredit = 0;
            CurTransDebit = 0;
            CurTransCredit = 0;
            ClosingBalanceDebit = 0;
            ClosingBalanceCredit = 0;
            xrClosingBalanceCredit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrClosingBalanceDebit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            ClosingBalance();

            ExcessDebitAmount = 0;
            ExcessCreditAmount = 0;
            GetTrialBanceExcessAmount();

            CurrentFinancialYear = 0;
            SortByLedgerorGroup();
            base.ShowReport();
        }
        #endregion

        #region Method
        private void BindTrialBalance()
        {
            try
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                xrOpDebit.Text = this.SetCurrencyFormat("Debit");
                xrOpCredit.Text = this.SetCurrencyFormat("Credit");
                xrCurrentDebit1.Text = this.SetCurrencyFormat("Debit");
                xrCurrentCredit.Text = this.SetCurrencyFormat("Credit");
                xrClosingDebit.Text = this.SetCurrencyFormat("Debit");
                xrClosingCredit.Text = this.SetCurrencyFormat("Credit");
                setHeaderTitleAlignment();
                this.SetLandscapeHeader = 950.25f;
                this.SetLandscapeFooter = 950.25f;
                xrtblHeaderCaption.WidthF = xrTrialBalance.WidthF = xrTable1.WidthF = xrTblGroup.WidthF = xrTblLedger.WidthF = xrtblbalance.WidthF = xrtblTotal.WidthF = 950.25f;
                SetReportTitle();

                if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
                {
                    ShowReportFilterDialog();
                }
                else
                {
                    string CloseDate = GetProgressiveDate(this.ReportProperties.DateFrom);
                    DateTime dtClosingDate = this.UtilityMember.DateSet.ToDate(CloseDate, false);
                    dtClosingDateRange = dtClosingDate.AddDays(-1).ToShortDateString();

                    DateTime dtYearClosing = Convert.ToDateTime(YearFrom);
                    dtClosingDateRangeYear = dtYearClosing.AddDays(-1).ToShortDateString();
                    string DFRange = this.ReportProperties.DateFrom;
                    DateTime DFDateRange = Convert.ToDateTime(DFRange);
                    DateRangeDFReducing = DFDateRange.AddDays(-1).ToShortDateString();

                    DataTable dtIncome = GetIncomeExcessAmount();
                    DataTable dtExpence = GetExpenceExcessAmt();
                    CurrentFinancialYear = IsFinancialYear();
                    ResultArgs resultArgs = GetTrialBalance();
                    DataView dtValue = resultArgs.DataSource.TableView;
                    DataTable dtOpValue = dtValue.ToTable();
                    dtTrialBalance = dtOpValue;
                    DataTable dtCurrentvalue = GetTrialBalanceCurrent();
                    SumCurTransDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CURRENTTRANS_DEBIT)", "").ToString());
                    SumCurTransCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CURRENTTRANS_CREDIT)", "").ToString());
                    CashDebitCurrentTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(CASH_IN_HAND_DEBIT)", "").ToString());
                    CashCreditCurrentTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(CASH_IN_HAND_CREDIT)", "").ToString());
                    BankAccountCreditTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(BANK_ACCOUNT_CREDIT)", "").ToString());
                    BankAccountDebitTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(BANK_ACCOUNT_DEBIT)", "").ToString());

                    SumofClDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CLOSING_DEBIT)", "").ToString());
                    SumofClCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CLOSING_CREDIT)", "").ToString());

                    if (dtValue != null)
                    {
                        dtValue.Table.TableName = "TrialBalance";
                        this.DataSource = dtValue;
                        this.DataMember = dtValue.Table.TableName;
                    }
                    TotalOpeningCashAmount = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                                        BalanceSystem.BalanceType.OpeningBalance);

                    TotalOpeningBankAmount = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                                        BalanceSystem.BalanceType.OpeningBalance);

                    TotalOpeningFixedDeposit = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance,
                                                        BalanceSystem.BalanceType.OpeningBalance);
                    IncomeAmt = this.UtilityMember.NumberSet.ToDouble(dtIncome.Rows[0]["RECEIPTAMT"].ToString());
                    ExpenceAmt = this.UtilityMember.NumberSet.ToDouble(dtExpence.Rows[0]["PAYMENTAMT"].ToString());
                    if (TotalOpeningCashAmount > 0)
                    {
                        OpCashDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningCashAmount.ToString()));
                        xrtblDrTotalCashAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpCashDebit);
                        xrtblCrTotalCashAmount.Text = string.Empty;
                    }
                    else
                    {
                        OpCashCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningCashAmount.ToString()));
                        xrtblCrTotalCashAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpCashCredit);
                        xrtblDrTotalCashAmount.Text = string.Empty;
                    }
                    if (TotalOpeningBankAmount > 0)
                    {
                        OpBankDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningBankAmount.ToString()));
                        xrtblDrBankAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpBankDebit);
                        xrtblCrBankAmount.Text = string.Empty;
                    }
                    else
                    {
                        OpBankCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningBankAmount.ToString()));
                        xrtblCrBankAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpBankCredit);
                        xrtblDrBankAmount.Text = string.Empty;
                    }

                    xrtblCurrentCashDebit.Text = this.UtilityMember.NumberSet.ToNumber(CashCreditCurrentTotal);
                    xrtblCurrentBankDebit.Text = this.UtilityMember.NumberSet.ToNumber(BankAccountCreditTotal);
                    xrtblCurrentCashCredit.Text = this.UtilityMember.NumberSet.ToNumber(CashDebitCurrentTotal);
                    xrtblCurrentBankCredit.Text = this.UtilityMember.NumberSet.ToNumber(BankAccountDebitTotal);

                    grpGroupHeader.Visible = (ReportProperties.ShowByLedgerGroup == 1);
                    GrpTrialBalanceLedger.Visible = ReportProperties.ShowByLedger == 1;
                    if (grpGroupHeader.Visible == false && GrpTrialBalanceLedger.Visible == false)
                    {
                        GrpTrialBalanceLedger.Visible = true;
                    }

                    if (grpGroupHeader.Visible)
                    {
                        if (ReportProperties.SortByGroup == 1)
                        {
                            grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                            grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.LEDGER_GROUPColumn.ColumnName;
                        }
                        else
                        {
                            grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                            grpGroupHeader.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_GROUPColumn.ColumnName;
                        }
                    }

                    if (grpGroupHeader.Visible)
                    {
                        xrtblbalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        xrtblCrTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        xrtblDrTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        allfooterDifferece.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        diifferOpDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        DifferOpCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        DifferCurDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        DifferCurCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        DifferClosingDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        DifferClosingCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        xrtblbalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        xrtblCrTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        xrtblDrTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }

                    if (GrpTrialBalanceLedger.Visible)
                    {
                        if (ReportProperties.SortByLedger == 1)
                        {
                            GrpTrialBalanceLedger.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                            GrpTrialBalanceLedger.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_NAMEColumn.ColumnName;
                        }
                        else
                        {
                            GrpTrialBalanceLedger.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                            GrpTrialBalanceLedger.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_NAMEColumn.ColumnName;
                        }
                        base.ShowReport();
                    }

                }
                xrTrialBalance = SetBorders(xrTrialBalance, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
                xrTblGroup = SetBorders(xrTblGroup, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
                xrTblLedger = SetBorders(xrTblLedger, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        public ResultArgs GetTrialBalance()
        {
            string TrialBalance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.TrialBalanceList);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.TrialBalanceList, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                if (CurrentFinancialYear > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, YearTo);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, "");
                }
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, DateRangeDFReducing);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, dtClosingDateRangeYear);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, TrialBalance);

                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtTrialBalance = resultArgs.DataSource.TableView.Table;

                    //modified by alwar on 23/01/2016, to avoid looping and calculating manually
                    //Calculate Closing balances by dynamically, if negative, assign to credit side else debit side of closing balance
                    string closingbalanceexpression = "((OP_DEBIT + CURRENTTRANS_DEBIT) - (OP_CREDIT + CURRENTTRANS_CREDIT))";

                    dtTrialBalance.Columns.Add(this.reportSetting1.TrialBalance.CLOSING_DEBITColumn.ColumnName,
                            this.reportSetting1.TrialBalance.CLOSING_DEBITColumn.DataType,
                            "IIF(" + closingbalanceexpression + " < 0, 0," + closingbalanceexpression + ")");

                    dtTrialBalance.Columns.Add(this.reportSetting1.TrialBalance.CLOSING_CREDITColumn.ColumnName,
                        this.reportSetting1.TrialBalance.CLOSING_CREDITColumn.DataType,
                        "IIF(" + closingbalanceexpression + " > 0, 0, - (" + closingbalanceexpression + "))");

                    string filter = "(CLOSING_CREDIT >0 OR CLOSING_DEBIT >0)";

                    filter += " OR (OP_CREDIT>0 OR OP_DEBIT>0)";

                    filter += " OR (CURRENTTRANS_CREDIT>0 OR CURRENTTRANS_DEBIT>0)";

                    dtTrialBalance.DefaultView.RowFilter = filter;
                    resultArgs.DataSource.Data = dtTrialBalance.DefaultView;
                }
            }
            return resultArgs;
        }

        private void GetTrialBanceExcessAmount()
        {
            string trialBalance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.TrialBalanceExcessDifference);
            using (DataManager dataManager = new DataManager())
            {
                if (CurrentFinancialYear == 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, trialBalance);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResource = resultArgs.DataSource.Table;
                        ExcessDebitAmount = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["DEBIT"].ToString());
                        ExcessCreditAmount = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["CREDIT"].ToString());
                    }
                }
            }
        }

        public int IsFinancialYear()
        {
            string FinancialYear = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IsFirstFinancialYear);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.IsFirstFinancialYear, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, YearTo);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.Scalar, FinancialYear);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public DataTable GetTrialBalanceCurrent()
        {
            string TrialBalanceCurrent = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.TrialBalaceCurrent);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.TrialBalaceCurrent, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (!(string.IsNullOrEmpty(this.ReportProperties.BranchOffice)) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!(string.IsNullOrEmpty(this.ReportProperties.Society)) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TrialBalanceCurrent);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable GetIncomeExcessAmount()
        {
            string IncomeAmt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IEReceitpsAmt);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.IEReceitpsAmt, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (!(string.IsNullOrEmpty(this.ReportProperties.BranchOffice)) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!(string.IsNullOrEmpty(this.ReportProperties.Society)) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                //dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, dtClosingDateRangeYear); 
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, IncomeAmt);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable GetExpenceExcessAmt()
        {
            string ExpenceAmt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IEPaymentsAmt);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.IEPaymentsAmt, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (!(string.IsNullOrEmpty(this.ReportProperties.BranchOffice)) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!(string.IsNullOrEmpty(this.ReportProperties.Society)) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                //dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, dtClosingDateRangeYear); 
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, ExpenceAmt);
            }
            return resultArgs.DataSource.Table;
        }

        private void xrGroupClosingBal_SummaryRowChanged(object sender, EventArgs e)
        {
            // find out Group By LedgerGroup
            // GroupNumber++;
            OPDebit = (GetCurrentColumnValue(reportSetting1.TrialBalance.OP_DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.OP_DEBITColumn.ColumnName).ToString());
            OPCredit = (GetCurrentColumnValue(reportSetting1.TrialBalance.OP_CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.OP_CREDITColumn.ColumnName).ToString());
            CurTransDebit = (GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_DEBITColumn.ColumnName).ToString());
            CurTransCredit = (GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_CREDITColumn.ColumnName).ToString());
            GroupCode = (GetCurrentColumnValue(reportSetting1.TrialBalance.GROUP_CODEColumn.ColumnName) == null) ? string.Empty : GetCurrentColumnValue(reportSetting1.TrialBalance.GROUP_CODEColumn.ColumnName).ToString();
            grpGroupHeader.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            if (grpGroupHeader.Visible == true)
            {
                if ((OPDebit < CurTransCredit) || (OPCredit < CurTransCredit) || (CurTransDebit < CurTransCredit || OPCredit > CurTransCredit))
                {
                    if (!GroupCode.Equals(PrevGroupCode))
                    {
                        xrGroupClosingBalCredit.Text = GrpClosingCredit.ToString();
                        GrpClosingCredit = GrpClosingDebit = 0;
                    }
                    double DebitAddAmount = OPDebit + CurTransDebit;
                    if (DebitAddAmount < (CurTransCredit + OPCredit))
                    {
                        GrpClosingCredit += (CurTransCredit + OPCredit) - (OPDebit + CurTransDebit);
                        xrGroupClosingBalCredit.Text = this.UtilityMember.NumberSet.ToNumber(GrpClosingCredit);
                    }
                    else
                    {
                        GrpClosingDebit += DebitAddAmount - (CurTransCredit + OPCredit);
                        xrGroupClosingBalDebit.Text = this.UtilityMember.NumberSet.ToNumber(GrpClosingDebit);

                    }
                    PrevGroupCode = GroupCode;
                }

                else if (OPDebit != 0 || CurTransDebit != 0)
                {
                    if (!GroupCode.Equals(PrevGroupCode))
                    {
                        GrpClosingCredit = GrpClosingDebit = 0;
                    }
                    GrpClosingDebit += (OPDebit + CurTransDebit) - CurTransCredit;
                    xrGroupClosingBalDebit.Text = this.UtilityMember.NumberSet.ToNumber(GrpClosingDebit);
                    // xrGroupClosingBalCredit.Text = string.Empty;
                }

                else if (OPCredit != 0 || CurTransCredit != 0)
                {
                    if (!GroupCode.Equals(PrevGroupCode))
                    {
                        GrpClosingCredit = GrpClosingDebit = 0;
                    }
                    GrpClosingCredit = (OPCredit + CurTransCredit) - CurTransDebit;
                    xrGroupClosingBalCredit.Text = this.UtilityMember.NumberSet.ToNumber(ClosingBalanceCredit);
                }
                OPDebit = OPCredit = CurTransDebit = CurTransCredit = 0;
            }
        }

        private void xrGroupClosingBalCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GrpClosingCredit;
            e.Handled = true;
        }

        public void ClosingBalance()
        {
            //  Cash Closing Balance 
            if (OpCashDebit < CashDebitCurrentTotal || CashCreditCurrentTotal < CashDebitCurrentTotal || OpCashCredit < CashCreditCurrentTotal)
            {
                double DebitAddTotal = OpCashDebit + CashCreditCurrentTotal;
                if (DebitAddTotal < (CashDebitCurrentTotal + OpCashCredit))
                {
                    TotalCashClosingBalanceCredit = (CashDebitCurrentTotal + OpCashCredit) - DebitAddTotal;
                    xrClosingCashTotalCredit.Text = this.UtilityMember.NumberSet.ToNumber(TotalCashClosingBalanceCredit);
                    xrClosingCashTotalDebit.Text = string.Empty;
                }
                else
                {
                    TotalCashClosingBalanceDebit = DebitAddTotal - (CashDebitCurrentTotal + OpCashCredit);
                    xrClosingCashTotalDebit.Text = this.UtilityMember.NumberSet.ToNumber(TotalCashClosingBalanceDebit);
                    xrClosingCashTotalCredit.Text = string.Empty;
                }
            }
            else if (OpCashDebit != 0 || CashCreditCurrentTotal != 0)
            {

                TotalCashClosingBalanceDebit = (TotalOpeningCashAmount + CashCreditCurrentTotal) - CashDebitCurrentTotal;
                xrClosingCashTotalDebit.Text = this.UtilityMember.NumberSet.ToNumber(TotalCashClosingBalanceDebit);
                xrClosingCashTotalCredit.Text = string.Empty;
            }

            //  Bank Closing Balance
            if (OpBankDebit < BankAccountDebitTotal || BankAccountCreditTotal < BankAccountDebitTotal || OpBankCredit < BankAccountCreditTotal)
            {
                double BankDebitAddTotal = OpBankDebit + BankAccountCreditTotal;
                if (BankDebitAddTotal < (BankAccountDebitTotal + OpBankCredit))
                {
                    TotalBankClosingBalanceCredit = (BankAccountDebitTotal + OpBankCredit) - BankDebitAddTotal;
                    xrclosingBankCreditTotal.Text = this.UtilityMember.NumberSet.ToNumber(TotalBankClosingBalanceCredit);
                    xrClosingBankDebitTotal.Text = string.Empty;
                }
                else
                {
                    TotalBankClosingBalanceDebit = BankDebitAddTotal - (BankAccountDebitTotal + OpBankCredit);
                    xrClosingBankDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(TotalBankClosingBalanceDebit);
                    xrclosingBankCreditTotal.Text = string.Empty;
                }
            }
            else if (OpBankDebit != 0 || BankAccountCreditTotal != 0)
            {

                TotalBankClosingBalanceDebit = (OpBankDebit + BankAccountCreditTotal) - BankAccountDebitTotal;
                xrClosingBankDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(TotalBankClosingBalanceDebit);
                xrclosingBankCreditTotal.Text = string.Empty;
            }

            //Fixed Deposit Closing banlance
            double ACIInterestAmount = FetchACIBalance();
            if (OpFDDebit < FixedDepositDebitTotal || FixedDepositCreditTotal < FixedDepositDebitTotal || OpFDCredit < FixedDepositCreditTotal)
            {
                double FDDebitAddTotal = OpFDDebit + FixedDepositCreditTotal;
                if (FDDebitAddTotal < (FixedDepositDebitTotal + OpFDCredit))
                {
                    TotalFDClosingBalanceCredit = (FixedDepositDebitTotal + OpFDCredit) - FDDebitAddTotal;
                }
                else
                {
                    TotalFDClosingBalanceDebit = FDDebitAddTotal - (FixedDepositDebitTotal + OpFDCredit);
                }
            }
            else if (OpFDDebit == 0 && FixedDepositDebitTotal == 0 && FixedDepositCreditTotal == 0 && OpFDCredit == 0)
            {
            }
            else if (OpFDDebit != 0 || FixedDepositCreditTotal != 0)
            {
                TotalFDClosingBalanceDebit = (OpFDDebit + FixedDepositCreditTotal) - (FixedDepositDebitTotal + OpFDCredit);
            }
        }

        private double FetchACIBalance()
        {
            double ACIInsAmount = 0;
            string FetchACIBalance = this.GetReportSQL(SQL.ReportSQLCommand.Report.FetchACIBalance);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FetchACIBalance);
            }
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ACIInsAmount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Compute("SUM(INTEREST_AMOUNT)", "").ToString());
            }
            return ACIInsAmount;
        }
        #endregion

        #region Events

        private void xrtblTotalOpeningBalanceDebitamt_SummaryReset(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerDebitBalance = 0;
        }

        private void xrtblTotalOpeningBalanceDebitamt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // Opening Balance of Debit
            TotalOpeningCashBankFD = OpCashDebit + OpBankDebit + OpFDDebit + AllTotalOpeningLedgerDebitBalance + IEDebit;
            TotalOpeningCashBankFDCredit = OpCashCredit + OpBankCredit + OpFDCredit + AllTotalOpeningLedgerCreditBalance + IECredit;
            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                if (TotalOpeningCashBankFD < TotalOpeningCashBankFDCredit)
                {
                    double Debitamt = TotalOpeningCashBankFDCredit - TotalOpeningCashBankFD;
                    diifferOpDebit.Text = this.UtilityMember.NumberSet.ToNumber(Debitamt);
                    e.Result = this.UtilityMember.NumberSet.ToNumber(Debitamt + TotalOpeningCashBankFD);
                    e.Handled = true;
                }
                else
                {
                    e.Result = TotalOpeningCashBankFD;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = TotalOpeningCashBankFD;
                e.Handled = true;
            }
        }

        private void xrtblTotalOpeningBalanceDebitamt_SummaryRowChanged(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerDebitBalance += (GetCurrentColumnValue("OP_DEBIT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("OP_DEBIT").ToString());

        }

        private void xrtblTotalOpeningBalanceCreditamount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // Opening Balance of Credit
            TotalOpeningCashBankFDCredit = OpCashCredit + OpBankCredit + OpFDCredit + AllTotalOpeningLedgerCreditBalance + IECredit;
            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                if (TotalOpeningCashBankFD > TotalOpeningCashBankFDCredit)
                {
                    double Creditamt = TotalOpeningCashBankFD - TotalOpeningCashBankFDCredit;
                    DifferOpCredit.Text = this.UtilityMember.NumberSet.ToNumber(Creditamt);
                    e.Result = this.UtilityMember.NumberSet.ToNumber(Creditamt + TotalOpeningCashBankFDCredit);
                    e.Handled = true;
                }
                else
                {
                    e.Result = TotalOpeningCashBankFDCredit;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = TotalOpeningCashBankFDCredit;
                e.Handled = true;
            }
        }

        private void xrtblTotalOpeningBalanceCreditamount_SummaryReset(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerCreditBalance = 0;
        }

        private void xrtblTotalOpeningBalanceCreditamount_SummaryRowChanged(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerCreditBalance += (GetCurrentColumnValue("OP_CREDIT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("OP_CREDIT").ToString());
        }

        private void xrtblDebitCurrent_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalCredit = CashCreditCurrentTotal + BankAccountCreditTotal + FixedDepositCreditTotal;
            e.Result = TotalCredit + SumCurTransDebit;
            e.Handled = true;
        }

        private void xrCreditCurrent_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalDebit = CashDebitCurrentTotal + BankAccountDebitTotal + FixedDepositDebitTotal;
            e.Result = TotalDebit + SumCurTransCredit;
            e.Handled = true;
        }

        private void xrtblClosingBalanceDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            //Debit Closing Balance
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            if (SumClosingBalanceDebit != SumClosingBalanceCredit)
            {
                if (SumClosingBalanceDebit < SumClosingBalanceCredit)
                {
                    double DebitClosingBalance = SumClosingBalanceCredit - SumClosingBalanceDebit;
                    e.Result = SumClosingBalanceDebit + DebitClosingBalance;
                    DebitClosingBalance = 0;
                    // SumofClDebit = 0;
                    e.Handled = true;

                }
                else
                {
                    e.Result = SumClosingBalanceDebit;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = SumClosingBalanceDebit;
                e.Handled = true;
            }

            ////Debit Closing Balance
            //double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit;
            //SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            //double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            //SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            //if (SumClosingBalanceDebit != SumClosingBalanceCredit)
            //{
            //    if (SumClosingBalanceDebit < SumClosingBalanceCredit)
            //    {
            //        double DebitClosingBalance = SumClosingBalanceCredit - SumClosingBalanceDebit;
            //        e.Result = SumClosingBalanceDebit + DebitClosingBalance;
            //        DebitClosingBalance = 0;
            //        // SumofClDebit = 0;
            //        e.Handled = true;

            //    }
            //    else
            //    {
            //        e.Result = SumClosingBalanceDebit;
            //        e.Handled = true;
            //    }
            //}
            //else
            //{
            //    e.Result = SumClosingBalanceDebit;
            //    e.Handled = true;
            //}

        }

        private void xrtblClosingBalanceCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //Credit Closing Balance
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            if (SumClosingBalanceCredit != SumClosingBalanceDebit)
            {
                if (SumClosingBalanceCredit < SumClosingBalanceDebit)
                {
                    double CreditClosingBalace = SumClosingBalanceDebit - SumClosingBalanceCredit;
                    e.Result = SumClosingBalanceCredit + CreditClosingBalace;
                    CreditClosingBalace = 0;
                    //SumofClCredit = 0;
                    e.Handled = true;

                }
                else
                {
                    e.Result = SumClosingBalanceCredit;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = SumClosingBalanceCredit;
                e.Handled = true;
            }

            ////Credit Closing Balance
            //double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            //SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            //if (SumClosingBalanceCredit != SumClosingBalanceDebit)
            //{
            //    if (SumClosingBalanceCredit < SumClosingBalanceDebit)
            //    {
            //        double CreditClosingBalace = SumClosingBalanceDebit - SumClosingBalanceCredit;
            //        e.Result = SumClosingBalanceCredit + CreditClosingBalace;
            //        CreditClosingBalace = 0;
            //        //SumofClCredit = 0;
            //        e.Handled = true;

            //    }
            //    else
            //    {
            //        e.Result = SumClosingBalanceCredit;
            //        e.Handled = true;
            //    }
            //}
            //else
            //{
            //    e.Result = SumClosingBalanceCredit;
            //    e.Handled = true;
            //}

        }

        private void xrOp_Debit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrOp_Debit.Text);
            if (OpeningDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOp_Debit.Text = "";
            }
        }

        private void xrOp_Credit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrOp_Credit.Text);
            if (OpeningCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOp_Credit.Text = "";
            }
        }

        private void xrCurTransDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CurTransDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrCurTransDebit.Text);
            if (CurTransDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCurTransDebit.Text = "";
            }
        }

        private void xrCurTransCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CurTransCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrCurTransCredit.Text);
            if (CurTransCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCurTransCredit.Text = "";
            }

        }
        private void diifferOpDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // To Include ExcessDebitAmount
            OPDebit = (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit + ExcessDebitAmount) - (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit + ExcessCreditAmount);
            if (OPDebit < 0)
            {
                double OPDebitValue = Math.Abs(this.UtilityMember.NumberSet.ToDouble(OPDebit.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(OPDebitValue);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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

            //OPDebit = (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit) - (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit);
            //if (OPDebit < 0)
            //{
            //    double OPDebitValue = Math.Abs(this.UtilityMember.NumberSet.ToDouble(OPDebit.ToString()));
            //    e.Result = this.UtilityMember.NumberSet.ToNumber(OPDebitValue);
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void DifferOpCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            OPCredit = (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit + ExcessCreditAmount) - (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit + ExcessDebitAmount);

            double dd1 = (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit);
            double dd = (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit);
            if (OPCredit < 0)
            {
                double OPCreditValue = Math.Abs(this.UtilityMember.NumberSet.ToDouble(OPDebit.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(OPCreditValue);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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
            //OPCredit = (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit) - (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit);
            //if (OPCredit < 0)
            //{
            //    double OPCreditValue = Math.Abs(this.UtilityMember.NumberSet.ToDouble(OPDebit.ToString()));
            //    e.Result = this.UtilityMember.NumberSet.ToNumber(OPCreditValue);
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void DifferClosingDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // to check
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit + ExcessCreditAmount;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + ExcessDebitAmount;  // + IEDebit;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double DebitClosingdiff = SumClosingBalanceDebit - SumClosingBalanceCredit;
            if (DebitClosingdiff < 0)
            {
                double CloseDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(DebitClosingdiff.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(CloseDebit);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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

            //// to check
            //double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            //SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            //double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit;  // + IEDebit;
            //SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            //double DebitClosingdiff = SumClosingBalanceDebit - SumClosingBalanceCredit;
            //if (DebitClosingdiff < 0)
            //{
            //    double CloseDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(DebitClosingdiff.ToString()));
            //    e.Result = this.UtilityMember.NumberSet.ToNumber(CloseDebit);
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void allfooterDifferece_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + IEDebit;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double DebitClosingdiff = SumClosingBalanceDebit - SumClosingBalanceCredit;
            double CreditClosingdiff = SumClosingBalanceCredit - SumClosingBalanceDebit;
            TotalOpeningCashBankFDCredit = OpCashCredit + OpBankCredit + OpFDCredit + AllTotalOpeningLedgerCreditBalance + IECredit;
            TotalOpeningCashBankFD = OpCashDebit + OpBankDebit + OpFDDebit + AllTotalOpeningLedgerDebitBalance + IEDebit;
            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                // Opening Balance of Debit
                if (TotalOpeningCashBankFD < TotalOpeningCashBankFDCredit)
                {
                    e.Result = "Diff. in Opening Balances";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;

            }

            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                // Opening Balance of Credit
                if (TotalOpeningCashBankFD > TotalOpeningCashBankFDCredit)
                {
                    e.Result = "Diff. in Opening Balances";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;

            }

            DebitClosingdiff = Math.Round(DebitClosingdiff);
            if (DebitClosingdiff < 0)
            {
                //Closing Balance Debit;
                if (DebitClosingdiff != -0.00000000093132257461547852)
                {
                    e.Result = "Diff. in Opening Balances";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            CreditClosingdiff = Math.Round(CreditClosingdiff);  // Round of the values (-0) or something started

            if (CreditClosingdiff < 0)
            {
                //Closing Balance Credit;
                e.Result = "Diff. in Opening Balances";
                e.Handled = true;
            }
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}

            //double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            //SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            //double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + IEDebit;
            //SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            //double DebitClosingdiff = SumClosingBalanceDebit - SumClosingBalanceCredit;
            //double CreditClosingdiff = SumClosingBalanceCredit - SumClosingBalanceDebit;
            //TotalOpeningCashBankFDCredit = OpCashCredit + OpBankCredit + OpFDCredit + AllTotalOpeningLedgerCreditBalance + IECredit;
            //TotalOpeningCashBankFD = OpCashDebit + OpBankDebit + OpFDDebit + AllTotalOpeningLedgerDebitBalance + IEDebit;
            //if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            //{
            //    // Opening Balance of Debit
            //    if (TotalOpeningCashBankFD < TotalOpeningCashBankFDCredit)
            //    {
            //        e.Result = "Difference in Balance";
            //        e.Handled = true;
            //    }
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;

            //}

            //if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            //{
            //    // Opening Balance of Credit
            //    if (TotalOpeningCashBankFD > TotalOpeningCashBankFDCredit)
            //    {
            //        e.Result = "Difference in Balance";
            //        e.Handled = true;
            //    }
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;

            //}

            //if (DebitClosingdiff < 0)
            //{
            //    //Closing Balance Debit;
            //    e.Result = "Difference in Balance";
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}

            //if (CreditClosingdiff < 0)
            //{
            //    //Closing Balance Credit;
            //    e.Result = "Difference in Balance";
            //    e.Handled = true;
            //}
            ////else
            ////{
            ////    e.Result = "";
            ////    e.Handled = true;
            ////}
        }


        private void DifferClosingCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // to check
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit + ExcessCreditAmount;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + ExcessDebitAmount; //+ IEDebit;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double CreditClosingdiff = SumClosingBalanceCredit - SumClosingBalanceDebit;
            if (CreditClosingdiff < 0)
            {
                double CloseCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(CreditClosingdiff.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(CloseCredit);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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

            //// to check
            //double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            //SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            //double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit; //+ IEDebit;
            //SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            //double CreditClosingdiff = SumClosingBalanceCredit - SumClosingBalanceDebit;
            //if (CreditClosingdiff < 0)
            //{
            //    double CloseCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(CreditClosingdiff.ToString()));
            //    e.Result = this.UtilityMember.NumberSet.ToNumber(CloseCredit);
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}

        }
        private void xrGroupClosingBal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double xrGroupHeaderAmount = this.ReportProperties.NumberSet.ToDouble(xrGroupClosingBal.Text);
            if (xrGroupHeaderAmount != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrGroupClosingBal.Text = "";
            }
        }

        private void xrtblTrialBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double xrTrialClosing = this.ReportProperties.NumberSet.ToDouble(xrtblTrialBalance.Text);
            if (xrTrialClosing != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrtblTrialBalance.Text = "";
            }
        }

        private void xrlblCostCenter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void SortByLedgerorGroup()
        {
            if (grpGroupHeader.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpGroupHeader.SortingSummary.Enabled = true;
                    grpGroupHeader.SortingSummary.FieldName = "LEDGER_GROUP";
                    grpGroupHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpGroupHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpGroupHeader.SortingSummary.Enabled = true;
                    grpGroupHeader.SortingSummary.FieldName = "LEDGER_GROUP";
                    grpGroupHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpGroupHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
            if (GrpTrialBalanceLedger.Visible)
            {
                if (this.ReportProperties.SortByLedger == 0)
                {
                    GrpTrialBalanceLedger.SortingSummary.Enabled = true;
                    GrpTrialBalanceLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    GrpTrialBalanceLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    GrpTrialBalanceLedger.SortingSummary.Enabled = true;
                    GrpTrialBalanceLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    GrpTrialBalanceLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
        }

        private void xrClosingBalanceDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ClosingCurDebit = this.ReportProperties.NumberSet.ToDouble(xrClosingBalanceDebit.Text);
            if (ClosingCurDebit != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrClosingBalanceDebit.Text = "";
            }
        }

        private void xrClosingBalanceCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CurClosingCredit = this.ReportProperties.NumberSet.ToDouble(xrClosingBalanceCredit.Text);
            if (CurClosingCredit != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrClosingBalanceCredit.Text = "";
            }
        }

        private void xrSummaryGetResult_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > ExcessCreditAmount)
            {
                e.Result = "Excess of Expenditure over Income";
                e.Handled = true;
            }
            else if (ExcessCreditAmount > ExcessDebitAmount)
            {
                e.Result = "Excess of Income Over Expenditure";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrExcessDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessDebitAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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

        private void xrExcessCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessCreditAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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

        private void xrExcessClosingDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessDebitAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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

        private void xrexcessClosingCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessCreditAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
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
