using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO.Data;
using Bosco.Utility.CommonMemberSet;
using System.Data;

namespace AcMeERP.Module.Inventory.Asset
{
    public partial class AssetItemAdd : Base.UIBase
    {
        #region Methods
        ResultArgs resultArgs = null;
        private int AssetItemId
        {
            get
            {
                int ItemId = this.Member.NumberSet.ToInteger(this.RowId);
                return ItemId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        private int AssetInsuranceApplicable
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["AssetInsuranceApplicable"].ToString());
            }
            set
            {
                ViewState["AssetInsuranceApplicable"] = value;
            }
        }
        private int AssetAMCApplicable
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["AssetAMCApplicable"].ToString());
            }
            set
            {
                ViewState["AssetAMCApplicable"] = value;
            }
        }
        private int AssetDepApplicable
        {
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["AssetDepApplicable"].ToString());
            }
            set
            {
                ViewState["AssetDepApplicable"] = value;
            }
        }



        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                this.CheckUserRights(RightsModule.Data, RightsActivity.AssetItemAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                hlkClose.PostBackUrl = this.ReturnUrl;
                LoadAssetClass();
                LoadAssetUnitOfMeasure();
                BindAssetIDGenerationMethod();
                if (AssetItemId > 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                this.SetControlFocus(ddlAssetClass);
                this.ShowLoadWaitPopUp(btnAssetItem);
            }
        }

        protected void btnAssetItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidAssetItemDetails())
                {
                    ResultArgs resultArgs = null;
                    using (AssetItemSystem itemsystem = new AssetItemSystem())
                    {
                        itemsystem.ItemId = AssetItemId == 0 ? (int)AddNewRow.NewRow : AssetItemId;
                        itemsystem.AssetClassId = this.Member.NumberSet.ToInteger(ddlAssetClass.SelectedValue.ToString());
                        itemsystem.Unit = this.Member.NumberSet.ToInteger(ddlUOM.SelectedValue.ToString());
                        itemsystem.Name = txtAssetItem.Text.Trim();
                        itemsystem.Suffix = txtSuffix.Text.Trim();
                        itemsystem.Prefix = txtPrefix.Text.Trim();
                        itemsystem.AssetItemMode = this.Member.NumberSet.ToInteger(ddlAssetIDGeneration.SelectedValue.ToString());
                        itemsystem.StartingNo = this.Member.NumberSet.ToInteger(txtStartingNo.Text.Trim());
                        itemsystem.RetentionYrs = this.Member.NumberSet.ToInteger(txtRetentionYear.Text.Trim());
                        itemsystem.DepreciationYrs = this.Member.NumberSet.ToInteger(txtDepreYear.Text.Trim());
                        itemsystem.InsuranceApplicable = AssetInsuranceApplicable > 0 ? AssetInsuranceApplicable : 0;
                        itemsystem.AMCApplicable = AssetAMCApplicable > 0 ? AssetAMCApplicable : 0;
                        itemsystem.DepreciatonApplicable = AssetDepApplicable > 0 ? AssetDepApplicable : 0;
                        resultArgs = itemsystem.SaveItemDetails();
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.Asset.AssetUoMSaved;
                            if (AssetItemId == 0)
                            {
                                AssetItemId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
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
            finally { }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }
        protected void chkAssetItemsApplicable_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AssetInsuranceApplicable = chkAssetItemsApplicable.Items[0].Selected ? 1 : 0;

            AssetAMCApplicable = chkAssetItemsApplicable.Items[1].Selected ? 1 : 0;

            AssetDepApplicable = chkAssetItemsApplicable.Items[2].Selected ? 1 : 0;
        }

        #endregion

        #region Methods

        private void AssignValuesToControls()
        {
            using (AssetItemSystem itemSystem = new AssetItemSystem(AssetItemId))
            {
                txtAssetItem.Text = itemSystem.Name;
                ddlAssetClass.SelectedValue = ((itemSystem.AssetClassId > 0) ? itemSystem.AssetClassId.ToString() : "0");
                txtDepreYear.Text = itemSystem.DepreciationYrs.ToString();
                txtRetentionYear.Text = itemSystem.RetentionYrs.ToString();
                txtStartingNo.Text = itemSystem.StartingNo.ToString();
                txtPrefix.Text = itemSystem.Prefix.ToString();
                txtSuffix.Text = itemSystem.Suffix.ToString();
                ddlUOM.SelectedValue = ((itemSystem.Unit > 0) ? itemSystem.Unit.ToString() : "0");
                ddlAssetIDGeneration.SelectedValue = ((itemSystem.AssetItemMode > 0) ? itemSystem.AssetItemMode.ToString() : "0");
                AssetInsuranceApplicable=itemSystem.InsuranceApplicable;
                AssetAMCApplicable=itemSystem.AMCApplicable;
                AssetDepApplicable=itemSystem.DepreciatonApplicable;
                chkAssetItemsApplicable.Items[0].Selected = AssetInsuranceApplicable > 0 ? true : false;
                chkAssetItemsApplicable.Items[1].Selected = AssetAMCApplicable > 0 ? true : false;
                chkAssetItemsApplicable.Items[2].Selected = AssetDepApplicable > 0 ? true : false;
            }
        }

        private bool ValidAssetItemDetails()
        {
            bool isvalid = true;
            if ((ddlAssetClass.SelectedValue == null))
            {
                this.Message = MessageCatalog.Message.Asset.AssetClassRequired;
                SetControlFocus(ddlAssetClass);
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtAssetItem.Text))
            {
                this.Message = MessageCatalog.Message.Asset.AssetItemRequired;
                SetControlFocus(txtAssetItem);
                isvalid = false;
            }
            else if (ddlAssetIDGeneration.SelectedValue == null)
            {
                this.Message = MessageCatalog.Message.Asset.AssetIDGenerationRequired;
                SetControlFocus(ddlAssetIDGeneration);
                isvalid = false;
            }
            else  if (ddlUOM.SelectedValue == null)
            {
                this.Message = MessageCatalog.Message.Asset.AssetUnitofMeasureEmpty;
                SetControlFocus(ddlUOM);
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtStartingNo.Text))
            {
                this.Message = MessageCatalog.Message.Asset.StratingNoRequired;
                SetControlFocus(txtStartingNo);
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtPrefix.Text))
            {
                this.Message = MessageCatalog.Message.Asset.PrefixRequired;
                SetControlFocus(txtPrefix);
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtRetentionYear.Text))
            {
                this.Message = MessageCatalog.Message.Asset.RetentionYearRequired;
                SetControlFocus(txtRetentionYear);
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(txtDepreYear.Text))
            {
                this.Message = MessageCatalog.Message.Asset.DepYearRequired;
                SetControlFocus(txtDepreYear);
                isvalid = false;
            }
            return isvalid;
        }

        private void ClearValues()
        {
            AssetItemId = 0;
            SetPageTitle();
            LoadAssetClass();
            LoadAssetUnitOfMeasure();
            BindAssetIDGenerationMethod();
            txtAssetItem.Text = txtStartingNo.Text = txtPrefix.Text = txtSuffix.Text = txtRetentionYear.Text = txtDepreYear.Text = string.Empty;
            this.SetControlFocus(ddlAssetClass);
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.Asset.AssetItemEdit : MessageCatalog.Message.Asset.AssetItemAdd));
        }

        private void LoadAssetUnitOfMeasure()
        {
            try
            {
                using (AssetUnitOfMeassureSystem assetUnitOfMeasureSystem = new AssetUnitOfMeassureSystem())
                {
                    resultArgs = assetUnitOfMeasureSystem.FetchMeasureDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlUOM, resultArgs.DataSource.Table, assetUnitOfMeasureSystem.AppSchema.AssetUnitofMeasure.SYMBOLColumn.ColumnName, assetUnitOfMeasureSystem.AppSchema.AssetUnitofMeasure.UOM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadAssetClass()
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchParentClassName();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlAssetClass, resultArgs.DataSource.Table, assetClassSystem.AppSchema.AssetClass.ASSET_CLASSColumn.ColumnName, assetClassSystem.AppSchema.AssetClass.ASSET_CLASS_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void BindAssetIDGenerationMethod()
        {
            try
            {
                TransactionVoucherMethod AssetMethod = new TransactionVoucherMethod();
                DataView dvAssetIdMethod = Member.EnumSet.GetEnumDataSource(AssetMethod, Sorting.Ascending);
                if (dvAssetIdMethod.Count > 0)
                {
                    DataTable dtAssetIdGeneration = dvAssetIdMethod.ToTable();
                    this.Member.ComboSet.BindDataCombo(ddlAssetIDGeneration, dvAssetIdMethod, "Name", "Id");
                    ddlAssetIDGeneration.DataBind();
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