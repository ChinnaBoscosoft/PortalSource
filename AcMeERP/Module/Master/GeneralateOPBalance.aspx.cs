/*****************************************************************************************************
 * Created by       : Chinna 
 * Created On       : 17th Nov 2020
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Transaction;
using System.Data;
using System.Globalization;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using AcMEDSync.Model;
using Bosco.Report.ReportObject;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using AcMeERP.MasterPage;
using Bosco.Model.UIModel.Master;

namespace AcMeERP.Module.Master
{
    public partial class GeneralateOPBalance : Base.UIBase
    {
        #region Declartion
        //For Print
        ResultArgs resultArgs = null;
        #endregion

        #region Properties


        private string YearTo
        {
            set { ViewState["YEAR_TO"] = value; }
            get
            {
                string rtn = string.Empty;
                rtn = ViewState["YEAR_TO"] == null ? string.Empty : ViewState["YEAR_TO"].ToString();
                return rtn;
            }
        }

        private DataTable OpeningBalanceDetails
        {
            set { ViewState["OpeningBalance"] = value; }
            get { return (DataTable)ViewState["OpeningBalance"]; }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblYear.Text = string.Empty;
                ActiveAcccounting();
                LoadGeneralateOpeningBalance();
                ShowLoadWaitPopUp(btnSave);
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGeneralateOpeningBalance();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateGridValue();
            SaveBudget();
            this.Message = MessageCatalog.Message.SaveConformation;
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            UpdateGridValue();
            SaveBudget();
            LoadGrid();
        }

        private void SaveBudget()
        {
            try
            {
                using (GeneralateOpeningBalance OpeningSystem = new GeneralateOpeningBalance())
                {
                    OpeningSystem.dtOpeningBalance = OpeningBalanceDetails.DefaultView.ToTable();
                    OpeningSystem.dtYearFrom = YearTo;
                    resultArgs = OpeningSystem.SaveOpeningDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.Message = MessageCatalog.Message.SaveConformation;
                    }
                }
            }
            catch (Exception err)
            {
                this.Message = "Problem in saving Opening Balance (" + err.Message + ")";
            }
        }


        private void ActiveAcccounting()
        {
            DateTime dtYearFrom;
            DateTime dtYearTo;
            using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
            {

                resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtYearFrom = this.Member.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);

                    dtYearTo = this.Member.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                    YearTo = dtYearTo.AddYears(-1).ToShortDateString();
                    DateTime dt = dtYearFrom.AddDays(-1);
                    lblYear.Text = dt.ToString("dd-MM-yyyy"); //dtYearFrom.ToString("dd-MM-yyyy") + " to " + dtYearTo.ToString("dd-MM-yyyy");
                }
            }
        }
        protected void gvBudgetView_Load(object sender, EventArgs e)
        {
            gvGeneralateOpeningBalance.DataSource = OpeningBalanceDetails;
            gvGeneralateOpeningBalance.DataBind();
        }

        #endregion

        #region Methods
        private void LoadGeneralateOpeningBalance()
        {
            try
            {
                using (GeneralateOpeningBalance OpBalanceDetails = new GeneralateOpeningBalance())
                {
                    OpBalanceDetails.dtYearFrom = YearTo;
                    resultArgs = OpBalanceDetails.FetchGeneralateAssetLiability(DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                        OpeningBalanceDetails = null;
                        gvGeneralateOpeningBalance.DataSource = OpeningBalanceDetails = resultArgs.DataSource.Table;
                        gvGeneralateOpeningBalance.DataBind();
                        gvGeneralateOpeningBalance.ExpandAll();
                    }
                    else
                    {
                        OpeningBalanceDetails = resultArgs.DataSource.Table;
                        this.Message = "Problem in loading budget from portal (" + resultArgs.Message + ")";
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadGrid()
        {
            try
            {
                gvGeneralateOpeningBalance.DataSource = OpeningBalanceDetails.DefaultView.ToTable();
                gvGeneralateOpeningBalance.DataBind();
                gvGeneralateOpeningBalance.ExpandAll();
                OpeningBalanceDetails = OpeningBalanceDetails.DefaultView.ToTable();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        private void UpdateGridValue()
        {
            try
            {
                GridViewDataColumn colConLedger = gvGeneralateOpeningBalance.Columns["CON_LEDGER_ID"] as GridViewDataColumn;
                //GridViewDataColumn colSubLedger = gvGeneralateOpeningBalance.Columns["SUB_LEDGER_ID"] as GridViewDataColumn;
                //GridViewDataColumn colM1ApprovedAmount = gvGeneralateOpeningBalance.Columns["APPROVED_CURRENT_YR"] as GridViewDataColumn;
                GridViewDataColumn colGenOpeningBalanceAmt = gvGeneralateOpeningBalance.Columns["AMOUNT"] as GridViewDataColumn;
                // GridViewDataColumn colHONarration = gvGeneralateOpeningBalance.Columns["HO_NARRATION"] as GridViewDataColumn;
                decimal GenOpeningBalanceAmt = 0;
                Int32 ConLedgerId = 0;
                for (int i = 0; i < gvGeneralateOpeningBalance.VisibleRowCount; i++)
                {
                    object value = gvGeneralateOpeningBalance.GetRowValues(i, new String[] { "CON_LEDGER_ID" });
                    ConLedgerId = 0;
                    if (value != null)
                    {
                        ConLedgerId = this.Member.NumberSet.ToInteger(value.ToString());
                    }

                    //SubLedgerId = 0;
                    //value = gvGeneralateOpeningBalance.GetRowValues(i, new String[] { "SUB_LEDGER_ID" });
                    //if (value != null)
                    //{
                    //    SubLedgerId = this.Member.NumberSet.ToInteger(value.ToString());
                    //}

                    if (gvGeneralateOpeningBalance.FindRowCellTemplateControl(i, colGenOpeningBalanceAmt, "txtOPAmount") != null)
                    {
                        ASPxSpinEdit txtAmount = gvGeneralateOpeningBalance.FindRowCellTemplateControl(i, colGenOpeningBalanceAmt, "txtOPAmount") as ASPxSpinEdit;
                        GenOpeningBalanceAmt = base.LoginUser.NumberSet.ToDecimal(txtAmount.Text);
                    }

                    //if (gvGeneralateOpeningBalance.FindRowCellTemplateControl(i, colM2ApprovedAmount, "txtM2ApprovedAmount") != null)
                    //{
                    //    ASPxSpinEdit txtM2Approved = gvGeneralateOpeningBalance.FindRowCellTemplateControl(i, colM2ApprovedAmount, "txtM2ApprovedAmount") as ASPxSpinEdit;
                    //    M2ApprovedAmt = base.LoginUser.NumberSet.ToDecimal(txtM2Approved.Text);
                    //}

                    //if (gvGeneralateOpeningBalance.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") != null)
                    //{
                    //    ASPxTextBox txtHONarration = gvGeneralateOpeningBalance.FindRowCellTemplateControl(i, colHONarration, "txtSpEditNarration") as ASPxTextBox;
                    //    HONarration = txtHONarration.Text;
                    //}

                    if (ConLedgerId > 0)
                    {
                        if (OpeningBalanceDetails.DefaultView.Count != 0)
                        {
                            OpeningBalanceDetails.DefaultView[i]["AMOUNT"] = GenOpeningBalanceAmt;
                            OpeningBalanceDetails.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.Message = "Unable update in grid " + err.Message;
            }
            LoadGrid();
        }
        #endregion
    }
}