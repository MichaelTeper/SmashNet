using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet
{
    public class SmashClient
    {
        private HttpClient client;

        public SmashClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://api.smash.gg/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetRawJsonAsync(string objectType, string objectId, params string[] expands)
        {
            string endpoint = objectType + "/" + objectId + "?expand[]=" + String.Join("&expand[]=", expands);
            return await client.GetStringAsync(endpoint);
        }
    }
}

