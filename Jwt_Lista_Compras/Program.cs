using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Lista_Compras
{
    //https://docs.microsoft.com/pt-br/dotnet/core/docker/build-container?tabs=windows
    //Criar
    //dotnet new console -o App -n DotNet.Docker
    //dotnet run
    //dotnet publish -c Release
    //docker build -t counter-image -f Dockerfile .
    //docker create --name core-counter counter-image
    //docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
