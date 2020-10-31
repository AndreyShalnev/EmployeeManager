using System.Configuration;

namespace EmployeeManager.Data
{
    public class ClientConfig : ConfigurationSection, IClientConfig
    {
        [ConfigurationProperty("BaseUrl", IsRequired = true)]
        public string BaseUrl => (string)this["BaseUrl"];
        [ConfigurationProperty("APIToken", IsRequired = true)]
        public string APIToken => (string)this["APIToken"];
    }
}
