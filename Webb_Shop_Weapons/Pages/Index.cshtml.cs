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
        public string SearchBar { get; set; }


        public IActionResult OnGet(int pageID = 0, string searchBar = null)
        {
            CurrentPage = pageID;
            SearchBar = searchBar;

            IQueryable<Weapon> weaponsearch = database.Weapons;

            if (!string.IsNullOrEmpty(SearchBar))
            {
                weaponsearch = weaponsearch.Where(weapon => weapon.Name.Contains(SearchBar));
            }


            var totalCount = weaponsearch.Count();
            var totalPages = (int)Math.Floor(totalCount / (double)PageSize);


            Weapons = weaponsearch
                .Skip((CurrentPage) * PageSize)
                .Take(PageSize)
                .ToList();

            HasNextPage = CurrentPage < totalPages;
            HasPreviousPage = CurrentPage >= 1;

            return Page();
        }

    }
}
