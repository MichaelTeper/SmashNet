using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmashNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmashNet
{
    public class SmashClient
    {
        private HttpClient Client;

        public SmashClient()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("http://api.smash.gg/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<string> GetRawJsonAsync(string objectType, string objectId, params string[] expands)
        {
            string endpoint = objectType + "/" + objectId + "?expand[]=" + String.Join("&expand[]=", expands);
            return await Client.GetStringAsync(endpoint);
        }

        private async Task<JObject> GetDeserializedJsonAsync(string objectType, int objectId, params string[] expands)
        {
            return await GetDeserializedJsonAsync(objectType, objectId.ToString(), expands);
        }

        private async Task<JObject> GetDeserializedJsonAsync(string objectType, string objectId, params string[] expands)
        {
            string endpoint = objectType + "/" + objectId + "?expand[]=" + String.Join("&expand[]=", expands);
            string json = await Client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<JObject>(json);
        }

        public async Task<Tournament> GetTournamentAsync(string tournamentId)
        {
            JObject JTournamentRoot = await GetDeserializedJsonAsync("tournament", tournamentId, "event", "phase", "groups");
            Tournament tournament = Tournament.MakeFromJObject(JTournamentRoot);

            return tournament;
        }

        public async Task GetAllBracketInfoForTournamentAsync(Tournament tournament)
        {
            foreach (Bracket bracket in tournament.Brackets)
            {
                await GetBracketInfoForAsync(bracket);
            }
        }

        public async Task GetAllBracketInfoForGameAsync(Tournament tournament, Games game)
        {
            await GetAllBracketInfoForGameAsync(tournament, (int)game);
        }

        public async Task GetAllBracketInfoForGameAsync(Tournament tournament, int gameId)
        {
            var gameEvents = tournament.Events
                .Where(@event => @event.GameId == gameId);

            foreach (Event @event in gameEvents)
            {
                await GetAllBracketInfoForEventAsync(@event);
            }
        }

        public async Task GetAllBracketInfoForEventAsync(Event @event)
        {
            foreach (Phase phase in @event.Phases)
            {
                await GetAllBracketInfoForPhaseAsync(phase);
            }
        }

        public async Task GetAllBracketInfoForPhaseAsync(Phase phase)
        {
            foreach (Bracket bracket in phase.Brackets)
            {
                await GetBracketInfoForAsync(bracket);
            }
        }

        public async Task<Bracket> GetBracketInfoForAsync(Bracket bracket)
        {
            JObject JBracketRoot = await GetDeserializedJsonAsync("phase_group", bracket.Id, "sets", "seeds", "entrants");
            bracket.GetInfoFromJObject(JBracketRoot);
            return bracket;
        }
    }
}

