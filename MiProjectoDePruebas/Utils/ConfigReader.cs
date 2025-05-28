using Microsoft.Extensions.Configuration;

namespace MiProyectoPruebas.Utils
{
    /** Single Responsibility Principle (SRP)
        each class should have only one reason to change
        ConfigReader class handles reading configuration values
        keeps code maintainable and easy to understand. Each class has a clear purpose.
    */
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot config;

        static ConfigReader()
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, "MiProjectoDePruebas", "Config", "appsettings.json");

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"configuration file was no found in {configPath}");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(configPath, optional: false, reloadOnChange: true);

            config = builder.Build();
        }

        // Metodo para obtener la Base URL
        public static string GetBaseUrl(string targetApp)
        {
            string url = config[$"BaseUrls:{targetApp}"];
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception($"Valid URL was not found for the key {targetApp} in BaseUrls");
            }
            return url;
        }

        public static (string USERNAME, string PASSWORD) GetCredentials(string targetApp)
        {
            string username = config[$"Credentials:{targetApp}:UserName"];
            string password = config[$"Credentials:{targetApp}:Password"];

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"Credentials not found for the app {targetApp}");
            }

            return (username, password);
        }

        //Metodo generico para obtener valores genericos de configuracion
        public static string GetConfigValue(string key)
        {
            string value = config[key];
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"'{key}' value is not defined or is empty in the appsettings.json.");
            }
            return value;
        }
    }
}