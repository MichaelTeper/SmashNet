using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmashNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model.Tests
{
    [TestClass()]
    public class TournamentTests
    {
        public SmashClient client = new SmashClient();

        [TestMethod()]
        public async Task PhasesTest()
        {
            Tournament theBigHouse = await client.GetTournamentAsync("tbh5");
            var phaseIds = theBigHouse.Phases.Select(phase => phase.Id);
            Assert.IsTrue(phaseIds.Count() == 13);
            Assert.IsTrue(phaseIds.Contains(1397));
            Assert.IsTrue(phaseIds.Contains(1398));
            Assert.IsTrue(phaseIds.Contains(1399));
            Assert.IsTrue(phaseIds.Contains(2718));
            Assert.IsTrue(phaseIds.Contains(2719));
            Assert.IsTrue(phaseIds.Contains(2804));
            Assert.IsTrue(phaseIds.Contains(2720));
            Assert.IsTrue(phaseIds.Contains(2721));
            Assert.IsTrue(phaseIds.Contains(2722));
            Assert.IsTrue(phaseIds.Contains(2723));
            Assert.IsTrue(phaseIds.Contains(2724));
            Assert.IsTrue(phaseIds.Contains(2805));
            Assert.IsTrue(phaseIds.Contains(2891));
        }

        [TestMethod()]
        public async Task GetPhasesWithNameTestAsync()
        {
            Tournament theBigHouse = await client.GetTournamentAsync("tbh5");
            var top8PhaseIds = theBigHouse.GetPhasesWithName("Top 8").Select(phase => phase.Id);

            Assert.IsTrue(top8PhaseIds.Count() == 2);
            Assert.IsTrue(top8PhaseIds.Contains(2804));
            Assert.IsTrue(top8PhaseIds.Contains(2805));
        }
    }
}