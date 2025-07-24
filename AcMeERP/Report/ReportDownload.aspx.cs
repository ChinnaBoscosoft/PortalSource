using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Report.ReportObject;

using System.Collections.Generic;
using System.IO;
using DevExpress.XtraPrinting;

namespace AcMeERP.Report
{
    public partial class ReportDownload : System.Web.UI.Page
    {
        /// <summary>
        /// Report Id
        /// </summary>
        private string ReportId
        {
            get
            {
                string rtn = string.Empty;
                if (ViewState["ReportId"] != null)
                {
                    rtn = ViewState["ReportId"].ToString();
                }
                return rtn;
            }
            set
            {
                ViewState["ReportId"] = value;
            }
        }

        /// <summary>
        /// Export Type
        /// </summary>
        private string ExportType
        {
            get
            {
                string rtn = string.Empty;
                if (ViewState["ExportType"] != null)
                {
                    rtn = ViewState["ExportType"].ToString();
                }
                return rtn;
            }
            set
            {
                ViewState["ExportType"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Printing
            if (Request.QueryString["rid"] != null)
            {
                ReportId = Request.QueryString["rid"].ToString();
                ExportType = (Request.QueryString["type"] == null ? "pdf" : Request.QueryString["type"].ToString());
                SetExportProperties();
            }
        }

        /// <summary>
        /// Set Report Export Properties
        /// </summary>
        private void SetExportProperties()
        {
            Bosco.Report.Base.ReportBase reportbase = new Bosco.Report.Base.ReportBase();
            string exportfilename = string.Empty;
            switch (ReportId)
            {
                case "RPT-171": //Monthly Budget Details
                    {
                        exportfilename = "MonthlyBudget";
                        BudgetApprovalByMonth report = new BudgetApprovalByMonth();
                        report.ReportId = ReportId;
                        report.BindBudgetExpenseApproval();
                        reportbase = report as Bosco.Report.Base.ReportBase;
                        break;
                    }
                case "RPT-177": //Budget Proposal
                    {
                        exportfilename = "BudgetProposal";
                        BudgetView report = new BudgetView();
                        report.HideReportLogoLeft = false;
                        report.ReportId = ReportId;
                        report.ShowBudgetView();
                        reportbase = report as Bosco.Report.Base.ReportBase;
                        break;
                    }
                case "RPT-191": //Budget Proposal
                    {
                        exportfilename = "BudgetProposal";
                        BudgetViewINM report = new BudgetViewINM();
                        report.HideReportLogoLeft = false;
                        report.ReportId = ReportId;
                        report.ShowBudgetView();
                        reportbase = report as Bosco.Report.Base.ReportBase;
                        break;
                    }
            }

            if (!string.IsNullOrEmpty(exportfilename))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    if (ExportType.ToUpper() == "XLS")
                    {
                        exportfilename = exportfilename + ".xls";
                        reportbase.ExportToXls(ms, new XlsExportOptions());
                    }
                    else
                    {
                        exportfilename = exportfilename + ".pdf";
                        reportbase.ExportToPdf(ms, new PdfExportOptions());
                    }
                    WriteDocumentToResponse(ms.ToArray(), ExportType, true, exportfilename);
                }
            }
        }

        /// <summary>
        /// Update Response content type based on export type
        /// </summary>
        /// <param name="documentData"></param>
        /// <param name="format"></param>
        /// <param name="isInline"></param>
        /// <param name="fileName"></param>
        private void WriteDocumentToResponse(byte[] documentData, string format, bool isInline, string fileName)
        {
            string contentType_Renamed;
            string disposition = isInline ? "inline" : "attachment";

            switch (format.ToLower())
            {
                case "pdf":
                    {
                        contentType_Renamed = "application/pdf";
                        break;
                    }
                case "xls":
                    {
                        contentType_Renamed = "application/vnd.ms-excel";
                        break;
                    }

                case "xlsx":
                    {
                        contentType_Renamed = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;
                    }

                case "mht":
                    {
                        contentType_Renamed = "message/rfc822";
                        break;
                    }

                case "html":
                    {
                        contentType_Renamed = "text/html";
                        break;
                    }

                case "txt":
                case "csv":
                    {
                        contentType_Renamed = "text/plain";
                        break;
                    }

                case "png":
                    {
                        contentType_Renamed = "image/png";
                        break;
                    }

                default:
                    {
                        contentType_Renamed = string.Format("application/{0}", format);
                        break;
                    }
            }

            Page.Response.Clear();
            //Page.Response.ContentType = "application/pdf";
            Page.Response.ContentType = contentType_Renamed;
            Page.Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}", disposition, fileName));
            Page.Response.BinaryWrite(documentData);
            Page.Response.End();
        }

    }
}