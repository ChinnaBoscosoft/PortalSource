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
 * Purpose          :This page helps the head office admin user to send message to single or multiple branch office
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;

namespace AcMeERP.Module.Office
{
    public partial class SendMessage : Base.UIBase
    {
        #region Properties
        ResultArgs resultArgs = null;
        ResultArgs mailResultArgs = null;
        private const string MAIL_ID = "MAIL_ID";
        private const string SELECT = "SELECT";
        private int SelectedBranchId = 0;
        private int MessageType = 0;
        private string BranchIdCollection { get; set; }

        private int MessageId
        {
            get
            {
                int messageid = this.Member.NumberSet.ToInteger(this.RowId);
                return messageid;
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
                hlkClose.PostBackUrl = this.ReturnUrl;
                LoadBranch();
                if (MessageId > 0)
                {
                    AssignValuesToControls();
                }
                this.SetControlFocus(txtSubject);
                this.ShowLoadWaitPopUp(btnSend);
            }
        }
        protected void gvBranch_Load(object sender, EventArgs e)
        {
            LoadBranch();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                if (ValidateFields())
                {
                    int mail = 0;
                    int broadcast = 0;
                    if (chkMail.Checked)
                    {
                        mail = 1;
                    }
                    if (chkBroadcast.Checked)
                    {
                        broadcast = 2;
                    }
                    MessageType = mail + broadcast;
                    if (MessageType.Equals((int)UserCommunication.Email) || MessageType.Equals((int)UserCommunication.Both))
                    {
                        List<object> BranchId = gvBranch.GetSelectedFieldValues(new string[] { "BRANCH_OFFICE_ID" });
                        if (BranchId.Count > 0)
                        {
                            var rowVal = BranchId.AsEnumerable();
                            string BranchIds = String.Join(",", (from id in rowVal
                                                                 select id));
                            resultArgs = SaveMessageDetails();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                string[] SelectedIds = BranchIds.Split(',');
                                foreach (string id in SelectedIds)
                                {
                                    resultArgs = branchOfficeSystem.FetchMailIdByBranchId(SelectedBranchId);// FetchMailId by BranchId from table branch_office
                                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                    {
                                        mailResultArgs = SendMailToBranch(resultArgs.DataSource.Table);// Sending Mail to Branches
                                        if (mailResultArgs != null && mailResultArgs.Success)
                                        {
                                            this.Message = MessageCatalog.Message.HeadOfficeMessage.MailSuccess;
                                        }
                                    }
                                }
                                if (MessageId == 0)
                                {
                                    ClearValues();
                                }
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.HeadOfficeMessage.BranchEmptyEmail;
                        }
                    }
                    else
                    {
                        List<object> BranchId = gvBranch.GetSelectedFieldValues(new string[] { "BRANCH_OFFICE_ID" });
                        if (BranchId.Count > 0)
                        {
                            resultArgs = SaveMessageDetails();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                this.Message = MessageCatalog.Message.HeadOfficeMessage.BroadCastSuccess;
                                if (MessageId == 0)
                                {
                                    ClearValues();
                                }
                            }
                        }
                        else
                        {
                            this.Message = MessageCatalog.Message.HeadOfficeMessage.BranchEmptyForBroadCast;
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods
        private void LoadBranch()
        {
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                resultArgs = branchOfficeSystem.FetchBranch();
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtBranch = resultArgs.DataSource.Table;
                    List<object> BranchId = gvBranch.GetSelectedFieldValues(new string[] { "BRANCH_OFFICE_ID" });
                    var rowVal = BranchId.AsEnumerable();
                    string BranchIds = String.Join(",", (from id in rowVal
                                                         select id));
                    string[] SelectedIds = BranchIds.Split(',');
                    for (int i = 0; i < SelectedIds.Count(); i++)
                    {
                        foreach (DataRow dr in dtBranch.Rows)
                        {
                            if (SelectedIds[i].Equals(dr[this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName].ToString()))
                            {
                                dr[SELECT] = 1;
                            }
                        }
                    }
                    DataView dvbranch = dtBranch.DefaultView;
                    dvbranch.Sort = "SELECT DESC";
                    dtBranch = dvbranch.ToTable();
                    gvBranch.DataSource = dtBranch;
                    gvBranch.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvBranch.Settings.VerticalScrollableHeight = 265;
                    gvBranch.Settings.ShowVerticalScrollBar = true;
                    gvBranch.DataBind();
                }
                else
                {
                    gvBranch.DataSource = resultArgs.DataSource.Table;
                    gvBranch.DataBind();
                }
            }
        }

        private bool ValidateFields()
        {
            bool IsSuccess = true;
            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                this.Message = MessageCatalog.Message.HeadOfficeMessage.SubjectEmpty;
                IsSuccess = false;
                txtSubject.Focus();
            }
            else if (string.IsNullOrEmpty(txtContent.Text))
            {
                this.Message = MessageCatalog.Message.HeadOfficeMessage.ContentEmpty;
                IsSuccess = false;
                txtContent.Focus();
            }
            else if (!chkBroadcast.Checked && !chkMail.Checked)
            {
                this.Message = MessageCatalog.Message.HeadOfficeMessage.MessageTypeEmpty;
                IsSuccess = false;
                chkMail.Focus();
            }
            return IsSuccess;
        }

        private void ClearValues()
        {
            txtSubject.Text = txtContent.Text = string.Empty;
            chkBroadcast.Checked = chkMail.Checked = false;
            LoadBranch();
        }

        private ResultArgs SaveMessageDetails()
        {
            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                branchOfficeSystem.Subject = txtSubject.Text.Trim().ToString();
                branchOfficeSystem.Content = txtContent.Text.Trim().ToString();
                branchOfficeSystem.MessageId = (MessageId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : MessageId;
                branchOfficeSystem.MessageType = MessageType;
                resultArgs = branchOfficeSystem.SaveMessageDetails(); // Save message details in table head_message
                if (resultArgs != null && resultArgs.Success)
                {
                    //Get Rowunique Id after saving in head_message table
                    branchOfficeSystem.MessageId = this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) > 0 ?
                        this.Member.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MessageId;

                    resultArgs = branchOfficeSystem.DeleteMailToBranches(MessageId); // Delete records from mapping table message_branch by MessageId
                    if (resultArgs!=null && resultArgs.Success)
                    {
                        List<object> BranchId = gvBranch.GetSelectedFieldValues(new string[] { "BRANCH_OFFICE_ID" });
                        if (BranchId.Count > 0)
                        {
                            var rowVal = BranchId.AsEnumerable();
                            string BranchIds = String.Join(",", (from id in rowVal
                                                                 select id));
                            string[] SelectedIds = BranchIds.Split(',');
                            foreach (string id in SelectedIds)
                            {
                                SelectedBranchId = this.Member.NumberSet.ToInteger(id.ToString());
                                branchOfficeSystem.BranchId = SelectedBranchId;
                                resultArgs = branchOfficeSystem.SaveBranchMessage(); // Save records in mapping table message_branch by MessageId 
                            }
                        }
                    }
                    else
                    {
                        this.Message = resultArgs.Message;
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs SendMailToBranch(DataTable dtMail)
        {
            DataTable dtBranch = dtMail;
            string MailId = dtBranch.Rows[0][MAIL_ID].ToString();
            string Name = "Branch Admin";
            string Header = " Message from Head Office(" + base.LoginUser.LoginUserHeadOfficeCode + ")";

            string MainContent = "<br/> <b>Subject :</b> " + txtSubject.Text.Trim() + "<br/>" +
                                "<b>Content :</b> " + txtContent.Text.Trim() + "<br/>";
            string message = CommonMethod.GetMailTemplate(Header, MainContent, Name, true);
            mailResultArgs = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(MailId),
                           CommonMethod.RemoveFirstValue(MailId), Header, message,false);
            return mailResultArgs;
        }

        private void AssignValuesToControls()
        {
            try
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem(MessageId))
                {
                    chkBroadcast.Checked = false;
                    chkMail.Checked = false;
                    txtSubject.Text = branchOfficeSystem.Subject;
                    txtContent.Text = branchOfficeSystem.Content;

                    if (branchOfficeSystem.MessageType == (int)UserCommunication.Both)
                    {
                        chkBroadcast.Checked = chkMail.Checked = true;
                    }
                    else
                    {
                        chkMail.Checked = (branchOfficeSystem.MessageType == (int)UserCommunication.Email) ? true : false;
                        chkBroadcast.Checked = branchOfficeSystem.MessageType == (int)UserCommunication.BroadCast ? true : false;
                    }

                    BranchIdCollection = branchOfficeSystem.BranchIdCollection;
                    DataTable dtBranch = gvBranch.DataSource as DataTable;
                    string[] SelectedIds = BranchIdCollection.Split(',');
                    for (int i = 0; i < SelectedIds.Count(); i++)
                    {
                        foreach (DataRow dr in dtBranch.Rows)
                        {
                            if (SelectedIds[i].Equals(dr[this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName].ToString()))
                            {
                                dr[SELECT] = 1;
                            }
                        }
                    }
                    //Rowfilter for descending order for selectable branch
                    DataView dvbranch = dtBranch.DefaultView;
                    dvbranch.Sort = "SELECT DESC";
                    dtBranch = dvbranch.ToTable();
                    gvBranch.DataSource = dtBranch;
                    gvBranch.DataBind();
                    //check the selected Branch
                    CheckSelectedBranches(dtBranch);
                }

            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }

        private void CheckSelectedBranches(DataTable dt)
        {
            try
            {
                gvBranch.Selection.UnselectAll();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (this.Member.NumberSet.ToInteger(dt.Rows[i][SELECT].ToString()) == 1)
                    {
                        gvBranch.Selection.SelectRowByKey(dt.Rows[i][this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
        #endregion
    }
}