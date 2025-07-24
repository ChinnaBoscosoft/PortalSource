using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AcMeERP.Base
{
    public delegate void TaskEventHandler(object sender, TaskEventArgs e);
    #region Class for Event Argument

    public class TaskEventArgs : EventArgs
    {
        private DataTable dtTaskData = null;
        private bool success = true;

        public DataTable TaskData
        {
            get { return dtTaskData; }
            set { dtTaskData = value; }
        }

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }
    }

    #endregion
}
