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
 * Purpose          :This page helps head office admin/user or branch office admin/user to view Budget available by the role wise
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

namespace AcMeERP.Module.Master
{
    public partial class BudgetView : Base.UIBase
    {

        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        private DataView BudgetViewSource = null;
        private DataTable BudgetSourcetoExport = null;
        private const string BUDGET_ID = "BUDGET_ID";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Properties

        private int BranchId
        {
            set
            {
                ViewState["BranchId"] = value;
            }
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
            }
        }

        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.BudgetAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.BudgetView;

            BranchId = 0;
            if (base.LoginUser.IsBranchOfficeUser)
                FetchBranchId();

            SetBudgetViewSource();
            gvBudget.RowCommand += new GridViewCommandEventHandler(gvBudget_RowCommand);
            gvBudget.ExportClicked += new EventHandler(gvBudget_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvBudget.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvBudget.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvBudget.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser || this.LoginUser.IsBranchOfficeAdminUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.BudgetAdd, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvBudget.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.BudgetEdit, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvBudget.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.BudgetDelete, true,
                         base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvBudget.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvBudget.HideColumn = this.hiddenColumn;
            gvBudget.RowIdColumn = this.rowIdColumn;
            gvBudget.DataSource = BudgetViewSource;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Master.Budget.BudgetViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.BudgetView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.ShowLoadWaitPopUp(); // Enabled on 18/04/2024
                gvBudget.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvBudget_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ResultArgs resultArgs = new ResultArgs();
            int BudgetId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (BudgetId != 0)
                {
                    using (BudgetSystem BudgetSystem = new BudgetSystem())
                    {
                        resultArgs = BudgetSystem.DeleteBudgetDetails(BudgetId, DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = "Deleted";
                            SetBudgetViewSource();
                            gvBudget.BindGrid(BudgetViewSource);
                        }
                        else
                        {
                            //this.Message = MessageCatalog.Message.Project.DenyProjectDeletion;
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
        }

        protected void gvBudget_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "BudgetSource" + DateTime.Now.Ticks.ToString();
                SetBudgetViewSource();
                if (!BudgetSourcetoExport.Equals(null))
                {
                    BudgetSourcetoExport.Columns.Remove(BUDGET_ID);
                    CommonMethod.WriteExcelFile(BudgetSourcetoExport, fileName);
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

        #region Method

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

        private void SetBudgetViewSource()
        {
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                budgetsystem.BranchId = BranchId;
                ResultArgs resultArgs = budgetsystem.FetchBudgetView();
                if (resultArgs.Success)
                {
                    if (base.LoginUser.IsHeadOfficeUser || base.LoginUser.IsHeadOfficeAdminUser)
                    {
                        BudgetViewSource = resultArgs.DataSource.Table.DefaultView;
                    }
                    else if (base.LoginUser.IsBranchOfficeUser || base.LoginUser.IsBranchOfficeAdminUser)
                    {
                        if (!string.IsNullOrEmpty(base.LoginUser.LoginUserBranchOfficeName))
                        {
                            BudgetViewSource = resultArgs.DataSource.Table.DefaultView;
                        }
                    }
                    BudgetSourcetoExport = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }
                this.rowIdColumn = "BUDGET_ID";
                this.hiddenColumn = this.rowIdColumn;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FetchBranchId()
        {
            try
            {
                using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                {
                    BranchOffice.BranchOfficeCode = base.LoginUser.LoginUserBranchOfficeCode;
                    ResultArgs resultArgs = BranchOffice.FetchBranch(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        BranchId = this.Member.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }
        #endregion

    }
}