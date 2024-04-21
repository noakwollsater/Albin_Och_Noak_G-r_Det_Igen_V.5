using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly AppDbContext _database;
        private readonly AccessControl _accessControl;

        public ShoppingCart ShoppingCart { get; set; }
        public decimal TotalPrice { get; private set; }

        public CheckoutModel(AppDbContext database, AccessControl accessControl)
        {
            _database = database;
            _accessControl = accessControl;
        }

        public IActionResult OnGet()
        {
            var currentUser = _database.Accounts.FirstOrDefault(a => a.ID == _accessControl.LoggedInAccountID);
            if (currentUser == null)
            {
                return RedirectToPage("/Index"); 
            }

            ShoppingCart = LoadUserShoppingCart(currentUser.ID);
            TotalPrice = ShoppingCart?.Items.Sum(i => i.Quantity * i.Product.Price) ?? 0;
            Console.WriteLine(TotalPrice);

            return Page(); 
        }

        private ShoppingCart LoadUserShoppingCart(int userId)
        {
            
            return _database.ShoppingCarts
                            .Include(cart => cart.Items)
                            .ThenInclude(item => item.Product)
                            .FirstOrDefault(cart => cart.AccountId == userId);
        }
    }
}