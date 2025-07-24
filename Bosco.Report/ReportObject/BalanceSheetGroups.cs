using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using System.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BalanceSheetGroups : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        SettingProperty settingProperty = new SettingProperty();
        ResultArgs resultArgs = new ResultArgs();
        string YearFromReducing = string.Empty;
        #endregion

        #region Constructor
        public BalanceSheetGroups()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindBalanceSheet();
        }
        #endregion

        #region Property
        string yearFrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearFrom = settingProperty.YearFrom;
                return yearFrom;
            }
        }

        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }
        #endregion

        #region Events

        #endregion

        #region Methods
        public void BindBalanceSheet()
        {
            try
            {
                string datetime = this.GetProgressiveDate(this.ReportProperties.DateAsOn);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.ReportTitle = this.ReportProperties.ReportTitle;
                DateTime dtDateFrom = Convert.ToDateTime(YearFrom);
                YearFromReducing = dtDateFrom.AddDays(-1).ToShortDateString();
                xrlblPreviousYear.Text = this.UtilityMember.DateSet.ToDate(YearFromReducing, DateFormatInfo.DateFormatYMD);
                xrlblCurrentYear.Text = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateAsOn, DateFormatInfo.DateFormatYMD);
                if (string.IsNullOrEmpty(this.ReportProperties.DateAsOn))
                {
                    SetReportTitle();
                    ShowReportFilterDialog();
                }
                else
                {
                    SetReportTitle();
                    this.ReportPeriod = String.Format("For the Period: {0}", this.ReportProperties.DateAsOn);
                    setHeaderTitleAlignment();
                    ResultArgs resultArgs = GetBalanceSheetGroups();
                    DataView dtValue = resultArgs.DataSource.TableView;
                    DataTable dtOpValue = dtValue.ToTable();
                    if (dtOpValue != null && dtOpValue.Rows.Count > 0)
                    {
                        dtValue.Table.TableName = "BalanceSheet";
                        this.DataSource = dtValue;
                        this.DataMember = dtValue.Table.TableName;
                    }
                    base.ShowReport();
                }
                SetReportBorders();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetBalanceSheetGroups()
        {
            string BalanceSheetGroups = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheetGroups);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.BalanceSheetGroups,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, YearFromReducing);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BalanceSheetGroups);
            }
            return resultArgs;
        }

        public void SetReportBorders()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblGroup = AlignGroupTable(xrtblGroup);
            xrtblDetails = AlignContentTable(xrtblDetails);
            xrtblGroupTotal = AlignTotalTable(xrtblGroupTotal);
            xrcellTemp.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Right;
            //      xrcellTemp.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
        }

        public XRTable AlignHeaderTable(XRTable table)
        {
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
                            tcell.Borders = BorderSide.All;

                        }
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;

                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //       tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public XRTable AlignContentTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (trow.Cells.Count == count)
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.None;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
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
                    //      tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        public XRTable AlignTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left;
                        else if (trow.Cells.Count == count)
                            tcell.Borders = BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.None;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //         tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public XRTable AlignGroupTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = BorderSide.Left | BorderSide.Right;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = BorderSide.Left | BorderSide.Right;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Left;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //          tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public void HideBalanceSheetGroupsHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        #endregion



    }
}
