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
    public class LobbyController : Controller
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMapper _mapper;

        public LobbyController(ILobbyRepository lobbyRepository,
            IMapper mapper)
        {
            _lobbyRepository = lobbyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lobby>))]
        public IActionResult GetLobbies()
        {
            var lobbies = _mapper.Map<List<LobbyDto>>(_lobbyRepository.GetLobbies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lobbies);
        }

        [HttpGet("{lobbyId}")]
        [ProducesResponseType(200, Type = typeof(Lobby))]
        [ProducesResponseType(400)]
        public IActionResult GetLobbies(int lobbyId)
        {
            if (!_lobbyRepository.LobbyExists(lobbyId))
                return NotFound();

            var lobby = _mapper.Map<LobbyDto>(_lobbyRepository.GetLobby(lobbyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lobby);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLobby([FromBody] LobbyDto lobbyCreate)
        {
            if (lobbyCreate == null)
                return BadRequest(ModelState);

            var lobbies = _lobbyRepository.GetLobbyTrimToUpper(lobbyCreate);

            if (lobbies != null)
            {
                ModelState.AddModelError("", "Lobby already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lobbyMap = _mapper.Map<Lobby>(lobbyCreate);


            if (!_lobbyRepository.CreateLobby(lobbyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpGet("{lobbyId}/chat")]
        [ProducesResponseType(200, Type = typeof(Lobby))]
        [ProducesResponseType(400)]
        public IActionResult GetChatByLobby(int lobbyId)
        {
            if (!_lobbyRepository.LobbyExists(lobbyId))
            {
                return NotFound();
            }

            var chat = _mapper.Map<List<LobbyChatDto>>(
                _lobbyRepository.GetChatFromLobby(lobbyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chat);
        }

        [HttpPut("{lobbyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLobby(int lobbyId,
            [FromBody] LobbyDto updatedLobby)
        {
            if (updatedLobby == null)
                return BadRequest(ModelState);

            if (lobbyId != updatedLobby.Id)
                return BadRequest(ModelState);

            if (!_lobbyRepository.LobbyExists(lobbyId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var lobbyMap = _mapper.Map<Lobby>(updatedLobby);

            if (!_lobbyRepository.UpdateLobby(lobbyMap))
            {
                ModelState.AddModelError("", "Something went wrong updating lobby");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{lobbyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLobby(int lobbyId)
        {
            if (!_lobbyRepository.LobbyExists(lobbyId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lobbyToDelete = _lobbyRepository.GetLobby(lobbyId);

            if (!_lobbyRepository.DeleteLobby(lobbyToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}