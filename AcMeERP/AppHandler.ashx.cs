﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcMeERP
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class AppHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}