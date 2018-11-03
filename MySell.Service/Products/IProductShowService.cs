using MySell.Dal;
using MySell.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Service.Products
{
    public interface IProductShowService
    {
        /// <summary>
        /// 获取全部产品图片及价格
        /// </summary>
        /// <remarks>主页面显示</remarks>
        /// <returns></returns>
        List<Product> GetProPic();

        /// <summary>
        /// 获取产品详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProduct(int id);

        /// <summary>
        /// 添加产品销量
        /// </summary>
        /// <param name="productId">产品Id</param>
        /// <param name="qty">数量</param>
        /// <returns></returns>
        int AddSold(int productId, int qty);
        /// <summary>
        /// 获取产品规格
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductSpec> GetProductSpec(int productId);
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        List<Product> Search(Paged page);
    }
}
