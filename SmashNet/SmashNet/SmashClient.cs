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

        private async Task<JToken> GetDeserializedJsonEntitiesAsync(string objectType, int objectId, params string[] expands)
        {
            return await GetDeserializedJsonEntitiesAsync(objectType, objectId.ToString(), expands);
        }

        private async Task<JToken> GetDeserializedJsonEntitiesAsync(string objectType, string objectId, params string[] expands)
        {
            string endpoint = objectType + "/" + objectId + "?expand[]=" + String.Join("&expand[]=", expands);
            string json = await Client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<JObject>(json)["entities"];
        }

        public async Task<Tournament> GetTournamentAsync(string tournamentId)
        {
            JToken JEntities = await GetDeserializedJsonEntitiesAsync("tournament", tournamentId, "event", "phase", "groups");
            JToken JTournament = JEntities["tournament"];
            JToken JEvent = JEntities["event"];
            JToken JPhase = JEntities["phase"];
            JToken JGroups = JEntities["groups"];

            Tournament tournament = new Tournament
            {
                Id = JTournament.Value<int>("id"),
                Name = JTournament.Value<string>("name"),
                StartTime = JTournament.Value<int?>("startAt"),
                EndTime = JTournament.Value<int?>("endAt"),
                VenueAddress = JTournament.Value<string>("venueAddress"),
                Events = JEvent.Select(@event => new Event
                {
                    Id = @event.Value<int>("id"),
                    Name = @event.Value<string>("name"),
                    GameName = @event.Value<string>("gameName"),
                    GameId = @event.Value<int>("videogameId"),
                    StartTime = @event.Value<int?>("startAt"),
                    EndTime = @event.Value<int?>("endAt"),
                    Phases = JPhase
                        .Where(phase => phase.Value<int>("eventId") == @event.Value<int>("id"))
                        .Select(phase => new Phase
                        {
                            Id = phase.Value<int>("id"),
                            Name = phase.Value<string>("name"),
                            Order = phase.Value<int>("phaseOrder"),
                            BracketIds = JGroups
                                .Where(bracket => bracket.Value<int>("phaseId") == phase.Value<int>("id"))
                                .Select(bracket => bracket.Value<int>("id"))
                                .ToList()
                        })
                        .ToList()
                })
                .ToList()
            };

            return tournament;
        }

        public async Task GetAllBracketsForTournamentAsync(Tournament tournament)
        {
            foreach (Phase phase in tournament.Phases)
            {
                await GetAllBracketsForPhaseAsync(phase);
            }
        }

        public async Task GetAllBracketsForGameAsync(Tournament tournament, Games game)
        {
            await GetAllBracketsForGameAsync(tournament, (int)game);
        }

        public async Task GetAllBracketsForGameAsync(Tournament tournament, int gameId)
        {
            ICollection<Event> events = tournament.Events.Where(@event => @event.GameId == gameId).ToList();

            foreach(Event @event in events)
            {
                await GetAllBracketsForEventAsync(@event);
            }
        }

        public async Task GetAllBracketsForEventAsync(Event @event)
        {
            foreach (Phase phase in @event.Phases)
            {
                await GetAllBracketsForPhaseAsync(phase);
            }
        }

        public async Task GetAllBracketsForPhaseAsync(Phase phase)
        {
            phase.Brackets = new List<Bracket>();

            foreach (var bracketId in phase.BracketIds)
            {
                JToken JEntities = await GetDeserializedJsonEntitiesAsync("phase_group", bracketId, "sets", "enterants", "standings", "seeds");
                JToken JGroups = JEntities["groups"];
                phase.Brackets.Add(new Bracket
                {
                    Id = JGroups.Value<int>("id"),
                    StartTime = JGroups.Value<int?>("startAt")
                });
            }
        }
    }
}

