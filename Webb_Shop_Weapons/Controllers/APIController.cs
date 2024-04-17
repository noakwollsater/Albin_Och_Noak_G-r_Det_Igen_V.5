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
        private readonly IHttpContextAccessor _httpContextAccessor;


        public APIController(AppDbContext database, IHttpContextAccessor httpContextAccessor)
        {
            this.database = database;
            _httpContextAccessor = httpContextAccessor;
        }

        //nytt
        [HttpGet("/products")]
        public IActionResult GetProducts(string name = null, string category = null, int page = 1)
        {
            string url = $"{Request.Scheme}://{Request.Host}";

            var query = database.Weapons.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.Name == category);
            }

            const int pageSize = 10;
            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new { 
                    Name = p.Name,
                    Image = url + "/Pictures/Vapen/Rifle/" + p.Image, 
                    Price = p.Price,
                    Category = p.Category.Name,
                    Description = p.Description
                })
                .ToList();
            if (!products.Any())
            {
                return NotFound("No products found matching the criteria.");
            }

            return Ok(products);
        }


    }
}

