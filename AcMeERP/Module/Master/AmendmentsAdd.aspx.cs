using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using AcMEDSync;
using System.Data;

namespace AcMeERP.Module.Master
{
    public partial class AmendmentsAdd : Base.UIBase
    {
        ResultArgs resultArgs;

        #region Properties

        private int BranchId
        {
            get { return this.Member.NumberSet.ToInteger(ViewState[AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName].ToString()); }
            set { ViewState[AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName] = value; }
        }

        private int VoucherId
        {
            get { return this.Member.NumberSet.ToInteger(ViewState[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString()); }
            set { ViewState[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] = value; }
        }
        private int LocationId
        {
            get { return this.Member.NumberSet.ToInteger(ViewState[AppSchema.BranchLocation.LOCATION_IDColumn.ColumnName].ToString()); }
            set { ViewState[AppSchema.BranchLocation.LOCATION_IDColumn.ColumnName] = value; }
        }
        private string BranchCode
        {
            get { return ViewState[AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString(); }
            set { ViewState[AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName] = value; }
        }

        private string HeadOfficeMail
        {
            get { return ViewState[AppSchema.HeadOffice.ORG_MAIL_IDColumn.ColumnName].ToString(); }
            set { ViewState[AppSchema.HeadOffice.ORG_MAIL_IDColumn.ColumnName] = value; }
        }

        private string BranchOfficeMail
        {
            get { return ViewState[AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName].ToString(); }
            set { ViewState[AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName] = value; }
        }

        private int AmendmentFlag
        {
            get { return this.Member.NumberSet.ToInteger(ViewState["AMENDMENT_FLAG"].ToString()); }
            set { ViewState["AMENDMENT_FLAG"] = value; }
        }



        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Amendment.AmendmentAddPageTitle;
                /*this.CheckUserRights(RightsModule.Data, RightsActivity.PostAmendment, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ?
                   DataBaseType.Portal : DataBaseType.HeadOffice);*/
                AssignValues();
                SetControlFocus(txtDescription);
                this.ShowLoadWaitPopUp(btnSave);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    using (AmendmentSystem amendmentSystem = new AmendmentSystem())
                    {
                        amendmentSystem.VoucherId = VoucherId;
                        amendmentSystem.BranchId = BranchId;
                        amendmentSystem.BranchOfficeCode = BranchCode;
                        amendmentSystem.HeadOfficeCode = base.LoginUser.HeadOfficeCode;
                        amendmentSystem.AmendmentDate = DateTime.Now.Date;
                        amendmentSystem.Remarks = txtDescription.Text;
                        amendmentSystem.Status = (int)AmendmentStatus.Posted;
                        //int Flag = Request.QueryString["3"] != null ? this.Member.NumberSet.ToInteger(Request.QueryString["3"].ToString()) : 0;
                        if (AmendmentFlag == 1)
                        {
                            resultArgs = amendmentSystem.UpdateRemarks();
                            if (resultArgs.Success)
                            {
                                this.Message = MessageCatalog.Message.SaveConformation;
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
                                SendMail();
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
                    this.Message = MessageCatalog.Message.Amendment.DescriptionRequired;
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }


        #endregion Events

        #region Methods

        private void AssignValues()
        {

            try
            {
                using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                {
                    voucherTransSystem.VoucherId = VoucherId = Request.QueryString["1"] != null ? this.Member.NumberSet.ToInteger(Request.QueryString["1"].ToString()) : 0;
                    voucherTransSystem.BranchId = BranchId = Request.QueryString["2"] != null ? this.Member.NumberSet.ToInteger(Request.QueryString["2"].ToString()) : 0;
                    voucherTransSystem.LocationId = LocationId = Request.QueryString["3"] != null ? this.Member.NumberSet.ToInteger(Request.QueryString["3"].ToString()) : 0;
                    resultArgs = voucherTransSystem.FetchVoucherMasterById();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        lblBranchOffice.Text = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_NAME"].ToString();
                        lblproject.Text = resultArgs.DataSource.Table.Rows[0]["PROJECT"].ToString();
                        lblVoucherDate.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_DATE"].ToString();
                        lblVoucherType.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_TYPE"].ToString();
                        lblVoucherNo.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_NO"].ToString();
                        lblParticulars.Text = resultArgs.DataSource.Table.Rows[0]["LEDGER_NAME"].ToString();
                        lblAmount.Text = resultArgs.DataSource.Table.Rows[0]["AMOUNT"].ToString();
                        txtDescription.Text = resultArgs.DataSource.Table.Rows[0]["REMARKS"].ToString();
                        BranchCode = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_CODE"].ToString();
                        AmendmentFlag = this.Member.NumberSet.ToInteger( resultArgs.DataSource.Table.Rows[0]["AMENDMENT_FLAG"].ToString());
                        FetchUserMail();
                    }
                    else
                    {
                        lblBranchOffice.Text = string.Empty;
                        lblproject.Text = string.Empty;
                        lblVoucherDate.Text = string.Empty;
                        lblVoucherType.Text = string.Empty;
                        lblVoucherNo.Text = string.Empty;
                        lblParticulars.Text = string.Empty;
                        lblAmount.Text = string.Empty;
                        txtDescription.Text = string.Empty;
                        ltlMailReceiver.Text = string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

        }

        private ResultArgs SendMail()
        {
            try
            {
                string Name = "Head Office/Branch Admin";

                string Header = "You have received amendment notes from the head office for the following voucher.";

                string MainContent = "Voucher Details "
                                      + "<br />"
                                      + "Voucher Date:" + lblVoucherDate.Text + " <br />"
                                      + "Voucher No:" + lblVoucherNo.Text + " <br />"
                                      + "Voucher Type:" + lblVoucherType.Text + " <br />"
                                      + "Particulars:" + lblParticulars.Text + " <br />"
                                      + "Amount:" + lblAmount.Text + " <br />"
                                      + "<br /> <br />"
                                      + "Please update the changes and sychronize the voucher to the portal";

                string content = Common.GetMailTemplate(Header, MainContent, Name, true);
                resultArgs = Common.SendEmail(CommonMethod.GetFirstValue(BranchOfficeMail), HeadOfficeMail + "," + CommonMethod.RemoveFirstValue(BranchOfficeMail), "Amendment Notes", content,true);
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return resultArgs;

        }

        private void FetchUserMail()
        {
            try
            {
                HeadOfficeMail = base.LoginUser.LoginUserEmailId;
                DataTable dtUserInfo = null;
                BranchOfficeMail = string.Empty;
                string HeadOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                using (UserSystem userSystem = new UserSystem())
                {
                    base.HeadOfficeCode = HeadOfficeCode;
                    userSystem.UserName = BranchCode.Substring(6, 6);
                    resultArgs = userSystem.FetchUserDetailsByHeadOfficeCode(DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dtUserInfo = resultArgs.DataSource.Table;
                        BranchOfficeMail = dtUserInfo.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString();
                    }
                    ltlMailReceiver.Text = HeadOfficeMail + "," + BranchOfficeMail;
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