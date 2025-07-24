using System;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;

namespace AcMeERP.Module.Master
{
    public partial class CountryView : Base.UIBase
    {

        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        private DataView CountryViewSource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";

        #endregion

        #region Methods

        private void SetCountryViewSource()
        {
            using (CountrySystem CountrySystem = new CountrySystem())
            {
                ResultArgs resultArgs = CountrySystem.FetchCountryDetails(DataBaseType.HeadOffice);

                if (resultArgs.Success)
                {
                    CountryViewSource = resultArgs.DataSource.Table.DefaultView;
                }
                else
                {
                    this.Message = resultArgs.Message;
                }

                this.rowIdColumn = CountrySystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn + "," + CountrySystem.AppSchema.Country.CURRENCYColumn.ColumnName;
            }
        }

        #endregion

        #region Events
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.Country.CountryViewPageTitle;
                this.CheckUserRights(RightsModule.Data, RightsActivity.CountryView, base.LoginUser.LoginUserHeadOfficeCode==string.Empty?DataBaseType.Portal : DataBaseType.HeadOffice);
                this.ShowLoadWaitPopUp();
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.CountryAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.CountryView;
            SetCountryViewSource();

            gvCountryView.RowCommand += new GridViewCommandEventHandler(gvCountryView_RowCommand);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.Country.AddCountryCaption, false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvCountryView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvCountryView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvCountryView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }
            else if (this.LoginUser.IsHeadOfficeUser)
            {
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.CountryAdd, true,
                          base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvCountryView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.CountryEdit, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvCountryView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                }
                if (this.CheckUserRights(RightsModule.Data, RightsActivity.CountryDelete, true,
                       base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice))
                {
                    gvCountryView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
                }
            }

            gvCountryView.HideColumn = this.hiddenColumn;
            gvCountryView.RowIdColumn = this.rowIdColumn;
            gvCountryView.DataSource = CountryViewSource;
        }

        protected void gvCountryView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int CountryId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (CountryId != 0)
                {
                    using (CountrySystem CountrySystem = new CountrySystem())
                    {
                        resultArgs = CountrySystem.DeleteCountryDetails(CountryId,DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.Country.CountryDeleteConformation;
                            SetCountryViewSource();
                            gvCountryView.BindGrid(CountryViewSource);
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

    }
}