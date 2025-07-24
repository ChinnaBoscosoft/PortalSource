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
 * Purpose          :This page helps head office admin to view branch office multilocation to map the different projects by location wise.
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
using AcMeERP.Base;
using System.Data;

namespace AcMeERP.Module.Office
{
    public partial class BranchLocationView : Base.UIBase
    {
        #region Properties

        CommonMember UtilityMember = new CommonMember();
        ResultArgs resultArgs = null;
        private DataView locationViewSource = null;
        private string targetPage = "";
        private string rowIdColumn = "";
        private string hiddenColumn = "";
        #endregion

        #region Events
        protected void Page_Init(object sender, EventArgs e)
        {
            this.TimeFrom = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            targetPage = this.GetPageUrlByName(URLPages.BranchLocationAdd.ToString()) + "?" + QueryStringMemeber.ReturnUrl + "=" + (int)URLPages.BranchLocationView;
            BranchLocationViewSource();

            gvBranchLocation.RowCommand += new GridViewCommandEventHandler(gvBranchLocation_RowCommand);
            gvBranchLocation.RowDataBound+=new GridViewRowEventHandler(gvBranchLocation_RowDataBound);

            LinkUrlColumn linkUrl = new LinkUrlColumn(this.targetPage, MessageCatalog.Message.LinkUrlCaptionAll, false);
            linkUrl.ShowModelWindow = false;

            gvBranchLocation.SetTemplateColumn(ControlType.HyperLink, CommandMode.Add, this.rowIdColumn, linkUrl);
            gvBranchLocation.SetTemplateColumn(ControlType.HyperLink, CommandMode.Edit, this.rowIdColumn, "", linkUrl, "", CommandMode.Edit.ToString());
            gvBranchLocation.SetTemplateColumn(ControlType.ImageButton, CommandMode.Delete, this.rowIdColumn, "", null, "", CommandMode.Delete.ToString());

            gvBranchLocation.HideColumn = this.hiddenColumn;
            gvBranchLocation.RowIdColumn = this.rowIdColumn;
            gvBranchLocation.DataSource = locationViewSource;
       
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageTitle = MessageCatalog.Message.BranchLocation.BranchLocationView;
                this.ShowLoadWaitPopUp();
                this.ShowTimeFromTimeTo = true;
                this.TimeTo = this.UtilityMember.DateSet.GetCurrentLocalDateTime();
            }
        }

        protected void gvBranchLocation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            int roleId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());

            if (e.CommandName == CommandMode.Delete.ToString())
            {
                if (roleId > 0)
                {
                    using (BranchLocationSystem branchLocationSystem = new BranchLocationSystem())
                    {
                        resultArgs = branchLocationSystem.DeleteLocation(roleId);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.BranchLocationDeleted;
                            BranchLocationViewSource();
                            gvBranchLocation.BindGrid(locationViewSource);
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }

            }
        }

        protected void gvBranchLocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlkEdit = (HyperLink)e.Row.FindControl("hlkEdit");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                if (imgDelete != null && hlkEdit != null)
                {
                    if (e.Row.Cells[0].Text.Equals("Primary"))
                    {
                        hlkEdit.Enabled = false;
                        imgDelete.Enabled = false;
                        hlkEdit.ImageUrl = "~/App_Themes/MainTheme/images/GridEdit.png";
                        imgDelete.ImageUrl = "~/App_Themes/MainTheme/images/DeleteDisable.png";
                    }
                }

            }

        }
        
        #endregion

        #region Methods
        private void BranchLocationViewSource()
        {
            using (BranchLocationSystem branchLocationSystem = new BranchLocationSystem())
            {  
                resultArgs = branchLocationSystem.FetchBranchLocationAll(DataBaseType.HeadOffice);
                if (resultArgs != null && resultArgs.Success)
                {
                    locationViewSource = resultArgs.DataSource.Table.DefaultView;
                }
                this.rowIdColumn = this.AppSchema.BranchLocation.LOCATION_IDColumn.ColumnName;
                this.hiddenColumn = this.rowIdColumn;
            }
        }
        #endregion

    }
}