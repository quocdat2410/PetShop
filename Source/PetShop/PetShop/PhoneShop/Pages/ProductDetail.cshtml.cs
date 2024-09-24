using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using static PetShop.Pages.IndexModel;

namespace PetShop.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductDetailModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("ShopPetDB");
            _httpContextAccessor = httpContextAccessor;
        }

        public Product Product { get; private set; }

        public IActionResult OnGet(int id)
        {
            Product = GetProductById(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }



        private Product GetProductById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT productName, productImage, price, cateId, color, brand, description FROM Product WHERE productId = @ProductId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", id);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                productId = id,
                                productImage = reader["productImage"].ToString(),
                                productName = reader["productName"].ToString(),
                                price = Convert.ToDouble(reader["price"]),
                                Category = new Category
                                {
                                    cateId = Convert.ToInt32(reader["cateId"])
                                },
                                color = reader["color"].ToString(),
                                brand = reader["brand"].ToString(),
                                description = reader["description"].ToString()
                            };
                        }
                    }
                }
            }

            // Trả về null nếu không tìm thấy sản phẩm
            return null;
        }

        private void AddToCart(Product product, int quantity, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"IF NOT EXISTS (SELECT 1 FROM Cart WHERE UserId = @UserId AND ProductId = @ProductId)
                        BEGIN
                            INSERT INTO Cart (UserId, ProductId, Quantity, Price, Total)
                            VALUES (@UserId, @ProductId, @Quantity, @Price, @Total)
                        END
                        ELSE
                        BEGIN
                            UPDATE Cart
                            SET Quantity = Quantity + @Quantity,
                                Total = Total + @Total
                            WHERE UserId = @UserId AND ProductId = @ProductId
                        END";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ProductId", product.productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Price", product.price);
                    command.Parameters.AddWithValue("@Total", quantity * product.price);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IActionResult OnPost(int productId)
        {
            int quantity = 1; // You can modify this to capture the quantity from the form if needed

            // Get the product by its ID
            Product product = GetProductById(productId);

            // Check if the product exists
            if (product == null)
            {
                return NotFound();
            }

            // Get UserId from the cookie
            int userId;
            if (!int.TryParse(_httpContextAccessor.HttpContext.Request.Cookies["UserId"], out userId))
            {
                // Handle case where UserId is not found in the cookie
                return RedirectToPage("/Login"); // Redirect to login page or handle as needed
            }

            // Add the product to the cart
            AddToCart(product, quantity, userId);

            // Redirect to the cart page or any other page as needed
            return RedirectToPage("/Cart"); // Change "/Cart" to the appropriate path
        }


    }
}
