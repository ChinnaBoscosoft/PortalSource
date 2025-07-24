using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Master
{
    public partial class StateAdd : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                hlkClose.PostBackUrl = this.ReturnUrl;
                this.CheckUserRights(RightsModule.Data, RightsActivity.CountryAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                //  LoadCurrencySymbol();
                if (CountryId != 0)
                {
                    AssignValues();
                    btnNew.Visible = false;
                }
                this.SetControlFocus(txtCountry);
            }

        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (ValidateProjectDetails())
            {
                this.ShowLoadWaitPopUp(btnSaveUser);
                using (CountrySystem CountrySystem = new CountrySystem())
                {
                    CountrySystem.CountryId = CountryId == 0 ? (int)AddNewRow.NewRow : CountryId;
                    CountrySystem.CountryName = txtCountry.Text.Trim();
                    CountrySystem.CountryCode = txtCountryCode.Text.ToString();
                    CountrySystem.CurrencySymbol = txtSymbolIcon.Text.ToString(); // ddlSymbol.SelectedItem.ToString();
                    CountrySystem.CurrencyCode = txtSymbolCode.Text.ToString();
                    CountrySystem.CurrencyName = txtSymbalName.Text.ToString();
                    resultArgs = CountrySystem.SaveCountryDetails(DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                        this.Message = MessageCatalog.Message.SaveConformation;
                        CountryId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        if (CountryId == 0)
                        {
                            ClearValues();
                        }
                        this.SetControlFocus(txtCountry);
                    }
                }
            }

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
            SetPageTitle();
            this.SetControlFocus(txtCountry);
        }

        #endregion

        #region Methods

        private void ClearValues()
        {
            txtCountry.Text = txtCountryCode.Text = txtSymbolCode.Text = txtSymbalName.Text = txtSymbolIcon.Text = string.Empty;
            //ddlSymbol.SelectedValue = "0";
            CountryId = 0;
        }

        private bool ValidateProjectDetails()
        {
            bool isProject = true;
            if (string.IsNullOrEmpty(txtCountry.Text))
            {
                this.Message = MessageCatalog.Message.Country.CountryFieldEmpty;
                txtCountry.Focus();
                isProject = false;
            }
            else if (string.IsNullOrEmpty(txtCountryCode.Text))
            {
                this.Message = MessageCatalog.Message.Country.CountryCodeFieldEmpty;
                txtCountryCode.Focus();
                isProject = false;
            }
            else if (string.IsNullOrEmpty(txtSymbolIcon.Text))
            {
                this.Message = MessageCatalog.Message.Country.CountrySymbolFieldEmpty;
                txtSymbolIcon.Focus();
                isProject = false;
            }
            //else if (ddlSymbol.SelectedValue == "0")
            //{
            //    this.Message = MessageCatalog.Message.Country.CountrySymbolFieldEmpty;
            //    ddlSymbol.Focus();
            //    isProject = false;
            //}
            else if (string.IsNullOrEmpty(txtSymbolCode.Text))
            {
                this.Message = MessageCatalog.Message.Country.CountrySymbolCodeFieldEmpty;
                txtSymbolCode.Focus();
                isProject = false;
            }

            else if (string.IsNullOrEmpty(txtSymbalName.Text))
            {
                this.Message = MessageCatalog.Message.Country.CountrySymbolNameFieldEmpty;
                txtSymbalName.Focus();
                isProject = false;
            }
            return isProject;
        }

        private int CountryId
        {
            get
            {
                int CountryId = this.Member.NumberSet.ToInteger(this.RowId);
                return CountryId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.Country.CountryEditPageTitle : MessageCatalog.Message.Country.CountryAddPageTitle));
        }

        private void AssignValues()
        {
            using (CountrySystem CountrySystem = new CountrySystem(CountryId))
            {
                txtCountry.Text = CountrySystem.CountryName;
                txtCountryCode.Text = CountrySystem.CountryCode;
                txtSymbolCode.Text = CountrySystem.CurrencyCode;
                txtSymbalName.Text = CountrySystem.CurrencyName;
                txtSymbolIcon.Text = CountrySystem.CurrencySymbol;
                //ddlSymbol.SelectedValue = CountrySystem.CountryId > 0 ? CountrySystem.CountryId.ToString() : "0";
            }

        }

        //private void LoadCurrencySymbol()
        //{
        //    using (CountrySystem CountrySystem = new CountrySystem())
        //    {
        //        resultArgs = CountrySystem.FetchCurrencySymbolsListDetails(DataBaseType.HeadOffice);
        //        this.Member.ComboSet.BindDataCombo(ddlSymbol, resultArgs.DataSource.Table, this.AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName,
        //            this.AppSchema.Country.COUNTRY_IDColumn.ColumnName, true, CommonMember.SELECT);
        //    }
        //}

        #endregion
    }
}