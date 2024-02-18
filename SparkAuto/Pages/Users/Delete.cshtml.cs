using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]

    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public DeleteModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        [BindProperty]

        public ApplicationUser ApplicationUser { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            ApplicationUser = await db.ApplicationUser.FindAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await db.ApplicationUser.FindAsync(ApplicationUser.Id);
            db.ApplicationUser.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
