using AutoMapper;
using Foody.WebApi.Context;
using Foody.WebApi.Dtos.MessageDtos;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public MessagesController(IMapper mapper, AppDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public IActionResult GetAllMessages()
        {
            var messages = context.Messages.ToList();
            var messageDtos = mapper.Map<List<ResultMessageDto>>(messages);
            return Ok(messageDtos);
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public IActionResult GetByIdMessage(int id)
        {
            var message = context.Messages.FirstOrDefault(x => x.Id == id);
            if (message == null)
                return NotFound($"Id={id} olan mesaj tapılmadı.");

            var messageDto = mapper.Map<GetByIdMessageDto>(message);
            return Ok(messageDto);
        }

        // POST: api/Messages
        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            var message = mapper.Map<Message>(createMessageDto);

            context.Messages.Add(message);
            context.SaveChanges();

            var resultDto = mapper.Map<ResultMessageDto>(message);
            return CreatedAtAction(nameof(GetByIdMessage), new { id = message.Id }, resultDto);
        }

        // PUT: api/Messages
        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var message = context.Messages.FirstOrDefault(x => x.Id == updateMessageDto.Id);
            if (message == null)
                return NotFound($"Id={updateMessageDto.Id} olan mesaj tapılmadı.");

            mapper.Map(updateMessageDto, message);
            context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var message = context.Messages.FirstOrDefault(x => x.Id == id);
            if (message == null)
                return NotFound($"Id={id} olan mesaj tapılmadı.");

            context.Messages.Remove(message);
            context.SaveChanges();

            return NoContent();
        }
    }
}