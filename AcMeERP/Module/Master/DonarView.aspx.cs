using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using AcMeERP.Base;

namespace AcMeERP.Module.Master
{
    public partial class DonarView : Base.UIBase
    {

        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        private DataView DonorViewResource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Donor.DonorViewPageTitle;
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.DonorAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.DonorView;
            SetDonorViewSource();

            gvDonorView.RowCommand += new GridViewCommandEventHandler(gvDonorView_RowCommand);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.Donor.AddDonorCaption, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvDonorView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvDonorView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvDonorView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }

            gvDonorView.HideColumn = this.hiddenColumn;
            gvDonorView.RowIdColumn = this.rowIdColumn;
            gvDonorView.DataSource = DonorViewResource;
        }

        protected void gvDonorView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ResultArgs resultArgs = new ResultArgs();
            int DonorId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (DonorId != 0)
                {
                    using (DonorAuditorSystem DonorAuditorSystem = new DonorAuditorSystem())
                    {
                        resultArgs = DonorAuditorSystem.DeleteDonorAuditorDetails(DonorId);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.Donor.DonorDeleteConformation;
                            SetDonorViewSource();
                            gvDonorView.BindGrid(DonorViewResource);
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.Delete_failure;
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void SetDonorViewSource()
        {
            using (DonorAuditorSystem DonorAuditorSystem = new DonorAuditorSystem())
            {
                ResultArgs resultArgs = DonorAuditorSystem.FetchAuditorDetails();

                if (resultArgs.Success)
                {
                    DonorViewResource = resultArgs.DataSource.Table.DefaultView;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }

        #endregion

    }
}