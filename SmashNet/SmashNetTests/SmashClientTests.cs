using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmashNet;
using SmashNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Tests
{
    [TestClass]
    public class SmashClientTests
    {
        public SmashClient Client { get; set; }
        public Tournament TheBigHouse { get; set; }

        [TestInitialize]
        public async Task AssemblyInit()
        {
            Client = new SmashClient();
            TheBigHouse = await Client.GetTournamentAsync("tbh5");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            foreach (var phase in TheBigHouse.Phases)
            {
                phase.Brackets = null;
            }
        }

        [TestMethod]
        public void GetTournamentAsyncTest()
        {
            var eventList = TheBigHouse.Events.ToList();
            var phaseList = eventList.SelectMany(@event => @event.Phases).ToList();

            //Tournament Data
            Assert.AreEqual(1102, TheBigHouse.Id);
            Assert.AreEqual("The Big House 5", TheBigHouse.Name);
            Assert.AreEqual(1443794400, TheBigHouse.StartTime);
            Assert.AreEqual(1444014000, TheBigHouse.EndTime);
            Assert.AreEqual("600 Town Center Dr, Dearborn, MI 48126, USA", TheBigHouse.VenueAddress);

            //Event Data
            var eventIds = TheBigHouse.Events.Select(@event => @event.Id);
            Assert.IsTrue(eventIds.Contains(10300));
            Assert.IsTrue(eventIds.Contains(10301));
            Assert.IsTrue(eventIds.Contains(10302));
            Assert.IsTrue(eventIds.Contains(10303));
            Assert.IsTrue(eventIds.Contains(10708));

            //Phase Data
            var event10300PhaseIds = TheBigHouse.Events.Single(@event => @event.Id == 10300).Phases.Select(phase => phase.Id);
            Assert.IsTrue(event10300PhaseIds.Count() == 3);
            Assert.IsTrue(event10300PhaseIds.Contains(1397));
            Assert.IsTrue(event10300PhaseIds.Contains(1398));
            Assert.IsTrue(event10300PhaseIds.Contains(1399));
            var event10301PhaseIds = TheBigHouse.Events.Single(@event => @event.Id == 10301).Phases.Select(phase => phase.Id);
            Assert.IsTrue(event10301PhaseIds.Count() == 3);
            Assert.IsTrue(event10301PhaseIds.Contains(2718));
            Assert.IsTrue(event10301PhaseIds.Contains(2719));
            Assert.IsTrue(event10301PhaseIds.Contains(2804));
            var event10302PhaseIds = TheBigHouse.Events.Single(@event => @event.Id == 10302).Phases.Select(phase => phase.Id);
            Assert.IsTrue(event10302PhaseIds.Count() == 3);
            Assert.IsTrue(event10302PhaseIds.Contains(2720));
            Assert.IsTrue(event10302PhaseIds.Contains(2721));
            Assert.IsTrue(event10302PhaseIds.Contains(2722));
            var event10303PhaseIds = TheBigHouse.Events.Single(@event => @event.Id == 10303).Phases.Select(phase => phase.Id);
            Assert.IsTrue(event10303PhaseIds.Count() == 3);
            Assert.IsTrue(event10303PhaseIds.Contains(2723));
            Assert.IsTrue(event10303PhaseIds.Contains(2724));
            Assert.IsTrue(event10303PhaseIds.Contains(2805));
            var event10708PhaseIds = TheBigHouse.Events.Single(@event => @event.Id == 10708).Phases.Select(phase => phase.Id);
            Assert.IsTrue(event10708PhaseIds.Count() == 1);
            Assert.IsTrue(event10708PhaseIds.Contains(2891));
        }

        [TestMethod]
        public async Task GetAllBracketsForTournamentAsyncTest()
        {
            await Client.GetAllBracketsForTournamentAsync(TheBigHouse);

            var phase1397BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1397).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1397BracketIds.Count() == 64);
            var phase1398BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1398).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1398BracketIds.Count() == 16);
            var phase1399BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1399).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1399BracketIds.Count() == 1);
            var phase2718BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2718).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2718BracketIds.Count() == 16);
            var phase2719BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2719).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2719BracketIds.Count() == 2);
            var phase2804BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2804).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2804BracketIds.Count() == 1);
            var phase2720BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2720).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2720BracketIds.Count() == 32);
            var phase2721BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2721).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2721BracketIds.Count() == 8);
            var phase2722BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2722).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2722BracketIds.Count() == 1);
            var phase2723BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2723).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2723BracketIds.Count() == 8);
            var phase2724BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2724).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2724BracketIds.Count() == 2);
            var phase2805BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2805).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2805BracketIds.Count() == 1);
            var phase2891BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2891).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2891BracketIds.Count() == 1);
        }

        [TestMethod]
        public async Task GetAllBracketsForGameAsyncTest()
        {
            await Client.GetAllBracketsForGameAsync(TheBigHouse, Games.Melee);

            var phase1397BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1397).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1397BracketIds.Count() == 64);
            var phase1398BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1398).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1398BracketIds.Count() == 16);
            var phase1399BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1399).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1399BracketIds.Count() == 1);
            var phase2718BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2718).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2718BracketIds.Count() == 16);
            var phase2719BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2719).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2719BracketIds.Count() == 2);
            var phase2804BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2804).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2804BracketIds.Count() == 1);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2720).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2721).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2722).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2723).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2724).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2805).Brackets);
            var phase2891BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 2891).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase2891BracketIds.Count() == 1);
        }

        [TestMethod]
        public async Task GetAllBracketsForEventAsyncTest()
        {
            Event meleeSinglesEvent = TheBigHouse.Events.Single(@event => @event.Id == 10300);
            await Client.GetAllBracketsForEventAsync(meleeSinglesEvent);

            var phase1397BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1397).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1397BracketIds.Count() == 64);
            var phase1398BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1398).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1398BracketIds.Count() == 16);
            var phase1399BracketIds = TheBigHouse.Phases.Single(phase => phase.Id == 1399).Brackets.Select(bracket => bracket.Id);
            Assert.IsTrue(phase1399BracketIds.Count() == 1);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2718).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2719).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2804).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2720).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2721).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2722).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2723).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2724).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2805).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2891).Brackets);
        }


        [TestMethod]
        public async Task GetAllBracketsForPhaseAsyncTest()
        {
            Phase meleeTop64Phase = TheBigHouse.Phases.Single(phase => phase.Id == 1399);
            await Client.GetAllBracketsForPhaseAsync(meleeTop64Phase);

            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 1397).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 1398).Brackets);
            ICollection<Set> meleeTop64Sets = meleeTop64Phase
                                            .Brackets.Single()
                                            .Sets.ToList();
            Assert.IsTrue(meleeTop64Sets.Count() == 95, "Expected sets : 95\nActual sets : " + meleeTop64Sets.Count());
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2718).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2719).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2804).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2720).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2721).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2722).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2723).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2724).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2805).Brackets);
            Assert.IsNull(TheBigHouse.Phases.Single(phase => phase.Id == 2891).Brackets);
        }
    }
}