using PlatformService.Dtos;

namespace PlatformService.SyncDatasServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}