using System.Collections.Generic;
using System.Net;

namespace I2RRexApi
{
    public class UserService
    {
        public UserService()
        {
            
        }

        public IEnumerable<User> GetUsers()
        {
            var client = new WebClient();
            var response = client.DownloadString("http://localhost:8080/Users");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(response);
        }
    }
}