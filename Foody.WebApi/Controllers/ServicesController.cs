using Foody.WebApi.Context;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ServicesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 1. Bütün kateqoriyaları gətir
        [HttpGet]
        public IActionResult GetAllServices()
        {
            var values = _dbContext.Services.ToList();
            return Ok(values);
        }

        // 2. ID-yə görə tək bir kateqoriya gətir
        [HttpGet("{id}")]
        public IActionResult GetServiceById(int id)
        {
            var value = _dbContext.Services.Find(id);
            if (value == null)
            {
                return NotFound("Service not found");
            }
            return Ok(value);
        }

        // 3. Yeni kateqoriya yarat
        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            _dbContext.Services.Add(service);
            _dbContext.SaveChanges();
            return Ok("Service created successfully");
        }

        // 4. Mövcud kateqoriyanı yenilə
        [HttpPut]
        public IActionResult UpdateService(Service service)
        {
            var value = _dbContext.Services.Find(service.Id);
            if (value == null)
            {
                return NotFound("Service not found");
            }

            // Sahələri yeniləyirik (öz Entity sahələrinə uyğun dəyişə bilərsən)
            value.IconUrl = service.IconUrl;
            value.Title = service.Title;
            value.Description = service.Description;

            _dbContext.SaveChanges();
            return Ok("Service updated successfully");
        }

        // 5. ID-yə görə kateqoriyanı sil
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var value = _dbContext.Services.Find(id);
            if (value == null)
            {
                return NotFound("Service not found");
            }

            _dbContext.Services.Remove(value);
            _dbContext.SaveChanges();
            return Ok("Service deleted successfully");
        }
    }
}
