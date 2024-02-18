using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]

    public class DeleteModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _db;

        public DeleteModel(SparkAuto.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType = await _db.serviceTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (ServiceType == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _db.serviceTypes.Remove(ServiceType);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool ServiceTypeExists(int id)
        {
            return _db.serviceTypes.Any(e => e.Id == id);
        }
    }
}
