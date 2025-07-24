using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Model.UIModel.Master;

namespace AcMeERP.Module.Master
{
    public partial class GeneralateLedgerMapping : Base.UIBase
    {

        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        static string CON_LEDGER_NAME = "CON_LEDGER_NAME";
        static string SELECT = "SELECT";
        private static object objLock = new object();

        #endregion

        #region Properties

        private int GeneralateLedgerId
        {
            set
            {
                ViewState["GeneralateLedgerId"] = value;
            }
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["GeneralateLedgerId"].ToString());
            }
        }

        private DataTable LedgerSource
        {
            set
            {
                ViewState["LedgerSource"] = value;
            }
            get
            {
                return (DataTable)ViewState["LedgerSource"];
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                this.PageTitle = MessageCatalog.Message.GeneralateLedger.GeneralateLedgerMappingTitle;
                LoadGeneralateLedgers();
                LoadLedger();
                // ShowLoadWaitPopUp(btnSaveOnTop);
                // ShowLoadWaitPopUp(btnSave);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MapLedgers();
        }

        protected void btnSaveOnTop_Click(object sender, EventArgs e)
        {
            MapLedgers();
        }

        protected void cmbGeneralateLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeneralateLedgerId = this.Member.NumberSet.ToInteger(cmbGeneralateLedger.SelectedItem.Value.ToString());
            LoadLedger();
        }

        protected void gvLedger_Load(object sender, EventArgs e)
        {
            gvLedger.DataSource = LedgerSource;
            gvLedger.DataBind();
        }

        #endregion

        #region Methods

        private void LoadLedger()
        {
            using (CongregationLedgerMappingSystem congregationSystem = new CongregationLedgerMappingSystem())
            {
                congregationSystem.GeneralateLedgerId = GeneralateLedgerId;
                resultArgs = congregationSystem.LoadLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataView dvLedger = new DataView(resultArgs.DataSource.Table);
                    dvLedger.Sort = "SELECT DESC";
                    LedgerSource = dvLedger.ToTable();
                    gvLedger.DataSource = LedgerSource;
                    gvLedger.DataBind();
                    SelectMappedLedgers();
                }
                else
                {
                    LedgerSource = null;
                    gvLedger.DataSource = null;
                    gvLedger.DataBind();
                    btnSave.Visible = false;
                    btnSaveOnTop.Visible = false;
                }
            }
        }

        private void SelectMappedLedgers()
        {
            string selectedLedgerCount = "Total Mapped Ledgers: ";
            int selectedLedgersCount = 0;
            gvLedger.Selection.UnselectAll();
            for (int i = 0; i < LedgerSource.Rows.Count; i++)
            {
                if (this.Member.NumberSet.ToInteger(LedgerSource.Rows[i][SELECT].ToString()) == 1)
                {
                    gvLedger.Selection.SelectRowByKey(LedgerSource.Rows[i]["LEDGER_ID"]);
                    selectedLedgersCount += 1;
                }
            }
            ltrlSelected.Text = selectedLedgerCount + selectedLedgersCount;
        }

        private void LoadGeneralateLedgers()
        {
            using (GeneralateSystem generalateSystem = new GeneralateSystem())
            {
                resultArgs = generalateSystem.FetchParentLedgers();// Loading Generalte ledgers which does not have any child ledgers to that.
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindCombo(cmbGeneralateLedger, resultArgs.DataSource.Table, CON_LEDGER_NAME, AppSchema.CongregationLedger.CON_LEDGER_IDColumn.ColumnName, false);
                    GeneralateLedgerId = this.Member.NumberSet.ToInteger(cmbGeneralateLedger.SelectedItem.Value.ToString());
                }
                else
                {
                    GeneralateLedgerId = 0;
                }
            }
        }

        private void MapLedgers()
        {
            try
            {
                lock (objLock)
                {
                    List<object> lLedgerId = new List<object>();
                    lLedgerId = gvLedger.GetSelectedFieldValues(AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    List<object> NatureId = new List<object>();
                    NatureId = gvLedger.GetSelectedFieldValues(AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName);
                    using (CongregationLedgerMappingSystem congregationSystem = new CongregationLedgerMappingSystem())
                    {
                        if (GeneralateLedgerId > 0)
                        {
                            //if (lLedgerId.Count > 0)
                            //{
                            //if (NatureId.Distinct().ToList().Count == 1 || NatureId.Distinct().ToList().Count == 0)
                            //{
                            // Checking mapping ledger nature and mapped ledger nature are same or not.
                            //int count = 0;
                            //int NatureID = 0;
                            //string natureId = string.Empty;
                            //foreach (object se in NatureId)
                            //{
                            //natureId += se.ToString();
                            //count++;
                            //if (count == 1)
                            //break;
                            //}
                            //NatureID = this.Member.NumberSet.ToInteger(natureId);
                            //resultArgs = congregationSystem.CheckingSameNature(GeneralateLedgerId);
                            //if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 0 || resultArgs.DataSource.Table.Rows.Count == 1)
                            //{
                            //int mappedNatureId = 0;
                            //if (resultArgs.DataSource.Table.Rows.Count == 1)
                            //{
                            //mappedNatureId = this.Member.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString());
                            //}

                            //if (NatureID == 0 || mappedNatureId == 0 || NatureID == mappedNatureId)
                            //{
                            congregationSystem.GeneralateLedgerId = GeneralateLedgerId;
                            resultArgs = congregationSystem.MapLedgers(lLedgerId, this.Member.NumberSet.ToInteger(cmbGeneralateLedger.SelectedItem.Value.ToString()));
                            if (resultArgs.Success)
                            {
                                this.Message = MessageCatalog.Message.SaveConformation;
                                LoadLedger();
                            }
                            else
                            {
                                this.Message = resultArgs.Message;
                            }
                            //}
                            //else
                            //{
                            //this.Message = MessageCatalog.Message.GeneralateLedger.TwoNaturesToDifferentSubLedgers;
                            //}
                            //}
                            //}
                            //else
                            //{
                            //this.Message = MessageCatalog.Message.GeneralateLedger.MappingTwoNaturesofLedgers;
                            //}
                            //}
                            //else
                            //{
                            //    this.Message = MessageCatalog.Message.GeneralateLedger.SelectedNone;
                            //}
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.GeneralateLedger.NoGeneralateLedger;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        #endregion
    }
}