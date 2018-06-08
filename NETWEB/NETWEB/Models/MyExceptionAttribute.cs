using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETWEB.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 捕获异常
        /// </summary>
        /// <param name="filterContext"></param>
       public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        //public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "localhost:11993"});
        //public static IRedisClientsManager clientManager = new PooledRedisClientManager();
        //public static IRedisClient redisClent = clientManager.GetClient();
        public override void OnException(ExceptionContext filterContext)
        {

               base.OnException(filterContext);
               ExceptionQueue.Enqueue(filterContext.Exception);//将异常信息写到队列中。
             
              //redisClent.EnqueueItemOnList("exception", filterContext.Exception.ToString());
               filterContext.HttpContext.Response.Redirect("/error.html");
        }

    }
}