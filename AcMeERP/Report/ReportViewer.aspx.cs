/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 17/07/2015
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          : This page is to load the report data based on the report Id passed from the querystring based on the given criteria.
 * *****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using System.Collections;
using System.IO;
using DevExpress.XtraReports.Web;
using DevExpress.Web.ASPxCallback;



namespace AcMeERP.Report
{
    public partial class ReportViewer : Base.UIBase
    {

        #region Property
        public EventDrillDownArgs ReportDrillDown = null;
        private CommonMember utilityMember = null;
        private ReportProperty objReportProperty = new ReportProperty();
        Bosco.Utility.ConfigSetting.SettingProperty objSettingProperty = new Bosco.Utility.ConfigSetting.SettingProperty();

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        //On 05/11/2020, To have proper pagging functionalities
        private string activerepot
        {
            set
            {
                ViewState["activerepot"] = value;
            }
            get
            {
                string rpt = string.Empty;
                if (ViewState["activerepot"] != null)
                {
                    rpt = ViewState["activerepot"].ToString();
                }
                return rpt;
            }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["REPORTPROPERTY"] != null)
                {
                    objReportProperty = ((ReportProperty)Session["REPORTPROPERTY"]);
                    if (Request.QueryString["rid"] != null)
                    {
                        objReportProperty.ReportId = Request.QueryString["rid"];
                    }
                    if (Session["DillDownLinks"] == null)
                    {
                        Session["DillDownLinks"] = new Stack<EventDrillDownArgs>();
                    }
                    //Showpopup if hdva contains value true
                    if (Request.QueryString["hdva"] == null)
                    {
                        InitialDrillDownProperties(objReportProperty.ReportId);
                        //Clear Previous Report Cache value before loading New Report
                        Page.Session["RPTCACHE"] = null;
                        LoadReport(objReportProperty.ReportId);
                    }
                    else if (Request.QueryString.AllKeys.Contains("DrillBack"))
                    {
                        DrillDownTarget(GetRecentDrillDown());
                    }
                    else
                    {
                        LoadReport(objReportProperty.ReportId);
                    }
                }
            }

            //On 03/03/2020, To hide report criteria button for mysore budget approval screen ---------------------------
            if (objReportProperty.ReportId == "RPT-171")
            {
                if (dvReportViewer.ToolbarItems[0].Name == "Customization")
                {
                    dvReportViewer.ToolbarItems.Remove(dvReportViewer.ToolbarItems[0]);
                }
            }
            //-----------------------------------------------------------------------------------------------------------
        }
        #endregion

        #region Methods
        private void ShowDrillDownInToolbar()
        {
            ReportToolbarButton drillButton = new ReportToolbarButton();
            drillButton.Name = "DrillDown";
            drillButton.ImageUrl = "~/App_Themes/MainTheme/images/Decrease.png";
            drillButton.ToolTip = "Drill-Down/Drill-Back";
            dvReportViewer.ToolbarItems.Insert(0, drillButton);
        }

        private void ShowCriteriaCustomiseInToolbar()
        {
            ReportToolbarButton rtbCustomise = new ReportToolbarButton();
            rtbCustomise.Name = "Customization";
            rtbCustomise.ImageUrl = "~/App_Themes/MainTheme/images/Report.png";
            rtbCustomise.ToolTip = "Customise";
            dvReportViewer.ToolbarItems.Insert(0, rtbCustomise);
        }

        /// <summary>
        /// This method loads the report in the report viewer and stores the value in rptcache for easy paging
        /// </summary>
        /// <param name="reportId"></param>
        private void LoadReport(string reportId)
        {
            if (!string.IsNullOrEmpty(reportId))
            {
                if (Request.QueryString["DrillDownType"] != null)
                {
                    dvReportViewer.ToolbarItems.RemoveAt(0);
                    ShowDrillDownInToolbar();
                    AttachDrillDownProperties();
                }
                objReportProperty.ReportId = reportId;

                string reportAssemblyType = objReportProperty.ReportAssembly;
                //new ErrorLog().WriteError("ReportId::" + objReportProperty.ReportId.ToString() + "Assembly Type::" + reportAssemblyType.ToString());
                Page.Session["RPTCACHE"] = null;
                ReportBase report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                report.ShowReport();
                dvReportViewer.Report = report;
            }
        }
        #endregion

        #region Report Caching
        /// <summary>
        /// This event fired when reportviewer is unloaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvReportViewer_Unload(object sender, EventArgs e)
        {
            dvReportViewer.Report = null;
        }
        //Report Caching -For Perfomance
        protected void dvReportViewer_CacheReportDocument(object sender, CacheReportDocumentEventArgs e)
        {
            //On 05/11/2020, To have proper pagging functionalities
            activerepot =  objReportProperty.ReportId;
            Page.Session["RPTCACHE" + activerepot] = e.SaveDocumentToMemoryStream();
        }
        protected void dvReportViewer_RestoreReportDocumentFromCache(object sender, RestoreReportDocumentFromCacheEventArgs e)
        {
            //On 05/11/2020, To have proper pagging functionalities
            Stream stream = Page.Session["RPTCACHE" + activerepot] as Stream;
            if (stream != null)
                e.RestoreDocumentFromStream(stream);
        }
        #endregion

        #region Drill-Down Methods

        /// <summary>
        /// Add Drill Down to the report
        /// </summary>
        /// <param name="RptId"></param>
        private void InitialDrillDownProperties(string RptId)
        {
            if (RptId != string.Empty)
            {
                ClearDrillDown();
                //Add base report into the collection for drill down, when it gets loaded in the viewer
                EventDrillDownArgs baseRepotEventArug = new EventDrillDownArgs(DrillDownType.BASE_REPORT, RptId, new Dictionary<string, object>());
                AddDrillDown(baseRepotEventArug);
            }
        }

        private void AddDrillDown(EventDrillDownArgs eventDrillDown)
        {
            if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Count == 0)
            {
                ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Push(eventDrillDown);
            }
            else
            {
                if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]) != null)
                {
                    EventDrillDownArgs rteventValue = ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Peek();
                    if (rteventValue.DrillDownRpt != eventDrillDown.DrillDownRpt)
                    {
                        ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Push(eventDrillDown);
                    }
                }
            }
        }
        /// <summary>
        /// Attach event to the drilldown links
        /// </summary>
        /// <returns></returns>
        private EventDrillDownArgs GetRecentDrillDown()
        {
            EventDrillDownArgs RtneventDrill = null;
            if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Count > 1)
            {
                ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Pop();
                RtneventDrill = ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Peek();
                if (RtneventDrill.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    objReportProperty.ReportId = RtneventDrill.DrillDownRpt;
                    //   InitiateReportCriteria();
                }
                else
                {
                    dvReportViewer.ToolbarItems.RemoveAt(0);
                    ShowDrillDownInToolbar();
                    objReportProperty.ReportId = RtneventDrill.DrillDownRpt;
                }
            }
            return RtneventDrill;
        }

        /// <summary>
        /// This method sets the drill down property
        /// </summary>
        private void DrillDownProperty()
        {
            try
            {
                Dictionary<string, object> dicDrillDownProperties = new Dictionary<string, object>();
                DrillDownType ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), Request.QueryString["DrillDownType"].ToString());
                dicDrillDownProperties.Add("DrillDownLink", ddtypeLinkType.ToString());
                ArrayList drilldownItmes = new ArrayList();
                string DrillToRptId = Request.QueryString["rid"].ToString();
                string sLinkField = Request.QueryString["FNAME"] != null ? Request.QueryString["FNAME"].ToString() : string.Empty;
                drilldownItmes.Add(sLinkField);
                if (Request.QueryString["PNAME"] != null)
                {
                    sLinkField = Request.QueryString["PNAME"].ToString();
                    drilldownItmes.Add(sLinkField);
                }
                string sVoucherSubTypeField = string.Empty;
                string sLinkFieldValue = string.Empty;
                string sVoucherType = string.Empty;
                sLinkFieldValue = Request.QueryString["FVALUE"] != null ? Request.QueryString["FVALUE"].ToString() : string.Empty;
                sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;

                foreach (string drilldownItem in drilldownItmes)
                {
                    sLinkField = drilldownItem;
                    if (sLinkField == "COST_CENTRE_ID")
                    {
                        sLinkFieldValue = this.objReportProperty.CostCentre;
                        sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;
                    }
                    else if (sLinkField == "DATE_AS_ON")
                    {
                        sLinkFieldValue = this.objReportProperty.DateAsOn;
                        sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;
                    }
                    else
                    {
                        sLinkFieldValue = Request.QueryString["FVALUE"] != null ? Request.QueryString["FVALUE"].ToString() : string.Empty;
                        sVoucherType = sVoucherSubTypeField != "" ? "GN" : string.Empty;
                    }
                    ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), Request.QueryString["DrillDownType"].ToString());
                    if ((ddtypeLinkType == DrillDownType.DRILL_DOWN ||
                        ddtypeLinkType == DrillDownType.LEDGER_VOUCHER) && sVoucherType == ledgerSubType.GN.ToString())
                    {
                        ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), Request.QueryString["PARTICULAR_TYPE"].ToString());
                    }
                    else if (sVoucherType == ledgerSubType.FD.ToString())
                    {
                        ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), "FD_VOUCHER");
                    }

                    DrillToRptId = Request.QueryString["rid"].ToString();
                    dicDrillDownProperties["DrillDownLink"] = Request.QueryString["DrillDownType"].ToString();
                    dicDrillDownProperties.Add(sLinkField, sLinkFieldValue);
                    if (!string.IsNullOrEmpty(sVoucherSubTypeField) && !string.IsNullOrEmpty(sVoucherType) && sLinkField != "DATE_AS_ON")
                    {
                        dicDrillDownProperties.Add(sVoucherSubTypeField, sVoucherType);
                    }

                }
                //Define DrillDown properties
                if (dicDrillDownProperties.Count > 1)
                {
                    ReportDrillDown = new EventDrillDownArgs(ddtypeLinkType, DrillToRptId, dicDrillDownProperties);
                    AddDrillDown(ReportDrillDown);
                    this.objReportProperty.DrillDownProperties = dicDrillDownProperties;
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Attaching DrillDownProperty::" + ex.Message);
            }
        }

        /// <summary>
        /// Attach Drill Down properties
        /// </summary>
        private void AttachDrillDownProperties()
        {
            DrillDownProperty();
        }

        /// <summary>
        /// Clear the Drill Down Properties
        /// </summary>
        private void ClearDrillDown()
        {
            if (((Stack<EventDrillDownArgs>)Session["DillDownLinks"]) != null)
            {
                ((Stack<EventDrillDownArgs>)Session["DillDownLinks"]).Clear();
                if (objReportProperty.DrillDownProperties != null)
                {
                    objReportProperty.DrillDownProperties.Clear();
                    HttpContext.Current.Session["DrillDownProperties"] = null;
                }
            }
        }

        /// <summary>
        /// This method used to load drilldown/ledger/end transaction screen based on the event triggered 
        /// when user clicks
        /// </summary>
        /// <param name="eDrilldownevent"></param>
        private bool DrillDownTarget(EventDrillDownArgs eDrilldownevent)
        {
            bool bSucessDrillDown = false;
            if (eDrilldownevent != null && eDrilldownevent.DrillDownRpt != string.Empty)
            {
                // Load Drill-Down Report for selected Group
                switch (eDrilldownevent.DrillDownType)
                {
                    case DrillDownType.BASE_REPORT:
                    case DrillDownType.GROUP_SUMMARY:
                    case DrillDownType.GROUP_SUMMARY_RECEIPTS:
                    case DrillDownType.GROUP_SUMMARY_PAYMENTS:
                    case DrillDownType.LEDGER_SUMMARY:
                    case DrillDownType.LEDGER_SUMMARY_RECEIPTS:
                    case DrillDownType.LEDGER_SUMMARY_PAYMENTS:
                    case DrillDownType.LEDGER_CASH:
                    case DrillDownType.LEDGER_BANK:
                        objReportProperty.DrillDownProperties = eDrilldownevent.DrillDownProperties;
                        LoadReport(eDrilldownevent.DrillDownRpt);
                        bSucessDrillDown = true;
                        break;
                }
            }
            return bSucessDrillDown;
        }

        #endregion
    }
}