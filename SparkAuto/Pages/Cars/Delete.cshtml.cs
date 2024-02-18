using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public DeleteModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public Car Car { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            Car = await db.Cars.FindAsync(id);
            return Page();

        }
        public async Task<IActionResult> OnPost()
        {

            var car =await db.Cars.FindAsync(Car.Id);
            db.Cars.Remove(car);
                await db.SaveChangesAsync();
                StatusMessage = "Car has been Deleted Successfully";
                return RedirectToPage("Index", new { userId = Car.UserId });
        }
    }
}
