// Code-behind: Logout.cshtml.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace PetShop.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            // Xóa cookie khi người dùng đăng xuất
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.SignOutAsync();

            // Chuyển hướng người dùng đến trang đăng nhập hoặc trang chính
            return RedirectToPage("/Login");
        }
    }
}