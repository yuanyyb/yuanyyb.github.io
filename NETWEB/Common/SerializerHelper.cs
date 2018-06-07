
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public class SerializerHelper
    {
       /// <summary>
       /// 进行序列化操作
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       public static string SerializerToString(object obj)
       {
           return JsonConvert.SerializeObject(obj);
       }
       /// <summary>
       /// 反序列化
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static T DeserializeToObject<T>(string str)
       {
           return JsonConvert.DeserializeObject<T>(str);
       }
    }
}
