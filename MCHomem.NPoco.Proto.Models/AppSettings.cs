using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MCHomem.NPoco.Proto.Models
{
    public static class AppSettings
    {
        public static string SqlServerConnection { get => GetConnectionString("SqlServerConnection"); }

        public static int MaxPagging { get => Convert.ToInt32(GetValeuFromKey("MaxPagging")); }

        private static IConfigurationRoot GetAppSettings()
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                IConfigurationRoot root = builder.Build();
                return root;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetConnectionString(string key) => GetAppSettings().GetConnectionString(key);

        private static string GetValeuFromKey(string key) => GetAppSettings().GetSection(key).Value;
    }
}
