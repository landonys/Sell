using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using MySell.Dal.Model;

namespace MySell.Dal.Repository
{
    public class ProductSpecRepository
    {
        public int Install(List<ProductSpec> specList)
        {
            var sql = string.Format(@"INSERT INTO ProductSpec(`ProductId`,`Name`,`Price`)VALUES(@ProductId,@Name,@Price);");

            return DapperService.MySqlConnection().Execute(sql, specList);
        }

        public int DeleteByProductId(int productId)
        {
            var sql = string.Format(@"Delete FROM ProductSpec WHERE `ProductId`=@ProductId;");

            return DapperService.MySqlConnection().Execute(sql, new { ProductId = productId });
        }

        public ProductSpec Get(int id)
        {
            var sql = string.Format(@"SELECT * FROM ProductSpec WHERE `Id`=@Id;");

            return DapperService.MySqlConnection().Query<ProductSpec>(sql, new { Id = id }).FirstOrDefault();
        }

        public List<ProductSpec> GetByProductId(int productId)
        {
            var sql = string.Format(@"SELECT * FROM ProductSpec WHERE `ProductId`=@ProductId;");

            return DapperService.MySqlConnection().Query<ProductSpec>(sql, new { ProductId = productId }).ToList();
        }
    }
}
