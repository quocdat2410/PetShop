using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;
using System.Globalization;

namespace PetShop.Models.AdminInvoice
{
    [Authorize(Roles = "Admin")]
    public class RevenueReportModel : PageModel
    {
        private readonly PetShopContext _context;

        public RevenueReportModel(PetShopContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get; set; }
        public IList<RevenueReport> RevenueReports { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }

        public async Task OnGetAsync(int? month, int? year)
        {
            if (month == null || year == null)
            {
                // If month and year are not provided, use the current month and year
                SelectedMonth = DateTime.Now.Month;
                SelectedYear = DateTime.Now.Year;
            }
            else
            {
                SelectedMonth = month.Value;
                SelectedYear = year.Value;
            }

            Invoice = await _context.Invoices
                .Where(i => i.InvoiceDate != null && i.InvoiceDate.Value.Month == SelectedMonth && i.InvoiceDate.Value.Year == SelectedYear)
                .Include(i => i.Cart)
                .Include(i => i.User)
                .ToListAsync();

            // Calculate revenue report
            RevenueReports = Invoice
                .GroupBy(i => new { i.InvoiceDate.Value.Month, i.InvoiceDate.Value.Year })
                .Select(g => new RevenueReport
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalRevenue = g.Sum(i => i.TotalAmount ?? 0)
                })
                .ToList();
        }
    }

    public class RevenueReport
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}

