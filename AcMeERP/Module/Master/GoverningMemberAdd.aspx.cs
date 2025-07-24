using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Master
{
    public partial class GoverningMemberAdd : Base.UIBase
    {
        #region Properties
        ResultArgs resultArgs = new ResultArgs();
        #endregion
        private int ExecutiveId
        {
            get
            {
                int executiveId = this.Member.NumberSet.ToInteger(this.RowId);
                return executiveId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hlkClose.PostBackUrl = this.ReturnUrl;
                SetPageTitle();
                LoadCountryToCombo();
                if (ExecutiveId > 0)
                {
                    AssignGoverningbodyDetails();
                    btnNew.Visible = false;
                }
                else
                {
                    SetDefaultCoutryState();
                }
                SetControlFocus();
                this.ShowLoadWaitPopUp(btnGMSave);
            }
        }
        protected void btnGMSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidGoverningMember())
                {
                    if (Page.IsValid)
                    {
                        using (ExecutiveMemberSystem executiveMemberSystem = new ExecutiveMemberSystem())
                        {
                            executiveMemberSystem.ExecutiveId = ExecutiveId == 0 ? (int)AddNewRow.NewRow : ExecutiveId;
                            executiveMemberSystem.ExecutiveName = txtMemberName.Text.Trim();
                            executiveMemberSystem.FatherName = txtFHusName.Text.Trim();
                            executiveMemberSystem.DateOfBirth = this.Member.DateSet.ToDate(dtDOB.Text);
                            executiveMemberSystem.Religion = txtReligion.Text.Trim();
                            executiveMemberSystem.Role = txtRole.Text.Trim();
                            executiveMemberSystem.Nationality = txtNationality.Text.Trim();
                            executiveMemberSystem.Occupation = txtOccupation.Text.Trim();
                            executiveMemberSystem.Association = txtOfficeHeld.Text.Trim();
                            executiveMemberSystem.OfficeBearer = txtRelationOB.Text.Trim();
                            executiveMemberSystem.Place = txtPlace.Text.Trim();
                            executiveMemberSystem.StateId = ddlState.SelectedValue != null ? this.Member.NumberSet.ToInteger(ddlState.SelectedValue.ToString()) : 0;
                            executiveMemberSystem.CountryId = ddlCountry.SelectedValue != null ? this.Member.NumberSet.ToInteger(ddlCountry.SelectedValue.ToString()) : 0;
                            executiveMemberSystem.Address = txtAddress.Text;
                            executiveMemberSystem.PinCode = txtPinCode.Text.Trim();
                            executiveMemberSystem.Pan_SSN = txtPanNo.Text.Trim();
                            executiveMemberSystem.Phone = txtContactNo.Text.Trim();
                            executiveMemberSystem.Fax = txtFax.Text.Trim();
                            executiveMemberSystem.EMail = txtMail.Text.Trim();
                            executiveMemberSystem.URL = txtUrl.Text.Trim();
                            executiveMemberSystem.DateOfAppointment = this.Member.DateSet.ToDate(dtDateofJoin.Text);
                            executiveMemberSystem.DateOfExit = this.Member.DateSet.ToDate(dtDateofExit.Text);
                            executiveMemberSystem.Notes = txtNotes.Text;
                            resultArgs = executiveMemberSystem.SaveExecutiveMemberDetails(DataBaseType.HeadOffice);
                            if (resultArgs.Success)
                            {
                                this.Message = MessageCatalog.Message.SaveConformation;
                                if (ExecutiveId > 0)
                                {
                                    AssignGoverningbodyDetails();
                                }
                                else
                                {
                                    ClearValues();
                                }
                                txtMemberName.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }
        protected void ddlCountrySelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStateToCombo();
                SetControlFocus(ddlState);
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        #endregion

        #region Methods
        private bool ValidGoverningMember()
        {
            bool isExecutiveMember = true;
            if (string.IsNullOrEmpty(txtMemberName.Text.Trim()))
            {
                this.Message = "Name is required";
                isExecutiveMember = false;
                this.SetControlFocus(txtMemberName);
            }
            else if (string.IsNullOrEmpty(txtNationality.Text.Trim()))
            {
                this.Message = "Nationality is required";
                isExecutiveMember = false;
                SetControlFocus(txtNationality);
            }
            else if (dtDOB.Date != DateTime.MinValue && dtDateofJoin.Date != DateTime.MinValue && dtDateofExit.Date != DateTime.MinValue)
            {
                if (!this.Member.DateSet.ValidateDate(dtDOB.Date, dtDateofJoin.Date))
                {
                    this.Message = "Date of Joining can't be less than Date of Birth";
                    isExecutiveMember = false;
                    SetControlFocus(dtDateofJoin);
                }
                else if (!this.Member.DateSet.ValidateDate(dtDOB.Date, dtDateofExit.Date))
                {
                    this.Message = "Date of Birth can't be less than the Date of Exit";
                    isExecutiveMember = false;
                    SetControlFocus(dtDateofExit);
                }
                else if (!this.Member.DateSet.ValidateDate(dtDateofJoin.Date, dtDateofExit.Date))
                {
                    this.Message = "Date of Exit can't be less than Date of Joining";
                    isExecutiveMember = false;
                    SetControlFocus(dtDateofExit);
                }

            }
            else if (dtDOB.Date != DateTime.MinValue && dtDateofJoin.Date != DateTime.MinValue)
            {
                if (!this.Member.DateSet.ValidateDate(dtDOB.Date, dtDateofJoin.Date))
                {
                    this.Message = "Date of Joining can't be less than Date of Birth";
                    isExecutiveMember = false;
                    SetControlFocus(dtDateofJoin);
                }
            }
            else if (dtDOB.Date != DateTime.MinValue && dtDateofExit.Date != DateTime.MinValue)
            {
                if (!this.Member.DateSet.ValidateDate(dtDOB.Date, dtDateofExit.Date))
                {
                    this.Message = "Date of Birth can't be less than the Date of Exit";
                    isExecutiveMember = false;
                    SetControlFocus(dtDateofExit);
                }
            }

            else if (dtDateofJoin.Date != DateTime.MinValue && dtDateofExit.Date != DateTime.MinValue)
            {
                if (!this.Member.DateSet.ValidateDate(dtDateofJoin.Date, dtDateofExit.Date))
                {
                    this.Message = "Date of Exit can't be less than Date of Joining";
                    isExecutiveMember = false;
                    SetControlFocus(dtDateofExit);
                }
            }
            else if (ddlCountry.SelectedValue == null)
            {
                this.Message = "Country is required";
                SetControlFocus(ddlCountry);
                isExecutiveMember = false;
            }
            else if (ddlState.SelectedValue == null)
            {
                this.Message = "State is required";
                SetControlFocus(ddlState);
                isExecutiveMember = false;
            }
            else if (!string.IsNullOrEmpty(txtMail.Text.Trim()))
            {

                this.Message = "Email is required";
                SetControlFocus(txtMail);
                isExecutiveMember = false;

            }
            return isExecutiveMember;
        }
        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ?
                MessageCatalog.Message.GoverningMember.GoverningMemberEditPageTitle : MessageCatalog.Message.GoverningMember.GoverningMemberAddPageTitle));
        }

        private void SetControlFocus()
        {
            this.SetControlFocus(txtMemberName);
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

        private void LoadStateToCombo()
        {
            try
            {
                using (HeadOfficeSystem headSystem = new HeadOfficeSystem())
                {
                    headSystem.Country_Id = this.Member.NumberSet.ToInteger(ddlCountry.SelectedValue);
                    ResultArgs resultArgs = new ResultArgs();
                    resultArgs = headSystem.FetchStateByCountry();
                    if (resultArgs.Success)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlState, resultArgs.DataSource.Table
                        , this.AppSchema.State.STATEColumn.ColumnName
                        , this.AppSchema.State.STATE_IDColumn.ColumnName
                        , true, CommonMember.SELECT);
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("LoadStateToCombo..." + ex.Message);
            }
        }

        private void SetDefaultCoutryState()
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
                        LoadStateToCombo();
                        if (ddlState.Items.Count > 1)
                        {
                            //This is the value available in the database for Tamil Nadu id 61
                            ListItem lststate = new ListItem(CommonMember.DefaultState, CommonMember.DefaultStateId);
                            if (ddlState.Items.Contains(lststate))
                                ddlState.SelectedValue = lststate.Value;
                        }
                    }
                    SetControlFocus(txtMemberName);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("BranchOfficeAdd.aspx.cs", "SetDefaultCountryState", ex.Message, new CommonMethod().GetExceptionline(ex));
            }
        }

        private void ClearValues()
        {
            ExecutiveId = 0;
            txtMemberName.Text = txtNationality.Text = txtReligion.Text = txtRole.Text = txtFHusName.Text = string.Empty;
            txtRelationOB.Text = txtOfficeHeld.Text = txtMail.Text = txtFax.Text = txtPinCode.Text = string.Empty;
            txtUrl.Text = txtPlace.Text = txtContactNo.Text = txtAddress.Text = txtNotes.Text = txtOccupation.Text = txtPanNo.Text = string.Empty;
            dtDOB.Text = dtDateofJoin.Text = dtDateofExit.Text = string.Empty;
            LoadCountryToCombo();
            LoadStateToCombo();
            SetDefaultCoutryState();
            SetControlFocus();
            SetPageTitle();
        }

        private void AssignGoverningbodyDetails()
        {
            try
            {
                if (ExecutiveId > 0)
                {
                    using (ExecutiveMemberSystem executivesystem = new ExecutiveMemberSystem(ExecutiveId))
                    {
                        txtMemberName.Text = executivesystem.ExecutiveName;
                        txtFHusName.Text = executivesystem.FatherName;
                        dtDOB.Text = this.Member.DateSet.ToDate(executivesystem.DateOfBirth.ToString());
                        txtReligion.Text = executivesystem.Religion;
                        txtRole.Text = executivesystem.Role;
                        txtNationality.Text = executivesystem.Nationality;
                        txtOccupation.Text = executivesystem.Occupation;
                        txtOfficeHeld.Text = executivesystem.Association;
                        txtRelationOB.Text = executivesystem.OfficeBearer;
                        txtPlace.Text = executivesystem.Place;
                        txtRelationOB.Text = executivesystem.OfficeBearer;
                        ddlCountry.SelectedValue = executivesystem.CountryId.ToString();
                        LoadStateToCombo();
                        ddlState.SelectedValue = executivesystem.StateId.ToString();
                        txtAddress.Text = executivesystem.Address;
                        txtPinCode.Text = executivesystem.PinCode;
                        txtPanNo.Text = executivesystem.Pan_SSN;
                        txtContactNo.Text = executivesystem.Phone;
                        txtFax.Text = executivesystem.Fax;
                        txtMail.Text = executivesystem.EMail;
                        txtUrl.Text = executivesystem.URL;
                        dtDateofJoin.Text = this.Member.DateSet.ToDate(executivesystem.DateOfAppointment.ToString());
                        dtDateofExit.Text = this.Member.DateSet.ToDate(executivesystem.DateOfExit.ToString());
                        txtNotes.Text = executivesystem.Notes;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }

        #endregion

    }
}