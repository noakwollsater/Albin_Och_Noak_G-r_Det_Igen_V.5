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
        private const int PageSize = 10;
        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public IList<Weapon> Weapons { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public IActionResult OnGet(int pageID = 1)
        {
            CurrentPage = pageID;

            var totalCount = database.Weapons.Count();
            var totalPages = (int)Math.Floor(totalCount / (double)PageSize);

            Weapons = database.Weapons
                .Skip((CurrentPage) * PageSize)
                .Take(PageSize)
                .ToList();

            HasNextPage = CurrentPage < totalPages;
            HasPreviousPage = CurrentPage >= 1;

            return Page();
        }

    }
}
