using System.Collections;
using System.Web;
using Castle.DynamicProxy;

namespace DocumentServices.Interceptors
{
    public class RequestCacheAttribute : AspectAttributeBase
    {
        public IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }

        protected override bool BeforeExecution(IInvocation invocation)
        {
            var parameterKey = CacheHelper.GetCacheParameterName(invocation);
            var hasItem = Items.Contains(parameterKey);
            if (hasItem)
            {
                invocation.ReturnValue = Items[parameterKey];

            }
            else
            {
                invocation.Proceed();
                Items[parameterKey] = invocation.ReturnValue;
            }
            return false;
        }
    }
}