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
 * Purpose          :This page helps the portal admin user to create new head office and creates the new database in their head office code and
 *                      It takes the script from the ACPERP_HO.sql Bosco.DAO whenever new head office is created. 
 *                      Head Office Code should be 6 character and used as Head office admin user name.
 *                      Portal admin can also activate and deactivate the head office
 *                      Sends automatic mail whenever head office is created or activated or deactived (First mail Id considered as Head  office admin mail id- for sending the
 *                      Head office admin credentials) others as CC and
 *                      BCC sent to the mail_ids mentioned in the web.config tag-DefaultBCCEmailId
 *                      For Sending mail and calling the finance balance udpate the dll in dll folder (AcMEDSync.dll,Bosco.HOSQL.dll) and give reference to the Bosco.Report,                                 Bosco.SQL,AcMEERP Project, Bosco.Model
 *                      NOte: Whenever Head office has to be removed, you have to manually delete the database in the server.
 *                      Whenever Head office is created, in Two database entry is made(admin_portal and in the concern Head office db name)
 *****************************************************************************************************/
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


namespace AcMeERP.Module.Office
{
    public partial class HeadOfficeAdd : Base.UIBase
    {
        #region Delcaration
        private static object objLock = new object();
        #endregion

        #region Property
        private int HeadOfficeId
        {
            get
            {
                int headOfficeId = this.Member.NumberSet.ToInteger(this.RowId);
                return headOfficeId;
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
        private string HeadOfficeCodeUpdate
        {
            get
            {
                return string.IsNullOrEmpty(ViewState["HeadOfficeCodeUpdate"].ToString()) ? string.Empty
                    : ViewState["HeadOfficeCodeUpdate"].ToString();
            }
            set { ViewState["HeadOfficeCodeUpdate"] = value; }
        }
        private int AccountingYearType
        {
            get
            {
                return ViewState["AccountingYearType"] != null ? (int)ViewState["AccountingYearType"] : 0;
            }
            set
            {
                ViewState["AccountingYearType"] = value;
            }
        }

        /// <summary>
        /// This is hold the random password of admin user
        /// </summary>
        private string RandomPassword { get; set; }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                SetControlFocus();
                hlkClose.PostBackUrl = this.ReturnUrl;
                LoadCountryToCombo();
                LoadHeadOfficeTypeToCombo();
                setdefaultvalues();
                this.CheckUserRights(RightsModule.Office, RightsActivity.HeadOfficeAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                if (HeadOfficeId > 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                else
                {
                    SetDefaultCountryState();
                }
               // this.ShowLoadWaitPopUp(btnSaveHeadOffice);
            }
        }
        /// <summary>
        /// This event is fired when user clicks Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveHeadOffice_Click(object sender, EventArgs e)
        {
            new ErrorLog().WriteError("Save Clicked");

            try
            {
                if (Page.IsValid)
                {
                    //lock (objLock)
                    //{
                        ResultArgs resultArgs = null;

                        using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
                        {
                            headOfficeSystem.HeadOfficeId = HeadOfficeId == 0 ? (int)AddNewRow.NewRow : HeadOfficeId;
                            headOfficeSystem.HeadOfficeCode = txtHOfficeCode.Text.Trim().ToLower();
                            headOfficeSystem.HeadOfficeName = txtHOName.Text.Trim();
                            headOfficeSystem.Type = this.Member.NumberSet.ToInteger(ddlType.SelectedValue.Trim());
                            headOfficeSystem.BelongsTo = txtBelongsto.Text.Trim();
                            headOfficeSystem.Org_Mail_Id = txtOrgMailId.Text.Trim();
                            headOfficeSystem.Designation = txtDesignation.Text.Trim();
                            headOfficeSystem.Incharge_Name = txtInchargeName.Text.Trim();
                            headOfficeSystem.Address = txtAddress.Text.Trim();
                            headOfficeSystem.Country_Id = this.Member.NumberSet.ToInteger(ddlCountry.SelectedValue.Trim());
                            headOfficeSystem.State_Id = this.Member.NumberSet.ToInteger(ddlState.SelectedValue.Trim());
                            headOfficeSystem.City = txtCity.Text.Trim();
                            headOfficeSystem.SR_Incharge_Name = txtSRName.Text.Trim();
                            headOfficeSystem.SR_Mobile_No = txtSRMobileNo.Text.Trim();
                            headOfficeSystem.CountryCode = txtCountryCode.Text.Trim();
                            headOfficeSystem.SR_Phone_No = txtSRPhoneNo.Text.Trim();
                            headOfficeSystem.SR_EmailId = txtSecMailId.Text.Trim();
                            headOfficeSystem.Mobile_No = txtOrgMobileNo.Text.Trim();
                            headOfficeSystem.Phone_No = txtOrgPhoneNo.Text.Trim();
                            headOfficeSystem.Pincode = txtPincode.Text.Trim();
                            headOfficeSystem.ModifiedDate = DateTime.Now;
                            headOfficeSystem.ModifiedBy = base.LoginUser.LoginUserId;
                            headOfficeSystem.CreatedDate = DateTime.Now;
                            headOfficeSystem.CreatedBy = base.LoginUser.LoginUserId;
                            headOfficeSystem.HostName = txtHostName.Text.Trim();
                            headOfficeSystem.DBName = txtDBName.Text.Trim();
                            headOfficeSystem.DBUsername = txtDBUserName.Text.Trim(); 
                            headOfficeSystem.DBPassword = txtDBPassword.Text.Trim(); 
                            headOfficeSystem.Status = 1;
                            if (ddlAccountingPeriodType.Enabled)
                            {
                                headOfficeSystem.AccountingPeriodType = this.Member.NumberSet.ToInteger(ddlAccountingPeriodType.SelectedValue.ToString());
                            }
                            else
                            {
                                headOfficeSystem.AccountingPeriodType = AccountingYearType;
                            }
                            //1- Created in add
                            if (HeadOfficeId == 0)
                            {
                                HeadOfficeCodeUpdate = txtHOfficeCode.Text.Trim().ToLower();
                            }
                            headOfficeSystem.HeadOfficeCodeUpdate = HeadOfficeCodeUpdate;

                            //Create Head Offfice Database(Automatic DB for each new Head office)
                            if (HeadOfficeId == 0)
                            {
                                new ErrorLog().WriteError("Head office Id" + HeadOfficeId);

                                RestoreDatabase objRestore = new RestoreDatabase();
                                string connectionString = "server=" + txtHostName.Text + ";database=mysql;uid="
                                    + txtDBUserName.Text + ";pwd=" + txtDBPassword.Text + "; charset=utf8; pooling=false";
                                new ErrorLog().WriteError("Head office Id" + connectionString);
                                if (!(objRestore.RestoreACPERPdatabase(txtDBName.Text, connectionString)))
                                {
                                    this.Message = MessageCatalog.Message.HeadOfficeDBCreation;

                                    //return;
                                }
                            }

                            resultArgs = headOfficeSystem.SaveHeadOfficeDetails(DataBaseType.Portal);
                            if (resultArgs.Success)
                            {
                                ResultArgs HeadofficeArgs;
                                //To set InsertedId from differen db based on logged in user credentials

                                base.HeadOfficeCode = txtHOfficeCode.Text;
                                //Set empty values to fileds which are not needed in head office data base
                                headOfficeSystem.HostName = headOfficeSystem.DBUsername = headOfficeSystem.DBPassword = headOfficeSystem.DBName = string.Empty;
                                HeadofficeArgs = headOfficeSystem.SaveHeadOfficeDetails(DataBaseType.HeadOffice);
                                if (HeadofficeArgs.Success)
                                {
                                    this.Message = MessageCatalog.Message.HeadOfficeSaved;

                                    if (HeadOfficeId == 0)
                                    {
                                        //Sends automatic mail to the Head office Admin
                                        ResultArgs mailresult;
                                        using (SettingSystem automail = new SettingSystem())
                                        {
                                            string Name = txtInchargeName.Text.Trim();
                                            string Header = " Your Head Office account with Acme.erp has been created successfully. Please wait for approval." +
                                                            "<br/>Once the account is approved you will get an email with the login details.<br />";
                                            string MainContent = "<b>Your account details available with us are as follows:</b><br/><br/>" +
                                                                 "Head Office Code: " + txtHOfficeCode.Text.ToUpper().Trim() + "<br/>" +
                                                                 "Head Office Name: " + txtHOName.Text.Trim() + "<br/>" +
                                                                 "Contact Person: <b>" + txtInchargeName.Text.Trim() + "</b><br/>" +
                                                                 "Address: " + txtAddress.Text.Trim() + "," + ddlCountry.Text.Trim() + "," + ddlState.Text.Trim() + "," + txtPincode.Text + "<br/" +
                                                                 "Mobile Number: " + txtOrgMobileNo.Text.Trim() + "<br/>" +
                                                                 "Email: " + txtOrgMailId.Text.Trim() + "<br/>";

                                            string content = CommonMethod.GetMailTemplate(Header, MainContent, Name);

                                            mailresult =  AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(txtOrgMailId.Text.Trim()),
                                            CommonMethod.RemoveFirstValue(txtOrgMailId.Text.Trim()), "Head Office Created,Waiting for Approval", content,true);
                                            if (mailresult.Success)
                                            {
                                                new ErrorLog().WriteError("HeadOfficeAdd.aspx.cs", "btnSaveHeadOffice_Click", "Creation Mail is sent", "0");
                                            }
                                        }
                                        HeadOfficeId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                        ClearValues();
                                    }

                                }
                                else
                                {
                                    this.Message = HeadofficeArgs.Message;
                                }
                            }
                            else
                            {
                                this.Message = resultArgs.Message;
                            }
                        //}
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

        /// <summary>
        /// Load states based on the country selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountrySelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStateToCombo();
            SetControlFocus(ddlState);
        }

        /// <summary>
        /// this event is fired when Head office text is changed and validates the Headoffice Code
        /// Head Office Code should be 6 character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        protected void txtHOOffice_TextChanged(object sender, EventArgs s)
        {
            if (!IsHeadOfficeCodeAvailable(txtHOfficeCode.Text.Trim()))
            {
                //txtDBUserName.Text = txtDBPassword.Text = txtDBName.Text = txtHOfficeCode.Text.Trim();
                txtDBName.Text = txtHOfficeCode.Text.Trim();
                checkOfficeCode.Visible = false;
                SetFocus(txtHOName);
            }
            else
            {
                checkOfficeCode.Visible = true;
                lblStatus.Text = MessageCatalog.Message.HeadOfficeAvailable;
                SetFocus(txtHOfficeCode);
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }


        #endregion

        #region Methods

        private void ClearValues()
        {
            HeadOfficeId = 0;
            txtHOfficeCode.Enabled = pnlDatabase.Visible = true;
            txtCountryCode.Text = CommonMember.CountryCode;
            //txtDBName.Text = txtHostName.Text = txtDBPassword.Text = txtDBUserName.Text = string.Empty;
            txtAddress.Text = txtCity.Text = txtHOfficeCode.Text = string.Empty;
            txtHOName.Text = txtBelongsto.Text = txtOrgMailId.Text = txtDesignation.Text = txtInchargeName.Text = string.Empty;
            txtSRName.Text = txtSRMobileNo.Text = txtSRPhoneNo.Text = txtSecMailId.Text = txtOrgMobileNo.Text = txtOrgPhoneNo.Text = txtPincode.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            ddlAccountingPeriodType.Enabled = true;
            ddlAccountingPeriodType.SelectedIndex = 0;
            txtDBName.Text = string.Empty;
            checkOfficeCode.Visible = false;
            SetDefaultCountryState();
            SetPageTitle();
            this.SetControlFocus();
            HeadOfficeCodeUpdate = string.Empty;
        }
        private void AssignValuesToControls()
        {
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem(HeadOfficeId, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice))
            {
                txtHOfficeCode.Text = headOfficeSystem.HeadOfficeCode;
                HeadOfficeCodeUpdate = headOfficeSystem.HeadOfficeCode;
                txtHOName.Text = headOfficeSystem.HeadOfficeName;
                ddlType.SelectedValue = headOfficeSystem.Type.ToString();
                txtBelongsto.Text = headOfficeSystem.BelongsTo;
                txtDesignation.Text = headOfficeSystem.Designation;
                txtInchargeName.Text = headOfficeSystem.Incharge_Name;
                txtOrgMobileNo.Text = headOfficeSystem.Mobile_No;
                txtOrgPhoneNo.Text = headOfficeSystem.Phone_No;
                txtOrgMailId.Text = headOfficeSystem.Org_Mail_Id;
                txtAddress.Text = headOfficeSystem.Address;
                ddlCountry.SelectedValue = headOfficeSystem.Country_Id.ToString();
                LoadStateToCombo();
                ddlState.SelectedValue = headOfficeSystem.State_Id.ToString();
                txtCity.Text = headOfficeSystem.City;
                txtPincode.Text = headOfficeSystem.Pincode;
                txtSRName.Text = headOfficeSystem.SR_Incharge_Name;
                txtSRMobileNo.Text = headOfficeSystem.SR_Mobile_No;
                txtSRPhoneNo.Text = headOfficeSystem.SR_Phone_No;
                txtSecMailId.Text = headOfficeSystem.SR_EmailId;
                UserCommunicationStatus = headOfficeSystem.UserCreatedStatus;
                pnlDatabase.Visible = false;
                if (this.CheckUserRights(RightsModule.Office, RightsActivity.HeadOffficeApprove, true,
                    base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {

                }
                //Disable Controls in Edit 
                txtHOfficeCode.Enabled = (headOfficeSystem.Status == (int)OfficeStatus.Created) ? true : false;
                AccountingYearType = headOfficeSystem.AccountingPeriodType;
                ddlAccountingPeriodType.SelectedValue = headOfficeSystem.AccountingPeriodType.ToString();
                ddlAccountingPeriodType.Enabled = (headOfficeSystem.Status == (int)OfficeStatus.Created) ? true : false;
            }

        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.HeadOffice.HeadOfficeEditPageTitle : MessageCatalog.Message.HeadOffice.HeadOfficeAddPageTitle));
        }

        private void SetControlFocus()
        {
            this.SetControlFocus(txtHOfficeCode);
        }


        /// <summary>
        /// This method checks whether Head Office Code is available.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <returns></returns>
        private bool IsHeadOfficeCodeAvailable(string headOfficeCode)
        {
            ResultArgs resultArgs = null;
            using (HeadOfficeSystem officesystem = new HeadOfficeSystem())
            {
                resultArgs = officesystem.HeadOfficeDetailsByCode(headOfficeCode);
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
            return resultArgs.Success;
        }

        private void LoadCountryToCombo()
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

        private void LoadStateToCombo()
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

        private void LoadHeadOfficeTypeToCombo()
        {
            using (HeadOfficeSystem headSystem = new HeadOfficeSystem())
            {
                headSystem.Country_Id = this.Member.NumberSet.ToInteger(ddlType.SelectedValue);
                ResultArgs resultArgs = new ResultArgs();
                resultArgs = headSystem.FetchHeadOfficeType();
                if (resultArgs.Success)
                {
                    this.Member.ComboSet.BindDataCombo(ddlType, resultArgs.DataSource.Table
                    , this.AppSchema.HOType.TYPEColumn.ColumnName
                    , this.AppSchema.HOType.TYPE_IDColumn.ColumnName
                    , true, CommonMember.SELECT);
                }
            }
        }

        private void SaveHadOfficeUser()
        {
            this.HeadOfficeCode = "";

            using (UserSystem userSystem = new UserSystem())
            {
                //Fill all the properties to save
                userSystem.SaveUser(DataBaseType.HeadOffice);
            }
        }

        private void setdefaultvalues()
        {
            txtHostName.Text = ConfigurationManager.AppSettings["DBHostname"];
            txtDBUserName.Text = ConfigurationManager.AppSettings["DBUsername"];
            txtDBPassword.Attributes["value"] = ConfigurationManager.AppSettings["DBPassword"];
        }

        /// <summary>
        /// Set india as default value in country combo and Tamil Nadu in state combo
        /// </summary>
        private void SetDefaultCountryState()
        {
            try
            {
                //Set india as default value in country combo and Tamil Nadu in state combo
                if (ddlCountry.Items.Count > 1)
                {//This is the value available in the database for india id 94
                    ListItem lstcountry = new ListItem(CommonMember.DefaultCountry, CommonMember.DefaultCountryId);
                    if (ddlCountry.Items.Contains(lstcountry))
                    {
                        ddlCountry.SelectedValue = lstcountry.Value;
                        LoadStateToCombo();
                        if (ddlState.Items.Count > 1)
                        {//This is the value available in the database for Tamil Nadu id 61
                            ListItem lststate = new ListItem(CommonMember.DefaultState, CommonMember.DefaultStateId);
                            if (ddlState.Items.Contains(lststate))
                                ddlState.SelectedValue = lststate.Value;
                        }
                    }
                    SetControlFocus(txtHOfficeCode);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("HeadOfficeAdd.aspx.cs", "SetDefaultCountryState", ex.Message, new CommonMethod().GetExceptionline(ex));
            }
        }
        #endregion

    }
}