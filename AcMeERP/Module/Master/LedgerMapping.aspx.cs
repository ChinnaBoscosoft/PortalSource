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
 * Purpose          :This page helps head office admin to map the available HO ledgers to the HO project category and those ledgers are sent as master data for each branch  office
 *                              when the user updates their master data. IS_BRANCH_OFFICE_LEDGER is removed while mapping and Cash Ledger is sent as slient default ledgers.
 *                              FD ledgers should be mapped to project category which is the group of 14.
 *****************************************************************************************************/
using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Collections.Generic;


namespace AcMeERP.Module.Master
{
    public partial class LedgerMapping : Base.UIBase
    {
        #region

        ResultArgs resultArg = null;
        CommonMember UtilityMember = new CommonMember();

        static string PROJECT_CATEGORY_NAME = "Name";
        static string SELECT = "SELECT";
        static string PROJECT_CATEGORY_ID = "PROJECT_CATEGORY_ID";
        private static object objLock = new object();

        #endregion

        #region Properties

        private int ProjectCategoryId
        {
            set
            {
                ViewState["ProjectCategoryId"] = value;
            }
            get
            {
                return this.Member.NumberSet.ToInteger(ViewState["ProjectCategoryId"].ToString());
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
                this.PageTitle = MessageCatalog.Message.LedgerMapping.LedgerMappingPageTitle;
                LoadProjectCategory();
                LoadLedger();
                ShowLoadWaitPopUp(btnSaveOnTop);
                ShowLoadWaitPopUp(btnSave);
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void cmbProjectCategory_cmbBranch(object sender, EventArgs e)
        {
            ProjectCategoryId = this.Member.NumberSet.ToInteger(cmbProjectCategory.SelectedItem.Value.ToString());
            LoadLedger();
        }

        protected void gvLedger_Load(object sender, EventArgs e)
        {
            gvLedger.DataSource = LedgerSource;
            gvLedger.DataBind();
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            MapLedgers();
        }

        protected void btnSaveOnTop_Click(object sender, EventArgs e)
        {

            MapLedgers();
        }

        /// <summary>
        /// Save Default Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDefaultLedgerMapping_Click(object sender, EventArgs e)
        {
            SaveDefaultLedger();
        }

        private void SaveDefaultLedger()
        {
            try
            {
                lock (objLock)
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        resultArg = ledgerSystem.MapDefaultLedgersforAllProjectCategory();
                        if (resultArg.Success)
                        {
                            this.Message = "Mapped Default Ledger for all the Project Category";
                        }
                        else
                        {
                            this.Message = resultArg.Message;
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

        #region Methods


        private void MapLedgers()
        {
            try
            {
                lock (objLock)
                {
                    List<object> lLedgerId = new List<object>();
                    lLedgerId = gvLedger.GetSelectedFieldValues(AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    using (ProjectSystem projectSystem = new ProjectSystem())
                    {
                        projectSystem.ProjectCategroyId = ProjectCategoryId;
                        if (projectSystem.GetProjectCategoryViseProjectCount() > 0)
                        {
                            //Fetch Branch Office Default Ledgers for the modules to disbles
                            using (LedgerSystem ledgerSystem = new LedgerSystem())
                            {
                                resultArg = ledgerSystem.FetchDefaultLedgers();
                                if (resultArg != null && resultArg.Success && resultArg.RowsAffected > 0)
                                {
                                    foreach (DataRow drRow in resultArg.DataSource.Table.Rows)
                                    {
                                        foreach (object SelectedRecords in lLedgerId)
                                        {
                                            if ((drRow["LEDGER_ID"].ToString() == SelectedRecords.ToString()))
                                            {
                                                int StrrValue = lLedgerId.IndexOf(SelectedRecords);
                                                if (StrrValue == -1)
                                                {
                                                    lLedgerId.Add(drRow[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            resultArg = projectSystem.MapLedgers(lLedgerId, this.Member.NumberSet.ToInteger(cmbProjectCategory.SelectedItem.Value.ToString()));
                            if (resultArg.Success)
                            {
                                this.Message = MessageCatalog.Message.SaveConformation;
                                LoadLedger();
                            }
                            else
                            {
                                this.Message = resultArg.Message;
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.LedgerMapping.DenyLedgerMapping;
                            resultArg = projectSystem.DeleteMappedLedger();
                            gvLedger.Selection.UnselectAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        private void LoadProjectCategory()
        {
            using (ProjectCatogorySystem ProjectCategorySystem = new ProjectCatogorySystem())
            {
                resultArg = ProjectCategorySystem.FetchProjectCatogoryDetails(DataBaseType.HeadOffice);
                if (resultArg != null && resultArg.Success && resultArg.DataSource.Table.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindCombo(cmbProjectCategory, resultArg.DataSource.Table, PROJECT_CATEGORY_NAME, AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName, false);
                    ProjectCategoryId = this.Member.NumberSet.ToInteger(cmbProjectCategory.SelectedItem.Value.ToString());
                }
                else
                {
                    ProjectCategoryId = 0;
                }
            }
        }

        private void LoadLedger()
        {
            using (LedgerSystem LedgerSystem = new LedgerSystem())
            {
                LedgerSystem.ProjectCategoryId = ProjectCategoryId;
                resultArg = LedgerSystem.FetchLedgersByProjectCategory();
                if (resultArg != null && resultArg.Success && resultArg.DataSource.Table.Rows.Count > 0)
                {
                    DataView dvLedger = new DataView(resultArg.DataSource.Table);
                    dvLedger.Sort = "SELECT DESC";
                    LedgerSource = dvLedger.ToTable();
                    gvLedger.DataSource = LedgerSource;
                    gvLedger.DataBind();
                    SelectMappedLedgers();
                    GetDefaultLedgers();
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

        private void GetDefaultLedgers()
        {
            string mappedDefaultmessage = "Default Mapped Ledgers for" + " " + cmbProjectCategory.SelectedItem.ToString() + " Category:";
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ledgersystem.ProjectCategoryId = ProjectCategoryId;
                resultArg = ledgersystem.FetchDefaultLedgerbyProjectCategoryDetails();
                if (resultArg != null && resultArg.Success && resultArg.DataSource.Table.Rows.Count > 0)
                {
                    string LedgerList = resultArg.DataSource.Table.Rows[0]["LEDGER_NAME"].ToString();

                    lblDefaultCaptions.Text = mappedDefaultmessage + " " + (string.IsNullOrEmpty(LedgerList) ? "0" : LedgerList.Split(',').Length.ToString());
                    lblDefaultLedger.Text = LedgerList;
                }
                else
                {
                    lblDefaultCaptions.Text = mappedDefaultmessage + " 0 ";
                    lblDefaultLedger.Text = string.Empty;
                }
            }
        }

        #endregion



    }
}