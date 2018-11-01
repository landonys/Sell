using MySell.Dal.Model;
using MySell.Dal.Repository;
using MySell.Service.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var proList = new ProductRepository().GetProductAll();
            //var productSpecRepository = new ProductSpecRepository();
            //foreach (Product pro in proList)
            //{
            //    var proSpecList = new List<ProductSpec>();
            //    var specArray = pro.ProductSpec.Split(',');
            //    foreach (string spec in specArray)
            //    {
            //        var productSpec = new ProductSpec();
            //        foreach (var number in ExtractNumbersFromString(spec))
            //        {
            //            productSpec.ProductId = pro.Id;
            //            productSpec.Name = spec;
            //            productSpec.Price = number;
            //        }

            //        proSpecList.Add(productSpec);
            //    }

            //    productSpecRepository.DeleteByProductId(pro.Id);
            //    productSpecRepository.Install(proSpecList);
            //}
        }

        private static IEnumerable<int> ExtractNumbersFromString(string s)
        {
            //Regex.Matches 方法：在输入字符串中搜索正则表达式的所有匹配项并返回所有匹配。
            //一次或多次匹配前面的字符或子表达式。等效于 {1,}。如果将+去掉，就是
            //return Regex.Matches(s, @"\d").Cast<Match>().Select(m => Convert.ToInt32(m.Value));
            return Regex.Matches(s, @"\d+").Cast<Match>().Select(m => Convert.ToInt32(m.Value));
        }
    }
}
