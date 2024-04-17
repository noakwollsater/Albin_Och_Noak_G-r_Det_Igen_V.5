using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webb_Shop_Weapons.Data;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Controllers
{
    [Route("/api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext database;

        public APIController(AppDbContext database)
        {
            this.database = database;
        }

        [HttpGet("/products")]
        public List<Weapon> GetProducts()
        {
            return database.Weapons.ToList();
        }

        [HttpGet("/categories")]
        public List<Category> GetCategories()
        {
            return database.Categories.ToList();
        }


        [HttpGet("/products/{id}")]
        public Weapon GetProduct(int id)
        {
            return database.Weapons.Include(p => p.Category).Include(p => p.Ammo).Single(p => p.ProductId == id);
        }

        [HttpGet("/categories/{id}")]
        public Category GetCategory(int id)
        {
            return database.Categories.Single(p => p.CategoryId == id);
        }

        [HttpPost("/products")]
        public Weapon AddProduct(Weapon product)
        {
            database.Weapons.Add(product);
            database.SaveChanges();
            return product;
        }

        [HttpPost("/categories")]
        public Category AddCategory(Category category)
        {
            database.Categories.Add(category);
            database.SaveChanges();
            return category;
        }


        [HttpPut("/products/{id}")]
        public Weapon UpdateProduct(int id, Weapon product)
        {
            product.ProductId = id;
            database.Weapons.Update(product);
            database.SaveChanges();
            return product;
        }

        [HttpPut("/categories/{id}")]
        public Category UpdateCategory(int id, Category category)
        {
            category.CategoryId = id;
            database.Categories.Update(category);
            database.SaveChanges();
            return category;
        }


        [HttpDelete("/products/{id}")]
        public void DeleteProduct(int id)
        {
            database.Weapons.Remove(database.Weapons.Single(p => p.ProductId == id));
            database.SaveChanges();
        }

        [HttpDelete("/categories/{id}")]
        public void DeleteCategory(int id)
        {
            database.Categories.Remove(database.Categories.Single(p => p.CategoryId == id));
            database.SaveChanges();
        }

        [HttpPost("/cart")]
        public CartItem AddToCart(CartItem cartItem)
        {
            database.CartItems.Add(cartItem);
            database.SaveChanges();
            return cartItem;
        }

        [HttpGet("/cart/{id}")]
        public List<CartItem> GetCart(int id)
        {
            return database.CartItems.Include(p => p.Product).Where(p => p.ShoppingCartId == id).ToList();
        }

        [HttpDelete("/cart/{id}")]
        public void RemoveFromCart(int id)
        {
            database.CartItems.Remove(database.CartItems.Single(p => p.CartItemId == id));
            database.SaveChanges();
        }


    }
}
