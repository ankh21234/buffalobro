using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Buffalo.WebKernel.WebCommons
{
    public class WipeHtml
    {
        /// <summary>         
        ///  移除HTMl 标记         
        /// </summary>         
        /// <param name="Html"> </param>               
        /// <returns> </returns>         
        private static string Remove(string Html)         
        {             
            string regesstr = " <.*?>";             
            return Regex.Replace(Html, regesstr, string.Empty, RegexOptions.IgnoreCase);
        }         
        public static string FilterScript(string content)         
        {             
            string regexstr = @" <(script)[^>]*>(\s* ¦.)* </\1>";             
            return Regex.Replace(content,regexstr,string.Empty,RegexOptions.IgnoreCase);         
        }         
        /// <summary>         
        /// 过略所有的 危险标记         
        /// </summary>         
        /// <param name="html"> </param>         
        /// <returns> </returns>         
        public static string WipeScript(string html)         
        {             
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"( <script){1,}[^ <>]*>[^\0]*( <\/script>){1,}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);             
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"href  *=  *[\s\S]*script  *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);             
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@"on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);             
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@" <iframe[\s\S]+ </iframe*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);             
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@" <frameset[\s\S]+ </frameset*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);             
            html = regex1.Replace(html, "");  //过滤 <script> </script>标记
            html = regex2.Replace(html, "");  //过滤href=javascript:  ( <A>)  属性
            html = regex3.Replace(html, "  _disibledevent=");  //过滤其它控件的on...事件
            html = regex4.Replace(html, "");  //过滤iframe
            html = regex5.Replace(html, "");  //过滤frameset
            return html;
        }
    }
}
