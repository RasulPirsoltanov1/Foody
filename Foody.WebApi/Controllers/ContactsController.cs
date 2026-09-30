using Foody.WebApi.Context;
using Foody.WebApi.Dtos.ContactDtos;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ContactsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ResultContactDto> contacts =await  _dbContext.Contacts.Select(c => new ResultContactDto
            {
                Id = c.Id,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                MapLocation = c.MapLocation,
                OpenHours = c.OpenHours
            }).ToListAsync();
            return Ok(contacts);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var contact = new Contact
            {
                Email = createContactDto.Email,
                Address = createContactDto.Address,
                Phone = createContactDto.Phone,
                MapLocation = createContactDto.MapLocation,
                OpenHours = createContactDto.OpenHours
            };
            _dbContext.Contacts.Add(contact);
            await _dbContext.SaveChangesAsync();
            return Ok("Contact created successfully");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound("Contact not found");
            }
            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();
            return Ok("Contact deleted successfully");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound("Contact not found");
            }
            return Ok(contact);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var contact = await _dbContext.Contacts.FindAsync(updateContactDto.Id);
            if (contact == null)
            {
                return NotFound("Contact not found");
            }
            contact.Email = updateContactDto.Email;
            contact.Address = updateContactDto.Address;
            contact.Phone = updateContactDto.Phone;
            contact.MapLocation = updateContactDto.MapLocation;
            contact.OpenHours = updateContactDto.OpenHours;
            await _dbContext.SaveChangesAsync();
            return Ok("Contact updated successfully"); 
        }
    }
}
