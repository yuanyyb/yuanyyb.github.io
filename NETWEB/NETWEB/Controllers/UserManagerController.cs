using Model;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETWEB.Controllers
{
    public class UserManagerController : Controller
    {
        //
        // GET: /UserManager/

        public ActionResult Index()
        {
            BaseOrm m = new BaseOrm();
            //List<UserInfo> list = new Class1().getUser();
            List<UserInfo> list = m.GetAll(System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ToString());
            ViewData.Model = list;
            return View();
        }

    }
}
