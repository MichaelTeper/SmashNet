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
    [TestClass()]
    public class SmashClientTests
    {
        public SmashClient client { get; set; }

        [TestMethod()]
        public async Task GetTournamentAsyncTest()
        {
            client = new SmashClient();
            Tournament tournament = await client.GetTournamentAsync("tbh5");
            var eventList = tournament.Events.ToList();
            var phaseList = eventList.SelectMany(e => e.Phases).ToList();

            //Tournament Data
            Assert.AreEqual(1102, tournament.Id);
            Assert.AreEqual("The Big House 5", tournament.Name);
            Assert.AreEqual(1443794400, tournament.StartTime);
            Assert.AreEqual(1444014000, tournament.EndTime);
            Assert.AreEqual("600 Town Center Dr, Dearborn, MI 48126, USA", tournament.VenueAddress);

            //Event Data
            var eventIds = tournament.Events.Select(e => e.Id);
            Assert.IsTrue(eventIds.Contains(10300));
            Assert.IsTrue(eventIds.Contains(10301));
            Assert.IsTrue(eventIds.Contains(10302));
            Assert.IsTrue(eventIds.Contains(10303));
            Assert.IsTrue(eventIds.Contains(10708));

            //Phase Data
            var event10300PhaseIds = tournament.Events.Single(e => e.Id == 10300).Phases.Select(p => p.Id);
            Assert.IsTrue(event10300PhaseIds.Count() == 3);
            Assert.IsTrue(event10300PhaseIds.Contains(1397));
            Assert.IsTrue(event10300PhaseIds.Contains(1398));
            Assert.IsTrue(event10300PhaseIds.Contains(1399));
            var event10301PhaseIds = tournament.Events.Single(e => e.Id == 10301).Phases.Select(p => p.Id);
            Assert.IsTrue(event10301PhaseIds.Count() == 3);
            Assert.IsTrue(event10301PhaseIds.Contains(2718));
            Assert.IsTrue(event10301PhaseIds.Contains(2719));
            Assert.IsTrue(event10301PhaseIds.Contains(2804));
            var event10302PhaseIds = tournament.Events.Single(e => e.Id == 10302).Phases.Select(p => p.Id);
            Assert.IsTrue(event10302PhaseIds.Count() == 3);
            Assert.IsTrue(event10302PhaseIds.Contains(2720));
            Assert.IsTrue(event10302PhaseIds.Contains(2721));
            Assert.IsTrue(event10302PhaseIds.Contains(2722));
            var event10303PhaseIds = tournament.Events.Single(e => e.Id == 10303).Phases.Select(p => p.Id);
            Assert.IsTrue(event10303PhaseIds.Count() == 3);
            Assert.IsTrue(event10303PhaseIds.Contains(2723));
            Assert.IsTrue(event10303PhaseIds.Contains(2724));
            Assert.IsTrue(event10303PhaseIds.Contains(2805));
            var event10708PhaseIds = tournament.Events.Single(e => e.Id == 10708).Phases.Select(p => p.Id);
            Assert.IsTrue(event10708PhaseIds.Count() == 1);
            Assert.IsTrue(event10708PhaseIds.Contains(2891));
        }
    }
}