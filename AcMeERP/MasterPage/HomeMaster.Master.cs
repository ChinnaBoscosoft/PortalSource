using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcMeERP.Base;
using Bosco.Utility;
using System.Web.UI.HtmlControls;

namespace AcMeERP.MasterPage
{
    public partial class HomeMaster : Base.MasterBase
    {
        public ScriptManager MasterScriptManager
        {
            get { return scmMain; }
        }

        public override string SiteMenuProvider
        {
            set { dsMenuTop.SiteMapProvider = value; }
        }

        public override string PageTitle
        {
            get { return ltTitle.Text; }
            set { ltTitle.Text = value; }
        }


        public override string TimeFrom
        {
            set
            {
                lblTimeFrom.Text ="Execution Start Time :"+ value;
            }
        }

        public override string TimeTo
        {
            set
            {
                lblTimeTo.Text = "Execution End Time :" + value;
            }
        }

        public override string Message
        {
            get { return lblMsg.Text; }
            set
            {
                lblMsg.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    divmsg.Style.Add("visibility", "hidden");
                }
                else
                {
                    divmsg.Style.Add("visibility", "visible");
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Message = string.Empty;
            LoadScripts();
            if (!IsPostBack)
            {
                string userName = "";
                string headOfficeCode = "";

                if (this.ActivePage != null)
                {
                    userName = ActivePage.LoginUser.LoginUserName;
                    if (userName != "") { userName = "Welcome " + userName + "!"; }
                    lblUser.Text = userName;

                    headOfficeCode = this.ActivePage.HeadOfficeCode;
                    if (headOfficeCode == "") { headOfficeCode = URLPages.Portal.ToString(); }
                    if (headOfficeCode.Equals(URLPages.Portal.ToString()))
                    {
                        //hlkLogout.NavigateUrl = "~/account/" + headOfficeCode;
                        hlkLogout.NavigateUrl = "~/" + URLPages.Portal.ToString().ToLower();
                    }
                    else
                    {
                        hlkLogout.NavigateUrl = "~/" + headOfficeCode;
                    }

                    if (lblUser.Text == "")
                    {
                        hlkLogout.Text = "Login";
                        hlMyAcct.Visible = false;
                    }
                    else
                    {
                        hlkLogout.Text = "Logout";
                        hlMyAcct.Visible = true;
                    }
                }
                ltHeader.Text = string.IsNullOrEmpty(this.ActivePage.HeadOfficeCode) ? PortalTitle.PortalName : ActivePage.LoginUser.LoginUserHeadOfficeName;
            }
        }

        #region Methods
        private void LoadScripts()
        {
            HtmlGenericControl hgc = new HtmlGenericControl("script");

            hgc = new HtmlGenericControl("script");
            hgc.Attributes.Add("type", "text/javascript");
            hgc.Attributes.Add("src", ResolveClientUrl("~/Scripts/Validation.js"));
            hmhead.Controls.Add(hgc);

            hgc = new HtmlGenericControl("script");
            hgc.Attributes.Add("type", "text/javascript");
            hgc.Attributes.Add("src", ResolveClientUrl("~/Scripts/jquery.js"));
            hmhead.Controls.Add(hgc);

            hgc = new HtmlGenericControl("script");
            hgc.Attributes.Add("type", "text/javascript");
            hgc.Attributes.Add("src", ResolveClientUrl("~/Scripts/jquery-1.8.0.min.js"));
            hmhead.Controls.Add(hgc);
        }

        #endregion
    }
}
