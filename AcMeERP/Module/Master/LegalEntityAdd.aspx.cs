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
 * Purpose          :This page helps head office admin to create new legal entity for the entire head office
 *****************************************************************************************************/
using System;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Master
{
    public partial class LegalEntityAdd : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;

        private int CustomerId
        {
            get
            {
                int CustomerId = this.Member.NumberSet.ToInteger(this.RowId);
                return CustomerId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                this.CheckUserRights(RightsModule.Data, RightsActivity.LegalEntityAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.SetControlFocus(txtSocietyName);
                hlkClose.PostBackUrl = this.ReturnUrl;
                LoadCountryToCombo();
                if (CustomerId != 0)
                {
                    AssignLegalEntityDetails();
                    btnNew.Visible = false;
                }
                else
                {
                    SetDefaultCoutry();
                }
                this.ShowLoadWaitPopUp(btnSaveUser);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
            SetPageTitle();
            SetControlFocus(txtSocietyName);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUserControl())
                {
                    using (LegalEntitySystem LegalEntitySystem = new LegalEntitySystem())
                    {
                        LegalEntitySystem.CustomerId = CustomerId == 0 ? (int)AddNewRow.NewRow : CustomerId;
                        LegalEntitySystem.SocietyName = txtSocietyName.Text.Trim();
                        LegalEntitySystem.ContactPerson = txtContactPerson.Text.Trim();
                        LegalEntitySystem.Address = txtAddress.Text.Trim();
                        LegalEntitySystem.Place = txtPlace.Text.Trim();
                        LegalEntitySystem.State = txtState.Text.Trim();
                        LegalEntitySystem.Country = ddlCountry.SelectedItem.Text;
                        LegalEntitySystem.Pincode = txtPinCode.Text.Trim();
                        LegalEntitySystem.Phone = txtPhone.Text.Trim();
                        LegalEntitySystem.Fax = txtFax.Text.Trim();
                        LegalEntitySystem.EMail = txtEmail.Text.Trim();
                        LegalEntitySystem.URL = txtUrl.Text.Trim();
                        LegalEntitySystem.RegNo = txtRegNo.Text.Trim();
                        LegalEntitySystem.RegDate = dteRegisterDate.Date;
                        LegalEntitySystem.PermissionDate = dtePermissionDate.Date;
                        LegalEntitySystem.PermissionNo = txtPermissionNo.Text;
                        LegalEntitySystem.A12No = txtAoneTwoNo.Text.Trim();
                        LegalEntitySystem.PANNo = txtPanNo.Text.Trim();
                        LegalEntitySystem.GIRNo = txtGIRNo.Text.Trim();
                        LegalEntitySystem.TANNo = txtTanNo.Text.Trim();
                        LegalEntitySystem.FCRINo = txtFCRINo.Text.Trim();
                        LegalEntitySystem.FCRIRegisterDate = dteFCRIRegDate.Date;
                        LegalEntitySystem.G80No = txt80GNo.Text.Trim();
                        LegalEntitySystem.G80RegDate = dte80GRegDate.Date;
                        LegalEntitySystem.Is_Foundation = chkFoundation.Checked ? 1 : 0;
                        LegalEntitySystem.PrincipalActivity = txtPrincipalActivity.Text.Trim();
                        if (rdAssociationNature.Items[(int)Association.Others].Selected)
                        {
                            LegalEntitySystem.AssociationOther = txtAssociationNatureOthers.Text.Trim();
                        }
                        LegalEntitySystem.AssoicationNature = GetSelectedNatureofAssociation();
                        LegalEntitySystem.Denomination = rdDenomination.Visible ? this.Member.NumberSet.ToInteger(rdDenomination.SelectedValue.ToString()) : 0;
                        if (rdDenomination.SelectedIndex == (int)Association.Others)
                        {
                            LegalEntitySystem.DenominationOther = txtDenominationOthers.Text.Trim();
                        }
                        resultArgs = LegalEntitySystem.SaveLegalEntityDetails(DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.LegalEntitySaved;
                            if (CustomerId == 0)
                            {
                                CustomerId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                ClearValues();
                            }
                            else
                                AssignLegalEntityDetails();
                        }
                        else
                            this.Message = resultArgs.Message;
                        SetControlFocus(txtSocietyName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }

        }
        protected void rdAssociationNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdAssociationNature.Items[(int)Association.Religious].Selected)
            {
                ltrDenomination.Visible = rdDenomination.Visible = true;
                SetControlFocus(rdDenomination);
            }
            else
            {
                rdDenomination.SelectedIndex = -1; txtDenominationOthers.Text = string.Empty;
                ltrDenomination.Visible = rdDenomination.Visible = false;
                txtDenominationOthers.Visible = false;
                SetControlFocus(btnSaveUser);
            }
            showAssociationOther();
        }
        protected void rdDenomination_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDenomationOther();
        }

        #endregion

        #region Methods

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.LegalEntity.LegalEntityEditPageTitle : MessageCatalog.Message.LegalEntity.LegalEntityAddPageTitle));
        }

        private bool ValidateUserControl()
        {
            bool Valid = true;
            if (string.IsNullOrEmpty(txtSocietyName.Text))
            {
                this.Message = MessageCatalog.Message.LegalEntity.SocietyNameFieldEmpty;
                txtSocietyName.Focus();
                Valid = false;
            }
            else if (ddlCountry.SelectedItem.Text.Equals(CommonMember.SELECT))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidCountry;
                ddlCountry.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(txtState.Text))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidState;
                txtState.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(txtRegNo.Text))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidRegistrationNumber;
                txtRegNo.Focus();
                Valid = false;
            }
            else if (!this.Member.DateSet.ValidateFutureDate(this.Member.DateSet.ChangeDateFormat(dteRegisterDate.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME)))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidRegisteredDate;
                dteRegisterDate.Text = string.Empty;
                dteRegisterDate.Focus();
                Valid = false;
            }
            else if (!this.Member.DateSet.ValidateFutureDate(this.Member.DateSet.ChangeDateFormat(dtePermissionDate.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME)))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidPermissionDate;
                dtePermissionDate.Text = string.Empty;
                dtePermissionDate.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(GetSelectedNatureofAssociation()))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidAssociationNature;
                rdAssociationNature.Focus();
                Valid = false;
            }
            else if (rdAssociationNature.Items[(int)Association.Others].Selected && string.IsNullOrEmpty(txtAssociationNatureOthers.Text.Trim()))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidAssociationNature;
                txtAssociationNatureOthers.Focus();
                Valid = false;
            }
            else if (rdAssociationNature.Items[(int)Association.Religious].Selected && rdDenomination.SelectedIndex == -1)
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidDenomination;
                rdDenomination.SelectedValue = ((int)Denomination.Christian).ToString();
                Valid = false;
            }
            else if (rdDenomination.SelectedIndex == (int)Association.Others && (string.IsNullOrEmpty(txtDenominationOthers.Text.Trim())))
            {
                this.Message = MessageCatalog.Message.LegalEntity.NotValidDenomination;
                rdDenomination.Focus();
                Valid = false;
            }
            return Valid;
        }

        private string GetSelectedNatureofAssociation()
        {
            string Selected = string.Empty;
            if (rdAssociationNature.Items[(int)Association.Cultural].Selected)
            {
                Selected += (int)Association.Cultural + ",";
            }
            if (rdAssociationNature.Items[(int)Association.Economic].Selected)
            {
                Selected += (int)Association.Economic + ",";
            }
            if (rdAssociationNature.Items[(int)Association.Educational].Selected)
            {
                Selected += (int)Association.Educational + ",";
            }
            if (rdAssociationNature.Items[(int)Association.Religious].Selected)
            {
                Selected += (int)Association.Religious + ",";
            }
            if (rdAssociationNature.Items[(int)Association.Social].Selected)
            {
                Selected += (int)Association.Social + ",";
            }
            if (rdAssociationNature.Items[(int)Association.Others].Selected)
            {
                Selected += (int)Association.Others + ",";
            }
            Selected = Selected.TrimEnd(',');
            return Selected;
        }

        private void SetAssociationNature(string assNature)
        {
            string[] nature = assNature.Split(',');
            for (int i = 0; i < nature.Length; i++)
            {
                if (this.Member.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Cultural)
                {
                    rdAssociationNature.Items[(int)Association.Cultural].Selected = true;
                }
                if (this.Member.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Economic)
                {
                    rdAssociationNature.Items[(int)Association.Economic].Selected = true;
                }
                if (this.Member.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Educational)
                {
                    rdAssociationNature.Items[(int)Association.Educational].Selected = true;
                }
                if (this.Member.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Religious)
                {
                    rdAssociationNature.Items[(int)Association.Religious].Selected = true;
                }
                if (this.Member.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Social)
                {
                    rdAssociationNature.Items[(int)Association.Social].Selected = true;
                }
                if (this.Member.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Others)
                {
                    rdAssociationNature.Items[(int)Association.Others].Selected = true;
                }
            }
        }

        private void AssignLegalEntityDetails()
        {
            try
            {
                if (CustomerId != 0)
                {
                    using (LegalEntitySystem legalEntity = new LegalEntitySystem(CustomerId))
                    {
                        // txtInstituteName.Text = legalEntity.InstitueName;
                        txtSocietyName.Text = legalEntity.SocietyName;
                        txtContactPerson.Text = legalEntity.ContactPerson;
                        txtAddress.Text = legalEntity.Address;
                        txtPlace.Text = legalEntity.Place;
                        txtFax.Text = legalEntity.Fax;
                        txtGIRNo.Text = legalEntity.GIRNo;
                        txtAoneTwoNo.Text = legalEntity.A12No;
                        txtPhone.Text = legalEntity.Phone;
                        txtState.Text = legalEntity.State;
                        txtEmail.Text = legalEntity.EMail;
                        txtPanNo.Text = legalEntity.PANNo;
                        txtTanNo.Text = legalEntity.TANNo;
                        //txtCountry.Text = legalEntity.Country;
                        if (!string.IsNullOrEmpty(legalEntity.Country))
                            ddlCountry.SelectedItem.Text = legalEntity.Country;
                        txtPinCode.Text = legalEntity.Pincode;
                        txtUrl.Text = legalEntity.URL;
                        txtRegNo.Text = legalEntity.RegNo;
                        txtPermissionNo.Text = legalEntity.PermissionNo;
                        dteRegisterDate.Text = ((legalEntity.RegDate != DateTime.MinValue) ? legalEntity.RegDate.ToString("dd/MM/yyyy") : string.Empty);
                        dtePermissionDate.Text = ((legalEntity.PermissionDate != DateTime.MinValue) ? legalEntity.PermissionDate.ToString("dd/MM/yyyy") : string.Empty);
                        dte80GRegDate.Text = ((legalEntity.G80RegDate != DateTime.MinValue) ? legalEntity.G80RegDate.ToString("dd/MM/yyyy") : string.Empty);
                        SetAssociationNature(legalEntity.AssoicationNature);
                        ltrDenomination.Visible = rdDenomination.Visible = rdAssociationNature.Items[(int)Association.Religious].Selected ? true : false;
                        if (rdAssociationNature.Items[(int)Association.Religious].Selected)
                        {
                            if (this.Member.NumberSet.ToInteger(legalEntity.Denomination.ToString()) > 0)
                            {
                                rdDenomination.SelectedValue = legalEntity.Denomination.ToString();
                            }
                            else
                            {
                                rdDenomination.SelectedIndex = -1;
                            }
                        }

                        if (rdAssociationNature.Items[(int)Association.Others].Selected)
                        {
                            txtAssociationNatureOthers.Text = legalEntity.AssociationOther;
                        }
                        if (rdDenomination.Visible)
                        {
                            if (rdDenomination.SelectedItem.Text.Equals(Denomination.Others.ToString()))
                            {
                                txtDenominationOthers.Text = legalEntity.DenominationOther;
                            }
                        }
                        showAssociationOther(); ShowDenomationOther();
                        txtFCRINo.Text = legalEntity.FCRINo;
                        dteFCRIRegDate.Date = legalEntity.FCRIRegisterDate;
                        txt80GNo.Text = legalEntity.G80No;
                        chkFoundation.Checked = legalEntity.Is_Foundation == 1 ? true : false;
                        txtPrincipalActivity.Text = legalEntity.PrincipalActivity;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }

        private void ClearValues()
        {
            txtSocietyName.Text = txtContactPerson.Text = txtAddress.Text = txtPlace.Text = txtPhone.Text = txtState.Text = txtPinCode.Text =
                txtFax.Text = txtEmail.Text = txtUrl.Text = txtRegNo.Text = dteRegisterDate.Text = txtPermissionNo.Text = dtePermissionDate.Text =
                txtAoneTwoNo.Text = txtPanNo.Text = txtGIRNo.Text = txtTanNo.Text = dte80GRegDate.Text = 
                txtAssociationNatureOthers.Text = txtDenominationOthers.Text = txtFCRINo.Text = dteFCRIRegDate.Text = txt80GNo.Text = string.Empty;
            rdAssociationNature.SelectedIndex = rdDenomination.SelectedIndex = -1;
            ltrDenomination.Visible = rdDenomination.Visible = false;
            divDenomination.Style.Add("display", "none");
            LoadCountryToCombo();
            SetDefaultCoutry();
            showAssociationOther();
            ShowDenomationOther();
            CustomerId = 0;
        }
        private void showAssociationOther()
        {
            if (rdAssociationNature.Items[(int)Association.Others].Selected)
            {
                divAssociationNature.Style.Add("display", "block");
                SetControlFocus(txtAssociationNatureOthers);
            }
            else
            {
                txtAssociationNatureOthers.Text = string.Empty;
                divAssociationNature.Style.Add("display", "none");
            }
        }
        private void ShowDenomationOther()
        {
            if (rdDenomination.Visible)
            {
                if (rdDenomination.SelectedValue == ((int)Denomination.Others).ToString())
                {
                    txtDenominationOthers.Visible = true;
                    divDenomination.Style.Add("display", "block");
                    SetControlFocus(txtDenominationOthers);
                }
                else
                {
                    txtDenominationOthers.Text = string.Empty;
                    divDenomination.Style.Add("display", "none");
                }
            }
        }

        private void LoadCountryToCombo()
        {
            try
            {
                using (HeadOfficeSystem HeadSystem = new HeadOfficeSystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    resultArgs = HeadSystem.FetchCountry();
                    if (resultArgs.Success)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlCountry, resultArgs.DataSource.Table
                        , this.AppSchema.Country.COUNTRYColumn.ColumnName
                        , this.AppSchema.Country.COUNTRY_IDColumn.ColumnName
                        , true, CommonMember.SELECT);
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("LoadCountryToCombo..." + ex.Message);
            }
        }

        /// <summary>
        /// This is to set the default country and state as India Tamil Nadu
        /// </summary>
        private void SetDefaultCoutry()
        {
            try
            {
                //Set india as default value in country combo and Tamil Nadu in state combo
                if (ddlCountry.Items.Count > 1)
                {
                    //This is the value available in the database for india id 94
                    ListItem lstcountry = new ListItem(CommonMember.DefaultCountry, CommonMember.DefaultCountryId);
                    if (ddlCountry.Items.Contains(lstcountry))
                    {
                        ddlCountry.SelectedValue = lstcountry.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("LegalEntityAdd.aspx.cs", "SetDefaultCountry", ex.Message, new CommonMethod().GetExceptionline(ex));
            }
        }
        #endregion
    }
}