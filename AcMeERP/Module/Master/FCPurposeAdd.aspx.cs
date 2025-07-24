/*********************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 10th Oct 2019
 *  
 * Modified by      : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose -----: This page helps head office admin/user or branch office admin/user to create the Purpose 
 **********************************************************************************************************/
using System;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Master
{
    public partial class FCPurposeAdd : Base.UIBase
    {
        #region Methods

        private int FCPurposeId
        {
            get
            {
                int fcPurposeId = this.Member.NumberSet.ToInteger(this.RowId);
                return fcPurposeId;
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
                this.CheckUserRights(RightsModule.Data, RightsActivity.FCPurposeAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                hlkClose.PostBackUrl = this.ReturnUrl;
                if (FCPurposeId != 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                this.SetControlFocus(txtPurpose);
                this.ShowLoadWaitPopUp(btnSaveFCPurpose);
            }
        }

        protected void btnSaveFCPurpose_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidFCPurposeDetails())
                {
                    ResultArgs resultArgs = null;
                    using (PurposeSystem purposesystem = new PurposeSystem())
                    {
                        purposesystem.PurposeId = FCPurposeId == 0 ? (int)AddNewRow.NewRow : FCPurposeId;
                        purposesystem.purposeCode = txtPurposeCode.Text.Trim();
                        purposesystem.PurposeHead = txtPurpose.Text.Trim();
                        resultArgs = purposesystem.SavePurposeDetails(DataBaseType.HeadOffice);
                        //  resultArgs = projectCategorySystem.SaveProjectCatogoryDetails(DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.FCPurposeSaved;
                            if (FCPurposeId == 0)
                            {
                                FCPurposeId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                ClearValues();
                            }
                            //    AssignValuesToControls();
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
            finally { }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        #endregion

        #region Methods

        private void AssignValuesToControls()
        {
            using (PurposeSystem Purposesystem = new PurposeSystem(FCPurposeId, DataBaseType.HeadOffice))
            {
                txtPurposeCode.Text = Purposesystem.purposeCode;
                txtPurpose.Text = Purposesystem.PurposeHead;
            }
        }

        private bool ValidFCPurposeDetails()
        {
            bool isvalid = true;
            if (string.IsNullOrEmpty(txtPurposeCode.Text))
            {
                this.Message = MessageCatalog.Message.FCPurpose.FCPurposeCodeEmpty;
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtPurpose.Text))
            {
                this.Message = MessageCatalog.Message.FCPurpose.FCPurposeEmpty;
                isvalid = false;
            }
            return isvalid;
        }

        private void ClearValues()
        {
            FCPurposeId = 0;
            SetPageTitle();
            txtPurpose.Text = string.Empty;
            txtPurposeCode.Text = string.Empty;
            this.SetControlFocus(txtPurpose);
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? "Edit FC Purpose" : "Add FC Purpose"));
        }

        #endregion
    }
}