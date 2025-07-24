using System;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using System.Data;
using AcMeERP.Base;
using Bosco.Model.Setting;


namespace AcMeERP.Module.Master
{
    public partial class AccountingPeriodView : Base.UIBase
    {
        #region Properties
        ResultArgs resultArgs = null;
        CommonMember UtilityMember = new CommonMember();
        private string Flag = "FLAG";
        private string Active = "Active";
        private string InActive = "InActive";
        private DataView AccountingYearViewSource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";

        ResultArgs resulArgs = null;
        private string booksBeginDate = "";
        private string BooksBeginDate
        {
            get { return booksBeginDate; }
            set { booksBeginDate = value; }
        }

        private bool isFirstAccYear;
        private bool IsFirstAccYear
        {
            get { return isFirstAccYear; }
            set { isFirstAccYear = value; }
        }
        private int accid;
        public int AccID
        {
            get { return accid; }
            set { accid = value; }
        }

        #endregion

        #region Events
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                targetPage = this.GetPageUrlByName(URLPages.AccountingPeriodAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.AccountingPeriodView;
                SetAccountingYearViewSource();

                gvAccountingYear.RowCommand += new GridViewCommandEventHandler(gvAccountingYear_RowCommand);
                gvAccountingYear.RowDataBound += new GridViewRowEventHandler(gvAccountingYear_RowDataBound);

                LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
                linkUrl.ShowModelWindow = false;
                if (this.LoginUser.IsAdminUser)
                {
                    gvAccountingYear.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                    gvAccountingYear.SetTemplateColumn(ControlType.ImageButton, CommandMode.Status, this.rowIdColumn, "", null, "", CommandMode.Status.ToString());
                    gvAccountingYear.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                    gvAccountingYear.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }

                gvAccountingYear.HideColumn = this.hiddenColumn;
                gvAccountingYear.RowIdColumn = this.rowIdColumn;
                gvAccountingYear.DataSource = AccountingYearViewSource;
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.BranchOffice.AccountinPeriodPageTitle;
                ShowLoadWaitPopUp();
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvAccountingYear_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int AccountingYearId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());
            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (AccountingYearId != 0)
                {
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        ResultArgs resultargs = accountingSystem.IsActivePeriod(AccountingYearId);
                        if (resultargs.DataSource.Table.Rows.Count > 0)
                        {
                            this.Message = MessageCatalog.Message.DeleteActivePeriod;
                        }
                        else
                        {

                            if (IsTransactoinExists(AccountingYearId))
                            {
                                resultArgs = accountingSystem.DeleteAccountingPeriodDetials(AccountingYearId);
                                if (resultArgs.Success)
                                {
                                    this.Message = MessageCatalog.Message.AccountinYearDelete;
                                    SetAccountingYearViewSource();
                                    gvAccountingYear.BindGrid(AccountingYearViewSource);
                                }
                            }
                            else
                            {
                                this.Message = MessageCatalog.Message.DenyAccountingYearDelete;
                            }
                        }

                    }
                }
            }

            else if (e.CommandName == CommandMode.Status.ToString())
            {
                resulArgs = IsAccountingPeriodSelected(AccountingYearId.ToString(), YesNo.Yes);
                if (resulArgs.DataSource.Table.Rows.Count == 0)
                {
                    SetCurrentTransPeriod(AccountingYearId.ToString(), YesNo.Yes);
                }
                else
                {
                    this.Message = MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_ONE_ACTIVE;
                }
            }
        }
        protected void gvAccountingYear_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
            if (imgStatus != null)
            {
                if (e.Row.Cells[2].Text.Trim().Equals(Status.Inactive.ToString()))
                {
                    if (imgStatus != null)
                    {
                        imgStatus.ImageUrl = "~/App_Themes/MainTheme/images/activate.gif";
                        imgStatus.ToolTip = MessageCatalog.Message.ActivateToolTip;
                        imgStatus.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.Activate_Confirm + "');";
                    }
                }
                else
                {
                    if (imgStatus != null)
                    {
                        imgStatus.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.DeActivate_Confirm + "');";
                    }
                }
            }
        }
        #endregion

        #region Methods

        private ResultArgs IsAccountingPeriodSelected(string AccountYearId, YesNo status)
        {
            using (AccouingPeriodSystem accountingPeriodSystem = new AccouingPeriodSystem())
            {
                resulArgs = accountingPeriodSystem.FetchActiveAccountingYearId(AccountYearId);
            }
            return resulArgs;
        }

        private void SetAccountingYearViewSource()
        {
            AccouingPeriodSystem accountingPeriodSystem = new AccouingPeriodSystem();
            resulArgs = accountingPeriodSystem.FetchAccountingPeriodDetails();
            if (resulArgs.Success)
            {
                AccountingYearViewSource = resulArgs.DataSource.Table.DefaultView;
            }
            else
            {
                this.Message = resulArgs.Message;
            }
            this.rowIdColumn = this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName;
            this.hiddenColumn = this.rowIdColumn;
        }

        private bool IsTransactoinExists(int AccountYearId)
        {
            bool isTransactoion = true;
            try
            {
                using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem())
                {
                    accPeriodSystem.AccPeriodId = AccountYearId;
                    resultArgs = accPeriodSystem.CheckIsTransaction();
                    if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                    {
                        isTransactoion = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return isTransactoion;
        }

        private DataTable SetCurrentTransPeriod(string accountYearId, YesNo status)
        {
            DataTable dtAccSource = AccountingYearViewSource.Table;
            for (int i = 0; i < dtAccSource.Rows.Count; i++)
            {
                if (dtAccSource.Rows[i][0].ToString() == accountYearId)
                {
                    dtAccSource.Rows[i]["Year Status"] = Active;
                    UpdateTransacationPeriod(accountYearId);
                }
                else
                {
                    dtAccSource.Rows[i]["Year Status"] = InActive;
                }
            }
            return dtAccSource;
        }

        private void UpdateTransacationPeriod(string accid)
        {
            ResultArgs resultArgs = new ResultArgs();
            using (GlobalSetting globalSystem = new GlobalSetting())
            {
                resultArgs = globalSystem.UpdateAccountingPeriod(accid);
                if (resultArgs.Success)
                {
                    SetAccountingYearViewSource();
                    gvAccountingYear.BindGrid(AccountingYearViewSource);
                }
            }
        }


        #endregion
    }

}