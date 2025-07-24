/*****************************************************************************************************
 * Created by       : Chinna M test
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page helps head office admin/user or branch office admin/user to view the TDS DutyTax
 *****************************************************************************************************/
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.IO;
using Bosco.Model.UIModel.Master;

namespace AcMeERP.Module.Master
{
    public partial class TDSDutyTax : Base.UIBase
    {
        #region Declaration
        private ResultArgs resultArgs = null;
        private string rowIdColumn = string.Empty;
        private const string TDS_DUTY_TAXTYPE_ID = "TDS_DUTY_TAXTYPE_ID";
        private const string TDS_RATE = "TDS_RATE";
        private const string TDS_EXEMPTION_LIMIT = "TDS_EXEMPTION_LIMIT";       

        private string chkBoundfield = string.Empty;
        private string hiddenColumn = "TDS_DUTY_TAXTYPE_ID,TDS_EXEMPTION_LIMIT,TDS_SECTION_ID,TDS_RATE,StatusValue";
        private DataView DutyTaxView = null;
        private DataTable ExportDutyTaxView = null;
        #endregion

        #region Event
        protected void Page_Init(object sender, EventArgs e)
        {
            SetTaxDutyView();
            gvDutyTax.ShowAdd = false;
            gvDutyTax.ExportClicked += new EventHandler(gvDutyTax_ExportClicked);
            //gvDutyTax.SetTemplateColumn(ControlType.CheckBox, CommandMode.Status, rowIdColumn, chkBoundfield,null,null, "Active?");
            gvDutyTax.HideColumn = hiddenColumn;
            gvDutyTax.RowIdColumn = rowIdColumn;
            gvDutyTax.DataSource = DutyTaxView;
            ExportDutyTaxView = DutyTaxView.ToTable();
        }

        protected void gvDutyTax_ExportClicked(object sender, EventArgs e)
        {
            string fileName = "DutyTax" + DateTime.Now.Ticks.ToString();
            SetTaxDutyView();
            if (!ExportDutyTaxView.Equals(null))
            {
                ExportDutyTaxView.Columns.Remove(TDS_DUTY_TAXTYPE_ID);
                ExportDutyTaxView.Columns.Remove(TDS_RATE);
                ExportDutyTaxView.Columns.Remove(TDS_EXEMPTION_LIMIT);
                CommonMethod.WriteExcelFile(ExportDutyTaxView, fileName);
                DownLoadFile(fileName);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.TDS.DutyTaxPageTitle;
                gvDutyTax.ShowExport = true;
            }
        }
        
        #endregion

        #region Methods

        public void SetTaxDutyView()
        {
            try
            {
                using (TDSSystem tdsSystem = new TDSSystem())
                {
                    resultArgs = tdsSystem.FetchDutyTaxTypes();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        DutyTaxView = resultArgs.DataSource.Table.DefaultView;
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                    this.chkBoundfield = "StatusValue";
                    this.rowIdColumn = AppSchema.DutyTaxType.TDS_DUTY_TAXTYPE_IDColumn.ColumnName;
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        private void DownLoadFile(string fileName)
        {
            try
            {
                byte[] bytes;
                bytes = File.ReadAllBytes(PagePath.AppFilePath + fileName + ".xlsx");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/xlsx";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".xlsx");
                Response.BinaryWrite(bytes);
                Response.Flush();
                System.IO.File.Delete(PagePath.AppFilePath + fileName + ".xlsx");
                Response.End();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        #endregion


    }
}