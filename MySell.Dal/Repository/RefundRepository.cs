using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using MySell.Dal.Model;

namespace MySell.Dal.Repository
{
    public class RefundRepository
    {
        public int Install(Refund refund)
        {
            var sql = string.Format(@"INSERT INTO Refund( `OrderNo` ,`Mobile`, `ProcessMode` , `Message` , `CreateDate` , `Account` ,`Status`) VALUES (@OrderNo, @Mobile ,@ProcessMode , @Message ,@CreateDate, @Account,0);");

            return DapperService.MySqlConnection().Execute(sql, refund);
        }

        public List<Refund> GetRefundALL(Paged page)
        {
            int start = page.PageSize * (page.PageIndex - 1);
            var sql = string.Format(@"SELECT * FROM Refund ORDER BY `Id` DESC limit @StartIndex, @PageSize;;");
            var list = DapperService.MySqlConnection().Query<Refund>(sql, new { StartIndex = start, PageSize = page.PageSize }).ToList();

            var sqlCount = string.Format("SELECT COUNT(1) FROM Refund;");
            page.RowCount = DapperService.MySqlConnection().ExecuteScalar<int>(sqlCount);

            return list;
        }

        public Refund Get(int refundId)
        {
            var sql = string.Format(@"SELECT * FROM Refund WHERE `Id`=@Id;");

            return DapperService.MySqlConnection().Query<Refund>(sql, new { Id = refundId }).FirstOrDefault();
        }

        public int UpdateStatus(int id)
        {
            var sql = string.Format(@"UPDATE Refund SET `Status`=1 WHERE `Id`=@Id;");

            return DapperService.MySqlConnection().Execute(sql, new { Id = id });
        }
    }
}
