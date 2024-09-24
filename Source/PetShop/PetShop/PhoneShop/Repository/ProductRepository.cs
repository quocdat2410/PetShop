using Microsoft.EntityFrameworkCore;
using PetShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Repository
{
    public interface IProductRepository
    {
        bool Create(Product product);
        bool Update(Product product);
        bool Delete(int productId);
        List<Product> GetAll();
        List<Product> GetAllProductsById(int id);
        List<Product> SearchProductByName(string productName);
        bool CheckName(string name);
        Product FindById(int id);
        List<Product> SortProductsByPriceDescending();
    }

    public class ProductRepository : IProductRepository
    {
        private readonly PetShopContext _ctx;

        public ProductRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public bool Create(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return true;
        }

        public bool Update(Product product)
        {
            _ctx.Products.Update(product);
            _ctx.SaveChanges();
            return true;
        }

        public bool Delete(int productId)
        {
            var product = _ctx.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _ctx.Products.Remove(product);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Product> GetAll()
        {
            return _ctx.Products.ToList();
        }

        public List<Product> GetAllProductsById(int id)
        {
            return _ctx.Products.Where(p => p.CateId == id).ToList();
        }

        public List<Product> SearchProductByName(string productName)
        {
            return _ctx.Products.Where(p => p.ProductName.Contains(productName)).ToList();
        }

        public bool CheckName(string name)
        {
            return _ctx.Products.Any(p => p.ProductName.Trim() == name.Trim());
        }

        public Product FindById(int id)
        {
            return _ctx.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public List<Product> SortProductsByPriceDescending()
        {
            return _ctx.Products.OrderByDescending(p => p.Price).ToList();
        }
    }
}
