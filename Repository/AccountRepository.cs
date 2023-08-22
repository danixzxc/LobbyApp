using LobbyApp.Data;
using LobbyApp.Interfaces;
using LobbyApp.Models;

namespace LobbyApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateAccount(Account account)
        {
            _context.Add(account);
            return Save();
        }

        public bool DeleteAccount(Account account)
        {
            _context.Remove(account);
            return Save();
        }

        public Account GetAccount(int accountId)
        {
            return _context.Accounts.Where(a => a.Id == accountId).FirstOrDefault();
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        public bool AccountExists(int accountId)
        {
            return _context.Accounts.Any(a => a.Id == accountId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAccount(Account account)
        {
            _context.Update(account);
            return Save();
        }

        public AccountChat GetChatFromAccount(int accountId)
        {
            return _context.Accounts.Where(a => a.Id == accountId).Select(c => c.AccountChat).FirstOrDefault();
        }
    }
}
