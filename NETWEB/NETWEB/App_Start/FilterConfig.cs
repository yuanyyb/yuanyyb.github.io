using NETWEB.Models;
using System.Web;
using System.Web.Mvc;

namespace NETWEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionAttribute());
        }
    }
}