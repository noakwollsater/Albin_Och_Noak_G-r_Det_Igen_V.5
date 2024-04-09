using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Protocols;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;
        public ShoppingCart shoppingCart { get; set; }

        public ShoppingCartModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public void OnGet()
        {
            var currentUser = database.Accounts.FirstOrDefault(a => a.ID == accessControl.LoggedInAccountID);
            if (currentUser == null)
            {
                RedirectToPage("index");
            }

            shoppingCart = LoadUserShoppingCart(currentUser.ID); 
        }

        public ShoppingCart LoadUserShoppingCart(int userId)
        {
            var shoppingCart = database.ShoppingCarts
                           .FirstOrDefault(cart => cart.AccountId == userId);

            if (shoppingCart != null)
            {
                shoppingCart = database.ShoppingCarts
                           .Include(cart => cart.Items)
                           .ThenInclude(item => item.Product)
                           .FirstOrDefault(cart => cart.AccountId == userId);
            }

            return shoppingCart;
        }


    }
}
