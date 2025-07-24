using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using DevExpress.XtraPrinting;
using System.Data;
using System.Web;
using Bosco.Model.UIModel;
using AcMEDSync.Model;

namespace Bosco.Report.Base
{
    public partial class ReportHeaderBase : ReportBase
    {
        ReportProperty objProperty = null;
        SettingProperty settingProperty = new SettingProperty();
        string CurrencyFormat = string.Empty;
        private bool isShowProjectTitle = false; // To show project title on the header (set true, not to show (set False))
        private bool isShowBranchName = false;

        private bool hideheaderfully = false; //14/01/2020, to hide header part fully

        public ReportHeaderBase()
        {
            InitializeComponent();
            HideReportHeader = true;
            HidePrintDate = true;
            HidePageInfo = true;
            HideInstitute = true;
            HideReportTitle = true;
            HideReportSubTitle = true;
            HideReportLogoLeft = true;
            HideDateRange = true;
            HideCostCenter = true;
            // xrlblInstitute.Text = settingProperty.InstituteName;//Institute of Brothers of St. Gabriel//Don Bosco Center, Yelagiri Hills            

        }

        protected ReportProperty objReportProperty
        {
            get
            {
                objProperty = new ReportProperty();
                return objProperty;
            }
            set { objProperty = value; }
        }
        public override void ShowReport()
        {
            SetReportHeaderFooterSetting();
            //SetReportDate = DateTime.Now.ToString();
            base.ShowReport();

        }

        public string ReportHeaderTitle
        {
            set { xrlblInstitute.Text = xrlphblInstitute.Text = value; }
        }

        public bool ReportAmountLakh
        {
            set { xrlblAmtLakh.Visible = value; }
        }

        public string ReportTitle
        {
            set { xrlblReportTitle.Text = xrlblpgReportTitle.Text = value; }
        }

        public string InstituteName
        {
            set { xrInstituteName.Text = xrIphnstituteName.Text = value; }
        }
        public string LegalEntityAddress
        {
            set { xrInstituteAddress.Text = xrIphnstituteAddress.Text = value; }
        }

        public float SetLandscapeBudgetNameWidth
        {
            set { xrlblBudgetname.WidthF = value; }
        }

        public float SetLandscapeHeader
        {
            set
            {
                xrlblReportSubTitle.WidthF = xrblpgReportSubTitle.WidthF = value;
                xrlblBranchName.WidthF = xrblphBranchName.WidthF = value;
                xrlblCostCentreName.WidthF = xrblpgCostCentreName.WidthF = value;
                xrlblBudgetname.WidthF = xrlblBudget.WidthF = value;
            }
        }

        public float SetLandscapeFooterDateWidth
        {
            set { this.xrlblReportDate.LocationF = new DevExpress.Utils.PointFloat(value, 20.291667F); }
        }

        public float SetLandscapeFooter
        {
            set { xrlnFooter.WidthF = value; }
        }

        public string ReportSubTitle
        {
            set { xrlblReportSubTitle.Text = xrblpgReportSubTitle.Text = value; }
        }

        public string ReportBranchName
        {
            set
            {
                xrlblBranchName.Text = xrblphBranchName.Text = value;
            }
        }

        public string CosCenterName
        {
            set { xrlblCostCentreName.Text = xrblpgCostCentreName.Text = value; }
        }

        public string BudgetName
        {
            set { xrlblBudgetname.Text = xrlblBudget.Text = value; }
        }

        public string ReportPeriod
        {
            set
            {
                xrDateRange.Text = xrpgDateRange.Text = value;
                xrDateRange.Visible = xrpgDateRange.Visible = true;
            }
        }

        public bool HideDateRange
        {
            set { xrDateRange.Visible = xrpgDateRange.Visible = value; }
        }

        public bool HideCostCenter
        {
            set { xrlblCostCentreName.Visible = value; }
        }

        public bool HideBudget
        {
            set { xrlblBudgetname.Visible = value; }
        }

        public bool HidePrintDate
        {
            set { xrlblReportDate.Visible = value; }
        }

        public bool HidePageInfo
        {
            set { xrPageInfo.Visible = value; }
        }

        public bool HideInstitute
        {
            set { xrlblInstitute.Visible = xrlphblInstitute.Visible = value; }
        }

        public bool HideReportTitle
        {
            set { xrlblReportTitle.Visible = xrlblpgReportTitle.Visible = value; }
        }

        public bool HideReportSubTitle
        {
            set { xrlblReportSubTitle.Visible = value; }
        }

        public bool HideBranchName
        {
            set { xrlblBranchName.Visible = value; }
        }

        public bool HideInstituteName
        {
            set { xrInstituteName.Visible = xrIphnstituteName.Visible = value; }
        }

        public bool HideLegalAddress
        {
            set { xrInstituteAddress.Visible = xrIphnstituteAddress.Visible = value; }
        }
        public void SetTitleWidth(float width)
        {
            xrlblReportSubTitle.WidthF = width;
            xrlblBranchName.WidthF = width;
            xrlblCostCentreName.WidthF = width;
            xrlblBudgetname.WidthF = width;
            xrlnFooter.WidthF = width - 20;
            xrlblReportDate.LeftF = xrlnFooter.LeftF + xrlnFooter.WidthF - xrlblReportDate.WidthF;
        }

        public string SetReportDate
        {
            set { xrlblReportDate.Text = value; }
        }
        public bool HideReportDate
        {
            set { xrlblReportDate.Visible = value; }
        }

        public bool HideReportLogoLeft
        {
            set
            {
                xrpicReportLogoLeft.Visible = xrpicphReportLogoLeft.Visible = value;

                if (value == true)
                {
                    xrDateRange.LeftF = (xrpicReportLogoLeft.LeftF + xrpicReportLogoLeft.WidthF);
                    xrDateRange.WidthF = (xrlblReportSubTitle.WidthF - xrpicReportLogoLeft.WidthF);

                    xrpgDateRange.LeftF = (xrpicphReportLogoLeft.LeftF + xrpicphReportLogoLeft.WidthF);
                    xrpgDateRange.WidthF = (xrblpgReportSubTitle.WidthF - xrpicphReportLogoLeft.WidthF);
                }
                else
                {
                    xrDateRange.LeftF = xrlblReportSubTitle.LeftF;
                    xrDateRange.WidthF = xrlblReportSubTitle.WidthF;

                    xrpgDateRange.LeftF = xrblpgReportSubTitle.LeftF;
                    xrpgDateRange.WidthF = xrblpgReportSubTitle.WidthF;
                }

                xrlblInstitute.LeftF = xrDateRange.LeftF;
                xrlblInstitute.WidthF = xrDateRange.WidthF;
                xrlblReportTitle.LeftF = xrDateRange.LeftF;
                xrlblReportTitle.WidthF = xrDateRange.WidthF;

                xrInstituteAddress.LeftF = xrDateRange.LeftF;
                xrInstituteAddress.WidthF = xrDateRange.WidthF;

                xrInstituteName.LeftF = xrDateRange.LeftF;
                xrInstituteName.WidthF = xrDateRange.WidthF;

                /*pageHeader*/
                xrlphblInstitute.LeftF = xrpgDateRange.LeftF;
                xrlphblInstitute.WidthF = xrpgDateRange.WidthF;
                xrlblpgReportTitle.LeftF = xrpgDateRange.LeftF;
                xrlblpgReportTitle.WidthF = xrpgDateRange.WidthF;

                xrIphnstituteAddress.LeftF = xrpgDateRange.LeftF;
                xrIphnstituteAddress.WidthF = xrpgDateRange.WidthF;

                xrIphnstituteName.LeftF = xrpgDateRange.LeftF;
                xrIphnstituteName.WidthF = xrpgDateRange.WidthF;
            }
        }

        public bool HideReportHeader
        {
            set { ReportHeader.Visible = value; }
        }
        public bool HidePageHeader
        {
            set { PageHeader.Visible = value; }
        }

        public bool HidePageFooter
        {
            set { PageFooter.Visible = value; }
        }

        public void SetHeaderColumnTitleAlignment(XRTable xrtTable)
        {
            foreach (XRTableCell xrCellHeader in xrtTable.Rows.FirstRow.Cells)
            {
                switch (objReportProperty.TitleAlignment)
                {
                    case 0:
                        {
                            xrCellHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            break;
                        }
                    case 1:
                        {
                            xrCellHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            break;
                        }
                    case 2:
                        {
                            xrCellHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            break;
                        }
                }
            }
        }

        public string SetCurrencyFormat(string Caption)
        {
            CurrencyFormat = String.Format("{0} ({1})", Caption, settingProperty.Currency);
            return CurrencyFormat;
        }

        public string GetGroupName(int GroupId)
        {
            string groupname = "";
            using (LedgerGroupSystem ledgerGroupSystem = new LedgerGroupSystem())
            {
                groupname = ledgerGroupSystem.GetGroupName(GroupId);
            }
            return groupname;

        }

        public Int32 GetGroupId(string GroupName)
        {
            Int32 groupid = 0;
            using (LedgerGroupSystem ledgerGroupSystem = new LedgerGroupSystem())
            {
                ResultArgs result = ledgerGroupSystem.GetLedgerGroupId(GroupName, DAO.Data.DataBaseType.HeadOffice);
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dt = result.DataSource.Table as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        groupid = UtilityMember.NumberSet.ToInteger(dt.Rows[0][ledgerGroupSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString());
                    }
                }
            }
            return groupid;
        }

        public string GetLedgerIds(Int32 GroupId)
        {
            string rtn = "0";
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                rtn = ledgerSystem.GetLedgerIdsByLedgerGroup(GroupId);
            }
            return rtn;
        }

        public string GetProjectIdsByProjectCategory(string ProjectCategoryids)
        {
            string rtn = "0";
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                rtn = projectSystem.FethcProjectIdsByProjectCategory(ProjectCategoryids);
            }
            return rtn;
        }


        public string GetLedgerName(int LedgerId)
        {
            string Ledgername = "";
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                Ledgername = ledgerSystem.GetLegerName(LedgerId);
            }
            return Ledgername;

        }
        public bool ShowProjectinFooter
        {
            set
            {
                xrlblProjectName.Visible = value;
                if (value == true)
                {
                    xrlblProjectName.Text = objReportProperty.ProjectTitle;
                }
                else
                {
                    xrlblProjectName.Text = string.Empty;
                }
            }
        }

        public bool HideHeaderFully
        {
            set { hideheaderfully = value; }
        }



        public bool ShowTitleAtEachPage
        {
            set
            {
                //Hide Header Section for FDCCSI Annual report
                if (hideheaderfully)
                {
                    HideReportHeader = HidePageHeader = HidePageFooter = false;
                }
                else
                {
                    if (value == true)
                    {
                        HideReportHeader = false;
                        HidePageHeader = true;
                    }
                    else
                    {
                        HideReportHeader = true;
                        HidePageHeader = false;
                    }
                }
            }
        }


        protected void SetReportHeaderFooterSetting()
        {
            this.HideReportLogoLeft = (ReportProperties.ShowLogo == 1);
            this.HidePageInfo = (ReportProperties.ShowPageNumber == 1);
            this.HidePrintDate = (ReportProperties.ShowPrintDate == 1);
            this.ShowProjectinFooter = (ReportProperties.ShowProjectsinFooter == 1);
            this.ShowTitleAtEachPage = (ReportProperties.ShowTitles == 1);
        }

        public void setHeaderTitleAlignment()
        {

            switch (objReportProperty.TitleAlignment)
            {
                case 0:
                    {
                        xrlblInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrlblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrInstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        //PageHeader

                        xrlphblInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrlblpgReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrIphnstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrIphnstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrpgDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        break;
                    }
                case 1:
                    {
                        xrlblInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrlblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrInstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        //PageHeader

                        xrlphblInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrlblpgReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrIphnstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrIphnstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrpgDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        break;
                    }
                case 2:
                    {
                        xrlblInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrlblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrInstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                        //PageHeader

                        xrlphblInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrlblpgReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrIphnstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrIphnstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrpgDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        break;
                    }
            }
        }

        public void RemoveVerticaLine(XRTable xrTableName)
        {
            foreach (XRTableCell xrCellHeader in xrTableName.Rows.FirstRow.Cells)
            {
                switch (objReportProperty.ShowVerticalLine)
                {
                    case 0:
                        {
                            xrCellHeader.BorderWidth = 0;
                            xrCellHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                            xrCellHeader.BorderWidth = 1;
                            break;
                        }
                }
            }
        }
        public void RemoveHorizontalLine(XRTable xrTableName)
        {
            foreach (XRTableCell xrCellHeader in xrTableName.Rows.FirstRow.Cells)
            {
                switch (objReportProperty.ShowHorizontalLine)
                {
                    case 0:
                        {
                            xrCellHeader.BorderWidth = 0;
                            xrCellHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left;
                            break;
                        }
                }
            }
        }

        public string GetInstituteName()
        {
            try
            {
                if (ReportProperty.dtLedgerEntity != null && ReportProperty.dtLedgerEntity.Rows.Count != 0)
                {
                    DataView dvFilter = ReportProperty.dtLedgerEntity.DefaultView;

                    if (objReportProperty.LedgalEntityId == null || string.IsNullOrEmpty(objReportProperty.LedgalEntityId))
                    {
                        foreach (DataRow dr in dvFilter.ToTable().Rows)
                        {
                            objReportProperty.LedgalEntityId += dr["CUSTOMERID"] != DBNull.Value ? dr["CUSTOMERID"] + "," : string.Empty;
                        }
                        objReportProperty.LedgalEntityId = objReportProperty.LedgalEntityId.TrimEnd(',');
                    }

                    dvFilter.RowFilter = "CUSTOMERID IN (" + objReportProperty.LedgalEntityId + ")";

                    if (objReportProperty.SelectedProjectCount >= 1 && dvFilter.Count != 0)
                    {
                        if (objReportProperty.HeaderInstituteSocietyName == 0)
                        {
                            objReportProperty.InstituteName = "InstituteName";
                            objReportProperty.LegalAddress = "Address";
                        }
                        else
                        {
                            objReportProperty.InstituteName = "InstituteName";
                            objReportProperty.LegalAddress = "Address";
                        }
                    }
                    else
                    {
                        objReportProperty.InstituteName = "InstituteName";
                        objReportProperty.LegalAddress = "Address";
                    }
                    dvFilter.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return objReportProperty.InstituteName;
        }
        public void CheckLegalEntity()
        {
            int legalCount = 0;
            //   string Pid = objReportProperty.Project;
            // commanded by chinna 0n 05.03.2020
            string Pid = objReportProperty.ProjectId;
            using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
            {
                DataTable dtres = null;
                legalCount = 0;
                if (!string.IsNullOrEmpty(objReportProperty.Project))
                {
                    dtres = legalEntitySystem.CheckNoofLegalentity(Pid);
                    if (dtres != null)
                    {
                        legalCount = (dtres.Rows.Count == 0) ? 0 : (dtres.Rows.Count == 1) ? 1 : 2;
                    }
                }
                if (legalCount == 1 && dtres != null)
                {
                    if (!string.IsNullOrEmpty(dtres.Rows[0]["CUSTOMERID"].ToString()))
                    {
                        this.InstituteName = this.GetInstituteName();
                    }
                    else
                    {
                        this.InstituteName = this.settingProperty.InstituteName;
                    }
                }
                else if (legalCount == 0)
                {
                    this.InstituteName = this.settingProperty.InstituteName;
                }
                else if (legalCount >= 2 && dtres != null)
                {
                    if (HttpContext.Current.Session["HeadOfficeInformation"] != null)
                    {
                        DataTable dtHeadOfficeInfo = HttpContext.Current.Session["HeadOfficeInformation"] as DataTable;
                        this.InstituteName = dtHeadOfficeInfo.Rows[0]["HEAD_OFFICE_NAME"].ToString();
                    }
                }
                //For testing Purpose
                if (HttpContext.Current.Session["HeadOfficeInformation"] != null)
                {
                    DataTable dtHeadOfficeInfo = HttpContext.Current.Session["HeadOfficeInformation"] as DataTable;
                    this.InstituteName = dtHeadOfficeInfo.Rows[0]["HEAD_OFFICE_NAME"].ToString();
                }
            }


            //For Temp Purpose On 26/11/2019, fix Title for Roem Reports --------------------------------------------------------------
            if (objReportProperty.IsSDBRomeReports)
            {
                this.ReportSubTitle = string.Empty; //this.ReportBranchName = "";
                using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                {
                    //ResultArgs resultLE = legalEntitySystem.FetchLegalEntity();
                    ResultArgs resultLE = legalEntitySystem.FetchBranchAttachedSociety();
                    if (resultLE.Success)
                    {
                        DataTable dtLE = resultLE.DataSource.Table;

                        if (ReportProperties.SelectedSocietyCount == dtLE.Rows.Count)
                        {
                            //this.ReportSubTitle = "Consolidated Statement";
                            this.ReportBranchName = "Consolidated Statement";
                        }
                        else
                        {
                            //this.ReportSubTitle = ReportProperties.SocietyName;
                            this.ReportBranchName = ReportProperties.SocietyName;
                        }

                    }
                }
            }
            //--------------------------------------------------------------------------------------------------------------------------------
        }
        public void SetReportTitle()
        {
            this.ReportTitle = objReportProperty.ReportTitle;
            if (objReportProperty.SelectedProjectCount != 1)
            {
                this.HideReportSubTitle = true;
                this.ReportHeaderTitle = null;
                this.InstituteName = null;
                this.ReportSubTitle = !string.IsNullOrEmpty(objReportProperty.ProjectTitle) ? objReportProperty.ProjectTitle : string.Empty;
                if (objReportProperty.BranchOffice != "0")
                {
                    this.ReportBranchName = !string.IsNullOrEmpty(objReportProperty.BranchOfficeName) ? objReportProperty.BranchOfficeName : string.Empty;
                }
                if (objReportProperty.ReportId == "RPT-032" || objReportProperty.ReportId == "RPT-033" || objReportProperty.ReportId == "RPT-034" || objReportProperty.ReportId == "RPT-035" || objReportProperty.ReportId == "RPT-036" || objReportProperty.ReportId == "RPT-037" || objReportProperty.ReportId == "RPT-038" || objReportProperty.ReportId == "RPT-039" || objReportProperty.ReportId == "RPT-040" || objReportProperty.ReportId == "RPT-041")
                {
                    if (objReportProperty.CostCentre != "0")
                    {
                        this.CosCenterName = !string.IsNullOrEmpty(objReportProperty.CostCentreName) ? objReportProperty.CostCentreName : string.Empty;
                    }
                }
                if (objReportProperty.ReportId == "RPT-048")
                {
                    if (objReportProperty.BudgetName != "0")
                    {
                        this.BudgetName = !string.IsNullOrEmpty(objReportProperty.BudgetName) ? objReportProperty.BudgetName : string.Empty;
                    }
                }
                this.xrInstituteName.Font = new System.Drawing.Font("Tahoma", 16.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                CheckLegalEntity();
            }
            else
            {
                this.HideInstitute = true;
                this.HideReportSubTitle = true;
                if (!string.IsNullOrEmpty(objReportProperty.ProjectTitle))
                {
                    string[] projectTitle = objReportProperty.ProjectTitle.Split('-');
                    if (isShowProjectTitle)
                    {
                        this.ReportHeaderTitle = projectTitle[0];
                    }
                }
                if (!isShowProjectTitle)
                {
                    this.ReportSubTitle = !string.IsNullOrEmpty(objReportProperty.ProjectTitle) ? objReportProperty.ProjectTitle : string.Empty;
                }
                else
                {
                    this.ReportSubTitle = null;
                }
                this.HideLegalAddress = true;
                this.HideInstituteName = true;
                this.xrInstituteName.Font = new System.Drawing.Font("Tahoma", 13F);
                CheckLegalEntity();
            }

            //if (HttpContext.Current.Session["HeadOfficeInformation"] != null)
            //{
            //    DataTable dtHeadOfficeInfo = HttpContext.Current.Session["HeadOfficeInformation"] as DataTable;
            //    this.LegalEntityAddress = dtHeadOfficeInfo.Rows[0]["ADDRESS"].ToString();
            //}
            setHeaderTitleAlignment();
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;


            this.DisplayName = xrlblReportTitle.Text;
        }

        public void CommonReportHeaderTitle()
        {
            this.ReportTitle = objReportProperty.ReportTitle;
            this.ReportSubTitle = objReportProperty.ProjectTitle;
            this.ReportBranchName = objReportProperty.BranchOffice;
            this.CosCenterName = objReportProperty.CostCentreName;
            this.BudgetName = objReportProperty.BudgetName;
            this.InstituteName = settingProperty.InstituteName;
            setHeaderTitleAlignment();
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
        }

        public string GetCommaSeparatedValue(DataTable dtValue, string OutputColumnName)
        {
            string retValue = String.Empty;
            if (dtValue != null && dtValue.Rows.Count > 0)
            {
                var rowVal = dtValue.AsEnumerable();
                retValue = String.Format("'{0}'", String.Join("','", (from r in rowVal
                                                                      select r.Field<string>(OutputColumnName).Replace("'", "''"))));
            }
            return retValue;
        }

        public string AssignBudgetDateRangeTitle()
        {
            string rtn = string.Empty;
            //if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE)
            //{
            if (objReportProperty.ReportId.Equals("RPT-046"))
            {
                this.SetReportDate = this.settingProperty.YearFrom + "-" + this.settingProperty.YearTo;
            }
            else
            {
                this.ReportPeriod = string.Empty;
            }
            //}
            //else
            //{
            //  this.SetReportDate = AppSetting.YearFrom + "-" + AppSetting.YearTo;
            //}


            //03/03/2020, For Mysore Budgets
            if (objReportProperty.ReportId.Equals("RPT-171"))
            {
                //if (objReportProperty.ReportId.Equals("RPT-171"))
                if (objReportProperty.IsTwoMonthBudget)
                {
                    // this.ReportPeriod = "Month : Budget for the month of " + UtilityMember.DateSet.ToDate(objReportProperty.Current.DateFrom, "MMM") + " & " + UtilityMember.DateSet.ToDate(objReportProperty.Current.DateTo, "MMM yyyy");
                    if (!(string.IsNullOrEmpty(ReportProperties.BudgetM1PropsedDateCaption) && string.IsNullOrEmpty(ReportProperties.BudgetM1PropsedDateCaption)))
                    {
                        // this.ReportPeriod = "Month : Budget for the month of " + ReportProperties.BudgetM1PropsedDateCaption.Substring(0, 3) + " & " + ReportProperties.BudgetM2PropsedDateCaption.Substring(0, 3) + " " + UtilityMember.DateSet.ToDate(objReportProperty.Current.DateTo, false).Year.ToString();
                        this.ReportPeriod = "Month : Budget for the month of " + ReportProperties.BudgetM1PropsedDateCaption.Substring(0, 3) + " & " + ReportProperties.BudgetM2PropsedDateCaption.Substring(0, 8);
                    }
                }
                else
                {
                    this.ReportPeriod = "Month : Budget for the month of " + ReportProperties.BudgetM1PropsedDateCaption; //+UtilityMember.DateSet.ToDate(objReportProperty.Current.DateFrom, "MMM yyyy");
                }


                //Set Society name as Title
                using (LegalEntitySystem legalsys = new LegalEntitySystem())
                {
                    ResultArgs result = legalsys.FetchSocietyByProject(objReportProperty.Current.ProjectId);
                    if (result.Success && result != null)
                    {
                        DataTable dtLegalEntity = result.DataSource.Table as DataTable;
                        if (dtLegalEntity.Rows.Count > 0)
                        {
                            this.InstituteName = dtLegalEntity.Rows[0]["SOCIETYNAME"].ToString().Trim(); //Mysore Diocesan Educational Society";
                            this.LegalEntityAddress = dtLegalEntity.Rows[0]["ADDRESS"].ToString().Trim();
                        }
                    }
                }

                this.ReportSubTitle = "Institution Name : " + objReportProperty.BranchOfficeName;// this.AppSetting.InstituteName;
                string bankname = GetBankNamesByProject();
                if (string.IsNullOrEmpty(bankname))
                {
                    this.BudgetName = objReportProperty.Current.Project;
                }
                else
                {
                    this.BudgetName = objReportProperty.Current.Project + " (" + bankname + ")";
                }
            }
            return rtn;
        }


        public string GetSocietyProjectIds()
        {
            string SocietyProjectIds = "0";
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                ResultArgs resulProjects = projectSystem.FetchProjetBySociety(this.ReportProperties.Society, string.Empty, string.Empty);

                if (resulProjects.Success && resulProjects.DataSource.Table != null)
                {
                    DataTable dtSocProjects = resulProjects.DataSource.Table;
                    foreach (DataRow dr in dtSocProjects.Rows)
                    {
                        SocietyProjectIds += dr["PROJECT_ID"].ToString() + ",";
                    }
                    SocietyProjectIds = SocietyProjectIds.TrimEnd(',');
                }
            }
            return SocietyProjectIds;
        }

        public string GetBranchProjectIds(string branchids)
        {
            string SocietyProjectIds = "0";
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                ResultArgs resulProjects = projectSystem.FetchProjetByBranch(branchids);

                if (resulProjects.Success && resulProjects.DataSource.Table != null)
                {
                    DataTable dtSocProjects = resulProjects.DataSource.Table;
                    foreach (DataRow dr in dtSocProjects.Rows)
                    {
                        SocietyProjectIds += dr["PROJECT_ID"].ToString() + ",";
                    }
                    SocietyProjectIds = SocietyProjectIds.TrimEnd(',');
                }
            }
            return SocietyProjectIds;
        }

        public string GetProjectIds(string societyids, string branchids)
        {
            string SocietyProjectIds = "0";
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                ResultArgs resulProjects = projectSystem.FetchProjetBySociety(societyids, branchids, string.Empty);

                if (resulProjects.Success && resulProjects.DataSource.Table != null)
                {
                    DataTable dtSocProjects = resulProjects.DataSource.Table;
                    foreach (DataRow dr in dtSocProjects.Rows)
                    {
                        SocietyProjectIds += dr["PROJECT_ID"].ToString() + ",";
                    }
                    SocietyProjectIds = SocietyProjectIds.TrimEnd(',');
                }
            }
            return SocietyProjectIds;
        }



    }
}
