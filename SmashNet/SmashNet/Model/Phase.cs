using Newtonsoft.Json.Linq;
using SmashNet.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Phase
    {
        public static Phase MakeFromJObject(JObject JTournamentRoot, int phaseId)
        {
            JToken JPhases = JTournamentRoot["entities"]["phase"];
            JToken JPhase = JPhases.Single(phase => phase.Value<int>("id") == phaseId);

            JToken JGroups = JTournamentRoot["entities"]["groups"];

            return new Phase
            {
                Id = JPhase.Value<int>("id"),
                Name = JPhase.Value<string>("name"),
                Order = JPhase.Value<int>("phaseOrder"),
                Brackets = JGroups
                    .Where(bracket => bracket.Value<int>("phaseId") == phaseId)
                    .Select(bracket => Bracket.MakeFromJObject(JTournamentRoot, bracket.Value<int>("id")))
                    .ToList()
            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public IEnumerable<Bracket> Brackets { get; set; } = new List<Bracket>();
        public IEnumerable<Set> Sets
        {
            get
            {
                return Brackets
                    .SelectMany(bracket => bracket.Sets)
                    .ToList();
            }
        }

        public Bracket GetBracketWithId(int id)
        {
            try
            {
                return Brackets.Single(bracket => bracket.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new BracketNotFoundException("bracket with id = " + id + " could not be found");
            }
        }


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
    }
}