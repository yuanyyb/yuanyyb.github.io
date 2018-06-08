using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETWEB.Controllers
{
    public class LoginController : Controller
    {
        IBLL.IUserInfoBLL UserInfoService { get; set; }
        public ActionResult Index()
        {
            CheckCookieInfo();
            return View();
        }

        #region 用户登录
        public ActionResult CheckUserInfo()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误!!");
            }
            Session["validateCode"] = null;
            string txtCode = Request["vCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!!");
            }
            //比较用户名密码
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            UserInfo userInfo = UserInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();
            if (userInfo != null)
            {
                // Session["userInfo"] = userInfo;
                //产生一个唯一的Memache的key
                string sessionId = Guid.NewGuid().ToString();
                Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializerToString(userInfo), DateTime.Now.AddMinutes(20));//向Memcache中存储登录用户的信息.
                Response.Cookies["sessionId"].Value = sessionId;//将KEY以Cookie的形式返回给浏览器。
                if (!string.IsNullOrEmpty(Request["checkMe"]))//判断是否选择了记住我
                {
                    HttpCookie cookie1 = new HttpCookie("cp1", userInfo.UName);
                    HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userInfo.UPwd)));
                    cookie1.Expires = DateTime.Now.AddDays(3);
                    cookie2.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                    //BootStrap
                }


                return Content("ok:登录成功!!");
            }
            else
            {
                return Content("no:用户名或密码错误!!");
            }

        }
        #endregion

        #region 验证码
        public ActionResult ShowValidate()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion

        #region 校验Cookie的信息
        private void CheckCookieInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string userName = Request.Cookies["cp1"].Value;
                string userPwd = Request.Cookies["cp2"].Value;
                var userInfo = UserInfoService.LoadEntities(u => u.UName == userName).FirstOrDefault();
                if (userInfo != null)
                {
                    if (userPwd == Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userInfo.UPwd)))
                    {
                        //产生一个唯一的Memache的key
                        string sessionId = Guid.NewGuid().ToString();
                        Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializerToString(userInfo), DateTime.Now.AddMinutes(20));//向Memcache中存储登录用户的信息.
                        Response.Cookies["sessionId"].Value = sessionId;//将KEY以Cookie的形式返回给浏览器。
                        Response.Redirect("/Home/Index");
                    }
                }

                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion

        #region 退出登录
        public ActionResult Logout()
        {
            if (Request.Cookies["sessionId"] != null)
            {
                Common.MemcacheHelper.Delete(Request.Cookies["sessionId"].Value);
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);

            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}
