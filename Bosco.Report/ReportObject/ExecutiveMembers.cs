using System.Data;

using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Report.ReportObject
{
    public partial class ExecutiveMembers : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ExecutiveMembers()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindExecutiveMembers();
            base.ShowReport();
            //  ShowReportFilterDialog();
        }
        #endregion

        #region Method
        private void BindExecutiveMembers()
        {
            //SplashScreenManager.ShowForm(typeof(frmReportWait));
            //  this.ReportTitle = this.ReportProperties.ReportTitle;
            SetReportTitle();
            setHeaderTitleAlignment();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            //this.ReportSubTitle = this.ReportProperties.ProjectTitle;
            DataTable dtexeutive = GetReportSource();
            if (dtexeutive != null)
            {
                this.DataSource = dtexeutive;
                this.DataMember = dtexeutive.TableName;
            }
            SetReportBorder();
          //  SplashScreenManager.CloseForm();
        }
        private DataTable GetReportSource()
        {
            string ExecutiveMembers = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.ExecutiveMembers);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.ForeginContribution.ExecutiveMembers,DataBaseType.HeadOffice))
            {
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, ExecutiveMembers);
            }
            return resultArgs.DataSource.Table;
        }
        private void SetReportBorder()
        {
            xrTable1 = SetHeadingTableBorder(xrTable1, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblExecutiveMembers = SetBorders(xrtblExecutiveMembers, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);            
        }
    }
        #endregion
}
