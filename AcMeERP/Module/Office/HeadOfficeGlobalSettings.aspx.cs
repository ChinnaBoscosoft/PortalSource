using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Configuration;
using Bosco.Model.Setting;
using DevExpress.Web.ASPxEditors;
using System.Collections;


namespace AcMeERP.Module.Office
{
    public partial class HeadOfficeGlobalSettings : Base.UIBase
    {
        #region Delcaration
        private static object objLock = new object();
        #endregion

        #region Property
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                SetControlFocus();
                this.CheckUserRights(RightsModule.Office, RightsActivity.BranchOfficeAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                /*//on 29/03/2021, For sdb tls, provide multi option 
                //for Time being fixed in DB and shown selected LEdgers
                if (this.LoginUser.HeadOfficeCode.ToUpper() == "SDBTLS")
                {
                    LoadLedgersForSDBTLS();
                    btnSaveHeadOffice.Visible = false;
                }
                else
                {
                    btnSaveHeadOffice.Visible = true;
                    LoadLedgers();
                }*/

                btnSaveHeadOffice.Visible = true;
                LoadLedgers();
            }
        }
        
        /// <summary>
        /// This event is fired when user clicks Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveHeadOffice_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ISetting isetting;
                    if (LoginUser.LoginUserId == 1)
                    {
                        DataTable dtSetting = SaveGlobalSetting();
                        isetting = new GlobalSetting();
                        ResultArgs resultArgs = isetting.SaveSetting(dtSetting);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.SETTING_GLOBAL_SAVED;
                            this.AssignSetting();
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                new ErrorLog().WriteError("HeadOfficeAdd.aspx.cs", "Save_Click", ex.Message, new CommonMethod().GetExceptionline(ex));
            }
            finally
            {
                //To set the current login(Portal or Head Office)
                if (base.LoginUser.IsPortalUser)
                {
                    base.HeadOfficeCode = DataBaseType.Portal.ToString();
                }
            }

        }

        private DataTable SaveGlobalSetting()
        {
            // if (ValidateSettingDetails())
            //  {
            Setting setting = new Setting();
            DataView dvSetting = null;
            dvSetting = this.Member.EnumSet.GetEnumDataSource(setting, Sorting.Ascending);
            DataTable dtSetting = dvSetting.ToTable();
            if (dtSetting != null)
            {
                dtSetting.Columns.Add("Value", typeof(string));
                for (int i = 0; i < dtSetting.Rows.Count; i++)
                {
                    Setting SettingName = (Setting)Enum.Parse(typeof(Setting), dtSetting.Rows[i][1].ToString());
                    string Value = "";
                    switch (SettingName)
                    {
                        case Setting.InterAccountFromLedger:
                            {
                                Value = "0";
                                if (cbchkInterAcFromList.SelectedValues != null && cbchkInterAcFromList.SelectedValues.Count > 0)
                                {
                                    Value = GetSelectedValueCheckBoxList(cbchkInterAcFromList);
                                }
                                break;
                            }
                        case Setting.InterAccountToLedger:
                            {
                                Value = "0";
                                if (cbchkInterAcToList.SelectedValues != null && cbchkInterAcToList.SelectedValues.Count > 0)
                                {
                                    Value = GetSelectedValueCheckBoxList(cbchkInterAcToList);
                                }
                                break;
                            }
                        case Setting.ProvinceContributionFromLedger:
                            {
                                Value = "0";
                                if (cbchkContributionFromList.SelectedValues != null && cbchkContributionFromList.SelectedValues.Count > 0)
                                {
                                    Value = GetSelectedValueCheckBoxList(cbchkContributionFromList);
                                }
                                break;
                            }
                        case Setting.ProvinceContributionToLedger:
                            {
                                Value = "0";
                                if (cbchkContributionToList.SelectedValues != null && cbchkContributionToList.SelectedValues.Count > 0)
                                {
                                    Value = GetSelectedValueCheckBoxList(cbchkContributionToList);
                                }
                                break;
                            }
                        //case Setting.Country:
                        //    {
                        //        Value = glkpCountry.EditValue.ToString();
                        //        break;
                        //    }
                        //case Setting.Currency:
                        //    {
                        //        Value = lblCurrencySymbol.Text.ToString();
                        //        break;
                        //    }
                        //case Setting.CreditBalance:
                        //    {
                        //        Value = glkpCreditBalance.EditValue.ToString();
                        //        break;
                        //    }
                        //case Setting.CurrencyPosition:
                        //    {
                        //        Value = cboCurrencyPosition.SelectedItem.ToString();
                        //        break;
                        //    }
                        //case Setting.CurrencyPositivePattern:
                        //    {
                        //        PositiveValue = cboCurrencyPosition.SelectedIndex == 0 ? (int)CurrencyPositivePattern.Before : (int)CurrencyPositivePattern.After;
                        //        Value = PositiveValue.ToString();
                        //        break;
                        //    }
                        //case Setting.CurrencyNegativePattern:
                        //    {
                        //        NegativeValue = cboCurrencyPosition.SelectedIndex == 0 ? (NegativeValue = cboNegativeSign.SelectedIndex == 0 ? (int)CurrencyNegativePatternBracket.Before : (int)CurrencyNegativePatternMinus.Before) : (cboNegativeSign.SelectedIndex == 0 ? (int)CurrencyNegativePatternBracket.After : (int)CurrencyNegativePatternMinus.After);
                        //        Value = NegativeValue.ToString();
                        //        break;
                        //    }
                        //case Setting.CurrencyNegativeSign:
                        //    {
                        //        Value = cboNegativeSign.SelectedItem.ToString();
                        //        break;
                        //    }
                        //case Setting.CurrencyCodePosition:
                        //    {
                        //        Value = cboCodePosition.SelectedItem.ToString();
                        //        break;
                        //    }
                        //case Setting.DigitGrouping:
                        //    {
                        //        Value = GetDigitGroupSizes();
                        //        break;
                        //    }
                        //case Setting.GroupingSeparator:
                        //    {
                        //        Value = cboGroupingSeparator.SelectedItem.ToString();
                        //        break;
                        //    }
                        //case Setting.DecimalPlaces:
                        //    {
                        //        Value = cboDecimalPlaces.SelectedItem.ToString();
                        //        break;
                        //    }
                        //case Setting.DecimalSeparator:
                        //    {
                        //        Value = cboDecimalSeparator.SelectedItem.ToString();
                        //        break;
                        //    }        
                        //case Setting.UILanguage:
                        //    {
                        //        Value = glkpUILanguage.EditValue.ToString();
                        //        break;
                        //    }
                        //case Setting.UIDateFormat:
                        //    {
                        //        Value = cboUIDateFormat.SelectedItem.ToString();
                        //        break;
                        //    }
                        //case Setting.UIDateSeparator:
                        //    {
                        //        Value = cboUIDateSeparator.SelectedItem.ToString();
                        //        break;
                        //    }
                        //case Setting.UIThemes:
                        //    {
                        //        Value = icboTheme.SelectedItem.ToString();
                        //        break;
                        //    }

                        //case Setting.UIProjSelection:
                        //    {
                        //        Value = chkProjectSelection.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        //case Setting.UITransClose:
                        //    {
                        //        Value = chkTransclose.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        //case Setting.TDSEnabled:
                        //    {
                        //        Value = chkTDS.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        //case Setting.PrintVoucher:
                        //    {
                        //        Value = chkVoucherPrint.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        //case Setting.CustomizationForm:
                        //    {
                        //        Value = chkCustomizationform.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        //case Setting.Location:
                        //    {
                        //        Value = (glkpLocation.EditValue != null) ? glkpLocation.Text : DefaultLocation.Primary.ToString();
                        //        break;
                        //    }
                        //case Setting.DBUploadedOn:
                        //    {
                        //        Value = this.AppSetting.DBUploadedOn;
                        //        break;
                        //    }
                        //case Setting.ThirdParty:
                        //    {
                        //        Value = this.AppSetting.ThirdPartyIntegration;
                        //        break;
                        //    }
                        //case Setting.ProductVersion:
                        //    {
                        //        Value = this.AppSetting.AcmeerpVersionFromDB;
                        //        break;
                        //    }
                        //case Setting.UpdaterDownloadBy:
                        //    {
                        //        Value = this.rgbDownloadBy.SelectedIndex.ToString();
                        //        break;
                        //    }
                        //case Setting.ProxyUse:
                        //    {
                        //        Value = chkProxyUse.Checked == true ? "1" : "0";
                        //        if (chkProxyUse.Checked == false)
                        //        {
                        //            txtProxyAddress.Text = string.Empty;
                        //            txtProxyPort.Text = string.Empty;
                        //            chkProxyAuthentication.Checked = false;
                        //            txtProxyUName.Text = string.Empty;
                        //            txtProxyPassword.Text = string.Empty;
                        //        }
                        //        break;
                        //    }
                        //case Setting.ProxyAddress:
                        //    {
                        //        Value = txtProxyAddress.Text.Trim();
                        //        break;
                        //    }
                        //case Setting.ProxyPort:
                        //    {
                        //        Value = txtProxyPort.Text.Trim();
                        //        break;
                        //    }
                        //case Setting.ProxyAuthenticationUse:
                        //    {
                        //        Value = chkProxyAuthentication.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                        //case Setting.ProxyUserName:
                        //    {
                        //        Value = txtProxyUName.Text.Trim();
                        //        break;
                        //    }
                        //case Setting.ProxyPassword:
                        //    {
                        //        Value = txtProxyPassword.Text.Trim();
                        //        break;
                        //    }
                        //case Setting.PayrollPassword:
                        //    {
                        //        string PayrollLockPassword = CommonMethod.Encrept(txtPayrollPassword.Text);
                        //        Value = PayrollLockPassword;
                        //        break;
                        //    }
                    }

                    dtSetting.Rows[i][2] = Value;
                }

                dtSetting.DefaultView.RowFilter = "ID IN(" + (int)Setting.InterAccountFromLedger + "," + (int)Setting.InterAccountToLedger + ","
                                                            + (int)Setting.ProvinceContributionFromLedger + "," + (int)Setting.ProvinceContributionToLedger + ")";

                dtSetting = dtSetting.DefaultView.ToTable();
                this.LoginUser.SettingInfo = dtSetting.DefaultView;
            }
            else
            {
                this.Message = "Setting is empty";
            }
            // }

            return dtSetting;
        }

        #endregion

        #region Methods
        private void SetPageTitle()
        {
            this.PageTitle = MessageCatalog.Settings.SETTING_GLOBAL_Title;
        }

        private void SetControlFocus()
        {
            //this.SetControlFocus(txtHOfficeCode);
        }

        private void LoadLedgers()
        {
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ResultArgs resultArgs = new ResultArgs();
                resultArgs = ledgersystem.FetchLedgerWithNature(DataBaseType.HeadOffice);
                if (resultArgs.Success && resultArgs.DataSource.Table!=null)
                {
                    DataTable dtLedgers =  resultArgs.DataSource.Table;
                    
                    //For Inter Accounts (Get Asset & Liabilities Ledgers)
                    dtLedgers.DefaultView.RowFilter = string.Empty;
                    dtLedgers.DefaultView.RowFilter = this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Assert + "," + (int)Natures.Libilities + ")";
                    DataTable dtAssetLiabilities = dtLedgers.DefaultView.ToTable();
                    DataTable dtAssetLiabilities1 = dtLedgers.DefaultView.ToTable();
                    dtLedgers.DefaultView.RowFilter = string.Empty;

                    dtAssetLiabilities.Columns.Add("SELECT", typeof(System.Int32), "IIF(LEDGER_ID IN (" + this.LoginUser.InterAccountFromLedgerIds + "), 1, 0)");
                    dtAssetLiabilities1.Columns.Add("SELECT", typeof(System.Int32), "IIF(LEDGER_ID IN (" + this.LoginUser.InterAccountToLedgerIds + "), 1, 0)");
                    dtAssetLiabilities.DefaultView.Sort = "SELECT DESC ," + this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    dtAssetLiabilities1.DefaultView.Sort = "SELECT DESC ," + this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    
                    LoadLedgersInMultiCheckbox(cbchkInterAcFromList, dtAssetLiabilities, this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName,
                                                this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName);
                    LoadLedgersInMultiCheckbox(cbchkInterAcToList, dtAssetLiabilities1, this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName,
                                                this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName);

                    //For Province Contribution From/To (Get Income & Expense Ledgers)
                    dtLedgers.DefaultView.RowFilter = string.Empty;
                    dtLedgers.DefaultView.RowFilter = this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + ")";
                    DataTable dtIELedgers = dtLedgers.DefaultView.ToTable();
                    DataTable dtIELedgers1 = dtLedgers.DefaultView.ToTable();
                    dtLedgers.DefaultView.RowFilter = string.Empty;
                    dtIELedgers.Columns.Add("SELECT", typeof(System.Int32), "IIF(LEDGER_ID IN (" + this.LoginUser.ProvinceFromLedgerIds + "), 1, 0)");
                    dtIELedgers1.Columns.Add("SELECT", typeof(System.Int32), "IIF(LEDGER_ID IN (" + this.LoginUser.ProvinceToLedgerIds + "), 1, 0)");
                    dtIELedgers.DefaultView.Sort = "SELECT DESC ," + this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    dtIELedgers1.DefaultView.Sort = "SELECT DESC ," + this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;

                    LoadLedgersInMultiCheckbox(cbchkContributionFromList, dtIELedgers, this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName,
                                                this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName);
                    LoadLedgersInMultiCheckbox(cbchkContributionToList, dtIELedgers1, this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName,
                                                this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName);
                   
                    //Assign From Settings----------------------------------------------------------------------------------------
                    if (!string.IsNullOrEmpty(this.LoginUser.InterAccountFromLedgerIds))
                    {
                        SetSelectedValueCheckBoxList(cbchkInterAcFromList, this.LoginUser.InterAccountFromLedgerIds);
                    }

                    if (!string.IsNullOrEmpty(this.LoginUser.InterAccountToLedgerIds))
                    {
                        SetSelectedValueCheckBoxList(cbchkInterAcToList, this.LoginUser.InterAccountToLedgerIds);
                    }

                    if (!string.IsNullOrEmpty(this.LoginUser.ProvinceFromLedgerIds))
                    {
                        SetSelectedValueCheckBoxList(cbchkContributionFromList, this.LoginUser.ProvinceFromLedgerIds);
                    }

                    if (!string.IsNullOrEmpty(this.LoginUser.ProvinceToLedgerIds))
                    {
                        SetSelectedValueCheckBoxList(cbchkContributionToList, this.LoginUser.ProvinceToLedgerIds);
                    }
                    //-------------------------------------------------------------------------------------------------------------


                }
            }


        }

        private void LoadLedgersInMultiCheckbox(ASPxCheckBoxList chkList, DataTable dtLedgers, string IdField, string NameField)
        {
            chkList.ValueType = typeof(System.Int32);
            chkList.ValueField = IdField;
            chkList.TextField = NameField;
            chkList.DataSource = dtLedgers;
            chkList.DataBind();
        }

        /// <summary>
        /// This method is used to selected given values to be selected 
        /// </summary>
        /// <param name="chklist"></param>
        /// <param name="sValues"></param>
        private void SetSelectedValueCheckBoxList(ASPxCheckBoxList chklist, string sValues)
        {
            if (!string.IsNullOrEmpty(sValues))
            {
                string[] selecteditems = sValues.Split(',');
                foreach (string value in selecteditems)
                {
                    Int32 rowvalue = this.Member.NumberSet.ToInteger(value);
                    if (chklist.Items.FindByValue(rowvalue) != null)
                    {
                        chklist.Items.FindByValue(rowvalue).Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to get selected items from the chekced list box
        /// </summary>
        /// <param name="chklist"></param>
        /// <returns></returns>
        private string GetSelectedValueCheckBoxList(ASPxCheckBoxList chklist)
        {
            string rtn = string.Empty;
            if (chklist.SelectedValues != null && chklist.SelectedValues.Count>0)
            {
                foreach (Int32 value in chklist.SelectedValues)
                {
                    rtn += value + ",";
                }
                rtn = rtn.TrimEnd(',');
            }
            return rtn;
        }


        /// <summary>
        /// On 29/03/2021
        /// </summary>
        private void LoadLedgersForSDBTLS()
        {
           /* using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ResultArgs resultArgs = new ResultArgs();

                //1. For Inter Accounts (Get Asset & Liabilities Ledgers) ----------------------------------------------------------------------------------
                resultArgs = ledgersystem.GetLegerNameByLedgerIds(this.LoginUser.InterAccountFromLedgerIds);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtAILedgers = resultArgs.DataSource.Table;
                    this.Member.ComboSet.BindDataCombo(ddlInterAccountFrom, dtAILedgers, this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName,
                                                    this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, true, "-Mapped Ledgers-");
                }
                resultArgs = ledgersystem.GetLegerNameByLedgerIds(this.LoginUser.InterAccountToLedgerIds);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtAILedgers = resultArgs.DataSource.Table;
                    this.Member.ComboSet.BindDataCombo(ddlInterAccountTo, dtAILedgers, this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName,
                                                    this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, true, "-Mapped Ledgers-");
                }
                //-------------------------------------------------------------------------------------------------------------------------------------------

                //2. For Province Contribution From/To (Get Income & Expense Ledgers) -----------------------------------------------------------------------
                resultArgs = ledgersystem.GetLegerNameByLedgerIds(this.LoginUser.ProvinceFromLedgerIds);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtIELedgers = resultArgs.DataSource.Table;
                    this.Member.ComboSet.BindDataCombo(ddlProvinceContributionFrom, dtIELedgers, this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName,
                                                        this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, true, "-Mapped Ledgers-");
                }

                resultArgs = ledgersystem.GetLegerNameByLedgerIds(this.LoginUser.ProvinceToLedgerIds);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtIELedgers = resultArgs.DataSource.Table;
                    this.Member.ComboSet.BindDataCombo(ddlProvinceContributionTo, dtIELedgers, this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName,
                                                        this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, true, "-Mapped Ledgers-");
                }
                //--------------------------------------------------------------------------------------------------------------------------------=-------
            }*/
        }

       
        #endregion

    }
}