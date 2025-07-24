using System;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Configuration;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.XtraPrinting;
using DevExpress.Web.ASPxGridView;

namespace AcMeERP.Module.Office
{
    public partial class Branch_Location_Logged_History : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;
        private static object objLock = new object();
        #endregion

        #region Properties

        private DataTable dtgvMorethanoneBranchSystem
        {
            set
            {
                ViewState["dtgvMorethanoneBranchSystem"] = value;
            }
            get
            {
                return (DataTable)ViewState["dtgvMorethanoneBranchSystem"];
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.BranchLoggedHistoryTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
            }
            showBranchUsingMorethanOneSystemUsingAcmeerp();
        }

        protected void aspxBtnExcel_Click(object sender, EventArgs e)
        {
            XlsExportOptions xlsexport = new XlsExportOptions();
            xlsexport.SheetName = "Acmeerp_Branch_Location_Logged_History";
            this.gridExportBranchLocationLoggedHistory.WriteXlsxToResponse("Acmeerp_Branch__Location_Logged_History", true);
        }

        protected void aspxBtnPdf_Click(object sender, EventArgs e)
        {
            this.gridExportBranchLocationLoggedHistory.WritePdfToResponse("Acmeerp_Branch__Location_Logged_History", true);
        }

        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            gvMorethanoneBranchSystem.DetailRows.ExpandAllRows();
        }
        #endregion

        #region Methods
        /// <summary>
        /// On 12/05/2022, Show more than one branch system using Acmerp in Local Communities
        /// </summary>
        /// <param name="NextNoOfDays"></param>
        private void showBranchUsingMorethanOneSystemUsingAcmeerp()
        {
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                ResultArgs resultArgs = null;
                string hocode = string.Empty;
                if (base.LoginUser.IsPortalAdminUser && this.LoginUser.IsPortalAdminUser && this.LoginUser.LoginUserName.ToUpper() == "ADMIN")
                {
                    gvMorethanoneBranchSystem.Columns[0].Visible = true;
                    gvMorethanoneBranchSystem.Columns[1].Visible = true;
                    gvMorethanoneBranchSystem.Columns[2].Visible = true;
                }
                //base.LoginUser.IsPortalAdminUser && this.LoginUser.IsHeadOfficeAdminUser.. chinna
                else if (base.LoginUser.IsPortalAdminUser || this.LoginUser.IsHeadOfficeAdminUser)
                {
                    hocode = this.HeadOfficeCode;
                    gvMorethanoneBranchSystem.Columns[0].Visible =  false;
                    gvMorethanoneBranchSystem.Columns[1].Visible = true;
                    gvMorethanoneBranchSystem.Columns[2].Visible = true;
                }
                resultArgs = branchOfficeSystem.FetchBranchHistoryMoreThanOneSystem(DataBaseType.Portal, true, hocode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtgvMorethanoneBranchSystem = resultArgs.DataSource.Table;
                    showBranchUsingMorethanOneSystemUsingAcmeerpExtracted();
                }
            }
        }

        private void showBranchUsingMorethanOneSystemUsingAcmeerpExtracted()
        {
            DataTable dtMaster = dtgvMorethanoneBranchSystem.DefaultView.ToTable(true,
                                            new string[] { "BRANCH_OFFICE_CODE", "HEAD_OFFICE_CODE", "BRANCH_OFFICE_NAME", "HEAD_OFFICE_NAME", "IPs" });
            //dtMaster.DefaultView.Sort = "IPs DESC, HEAD_OFFICE_NAME, BRANCH_OFFICE_NAME";
            dtMaster.DefaultView.Sort = "HEAD_OFFICE_NAME, BRANCH_OFFICE_NAME, IPs DESC";
            gvMorethanoneBranchSystem.DataSource = dtMaster;
            gvMorethanoneBranchSystem.DataBind();
        }

        protected void gvMorethanoneBranchSystemDetails_BeforePerformDataSelect(object sender, EventArgs e)
        {
            string hcode = string.Empty;
            string bcode = string.Empty;

            if (dtgvMorethanoneBranchSystem != null)
            {
                try
                {
                    ASPxGridView grid = sender as ASPxGridView;
                    if (!string.IsNullOrEmpty(grid.GetMasterRowKeyValue().ToString()))
                    {
                        string[] ValueIds = grid.GetMasterRowKeyValue().ToString().Split(',');
                        if (ValueIds != null)
                        {
                            //0-index-head office code, 1-branch office code
                            //hcode = "SDBINM";// ValueIds[0].ToString();
                            bcode = ValueIds[0].ToString();
                        }
                    }
                    DataTable dtgvMorethanoneBranchSystemDetails = dtgvMorethanoneBranchSystem.DefaultView.ToTable();
                    //this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName + " = '" + hcode + "' AND " +
                    string filter = this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName + " = '" + bcode + "'";
                    dtgvMorethanoneBranchSystemDetails.DefaultView.RowFilter = filter;
                    //dtgvMorethanoneBranchSystemDetails.DefaultView.Sort = "LOGGED_ON DESC";
                    dtgvMorethanoneBranchSystemDetails.DefaultView.Sort = "LICENSE_KEY_NUMBER DESC, LOGGED_ON DESC, REMARKS";

                    if (dtgvMorethanoneBranchSystemDetails.DefaultView.Count > 0)
                    {
                        grid.DataSource = dtgvMorethanoneBranchSystemDetails.DefaultView.ToTable();
                    }
                }
                catch (Exception ex)
                {
                    string exception = ex.ToString();
                    this.Message = ex.Message;
                }
            }
        }

        protected void gvMorethanoneBranchSystem_Load(object sender, EventArgs e)
        {
            showBranchUsingMorethanOneSystemUsingAcmeerpExtracted();
        }
        
        #endregion

        
    }
}