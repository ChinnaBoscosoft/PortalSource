using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ProfitandLossofHousesInterAc : Bosco.Report.Base.ReportHeaderBase
    {
        //private string InterACTransfer = "Inter Account Transfer";
        //private string ContributionFromProvince = "Contribution from Province";
        //private string ContributionToProvince = "Contribution to Province";
        //private int InterAcTransfer = 0;
        //private int ContributionFrom = 0;
        //private int ContributionTo = 0;

        private double LETotalIncome = 0;
        private double LETotalExpense = 0;
        private double LEFinalResult = 0;

        private double GrandLETotalIncome = 0;
        private double GrandLETotalExpense = 0;
        private double GrandLEFinalResult = 0;

        private DataTable dtHouseAccountDetails = new DataTable();

        #region Constructor

        public ProfitandLossofHousesInterAc()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 1158.25f;
            this.SetLandscapeFooter = 1158.25f;
        }

        #endregion

        #region ShowReports

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
                BindSource();
            }
            base.ShowReport();
        }

        #endregion

        #region Methods

        public void BindSource()
        {
            //this.ReportProperties.Society = "391, 400";
            LETotalIncome = LETotalExpense = LEFinalResult = 0;
            GrandLETotalIncome = GrandLETotalExpense = GrandLEFinalResult = 0;
            this.ReportTitle = objReportProperty.ReportTitle;
            SetReportTitle();
            //InterAcTransfer = GetInterAcTransferId();
            //ContributionFrom = GetContriFromProvinceId();
            //ContributionTo = GetContriToProvinceId();
            this.SetLandscapeFooterDateWidth = 1000.00f;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            xrlblline.Text = string.Empty;
            //xrTableHeader.WidthF = xrTableDetails.WidthF = xrTable1.WidthF = 1040.25f;
            ResultArgs resultArgs = GetReportSource();
            if (resultArgs != null && resultArgs.Success)
            {
                DataView dvProfitandLoss = resultArgs.DataSource.TableView;
                if (dvProfitandLoss != null)
                {
                    dvProfitandLoss.Table.TableName = "ProfitandLossbyHouse";
                    this.DataSource = dvProfitandLoss;
                    this.DataMember = dvProfitandLoss.Table.TableName;

                    if (this.ReportProperties.ShowInterAccountDetails == 1 || this.ReportProperties.ShowProvinceFromToContributionDetails == 1)
                    {
                        resultArgs = GetReportAccountDetailsSource();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            dtHouseAccountDetails = resultArgs.DataSource.Table;
                        }

                    }
                }
            }
        }

        private ResultArgs GetReportSource()
        {
            //Get Projects for selected projects ---------------------
            // this.ReportProperties.Project = this.GetSocietyProjectIds();
            this.ReportProperties.Project = this.GetProjectIds(this.ReportProperties.Society, this.ReportProperties.BranchOffice);
            //--------------------------------------------------------

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
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlProfitandLoss);
            }
            return resultArgs;
        }

        private ResultArgs GetReportAccountDetailsSource()
        {
            ResultArgs resultArgs = null;
            try
            {
                string sqlProfitandLossInterAcDetails = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.ProfitandLossbyHoseWiseInterAccDetail);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.ProfitandLossbyHoseWiseInterAccDetail, DataBaseType.HeadOffice))
                {
                    //string Ledger = InterAcTransfer + "," + ContributionFrom + "," + ContributionTo;
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                    dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                    dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                    dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlProfitandLossInterAcDetails);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private void MakeHighlightColor(XRTableCell xrcell, IEHouseDetailAc detailAccount)
        {
            if (GetCurrentColumnValue("CID") != null)
            {
                if (this.ReportProperties.ShowInterAccountDetails == 1 && detailAccount == IEHouseDetailAc.InterAcTransfer)
                {
                    xrcell.Font = new Font(xrcell.Font, FontStyle.Bold);
                    xrcell.BackColor = Color.LightYellow;
                }
                else if (this.ReportProperties.ShowProvinceFromToContributionDetails == 1)
                {
                    if (detailAccount == IEHouseDetailAc.ContributionFromProvince)
                    {
                        xrcell.Font = new Font(xrcell.Font, FontStyle.Bold);
                        xrcell.BackColor = Color.LightGreen;
                    }
                    else if (detailAccount == IEHouseDetailAc.ContributionToProvince)
                    {
                        xrcell.BackColor = Color.LightBlue;
                        xrcell.Font = new Font(xrcell.Font, FontStyle.Bold);
                    }
                }
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

        #endregion

        #region Events
        private void xrIncomeTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = "0.00";
            LETotalIncome = 0;
            if (GetCurrentColumnValue("RECEIPT") != null &&
                GetCurrentColumnValue("INTER_CR") != null && GetCurrentColumnValue("CONTRIBUTION_FROM_CR") != null)
            {
                double Income = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("RECEIPT").ToString());
                double InterAcFrom = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("INTER_CR").ToString());
                double FromProvince = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("CONTRIBUTION_FROM_CR").ToString());
                LETotalIncome = Income + InterAcFrom + FromProvince;
                cell.Text = UtilityMember.NumberSet.ToNumber(LETotalIncome);
            }
        }

        private void xrExpenseTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = "0.00";
            LETotalExpense = 0;
            if (GetCurrentColumnValue("PAYMENT") != null &&
                GetCurrentColumnValue("INTER_DR") != null && GetCurrentColumnValue("CONTRIBUTION_TO_DR") != null)
            {
                double Expense = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("PAYMENT").ToString());
                double InterAcTo = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("INTER_DR").ToString());
                double ToProvince = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("CONTRIBUTION_TO_DR").ToString());
                LETotalExpense = Expense + InterAcTo + ToProvince;
                cell.Text = UtilityMember.NumberSet.ToNumber(LETotalExpense);
            }

            //Calculation
            LEFinalResult = LETotalIncome - LETotalExpense;
            GrandLETotalIncome += LETotalIncome;
            GrandLETotalExpense += LETotalExpense;
            GrandLEFinalResult += LEFinalResult;
        }

        private void xrResult_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(LEFinalResult);
        }

        private void xrGIncomeTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(GrandLETotalIncome);
        }

        private void xrGTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(GrandLETotalExpense);
        }

        private void xrGResult_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(GrandLEFinalResult);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.ReportProperties.ShowInterAccountDetails == 1 || this.ReportProperties.ShowProvinceFromToContributionDetails == 1)
            {
                Int32 SocietyId = GetCurrentColumnValue("CID") == null ? 0 : UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("CID").ToString());
                dtHouseAccountDetails.DefaultView.RowFilter = "";
                dtHouseAccountDetails.DefaultView.RowFilter = "CUSTOMERID=" + SocietyId;
                DataTable dtAccountDetails = dtHouseAccountDetails.DefaultView.ToTable();
                dtHouseAccountDetails.DefaultView.RowFilter = "";

                //Int32 SocietyId = GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.CUSTOMERIDColumn.ColumnName) == null ? 0 : UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.CUSTOMERIDColumn.ColumnName).ToString());
                //double CR_InterAc = GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.INTER_CRColumn.ColumnName) == null ? 0 : UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.INTER_CRColumn.ColumnName).ToString());
                //double DR_InterAc = GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.INTER_DRColumn.ColumnName) == null ? 0 : UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.INTER_DRColumn.ColumnName).ToString());
                //double CR_ProvinceFromAc = GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_FROM_CRColumn.ColumnName) == null ? 0 : UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_FROM_CRColumn.ColumnName).ToString());
                //double DR_ProvinceToAc = GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_TO_DRColumn.ColumnName) == null ? 0 : UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_TO_DRColumn.ColumnName).ToString());
                //xrSubLedgerDetail.Visible = xrSubLedgerDetail1.Visible = false;
                if (dtHouseAccountDetails != null && (this.ReportProperties.ShowInterAccountDetails == 1 || this.ReportProperties.ShowProvinceFromToContributionDetails == 1))
                {
                    ProfitAndLossofHousesInterAcDetail reportInterAcDetails = xrSubLedgerDetail.ReportSource as ProfitAndLossofHousesInterAcDetail;
                    if (this.ReportProperties.ShowProvinceFromToContributionDetails == 1) //For Province Contribution From
                    {
                        reportInterAcDetails.BindPAndLHousesInterAcDetail(SocietyId, dtAccountDetails, IEHouseDetailAc.ContributionFromProvince);
                    }
                    else //For Inter Account details
                    {
                        reportInterAcDetails.BindPAndLHousesInterAcDetail(SocietyId, dtAccountDetails, IEHouseDetailAc.InterAcTransfer);
                    }

                    reportInterAcDetails.TitleColumnWidth = xrCapLEName.WidthF;
                    reportInterAcDetails.DateColumnWidth = xrCapLEDIncome.WidthF;
                    reportInterAcDetails.ReceiptsColumnWidth = xrCapInterAcFrom.WidthF;
                    reportInterAcDetails.PaymentsColumnWidth = xrCapProvinceFrom.WidthF;
                    reportInterAcDetails.LedgerTitleWidth = xrCapLEName.WidthF + xrCapLEDIncome.WidthF + xrCapInterAcFrom.WidthF + xrCapProvinceFrom.WidthF;
                    reportInterAcDetails.HidePAndLHousesInterAcDetailHeaders();
                    xrlblline.Text = "-----------------------------------------------------------------------------------------------------------------------------------------------------------";
                }

                //For Province Contribution From
                if (dtHouseAccountDetails != null && this.ReportProperties.ShowProvinceFromToContributionDetails == 1)
                {
                    dtHouseAccountDetails.DefaultView.RowFilter = "";
                    dtHouseAccountDetails.DefaultView.RowFilter = "CUSTOMERID=" + SocietyId;
                    DataTable dtAccountDetails1 = dtHouseAccountDetails.DefaultView.ToTable();
                    dtHouseAccountDetails.DefaultView.RowFilter = "";

                    ProfitAndLossofHousesInterAcDetail reportInterAcDetails1 = xrSubLedgerDetail1.ReportSource as ProfitAndLossofHousesInterAcDetail;
                    reportInterAcDetails1.BindPAndLHousesInterAcDetail(SocietyId, dtAccountDetails1, IEHouseDetailAc.ContributionToProvince);
                    reportInterAcDetails1.TitleColumnWidth = xrCapLEDExpense.WidthF + xrCapInterAcTo.WidthF;
                    reportInterAcDetails1.DateColumnWidth = xrCapProvinceTo.WidthF;
                    reportInterAcDetails1.ReceiptsColumnWidth = xrCapExpenseTotal.WidthF;
                    reportInterAcDetails1.PaymentsColumnWidth = xrCapResult1.WidthF;
                    reportInterAcDetails1.LedgerTitleWidth = xrCapLEDExpense.WidthF + xrCapInterAcTo.WidthF + xrCapProvinceTo.WidthF + xrCapExpenseTotal.WidthF + xrCapResult1.WidthF;
                    reportInterAcDetails1.HidePAndLHousesInterAcDetailHeaders();
                    xrlblline.Text = "-----------------------------------------------------------------------------------------------------------------------------------------------------------";
                }
            }
        }

        private void xrInterAcFrom_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MakeHighlightColor((sender as XRTableCell), IEHouseDetailAc.InterAcTransfer);
        }

        private void xrInterAcTo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MakeHighlightColor((sender as XRTableCell), IEHouseDetailAc.InterAcTransfer);
        }

        private void xrProvinceFrom_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MakeHighlightColor((sender as XRTableCell), IEHouseDetailAc.ContributionFromProvince);
        }

        private void xrProvinceTo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MakeHighlightColor((sender as XRTableCell), IEHouseDetailAc.ContributionToProvince);
        }

        private void xrSubLedgerDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !(this.ReportProperties.ShowInterAccountDetails == 1 || this.ReportProperties.ShowProvinceFromToContributionDetails == 1);
        }

        private void xrSubLedgerDetail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !(this.ReportProperties.ShowProvinceFromToContributionDetails == 1);
        }


        #endregion

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !(this.ReportProperties.ShowInterAccountDetails == 1 || this.ReportProperties.ShowProvinceFromToContributionDetails == 1);
        }
    }
}
