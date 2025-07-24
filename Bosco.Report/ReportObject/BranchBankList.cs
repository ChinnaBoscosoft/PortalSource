using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BranchBankList : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public BranchBankList()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            SetReportTitle();
            this.HideDateRange = false;
            BindReceiptSource();
            base.ShowReport();
        }

        #endregion

        #region Methods

        public void BindReceiptSource()
        {
            ResultArgs resultArgs = GetReportSource();
            DataView dvProjectBank = resultArgs.DataSource.TableView;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.ReportSubTitle = string.Empty;
         //   this.ReportBranchName = string.Empty;
            this.CosCenterName = string.Empty;

            if (dvProjectBank != null)
            {
                dvProjectBank.Table.TableName = "MonthWiseLedger";
                this.DataSource = dvProjectBank.ToTable();
                this.DataMember = dvProjectBank.Table.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlProjectBankList = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.BranchBankList);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.BranchBankList, DataBaseType.HeadOffice))
            {
                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlProjectBankList);
            }
            return resultArgs;
        }

        #endregion
    }
}
