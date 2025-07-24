using System;
using System.Collections.Generic;
using System.Text;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;

namespace Bosco.DAO.Schema
{
    public class AppSchemaSet : CommonMember
    {
        private ApplicationSchemaSet appSchemaSet = null;

        public ApplicationSchemaSet AppSchema
        {
            get
            {
                if (appSchemaSet == null)
                {
                    appSchemaSet = new ApplicationSchemaSet();
                }
                return appSchemaSet;
            }
        }

        public class ApplicationSchemaSet
        {
            private EnumTypeSchema.EnumTypeDataTable enumSchema = null;
            public EnumTypeSchema.EnumTypeDataTable EnumSchema
            {
                get
                {
                    if (enumSchema == null)
                    {
                        enumSchema = new EnumTypeSchema.EnumTypeDataTable();
                    }
                    return enumSchema;
                }
            }

            private ApplicationSchema.UserDataTable userSchema = null;
            public ApplicationSchema.UserDataTable User
            {
                get
                {
                    if (userSchema == null)
                    {
                        userSchema = new ApplicationSchema.UserDataTable();
                    }
                    return userSchema;
                }
            }

            private ApplicationSchema.LedgerGroupDataTable ledgerGroupSchema = null;
            public ApplicationSchema.LedgerGroupDataTable LedgerGroup
            {
                get
                {
                    if (ledgerGroupSchema == null)
                    {
                        ledgerGroupSchema = new ApplicationSchema.LedgerGroupDataTable();
                    }
                    return ledgerGroupSchema;
                }
            }

            private ApplicationSchema.CountryDataTable CountrySchema = null;
            public ApplicationSchema.CountryDataTable Country
            {
                get
                {
                    if (CountrySchema == null)
                    {
                        CountrySchema = new ApplicationSchema.CountryDataTable();
                    }
                    return CountrySchema;
                }
            }

            private ApplicationSchema.BankDataTable bankSchema = null;
            public ApplicationSchema.BankDataTable Bank
            {
                get
                {
                    if (bankSchema == null)
                    {
                        bankSchema = new ApplicationSchema.BankDataTable();
                    }
                    return bankSchema;
                }

            }

            private ApplicationSchema.CostCentreDataTable costCentreSchema = null;
            public ApplicationSchema.CostCentreDataTable CostCentre
            {
                get
                {
                    if (costCentreSchema == null) { costCentreSchema = new ApplicationSchema.CostCentreDataTable(); }
                    return costCentreSchema;
                }
            }

            private ApplicationSchema.DonorAuditorDataTable donorAuditorSchema = null;
            public ApplicationSchema.DonorAuditorDataTable DonorAuditor
            {
                get
                {
                    if (donorAuditorSchema == null) { donorAuditorSchema = new ApplicationSchema.DonorAuditorDataTable(); }
                    return donorAuditorSchema;
                }
            }


            private ApplicationSchema.InKindArticleDataTable inKindArticle = null;
            public ApplicationSchema.InKindArticleDataTable InKindArticle
            {
                get
                {
                    if (inKindArticle == null) { inKindArticle = new ApplicationSchema.InKindArticleDataTable(); }
                    return inKindArticle;
                }
            }
            private ApplicationSchema.ExecutiveMemberDataTable executiveMembers = null;
            public ApplicationSchema.ExecutiveMemberDataTable ExecutiveMembers
            {
                get
                {
                    if (executiveMembers == null) { executiveMembers = new ApplicationSchema.ExecutiveMemberDataTable(); }
                    return executiveMembers;
                }
            }
            private ApplicationSchema.CurrencySymbolsDataTable currencySymbols = null;
            public ApplicationSchema.CurrencySymbolsDataTable CurrencySymbols
            {
                get
                {
                    if (currencySymbols == null) { currencySymbols = new ApplicationSchema.CurrencySymbolsDataTable(); }
                    return currencySymbols;
                }
            }
            private ApplicationSchema.CurrencyCodeDataTable currencyCode = null;
            public ApplicationSchema.CurrencyCodeDataTable CurrencyCode
            {
                get
                {
                    if (currencyCode == null) { currencyCode = new ApplicationSchema.CurrencyCodeDataTable(); }
                    return currencyCode;
                }
            }
            private ApplicationSchema.BankAccountDataTable BankAccountShema = null;
            public ApplicationSchema.BankAccountDataTable BankAccount
            {
                get
                {
                    if (BankAccountShema == null) { BankAccountShema = new ApplicationSchema.BankAccountDataTable(); }
                    return BankAccountShema;
                }
            }

            private ApplicationSchema.BudgetDataTable BudgetSchema = null;
            public ApplicationSchema.BudgetDataTable Budget
            {
                get
                {
                    if (BudgetSchema == null) { BudgetSchema = new ApplicationSchema.BudgetDataTable(); }
                    return BudgetSchema;
                }
            }

            private ApplicationSchema.AllotFundDataTable AllotingFund = null;
            public ApplicationSchema.AllotFundDataTable AllotFund
            {
                get
                {
                    if (AllotingFund == null) { AllotingFund = new ApplicationSchema.AllotFundDataTable(); }
                    return AllotingFund;
                }
            }

            private ApplicationSchema.LedgerDataTable LedgerShema = null;
            public ApplicationSchema.LedgerDataTable Ledger
            {
                get
                {
                    if (LedgerShema == null) { LedgerShema = new ApplicationSchema.LedgerDataTable(); }
                    return LedgerShema;
                }
            }
            private ApplicationSchema.VoucherDataTable VoucherSchema = null;
            public ApplicationSchema.VoucherDataTable Voucher
            {
                get
                {
                    if (VoucherSchema == null) VoucherSchema = new ApplicationSchema.VoucherDataTable();
                    return VoucherSchema;
                }

            }
            private ApplicationSchema.LegalEntityDataTable LegalEntitySchema = null;
            public ApplicationSchema.LegalEntityDataTable LegalEntity
            {
                get
                {
                    if (LegalEntitySchema == null) LegalEntitySchema = new ApplicationSchema.LegalEntityDataTable();
                    return LegalEntitySchema;
                }

            }


            private ApplicationSchema.ProjectDataTable ProjectSchema = null;
            public ApplicationSchema.ProjectDataTable Project
            {
                get
                {
                    if (ProjectSchema == null) { ProjectSchema = new ApplicationSchema.ProjectDataTable(); }
                    return ProjectSchema;
                }
            }
            private ApplicationSchema.PurposesDataTable PurposeSchema = null;
            public ApplicationSchema.PurposesDataTable Purposes
            {
                get
                {
                    if (PurposeSchema == null) { PurposeSchema = new ApplicationSchema.PurposesDataTable(); }
                    return PurposeSchema;
                }
            }

            private ApplicationSchema.Project_VoucherDataTable ProjectVoucherSchema = null;
            public ApplicationSchema.Project_VoucherDataTable ProjectVoucher
            {
                get
                {
                    if (ProjectVoucherSchema == null) { ProjectVoucherSchema = new ApplicationSchema.Project_VoucherDataTable(); }
                    return ProjectVoucherSchema;
                }
            }

            private ApplicationSchema.SettingDataTable SettingSchema = null;
            public ApplicationSchema.SettingDataTable Settings
            {
                get
                {
                    if (SettingSchema == null) { SettingSchema = new ApplicationSchema.SettingDataTable(); }
                    return SettingSchema;
                }
            }

            private ApplicationSchema.CultureDataTable CultureSchema = null;
            public ApplicationSchema.CultureDataTable Culture
            {
                get
                {
                    if (CultureSchema == null) { CultureSchema = new ApplicationSchema.CultureDataTable(); }
                    return CultureSchema;
                }
            }

            private ApplicationSchema.dtSettingDataTable dtSetting = null;
            public ApplicationSchema.dtSettingDataTable Setting
            {
                get
                {
                    if (dtSetting == null) { dtSetting = new ApplicationSchema.dtSettingDataTable(); }
                    return dtSetting;
                }
            }

            private ApplicationSchema.DivisionDataTable DivisionSchema = null;
            public ApplicationSchema.DivisionDataTable Division
            {
                get
                {
                    if (DivisionSchema == null) { DivisionSchema = new ApplicationSchema.DivisionDataTable(); }
                    return DivisionSchema;
                }
            }
            private ApplicationSchema.ACCOUNT_TYPEDataTable AccountTypeSchema = null;
            public ApplicationSchema.ACCOUNT_TYPEDataTable AccountType
            {
                get
                {
                    if (AccountTypeSchema == null) { AccountTypeSchema = new ApplicationSchema.ACCOUNT_TYPEDataTable(); }
                    return AccountTypeSchema;
                }
            }
            private ApplicationSchema.Audit_InfoDataTable AuditInfoSchema = null;
            public ApplicationSchema.Audit_InfoDataTable AuditInfo
            {
                get
                {
                    if (AuditInfoSchema == null) { AuditInfoSchema = new ApplicationSchema.Audit_InfoDataTable(); }
                    return AuditInfoSchema;
                }
            }

            private ApplicationSchema.UserRightsDataTable AcmeUserRights = null;
            public ApplicationSchema.UserRightsDataTable UserRights
            {
                get
                {
                    if (AcmeUserRights == null) { AcmeUserRights = new ApplicationSchema.UserRightsDataTable(); }
                    return AcmeUserRights;
                }
            }

            private ApplicationSchema.RightsDataTable Rights = null;
            public ApplicationSchema.RightsDataTable rights
            {
                get
                {
                    if (Rights == null) { Rights = new ApplicationSchema.RightsDataTable(); }
                    return Rights;
                }
            }

            private ApplicationSchema.AddressBookDataTable AddressBookSchema;
            public ApplicationSchema.AddressBookDataTable AddressBook
            {
                get
                {
                    if (AddressBookSchema == null) { AddressBookSchema = new ApplicationSchema.AddressBookDataTable(); }
                    return AddressBookSchema;
                }
            }

            private ApplicationSchema.ProjectCatogoryDataTable _ProjectCatogory;
            public ApplicationSchema.ProjectCatogoryDataTable ProjectCatogory
            {
                get
                {
                    if (_ProjectCatogory == null) { _ProjectCatogory = new ApplicationSchema.ProjectCatogoryDataTable(); }
                    return _ProjectCatogory;
                }
            }

            private ApplicationSchema.ProjectCatogoryGroupDataTable projectcatogorygroup;
            public ApplicationSchema.ProjectCatogoryGroupDataTable ProjectCatogoryGroup
            {
                get
                {
                    if (projectcatogorygroup == null) { projectcatogorygroup = new ApplicationSchema.ProjectCatogoryGroupDataTable(); }
                    return projectcatogorygroup;
                }
            }

            private ApplicationSchema.ProjectCatogoryITRGroupDataTable projectcatogoryitrgroup;
            public ApplicationSchema.ProjectCatogoryITRGroupDataTable ProjectCatogoryITRGroup
            {
                get
                {
                    if (projectcatogoryitrgroup == null) { projectcatogoryitrgroup = new ApplicationSchema.ProjectCatogoryITRGroupDataTable(); }
                    return projectcatogoryitrgroup;
                }
            }

            private ApplicationSchema.UserRoleDataTable userRole;
            public ApplicationSchema.UserRoleDataTable UserRole
            {
                get
                {
                    if (userRole == null) { userRole = new ApplicationSchema.UserRoleDataTable(); }
                    return userRole;
                }
            }


            private ApplicationSchema.ModuleDataTable module;
            public ApplicationSchema.ModuleDataTable Module
            {
                get
                {
                    if (module == null) { module = new ApplicationSchema.ModuleDataTable(); }
                    return module;
                }
            }

            private ApplicationSchema.MasterRightsDataTable masterRightsTable;
            public ApplicationSchema.MasterRightsDataTable MasterRights
            {
                get
                {
                    if (masterRightsTable == null) { masterRightsTable = new ApplicationSchema.MasterRightsDataTable(); }
                    return masterRightsTable;
                }
            }

            private ApplicationSchema.VoucherMasterDataTable voucherMasterTable;
            public ApplicationSchema.VoucherMasterDataTable VoucherMaster
            {
                get
                {
                    if (voucherMasterTable == null) { voucherMasterTable = new ApplicationSchema.VoucherMasterDataTable(); }
                    return voucherMasterTable;
                }
            }

            private ApplicationSchema.VoucherTransactionDataTable voucherTransactionTable;
            public ApplicationSchema.VoucherTransactionDataTable VoucherTransaction
            {
                get
                {
                    if (voucherTransactionTable == null) { voucherTransactionTable = new ApplicationSchema.VoucherTransactionDataTable(); }
                    return voucherTransactionTable;
                }
            }
            private ApplicationSchema.LedgerBalanceDataTable ledgerBalanceTable;
            public ApplicationSchema.LedgerBalanceDataTable LedgerBalance
            {
                get
                {
                    if (ledgerBalanceTable == null) { ledgerBalanceTable = new ApplicationSchema.LedgerBalanceDataTable(); }
                    return ledgerBalanceTable;
                }
            }

            private ApplicationSchema.VoucherCostCentreDataTable Voucher_Cost_Centre;
            public ApplicationSchema.VoucherCostCentreDataTable VoucherCostCentre
            {
                get
                {
                    if (Voucher_Cost_Centre == null) { Voucher_Cost_Centre = new ApplicationSchema.VoucherCostCentreDataTable(); }
                    return Voucher_Cost_Centre;
                }
            }
            private ApplicationSchema.BreakUpDataTable _BreakUp;
            public ApplicationSchema.BreakUpDataTable BreakUp
            {

                get
                {
                    if (_BreakUp == null) { _BreakUp = new ApplicationSchema.BreakUpDataTable(); }
                    return _BreakUp;
                }
            }

            private ApplicationSchema.VoucherNumberDataTable voucherNumber;
            public ApplicationSchema.VoucherNumberDataTable VoucherNumber
            {

                get
                {
                    if (voucherNumber == null) { voucherNumber = new ApplicationSchema.VoucherNumberDataTable(); }
                    return voucherNumber;
                }
            }

            private ApplicationSchema.AccountingYearDataTable accountingPeriod;
            public ApplicationSchema.AccountingYearDataTable AccountingPeriod
            {

                get
                {
                    if (accountingPeriod == null) { accountingPeriod = new ApplicationSchema.AccountingYearDataTable(); }
                    return accountingPeriod;
                }
            }

            private ApplicationSchema.FDRegistersDataTable fdRegisters;
            public ApplicationSchema.FDRegistersDataTable FDRegisters
            {
                get
                {
                    if (fdRegisters == null)
                    {
                        fdRegisters = new ApplicationSchema.FDRegistersDataTable();
                    }
                    return fdRegisters;
                }
            }

            private ApplicationSchema.InKindTransactionDataTable inKindTrans;
            public ApplicationSchema.InKindTransactionDataTable InKindTrans
            {
                get
                {
                    if (inKindTrans == null)
                    {
                        inKindTrans = new ApplicationSchema.InKindTransactionDataTable();
                    }
                    return inKindTrans;
                }
            }

            private ApplicationSchema.VoucherFDInterestDataTable voucherFDInterest;
            public ApplicationSchema.VoucherFDInterestDataTable VoucherFDInterest
            {
                get
                {
                    if (voucherFDInterest == null)
                    {
                        voucherFDInterest = new ApplicationSchema.VoucherFDInterestDataTable();
                    }
                    return voucherFDInterest;
                }
            }

            private ApplicationSchema.FDAccountDataTable fdAccount;
            public ApplicationSchema.FDAccountDataTable FDAccount
            {
                get
                {
                    if (fdAccount == null)
                    {
                        fdAccount = new ApplicationSchema.FDAccountDataTable();
                    }
                    return fdAccount;
                }
            }

            private ApplicationSchema.FDRenewalDataTable fdRenewal;
            public ApplicationSchema.FDRenewalDataTable FDRenewal
            {
                get
                {
                    if (fdRenewal == null)
                    {
                        fdRenewal = new ApplicationSchema.FDRenewalDataTable();
                    }
                    return fdRenewal;
                }
            }

            private ApplicationSchema.HeadOfficeDataTable headOffice;
            public ApplicationSchema.HeadOfficeDataTable HeadOffice
            {
                get
                {
                    if (headOffice == null) { headOffice = new ApplicationSchema.HeadOfficeDataTable(); }
                    return headOffice;
                }
            }

            private ApplicationSchema.BranchOfficeDataTable branchOffice;
            public ApplicationSchema.BranchOfficeDataTable BranchOffice
            {
                get
                {
                    if (branchOffice == null) { branchOffice = new ApplicationSchema.BranchOfficeDataTable(); }
                    return branchOffice;
                }
            }

            private ApplicationSchema.BranchLocationDataTable branchLocation;
            public ApplicationSchema.BranchLocationDataTable BranchLocation
            {
                get
                {
                    if (branchLocation == null) { branchLocation = new ApplicationSchema.BranchLocationDataTable(); }
                    return branchLocation;
                }
            }

            /// <summary>
            /// This is for software updates
            /// </summary>

            private ApplicationSchema.SoftwareDataTable software;
            public ApplicationSchema.SoftwareDataTable Software
            {
                get
                {
                    if (software == null) { software = new ApplicationSchema.SoftwareDataTable(); }
                    return software;
                }
            }

            private ApplicationSchema.STATEDataTable state;
            public ApplicationSchema.STATEDataTable State
            {
                get
                {
                    if (state == null) { state = new ApplicationSchema.STATEDataTable(); }
                    return state;
                }
            }

            private ApplicationSchema.HeadOfficeTypeDataTable hoType;
            public ApplicationSchema.HeadOfficeTypeDataTable HOType
            {
                get
                {
                    if (hoType == null) { hoType = new ApplicationSchema.HeadOfficeTypeDataTable(); }
                    return hoType;
                }
            }
            private ApplicationSchema.SocietyNamesDataTable society;
            public ApplicationSchema.SocietyNamesDataTable SOCIETY
            {
                get
                {
                    if (society == null) { society = new ApplicationSchema.SocietyNamesDataTable(); }
                    return society;
                }
            }
            private ApplicationSchema.TroubleTicketDataTable troubleTicket;
            public ApplicationSchema.TroubleTicketDataTable TROUBLETICKET
            {
                get
                {
                    if (troubleTicket == null)
                    {
                        troubleTicket = new ApplicationSchema.TroubleTicketDataTable();
                    }
                    return troubleTicket;
                }
            }
            private ApplicationSchema.Datasync_TaskDataTable datasyncTask;
            public ApplicationSchema.Datasync_TaskDataTable Datasync_Task
            {
                get
                {
                    if (datasyncTask == null)
                    {
                        datasyncTask = new ApplicationSchema.Datasync_TaskDataTable();
                    }
                    return datasyncTask;
                }
            }

            private ApplicationSchema.BranchLicenseDataTable branchLicense;
            public ApplicationSchema.BranchLicenseDataTable Branch_License
            {
                get
                {
                    if (branchLicense == null)
                    {
                        branchLicense = new ApplicationSchema.BranchLicenseDataTable();
                    }
                    return branchLicense;
                }
            }

            private ApplicationSchema.AmendmentsDataTable amendments;
            public ApplicationSchema.AmendmentsDataTable Amendments
            {
                get
                {
                    if (amendments == null)
                    {
                        amendments = new ApplicationSchema.AmendmentsDataTable();
                    }
                    return amendments;
                }
            }
            private ApplicationSchema.AcMEERPLicenseDataTable licenseDataTable;
            public ApplicationSchema.AcMEERPLicenseDataTable LicenseDataTable
            {
                get
                {
                    if (licenseDataTable == null)
                    {
                        licenseDataTable = new ApplicationSchema.AcMEERPLicenseDataTable();
                    }
                    return licenseDataTable;
                }
            }
            private ApplicationSchema.Ledger_ProfileDataTable ledgerProfileDataTable;
            public ApplicationSchema.Ledger_ProfileDataTable LedgerProfile
            {
                get
                {
                    if (ledgerProfileDataTable == null)
                    {
                        ledgerProfileDataTable = new ApplicationSchema.Ledger_ProfileDataTable();
                    }
                    return ledgerProfileDataTable;
                }
            }
            private ApplicationSchema.DutyTaxDataTable DutyTaxDataTable;
            public ApplicationSchema.DutyTaxDataTable DutyTax
            {
                get
                {
                    if (DutyTaxDataTable == null)
                    {
                        DutyTaxDataTable = new ApplicationSchema.DutyTaxDataTable();
                    }
                    return DutyTaxDataTable;
                }
            }
            private ApplicationSchema.DeducteeTypesDataTable DeducteeTypeDataTable;
            public ApplicationSchema.DeducteeTypesDataTable DeducteeType
            {
                get
                {
                    if (DeducteeTypeDataTable == null)
                    {
                        DeducteeTypeDataTable = new ApplicationSchema.DeducteeTypesDataTable();
                    }
                    return DeducteeTypeDataTable;
                }
            }

            private ApplicationSchema.NatureofPaymentsDataTable NatureofPaymentsDatatable;
            public ApplicationSchema.NatureofPaymentsDataTable NatureOfPayment
            {
                get
                {
                    if (NatureofPaymentsDatatable == null)
                    {
                        NatureofPaymentsDatatable = new ApplicationSchema.NatureofPaymentsDataTable();
                    }
                    return NatureofPaymentsDatatable;
                }
            }
            private ApplicationSchema.DutyTaxTypeDataTable DutyTaxTypeDataTable;
            public ApplicationSchema.DutyTaxTypeDataTable DutyTaxType
            {
                get
                {
                    if (DutyTaxTypeDataTable == null)
                    {
                        DutyTaxTypeDataTable = new ApplicationSchema.DutyTaxTypeDataTable();
                    }
                    return DutyTaxTypeDataTable;
                }
            }

            private ApplicationSchema.SendMessageDataTable SendMessageDataTable;
            public ApplicationSchema.SendMessageDataTable SendMessage
            {
                get
                {
                    if (SendMessageDataTable == null)
                    {
                        SendMessageDataTable = new ApplicationSchema.SendMessageDataTable();
                    }
                    return SendMessageDataTable;
                }
            }

            private ApplicationSchema.TDSSectionDataTable TDSSectionDataTable;
            public ApplicationSchema.TDSSectionDataTable TDSSection
            {
                get
                {
                    if (TDSSectionDataTable == null)
                    {
                        TDSSectionDataTable = new ApplicationSchema.TDSSectionDataTable();
                    }
                    return TDSSectionDataTable;
                }
            }

            private ApplicationSchema.GeneralateMappingDataTable generalateMapping;
            public ApplicationSchema.GeneralateMappingDataTable GeneralateMapping
            {
                get
                {
                    if (generalateMapping == null)
                    {
                        generalateMapping = new ApplicationSchema.GeneralateMappingDataTable();
                    }
                    return generalateMapping;
                }
            }

            private ApplicationSchema.AssetClassDataTable AssetClassDataTable;
            public ApplicationSchema.AssetClassDataTable AssetClass
            {
                get
                {
                    if (AssetClassDataTable == null)
                    {
                        AssetClassDataTable = new ApplicationSchema.AssetClassDataTable();
                    }
                    return AssetClassDataTable;
                }
            }

            private ApplicationSchema.AssetItemDataTable AssetItemsDataTable;
            public ApplicationSchema.AssetItemDataTable AssetItems
            {
                get
                {
                    if (AssetItemsDataTable == null)
                    {
                        AssetItemsDataTable = new ApplicationSchema.AssetItemDataTable();
                    }
                    return AssetItemsDataTable;
                }
            }

            private ApplicationSchema.UnitofMeasureDataTable AssetUnitofMeasureDataTable;
            public ApplicationSchema.UnitofMeasureDataTable AssetUnitofMeasure
            {
                get
                {
                    if (AssetUnitofMeasureDataTable == null)
                    {
                        AssetUnitofMeasureDataTable = new ApplicationSchema.UnitofMeasureDataTable();
                    }
                    return AssetUnitofMeasureDataTable;
                }
            }
            private ApplicationSchema.CongregationLedgerDataTable CongregationLedgerDataTable;
            public ApplicationSchema.CongregationLedgerDataTable CongregationLedger
            {
                get
                {
                    if (CongregationLedgerDataTable == null)
                    {
                        CongregationLedgerDataTable = new ApplicationSchema.CongregationLedgerDataTable();
                    }
                    return CongregationLedgerDataTable;
                }
            }

            private ApplicationSchema.CongregationLedgerMapDataTable CongregationLedgerMapDataTable;
            public ApplicationSchema.CongregationLedgerMapDataTable CongregationLedgerMap
            {
                get
                {
                    if (CongregationLedgerMapDataTable == null)
                    {
                        CongregationLedgerMapDataTable = new ApplicationSchema.CongregationLedgerMapDataTable();
                    }
                    return CongregationLedgerMapDataTable;
                }
            }

            private ApplicationSchema.LockVoucherDataTable lockvoucher;
            public ApplicationSchema.LockVoucherDataTable LockVoucher
            {
                get
                {
                    if (lockvoucher == null)
                    {
                        lockvoucher = new ApplicationSchema.LockVoucherDataTable();
                    }
                    return lockvoucher;
                }
            }

            private ApplicationSchema.branch_logged_historyDataTable branchloggedhistory;
            public ApplicationSchema.branch_logged_historyDataTable BranchLoggedHistory
            {
                get
                {
                    if (branchloggedhistory == null)
                    {
                        branchloggedhistory = new ApplicationSchema.branch_logged_historyDataTable();
                    }
                    return branchloggedhistory;
                }
            }

            private ApplicationSchema.SubLedgerDataTable Sub_Ledger;
            public ApplicationSchema.SubLedgerDataTable SubLedger
            {
                get
                {
                    if (Sub_Ledger == null) { Sub_Ledger = new ApplicationSchema.SubLedgerDataTable(); }
                    return Sub_Ledger;
                }
            }


            private ApplicationSchema.VoucherSubLegerDataTable Voucher_Sub_Ledger;
            public ApplicationSchema.VoucherSubLegerDataTable VoucherSubLedger
            {
                get
                {
                    if (Voucher_Sub_Ledger == null) { Voucher_Sub_Ledger = new ApplicationSchema.VoucherSubLegerDataTable(); }
                    return Voucher_Sub_Ledger;
                }
            }

            private ApplicationSchema.GeneralateOpeningBalanceDataTable genopeningBalance;
            public ApplicationSchema.GeneralateOpeningBalanceDataTable GeneralateOpeningBalance
            {
                get
                {
                    if (genopeningBalance == null) { genopeningBalance = new ApplicationSchema.GeneralateOpeningBalanceDataTable(); }
                    return genopeningBalance;
                }
            }

            private ApplicationSchema.BudgetGroupDataTable budgetgroup;
            public ApplicationSchema.BudgetGroupDataTable BudgetGroup
            {
                get
                {
                    if (budgetgroup == null) { budgetgroup = new ApplicationSchema.BudgetGroupDataTable(); }
                    return budgetgroup;
                }
            }

            private ApplicationSchema.BudgetSubGroupDataTable budgetsubgroup;
            public ApplicationSchema.BudgetSubGroupDataTable BudgetSubGroup
            {
                get
                {
                    if (budgetsubgroup == null) { budgetsubgroup = new ApplicationSchema.BudgetSubGroupDataTable(); }
                    return budgetsubgroup;
                }
            }

            private ApplicationSchema.FDINVESTMENTDataTable fdinvestment;
            public ApplicationSchema.FDINVESTMENTDataTable FDInvestment
            {
                get
                {
                    if (fdinvestment == null)
                    {
                        fdinvestment = new ApplicationSchema.FDINVESTMENTDataTable();
                    }
                    return fdinvestment;
                }
            }

            private ApplicationSchema.STATISTICS_TYPEDataTable StatisticsTypeSchema = null;
            public ApplicationSchema.STATISTICS_TYPEDataTable StatisticsType
            {
                get
                {
                    if (StatisticsTypeSchema == null) { StatisticsTypeSchema = new ApplicationSchema.STATISTICS_TYPEDataTable(); }
                    return StatisticsTypeSchema;
                }
            }

            private ApplicationSchema.BUDGET_STATISTICS_DETAILDataTable BudgetStatisticsSchema = null;
            public ApplicationSchema.BUDGET_STATISTICS_DETAILDataTable BudgetStatistics
            {
                get
                {
                    if (BudgetStatisticsSchema == null) { BudgetStatisticsSchema = new ApplicationSchema.BUDGET_STATISTICS_DETAILDataTable(); }
                    return BudgetStatisticsSchema;
                }
            }
            private ApplicationSchema.AssetVendorDataTable vendors;
            public ApplicationSchema.AssetVendorDataTable Vendors
            {
                get
                {
                    if (vendors == null)
                    {
                        vendors = new ApplicationSchema.AssetVendorDataTable();
                    }
                    return vendors;

                }
            }

            private ApplicationSchema.MasterGSTClassDataTable gstclass;
            public ApplicationSchema.MasterGSTClassDataTable MasterGSTClass
            {
                get
                {
                    if (gstclass == null)
                    {
                        gstclass = new ApplicationSchema.MasterGSTClassDataTable();
                    }
                    return gstclass;
                }
            }

            private ApplicationSchema.BRANCH_LC_ENABLE_TRACK_MODULESDataTable lcbranchenabletrackmodules;
            public ApplicationSchema.BRANCH_LC_ENABLE_TRACK_MODULESDataTable LcBranchEnableTrackModules
            {
                get
                {
                    if (lcbranchenabletrackmodules == null)
                    {
                        lcbranchenabletrackmodules = new ApplicationSchema.BRANCH_LC_ENABLE_TRACK_MODULESDataTable();
                    }
                    return lcbranchenabletrackmodules;
                }
            }

            private ApplicationSchema.BranchVoucherGraceDaysDataTable branchvoucerhgracedays;
            public ApplicationSchema.BranchVoucherGraceDaysDataTable BranchVoucherGraceDays
            {
                get
                {
                    if (branchvoucerhgracedays == null)
                    {
                        branchvoucerhgracedays = new ApplicationSchema.BranchVoucherGraceDaysDataTable();
                    }
                    return branchvoucerhgracedays;
                }
            }


        }
    }
}
