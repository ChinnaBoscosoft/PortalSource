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
 * Purpose          :This page helps head office admin to create new projects for the head office that project has to be mapped with the concern branch office
 *****************************************************************************************************/
using System;


using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Master
{
    public partial class ProjectAdd : Base.UIBase
    {
        #region Declaration

        ResultArgs resultArgs = null;
        string temp = "01/01/0001";
        private int ProjectId
        {
            get
            {
                int ProjectId = this.Member.NumberSet.ToInteger(this.RowId);
                return ProjectId;
            }
            set
            {
                this.RowId = value.ToString();
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPageTitle();
                this.CheckUserRights(RightsModule.Data, RightsActivity.ProjectAdd, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                hlkClose.PostBackUrl = this.ReturnUrl;
                LoadProjectCategory();
               // LoadProjectCodes();
                LoadSocietyNames();
                LoadRole();
                this.SetControlFocus(txtProjectCode);
                if (ProjectId != 0)
                {
                    AssignValues();
                    btnNew.Visible = false;
                }
                this.ShowLoadWaitPopUp(btnSaveUser);
            }
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateProjectDetails())
                {
                    using (ProjectSystem projectSystem = new ProjectSystem())
                    {
                        projectSystem.ProjectId = ProjectId == 0 ? (int)AddNewRow.NewRow : ProjectId;
                        projectSystem.ProjectCode = txtProjectCode.Text.Trim().ToUpper();
                        projectSystem.SocietyId = this.Member.NumberSet.ToInteger(ddlSociety.SelectedValue.ToString());
                        projectSystem.ProjectName = txtProject.Text.Trim();
                        projectSystem.ProjectCategroyId = this.Member.NumberSet.ToInteger(ddlProjectCategory.SelectedValue.ToString());
                        projectSystem.Description = txtDescription.Text.Trim();
                        projectSystem.StartedOn = dteStartedOn.Date;
                        //if (!string.IsNullOrEmpty(txtClosedOn.DateTextBox.Text))
                        //{
                        //    projectSystem.Closed_On = this.Member.DateSet.ToDate(txtClosedOn.DateTimeValue.ToString(), false);
                        //}
                        projectSystem.Closed_On = dteClosedOn.Date;
                        projectSystem.DivisionId = this.Member.NumberSet.ToInteger(ddlDivision.SelectedValue.ToString());

                        projectSystem.Notes = txtNotes.Text.Trim();
                        resultArgs = projectSystem.SaveProject(DataBaseType.HeadOffice);
                        if (resultArgs.Success)
                        {
                            this.Message = MessageCatalog.Message.ProjectSaved;
                            if (ProjectId == 0)
                            {
                               // LoadProjectCodes();
                                //ProjectId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                ClearValues();
                            }
                        }
                        else
                            this.Message = resultArgs.Message;
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
            this.SetControlFocus(txtProjectCode);
            ProjectId = 0;
           // LoadProjectCodes();
            ClearValues();
            SetPageTitle();
        }
        #endregion

        #region Methods
        private void ClearValues()
        {
            dteClosedOn.Text = dteStartedOn.Text = txtDescription.Text = txtNotes.Text = txtProject.Text =txtProjectCode.Text= string.Empty;
            ddlDivision.SelectedValue = ddlProjectCategory.SelectedValue = ddlSociety.SelectedValue = "0";
            SetControlFocus(txtProjectCode);
        }
        private void AssignValues()
        {
            using (ProjectSystem ProjectSystem = new ProjectSystem(ProjectId, DataBaseType.HeadOffice))
            {
                txtProjectCode.Text = ProjectSystem.ProjectCode;
                ddlProjectCategory.SelectedValue = ((ProjectSystem.ProjectCategroyId > 0) ? ProjectSystem.ProjectCategroyId.ToString() : "0");
                txtProject.Text = ProjectSystem.ProjectName;
                txtDescription.Text = ProjectSystem.Description;
                dteStartedOn.Text = ((ProjectSystem.StartedOn != DateTime.MinValue) ? ProjectSystem.StartedOn.ToString("dd/MM/yyyy") : string.Empty);
                dteClosedOn.Text = ((ProjectSystem.Closed_On != DateTime.MinValue) ? ProjectSystem.Closed_On.ToString("dd/MM/yyyy") : string.Empty);
                ddlDivision.SelectedValue = ((ProjectSystem.DivisionId > 0) ? ProjectSystem.DivisionId.ToString() : "0");
                txtNotes.Text = ProjectSystem.Notes;
                ddlSociety.SelectedValue = ((ProjectSystem.SocietyId > 0) ? ProjectSystem.SocietyId.ToString() : "0");
            }
        }

        private void SetPageTitle()
        {
            this.PageTitle = ((this.HasRowId ? MessageCatalog.Message.Project.ProjectEditPageTitel : MessageCatalog.Message.Project.ProjectAddPageTitle));
        }

        private bool ValidateProjectDetails()
        {
            bool isProject = true;
            //if (string.IsNullOrEmpty(txtProjectCode.Text))
            //{
            //    this.Message = MessageCatalog.Message.Project.ProjectCodeFieldEmpty;
            //    txtProjectCode.Focus();
            //    isProject = false;
            //}
             if (string.IsNullOrEmpty(txtProject.Text))
            {
                this.Message = MessageCatalog.Message.Project.ProjectFiedlEmpty;
                txtProject.Focus();
                isProject = false;
            }
            else if (ddlProjectCategory.SelectedValue == "0")
            {
                this.Message = MessageCatalog.Message.Project.ProjectCategoryFieldEmpty;
                ddlProjectCategory.Focus();
                isProject = false;
            }
            else if (ddlDivision.SelectedValue == "0")
            {
                this.Message = MessageCatalog.Message.Project.ProjectDivisionFieldEmpty;
                ddlDivision.Focus();
                isProject = false;
            }
            else if (string.IsNullOrEmpty(dteStartedOn.Text))
            {
                this.Message = MessageCatalog.Message.ProjectStartDate;
                dteStartedOn.Focus();
                isProject = false;
            }
            else if (dteStartedOn.Text != string.Empty && dteClosedOn.Text != string.Empty)
            {
                if (this.Member.DateSet.ValidateFutureDate(this.Member.DateSet.ChangeDateFormat(dteStartedOn.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME)))
                {
                    if (!this.Member.DateSet.ValidateDate(this.Member.DateSet.ChangeDateFormat(dteStartedOn.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME), this.Member.DateSet.ChangeDateFormat(dteClosedOn.Text, CommonMember.DATEFORMAT, CommonMember.DATEFORMAT_TIME)))
                    {
                        this.Message = MessageCatalog.Message.CloseDate;
                        dteClosedOn.Text = string.Empty;
                        dteClosedOn.Focus();
                        isProject = false;
                    }
                }
                else
                {
                    this.Message = MessageCatalog.Message.StartDate;
                    dteStartedOn.Text = string.Empty;
                    dteStartedOn.Focus();
                    isProject = false;
                }
            }
            return isProject;
        }

        private void LoadProjectCategory()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchProjectCategroy(DataBaseType.HeadOffice);
                this.Member.ComboSet.BindDataCombo(ddlProjectCategory, resultArgs.DataSource.Table, this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName,
                    this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName, true, CommonMember.SELECT);
            }
        }
        private void LoadSocietyNames()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchSocietyNames();
                this.Member.ComboSet.BindDataCombo(ddlSociety, resultArgs.DataSource.Table, this.AppSchema.SOCIETY.SOCIETYNAMEColumn.ColumnName,
                    this.AppSchema.SOCIETY.CUSTOMERIDColumn.ColumnName, true, CommonMember.SELECT);
            }
        }

        private void LoadRole()
        {
            using (ProjectSystem ProjectSystem = new ProjectSystem())
            {
                resultArgs = ProjectSystem.FetchDivision(DataBaseType.HeadOffice);
                this.Member.ComboSet.BindDataCombo(ddlDivision, resultArgs.DataSource.Table, this.AppSchema.Division.DIVISIONColumn.ColumnName,
                                  this.AppSchema.Division.DIVISION_IDColumn.ColumnName, true, CommonMember.SELECT);
            }
        }

        //private void LoadProjectCodes()
        //{
        //    try
        //    {
        //        using (ProjectSystem projectSystem = new ProjectSystem())
        //        {
        //            resultArgs = projectSystem.FetchProjectCodes(DataBaseType.HeadOffice);
        //            if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
        //            {
        //                this.Member.ComboSet.BindDataList(lvUsedCodes, resultArgs.DataSource.Table, AppSchema.Project.PROJECT_CODEColumn.ColumnName, AppSchema.Project.PROJECT_CODEColumn.ColumnName);
        //                if (ProjectId == 0)
        //                {
        //                    lvUsedCodes.SelectedIndex = 0;
        //                    txtProjectCode.Text = Base.MasterBase.CodePredictor(lvUsedCodes.Text, resultArgs.DataSource.Table);
        //                }
        //            }
                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Message = ex.Message;
        //    }
        //}

        #endregion

    }
}