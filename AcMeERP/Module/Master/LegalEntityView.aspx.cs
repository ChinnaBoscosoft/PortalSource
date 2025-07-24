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
 * Purpose          :This page helps head office admin to view the legal entity for the entire head office
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
    public partial class LegalEntityView : Base.UIBase
    {
        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        private DataView LegalEntityViewResource = null;
        private DataTable LegalEntitySourceToExport = null;
        private const string CUSTOMERID = "CUSTOMERID";
        private const string SELECT = "SELECT";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Events

        #region Property

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.LegalEntity.LegalEntityViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.LegalEntityView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                //     this.ShowLoadWaitPopUp();
                gvLegalEntity.ShowExport = true;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.LegalEntityAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.LegalEntityView;
            BranchId = 0;
            if (base.LoginUser.IsBranchOfficeUser)
                FetchBranchId();
            SetLegalEntityViewSource();

            gvLegalEntity.RowCommand += new GridViewCommandEventHandler(gvLegalEntity_RowCommand);
            gvLegalEntity.ExportClicked += new EventHandler(gvLegalEntity_ExportClicked);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvLegalEntity.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvLegalEntity.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvLegalEntity.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LegalEntityAdd, true,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLegalEntity.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LegalEntityEdit, true,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLegalEntity.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.LegalEntityDelete, true,
                      base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvLegalEntity.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }
            gvLegalEntity.HideColumn = this.hiddenColumn;
            gvLegalEntity.RowIdColumn = this.rowIdColumn;
            gvLegalEntity.DataSource = LegalEntityViewResource;
        }


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
        protected void gvLegalEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ResultArgs resultArgs = new ResultArgs();
            int LegalEntityId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (LegalEntityId != 0)
                {
                    using (LegalEntitySystem LegalEntitySystem = new LegalEntitySystem())
                    {
                        resultArgs = LegalEntitySystem.DeleteLegalEntityData(LegalEntityId, DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.LegalEntityDeleted;
                            SetLegalEntityViewSource();
                            gvLegalEntity.BindGrid(LegalEntityViewResource);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
        }

        protected void gvLegalEntity_ExportClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = "LegalEntitySource" + DateTime.Now.Ticks.ToString();
                SetLegalEntityViewSource();
                if (!LegalEntitySourceToExport.Equals(null))
                {
                    LegalEntitySourceToExport.Columns.Remove(CUSTOMERID);
                    LegalEntitySourceToExport.Columns.Remove(SELECT);

                    CommonMethod.WriteExcelFile(LegalEntitySourceToExport, fileName);
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

        private void SetLegalEntityViewSource()
        {
            using (LegalEntitySystem LegalEntitySystem = new LegalEntitySystem())
            {
                if (BranchId != 0)
                    LegalEntitySystem.BranchId = BranchId;
                ResultArgs resultArgs = LegalEntitySystem.FetchLegalEntity(DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    LegalEntityViewResource = resultArgs.DataSource.Table.DefaultView;
                    LegalEntitySourceToExport = resultArgs.DataSource.Table;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = LegalEntitySystem.AppSchema.LegalEntity.CUSTOMERIDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn + "," + LegalEntitySystem.AppSchema.LegalEntity.SELECTColumn.ColumnName + "," + "SOCIETY_FILTER";

            }
        }

        #endregion
    }
}