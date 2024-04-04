using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        public List<Weapon> weapons { get; set; } = new List<Weapon>();
        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public bool prevPage { get; set; }
        public bool nextPage { get; set; }
        public int currentPage { get; set; }

        public void OnGet(int page = 1)
        {
            int weaponCount = 10;
            int totalWeapons = database.Weapons.Count();
            int totalPages = (int)Math.Ceiling((double)totalWeapons / weaponCount);
            currentPage = page;
            int startIndex = (page - 1) * weaponCount;
            weapons = database.Weapons.Skip(startIndex).Take(weaponCount).ToList();
            prevPage = page > 1;
            nextPage = page < totalPages;
        }
        public IActionResult OnGetPrevPage(int page)
        {
            return RedirectToPage("/Index.cshtml", new { currentPage = page - 1 });
        }
        public IActionResult OnGetNextPage(int page)
        {
            return RedirectToPage("/Index", new { page = page + 1 });
        }
    }
}
