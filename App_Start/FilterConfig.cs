using System.Web;
using System.Web.Mvc;

namespace a3_seantrudeln01525609_http5112webdev1c
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
