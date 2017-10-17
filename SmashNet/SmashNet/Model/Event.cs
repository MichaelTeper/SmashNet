using Newtonsoft.Json.Linq;
using SmashNet.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Event
    {
        public static Event MakeFromJObject(JObject JTournamentRoot, int eventId)
        {
            JToken JEvents = JTournamentRoot["entities"]["event"];
            JToken JEvent = JEvents.Single(@event => @event.Value<int>("id") == eventId);

            JToken JPhases = JTournamentRoot["entities"]["phase"];

            return new Event
            {
                Id = JEvent.Value<int>("id"),
                Name = JEvent.Value<string>("name"),
                GameName = JEvent.Value<string>("gameName"),
                GameId = JEvent.Value<int>("videogameId"),
                StartTime = JEvent.Value<int?>("startAt"),
                EndTime = JEvent.Value<int?>("endAt"),
                Phases = JPhases
                    .Where(phase => phase.Value<int>("eventId") == eventId)
                    .Select(phase => Phase.MakeFromJObject(JTournamentRoot, phase.Value<int>("id")))
                    .ToList()
            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string GameName { get; set; }
        public int GameId { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public IEnumerable<Phase> Phases { get; set; }
        public IEnumerable<Bracket> Brackets
        {
            get
            {
                return Phases
                    .SelectMany(phase => phase.Brackets)
                    .ToList();
            }
        }
        public IEnumerable<Set> Sets
        {
            get
            {
                return Phases
                    .SelectMany(phase => phase.Brackets)
                    .SelectMany(bracket => bracket.Sets)
                    .ToList();
            }
        }

        public IEnumerable<Phase> GetPhasesWithName(string name)
        {
            var phasesWithName = Phases
                .Where(phase => phase.Name.Equals(name))
                .ToList();

            if (phasesWithName.Count() > 0)
                return phasesWithName;

            throw new PhaseNotFoundException("phases with name = " + name + " could not be found");
        }

        public Phase GetPhaseWithId(int id)
        {
            try
            {
                return Phases.Single(phase => phase.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new PhaseNotFoundException("phase with id = " + id + " could not be found");
            }
        }

        public Bracket GetBracketWithId(int id)
        {
            try
            {
                return Phases
                    .SelectMany(phase => phase.Brackets)
                    .Single(bracket => bracket.Id == id);
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
