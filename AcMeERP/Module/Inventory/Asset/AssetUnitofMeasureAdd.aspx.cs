using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model;

namespace AcMeERP.Module.Inventory.Asset
{
    public partial class AssetUnitofMeasureAdd : Base.UIBase
    {

        #region Methods

        private int UOMId
        {
            get
            {
                int uomId = this.Member.NumberSet.ToInteger(this.RowId);
                return uomId;
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
                this.CheckUserRights(RightsModule.Data, RightsActivity.UnitofMeasureAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                hlkClose.PostBackUrl = this.ReturnUrl;
                if (UOMId > 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                this.SetControlFocus(txtUoM);
                this.ShowLoadWaitPopUp(btnUoM);
            }
        }

        protected void btnUoM_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidUnitofMeasureDetails())
                {
                    ResultArgs resultArgs = null;
                    using (AssetUnitOfMeassureSystem uomsystem = new AssetUnitOfMeassureSystem())
                    {
                        uomsystem.UnitId = UOMId == 0 ? (int)AddNewRow.NewRow : UOMId;
                        uomsystem.SYMBOL = txtUoM.Text.Trim();
                        uomsystem.NAME = txtFormalName.Text.Trim();
                        resultArgs = uomsystem.SaveMeasureDetails();
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.Asset.AssetUoMSaved;
                            if (UOMId == 0)
                            {
                                UOMId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
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
            using (AssetUnitOfMeassureSystem uomSystem = new AssetUnitOfMeassureSystem(UOMId))
            {
                txtUoM.Text = uomSystem.SYMBOL;
                txtFormalName.Text = uomSystem.NAME;
            }
        }

        private bool ValidUnitofMeasureDetails()
        {
            bool isvalid = true;
            if (string.IsNullOrEmpty(txtUoM.Text))
            {
                this.Message = MessageCatalog.Message.Asset.AssetUnitofMeasureEmpty;
                isvalid = false;
            }
            return isvalid;
        }

        private void ClearValues()
        {
            UOMId = 0;
            SetPageTitle();
            txtUoM.Text = string.Empty;
            txtFormalName.Text = string.Empty;
            this.SetControlFocus(txtUoM);
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.Asset.AssetUnitofMeasureEdit : MessageCatalog.Message.Asset.AssetUnitofMeasureAdd));
        }

        #endregion

    }
}