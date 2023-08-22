using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LobbyApp.Dto;
using LobbyApp.Interfaces;
using LobbyApp.Models;

namespace LobbyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyChatController : Controller
    {
        private readonly ILobbyChatRepository _lobbyChatRepository;
        private readonly IMapper _mapper;

        public LobbyChatController(ILobbyChatRepository chatRepository, IMapper mapper)
        {
            _lobbyChatRepository = chatRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LobbyChat>))]
        public IActionResult GetChats()
        {
            var chats = _mapper.Map<List<LobbyChatDto>>(_lobbyChatRepository.GetChats());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chats);
        }

        [HttpGet("{chatId}")]
        [ProducesResponseType(200, Type = typeof(LobbyChat))]
        [ProducesResponseType(400)]
        public IActionResult GetChat(int chatId)
        {
            if (!_lobbyChatRepository.ChatExtsts(chatId))
                return NotFound();

            var chat = _mapper.Map<LobbyChatDto>(_lobbyChatRepository.GetChat(chatId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chat);
        }

        [HttpGet("/chat/{LobbyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(LobbyChat))]
        public IActionResult GetChatByLobby(int lobbyId)
        {
            var chat = _mapper.Map<LobbyChatDto>(
                _lobbyChatRepository.GetChatByLobby(lobbyId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(chat);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateChat([FromBody] LobbyChatDto chatCreate)
        {
            if (chatCreate == null)
                return BadRequest(ModelState);

            var chat = _lobbyChatRepository.GetChats()
                .Where(c => c.Name.Trim().ToUpper() == chatCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (chat != null)
            {
                ModelState.AddModelError("", "Chat already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chatMap = _mapper.Map<LobbyChat>(chatCreate);

            if (!_lobbyChatRepository.CreateChat(chatMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{chatId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateChat(int chatId, [FromBody] LobbyChatDto updatedChat)
        {
            if (updatedChat == null)
                return BadRequest(ModelState);

            if (chatId != updatedChat.Id)
                return BadRequest(ModelState);

            if (!_lobbyChatRepository.ChatExtsts(chatId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var chatMap = _mapper.Map<LobbyChat>(updatedChat);

            if (!_lobbyChatRepository.UpdateChat(chatMap))
            {
                ModelState.AddModelError("", "Something went wrong updating chat");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{chatId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteChat(int chatId)
        {
            if (!_lobbyChatRepository.ChatExtsts(chatId))
            {
                return NotFound();
            }

            var chatToDelete = _lobbyChatRepository.GetChat(chatId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_lobbyChatRepository.DeleteChat(chatToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting chat");
            }

            return NoContent();
        }
    }
}