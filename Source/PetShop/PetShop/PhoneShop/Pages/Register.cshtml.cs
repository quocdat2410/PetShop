// Code-behind: Register.cshtml.cs
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
namespace PetShop.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("ShopPetDB");
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnPost(string userName, string email, string phone, string password, string role, int isAdmin)
        {
            // Kiểm tra xác thực, tránh SQL Injection bằng cách sử dụng tham số trong truy vấn
            string query = "INSERT INTO [dbo].[User] (userName, email, phone, password, role, isAdmin) VALUES (@UserName, @Email, @Phone, @Password, @Role, @IsAdmin)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@IsAdmin", isAdmin);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Đăng nhập tự động sau khi đăng ký thành công
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("UserName", userName);
                    return RedirectToPage("/Index");
                }
                else
                {
                    // Xử lý lỗi, ví dụ: thông báo cho người dùng rằng đăng ký không thành công
                    return Page();
                }
            }
        }
    }
}

