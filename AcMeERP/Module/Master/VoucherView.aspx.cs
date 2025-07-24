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
 * Purpose          :This page helps the head office admin/user or Branch office Admin/User to view the voucher details by date wise and give their amendments
 *****************************************************************************************************/
using System;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using DevExpress.Web.ASPxGridView;
using AcMEDSync;
using AcMEDSync.Model;
using System.Web.UI;


namespace AcMeERP.Module.Master
{
    public partial class VoucherView : Base.UIBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        DataTable dtTranasaction = null;
        DateTime temp;
        private static string CODE = "CODE";
        private string[] Columns = { "VOUCHER_DATE", "VOUCHER_NO", "VOUCHERTYPE", "LEDGER_NAME", "AMOUNT", "AMENDMENT_FLAG" };
        private static object objLock = new object();

        #endregion

        #region Properties

        private int projectid = 0;
        private int ProjectId
        {
            get
            {
                projectid = cmbProject.SelectedItem.Value.ToString() == "All" ? 0 : this.Member.NumberSet.ToInteger(cmbProject.SelectedItem.Value.ToString());
                return projectid;
            }
            set
            {
                projectid = value;
            }
        }

        private bool IsProjectMapped
        {
            get
            {
                return Convert.ToBoolean(ViewState["IsProjectMapped"]);
            }
            set
            {
                ViewState["IsProjectMapped"] = value;
            }
        }
        private int BranchId
        {
            get
            {
                return Member.NumberSet.ToInteger(ViewState["BranchId"].ToString());
            }
            set
            {
                ViewState["BranchId"] = value;
            }
        }
        private string ProjectIds
        {
            get
            {
                return ViewState["ProjectIds"].ToString();
            }
            set
            {
                ViewState["ProjectIds"] = value;
            }
        }
        private DataTable Vouchers
        {
            get
            {
                return (DataTable)ViewState["Vouchers"];
            }
            set
            {
                ViewState["Vouchers"] = value;
            }
        }

        private DateTime BalanceDate
        {
            get
            {
                return this.Member.DateSet.ToDate(ViewState["BalanceDate"].ToString(), false);
            }
            set
            {
                ViewState["BalanceDate"] = value;
            }
        }

        private int voucherid = 0;
        private int VoucherId
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["VoucherId"].ToString());
            }
            set
            {
                voucherid = value;
                ViewState["VoucherId"] = voucherid;
            }

        }

        private int locationId = 0;
        private int LocationId
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["LocationId"].ToString());
            }
            set
            {
                voucherid = value;
                ViewState["LocationId"] = voucherid;
            }

        }

        private string branchCode = string.Empty;
        private string BranchCode
        {
            get
            {
                return ViewState["BranchCode"].ToString();
            }
            set
            {
                branchCode = value;
                ViewState["BranchCode"] = branchCode;
            }
        }


        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                this.PageTitle = MessageCatalog.Message.Vouchers.VoucherPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.VoucherView, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                LoadBranch();
                LoadProjects();
                SetDate();
                uiOpeningBalance.BalanceCaption = "Opening Balance";
                uiClosingBalance.BalanceCaption = "Closing Balance";
                LoadVouchers();
                this.SetControlFocus(cmbBranch);
                this.ShowLoadWaitPopUp(btnLoad);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }

        }

        protected void gvVoucherTrans_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;
                if (!string.IsNullOrEmpty(grid.GetMasterRowKeyValue().ToString()))
                {
                    string[] ValueIds = grid.GetMasterRowKeyValue().ToString().Split(',');
                    if (ValueIds != null)
                    {
                        //0-index-VoucherId, 1-index-LocationId
                        VoucherId = this.Member.NumberSet.ToInteger(ValueIds[0].ToString());
                        LocationId = this.Member.NumberSet.ToInteger(ValueIds[1].ToString());
                    }

                }
                dtTranasaction = LoadTransaction(VoucherId, BranchId, LocationId);
                if (dtTranasaction.Rows.Count > 0)
                {
                    grid.DataSource = dtTranasaction;
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
                this.Message = ex.Message;
            }

        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchId = Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
            // BranchCode = BranchCode.Substring(6, 6);
            SetDate();
            LoadProjects();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadVouchers();
        }

        protected void gvMasterVoucher_Load(object sender, EventArgs e)
        {
            gvMasterVoucher.DetailRows.CollapseAllRows();
            gvMasterVoucher.DataSource = Vouchers;
            gvMasterVoucher.DataBind();
        }

        protected void gvMasterVoucher_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName == "Amendments")
                {
                    string[] ValueIds = e.KeyValue.ToString().Split(',');
                    if (ValueIds != null)
                    {
                        //0-index-VoucherId, 1-index-LocationId
                        VoucherId = this.Member.NumberSet.ToInteger(ValueIds[0].ToString());
                        LocationId = this.Member.NumberSet.ToInteger(ValueIds[1].ToString());
                        Response.Redirect("~/module/master/AmendmentsAdd.aspx?1=" + VoucherId + "&2=" + BranchId +"&3="+LocationId);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }

        protected void gvMasterVoucher_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                if (e.RowType != GridViewRowType.Data) return;
                int AmendmentFlag = 0;
                AmendmentFlag = this.Member.NumberSet.ToInteger(gvMasterVoucher.GetRowValues(e.VisibleIndex, "AMENDMENT_FLAG").ToString());
                if (AmendmentFlag == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAmendments();
                resultArgs = SendMail();
                if (resultArgs.Success)
                {
                    new ErrorLog().WriteError("Amendment Notification sent::" + MessageCatalog.Message.Mail_Succes);
                }
                else
                {
                    new ErrorLog().WriteError("Amendment Notification sent::" + MessageCatalog.Message.Mail_Failure);
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

        }

        #endregion

        #region Methods

        private void LoadVouchers()
        {
            try
            {
                string VoucherType = GetVoucherType();
                if (!this.Member.DateSet.ValidateDate(dteDateFrom.Date, dteDateTo.Date))
                {
                    temp = dteDateTo.Date;
                    dteDateTo.Date = dteDateFrom.Date;
                    dteDateFrom.Date = temp;

                }

                if (base.LoginUser.IsBranchOfficeUser)
                {
                    gvMasterVoucher.Columns["ColAmendment"].Visible = false;
                }

                if (IsProjectMapped)
                {

                    using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                    {
                        resultArgs = voucherTransactionSystem.FetchVouchers(BranchId, ProjectId, dteDateFrom.Date, dteDateTo.Date, VoucherType);
                        if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            gvMasterVoucher.SettingsDetail.ShowDetailRow = true;
                            gvMasterVoucher.DataSource = resultArgs.DataSource.Table;
                            gvMasterVoucher.DataBind();
                            Vouchers = resultArgs.DataSource.Table;
                            uiOpeningBalance.LoadBalance(BranchId.ToString(), ProjectId == 0 ? ProjectIds : ProjectId.ToString(), dteDateFrom.Value.ToString(), BalanceSystem.BalanceType.OpeningBalance);
                            uiClosingBalance.LoadBalance(BranchId.ToString(), ProjectId == 0 ? ProjectIds : ProjectId.ToString(), dteDateTo.Value.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                        }
                        else
                        {
                            gvMasterVoucher.DataSource = null;
                            gvMasterVoucher.DataBind();
                            Vouchers = resultArgs.DataSource.Table;
                            uiOpeningBalance.LoadBalance(BranchId.ToString(), ProjectId == 0 ? ProjectIds : ProjectId.ToString(), dteDateFrom.Value.ToString(), BalanceSystem.BalanceType.OpeningBalance);
                            uiClosingBalance.LoadBalance(BranchId.ToString(), ProjectId == 0 ? ProjectIds : ProjectId.ToString(), dteDateTo.Value.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                        }
                    }
                }
                else
                {
                    this.Message = MessageCatalog.Message.ProjectNotMapped;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranch(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbBranch, resultArgs.DataSource.Table, CODE, this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, false);
                        if (!base.LoginUser.IsHeadOfficeUser)
                        {
                            if (!string.IsNullOrEmpty(base.LoginUser.LoginUserBranchOfficeCode))
                            {
                                //cmbBranch.SelectedItem.Text = base.LoginUser.LoginUserBranchOfficeCode;                           
                                cmbBranch.Text = base.LoginUser.LoginUserBranchOfficeName;
                                BranchId = Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                                resultArgs = BranchOfficeSystem.FillBranchOfficeDetails(BranchId, DataBaseType.HeadOffice);
                                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    BranchCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                                }
                                cmbBranch.Enabled = false;
                            }
                        }
                        else if (base.LoginUser.IsHeadOfficeUser)
                        {
                            if (this.Member.NumberSet.ToInteger(Session[base.DefaultBranchId].ToString()) != 0)
                            {
                                cmbBranch.Text = Session[base.DefaultBranchId].ToString();
                            }
                            BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                            resultArgs = BranchOfficeSystem.FillBranchOfficeDetails(BranchId, DataBaseType.HeadOffice);
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                BranchCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                            }
                        }
                    }
                    else
                    {
                        BranchId = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private DataTable LoadTransaction(int VoucherId, int BranchId, int LocationId)
        {
            try
            {
                using (VoucherTransactionSystem VoucherTransaction = new VoucherTransactionSystem())
                {
                    resultArgs = VoucherTransaction.FetchTransactions(VoucherId, BranchId, LocationId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        new ErrorLog().WriteError("Voucher view screen" + "Project loading success");
                    }
                    else
                    {
                        new ErrorLog().WriteError("Voucher view screen" + "Project loading success");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return resultArgs.DataSource.Table;
        }

        private void LoadProjects()
        {
            using (BranchOfficeSystem BranchOffiecSystem = new BranchOfficeSystem())
            {
                try
                {
                    resultArgs = BranchOffiecSystem.FetchProjects(BranchId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, true);
                        ProjectId = 0;
                        string projectid = string.Empty;
                        for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                        {
                            projectid += resultArgs.DataSource.Table.Rows[i]["PROJECT_ID"].ToString() + ",";
                        }
                        ProjectIds = projectid.TrimEnd(',');
                        IsProjectMapped = true;
                    }
                    else
                    {
                        this.Member.ComboSet.BindCombo(cmbProject, resultArgs.DataSource.Table, this.AppSchema.Project.PROJECTColumn.ColumnName, this.AppSchema.Project.PROJECT_IDColumn.ColumnName, true);
                        ProjectId = 0;
                        ProjectIds = string.Empty;
                        IsProjectMapped = false;
                    }
                }
                catch (Exception ex)
                {
                    this.Message = ex.ToString();
                }
            }
        }

        private void SetDate()
        {
            BalanceDate = DateTime.Now;
            try
            {
                using (VoucherTransactionSystem VoucherMasterSystem = new VoucherTransactionSystem())
                {
                    resultArgs = VoucherMasterSystem.FetchVoucherDate(BranchId);
                    BalanceDate = VoucherMasterSystem.FetchOPBalanceDate(BranchId, ProjectId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            DateTime dateTo = this.Member.DateSet.ToDate(dr[1].ToString(), false);
                            dteDateFrom.Date = dateTo.Date.AddDays(-(dateTo.Date.Day - 1));
                            int daysInMonth = DateTime.DaysInMonth(dateTo.Date.Year,
                                dateTo.Date.Month);
                            dteDateTo.Date = dteDateFrom.Date.AddDays(daysInMonth - 1);
                        }
                    }
                    else
                    {
                        dteDateFrom.Date = this.Member.DateSet.ToDate(DateTime.Now.AddDays(1 - DateTime.Today.Day).ToString(), false);
                        int daysInMonth = DateTime.DaysInMonth(dteDateFrom.Date.Year, dteDateFrom.Date.Month);
                        dteDateTo.Date = this.Member.DateSet.ToDate(dteDateFrom.Date.ToString(), false).AddDays(daysInMonth - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }

        private string GetVoucherType()
        {
            string voucherType = string.Empty;
            voucherType = (chkReceipt.Checked) ? "RC," : voucherType;
            voucherType = (chkPayment.Checked) ? voucherType + "PY," : voucherType;
            voucherType = (chkContra.Checked) ? voucherType + "CN," : voucherType;
            voucherType = (chkJournal.Checked) ? voucherType + "JN," : voucherType;
            voucherType = voucherType.TrimEnd(',');
            return voucherType;
        }

        private void LoadVoucherFromViewState()
        {
            try
            {
                gvMasterVoucher.DataSource = Vouchers;
                gvMasterVoucher.DataBind();
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }
        private ResultArgs SendMail()
        {
            try
            {
                DataTable dtUserInfo = null;
                string branchUserMailId = string.Empty;
                string HeadOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                DataSynchronizeService.DataSynchronizerClient objService = new DataSynchronizeService.DataSynchronizerClient();
                //string branchMailId = objService.GetBranchMailAddress(BranchOfficeCode);
                string branchMailId = string.Empty;
                using (UserSystem userSystem = new UserSystem())
                {
                    base.HeadOfficeCode = HeadOfficeCode;
                    userSystem.UserName = BranchCode.Substring(6, 6);
                    resultArgs = userSystem.FetchUserDetailsByHeadOfficeCode(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dtUserInfo = resultArgs.DataSource.Table;
                        branchUserMailId = dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString();
                    }
                }
                string Name = "Branch Admin";

                string Header = "You have received amendment notes from the head office for the following voucher.";

                string MainContent = "Voucher Details "
                                      + "<br />"
                                      + "Voucher Date:" + gvMasterVoucher.GetRowValuesByKeyValue(VoucherId, Columns[0]).ToString() + " <br />"
                                      + "Voucher No:" + gvMasterVoucher.GetRowValuesByKeyValue(VoucherId, Columns[1]).ToString() + " <br />"
                                      + "Voucher Type:" + gvMasterVoucher.GetRowValuesByKeyValue(VoucherId, Columns[2]).ToString() + " <br />"
                                      + "Particulars:" + gvMasterVoucher.GetRowValuesByKeyValue(VoucherId, Columns[3]).ToString() + " <br />"
                                      + "Amount:" + gvMasterVoucher.GetRowValuesByKeyValue(VoucherId, Columns[4]).ToString() + " <br />"
                                      + "<br /> <br />"
                                      + "Please update the changes and sychronize the voucher to the portal";

                string content = Common.GetMailTemplate(Header, MainContent, Name, true);

                resultArgs = Common.SendEmail(CommonMethod.GetFirstValue(string.IsNullOrEmpty(branchUserMailId) ? branchMailId : branchUserMailId), branchMailId, "Amendment Notes", content,true);
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return resultArgs;

        }

        private void SaveAmendments()
        {
            if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                using (AmendmentSystem amendmentSystem = new AmendmentSystem())
                {

                    amendmentSystem.VoucherId = VoucherId;
                    amendmentSystem.BranchId = this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString());
                    using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                    {
                        resultArgs = branchOfficeSystem.FillBranchOfficeDetails(this.Member.NumberSet.ToInteger(cmbBranch.SelectedItem.Value.ToString()),
                            DataBaseType.HeadOffice);
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            amendmentSystem.BranchOfficeCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                        }
                    }
                    amendmentSystem.HeadOfficeCode = base.LoginUser.HeadOfficeCode;
                    amendmentSystem.AmendmentDate = DateTime.Now.Date;
                    amendmentSystem.Remarks = txtDescription.Text;
                    amendmentSystem.Status = (int)AmendmentStatus.Posted;

                    if (this.Member.NumberSet.ToInteger(gvMasterVoucher.GetRowValuesByKeyValue(VoucherId, "AMENDMENT_FLAG").ToString()) == 1)
                    {
                        resultArgs = amendmentSystem.UpdateRemarks();
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.SaveConformation;
                            SendMail();
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.Save_Failure;
                        }
                    }
                    else
                    {
                        resultArgs = amendmentSystem.UpdateAmendment();
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.SaveConformation;
                            LoadVouchers();
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.Save_Failure;
                        }
                    }

                }

            }
            else
            {
                ltMessage.Text = "Decription is required";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Amendment Notes", "javascript:ShowDisplayPopUp()", true);
            }
            txtDescription.Text = "";
        }



        #endregion
    }
}