using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class AccountBalanceCode : Report.Base.ReportBase
    {
        ResultArgs resultArgs = null;

        public double PeriodBalanceAmount { get; set; }
        public double ProgressiveBalanceAmount { get; set; }

        public float LeftPosition
        {
            set
            {
                xrTableLedgerGroup.LeftF = value;
                xrtblLedger.LeftF = value;
            }
        }
        public float CodeHeaderColumWidth
        {
            set
            {
                //      xrcelGroupCode.WidthF = value;
            }
        }

        public float CodeColumnWidth
        {
            set
            {
                //       xrcelLedgerCode.WidthF = value;
            }
        }
        public float NameHeaderColumWidth
        {
            set { tcGroupName.WidthF = value; }
        }
        public float NameColumnWidth
        {
            set
            {
                tcLedgerName.WidthF = value;
            }
        }
        public float AmountHeaderColumWidth
        {
            set { tcGroupAmountPeriod.WidthF = value; }
        }
        public float AmountColumnWidth
        {
            set
            {
                tcAmountPeriod.WidthF = value;
            }
        }
        public float AmountProgressiveHeaderColumnWidth
        {
            set { tcGroupAmountProgress.WidthF = value; }
        }

        public float AmountProgressiveColumnWidth
        {
            set
            {
                tcAmountProgress.WidthF = value;
            }
        }

        public float GroupCode
        {
            set
            {
                //     xrcelGroupCode.WidthF = value;
            }
        }
        public float GroupNameWidth
        {
            set { tcGroupName.WidthF = value; }
        }
        public float GroupAmountWidth
        {
            set { tcGroupAmountPeriod.WidthF = value; }
        }

        public bool GroupCodeVisible
        {
            set
            {
                //     xrcelGroupCode.Visible = value;
            }
        }

        public bool GroupProgressVisible
        {
            set { tcGroupAmountProgress.Visible = value; }
        }

        public bool ProgressAmountVisible
        {
            set { tcAmountProgress.Visible = value; }
        }

        public bool AmountProgressVisible
        {
            set
            {
                tcAmountProgress.Visible = value;
            }
        }


        public AccountBalanceCode()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            base.ShowReport();
        }

        private ResultArgs GetBalance(string balDate, string projectIds, string groupIds)
        {

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            } return resultArgs;
        }

        public void BindBalance(bool isOpBalance, bool isProgressive, bool IsReportAsOn = false)
        {
            string dateFrom = ReportProperties.DateFrom;
            string dateTo = ReportProperties.DateTo;
            if (IsReportAsOn)
            {
                dateFrom = ReportProperties.DateAsOn;
                dateTo = ReportProperties.DateAsOn;
            }
            string balDate = "";
            string progressBalDate = GetProgressiveDate(dateFrom);
            string projectIds = ReportProperties.Project;
            string groupIds = this.GetLiquidityGroupIds();

            double amtPeriod = 0;
            double amtProgress = 0;
            double totAmtPeriod = 0;
            double totAmtProgress = 0;

            if (dateTo == "") { dateTo = ReportProperties.DateAsOn; }

            if (isOpBalance)
            {
                DateTime dateBalance = DateTime.Parse(dateFrom).AddDays(-1);
                balDate = dateBalance.ToShortDateString();

                if (isProgressive)
                {
                    DateTime dateProgBalance = DateTime.Parse(progressBalDate).AddDays(-1);
                    progressBalDate = dateProgBalance.ToShortDateString();
                }
            }
            else
            {
                balDate = dateTo;
                progressBalDate = balDate;
            }


            resultArgs = GetBalance(balDate, projectIds, groupIds);
            DataTable dtBalance = resultArgs.DataSource.Table;
            DataView dvBalance = dtBalance.DefaultView;

            if (dvBalance != null && isProgressive)
            {
                dtBalance.Columns.Add(reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName, typeof(double));

                ResultArgs result = GetBalance(progressBalDate, projectIds, groupIds);
                DataTable dtProgressBalance = result.DataSource.Table;
                DataView dvProgressBalance = dtProgressBalance.DefaultView;
                string transMode = "";
                string transModeProgress = "";
                double progressAmt = 0;

                foreach (DataRowView drvBalance in dvBalance)
                {
                    dvProgressBalance.RowFilter = reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName +
                           " = " + drvBalance[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString();

                    if (dvProgressBalance.Count > 0)
                    {
                        drvBalance.BeginEdit();
                        transMode = drvBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                        transModeProgress = dvProgressBalance[0][reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                        progressAmt = UtilityMember.NumberSet.ToDouble(dvProgressBalance[0][reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());

                        if (transMode == TransactionMode.CR.ToString())
                        {
                            drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] =
                                -UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                        }

                        if (transModeProgress == TransactionMode.CR.ToString())
                        {
                            progressAmt = -progressAmt;
                        }

                        drvBalance[reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName] = progressAmt;
                        drvBalance.EndEdit();
                    }

                    dvProgressBalance.RowFilter = "";
                }
            }

            //Calculate Sum of Balance
            foreach (DataRowView drvBalance in dvBalance)
            {
                amtPeriod = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());

                if (isProgressive)
                {
                    amtProgress = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName].ToString());
                }

                totAmtPeriod += amtPeriod;
                totAmtProgress += amtProgress;
            }

            PeriodBalanceAmount = totAmtPeriod;
            ProgressiveBalanceAmount = totAmtProgress;
            SetReportSetting(dvBalance, isProgressive);

            if (dvBalance != null)
            {
                dvBalance.Table.TableName = "AccountBalance";
                this.DataSource = dvBalance;
                this.DataMember = dvBalance.Table.TableName;
            }
        }

        private void SetReportSetting(DataView dvBalance, bool isProgressive)
        {
            //    float actualCodeWidth = xrcelGroupCode.WidthF;
            dvBalance.RowFilter = "";
            if (ReportProperties.IncludeAllLedger == 0)
            {
                dvBalance.RowFilter = reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + " <> 0";

                if (dvBalance.Table.Columns.Contains(reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName))
                {
                    dvBalance.RowFilter += " OR " + reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName + " <> 0";
                }
            }

            ReportProperties.ShowByLedger = ReportProperties.ShowDetailedBalance == 1 ? 1 : ReportProperties.ShowByLedger == 1 ? 1 : 0;
            //Include / Exclude Progressive total Column

            //if (xrcelGroupCode.Tag != null && xrcelGroupCode.Tag.ToString() != "")
            //{
            //    actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrcelGroupCode.Tag.ToString());
            //}
            //else
            //{
            //    xrcelGroupCode.Tag = xrcelGroupCode.WidthF;
            //}

            //xrcelGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            //xrcelLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            tcGroupAmountProgress.Visible = (isProgressive == true);
            tcAmountProgress.Visible = (isProgressive == true);

            //Include / Exclude Ledger group or Ledger
            grpBalanceGroup.Visible = (ReportProperties.ShowByLedger == 1);
            grpBalanceLedger.Visible = (ReportProperties.ShowDetailedBalance == 1 && ReportProperties.ReportId!="RPT-079");
            if (ReportProperties.ShowDetailedBalance == 1 && ReportProperties.ReportId != "RPT-079")
            {
                ReportProperties.ShowByLedger = 1;
                grpBalanceGroup.Visible = true;
            }
            grpBalanceGroup.GroupFields[0].FieldName = "";
            grpBalanceLedger.GroupFields[0].FieldName = "";

            if (grpBalanceGroup.Visible == false && grpBalanceLedger.Visible == false)
            {
                grpBalanceGroup.Visible = true;
            }

            if (grpBalanceGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpBalanceGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpBalanceGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
            }

            if (grpBalanceLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpBalanceLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpBalanceLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
            }

            xrTableLedgerGroup = SetLedgerGroupeBorders(xrTableLedgerGroup);
            xrtblLedger = SetTableBorder(xrtblLedger);

        }

        public void SetClosingBalanceWidth()
        {
            tcGroupName.WidthF = (float)221.3;
            tcGroupAmountPeriod.WidthF = (float)135.7;
            tcGroupAmountProgress.WidthF = 0;
            tcGroupAmountProgress.Visible = false;
        }
        public void setClosingBalanceDetailWidth()
        {
            //  xrcelGroupCode.WidthF = (float)52.06;
            //tcGroupName.WidthF = (float)155.23;
            //tcGroupAmountPeriod.WidthF = (float)150.51;

            tcLedgerName.WidthF = (float)221.3;
            tcAmountPeriod.WidthF = (float)135.7;
            tcAmountProgress.WidthF = 0;
            tcAmountProgress.Visible = false;

            //tcGroupAmountProgress.Visible = false;
        }
        public void setCBCClosingBalanceDetailWidth()
        {
            //    xrcelGroupCode.WidthF = (float)85.06;
            tcLedgerName.WidthF = (float)200.25;
            tcAmountPeriod.WidthF = (float)150.00;
            tcAmountProgress.WidthF = (float)5.2;
            tcAmountProgress.Visible = false;
        }

        public void SetCBWidth(float ledger, float amount, float progressiveAmount, bool ProgressiveAmountVisible)
        {
            tcLedgerName.WidthF = ledger;
            tcGroupName.WidthF = ledger;

            tcGroupAmountPeriod.WidthF = amount;
            tcAmountPeriod.WidthF = amount;

            tcGroupAmountProgress.WidthF = progressiveAmount;
            tcAmountProgress.WidthF = progressiveAmount;

            tcGroupAmountProgress.Visible = tcAmountProgress.Visible = ProgressiveAmountVisible;

        }
    }
}
