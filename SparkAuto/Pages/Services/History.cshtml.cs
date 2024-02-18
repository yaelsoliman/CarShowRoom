using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Services
{
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public HistoryModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public List<Service> Services { get; set; }
        public Car Car { get; set; }
        public IActionResult OnGet(int carId)
        {
            Services = db.Services.Where(m => m.CarId == carId).ToList();
            Car = db.Cars.Include(m => m.ApplicationUser).Where(m => m.Id == carId).FirstOrDefault();
            return Page();
        }
    }
}
