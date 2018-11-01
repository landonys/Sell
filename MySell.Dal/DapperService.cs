using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MySell.Dal
{
    public class DapperService
    {
        public static MySqlConnection MySqlConnection()
        {
            string mysqlConnectionStr = ConfigurationManager.ConnectionStrings["MsConnectionStr"].ToString();
            var connection = new MySqlConnection(mysqlConnectionStr);
            connection.Open();
            return connection;
        }
    }
}
