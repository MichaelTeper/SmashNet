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
                foreach (var bracket in phase.Brackets)
                {
                    bracket.Sets = null;
                    bracket.Entrants = null;
                }
            }
        }

        [TestMethod]
        public void GetTournamentAsyncTest()
        {
            var phases = TheBigHouse.Phases;

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
        public async Task GetAllBracketInfoForTournamentAsyncTest()
        {
            await Client.GetAllBracketInfoForTournamentAsync(TheBigHouse);

            var phase1397SetsCount = TheBigHouse.GetPhaseWithId(1397).Sets.Count();
            Assert.IsTrue(phase1397SetsCount == 3621, "Expected Set Count : 3621\nActual Set Count : " + phase1397SetsCount);
            var phase1398SetsCount = TheBigHouse.GetPhaseWithId(1398).Sets.Count();
            Assert.IsTrue(phase1398SetsCount == 320, "Expected Set Count : 320\nActual Set Count : " + phase1398SetsCount);
            var phase1399SetsCount = TheBigHouse.GetPhaseWithId(1399).Sets.Count();
            Assert.IsTrue(phase1399SetsCount == 94, "Expected Set Count : 94\nActual Set Count : " + phase1399SetsCount);
            var phase2718SetsCount = TheBigHouse.GetPhaseWithId(2718).Sets.Count();
            Assert.IsTrue(phase2718SetsCount == 480, "Expected Set Count : 480\nActual Set Count : " + phase2718SetsCount);
            var phase2719SetsCount = TheBigHouse.GetPhaseWithId(2719).Sets.Count();
            Assert.IsTrue(phase2719SetsCount == 40, "Expected Set Count : 40\nActual Set Count : " + phase2719SetsCount);
            var phase2804SetsCount = TheBigHouse.GetPhaseWithId(2804).Sets.Count();
            Assert.IsTrue(phase2804SetsCount == 10, "Expected Set Count : 10\nActual Set Count : " + phase2804SetsCount);
            var phase2720SetsCount = TheBigHouse.GetPhaseWithId(2720).Sets.Count();
            Assert.IsTrue(phase2720SetsCount == 896, "Expected Set Count : 896\nActual Set Count : " + phase2720SetsCount);
            var phase2721SetsCount = TheBigHouse.GetPhaseWithId(2721).Sets.Count();
            Assert.IsTrue(phase2721SetsCount == 160, "Expected Set Count : 160\nActual Set Count : " + phase2721SetsCount);
            var phase2722SetsCount = TheBigHouse.GetPhaseWithId(2722).Sets.Count();
            Assert.IsTrue(phase2722SetsCount == 47, "Expected Set Count : 47\nActual Set Count : " + phase2722SetsCount);
            var phase2723SetsCount = TheBigHouse.GetPhaseWithId(2723).Sets.Count();
            Assert.IsTrue(phase2723SetsCount == 224, "Expected Set Count : 224\nActual Set Count : " + phase2723SetsCount);
            var phase2724SetsCount = TheBigHouse.GetPhaseWithId(2724).Sets.Count();
            Assert.IsTrue(phase2724SetsCount == 40, "Expected Set Count : 40\nActual Set Count : " + phase2724SetsCount);
            var phase2805SetsCount = TheBigHouse.GetPhaseWithId(2805).Sets.Count();
            Assert.IsTrue(phase2805SetsCount == 10, "Expected Set Count : 10\nActual Set Count : " + phase2805SetsCount);
            var phase2891SetsCount = TheBigHouse.GetPhaseWithId(2891).Sets.Count();
            Assert.IsTrue(phase2891SetsCount == 7, "Expected Set Count : 7\nActual Set Count : " + phase2891SetsCount);
        }

        [TestMethod]
        public async Task GetAllBracketInfoForGameAsyncTest()
        {
            await Client.GetAllBracketInfoForGameAsync(TheBigHouse, Games.Melee);

            var phase1397SetsCount = TheBigHouse.GetPhaseWithId(1397).Sets.Count();
            Assert.IsTrue(phase1397SetsCount == 3621, "Expected Set Count : 3621\nActual Set Count : " + phase1397SetsCount);
            var phase1398SetsCount = TheBigHouse.GetPhaseWithId(1398).Sets.Count();
            Assert.IsTrue(phase1398SetsCount == 320, "Expected Set Count : 320\nActual Set Count : " + phase1398SetsCount);
            var phase1399SetsCount = TheBigHouse.GetPhaseWithId(1399).Sets.Count();
            Assert.IsTrue(phase1399SetsCount == 94, "Expected Set Count : 94\nActual Set Count : " + phase1399SetsCount);
            var phase2718SetsCount = TheBigHouse.GetPhaseWithId(2718).Sets.Count();
            Assert.IsTrue(phase2718SetsCount == 480, "Expected Set Count : 480\nActual Set Count : " + phase2718SetsCount);
            var phase2719SetsCount = TheBigHouse.GetPhaseWithId(2719).Sets.Count();
            Assert.IsTrue(phase2719SetsCount == 40, "Expected Set Count : 40\nActual Set Count : " + phase2719SetsCount);
            var phase2804SetsCount = TheBigHouse.GetPhaseWithId(2804).Sets.Count();
            Assert.IsTrue(phase2804SetsCount == 10, "Expected Set Count : 10\nActual Set Count : " + phase2804SetsCount);
            var phase2720SetsCount = TheBigHouse.GetPhaseWithId(2720).Sets.Count();
            Assert.IsTrue(phase2720SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2720SetsCount);
            var phase2721SetsCount = TheBigHouse.GetPhaseWithId(2721).Sets.Count();
            Assert.IsTrue(phase2721SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2721SetsCount);
            var phase2722SetsCount = TheBigHouse.GetPhaseWithId(2722).Sets.Count();
            Assert.IsTrue(phase2722SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2722SetsCount);
            var phase2723SetsCount = TheBigHouse.GetPhaseWithId(2723).Sets.Count();
            Assert.IsTrue(phase2723SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2723SetsCount);
            var phase2724SetsCount = TheBigHouse.GetPhaseWithId(2724).Sets.Count();
            Assert.IsTrue(phase2724SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2724SetsCount);
            var phase2805SetsCount = TheBigHouse.GetPhaseWithId(2805).Sets.Count();
            Assert.IsTrue(phase2805SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2805SetsCount);
            var phase2891SetsCount = TheBigHouse.GetPhaseWithId(2891).Sets.Count();
            Assert.IsTrue(phase2891SetsCount == 7, "Expected Set Count : 7\nActual Set Count : " + phase2891SetsCount);
        }

        [TestMethod]
        public async Task GetAllBracketInfoForEventAsyncTest()
        {
            await Client.GetAllBracketInfoForEventAsync(TheBigHouse.GetEventWithId(10300));

            var phase1397SetsCount = TheBigHouse.GetPhaseWithId(1397).Sets.Count();
            Assert.IsTrue(phase1397SetsCount == 3621, "Expected Set Count : 3621\nActual Set Count : " + phase1397SetsCount);
            var phase1398SetsCount = TheBigHouse.GetPhaseWithId(1398).Sets.Count();
            Assert.IsTrue(phase1398SetsCount == 320, "Expected Set Count : 320\nActual Set Count : " + phase1398SetsCount);
            var phase1399SetsCount = TheBigHouse.GetPhaseWithId(1399).Sets.Count();
            Assert.IsTrue(phase1399SetsCount == 94, "Expected Set Count : 94\nActual Set Count : " + phase1399SetsCount);
            var phase2718SetsCount = TheBigHouse.GetPhaseWithId(2718).Sets.Count();
            Assert.IsTrue(phase2718SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2718SetsCount);
            var phase2719SetsCount = TheBigHouse.GetPhaseWithId(2719).Sets.Count();
            Assert.IsTrue(phase2719SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2719SetsCount);
            var phase2804SetsCount = TheBigHouse.GetPhaseWithId(2804).Sets.Count();
            Assert.IsTrue(phase2804SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2804SetsCount);
            var phase2720SetsCount = TheBigHouse.GetPhaseWithId(2720).Sets.Count();
            Assert.IsTrue(phase2720SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2720SetsCount);
            var phase2721SetsCount = TheBigHouse.GetPhaseWithId(2721).Sets.Count();
            Assert.IsTrue(phase2721SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2721SetsCount);
            var phase2722SetsCount = TheBigHouse.GetPhaseWithId(2722).Sets.Count();
            Assert.IsTrue(phase2722SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2722SetsCount);
            var phase2723SetsCount = TheBigHouse.GetPhaseWithId(2723).Sets.Count();
            Assert.IsTrue(phase2723SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2723SetsCount);
            var phase2724SetsCount = TheBigHouse.GetPhaseWithId(2724).Sets.Count();
            Assert.IsTrue(phase2724SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2724SetsCount);
            var phase2805SetsCount = TheBigHouse.GetPhaseWithId(2805).Sets.Count();
            Assert.IsTrue(phase2805SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2805SetsCount);
            var phase2891SetsCount = TheBigHouse.GetPhaseWithId(2891).Sets.Count();
            Assert.IsTrue(phase2891SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2891SetsCount);
        }


        [TestMethod]
        public async Task GetAllBracketInfoForPhaseAsyncTest()
        {
            await Client.GetAllBracketInfoForPhaseAsync(TheBigHouse.GetPhaseWithId(1399));

            var phase1397SetsCount = TheBigHouse.GetPhaseWithId(1397).Sets.Count();
            Assert.IsTrue(phase1397SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase1397SetsCount);
            var phase1398SetsCount = TheBigHouse.GetPhaseWithId(1398).Sets.Count();
            Assert.IsTrue(phase1398SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase1398SetsCount);
            var phase1399SetsCount = TheBigHouse.GetPhaseWithId(1399).Sets.Count();
            Assert.IsTrue(phase1399SetsCount == 94, "Expected Set Count : 94\nActual Set Count : " + phase1399SetsCount);
            var phase2718SetsCount = TheBigHouse.GetPhaseWithId(2718).Sets.Count();
            Assert.IsTrue(phase2718SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2718SetsCount);
            var phase2719SetsCount = TheBigHouse.GetPhaseWithId(2719).Sets.Count();
            Assert.IsTrue(phase2719SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2719SetsCount);
            var phase2804SetsCount = TheBigHouse.GetPhaseWithId(2804).Sets.Count();
            Assert.IsTrue(phase2804SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2804SetsCount);
            var phase2720SetsCount = TheBigHouse.GetPhaseWithId(2720).Sets.Count();
            Assert.IsTrue(phase2720SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2720SetsCount);
            var phase2721SetsCount = TheBigHouse.GetPhaseWithId(2721).Sets.Count();
            Assert.IsTrue(phase2721SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2721SetsCount);
            var phase2722SetsCount = TheBigHouse.GetPhaseWithId(2722).Sets.Count();
            Assert.IsTrue(phase2722SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2722SetsCount);
            var phase2723SetsCount = TheBigHouse.GetPhaseWithId(2723).Sets.Count();
            Assert.IsTrue(phase2723SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2723SetsCount);
            var phase2724SetsCount = TheBigHouse.GetPhaseWithId(2724).Sets.Count();
            Assert.IsTrue(phase2724SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2724SetsCount);
            var phase2805SetsCount = TheBigHouse.GetPhaseWithId(2805).Sets.Count();
            Assert.IsTrue(phase2805SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2805SetsCount);
            var phase2891SetsCount = TheBigHouse.GetPhaseWithId(2891).Sets.Count();
            Assert.IsTrue(phase2891SetsCount == 0, "Expected Set Count : 0\nActual Set Count : " + phase2891SetsCount);
        }

        [TestMethod]
        public async Task GetBracketInfoForAsyncTest()
        {
            Phase meleeRound2Pools = TheBigHouse.GetPhaseWithId(1398);
            Bracket meleePoolK1 = meleeRound2Pools.GetBracketWithId(8214);

            await Client.GetBracketInfoForAsync(meleePoolK1);

            IEnumerable<Set> meleePoolK1Sets = TheBigHouse.GetPhaseWithId(1398).GetBracketWithId(8214).Sets;

            Assert.IsTrue(meleePoolK1Sets.Count() == 20, "Expected sets : 20\nActual sets : " + meleePoolK1Sets.Count());

            Phase top64Phase = TheBigHouse.GetPhaseWithId(1399);
            Bracket top64Bracket = top64Phase.GetBracketWithId(4106);

            await Client.GetBracketInfoForAsync(top64Bracket);

            IEnumerable<Set> top64BracketSets = TheBigHouse.GetPhaseWithId(1399).GetBracketWithId(4106).Sets;

            Assert.IsTrue(top64BracketSets.Count() == 94, "Expected sets : 94\nActual sets : " + top64BracketSets.Count());
        }
    }
}