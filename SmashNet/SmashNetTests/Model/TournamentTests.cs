using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmashNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model.Tests
{
    [TestClass]
    public class TournamentTests
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
        public void PhasesTest()
        {
            var phaseIds = TheBigHouse.Phases.Select(phase => phase.Id);
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

        [TestMethod]
        public void GetPhasesWithNameTest()
        {
            var top8PhaseIds = TheBigHouse.GetPhasesWithName("Top 8").Select(phase => phase.Id);

            Assert.IsTrue(top8PhaseIds.Count() == 2);
            Assert.IsTrue(top8PhaseIds.Contains(2804));
            Assert.IsTrue(top8PhaseIds.Contains(2805));
        }

        [TestMethod]
        public void GetPhaseWithIdTest()
        {
            Phase phase1397 = TheBigHouse.GetPhaseWithId(1397);

            Assert.IsTrue(phase1397.Id == 1397);
            Assert.IsTrue(phase1397.Name == "Phase 1 Pools");
            Assert.IsTrue(phase1397.Order == 1);
        }
    }
}