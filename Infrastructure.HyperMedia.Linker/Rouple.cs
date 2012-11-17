using System;
using System.Collections.Generic;

namespace Infrastructure.HyperMedia.Linker
{
    /// <summary>
    /// A route tuple: a rouple - pardon the pun.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is simply a tuple of <see cref="RouteName" /> and
    /// <see cref="RouteValues" />.
    /// </para>
    /// </remarks>
    public class Rouple
    {
        private readonly string m_routeName;
        private readonly IDictionary<string, object> m_routeValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rouple"/> class.
        /// </summary>
        /// <param name="a_routeName">A route name.</param>
        /// <param name="a_routeValues">Route values.</param>
        /// <remarks>
        /// <para>
        /// The <paramref name="a_routeName" /> is available after initialization
        /// via the <see cref="RouteName" /> property.
        /// </para>
        /// <para>
        /// The <paramref name="a_routeValues" /> are available after
        /// initialization via the <see cref="RouteValues" /> property.
        /// </para>
        /// </remarks>
        public Rouple(string a_routeName, IDictionary<string, object> a_routeValues)
        {
            if (a_routeName == null)
                throw new ArgumentNullException("a_routeName");
            if (a_routeValues == null)
                throw new ArgumentNullException("a_routeValues");
                        
            m_routeName = a_routeName;
            m_routeValues = a_routeValues;
        }

        /// <summary>
        /// Gets the route name.
        /// </summary>
        /// <seealso cref="Rouple(string, IDictionary{string, object})" />
        public string RouteName
        {
            get { return m_routeName; }
        }

        /// <summary>
        /// Gets the route values.
        /// </summary>
        /// <seealso cref="Rouple(string, IDictionary{string, object})" />
        public IDictionary<string, object> RouteValues
        {
            get { return m_routeValues; }
        }
    }
}
