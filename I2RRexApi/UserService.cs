using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace I2RRexApi
{
    public class UserService
    {
        private readonly APIConfig config ;

        public UserService(APIConfig config)
        {
            this.config = config;
        }

        public IEnumerable<User> GetUsers()
        {
            var client = new WebClient();
            var response = client.DownloadString(config.MainUrl + "/Users");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(response);
        }
    }

    public class APIConfig : ConfigurationSection
    {

        [ConfigurationProperty("MainUrl")]
        public string MainUrl
        {
            get { return this["MainUrl"] as string; }
            set { this["MainUrl"] = value; }
        }
    }
}