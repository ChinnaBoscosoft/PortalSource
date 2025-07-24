using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using System.Web;
using Bosco.Model.UIModel;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateCommercial : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        int RecordNumber = 0;
        SettingProperty settingProperty = new SettingProperty();
        #endregion

        #region Constructor
        public GeneralateCommercial()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReports
        public override void ShowReport()
        {
            RecordNumber = 0;
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }

            //if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
            //    || String.IsNullOrEmpty(this.ReportProperties.DateTo)
            //    || this.ReportProperties.Project == "0")
            //{
            //    SetReportTitle();
            //    ShowReportFilterDialog();
            //}
            //else
            //{
            //    BindSource();
            //}
            
            SetReportTitle();
            base.ShowReport();
        }

        #endregion

        #region Methods
        public void BindSource(bool fromMasterReport=false)
        {
            RecordNumber = 0;
            SetReportTitle();
            // ResultArgs resultArgs = GetReportSource();
            this.SetLandscapeHeader = xrTblCommericialActivities.WidthF;
            this.SetLandscapeFooter = xrTblCommericialActivities.WidthF;
            this.SetLandscapeFooterDateWidth = xrTblCommericialActivities.WidthF;

            if (fromMasterReport)
            {
                this.HideReportHeader = this.HidePageHeader = this.HidePageFooter = false;
                this.HideHeaderFully = true;
                xrlblYear.Text = "Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            }

            FillReportProperties();
        }

        private void FillReportProperties()
        {
            double dHospitalPL = 0;
            double dInstitutionPL = 0;
            double dTechSchoolPL = 0;
            double dSocietyPL = 0;
            double dCollegesPL = 0;
            double dSocialWorkPL = 0;
            xrCellMedicalIncome.Borders = DevExpress.XtraPrinting.BorderSide.All;

            //For General Annaul reports, hide/remove budget columns //RPT-170
            xrlblYear.Text = (this.ReportProperties.Current.ReportId == "RPT-179" ? "Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString() : string.Empty);
            xrlblYear.Visible = (this.ReportProperties.Current.ReportId == "RPT-179");
            xrLblTitle.Text = "Name of the  House : " + ReportProperties.BranchOfficeName;
            xrTblSignFooter.Visible = (this.ReportProperties.Current.ReportId == "RPT-176");
            grpSignDetails.Visible = false; //(this.ReportProperties.Current.ReportId == "RPT-176");
            xrTblCommericalBudget.Visible = (this.ReportProperties.Current.ReportId == "RPT-176");
            lblBudgetTitle.Visible = (this.ReportProperties.Current.ReportId == "RPT-176" || this.ReportProperties.Current.ReportId == "RPT-179");
            xrRowActuallCaption.Visible =  xrRowBudgetCaption.Visible = (this.ReportProperties.Current.ReportId == "RPT-176");
            xrCellActualCaption.Text = "Actual Results " + this.UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year.ToString();
                        
            xrCellBudgetCaption.Text = "Budget " + (this.UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year + 1).ToString();
            if (this.ReportProperties.Current.ReportId == "RPT-179") //RPT-170
            {
                lblBudgetTitle.Text = "Annual Report - Apostolic Activities";
                //xrLblTitle.Text = "Commercial Activities";
                xrTblCommericialActivities.WidthF = this.PageWidth - 25;
                xrTblSignFooter.WidthF = xrTblfooter.WidthF = xrTblCommericialActivities.WidthF;
                xrcellfooterNode2.Text = " “Income, Expenditure and Balance” in other words please don’t add Previous Balance.";
            }
            else
            {
                lblBudgetTitle.Text = "Budget - Apostolic Activities";
                xrcellfooterNode2.Text = " “Income, Expenditure and Balance” in other words please don’t add Previous Balance";
                xrcellfooterNode2.Text += " and also in filling the Budget " + (this.UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year + 1).ToString() + ".";
                xrRowBalanceBudget.HeightF = xrIncomeExpenditureBudget1.HeightF = xrIncomeExpenditureBudget.HeightF = 150;
                xrRowExpenditureBudget.HeightF = xrRowExpenditureBudget1.HeightF = xrRowBudgetEmpty2.HeightF = 150;
                xrRowBudgetEmpty2.HeightF = 150;

                xrRowExpenditureBudget.HeightF = 29;
                xrRowExpenditureBudget1.HeightF = 29; //42;

                xrRowBudgetEmpty2.HeightF = 25;
                xrIncomeExpenditureBudget.HeightF = 29;
                xrIncomeExpenditureBudget1.HeightF = 29; //42;
                xrRowBalanceBudget.HeightF = 25;

                xrTblCommericialActivities.TopF = xrTblCommericalBudget.TopF = lblBudgetTitle.TopF + lblBudgetTitle.HeightF;

                xrTblCommericialActivities.HeightF = 845;
                xrTblCommericalBudget.HeightF = xrTblCommericialActivities.HeightF + 41;

                xrTblSignFooter.TopF = xrTblCommericialActivities.TopF + xrTblCommericialActivities.HeightF +10;
                xrTblfooter.TopF = xrTblSignFooter.TopF + xrTblSignFooter.HeightF+ 25;
            }
            //xrLnMedicalIncomeService.WidthF = xrCellMedicalIncome.WidthF;
            //xrLnSchoolIncome.WidthF = xrCellSchoolsIncome.WidthF;
            //xrLnTechnicalSchoolIncome.WidthF = xrCellTecSchoolIncome.WidthF;
            //xrLnUniverysityIncome.WidthF = xrCellCollegesIncome.WidthF;
            //xrlnOthersSocialIncome.WidthF = xrCellSocialIncome.WidthF;


           //#05/02/2020 for Temp purpose, we take IE amount for every Project Category
           //It has to be changed as ProjectCategory-wise IE report

            //#. Get A Medical Services ( Hospitals, clinics etc.) Projects IE :: Project Category -  Hospital
            string HospitalProjects = GetCommunityProjectIds("Hospital");
            ResultArgs IEResultArg = getIEBasedData(HospitalProjects);
            if (IEResultArg.Success && IEResultArg.DataSource.Table!=null)
            {
                DataTable dtHospital= IEResultArg.DataSource.Table;
                object objAmouunt = dtHospital.Compute("SUM(AMOUNT)","AMOUNT>0");
                double dHospitalIncome = UtilityMember.NumberSet.ToDouble(dtHospital.Compute("SUM(AMOUNT)", "AMOUNT>0").ToString());
                double dHospitalExpense = UtilityMember.NumberSet.ToDouble(dtHospital.Compute("SUM(AMOUNT) * -1", "AMOUNT<0").ToString());
                dHospitalPL = dHospitalIncome - dHospitalExpense;
                xrCellMedicalIncome.Text = UtilityMember.NumberSet.ToNumber(dHospitalIncome);
                xrCellMedicalExpense.Text = UtilityMember.NumberSet.ToNumber(dHospitalExpense);
                xrCellMedicalPL.Text = UtilityMember.NumberSet.ToNumber(dHospitalPL);
            }

            //#. Get B Schools Projects IE :: Project Category -  Institution
            string InstitutionProjects = GetCommunityProjectIds("Institution");
            IEResultArg = getIEBasedData(InstitutionProjects);
            if (IEResultArg.Success && IEResultArg.DataSource.Table != null)
            {
                DataTable dtInstitution = IEResultArg.DataSource.Table;
                double dInstitutionIncome = UtilityMember.NumberSet.ToDouble(dtInstitution.Compute("SUM(AMOUNT)", "AMOUNT>0").ToString());
                double dInstitutionExpense = UtilityMember.NumberSet.ToDouble(dtInstitution.Compute("SUM(AMOUNT) * -1", "AMOUNT<0").ToString());
                dInstitutionPL = dInstitutionIncome - dInstitutionExpense;
                xrCellSchoolsIncome.Text = UtilityMember.NumberSet.ToNumber(dInstitutionIncome);
                xrCellSchoolsExpense.Text = UtilityMember.NumberSet.ToNumber(dInstitutionExpense);
                xrCellSchoolsPL.Text = UtilityMember.NumberSet.ToNumber(dInstitutionPL);
            }

            //#. Get C Technical Schools or Courses Projects IE :: Project Category -  Technical
            string TechSchoolsProjects = GetCommunityProjectIds("Technical");
            IEResultArg = getIEBasedData(TechSchoolsProjects);
            if (IEResultArg.Success && IEResultArg.DataSource.Table != null)
            {
                DataTable dtTechSchools = IEResultArg.DataSource.Table;
                double dtTechSchoolsIncome = UtilityMember.NumberSet.ToDouble(dtTechSchools.Compute("SUM(AMOUNT)", "AMOUNT>0").ToString());
                double dtTechSchoolsExpense = UtilityMember.NumberSet.ToDouble(dtTechSchools.Compute("SUM(AMOUNT) * -1", "AMOUNT<0").ToString());
                dTechSchoolPL = dtTechSchoolsIncome - dtTechSchoolsExpense;
                xrCellTecSchoolIncome.Text = UtilityMember.NumberSet.ToNumber(dtTechSchoolsIncome);
                xrCellTecSchoolExpense.Text = UtilityMember.NumberSet.ToNumber(dtTechSchoolsExpense);
                xrCellTecSchoolPL.Text = UtilityMember.NumberSet.ToNumber(dTechSchoolPL);
            }

            //#. Get D Retirments Homes Projects IE :: Project Category -  Technical
            string SocietyProjects = GetCommunityProjectIds("Society");
            IEResultArg = getIEBasedData(SocietyProjects);
            if (IEResultArg.Success && IEResultArg.DataSource.Table != null)
            {
                DataTable dtSocietyProjects = IEResultArg.DataSource.Table;
                double dtSocietyProjectsIncome = UtilityMember.NumberSet.ToDouble(dtSocietyProjects.Compute("SUM(AMOUNT)", "AMOUNT>0").ToString());
                double dtSocietyProjectsExpense = UtilityMember.NumberSet.ToDouble(dtSocietyProjects.Compute("SUM(AMOUNT) * -1", "AMOUNT<0").ToString());
                dSocietyPL = dtSocietyProjectsIncome - dtSocietyProjectsExpense;
                xrCellRetirementsHomesIncome.Text = UtilityMember.NumberSet.ToNumber(dtSocietyProjectsIncome);
                xrCellRetirementsHomesExpense.Text = UtilityMember.NumberSet.ToNumber(dtSocietyProjectsExpense);
                xrCellRetirementsPL.Text = UtilityMember.NumberSet.ToNumber(dSocietyPL);
            }

            //#. Get E University/College hostels Projects IE :: Project Category -  College
            string CollegeProjects = GetCommunityProjectIds("College");
            IEResultArg = getIEBasedData(CollegeProjects);
            if (IEResultArg.Success && IEResultArg.DataSource.Table != null)
            {
                DataTable dtCollege = IEResultArg.DataSource.Table;
                double dCollegeIncome = UtilityMember.NumberSet.ToDouble(dtCollege.Compute("SUM(AMOUNT)", "AMOUNT>0").ToString());
                double dCollegeExpense = UtilityMember.NumberSet.ToDouble(dtCollege.Compute("SUM(AMOUNT) * -1", "AMOUNT<0").ToString());
                dCollegesPL = dCollegeIncome - dCollegeExpense;
                xrCellCollegesIncome.Text = UtilityMember.NumberSet.ToNumber(dCollegeIncome);
                xrCellCollegesExpense.Text = UtilityMember.NumberSet.ToNumber(dCollegeExpense);
                xrCellCollegesPL.Text = UtilityMember.NumberSet.ToNumber(dCollegesPL);
            }

            //#. Get F Others (Social Work) Projects IE :: Project Category -  Social Work
            string SocialWorkProjects = GetCommunityProjectIds("Social Work");
            IEResultArg = getIEBasedData(SocialWorkProjects);
            if (IEResultArg.Success && IEResultArg.DataSource.Table != null)
            {
                DataTable dtSocialWork = IEResultArg.DataSource.Table;
                double dSocialWorkIncome = UtilityMember.NumberSet.ToDouble(dtSocialWork.Compute("SUM(AMOUNT)", "AMOUNT>0").ToString());
                double dSocialWorkExpense = UtilityMember.NumberSet.ToDouble(dtSocialWork.Compute("SUM(AMOUNT) * -1", "AMOUNT<0").ToString());
                dSocialWorkPL = dSocialWorkIncome - dSocialWorkExpense;
                xrCellSocialIncome.Text = UtilityMember.NumberSet.ToNumber(dSocialWorkIncome);
                xrCellSocialExpense.Text = UtilityMember.NumberSet.ToNumber(dSocialWorkExpense);
                xrCellSocialPL.Text = UtilityMember.NumberSet.ToNumber(dSocialWorkPL);
            }

            xrCellPL.Text = UtilityMember.NumberSet.ToNumber(dHospitalPL + dInstitutionPL + dTechSchoolPL + dSocietyPL + dCollegesPL + dSocialWorkPL);
        }
        
        /// <summary>
        /// To get Community ProjectsId
        /// </summary>
        /// <returns></returns>
        private string GetCommunityProjectIds(string ProjectCategory)
        {
            string Rtn = string.Empty;
            Int32 CommunityProjectCategoryId = 0;

            if (HttpContext.Current.Session["ProjectSource"] != null)
            {
                //# Get Project Category Community Id 
                using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                {
                    ResultArgs resultArgs = projectCategorySystem.ProjectCatogoryDetailsByName(ProjectCategory, DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProjectCategory = resultArgs.DataSource.Table;
                        CommunityProjectCategoryId = UtilityMember.NumberSet.ToInteger(dtProjectCategory.Rows[0][projectCategorySystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName].ToString());
                    }
                }

                //# Get Community Projects
                DataTable dCommunityProjects = ((DataTable)HttpContext.Current.Session["ProjectSource"]).DefaultView.ToTable();
                if (dCommunityProjects != null)
                {
                    dCommunityProjects.DefaultView.RowFilter = "PROJECT_CATEGORY_ID = " + CommunityProjectCategoryId;
                    dCommunityProjects = dCommunityProjects.DefaultView.ToTable();
                    foreach (DataRow dr in dCommunityProjects.Rows)
                    {
                        Rtn += dr["PROJECT_ID"].ToString() + ",";
                    }
                }
            }
            Rtn = Rtn.TrimEnd(',').Trim();

            if (String.IsNullOrEmpty(Rtn))
            {
                Rtn = "0";
            }
            return Rtn;
        }

        private ResultArgs getIEBasedData(string ProjectCategoryProjects)
        {
            ResultArgs FinalIEResultArg = new ResultArgs();
            string sqlFinalIE = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalIncomeExpenditure);
            object sqlCommandId = string.Empty;
            using (DataManager dataManager = new DataManager(sqlCommandId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ProjectCategoryProjects);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.settingProperty.BookBeginFrom);
                
                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, ReportProperties.ShowByLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, ReportProperties.ShowByLedgerGroup);

                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                FinalIEResultArg = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlFinalIE);
            }
            return FinalIEResultArg;
        }

        #endregion

        /*
        private void xrRowExpenditureBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 29;
        }

        private void xrRowExpenditureBudget1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 42;
        }

        private void xrIncomeExpenditureBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 30;
        }

        private void xrIncomeExpenditureBudget1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 30;
        }

        private void xrRowBalanceBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 30;
        }

        private void xrRowNBPLBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 10;
        }
              

        private void xrRowEmptyBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 15;
        }

        private void xrRowPLBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableRow row = sender as XRTableRow;
            row.HeightF = 26;
        }

        private void xrcellBRSLedgerNameBudget28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.HeightF = 42;
        }*/
        
    }
}
