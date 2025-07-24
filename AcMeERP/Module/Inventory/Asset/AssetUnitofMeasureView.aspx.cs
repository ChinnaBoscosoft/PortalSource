using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bosco.Utility;
using Bosco.Model;
using System.IO;
using Bosco.DAO.Data;
using AcMeERP.Base;

namespace AcMeERP.Module.Inventory.Asset
{
    public partial class AssetUnitofMeasureView : Base.UIBase
    {
        #region Declaration
        private DataView unitofMeasureViewSource = null;
        private DataTable UnitofMeasureSourceToExport = null;
        private const string UOM_ID = "UOM_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            targetPage = this.GetPageUrlByName(URLPages.AssetUnitofMeasureAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.AssetUnitofMeasureView;
            SetUnitofMeasureViewSource();

            gvUOMView.RowCommand += new GridViewCommandEventHandler(gvUOMView_RowCommand);
            gvUOMView.ExportClicked += new EventHandler(gvUOMView_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvUOMView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvUOMView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvUOMView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.UnitofMeasureAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvUOMView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.UnitofMeasureEdit, true,
                           base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvUOMView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.UnitofMeasureDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvUOMView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvUOMView.HideColumn = this.hiddenColumn;
            gvUOMView.RowIdColumn = this.rowIdColumn;
            gvUOMView.DataSource = unitofMeasureViewSource;
            //this.ShowLoadWaitPopUp();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Asset.AssetUnitofMeasureViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.UnitofMeasureView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                gvUOMView.ShowExport = true;
            }
        }

        protected void gvUOMView_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "UnitofMeasure" + DateTime.Now.Ticks.ToString();
                SetUnitofMeasureViewSource();
                if (!UnitofMeasureSourceToExport.Equals(null))
                {
                    UnitofMeasureSourceToExport.Columns.Remove(UOM_ID);
                    CommonMethod.WriteExcelFile(UnitofMeasureSourceToExport, fileName);
                    DownLoadFile(fileName);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void gvUOMView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int unitofId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (unitofId > 0)
                {
                    using (AssetUnitOfMeassureSystem uomSystem = new AssetUnitOfMeassureSystem())
                    {
                        uomSystem.UnitId = unitofId;
                        resultArgs = uomSystem.DeleteMeasureDetails();

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.Asset.AssetUOMDeleted;
                            SetUnitofMeasureViewSource();
                            gvUOMView.BindGrid(unitofMeasureViewSource);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }

            }
        }

        #endregion

        #region Methods

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

        private void SetUnitofMeasureViewSource()
        {
            using (AssetUnitOfMeassureSystem uomSystem = new AssetUnitOfMeassureSystem())
            {
                ResultArgs resultArgs = uomSystem.FetchMeasureDetails();

                if (resultArgs.Success)
                {
                    unitofMeasureViewSource = resultArgs.DataSource.Table.DefaultView;
                    UnitofMeasureSourceToExport = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = uomSystem.AppSchema.AssetUnitofMeasure.UOM_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
        #endregion
    }
}