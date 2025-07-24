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

namespace AcMeERP.Module.Office
{
    public partial class Branch_Logged_History : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;
        private static object objLock = new object();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.BranchLoggedHistoryTitle;
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
            }
            LoadBranchLoggedHistory();
        }

        protected void aspxBtnExcel_Click(object sender, EventArgs e)
        {
            XlsExportOptions xlsexport = new XlsExportOptions();
            xlsexport.SheetName = "Acmeerp_Branch_Logged_History";
            this.gridBranchLoggedHistory.WriteXlsToResponse("Acmeerp_Branch_Logged_History", true, xlsexport);
        }

        protected void aspxBtnPdf_Click(object sender, EventArgs e)
        {
            this.gridBranchLoggedHistory.WritePdfToResponse("Acmeerp_BO_Backup", true);
        }

        protected void apxDate_DateChanged(object sender, EventArgs e)
        {
            LoadBranchLoggedHistory();
        }
        #endregion

        #region Methods
        private void LoadBranchLoggedHistory()
        {
            try
            {
                using (BranchOfficeSystem branchsystem = new BranchOfficeSystem())
                {
                    string dateFitler = (this.apxDate.Date == DateTime.MinValue ? string.Empty : this.Member.DateSet.ToDate(this.apxDate.Date.ToShortDateString()));
                    if (base.LoginUser.IsPortalAdminUser && this.LoginUser.IsPortalAdminUser && this.LoginUser.LoginUserName.ToUpper() == "ADMIN")
                    {
                        resultArgs = branchsystem.FetchBranchLoggedHistoryByHeadOfficeCode(string.Empty, dateFitler);
                        gvBranchLoggedHistory.Columns[0].Visible = gvBranchLoggedHistory.Columns[1].Visible = true;
                    }
                    //base.LoginUser.IsPortalAdminUser && this.LoginUser.IsHeadOfficeAdminUser.. chinna
                    else if (base.LoginUser.IsPortalAdminUser || this.LoginUser.IsHeadOfficeAdminUser)
                    {
                        resultArgs = branchsystem.FetchBranchLoggedHistoryByHeadOfficeCode(this.HeadOfficeCode, dateFitler);
                        gvBranchLoggedHistory.Columns[0].Visible = gvBranchLoggedHistory.Columns[1].Visible = false;
                    }
                    else
                    {
                        resultArgs = branchsystem.FetchBranchLoggedHistoryByHeadOfficeCode("test", dateFitler);
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtBranchLoggedHistory = resultArgs.DataSource.Table;
                        dtBranchLoggedHistory.DefaultView.Sort = "LOGGED_ON DESC";
                        dtBranchLoggedHistory = dtBranchLoggedHistory.DefaultView.Table;
                        gvBranchLoggedHistory.DataSource = dtBranchLoggedHistory;
                        gvBranchLoggedHistory.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        #endregion




    }
}