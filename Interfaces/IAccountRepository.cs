using LobbyApp.Models;

namespace LobbyApp.Interfaces
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();
        Account GetAccount(int accountId);
        AccountChat GetChatFromAccount(int accountId);
        bool AccountExists(int accountId);
        bool CreateAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Account account);
        bool Save();
    }
}
