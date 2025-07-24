using System;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateLedgerDetails : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public GeneralateLedgerDetails()
        {
            InitializeComponent();
        }

        #endregion

        #region Show Report
        public override void ShowReport()
        {
            SetReportTitle();
            CongregationReport();
            this.HideDateRange = false;
            base.ShowReport();
        }
        #endregion

        #region Methods
        private void CongregationReport()
        {
            ResultArgs resultArgs = GetReportSource();
            DataView dvLedgers = resultArgs.DataSource.TableView;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.ReportSubTitle = string.Empty;
            this.ReportBranchName = string.Empty;
            this.CosCenterName = string.Empty;

            if (dvLedgers != null)
            {
                dvLedgers.Table.TableName = "CongiregationProfitandLoss";
                this.DataSource = dvLedgers;
                this.DataMember = dvLedgers.Table.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string sqlGeneralateMapUnmapLedger = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.FetchGeneralateMapUnmapLedger);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.FetchGeneralateMapUnmapLedger, DataBaseType.HeadOffice))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlGeneralateMapUnmapLedger);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrtblCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
