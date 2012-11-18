using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Application;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure.HyperMedia.Linker;
using RestBucks.WebApi.ResourceProvider;

namespace RestBucks.WebApi.Modules
{
   public class ControllerResolver : IHttpControllerActivator
   {
      private readonly IContainer m_container;

      private static void RegisterRequestDependantResources(ContainerBuilder a_containerBuilder, HttpRequestMessage a_request)
      {
         a_containerBuilder.RegisterType<ResourceLinkProvider>().AsImplementedInterfaces();
         a_containerBuilder.RegisterType<DtoMapper>().AsImplementedInterfaces();
         a_containerBuilder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
         a_containerBuilder.RegisterInstance(new RouteLinker(a_request)).AsImplementedInterfaces();
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="T:System.Object"/> class.
      /// </summary>
      public ControllerResolver(IContainer a_container)
      {
         if (a_container == null)
         {
            throw new ArgumentNullException("a_container");
         }

         m_container = a_container;
      }

      #region Implementation of IHttpControllerActivator

      /// <summary>
      /// Creates an <see cref="T:System.Web.Http.Controllers.IHttpController"/> object.
      /// </summary>
      /// <returns>
      /// An <see cref="T:System.Web.Http.Controllers.IHttpController"/> object.
      /// </returns>
      /// <param name="a_request">The message request.</param><param name="a_controllerDescriptor">The HTTP controller descriptor.</param><param name="a_controllerType">The type of the controller.</param>
      public IHttpController Create(HttpRequestMessage a_request, HttpControllerDescriptor a_controllerDescriptor, Type a_controllerType)
      {
         var scope = m_container.BeginLifetimeScope(a_x => RegisterRequestDependantResources(a_x, a_request));
         var controller = (IHttpController)scope.ResolveOptional(a_controllerType);
         a_request.RegisterForDispose(scope);
         return controller;
      }

      #endregion
   }
}