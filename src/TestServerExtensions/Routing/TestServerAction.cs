using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TestServerExtensions.Routing.Argument;

namespace TestServerExtensions.Routing
{
    internal class TestServerAction
    {
        public MethodInfo MethodInfo { get; private set; }
        public List<TestServerArgument> Arguments { get; private set; } = new List<TestServerArgument>();

        public TestServerAction(MethodCallExpression methodCallExpression)
        {
            MethodInfo = methodCallExpression.Method;
            AddArguments(methodCallExpression.Arguments);
        }

        private void AddArguments(IEnumerable<Expression> expressions)
        {
            for (int i = 0; i < expressions.Count(); i++)
            {
                var parameter = MethodInfo.GetParameters()[i];

                var instance = ResolveExpressionInstance(expressions.ElementAt(i));
                if (instance != null)
                {
                    var argument = new TestServerArgument(instance, parameter);
                    Arguments.Add(argument);
                }
            }
        }

        private object ResolveExpressionInstance(Expression expression)
        {
            if (expression is ConstantExpression constant)
            {
                return constant;
            }

            if (expression is MemberExpression member)
            {
                var instance = Expression.Lambda(member)
                    .Compile()
                    .DynamicInvoke();

                return instance;
            }

            return null;
        }
    }
}
