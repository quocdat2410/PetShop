using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PetShop.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminIndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
