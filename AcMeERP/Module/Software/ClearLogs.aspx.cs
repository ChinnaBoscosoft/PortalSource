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
 * Purpose          :This page helps the portal admin to clear the datasync log file and portal log file to improve the performance
 *****************************************************************************************************/
using System;
using System.IO;
using System.Web.UI.WebControls;

using Bosco.Model.UIModel.Software;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Software
{
    public partial class ClearLogs : Base.UIBase
    {

        # region Variables
        public string ClrVal = string.Empty;
        string[] status = { "1", "2", "3", "4" };
        bool islogdeleted = false;
        bool isDataSyncLogdeleted = false;
        #endregion

        # region Method

        private string ClearLogStatus()
        {            
            if (chblLogStatus.Items[0].Selected)
            {
                ClrVal += status[0] + ",";
            }
            if (chblLogStatus.Items[1].Selected)
            {
                ClrVal += status[1] + ",";
            }
            if (chblLogStatus.Items[2].Selected)
            {
                ClrVal += status[2] + ",";
            }
            if (chblLogStatus.Items[3].Selected)
            {
                ClrVal += status[3] + ",";
            }
            return ClrVal.TrimEnd(',');
        }

        private bool Deletelogfile()
        {
            string file = PagePath.ErrorLogFilePath;
            if (File.Exists(file))
            {
                File.Delete(file);
                islogdeleted = true;
            }
            return islogdeleted;
        }

        private bool IsValid()
        {
            bool IsValid = true;
            int selectedCount = 0;
            int SelectedDsyncCount = 0;
            foreach (ListItem item in chblLogStatus.Items)
            {
                if (item.Selected)
                {
                    selectedCount++;
                }
            }
            foreach (ListItem item in chkblClearLogFile.Items)
            {
                if (item.Selected)
                {
                    SelectedDsyncCount++;
                }
            }
            if (selectedCount == 0 && SelectedDsyncCount == 0)
            {
                btnClear.Enabled = false;
                IsValid = false;
            }
            else
            {
                btnClear.Enabled = true;
            }
            return IsValid;
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CheckUserRights(RightsModule.Tools, RightsActivity.ClearLogData, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                this.ShowLoadWaitPopUp(btnClear);

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    string statuslog = ClearLogStatus();
                    using (ClearLogSystem clearlogsystem = new ClearLogSystem())
                    {
                        foreach (string item in statuslog.Split(','))
                        {
                            clearlogsystem.DeleteLog(this.Member.NumberSet.ToInteger(item));
                        }
                        if (chkblClearLogFile.Items[0].Selected)
                        {
                            Deletelogfile();
                        }
                        this.Message = MessageCatalog.Message.ClearLog;
                        chblLogStatus.ClearSelection();
                        chkblClearLogFile.ClearSelection();
                    }
                }
                else
                {
                    this.Message = MessageCatalog.Message.NothingSelected;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            string navurl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
            Response.Redirect(navurl, false);
        }

        #endregion
    }
}