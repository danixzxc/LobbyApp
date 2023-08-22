using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LobbyApp.Dto;
using LobbyApp.Interfaces;
using LobbyApp.Models;
using LobbyApp.Repository;

namespace LobbyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageController(IMessageRepository categoryRepository, IMapper mapper)
        {
            _messageRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LobbyChat>))]
        public IActionResult GetMessages()
        {
            var messages = _mapper.Map<List<MessageDto>>(_messageRepository.GetMessages());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(messages);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMessage([FromBody] MessageDto messageCreate)
        {
            if (messageCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var messageMap = _mapper.Map<Message>(messageCreate);

            if (!_messageRepository.CreateMessage(messageMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


    }
}