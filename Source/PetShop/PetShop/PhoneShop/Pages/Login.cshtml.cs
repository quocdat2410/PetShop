using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PetShop.Pages
{
    public class LoginModel : PageModel
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("ShopPetDB");
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnPost(string userName, string password)
        {
            string query = "SELECT userId, Role FROM [dbo].[User] WHERE userName = @UserName AND password = @Password";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var userId = reader["userId"].ToString();
                        var role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : null;

                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                };

                        if (!string.IsNullOrEmpty(role))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Adjust as needed
                        };

                        var principal = new ClaimsPrincipal(claimsIdentity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties).Wait();
                        var options = new CookieOptions
                        {
                            Expires = DateTime.UtcNow.AddMinutes(30), // Thời gian hết hạn của cookie
                            HttpOnly = true // Cookie không thể truy cập bằng JavaScript
                        };
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", userId.ToString(), options);

                        if (role == "Admin")
                        {
                            return RedirectToPage("/AdminIndex");
                        }
                        else
                        {
                            return RedirectToPage("/Index");
                        }
                    }
                    else
                    {
                        return Page();
                    }
                }
            }
        }

    }
}
