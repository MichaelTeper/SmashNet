using Newtonsoft.Json.Linq;
using SmashNet.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{

    public class Tournament
    {
        public static Tournament MakeFromJObject(JObject JTournamentRoot)
        {
            JToken JTournament = JTournamentRoot["entities"]["tournament"];
            JToken JEvents = JTournamentRoot["entities"]["event"];

            return new Tournament
            {
                Id = JTournament.Value<int>("id"),
                Name = JTournament.Value<string>("name"),
                StartTime = JTournament.Value<int?>("startAt"),
                EndTime = JTournament.Value<int?>("endAt"),
                VenueAddress = JTournament.Value<string>("venueAddress"),
                Events = JEvents
                    .Select(@event => Event.MakeFromJObject(JTournamentRoot, @event.Value<int>("id")))
                    .ToList()
            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string VenueAddress { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Phase> Phases
        {
            get
            {
                return Events
                    .SelectMany(@event => @event.Phases)
                    .ToList();
            }
        }
        public IEnumerable<Bracket> Brackets
        {
            get
            {
                return Events
                    .SelectMany(@event => @event.Phases)
                    .SelectMany(phase => phase.Brackets)
                    .ToList();
            }
        }
        public IEnumerable<Set> Sets
        {
            get
            {
                return Events
                    .SelectMany(@event => @event.Phases)
                    .SelectMany(phase => phase.Brackets)
                    .SelectMany(bracket => bracket.Sets)
                    .ToList();
            }
        }

        public Event GetEventWithId(int id)
        {
            try
            {
                return Events.Single(@event => @event.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new EventNotFoundException("phase with id = " + id + " could not be found");
            }
        }

        public IEnumerable<Phase> GetPhasesWithName(string name)
        {
            var phasesWithName = Events
                .SelectMany(@event => @event.Phases)
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
                return Events
                    .SelectMany(@event => @event.Phases)
                    .Single(phase => phase.Id == id);
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
                return Events
                    .SelectMany(@event => @event.Phases)
                    .SelectMany(phase => phase.Brackets)
                    .Single(bracket => bracket.Id == id);
            }catch (InvalidOperationException)
            {
                throw new BracketNotFoundException("bracket with id = " + id + " coud not be found");
            }
        }

        public Set GetSetWithId(int id)
        {
            try
            {
                return Events
                    .SelectMany(@event => @event.Phases)
                    .SelectMany(phase => phase.Brackets)
                    .SelectMany(bracket => bracket.Sets)
                    .Single(set => set.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new SetNotFoundException("set with id = " + id + " could not be found");
            }
        }

        public override string ToString()
        {
            return String.Format(
                "Id : {0}\n" +
                "Name : {1}\n" +
                "VenueAddress : {2}\n" +
                "StartTime : {3}\n" +
                "EndTime : {4}\n" +
                "Events : {5}",
                Id, Name, VenueAddress, StartTime, EndTime, String.Join(",", Events));
        }
    }
}
