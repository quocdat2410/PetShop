using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PetShop.Pages
{
    public class CartModel : PageModel
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<CartItem> CartItems { get; private set; } = new List<CartItem>();

        public CartModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("ShopPetDB");
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            // Lấy UserId từ cookie
            int userId;
            if (!int.TryParse(_httpContextAccessor.HttpContext.Request.Cookies["UserId"], out userId))
            {
                // Xử lý trường hợp người dùng chưa đăng nhập
                return;
            }

            // Truy vấn cơ sở dữ liệu để lấy giỏ hàng của người dùng dựa trên UserId
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT p.productId, p.productName, p.price, c.Quantity FROM Cart c JOIN Product p ON c.ProductId = p.ProductId WHERE c.UserId = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                productId = (int)reader["productId"],
                                productName = reader["productName"].ToString(),
                                price = Convert.ToDouble(reader["price"])
                            };
                            var quantity = (int)reader["Quantity"];
                            CartItems.Add(new CartItem { Product = product, Quantity = quantity });
                        }
                    }
                }
            }
        }


        public IActionResult OnPostCheckout()
        {
            // Lấy UserId từ cookie
            int userId;
            if (!int.TryParse(_httpContextAccessor.HttpContext.Request.Cookies["UserId"], out userId))
            {
                // Xử lý trường hợp người dùng chưa đăng nhập
                return RedirectToPage("/Login"); // Redirect đến trang đăng nhập
            }

            // Tạo hóa đơn
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                CreateInvoice(connection, userId);
                ClearCart(connection, userId);
            }

            return RedirectToPage("/Invoice"); // Redirect đến trang hiển thị hóa đơn
        }

        private void CreateInvoice(SqlConnection connection, int userId)
        {
            string insertQuery = "INSERT INTO Invoice (cartId, userId, totalAmount, invoiceDate, status) VALUES (@CartId, @UserId, @TotalAmount, @InvoiceDate, @Status);";
            string selectCartQuery = "SELECT CartId, SUM(Quantity * Price) AS TotalAmount FROM Cart WHERE UserId = @UserId GROUP BY CartId;";

            using (var selectCommand = new SqlCommand(selectCartQuery, connection))
            {
                selectCommand.Parameters.AddWithValue("@UserId", userId);

                using (var reader = selectCommand.ExecuteReader())
                {
                    // Kiểm tra xem có dữ liệu trả về không
                    if (reader.HasRows)
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                int cartId = (int)reader["CartId"];
                                decimal totalAmount = (decimal)reader["TotalAmount"];
                                DateTime invoiceDate = DateTime.Now;
                                int status = 1; // Đánh dấu hóa đơn đã được tạo

                                using (var insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@CartId", cartId);
                                    insertCommand.Parameters.AddWithValue("@UserId", userId);
                                    insertCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
                                    insertCommand.Parameters.AddWithValue("@InvoiceDate", invoiceDate);
                                    insertCommand.Parameters.AddWithValue("@Status", status);

                                    insertCommand.ExecuteNonQuery();
                                }
                                // Sau khi thêm hóa đơn, xóa cartId trong bảng Invoice
                                string updateInvoiceQuery = "UPDATE Invoice SET cartId = NULL WHERE cartId = @CartId;";
                                using (var updateInvoiceCommand = new SqlCommand(updateInvoiceQuery, connection))
                                {
                                    updateInvoiceCommand.Parameters.AddWithValue("@CartId", cartId);
                                    updateInvoiceCommand.ExecuteNonQuery();
                                }
                            }
                        } while (reader.NextResult());
                    }
                }
            }
        }

        private void ClearCart(SqlConnection connection, int userId)
        {
            // Xóa các mục trong giỏ hàng
            string deleteQuery = "DELETE FROM Cart WHERE UserId = @UserId;";

            using (var command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.ExecuteNonQuery();
            }
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
    public class CartItem
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
