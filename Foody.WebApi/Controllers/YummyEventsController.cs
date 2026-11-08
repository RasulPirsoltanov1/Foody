using Foody.WebApi.Context;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YummyEventsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public YummyEventsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 1. Bütün kateqoriyaları gətir
        [HttpGet]
        public IActionResult GetAllYummyEvents()
        {
            var values = _dbContext.YummyEvents.ToList();
            return Ok(values);
        }

        // 2. ID-yə görə tək bir kateqoriya gətir
        [HttpGet("{id}")]
        public IActionResult GetYummyEventById(int id)
        {
            var value = _dbContext.YummyEvents.Find(id);
            if (value == null)
            {
                return NotFound("YummyEvent not found");
            }
            return Ok(value);
        }

        // 3. Yeni kateqoriya yarat
        [HttpPost]
        public IActionResult CreateYummyEvent(YummyEvent service)
        {
            _dbContext.YummyEvents.Add(service);
            _dbContext.SaveChanges();
            return Ok("YummyEvent created successfully");
        }

        // 4. Mövcud kateqoriyanı yenilə
        [HttpPut]
        public IActionResult UpdateYummyEvent(YummyEvent service)
        {
            var value = _dbContext.YummyEvents.Find(service.Id);
            if (value == null)
            {
                return NotFound("YummyEvent not found");
            }

            // Sahələri yeniləyirik (öz Entity sahələrinə uyğun dəyişə bilərsən)
            value.ImageUrl = service.ImageUrl;
            value.Title = service.Title;
            value.Description = service.Description;

            _dbContext.SaveChanges();
            return Ok("YummyEvent updated successfully");
        }

        // 5. ID-yə görə kateqoriyanı sil
        [HttpDelete("{id}")]
        public IActionResult DeleteYummyEvent(int id)
        {
            var value = _dbContext.YummyEvents.Find(id);
            if (value == null)
            {
                return NotFound("YummyEvent not found");
            }

            _dbContext.YummyEvents.Remove(value);
            _dbContext.SaveChanges();
            return Ok("YummyEvent deleted successfully");
        }
    }
}
