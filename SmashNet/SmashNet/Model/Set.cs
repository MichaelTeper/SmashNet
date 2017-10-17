using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Set
    {
        public static Set MakeFromJObject(JObject JBracketRoot, int setId)
        {
            JToken JSets = JBracketRoot["entities"]["sets"];
            JToken JSet = JSets.Single(set => set.Value<int>("id") == setId);

            return new Set
            {
                Id = JSet.Value<int>("id"),
                Name = JSet.Value<string>("fullRoundText"),
                Round = JSet.Value<int>("round"),
                BestOf = JSet.Value<int>("bestOf"),
                State = JSet.Value<int>("state"),
                Entrant1Id = JSet.Value<int?>("entrant1Id"),
                Entrant2Id = JSet.Value<int?>("entrant2Id"),
                Entrant1Score = JSet.Value<int?>("entrant1Score"),
                Entrant2Score = JSet.Value<int?>("entrant2Score"),
                WinnerEntrantId = JSet.Value<int?>("winnerId"),
                LoserEntrantId = JSet.Value<int?>("loserId")
            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }
        public int BestOf { get; set; }
        public int State { get; set; }
        public int? Entrant1Id { get; set; }
        public int? Entrant2Id { get; set; }
        public int? Entrant1Score { get; set; }
        public int? Entrant2Score { get; set; }
        public int? LoserEntrantId { get; set; }
        public int? WinnerEntrantId { get; set; }
    }
}
