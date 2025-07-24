/*  Class Name      : EnumActivityDataCommand.cs
 *  Purpose         : Enum Data type for Indetifying SQL Statement from UI request
 *  Author          : CS
 *  Created on      : 02-Aug-2010
 */

namespace Bosco.DAO.Schema
{
    public class SQLCommand
    {
        public enum User
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchUserNames,
            FetchAll,
            Authenticate,
            CheckOldPassword,
            ResetPassword,
            FetchUserId,
            UpdateUserStatus,
            FetchAllHeadOffice,
            FetchAllCreatedBy,
            FetchUserByHeadOffice,
            Profile,
            ResetPasswordByUserName,
            FetchAllByHeadOfficeUser,
            FetchAdminEmail,
            FetchHeadOfficeUsers,
            FetchHeadOfficeUsersAdmin,
            FetchBranchOfficeUsers,
            FetchBranchOfficeUsersAdmin
        }

        public enum Budget
        {
            FetchById,
            SDBFetchById,
            FetchByStatisticId,
            FetchAll,
            FetchBudgetNames,
            FetchBudget,
            FetchRecentBudgetList,
            AddNewBudgetFetchLedger,
            FetchBudgetLedgerAll,
            DeleteBudgetLedgerById,
            DeleteBudgetProjectById,
            DeleteBudgetStatisticsDetails,
            CheckStatus,
            AddPeriod,
            FetchOneTwoMonthStatus,
            AddAnnual,
            AddStatisticDetails,
            BudgetLoad,
            Update,
            UpdateOnline,
            Delete,
            DeleteAllotFund,
            AnnualBudgetProjectAdd,
            BudgetLedgerAdd,
            BudgetLedgerUpdate,
            BudgetOnlineLedgerUpdate,
            BudgetSubLedgerUpdate,
            BudgetLedgerDelete,
            FetchMappedLedgers,
            ChangeStatusToInActive,
            FetchBudgetBalance,
            CheckBudgetByDate,
            FetchbyBudgetProject,
            AddAllotFund,
            UpdateAllotFund,
            FetchAllotFund,
            GetLedgerExist,
            GetRandomMonth,
            FetchBudgetByProject,
            ImportBudget,
            FetchBudgetAmount,
            AnnualBudgetFetchAdd,
            AnnualBudgetFetchEdit,
            BudgetAddEditDetails,
            BudgetAddEditDetailsProposals,
            BudgetMysoreDetails,
            AnnualBudgetFetch,
            CalendarYearBudget,
            AnnualBudgetProject,
            InsertBudgetCostCentreDetails,
            DeleteBudgetCCdetailsByBudgetId,
            FetchCostCentreByLedger,
            CheckForBudgetEntry,
            FetchBudgetedProjects,
            FetchBudgetProjectforLookup,
            FetchLastBudgetMonth,
            FetchProjectforBudget,
            FetchBudgetIdByDateRangeProject,
            FetchBranchCodebyBranchId
        }

        public enum Ledger
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
        }
        public enum Bank
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchforLookup,
            SetBankAccountSource,
            SelectAllBank,
            SelectAllFD,
            FetchFDByProject,
            FetchBankByProject,
            FetchBankCodes,
            FetchSettingBankAccount,

        }

        public enum Setting
        {
            Fetch,
            InsertUpdate,
            InsertUpdateUI,
            Update
        }

        public enum UISetting
        {
            FetchUI,
            InsertUpdateUI,
            DeleteUI,
        }

        public enum DonorAuditor
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchDonor,
            FetchAuditor,
            FetchAuditorList
        }

        public enum LockVoucher
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchLockVoucher,
            FetchBranchByProject,
            FetchLockVoucherById,
            FetchBranchLockVoucherGraceDays,
            FetchBranchLockVoucherGraceDaysByBranchLocation
        }

        public enum AddressBook
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchDonor,
            FetchAuditor,
            FetchOthers,
            FetchAll,
            FetchAuditorList
        }

        public enum FDAccount
        {
            FetchLedgers,
            FetchProjectByLedger,
            DeleteProjectLedger,
            FetchAllProjectId,
            Add,
            Update,
            Delete,
            Fetch,
            FetchLedgerCurBalance,
            FetchFDById,
            FetchLedgerByProject,
            FetchProjectId,
            FetchLedgerBalance,
            DeleteFDAcountDetails,
            FetchFDRenewalById,
            GetLastFDRenewalDate,
            FetchFDRegistersView,
            FetchAccumulatedAmount,
            FetchRenewalByRenewalId,
            UpdateFDStatus,
            FetchVoucherId,
            FetchAccountIdByVoucherId,
            FetchRenewalDetailsById,
            FetchFDStatus,
            FetchDate

        }
        public enum LedgerGroup
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchforLookup,
            FetchforLedgerLookup,
            GetGroupId,
            FetchByGroupId,
            FetchNatureId,
            FetchValidateGroup,
            FetchAccessFlag,
            UpdateImageIndex,
            FetchAccoutType,
            FetchLedger,
            FetchLedgerList,
            FetchLedgerGroupCodes,
            FetchFDLedger,
            FetchSubgroupById,
            LedgerGroupFetchAll,
            FetchSortOrder,
            FetchMainGroupSortOrder,
            FetchLedgerGroupIdbyLedgerGroup,
            FetchHeadOfficeLedgerGroup
        }

        public enum CongregationLedger
        {
            Add,
            Delete,
            Update,
            FetchById,
            FetchAll,
            FetchLedgerList,
            FetchAllParents,
            FetchAllChildLedgers,
            FetchParentLedgers,
            FetchGroupedParentLedgers,
            FetchProjectCategoryLedgers,
            FetchProjectCategorybyGroupedProjectCategory,
            FetchMappedLedgers,
            DeleteMappedLedgers
        }
        public enum CongregationOpeningBalance
        {
            FetchGeneralateAssetLiabilty,
            SaveGeneralateOpeningDetails,
            DeleteGenOpeningBalance
        }
        public enum CongregationMapping
        {
            MapCongregationLedger,
            MapProjectCatogoryCongregationLedger,
            DeleteMappedLedger,
            DeleteGroupedProjectCatogoryLedgers,
            DeleteIndividualMappedLedger,
            FetchLedgerByCongregationLedger,
            FetchLedgerByProjectCategoryLedger,
            CheckingMappedCount,
            CheckingSameNature
        }

        public enum Country
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchCountryList,
            FetchCountryCodeList,
            FetchCurrencySymbolsList,
            FetchCurrencyCodeList,
            FetchCurrencyNameList,
        }
        public enum CostCentre
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchforLookup,
            SetCostCentreSource,
            FetchCostCentreCodes
        }

        public enum MasterTransactionCostCentre
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchCostCentre,
            FetchCostCentreByLedger
        }
        public enum InKindArticle
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll
        }
        public enum ExecutiveMembers
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            MapGoverningMember,
            FetchGoverningMemberByLegalEntity,
            UnmapLegalEntitytoGoverningMember
        }

        public enum Mapping
        {
            FetchMappedCostCenter,
            FetchMappedCostCenterByCostCenterId,
            FetchMappedProject,
            ProjectCostCentreShowMapping,
            FetchProjectforLookup,
            FetchProjectForGridView,
            LoadProjectMappingGrid,
            LoadProjectDonorGrid,
            LoadProjectCostCentreGrid,
            ProjectLedgerMappingDelete,
            DeleteMappedLedgerBalance,
            UnMapProjectLedger,
            FetchMappedFDByFDId,
            FetchMappedLedgers,
            ProjectLedgerMappingAdd,
            DeleteProjectCostCenterMapping,
            UnMapCostCentreByCCId,
            ProjectCostCentreMappingAdd,
            LedgerProjectMappingDelete,
            LoadAllLedgers,
            LoadAllCostCentre,
            LoadLedgerFD,
            LoadAllDonor,
            DonorMap,
            DonorUnMap,
            DonorUnMapByDonorId,
            FetchDonorMapped,
            FetchMappedDonorByDonorId,
            TransactionFixedDepositId,
            BankIdByLedgerId,
            FetchProjects,
            MapLedgersToProject,
            MapCostCentreToProject,
            CheckLedgerMapped,
            CheckDonorMapped,
            CheckCostCentreMapped,
            LoadLedgerByProId,
            LoadProjectFDLedgerGrid,
            FetchProjectBySociety,
            FetchProjectIdByBranchLocation,
            FetchLedgerByProjectCategory,
            FetchProjectBySocietyUser,
            FetchProjectsByLoginUser,
            DeleteGeneralateMapping,
            MapGeneralateLedgers,
            LoadMappedLedgers,
            LoadMappedGeneralateLedgers
        }
        public enum LedgerBank
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchAllWithNature,
            BankAccountAdd,
            BankAccountUpdate,
            BankAccountDelete,
            BankAccountFetch,
            LedgerIdFetch,
            BankAccountIdFetch,
            BankAccountFetchAll,
            FixedDepositFetchAll,
            FetchLedgerForLookup,
            FetchLedgerByGroup,
            FetchCashBankFDLedger,
            IsBankLedger,
            FetchCostCenterId,
            FetchLedgerNature,
            SetLedgerSource,
            SetCongregationLedgerSource,
            FetchConLedgerIdByLedgerGroupId,
            SetLedgerDetailSource,
            FetchLedgerGroupbyLedgerId,
            FetchBankAccountById,
            UpdateFDBankAccount,
            FetchLedgerByLedgerGroup,
            FetchMaturityDate,
            FetchCashBankLedger,
            CheckProjectExist,
            FetchLedgerCodes,
            FetchBankAccountCodes,
            FetchFixedDepositCodes,
            FetchBankInterestLedger,
            FetchFDLedgers,
            FixedDepositByLedger,
            FetchFDLedgerById,
            FDLedgerUpdate,
            LedgerFetchAll,
            FetchLedgersByProjectCategory,
            FetchLedgerDefaultByProjectCategory,
            FetchLedgerNatureByLedgerGroup,
            FetchBranchLedger,
            FetchLedgerDetailSourcebyBranch,
            FetchLedgerIdByLedgerName,
            FetchBranchOfficeDeafultLedger,
            MapDefaultLedgerMapping,
            FetchBudgetGroup,
            FetchBudgetSubGroup,
            FetchFDInvestment,
            FetchProjectCategorybyLedger,
            MapLedgerToProjectCategory,
            DeleteProjectCategoryByLedger,
            FetchLedgerNameByLedgerIds,
            CheckTransactionExistsByDateClose,
            FetchLedgerIdsByLedgerGroup,
        }

        public enum LedgerProfile
        {
            Add,
            Delete,
            Update,
            Fetch
        }

        public enum DeducteeType
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchDeductType,
            FetchActiveDeductTypes
        }

        public enum NatureofPayments
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchSectionCodes,
            FetchNatureofPaymentsSection,
            FetchNatureofPayments,
            FetchTaxRate,
            FetchTDSLedger,
            FetchNatureOfPaymentWithCode,
            FetchTDSWithoutPAN
        }


        public enum FixedDeposit
        {
            FixedDepositAdd,
            FixedDepositUpdate,
            FixedDepositDelete,
            FixedDepositFetch,
            FixedDepositFetchAll,
            BreakUpAdd,
            BreakUpDelete,
            BreakUpFetchByAccountNo,
            FetchFDByID,
            UpdateFD,
            FDRegisterAdd,
            FetchFDNumber,
            FDRegisterUpdate
        }
        public enum Voucher
        {
            Add,
            Update,
            Delete,
            FetchByVoucherId,
            FetchAll,
            FetchVoucherNumberFormat,
            UpdateLastVoucherNumber,
            InsertVoucherNumber,
            FetchVoucherNumberFormatExist,
        }

        public enum LegalEntity
        {
            Add,
            Update,
            Delete,
            FetchAll,
            FetchByID,
            FetchSocieties,
            FetchSocietyByBranch,
            FetchSocietyByProject,
            LegalEntityFetchAll,
            FetchLegalEntityByBranch,
            FetchBranchAttachedSociety,
            CheckLegalEntity,
            LegalEntityCount,
            FetchCustomerIdyBySocietyName
        }

        public enum Project
        {
            Add,
            Update,
            UpdateClosedDate,
            Delete,
            Fetch,
            FetchAll,
            FetchAllWithBranch,
            FilterVoucherTypeById,
            DeleteProjectVouchers,
            FetchDivision,
            FetchVoucherTypes,
            FetchVouchers,
            FetchLedgers,
            FetchProjectIdByProjectName,
            AddProjectVouchers,
            AvailableVoucher,
            ProjectVoucher,
            FetchProjectList,
            ProjectCostCentreShowMapping,
            ProjectCategory,
            FetchDefaultVouchers,
            FetchAvailableVouchers,
            FetchVoucherDetailsByProjectId,
            FetchRecentProject,
            DeleteProject,
            DeleteVoucher,
            FetchProjectCodes,
            FetchProjectDetails,
            FetchDefaultProjectVouchers,
            FetchSelectedProjectVouchers,
            LoadAllLedgerByProjectId,
            ProjectFetchAll,
            FetchpProjectByLegalEntity,
            FetchProjectIdByProjectCategory,
            FetchProjectIdsByProjectCategory,
            FetchProjectByBranch,
            DeleteProjectBranch,
            DeleteProjectLedger,
            DeleteProjectCategoryLedger,
            DeleteProjectCostCentre,
            ProjectBalance,
            ProjectCount,
            ProjectCategoryViseProjectCount,
            FetchBranchBalance,
            FetchProjectByUser,
            FetchAllBranchProjects
        }
        public enum Purposes
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            PurposeFetchAll
        }
        public enum AuditInfo
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll
        }

        public enum AccountingPeriod
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchForSettings,
            UpdateStatus,
            UpdateBooksbeginningDate,
            FetchIsFirstAccountingyear,
            FetchBooksBeginingFrom,
            FetchTransactionYearTo,
            ValidateBooksBegining,
            FetchActiveTransactionperiod,
            CheckIstransacton,
            FecthRecentProjectDetails,
            FetchMaxAccountingPeriod,
            IsActivePeriod,
            InsertAccountingYear,
            IsActivePeriodId,
            FetchRecentVoucherDate,
            IsAccountingPeriodExists

        }

        public enum UserRights
        {
            Add,
            Update,
            Fetch,
            FetchAll
        }

        public enum Rights
        {
            Add,
            Fetch,
            FetchRightsByRole,
            Delete,

            FetchActivities,
            FetchModuleList,
            FetchRightsType,
            FetchAllRightsByUserGroup,
            FetchRoleType,
            InsertRightsByUserRole,
            CheckDuplicateUserRightsByUserRole,
            DeleteRightsByUserRole,
            FetchUserRightsForMenu,
            FetchRightsByUserRole
        }

        public enum ProjectCatogory
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchITR,
            FetchByName,
            FetchAll,
            ProjectCategoryFetchAll,
            MapProjectCategorytoLedger,
            UnmapProjectCategorytoLedger,
            ProjectCategoryCount,
            CreatUpdateDefaultLedgerDetails
        }

        public enum UserRole
        {
            Add,
            Edit,
            Delete,
            Fetch,
            FetchAll,
            FetchUserRole,
            FetchRole,
            FetchRoleByAdmin,
            FetchRoleByRoleId
        }

        public enum Module
        {
            Add,
            Edit,
            Delete,
            Fetch,
            FetchAll
        }

        public enum ManageSecurity
        {
            Edit,
            Fetch,
            FetchUserRole
        }

        public enum MasterRights
        {
            FetchByMasterName
        }

        public enum VoucherMaster
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchMasterByID,
            FetchMasterByBranchLocationVoucherId,
            FetchMasterDetails,
            FetchVoucherStartingNo,
            IsTransactionMadeForProject,
            IsTransactionMadeForLedger,
            FetchLastVoucherDate,
            FetchBRS,
            UpdateBRS,
            FetchJournalDetails,
            FetchJournalTransDetails,
            CheckProjectExist,
            VoucherFDInterestAdd,
            VoucherFDInterestDelete,
            FetchFDVoucherInterest,
            FetchFDVoucherInterestByVoucherId,
            FetchFDVoucherPostedInterest,
            FetchMasterVoucherDetails,
            FetchVoucherDate,
            FetchOPBalanceDate,
            DeleteOPBalance,
            DeleteOPFDOPBalance,
            DeleteCCVoucher,
            DeleteFDVoucher,
            DashboardCashBankFlow,
            DashboardReceiptPayments,
            DataSynStatusbyMonth,
            DataSynStatusNobyMonth,
            FetchVoucherMasterById,
            FetchDeducteeTypes,
            FetchTaxDetails,
            FetchDataSynStatusProjectWise,
            FetchDataSynStatusProjectUserWise,
            FetchNonConformityBranches,
            FetchBRSByMaterialized,
            CheckTransExistsByDateProject,
            CheckBranchTransExist,
            FetchVouchersInOtherProjectsOrDates,

        }
        public enum VoucherTransDetails
        {
            Add,
            Edit,
            Delete,
            Fetch,
            FetchAll,
            FetchTransactionByID,
            FetchTransactionDetails,
            FetchTransDetails,
            FetchCashBankDetails,
            TransOPBalance,
            TransCBBalance,
            FetchJournalDetailById,
            FetchFixedDepositStatus,
            DeleteFDAccount,
            DeleteFDRenewal,
            TransFDCBalance,
            FetchReceipts,
            FetchPayment,
            FetchLedgerSummary
        }

        public enum TransBalance
        {
            UpdateBalance,
            FetchTransaction,
            FetchOpBalanceList,
            FetchOpBalance,
            FetchGroupSumBalance,
            FetchBalance,
            HasBalance,
            DeleteBalance,
            FetchLiquidGroupBalance,
            FetchLedgerName,
            FetchGroupName,
            FetchCCOPBalance,
            FetchBalanceIE,
            FetchBranchBalance
            //FetchOpeningBalance
        }

        public enum FDRenewal
        {
            FetchFixedDepositStatus,
            Add,
            Update,
            UpdateById,
            UpdateFDStatus,
            UpdateStatusByID,
            DeleteFDByID,
            Fetch,
            FetchAll,
            FetchById,
            DeleteFDRegisters,
            UpdateLastFDRow,
            FetchFDRegisters



        }

        public enum InKindReceived
        {
            Add,
            Update,
            Fetch,
            Delete,
            FetchAll
        }
        public enum InKindUtilised
        {
            Add,
            Update,
            Fetch,
            FetchAll
        }
        public enum HeadOffice
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchDatabase,
            FetchBranch,
            FetchBranchByUser,
            FetchCountry,
            FetchState,
            FecthType,
            Status,
            FetchHOAUser,
            UpdateUserStatus,
            FetchActiveHeadOffice,
            UpdateDB,
            FetchByCode,
            FetchAllHeadOffice,
            FetchHeadOffice,
            FetchAllToExport,
            FetchMailId,
            FetchHeadOfficeMailId,
            FetchBranchOfficeMailId,
            FetchMainContent,
            ScheduleDataSycnTask,
            GetActiveOfficeInfo,
            FetchDataSyncStatus,
            LoginUserHeadOffice,
            FetchHeadOfficeforCombo,
            FetchAccountingYearType,
            FetchDataSyncMessage,
            FetchBranchDetails

        }
        public enum BranchOffice
        {
            Add,
            Update,
            UpdateUserInfo,
            Delete,
            Fetch,
            FetchAll,
            FetchAllBranchesByUser,
            BranchByProject,
            Status,
            FetchBOAUser,
            UpdateUserStatus,
            View,
            FetchByBranchCode,
            FetchAllBranchOffice,
            FetchAllBranchOfficeByHeadOffice,
            FetchRenewalBranchOfficeByDays,
            FetchBranchHistoryMoreThanOneSystemByBranch,
            FetchBranchHistoryMoreThanOneSystemByBranchLocation,
            FetchAllToExport,
            FetchByBranchCodeAvailable,
            FetchBranch,
            FetchBranchbyLocations,
            FetchBranchbyGracedays,
            InsertGraceDays,
            DeleteGraceDays,
            FetchBranchByUser,
            FetchBranchByBudget,
            DeleteMappedBranchtoProjects,
            MapBranchtoProject,
            FetchProjectbyBranch,
            FetchActiveBranchs,
            FetchProjectsforCombo,
            FetchBudgetforCombo,
            FetchBudgetforCombosdbinm,
            FetchBudgetforComboByTwoMonths,
            FetchProjectsForVoucherLock,
            FetchBranchbyHeadOffice,
            FetchBranchforKeyDownload,
            FetchBranchforKeyDownloadByUser,
            FetchBranchforKeyDownloadByUserId,
            FetchSubBranches,
            FetchBranchByBranchPartCode,
            FetchBranchUserId,
            FetchBranchforMasterDownload,
            FetchBranchforMasterDownloadByUser,
            MapProjectToUser,
            MapBranchToUser,
            DeleteMappedUsertoProjects,
            DeleteMappedUsertoBranch,
            FetchProjectbyHeadOfficeUsers,
            FetchProjectbyHeadOfficeUsersFilter,
            FetchBranchbyHeadOfficeUsers,
            FetchBranchbyHeadOfficeUsersFilter,
            FetchProjectbyBranchOfficeUsers,
            FetchBranchbyBranchOfficeUsers,
            FetchProjectsforComboByLoginUser,
            FetchBranchProjectBudgetComboByLoginUser,
            FetchMailByBranch,
            FetchMailByBranchCode,
            FetchBudgetProject,
            SendMessage,
            AddMessage,
            ViewMessageDetail,
            FetchMessage,
            UpdateMessage,
            DeleteMessageBranch,
            FetchBranchIdByBranchCode,
            DeleteLicenseByBranch,
            FetchBranches,
            FetchBranchLoggedInfoByHeadOfficeCode,
            FetchHeadOfficewiseBranchOffice,
            FetchHeadOfficewiseBranchOfficeCount,
            FetchHeadOfficewiseBranchOfficeDetailed,
            FetchBranchLoggedInfoByHeadOfficeCodeByBranch,
            IsExistsBranchLoggedInfo,
            InsertBranchLoggedInfo,
            UpdateBranchLoggedInfo,
        }

        public enum BranchLocation
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchBranchLocation,
            FetchLocationbyBranch,
            FetchLocationbyBranchLocation
        }

        public enum Software
        {
            Add,
            Update,
            Delete,
            Download,
            FetchAll,
            FetchByType,
            FetchByCurrentMonth,
            DeleteLog

        }
        public enum TroubleTicket
        {
            Add,
            Update,
            FetchAll,
            Delete,
            FetchTicketById,
            UpdateStatus,
            FetchReplies,
            FetchTicketsByBranch,
            FetchUserDetailsByBOCode
        }
        public enum License
        {
            Add,
            Update,
            Delete,
            FetchAll,
            FetchByCurrentMonth,
            NewBranchUniqueCodeFetch,
            NewLicenseIdentificationNumberFetch,
            NewLCBranchEnableRequestIdentificationNumberFetch,
            IsLicenseNoExist,
            LicenseDetailsByLicenseIdFetch,
            LicenseDetailsByBranchCodeFetch,
            LicenseDetailsByBranchIdFetch,

            FetchLCBranchClientEnableModuleRequests,
            FetchLCBranchClientEnableModuleRequestsByBranch,
            IsExistsLCBranchClientEnableModuleRequestsByBranchRequestCode,
            RequestLCBranchClientEnableModuleRequests,
            UpdateLCBranchReceiptModuleStatus,
            DeleteAllLCBranchRequests,
            DeleteLCBranchRequestsByBranch,
        }
        public enum Amendments
        {
            Add,
            FetchAmendments,
            UpdateStatus,
            FetchVocherDetail,
            Save,
            FetchRemark,
            UpdateRemark,
            FetchAmendmentHistory
        }
        public enum ExportMasters
        {
            LegalEntityFetchAll,
            ProjectCategoryFetchAll,
            ProjectFetchAll,
            LedgerGroupFetchAll,
            LedgerFetchAll,
            PurposeFetchAll,

            //Budget
            FetchBudgetMasterByDateRange,
            FetchBudgetProjectByDateRange,
            FetchBudgetLedgerByDateRange,
            FetchBudgetSubLedgerByDateRange
        }

        public enum ImportVoucher
        {
            //Budget
            FetchBranchProjects,
            FetchProjects,
            GetBudgetId,
            InsertBudgetMaster,
            UpdateBudgetMaster,
            InsertBudgetProject,
            InsertBudgetLedger,
            InsertBudgetSubLedger,
            DeleteBudgetProject,
            DeleteBudgetLedger,
            DeleteBudgetSubLedger,

            //For Sub Ledgers Vouchers
            GetSubLedgerId,
            InsertSubLedger,
            MapSubLedger,
            DeleteVoucherSubLedger,
            InsertVoucherSubLedger,
            MapProjectBudgetLedger
        }

        public enum TDS
        {
            FetchTDSSection,
            FetchNatureofPaymentsSection,
            FetchDeducteeTypes,
            FetchDutyTax
        }
        public enum AssetClass
        {
            Add,
            Fetch,
            FetchAll,
            Delete,
            Update,
            FetchSelectedClass,
            FetchClassNameByParentID,
            FetchDepreciationMethod,
            FetchbyID,
            FetchAssetSubClassbyAssetParentId,
            FetchAssetClassIdByName
        }

        public enum AssetItem
        {
            Add,
            Fetch,
            FetchAll,
            Delete,
            Update,
            FetchAllAssetItems,
            FetchAssetItemIdByName

        }

        public enum AssetUnitOfMeasure
        {
            Add,
            Fetch,
            FetchAll,
            Delete,
            Update,
            FetchForFirstUnit,
            FetchUnitOfMeasureId
        }
    }
}