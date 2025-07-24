using System;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace AcMeERP.Module.Master
{
    public partial class AccountingPeriodAdd : Base.UIBase
    {
        #region Properties

        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private int AccCount = 0;
        private int AccountYearId = 0;
        private string SelectedLang;

        private string booksBeginDate = "";
        private string BooksBeginDate
        {
            get { return booksBeginDate; }
            set { booksBeginDate = value; }
        }
        private int AccYearId
        {
            get
            {
                int AccountYearId = this.Member.NumberSet.ToInteger(this.RowId);
                return AccountYearId;
            }
            set
            {
                this.RowId = value.ToString();
            }
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

        #region Methods
        private bool ValidateAccountPeriodDetails()
        {
            bool IsAccPereidValid = true;
            if (string.IsNullOrEmpty(dteYearFrom.Date.ToString()))
            {
                this.Message = MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_YEAR_FROM_EMPTY;
                IsAccPereidValid = false;
                dteYearFrom.Focus();
            }
            else if (string.IsNullOrEmpty(dteYearTo.Date.ToString()))
            {
                this.Message = MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_YEAR_TO_EMPTY;
                IsAccPereidValid = false;
                dteYearTo.Focus();
            }
            else if (dteYearFrom.Date > dteYearTo.Date)
            {
                this.Message = MessageCatalog.Master.AccountingPeriod.DATECOMPAREERROR;
                IsAccPereidValid = false;
                dteYearTo.Focus();
            }
            else if (!(string.IsNullOrEmpty(dteYearFrom.Date.ToString()) && string.IsNullOrEmpty(dteYearTo.Date.ToString())))
            {
                using (AccouingPeriodSystem accountSystem = new AccouingPeriodSystem())
                {
                    accountSystem.YearFrom = dteYearFrom.Date.ToString();
                    accountSystem.YearTo = dteYearTo.Date.ToString();
                    resultArgs= accountSystem.IsAccountingPeriodExists();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        this.Message = "Accounting Period is available";
                        IsAccPereidValid = false;
                        dteYearFrom.Focus();
                    }
                }
            }

            return IsAccPereidValid;
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (AccouingPeriodSystem accountingperiodsystem = new AccouingPeriodSystem(AccYearId))
                {
                    if (AccYearId == 0)
                    {
                        this.PageTitle = MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_ADD_PAGE_TITLE;
                        ResultArgs resultArgs = accountingperiodsystem.FetchMaxAccountingPeriod();
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows[0][this.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName] != DBNull.Value)
                        {
                            // dteYearFrom.Enabled = false;
                            DateTime dtYearFromDate = this.Member.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                            dteYearFrom.Date = dtYearFromDate.Date.AddDays(1);
                            dteYearTo.Date = dteYearFrom.Date.AddYears(1).AddDays(-1);
                        }
                        else
                        {
                            dteYearFrom.Enabled = false;
                            dteYearFrom.Date = new DateTime(this.Member.DateSet.ToDate(this.Member.DateSet.GetDateToday(), false).Year, 4, 1);
                            dteYearTo.Date = dteYearFrom.Date.AddYears(1).AddDays(-1);
                        }
                    }
                    else
                    {
                        this.PageTitle = MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_EDIT_PAGE_TITLE;
                        //dteYearFrom.Enabled = true;
                        dteYearFrom.Date = this.Member.DateSet.ToDate(accountingperiodsystem.YearFrom, false);
                        dteYearTo.Date = this.Member.DateSet.ToDate(accountingperiodsystem.YearTo, false);
                    }
                    ShowLoadWaitPopUp(btnSave);
                }

                hlkClose.PostBackUrl = this.ReturnUrl;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAccountPeriodDetails())
            {
                try
                {
                    using (AccouingPeriodSystem AccPeriodSystem = new AccouingPeriodSystem())
                    {
                        AccPeriodSystem.AccPeriodId = AccYearId;
                        AccPeriodSystem.YearFrom = this.Member.DateSet.ToDate(dteYearFrom.Text.ToString(), DateFormatInfo.DateFormatYMD);
                        AccPeriodSystem.YearTo = this.Member.DateSet.ToDate(dteYearTo.Text.ToString(), DateFormatInfo.DateFormatYMD);
                        AccPeriodSystem.IsFirstAccYear = IsFirstAccYear;
                        if (isFirstAccYear)
                        {
                            AccPeriodSystem.Status = 1;
                        }
                        resultArgs = AccPeriodSystem.SaveAccountingPeriodDetails();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.Message = MessageCatalog.Common.COMMON_SAVED_CONFIRMATION;
                            if (AccYearId == 0)
                            {
                                DateTime dtFrom = dteYearTo.Date;
                                dteYearFrom.Date = dtFrom.AddDays(1);
                                dteYearTo.Date = dteYearFrom.Date.AddYears(1).AddDays(-1);
                            }
                            AccountYearId = AccPeriodSystem.AccPeriodId;
                            if (isFirstAccYear)
                            {
                                accid = AccPeriodSystem.AccPeriodId;
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    this.Message = Ex.Message;
                }
            }
        }

        #endregion
    }
}