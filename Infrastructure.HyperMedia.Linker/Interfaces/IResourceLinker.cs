using System;
using System.Linq.Expressions;

namespace Infrastructure.HyperMedia.Linker.Interfaces
{
    /// <summary>
    /// Creates URIs from type-safe expressions.
    /// </summary>
    public interface IResourceLinker
    {
        /// <summary>
        /// Creates an URI based on a type-safe expression.
        /// </summary>
        /// <typeparam name="T">
        /// The type of resource to link to. This will typically be the type of an
        /// <see cref="System.Web.Http.ApiController" />, but doesn't have to be.
        /// </typeparam>
        /// <param name="a_method">
        /// An expression wich identifies the action method that serves the desired resource.
        /// </param>
        /// <returns>
        /// An <see cref="Uri" /> instance which represents the resource identifed by
        /// <paramref name="a_method" />.
        /// </returns>
        Uri GetUri<T>(Expression<Action<T>> a_method);
    }
}
