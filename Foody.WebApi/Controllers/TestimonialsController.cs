using AutoMapper;
using Foody.WebApi.Context;
using Foody.WebApi.Dtos.TestimonialDtos;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        IMapper _mapper;
        public TestimonialsController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // 1. Bütün kateqoriyaları gətir
        [HttpGet]
        public IActionResult GetAllTestimonials()
        {
            var values = _dbContext.Testimonials.ToList();
            List<ResultTestimonialDto> result = _mapper.Map<List<ResultTestimonialDto>>(values);
            return Ok(result);
        }

        // 2. ID-yə görə tək bir kateqoriya gətir
        [HttpGet("{id}")]
        public IActionResult GetTestimonialById(int id)
        {
            var value = _dbContext.Testimonials.Find(id);
            if (value == null)
            {
                return NotFound("Testimonial not found");
            }
            return Ok(value);
        }

        // 3. Yeni kateqoriya yarat
        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial service)
        {
            _dbContext.Testimonials.Add(service);
            _dbContext.SaveChanges();
            return Ok("Testimonial created successfully");
        }

        // 4. Mövcud kateqoriyanı yenilə
        [HttpPut]
        public IActionResult UpdateTestimonial(Testimonial service)
        {
            var value = _dbContext.Testimonials.Find(service.Id);
            if (value == null)
            {
                return NotFound("Testimonial not found");
            }

            // Sahələri yeniləyirik (öz Entity sahələrinə uyğun dəyişə bilərsən)
            value.Comment = service.Comment;
            value.Title = service.Title;
            value.NameSurname = service.NameSurname;
            value.Title = service.Title;
            value.ImageUrl = service.ImageUrl;

            _dbContext.SaveChanges();
            return Ok("Testimonial updated successfully");
        }

        // 5. ID-yə görə kateqoriyanı sil
        [HttpDelete("{id}")]
        public IActionResult DeleteTestimonial(int id)
        {
            var value = _dbContext.Testimonials.Find(id);
            if (value == null)
            {
                return NotFound("Testimonial not found");
            }

            _dbContext.Testimonials.Remove(value);
            _dbContext.SaveChanges();
            return Ok("Testimonial deleted successfully");
        }
    }
}
