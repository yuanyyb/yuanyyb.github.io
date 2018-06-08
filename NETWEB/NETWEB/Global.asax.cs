using log4net;
using NETWEB.Models;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NETWEB
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    //public class MvcApplication : System.Web.HttpApplication
    public class MvcApplication :SpringMvcApplication
    {
        protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();

        //    WebApiConfig.Register(GlobalConfiguration.Configuration);
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}
        {
            SearchIndexManager.GetInstance().StartThread();//开始一个线程扫描队列，将该队列中的数据写到Lucene.Net文件中。

            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string fileLogPath = Server.MapPath("/Log/");

            //开启一个线程扫描异常队列。
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)//表示队列中有数据
                    //if (MyExceptionAttribute.redisClent.GetListCount("exception") > 0)
                    {
                         Exception ex=MyExceptionAttribute.ExceptionQueue.Dequeue();//出队
                        //string errorMsg = MyExceptionAttribute.redisClent.DequeueItemFromList("exception");
                        if (!string.IsNullOrEmpty(ex.Message))
                        {
                            //将异常信息写到文件中。
                            //string fileName = DateTime.Now.ToString("yyyy-MM-dd");
                            //File.AppendAllText(fileLogPath + fileName + ".txt", ex.ToString(), Encoding.UTF8);
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.Message);
                        }
                        else
                        {
                            Thread.Sleep(3000);//队列没有数据，让当前的线程休息，避免了造成CPU空转。
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }



            }, fileLogPath);
        }

    }
}