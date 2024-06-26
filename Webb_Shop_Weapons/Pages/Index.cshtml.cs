﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;
        public List<Weapon> weapons { get; set; } = new List<Weapon>();
        private const int PageSize = 10;
        public ShoppingCart shoppingCart { get; set; }

        public IndexModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public IList<Weapon> Weapons { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string SearchBar { get; set; }
        public string CategorySelect { get; set; }


        public IActionResult OnGet(int pageID = 0, string? searchBar = null, string? category = null)
        {
            CurrentPage = pageID;
            SearchBar = searchBar;
            CategorySelect = category;

            IQueryable<Weapon> weaponsearch = database.Weapons;

            if (!string.IsNullOrEmpty(CategorySelect) && CategorySelect != "All")
            {
                weaponsearch = weaponsearch.Where(weapon => weapon.Category.Name == CategorySelect);
            }

            if (!string.IsNullOrEmpty(SearchBar))
            {
                weaponsearch = weaponsearch.Where(weapon => weapon.Name.Contains(SearchBar));
            }
            
            var totalCount = weaponsearch.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Weapons = weaponsearch
                .OrderBy(weapon => weapon.Name)
                .Skip((CurrentPage) * PageSize)
                .Take(PageSize)
                .ToList();

            HasNextPage = CurrentPage < totalPages - 1; 
            HasPreviousPage = CurrentPage > 0;

            return Page();

            
        }



        public IActionResult OnPostAddToCart(int productId)
        {
            var currentUser = database.Accounts.FirstOrDefault(a => a.ID == accessControl.LoggedInAccountID);
            if (currentUser == null)
            {
                return RedirectToPage("index");
            }

            shoppingCart = LoadUserShoppingCart(currentUser.ID);


            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { AccountId = currentUser.ID };
                database.ShoppingCarts.Add(shoppingCart);
            }


            var item = shoppingCart.Items.FirstOrDefault(i => i.Product.ProductId == productId);

            if (item != null)
            {

                item.Quantity += 1;
            }
            else
            {

                var newItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = 1,
                    ShoppingCartId = shoppingCart.ShoppingCartId
                };
                shoppingCart.Items.Add(newItem);
            }


            database.SaveChanges();

            var product = database.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                TempData["Message"] = $"{product.Name} has been added to your cart.";
            }
            return RedirectToPage();
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
