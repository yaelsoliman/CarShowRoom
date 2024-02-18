using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.ViewModel;

namespace SparkAuto.Pages.Cars
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public CustomerAndCarsViewModel CustomerAndCarVM { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }
            CustomerAndCarVM = new CustomerAndCarsViewModel()
            {
                UserObject = db.ApplicationUser.Find(userId),
                Cars =await db.Cars.Where(m=>m.UserId==userId).ToListAsync()
            };
            return Page();

        }

    }
}
