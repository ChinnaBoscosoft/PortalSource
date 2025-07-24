using System;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BranchwiseBalance : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor

        public BranchwiseBalance()
        {
            InitializeComponent();
            this.SetTitleWidth(xrTblHeader.WidthF);
            //xrTblHeader.WidthF = xrTblProject.WidthF = xrTable1.WidthF = 675.25f;
        }


        #endregion
        #region Methods

        public override void ShowReport()
        {
            BindBranchwiseBalanceSource();
            SetReportTitle();
            xrCellCashOPHeader.Text = this.SetCurrencyFormat(xrCellCashOPHeader.Text);
            xrCellBankOPHeader.Text =this.SetCurrencyFormat(xrCellBankOPHeader.Text);
            xrCellFDOPHeader.Text = this.SetCurrencyFormat(xrCellFDOPHeader.Text);

            xrCellReceiptHeader.Text = this.SetCurrencyFormat(xrCellReceiptHeader.Text);
            xrCellPaymentHeader.Text = this.SetCurrencyFormat(xrCellPaymentHeader.Text);
            xrCellCashCLHeader.Text = this.SetCurrencyFormat(xrCellCashCLHeader.Text);
            xrCellBankCLHeader.Text = this.SetCurrencyFormat(xrCellBankCLHeader.Text);
            xrCellFDCLHeader.Text = this.SetCurrencyFormat(xrCellFDCLHeader.Text);

            Detail.SortFields.Clear();
            Detail.SortFields.Add(new DevExpress.XtraReports.UI.GroupField("SORT_ORDER", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending));
            //this.ReportPeriod = String.Format("For the Period: {0}", this.ReportProperties.DateAsOn);
            base.ShowReport();
        }
        private void BindBranchwiseBalanceSource()
        {
            try
            {
                resultArgs = FetchDashboardBranchDetails();
                if (resultArgs != null && resultArgs.DataSource != null)
                {
                    resultArgs.DataSource.Table.TableName = "BranchwiseBalance";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
        }

        private ResultArgs FetchDashboardBranchDetails()
        {
            try
            {
                string BranchwiseBalanceQueryPath = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.FetchBranchBalance);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.FetchBranchBalance, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

                    if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    
                    if (!string.IsNullOrEmpty(ReportProperties.Project) && ReportProperties.Project != "0")
                        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    
                                        
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BranchwiseBalanceQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }
        #endregion
    }
}
