using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Orion.Utilities.Configuration {
    /// <summary>
    /// Exposes the configurations of the Orion framework only.
    /// This class loads the configurations from orionsettings.json file, making it available through its Settings dictionary property.
    /// Example: ConfigurationManager.Settings["Foo"] will return the string value defined by parameter "Foo" in orionsettings.json.
    /// </summary>
    public static class ConfigurationManager {
        public static Dictionary<string, string> Settings { get; set; }

        static ConfigurationManager() {
            Settings = new Dictionary<string, string>();

            LoadConfiguration();
        }

        private static void LoadConfiguration() {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("sgmsettings.json", optional: false, reloadOnChange: true).Build();
            var settings = config.GetChildren();

            foreach (var setting in settings)
                Settings.Add(setting.Key, setting.Value);
        }
    }
}
