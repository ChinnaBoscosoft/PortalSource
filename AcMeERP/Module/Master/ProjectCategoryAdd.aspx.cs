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
 * Purpose          :This page helps head office admin/user or branch office admin/user to create the project category 
 *****************************************************************************************************/
using System;

using System.Web.UI;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.UIModel.Master;

namespace AcMeERP.Module.Master
{
    public partial class ProjectCategoryAdd : Base.UIBase
    {
        #region Methods

        private int ProjectCategoryId
        {
            get
            {
                int ProjectCategoryId = this.Member.NumberSet.ToInteger(this.RowId);
                return ProjectCategoryId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }
        string IsHeadOfficeCode = "";
        ResultArgs resultArgs = null;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectCategoryAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                hlkClose.PostBackUrl = this.ReturnUrl;
                if (ProjectCategoryId != 0)
                {
                    AssignValuesToControls();
                    btnNew.Visible = false;
                }
                this.SetControlFocus(txtProjectCategory);
                this.ShowLoadWaitPopUp(btnSaveProjectCategory);
                LoadGeneralateGroup();

                // 15/11/2024
                LoadITRGroup();

                Hidevisible();
                if (HeadOfficeCode.ToUpper().Substring(0, 3) == "SDB" && HeadOfficeCode.ToUpper().Substring(0, 3) != "BOS")
                    IsHeadOfficeCode = this.HeadOfficeCode.ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideGeneralateCategory", "ShowHideGeneralateCategory('" + IsHeadOfficeCode + "');", true);
            }
        }
        private void Hidevisible()
        {
            if (HeadOfficeCode.ToUpper() == "SDBINM")
            {
                ItrGroupName.Visible = true;
                ddlITRGroup.Visible = true;
            }
            else
            {
                ItrGroupName.Visible = false;
                ddlITRGroup.Visible = false;
            }
        }
        protected void btnSaveProjectCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidProjectCategoryDetails())
                {
                    ResultArgs resultArgs = null;
                    using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                    {
                        projectCategorySystem.ProjectCatogoryId = ProjectCategoryId == 0 ? (int)AddNewRow.NewRow : ProjectCategoryId;
                        projectCategorySystem.ProjectCatogoryName = txtProjectCategory.Text.Trim();
                        if (HeadOfficeCode.ToUpper().Substring(0, 3) == "SDB" && HeadOfficeCode.ToUpper().Substring(0, 3) != "BOS")
                        {
                            projectCategorySystem.GeneralateCategoryId = this.Member.NumberSet.ToInteger(ddlGeneralateCategory.SelectedValue.ToString());
                            IsHeadOfficeCode = this.HeadOfficeCode.ToString();
                            projectCategorySystem.ITRGroupId = HeadOfficeCode.ToUpper() == "SDBINM" ? this.Member.NumberSet.ToInteger(ddlITRGroup.SelectedValue.ToString()) : 1;
                        }
                        else projectCategorySystem.GeneralateCategoryId = projectCategorySystem.ITRGroupId = 1;     // PRIMARY
                        resultArgs = projectCategorySystem.SaveProjectCatogoryDetails(DataBaseType.HeadOffice);

                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.ProjectCategorySaved;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideGeneralateCategory", "ShowHideGeneralateCategory('" + IsHeadOfficeCode + "');", true);
                            if (ProjectCategoryId == 0)
                            {
                                ProjectCategoryId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                projectCategorySystem.ProjectCatogoryId = ProjectCategoryId;

                                ClearValues();

                            }

                            if (projectCategorySystem.ProjectCatogoryId != 0)
                            {
                                projectCategorySystem.ProjectCatogoryId = ProjectCategoryId == 0 ? projectCategorySystem.ProjectCatogoryId : ProjectCategoryId;
                                resultArgs = projectCategorySystem.SaveUpdateProjectCategoryDetails(DataBaseType.HeadOffice);
                            }

                            //    AssignValuesToControls();
                        }
                        else
                        {
                            this.Message = resultArgs.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
            finally { }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        #endregion

        #region Methods

        private void AssignValuesToControls()
        {
            using (ProjectCatogorySystem ProjectCategorySystem = new ProjectCatogorySystem(ProjectCategoryId, DataBaseType.HeadOffice))
            {
                txtProjectCategory.Text = ProjectCategorySystem.ProjectCatogoryName;
                ddlGeneralateCategory.SelectedValue = ProjectCategorySystem.GeneralateCategoryId.ToString();
                ddlITRGroup.SelectedValue = ProjectCategorySystem.ITRGroupId.ToString();
            }
        }

        private bool ValidProjectCategoryDetails()
        {
            bool isvalid = true;
            if (string.IsNullOrEmpty(txtProjectCategory.Text))
            {
                this.Message = MessageCatalog.Message.ProjectCategory.ProjectCategoryFieldEmpty;
                isvalid = false;
            }
            else if (ddlGeneralateCategory.SelectedValue == "0")
            {
                this.Message = "Generalate Category is empty";
                isvalid = false;
                ddlGeneralateCategory.Focus();
            }
            //else if (ddlITRGroup.SelectedValue == "0")
            //{
            //    this.Message = "ITRGroup Category is empty";
            //    isvalid = false;
            //    ddlITRGroup.Focus();
            //}
            return isvalid;
        }

        private void ClearValues()
        {
            ProjectCategoryId = 0;
            SetPageTitle();
            txtProjectCategory.Text = string.Empty;
            this.SetControlFocus(txtProjectCategory);
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.ProjectCategory.ProjectCategoryEditPageTitle : MessageCatalog.Message.ProjectCategory.ProjectCategoryAddPageTitle));
        }

        private void LoadGeneralateGroup()
        {
            using (GeneralateSystem GeneralateSystem = new GeneralateSystem())
            {
                resultArgs = GeneralateSystem.FetchProjectCategoryDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindDataCombo(ddlGeneralateCategory, resultArgs.DataSource.Table, "PROJECT_CATOGORY_GROUP",
                        "PROJECT_CATOGORY_GROUP_ID", true, CommonMember.SELECT);
                    //  LedgerGroupDetails = resultArgs.DataSource.Table;

                    // ddlGeneralateCategory.SelectedIndex = 1;
                }
            }
        }

        private void LoadITRGroup()
        {
            using (ProjectCatogorySystem projectcategory = new ProjectCatogorySystem())
            {
                resultArgs = projectcategory.FetchITRGroupCategory(DataBaseType.HeadOffice);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.Member.ComboSet.BindDataCombo(ddlITRGroup, resultArgs.DataSource.Table, "PROJECT_CATOGORY_ITRGROUP",
                       "PROJECT_CATOGORY_ITRGROUP_ID", true, CommonMember.SELECT);
                }
            }
        }

        #endregion

        protected void ddlGeneralateCategory_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlITRGroup_TextChanged(object sender, EventArgs e)
        {

        }
    }
}