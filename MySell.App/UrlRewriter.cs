using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySell.App
{
    public class UrlRewriter : IHttpHandler
    {
        public UrlRewriter()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public void ProcessRequest(HttpContext Context)
        {
            try
            {
                //取得原始URL屏蔽掉参数
                string Url = Context.Request.RawUrl;
                //建立正则表达式
                System.Text.RegularExpressions.Regex Reg = new System.Text.RegularExpressions.Regex

                (@"/show-(\d+)-(\d+)\..+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //用正则表达式进行匹配
                System.Text.RegularExpressions.Match m =

                              Reg.Match(Url, Url.LastIndexOf("/"));//从最后一个“/”开始匹配
                if (m.Success)//匹配成功
                {
                    String RealPath = @"~/aspx/show.aspx?type=" + m.Groups[1] + "&id=" + m.Groups[2];
                    //Context.Response.Write(RealPath);
                    //Context.RewritePath(RealPath);//(RewritePath 用在无 Cookie 会话状态中。)
                    Context.Server.Execute(RealPath);
                }
                else

                {
                    Context.Response.Redirect(Context.Request.Url.ToString());
                }
            }
            catch
            {
                Context.Response.Redirect(Context.Request.Url.ToString());
            }
        }
        /// <summary>
        /// 实现“IHttpHandler”接口所必须的成员
        /// </summary>
        /// <value></value>
        public bool IsReusable
        {
            get { return false; }
        }
    }
}