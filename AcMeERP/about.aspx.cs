using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcMeERP
{
    public partial class about : Base.UIBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //bharath
            if (!IsPostBack)
            {
                this.PageTitle = "Overviews";
                this.LoginUser.UserInfo = null;
                this.HeadOfficeCode = "";
                Master.FindControl("divinfo").Visible = false;
                Master.FindControl("divspace").Visible = true;
            }

        }
    }
}