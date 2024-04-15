using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Pages
{
    public class WeaponInfoModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;

        public Weapon Weapon { get; set; }
        public Category Category { get; set; }
        public Ammo Ammo { get; set; }

        public WeaponInfoModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
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

        public IActionResult OnPost(int productId, int quantity, int viewingWeaponId)
        {
            var currentUser = database.Accounts.FirstOrDefault(a => a.ID == accessControl.LoggedInAccountID);
            if (currentUser == null)
            {
                return RedirectToPage("index");
            }

            AddItemToCart(currentUser.ID, productId, quantity);

            
            return RedirectToPage(new { id = viewingWeaponId });
        }

        public void AddItemToCart(int userID, int product, int quantity)
        {
            
            

            var shoppingCart = database.ShoppingCarts
                            .Include(s => s.Items)
                            .FirstOrDefault(s => s.AccountId == userID);

            if (shoppingCart == null)
            {
               
                shoppingCart = new ShoppingCart
                {
                    AccountId = userID,
                    Items = new List<CartItem>()
                };
                database.ShoppingCarts.Add(shoppingCart);
            }

            
            var existingItem = shoppingCart.Items.FirstOrDefault(item => item.ProductId == product);
            //kollar om det finns en likadan product, isåfall ökar endast kvantiteten 
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                //finns det ingen matchande product så skapa ett nytt cartItem med inskickade värden
                var newItem = new CartItem
                {
                    ShoppingCartId = shoppingCart.ShoppingCartId,
                    ProductId = product,
                    Quantity = quantity
                };
                shoppingCart.Items.Add(newItem);
            }
                database.SaveChanges();


            


        }
    }


}
