using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using AcMeERP.WebControl;
using AcMeERP.Base;

namespace AcMeERP.Module.User
{
    public partial class ModuleView :Base.UIBase
    {
        #region Declaration
        private DataView ModuleViewSource = null;
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        private string targetPage = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.PageTitle = "Module View";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            targetPage = this.GetPageUrlByName(URLPages.ModuleAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.ModuleView;
            SetProjectCategoryViewSource();

            gvModuleView.RowCommand += new GridViewCommandEventHandler(gvProjCategory_RowCommand);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, "Add Project Category", false);
            linkUrl.ShowModelWindow = false;

            if (this.LoginUser.IsAdminUser)
            {
                gvModuleView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
                gvModuleView.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
                gvModuleView.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());
            }

            gvModuleView.HideColumn = this.hiddenColumn;
            gvModuleView.RowIdColumn = this.rowIdColumn;
            gvModuleView.DataSource = ModuleViewSource;
        }

        protected void gvProjCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int ModuleId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (ModuleId != 0)
                {
                    using (RightSystem ModuleSystem = new RightSystem())
                    {
                        resultArgs = ModuleSystem.DeleteModule(ModuleId);

                        if (resultArgs.Success)
                        {
                            this.Message = "Module Deleted";
                            SetProjectCategoryViewSource();
                            
                            gvModuleView.BindGrid(ModuleViewSource);
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.Delete_failure;
                        }
                    }
                }

            }
        }

        private void SetProjectCategoryViewSource()
        {
            using (RightSystem ModuleSystem = new RightSystem())
            {
                ResultArgs resultArgs = ModuleSystem.FetchModuleDetails();
                if (resultArgs.Success)
                    ModuleViewSource = resultArgs.DataSource.Table.DefaultView;
                else
                    this.Message = resultArgs.Message;
                this.rowIdColumn = ModuleSystem.AppSchema.Module.MODULE_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
    }
}