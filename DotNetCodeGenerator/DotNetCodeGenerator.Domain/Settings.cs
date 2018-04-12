using DotNetCodeGenerator.Domain.Helpers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace DotNetCodeGenerator.Domain
{
    public class Settings
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static string GetWebConfigString(string configName, string defaultValue = "")
        {
            var appValue = WebConfigurationManager.AppSettings[configName];
            if (String.IsNullOrEmpty(appValue))
            {
                Logger.Info(String.Format("Config Name {0} is using default value {1}    <add key=\"{0}\" value=\"{1}\" />", configName, defaultValue));
                return defaultValue;
            }
            else
            {
                return appValue;
            }
        }

        public static bool GetWebConfigBool(string configName, bool defaultValue = false)
        {
            //return !String.IsNullOrEmpty(WebConfigurationManager.AppSettings[configName]) ? WebConfigurationManager.AppSettings[configName].ToBool() : defaultValue;

            var configValue = defaultValue;
            if (!String.IsNullOrEmpty(WebConfigurationManager.AppSettings[configName]))
            {
                configValue = WebConfigurationManager.AppSettings[configName].ToBool();
            }
            else
            {
                Logger.Info(String.Format("Config Name {0} is using default value {1}   <add key=\"{0}\" value=\"{1}\" /> ", configName, defaultValue));
            }
            return configValue;

        }

        public static int GetWebConfigInt(string configName, int defaultValue = 0)
        {
            int configValue = -1;
            if (!String.IsNullOrEmpty(WebConfigurationManager.AppSettings[configName]))
            {
                configValue = WebConfigurationManager.AppSettings[configName].ToInt();
            }
            else
            {
                Logger.Info(String.Format("Config Name {0} is using default value {1}   <add key=\"{0}\" value=\"{1}\" /> ", configName, defaultValue));
            }
            return configValue == -1 ? defaultValue : configValue;
        }


        public static int CacheTinySeconds
        {
            get
            {
                return GetWebConfigInt("CacheTinySeconds", 1);
            }
        }


        public static int CacheShortSeconds
        {
            get
            {
                //return GetWebConfigInt("CacheShortSeconds", 1);
                return GetWebConfigInt("CacheShortSeconds", 10);
            }
        }

        public static int CacheMediumSeconds
        {
            get
            {
                return GetWebConfigInt("CacheMediumSeconds", 300);
            }
        }

        public static int CacheLongSeconds
        {
            get
            {
                return GetWebConfigInt("CacheLongSeconds", 1800);
            }
        }


    }
}
