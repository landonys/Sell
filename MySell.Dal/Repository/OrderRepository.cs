using MySell.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace MySell.Dal.Repository
{
    public class OrderRepository
    {
        public int Install(ProOrder order)
        {
            var sql = string.Format(@"INSERT INTO ProOrder( `OrderNo` ,`ProductId`, `ProName` , `Qty` , `UserName` ,  `Mobile` , `Address` , `Amount` , `PayType` , `LeavMessage`, `CreateDate`,`Status`)
                                                        VALUES  (@OrderNo, @ProductId ,@ProName , @Qty ,@UserName , @Mobile, @Address , @Amount ,0 , @LeavMessage , @CreateDate,0);");

            return DapperService.MySqlConnection().Execute(sql, order);
        }

        public ProOrder GetOrder(int id)
        {
            var sql = string.Format("SELECT * FROM ProOrder WHERE Id=@Id;");

            return DapperService.MySqlConnection().Query<ProOrder>(sql, new { Id = id }).FirstOrDefault();
        }

        public ProOrder GetOrderByMobile(string mobile)
        {
            var sql = string.Format("SELECT * FROM ProOrder WHERE Mobile=@Mobile ORDER BY ID DESC;");

            return DapperService.MySqlConnection().Query<ProOrder>(sql, new { Mobile = mobile }).FirstOrDefault();
        }

        public ProOrder GetOrderByOrderNo(string orderNo)
        {
            var sql = string.Format("SELECT * FROM ProOrder WHERE OrderNo=@OrderNo ORDER BY ID DESC;");

            return DapperService.MySqlConnection().Query<ProOrder>(sql, new { OrderNo = orderNo }).FirstOrDefault();
        }

        public List<ProOrder> GetOrderAll(string name, Paged page)
        {
            int start = page.PageSize * (page.PageIndex - 1);
            var sql = new StringBuilder("SELECT * FROM ProOrder WHERE 1=1 ");
            var text = string.Format("AND (`OrderNo`='{0}' OR `UserName`='{0}' OR `Mobile`='{0}') ", name);
            if (!string.IsNullOrWhiteSpace(name))
                sql.Append(text);

            sql.Append("order by `Id` desc limit @StartIndex, @PageSize;");

            var list = DapperService.MySqlConnection().Query<ProOrder>(sql.ToString(), new { StartIndex = start, PageSize = page.PageSize }).ToList();

            var sqlCount = new StringBuilder("SELECT COUNT(1) FROM ProOrder WHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(name))
                sqlCount.Append(text);

            page.RowCount = DapperService.MySqlConnection().ExecuteScalar<int>(sqlCount.ToString());

            return list;
        }

        /// <summary>
        /// 最近三十天前20条记录
        /// </summary>
        /// <returns></returns>
        public List<ProOrder> GetOrderInfo()
        {
            var sql = string.Format("SELECT * FROM ProOrder where  DATE_SUB(CURDATE(), INTERVAL 30 DAY) <= date(`CreateDate`)  order by `Id` desc  LIMIT 20;");
            return DapperService.MySqlConnection().Query<ProOrder>(sql).ToList();
        }

        public int UpdateStatus(int id, int status, DateTime deliverDate)
        {
            var sql = string.Format("UPDATE ProOrder SET `Status`=@Status,DeliverDate=@DeliverDate WHERE `Id`=@Id;");
            return DapperService.MySqlConnection().Execute(sql, new { Id = id, Status = status, DeliverDate = deliverDate });
        }
    }
}
