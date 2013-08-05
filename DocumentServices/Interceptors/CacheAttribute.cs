using System.Web;
using System.Web.Caching;
using Castle.DynamicProxy;

namespace DocumentServices.Interceptors
{
    public class CacheAttribute : AspectAttributeBase
    {
        public Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        protected override bool BeforeExecution(IInvocation invocation)
        {
            string key = CacheHelper.GetCacheParameterName(invocation);

            var value = Cache[key];

            if (value == null)
            {
                invocation.Proceed();
                Cache[key] = invocation.ReturnValue;
            }
            else
            {
                invocation.ReturnValue = Cache[key];
            }

            return false;
        }
    }
}