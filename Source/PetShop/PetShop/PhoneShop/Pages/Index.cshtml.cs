using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Pages
{
    public class IndexModel : PageModel
    {
        public IList<Product> Products { get; set; } // Danh sách sản phẩm để hiển thị
        public IList<Category> Categories { get; set; } // Danh sách thể loại để hiển thị trong dropdown

        public void OnGet(int? categoryId)
        {
            string connectionString = "Server=QUOCDATT;Database=PetShopp;Integrated Security=true;TrustServerCertificate=True;";
            string productQuery = "SELECT  productId, productName, productImage, price, cateId, color, brand, description FROM Product";
            string categoryQuery = "SELECT cateId, cateName FROM Categories";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Lấy danh sách sản phẩm
                SqlCommand productCommand;
                if (categoryId != null)
                {
                    productCommand = new SqlCommand($"SELECT productId, productName, productImage, price, cateId, color, brand, description FROM Product WHERE cateId = {categoryId}", connection);
                }
                else
                {
                    productCommand = new SqlCommand(productQuery, connection);
                }

                SqlDataReader productReader = productCommand.ExecuteReader();

                Products = new List<Product>();
                while (productReader.Read())
                {
                    Product product = new Product
                    {
                        productId = Convert.ToInt32(productReader["productId"]),
                        productImage = productReader["productImage"].ToString(),
                        productName = productReader["productName"].ToString(),
                        price = Convert.ToDouble(productReader["price"]),
                        Category = new Category
                        {
                            cateId = Convert.ToInt32(productReader["cateId"])
                        },
                        color = productReader["color"].ToString(),
                        brand = productReader["brand"].ToString(),
                        description = productReader["description"].ToString()
                    };
                    Products.Add(product);
                }
                productReader.Close();

                // Lấy danh sách thể loại
                SqlCommand categoryCommand = new SqlCommand(categoryQuery, connection);
                SqlDataReader categoryReader = categoryCommand.ExecuteReader();

                Categories = new List<Category>();
                while (categoryReader.Read())
                {
                    Category category = new Category
                    {
                        cateId = Convert.ToInt32(categoryReader["cateId"]),
                        cateName = categoryReader["cateName"].ToString()
                    };
                    Categories.Add(category);
                }
                categoryReader.Close();
            }
        }

        public class Product
        {
            public int productId { get; set; }
            public string productImage { get; set; }
            public string productName { get; set; }
            public double price { get; set; }
            public Category Category { get; set; }
            public string color { get; set; }
            public string brand { get; set; }
            public string description { get; set; }
        }

        public class Category
        {
            public int cateId { get; set; }
            public string cateName { get; set; }
        }
    }

}
