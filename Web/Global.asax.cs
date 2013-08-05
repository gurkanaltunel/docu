using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DocumentServices;
using DocumentServices.Installers;
using Web.Services;

namespace Web
{
    public class MvcApplication : HttpApplication
    {
        private static Bootstrapper _bootstrapper;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _bootstrapper = Bootstrapper.StartApplication(new ControllerInstaller(), new RepositoryInstaller())
                                        .InitializeDependencies()
                                        .InstallDatabase();
        }

        protected void Application_End()
        {
            _bootstrapper.Dispose();
        }
    }
}