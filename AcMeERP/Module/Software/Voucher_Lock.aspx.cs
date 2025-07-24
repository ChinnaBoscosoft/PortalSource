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
 * Purpose          :This page allows the branch office to upload their branch office database backup to portal server.
 *****************************************************************************************************/
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
using Ionic.Zip;
using System.Linq;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;


namespace AcMeERP.Module.Software
{
    public partial class Voucher_Lock : Base.UIBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        public DataTable dtSourceBackup = new DataTable();
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

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = "Voucher Lock Details";
                this.CheckUserRights(RightsModule.Tools, RightsActivity.UploadVoucherFile, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);

                LoadBackupData();
            }
            //else
            //{
            //    if (ViewState["backupdata"] != null)
            //    {
            //        dtSourceBackup = (DataTable)ViewState["backupdata"];
            //        dtSourceBackup = (DataTable)ViewState["backupdata"];
            //        BindBackupGrid();
            //    }
            //}
        }

        /// <summary>
        /// Load : Back up
        /// </summary>
        private void LoadBackupData()
        {
            try
            {
                if (ViewState["backupdata"] == null)
                {
                    dtSourceBackup = new DataTable();
                    using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                    {
                        resultArgs = branchOfficeSystem.FetchBranchbyGraceDays(
                            string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode)
                                ? DataBaseType.Portal
                                : DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtSourceBackup = resultArgs.DataSource.Table;

                            foreach (DataRow row in dtSourceBackup.Rows)
                            {
                                if (row["GRACE_TMP_DATE_FROM"] != DBNull.Value)
                                {
                                    row["GRACE_TMP_DATE_FROM"] = Convert.ToDateTime(row["GRACE_TMP_DATE_FROM"]).ToString("yyyy-MM-dd");
                                }

                                if (row["GRACE_TMP_DATE_TO"] != DBNull.Value)
                                {
                                    row["GRACE_TMP_DATE_TO"] = Convert.ToDateTime(row["GRACE_TMP_DATE_TO"]).ToString("yyyy-MM-dd");
                                }

                                if (row["GRACE_TMP_VALID_UPTO"] != DBNull.Value)
                                {
                                    row["GRACE_TMP_VALID_UPTO"] = Convert.ToDateTime(row["GRACE_TMP_VALID_UPTO"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }
                        }
                    }
                    ViewState["backupdata"] = dtSourceBackup;
                }

                BindBackupGrid();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["backupdata"] != null)
            {
                dtSourceBackup = ViewState["backupdata"] as DataTable;
            }

            for (int i = 0; i < gvDownloadBackup.VisibleRowCount; i++)
            {
                object branchOfficeObj = gvDownloadBackup.GetRowValues(i, "BRANCH_OFFICE_NAME");
                string branchOffice = (branchOfficeObj != null) ? branchOfficeObj.ToString() : string.Empty;


                object branchOfficeLocationObj = gvDownloadBackup.GetRowValues(i, "location_name");
                string branchOfficeLocation = (branchOfficeLocationObj != null) ? branchOfficeLocationObj.ToString() : string.Empty;

                ASPxTextBox txtGraceDays = gvDownloadBackup.FindRowCellTemplateControl(
                    i, gvDownloadBackup.Columns["colGraceDays"] as GridViewDataColumn, "txtGraceDays"
                ) as ASPxTextBox;

                ASPxDateEdit txtGraceTmpDateFrom = gvDownloadBackup.FindRowCellTemplateControl(
                    i, gvDownloadBackup.Columns["colGraceTmpDateFrom"] as GridViewDataColumn, "txtGraceTmpDateFrom"
                ) as ASPxDateEdit;

                ASPxDateEdit txtGraceTmpDateTo = gvDownloadBackup.FindRowCellTemplateControl(
                    i, gvDownloadBackup.Columns["colGraceTmpDateTo"] as GridViewDataColumn, "txtGraceTmpDateTo"
                ) as ASPxDateEdit;

                ASPxDateEdit txtGraceTmpValidUpto = gvDownloadBackup.FindRowCellTemplateControl(
                    i, gvDownloadBackup.Columns["colGraceTmpValidUpto"] as GridViewDataColumn, "txtGraceTmpValidUpto"
                ) as ASPxDateEdit;

                int graceDays = 0;
                if (txtGraceDays != null && !string.IsNullOrEmpty(txtGraceDays.Text))
                {
                    int.TryParse(txtGraceDays.Text, out graceDays);
                }

                DateTime? graceDateFrom = null;
                if (txtGraceTmpDateFrom != null && txtGraceTmpDateFrom.Value != null && txtGraceTmpDateFrom.Date != DateTime.MinValue)
                {
                    graceDateFrom = txtGraceTmpDateFrom.Date;
                }

                DateTime? graceDateTo = null;
                if (txtGraceTmpDateTo != null && txtGraceTmpDateTo.Value != null && txtGraceTmpDateTo.Date != DateTime.MinValue)
                {
                    graceDateTo = txtGraceTmpDateTo.Date;
                }

                DateTime? graceValidUpto = null;
                if (txtGraceTmpValidUpto != null && txtGraceTmpValidUpto.Value != null && txtGraceTmpValidUpto.Date != DateTime.MinValue)
                {
                    graceValidUpto = txtGraceTmpValidUpto.Date;
                }

                if (graceDateFrom.HasValue && graceDateTo.HasValue && graceDateFrom > graceDateTo)
                {
                    this.Message = "Error: 'Grace Date To' must be greater than 'Grace Date From'.";
                    return;
                }

                if (graceValidUpto.HasValue && graceValidUpto < DateTime.Today)
                {
                    this.Message = "Error: 'Grace Valid Upto' must be a future date.";
                    return;
                }

                string safeBranchOffice = branchOffice.Replace("'", "''");
                string safeBranchOfficeLocation = branchOfficeLocation.Replace("'", "''");



                DataRow[] existingRows = dtSourceBackup.Select(
                    "BRANCH_OFFICE_NAME = '" + safeBranchOffice + "' AND LOCATION_NAME = '" + safeBranchOfficeLocation + "'"
                );

                if (existingRows.Length > 0)
                {
                    foreach (var row in existingRows)
                    {
                        row["ENFORCE_GRACE_DAYS"] = (graceDays == 0) ? "No" : "Yes";
                        row["GRACE_DAYS"] = graceDays;
                        row["GRACE_TMP_DATE_FROM"] = graceDateFrom.HasValue ? (object)graceDateFrom.Value : DBNull.Value;
                        row["GRACE_TMP_DATE_TO"] = graceDateTo.HasValue ? (object)graceDateTo.Value : DBNull.Value;
                        row["GRACE_TMP_VALID_UPTO"] = graceValidUpto.HasValue ? (object)graceValidUpto.Value : DBNull.Value;
                    }
                }
            }

            resultArgs = SaveVoucherLockfeatures(dtSourceBackup);

            if (resultArgs.Success)
            {
                this.Message = "Updated Success";
            }

            ViewState["backupdata"] = dtSourceBackup;
            BindBackupGrid();
        }

        private ResultArgs SaveVoucherLockfeatures(DataTable source)
        {
            try
            {
                using (BranchOfficeSystem branchoffice = new BranchOfficeSystem())
                {
                    branchoffice.dtSource = source;
                    resultArgs = branchoffice.InsertUpdateGraceDays(DataBaseType.HeadOffice);
                }
            }
            catch (Exception e)
            {
                this.Message = e.Message;
            }
            return resultArgs;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            // LoadBackupData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindBackupGrid()
        {
            if (dtSourceBackup.Rows.Count > 0)
            {
                gvDownloadBackup.DataSource = dtSourceBackup;
                gvDownloadBackup.DataBind();
            }
            else
            {
                gvDownloadBackup.DataSource = null;
                gvDownloadBackup.DataBind();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update Response content type based on export type
        /// </summary>
        /// This is to get Branch Code
        /// </summary>
        /// <returns></returns>
        public string GetBranchCode()
        {
            string rtn = string.Empty;
            using (DataManager dataManager = new DataManager(Bosco.DAO.Schema.SQLCommand.BranchOffice.FetchBranchforKeyDownloadByUserId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUser.LoginUserId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                rtn = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_CODE"].ToString();
                rtn = rtn.Replace(",", "','");
                rtn = rtn.Substring(1, rtn.Length - 3);
            }
            return rtn;
        }
        #endregion

    }
}