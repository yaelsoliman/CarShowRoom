using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public EditModel(ApplicationDbContext db)
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
            if (ModelState.IsValid)
            {
                var carobj = await db.Cars.FindAsync(Car.Id);
                carobj.VIN = Car.VIN;
                carobj.Make = Car.Make;
                carobj.Model = Car.Model;
                carobj.Style = Car.Style;
                carobj.Year = Car.Year;
                carobj.Color = Car.Color;
                carobj.Miles = Car.Miles;
                



                await db.SaveChangesAsync();
                StatusMessage = "Car has been updated Successfully";
                return RedirectToPage("Index", new { userId = Car.UserId });
            }
            return Page();
        }
    }
}
