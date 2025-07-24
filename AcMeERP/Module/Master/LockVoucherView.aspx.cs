/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 22 June 2017
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page helps head office admin/user or branch office admin/user to view LockVoucher available by the role wise
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel.Master;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using AcMeERP.Base;
using System.IO;

namespace AcMeERP.Module.Master
{
    public partial class LockVoucherView : Base.UIBase
    {
        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        private DataView LockedView = null;
        private DataTable LockViewExport = null;
        private const string LOCK_TRANS_ID = "LOCK_TRANS_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Events

        /// <summary>
        /// Page Load the Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.LockVoucher.LockViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.AuditLockView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                gvLockView.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        /// <summary>
        /// Page Init Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.LockVoucherAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.LockVoucherView;
            SetLockSource();

            gvLockView.RowCommand += new GridViewCommandEventHandler(gvLockView_RowCommand);
            gvLockView.ExportClicked += new EventHandler(gvLockView_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvLockView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvLockView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvLockView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LockVoucherAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLockView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LockVoucherEdit, true,
                           base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLockView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LockVoucherDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLockView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvLockView.HideColumn = this.hiddenColumn;
            gvLockView.RowIdColumn = this.rowIdColumn;
            gvLockView.DataSource = LockedView;
        }

        /// <summary>
        /// fetch the rows Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLockView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int LockVoucherId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (LockVoucherId > 0)
                {
                    using (LockVoucherSystem LockVoucherSystem = new LockVoucherSystem())
                    {
                        resultArgs = LockVoucherSystem.DeleteLockDetails(LockVoucherId, DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.LockVoucherDeleted;
                            SetLockSource();
                            gvLockView.BindGrid(LockedView);
                        }
                        else
                        {
                            // this.Message = resultArgs.Message + MessageCatalog.Message.LedgerMapping.ProjectCategoryDelete;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Export the Lock Voucher Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void gvLockView_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "LockVoucher" + DateTime.Now.Ticks.ToString();
                SetLockSource();
                if (!LockViewExport.Equals(null))
                {
                    LockViewExport.Columns.Remove(LOCK_TRANS_ID);
                    CommonMethod.WriteExcelFile(LockViewExport, fileName);
                    DownLoadFile(fileName);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        /// <summary>
        /// To Download the File Backup 
        /// </summary>
        /// <param name="fileName"></param>
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

        #region Methods

        /// <summary>
        /// Load the Lock Features
        /// </summary>
        private void SetLockSource()
        {
            using (LockVoucherSystem LockVoucher = new LockVoucherSystem())
            {
                ResultArgs resultArgs = LockVoucher.FetchLockedProjects(DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    LockedView = resultArgs.DataSource.Table.DefaultView;
                    LockViewExport = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = this.AppSchema.LockVoucher.LOCK_TRANS_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
        #endregion
    }
}