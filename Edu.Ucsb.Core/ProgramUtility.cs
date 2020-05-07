using Edu.Ucsb.Core.Extensions;
using Microsoft.Extensions.Configuration;
using System;

namespace Edu.Ucsb.Core
{
    /// <summary>
    /// Defines shared routines for Studio programs
    /// </summary>
    public class ProgramUtility
    {
        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <returns></returns>
        public static IConfigurationRoot LoadConfiguration(string basePath)
        {
            return LoadConfiguration(basePath, builderModifier : null);
        }

        /// <summary>
        /// Loads the built configuration.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="requiredJsonConfigurationFiles">specify any additional JSON configuration files before build</param>
        /// <returns>Returns the built configuration.</returns>
        public static IConfigurationRoot LoadConfiguration(string basePath, params string[] requiredJsonConfigurationFiles)
        {
            return LoadConfiguration(basePath, builderModifier : null, requiredJsonConfigurationFiles);
        }

        /// <summary>
        /// Loads the built configuration.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="builderModifier">Allows modification of <see cref="ConfigurationBuilder"/> before build.</param>
        /// <param name="requiredJsonConfigurationFiles">specify any additional JSON configuration files before build</param>
        /// <returns>Returns the built configuration.</returns>
        public static IConfigurationRoot LoadConfiguration(string basePath, Func<IConfigurationBuilder, IConfigurationBuilder> builderModifier, params string[] requiredJsonConfigurationFiles)
        {

            Console.WriteLine("Loading configuration...");
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(basePath)
                .AddJsonFile("./appsettings.json", optional : false, reloadOnChange : false);

            requiredJsonConfigurationFiles.ForEachInEnumerable(i =>
            {
                builder.AddJsonFile(i, optional : false, reloadOnChange : false);
            });

            if (builderModifier != null) builder = builderModifier(builder);

            Console.WriteLine("Building configuration...");
            var configuration = builder.Build();

            return configuration;
        }
    }
}