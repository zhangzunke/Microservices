using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Data.SqlClient;
using PlatformService;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, PlatformReadDto>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
              .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcPlatformModel, Platform>()
              .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }
}