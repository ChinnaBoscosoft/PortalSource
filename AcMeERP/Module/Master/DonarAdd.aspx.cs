using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Master
{
    public partial class DonarAdd : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;

        #endregion

        private int DonorId
        {
            get
            {
                int DonorId = this.Member.NumberSet.ToInteger(this.RowId);
                return DonorId;
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
                SetPageTitle();
                 hlkClose.PostBackUrl = this.ReturnUrl;
                LoadCountry();
                this.ShowLoadWaitPopUp();
                this.SetControlFocus(txtName);
            }
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            int Type;
            if(rdType.Checked==true){Type=0;} else{Type=1;}
            using (DonorAuditorSystem DonorAuditorSystem = new DonorAuditorSystem())
            {
                DonorAuditorSystem.DonAudId = DonorId == 0 ? (int)AddNewRow.NewRow : DonorId;
                DonorAuditorSystem.Name = txtName.Text.Trim();
                DonorAuditorSystem.Type = Type;
                DonorAuditorSystem.CountryId = this.Member.NumberSet.ToInteger(ddlCountry.SelectedValue.ToString());
                DonorAuditorSystem.Place = txtCity.Text.Trim();
                DonorAuditorSystem.State = txtStateProvince.Text.Trim();
                DonorAuditorSystem.Address = txtAddress.Text.Trim();
                DonorAuditorSystem.Pincode = txtPinZipCode.Text.Trim();
                DonorAuditorSystem.PAN = txtPan.Text.Trim();
                DonorAuditorSystem.Phone = txtPhone.Text.Trim();
                DonorAuditorSystem.Fax = txtFax.Text.Trim();
                DonorAuditorSystem.Email = txtEmail.Text.Trim();
                DonorAuditorSystem.URL = txtURL.Text.Trim();
                DonorAuditorSystem.Notes = txtNotes.Text.Trim();
                resultArgs = DonorAuditorSystem.SaveDonorAuditor();
                if (resultArgs.Success)
                {
                    this.Message = MessageCatalog.Message.SaveConformation;
                    if (DonorId == 0)
                        DonorId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                }

            }
        }

        #endregion

        #region Methods

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ?MessageCatalog.Message.Donor.DonorEditPageTitle : MessageCatalog.Message.Donor.DonorAddPageTitle));
        }

        private void LoadCountry()
        {
            using (CountrySystem CountrySystem = new CountrySystem())
            {
                resultArgs = CountrySystem.FetchCountryListDetails();
                this.Member.ComboSet.BindDataCombo(ddlCountry, resultArgs.DataSource.Table, this.AppSchema.Country.COUNTRYColumn.ColumnName,
                    this.AppSchema.Country.COUNTRY_IDColumn.ColumnName, true, CommonMember.SELECT);
            }
        }

        #endregion
    }
}