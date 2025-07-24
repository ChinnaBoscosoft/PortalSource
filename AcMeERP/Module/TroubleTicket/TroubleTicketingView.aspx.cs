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
 * Purpose          :This page helps all the users to post their tickets and view the tickets regarding acme.erp product
 *****************************************************************************************************/
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel;
using System.IO;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Model.UIModel.TroubleTicket;
using DevExpress.Web.ASPxUploadControl;

namespace AcMeERP.Module.TroubleTicket
{
    public partial class TroubleTicketingView : Base.UIBase
    {
        #region Properties
        ResultArgs resultArgs = null;

        private PagedDataSource page;
        private int PostedBy = 0;
        private int _ticket_id = 0;
        private int TicketId
        {
            get
            {
                _ticket_id = Member.NumberSet.ToInteger(ViewState["_ticket_id"].ToString());
                return _ticket_id;
            }
            set
            {
                ViewState["_ticket_id"] = value;
            }
        }
        private int replied_ticket_id = 0;
        private int RepliedTicketId
        {
            get
            {
                replied_ticket_id = Member.NumberSet.ToInteger(ViewState["replied_ticket_id"].ToString());
                return replied_ticket_id;
            }
            set
            {
                ViewState["replied_ticket_id"] = value;
            }
        }

        private bool refresh;
        private bool Refresh
        {
            get
            {
                return refresh;
            }
            set
            {
                ViewState["refresh"] = value;
            }
        }

        private int PageNumber
        {
            get
            {
                if (ViewState["pagenumber"] != null)
                {
                    return this.Member.NumberSet.ToInteger(ViewState["pagenumber"].ToString());
                }
                else
                    return 0;
            }
            set
            {
                ViewState["pagenumber"] = value;
            }

        }
        private string AttachmentFileName
        {
            get
            {
                if (ViewState["AttachmentFileName"] != null)
                {
                    return ViewState["AttachmentFileName"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["AttachmentFileName"] = value;
            }
        }

        private string HeadOfficeCode
        {
            get
            {
                return ViewState["HeadOfficeCode"].ToString();
            }
            set
            {
                ViewState["HeadOfficeCode"] = value;
            }
        }

        private int UserId
        {
            get
            {
                return Member.NumberSet.ToInteger(ViewState["UserId"].ToString());
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }



        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPriority();
                SetPageTitle();
                showTroubleTickets();
                SetControlFocus();
                ShowLoadWaitPopUp(btnPostTicket);
                ShowLoadWaitPopUp(btnSave);
                ShowLoadWaitPopUp(btnNew);
                RepliedTicketId = 0;
                if (this.LoginUser.IsPortalUser)
                {
                    divPostTicket.Visible = false;
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltMessage.Text = string.Empty;
            LoadPostTicket();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ResultArgs resultArgs = null;
                if (ValidControlData())
                {
                    using (TroubleTicketingSystem troubleTicketingSystem = new TroubleTicketingSystem())
                    {
                        troubleTicketingSystem.TicketId = TicketId == 0 ? (int)AddNewRow.NewRow : TicketId;
                        troubleTicketingSystem.HeadOfficeCode = base.LoginUser.LoginUserHeadOfficeCode;
                        troubleTicketingSystem.BranchOfficeCode = base.LoginUser.LoginUserBranchOfficeCode;
                        troubleTicketingSystem.Subject = txtSubject.Text.Trim();
                        troubleTicketingSystem.Description = txtDescription.Text.Trim();
                        troubleTicketingSystem.Priority = this.Member.NumberSet.ToInteger(ddlPriority.SelectedValue);
                        troubleTicketingSystem.PostedDate = DateTime.Now;
                        troubleTicketingSystem.CompletedDate = DateTime.Now;
                        //troubleTicketingSystem.AttachFileName = fupAttachFile.FileName;
                        troubleTicketingSystem.PostedBy = base.LoginUser.LoginUserId;
                        troubleTicketingSystem.UserName = base.LoginUser.LoginUserName;
                        troubleTicketingSystem.AttachFileName = spFileName.InnerHtml = hfFileName.Value;
                        troubleTicketingSystem.AttachFileNamePhysical = AttachmentFileName;
                        troubleTicketingSystem.RepliedTicketId = RepliedTicketId == 0 ? 0 : RepliedTicketId;
                        if (TicketId == 0 && RepliedTicketId == 0)
                            troubleTicketingSystem.Status = (int)TroubleTicketStatus.Posted;
                        resultArgs = troubleTicketingSystem.SaveTicketDetails(DataBaseType.Portal);
                        if (resultArgs.Success)
                        {
                            if (TicketId < 0)
                                SendMail(txtDescription.Text.Trim());
                            if (TicketId == 0 && RepliedTicketId == 0)
                            {
                                ClearValues();
                            }

                            //TicketId = Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            showTroubleTickets();
                            ltMessage.Text = MessageCatalog.Message.SaveConformation;

                        }
                        else
                        {
                            ltMessage.Text = resultArgs.Message;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(MessageCatalog.Message.TroubleTicket.TroubleTicketEdit, "TroubleTicketView.aspx", ex.Message, "0");
            }
            finally { }
        }
        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            ltMessage.Text = string.Empty;
            ImageButton imgButton = (ImageButton)sender;
            TicketId = Member.NumberSet.ToInteger(imgButton.CommandArgument.ToString());
            TroubleTicketingSystem troubleTicketSystem = new TroubleTicketingSystem(TicketId);
            if (base.LoginUser.LoginUserId.Equals(troubleTicketSystem.PostedBy))
            {
                if (TicketId > 0)
                {
                    ltrPageTitle.Text = MessageCatalog.Message.TroubleTicket.EditPageTitel;
                    AssignValuesToControls();
                    btnNew.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), MessageCatalog.Message.TroubleTicket.TroubleTicketEdit, "javascript:ShowDisplayPopUp()", true);
                }
            }
        }
        protected void btnReply_Click(object sender, EventArgs e)
        {
            try
            {
                ltPriority.Visible = false;
                ddlPriority.Visible = false;
                btnNew.Visible = false;
                ltMessage.Text = "";
                ImageButton btnReply = (ImageButton)sender;
                TicketId = Member.NumberSet.ToInteger(btnReply.CommandArgument.ToString());
                RepliedTicketId = Member.NumberSet.ToInteger(btnReply.CommandArgument.ToString());
                TroubleTicketingSystem troubleticketsystem = new TroubleTicketingSystem(RepliedTicketId);
                troubleticketsystem.RepliedTicketId = RepliedTicketId;
                if (RepliedTicketId > 0)
                {
                    ltrPageTitle.Text = MessageCatalog.Message.TroubleTicket.ReplyPageTitle;
                    AssignValuesToControlsToReply();
                    txtSubject.Text = MessageCatalog.Message.TroubleTicket.ReplyPrefix + txtSubject.Text;
                    txtSubject.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), MessageCatalog.Message.TroubleTicket.TroubleTicketEdit, "javascript:ShowDisplayPopUp()", true);
                }
            }
            catch (Exception ex)
            {
                string Exception = ex.ToString();
            }
        }
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgButton = (ImageButton)sender;
            TicketId = Member.NumberSet.ToInteger(imgButton.CommandArgument.ToString());
            TroubleTicketingSystem troubleTicketingSystem = new TroubleTicketingSystem(TicketId);
            if (base.LoginUser.LoginUserId.Equals(troubleTicketingSystem.PostedBy))
            {
                if (TicketId > 0)
                {
                    troubleTicketingSystem.DeleteTroubleTicket(TicketId, DataBaseType.Portal);
                    showTroubleTickets();
                    this.Message = MessageCatalog.Message.TicketDeleted;
                }
            }
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            PageNumber += 1;
            showTroubleTickets();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            PageNumber -= 1;
            showTroubleTickets();
        }
        protected void btnPostTicket_Click(object sender, EventArgs e)
        {
            TicketId = 0;
            RepliedTicketId = 0;
            ltMessage.Text = string.Empty;
            if (TicketId == 0)
            {
                LoadPostTicket();
            }
        }

        protected void dlTroubleTicketView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DataTable dtReply = new DataTable();
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                ImageButton btnReply = (ImageButton)e.Item.FindControl("btnReplytemp");
                Repeater rptReply = (Repeater)e.Item.FindControl("rptReplies");
                ImageButton btnComplete = (ImageButton)e.Item.FindControl("btnComplete");
                ImageButton btnInProgress = (ImageButton)e.Item.FindControl("btnInprogress");

                if (dlTroubleTicketView != null && dlTroubleTicketView.Items.Count < 1)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        Label lblNullText = e.Item.FindControl("lblNullText") as Label;
                        lblNullText.Visible = true;
                    }
                }

                if (dr != null)
                {

                    if (dr[AppSchema.User.USER_NAMEColumn.ColumnName].ToString() != base.LoginUser.LoginUserName)
                    {
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                    }
                    else
                    {

                        if (dr[AppSchema.TROUBLETICKET.STATUSColumn.ColumnName].ToString() == TroubleTicketStatus.Completed.ToString())
                        {
                            btnEdit.Visible = false;
                            btnDelete.Visible = false;
                            btnReply.Visible = false;
                        }
                        btnReply.Visible = false;

                    }
                    if (!base.LoginUser.IsPortalUser)
                    {
                        btnComplete.Visible = false;
                    }
                    TicketId = this.Member.NumberSet.ToInteger(dr[AppSchema.TROUBLETICKET.TICKET_IDColumn.ColumnName].ToString()) == 0 ? 0 : this.Member.NumberSet.ToInteger(dr[AppSchema.TROUBLETICKET.TICKET_IDColumn.ColumnName].ToString());
                    dtReply = FetchReply();
                    if (dtReply != null && dtReply.Rows.Count > 0)
                    {
                        rptReply.DataSource = dtReply;
                        rptReply.DataBind();
                    }
                }


            }
            catch (Exception ex)
            {

                this.Message = ex.ToString();
            }
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                int TicketId = this.Member.NumberSet.ToInteger(btn.CommandArgument.ToString());
                if (TicketId != 0)
                {
                    using (TroubleTicketingSystem TroubleTicketSystem = new TroubleTicketingSystem())
                    {
                        TroubleTicketSystem.Status = (int)TroubleTicketStatus.Completed;
                        TroubleTicketSystem.TicketId = TicketId;
                        resultArgs = TroubleTicketSystem.UpdateStatus();
                        if (!resultArgs.Success)
                        {
                            this.Message = string.Empty;
                        }

                        showTroubleTickets();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }

        protected void btnInprograss_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                int TicketId = this.Member.NumberSet.ToInteger(btn.CommandArgument.ToString());
                if (TicketId != 0)
                {
                    using (TroubleTicketingSystem TroubleTicketSystem = new TroubleTicketingSystem())
                    {
                        TroubleTicketSystem.Status = (int)TroubleTicketStatus.InPrograss;
                        TroubleTicketSystem.TicketId = TicketId;
                        resultArgs = TroubleTicketSystem.UpdateStatus();
                        if (!resultArgs.Success)
                        {
                            this.Message = "";
                        }
                        showTroubleTickets();

                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }

        protected void btnClarification_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                int TicketId = this.Member.NumberSet.ToInteger(btn.CommandArgument.ToString());
                if (TicketId != 0)
                {
                    using (TroubleTicketingSystem TroubleTicketSystem = new TroubleTicketingSystem())
                    {
                        TroubleTicketSystem.Status = (int)TroubleTicketStatus.Clarification;
                        TroubleTicketSystem.TicketId = TicketId;
                        resultArgs = TroubleTicketSystem.UpdateStatus();
                        if (!resultArgs.Success)
                        {

                        }
                        showTroubleTickets();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }

        }

        protected void dlTroubleTicketView_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                TicketId = this.Member.NumberSet.ToInteger(e.CommandArgument.ToString());
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
        }

        #endregion

        #region Methods

        public void LoadPostTicket()
        {
            TicketId = 0;
            SetControlFocus();
            SetPageTitle();
            txtDescription.Text = txtSubject.Text = string.Empty;
            ddlPriority.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), MessageCatalog.Message.TroubleTicket.AddPageTitle, "javascript:ShowDisplayPopUp()", true);
        }
        private void SetControlFocus()
        {
            SetControlFocus(txtSubject);
        }

        private void BindPriority()
        {
            this.Member.ComboSet.BindEnum2DropDownList(ddlPriority, typeof(TicketPriority));
        }

        private void SetPageTitle()
        {
            this.PageTitle = MessageCatalog.Message.TroubleTicket.TroubleTicketPageTitle;
            ltrPageTitle.Text = MessageCatalog.Message.TroubleTicket.PostTicket;
        }

        private void AssignValuesToControls()
        {
            ResultArgs resultarg;
            using (TroubleTicketingSystem troubleTicketingSystem = new TroubleTicketingSystem())
            {
                troubleTicketingSystem.TicketId = TicketId;
                resultarg = troubleTicketingSystem.FetchTicketDetailsById(DataBaseType.Portal);
            }
            if (resultarg.Success)
            {
                DataTable dt = resultarg.DataSource.Table;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtSubject.Text = dt.Rows[0][this.AppSchema.TROUBLETICKET.SUBJECTColumn.ColumnName].ToString();
                        txtDescription.Text = dt.Rows[0][this.AppSchema.TROUBLETICKET.DESCRIPTIONColumn.ColumnName].ToString();
                        ddlPriority.SelectedValue = dt.Rows[0][this.AppSchema.TROUBLETICKET.PRIORITYColumn.ColumnName].ToString();
                        hfFileName.Value = dt.Rows[0][this.AppSchema.TROUBLETICKET.ATTACH_FILE_NAMEColumn.ColumnName].ToString();
                        PostedBy = Member.NumberSet.ToInteger(dt.Rows[0][this.AppSchema.TROUBLETICKET.POSTED_BYColumn.ColumnName].ToString());
                    }
                }
            }
        }
        private void AssignValuesToControlsToReply()
        {
            ResultArgs resultarg;
            using (TroubleTicketingSystem troubleTicketingSystem = new TroubleTicketingSystem())
            {
                troubleTicketingSystem.TicketId = TicketId;
                resultarg = troubleTicketingSystem.FetchTicketDetailsById(DataBaseType.Portal);
            }
            TicketId = 0;
            if (resultarg.Success)
            {
                DataTable dt = resultarg.DataSource.Table;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtSubject.Text = dt.Rows[0][this.AppSchema.TROUBLETICKET.SUBJECTColumn.ColumnName].ToString();
                        txtDescription.Text = string.Empty;
                        ddlPriority.SelectedValue = dt.Rows[0][this.AppSchema.TROUBLETICKET.PRIORITYColumn.ColumnName].ToString();
                        hfFileName.Value = dt.Rows[0][this.AppSchema.TROUBLETICKET.ATTACH_FILE_NAMEColumn.ColumnName].ToString();
                        HeadOfficeCode = dt.Rows[0][this.AppSchema.TROUBLETICKET.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                        UserId = this.Member.NumberSet.ToInteger(dt.Rows[0][this.AppSchema.TROUBLETICKET.POSTED_BYColumn.ColumnName].ToString());

                    }
                }
            }
        }

        private string RenameFile(ASPxUploadControl fuUpControl)
        {
            string fileRename = string.Empty;
            fileRename = Path.GetFileNameWithoutExtension(fuUpControl.FileName) +
                DateTime.Now.ToString(DateFormatInfo.MySQLFormat.DateTimeLong).ToString()
                + Path.GetExtension(fuUpControl.FileName);
            return fileRename;
        }

        private void DeleteFiles(string filename)
        {
            string filePath = string.Empty;
            filePath = Server.MapPath(CommonMember.UPLOAD_PATH) + filename;
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        private bool ValidControlData()
        {
            bool Valid = true;
            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                ltMessage.Text = MessageCatalog.Message.TroubleTicket.SubjectisEmpty;
                txtSubject.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(txtDescription.Text))
            {
                ltMessage.Text = MessageCatalog.Message.TroubleTicket.DescriptionisEmpty;
                txtSubject.Focus();
                Valid = false;
            }
            return Valid;
        }


        private DataTable FetchReply()
        {
            DataTable dtReplySource = new DataTable();
            try
            {
                using (TroubleTicketingSystem troubleTicketSystem = new TroubleTicketingSystem())
                {
                    troubleTicketSystem.TicketId = TicketId;
                    resultArgs = troubleTicketSystem.FetchReplies();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dtReplySource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.ToString();
            }
            return dtReplySource;
        }

        private void showTroubleTickets()
        {
            DataTable dtTicket = null;
            using (TroubleTicketingSystem troubleTicketingSystem = new TroubleTicketingSystem())
            {

                int count = 0;
                resultArgs = troubleTicketingSystem.FetchAllTroubleTicket(DataBaseType.Portal);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTicket = resultArgs.DataSource.Table;
                    PagedDataSource PagedData = new PagedDataSource();
                    PagedData.DataSource = dtTicket.DefaultView;
                    count = dtTicket.Rows.Count;
                    PagedData.AllowPaging = true;
                    PagedData.PageSize = 4;

                    PagedData.CurrentPageIndex = PageNumber;
                    if (PageNumber == 0)
                    {
                        lnkPreviousUp.Visible = false;
                    }
                    else
                    {
                        lnkPreviousUp.Visible = true;
                    }
                    if (PagedData.PageCount == PageNumber + 1)
                    {
                        lnkNextUp.Visible = false;
                    }
                    else
                    {
                        lnkNextUp.Visible = true;
                    }
                    dlTroubleTicketView.DataSource = PagedData;
                    dlTroubleTicketView.DataBind();
                }
                else
                {
                    dtTicket = resultArgs.DataSource.Table;
                    PagedDataSource PagedData = new PagedDataSource();
                    PagedData.DataSource = dtTicket.DefaultView;
                    dlTroubleTicketView.DataSource = PagedData;
                    dlTroubleTicketView.DataBind();
                    lnkPreviousUp.Visible = false;
                    lnkNextUp.Visible = false;
                }
            }
        }

        private void ClearValues()
        {
            txtSubject.Text = txtDescription.Text = string.Empty;
        }

        private void SendMail(string Description)
        {
            try
            {
                string AdminEmailId = string.Empty;
                string Name = string.Empty;
                string Header = string.Empty;
                string MainContent = string.Empty;
                string content = string.Empty;

                using (UserSystem userSystem = new UserSystem())
                {

                    if (TicketId == 0 && RepliedTicketId == 0)
                    {
                        Name = MessageCatalog.Message.TroubleTicket.MailUser;
                        Header = MessageCatalog.Message.TroubleTicket.MailHeader + " " + base.LoginUser.HeadOfficeCode;
                        MainContent = Description;
                        content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);
                        AdminEmailId = userSystem.FetchAdminEmail();
                        resultArgs = AcMEDSync.Common.SendEmail(AdminEmailId, string.Empty, MessageCatalog.Message.TroubleTicket.MailSubject, content,false);
                    }
                    else if (RepliedTicketId > 0)
                    {
                        Name = HeadOfficeCode;
                        Header = MessageCatalog.Message.TroubleTicket.ReplyMailHeader + " " + base.LoginUser.LoginUserName;
                        MainContent = Description;
                        content = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);
                        base.HeadOfficeCode = HeadOfficeCode;
                        userSystem.UserId = UserId;
                        resultArgs = userSystem.FetchUserDetailsById(DataBaseType.HeadOffice);
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            string MailId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.User.EMAIL_IDColumn.ColumnName].ToString();
                            resultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(MailId), CommonMethod.RemoveFirstValue(MailId), MessageCatalog.Message.TroubleTicket.MailSubject, content,true);
                        }
                    }

                }
                new ErrorLog().WriteError(resultArgs.Message);
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        #endregion

    }
}