
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
 * Purpose          :This page helps head office admin to create the branch office multilocation to map the different projects by location wise.
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace AcMeERP.Module.Office
{
    public partial class BranchLocation : Base.UIBase
    {

        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        #endregion

        #region Properties

        private int LocationId
        {
            get
            {
                int locationId = this.Member.NumberSet.ToInteger(this.RowId);
                return locationId;
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
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                LoadBranch();
                SetPageTitle();
                hlkClose.PostBackUrl = this.ReturnUrl;
                if (LocationId > 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                this.ShowLoadWaitPopUp(btnSaveBranchLocation);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void btnSaveBranchLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLocationDetails())
                {
                    using (BranchLocationSystem branchLocationSystem = new BranchLocationSystem())
                    {
                        branchLocationSystem.BranchId = this.Member.NumberSet.ToInteger(ddlBranch.SelectedValue.ToString());
                        branchLocationSystem.LocationId = (LocationId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : LocationId;
                        branchLocationSystem.LocationName = txtBranchLocation.Text.Trim();
                        resultArgs = branchLocationSystem.SaveBranchOfficeDetails(DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.BranchLocationSaved;
                            if (LocationId > 0)
                            {
                                AssignValuesToControls();
                            }
                            else
                            {
                                ClearValues();
                            }

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
            }

        }

        protected void btnNew_Click1(object sender, EventArgs e)
        {
            ClearValues();
        }

        #endregion

        #region Methods

        private bool ValidateLocationDetails()
        {
            bool IsLocationValid = true;
            
            if (ddlBranch.SelectedValue == null)
            {
                this.Message = "Branch is empty";
                IsLocationValid = false;
                ddlBranch.Focus();
            }
            else if (string.IsNullOrEmpty(txtBranchLocation.Text))
            {
                this.Message = "Location is empty";
                IsLocationValid = false;
                txtBranchLocation.Focus();
            }
            return IsLocationValid;
        }
        public void LoadBranch()
        {
            try
            {
                using (BranchOfficeSystem BranchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = BranchOfficeSystem.FetchBranch(base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlBranch, resultArgs.DataSource.Table, "CODE", this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, true, CommonMember.SELECT);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
        }

        private void ClearValues()
        {
            LoadBranch();
            txtBranchLocation.Text = string.Empty;
            LocationId = 0;
            ddlBranch.SelectedValue = "0";
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.BranchLocation.BaranchLocationEditPageTitle : MessageCatalog.Message.BranchLocation.BaranchLocationAddPageTitle));
        }

        private void AssignValuesToControls()
        {
            try
            {
                using (BranchLocationSystem branchLocationSystem = new BranchLocationSystem(LocationId, string.IsNullOrEmpty(base.LoginUser.LoginUserHeadOfficeCode) ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    //   cmbBranch.Value = branchLocationSystem.BranchId.ToString();
                    ddlBranch.SelectedValue = branchLocationSystem.BranchId.ToString();
                    txtBranchLocation.Text = branchLocationSystem.LocationName;
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