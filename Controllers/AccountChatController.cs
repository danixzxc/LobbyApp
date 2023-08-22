using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LobbyApp.Dto;
using LobbyApp.Interfaces;
using LobbyApp.Models;

namespace LobbyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountChatController : Controller
    {
        private readonly IAccountChatRepository _accountChatRepository;
        private readonly IMapper _mapper;

        public AccountChatController(IAccountChatRepository chatRepository, IMapper mapper)
        {
            _accountChatRepository = chatRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountChat>))]
        public IActionResult GetChats()
        {
            var chats = _mapper.Map<List<LobbyChatDto>>(_accountChatRepository.GetChats());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chats);
        }

        [HttpGet("{chatId}")]
        [ProducesResponseType(200, Type = typeof(AccountChat))]
        [ProducesResponseType(400)]
        public IActionResult GetChat(int chatId)
        {
            if (!_accountChatRepository.ChatExtsts(chatId))
                return NotFound();

            var chat = _mapper.Map<LobbyChatDto>(_accountChatRepository.GetChat(chatId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chat);
        }

        [HttpGet("/chat/{AccountId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(AccountChat))]
        public IActionResult GetChatByAccount(int accountId)
        {
            var chat = _mapper.Map<LobbyChatDto>(
                _accountChatRepository.GetChatByAccount(accountId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(chat);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateChat([FromBody] AccountChatDto chatCreate)
        {
            if (chatCreate == null)
                return BadRequest(ModelState);

            var chat = _accountChatRepository.GetChats()
                .Where(c => c.Name.Trim().ToUpper() == chatCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (chat != null)
            {
                ModelState.AddModelError("", "Chat already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chatMap = _mapper.Map<AccountChat>(chatCreate);

            if (!_accountChatRepository.CreateChat(chatMap))
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

            if (!_accountChatRepository.ChatExtsts(chatId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var chatMap = _mapper.Map<AccountChat>(updatedChat);

            if (!_accountChatRepository.UpdateChat(chatMap))
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
            if (!_accountChatRepository.ChatExtsts(chatId))
            {
                return NotFound();
            }

            var chatToDelete = _accountChatRepository.GetChat(chatId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_accountChatRepository.DeleteChat(chatToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting chat");
            }

            return NoContent();
        }
    }
}