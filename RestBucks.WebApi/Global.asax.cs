using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http.Services;
using Autofac;
using Autofac.Integration.WebApi;

namespace RestBucks.WebApi
{
   // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
   // visit http://go.microsoft.com/?LinkId=9394801

   public class WebApiApplication : HttpApplication
   {
      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();

         WebApiConfig.Register(GlobalConfiguration.Configuration);
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);
         var builder = new ContainerBuilder();
         builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
         var container = builder.Build();
         var resolver = new AutofacWebApiDependencyResolver(container);
         GlobalConfiguration.Configuration.DependencyResolver = resolver;
      }
   }
}