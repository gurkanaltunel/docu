using Castle.DynamicProxy;
using System.Linq;

namespace DocumentServices.Interceptors
{
    public class AspectInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var attributes = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(AspectAttributeBase), true).Cast<AspectAttributeBase>();
            foreach (var attribute in attributes)
            {
                attribute.ExecuteAttribute(invocation);
            }
            invocation.Proceed();
        }
    }
}