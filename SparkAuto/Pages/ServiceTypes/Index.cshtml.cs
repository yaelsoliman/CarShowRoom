using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IList<ServiceType> ServiceTypes { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ServiceTypes = await db.serviceTypes.ToListAsync();
            return Page();
        }
    }
}
