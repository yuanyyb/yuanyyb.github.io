
using IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DALFactory
{
    public class AbstractFactory
    {
        /// <summary>
        /// 通过反射的形式创建数据操作类的实例
        /// </summary>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        public static object CreateInstance(string dalAssemblyPath, string fullClassName)
        {
            var assembly = Assembly.Load(dalAssemblyPath);
            return assembly.CreateInstance(fullClassName);

        }

        public static IUserInfoDAL CreateUserInfoDAL() 
        {
            string classFulleName = ConfigurationManager.AppSettings["DalNamespace"] + ".UserInfoDAL";
            var obj = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);
            return obj as IUserInfoDAL;
        }

    }
}
