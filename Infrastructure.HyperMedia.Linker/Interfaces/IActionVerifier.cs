using System;
using System.Linq.Expressions;
using System.Web.Http.Controllers;

namespace Infrastructure.HyperMedia.Linker.Interfaces
{
    /// <summary>
    /// Verifies via type-safe expressions that an <see cref="HttpActionContext"/> calls a specific controller action.
    /// </summary>
    public interface IActionVerifier
    {
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
        bool Verify<TController>(HttpActionContext a_actionContext, Expression<Action<TController>> a_expectedAction);
    }
}
