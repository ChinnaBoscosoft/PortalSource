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
 * Purpose          :This page helps the portal admin to write some queries with server database.
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.Transaction;

namespace AcMeERP.Module.Office
{
    public partial class ExcuteDatabaseQuery : Base.UIBase
    {
        #region Property
        ResultArgs resultArgs = new ResultArgs();
        string headOfficeCode = string.Empty;
        string HOCode
        {
            get
            {
                return headOfficeCode;
            }
            set
            {
                headOfficeCode = value;
            }

        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                LoadHeadOffice();
            }
        }
        protected void ddlHeadOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHeadOffice.SelectedIndex > 0)
            {
                HOCode = ddlHeadOffice.SelectedValue;
            }
        }
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUpdateQueryDetails())
                {
                    using (VoucherTransactionSystem transSystem = new VoucherTransactionSystem())
                    {
                        base.HeadOfficeCode = HOCode;
                        resultArgs = transSystem.ExecuteHeadOfficeUpdateQuery(txtQuery.Text);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.Message = "Query is executed";
                            txtQuery.Text = string.Empty;
                            this.SetFocus(txtQuery);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                        base.HeadOfficeCode = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSelectQueryDetails())
                {
                    using (VoucherTransactionSystem transSystem = new VoucherTransactionSystem())
                    {
                        base.HeadOfficeCode = HOCode;
                        resultArgs = transSystem.ExecuteHeadOfficeSelectQuery(txtSelectQuery.Text);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            gvFetchData.DataSource = resultArgs.DataSource.Table;
                            gvFetchData.DataBind();
                            this.Message = "Query is executed";
                            txtSelectQuery.Text = string.Empty;
                            this.SetFocus(txtSelectQuery);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                        base.HeadOfficeCode = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        protected void btnSelectClear_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Text = string.Empty;
        }
        protected void btnExecuteClear_Click(object sender, EventArgs e)
        {
            txtQuery.Text = string.Empty;
        }
        #endregion

        #region Methods
        private void SetPageTitle()
        {
            this.PageTitle = "Query";
        }
        private bool ValidateUpdateQueryDetails()
        {
            bool _isValid = true;
            if (ddlHeadOffice.SelectedValue == null || string.IsNullOrEmpty(ddlHeadOffice.SelectedValue.ToString()) || ddlHeadOffice.SelectedValue.ToString().Equals("0"))
            {
                this.Message = "Head office is required";
                this.SetControlFocus(ddlHeadOffice);
                _isValid = false;
            }
            else if (string.IsNullOrEmpty(txtQuery.Text))
            {
                this.Message = "Query is required";
                this.SetControlFocus(txtQuery);
                _isValid = false;
            }
            return _isValid;
        }
        private bool ValidateSelectQueryDetails()
        {
            bool _isValid = true;
            if (ddlHeadOffice.SelectedValue == null || string.IsNullOrEmpty(ddlHeadOffice.SelectedValue.ToString()) || ddlHeadOffice.SelectedValue.ToString().Equals("0"))
            {
                this.Message = "Head office is required";
                this.SetControlFocus(ddlHeadOffice);
                _isValid = false;
            }
            else if (string.IsNullOrEmpty(txtSelectQuery.Text))
            {
                this.Message = "Select query is required";
                this.SetControlFocus(txtSelectQuery);
                _isValid = false;
            }
            return _isValid;
        }
        private void LoadHeadOffice()
        {
            try
            {
                using (HeadOfficeSystem HeadOfficeSystem = new HeadOfficeSystem())
                {
                    resultArgs = HeadOfficeSystem.FetchHeadOffice();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.Member.ComboSet.BindDataCombo(ddlHeadOffice, resultArgs.DataSource.Table, this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName, AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName, true, CommonMember.SELECT);
                        ddlHeadOffice.SelectedIndex = 0;
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