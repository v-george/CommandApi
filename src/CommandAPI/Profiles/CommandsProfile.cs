using AutoMapper;
using src.CommandAPI.Dtos;
using src.CommandAPI.Models;

namespace src.CommandAPI.Profiles
{
    public class CommandsProfile: Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}