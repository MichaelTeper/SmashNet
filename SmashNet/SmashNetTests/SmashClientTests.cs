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
        public async Task GetTournamentAsyncTestAsync()
        {
            client = new SmashClient();

            Tournament tournament = await client.GetTournamentAsync("tbh5");
            Assert.AreEqual(1102, tournament.Id);
            Assert.AreEqual("The Big House 5", tournament.Name);
            Assert.AreEqual(1443794400, tournament.StartTime);
            Assert.AreEqual(1444014000, tournament.EndTime);
            Assert.AreEqual("600 Town Center Dr, Dearborn, MI 48126, USA", tournament.VenueAddress);

            var eventids = tournament.Events.Select(e => e.Id);
            Assert.IsTrue(eventids.Contains(10300));
            Assert.IsTrue(eventids.Contains(10301));
            Assert.IsTrue(eventids.Contains(10302));
            Assert.IsTrue(eventids.Contains(10303));
            Assert.IsTrue(eventids.Contains(10708));
        }
    }
}