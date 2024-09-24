using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PetShop.Pages
{
    public class InvoiceModel : PageModel
    {
        private readonly string _connectionString;

        public List<Invoice> Invoices { get; private set; } = new List<Invoice>();

        public InvoiceModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShopPetDB");
        }

        public void OnGet()
        {
            // Lấy UserId từ cookie
            int userId;
            if (!int.TryParse(Request.Cookies["UserId"], out userId))
            {
                // Xử lý trường hợp người dùng chưa đăng nhập
                return;
            }

            // Truy vấn cơ sở dữ liệu để lấy danh sách hóa đơn của người dùng dựa trên UserId
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Invoice WHERE userId = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var invoice = new Invoice
                            {
                                InvoiceId = (int)reader["invoiceId"],
                                TotalAmount = (decimal)reader["totalAmount"],
                                InvoiceDate = (DateTime)reader["invoiceDate"],
                                Status = (int)reader["status"]
                            };
                            Invoices.Add(invoice);
                        }
                    }
                }
            }
        }
    }

    public class Invoice
    {
        public int InvoiceId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int Status { get; set; }
    }
}
