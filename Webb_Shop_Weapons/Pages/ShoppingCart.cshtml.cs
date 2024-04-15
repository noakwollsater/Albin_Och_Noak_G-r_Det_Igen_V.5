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

        public IActionResult OnPostUpdateQuantity(int productId, int quantity)
        {
            var currentUser = database.Accounts.FirstOrDefault(a => a.ID == accessControl.LoggedInAccountID);
            if (currentUser == null)
            {
                return RedirectToPage("index");
            }

            shoppingCart = LoadUserShoppingCart(currentUser.ID);

            if (shoppingCart == null || !shoppingCart.Items.Any())
            {
                return Page();
            }

            var item = shoppingCart.Items.FirstOrDefault(i => i.Product.ProductId == productId);
            if (item != null)
            {
                if (quantity > 0)
                {

                    item.Quantity = quantity;
                    database.Update(item);
                }
                else
                {
                    shoppingCart.Items.Remove(item);
                    database.Remove(item);
                }

                database.SaveChanges();
            }

            return RedirectToPage();
        }


        public IActionResult OnPostDeleteItem(int productId)
        {
            //skulle kunna göra en metod för denna så det inte behövs upprepas
            var currentUser = database.Accounts.FirstOrDefault(a => a.ID == accessControl.LoggedInAccountID);


            if (currentUser == null)
            {
                return RedirectToPage("index");
            }


            shoppingCart = LoadUserShoppingCart(currentUser.ID);


            if (shoppingCart == null || shoppingCart.Items == null || !shoppingCart.Items.Any())
            {
                return Page();
            }


            var item = shoppingCart.Items.FirstOrDefault(i => i.Product.ProductId == productId);


            if (item == null)
            {
                return Page();
            }


            shoppingCart.Items.Remove(item);
            database.Remove(item);
            database.SaveChanges();
            return RedirectToPage();
        }
    public IActionResult OnPostDeleteAll()
    {
       
        var currentUser = database.Accounts.FirstOrDefault(a => a.ID == accessControl.LoggedInAccountID);

        if (currentUser == null)
        {
            return RedirectToPage("Index");
        }

        shoppingCart = LoadUserShoppingCart(currentUser.ID);

        if (shoppingCart == null || shoppingCart.Items == null || !shoppingCart.Items.Any())
        {
            return Page();
        }

        foreach (var item in shoppingCart.Items.ToList())
        {
            shoppingCart.Items.Remove(item);
            database.Remove(item);
        }

        database.SaveChanges();
       
        return RedirectToPage();
    }
}
}
