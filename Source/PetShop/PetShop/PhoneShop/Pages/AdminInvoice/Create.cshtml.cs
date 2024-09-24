using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models;

namespace PetShop.Models.AdminInvoice
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly PetShopContext _context;

        public CreateModel(PetShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CartId"] = new SelectList(_context.Carts, "CartId", "CartId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
