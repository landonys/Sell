using MySell.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Service.User
{
    public interface IUserService
    {
        Feedback Login(string name, string password);
        Feedback OutLogin();
    }
}
