using Microsoft.Extensions.Configuration;
using System.IO;

namespace Sonovate.CodeTest.Configurations
{
    public class SonovateConfigurationBuilder
    {
        public IConfiguration Build()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

      
    }
}
