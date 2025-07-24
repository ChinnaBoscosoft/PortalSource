using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class DrillDownReport : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        decimal subGrandTotal = 0;
        string VoucherType = "";
        Int32 GrpId = 0;
        string CostCenter = string.Empty;
        #endregion

        #region Constructor
        public DrillDownReport()
        {
            InitializeComponent();
            ArrayList alDrillproperties = new ArrayList { reportSetting1.DrillDownReport.PARTICULARS_IDColumn.ColumnName };
            if (this.ReportProperties.DrillDownProperties != null && this.ReportProperties.DrillDownProperties.Count > 1)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                switch (ddtypeLinkType)
                {
                    case DrillDownType.GROUP_SUMMARY_RECEIPTS:
                        VoucherType = "RC";
                        break;
                    case DrillDownType.GROUP_SUMMARY_PAYMENTS:
                        VoucherType = "PY";
                        break;
                }

                if (dicDDProperties.ContainsKey("GROUP_ID"))
                    GrpId = UtilityMember.NumberSet.ToInteger(dicDDProperties["GROUP_ID"].ToString());

                if (dicDDProperties.ContainsKey("PARTICULARS_ID"))
                    GrpId = UtilityMember.NumberSet.ToInteger(dicDDProperties["PARTICULARS_ID"].ToString());

                if (dicDDProperties.ContainsKey(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName))
                {
                    CostCenter = dicDDProperties[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString();
                    alDrillproperties.Add(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName);
                }

            }
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindReceiptSource();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private ResultArgs GetReportSource()
        {
            string sqlMonthlyAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.DrillDownReport);
            //string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateTo);
            //string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Report.DrillDownReport, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, GrpId);
                if (!string.IsNullOrEmpty(CostCenter))
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, CostCenter);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }
        public void BindReceiptSource()
        {
            SetReportTitle();
            this.ReportTitle = GetGroupName(GrpId);
            resultArgs = GetReportSource();
            DataView dvReceipt = resultArgs.DataSource.TableView;

            if (dvReceipt != null)
            {
                dvReceipt.Table.TableName = "DrillDownReport";
                this.DataSource = dvReceipt;
                this.DataMember = dvReceipt.Table.TableName;
            }
        }
        #endregion

        private void xrParticulars_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView dvrow = GetCurrentRow() as DataRowView;
            XRTableCell FldParticular = ((XRTableCell)sender);
            if (dvrow != null)
            {
                DrillDownType recordType = ((DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dvrow["PARTICULAR_TYPE"].ToString()));
                if (recordType == DrillDownType.GROUP_SUMMARY ||
                    recordType == DrillDownType.GROUP_SUMMARY_RECEIPTS ||
                    recordType == DrillDownType.GROUP_SUMMARY_PAYMENTS)
                {
                    FldParticular.Font = new Font(FldParticular.Font, FontStyle.Bold);
                }
                else
                {
                    FldParticular.Font = new Font(FldParticular.Font, FontStyle.Regular);
                }
                xrParticulars.NavigateUrl = PagePath.ReportPath + "?rid=" + this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(recordType) +
"&hdva=true&DrillDownType=" + recordType.ToString() + "&FNAME=" + reportSetting1.DrillDownReport.PARTICULARS_IDColumn.ColumnName +
"&FVALUE=" + GetCurrentColumnValue(reportSetting1.DrillDownReport.PARTICULARS_IDColumn.ColumnName);
                xrParticulars.Target = "_self";
            }

          



        }

    }
}
