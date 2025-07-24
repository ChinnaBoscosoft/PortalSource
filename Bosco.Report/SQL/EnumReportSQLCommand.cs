/*  Class Name      : EnumActivityDataCommand.cs
 *  Purpose         : Enum Data type for Indetifying SQL Statement from UI request
 *  Author          : CS
 *  Created on      : 02-Aug-2010
 */

namespace Bosco.Report.SQL
{
    public class ReportSQLCommand
    {
        public enum Report
        {
            AccountYear,
            MonthlyAbstract,
            MultiAbstract,
            DrillDownReport,
            Ledger,
            FetchLedgerName,
            FetchGroupName,
            LedgerOpBalance,
            FetchACIBalance

        }

        public enum BankReport
        {
            ChequeCleared,
            ChequeUncleared,
            ChequeRealiszed,
            ChequeUnrealized,
            CashBankBook,
            CashJournal,
            CashFlow,
            FinancialPosition,
            BankFlow,
            BankJournal,
            BankReconcilationStatement,
            BankReconcilationStatementCleared,
            BankBalanceStatement,

            FixedDepositStatement,
            CashBankReceipts,
            CashBankPayments,
            ReceiptPaymentWithPrevious,
            BankCurrentClosingBalance,
            FetchFDRegisterDetails
        }

        public enum FinalAccounts
        {
            ReceiptsPayments,
            Receipts,
            Payments,
            ReceiptsCode,
            PaymentsCode,
            TrialBalaceCurrent,
            TrialBalanceList,
            TrialBalanceExcessDifference,
            Expenditure,
            Income,
            IncomeExpenditure,
            FinalIncomeExpenditure,
            IEReceitpsAmt,
            IEPaymentsAmt,
            IsFirstFinancialYear,
            BalanceSheet,
            BalanceSheetSAP,
            BalanceSchedules,
            BalanceScheduleIncome,
            BalanceScheduleExpence,
            BalanceCapital,
            BranchWiseIncomeExpense,
            BranchWiseLedgerComparative,
            BalanceSheetGroups,
            ReceiptJournal,
            FinalReceiptJournal,
            MonthWiseLedgerComparative,
            FetchTDSOnFDInterest,
            BalanceSheetExcessDifference,
            BalanceSheetOpeningAmt,
            BranchFixedAssetsInvestments,
            BranchIEYearWise,
            BranchLocationIEYearWise
        }

        public enum Generalate
        {
            BalanceSheet,
            BalanceSheetDetailByConLedger,
            BalanceSheetDetailByHOLedger,
            GeneralateProfitandLoss,
            GeneralateProfitandLossDetailByConLedger,
            GeneralateProfitandLossDetailByHOLedger,
            Profit,
            Loss,
            ProfitandLossbyHoseWise,
            ProfitandLossbyBranchHousewise,
            ProfitandLossbyHoseWiseInterAcc,
            ProfitandLossbyHoseWiseInterAccByBranch,
            ProfitandLossbyHoseWiseInterAccDetail,
            //GetLedgerInterAccountTransferId,
            //GetLedgerContributionFromProvince,
            //GetLegerContributionToProvince,
            ProfitandLossbyFoundationWise,
            GeneralateActivityIncomeExpense,
            GeneralateActivityIncomeExpenseFA,
            GeneralateCommercialIncomeExpense,
            GeneralatePatrimonial,
            GeneralateMapandUnmapLedger,
            GeneralateAbstract,
            GeneralateJournalLedgerVouchers,
            GeneralateActivityGSTLedgerIncomeExpense,
            GeneralateLedgerBalance,
            GeneralateLedgerBalanceByConLedger,
            GeneralateLedgerBalanceByLedgerGroup,
        }

        public enum Masters
        {
            FetchHeadOfficeLedgers,
            DetailsOfAllInstitution,
            BranchBankList,
            BranchExportVoucher,
            FetchBranchBalance,
            FetchBranchDatastatus,
            FetchGeneralateMapUnmapLedger,
            FetchBranchProjectLedger,
            FetchHeadOfficeProject
        }

        public enum CostCentre
        {
            CostCenterCashJournal,
            CostCenterBankJournal,
            CostCenterCashBankBook,
            CostCenterLedger,
            MonthlyAbstract,
            MultiAbstract,
            CostCentreReceipts,
            CostCentrePayments,
            CostCentreSummary,
            CostCentreIncome,
            CostCentreExpenditure
        }
        public enum CashBankVoucher
        {
            CashBankVoucherReceipts,
            CashBankVoucherPayments,
            CashBankVoucher,
            JournalVoucher,
            CashBankTransactions,
            JournalTransactions,
            FetchcashBankByVoucher,
            FetchJournalByVoucher
        }

        public enum ForeginContribution
        {
            FCCountry,
            FCPurpose,
            FCDonorInstitutional,
            FCDonorIndividual,
            ExecutiveMembers,
            FC6,
            FC6Purpose,
            FCBank,
            FCInstPreference,
            FCContribution,
            FC6Donor,
            FC6DonorAmount,
            FC6BankAccount,
            FC6BankInterestAmount,
            FC6DesignatedBankAmount,
            FC6FixedDeposit

        }

        public enum ReportCriteria
        {
            DF,
            DT,
            DA,
            AT,
            BL,
            BG,
            DB,
            IK,
            IJ,
            GT,
            AG,
            AC,
            MT,
            AD,
            CD,
            AB,
            PJ,
            BK,
            LG,
            GP,
            CC,
            NN
        }

        public enum FinacialTransType
        {
            RC,
            PY,
            JN
        }

        public enum FianacialMode
        {
            Add,
            Edit
        }

        public enum BudgetVariance
        {
            BudgetVarianceReport,
            BudgetDetails,
            BudgetInfo,
            PreviousBudgetInfo,
            BudgetExpenditure,
            BudgetLedgers,
            BudgetLedgersINM,
            BudgetStatistics,
            FetchBudgetNames,
            FetchMysoreBudget
        }

        public enum ReportProperty
        {

        }

        public enum UserRights
        {
            Reports = 196,
            Abstract = 197,
            BankActivities = 198,
            BookofAccounts = 199,
            FinalAccounts = 200,
            ForeginContribution = 201,
            CostCentre = 202,
            FinancialRecords = 203,
            Budget = 204
        }
    }
}