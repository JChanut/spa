using System.Web;
using System.Web.Mvc;

namespace Qoveo.Impact
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Register the authorize attribute for all the controller (login first)
            //filters.Add(new AuthorizeAttribute());
        }
    }
}