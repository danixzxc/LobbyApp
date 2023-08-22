using AutoMapper;
using LobbyApp.Dto;
using LobbyApp.Models;

namespace LobbyApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Lobby, LobbyDto>();
            CreateMap<LobbyDto, Lobby>();
            CreateMap<MessageDto, Message>();
            CreateMap<Message, MessageDto>();
            CreateMap<LobbyChat, LobbyChatDto>();
            CreateMap<LobbyChatDto, LobbyChat>();
            CreateMap<AccountChat, AccountChatDto>();
            CreateMap<AccountChatDto, AccountChat>();
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
        }
    }
}