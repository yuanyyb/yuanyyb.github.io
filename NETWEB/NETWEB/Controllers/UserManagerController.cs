using Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETWEB.Controllers
{
    public class UserManagerController : BaseController
    {
        //
        // GET: /UserManager/
        IBLL.IUserInfoBLL UserInfoBLL { get; set; }

        public ActionResult UserList()
        {
            int pindex = !string.IsNullOrEmpty(Request["pindex"]) ? int.Parse(Request["pindex"]) : 1;
            int psize = !string.IsNullOrEmpty(Request["psize"]) ? int.Parse(Request["psize"]) : 10;
            List<UserInfo> list = new List<UserInfo>();
            int total = 0;
            var userlist = UserInfoBLL.LoadPageEntities<string>(pindex, psize, out total, u => u.DelFlag == 0, u => u.Sort, true);
            var temp = from u in userlist
                       select new { ID = u.ID, UserName = u.UName, Remark = u.Remark, Password = u.UPwd };
            return Json(new { rows = temp, total = total }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            
            int pindex = !string.IsNullOrEmpty(Request["pindex"]) ? int.Parse(Request["pindex"]) : 1;
            int psize = !string.IsNullOrEmpty(Request["psize"]) ? int.Parse(Request["psize"]) : 10;
            List<UserInfo> list = new List<UserInfo>();
            int total = 0;
            var sss = UserInfoBLL.LoadPageEntities<string>(pindex, psize, out total, u => u.DelFlag == 0, u => u.Sort, true);
            if (sss != null)
            {
                list = sss.ToList<UserInfo>();
            }

            ViewData.Model = list;
            ViewBag.total = total;
            return View();
        }


    }
}
