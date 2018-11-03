using MySell.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace MySell.Dal.Repository
{
    public class ProductRepository
    {
        public List<Product> GetProPic()
        {
            var sql = string.Format("SELECT `Id`,`PicSrc`,`Price`,`DPrice` FROM Product WHERE `IsShow`=1 ORDER BY `Sort` desc,`Id` desc;");
            return DapperService.MySqlConnection().Query<Product>(sql).ToList();
        }

        public Product GetProduct(int id)
        {
            var sql = string.Format("SELECT * FROM Product WHERE Id=@Id;");
            return DapperService.MySqlConnection().Query<Product>(sql, new { Id = id }).SingleOrDefault();
        }

        public int Install(Product product)
        {
            var sql = string.Format(@"INSERT INTO Product(Title, Price, Discount, DPrice, PicSrc, Sold, ActivityDate, Describe1,Describe2,Describe3,BannerPic,DescribePic,ProcessPic,SynopsisPic,ProductSpec,Sort,IsShow)
                                        VALUES(@Title,@Price,@Discount,@DPrice,@PicSrc,@Sold,@ActivityDate,@Describe1,@Describe2,@Describe3,@BannerPic,@DescribePic,@ProcessPic,@SynopsisPic,@ProductSpec,@Sort,@IsShow);");
            return DapperService.MySqlConnection().Execute(sql, product);
        }

        public int Update(Product product)
        {
            var sql = string.Format(@"UPDATE `Product`
                                    SET `Title` = @Title, `Price` = @Price, `Discount` = @Discount, `DPrice` = @DPrice, `PicSrc` = @PicSrc, `Sold` = @Sold, 
                                    `ActivityDate` = @ActivityDate, `Describe1` = @Describe1, `Describe2` = @Describe2, `Describe3` = @Describe3, `BannerPic` = @BannerPic,
                                    `DescribePic` = @DescribePic, `ProcessPic` = @ProcessPic, `SynopsisPic` = @SynopsisPic, `ProductSpec` = @ProductSpec,`Sort` = @Sort
                                    WHERE `Id` = @Id;");
            return DapperService.MySqlConnection().Execute(sql, product);
        }

        public List<Product> GetProductPage(string name, bool show, Paged page)
        {
            int start = page.PageSize * (page.PageIndex - 1);
            var sql = new StringBuilder("SELECT * FROM Product WHERE 1=1 ");
            var title = string.Format("AND `Title` LIKE '%{0}%' ", name);
            if (!string.IsNullOrWhiteSpace(name))
                sql.Append(title);

            if (show)
                sql.Append("AND `IsShow`=1 ");

            sql.Append("ORDER BY `Sort` desc,`Id` desc limit @StartIndex, @PageSize;");

            var proList = DapperService.MySqlConnection().Query<Product>(sql.ToString(), new { StartIndex = start, PageSize = page.PageSize }).ToList();

            var sqlCount = new StringBuilder("SELECT COUNT(1) FROM Product WHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(name))
                sqlCount.Append(title);

            page.RowCount = DapperService.MySqlConnection().ExecuteScalar<int>(sqlCount.ToString());

            return proList;
        }

        public int AddSold(int productId, int qty)
        {
            var sql = string.Format("UPDATE Product SET Sold=Sold+@Qty where Id=@ProductId;");
            return DapperService.MySqlConnection().Execute(sql, new { Qty = qty, ProductId = productId });
        }

        public int DeletePro(int productId)
        {
            var sql = string.Format("DELETE FROM Product WHERE `Id`=@Id;");
            return DapperService.MySqlConnection().Execute(sql, new { Id = productId });
        }

        public int UpdateIsShow(int id, int show)
        {
            var sql = string.Format("UPDATE Product SET `IsShow`=@IsShow where Id=@ProductId;");
            return DapperService.MySqlConnection().Execute(sql, new { ProductId = id, IsShow = show });
        }
    }
}
