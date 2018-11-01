using MySell.Dal;
using MySell.Dal.Model;
using MySell.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MySell.Service.User
{
    public class UserService : IUserService
    {
        private const string SESSION_SELL_LOGIN_KEY = "SELL_LOGIN_MAIN";
        private UsersRepository usersRepository = null;
        public UserService()
        {
            usersRepository = new UsersRepository();
        }

        /// <summary>
        /// 获取当前登录的主账号
        /// </summary>
        public static Users CurrentBusiness
        {
            get
            {
                var web = HttpContext.Current;
                if (web == null || web.Session == null)
                    return null;

                return web.Session[SESSION_SELL_LOGIN_KEY] as Users;
            }
        }

        public Feedback Login(string name, string password)
        {
            var user = usersRepository.Get(name, password);
            if (user == null)
            {
                return Feedback.Failure("用户名或密码错误！");
            }

            HttpContext.Current.Session[SESSION_SELL_LOGIN_KEY] = user;
            return Feedback.Ok("登录成功！");
        }

        public Feedback OutLogin()
        {
            HttpContext.Current.Session[SESSION_SELL_LOGIN_KEY] = null;
            return Feedback.Ok("退出成功！");
        }
    }
}
