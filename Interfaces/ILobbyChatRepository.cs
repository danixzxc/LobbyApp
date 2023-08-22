using LobbyApp.Models;

namespace LobbyApp.Interfaces
{
    public interface ILobbyChatRepository
    {
        ICollection<Message> GetMessages();
        List<LobbyChat> GetChats();
        LobbyChat GetChat(int id);
        LobbyChat GetChatByLobby(int lobbyId);
        bool ChatExtsts(int id);
        bool CreateChat(LobbyChat chat);
        bool UpdateChat(LobbyChat chat);
        bool DeleteChat(LobbyChat chat);
        bool Save();
    }
}