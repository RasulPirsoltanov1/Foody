using Foody.WebApi.Context;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CategoriesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 1. Bütün kateqoriyaları gətir
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var values = _dbContext.Categories.Include(c => c.Products).ToList();
            return Ok(values);
        }

        // 2. ID-yə görə tək bir kateqoriya gətir
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var value = _dbContext.Categories.Find(id);
            if (value == null)
            {
                return NotFound("Category not found");
            }
            return Ok(value);
        }

        // 3. Yeni kateqoriya yarat
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return Ok("Category created successfully");
        }

        // 4. Mövcud kateqoriyanı yenilə
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var value = _dbContext.Categories.Find(category.Id);
            if (value == null)
            {
                return NotFound("Category not found");
            }

            // Sahələri yeniləyirik (öz Entity sahələrinə uyğun dəyişə bilərsən)
            value.CategoryName = category.CategoryName;

            _dbContext.SaveChanges();
            return Ok("Category updated successfully");
        }

        // 5. ID-yə görə kateqoriyanı sil
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var value = _dbContext.Categories.Find(id);
            if (value == null)
            {
                return NotFound("Category not found");
            }

            _dbContext.Categories.Remove(value);
            _dbContext.SaveChanges();
            return Ok("Category deleted successfully");
        }
    }
}