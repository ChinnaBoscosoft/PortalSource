using System;
using System.Data;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraReports.UI;
namespace Bosco.Report.ReportObject
{
    public partial class FixedDepositStatement : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public FixedDepositStatement()
        {
            InitializeComponent();
            //     this.AttachDrillDownToRecord(xrtblBindData, xrFDAccountNo,
            //new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
            this.AttachDrillDownToRecord(xrtblBindData, xrFDAccountNo,
     new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFixedDeposit();

        }
        #endregion

        #region Method
        private void BindFixedDeposit()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                xrCellFDAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.FDAMOUNT);
                //this.ReportTitle = ReportProperty.Current.ReportTitle;
                //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.SetTitleWidth(xrtblHeader.WidthF);
                setHeaderTitleAlignment();
                //this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                resultArgs = GetReportSource();
                DataView dvBankReconciliation = resultArgs.DataSource.TableView;
                if (dvBankReconciliation != null)
                {
                    ////Show only active FDs
                    //dvBankReconciliation.RowFilter = reportSetting1.FixedDepositStatement.AMOUNTColumn.ColumnName + " >0";
                    //dvBankReconciliation.ToTable();

                    DataTable dtReport  = dvBankReconciliation.ToTable();
                    dtReport.TableName = "FixedDepositStatement";
                    this.DataSource = dtReport;
                    this.DataMember = dtReport.TableName;
                }
                base.ShowReport();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string FixedDepositStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FixedDepositStatement);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.FixedDepositStatement, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FixedDepositStatement);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblBindData = AlignContentTable(xrtblBindData);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
        }
        #endregion

        private void xrMaturedOn_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FixedDepositStatement.MATURITY_DATEColumn.ColumnName) != null)
            {
                string matDate = GetCurrentColumnValue(reportSetting1.FixedDepositStatement.MATURITY_DATEColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(matDate))
                {
                    XRTableCell cell = sender as XRTableCell;
                    DateTime mDate = UtilityMember.DateSet.ToDate(matDate.ToString(), false);
                    DateTime frmDate = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false);
                    DateTime toDate = UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false);

                    cell.Font = new System.Drawing.Font(cell.Font.FontFamily, cell.Font.Size, System.Drawing.FontStyle.Regular);
                    if ((DateTime.Compare(mDate, frmDate) >=0) && (DateTime.Compare(mDate, toDate) <= 0))
                    {
                        cell.Font = new System.Drawing.Font(cell.Font.FontFamily, cell.Font.Size, System.Drawing.FontStyle.Bold);
                    }
                }
            }
        }
    }
}
