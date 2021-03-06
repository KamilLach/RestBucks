﻿using System;
using System.Web.Http.Controllers;

namespace Infrastructure.HyperMedia.Linker.Interfaces
{
    /// <summary>
    /// Parses resource links.
    /// </summary>
    public interface IResourceLinkParser
    {
        /// <summary>
        /// Parses the specified URI.
        /// </summary>
        /// <param name="a_uri">
        /// The URI to parse.
        /// </param>
        /// <returns>
        /// The <see cref="HttpActionContext"/> with bound parameter values.
        /// </returns>
        HttpActionContext Parse(Uri a_uri);

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
        ///     <c>true</c> in case the URI could be parsed successfully; otherwise, <c>false</c> .
        /// </returns>
        bool TryParse(Uri a_uri, out HttpActionContext a_actionContext);
    }
}
