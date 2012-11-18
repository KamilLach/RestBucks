using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http.Services;
using Application;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure.HyperMedia.Linker;
using Infrastructure.Persistance.Modules;
using RestBucks.WebApi.Modules;
using RestBucks.WebApi.ResourceProvider;

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
         ContainerBuilder builder = new ContainerBuilder();
         //builder.RegisterAssemblyTypes(typeof (WebApiApplication).Assembly).Where(
         //   a_c => a_c.IsAssignableTo<IHttpController>());

         builder.RegisterModule<NHibernateModule>();
         IContainer container = builder.Build();
         //AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
         //GlobalConfiguration.Configuration.DependencyResolver = resolver;
         GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new ControllerResolver(container));
      }
   }
}