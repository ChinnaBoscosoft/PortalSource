using System;
using System.Web.UI.WebControls;

namespace AcMeERP.Module.User
{
    public partial class ChangePasswordSuccess : Base.UIBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Menu mnuBar = (Menu)Master.FindControl("mnuTop");
                mnuBar.Visible = true;
            }
        }
    }
}
