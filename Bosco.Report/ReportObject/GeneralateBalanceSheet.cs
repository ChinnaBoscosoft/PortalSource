using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using System.Data;
using Bosco.Model.UIModel;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateBalanceSheet : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();

        double AssetTotal = 0;
        double OpAssetTotal = 0;
        double LiabilityTotal = 0;
        double OpLiabilityTotal = 0;
        double OPGNCashBankTotal = 0;
        double OPRPCashBankTotal = 0;

        //private string InterACTransfer = "Inter Account Transfer";
        //private string ContributionFromProvince = "Contribution from Province";
        //private string ContributionToProvince = "Contribution to Province";
        //private int InterAcTransfer = 0;
        //private int ContributionFrom = 0;
        //private int ContributionTo = 0;
        #endregion

        #region Constructor
        public GeneralateBalanceSheet()
        {
            InitializeComponent();
        }
        #endregion


        #region Property

        double FDdifferenceAmount = 0;

        string yearFrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearFrom = settingProperty.YearFrom;
                return yearFrom;
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

        private bool isGBVerification
        {
            get
            {
                return (this.ReportProperties.ReportId == "RPT-077");
            }
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Society))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindBalanceSheet();
            }

            base.ShowReport();
        }
        #endregion

        #region Events
        private void xrtblGrandAssetTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double liability = Math.Abs(LiabilityTotal);
            double Asset = Math.Abs(AssetTotal);

            if (liability > Asset)
            {
                e.Result = liability - Asset;
            }
            else if (liability < Asset)
            {
                e.Result = Asset - liability;

            }
            e.Handled = true;
        }

        #endregion

        #region Methods
        public void BindBalanceSheet()
        {
            try
            {
                this.ReportTitle = objReportProperty.ReportTitle;
                SetReportTitle();
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                this.SetLandscapeHeader = 1066.25f;
                this.SetLandscapeFooter = 1066.25f;
                this.SetLandscapeFooterDateWidth = 860.00f;

                if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Society))
                {
                    SetReportTitle();
                    ShowReportFilterDialog();
                }
                else
                {
                    SetReportTitle();

                    xrcolcapCurrentAsset.Text = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
                    string apr = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).ToString();
                    xrtblCapOpeningAsset.Text = UtilityMember.DateSet.ToDate(apr, false).Year.ToString();

                    xrcolCapCurrentLiability.Text = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
                    string lpy = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).ToString();
                    xrtblOpeningLiability.Text = UtilityMember.DateSet.ToDate(lpy, false).Year.ToString();

                    setHeaderTitleAlignment();
                    BindSubReportSource();
                    base.ShowReport();
                    xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
                    xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        public void BindSubReportSource()
        {
            ResultArgs resultArgs = GetBalanceSheetSource();
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                xrSubLiabilities.Visible = xrsubAssets.Visible = true;
                DataTable dtGeneralateBalanceRpt = resultArgs.DataSource.Table;
                DataTable dtGBAssetDetails = new DataTable(); ;
                DataTable dtGBLiabilityDetails = new DataTable();
                ReportFooter.Visible = false;
                //For Balancesheet Detail by Con Ledger Details -----------------------------------------------
                if (isGBVerification)
                {
                    if (this.ReportProperties.ShowDetailedBalance == 1)
                    {
                        resultArgs = GetBalanceSheetDetailsByHOLedger();
                    }
                    else
                    {
                        resultArgs = GetBalanceSheetDetailsByConLedger();
                    }
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtGeneralateBalanceRptDetails = resultArgs.DataSource.Table;
                        //dtGeneralateBalanceRptDetails.DefaultView.RowFilter = "AMOUNT < 0";
                        dtGeneralateBalanceRptDetails.DefaultView.RowFilter = "CON_LEDGER_CODE LIKE '%A%' OR CON_LEDGER_ID = -1"; //For Unmapped Asset LEdgers (-1)
                        dtGBAssetDetails = dtGeneralateBalanceRptDetails.DefaultView.ToTable();
                        dtGBAssetDetails.Columns["AMOUNT"].ColumnName = "FINAL";

                        dtGeneralateBalanceRptDetails.DefaultView.RowFilter = string.Empty;
                        //dtGeneralateBalanceRptDetails.DefaultView.RowFilter = "AMOUNT > 0";
                        dtGeneralateBalanceRptDetails.DefaultView.RowFilter = "CON_LEDGER_CODE LIKE '%B%' OR CON_LEDGER_ID = -2"; //For Unmapped Lia Leedgers (-2)
                        dtGBLiabilityDetails = dtGeneralateBalanceRptDetails.DefaultView.ToTable();
                        dtGBLiabilityDetails.Columns.Add("FINAL", dtGeneralateBalanceRptDetails.Columns["AMOUNT"].DataType, "AMOUNT * -1"); //Change negative value to possitive value
                    }
                }
                //-----------------------------------------------------------------------------------------

                //For Liability
                //On 24/10/2020, Show only Liability ledgers based on its nature
                //dtGeneralateBalanceRpt.DefaultView.RowFilter = "AMOUNT_ACTUAL < 0";
                dtGeneralateBalanceRpt.DefaultView.RowFilter = "PARENT_CON_CODE LIKE '%B%' OR CON_LEDGER_ID = -2"; //For Unmapped Lia Leedgers (-2)
                DataTable dtGenerlateLiabilities = dtGeneralateBalanceRpt.DefaultView.ToTable();
                dtGenerlateLiabilities.Columns.Add("AMOUNT", dtGeneralateBalanceRpt.Columns["AMOUNT_ACTUAL"].DataType, "AMOUNT_ACTUAL * -1"); //Change negative value to possitive value
                GeneralateBalanceSheetLiability liabilities = xrSubLiabilities.ReportSource as GeneralateBalanceSheetLiability;
                liabilities.BindBalanceSheetLiability(dtGenerlateLiabilities, dtGBLiabilityDetails);
                LiabilityTotal = liabilities.TotalLiabilities;
                OpLiabilityTotal = liabilities.OpTotalLiabilities;
                //this.AttachDrillDownToSubReport(liabilities);
                liabilities.HideBalanceSheetLiabilityHeader();

                //For Asset
                //On 24/10/2020, Show only Liability ledgers based on its nature
                dtGeneralateBalanceRpt.DefaultView.RowFilter = string.Empty;
                //dtGeneralateBalanceRpt.DefaultView.RowFilter = "AMOUNT_ACTUAL > 0";
                dtGeneralateBalanceRpt.DefaultView.RowFilter = "PARENT_CON_CODE LIKE '%A%' OR CON_LEDGER_ID = -1"; //For Unmapped Asset LEdgers (-1)
                DataTable dtAsset = dtGeneralateBalanceRpt.DefaultView.ToTable();
                dtAsset.Columns["AMOUNT_ACTUAL"].ColumnName = "AMOUNT";

                if (isGBVerification && this.ReportProperties.ShowDetailedBalance == 1)
                {
                    AttachFDDifferenceToBank(dtAsset);
                }

                //On 14/02/2024
                //AttachFDDifferenceToBank(dtAsset);

                GeneralateBalanceSheetAsset Asset = xrsubAssets.ReportSource as GeneralateBalanceSheetAsset;
                Asset.BindBalanceSheetAsset(dtAsset, dtGBAssetDetails);
                AssetTotal = Asset.TotalAssets;
                OpAssetTotal = Asset.OPTotalAssets;
                //this.AttachDrillDownToSubReport(Asset);
                Asset.HideBalanceSheetAssetCapHeader();

                if (!isGBVerification && this.ReportProperties.IncludeDetailed == 1)
                {
                    ReportFooter.Visible = true;
                    OPGNCashBankTotal = GetCashBankOPTotal(dtAsset);
                    ShowGenerlateDifferenceDetails();
                }
            }
            else
            {
                xrSubLiabilities.Visible = xrsubAssets.Visible = false;
            }
        }

        public ResultArgs GetBalanceSheetSource()
        {
            //Get Projects for selected projects ---------------------
            //this.ReportProperties.Project = this.GetSocietyProjectIds();
            this.ReportProperties.Project = this.GetProjectIds(this.ReportProperties.Society, this.ReportProperties.BranchOffice);
            //--------------------------------------------------------

            //int includeopbalance = (isGBVerification ? 1 : 0);
            int includeopbalance = (isGBVerification ? 0 : 1);

            //InterAcTransfer = GetInterAcTransferId();
            //ContributionFrom = GetContriFromProvinceId();
            //ContributionTo = GetContriToProvinceId();

            string BalanceSheet = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.BalanceSheet);
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddDays(-1));

                int LedgerPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, LedgerPaddingRequired);

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);

                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_OP_BALANCEColumn, includeopbalance); //For Opening

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
            }
            return resultArgs;
        }

        private ResultArgs GetRPReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateAbstract, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);


                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                //if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                //}
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }

        private ResultArgs GetPLSummaryDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlProfitandLoss = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.ProfitandLossbyHoseWiseInterAcc);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.ProfitandLossbyHoseWiseInterAcc, DataBaseType.HeadOffice))
            {
                string Ledger = this.LoginUser.InterAccountFromLedgerIds + "," + this.LoginUser.InterAccountToLedgerIds + "," + this.LoginUser.ProvinceFromLedgerIds + "," + this.LoginUser.ProvinceToLedgerIds;
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, Ledger);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlProfitandLoss);
            }
            return resultArgs;
        }

        public ResultArgs GetBalanceSheetDetailsByConLedger()
        {
            string BalanceSheet = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.BalanceSheetDetailByConLedger);
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                int LedgerPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, LedgerPaddingRequired);

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
            }
            return resultArgs;
        }

        public ResultArgs GetBalanceSheetDetailsByHOLedger()
        {
            string BalanceSheet = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.BalanceSheetDetailByHOLedger);
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                int LedgerPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, LedgerPaddingRequired);

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
            }
            return resultArgs;
        }

        private void AttachFDDifferenceToBank(DataTable dtAsset)
        {
            double cashbankfdOpening = 0;
            double cashbankfdClosing = 0;

            //Get Projects for selected projects ---------------------
            //  this.ReportProperties.Project = this.GetSocietyProjectIds();
            // this.ReportProperties.Project = this.GetProjectIds(this.ReportProperties.Society, this.ReportProperties.BranchOffice);
            this.ReportProperties.Project = this.GetProjectIds(this.ReportProperties.Society, this.ReportProperties.BranchOffice);
            //--------------------------------------------------------

            //For Receipts & Payments ---------------------------
            double totalreceitps = 0;
            double totalpayments = 0;
            ResultArgs resultRP = GetRPReportSource();
            if (resultRP.Success && resultRP.DataSource.Table != null)
            {
                DataTable dtRP = resultRP.DataSource.Table;

                //For Receipts
                //DataTable dtReceipts = resultRP.DataSource.Table;
                //For Receipts
                dtRP.DefaultView.RowFilter = string.Empty;
                dtRP.DefaultView.RowFilter = "AMOUNT_CR > 0";
                DataTable dtReceipts = dtRP.DefaultView.ToTable();
                dtReceipts.Columns["AMOUNT_CR"].ColumnName = "AMOUNT_PERIOD";
                dtRP.DefaultView.RowFilter = string.Empty;

                //For Payments
                //resultRP = GetRPReportSource(TransType.PY);
                //DataTable dtPayments = resultRP.DataSource.Table;
                dtRP.DefaultView.RowFilter = string.Empty;
                dtRP.DefaultView.RowFilter = "AMOUNT_DR > 0";
                DataTable dtPayments = dtRP.DefaultView.ToTable();
                dtPayments.Columns["AMOUNT_DR"].ColumnName = "AMOUNT_PERIOD";
                dtRP.DefaultView.RowFilter = string.Empty;

                if (resultRP.Success && resultRP.DataSource.Table != null)
                {
                    totalreceitps = UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(AMOUNT_PERIOD)", string.Empty).ToString());
                    totalpayments = UtilityMember.NumberSet.ToDouble(dtPayments.Compute("SUM(AMOUNT_PERIOD)", string.Empty).ToString());
                }
            }
            //---------------------------------------------------

            ResultArgs resultbalance = this.GetBalanceDetail(true, this.ReportProperties.DateFrom, this.ReportProperties.Project, "12,13,14");
            if (resultbalance.Success && resultbalance.DataSource.Table != null)
            {
                DataTable dtOpBalance = resultbalance.DataSource.Table;
                dtOpBalance.Columns.Add("FINAL_AMOUNT", dtOpBalance.Columns["AMOUNT"].DataType, "IIF(TRANS_MODE='CR', AMOUNT * -1, AMOUNT)");
                cashbankfdOpening = UtilityMember.NumberSet.ToDouble(dtOpBalance.Compute("SUM(FINAL_AMOUNT)", string.Empty).ToString());
                OPRPCashBankTotal = cashbankfdOpening;
                totalreceitps = totalreceitps + cashbankfdOpening;
            }

            resultbalance = this.GetBalanceDetail(false, this.ReportProperties.DateTo, this.ReportProperties.Project, "12,13,14");
            if (resultbalance.Success && resultbalance.DataSource.Table != null)
            {
                DataTable dtCLBalance = resultbalance.DataSource.Table;
                dtCLBalance.Columns.Add("FINAL_AMOUNT", dtCLBalance.Columns["AMOUNT"].DataType, "IIF(TRANS_MODE='CR', AMOUNT * -1, AMOUNT)");
                cashbankfdClosing = UtilityMember.NumberSet.ToDouble(dtCLBalance.Compute("SUM(FINAL_AMOUNT)", string.Empty).ToString());
                totalpayments = totalpayments + cashbankfdClosing;
            }
            FDdifferenceAmount = (totalreceitps - totalpayments);

            if (FDdifferenceAmount != 0 && dtAsset != null && dtAsset.Rows.Count > 0)
            {
                /*dtAsset.DefaultView.RowFilter = "CON_CODE = 'A.4.1.2'"; //For Bank Accounts
                if (dtAsset.DefaultView.Count > 0)
                {
                    dtAsset.DefaultView.BeginInit();
                    double amt = UtilityMember.NumberSet.ToDouble(dtAsset.DefaultView[0]["AMOUNT"].ToString());
                    dtAsset.DefaultView[0]["AMOUNT"] = amt + FDdifferenceAmount;
                    dtAsset.DefaultView.EndInit();
                }
                dtAsset.DefaultView.RowFilter = string.Empty;*/
            }
        }

        /*
        private int GetInterAcTransferId()
        {
            ResultArgs resultArgs = null;
            string sqlLedgerIds = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GetLedgerInterAccountTransferId);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GetLedgerInterAccountTransferId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_NAMEColumn, InterACTransfer);
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.Scalar, sqlLedgerIds);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        private int GetContriFromProvinceId()
        {
            ResultArgs resultArgs = null;
            string sqlLedgerIds = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GetLedgerContributionFromProvince);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GetLedgerContributionFromProvince, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_NAMEColumn, ContributionFromProvince);
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.Scalar, sqlLedgerIds);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        private int GetContriToProvinceId()
        {
            ResultArgs resultArgs = null;
            string sqlLedgerIds = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GetLegerContributionToProvince);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GetLegerContributionToProvince, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_NAMEColumn, ContributionToProvince);
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.Scalar, sqlLedgerIds);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        */


        private double GetCashBankOPTotal(DataTable dtAsset)
        {
            double rtn = 0;
            if (this.DataSource != null)
            {
                rtn = OPGNCashBankTotal = UtilityMember.NumberSet.ToDouble(dtAsset.Compute("SUM(OP_AMOUNT)", "CON_CODE IN ('A.4.1.2', 'A.4.1.1')").ToString());
            }
            return OPGNCashBankTotal;
        }

        private void ShowGenerlateDifferenceDetails()
        {
            if (!isGBVerification && this.ReportProperties.IncludeDetailed == 1)
            {
                double DirectIncome = 0;
                double DirectExpense = 0;
                double InterAccountCr = 0;
                double InterAccountDr = 0;
                double ProvinceContributionFrom = 0;
                double ProvinceContributionTo = 0;
                double IEdifference = 0;
                double ContributionInterAcProvinceDifference = 0;
                double ExcessIE = 0;
                xrcellOPYearCaption.Text = xrtblCapOpeningAsset.Text;
                xrcellCurrentYearCaption.Text = xrcolcapCurrentAsset.Text;

                xrcellFDDiffAmount.Text = UtilityMember.NumberSet.ToNumber(FDdifferenceAmount);

                ResultArgs resultargpl = GetPLSummaryDetailReportSource();
                if (resultargpl.Success && resultargpl.DataSource.Table != null)
                {
                    DataTable dtPLSummaryDetail = resultargpl.DataSource.Table;
                    DirectIncome = UtilityMember.NumberSet.ToDouble(dtPLSummaryDetail.Compute("SUM(" + this.reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName + ")", string.Empty).ToString());
                    DirectExpense = UtilityMember.NumberSet.ToDouble(dtPLSummaryDetail.Compute("SUM(" + this.reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName + ")", string.Empty).ToString());
                    xrcellDirectIncome.Text = UtilityMember.NumberSet.ToNumber(DirectIncome);
                    xrcellDirectExpense.Text = UtilityMember.NumberSet.ToNumber(DirectExpense);
                    IEdifference = (DirectIncome - DirectExpense);
                    xrcellPLDifference.Text = UtilityMember.NumberSet.ToNumber(IEdifference);

                    InterAccountCr = UtilityMember.NumberSet.ToDouble(dtPLSummaryDetail.Compute("SUM(" + this.reportSetting1.ProfitandLossbyHouse.INTER_CRColumn.ColumnName + ")", string.Empty).ToString());
                    InterAccountDr = UtilityMember.NumberSet.ToDouble(dtPLSummaryDetail.Compute("SUM(" + this.reportSetting1.ProfitandLossbyHouse.INTER_DRColumn.ColumnName + ")", string.Empty).ToString());
                    xrcellInterAccountReceipt.Text = UtilityMember.NumberSet.ToNumber(InterAccountCr);
                    xrcellInterAccountPayment.Text = UtilityMember.NumberSet.ToNumber(InterAccountDr);

                    ProvinceContributionFrom = UtilityMember.NumberSet.ToDouble(dtPLSummaryDetail.Compute("SUM(" + this.reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_FROM_CRColumn.ColumnName + ")", string.Empty).ToString());
                    ProvinceContributionTo = UtilityMember.NumberSet.ToDouble(dtPLSummaryDetail.Compute("SUM(" + this.reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_TO_DRColumn.ColumnName + ")", string.Empty).ToString());
                    xrcellProvinceConFromAmt.Text = UtilityMember.NumberSet.ToNumber(ProvinceContributionFrom);
                    xrcellProvinceConToAmt.Text = UtilityMember.NumberSet.ToNumber(ProvinceContributionTo);

                    xrcellContributionFrom.Text = UtilityMember.NumberSet.ToNumber(InterAccountCr + ProvinceContributionFrom);
                    xrcellContributionTo.Text = UtilityMember.NumberSet.ToNumber(InterAccountDr + ProvinceContributionTo);
                    ContributionInterAcProvinceDifference = (InterAccountCr + ProvinceContributionFrom) - (InterAccountDr + ProvinceContributionTo);
                    xrcellContributionDifference.Text = UtilityMember.NumberSet.ToNumber(ContributionInterAcProvinceDifference);
                    ExcessIE = IEdifference + ContributionInterAcProvinceDifference;
                    xrcellGainLoss.Text = UtilityMember.NumberSet.ToNumber(ExcessIE);
                    //xrcellGainLossTitle.Text = "P & L Net Result";
                    /*if (ExcessIE > 0)
                    {
                        xrcellGainLossTitle.Text = "Excess of Income over Expenditure";
                    }
                    else if (ExcessIE < 0)
                    {
                        xrcellGainLossTitle.Text = "Excess of Expenses over income";
                    }*/

                    //Calculate Balance Sheet Difference ------------------------------------
                    double TotalAssetBalancesheet = AssetTotal; //+(OpAssetTotal - OPGNCashBankTotal);
                    double TotalLiabilityBalancesheet = Math.Abs(LiabilityTotal); //+ OpLiabilityTotal;
                    double BalanceSheetDifference = TotalAssetBalancesheet - TotalLiabilityBalancesheet;

                    double BalancesheetOP = (OpAssetTotal > OpLiabilityTotal ? OpAssetTotal - OpLiabilityTotal : OpLiabilityTotal - OpAssetTotal);
                    double BlaanceshetCU = (TotalAssetBalancesheet > TotalLiabilityBalancesheet ? TotalAssetBalancesheet - TotalLiabilityBalancesheet : TotalLiabilityBalancesheet - TotalAssetBalancesheet);
                    xrcellTotalAsset.Text = UtilityMember.NumberSet.ToNumber(BalancesheetOP);
                    xrcellTotalLiabilities.Text = UtilityMember.NumberSet.ToNumber(BlaanceshetCU);

                    double BalanceSheetOPDifferene = 0;
                    if (BalancesheetOP > BlaanceshetCU)
                    {
                        BalanceSheetOPDifferene = BalancesheetOP - BlaanceshetCU;
                    }
                    else
                    {
                        BalanceSheetOPDifferene = BlaanceshetCU - BalancesheetOP;
                    }
                    xrcellBalancesheetDifference.Text = UtilityMember.NumberSet.ToNumber(BalanceSheetOPDifferene);
                    xrcellBalanceSheetGNOP.Text = UtilityMember.NumberSet.ToNumber(OPGNCashBankTotal);
                    xrcellBalanceSheetRPOP.Text = UtilityMember.NumberSet.ToNumber(OPRPCashBankTotal);
                    double BalansheetDiffAmt = (OPRPCashBankTotal - OPGNCashBankTotal);
                    xrcellBalanceSheetOPDiff.Text = UtilityMember.NumberSet.ToNumber(BalansheetDiffAmt);
                    double BalansheetAmt = (OPRPCashBankTotal - OPGNCashBankTotal);
                    xrcellBalansheetAmt.Text = UtilityMember.NumberSet.ToNumber(BalanceSheetOPDifferene - BalansheetAmt);
                    //-----------------------------------------------------------------------
                }
            }
        }

        private void xrtblCurrentAssetValueAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(AssetTotal);
            e.Handled = true;
        }

        private void xrtblCurrentLiabilityValueAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(LiabilityTotal);
            e.Handled = true;
        }

        #endregion

        private void xrtblAssetOpeningtatallAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(OpAssetTotal);
            e.Handled = true;
        }

        private void xrtblLiabilityOpeningTotalAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(OpLiabilityTotal);
            e.Handled = true;
        }

        private void xrtblGrandOpeningTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double liability = Math.Abs(OpLiabilityTotal);
            double Asset = Math.Abs(OpAssetTotal);

            if (liability > Asset)
            {
                e.Result = liability - Asset;
            }
            else if (liability < Asset)
            {
                e.Result = Asset - liability;
            }
            e.Handled = true;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (isGBVerification && this.ReportProperties.IncludeDetailed == 1);
        }
    }
}
