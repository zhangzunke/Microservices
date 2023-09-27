using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms = grpcClient!.ReturnAllPlatforms();
                var commandRepository = serviceScope.ServiceProvider.GetService<ICommandRepository>();
                SeedData(commandRepository!, platforms!);
            }
        }

        private static void SeedData(ICommandRepository commandRepository, IEnumerable<Platform> platforms)
        {
            Console.WriteLine($"--> Seeding new platforms...");
            foreach (var platform in platforms)
            {
                if(!commandRepository.ExternalPlatformExist(platform.ExternalID))
                {
                    commandRepository.CreatePlatform(platform);
                }
                commandRepository.SaveChanges();
            }
        }
    }
}