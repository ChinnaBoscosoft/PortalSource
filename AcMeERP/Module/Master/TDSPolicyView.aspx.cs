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
 * Purpose          :This page helps head office admin/user or branch office admin/user to view the TDS Policy
 *****************************************************************************************************/
using System;
using Bosco.Model.Transaction;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using System.IO;
using System.Web;

namespace AcMeERP.Module.Master
{
    public partial class TDSPolicyView : Base.UIBase
    {
        #region Declartion

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        DataView dvDeducteeType = null;
        DataTable ExportTDSPolicyView = null;

        #endregion

        #region Properties

        private int deducteetypeid = 0;
        private int DeducteeTypeId
        {
            get { return deducteetypeid; }
            set { deducteetypeid = value; }
        }

        private DataTable TaxPolicy
        {
            get { return (DataTable)ViewState[AppSchema.DutyTax.TableName]; }
            set { ViewState[AppSchema.DutyTax.TableName] = value; }
        }

        private DataTable DeducteeType
        {
            get { return (DataTable)ViewState["DeducteeType"]; }
            set { ViewState["DeducteeType"] = value; }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
                this.CheckUserRights(RightsModule.Data, RightsActivity.TDSPolicyView, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.PageTitle = MessageCatalog.Message.TDS.StatutoryCompliance;
                LoadDeducteeTypes();
                this.SetControlFocus(cmbDeducteeType);
              //  this.ShowLoadWaitPopUp();
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void cmbDeducteeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeducteeTypeId = this.Member.NumberSet.ToInteger(cmbDeducteeType.SelectedItem.Value.ToString());
            LoadStatus(DeducteeType.DefaultView);
            LoadTaxDetails();
        }

        protected void imgExport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                string fileName = "PolicyView" + DateTime.Now.Ticks.ToString();
                LoadDeducteeTypes();
                if (!ExportTDSPolicyView.Equals(null))
                {
                    CommonMethod.WriteExcelFile(ExportTDSPolicyView, fileName);
                    DownLoadFile(fileName);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        protected void gvTDSPolicy_Load(object sender, EventArgs e)
        {
            gvTDSPolicy.DataSource = TaxPolicy;
            gvTDSPolicy.DataBind();
        }

        #endregion

        #region Methods

        private void LoadDeducteeTypes()
        {
            try
            {
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    resultArgs = voucherTransactionSystem.FetchActiveDeductTypes();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        DeducteeType = resultArgs.DataSource.Table;
                        this.Member.ComboSet.BindCombo(cmbDeducteeType, resultArgs.DataSource.Table, "NAME", "DEDUCTEE_TYPE_ID", false);
                        DeducteeTypeId = this.Member.NumberSet.ToInteger(cmbDeducteeType.SelectedItem.Value.ToString());
                        LoadStatus(DeducteeType.DefaultView);
                        LoadTaxDetails();
                    }
                    else
                    {
                        this.Member.ComboSet.BindCombo(cmbDeducteeType, resultArgs.DataSource.Table, "NAME", "DEDUCTEE_TYPE_ID", false);
                        DeducteeTypeId = 0;
                    }
                    //DeducteeType.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void DownLoadFile(string fileName)
        {
            try
            {
                byte[] bytes;
                bytes = File.ReadAllBytes(PagePath.AppFilePath + fileName + ".xlsx");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/xlsx";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".xlsx");
                Response.BinaryWrite(bytes);
                Response.Flush();
                System.IO.File.Delete(PagePath.AppFilePath + fileName + ".xlsx");
                Response.End();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadTaxDetails()
        {
            try
            {
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    voucherTransactionSystem.DeducteeTypeId = DeducteeTypeId;
                    resultArgs = voucherTransactionSystem.FetchDeducteeTaxDetails();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        ExportTDSPolicyView = resultArgs.DataSource.Table;
                        gvTDSPolicy.DataSource = TaxPolicy = resultArgs.DataSource.Table;
                        gvTDSPolicy.DataBind();
                        gvTDSPolicy.GroupBy(gvTDSPolicy.Columns["colNatureOfpayments"]);
                        gvTDSPolicy.ExpandAll();
                    }
                    else
                    {
                        gvTDSPolicy.DataSource = TaxPolicy = resultArgs.DataSource.Table;
                        gvTDSPolicy.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void LoadStatus(DataView DeducteeType)
        {
            try
            {
                DeducteeType.RowFilter = "DEDUCTEE_TYPE_ID=" + DeducteeTypeId + "";
                if (DeducteeType != null && DeducteeType.Count > 0)
                {
                    lblResidentStatus.Text = DeducteeType[0]["RESIDENTIAL_STATUS"].ToString();
                    lblDeducteeStatus.Text = DeducteeType[0]["DEDUCTEE_TYPE"].ToString();
                    lblStatus.Text = DeducteeType[0]["STATUS"].ToString();
                }
                DeducteeType.RowFilter = "";
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            //dvDeducteeType.RowFilter = String.Format("{0}={1}", "DEDUCTEE_TYPE_ID", cmbDeducteeType.SelectedItem.ValueString);
        }

        #endregion

       
    }
}