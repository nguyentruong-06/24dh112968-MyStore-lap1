using System.Web;
using System.Web.Mvc;

namespace _24dh112968_MyStore_lap1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
