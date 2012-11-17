using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Infrastructure.HyperMedia.Linker.Interfaces;

namespace Infrastructure.HyperMedia.Linker
{
    /// <summary>
    ///     <para> This class parses URIs into a structured representation. The <see cref="HttpActionContext"/> class is used as said representation. </para>
    ///     <para> Additionally, this class is capable of verifying that a <see cref="HttpActionContext"/> matches a specific controller action. </para>
    /// </summary>
    /// <remarks>
    /// Example: <code>
    /// <![CDATA[
    /// HttpContextAction contextAction;
    /// if(linkParser.TryParseUri(uri, out contextAction) && linkParser.Verify<SomeController>(x => x.SomeAction(Arg<int>.Any)))
    /// {
    ///     var id = (int)contextAction.ActionArguments["id"];
    /// }
    /// ]]>
    /// </code>
    /// </remarks>
    public class ResourceLinkParser : IResourceLinkParser, IActionVerifier
    {
        private readonly IHttpActionSelector m_actionSelector;
        private readonly HttpConfiguration m_configuration;
        private readonly IHttpControllerSelector m_controllerSelector;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLinkParser"/> class.
        /// </summary>
        /// <param name="a_configuration">
        /// The configuration to use to parse the URIs.
        /// </param>
        public ResourceLinkParser(HttpConfiguration a_configuration)
        {
            if (a_configuration == null)
                throw new ArgumentNullException("a_configuration");

            m_configuration = a_configuration;
            m_actionSelector = m_configuration.Services.GetActionSelector();
            m_controllerSelector = m_configuration.Services.GetHttpControllerSelector();
        }

        /// <summary>
        /// Gets the configuration used to parse the URIs.
        /// </summary>
        public HttpConfiguration Configuration
        {
            get { return m_configuration; }
        }

        /// <summary>
        /// Parses the specified URI.
        /// </summary>
        /// <param name="a_uri">
        /// The URI to parse.
        /// </param>
        /// <returns>
        /// The <see cref="HttpActionContext"/> with bound parameter values.
        /// </returns>
        /// <exception cref="System.ArgumentException">The URI is invalid or no action matches the specified URI.</exception>
        public HttpActionContext Parse(Uri a_uri)
        {
            HttpActionContext result;
            if (TryParse(a_uri, out result))
                return result;

            throw new ArgumentException("The URI is invalid or no action matches the specified URI");
        }

        /// <summary>
        /// Tries to parse the specified URI.
        /// </summary>
        /// <param name="a_uri">
        /// The URI to parse.
        /// </param>
        /// <param name="a_actionContext">
        /// The <see cref="HttpActionContext"/> with bound parameter values.
        /// </param>
        /// <returns>
        ///     <c>true</c> in case the URI could be parsed successfully and matched to an action; otherwise, <c>false</c> .
        /// </returns>
        public bool TryParse(Uri a_uri, out HttpActionContext a_actionContext)
        {
            a_actionContext = null;

            var controllerContext = GetControllerContext(a_uri);
            if (controllerContext == null)
                return false;

            var actionDescriptor = GetActionDescriptor(controllerContext);
            if (actionDescriptor == null)
                return false;

            a_actionContext = new HttpActionContext(controllerContext, actionDescriptor);
            actionDescriptor.ActionBinding.ExecuteBindingAsync(a_actionContext, CancellationToken.None).Wait();

            return true;
        }

        /// <summary>
        /// Verifies that the specified action context refers to the same controller action as the action specified by the expression.
        /// </summary>
        /// <typeparam name="TController">
        /// The type of the controller.
        /// </typeparam>
        /// <param name="a_actionContext">
        /// The action context to verify.
        /// </param>
        /// <param name="a_expectedAction">
        /// The expression defining the expected action.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified action context refers to the same controller action as the supplied expression; otherwise, <c>false</c> .
        /// </returns>
        public bool Verify<TController>(HttpActionContext a_actionContext, Expression<Action<TController>> a_expectedAction)
        {
            if (a_actionContext == null)
                throw new ArgumentNullException("a_actionContext");
            if (a_expectedAction == null)
                throw new ArgumentNullException("a_expectedAction");

            if (typeof(TController) != a_actionContext.ControllerContext.ControllerDescriptor.ControllerType)
                return false;

            var expectedActionMethod = a_expectedAction.GetMethodInfo();

           var actionDescriptor = a_actionContext.ActionDescriptor as ReflectedHttpActionDescriptor;
            MethodInfo actualActionMethod = actionDescriptor == null ? GetActionMethod(a_actionContext.ActionDescriptor) : actionDescriptor.MethodInfo;

            return actualActionMethod.RefersToTheSameMethodAs(expectedActionMethod);
        }

        /// <summary>
        /// Gets the <see cref="MethodInfo"/> object the supplied <see cref="HttpActionDescriptor"/> describes.
        /// </summary>
        /// <param name="a_actionDescriptor">
        /// The action descriptor.
        /// </param>
        /// <returns>
        /// The <see cref="MethodInfo"/> object the supplied <see cref="HttpActionDescriptor"/> describes or <c>null</c> if no matching method is found.
        /// </returns>
        private static MethodInfo GetActionMethod(HttpActionDescriptor a_actionDescriptor)
        {
            var actionParameters =
                a_actionDescriptor.ActionBinding.ParameterBindings.Select(a_x => new { Name = a_x.Descriptor.ParameterName, a_x.Descriptor.ParameterType })
                                .OrderBy(a_x => a_x.Name);

            return
                a_actionDescriptor.ControllerDescriptor.ControllerType.GetMethods()
                                .Where(a_x => a_x.Name == a_actionDescriptor.ActionName)
                                .SingleOrDefault(
                                    a_x => a_x.GetParameters().Select(a_y => new { a_y.Name, a_y.ParameterType }).OrderBy(a_y => a_y.Name).SequenceEqual(actionParameters));
        }

        /// <summary>
        /// Gets the name of the controller for the supplied route data.
        /// </summary>
        /// <param name="a_routeData">
        /// The route data.
        /// </param>
        /// <returns>
        /// The name of the controller or <c>null</c> if the route data doesn't contain the name of the controller.
        /// </returns>
        private static string GetControllerName(IHttpRouteData a_routeData)
        {
            if (a_routeData == null)
                throw new ArgumentNullException("a_routeData");
            object tmp;
            if (a_routeData.Values.TryGetValue("controller", out tmp))
                return (string)tmp;

            return null;
        }

        /// <summary>
        /// Gets the action descriptor.
        /// </summary>
        /// <param name="a_controllerContext">
        /// The controller context.
        /// </param>
        /// <returns>
        /// The action descriptor for the controller context.
        /// </returns>
        private HttpActionDescriptor GetActionDescriptor(HttpControllerContext a_controllerContext)
        {
            try
            {
                return m_actionSelector.SelectAction(a_controllerContext);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the <see cref="HttpControllerContext"/> for the supplied <see cref="Uri"/>.
        /// </summary>
        /// <param name="a_uri">
        /// The URI.
        /// </param>
        /// <returns>
        /// The <see cref="HttpControllerContext"/> instance for the supplier URI.
        /// </returns>
        private HttpControllerContext GetControllerContext(Uri a_uri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, a_uri))
            {
                var routeData = Configuration.Routes.GetRouteData(request);
                if (routeData == null)
                    return null;

                request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
                request.Properties[HttpPropertyKeys.HttpConfigurationKey] = Configuration;

                var controllerContext = new HttpControllerContext(Configuration, routeData, request);

                var controllerName = GetControllerName(routeData);

                if (!m_controllerSelector.GetControllerMapping().ContainsKey(controllerName))
                    return null;

                controllerContext.ControllerDescriptor = m_controllerSelector.SelectController(request);
                return controllerContext;
            }
        }
    }
}
