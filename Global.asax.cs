using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Trirand.Web.UI.WebControls;

namespace TMID
{
    public class Global : HttpApplication
    {
        protected void Session_Start(object sender, EventArgs e)
        {
        }
        protected void Session_End(object sender, EventArgs e)
        {
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public static class GlobalData
    {
        // Global variables
        static string _cItem;
        static string _canVote;

        // Get or set the static important data.
        public static string cItem
        {
            get
            {
                return _cItem;
            }
            set
            {
                _cItem = value;
            }
        }

    }
}