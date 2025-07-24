using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
namespace Bosco.Report.ReportObject
{
    public partial class ExpenditureIncomewithPreviousYear : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Construnctor
        public ExpenditureIncomewithPreviousYear()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindPreviousExpenditureIncomeSource();
            base.ShowReport();
        }
        #endregion

        #region Method
        private void BindPreviousExpenditureIncomeSource()
        {
            try
            {
                this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                HeaderExpenditureAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);
                HeaderIncomeAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);
                HeaderPreviousIncome.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.PREVIOUS);
                HeaderPreviousExpenditure.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.PREVIOUS);
                this.ReportSubTitle = objReportProperty.ProjectTitle;
                if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
                {
                    ShowReportFilterDialog();
                }
                else
                {
                    ResultArgs resultArgs = SetPreviousExpenditureIncomeSource();
                    DataView dvReceiptsPayments = resultArgs.DataSource.TableView;
                    if (dvReceiptsPayments != null)
                    {
                        dvReceiptsPayments.Table.TableName = "FinalReceiptsPayments";
                        this.DataSource = dvReceiptsPayments;
                        this.DataMember = dvReceiptsPayments.Table.TableName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }
        private ResultArgs SetPreviousExpenditureIncomeSource()
        {
            string PreviousExpenditureIncome = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.ReceiptsPayments);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.ReceiptsPayments,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, PreviousExpenditureIncome);
            }
            return resultArgs;
        }
        #endregion
    }
}
