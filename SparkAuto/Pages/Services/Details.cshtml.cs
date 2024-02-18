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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public DetailsModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Service service { get; set; }
        public List<ServiceDetails> serviceDetails { get; set; }
        public void OnGet(int serviceId)
        {
            service = db.Services.Include(m => m.Car).Include(m => m.Car.ApplicationUser)
                .Where(m => m.Id == serviceId).FirstOrDefault();

            serviceDetails = db.ServiceDetails.Where(m => m.ServiceId == serviceId).ToList();
        }
    }
}
