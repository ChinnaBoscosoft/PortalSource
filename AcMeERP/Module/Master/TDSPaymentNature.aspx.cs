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
 * Purpose          :This page helps head office admin/user or branch office admin/user to view the TDS Payment nature
 *****************************************************************************************************/
using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.Data;

using Bosco.Model.UIModel.Master;
using System.IO;
using System.Web;

namespace AcMeERP.Module.Master
{
    public partial class TDSPaymentNature : Base.UIBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        DataView NatureOfPayment = null;
        DataTable ExportNatureOfPayment = null;
        private const string NATURE_PAY_ID = "NATURE_PAY_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        #endregion

        #region Page Load Events
        protected void Page_Init(object sender, EventArgs e)
        {
            SetNatureOfPaymentSource();
            gvPaymentNature.ExportClicked += new EventHandler(gvPaymentNature_ExportClicked);
            gvPaymentNature.DataSource = NatureOfPayment;
            ExportNatureOfPayment = NatureOfPayment.ToTable();
            gvPaymentNature.ShowAdd = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.TDS.TDSNatureOfPayment;
                gvPaymentNature.ShowExport = true;
            }
        }
        #endregion

        #region Methods
        private void SetNatureOfPaymentSource()
        {
            try
            {
                using (TDSSystem tdsSystem = new TDSSystem())
                {
                    resultArgs = tdsSystem.FetchNatureofPaymentsSections();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        NatureOfPayment = resultArgs.DataSource.Table.DefaultView;
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                    this.rowIdColumn = AppSchema.NatureOfPayment.NATURE_PAY_IDColumn.ColumnName;
                    gvPaymentNature.HideColumn = rowIdColumn;
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        #region Download Excel
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
        #endregion

        #region Events
        protected void gvPaymentNature_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "PaymentNature" + DateTime.Now.Ticks.ToString();
                SetNatureOfPaymentSource();
                if (!ExportNatureOfPayment.Equals(null))
                {
                    ExportNatureOfPayment.Columns.Remove(NATURE_PAY_ID);
                    CommonMethod.WriteExcelFile(ExportNatureOfPayment, fileName);
                    DownLoadFile(fileName);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + Environment.NewLine + ex.Source);
            }
        }
        #endregion
    }
}