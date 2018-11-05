using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Data;
using System.Net;
using CoinFlip.Main.Controllers;
using System.Web.Hosting;

namespace CoinFlip.Main
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer
            (
                new MigrateDatabaseToLatestVersion<AppDbContext, CustomDatabaseInitializer>()
            );

            HostingEnvironment.RegisterObject(new BackgroundUptimeServerTimer());
        }

        /*protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());


            ServicePointManager.DefaultConnectionLimit = 1000;
            ServicePointManager.UseNagleAlgorithm = false; //http://blogs.msdn.com/b/windowsazurestorage/archive/2010/06/25/nagle-s-algorithm-is-not-friendly-towards-small-requests.aspx
            ServicePointManager.Expect100Continue = false;

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
            json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            MvcHandler.DisableMvcResponseHeader = true;

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<AppDbContext>(null); // Used to disable EF db check
        }*/
    }
}
