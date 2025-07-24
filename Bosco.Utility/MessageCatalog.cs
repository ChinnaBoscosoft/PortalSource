/*  Class Name      : MessageCatalog.cs
 *  Purpose         : Declare common messages
 *  Author          : Salamon Raj M
 *  Created on      : 26-07-2013
 */

using System;

namespace Bosco.Utility
{
    public class MessageCatalog
    {
        #region Common Messages
        public class Common
        {
            public const string COMMON_MESSAGE_TITLE = "COMMON_MESSAGE_TITLE";
            public const string COMMON_DELETE_CONFIRMATION = "COMMON_DELETE_CONFIRMATION";
            public const string COMMON_WELCOME_NOTE = "COMMON_WELCOME_NOTE";
            public const string COMMON_WAIT_DIALOG_CAPTION = "COMMON_WAIT_DIALOG_CAPTION";
            public const string COMMON_PROCESS_DIALOG_CAPTION = "COMMON_PROCESS_DIALOG_CAPTION";
            public const string COMMON_GRID_EMPTY = "COMMON_GRID_EMPTY";
            public const string COMMON_EMAIL_INVALID = "COMMON_EMAIL_INVALID";
            public const string COMMON_URL_INVALID = "COMMON_URL_INVALID";
            public const string COMMON_SAVE_FAILURE = "COMMON_SAVE_FAILURE";
            public const string COMMON_SAVED_CONFIRMATION = "Saved Successfully";
            public const string COMMON_DELETED_CONFIRMATION = "COMMON_DELETED_CONFIRMATION";
            public const string COMMON_PRINT_MESSAGE = "COMMON_PRINT_MESSAGE";
            public const string COMMON_INVALID_EXCEPTION = "COMMON_INVALID_EXCEPTION";
            public const string COMMON_NOSELECTION_FOR_EDIT = "COMMON_NOSELECTION_FOR_EDIT";
            public const string COMMON_NOSELECTION_FOR_DELETE = "COMMON_NOSELECTION_FOR_DELETE";
            public const string COMMON_DELETE_FAILURE = "COMMON_DELETE_FAILURE";

        }
        #endregion

        #region User
        public class User
        {
            public const string USER_VIEW = "USER_VIEW";
            public const string USER_ADD_CAPTION = "USER_ADD_CAPTION";
            public const string USER_EDIT_CAPTION = "USER_EDIT_CAPTION";
            public const string USER_CHANGE_PASSWORD = "USER_CHANGE_PASSWORD";
            public const string USER_FORGOT_PASSWORD = "USER_FORGOT_PASSWORD";
            public const string USER_LOGIN = "USER_LOGIN";
            public const string USER_INVALID = "USER_INVALID";
            public const string USER_LOGIN_SUCCESS = "USER_LOGIN_SUCCESS";
            public const string USER_SESSIONEXPIRY_INVALID = "USER_SESSIONEXPIRY_INVALID";
            public const string USER_NAME_EMPTY = "USER_NAME_EMPTY";
            public const string USER_PASSWORD_EMPTY = "USER_PASSWORD_EMPTY";
            public const string USER_CONFIRM_PASSWORD_EMPTY = "USER_CONFIRM_PASSWORD_EMPTY";
            public const string USER_PASSWORD_UNMATCHED = "USER_PASSWORD_UNMATCHED";
            public const string USER_SAVED_SUCCESS = "USER_SAVED_SUCCESS";
            public const string USER_DELETE_SUCCESS = "USER_DELETE_SUCCESS";
            public const string USER_PRINT_CAPTION = "USER_PRINT_CAPTION";
            public const string USER_FIRST_NAME_EMPTY = "USER_FIRST_NAME_EMPTY";
            public const string USER_CURRENT_PASSWORD_EMPTY = "USER_CURRENT_PASSWORD_EMPTY";
            public const string USER_NEW_PASSWORD_MISMATCH = "USER_NEW_PASSWORD_MISMATCH";
            public const string USER_CURRENT_PASSWORD_FAIL = "USER_CURRENT_PASSWORD_FAIL";
            public const string USER_NEW_PASSWORD_EMPTY = "USER_NEW_PASSWORD_EMPTY";
            public const string USER_RESET_PASSWORD_SUCCESS = "USER_RESET_PASSWORD_SUCCESS";
        }

        public class UserRole
        {
            //public const string USERROLE_DELETE_SUCCESS = "USERROLE_DELETE_SUCCESS";
            // public const string USERROLE_SAVE_SUCCESS = "USERROLE_SAVE_SUCCESS";
            // public const string USERROLE_CAPTION = "USERROLE_CAPTION";
            //public const string USERROLE_NAME = "USERROLE_NAME";
            // public const string USERROLE_DELETE = "USERROLE_DELETE";
            //public const string USERROLE_DELETE_ASSOCIATION = "USERROLE_DELETE_ASSOCIATION";
            //  public const string USERROLE_EXITS = "USERROLE_EXITS";
            public const string USER_ROLE_ADD_CAPTION = "USER_ROLE_ADD_CAPTION";
            public const string USER_ROLE_EDIT_CAPTION = "USER_ROLE_EDIT_CAPTION";
            public const string USER_ROLE_DELETE_SUCCESS = "USER_ROLE_DELETE_SUCCESS";
            public const string USERROLE_EMPTY = "USERROLE_EMPTY";
            public const string USER_ROLE_SAVE_SUCCESS = "USER_ROLE_SAVE_SUCCESS";
            public const string USER_ROLE_PRINT_CAPTION = "USER_ROLE_PRINT_CAPTION";
        }
        #endregion

        #region Master
        public static class Master
        {
            public class Bank
            {
                public const string BANK_DELETE_SUCCESS = "BANK_DELETE_SUCCESS";
                public const string BANK_SAVE_SUCCESS = "BANK_SAVE_SUCCESS";
                public const string BANK_CODE_EMPTY = "BANK_CODE_EMPTY";
                public const string BANK_NAME_EMPTY = "BANK_NAME_EMPTY";
                public const string BANK_BRANCH_EMPTY = "BANK_BRANCH_EMPTY";
                public const string BANK_SAVE_FAILURE = "BANK_SAVE_FAILURE";
                public const string BANK_ADD_CAPTION = "BANK_ADD_CAPTION";
                public const string BANK_EDIT_CAPTION = "BANK_EDIT_CAPTION";
                public const string BANK_ACCOUNT = "BANK_ACCOUNT";
                public const string BANK_OPERATING_CAPTION = "BANK_OPERATING_CAPTION";
                public const string BANK_DELETE = "BANK_DELETE";
                public const string BANK_DELETE_ASSOCIATION = "BANK_DELETE_ASSOCIATION";
                public const string BANK_PRINT_CAPTION = "BANK_PRINT_CAPTION";
            }

            public class Budget
            {
                public const string BUDGET_ADD_CAPTION = "BUDGET_ADD_CAPTION";
                public const string BUDGET_EDIT_CAPTION = "BUDGET_EDIT_CAPTION";
                public const string BudgetViewPageTitle = "Budget";
            }

            public class Country
            {
                public const string COUNTRY_SAVE_SUCCESS = "COUNTRY_SAVE_SUCCESS";
                public const string COUNTRY_DELETE_SUCCESS = "COUNTRY_DELETE_SUCCESS";
                public const string COUNTRY_DELETE_FAILURE = "COUNTRY_DELETE_FAILURE";
                public const string COUNTRY_NAME_EXIST = "COUNTRY_NAME_EXIST";
                public const string COUNTRY_CODE_EMPTY = "COUNTRY_CODE_EMPTY";
                public const string CURRENCY_SYMBOL_EMPTY = "CURRENCY_SYMBOL_EMPTY";
                public const string CURRENCY_CODE_EMPTY = "CURRENCY_CODE_EMPTY";
                public const string COUNTRY_ADD_CAPTION = "COUNTRY_ADD_CAPTION";
                public const string COUNTRY_EDIT_CAPTION = "COUNTRY_EDIT_CAPTION";
                public const string COUNTRY_NAME_EMPTY = "COUNTRY_NAME_EMPTY";
                public const string COUNTRY_ASSOCIATION = "COUNTRY_ASSOCIATION";
                public const string COUNTRY_PRINT_CAPTION = "COUNTRY_PRINT_CAPTION";
            }

            public class Mapping
            {
                public const string MAPPING_NEGATIVE_BALANCE = "MAPPING_NEGATIVE_BALANCE";
                public const string LEDGER_MAPPING_SUCCESS = "LEDGER_MAPPING_SUCCESS";
                public const string PROJECT_MAPPING_SUCESS = "PROJECT_MAPPING_SUCCESS";
                public const string COST_CENTER_MAPPING_SUCESS = "COST_CENTER_MAPPING_SUCESS";
                public const string MAPPING_DONOR_SUCCESS = "MAPPING_DONOR_SUCCESS";
                public const string MAPPING_CONTRIBUTION_SUCCESS = "MAPPING_CONTRIBUTION_SUCCESS";
                public const string MAPPING_TRANSACTION_MADE_ALREADY = "MAPPING_TRANSACTION_MADE_ALREADY";
                public const string MAPPING_MAKE_AMOUNT_ZERO = "MAPPING_MAKE_AMOUNT_ZERO";
                public const string MAPPING_FD_LEDGER_RESTRICTION = "MAPPING_FD_LEDGER_RESTRICTION";
                public const string NO_RECORD = "NO_RECORD";
                public const string BOOK_BEGINNING_DATE_EMPTY = "BOOK_BEGINNING_DATE_EMPTY";
                public const string COST_CENTER_TYPE_EMPTY = "COST_CENTER_TYPE_EMPTY";
                public const string TRANSACTION_FD = "TRANSACTION_FD";
                public const string MASTER_VOUCHER_MAPPING = "MASTER_VOUCHER_MAPPING";
            }

            public class CostCentre
            {
                public const string COST_CENTER_SUCCESS = "COST_CENTER_SUCSESS";
                public const string COST_CENTER_SAVE_FAILURE = "COST_CENTER_SAVE_FAILURE";
                public const string COST_CENTER_CODE_EMPTY = "COST_CENTER_CODE_EMPTY";
                public const string COST_CENTER_NAME_EMPTY = "COST_CENTER_NAME_EMPTY";
                public const string COST_CENTER_ADD_CAPTION = "COST_CENTER_ADD_CAPTION";
                public const string COST_CENTER_EDIT_CAPTION = "COST_CENTER_EDIT_CAPTION";
                public const string COST_CENTER_PRINT_CAPTION = "COST_CENTER_PRINT_CAPTION";
                public const string COST_CENTER_DELETE_SUCCESS = "COST_CENTER_DELETE_SUCCESS";

            }

            public class Donor
            {
                public const string DONOR_ADD_CAPTION = "DONOR_ADD_CAPTION";
                public const string DONOR_EDIT_CAPTION = "DONOR_EDIT_CAPTION";
                public const string DONOR_NAME_EMPTY = "DONOR_NAME_EMPTY";
                public const string DONOR_COUNTRY_EMPTY = "DONOR_COUNTRY_EMPTY";
                public const string DONOR_SAVE_SUCCESS = "DONOR_SAVE_SUCCESS";
                public const string DONOR_DELETE_SUCCESS = "DONOR_DELETE_SUCCESS";
                public const string DONOR_PRINT_CAPTION = "DONOR_PRINT_CAPTION";
            }

            public class Auditor
            {
                public const string AUDITOR_ADD_CAPTION = "AUDITOR_ADD_CAPTION";
                public const string AUDITOR_EDIT_CAPTION = "AUDITOR_EDIT_CAPTION";
                public const string AUDITOR_SAVE_SUCCESS = "AUDITOR_SAVE_SUCCESS";
                public const string AUDITOR_DELETE_SUCCESS = "AUDITOR_DELETE_SUCCESS";
                public const string AUDITOR_PRINT_CAPTION = "AUDITOR_PRINT_CAPTION";
                public const string AUDITOR_NAME_EMPTY = "AUDITOR_NAME_EMPTY";
            }

            public class AccountingPeriod
            {
                public const string ACCOUNTING_PERIOD_ADD_PAGE_TITLE = "A/C Period (Add)";
                public const string ACCOUNTING_PERIOD_EDIT_PAGE_TITLE = "A/C Period (Edit)";

                public const string ACCOUNTING_PERIOD_ADD_CAPTION = "ACCOUNTING_PERIOD_ADD_CAPTION";
                public const string ACCOUNTING_PERIOD_EDIT_CAPTION = "ACCOUNTING_PERIOD_EDIT_CAPTION";
                public const string ACCOUNTING_PERIOD_SAVE_SUCCESS = "ACCOUNTING_PERIOD_SAVE_SUCCESS";
                public const string ACCOUNTING_PERIOD_DELETE_SUCCESS = "ACCOUNTING_PERIOD_DELETE_SUCCESS";
                public const string ACCOUNTING_PERIOD_PRINT_CAPTION = "ACCOUNTING_PERIOD_PRINT_CAPTION";
                public const string ACCOUNTING_PERIOD_YEAR_FROM_EMPTY = "Year From is Empty";
                public const string ACCOUNTING_PERIOD_YEAR_TO_EMPTY = "Year To is Empty";
                public const string DATECOMPAREERROR = "Year To is less than Year From";
                public const string ACCOUNTING_PERIOD_YEAR_EQUAL_EMPTY = "ACCOUNTING_PERIOD_YEAR_EQUAL_EMPTY";
                public const string ACCOUNTING_PERIOD_ACTIVE = "ACCOUNTING_PERIOD_ACTIVE";
                public const string ACCOUNTING_PERIOD_CANNOT_DELETE = "ACCOUNTING_PERIOD_CANNOT_DELETE";
                public const string ACCOUNTING_PERIOD_CHANGE_PERIOD = "ACCOUNTING_PERIOD_CHANGE_PERIOD";
                public const string ACCOUNTING_PERIOD_ONE_ACTIVE = "One Accounting period should be active.";
            }

            public class AddressBook
            {
                public const string ADDRESS_ADD_CAPTION = "ADDRESS_ADD_CAPTION";
                public const string ADDRESS_EDIT_CAPTION = "ADDRESS_EDIT_CAPTION";
                public const string ADDRESS_SAVE_SUCCESS = "ADDRESS_SAVE_SUCCESS";
                public const string ADDRESS_DELETE_SUCCESS = "ADDRESS_DELETE_SUCCESS";
                public const string ADDRESS_PRINT_CAPTION = "ADDRESS_PRINT_CAPTION";
                public const string ADDRESS_COUNTRY_EMPTY = "ADDRESS_COUNTRY_EMPTY";
                public const string ADDRESS_NAME_EMPTY = "ADDRESS_NAME_EMPTY";
            }

            public class InKindArticle
            {
                public const string INKINDARTICLE_ADD_CAPTION = "INKINDARTICLE_ADD_CAPTION";
                public const string INKINDARTICLE_EDIT_CAPTION = "INKINDARTICLE_EDIT_CAPTION";
                public const string INKINDARTICLE_DELETE_SUCCESS = "INKINDARTICLE_DELETE_SUCCESS";
                public const string INKINDARTICLE_SAVE_SUCCESS = "INKINDARTICLE_SAVE_SUCCESS";
                public const string INKINDARTICLE_PRINT_CAPTION = "INKINDARTICLE_PRINT_CAPTION";
                public const string INKINDARTICLE_ABBREVATION_EMPTY = "INKINDARTICLE_ABBREVATION_EMPTY";
                public const string INKINDARTICLE_NAME_EMPTY = "INKINDARTICLE_NAME_EMPTY";
            }

            public class ExecutiveMembers
            {
                public const string EXECUTIVE_ADD_CAPTION = "EXECUTIVE_ADD_CAPTION";
                public const string EXECUTIVE_EDIT_CAPTION = "EXECUTIVE_EDIT_CAPTION";
                public const string EXECUTIVE_DELETE_SUCCESS = "EXECUTIVE_DELETE_SUCCESS";
                public const string EXECUTIVE_DELETE_FAILURE = "EXECUTIVE_DELETE_FAILURE";
                public const string EXECUTIVE_SAVE_SUCCESS = "EXECUTIVE_SAVE_SUCCESS";
                public const string EXECUTIVE_NAME_EMPTY = "EXECUTIVE_NAME_EMPTY";
                public const string EXECUTIVE_JOIN_DOB = "EXECUTIVE_JOIN_DOB";
                public const string EXECUTIVE_DOB_EXIT = "EXECUTIVE_DOB_EXIT";
                public const string EXECUTIVE_EXIT_JOIN = "EXECUTIVE_EXIT_JOIN";
                public const string EXECUTIVE_NAIONALITY_EMPTY = "EXECUTIVE_NATIONALITY_EMPTY";
                public const string EXECUTIVE_COUNTRY_EMPTY = "EXECUTIVE_COUNTRY_EMPTY";
                public const string EXECUTIVE_PRINT_CAPTION = "EXECUTIVE_PRINT_CAPTION";
                public const string EXECUTIVE_EMAIL_EMPTY = "EXECUTIVE_EMAIL_EMPTY";
            }

            public class Purposes
            {
                public const string PURPOSE_ADD_CAPTION = "PURPOSE_ADD_CAPTION";
                public const string PURPOSE_EDIT_CAPTION = "PURPOSE_EDIT_CAPTION";
                public const string PURPOSE_DELETE_SUCCESS = "PURPOSE_DELETE_SUCCESS";
                public const string PURPOSE_DELETE_FAILURE = "PURPOSE_DELETE_FAILURE";
                public const string PURPOSE_SAVE_SUCCESS = "PURPOSE_SAVE_SUCCESS";
                public const string PURPOSE_CODE_EMPTY = "PURPOSE_CODE_EMPTY";
                public const string PURPOSE_HEAD_EMPTY = "PURPOSE_HEAD_EMPTY";
                public const string PURPOSE_PRINT_CAPTION = "PURPOSE_PRINT_CAPTION";
            }

            public class Group
            {
                public const string GROUP_ADD_CAPTION = "GROUP_ADD_CAPTION";
                public const string GROUP_EDIT_CAPTION = "GROUP_EDIT_CAPTION";
                public const string GROUP_CODE_EMPTY = "GROUP_CODE_EMPTY";
                public const string GROUP_NAME_EMPTY = "GROUP_NAME_EMPTY";
                public const string GROUP_LEVEL_CHECK = "GROUP_LEVEL_CHECK";
                public const string GROUP_SAVE_SUCCESS = "GROUP_SAVE_SUCCESS";
                public const string GROUP_SAVE_FAILURE = "GROUP_SAVE_FAILURE";
                public const string GROUP_DELETE_SUCCESS = "GROUP_DELETE_SUCCESS";
                public const string GROUP_PARENT_EMPTY = "GROUP_PARENT_EMPTY";
                public const string GROUP_CAN_DELETE = "GROUP_CAN_DELETE";
                public const string GROUP_CAN_EDIT = "GROUP_CAN_EDIT";
                public const string GROUP_NATURE_DELETE = "GROUP_NATURE_DELETE";
                public const string GROUP_FIXED_EDIT = "GROUP_FIXED_EDIT";
            }

            public class Voucher
            {
                public const string VOUCHER_ADD_CAPTION = "VOUCHER_ADD_CAPTION";
                public const string VOUCHER_EDIT_CAPTION = "VOUCHER_EDIT_CAPTION";
                public const string VOUCHER_NAME_EMPTY = "VOUCHER_NAME_EMPTY";
                public const string VOUCHER_TYPE_EMPTY = "VOUCHER_TYPE_EMPTY";
                public const string VOUCHER_METHOD_EMPTY = "VOUCHER_METHOD_EMPTY";
                public const string VOUCHER_SUCCESS = "VOUCHER_SUCCESS";
                public const string VOUCHER_DELETE_SUCCESS = "VOUCHER_DELETE";
                public const string VOUCHER_PRINT_CAPTION = "VOUCHER_PRINT_CAPTION";
                public const string VOUCHER_EXISTS = "VOUCHER_NAME_EXISTS";
            }

            public class Project
            {
                public const string PROJECT_SUCCESS = "PROJECT_SUCCESS";
                public const string PROJECT_FAILURE = "PROJECT_FAILURE";
                public const string PROJECT_CODE_EMPTY = "PROJECT_CODE_EMPTY";
                public const string PROJECT_NAME_EMPTY = "PROJECT_NAME_EMPTY";
                public const string PROJECT_ADD_CAPTION = "PROJECT_ADD_CAPTION";
                public const string PROJECT_EDIT_CAPTION = "PROJECT_EDIT_CAPTION";
                public const string PROJECT_PRINT_CAPTION = "PROJECT_PRINT_CAPTION";
                public const string PROJECT_DELETE_SUCCESS = "PROJECT_DELETE_SUCCESS";
                public const string PROJECT_DELETE_ASSOCIATION = "PROJECT_DELETE_ASSOCIATION";
                public const string PROJECT_AVAILABLE_VOUCHERS = "PROJECT_AVAILABLE_VOUCHERS";
                public const string PROJECT_PROJECT_VOUCHERS = "PROJECT_PROJECT_VOUCHERS";
                public const string PROJECT_VOUCHER_INFO = "PROJECT_VOUCHER_INFO";
                public const string PROJECT_DATE_VALIDATION = "PROJECT_DATE_VALIDATION";
                public const string PROJECT_VOUCHER = "PROJECT_VOUCHER";
                public const string PROJECT_MAP_LEDGER = "PROJECT_MAP_LEDGER";

            }

            public class ProjectCatogory
            {
                public const string PROJECT_CATOGORY_DELETE_SUCCESS = "PROJECT_CATOGORY_DELETE_SUCCESS";
                public const string PROJECT_CATOGORY_SAVE_SUCCESS = "PROJECT_CATOGORY_SAVE_SUCCESS";
                public const string PROJECT_CATOGORY_EMPTY = "PROJECT_CATOGORY_EMPTY";
                public const string PROJECT_CATEGORY_ADD_CAPTION = "PROJECT_CATEGORY_ADD_CAPTION";
                public const string PROJECT_CATEGORY_EDIT_CAPTION = "PROJECT_CATEGORY_EDIT_CAPTION";
                public const string PROJECT_CATOGORY_AVAILABLE = "PROJECT_CATOGORY_AVAILABLE";
                public const string PROJECT_CATEGORY_PRINT_CAPTION = "PROJECT_CATEGORY_PRINT_CAPTION";
            }

            public class AuditorInfo
            {
                public const string AUDITOR_INFO_SUCCESS = "AUDITOR_INFO_SUCCESS";
                public const string AUDITOR_INFO_PROJECT_EMPTY = "AUDITOR_INFO_PROJECT_EMPTY";
                public const string AUDITOR_INFO_STARTEDON_EMPTY = "AUDITOR_INFO_STARTEDON_EMPTY";
                public const string AUDITOR_INFO_CLOSEDON_EMPTY = "AUDITOR_INFO_CLOSEDON_EMPTY";
                public const string AUDITOR_INFO_ADD = "AUDITOR_INFO_ADD";
                public const string AUDITOR_INFO_EDIT = "AUDITOR_INFO_EDIT";
                public const string AUDITOR_INFO_PRINT_CAPTION = "AUDITOR_INFO_PRINT_CAPTION";
                public const string AUDITOR_INFO_AUDITOR_EMPTY = "AUDITOR_INFO_AUDITOR_EMPTY";
                public const string AUDITOR_INFO_DELETE_SUCCESS = "AUDITOR_INFO_DELETE_SUCCESS";
                public const string AUDITOR_BEGIN_DATE = "AUDITOR_BEGIN_DATE";
                public const string AUDITOR_ON_FROM = "AUDITOR_ON_FROM";
                public const string AUDITOR_ON_TO = "AUDITOR_ON_TO";
            }

            public class Ledger
            {
                public const string LEDGER_SUCCESS = "LEDGER_SUCCESS";
                public const string LEDGER_FAILURE = "LEDGER_FAILURE";
                public const string LEDGER_DELETED = "LEDGER_DELETED";
                public const string lEDGER_CODE_EMPTY = "lEDGER_CODE_EMPTY";
                public const string LEDGER_NAME_EMPTY = "LEDGER_NAME_EMPTY";
                public const string LEDGER_GROUP_EMPTY = "LEDGER_GROUP_EMPTY";
                public const string LEDGER_ADD_CAPTION = "LEDGER_ADD_CAPTION";
                public const string LEDGER_EDIT_CAPTION = "LEDGER_EDIT_CAPTION";
                public const string LEDGER_PRINT_CAPTION = "LEDGER_PRINT_CAPTION";
                public const string LEDGER_ACCOUNT_TYPE_EMPTY = "LEDGER_ACCOUNT_TYPE_EMPTY";
                public const string LEDGER_BANK_EMPTY = "LEDGER_BANK_EMPTY";
                public const string LEDGER_ACCOUNT_NUMBER_EMPTY = "LEDGER_ACCOUNT_NUMBER_EMPTY";
                public const string LEDGER_DATE_OPEN_EMPTY = "LEDGER_DATE_OPEN_EMPTY";
                public const string LEDGER_ACCOUNT_DATE_EMPTY = "LEDGER_ACCOUNT_DATE_EMPTY";
                public const string BANK_ACCOUNT_SUCCESS = "BANK_ACCOUNT_SUCCESS";
                public const string BANK_ACCOUNT_DELETED = "BANK_ACCOUNT_DELETED";
                public const string BANK_ACCCOUNT_FAILURE = "BANK_ACCOUNT_FAILURE";
                public const string BANK_ACCOUNT_CODE_EMPTY = "BANK_ACCOUNT_CODE_EMPTY";
                public const string BANK_ACCOUNT_ADD = "BANK_ACCOUNT_ADD";
                public const string BANK_ACCOUNT_EDIT = "BANK_ACCOUNT_EDIT";
                public const string BANK_ACCOUNT_PRINT_CAPTION = "BANK_ACCOUNT_PRINT_CAPTION";
                public const string BANK_ACCOUNT_CLOSEDATE_VALIDATION = "BANK_ACCOUNT_CLOSEDATE_VALIDATION";
                public const string FD_LEDGER_PRINT_CAPTION = "FD_LEDGER_PRINT_CAPTION";
                public const string FD_CREATED_DATE = "FD_CREATED_DATE";
            }

            public class FixedDeposit
            {
                public const string FD_SUCCESS = "FD_SUCCESS";
                public const string FD_DELETED = "FD_DELETED";
                public const string FD_ADD = "FD_ADD";
                public const string FD_EDIT = "FD_EDIT";
                public const string FD_PRINT_CAPTION = "FD_PRINT_CAPTION";
                public const string FD_CREATED_DATE_EMPTY = "FD_CREATED_DATE_EMPTY";
                public const string FD_MATURITY_DATE_EMPTY = "FD_MATURITY_DATE_EMPTY";
                public const string FD_MATURITY_DATE_LESS_THAN_CREATED_DATE = "FD_MATURITY_DATE_LESS_THAN_CREATED_DATE";
                public const string FD_INTEREST_RATE_EMPTY = "FD_INTEREST_RATE_EMPTY";
                public const string FD_AMOUNT_EMPTY = "FD_AMOUNT_EMPTY";
                public const string FD_NUMBER_EMPTY = "FD_NUMBER_EMPTY";
                public const string FD_MATURITY_DATE = "FD_MATURITY_DATE";
                public const string FD_RENEWED_DATE = "FD_RENEWED_DATE";
                public const string FD_RENEWAL_CAPTION = "FD_RENEWAL_CAPTION";
                public const string FD_DEPOSIT = "FD_DEPOSIT";
            }

            public class LedgerType
            {
                public const string General = "GN";
                public const string BankAccounts = "SA";
                public const string FixedDeposit = "FD";
            }

            public class BreakUp
            {
                public const string BREAKUP_AMOUT_ZERO = "BREAKUP_AMOUT_ZERO";
                public const string BREAKUP_REQUIRED_FIELD = "BREAKUP_REQUIRED_FIELD";
                public const string BREAKUP_DATE_EXCEPTION = "BREAKUP_DATE_EXCEPTION";
                public const string BREAKUP_AMOUNT_EXCEEDS = "BREAKUP_AMOUNT_EXCEEDS";
                public const string BREAKUP_SAVED_SUCCESS = "BREAKUP_SAVED_SUCCESS";
                public const string BREAKUP_NOT_TALLIED = "BREAKUP_NOT_TALLIED";
                public const string PERCENTAGE_NEGATIVE = "PERCENTAGE_NEGATIVE";
                public const string BREAKUP_AMOUNT_LESS_THAN = "BREAKUP_AMOUNT_LESS_THAN";
                public const string BREAKUP_AMOUNT_GREATER_THAN = "BREAKUP_AMOUNT_GREATER_THAN";
                public const string BREAKUP_GRID_ERROR = "BREAKUP_GRID_ERROR";
                public const string BREAKUP_INTEREST_RATE_EMPTY_ZERO = "BREAKUP_INTEREST_RATE_EMPTY_ZERO";
                public const string BREAKUP_FD_NUMBER_EMPTY = "BREAKUP_FD_NUMBER_EMPTY";
                public const string BREAKUP_ZERO_ENTRY = "BREAKUP_ZERO_ENTRY";
                public const string BREAKUP_NEGATIVE_BALANCE = "BREAKUP_NEGATIVE_BALANCE";
                public const string BREAKUP_INTEREST_RATE_NEGATIVE = "BREAKUP_INTEREST_RATE_NEGATIVE";
            }

            public class FDRenewal
            {
                public const string FD_NUMBER = "FD_NUMBER";
                public const string FD_AMOUNT = "FD_AMOUNT";
                public const string FD_INTEREST_RATE = "FD_INTEREST_RATE";
                public const string FD_TITLE = "FD_TITLE";
            }

            public class InKindReceived
            {
                public const string INKIND_LEDGER = "INKIND_LEDGER";
                public const string INKIND_ITEM = "INKIND_ITEM";
                public const string INKIND_PURPOSE = "INKIND_PURPOSE";
                public const string INKIND_VALUE = "INKIND_VALUE";
                public const string INKIND_LEDGER_ITEM = "INKIND_LEDGER_ITEM";
            }

            public class FDLedger
            {
                public const string FD_LEDGER_CODE = "FD_LEDGER_CODE";
                public const string FD_LEDGER_NAME = "FD_LEDGER_NAME";
                public const string FD_CASH_BANK_LEDGERS = "FD_CASH_BANK_LEDGERS";
                public const string FD_ACCOUNT_ADD = "FD_ACCOUNT_ADD";
                public const string FD_ACCOUNT_EDIT = "FD_ACCOUNT_EDIT";
                public const string FD_INVESTMENT_ADD = "FD_INVESTMENT_ADD";
                public const string FD_INVESTMENT_EDIT = "FD_INVESTMENT_EDIT";
                public const string FD_OP_DATE_AS_ON = "FD_OP_DATE_AS_ON";
                public const string FD_INVESTENT_DATE_AS_ON = "FD_INVESTENT_DATE_AS_ON";
                public const string FD_OP_AMOUNT = "FD_OP_AMOUNT";
                public const string FD_INVESTMENT_AMOUNT = "FD_INVESTMENT_AMOUNT";
                public const string FD_OPENING_CAPTION = "FD_OPENING_CAPTION";
                public const string FD_INVESTMENT_CAPTION = "FD_INVESTMENT_CAPTION";
                public const string FD_OPENING_GROUP_CAPTION = "FD_OPENING_GROUP_CAPTION";
                public const string FD_INVESTMENT_GROUP_CAPTION = "FD_INVESTMENT_GROUP_CAPTION";
                public const string FD_ACCOUNT_OP_PRINT_CAPTION = "FD_ACCOUNT_OP_PRINT_CAPTION";
                public const string FD_ACCOUNT_INV_PRINT_CAPTION = "FD_ACCOUNT_INV_PRINT_CAPTION";

            }
        }
        #endregion

        public class Message
        {
            public const string Invalid_User = "Invalid Username or Password!";
            public const string Login_Success = "Login Success";
            public const string SessionExpiry = "Your session has been expired. Please login again.";
            public const string Delete_Confirm = "Are you sure to delete?";
            public const string Activate_Confirm = "Are you sure to activate?";
            public const string DeActivate_Confirm = "Are you sure to deactivate?";
            public const string Delete_failure = "Cannot Delete! It is associated";
            public const string Office_Status_failure = "Office status is not changed";
            public const string RecordExistence = "The Record is Available";
            public const string RightsDenied = "Rights Denied";
            public const string HeadOfficeLoginInfo = "Head Office Admin Login Info is communicated";
            public const string BranchOfficeLoginInfo = "Branch Office Admin Login Info is communicated";
            public const string MailSendingFailure = "Sending Mail Failed";
            public const string UserCommunicated = "User is Communicated";
            public const string BranchOfficeActivated = "Branch Office is activated";
            public const string BranchOfficeDeactivated = "Branch Office deactivated";
            public const string ActivateToolTip = "Click here to activate";
            public const string BranchOfficeDeleted = "Branch Office is Deleted";
            public const string HeadOfficeSaved = "Head Office is Saved";
            public const string BranchOfficeSaved = "Branch Office is Saved";
            public const string DatabaseInfoUpdated = "Database info is updated";
            public const string UpdateDBToolTip = "Update database connection";
            public const string HeadofficeActivated = "Head Office is activated";
            public const string HeadofficeDeactivated = "Head Office is deactivated";
            public const string HeadOfficeDeleted = "Head Office is Deleted";
            public const string TicketDeleted = "Ticket is Deleted";
            public const string ResetPasswordSuccess = "Password is reset successfully";
            public const string UserDeleted = "User Deleted";
            public const string NotActiveUserDelete = "Cannot Delete Active User";
            public const string EnterCorrectPassword = "Please Enter Correct Old Password";
            public const string EnterCorrectUserName = "Please Enter Correct Username";
            public const string EnterCorrectConfirmationCode = "Please enter correct confirmation code";
            public const string ProfileUpdated = "Profile is updated";
            public const string UserSaved = "User Saved";
            public const string BranchLocationSaved = "Location Saved";
            public const string BranchLocationDeleted = "Location Deleted";
            public const string UserRoleDeleted = "User Role is Deleted";
            public const string RoleSaved = "Role Saved";
            public const string ChangedPassword = "Your Password has been changed successfully";
            public const string SoftwareDeleted = "Software package is Deleted";
            public const string FileUpload = "File(s) uploaded successfully!";
            public const string FileExists = "File exists already";
            public const string ProjectDeleted = "Project is Deleted";
            public const string AccountinYearDelete = "Accounting Year is Deleted";
            public const string DenyAccountingYearDelete = "Transaction is made. Can not be Deleted.";
            public const string DeleteActivePeriod = "Active period cannot be deleted";
            public const string ProjectSaved = "Project is Saved";
            public const string CloseDate = "Closed on should not be less than Started on date";
            public const string LockCloseDate = "Date To should not be less than Date From";
            public const string StartDate = "Started on should not be less than future date";
            public const string ProjectStartDate = "Started On is required";
            public const string ProjectCategoryDeleted = "Project Category is Deleted";
            public const string LockVoucherDeleted = "Lock Voucher is Deleted";
            public const string ProjectCategorySaved = "Project Category is Saved";
            public const string LegalEntityDeleted = "Legal Entity is Deleted";
            public const string LegalEntitySaved = "Legal Entity is Saved";
            public const string LedgerDeleted = "Ledger is Deleted";
            public const string LedgerSaved = "Ledger is Saved";
            public const string UserNotCommunicated = "User is not communicated";
            public const string HeadOfficeDBCreation = "Head Office Database Creation is Failed,Please Check the Database Connection";
            public const string HeadOfficeNotDeactivate = "Head Office cannot be deactivated";
            public const string SelectLedgerType = "Please Select the Ledger type";
            public const string LedgerGroupEdit = "Ledger Group (Edit)";
            public const string GeneralateLedgerEdit = "Generalate Ledger Group (Edit)";
            public const string LedgerGroupAdd = "Ledger Group (Add)";
            public const string GeneralateLedgerAdd = "Generalate Ledger Group (Add)";
            public const string LedgerGroupSaved = "Ledger Group is Saved";
            public const string GeneralateLedgerSaved = "Generalate Ledger Group is Saved";
            public const string LedgerGroupNextlevel = "Next group level can't be added";
            public const string LedgerGroupNotDelete = "Group cannot be deleted. It has association";
            public const string GeneralateLedgerNotDelete = "Generalate Ledger Group cannot be deleted. It has association";
            public const string LedgerGroupNatureNotDelete = "Fixed Nature cannot be deleted";
            public const string LedgerGroupDeleteSccessful = "Ledger Group is Deleted";
            public const string GeneralateLedgerDeleteSccessful = "Generalate Ledger Group is Deleted";
            public const string TicketPosted = "Ticket is Posted";
            public const string UserNameAvailable = "Username is Available";
            public const string BranchOfficeAvailable = "Branch Office is Available";
            public const string HeadOfficeAvailable = "Head Office is Available";
            public const string VoucherUploadXML = "Invalid Voucher File. Please Export Vouchers from Acme.erp";
            public const string VoucherUploadSuccess = "File uploaded successfully";
            public const string VoucherUploadFailed = "File Uploading is failed due to web service";
            public const string SaveConformation = "Saved Successfully";
            public const string ApproveConfirmation = "Approved Sucessfully";
            public const string ClearLog = "Log Cleared successfully";
            public const string NothingSelected = "Nothing is Selected";
            public const string FileUploadFiledEmpty = "Please Select a file to upload";
            public const string LicenseGenerated = "License key is generated successfully";
            public const string ProjectNotMapped = "Project(s) are not mapped to the selected branch";
            public const string Save_Failure = "Failed";
            public const string Mail_Succes = "Mail is sent ";
            public const string Mail_Failure = "Mail is not sent";
            public const string NoRecordToExport = "No Record to Export";
            public const string MailCommunicationSubBranch = "Mail Communication is not applicable to sub Branch";
            public const string LinkUrlCaptionAll = "Add";
            public const string LinkUrlComposeCaption = "Compose";
            public const string CreateBulkBranch = "Download Template for Branch Office to create bulk branches";
            public const string ImportMastersPageTitle = "Import Masters";
            public const string AcMEERPBackupTitle = "Acme.erp Backup";
            public const string AcMEERPBackupTitleold = "Acme.erp Backup (01-04-2015 To 31-03-2023)";
            public const string AcMEERPBranchReportTitle = "Acme.erp Branch Reports";
            public const string BranchLoggedHistoryTitle = "Branch Logged History";
            public const string BranchLocationLoggedHistoryTitle = "Branch Location Logged History";
            public const string UserRightsSaved = "User Rights saved";
            public const string FCPurposeSaved = "FC Purpose saved successfully";
            public const string SETTING_GLOBAL_SAVED = "Head Office Global Setting is Saved";


            public class Country
            {
                //Country add page
                public const string CountryAddPageTitle = "Country (Add)";
                public const string CountryEditPageTitle = "Country (Edit)";
                public const string CountryFieldEmpty = "Country is required";
                public const string CountryCodeFieldEmpty = "Country code is required";
                public const string CountrySymbolFieldEmpty = "Symbol is required";
                public const string CountrySymbolCodeFieldEmpty = "Country symbol code is required";
                public const string CountrySymbolNameFieldEmpty = "Country symbol name is required";
                //Country view page
                public const string CountryViewPageTitle = "Country";
                public const string AddCountryCaption = "Country";
                public const string CountryDeleteConformation = "Country Deleted";
            }
            public class Donor
            {
                //Donor add page
                public const string DonorAddPageTitle = "Donor (Add)";
                public const string DonorEditPageTitle = "Donor (Edit)";
                //Donor view page
                public const string DonorViewPageTitle = "Donor View";
                public const string AddDonorCaption = "Add Donor";
                public const string DonorDeleteConformation = "Donor Deleted";

            }
            public class LockVoucher
            {
                //Lock Voucher Add Page
                public const string LockAddPageTitle = "LockVoucher (Add)";
                public const string LockEditPageTitle = "LockVoucher (Edit)";

                //Lock Voucher View Page
                public const string LockViewPageTitle = "LockVoucher";
                public const string AddLockCaption = "Add LockVoucher";
                public const string LockDeleteConformation = "LockVoucher Deleted";
            }
            public class FCPurpose
            {
                //FCPurpose view page
                public const string FCPurposeViewPageTitle = "FC Purpose";
                public const string FCPurposeDeleteConformation = "FC Purpose Deleted";
                public const string FCPurposeEmpty = "FC Purpose is required";
                public const string FCPurposeCodeEmpty = "FC Purpose Code is required";
            }
            public class Ledger
            {
                //Ledger add page
                public const string LedgerAddPageTitle = "Ledger (Add)";
                public const string LedgerEditPageTitle = "Ledger (Edit)";
                public const string LedgerCodeFieldEmpty = "Ledger code is required";
                public const string LedgerNameFieldEmpty = "Ledger Name is required";
                public const string LedgerGroupFieldEmpty = "Ledger group is required";
                //Ledger view page
                public const string LedgerViewPageTitle = "Ledger";
                public const string LedgerAddCaption = "Add";
                public const string DenyLedgerDeletion = "Can not delete the ledger, Ledger has balance or Mapped with Project Category";

            }
            public class LedgerGroup
            {
                //LedgerGroup view page
                public const string LedgerGroupViewPageTitle = "Ledger Group";
            }

            public class GeneralateLedger
            {
                //LedgerGroup view page
                public const string GeneralateLedgerViewPageTitle = "Generalate Ledger Group";
                public const string GeneralateLedgerMappingTitle = "Generalate Ledger Group Mapping";
                public const string SelectedNone = "No ledger is selected to map";
                public const string NoGeneralateLedger = "No Generalate Ledger Group is selected to map with HeadOffice ledgers";
                public const string MappingTwoNaturesofLedgers = "Ledgers from two different natures can not be mapped to a Generalate ledger Group.";
                public const string MappingWhicharenotMappedAlready = "Select only the ledgers that are not mapped already.";
                public const string TwoNaturesToDifferentSubLedgers = "Ledgers of different natures can not be mapped to Sub-Ledgers of a Generalate Ledger Group.";

            }

            public class HeadOfficeMessage
            {
                public const string BranchEmptyEmail = "Select Branch(es) to send Mail";
                public const string BranchEmptyForBroadCast = "Select Branch(es) to send BroadCast Mail";
                public const string BroadCastSuccess = "Your message will be broadcasted to the Branch Admin";
                public const string MailSuccess = "Your message has been sent";
                public const string MessageTypeEmpty = "Select either Email or BroadCast";
                public const string ContentEmpty = "Message is empty";
                public const string SubjectEmpty = "Subject is empty";


            }
            public class LegalEntity
            {
                //LegalEntity add page
                public const string LegalEntityAddPageTitle = "Legal Entity (Add)";
                public const string LegalEntityEditPageTitle = "Legal Entity (Edit)";
                public const string InstituteFieldEmpty = "Institute Name is required";
                public const string SocietyNameFieldEmpty = "Society Name is required";
                public const string NotValidRegisteredDate = "Reg.Date should not be in Future";
                public const string NotValidPermissionDate = "Prior Permission Date should not be in Future";
                public const string NotValidAssociationNature = "Nature of the Association is required";
                public const string NotValidDenomination = "Denomination is required";
                public const string NotValidRegistrationNumber = "Society/Reg.No is required";
                public const string NotValidCountry = "Country is required";
                public const string NotValidState = "State/Province is required";

                //LegalEntity view page
                public const string LegalEntityViewPageTitle = "Legal Entity";
            }
            public class Project
            {
                //Project add page
                public const string ProjectAddPageTitle = "Project (Add)";
                public const string ProjectEditPageTitel = "Project (Edit)";
                public const string ProjectCodeFieldEmpty = "Project Code is required";
                public const string ProjectFiedlEmpty = "Project is required";
                public const string ProjectCategoryFieldEmpty = "Project Category is required";
                public const string ProjectDivisionFieldEmpty = "Division is required";
                //Project view page
                public const string ProjectViewPageTitle = "Project";
                public const string DenyProjectDeletion = "Project is running currently and Vouchers available.";
            }
            public class ProjectCategory
            {
                //ProjectCategory add page
                public const string ProjectCategoryAddPageTitle = "Project Category (Add)";
                public const string ProjectCategoryEditPageTitle = "Project Category (Edit)";
                public const string ProjectCategoryFieldEmpty = "Project Category is required";
                //Projecct Category view page
                public const string ProjectCategoryViewPageTitle = "Project Category";
            }
            public class Vouchers
            {
                public const string VoucherPageTitle = "Vouchers";
                public const string FDRegistersTitle = "FD Registers";
                public const string LedgerOpenBalanceTitle = "Ledger O/P Balance";
            }
            public class BranchOffice
            {
                //Branch office add page
                public const string BranchOfficeAddPageTitle = "Branch Office (Add)";
                public const string BranchOfficeEditPageTitle = "Branch Office (Edit)";
                //Branch office view page
                public const string BranchOfficeViewPageTitle = "Branch Office";
                public const string AccountinPeriodPageTitle = "A/C Period";
                public const string GenerateBranchOfficeKeyCaption = "Generate License Key";
                public const string DownloadMasterPageTitle = "Download Master";
                public const string DownloadKeyPageTitle = "Download Key";

            }
            public class BranchLocation
            {
                public const string BaranchLocationAddPageTitle = "Branch Location (Add)";
                public const string BaranchLocationEditPageTitle = "Branch Location (Edit)";
                public const string BranchLocationView = "Branch Location";
                public const string MessageView = "Message";
            }
            public class GoverningMember
            {
                //GoverningMember add page
                public const string GoverningMemberAddPageTitle = "Governing Member (Add)";
                public const string GoverningMemberEditPageTitle = "Governing Member (Edit)";
                //GoverningMember view page
                public const string GoverningMemberPageTitle = "Governing Member";
                public const string GoverningMemberDelete = "Governing Member is Deleted";
                public const string MapGoverningMember = "Mapping Governing Member";

            }

            public class GenerateLicenseKey
            {
                public const string GenerateLicenseKeyPageTitle = "License Key";

            }
            public class HeadOffice
            {
                //Head office add page
                public const string HeadOfficeAddPageTitle = "Head Office (Add)";
                public const string HeadOfficeEditPageTitle = "Head Office (Edit)";
                //Head office view page
                public const string HeadOfficeViewPageTitel = "Head Office";
            }
            public class ProjectMapping
            {
                public const string ProjectMappingPageTitle = "Project Mapping";
                public const string BranchMappingPageTitle = "Branch Mapping";
                public const string ProjectLocationMappingPageTitle = "Location Mapping";
                public const string ProjectMappingSaveConformation = "Saved Successfully";
                public const string BranchMappingSaveConformation = "Saved Successfully";
                public const string ProjectMappingSavingFailedConformation = "Saving Failed";
                public const string BranchMappingSavingFailedConformation = "Saving Failed";
            }
            public class UploadVoucher
            {
                public const string UploadVoucherPageTitle = "Upload Vouchers";
                public const string DownloadNoMasterDataProject = "Map Project(s) to the Branch Office";
                public const string DownloadNoMasterDataLedger = "Map Ledgers(s) to the Project Category in Head Office";
            }
            public class UploadDownload
            {
                public const string UploadDownloadAddPageTitle = "Software Upload";
                public const string UplaodDownloadViewPageTitle = "Software";
            }
            public class User
            {
                //Change password page
                public const string ChangePasswordPageTitle = "Change Password";
                //Forgot password page
                public const string ForgotPasswordPageTitle = "Forgot Password";
                //UserAddPage
                public const string UserAddPageTitle = "User (Add)";
                public const string UserEditPageTitle = "User (Edit)";
                //User rights page
                public const string UserRightsPageTitle = "User Rights";
                //User view page
                public const string UserViewPageTitle = "User";
                //User Rights Page
                public const string UserRightsMap = "Match rights with the role";
            }
            public class Role
            {
                //Role add page
                public const string RoleAddPageTitle = "User Role (Add)";
                public const string RoleEditPageTitle = "User Role (Edit)";
                public const string RoleNameFieldEmpty = "Role Name is required";
                //Role view page
                public const string RoleViewPageTitle = "User Role";
            }
            public class FileUploading
            {
                public const string SendMailSuccess = "Mail is sent";
            }

            public class Amendment
            {
                public const string AmendmentViewPageTitle = "Amendments";
                public const string AmendmentAddPageTitle = "Amendment";
                public const string UpdateStatusComformation = "Status updated successfully";
                public const string DescriptionRequired = "Description is required";

            }

            public class LedgerMapping
            {
                public const string LedgerMappingPageTitle = "Ledger Mapping";
                public const string MapSubsidyLedger = "Subsidy & Contribution Ledger Mapping";
                public const string ProjectCategoryDelete = " ,Ledgers are mapped to the category, unmap the ledgers to delete category";
                public const string DenyLedgerMapping = "The project category is not mapped to any project(s), So cannot map the ledgers to the project category";
                public const string NoSelection = "Please select a ledger to be mapped with the selected project category";

            }
            public class WebServiceMessage
            {
                public const string LedgerMismatched = "Ledger(s) is mismatched";
                public const string ProjectMismatched = "Project(s) is mismatched";
                public const string ProjectNotAvailable = "Project(s) is not available in the Head office";
                public const string LedgerNotAvailable = "Ledger(s) is not available in the Head office";
                public const string InvalidBranchOfficeCredentials = "Invalid Branch Office Credentials or (User or Branch Office) is not active";
                public const string LicenseNotAvailable = "License is not available for your Branch";
                public const string BranchProjectNotAvailable = "Project(s) are not sent from Branch Office";
                public const string BranchLedgerNotAvailable = "Ledger(s) are not sent from Branch Office";
                public const string BranchNotAvailable = "Branch Office is not available";
                public const string BranchLocationNotAvailable = "Branch Office Location is not available";
                public const string UploadVoucherRequired = "UploadVoucher method requires byte data";
                public const string InvalidVouchers = "Invalid voucher file, please upload valid voucher file";
                public const string AmendmentNotesNotAvailable = "Amendment Notes are not available";
                public const string AmendmentNotesRequired = "GetVoucherAmendments requires valid Head Office and Branch Office Code";
                public const string NotSentBranchOfficeProjects = "Please send Branch Office Project(s) for verification";
                public const string NotSentBranchOfficeLedgers = "Please send Branch Office Ledger(s) for verification";
                public const string NotSentDataSyncUpdateStatus = "Please send Head Office, Branch Office Code and Filename to update voucher file status";
            }

            public class TroubleTicket
            {
                public const string AddPageTitle = "TroubleTicketAdd";
                public const string EditPageTitel = "Modify Ticket";
                public const string TroubleTicketEdit = "TroubleTicketEdit";
                public const string TroubleTicketSave = "TroubleTicketSave";

                public const string ReplyPageTitle = "Reply Ticket";
                public const string ReplyPrefix = "Re:";

                public const string TroubleTicketPageTitle = "Tickets";
                public const string PostTicket = "Post Ticket";

                public const string MailUser = "Admin";
                public const string MailHeader = "Tickets posted from";
                public const string MailSubject = "Trouble Tickets";

                public const string ReplyMailHeader = "You have got reply for your ticket from";
                public const string SubjectisEmpty = "Subject is required";
                public const string DescriptionisEmpty = "Description is required";

            }

            public class TDS
            {
                public const string StatutoryCompliance = "Statutory Compliance";
                public const string DeducteeTypePageTitle = "Deductee Type";
                public const string TDSSectionPageTitle = "TDS Section";
                public const string TDSNatureOfPayment = "Nature of Payments";
                public const string DutyTaxPageTitle = "Duty Tax";
            }
            public class Asset
            {
                public const string ImportAssetMastersPageTitle = "Import Asset Masters";
                public const string AssetClassViewPageTitle = "Asset Class";
                public const string AssetClassAdd = "Asset Class (Add)";
                public const string AssetClassEdit = "Asset Class (Edit)";
                public const string SelectParentClass = "Please Select the Parent Class";
                public const string AssetClassNotDelete = "Class cannot be deleted. It has association";
                public const string AssetClassDeleteSccessful = "Asset Class is Deleted";
                public const string ParentClassisrequired = "Parent Class is required";
                public const string ClassNameisrequired = "Class Name is required";
                public const string AssetClassSaved = "Asset Class is saved";

                public const string AssetItemViewPageTitle = "Asset Item";
                public const string AssetItemAdd = "Asset Item (Add)";
                public const string AssetItemEdit = "Asset Item (Edit)";
                public const string AssetItemSaved = "Asset Item is saved";
                public const string AssetItemDeleted = "Asset Item is Deleted";
                public const string AssetClassRequired = "Asset Class is required";
                public const string AssetItemRequired = "Asset Item is required";
                public const string AssetIDGenerationRequired = "Asset ID Generation is required";
                public const string StratingNoRequired = "Starting No is required";
                public const string PrefixRequired = "Prefix is required";
                public const string RetentionYearRequired = "Retention Yrs is required";
                public const string DepYearRequired = "Depreciation Yrs is required";


                public const string AssetUnitofMeasureViewPageTitle = "Unit of Measure";
                public const string AssetUnitofMeasureAdd = "Unit of Measure (Add)";
                public const string AssetUnitofMeasureEdit = "Unit of Measure (Edit)";
                public const string AssetUnitofMeasureEmpty = "UoM is required";
                public const string AssetUoMSaved = "Unit of Measure is saved";
                public const string AssetUOMDeleted = "Unit of Measure is Deleted";
            }

            public class LCEnableTrackModules
            {
                public const string LCEnableTrackModulesPageTitle = "Disable/Approve Branch Receipt Module";
                public const string LCEnableTrackModulesUpdateConformation = "Updated Successfully Local Branch Requests to enable/disable Receipt Module";
                public const string LCEnableTrackModulesUpdateFailedConformation = "Not Updated Local Branch Requests to enable/disable Receipt Module";
                public const string LCEnableTrackModulesSucessfullyDeletedAll = "Deleted all the Local Branch Requests";
                public const string LCEnableTrackModulesFailedDeletedAll = "Failed to delete all the Local Branch Requests";
                public const string LCEnableTrackModulesSucessfullyDeletedSelected = "Deleted selected Local Branch Requests";
                public const string LCEnableTrackModulesFailedDeletedSelected = "Failed to delete selected Local Branch Requests";
            }

        }

        #region Settings
        public class Settings
        {
            public const string SETTING_GLOBAL_Title = "Head Office Global Setting";

            public const string SETTING_SUCCESS = "SETTING_SUCCESS";
            public const string SETTING_FAILURE = "SETTING_FAILURE";
            public const string SETTING_LANGUAGE_INVALID = "SETTING_LANGUAGE_INVALID";
            public const string SETTING_DATE_FORMAT_INVALID = "SETTING__DATE_FORMAT_INVALID";
            public const string SETTING_DATE_SEPARATOR_INVALID = "SETTING_DATE_SEPARATOR_INVALID";
            public const string SETTING_CURRENCY_INVALID = "SETTING_CURRENCY_INVALID";
            public const string SETTING_CURRENCY_CODE_INVALID = "SETTING_CURRENCY_CODE_INVALID";
            public const string SETTING_CURRENCY__POSITION_INVALID = "SETTING_CURRENCY__POSITION_INVALID";
            public const string SETTING_DIGIT_GROUPING_INVALID = "SETTING_DIGIT_GROUPING_INVALID";
            public const string SETTING_DIGIT_GROUPING_SEPARATOR_INVALID = "SETTING_DIGIT_GROUPING_SEPARATOR_INVALID";
            public const string SETTING_DECIMAL_PLACES_INVALID = "SETTING_DECIMAL_PLACES_INVALID";
            public const string SETTING_DECIMAL_PLACES_SEPARATOR_INVALID = "SETTING_DECIMAL_PLACES_SEPARATOR_INVALID";
            public const string SETTING_NEGATIVE_SIGN_INVALID = "SETTING_NEGATIVE_SIGN_INVALID";
            public const string SETTING_DEFAULT_LANGUAGE_SET = "SETTING_DEFAULT_LANGUAGE_SET";
            public const string SETTING_TRANSACTION_FAILURE = "SETTING_TRANSACTION_FAILURE";
            public const string SETTING_BOOK_BEGINNING_EMPTY = "SETTING_BOOK_BEGINNING_EMPTY";
            public const string SETTING_APPLICATION_RESTART_CONFIRMATION = "SETTING_APPLICATION_RESTART_CONFIRMATION";
            public const string SETTING_RESTART_PREVIEW = "SETTING_RESTART_PREVIEW";
            public const string SETTING_FOREIGN_BANKACCOUNT = "SETTING_FOREIGN_BANKACCOUNT";
        }


        #endregion

        #region Transaction
        public class Transaction
        {
            public class VocherTransaction
            {
                public const string VOUCHER_ADD_CAPTION = "VOUCHER_ADD_CAPTION";
                public const string VOUCHER_EDIT_CAPTION = "VOUCHER_EDIT_CAPTION";
                public const string VOUCHER_VALID_AMOUNT = "VOUCHER_VALID_AMOUNT";
                public const string VOUCHER_MULTY_ENTRY_CHECK = "VOUCHER_MULTY_ENTRY_CHECK";
                public const string VOUCHER_MULTY_RECEIPT_CONFIRM = "VOUCHER_MULTY_RECEIPT_CONFIRM";
                public const string VOUCHER_MULTY_PAYMENT_CONFIRM = "VOUCHER_MULTY_PAYMENT_CONFIRM";
                public const string VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string VOUCHER_MULTY_INVALID_TRANSACTION_AMOUNT = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string VOUCHER_MULTY_INVALID_CASH_TRANSACTION_LEDGER = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string VOUCHER_MULTY_INVALID_CASH_TRANSACTION_AMOUNT = "VOUCHER_MULTY_INVALID_TRANSACTION_LEDGER";
                public const string TRANS_DATE = "TRANS_DATE";
                public const string BRS_MATERIALIZED_DATE = "BRS_MATERIALIZED_DATE";
                public const string VOUCHER_SAVE = "VOUCHER_SAVE";
                public const string VOUCHER_AMOUNT_LESS_THAN_ZERO = "VOUCHER_AMOUNT_LESS_THAN_ZERO";
                public const string VOUCHER_TRANSACTION_CHANGE_TYPE = "VOUCHER_TRANSACTION_CHANGE_TYPE";
                public const string VOUCHER_TRANSACTION_CONTRA_CHANGE_TYPE = "VOUCHER_TRANSACTION_CONTRA_CHANGE_TYPE";
                public const string VOUCHER_TRANSACTION_DATE = "VOUCHER_TRANSACTION_DATE";
                public const string VOUCHER_NUMBER_EMPTY = "VOUCHER_NUMBER_EMPTY";
                public const string VOUCHER_AMOUNT_MISMATCH = "VOUCHER_AMOUNT_MISMATCH";
                public const string VOUCHER_NEGATIVE_BALANCE_CASHBANK = "VOUCHER_NEGATIVE_BALANCE_CASHBANK";
                public const string VOUCHER_NEGATIVE_BALANCE_CASH = "VOUCHER_NEGATIVE_BALANCE_CASH";
                public const string VOUCHER_NEGATIVE_BALANCE_BANK = "VOUCHER_NEGATIVE_BALANCE_BANK";
                public const string DONOR_CONTRIBUTION_AMOUNT_EMPTY = "DONOR_CONTRIBUTION_AMOUNT_EMPTY";
                public const string DONOR_CONTRIBUTION_ACTUAL_AMOUNT_EMPTY = "DONOR_CONTRIBUTION_ACTUAL_AMOUNT_EMPTY";
                public const string TRANSACTION_AMOUNT_NOT_EQUAL_ACTUAL_AMOUNT = "TRANSACTION_AMOUNT_NOT_EQUAL_ACTUAL_AMOUNT";
                public const string VOUCHER_NO_RECORD = "VOUCHER_NO_RECORD";
                public const string JOURNAL_ADD_CAPTION = "JOURNAL_ADD_CAPTION";
                public const string JOURNAL_EDIT_CAPTION = "JOURNAL_EDIT_CAPTION";
                public const string VOUCHER_TRANSACTION_RECEIPT = "VOUCHER_TRANSACTION_RECEIPT";
                public const string VOUCHER_TRANSACTION_PAYMENT = "VOUCHER_TRANSACTION_PAYMENT";
                public const string VOUCHER_TRANSACTION_CONTRA = "VOUCHER_TRANSACTION_CONTRA";
                public const string VOUCHER_FD_REALIZE_CONFIRMATION = "VOUCHER_FD_REALIZE_CONFIRMATION";
                public const string VOUCHER_CASHBANK_MAPPING_TO_PROJECT = "VOUCHER_CASHBANK_MAPPING_TO_PROJECT";
                public const string VOUCHER_LEDGER_MAPPING_TO_PROJECT = "VOUCHER_LEDGER_MAPPING_TO_PROJECT";
                public const string SAVED_PRINT_VOUCHER = "SAVED_PRINT_VOUCHER";
                public const string CONFIRM_PRINT_VOUCHER = "CONFIRM_PRINT_VOUCHER";

            }

            public class VoucherCostCentre
            {
                public const string VOUCHER_COST_CENTRE_NAME_EXIST = "VOUCHER_COST_CENTRE_NAME_EXIST";
                public const string VOUCHER_COST_CENTRE_NAME_EMPTY = "VOUCHER_COST_CENTRE_NAME_EMPTY";
                public const string VOUCHER_VALID_AMOUNT = "VOUCHER_VALID_AMOUNT";
                public const string VOUCHER_COST_CENTRE_SAVED_SUCCESSFULLY = "VOUCHER_COST_CENTRE_SAVED_SUCCESSFULLY";
                public const string VOUCHER_COST_CENTRE_DELETED_SUCCESSFULLY = "VOUCHER_COST_CENTRE_DELETED_SUCCESSFULLY";
                public const string VOUCHER_COST_CENTRE_AMOUNT_EMPTY = "VOUCHER_COST_CENTRE_AMOUNT_EMPTY";
                public const string VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_GREATER = "VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_GREATER";
                public const string VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_LESS = "VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_LESS";
                public const string ALLOCATION_AMOUNT_IS_NOT_EQUAL = "ALLOCATION_AMOUNT_IS_NOT_EQUAL";
            }
        }
        #endregion

        #region Report
        public class ReportMessage
        {
            //public const string DATEFROMEMPTY = "DATEFROMEMPTY";
            //public const string DATE_VALIDATION = "DATE_VALIDATION";
            //public const string REPORT_PROJECT_EMPTY = "REPORT_PROJECT_EMPTY";
            //public const string REPORT_BANK_EMPTY = "REPORT_BANK_EMPTY";
            //public const string REPORT_COSTCENTRE_EMPTY = "REPORT_COSTCENTRE_EMPTY";
            //public const string REPORT_LEDGER_EMPTY = "REPORT_LEDGER_EMPTY";
            public const string REPORT_COSTCENTRE_EMPTY = "A Cost Center must be selected!";
            public const string REPORT_LEDGERGROUP_EMPTY = "A Ledger Group must be selected!";
            public const string REPORT_LEDGER_EMPTY = "A Ledger must be selected!";
            public const string REPORT_BANK_EMPTY = "A Bank Account must be selected!";
            public const string REPORT_PROJECT_EMPTY = "A Project must be selected!";
            public const string BRANCH_ID_EMPTY = "A Branch must be selected!";
            public const string SUBSIDY_LEDGER_EMPTY = "A Subsidy ledger must be selected!";
            public const string CONTRIBUTION_LEDGER_EMPTY = "A Contribution ledger must be selected!";
            public const string SUBSIDY_CONTRIBUTION_LEDGER_EMPTY = "Subsidy and Contribution ledger must be selected!";
        }
        public class ReportCommonTitle
        {
            public const string AMOUNT = "Amount";
            public const string DEBIT = "Debit";
            public const string CREDIT = "Credit";
            public const string OPBALANCE = "O/p Balance";
            public const string INCOME = "Income";
            public const string EXPENDITURE = "Expenditure";
            public const string PERIOD = "For the Period:";
            public const string DR = "DR";
            public const string CR = "CR";
            public const string INFLOW = "In Flow";
            public const string OUTFLOW = "Out Flow";
            public const string BALANCE = "Balance";
            public const string RECEIPT = "Receipts";
            public const string PAYMENTS = "Payments";
            public const string UNREALIZED = "UnRealized";
            public const string UNCLEARED = "UnCleared";
            public const string CASH = "Cash";
            public const string BANK = "Bank";
            public const string CLOSINGBALANCE = "Closing Balance";
            public const string FORTHEPERIOD = "For the Period";
            public const string ASON = "As on";
            public const string PROGRESSIVETOTAL = "Progressive Total";
            public const string PREVIOUS = "Previous";
            public const string AMOUNTIN = "Amount in";
            public const string FDAMOUNT = "FD Amount";

        }
        #endregion

        #region MobileService
        public class MobileService
        {
            public const string DBCONNECTION = "Failed to connect acme.erp database";
            public const string SuccessStatus = "true";
            public const string FalseStatus = "false";
            public const string SuccessErrorCode = "200";
            public const string NotFoundErrorCode = "404";
            public const string ResultsNotFound = "No results found";
            public const string InvalidLoginErrorCode = "504";
            public const string InvalidLogin = "Invalid Login Credentials";
            public const string FailedMail = "Fail in sending mail";

            public const string EmptyErrorCode = "1";
            public const string MobileRequestEmpty = "MobileRequest is empty";
            public const string HeadOfficeCodeEmpty = "HeadOfficeCode is empty";
            public const string BranchOfficeCodeEmpty = "BranchOfficeCode is empty";
            public const string UserNameEmpty = "Username is empty";
            public const string PasswordEmpty = "Password is empty";
            public const string ProjectNameEmpty = "ProjectName is empty";
            public const string LedgerNameEmpty = "LedgerName is empty";
            public const string DateFromEmpty = "DateFrom is empty";
            public const string DateToEmpty = "DateTo is empty";
            public const string DateAsOnEmpty = "DateAsOn is empty";
            public const string MailIdsEmpty = "MailIds is empty";
            public const string ContentEmpty = "Content is empty";
            public const string SubjectEmpty = "Subject is empty";
        }
        #endregion
    }
}

