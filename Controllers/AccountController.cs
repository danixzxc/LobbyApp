using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LobbyApp.Dto;
using LobbyApp.Interfaces;
using LobbyApp.Models;
namespace LobbyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountChatRepository _accountChatRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository ownerRepository,
            IAccountChatRepository accountChatRepository,
            IMapper mapper)
        {
            _accountRepository = ownerRepository;
            _accountChatRepository = accountChatRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        public IActionResult GetAccounts()
        {
            var accounts = _mapper.Map<List<AccountDto>>(_accountRepository.GetAccounts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(accounts);
        }

        [HttpGet("{accountsId}")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetAccount(int accountId)
        {
            if (!_accountRepository.AccountExists(accountId))
                return NotFound();

            var account = _mapper.Map<AccountDto>(_accountRepository.GetAccount(accountId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(account);
        }

        [HttpGet("{accountId}/chat")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetChatByAccount(int accountId)
        {
            if (!_accountRepository.AccountExists(accountId))
            {
                return NotFound();
            }

            var chat = _mapper.Map<List<LobbyChatDto>>(
                _accountRepository.GetChatFromAccount(accountId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chat);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAccount([FromBody] AccountDto account)
        {
            if (account == null)
                return BadRequest(ModelState);

            var accounts = _accountRepository.GetAccounts()
                .Where(c => c.Login.Trim().ToUpper() == account.Login.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (accounts != null)
            {
                ModelState.AddModelError("", "This login already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountMap = _mapper.Map<Account>(account);

            accountMap.AccountChat = _accountChatRepository.GetChat(account.Id);

            if (!_accountRepository.CreateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{accountId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAccount(int accountId, [FromBody] AccountDto updatedAccount)
        {
            if (updatedAccount == null)
                return BadRequest(ModelState);

            if (accountId != updatedAccount.Id)
                return BadRequest(ModelState);

            if (!_accountRepository.AccountExists(accountId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var accountMap = _mapper.Map<Account>(updatedAccount);

            if (!_accountRepository.UpdateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong updating account");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{accountId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAccount(int accountId)
        {
            if (!_accountRepository.AccountExists(accountId))
            {
                return NotFound();
            }

            var accountToDelete = _accountRepository.GetAccount(accountId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_accountRepository.DeleteAccount(accountToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting account");
            }

            return NoContent();
        }
    }
}