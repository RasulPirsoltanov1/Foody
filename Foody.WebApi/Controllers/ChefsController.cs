using Foody.WebApi.Context;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChefsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Chefs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var chefs = await _context.Chefs.AsNoTracking().ToListAsync();
            return Ok(chefs);
        }

        // GET: api/Chefs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            if (chef == null)
                return NotFound($"Id={id} olan aşpaz tapılmadı.");

            return Ok(chef);
        }

        // POST: api/Chefs
        [HttpPost]
        public async Task<IActionResult> Create(Chef chef)
        {
            _context.Chefs.Add(chef);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = chef.Id }, chef);
        }

        // PUT: api/Chefs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Chef chef)
        {
            if (id != chef.Id)
                return BadRequest("URL-dəki id ilə göndərilən id uyğun gəlmir.");

            var existing = await _context.Chefs.FindAsync(id);
            if (existing == null)
                return NotFound($"Id={id} olan aşpaz tapılmadı.");

            existing.NameSurname = chef.NameSurname;
            existing.Title = chef.Title;
            existing.Description = chef.Description;
            existing.ImageUrl = chef.ImageUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Chefs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            if (chef == null)
                return NotFound($"Id={id} olan aşpaz tapılmadı.");

            _context.Chefs.Remove(chef);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}