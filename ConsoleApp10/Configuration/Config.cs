using System.Configuration;

namespace ConsoleApp10.Configuration
{
    public class Config : IConfig
    {
        public string GetBaseUrl() => ConfigurationSettings.AppSettings.GetValues("BaseUrl")[0];

        public string GetUserName() => ConfigurationSettings.AppSettings.GetValues("UserName")[0];

        public string GetPassword() => ConfigurationSettings.AppSettings.GetValues("Password")[0];
        
    }
}
