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
 * Purpose          :This page helps head office admin/user or branch office admin/user to view their concern project category mapped ledgers by role
 *****************************************************************************************************/
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.IO;
using Bosco.Model.UIModel.Master;

namespace AcMeERP.Module.Master
{
    public partial class LedgerView : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();

        private DataView LedgerViewResource = null;
        private DataTable LedgerViewSourceToExport = null;

        private const string LEDGER_ID = "LEDGER_ID";
        private const string GROUP_ID = "GROUP_ID";
        private const string LEDGER_TYPE = "LEDGER_TYPE";
        private const string LEDGER_SUB_TYPE = "LEDGER_SUB_TYPE";
        private const string BANK_ACCOUNT_ID = "BANK_ACCOUNT_ID";
        private const string ACCESS_FLAG = "ACCESS_FLAG";
        private const string NATURE_ID = "NATURE_ID";

        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        private ledgerSubType ledgerSubType;
        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.LedgerAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.LedgerView;
            SetLedgerViewSource();

            if (this.LoginUser.IsAdminUser || this.LoginUser.IsHeadOfficeUser)
            {
                gvLedgerView.RowCommand += new GridViewCommandEventHandler(gvLedgerView_RowCommand);
                gvLedgerView.RowDataBound += new GridViewRowEventHandler(gvLedgerView_RowDataBound);
                gvLedgerView.ExportClicked += new EventHandler(gvLedgerView_ExportClicked);
            }
            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.Ledger.LedgerAddCaption, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvLedgerView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvLedgerView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvLedgerView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerAdd, true,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLedgerView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerEdit, true,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLedgerView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerDelete, true,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLedgerView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvLedgerView.HideColumn = this.hiddenColumn;
            gvLedgerView.RowIdColumn = this.rowIdColumn;
            gvLedgerView.DataSource = LedgerViewResource;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string SubType = string.Empty;
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Ledger.LedgerViewPageTitle;
                SubType = ledgerSubType == ledgerSubType.GN ? "Ledger" : "FD Ledger";
                this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                //       this.ShowLoadWaitPopUp();
                gvLedgerView.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }


        protected void gvLedgerView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int LedgerId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (LedgerId != 0)
                {
                    using (LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem())
                    {
                        ledgerProfileSystem.LedgerID = LedgerId;
                        resultArgs = ledgerProfileSystem.DeleteLedgerProfile(DataBaseType.HeadOffice);
                        using (LedgerSystem LedgerSystem = new LedgerSystem())
                        {
                            resultArgs = LedgerSystem.DeleteLedgerDetails(LedgerId, DataBaseType.HeadOffice);

                            if (resultArgs != null && resultArgs.Success)
                            {
                                this.Message = MessageCatalog.Message.LedgerDeleted;
                                SetLedgerViewSource();
                                gvLedgerView.BindGrid(LedgerViewResource);
                            }
                            else
                            {
                                this.Message = MessageCatalog.Message.Ledger.DenyLedgerDeletion;
                            }
                        }
                    }
                }
            }
        }

        protected void gvLedgerView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HyperLink hlkEdit = (HyperLink)e.Row.FindControl("hlkEdit");
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                    string AccessFlag = "0";
                    object dr = e.Row.DataItem;
                    DataRowView tempDr = (DataRowView)dr;
                    AccessFlag = tempDr["ACCESS_FLAG"].ToString();

                    if (e.Row.Cells[1].Text.Equals("Cash") || e.Row.Cells[2].Text.Equals("Bank Accounts") || AccessFlag.Equals("2"))
                    {
                        hlkEdit.ImageUrl = "~/App_Themes/MainTheme/images/GridEdit.png";
                        imgDelete.ImageUrl = "~/App_Themes/MainTheme/images/DeleteDisable.png";
                        imgDelete.Enabled = hlkEdit.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        protected void gvLedgerView_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "LedgerSource" + DateTime.Now.Ticks.ToString();
                SetLedgerViewSource();
                if (!LedgerViewSourceToExport.Equals(null))
                {
                    LedgerViewSourceToExport.DefaultView.Sort = "NATURE_ID, Code, Group, Name";
                    LedgerViewSourceToExport.Columns.Remove(LEDGER_ID);
                    LedgerViewSourceToExport.Columns.Remove(GROUP_ID);
                    LedgerViewSourceToExport.Columns.Remove(LEDGER_TYPE);
                    LedgerViewSourceToExport.Columns.Remove(LEDGER_SUB_TYPE);
                    LedgerViewSourceToExport.Columns.Remove(BANK_ACCOUNT_ID);
                    LedgerViewSourceToExport.Columns.Remove(ACCESS_FLAG);
                    LedgerViewSourceToExport.Columns.Remove(NATURE_ID);
                    CommonMethod.WriteExcelFile(LedgerViewSourceToExport.DefaultView.ToTable(), fileName);
                    DownLoadFile(fileName);
                }
                else
                {
                    this.Message = MessageCatalog.Message.NoRecordToExport;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
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

        private void SetLedgerViewSource()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    DataView LedgerView = null;
                    ResultArgs resultArgs = ledgerSystem.FetchLedgerDetails(DataBaseType.HeadOffice);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        LedgerView = resultArgs.DataSource.Table.DefaultView;
                        LedgerViewSourceToExport = resultArgs.DataSource.Table;
                        if (resultArgs.RowsAffected > 0)
                        {
                            if (base.LoginUser.IsBranchOfficeUser)
                            {
                                ledgerSystem.BranchOfficeCode = base.LoginUser.LoginUserBranchOfficeCode;
                                resultArgs = ledgerSystem.FetchBranchLedgers(DataBaseType.HeadOffice);
                                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    LedgerView.Table.Merge(resultArgs.DataSource.Table);
                                }
                            }

                            //if (ledgerSubType == ledgerSubType.FD)
                            //{
                            //    LedgerView.RowFilter = "GROUP_ID  IN (14)";
                            //    //colGroup.Visible = false;
                            //}
                            //else
                            //{
                            //    LedgerView.RowFilter = "GROUP_ID NOT IN (14)";
                            //}
                        }
                    }

                    LedgerViewResource = LedgerView;
                }
            }
            catch (Exception ex)
            {

                this.Message = ex.Message;
            }

            this.rowIdColumn = this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName;
            this.hiddenColumn = this.rowIdColumn + "," + this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn.ColumnName + "," + this.AppSchema.Ledger.LEDGER_TYPEColumn.ColumnName + "," + this.AppSchema.Ledger.GROUP_IDColumn.ColumnName + "," + this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn.ColumnName + "," + this.AppSchema.Ledger.ACCESS_FLAGColumn.ColumnName + "," + "NATURE_ID";
        }

        #endregion

    }
}