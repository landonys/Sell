using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using MySell.Dal.Model;

namespace MySell.Dal.Repository
{
    public class ConfigRepository
    {
        public int Install(Config config)
        {
            var sql = string.Format(@"INSERT INTO Config( `Mobile` ,`ActivityDate`) VALUES (@Mobile, @ActivityDate);");

            return DapperService.MySqlConnection().Execute(sql, config);
        }

        public Config Get()
        {
            var sql = string.Format(@"SELECT * FROM Config LIMIT 1;");

            return DapperService.MySqlConnection().Query<Config>(sql).FirstOrDefault();
        }

        public int Update(Config config)
        {
            var sql = string.Format(@"UPDATE Config set `Mobile` =@Mobile,`ActivityDate`=@ActivityDate WHERE `Id`=@Id;;");

            return DapperService.MySqlConnection().Execute(sql, config);
        }
    }
}
