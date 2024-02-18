using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public CreateModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public Car Car { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public IActionResult OnGet(string userId=null)
        {
            if (userId == null)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }
            Car = new Car();
            Car.UserId = userId;
            return Page();
          
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await db.Cars.AddAsync(Car);
                await db.SaveChangesAsync();
                StatusMessage = "Car has been Added Successfully";
                return RedirectToPage("Index", new { userId = Car.UserId });
            }
            return Page();
        }
    }
}
