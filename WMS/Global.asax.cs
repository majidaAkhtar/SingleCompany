using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WMS.CustomClass;

namespace WMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            // Initialize Session["FiltersModel"] -- Move to First Page
            Session["FiltersModel"] = SessionManager.Init();
            LoadSessionValues();
        }

        private void LoadSessionValues()
        {
            Session["ReportSession"] = new List<string>();
            List<string> list = Session["ReportSession"] as List<string>;
            list.Add(DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"));
            list.Add(DateTime.Today.ToString("yyyy-MM-dd"));
            list.Add("EmpView");
            Session["ReportSession"] = list;
        }
        protected void Session_End(object sender, EventArgs e)
        {
            Session["FiltersModel"] = null;
        }
        //void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    if (Request == null || Request.Cookies == null)
        //    {
        //        return;
        //    }
        //    if (Request.Cookies.Count < 10)
        //    {
        //        return;
        //    }
        //    for (int i = 0; i < Request.Cookies.Count; ++i)
        //    {
        //        if (StringComparer.OrdinalIgnoreCase.Equals(Request.Cookies[i].Name, System.Web.Security.FormsAuthentication.FormsCookieName))
        //        {
        //            continue;
        //        }
        //        if (!Request.Cookies[i].Name.EndsWith("_SKA", System.StringComparison.OrdinalIgnoreCase))
        //        {
        //            continue;
        //        }
        //        if (i > 10)
        //        {
        //            break;
        //        }

        //        System.Web.HttpCookie c = new System.Web.HttpCookie(Request.Cookies[i].Name);
        //        c.Expires = DateTime.Now.AddDays(-1);
        //        c.Path = "/";
        //        c.Secure = false;
        //        c.HttpOnly = true;
        //        Response.Cookies.Set(c);
        //    }
        //}

        protected void Session_End()
        {
            
        }
    }
}
