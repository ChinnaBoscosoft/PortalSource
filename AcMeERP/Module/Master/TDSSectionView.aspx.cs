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
 * Purpose          :This page helps head office admin/user or branch office admin/user to view the TDS Sections
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
    public partial class TDSSectionView : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        private ResultArgs resultArgs = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private DataView TDSSecionView = null;
        private const string TDS_SECTION_ID = "TDS_SECTION_ID";
        private DataTable ExportTDSSessionView = null;

        #endregion

        #region Events
        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            SetTDSSection();
            gvTDSSectionView.ShowAdd = false;
            gvTDSSectionView.ExportClicked += new EventHandler(gvTDSSectionView_ExportClicked);
            gvTDSSectionView.DataSource = TDSSecionView;
            ExportTDSSessionView = TDSSecionView.ToTable();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.TDS.TDSSectionPageTitle;
                gvTDSSectionView.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvTDSSectionView_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "TDSSession" + DateTime.Now.Ticks.ToString();
                SetTDSSection();
                if (!ExportTDSSessionView.Equals(null))
                {
                    ExportTDSSessionView.Columns.Remove(TDS_SECTION_ID);
                    CommonMethod.WriteExcelFile(ExportTDSSessionView, fileName);
                    DownLoadFile(fileName);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        #endregion

        #region Methods

        private void SetTDSSection()
        {
            try
            {
                using (TDSSystem tdsSystem = new TDSSystem())
                {
                    resultArgs = tdsSystem.FetchTDSSectionDetails();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        TDSSecionView = resultArgs.DataSource.Table.DefaultView;
                    }
                    else
                    {
                        new ErrorLog().WriteError(resultArgs.Message);
                    }
                    this.rowIdColumn = AppSchema.TDSSection.TDS_SECTION_IDColumn.ColumnName;
                    gvTDSSectionView.HideColumn = rowIdColumn;
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