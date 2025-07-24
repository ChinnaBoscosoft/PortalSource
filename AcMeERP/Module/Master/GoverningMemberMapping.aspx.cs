using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Collections.Generic;

namespace AcMeERP.Module.Master
{
    public partial class GoverningMemberMapping : Base.UIBase
    {
        #region Properties
        ResultArgs resultArg = null;
        static string SOCIETYNAME = "Society Name";
        static string SELECT = "SELECT";
        static string EXECUTIVE_ID = "EXECUTIVE_ID";
        private static object objLock = new object();

        private int LegalEntityId
        {
            set
            {
                ViewState["LegalEntityId"] = value;
            }
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["LegalEntityId"].ToString());
            }
        }
        private DataTable GoverningMemberSource
        {
            set
            {
                ViewState["GoverningMemberSource"] = value;
            }
            get
            {
                return (DataTable)ViewState["GoverningMemberSource"];
            }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.GoverningMember.MapGoverningMember;
                LoadLegalEntity();
                LoadGoverningMember();
                this.ShowLoadWaitPopUp(btnSave);
                this.ShowLoadWaitPopUp(btnSaveOnTop);
            }
        }
        protected void btnSaveOnTop_Click(object sender, EventArgs e)
        {
            MapLegalEntityToGoverningMember();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MapLegalEntityToGoverningMember();
        }
        protected void gvGoverningMember_Load(object sender, EventArgs e)
        {
            gvGoverningMember.DataSource = GoverningMemberSource;
            gvGoverningMember.DataBind();
        }

        protected void cmbLegalEntity_IndexChanged(object sender, EventArgs e)
        {
            LegalEntityId = this.Member.NumberSet.ToInteger(cmbLegalEntity.SelectedItem.Value.ToString());
            LoadGoverningMember();
        }
        #endregion

        #region Methods
        private void MapLegalEntityToGoverningMember()
        {
            try
            {
                lock (objLock)
                {
                    List<object> lExecutiveId = new List<object>();
                    lExecutiveId = gvGoverningMember.GetSelectedFieldValues(AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn.ColumnName);
                   if (gvGoverningMember.DataSource!=null)
                    {
                        using (ExecutiveMemberSystem executiveMemberSystem = new ExecutiveMemberSystem())
                        {
                            resultArg = executiveMemberSystem.MapGoverningMembers(lExecutiveId, this.Member.NumberSet.ToInteger(cmbLegalEntity.SelectedItem.Value.ToString()));
                            if (resultArg.Success)
                            {
                                this.Message = MessageCatalog.Message.SaveConformation;
                                LoadGoverningMember();
                            }
                            else
                            {
                                this.Message = resultArg.Message;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        private void LoadLegalEntity()
        {
            using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
            {
                resultArg = legalEntitySystem.FetchLegalEntity(DataBaseType.HeadOffice);
                if (resultArg.Success && resultArg.DataSource.Table.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindCombo(cmbLegalEntity, resultArg.DataSource.Table, SOCIETYNAME, AppSchema.LegalEntity.CUSTOMERIDColumn.ColumnName, false);
                    LegalEntityId = this.Member.NumberSet.ToInteger(cmbLegalEntity.SelectedItem.Value.ToString());
                }
                else
                {
                    LegalEntityId = 0;
                }

            }
        }

        private void LoadGoverningMember()
        {
            using (ExecutiveMemberSystem executiveSystem = new ExecutiveMemberSystem())
            {
                executiveSystem.LegalEntityId = LegalEntityId;
                resultArg = executiveSystem.FetchGoverningMembersByLegalEntity();
                if (resultArg.Success && resultArg.DataSource.Table.Rows.Count > 0)
                {
                    DataView dvGoverningMember = new DataView(resultArg.DataSource.Table);
                    dvGoverningMember.Sort = "SELECT DESC";
                    GoverningMemberSource = dvGoverningMember.ToTable();
                    gvGoverningMember.DataSource = GoverningMemberSource;
                    gvGoverningMember.DataBind();
                    SelectMappedLedgers();
                }
                else
                {
                    GoverningMemberSource = null;
                    gvGoverningMember.DataSource = null;
                    gvGoverningMember.DataBind();
                    btnSave.Visible = false;
                    btnSaveOnTop.Visible = false;
                }
            }
        }

        private void SelectMappedLedgers()
        {
            gvGoverningMember.Selection.UnselectAll();
            for (int i = 0; i < GoverningMemberSource.Rows.Count; i++)
            {
                if (this.Member.NumberSet.ToInteger(GoverningMemberSource.Rows[i][SELECT].ToString()) == 1)
                {
                    gvGoverningMember.Selection.SelectRowByKey(GoverningMemberSource.Rows[i]["EXECUTIVE_ID"]);
                }
            }
        }
        #endregion
    }
}