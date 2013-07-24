using System.Web;
using System.Web.Mvc;

namespace MVC_MutilLayers_ADONET
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}