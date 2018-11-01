using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using MySell.Dal.Model;

namespace MySell.Dal.Repository
{
    public class LogisticsRepository
    {
        public int Install(Logistics logistics)
        {
            var sql = string.Format(@"INSERT INTO Logistics( `OrderNo` ,`Company`, `CourierNumber` , `Message` , `CreateDate`) VALUES (@OrderNo, @Company ,@CourierNumber , @Message ,@CreateDate);");

            return DapperService.MySqlConnection().Execute(sql, logistics);
        }

        public List<Logistics> GetLogistics(string moblie)
        {
            var sql = string.Format(@"select l.`OrderNo`,l.`Company`,l.`CourierNumber`,l.`Message`,l.`CreateDate` from `Logistics` l LEFT JOIN `ProOrder` o on(l.`OrderNo`=o.`OrderNo`) 
                                      where o.`Mobile` =@Moblie;");

            return DapperService.MySqlConnection().Query<Logistics>(sql, new { Moblie = moblie }).ToList();
        }

        public List<Logistics> SearchLogistics(string orderNo)
        {
            var sql = string.Format(@"select `OrderNo`,`Company`,`CourierNumber`,`Message`,`CreateDate` from `Logistics` where `OrderNo` =@OrderNo;");

            return DapperService.MySqlConnection().Query<Logistics>(sql, new { OrderNo = orderNo }).ToList();
        }
    }
}
