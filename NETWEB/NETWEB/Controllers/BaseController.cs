using IBLL;
using Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETWEB.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public UserInfo LoginUserInfo { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //IApplicationContext ctx = ContextRegistry.GetContext();
            //IUserInfoBLL UserInfoBLL = (IUserInfoBLL)ctx.GetObject("UserInfoBLL");


        }

    }
}
