using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;


namespace TEST.Helpers
{
    class Configuration
    {

        public static string TPSUSBPort { get; set; }
        //TODO: Aceess file config.

        static public void LoadConf()
        {
            TPSUSBPort = ReadAppSettings("TPSUSBPort");
        }
        

        /// <summary>
        /// read params from onfig file
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadAppSettings(string key)
        {
            string rt;
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null) rt = "";
            else rt = settings[key].Value;
            return rt;
        }

        /// <summary>
        /// save params to configuration
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
