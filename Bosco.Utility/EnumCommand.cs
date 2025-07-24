/*  Class Name      : EnumCommand.cs
 *  Purpose         : Enum Data type to avoid using scalars/magic numbers in application
 *  Author          : CS
 *  Created on      : 14-Jul-2010
 */

using System.ComponentModel;
namespace Bosco.Utility
{

    public enum AppSettingName
    {
        DatabaseProvider = 1,
        AppConnectionString = 2,
        SQLAdapter = 3
    }

    public enum SelectionType
    {
        All = 0,
        Selected = 1,
        Deselected = 2
    }

    public enum Source
    {
        By,
        To
    }

    public enum PrintType
    {
        DT,
        DS
    }

    public enum CashSource
    {
        By,
        To
    }

    public enum CashFlag
    {
        Cash,
        Bank
    }

    public enum Sorting
    {
        Ascending,
        Descending,
        None
    }

    public enum TimeMode
    {
        AM = 1,
        PM = 2
    }

    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public enum BorderStyleCell
    {
        Regular = 0,
        Bold = 1
    }

    public enum YesNo
    {
        No = 0,
        Yes = 1
    }

    public enum DonorType
    {
        Institutional = 1,
        Individual = 2
    }

    public enum Types
    {
        Donor = 0,
        Auditor = 1,
        Other = 2
    }

    public enum ImportType
    {
        SplitProject,
        SubBranch,
        HeadOffice,
        SplitHOBranchProject
    }

    public enum DonorCategory
    {
        Above = 1,
        Below = 2
    }

    public enum DateDataType
    {
        Date,
        DateTime,
        Time,
        TimeStamp,
        DateNoFormatBegin,
        DateNoFormatEnd
    }

    public enum UserType
    {
        Portal = 0,
        HeadOffice = 1,
        BranchOffice = 2
    }

    public enum BudgetType
    {
        [Description("Budget Year")]
        BudgetYear = 1,
        [Description("Budget Period")]
        BudgetPeriod = 2,
        [Description("Financial Year")]
        BudgetByAnnualYear = 3,
        [Description("Calendar Year")]
        BudgetByCalendarYear = 4,
        [Description("Month")]
        BudgetMonth = 5,
        [Description("Academic Year")]
        BudgetAcademic = 6
        //BudgetYear = 1,
        //BudgetPeriod = 2,
        //BudgetByAnnualYear = 3,
        //BudgetByCalendarYear = 4,
        //BudgetMonth = 5
    }

    public enum BudgetAction
    {
        [Description("Created")]
        Created = 0,
        [Description("Recommended")]
        Recommended = 1,
        [Description("Approved")]
        Approved = 2
    }

    public enum LCBranchModuleStatus
    {
        Disabled = 0,
        Requested = 1,
        Approved = 2
    }

    public enum LedgerType
    {
        General = 1,
        InKind = 2
    }

    public enum ledgerSubType
    {
        CA,
        BK,
        FD,
        IK,
        GN
    }

    public enum FixedLedgerGroup
    {
        BankAccounts = 12,
        Cash = 13,
        FixedDeposit = 14
    }

    public enum FixedDepositStatus
    {
        Deposited = 1,
        Realized = 2
    }

    public enum LedgerSortOrder
    {
        Cash = 1,
        Bank = 2,
        FD = 3,
        IK = 4,
        GN = 255
    }

    public enum Status
    {
        Inactive = 0,
        Active = 1
    }

    public enum UserCreation
    {
        UserNotCreated = 0,
        UserCreatedNotCommunicated = 1,
        UserCreatedCommunicated = 2
    }

    public enum ResetPassword
    {
        AutomaticPassword = 0,
        ResetPassword = 1

    }

    public enum ViewDetails
    {
        Donor = 0,
        Auditor = 1,
    }

    public enum IdentityKey
    {
        Donor = 0,
        Auditor = 1,
    }

    public enum AddNewRow
    {
        NewRow = 0
    }
    public enum FormMode
    {
        Add = 0,
        Edit = 1
    }
    public enum AccessFlag
    {
        Accessable = 0,
        Editable = 1,
        Readonly = 2
    }
    public enum BankAccoutType
    {
        SavingAccount = 1,
        FixedDeposit = 2,
        MutualFund = 3,
        RecurringDeposit = 4,
        Equity = 5
    }

    public enum Setting
    {
        //Global Setting for Admin
        InterAccountFromLedger,
        InterAccountToLedger,
        ProvinceContributionFromLedger,
        ProvinceContributionToLedger,


        //Currency
        Country,
        Currency,
        CurrencyPosition,
        CurrencyPositivePattern,
        CurrencyNegativePattern,
        CurrencyNegativeSign,
        CurrencyCode,
        CurrencyCodePosition,

        //Number format and Currency
        DigitGrouping,
        GroupingSeparator,
        DecimalPlaces,
        DecimalSeparator,

        //Transaction
        HighNaturedAmt,
        TransEntryMethod,

        //UI Setting for Admin
        UILanguage,
        UIDateFormat,
        UIDateSeparator,
        UIThemes,
        UIProjSelection,
        UITransClose,
        UIPrintVoucher,
        UIForeignBankAccount,
        UITransMode,
        PrintVoucher
    }

    public enum UserSetting
    {
        //UI Setting for other users
        UILanguage,
        UIDateFormat,
        UIDateSeparator,
        UIThemes,
        UIProjSelection,
        TransEntryMethod,
        UITransClose,
        UIPrintVoucher,
        UIForeignBankAccount,
        UITransMode
    }

    public enum VoucherEntryMethod
    {
        Single = 1,
        Multi = 2
    }

    public enum Division
    {
        Local = 1,
        Foreign = 2
    }

    public enum VoucherType
    {
        Receipts = 1,
        Payments = 2,
        Contra = 3,
        Journal = 4
    }
    public enum TransactionVoucherMethod
    {
        Automatic = 1,
        Manual = 2
    }

    public enum TransType
    {
        Receipts = 0,
        Payments = 1,
        Contra = 2,
        Journal = 3
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum EnumColumns
    {
        Id,
        Name
    }

    public enum CurrencyPosition
    {
        Before,
        After,
        None
    }

    public enum CurrencyPositivePattern
    {
        Before = 2,
        After = 3
    }

    public enum CurrencyNegativePatternBracket
    {
        Before = 14,
        After = 15
    }

    public enum CurrencyNegativePatternMinus
    {
        Before = 9,
        After = 8
    }

    public enum MapForm
    {
        Ledger,
        Project,
        CostCentre,
        Donor,
        FDLedger
    }
    public enum ProjectVoucher
    {
        MoveIn,
        MoveOut
    }

    public enum MasterRights
    {
        ReadOnly = 0,
        FullRights = 1
    }

    public enum ReceiptType
    {
        First,
        Subsequent
    }

    public enum VoucherTransType
    {
        Receipt = 1,
        Payment = 2,
        Contra = 3,
        Journal = 4
    }

    public enum TransactionMode
    {
        CR,
        DR
    }

    public enum TransactionAction
    {
        New,
        EditBeforeSave,
        EditAfterSave,
        Cancel
    }
    public enum NumberFormat
    {
        VoucherNumber = 1,
        ReceiptNumber = 2,
        ContraVoucherNumber = 3,
        JournalVoucherNumber = 4
    }
    public enum BankReconciliation
    {
        UnCleared,
        Cleared,
        UnReconciled,
        Reconciled
    }
    public enum FDRenewal
    {
        New = 0,
        Renewal = 1,
        RelizedOn = 2,
        FDOP = 3
    }
    public enum TransSelectionType
    {
        Add = 0,
        View = 1
    }

    public enum FDRenewalMode
    {
        Edit,
        Delete
    }
    public enum TransactionLedgerType
    {
        gvTrans = 0,
        gvCashTrans = 1
    }
    public enum FDTransType
    {
        Invest = 0,
        Reinvest = 1,
        Realize = 2,
        Break = 3
    }

    public enum FDInterestTypes
    {
        Renewal,
        Realize
    }
    public enum IsPeriodically
    {
        Yes,
        No
    }

    public enum LedgerTypes
    {
        FD,
        Ledger,
        GN
    }

    public enum FDTypes
    {
        OP,
        IN,
        RN,
        WD
    }

    public enum FDRenewalTypes
    {
        IRI,
        ACI,
        WDI,
        CLS,
        PWD
    }

    public enum ProjectSelection
    {
        EnableVoucherSelectionMethod,
        DisableVoucherSelectionMethod
    }

    public enum VoucherPrint
    {
        [Description("RPT-024")]
        CASHBANKRECEIPTS,
        [Description("RPT-025")]
        CASHBANKPAYMENTS,
        [Description("RPT-026")]
        JOURNALVOUCHER,

    }

    public enum DrillDownType
    {
        BASE_REPORT,
        [Description("RPT-DDM")]
        GROUP_SUMMARY,
        [Description("RPT-DDM")]
        GROUP_SUMMARY_RECEIPTS,
        [Description("RPT-DDM")]
        GROUP_SUMMARY_PAYMENTS,

        [Description("RPT-012")]
        LEDGER_SUMMARY,
        [Description("RPT-016")]
        LEDGER_CASH,
        [Description("RPT-017")]
        LEDGER_BANK,
        [Description("RPT-012")]
        LEDGER_SUMMARY_RECEIPTS,
        [Description("RPT-012")]
        LEDGER_SUMMARY_PAYMENTS,

        [Description("RPT-FCR")]
        FC_REPORT,

        [Description("ACPP.Modules.Transaction.frmTransactionMultiAdd,ACPP")]
        LEDGER_VOUCHER,

        [Description("ACPP.Modules.Transaction.frmTransactionMultiAdd,ACPP")]
        LEDGER_CASHBANK_VOUCHER,

        [Description("ACPP.Modules.Transaction.JournalAdd,ACPP")]
        LEDGER_JOURNAL_VOUCHER,

        [Description("RPT-DDM")]
        DRILL_DOWN,
    }

    public enum SiteMenuProvider
    {
        HomeMenuProvider = 1,
        FooterMenuProvider = 2,
        SiteAdminMenuProvider = 3,
        HeadOfficeMenuProvider = 4,
        BranchOfficeMenuProvider = 5
    }

    public enum URLPages
    {
        ErrorPage = 1,
        Default = 2,
        HomeLogin = 3,
        UserView = 4,
        UserAdd = 5,
        RoleView = 6,
        RoleAdd = 7,
        RoleRights = 8,
        ProjectCategoryAdd = 9,
        ProjectCategoryView = 10,
        ProjectAdd = 11,
        ProjectView = 12,
        LegalEntityAdd = 13,
        LegalEntityView = 14,
        RightsView = 15,
        CountryView = 16,
        CountryAdd = 17,
        HeadOfficeAdd = 18,
        HeadOfficeView = 19,
        LedgerAdd = 20,
        LedgerView = 21,
        DonorAdd = 22,
        DonorView = 23,
        BranchOfficeView = 24,
        BranchOfficeAdd = 25,
        SoftwareView = 26,
        SoftwareAdd = 27,
        ChangePassword = 28,
        EndUserDownload = 35,
        TroubleTicketingView = 36,
        MapProject = 37,
        GenerateLicenseKey = 38,
        VoucherView = 39,
        ReportViewer = 40,
        ChangePasswordSuccess = 41,
        AmendmentView = 42,
        AccountingPeriodAdd = 43,
        AccountingPeriodView = 44,
        GoverningMemberAdd = 45,
        GoverningMemberView = 46,
        BranchLocationView = 47,
        BranchLocationAdd = 48,
        SendMessage = 49,
        ViewMessage = 50,
        AssetItemView = 51,
        AssetItemAdd = 52,
        AssetUnitofMeasureView = 53,
        AssetUnitofMeasureAdd = 54,
        LockVoucherAdd = 55,
        LockVoucherView = 56,
        FCPurposeAdd = 57,
        FCPurposeView = 58,
        BudgetAdd = 59,
        BudgetView = 60,


        [Description("Portal")]
        Portal = 101,

    }

    public enum CommandMode
    {
        None,
        Add,
        Edit,
        Delete,
        Save,
        Cancel,
        Select,
        LookUp,
        View,
        Print,
        License,
        Send,
        Email,
        UserRight,
        Apply,
        Settle,
        Download,
        Status,
        DB,
        Reset,
        Data,
        Key,
        Resend
    }

    public enum ControlType
    {
        Label,
        LinkButton,
        Literal,
        HyperLink,
        ImageButton,
        TextBox,
        ComboBox,
        CheckBox
    }
    public enum UserRole
    {
        Admin = 1,
        BranchAdmin = 2,
        Others = 0
    }
    public enum OfficeStatus
    {
        Created = 1,
        Activated = 2,
        DeActivated = 3
    }
    public enum UserCreatedStatus
    {
        HeadOfficeCreated = 1,
        UserCreated = 2,
        UserCommunicated = 3
    }
    public enum UserRightsType
    {
        Page = 0,
        Task = 1
    }
    public enum UserRightsMode
    {
        UserGroup = 1,
        User = 2
    }
    public enum RightsModule
    {
        None,
        User,
        Data,
        Office,
        Tools,
        TroubleTicket,
        Help,
        Message,

        ReportAbstract,
        ReportBookofAccounts,
        ReportBankActivities,
        ReportCostCentre,
        FinalBudgetData,
        IncomeExpenditureRome
    }

    public enum ConfirmationMode
    {
        SMS,
        Email
    }
    public enum RightsActivity
    {
        None,
        HeadOfficeView,
        BranchOfficeView,
        CreateLicenseKey,
        SoftwareUpload,
        SoftwareModify,
        SoftwareDelete,
        SoftwareDownload,
        BranchLoggedHistory,
        UploadVoucherFile,
        DownloadLicenseKey,
        ViewTicket,
        TicketView,
        TrackTicket,
        PostTicket,
        ChangeTicketStatus,
        TicketEdit,
        TicketDelete,
        SendMail,
        CommunicateLoginInfo,
        DownloadMasterData,
        ProjectView,
        BudgetView,
        ProjectCategoryView,
        AuditLockView,
        LedgerGroupView,
        LedgerView,
        LegalEntityView,
        CountryView,
        FCPurposeAdd,
        FCPurposeEdit,
        FCPurposeDelete,
        FCPurposeView,
        VoucherView,
        FDRegistersView,
        MapProjectToBranch,
        HeadOffficeApprove,
        UpdateDatbaseConnection,
        HeadOfficeEdit,
        HeadOfficeDelete,
        HeadOfficeAdd,
        UserView,
        ProjectAdd,
        ProjectEdit,
        ProjectDelete,
        BudgetAdd,
        BudgetEdit,
        BudgetDelete,
        BranchOfficeAdd,
        BranchOfficeApprove,
        BranchOfficeEdit,
        BranchOfficeDelete,
        CountryAdd,
        CountryEdit,
        CountryDelete,
        UserRights,
        ProjectCategoryAdd,
        ProjectCategoryEdit,
        ProjectCategoryDelete,
        LockVoucherAdd,
        LockVoucherEdit,
        LockVoucherDelete,
        LegalEntityAdd,
        LegalEntityEdit,
        LegalEntityDelete,
        LedgerAdd,
        LedgerEdit,
        LedgerDelete,
        LedgerGroupAdd,
        LedgerGroupEdit,
        LedgerGroupDelete,
        DeleteVoucher,
        ClearLogData,
        AmendmentView,
        PostAmendment,
        MapLedgerstoBranch,
        TDSPolicyView,
        ImportMasterData,
        GoverningMemberAdd,
        GoverningMemberEdit,
        GoverningMemberDelete,
        GoverningMemberView,
        //Asset
        UnitofMeasureView,
        UnitofMeasureAdd,
        UnitofMeasureEdit,
        UnitofMeasureDelete,
        AssetItemView,
        AssetItemAdd,
        AssetItemEdit,
        AssetItemDelete,
        ImportAssetMasters,
        Help,
        UserManual
    }
    public enum Accessibility
    {
        Both = 0,
        HeadOffice = 1,
        BranchOffice = 2
    }
    public enum FileUploadType
    {
        Build = 0,
        Prerequisite = 1
    }
    public enum TicketPriority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
    public enum FDStatus
    {
        All = 1,
        Active = 2,
        Closed = 3
    }
    public enum DataSyncStatus
    {
        Received = 1,
        InProgress = 2,
        Closed = 3,
        Failed = 4
    }
    public enum NumberFormats
    {
        BranchKeyUniqueCode = 1,
        LicenseIdentificationNumber = 2,
        LC_Branch_Enable_Request_IdentificationNumber = 3,
    }

    public enum ModuleList
    {
        [Description("Finance")]
        AcmeerpFinance = 1,
        [Description("Statutory")]
        AcmeerpTDS = 2,
        [Description("Fixed Asset")]
        AcmeerpFixedAsset = 3,
        [Description("Payroll")]
        AcmeerpPayroll = 4,
        [Description("Stock")]
        AcmeerpStock = 5,
        [Description("Networking")]
        AcmeerpNetworking = 6,
        [Description("Reports")]
        AcmeerpReports = 7,
        [Description("Cristo")]
        AcmeerpCristo = 8
    }

    public enum LincenseReportList
    {
        [Description("Profit and Loss (Verification)")]
        RPT_199 = 1,
        [Description("Ledger-wise Receipts and Payments (Cash/Bank)")]
        RPT_208 = 2,
        [Description("Abstract Receipts - Cash and Bank")]
        RPT_214 = 3,
        [Description("Abstract Payments - Cash and Bank")]
        RPT_215 = 4,
        [Description("Abstract Receipts and Payments - Cash and Bank")]
        RPT_216 = 5
    }


    public enum DeploymentType
    {
        Standalone = 0,
        ClientServer = 1

    }
    public enum AmendmentStatus
    {
        Posted = 1,
        Updated = 2
    }

    public enum TroubleTicketStatus
    {
        Posted = 1,
        Completed = 2,
        InPrograss = 3,
        Clarification = 4
    }

    public enum AccountingYearType
    {
        FinancialYear = 0,
        CalendarYear = 1
    }

    public enum Association
    {
        Cultural = 0,
        Economic = 1,
        Educational = 2,
        Religious = 3,
        Social = 4,
        Others = 5
    }

    public enum UserCommunication
    {
        Email = 1,
        BroadCast = 2,
        Both = 3
    }

    public enum Natures
    {
        Income = 1,
        Expenses = 2,
        Assert = 3,
        Libilities = 4
    }

    public enum Denomination
    {
        Hindu = 0,
        Sikh = 1,
        Muslim = 2,
        Christian = 3,
        Buddhist = 4,
        Others = 5
    }
    public enum TDSLedgerGroup
    {
        ExpensesLedger = 8,
        DutiesAndTax = 24,
        SundryCreditors = 26
    }

    public enum ModuleTemplate
    {
        Finance,
        Asset,
        TDS,
        PayRoll,
        Stock
    }

    public enum BranchUploadAction
    {
        BranchReport, 
        BranchVouchers,
        BranchDatabase,
        BranchVoucherAttachFiles,
    }

    public enum HeadOfficeReport
    {
        [Description("Subsidy")]
        Subsidy = 1,
        [Description("Contribution")]
        Contribution = 2
    }
    public enum FixedAssetDefaultUOM
    {
        Nos = 1
    }

    public enum Id
    {
        BranchId,
        ProjectId,
        HeadofficeId,
        LedgerId,
        LedgerGroupId
    }
}
