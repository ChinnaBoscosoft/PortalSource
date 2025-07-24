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
 * Purpose          :This page helps the portal admin or head office admin to create the new branch office for each head office and activate or deactivate and send the branch office
 *                  admin credentials to the branch office admin mail(First mail Id considered as branch office admin mail id) for the given mail ids.
 *                  Branch Office Code should be first 6 character of Head office code and other entered Branch office code by the user totally (12 or more)
 *                  Branch office admin user name expect the Head office code
 *                   Sends automatic mail whenever Branch office is created or activated or deactived (First mail Id considered as Branch  office admin mail id- for sending the
 *                   Branch office admin credentials) others as CC and
 *                   BCC sent to the mail_ids mentioned in the web.config tag-DefaultBCCEmailId
 *                   For Sending mail and calling the finance balance udpate the dll in dll folder (AcMEDSync.dll,Bosco.HOSQL.dll) and give reference to the Bosco.Report, Bosco.SQL,                      AcMEERP Project, Bosco.Model
 *                   Note:Whenever NEW Branch office is created, in Two database entry is made(admin_portal and in the concern Head office db name)
 *****************************************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.IO;


namespace AcMeERP.Module.Office
{
    public partial class BranchOfficeAdd : Base.UIBase
    {

        #region Declaration
        private static object objLock = new object();
        string CountryCode = string.Empty;
        #endregion

        #region Property
        private int BranchOfficeId
        {
            get
            {
                int branchOfficeId = this.Member.NumberSet.ToInteger(this.RowId);
                return branchOfficeId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        private int UserCommunicationStatus
        {
            get;
            set;
        }

        private string BranchOfficeCodeUpdate
        {
            get
            {
                return string.IsNullOrEmpty(ViewState["BranchOfficeCodeUpdate"].ToString()) ? string.Empty
                    : ViewState["BranchOfficeCodeUpdate"].ToString();
            }
            set { ViewState["BranchOfficeCodeUpdate"] = value; }
        }

        private DataTable States
        {
            set
            {
                ViewState["States"] = value;
            }
            get
            {
                return (DataTable)ViewState["States"];
            }
        }

        private DataTable Country
        {
            set
            {
                ViewState["Country"] = value;
            }
            get
            {
                return (DataTable)ViewState["Country"];
            }
        }


        #endregion


        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                SetControlFocus();
                BindHeadOffice();//To bind HeadOffice
                LoadCountryToCombo();

                hlkClose.PostBackUrl = this.ReturnUrl;
                this.CheckUserRights(RightsModule.Office, RightsActivity.BranchOfficeAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ?
                    DataBaseType.Portal : DataBaseType.HeadOffice);

                if (base.LoginUser.IsHeadOfficeUser)
                {
                    ShowBranchInfoPopup(base.HeadOfficeCode);//show Branch Office Info
                }
                if (BranchOfficeId > 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;

                }
                else
                {
                    SetDefaultCoutryState();
                }

                if ((int)UserRole.BranchAdmin == base.LoginUser.LoginUserRoleId)
                {
                    //Enable for creating sub Branch
                    using (LicenseSystem licenseSystem = new LicenseSystem())
                    {
                        licenseSystem.BranchCode = base.LoginUser.LoginUserBranchOfficeCode;
                        if (licenseSystem.IsBranchMultilocated() && (int)UserRole.BranchAdmin == base.LoginUser.LoginUserRoleId)
                        {
                            divSubBranch.Visible = true;
                            rbtnIsSubbranch.Visible = true;
                        }
                    }
                }
                //this.ShowLoadWaitPopUp(btnSaveBranchOffice);
                this.ShowLoadWaitPopUp();
            }
            if (ddlHeadOffice.SelectedValue != "0" && base.LoginUser.IsHeadOfficeUser)  //show Branch Office Information in Pop up
            {
                ShowBranchInfoPopup(ddlHeadOffice.Text.Trim());
            }
        }

        /// <summary>
        /// This event is fired when user clicks the Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveBranchOffice_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    lock (objLock)
                    {
                        ResultArgs resultArgs = null;

                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.BranchOfficeId = BranchOfficeId == 0 ? (int)AddNewRow.NewRow : BranchOfficeId;
                            branchOfficeSystem.HeadOffice_Code = ddlHeadOffice.SelectedValue.ToString().ToLower();
                            branchOfficeSystem.BranchOfficeName = txtBOName.Text.Trim();
                            branchOfficeSystem.BranchOfficeCode = (ddlHeadOffice.SelectedItem.ToString().Trim() + txtBOfficeCode.Text.Trim()).ToLower();
                            branchOfficeSystem.BranchPartCode = txtBOfficeCode.Text.Trim().ToLower();
                            branchOfficeSystem.ThirdParyCode = txtThirdParty.Text.Trim();
                            branchOfficeSystem.Deployment_Type = this.Member.NumberSet.ToInteger(radbtnlstDeployment.SelectedValue);
                            branchOfficeSystem.State_Id = this.Member.NumberSet.ToInteger(ddlState.SelectedValue);
                            branchOfficeSystem.City = txtCity.Text.Trim();
                            branchOfficeSystem.Country_Id = this.Member.NumberSet.ToInteger(ddlCountry.SelectedValue);
                            branchOfficeSystem.PinCode = txtPinCode.Text.Trim();
                            branchOfficeSystem.Address = txtAddress.Text.Trim();
                            branchOfficeSystem.PhoneNo = txtOrgPhoneNo.Text.Trim();
                            branchOfficeSystem.MobileNo = txtOrgMobileNo.Text.Trim();
                            branchOfficeSystem.BranchEmail = txtBranchMail.Text.Trim();
                            branchOfficeSystem.CountryCode = txtCountryCode.Text;
                            branchOfficeSystem.Status = 1;//1- Created in add
                            branchOfficeSystem.ModifiedDate = DateTime.Now;
                            branchOfficeSystem.ModifiedBy = base.LoginUser.LoginUserId;
                            branchOfficeSystem.CreatedDate = DateTime.Now;
                            branchOfficeSystem.CreatedBy = base.LoginUser.LoginUserId;
                            branchOfficeSystem.PersonIncharge = txtInchargeName.Text.Trim();
                            branchOfficeSystem.IsSubBranch = rbtnIsSubbranch.Visible ? 1 : 0;//1-SubBranch-0-MainBranch
                            branchOfficeSystem.AssociateBranchCode = rbtnIsSubbranch.Visible ? base.LoginUser.LoginUserBranchOfficeCode : string.Empty;
                            //Generate Unique Branch Code
                            if (!this.HasRowId)
                            {
                                //Genearte Branch key code for license purpose (Unique License)
                                LicenseSystem licenseSystem = new LicenseSystem();
                                branchOfficeSystem.BranchKeyCode = licenseSystem.GetNewNumber(NumberFormats.BranchKeyUniqueCode, "", "");
                            }
                            if (BranchOfficeId == 0)
                            {
                                BranchOfficeCodeUpdate = (ddlHeadOffice.SelectedItem.ToString().Trim() + txtBOfficeCode.Text.Trim()).ToLower();
                            }
                            branchOfficeSystem.BranchOfficeCodeUpdate = BranchOfficeCodeUpdate;
                            resultArgs = branchOfficeSystem.SaveBranchOfficeDetails(DataBaseType.Portal);
                            if (resultArgs.Success)
                            {
                                ResultArgs HeadOfficeArs = null;  //To save details in the head office branch table
                                base.HeadOfficeCode = ddlHeadOffice.SelectedValue;

                                HeadOfficeArs = branchOfficeSystem.SaveBranchOfficeDetails(DataBaseType.HeadOffice);
                                if (HeadOfficeArs.Success)
                                {
                                    ResultArgs HeadofficeUserArgs = null;
                                    base.HeadOfficeCode = ddlHeadOffice.SelectedValue;
                                    HeadofficeUserArgs = branchOfficeSystem.SaveUserUpdateDetails(DataBaseType.HeadOffice);
                                    if (HeadofficeUserArgs.Success)
                                    {
                                        ResultArgs mailResultArgs;
                                        this.Message = MessageCatalog.Message.BranchOfficeSaved;
                                        //Subbranch is created only for the license purpose not as branch office and no datasynchronization and no master data updation
                                        if (BranchOfficeId == 0 && !rbtnIsSubbranch.Visible)
                                        {
                                            //Sends automatic mail to the Branch office admin
                                            string Name = "Branch Admin";
                                            string Header = " Your Branch Office account with Acme.erp has been created successfully. Please wait for approval. " +
                                                            "<br/>Once the account is approved you will get an email with the login details.";
                                            string MainContent = "<b>Your account details available with us are as follows:</b><br/><br/>" +
                                                                "Branch Office Code: " + ddlHeadOffice.SelectedItem.ToString().ToUpper().Trim() + txtBOfficeCode.Text.ToUpper().Trim() + "<br/>" +
                                                                "Branch Office Name: " + txtBOName.Text.Trim() + "<br/>" +
                                                                "Address: " + txtAddress.Text.Trim() + "," + ddlCountry.Text.Trim() + "," + ddlState.Text.Trim() + "," + txtPinCode.Text + "<br/" +
                                                                "Mobile Number: " + txtOrgMobileNo.Text.Trim() + "<br/>" +
                                                                "Email: " + txtBranchMail.Text.Trim() + "<br/>";

                                            string content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);
                                            mailResultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(txtBranchMail.Text.Trim()),
                                            CommonMethod.RemoveFirstValue(txtBranchMail.Text.Trim()),
                                            "Branch Office Created,Waiting for Approval", content, true);

                                            BranchOfficeId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                            ClearValues();
                                        }
                                    }
                                    else
                                    {
                                        this.Message = HeadofficeUserArgs.Message;
                                    }
                                }
                                else
                                {
                                    this.Message = HeadOfficeArs.Message;
                                }
                            }
                            else
                            {
                                this.Message = resultArgs.Message;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
            finally
            {
                //To set the current login(Portal or Head Office)
                if (base.LoginUser.IsPortalUser)
                {
                    base.HeadOfficeCode = DataBaseType.Portal.ToString();
                }
                if (BranchOfficeId == 0)
                {
                    ClearValues();
                }
            }

        }

        /// <summary>
        /// this event validates the branch office code is unique and It should be of minimum 6 and maximum of 12 character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtBOfficeCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(ltHeadofficecode.Text.Trim()) && string.IsNullOrEmpty(txtBOfficeCode.Text.Trim())))
                {
                    if ((IsBranchOfficeCodeAvailable(ltHeadofficecode.Text.Trim() + txtBOfficeCode.Text.Trim())) || ltHeadofficecode.Text.Trim().Equals(txtBOfficeCode.Text.Trim()))
                    {
                        checkOfficeCode.Visible = true;
                        lblStatus.Text = MessageCatalog.Message.BranchOfficeAvailable;
                        this.SetControlFocus(txtBOfficeCode);
                    }
                    else
                    {
                        checkOfficeCode.Visible = false;
                        this.SetControlFocus(txtBOName);

                        if (BranchOfficeId == 0)
                        {
                            txtThirdParty.Text = txtBOfficeCode.Text;
                        }

                        if (ddlHeadOffice.SelectedValue != "0")
                        {
                            ShowBranchInfoPopup(ddlHeadOffice.Text.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

        }

        protected void ddlHeadOffice_SelectedIndexChanged(object sender, EventArgs s)
        {
            try
            {
                if (ddlHeadOffice.SelectedValue != "0")
                {
                    ltHeadofficecode.Text = ddlHeadOffice.SelectedValue.Trim();
                    ShowBranchInfoPopup(ddlHeadOffice.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        /// <summary>
        /// Shows the Head office Information of the selected head office
        /// </summary>
        /// <param name="headOfficeCode"></param>

        private void ShowBranchInfoPopup(string headOfficeCode)
        {
            try
            {
                ResultArgs result = null;
                using (HeadOfficeSystem headsystem = new HeadOfficeSystem())
                {
                    result = headsystem.HeadOfficeDetailsByCode(headOfficeCode);
                }
                if (result.Success && result.RowsAffected > 0)
                {
                    DataTable dtheadoffice = result.DataSource.Table;
                    if (dtheadoffice != null && dtheadoffice.Rows.Count > 0)
                    {
                        ltName.Text = dtheadoffice.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName].ToString();
                        lthozipcode.Text = dtheadoffice.Rows[0][this.AppSchema.HeadOffice.PINCODEColumn.ColumnName].ToString();
                        lthoaddress.Text = dtheadoffice.Rows[0][this.AppSchema.HeadOffice.ADDRESSColumn.ColumnName].ToString();
                        lthocity.Text = dtheadoffice.Rows[0][this.AppSchema.HeadOffice.CITYColumn.ColumnName].ToString();
                        lthobelongsto.Text = dtheadoffice.Rows[0][this.AppSchema.HeadOffice.BELONGSTOColumn.ColumnName].ToString();
                        lthomobileno.Text = dtheadoffice.Rows[0][this.AppSchema.HeadOffice.MOBILE_NOColumn.ColumnName].ToString();
                        lthoemail.Text = dtheadoffice.Rows[0][this.AppSchema.HeadOffice.ORG_MAIL_IDColumn.ColumnName].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Head Office View", "javascript:showpopupbox(true)", true);
                    }

                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        /// <summary>
        /// When Countyr is selected loadstate and show the Headoffice info popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlCountrySelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStateToCombo();
                SetControlFocus(ddlState);
                if (ddlHeadOffice.SelectedValue != "0")
                {
                    ShowBranchInfoPopup(ddlHeadOffice.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method clears the values
        /// </summary>
        private void ClearValues()
        {
            BranchOfficeId = 0;
            txtBOfficeCode.Enabled = true;
            txtBOName.Text = txtBOfficeCode.Text = txtAddress.Text = txtBranchMail.Text = txtCity.Text = string.Empty;
            txtPinCode.Text = txtBranchMail.Text = txtOrgMobileNo.Text = txtOrgPhoneNo.Text = txtInchargeName.Text = string.Empty;
            if (base.LoginUser.IsPortalUser)
            {
                ddlHeadOffice.SelectedIndex = 0; ltHeadofficecode.Text = string.Empty; ddlHeadOffice.Enabled = true;
            }
            radbtnlstDeployment.SelectedValue = "0";
            checkOfficeCode.Visible = false;
            SetDefaultCoutryState();
            BranchOfficeCodeUpdate = string.Empty;
            txtCountryCode.Text = CommonMember.CountryCode;
            this.SetControlFocus();
            SetPageTitle();
        }

        /// <summary>
        /// This method binds the Branch office based on the logged in user.
        /// </summary>
        private void BindHeadOffice()
        {
            try
            {
                using (HeadOfficeSystem branchSystem = new HeadOfficeSystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    resultArgs = branchSystem.FetchActiveHeadOfficeDetails(string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlHeadOffice, resultArgs.DataSource.Table
                        , this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName
                        , this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName
                        , true, CommonMember.SELECT);
                    }
                }

                //Assign by default the logged in user head office code for the branch office
                if (ddlHeadOffice.Items.Count > 1)
                {
                    ddlHeadOffice.SelectedValue = base.LoginUser.LoginUserHeadOfficeCode.ToString().ToLower();
                    ltHeadofficecode.Text = base.LoginUser.LoginUserHeadOfficeCode.ToString().ToLower();
                    ddlHeadOffice.Enabled = string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode);
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        /// <summary>
        /// Show values in Edit mode
        /// </summary>
        private void AssignValuesToControls()
        {
            try
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem(BranchOfficeId, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    txtBOfficeCode.Text = branchOfficeSystem.BranchPartCode;
                    BranchOfficeCodeUpdate = string.Concat(branchOfficeSystem.HeadOffice_Code, branchOfficeSystem.BranchPartCode);
                    txtBOName.Text = branchOfficeSystem.BranchOfficeName;
                    txtBranchMail.Text = branchOfficeSystem.BranchEmail;
                    txtAddress.Text = branchOfficeSystem.Address;
                    txtOrgMobileNo.Text = branchOfficeSystem.MobileNo;
                    txtOrgPhoneNo.Text = branchOfficeSystem.PhoneNo;
                    txtPinCode.Text = branchOfficeSystem.PinCode;
                    txtThirdParty.Text = branchOfficeSystem.ThirdParyCode;
                    ddlCountry.SelectedValue = branchOfficeSystem.Country_Id.ToString();
                    LoadStateToCombo();
                    ddlState.SelectedValue = branchOfficeSystem.State_Id.ToString();
                    ddlHeadOffice.SelectedValue = branchOfficeSystem.HeadOffice_Code;
                    txtCountryCode.Text = branchOfficeSystem.CountryCode;
                    radbtnlstDeployment.SelectedValue = branchOfficeSystem.Deployment_Type.ToString();
                    txtCity.Text = branchOfficeSystem.City;
                    UserCommunicationStatus = branchOfficeSystem.UserCreatedStatus;
                    ltHeadofficecode.Text = ddlHeadOffice.SelectedValue.Trim();
                    txtInchargeName.Text = branchOfficeSystem.PersonIncharge;
                    //Disable Controls in Edit Mode
                    if (base.LoginUser.IsPortalUser)
                    {
                        //Enable only if status is in the created mode
                        ddlHeadOffice.Enabled = txtBOfficeCode.Enabled = ((int)branchOfficeSystem.Status == (int)OfficeStatus.Created) ? true : false;
                    }
                    else
                    {
                        ddlHeadOffice.Enabled = false;
                        txtBOfficeCode.Enabled = ((int)branchOfficeSystem.Status == (int)OfficeStatus.Created) ? true : false;
                    }

                    if (base.LoginUser.IsHeadOfficeUser)
                    {
                        ddlHeadOffice.SelectedValue = base.HeadOfficeCode.ToLower();
                        ltHeadofficecode.Text = ddlHeadOffice.SelectedValue.Trim();
                    }

                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        /// <summary>
        /// Sets the Page Title
        /// </summary>
        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.BranchOffice.BranchOfficeEditPageTitle : MessageCatalog.Message.BranchOffice.BranchOfficeAddPageTitle));
        }

        private void SetControlFocus()
        {
            this.SetControlFocus(ddlHeadOffice);
        }

        /// <summary>
        /// Binds Country 
        /// </summary>
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
        /// Binds State based on the Country Selected in the Country Combo
        /// </summary>
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

        private void SetPassword(TextBox txtControl, string password)
        {
            txtControl.Attributes["value"] = password;
        }

        /// <summary>
        /// This is to set the default country and state as India Tamil Nadu
        /// </summary>
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
                    SetControlFocus(ddlHeadOffice);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("BranchOfficeAdd.aspx.cs", "SetDefaultCountryState", ex.Message, new CommonMethod().GetExceptionline(ex));
            }
        }
        /// <summary>
        /// This method checks whether Branch Office Code is available.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <returns></returns>
        private bool IsBranchOfficeCodeAvailable(string branchOfficePartCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {

                using (BranchOfficeSystem officesystem = new BranchOfficeSystem())
                {
                    resultArgs = officesystem.BranchOfficeDetailsByCodeAvailable(branchOfficePartCode);
                }
                if (resultArgs.Success)
                {
                    DataTable dtoffice = resultArgs.DataSource.Table;
                    if (dtoffice != null)
                    {
                        if (dtoffice.Rows.Count == 1)
                        {
                            resultArgs.Success = true;
                        }
                        else
                        {
                            resultArgs.Success = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            return resultArgs.Success;
        }

        #endregion

        protected void txtCity_TextChanged(object sender, EventArgs e)
        {

        }

    }
}