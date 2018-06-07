using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public class WebCommon
    {
       public static string GetMd5String(string str)
       {
           MD5 md5 = MD5.Create();
           byte[]buffer=Encoding.UTF8.GetBytes(str);
          byte[]md5Buffer= md5.ComputeHash(buffer);
          StringBuilder sb = new StringBuilder();
          foreach (byte b in md5Buffer)
          {
              sb.Append(b.ToString("x2"));
          }
          md5.Clear();
          return sb.ToString();
       }
       /// <summary>
       /// 对字符串进行盘古分词
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static List<string>  GetPanGu(string str)
       {
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
           List<string>list=new List<string>();
            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }
            return list;
       }
       /// <summary>
       /// 搜索结果的高亮显示
       /// </summary>
       /// <param name="keywords">搜索的关键词</param>
       /// <param name="Content">内容简介</param>
       /// <returns></returns>
       public static string CreateHightLight(string keywords, string Content)
       {
           PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
            new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
           //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
           PanGu.HighLight.Highlighter highlighter =
           new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
           new Segment());
           //设置每个摘要段的字符数
           highlighter.FragmentSize = 150;
           //获取最匹配的摘要段
           return highlighter.GetBestFragment(keywords, Content);

       }
    }
}
