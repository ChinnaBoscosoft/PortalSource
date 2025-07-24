using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class FCPurpose : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public FCPurpose()
        {
            InitializeComponent();
            // this.AttachDrillDownToRecord(xrGroupIndividual, xrDonorName,
            //new ArrayList { this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName, this.ReportParameters.PURPOSEColumn.ColumnName }, DrillDownType.FC_REPORT, false);
            this.AttachDrillDownToRecord(xrFCPurposeDetails, xrDonor,
               new ArrayList { this.ReportParameters.DONAUD_IDColumn.ColumnName, this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName, this.ReportParameters.PURPOSEColumn.ColumnName, this.ReportParameters.DONORColumn.ColumnName, "RECEIPT_DATE" }, DrillDownType.FC_REPORT, false);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            FCPurposeReport();

        }

        private void FCPurposeReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                // this.ReportTitle = ReportProperty.Current.ReportTitle;
                setHeaderTitleAlignment();
                SetReportTitle();
                //  this.ReportSubTitle = "Foreign Projects"; //ReportProperty.Current.ProjectTitle;
                // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                xrCapAmount.Text = this.SetCurrencyFormat(xrCapAmount.Text);
                DataTable dtFCPurpose = GetReportSource();
                if (dtFCPurpose != null)
                {
                    this.DataSource = dtFCPurpose;
                    this.DataMember = dtFCPurpose.TableName;
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

        private DataTable GetReportSource()
        {
            try
            {
                string FCPurposeQueryPath = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCPurpose);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.ForeginContribution.FCPurpose, DataBaseType.HeadOffice))
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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FCPurposeQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            FCPurposeHeader = AlignHeaderTable(FCPurposeHeader);
            xrGroupIndividual = AlignPurposeTable(xrGroupIndividual);
            xrDonorName.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrFCPurposeDetails = AlignContentTable(xrFCPurposeDetails);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrPCPurposeFooter = AlignGrandTotalTable(xrPCPurposeFooter);

        }

        public override XRTable AlignTotalTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Top | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignGrandTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        private XRTable AlignPurposeTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {

                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        #endregion

    }
}
