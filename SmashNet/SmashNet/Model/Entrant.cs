using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Entrant
    {
        public static Entrant MakeFromJObject(JObject JBracketRoot, int? entrantId)
        {
            JToken JSeeds = JBracketRoot["entities"]["seeds"];
            JToken JSeed = JSeeds.Single(seed => seed.Value<int>("entrantId") == entrantId);
            List<JToken> seedList = JSeeds.ToList();

            JToken JPlayers = JSeed["mutations"]["players"];
            IEnumerable<JToken> players = JPlayers.Select(player => player.First());


            if (entrantId.HasValue)
            {
                return new Entrant
                {
                    Id = entrantId.Value,
                    Players = players
                        .Select(player => Player.MakeFromJObject(JBracketRoot, player.Value<int>("id")))
                        .ToList()
                };
            }

            return null;
        }

        public int Id { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}
