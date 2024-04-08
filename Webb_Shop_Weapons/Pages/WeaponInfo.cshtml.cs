using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Pages
{
    public class WeaponInfoModel : PageModel
    {
        private readonly AppDbContext database;

        public Weapon Weapon { get; set; }
        public Category Category { get; set; }
        public Ammo Ammo { get; set; }

        public WeaponInfoModel(AppDbContext database)
        {
            this.database = database;
        }

        public IActionResult OnGet(int id)
        {
            Weapon = database.Weapons.Find(id);

            Category = database.Categories.Find(Weapon.CategoryId);

            Ammo = database.Ammos.Find(Weapon.AmmoId);

            if (Weapon == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
