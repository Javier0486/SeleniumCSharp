using Microsoft.Extensions.Configuration;

namespace MiProyectoPruebas.Utils
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot config;

        static ConfigReader()
        {
            try
            {
                string configPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");

                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException($"No se encontró el archivo de configuración en {configPath}");
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile(configPath, optional: false, reloadOnChange: true);

                config = builder.Build();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar appsettings.json. Verifica que el archivo exista y tenga el formato correcto.", ex);
            }
        }

        public static string GetBaseUrl()
        {
            return config.GetValue<string>("TestSettings:baseUrl")
                ?? throw new Exception("baseUrl no está definido en appsettings.json");

        }
    }
}