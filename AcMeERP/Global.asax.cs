using System;
using System.Web.Routing;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Web.SessionState;
namespace AcMeERP
{
    public class Global1 : System.Web.HttpApplication
    {
        protected void Application_PostAuthorizeRequest()
        {
            //for Web API request
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            RouteTable.Routes.MapPageRoute("portal", "portal", "~/Account/portal/Default.aspx", true);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            // Log the exception.

            HttpException httpException = exception as HttpException;

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            new ErrorLog().WriteError("Application error event" + exception.ToString());

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        routeData.Values.Add("action", "~/ErrorPage.aspx");
                        Response.Redirect("~/PageNotFound.aspx");
                        break;
                    case 500:
                        // Server error.
                        routeData.Values.Add("action", "HttpError500");
                        break;

                    // Here you can handle Views to other error codes.
                    // I choose a General error template  
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
            }


            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception);

            // clear the error on server.
            Server.ClearError();

            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void RegisterRoutes(RouteCollection routes)
        {
            DataTable dtHeadOffice = new DataTable();
            using (HeadOfficeSystem branchSystem = new HeadOfficeSystem())
            {
                ResultArgs resultArgs = new ResultArgs();
                resultArgs = branchSystem.IsDatabaseServerConnected();
                if (resultArgs!=null && resultArgs.Success)
                {
                    resultArgs = branchSystem.FetchActiveHeadOfficeDetails(DataBaseType.Portal);
                    dtHeadOffice = resultArgs.DataSource.Table;
                    if (dtHeadOffice.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtHeadOffice.Rows.Count; i++)
                        {
                            routes.MapPageRoute(dtHeadOffice.Rows[i]["HEAD_OFFICE_CODE"].ToString(), dtHeadOffice.Rows[i]["HEAD_OFFICE_CODE"].ToString(),
                                "~/Account/portal/Default.aspx", true);
                        }
                    }
                }
            }
        }

    }
}