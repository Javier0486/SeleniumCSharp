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
                //Ruta correcta hacia el archivo appsettings.json en la carpeta Config
                string configPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");

                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException($"No se encontró el archivo de configuración en {configPath}");
                }

                //Configuracion del ConfigurationBuilder con la ruta especifica
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory) //Base es AppContext.BaseDirectory
                    .AddJsonFile(configPath, optional: false, reloadOnChange: true);

                config = builder.Build();
            }
            catch (FileNotFoundException fnfEx)
            {
                Logger.LogAction($"{fnfEx.Message}");
                throw new Exception("Error al cargar appsettings.json. Verifica que el archivo exista y tenga el formato correcto.", fnfEx);
            }
            catch (Exception ex)
            {
                Logger.LogAction($"{ex.Message}");
                throw new Exception("Error al cargar appsettings.json. Verifica que el archivo tenga el formato correcto.", ex);
            }
        }

        // Metodo para obtener la Base URL
        public static string GetBaseUrl()
        {
            string baseUrl = config.GetValue<string>("TestSettings:baseUrl");
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("El valor 'baseUrl' no está definido o está vacío en appsettings.json.");
            }
            return baseUrl;
        }

        //Metodo generico para obtener valores genericos de configuracion
        public static string GetConfigValue(string key)
        {
            string value = config.GetValue<string>(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"El valor '{key}' no está definido o está vacío en appsettings.json.");
            }
            return value;
        }
    }
}