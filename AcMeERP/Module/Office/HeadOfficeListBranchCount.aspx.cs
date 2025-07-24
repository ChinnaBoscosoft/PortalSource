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
    public partial class HeadOfficeListBranchCount : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;
        private static object objLock = new object();
        #endregion

        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = "Head Office wise Branch Count";
            }
            LoadHOwiseBranch();
        }

        protected void aspxBtnExcel_Click(object sender, EventArgs e)
        {
            XlsExportOptions xlsexport = new XlsExportOptions();
            xlsexport.SheetName = "Head_Office_wise_Branch_Count";
            this.gridHOwiseBranchList.WriteXlsToResponse("Head_Office_wise_Branch_Count", true, xlsexport);
        }

        protected void aspxBtnPdf_Click(object sender, EventArgs e)
        {
            this.gridHOwiseBranchList.WritePdfToResponse("Acmeerp_BO_Backup", true);
        }

        protected void apxDate_DateChanged(object sender, EventArgs e)
        {
            LoadHOwiseBranch();
        }
        #endregion

        #region Methods
        private void LoadHOwiseBranch()
        {
            try
            {
                using (BranchOfficeSystem branchsystem = new BranchOfficeSystem())
                {
                    if (base.LoginUser.IsPortalAdminUser && this.LoginUser.IsPortalAdminUser && this.LoginUser.LoginUserName.ToUpper() == "ADMIN")
                    {
                        resultArgs = branchsystem.FetchHeadOfficewiseBranchOfficeCount();
                    }

                    if (resultArgs != null && resultArgs.Success)
                    {
                        gvHOwiseBranchlistdetails.DataSource = resultArgs.DataSource.Table;
                        gvHOwiseBranchlistdetails.DataBind();
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