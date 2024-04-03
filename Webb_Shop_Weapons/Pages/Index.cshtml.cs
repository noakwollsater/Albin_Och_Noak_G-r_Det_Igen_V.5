using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public List<Weapon> weapons { get; set; }

        public void OnGet()
        {
            weapons = database.Weapons.ToList();
        }
    }
}