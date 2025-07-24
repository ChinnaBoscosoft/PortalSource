/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page helps head office admin/user or branch office admin/user to view the TDS Deductee types
 *****************************************************************************************************/
using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel.Master;
using System.Data;
using System.IO;
using System.Web;
namespace AcMeERP.Module.Master
{

    public partial class TDSDeducteeType : Base.UIBase
    {
        #region Declaration
        private ResultArgs resultArgs = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private const string DEDUCTEE_TYPE_ID = "DEDUCTEE_TYPE_ID";
        private DataView DeducteeTypeView = null;
        private DataTable ExportDeducteeTypeView = null;
        #endregion

        #region Events
        protected void Page_Init(object sender, EventArgs e)
        {
            SetTDSDeducteeType();
            gvDeducteeType.ShowAdd = false;
            gvDeducteeType.ExportClicked += new EventHandler(gvDeducteeType_ExportClicked);
            gvDeducteeType.DataSource = DeducteeTypeView;
            ExportDeducteeTypeView = DeducteeTypeView.ToTable();
        }

        protected void gvDeducteeType_ExportClicked(object sender, EventArgs e)
        {
            string fileName = "TDSDeducteeType" + DateTime.Now.Ticks.ToString();
            SetTDSDeducteeType();
            if (!ExportDeducteeTypeView.Equals(null))
            {
                ExportDeducteeTypeView.Columns.Remove(DEDUCTEE_TYPE_ID);
                CommonMethod.WriteExcelFile(ExportDeducteeTypeView, fileName);
                DownLoadFile(fileName);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.TDS.DeducteeTypePageTitle;
                gvDeducteeType.ShowExport = true;
            }
        }

        #endregion

        #region Methods

        public void SetTDSDeducteeType()
        {
            try
            {
                using (TDSSystem tdsSystem = new TDSSystem())
                {
                    resultArgs = tdsSystem.FetchDeducteeTypes();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        DeducteeTypeView = resultArgs.DataSource.Table.DefaultView;
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                    this.rowIdColumn = AppSchema.DeducteeType.DEDUCTEE_TYPE_IDColumn.ColumnName;
                    gvDeducteeType.HideColumn = rowIdColumn;
                }

            }
            catch (Exception ex)
            {
                this.Message = resultArgs.Message;
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