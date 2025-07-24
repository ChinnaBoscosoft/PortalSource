using System;

using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;


namespace Bosco.Report.ReportObject
{
    public partial class FC6Purpose : Bosco.Report.Base.ReportBase
    {

        #region VariableDeclaration
        ResultArgs resultArgs = null;
        public double DonorTotal = 0.0;
        #endregion

        #region Constructor
        public FC6Purpose()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFC6purpose();            
        }
        #endregion

        #region Method
        private void BindFC6purpose()
        {
            GetReportDonorSource();
            base.ShowReport();
        }
        private void GetReportDonorSource()
        {
            try
            {
                resultArgs = null;
                string Donor = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6Donor);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.ForeginContribution.FC6Donor,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Donor);

                    DataView dvDonor = resultArgs.DataSource.TableView;
                    if (dvDonor != null && dvDonor.Count != 0)
                    {
                        dvDonor.Table.TableName = "FC6DONORLIST";
                        this.DataSource = dvDonor;
                        this.DataMember = dvDonor.Table.TableName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        #endregion

        
        #region Events
        
        #endregion

    }
}
