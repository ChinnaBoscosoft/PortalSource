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
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;

namespace AcMeERP.Module.Master
{
    public partial class SDBGeneralateLedgerMapping : Base.UIBase
    {

        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        static string CON_LEDGER_NAME = "CON_LEDGER_NAME";
        static string SELECT = "SELECT";
        private int GroupedProjectCategoryLedgerId = 0;

        private const string CON_LEDGER_ID = "CON_LEDGER_ID";
        private const string COLGENERALATELEDGER = "colGeneralateLedger";
        private const string CMBGENERALATE = "CMBGENERALATE";

        private const string PROJECT_CATOGORY_GROUP_ID = "PROJECT_CATOGORY_GROUP_ID";
        private const string PROJECT_CATOGORY_GROUP = "PROJECT_CATOGORY_GROUP";
        private static object objLock = new object();

        #endregion

        #region Properties

        private string ProjectCategoryLedgerId
        {
            set
            {
                ViewState["ProjectCategoryLedgerId"] = value;
            }
            get
            {
                return ViewState["ProjectCategoryLedgerId"].ToString();
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

        private DataTable GenerlateLedgerSource
        {
            set
            {
                ViewState["GenerlateLedgerSource"] = value;
            }
            get
            {
                return (DataTable)ViewState["GenerlateLedgerSource"];
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
                BindGenerlateLedgers();
                LoadProjectCategoryLedgers();
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
        protected void cmbProjectCategoryLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupedProjectCategoryLedgerId = this.UtilityMember.NumberSet.ToInteger(cmbProjectCategoryLedger.SelectedItem.Value.ToString());
            ProjectCategoryLedgerId = FilterGroupedProjectCategory(GroupedProjectCategoryLedgerId);
            LoadLedger();
        }

        //protected void cmbGeneralateLedger_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GroupedProjectCategoryLedgerId = this.UtilityMember.NumberSet.ToInteger(cmbProjectCategoryLedger.SelectedItem.Value.ToString());
        //    ProjectCategoryLedgerId = FilterGroupedProjectCategory(GroupedProjectCategoryLedgerId);
        //    //ProjectCategoryLedgerId = cmbProjectCategoryLedger.SelectedItem.Value.ToString();
        //    LoadLedger();
        //}

        protected void gvLedger_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UpdateViewStateProjects();
            }

            gvGeneralateLedger.DataSource = LedgerSource;
            gvGeneralateLedger.DataBind();
        }

        #endregion

        #region Methods

        private void BindGenerlateLedgers()
        {
            DataTable dtGenerlateLedgers = new DataTable();
            dtGenerlateLedgers.Columns.Add(CON_LEDGER_ID, typeof(System.Int32));
            dtGenerlateLedgers.Columns.Add("CON_LEDGER_CODE", typeof(System.String));
            dtGenerlateLedgers.Columns.Add(CON_LEDGER_NAME, typeof(System.String));

            using (GeneralateSystem generalateSystem = new GeneralateSystem())
            {
                resultArgs = generalateSystem.FetchParentLedgers();
                GenerlateLedgerSource = dtGenerlateLedgers;
                if (resultArgs != null && resultArgs.Success)
                {
                    dtGenerlateLedgers = resultArgs.DataSource.Table;
                }

                if (dtGenerlateLedgers != null)
                {
                    DataRow drEmpty = dtGenerlateLedgers.NewRow();
                    drEmpty[CON_LEDGER_ID] = 0;
                    drEmpty["CON_LEDGER_CODE"] = "";
                    drEmpty[CON_LEDGER_NAME] = "-Select-";
                    dtGenerlateLedgers.Rows.InsertAt(drEmpty, 0);
                }

                GenerlateLedgerSource = dtGenerlateLedgers;
            }
        }


        private void LoadLedger()
        {
            using (CongregationLedgerMappingSystem congregationSystem = new CongregationLedgerMappingSystem())
            {
                congregationSystem.ProjectCatogoryLedgerId = ProjectCategoryLedgerId;
                congregationSystem.ProjectCategoryGroupedLedgerId = GroupedProjectCategoryLedgerId;
                resultArgs = congregationSystem.LoadProjectCategoryLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataView dvLedger = new DataView(resultArgs.DataSource.Table);
                    dvLedger.Sort = "SELECT ASC, NATURE_ID,LEDGER_CODE,LEDGER_NAME";
                    LedgerSource = dvLedger.ToTable();
                    gvGeneralateLedger.DataSource = LedgerSource;
                    gvGeneralateLedger.DataBind();
                    SelectMappedLedgers();
                }
                else
                {
                    LedgerSource = null;
                    gvGeneralateLedger.DataSource = null;
                    gvGeneralateLedger.DataBind();
                    btnSave.Visible = false;
                    btnSaveOnTop.Visible = false;
                }
            }
        }

        private void SelectMappedLedgers()
        {
            int selectedLedgersCount = 0;
            int AllLedgerCount = 0;
            if (LedgerSource != null && LedgerSource.Rows.Count > 0)
            {
                AllLedgerCount = LedgerSource.DefaultView.Count;

                LedgerSource.DefaultView.RowFilter = CON_LEDGER_ID + " > 0";
                selectedLedgersCount = LedgerSource.DefaultView.Count;
                LedgerSource.DefaultView.RowFilter = ""; ;
            }
            ltrlSelected.Text = "Total Mapped Ledgers: " + selectedLedgersCount + "/" + AllLedgerCount;
        }

        private void LoadProjectCategoryLedgers()
        {
            using (GeneralateSystem generalateSystem = new GeneralateSystem())
            {
                resultArgs = generalateSystem.FetchProjectCategoryDetails();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindCombo(cmbProjectCategoryLedger, resultArgs.DataSource.Table, PROJECT_CATOGORY_GROUP, PROJECT_CATOGORY_GROUP_ID, false);
                    GroupedProjectCategoryLedgerId = this.Member.NumberSet.ToInteger(cmbProjectCategoryLedger.SelectedItem.Value.ToString());
                    ProjectCategoryLedgerId = FilterGroupedProjectCategory(GroupedProjectCategoryLedgerId);

                    // generalateSystem.GrouppedProjectCategoryLedgerId = 
                    //   resultArgs = generalateSystem.FetchProjectCategoryByGroupedProjectCategory();
                    //  ProjectCategoryLedgerId = resultArgs.DataSource.Table.Rows[0]["PROJECT_CATOGORY_ID"].ToString();
                }
                else
                {
                    ProjectCategoryLedgerId = string.Empty;
                }
            }
        }

        private string FilterGroupedProjectCategory(int GroupedProjectCatogoryId)
        {
            string ProjectCatogoryIds = string.Empty;
            using (GeneralateSystem generalateSystem = new GeneralateSystem())
            {
                generalateSystem.GrouppedProjectCategoryLedgerId = GroupedProjectCategoryLedgerId = GroupedProjectCatogoryId;
                resultArgs = generalateSystem.FetchProjectCategoryByGroupedProjectCategory();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0) // Assign Project Category Id details and update the Ids details
                    ProjectCatogoryIds = resultArgs.DataSource.Table.Rows[0]["PROJECT_CATOGORY_ID"].ToString();
            }
            return ProjectCatogoryIds;
        }

        private void MapLedgers()
        {
            try
            {
                lock (objLock)
                {
                    GroupedProjectCategoryLedgerId = this.Member.NumberSet.ToInteger(cmbProjectCategoryLedger.SelectedItem.Value.ToString());
                    using (CongregationLedgerMappingSystem congregationSystem = new CongregationLedgerMappingSystem())
                    {
                        if (GroupedProjectCategoryLedgerId > 0)
                        {
                            DataTable dtMappedGenLedgers = gvGeneralateLedger.DataSource as DataTable;
                            congregationSystem.ProjectCategoryGroupedLedgerId = GroupedProjectCategoryLedgerId;
                            dtMappedGenLedgers.DefaultView.RowFilter = CON_LEDGER_ID + " > 0";
                            if (dtMappedGenLedgers.DefaultView.Count > 0)
                            {
                                DataTable dtMapped = dtMappedGenLedgers.DefaultView.ToTable();
                                resultArgs = congregationSystem.MapGroupedCatogorywithGeneralate(dtMapped, GroupedProjectCategoryLedgerId);
                                if (resultArgs.Success)
                                {
                                    this.Message = MessageCatalog.Message.SaveConformation;
                                    LoadLedger();
                                }
                                else
                                {
                                    this.Message = resultArgs.Message;
                                }
                            }
                            else
                            {
                                this.Message = "Map anyone of Ledger";
                            }
                            dtMappedGenLedgers.DefaultView.RowFilter = string.Empty;
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

        protected void ddl_Init(object sender, EventArgs e)
        {
            DropDownList ddlGeneralateLedger = sender as DropDownList;
            ddlGeneralateLedger.Width = 275;
            GenerlateLedgerSource.DefaultView.RowFilter = "";
            DataTable dtSource = GenerlateLedgerSource;
            if (LedgerSource != null && LedgerSource.Rows.Count > 0)
            {
                GridViewDataItemTemplateContainer container = ddlGeneralateLedger.NamingContainer as GridViewDataItemTemplateContainer;
                int activerow = container.VisibleIndex;
                int natureId = this.Member.NumberSet.ToInteger(LedgerSource.Rows[activerow]["NATURE_ID"].ToString());
                if (natureId == 1)
                {
                    GenerlateLedgerSource.DefaultView.RowFilter = "CON_LEDGER_CODE LIKE '%D%'";
                }
                else if (natureId == 2)
                {
                    GenerlateLedgerSource.DefaultView.RowFilter = "CON_LEDGER_CODE LIKE '%C%'";
                }
                else if (natureId == 3)
                {
                    GenerlateLedgerSource.DefaultView.RowFilter = "CON_LEDGER_CODE LIKE '%A%'";
                }
                else if (natureId == 4)
                {
                    GenerlateLedgerSource.DefaultView.RowFilter = "CON_LEDGER_CODE LIKE '%B%'";
                }
                dtSource = GenerlateLedgerSource.DefaultView.ToTable();
            }
            GenerlateLedgerSource.DefaultView.RowFilter = "";

            DataRow drEmpty = dtSource.NewRow();
            drEmpty[CON_LEDGER_ID] = 0;
            drEmpty["CON_LEDGER_CODE"] = "";
            drEmpty[CON_LEDGER_NAME] = "-Select-";
            dtSource.Rows.InsertAt(drEmpty, 0);

            this.Member.ComboSet.BindDataCombo(ddlGeneralateLedger, dtSource, CON_LEDGER_NAME, CON_LEDGER_ID);
            int activegenledgerid = 0;

            if (LedgerSource != null && LedgerSource.Rows.Count > 0)
            {
                DataTable dtMappedLedgerSournce = LedgerSource;
                if (!string.IsNullOrEmpty(gvGeneralateLedger.FilterExpression))
                {
                    string filter = gvGeneralateLedger.FilterExpression;
                    string filterValue = string.Empty;
                    CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                    string filterString = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(criteria);
                    LedgerSource.DefaultView.RowFilter = filterString;
                    dtMappedLedgerSournce = LedgerSource.DefaultView.ToTable();
                    LedgerSource.DefaultView.RowFilter = string.Empty;
                }

                GridViewDataItemTemplateContainer container = ddlGeneralateLedger.NamingContainer as GridViewDataItemTemplateContainer;
                int activerow = container.VisibleIndex;
                int ledgerid = this.Member.NumberSet.ToInteger(dtMappedLedgerSournce.Rows[activerow]["LEDGER_ID"].ToString());
                activegenledgerid = this.Member.NumberSet.ToInteger(dtMappedLedgerSournce.Rows[activerow][CON_LEDGER_ID].ToString());

                //LedgerSource.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid;
                //if (LedgerSource.DefaultView.Count > 0)
                //{
                //    activegenledgerid = this.Member.NumberSet.ToInteger(LedgerSource.DefaultView[0][CON_LEDGER_ID].ToString());
                //}
                LedgerSource.DefaultView.RowFilter = string.Empty;
            }

            if (ddlGeneralateLedger != null)
            {
                ddlGeneralateLedger.SelectedValue = "0";
                if (ddlGeneralateLedger.Items.FindByValue(activegenledgerid.ToString()) != null)
                {
                    ddlGeneralateLedger.SelectedValue = activegenledgerid.ToString();
                    ddlGeneralateLedger.Items.FindByValue(activegenledgerid.ToString()).Selected = true;
                }
            }
        }

        private void UpdateViewStateProjects()
        {
            if (gvGeneralateLedger.Columns[CON_LEDGER_ID].Visible)
            {
                if (gvGeneralateLedger.VisibleRowCount > 0)
                {
                    for (int i = 0; i < gvGeneralateLedger.VisibleRowCount; i++)
                    {
                        DropDownList ddlGeneralateLedger = ((DropDownList)gvGeneralateLedger.FindRowCellTemplateControl(i, gvGeneralateLedger.Columns["colGeneralateLedger"] as GridViewDataColumn, "ddlGeneralateLedger"));
                        int GenLedgerid = 0;
                        if (ddlGeneralateLedger != null && ddlGeneralateLedger.SelectedValue != null)
                        {
                            GenLedgerid = this.Member.NumberSet.ToInteger(ddlGeneralateLedger.SelectedValue.ToString());
                        }

                        if (LedgerSource != null && LedgerSource.Rows.Count > 0)
                        {
                            LedgerSource.Rows[i][CON_LEDGER_ID] = GenLedgerid;
                            LedgerSource.AcceptChanges();
                        }
                    }
                }
            }
        }
        #endregion


    }
}