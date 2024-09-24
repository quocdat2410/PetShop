using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Models.AdminProduct
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly PetShopContext _context;

        public IndexModel(PetShopContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.Cate).ToListAsync();
        }
    }
}
