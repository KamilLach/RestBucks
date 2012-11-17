using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.HyperMedia.Linker
{
    /// <summary>
    /// A class with some extension methods based around expressions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Gets the <see cref="MethodCallExpression"/> of the body of the supplied expression.
        /// </summary>
        /// <param name="a_expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="MethodCallExpression"/> of the body of the supplied expression.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="a_expression"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="a_expression"/> doesn't represent an Action that invokes a method.</exception>
        public static MethodCallExpression GetBodyMethodCallExpression(this LambdaExpression a_expression)
        {
            if (a_expression == null)
                throw new ArgumentNullException("a_expression");

            var methodCallExpression = a_expression.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw new ArgumentException(
                    "The expression's body must be a MethodCallExpression. The code block supplied should invoke a method.\nExample: x => x.Foo().",
                    "a_expression");
            }

            return methodCallExpression;
        }

        /// <summary>
        /// Gets the <see cref="MethodInfo"/> object of the body of the supplied expression.
        /// </summary>
        /// <param name="a_expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="MethodInfo"/> object of the body of the supplied expression.
        /// </returns>
        public static MethodInfo GetMethodInfo(this LambdaExpression a_expression)
        {
            if (a_expression == null)
                throw new ArgumentNullException("a_expression");

            return a_expression.GetBodyMethodCallExpression().Method;
        }

        /// <summary>
        /// Gets the parameter value of the specified parameter from the <see cref="MethodCallExpression"/> .
        /// </summary>
        /// <param name="a_methodCallExpression">
        /// The method call expression.
        /// </param>
        /// <param name="a_parameterInfo">
        /// The parameter to return the value for.
        /// </param>
        /// <returns>
        /// The value of the parameter.
        /// </returns>
        public static object GetParameterValue(this MethodCallExpression a_methodCallExpression, ParameterInfo a_parameterInfo)
        {
            if (a_methodCallExpression == null)
                throw new ArgumentNullException("a_methodCallExpression");
            if (a_parameterInfo == null)
                throw new ArgumentNullException("a_parameterInfo");

            var arg = a_methodCallExpression.Arguments[a_parameterInfo.Position];
            var lambda = Expression.Lambda(arg);
            return lambda.Compile().DynamicInvoke();
        }
    }
}
