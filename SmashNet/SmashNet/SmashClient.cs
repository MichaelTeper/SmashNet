using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmashNet.Model;
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

        private async Task<string> GetRawJsonAsync(string objectType, string objectId, params string[] expands)
        {
            string endpoint = objectType + "/" + objectId + "?expand[]=" + String.Join("&expand[]=", expands);
            return await client.GetStringAsync(endpoint);
        }

        public async Task<Tournament> GetTournamentAsync(string tournamentId)
        {
            string json = await GetRawJsonAsync("tournament", tournamentId, "event");
            JObject JRoot = JsonConvert.DeserializeObject<JObject>(json);
            JToken JTournament = JRoot["entities"]["tournament"];
            JToken JEvent = JRoot["entities"]["event"];

            Tournament tournament = new Tournament
            {
                Id = JTournament.Value<int>("id"),
                Name = JTournament.Value<string>("name"),
                StartTime = JTournament.Value<int>("startAt"),
                EndTime = JTournament.Value<int>("endAt"),
                VenueAddress = JTournament.Value<string>("venueAddress"),
                Events = JEvent.Select(e => new Event
                {
                    Id = e.Value<int>("id"),
                    Name = e.Value<string>("name"),
                    GameName = e.Value<string>("gameName"),
                    StartTime = e.Value<int>("startAt"),
                    EndTime = e.Value<int>("endAt")
                })
            };

            return tournament;
        }
    }
}

