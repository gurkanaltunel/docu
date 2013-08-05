// ReSharper disable CheckNamespace

using System.Web.Mvc;

namespace DocumentServices
{
    public static class HtmlExtensions
    {
        public static string MinimizeString(this HtmlHelper helper, string value, int maximumSize)
        {
            var isEmpty = string.IsNullOrEmpty(value);
            if (!isEmpty && value.Length > maximumSize)
            {
                return string.Format("{0}...", value.Substring(0, maximumSize));
            }
            return value;
        }
    }
}