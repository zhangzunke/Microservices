using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDatasServices.Http;

namespace PlatformService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            IPlatformRepository repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Gettingg Platforms...");
            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<Platform>, IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatform")]
        public ActionResult<PlatformReadDto> GetPlatform(int id)
        {
            var platform = _repository.GetPlatformById(id);
            if(platform == null)
            return NotFound();
            return Ok(_mapper.Map<Platform, PlatformReadDto>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);
             _repository.CreatePlatform(platform);
             _repository.SaveChanges();
             var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
             try
             {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
               
             }
             catch (Exception ex)
             {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
             }

             try
             {
                 var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
                 platformPublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
             }
             catch (Exception ex)
             {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
             }
             
             return CreatedAtRoute(nameof(GetPlatform), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}