using System.Web;
using System.Web.Mvc;

namespace TPAPIs_Equipo7
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
