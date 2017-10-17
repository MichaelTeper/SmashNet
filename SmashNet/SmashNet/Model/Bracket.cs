using Newtonsoft.Json.Linq;
using SmashNet.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SmashNet.Model
{
    public class Bracket
    {
        public static Bracket MakeFromJObject(JObject JTournamentRoot, int bracketId)
        {
            JToken JBrackets = JTournamentRoot["entities"]["groups"];
            JToken JBracket = JBrackets
                .Single(bracket => bracket.Value<int>("id") == bracketId);

            return new Bracket
            {
                Id = bracketId,
                StartTime = JBracket.Value<int?>("startAt"),
                Entrants = new List<Entrant>(),
                Sets = new List<Set>()
            };
        }

        public int Id { get; set; }
        public int? StartTime { get; set; }
        public IEnumerable<Entrant> Entrants { get; set; }
        public IEnumerable<Set> Sets { get; set; }

        public Set GetSetWithId(int id)
        {
            try
            {
                return Sets.Single(set => set.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new SetNotFoundException("set with id = " + id + " could not be found");
            }
        }

        public void GetInfoFromJObject(JObject JBracketRoot)
        {
            JToken JSets = JBracketRoot["entities"]["sets"];
            JToken JEntrants = JBracketRoot["entities"]["entrants"];

            Entrants = JEntrants
                .Select(entrant => Entrant.MakeFromJObject(JBracketRoot, entrant.Value<int>("id")))
                .ToList();
            Sets = JSets
                .Select(set => Set.MakeFromJObject(JBracketRoot, set.Value<int>("id")))
                .Where(set => set.Entrant1Id != null || set.Entrant2Id != null)
                .ToList();
        }
    }
}