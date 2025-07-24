using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BranchWiseLedgerReport : ReportHeaderBase
    {
        private bool rowemptyremoved = false;
        public BranchWiseLedgerReport()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 980.25f;
            this.SetLandscapeFooter = 980.25f;
            xrpgfReceiptBranch.Width = xrpgfReceiptBranch.Width - 10;
            xrpgfReceiptLedger.Width = xrpgfReceiptLedger.Width + 90;

            xrpgfPaymentBranch.Width = xrpgfPaymentBranch.Width - 10;
            xrpgfPaymentLedger.Width = xrpgfPaymentLedger.Width + 90;
        }
        public override void ShowReport()
        {
            ShowLedgerComparativeReport();
            base.ShowReport();
        }
        public void ShowLedgerComparativeReport()
        {

            xrcellBranchInfo.Text = string.Empty;
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                //this.ReportAmountLakh = true;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.SetLandscapeFooterDateWidth = 817.25f;
                setHeaderTitleAlignment();
                SetReportTitle();
                this.ReportBranchName = string.Empty;
                rowemptyremoved = false;
                ResultArgs resultArgs = GetReportSource();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "MultiAbstract";
                    DataTable dtRpt = resultArgs.DataSource.Table;
                    DataTable dtBranches =  dtRpt.DefaultView.ToTable(true, new string[] { "BRANCH_OFFICE_ID", "BRANCH_OFFICE_CODE", "BRANCH_OFFICE_NAME" });
                    dtBranches.DefaultView.Sort = "BRANCH_OFFICE_NAME";

                    dtRpt.DefaultView.RowFilter = "RECEIPT_AMOUNT > 0";
                    DataTable dtReceipt = dtRpt.DefaultView.ToTable();
                    dtReceipt.Columns["RECEIPT_AMOUNT"].ColumnName = "AMOUNT";

                    dtRpt.DefaultView.RowFilter = "";
                    dtRpt.DefaultView.RowFilter = "PAYMENT_AMOUNT > 0";
                    DataTable dtPayment = dtRpt.DefaultView.ToTable();
                    dtPayment.Columns["PAYMENT_AMOUNT"].ColumnName = "AMOUNT";

                    string branchname = string.Empty + Environment.NewLine;
                    foreach (DataRowView drv in dtBranches.DefaultView)
                    {
                        Int32 bid = UtilityMember.NumberSet.ToInteger(drv["BRANCH_OFFICE_ID"].ToString());
                        string bcode = drv["BRANCH_OFFICE_CODE"].ToString();
                        string boname = drv["BRANCH_OFFICE_NAME"].ToString();
                        branchname += bcode + " - " + boname + Environment.NewLine;

                        //To have common columns even branch doest have receipts
                        UpdateEmptyBranch(dtReceipt, bid, bcode, boname);
                        UpdateEmptyBranch(dtPayment, bid, bcode, boname);
                    }

                    //xrcellBranchInfo.Text = branchname;
                    rowemptyremoved = false;
                    //Receipt
                    this.xrPivotGrid1.DataSource = dtReceipt;// resultArgs.DataSource.Table;
                    this.xrPivotGrid1.DataMember = resultArgs.DataSource.Table.TableName;
                                        
                    //Payment
                    this.xrPivotGrid2.DataSource = dtPayment;
                    this.xrPivotGrid2.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
        }

        private ResultArgs UpdateEmptyBranch(DataTable dt, Int32 bid, string bcode, string boname)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                dt.DefaultView.RowFilter = string.Empty;
                dt.DefaultView.RowFilter = "BRANCH_OFFICE_ID = " + bid;
                if (dt.DefaultView.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["BRANCH_OFFICE_ID"] = bid;
                    dr["BRANCH_OFFICE_CODE"] = bcode;
                    dr["BRANCH_OFFICE_NAME"] = boname;
                    dr["LEDGER_ID"] = 0;
                    dr["LEDGER_NAME"] = string.Empty;
                    dr["AMOUNT"] = 0;
                    dt.Rows.Add(dr);
                }
                dt.DefaultView.RowFilter = string.Empty;

                resultArgs.DataSource.Data = dt;
                resultArgs.Success = true;
            }
            catch (Exception err)
            {
                dt.DefaultView.RowFilter = string.Empty;
                resultArgs.DataSource.Data = dt;
                resultArgs.Success = true;
            }

            return resultArgs;
        }

        private ResultArgs GetReportSource()
        {
            string projectcategoryids = "0";
            string projectids = "0";
            if (this.ReportProperties.ProjectCategory != null && !string.IsNullOrEmpty(this.ReportProperties.ProjectCategory))
            {
                projectcategoryids = this.ReportProperties.ProjectCategory;
            }
            //projectids = this.GetProjectIdsByProjectCategory(projectcategoryids);
            
            ResultArgs resultArgs = null;
            string sqlMultiAbstractPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BranchWiseLedgerComparative);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.BranchWiseLedgerComparative, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_CATOGORY_IDColumn, projectcategoryids);
                //dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectids);
                
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractPayments);
            }
            return resultArgs;
        }

        private void xrPivotGrid1_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == xrpgfReceiptMonth.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.MONTH_NAMEColumn.ColumnName).ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.MONTH_NAMEColumn.ColumnName).ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void xrPivotGrid2_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == xrpgfPaymentMonth.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.MONTH_NAMEColumn.ColumnName).ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.MONTH_NAMEColumn.ColumnName).ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void xrPivotGrid1_CustomFieldValueCells(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs e)
        {
            //If particular branch does not contain data, empty row will be displayed, it should be removed
            if (!rowemptyremoved)
            {
                bool isrowempty = false;
                for (int j = 0; j < e.ColumnCount; j++)
                {
                    DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);
                    if (cell != null && cell.Field != null && cell.Field.FieldName == "LEDGER_NAME")
                    {
                        isrowempty = string.IsNullOrEmpty(cell.Value.ToString().Trim());
                        break;
                    }
                }

                if (isrowempty)
                {
                    for (int j = 0; j < e.ColumnCount; j++)
                    {
                        DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);

                        if (cell == null) continue;
                        if (cell.EndLevel == e.GetLevelCount(false) - 1)
                        {
                            if (cell.Field != null && rowemptyremoved == false)
                            {
                                e.Remove(cell);
                                rowemptyremoved = true; ;
                            }
                        }
                    }
                }
            }
        }

        private void xrPivotGrid2_CustomFieldValueCells(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs e)
        {
            //If particular branch does not contain data, empty row will be displayed, it should be removed
            if (!rowemptyremoved)
            {
                bool isrowempty = false;
                for (int j = 0; j < e.ColumnCount; j++)
                {
                    DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);
                    if (cell != null && cell.Field != null && cell.Field.FieldName == "LEDGER_NAME")
                    {
                        isrowempty = string.IsNullOrEmpty(cell.Value.ToString().Trim());
                        break;
                    }
                }

                if (isrowempty)
                {
                    for (int j = 0; j < e.ColumnCount; j++)
                    {
                        DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);

                        if (cell == null) continue;
                        if (cell.EndLevel == e.GetLevelCount(false) - 1)
                        {
                            if (cell.Field != null && rowemptyremoved == false)
                            {
                                e.Remove(cell);
                                rowemptyremoved = true; ;
                            }
                        }
                    }
                }
            }
        }

        private void GrpHeaderPayments_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rowemptyremoved = false;
        }

    }
}
