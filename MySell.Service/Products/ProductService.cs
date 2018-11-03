using MySell.Dal;
using MySell.Dal.Model;
using MySell.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Service.Products
{
    public class ProductService : IProductService
    {
        private ProductRepository productRepository = null;
        private ProductSpecRepository productSpecRepository = null;
        public ProductService()
        {
            productRepository = new ProductRepository();
            productSpecRepository = new ProductSpecRepository();
        }

        public Feedback DeletePro(int id)
        {
            var result = productRepository.DeletePro(id);
            if (result > 0)
                return Feedback.Ok("恭喜，产品删除成功。");

            return Feedback.Failure("抱歉，产品删除失败。");
        }

        public Feedback UpdateIsShow(int id)
        {
            var product = productRepository.GetProduct(id);
            var show = 0;
            if (!product.IsShow)
                show = 1;

            var result = productRepository.UpdateIsShow(id, show);
            if (result < 1)
                return Feedback.Failure("抱歉，操作失败，请重试。");

            if (show == 1)
                return Feedback.Ok("恭喜，产品上架成功，网页上将显示该产品。");
            else
                return Feedback.Ok("恭喜，产品下架成功。");
        }

        public Feedback Install(Product product)
        {
            var result = productRepository.Install(product);
            if (result > 0)
                return Feedback.Ok("恭喜，产品添加成功。");

            return Feedback.Failure("抱歉，产品添加失败。");
        }

        public Feedback Update(Product product)
        {
            var result = productRepository.Update(product);
            if (result > 0)
                return Feedback.Ok("恭喜，产品修改成功。");

            return Feedback.Failure("抱歉，产品修改失败。");
        }

        public Product GetProduct(int id)
        {
            return productRepository.GetProduct(id);
        }

        public List<Product> Search(string name, Paged page)
        {
            var productList = productRepository.GetProductPage(name, false, page);

            return productList;
        }

        public Feedback ResetPrice(int id, List<ProductSpec> speclist)
        {
            int result = productSpecRepository.DeleteByProductId(id);
            if (result > 0)
                result = productSpecRepository.Install(speclist);

            if (result > 0)
                return Feedback.Ok("生效成功。");

            return Feedback.Failure("操作失败，请重试！");
        }
    }
}
