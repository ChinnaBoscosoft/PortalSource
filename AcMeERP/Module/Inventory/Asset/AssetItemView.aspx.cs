using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using AcMeERP.Base;
using Bosco.Model;
using System.IO;

namespace AcMeERP.Module.Inventory.Asset
{
    public partial class AssetItemView : Base.UIBase
    {
        #region Declaration
        private DataView AssetItemViewSource = null;
        private DataTable AssetItemSourceToExport = null;
        private const string ITEM_ID = "ITEM_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            targetPage = this.GetPageUrlByName(URLPages.AssetItemAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.AssetItemView;
            SetAssetItemViewSource();

            gvAssetItem.RowCommand += new GridViewCommandEventHandler(gvAssetItem_RowCommand);
            gvAssetItem.ExportClicked += new EventHandler(gvAssetItem_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvAssetItem.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvAssetItem.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvAssetItem.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.AssetItemAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvAssetItem.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.AssetItemEdit, true,
                           base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvAssetItem.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.AssetItemDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvAssetItem.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvAssetItem.HideColumn = this.hiddenColumn;
            gvAssetItem.RowIdColumn = this.rowIdColumn;
            gvAssetItem.DataSource = AssetItemViewSource;
            //this.ShowLoadWaitPopUp();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Asset.AssetItemViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.AssetItemView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                gvAssetItem.ShowExport = true;
            }
        }

        protected void gvAssetItem_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "AssetItems" + DateTime.Now.Ticks.ToString();
                SetAssetItemViewSource();
                if (!AssetItemSourceToExport.Equals(null))
                {
                    AssetItemSourceToExport.Columns.Remove(ITEM_ID);
                    CommonMethod.WriteExcelFile(AssetItemSourceToExport, fileName);
                    DownLoadFile(fileName);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void gvAssetItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int assetItemId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (assetItemId > 0)
                {
                    using (AssetItemSystem itemSystem = new AssetItemSystem())
                    {
                        itemSystem.ItemId = assetItemId;
                        resultArgs = itemSystem.DeleteAssetItem();

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.Asset.AssetItemDeleted;
                            SetAssetItemViewSource();
                            gvAssetItem.BindGrid(AssetItemViewSource);
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

        private void SetAssetItemViewSource()
        {
            using (AssetItemSystem itemSystem = new AssetItemSystem())
            {
                ResultArgs resultArgs = itemSystem.FetchAllAssetItems();

                if (resultArgs.Success)
                {
                    AssetItemViewSource = resultArgs.DataSource.Table.DefaultView;
                    AssetItemSourceToExport = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = itemSystem.AppSchema.AssetItems.ITEM_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
        #endregion

    }
}