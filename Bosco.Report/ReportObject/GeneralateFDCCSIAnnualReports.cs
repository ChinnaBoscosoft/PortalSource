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
using Bosco.Model.UIModel;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateFDCCSIAnnualReports : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        int RecordNumber = 0;
        SettingProperty settingProperty = new SettingProperty();
        #endregion

        #region Constructor
        public GeneralateFDCCSIAnnualReports()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReports
        public override void ShowReport()
        {
            RecordNumber = 0;
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
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
            RecordNumber = 0;
            SetReportTitle();
            ResultArgs resultArgs = GetReportSource();
            this.SetLandscapeHeader = xrTblCommunityInfo.WidthF;
            this.SetLandscapeFooter= xrTblCommunityInfo.WidthF;
            this.SetLandscapeFooterDateWidth = xrTblCommunityInfo.WidthF;
            this.HideReportHeader = this.HidePageHeader = false;
            this.HideHeaderFully= true;
            
            FillReportProperties();


            //Bind Management Activities
            if (grpManagementActivities.Visible)
            {
                GeneralateActivityIE managementActiviteis = xrSubManagementActivitites.ReportSource as GeneralateActivityIE;
                managementActiviteis.BindSource(true);

                //Bind Movement FA Activities
                if (grpMovementFA.Visible)
                {
                    GeneralateActivityIEFA movementFAActiviteis = xrSubMovementFA.ReportSource as GeneralateActivityIEFA;
                    movementFAActiviteis.BindSource(true, managementActiviteis.dtSource);
                }
                        
            }
                        
            //Bind Commercial Activities
            if (grpCommercialActivities.Visible)
            {
                GeneralateCommercial movementCommercial = xrSubCommercialActivities.ReportSource as GeneralateCommercial;
                movementCommercial.BindSource(true);
            }

            //Bind Patrimonial
            if (grpPatrimonial.Visible)
            {
                GeneralatePatrimonial movementPatrimonial = xrSubPatrimonial.ReportSource as GeneralatePatrimonial;
                movementPatrimonial.BindSource(true);
            }

            //Bind Reconciliazione
            if (grpReconciliazione.Visible)
            {
                GeneralateReconciliazione movementReconciliazione = xrSubReconciliazione.ReportSource as GeneralateReconciliazione;
                movementReconciliazione.BindSource(true);
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlIncomeExpenditure = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralatePatrimonial);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralatePatrimonial, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlIncomeExpenditure);
            }
            return resultArgs;
        }

        private void FillReportProperties()
        {
            xrCellDeadline.Text = xrcellProvince.Text = string.Empty;
            xrCellCommunity.Text = ReportProperties.BranchOfficeName;
            xrCellAddress.Text = xrCellTel.Text = xrCellEmailAddress.Text = string.Empty;

            Int32 branchid = UtilityMember.NumberSet.ToInteger(ReportProperties.BranchOffice);

            if (branchid > 0)
            {
                using (BranchOfficeSystem branchsystem = new BranchOfficeSystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    resultArgs = branchsystem.FillBranchOfficeDetails(branchid, DataBaseType.HeadOffice);

                    if (resultArgs.Success )
                    {
                        xrCellAddress.Text = branchsystem.Address;
                        xrCellTel.Text = branchsystem.PhoneNo;
                        //xrCellEmailAddress.Text = branchsystem.BranchEmail;
                    }
                }
            }

            //Hide Sub reports based on Reports ----------------------------------------------------------------
            //1. RPT-170 - General Annual Report (Show all Sub Reports except Commericial Activitis)
            //2. RPT-175 - Show Only Management Activitis Reports (Community Budget)
            //3. RPT-176 - Show Only Commericial Activitis Reports (Apostolic Activities Budget)
            //4. RPT-179 - Show Only Commericial Activitis Reports (Annual Apostolic Activities)
            grpHeader.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            grpManagementActivities.Visible = (this.ReportProperties.Current.ReportId == "RPT-170" || this.ReportProperties.Current.ReportId == "RPT-175");
            grpMovementFA.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            grpCommercialActivities.Visible = (this.ReportProperties.Current.ReportId == "RPT-176" || this.ReportProperties.Current.ReportId == "RPT-179");
            grpPatrimonial.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            grpReconciliazione.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            ReportFooter.Visible = (this.ReportProperties.Current.ReportId == "RPT-175" || this.ReportProperties.Current.ReportId == "RPT-176");
            //---------------------------------------------------------------------------------------------------
        }

        
        #endregion

        #region Events
        
        #endregion
    }
}
