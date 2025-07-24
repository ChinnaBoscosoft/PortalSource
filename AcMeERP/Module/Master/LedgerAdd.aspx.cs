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
 * Purpose          :This page helps head office admin to create the HO ledgers for the branch office and map the ledgers to the HO project category
 *****************************************************************************************************/
using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections.Generic;
using Bosco.Model.UIModel.Master;

namespace AcMeERP.Module.Master
{
    public partial class LedgerAdd : Base.UIBase
    {
        #region Properties
        private const string Nature_of_Payments = "Nature of Payments * ";
        private const string Deductee_Type = "Deductee Type * ";
        static string SELECT = "SELECT";
        static string CON_LEDGER_NAME = "CON_LEDGER_NAME";
        private int LedgerId
        {
            get
            {
                int LedgerId = this.Member.NumberSet.ToInteger(this.RowId);
                return LedgerId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        private DataTable ProjectCategorySource
        {
            set
            {
                ViewState["ProjectCategorySource"] = value;
            }
            get
            {
                return (DataTable)ViewState["ProjectCategorySource"];
            }
        }

        #endregion

        #region Declaration
        LedgerSystem ledgersystem = new LedgerSystem();
        ledgerSubType ledType;

        private DataTable LedgerGroupDetails
        {
            set
            {
                ViewState["LedgerGroupDetails"] = value;
            }
            get
            {
                return (DataTable)ViewState["LedgerGroupDetails"];
            }
        }

        private DataTable BudgetGroup
        {
            set
            {
                ViewState["BudgetGroup"] = value;
            }
            get
            {
                return (DataTable)ViewState["BudgetGroup"];
            }
        }

        private DataTable BudgetSubGroup
        {
            set
            {
                ViewState["BudgetSubGroup"] = value;
            }
            get
            {
                return (DataTable)ViewState["BudgetSubGroup"];
            }
        }

        private DataTable FDInvestmentType
        {
            set
            {
                ViewState["FDInvestment"] = value;
            }
            get
            {
                return (DataTable)ViewState["FDInvestment"];
            }
        }

        private DataTable GeneralateGroupDetails
        {
            set
            {
                ViewState["GeneralateGroupDetails"] = value;
            }
            get
            {
                return (DataTable)ViewState["GeneralateGroupDetails"];
            }
        }

        ResultArgs resultArgs = null;

        private int NatureId
        {
            set
            {
                ViewState["NatureId"] = value;
            }
            get
            {
                return (int)ViewState["NatureId"];
            }
        }

        private int GroupID
        {
            set
            {
                ViewState["GroupID"] = value;
            }
            get
            {
                return (int)ViewState["GroupID"];
            }
        }

        private int GeneralateLedgerId
        {
            set
            {
                ViewState["CON_LEDGER_ID"] = value;
            }
            get
            {
                return (int)ViewState["CON_LEDGER_ID"];
            }
        }

        #endregion

        #region Methods

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.Ledger.LedgerEditPageTitle : MessageCatalog.Message.Ledger.LedgerAddPageTitle));
        }

        private void AssignLedgerDetails()
        {
            try
            {
                if (LedgerId > 0)
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem(LedgerId))
                    {
                        txtCode.Text = ledgerSystem.LedgerCode;
                        ledgerSystem.LedgerId = LedgerId;
                        txtName.Text = ledgerSystem.LedgerName;
                        ddlGroup.SelectedValue = ledgerSystem.GroupId.ToString();
                        GroupID = ledgerSystem.GroupId;

                        ddlBudgetGroup.SelectedValue = ledgerSystem.BudgetGroupId.ToString();

                        if (ledgerSystem.BudgetGroupId == 2)
                        {
                            ddlBudgetSubGroup.SelectedValue = "0";
                            ddlBudgetSubGroup.Enabled = false;
                        }
                        else
                        {
                            ddlBudgetSubGroup.SelectedValue = ledgerSystem.BudgetSubGroupId.ToString();
                            ddlBudgetSubGroup.Enabled = true;
                        }

                        HideShowFDInvestment(GroupID);
                        if (GroupID == CommonMember.FixedDepositGroup)
                        {
                            ddlFDInvestmentType.SelectedValue = ledgerSystem.FDInvTypeId.ToString();
                        }
                        else
                        {
                            ddlFDInvestmentType.SelectedValue = "0";
                        }
                        // Commanded by chinna 22.10.2020
                        //   GeneralateLedgerId = ledgerSystem.GeneralateGroupLedgerId;
                        //  ddlGeneralateGroup.SelectedValue = GeneralateLedgerId == 0 ? CommonMember.SELECT : ledgerSystem.GeneralateGroupLedgerId.ToString();

                        //ddlGeneralateGroup.SelectedValue = GeneralateLedgerID != 0 ? ledgerSystem.GeneralateGroupLedgerId.ToString() : CommonMember.SELECT;
                        // if (GeneralateLedgerId == 0)
                        //  ddlGeneralateGroup.Enabled = true;
                        //  else
                        //ddlGeneralateGroup.Enabled = false;
                        txtNotes.Text = ledgerSystem.LedgerNotes;
                        ddlLedgerType.SelectedValue = (ledgerSystem.LedgerType == ledgerSubType.GN.ToString()) ? "1" : "2";
                        chkAssetGainLedger.Checked = ledgerSystem.IsAssetGainLedger > 0 ? true : false;
                        chkAssetLossLedger.Checked = ledgerSystem.IsAssetLossLedger > 0 ? true : false;
                        chkDisposalLedger.Checked = ledgerSystem.IsAssetDisposalLedger > 0 ? true : false;
                        chkDepreciationLedger.Checked = ledgerSystem.IsDepriciationLedger > 0 ? true : false;
                        chkInKindLedger.Checked = ledgerSystem.IsInKindLedger > 0 ? true : false;
                        LoadProjectCategory();
                        using (LedgerSystem ledgerSys = new LedgerSystem())
                        {
                            NatureId = ledgerSys.FetchLedgerNatureByLedgerGroup(GroupID);
                        }
                        //Group Ledger Options for Income and Expense
                        GroupLedgerOptionsByNature(NatureId);
                        //if (NatureId.Equals(2) || GroupID.Equals(24) || GroupID.Equals(26))
                        //{
                        chkIsTDSApplicable.Visible = true;
                        if (ledgerSystem.NatureOfPaymentId > 0 || ledgerSystem.DeducteeTypeId > 0)
                        {
                            chkIsTDSApplicable.Checked = true;
                        }
                        else
                        {
                            chkIsTDSApplicable.Checked = false;
                        }
                        //}
                        if (ledgerSystem.NatureOfPaymentId > 0)
                        {
                            chkIsTDSApplicable.Visible = true;
                            pnlIsTDSApplicable.Visible = true;
                            ltrlNatureDeductee.Text = Nature_of_Payments;
                            FetchDefaultNatureofPayments();
                            ddlNatureDeductee.SelectedValue = ledgerSystem.NatureOfPaymentId.ToString();
                        }
                        if (ledgerSystem.DeducteeTypeId > 0)
                        {
                            chkIsTDSApplicable.Visible = true;
                            pnlIsTDSApplicable.Visible = true;
                            ltrlNatureDeductee.Text = Deductee_Type;
                            FetchDeducteeTypes();
                            ddlNatureDeductee.SelectedValue = ledgerSystem.DeducteeTypeId.ToString();
                        }
                        if (NatureId == 3 || NatureId == 4)
                        {
                            pnlLedgerProfile.Visible = true;
                            txtLedgerProfileName.Text = ledgerSystem.LedgerProfileName;
                            txtProfileAddress.Text = ledgerSystem.LedgerProfileAddress;
                            txtProfileEmail.Text = ledgerSystem.LedgerProfileEmail;
                            txtProfileContactNo.Text = ledgerSystem.LedgerProfileContactNo;
                            if (!ledgerSystem.LedgerProfileCountryId.ToString().Equals("0"))
                            {
                                ddlProfileCountry.SelectedValue = ledgerSystem.LedgerProfileCountryId.ToString();
                            }
                            if (!ledgerSystem.LedgerProfileStateId.ToString().Equals("0"))
                            {
                                ddlProfileState.SelectedValue = ledgerSystem.LedgerProfileStateId.ToString();
                            }
                            txtProfilePanNo.Text = ledgerSystem.LedgerProfilePanNo;
                            txtProfilePINCode.Text = ledgerSystem.LedgerProfilePincode;
                            LoadLedgerGroup();

                            // chinna on 22.10.2020
                            //if (!ledgersystem.GeneralateGroupLedgerId.ToString().Equals("0"))
                            //{
                            //    LoadGeneralateLedgers();
                            //}

                            LoadCountryToCombo();
                            LoadStateToCombo();
                        }
                        if (ledgerSystem.IsCostCentre == (int)YesNo.No)
                            chkIncludeCostCenter.Checked = false;
                        else
                            chkIncludeCostCenter.Checked = true;
                        if (ledgerSystem.IsFDInterestLedger == (int)YesNo.No)
                            chkBankInterestLedger.Checked = false;
                        else
                            chkBankInterestLedger.Checked = true;

                        chkFDPenaltyLedger.Checked = false;
                        if (ledgerSystem.IsFDPenaltyLedger == (int)YesNo.Yes)
                            chkFDPenaltyLedger.Checked = true;

                        chkBankSBInterestLedger.Checked = false;
                        if (ledgerSystem.IsSBInterestLedger == (int)YesNo.Yes)
                            chkBankSBInterestLedger.Checked = true;

                        chkBankCommissionLedger.Checked = false;
                        if (ledgerSystem.IsBankCommissionLedger == (int)YesNo.Yes)
                            chkBankCommissionLedger.Checked = true;

                        //On 25/11/2021, To get Ledger Closed Date
                        if (ledgerSystem.LedgerClosedOn != null && ledgerSystem.LedgerClosedOn != DateTime.MinValue)
                        {
                            dteClosedOn.Date = ledgerSystem.LedgerClosedOn;
                        }

                    }
                    btnNew.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                this.Message = Ex.Message;
            }
            finally { }
        }

        private void MapLedgers()
        {
            try
            {
                using (CongregationLedgerMappingSystem congregationSystem = new CongregationLedgerMappingSystem())
                {
                    if (GeneralateLedgerId > 0)
                    {
                        congregationSystem.GeneralateLedgerId = GeneralateLedgerId;
                        resultArgs = congregationSystem.DeleteSingleMappedLedger(LedgerId);
                        if (resultArgs.Success)
                        {
                            resultArgs = congregationSystem.MapLedger(GeneralateLedgerId, LedgerId);
                            if (!resultArgs.Success)
                            {
                                this.Message = resultArgs.Message;
                            }
                        }
                    }
                    //else
                    //{
                    //    this.Message = MessageCatalog.Message.GeneralateLedger.NoGeneralateLedger;
                    //}
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        private bool ValidateLedgerDetails()
        {
            bool IsGroudValid = true;
            //if (string.IsNullOrEmpty(txtCode.Text))
            //{
            //    this.Message = MessageCatalog.Message.Ledger.LedgerCodeFieldEmpty;
            //    IsGroudValid = false;
            //    txtCode.Focus();
            //}
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.Message = MessageCatalog.Message.Ledger.LedgerNameFieldEmpty;
                IsGroudValid = false;
                txtName.Focus();
            }
            else if (ddlGroup.SelectedValue == null)
            {
                this.Message = MessageCatalog.Message.Ledger.LedgerNameFieldEmpty;
                IsGroudValid = false;
                ddlGroup.Focus();
            }
            else if (dteClosedOn.Date != null && this.dteClosedOn.Date != DateTime.MinValue)
            {
                using (LedgerSystem ledSys = new LedgerSystem())
                {
                    resultArgs = ledSys.CheckLedgerClosedDate(LedgerId, this.dteClosedOn.Date);
                }

                if (!resultArgs.Success)
                {
                    this.Message = resultArgs.Message;
                    IsGroudValid = false;
                    dteClosedOn.Focus();
                }
            }
            //else if (ddlGeneralateGroup.SelectedValue == "0")
            //{
            //    this.Message = "Generalate Ledger is empty";
            //    IsGroudValid = false;
            //    ddlGeneralateGroup.Focus();
            //}
            else if (chkIsTDSApplicable.Checked)
            {
                if (GroupID.Equals((int)TDSLedgerGroup.ExpensesLedger) && string.IsNullOrEmpty(ddlNatureDeductee.Text))
                {
                    if (ddlNatureDeductee.SelectedValue == "0")
                    {
                        this.Message = "Nature of Payment is required for the Expense Ledger, If TDS applicable.";
                        IsGroudValid = false;
                        ddlNatureDeductee.Focus();
                    }
                }
                if (GroupID.Equals((int)TDSLedgerGroup.SundryCreditors) && string.IsNullOrEmpty(ddlNatureDeductee.Text))
                {
                    if (ddlNatureDeductee.SelectedValue == "0")
                    {
                        this.Message = "Deductee Type is required for the Sundry Creditors Ledger, If TDS applicable.";
                        IsGroudValid = false;
                        ddlNatureDeductee.Focus();
                    }
                }
            }
            return IsGroudValid;
        }

        private void LoadLedgerGroup()
        {
            using (LedgerGroupSystem LedgerGroupSystem = new LedgerGroupSystem())
            {
                resultArgs = LedgerGroupSystem.LoadLedgerGroupforLedgerLoodkup(ledgerSubType.GN, DataBaseType.HeadOffice);
                this.Member.ComboSet.BindDataCombo(ddlGroup, resultArgs.DataSource.Table, this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName,
                    this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName, true, CommonMember.SELECT);
                LedgerGroupDetails = resultArgs.DataSource.Table;
            }
        }

        private void LoadBudgetGroup()
        {
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                resultArgs = ledgersystem.LoadBudgetGroupforLoodkup();
                this.Member.ComboSet.BindDataCombo(ddlBudgetGroup, resultArgs.DataSource.Table, this.AppSchema.BudgetGroup.BUDGET_GROUPColumn.ColumnName,
                    this.AppSchema.BudgetGroup.BUDGET_GROUP_IDColumn.ColumnName, true, CommonMember.SELECT);
                ddlBudgetGroup.SelectedValue = ddlBudgetGroup.SelectedValue != null ? ddlBudgetGroup.SelectedValue.ToString() : "0";
                BudgetGroup = resultArgs.DataSource.Table;
            }
        }

        private void LoadBudgetSubGroup()
        {
            using (LedgerSystem Ledgersystem = new LedgerSystem())
            {
                resultArgs = Ledgersystem.LoadBudgetSubGroupforLoodkup();
                this.Member.ComboSet.BindDataCombo(ddlBudgetSubGroup, resultArgs.DataSource.Table, this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ColumnName,
                    this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUP_IDColumn.ColumnName, true, CommonMember.SELECT);
                ddlBudgetSubGroup.SelectedValue = ddlBudgetSubGroup.SelectedValue != null ? ddlBudgetSubGroup.SelectedValue.ToString() : "0";
                BudgetSubGroup = resultArgs.DataSource.Table;
            }
        }

        private void LoadFDInvestmentType()
        {
            using (LedgerSystem Ledgersystem = new LedgerSystem())
            {
                resultArgs = ledgersystem.LoadFDInvestmentforLookup();
                this.Member.ComboSet.BindDataCombo(ddlFDInvestmentType, resultArgs.DataSource.Table, this.AppSchema.FDInvestment.INVESTMENT_TYPEColumn.ColumnName,
                    this.AppSchema.FDInvestment.INVESTMENT_TYPE_IDColumn.ColumnName, true, CommonMember.SELECT);
                ddlFDInvestmentType.SelectedValue = ddlFDInvestmentType.SelectedValue != null ? ddlFDInvestmentType.SelectedValue.ToString() : "0";
                FDInvestmentType = resultArgs.DataSource.Table;
            }
        }

        // chinna on 22.101.2020
        //private void LoadGeneralateLedgers()
        //{
        //    using (GeneralateSystem generalateSystem = new GeneralateSystem())
        //    {
        //        resultArgs = generalateSystem.FetchParentLedgers();// Loading Generalte ledgers which does not have any child ledgers to that.
        //        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //        {
        //            this.Member.ComboSet.BindDataCombo(ddlGeneralateGroup, resultArgs.DataSource.Table, CON_LEDGER_NAME, AppSchema.CongregationLedger.CON_LEDGER_IDColumn.ColumnName, true, CommonMember.SELECT);
        //            GeneralateLedgerId = this.Member.NumberSet.ToInteger(ddlGeneralateGroup.SelectedItem.Value.ToString());
        //            GeneralateGroupDetails = resultArgs.DataSource.Table;
        //            //GeneralateGroupDetails = resultArgs.DataSource.Table;
        //            // this.Member.ComboSet.BindCombo(cmbGeneralateLedger, resultArgs.DataSource.Table, CON_LEDGER_NAME, AppSchema.CongregationLedger.CON_LEDGER_IDColumn.ColumnName, false);
        //            // GeneralateLedgerId = this.Member.NumberSet.ToInteger(cmbGeneralateLedger.SelectedItem.Value.ToString());
        //        }
        //    }
        //}

        //private void LoadLedgerCode()
        //{
        //    using (LedgerSystem ledgersystem = new LedgerSystem())
        //    {
        //        resultArgs = ledgersystem.FetchLedgerCodes(DataBaseType.HeadOffice);
        //        if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        {
        //            this.Member.ComboSet.BindDataList(lvUsedCodes, resultArgs.DataSource.Table, AppSchema.Ledger.LEDGER_CODEColumn.ColumnName, AppSchema.Ledger.LEDGER_CODEColumn.ColumnName);
        //            if (LedgerId == 0)
        //            {
        //                lvUsedCodes.SelectedIndex = 0;
        //                txtCode.Text = Base.MasterBase.CodePredictor(lvUsedCodes.Text, resultArgs.DataSource.Table);
        //            }
        //        }
        //    }

        //}

        private void ClearValues()
        {
            txtName.Text = txtNotes.Text = string.Empty;
            txtLedgerProfileName.Text = txtProfileAddress.Text = txtProfileEmail.Text = txtProfileContactNo.Text =
                txtProfilePanNo.Text = txtCode.Text = txtProfilePINCode.Text = string.Empty;
            ddlGroup.SelectedValue = "0";
            ddlBudgetGroup.SelectedValue = "0";
            ddlBudgetSubGroup.SelectedValue = "0";
            ddlFDInvestmentType.SelectedValue = "0";
            ddlLedgerType.SelectedIndex = 1;
            chkBankInterestLedger.Checked = chkIncludeCostCenter.Checked = chkFDPenaltyLedger.Checked = chkBankSBInterestLedger.Checked = chkBankCommissionLedger.Checked = false;
            chkAssetGainLedger.Checked = chkAssetLossLedger.Checked = chkBankInterestLedger.Checked = chkDepreciationLedger.Checked = false;
            chkDisposalLedger.Checked = chkInKindLedger.Checked = false;
            divExpenseNature.Visible = divIncomeNature.Visible = false;
            LedgerId = 0;
            pnlLedgerProfile.Visible = false;
            chkIsTDSApplicable.Checked = false;
            pnlIsTDSApplicable.Visible = false;
            gvProjectCategory.Selection.UnselectAll();

            this.SetControlFocus(ddlGroup);
        }

        private void SetDefaultCoutryState()
        {
            try
            {
                //Set india as default value in country combo and Tamil Nadu in state combo
                if (ddlProfileCountry.Items.Count > 1)
                {
                    //This is the value available in the database for india id 94
                    ListItem lstcountry = new ListItem(CommonMember.DefaultCountry, CommonMember.DefaultCountryId);
                    if (ddlProfileCountry.Items.Contains(lstcountry))
                    {
                        ddlProfileCountry.SelectedValue = lstcountry.Value;
                        LoadStateToCombo();
                        if (ddlProfileState.Items.Count > 1)
                        {
                            //This is the value available in the database for Tamil Nadu id 61
                            ListItem lststate = new ListItem(CommonMember.DefaultState, CommonMember.DefaultStateId);
                            if (ddlProfileState.Items.Contains(lststate))
                                ddlProfileState.SelectedValue = lststate.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("BranchOfficeAdd.aspx.cs", "SetDefaultCountryState", ex.Message, new CommonMethod().GetExceptionline(ex));
            }
        }

        private void LoadCountryToCombo()
        {
            using (HeadOfficeSystem HeadSystem = new HeadOfficeSystem())
            {
                ResultArgs resultArgs = new ResultArgs();
                resultArgs = HeadSystem.FetchCountry();
                if (resultArgs.Success)
                {
                    this.Member.ComboSet.BindDataCombo(ddlProfileCountry, resultArgs.DataSource.Table
                    , this.AppSchema.Country.COUNTRYColumn.ColumnName
                    , this.AppSchema.Country.COUNTRY_IDColumn.ColumnName
                    , true, CommonMember.SELECT);
                }
            }
        }

        private void FetchDefaultNatureofPayments()
        {
            using (DeducteeTypeSystem deducteeSystem = new DeducteeTypeSystem())
            {
                resultArgs = deducteeSystem.NOP();
                if (resultArgs.Success)
                {
                    this.Member.ComboSet.BindDataCombo(ddlNatureDeductee, resultArgs.DataSource.Table,
                        this.AppSchema.NatureOfPayment.NAMEColumn.ColumnName,
                        this.AppSchema.NatureOfPayment.NATURE_PAY_IDColumn.ColumnName, true, CommonMember.SELECT);
                }
            }
        }

        private void FetchDeducteeTypes()
        {
            using (DeducteeTypeSystem deducteeSystem = new DeducteeTypeSystem())
            {
                resultArgs = deducteeSystem.FetchActiveDeductTypes();
                if (resultArgs.Success)
                {
                    this.Member.ComboSet.BindDataCombo(ddlNatureDeductee, resultArgs.DataSource.Table,
                        this.AppSchema.DeducteeType.NAMEColumn.ColumnName,
                        this.AppSchema.DeducteeType.DEDUCTEE_TYPE_IDColumn.ColumnName, true, CommonMember.SELECT);
                }
            }
        }

        private void LoadStateToCombo()
        {
            using (HeadOfficeSystem headSystem = new HeadOfficeSystem())
            {
                headSystem.Country_Id = this.Member.NumberSet.ToInteger(ddlProfileCountry.SelectedValue);
                ResultArgs resultArgs = new ResultArgs();
                resultArgs = headSystem.FetchStateByCountry();
                if (resultArgs.Success)
                {
                    this.Member.ComboSet.BindDataCombo(ddlProfileState, resultArgs.DataSource.Table
                    , this.AppSchema.State.STATEColumn.ColumnName
                    , this.AppSchema.State.STATE_IDColumn.ColumnName
                    , true, CommonMember.SELECT);
                }
            }
        }

        private void LoadProjectCategory()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ResultArgs resultArgs = new ResultArgs();
                resultArgs = ledgerSystem.FetchProjectCategoryByLedger(LedgerId = LedgerId == 0 ? 0 : LedgerId);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    ProjectCategorySource = resultArgs.DataSource.Table;
                    gvProjectCategory.DataSource = ProjectCategorySource;
                    gvProjectCategory.DataBind();
                    if (LedgerId > 0)
                        SelectMappedLedgers();
                }
                else
                {
                    ProjectCategorySource = null;
                    gvProjectCategory.DataSource = null;
                    gvProjectCategory.DataBind();
                }
            }
        }

        private void SelectMappedLedgers()
        {
            gvProjectCategory.Selection.UnselectAll();
            for (int i = 0; i < ProjectCategorySource.Rows.Count; i++)
            {
                if (this.Member.NumberSet.ToInteger(ProjectCategorySource.Rows[i][SELECT].ToString()) == 1)
                {
                    gvProjectCategory.Selection.SelectRowByKey(ProjectCategorySource.Rows[i]["PROJECT_CATOGORY_ID"]);
                }
            }
        }

        #endregion

        #region Evnets

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                hlkClose.PostBackUrl = this.ReturnUrl;
                LoadLedgerGroup();

                LoadBudgetGroup();
                LoadBudgetSubGroup();

                LoadFDInvestmentType();

                // chinna on 22.10.2020
                //LoadGeneralateLedgers();

                LoadProjectCategory();
                ddlLedgerType.SelectedIndex = 1;
                this.CheckUserRights(RightsModule.Data, RightsActivity.LedgerAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);

                //Only one Income options can be checked.
                chkBankInterestLedger.Attributes.Add("onclick", "return validateIncomeCheckboxes_ClientValidate(this)");
                chkAssetGainLedger.Attributes.Add("onclick", "return validateIncomeCheckboxes_ClientValidate(this)");
                chkInKindLedger.Attributes.Add("onclick", "return validateIncomeCheckboxes_ClientValidate(this)");

                //Only one Expense options can be checked.
                chkDisposalLedger.Attributes.Add("onclick", "return validateExpenseCheckboxes_ClientValidate(this)");
                chkAssetLossLedger.Attributes.Add("onclick", "return validateExpenseCheckboxes_ClientValidate(this)");
                chkDepreciationLedger.Attributes.Add("onclick", "return validateExpenseCheckboxes_ClientValidate(this)");
                if (LedgerId != 0)
                {
                    AssignLedgerDetails();
                    btnNew.Visible = false;
                }
                this.SetControlFocus(ddlGroup);
                this.ShowLoadWaitPopUp(btnSaveUser);
            }

        }
        protected void ddlNatureDeductee_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlLedgerProfile.Visible)
            {
                this.SetControlFocus(txtLedgerProfileName);
            }
        }
        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (ValidateLedgerDetails())
            {
                try
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        List<object> projectCategory = new List<object>();
                        projectCategory = gvProjectCategory.GetSelectedFieldValues(AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName);
                        ledgerSystem.lProjectCategoryId = projectCategory;
                        //ledgerSystem.LedgerCode = txtCode.Text.Trim().ToUpper();
                        ledgerSystem.LedgerCode = txtCode.Text.Trim();
                        ledgerSystem.LedgerName = txtName.Text.Trim();
                        ledgerSystem.GroupId = this.Member.NumberSet.ToInteger(ddlGroup.SelectedValue.ToString());
                        ledgerSystem.BudgetGroupId = ddlBudgetGroup.SelectedValue != null ? this.Member.NumberSet.ToInteger(ddlBudgetGroup.SelectedValue.ToString()) : 0;
                        ledgerSystem.BudgetSubGroupId = ddlBudgetSubGroup.SelectedValue != null ? this.Member.NumberSet.ToInteger(ddlBudgetSubGroup.SelectedValue.ToString()) : 0;

                        if (ledgerSystem.GroupId == CommonMember.FixedDepositGroup)
                        {
                            ledgerSystem.FDInvTypeId = ddlFDInvestmentType.SelectedValue != null ? this.Member.NumberSet.ToInteger(ddlFDInvestmentType.SelectedValue.ToString()) : 0;
                        }
                        else
                        {
                            ledgerSystem.FDInvTypeId = 0;
                        }

                        // chinna on 22.10.2020
                        //ledgerSystem.GeneralateGroupLedgerId = GeneralateLedgerId = this.Member.NumberSet.ToInteger(ddlGeneralateGroup.SelectedValue.ToString());

                        ledgerSystem.IsCostCentre = (chkIncludeCostCenter.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsFDInterestLedger = (chkBankInterestLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsTDSApplicable = (chkIsTDSApplicable.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.LedgerType = GetLedgerType(ledgerSystem.GroupId);
                        ledgerSystem.LedgerSubType = ledgerSystem.GroupId == 14 ? ledgerSubType.FD.ToString() : ledgerSubType.GN.ToString();
                        ledgerSystem.LedgerId = (LedgerId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : LedgerId;
                        ledgerSystem.LedgerNotes = txtNotes.Text.Trim();
                        ledgerSystem.SortId = this.Member.NumberSet.ToInteger(ledgerSubType.GN.ToString());// (int)LedgerSortOrder.Bank;
                        ledgerSystem.FDType = ledgerSubType.GN.ToString();
                        ledgerSystem.LedgerProfileName = txtLedgerProfileName.Text.Trim();
                        ledgerSystem.LedgerProfileAddress = txtProfileAddress.Text.Trim();
                        ledgerSystem.LedgerProfileEmail = txtProfileEmail.Text.Trim();
                        ledgerSystem.LedgerProfileContactNo = txtProfileContactNo.Text.Trim();
                        ledgerSystem.LedgerProfilePanNo = txtProfilePanNo.Text.Trim();
                        ledgerSystem.LedgerProfilePincode = txtProfilePINCode.Text.Trim();
                        if (chkIsTDSApplicable.Checked)
                        {
                            ledgerSystem.NatureOfPaymentId = this.Member.NumberSet.ToInteger(ddlNatureDeductee.SelectedValue.ToString());
                        }
                        ledgerSystem.LedgerProfileCountryId = this.Member.NumberSet.ToInteger(ddlProfileCountry.SelectedValue.ToString());
                        ledgerSystem.LedgerProfileStateId = this.Member.NumberSet.ToInteger(ddlProfileState.SelectedValue.ToString());

                        ledgerSystem.IsDepriciationLedger = (chkDepreciationLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsAssetGainLedger = (chkAssetGainLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsAssetLossLedger = (chkAssetLossLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsInKindLedger = (chkInKindLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsAssetDisposalLedger = (chkDisposalLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;

                        ledgerSystem.IsFDPenaltyLedger = (chkFDPenaltyLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsSBInterestLedger = (chkBankSBInterestLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsBankCommissionLedger = (chkBankCommissionLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;

                        // On 25/11/2021, to set and define when ledger is closed---------------------------------
                        ledgerSystem.LedgerClosedOn = dteClosedOn.Date;
                        //----------------------------------------------------------------------------------------

                        resultArgs = ledgerSystem.SaveLedger(DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.SaveConformation;
                            if (LedgerId > 0)
                            {
                                // this is commanded by chinna on 22.10.2020
                                // Generalate Ledger should be mapped in the Generalate screen itself
                                //  MapLedgers();
                                AssignLedgerDetails();
                            }
                            else
                            {
                                // chkIsTDSApplicable.Visible = false;
                                // pnlIsTDSApplicable.Visible = false;
                                // LoadLedgerCode();
                                ClearValues();
                                pnlLedgerProfile.Visible = false;
                                LoadCountryToCombo();
                                SetDefaultCoutryState();
                            }
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    this.Message = Ex.Message;
                }
                finally
                {
                }
            }

        }

        private string GetLedgerType(int groupId)
        {
            string ledgerType = "";
            if (this.Member.NumberSet.ToInteger(ddlLedgerType.SelectedValue.ToString()) == (int)LedgerType.General)
                ledgerType = ledgerSubType.GN.ToString();
            else
                ledgerType = ledgerSubType.IK.ToString();
            return ledgerType;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
            SetPageTitle();
            LoadCountryToCombo();
            SetDefaultCoutryState();
        }

        protected void ddlGroup_TextChanged(object sender, EventArgs e)
        {
            ledgersystem.GroupId = this.Member.NumberSet.ToInteger(ddlGroup.SelectedValue.ToString());
            GroupID = ledgersystem.GroupId;
            if (LedgerGroupDetails != null && LedgerGroupDetails.Rows.Count > 0)
            {
                DataView dvLedgerGroupdetails = LedgerGroupDetails.DefaultView;
                dvLedgerGroupdetails.RowFilter = "GROUP_ID=" + ledgersystem.GroupId + "";
                if (dvLedgerGroupdetails != null && dvLedgerGroupdetails.Count > 0)
                {
                    NatureId = this.Member.NumberSet.ToInteger(dvLedgerGroupdetails.ToTable().Rows[0]["NATURE_ID"].ToString());
                    if (chkIsTDSApplicable.Checked)
                    {
                        if (GroupID.Equals(26))
                        {
                            ltrlNatureDeductee.Text = Deductee_Type;
                            FetchDeducteeTypes();
                        }
                        else
                        {
                            ltrlNatureDeductee.Text = Nature_of_Payments;
                            FetchDefaultNatureofPayments();
                        }

                    }
                    if (NatureId.Equals((int)Natures.Assert) || NatureId.Equals((int)Natures.Libilities))
                    {
                        pnlLedgerProfile.Visible = true;
                        LoadCountryToCombo();
                        SetDefaultCoutryState();
                    }
                    else
                    {
                        pnlLedgerProfile.Visible = false;
                    }
                    GroupLedgerOptionsByNature(NatureId);
                }
            }

            HideShowFDInvestment(GroupID);

            this.SetControlFocus(txtCode);
        }

        private void HideShowFDInvestment(int GroupId)
        {
            if (GroupID == CommonMember.FixedDepositGroup)
            {
                FDInvestments.Visible = true;
            }
            else
            {
                FDInvestments.Visible = false;
            }
        }

        //Group Ledger Options for Income and Expense
        private void GroupLedgerOptionsByNature(int NatureId)
        {
            if (NatureId.Equals((int)Natures.Income))
            {
                divExpenseNature.Visible = false;
                divIncomeNature.Visible = true;
            }
            if (NatureId.Equals((int)Natures.Expenses))
            {
                divExpenseNature.Visible = true;
                divIncomeNature.Visible = false;
            }
        }

        protected void ddlProfileCountry_TextChanged(object sender, EventArgs e)
        {
            LoadStateToCombo();
        }

        protected void chkIsTDSApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsTDSApplicable.Checked)
            {
                GroupID = this.Member.NumberSet.ToInteger(ddlGroup.SelectedValue.ToString());
                LedgerSystem ledgerSystem = new LedgerSystem();
                NatureId = ledgerSystem.FetchLedgerNatureByLedgerGroup(GroupID);
                //if (GroupID.Equals(24) || NatureId.Equals(2))
                ltrlNatureDeductee.Text = Nature_of_Payments;
                FetchDefaultNatureofPayments();
                if (GroupID.Equals(26))
                {
                    ltrlNatureDeductee.Text = Deductee_Type;
                    FetchDeducteeTypes();
                }
                pnlIsTDSApplicable.Visible = true;
                ddlNatureDeductee.Focus();
            }
            else
            {
                pnlIsTDSApplicable.Visible = false;
            }
        }

        protected void ddlBudgetGroup_TextChanged(object sender, EventArgs e)
        {
            int BudgetGroupId = this.Member.NumberSet.ToInteger(ddlBudgetGroup.SelectedValue.ToString());
            if (BudgetGroupId == 2)  // Non Recurring  is 2 
            {
                ddlBudgetSubGroup.SelectedValue = "0";
                ddlBudgetSubGroup.Enabled = false;
            }
            else
            {
                ddlBudgetSubGroup.Enabled = true;
            }
        }

        #endregion
    }
}