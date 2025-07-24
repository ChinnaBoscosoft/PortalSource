using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace AcMeERP
{
    public partial class Default : Base.UIBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.PageTitle = "Home";
                this.LoginUser.UserInfo = null;
                this.HeadOfficeCode = "";
                Master.FindControl("divinfo").Visible = false;
                Master.FindControl("divspace").Visible = true;
            }
            if (Request.QueryString["msg"] != null)
            {
                // lblmsg.Text = Request.QueryString["msg"].ToString().Equals("1")?MessageCatalog.Message.SessionExpiry + "...<br/><br/>":string.Empty;
                //   Response.Redirect("https://www.acmeerp.org/login.php?msg=1", true);
                //   Response.Redirect("https://www.acmeerp.org/login.php?msg=1", true);
                //   Response.Redirect("https://staging.acmeerp.org/login.html?msg=1", true); // Invalid User Name and Password
                Response.Redirect("https://acmeerp.org/login.html?msg=1", true); // Invalid User Name and Password
                //Session Expiry
            }
        }
    }
}
