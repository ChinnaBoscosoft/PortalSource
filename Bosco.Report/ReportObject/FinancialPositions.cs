using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using System.Data;
using Bosco.DAO.Schema;

namespace Bosco.Report.ReportObject
{
    public partial class FinancialPositions : ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();

        #endregion

        #region Constructor
        public FinancialPositions()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private string GetCashBankIds()
        {
            string groupIds = (int)FixedLedgerGroup.BankAccounts + "," +
                              (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.FixedDeposit;
            return groupIds;
        }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindFinancialPositions();
        }

        #endregion

        #region Events

        #endregion

        #region Methods

        public void BindFinancialPositions()
        {
            try
            {

                this.SetLandscapeHeader = 1055.25f;
                this.SetLandscapeFooter = 1055.25f;
                this.SetLandscapeFooterDateWidth = 900.00f;
                setHeaderTitleAlignment();
                string datetime = this.GetProgressiveDate(this.ReportProperties.DateAsOn);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.ReportTitle = this.ReportProperties.ReportTitle;
                xrcellDate.Text = "Balance as on " + this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateAsOn, false).ToShortDateString();
                if (string.IsNullOrEmpty(this.ReportProperties.DateAsOn))
                {
                    SetReportTitle();
                    ShowReportFilterDialog();
                }
                else
                {
                    SetReportTitle();
                    this.ReportPeriod = String.Format("Date as on: {0}", this.ReportProperties.DateAsOn);
                    ResultArgs resultArgs = GetFinancialPositions();

                    DataTable dtValue = resultArgs.DataSource.Table;
                    if (dtValue != null)
                    {
                        dtValue.TableName = "FinancialPosition";
                        this.DataSource = dtValue;
                        this.DataMember = dtValue.TableName;
                    }

                    base.ShowReport();

                    // xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }



        }

        public ResultArgs GetFinancialPositions()
        {
            double amtPeriod = 0;
            double totAmtPeriod = 0;

            DataTable dtFinancialPosition = this.reportSetting1.FinancialPosition;
            dtFinancialPosition.Clear();
            string Financial = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FinancialPosition);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.FinancialPosition, DataBaseType.HeadOffice))
            {
                string Ids = this.GetCashBankIds();
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, Ids);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, this.ReportProperties.DateAsOn);

                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Financial);
            }

            if (resultArgs.Success)
            {
                DataView dvSource = resultArgs.DataSource.TableView;
                //Calculate Sum of Balance, It will be used in grand toal or group Total
                foreach (DataRowView drvBalance in dvSource)
                {
                    amtPeriod = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                    string CurrentTransMode = drvBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                    if (CurrentTransMode == TransactionMode.CR.ToString())
                    {
                        amtPeriod = -amtPeriod;
                    }

                    //Update Amount to banalnce datable, if transmode CR
                    drvBalance.BeginEdit();
                    drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = amtPeriod;
                    drvBalance.EndEdit();

                    totAmtPeriod += amtPeriod;
                }


                DataTable dt = dvSource.Table;
                //Take Unique project names
                string[] distinct = { "PROJECT_ID", "PROJECT" };
                DataTable dtProjects = dvSource.ToTable(true, distinct);
                foreach (DataRow drProject in dtProjects.Rows)
                {
                    string project = drProject[this.ReportParameters.PROJECTColumn.ColumnName].ToString();
                    int projectid = UtilityMember.NumberSet.ToInteger(drProject[this.ReportParameters.PROJECT_IDColumn.ColumnName].ToString());
                    dvSource.RowFilter = string.Empty;
                    //object CashAmount = dvSource.Table.Compute("SUM(AMOUNT)", "PROJECT_ID=" + projectid + " AND GROUP_ID=" + (int)Utility.FixedLedgerGroup.Cash);
                    object CashAmount = dvSource.Table.Compute("SUM(AMOUNT)", this.ReportParameters.PROJECT_IDColumn.ColumnName + "=" + projectid + " AND GROUP_ID=" + (int)Utility.FixedLedgerGroup.Cash);
                    object FDAmount = dvSource.Table.Compute("SUM(AMOUNT)", this.ReportParameters.PROJECT_IDColumn.ColumnName + "=" + projectid + " AND GROUP_ID=" + (int)Utility.FixedLedgerGroup.FixedDeposit);

                    dvSource.RowFilter = this.ReportParameters.PROJECT_IDColumn.ColumnName + "=" + projectid + " AND GROUP_ID = " + (int)Utility.FixedLedgerGroup.BankAccounts;
                    foreach (DataRowView drvsource in dvSource)
                    {
                        DataRow dr = dtFinancialPosition.NewRow();
                        dr[reportSetting1.FinancialPosition.PROJECTColumn.ColumnName] = project;
                        dr[reportSetting1.FinancialPosition.BANKColumn.ColumnName] = drvsource[reportSetting1.FinancialPosition.BANKColumn.ColumnName].ToString();
                        dr[reportSetting1.FinancialPosition.LEDGER_NAMEColumn.ColumnName] = drvsource[reportSetting1.FinancialPosition.LEDGER_NAMEColumn.ColumnName].ToString();
                        dr[reportSetting1.FinancialPosition.OPERATED_BYColumn.ColumnName] = drvsource[reportSetting1.FinancialPosition.OPERATED_BYColumn.ColumnName].ToString();
                        dr[reportSetting1.FinancialPosition.CASH_AMOUNTColumn.ColumnName] = this.UtilityMember.NumberSet.ToDecimal(CashAmount.ToString());
                        dr[reportSetting1.FinancialPosition.BANK_AMOUNTColumn.ColumnName] = this.UtilityMember.NumberSet.ToDecimal(drvsource[reportSetting1.FinalReceiptsPayments.AMOUNTColumn.ColumnName].ToString());
                        dr[reportSetting1.FinancialPosition.FD_AMOUNTColumn.ColumnName] = this.UtilityMember.NumberSet.ToDecimal(FDAmount.ToString());
                        dr[reportSetting1.FinancialPosition.PURPOSESColumn.ColumnName] = drvsource[reportSetting1.FinancialPosition.PURPOSESColumn.ColumnName].ToString();
                        CashAmount = 0; //Reset 
                        FDAmount = 0;  //Reset 
                        dtFinancialPosition.Rows.Add(dr);
                    }

                    //If there is no bank account, add default cash, FD ledger
                    if (dvSource.Count == 0)
                    {
                        DataRow dr = dtFinancialPosition.NewRow();
                        dr[reportSetting1.FinancialPosition.PROJECTColumn.ColumnName] = project;
                        dr[reportSetting1.FinancialPosition.BANKColumn.ColumnName] = string.Empty;
                        dr[reportSetting1.FinancialPosition.LEDGER_NAMEColumn.ColumnName] = string.Empty;
                        dr[reportSetting1.FinancialPosition.OPERATED_BYColumn.ColumnName] = string.Empty;
                        dr[reportSetting1.FinancialPosition.PURPOSESColumn.ColumnName] = string.Empty;
                        dr[reportSetting1.FinancialPosition.CASH_AMOUNTColumn.ColumnName] = this.UtilityMember.NumberSet.ToDecimal(CashAmount.ToString());
                        dr[reportSetting1.FinancialPosition.BANK_AMOUNTColumn.ColumnName] = 0;
                        dr[reportSetting1.FinancialPosition.FD_AMOUNTColumn.ColumnName] = this.UtilityMember.NumberSet.ToDecimal(FDAmount.ToString());
                        dr[reportSetting1.FinancialPosition.PURPOSESColumn.ColumnName] = string.Empty; ;
                        CashAmount = 0; //Reset 
                        FDAmount = 0;  //Reset 
                        dtFinancialPosition.Rows.Add(dr);
                    }
                }
            }
            resultArgs.DataSource.Data = dtFinancialPosition;
            return resultArgs;
        }
        #endregion
    }
}
