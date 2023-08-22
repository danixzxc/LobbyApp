using LobbyApp.Dto;
using LobbyApp.Models;

namespace LobbyApp.Interfaces
{
    public interface ILobbyRepository
    {
        ICollection<Lobby> GetLobbies();
        Lobby GetLobby(int id);
        Lobby GetLobby(string name);
        Lobby GetLobbyTrimToUpper(LobbyDto lobbyCreate);
        LobbyChat GetChatFromLobby(int lobbyId);
        bool LobbyExists(int lobbyId);
        bool CreateLobby(Lobby lobby);
        bool UpdateLobby(Lobby lobby);
        bool DeleteLobby(Lobby lobby);
        bool Save();
    }
}
