/*****************************************************************************************************
 * Created by       : Carmel Raj
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page helps head office admin to make the ledgers as subsidy for the subsidy report purpose
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Data;
using DevExpress.Web.ASPxGridView;

namespace AcMeERP.Module.Master
{
    public partial class MapSubsidyLedgers : Base.UIBase
    {
        #region Variables
        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs;
        private string LedgerIdCollection { get; set; }
        private const string SELECT = "SELECT";
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                this.PageTitle = MessageCatalog.Message.LedgerMapping.MapSubsidyLedger;
                LoadLedgerType();
                LoadLedgerList();
                this.SetControlFocus(cmbLedgerType);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveLedgerList();
        }

        protected void btnSaveLedger_Click(object sender, EventArgs e)
        {
            SaveLedgerList();
        }

        protected void cmbLedgerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLedgerList();
        }
       
        #endregion

        #region Methods

        private void SaveLedgerList()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    List<object> LedgerId = gvLedgerList.GetSelectedFieldValues(new string[] { "LEDGER_ID" });
                    mappingSystem.GeneralateLedgersMapping = LedgerId;
                    mappingSystem.SubsidyLedger = Member.NumberSet.ToInteger(cmbLedgerType.Value.ToString());
                    HeadOfficeReport test = (HeadOfficeReport)Enum.Parse(typeof(HeadOfficeReport), cmbLedgerType.Value.ToString());
                    mappingSystem.IsSubsidyLedger = ((HeadOfficeReport)Enum.Parse(typeof(HeadOfficeReport), cmbLedgerType.Value.ToString())).Equals(HeadOfficeReport.Subsidy);
                    resultArgs = mappingSystem.MapGeneralateLedgersMap();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.Message = "Mapped Successfully";
                        LoadLedgerType();
                        LoadLedgerList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadLedgerType()
        {
            HeadOfficeReport LederType = new HeadOfficeReport();
            DataView dvbudget = Member.EnumSet.GetEnumDataSource(LederType, Sorting.Ascending);
            if (dvbudget.Count > 0)
            {
                DataTable dtLedgerType = dvbudget.ToTable();
                this.Member.ComboSet.BindCombo(cmbLedgerType, dtLedgerType, "Name", "Id", false);
                cmbLedgerType.DataBind();
            }
        }

        private void CheckSelectedLedgers(DataTable dtLedger)
        {
            try
            {
                gvLedgerList.Selection.UnselectAll();
                for (int i = 0; i < dtLedger.Rows.Count; i++)
                {
                    if (this.Member.NumberSet.ToInteger(dtLedger.Rows[i][SELECT].ToString()) == 1)
                    {
                        gvLedgerList.Selection.SelectRowByKey(dtLedger.Rows[i][this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadLedgerList()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.SubsidyLedger = Member.NumberSet.ToInteger(cmbLedgerType.Value.ToString());
                    resultArgs = mappingSystem.LoadMappedLedgers();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        LedgerIdCollection = mappingSystem.LedgerIdCollection;
                        DataTable dtLedger = resultArgs.DataSource.Table;
                        string[] SelectedIds = LedgerIdCollection.Split(',');
                        for (int i = 0; i < SelectedIds.Count(); i++)
                        {
                            foreach (DataRow dr in dtLedger.Rows)
                            {
                                if (SelectedIds[i].Equals(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()))
                                {
                                    dr[SELECT] = 1;
                                }
                            }
                        }
                        //Rowfilter for descending order for selectable branch
                        DataView dvbranch = dtLedger.DefaultView;
                        dvbranch.Sort = "SELECT DESC";
                        dtLedger = dvbranch.ToTable();
                        gvLedgerList.DataSource = dtLedger;
                        gvLedgerList.DataBind();
                        //check the selected Branch
                        CheckSelectedLedgers(dtLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}