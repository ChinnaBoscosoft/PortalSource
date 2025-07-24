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
    public partial class HeadOfficeListBranchOffice : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;
        private static object objLock = new object();
        #endregion

        #region Properties

        //private int BranchId
        //{
        //    set
        //    {
        //        ViewState["BranchId"] = value;
        //    }
        //    get
        //    {
        //        return this.Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
        //    }
        //}

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = "Head Office wise Branch Office";
            }
            LoadHOwiseBranch();
        }

        protected void aspxBtnExcel_Click(object sender, EventArgs e)
        {
            XlsExportOptions xlsexport = new XlsExportOptions();
            xlsexport.SheetName = "Head_Office_wise_Branch_Office";
            this.gridHOwiseBranchList.WriteXlsToResponse("Head_Office_wise_Branch_Office", true, xlsexport);
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
                    // string dateFitler = (this.apxDate.Date == DateTime.MinValue ? string.Empty : this.Member.DateSet.ToDate(this.apxDate.Date.ToShortDateString()));
                    if (base.LoginUser.IsPortalAdminUser && this.LoginUser.IsPortalAdminUser && this.LoginUser.LoginUserName.ToUpper() == "ADMIN")
                    {
                        resultArgs = branchsystem.FetchHeadOfficewiseBranchOffice();
                    //    gvHOwiseBranchlistdetails.Columns[0].Visible = gvHOwiseBranchlistdetails.Columns[1].Visible = true;
                    }
                    ////base.LoginUser.IsPortalAdminUser && this.LoginUser.IsHeadOfficeAdminUser.. chinna
                    //else if (base.LoginUser.IsPortalAdminUser || this.LoginUser.IsHeadOfficeAdminUser)
                    //{
                    //    resultArgs = branchsystem.FetchBranchLoggedHistoryByHeadOfficeCode(this.HeadOfficeCode, dateFitler);
                    //    gvBranchLoggedHistory.Columns[0].Visible = gvBranchLoggedHistory.Columns[1].Visible = false;
                    //}
                    //else
                    //{
                    //    resultArgs = branchsystem.FetchBranchLoggedHistoryByHeadOfficeCode("test", dateFitler);
                    //}

                    if (resultArgs != null && resultArgs.Success)
                    {
                        //DataTable dtHOBranchOffice = resultArgs.DataSource.Table;
                        //dtHOBranchOffice.DefaultView.Sort = "LOGGED_ON DESC";
                        //dtHOBranchOffice = dtHOBranchOffice.DefaultView.Table;// dtHOBranchOffice;

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