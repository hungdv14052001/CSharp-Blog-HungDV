using System.Web;
using System.Web.Mvc;

namespace CShap_Blog_HungDV
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
