using MySell.Dal;
using MySell.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Service.Products
{
    public interface IProductService
    {
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Feedback Install(Product product);
        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Feedback Update(Product product);
        /// <summary>
        /// 查询产品
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="page"></param>
        /// <returns></returns>
        List<Product> Search(string name, Paged page);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Feedback DeletePro(int id);
        /// <summary>
        /// 修改产品上架状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Feedback UpdateIsShow(int id);
        /// <summary>
        /// 根据id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProduct(int id);
        /// <summary>
        /// 重置产品规格中价格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="speclist"></param>
        /// <returns></returns>
        Feedback ResetPrice(int id, List<ProductSpec> speclist);
    }
}
