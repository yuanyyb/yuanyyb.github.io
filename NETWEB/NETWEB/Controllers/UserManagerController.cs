﻿using Model;
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
            List<UserInfo> list = m.GetAll();
            ViewData.Model = list;
            return View();
        }

    }
}
