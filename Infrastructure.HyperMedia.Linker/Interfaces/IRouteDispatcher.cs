using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;

namespace Infrastructure.HyperMedia.Linker.Interfaces
{
    /// <summary>
    /// A Strategy for dispatching a method to a named route.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In the ASP.NET Web API, routes added to an application's configured
    /// <see cref="HttpRouteCollection" /> have names. When an
    /// URI is to be built from the routing configuration, one must supply the
    /// name of the route to be used.
    /// </para>
    /// <para>
    /// This interface represents a Strategy for selecting a route name given
    /// an Action Method.
    /// </para>
    /// </remarks>
    /// <seealso cref="Dispatch(MethodCallExpression, IDictionary{string, object})" />
    public interface IRouteDispatcher
    {
        /// <summary>
        /// Provides dispatch information based on an Action Method.
        /// </summary>
        /// <param name="a_method">The method expression.</param>
        /// <param name="a_routeValues">Route values.</param>
        /// <returns>
        /// An object containing the route name, as well as the route values.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Note to implementers: Pass <paramref name="a_routeValues" /> through
        /// to the return value if you don't modify it. However, if you wish to
        /// add or remove values from the dictionary, you should create a copy
        /// and mutate that copy, leaving the input unmodified.
        /// </para>
        /// </remarks>
        Rouple Dispatch(
            MethodCallExpression a_method,
            IDictionary<string, object> a_routeValues);
    }
}
