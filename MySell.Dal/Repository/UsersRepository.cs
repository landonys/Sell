using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using MySell.Dal.Model;

namespace MySell.Dal.Repository
{
    public class UsersRepository
    {
        public int Install(Users user)
        {
            var sql = string.Format(@"INSERT INTO Users( `LoginName` ,`Password`, `Name`) VALUES (@LoginName, @Password ,@Name);");

            return DapperService.MySqlConnection().Execute(sql, user);
        }

        public int Update(Users user)
        {
            var sql = string.Format(@"Update Users set `LoginName`=@LoginName,`Name`=@Name where `Id`=@Id;");

            return DapperService.MySqlConnection().Execute(sql, user);
        }

        public List<Users> GetUsers()
        {
            var sql = string.Format(@"SELECT * FROM Users ORDER BY `Id` DESC;");

            return DapperService.MySqlConnection().Query<Users>(sql).ToList();
        }

        public int SetPassword(int id, string password)
        {
            var sql = string.Format(@"Update Users set `Password`=@Password where `Id`=@Id;");

            return DapperService.MySqlConnection().Execute(sql, new { Id = id, Password = password });
        }

        public Users Get(int id)
        {
            var sql = string.Format(@"SELECT * FROM Users where `Id`=@Id;");

            return DapperService.MySqlConnection().Query<Users>(sql, new { Id = id }).FirstOrDefault();
        }

        public Users Get(string loginName, string password)
        {
            var sql = string.Format(@"SELECT * FROM Users where `LoginName`=@LoginName AND `Password`=@Password;");

            return DapperService.MySqlConnection().Query<Users>(sql, new { LoginName = loginName, Password = password }).FirstOrDefault();
        }
    }
}
