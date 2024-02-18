using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.ViewModel;

namespace SparkAuto.Pages.Services
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public CreateModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public CarServiceViewModel CarServiceVM { get; set; }
        public IActionResult OnGet(int carId)
        {
            CarServiceVM = new CarServiceViewModel()
            {
                Car = db.Cars.Include(m => m.ApplicationUser).Where(m => m.Id == carId).FirstOrDefault(),
                Service = new Service()
            };

            List<string> ListServiceTypesInShoppingCart = db.ServiceShoppingCarts
                .Include(m => m.ServiceType).Where(m => m.CarId == carId)
                .Select(m => m.ServiceType.Name).ToList();

            IQueryable<ServiceType> ListServiceType = from s in db.serviceTypes
                                                      where !(ListServiceTypesInShoppingCart.Contains(s.Name))
                                                      select s;

            CarServiceVM.ServiceTypes = ListServiceType.ToList();

            CarServiceVM.ServiceShoppingCarts = db.ServiceShoppingCarts.Include(m => m.ServiceType)
                .Where(m => m.CarId == carId).ToList();

            foreach (var item in CarServiceVM.ServiceShoppingCarts)
            {
                CarServiceVM.Service.TotalPrice += item.ServiceType.Price;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCart()
        {
            ServiceShoppingCart serviceShoppingCart = new ServiceShoppingCart()
            {
                CarId=CarServiceVM.Car.Id,
                ServiceTypeId=CarServiceVM.ServiceDetails.ServiceTypeId
                
            };
            await db.ServiceShoppingCarts.AddAsync(serviceShoppingCart);
            await db.SaveChangesAsync();
            return RedirectToPage("Create", new { carId = CarServiceVM.Car.Id });
        }
        public async Task<IActionResult> OnPostRemoveFromCart(int ServiceCartId)
        {
            ServiceShoppingCart serviceShoppingCart = await db.ServiceShoppingCarts.FindAsync(ServiceCartId);
            db.ServiceShoppingCarts.Remove(serviceShoppingCart);
            await db.SaveChangesAsync();
            return RedirectToPage("Create", new { carId = CarServiceVM.Car.Id });

        }
        public async Task<IActionResult> OnPost()
        {
            CarServiceVM.Service.DateAdded = DateTime.Now;
            CarServiceVM.Service.CarId = CarServiceVM.Car.Id;

            CarServiceVM.ServiceShoppingCarts = db.ServiceShoppingCarts.Include(m => m.ServiceType)
            .Where(m => m.CarId == CarServiceVM.Car.Id).ToList();

            foreach (var item in CarServiceVM.ServiceShoppingCarts)
            {
                CarServiceVM.Service.TotalPrice += item.ServiceType.Price;
            }
            await db.Services.AddAsync(CarServiceVM.Service);
            await db.SaveChangesAsync();


            foreach (var item in CarServiceVM.ServiceShoppingCarts)
            {
                ServiceDetails serviceDetails = new ServiceDetails()
                {
                    ServiceId=CarServiceVM.Service.Id,
                    ServiceName=item.ServiceType.Name,
                    ServicePrice=item.ServiceType.Price,
                    ServiceTypeId=item.ServiceTypeId
                };
                 db.ServiceDetails.Add(serviceDetails);
            }
            db.ServiceShoppingCarts.RemoveRange(CarServiceVM.ServiceShoppingCarts);

            await db.SaveChangesAsync();

            return RedirectToPage("../Cars/Index", new { userId = CarServiceVM.Car.UserId });
        }
    }
}
