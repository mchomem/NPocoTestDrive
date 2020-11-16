using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MCHomem.NPoco.Proto.Models
{
    public static class AppSettings
    {
        public static String Get(String key)
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                IConfigurationRoot root = builder.Build();
                return root.GetConnectionString(key);
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
        }
    }
}
