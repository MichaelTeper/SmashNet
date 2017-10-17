using Newtonsoft.Json.Linq;
using SmashNet.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Player
    {

        public static Player MakeFromJObject(JObject JBracketRoot, int playerId)
        {
            JToken JSeeds = JBracketRoot["entities"]["seeds"];

            foreach (var seed in JSeeds)
            {
                foreach (var player in seed["mutations"]["players"].Select(player => player.First()))
                {
                    if (player.Value<int>("id") == playerId)
                    {
                        return new Player
                        {
                            Id = playerId,
                            FullName = player.ValueNoNull("name"),
                            GamerTag = player.ValueNoNull("gamerTag"),
                            Prefix = player.ValueNoNull("prefix"),
                            State = player.ValueNoNull("state"),
                            CountryCode = player.ValueNoNull("country"),
                            Region = player.ValueNoNull("region")
                        };
                    }
                }
            }

            throw new BracketNotFoundException("player with id = " + playerId + " could not be found");
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string GamerTag { get; set; }
        public string Prefix { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
    }
}
