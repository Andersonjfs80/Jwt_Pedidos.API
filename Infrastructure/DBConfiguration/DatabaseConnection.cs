using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.DBConfiguration
{
    public class DatabaseConnection
    {
        public static IConfiguration ConnectionConfiguration
        {
            get
            {
                var path = $"{Directory.GetParent(Directory.GetCurrentDirectory())}\\Infrastructure";
                var Configuration = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .Build();
                return Configuration;
            }
        }
    }
}
