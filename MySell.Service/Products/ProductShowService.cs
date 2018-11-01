using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySell.Dal.Model;
using MySell.Dal.Repository;

namespace MySell.Service.Products
{
    public class ProductShowService : IProductShowService
    {
        private ProductRepository productRepository = null;
        private ProductSpecRepository productSpecRepository = null;
        public ProductShowService()
        {
            productRepository = new ProductRepository();
            productSpecRepository = new ProductSpecRepository();
        }

        public int AddSold(int productId, int qty)
        {
            return productRepository.AddSold(productId, qty);
        }

        public Product GetProduct(int id)
        {
            return productRepository.GetProduct(id);
        }

        public List<Product> GetProPic()
        {
            return productRepository.GetProPic();
        }

        public List<ProductSpec> GetProductSpec(int productId)
        {
            return productSpecRepository.GetByProductId(productId);
        }
    }
}
