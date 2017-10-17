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
                foreach (var bracket in phase.Brackets)
                {
                    bracket.Sets = null;
                    bracket.Entrants = null;
                }
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
        public void BracketsTest()
        {
            var allBrackets = TheBigHouse.Brackets;
            Assert.IsTrue(allBrackets.Count() == 153, "Expected brackets : 153\nActual brackets : " + allBrackets.Count());
        }

        [TestMethod]
        public async Task SetsTest()
        {
            Assert.IsTrue(TheBigHouse.Sets.Count() == 0);

            await Client.GetAllBracketInfoForPhaseAsync(TheBigHouse.GetPhaseWithId(1397));

            var allSets = TheBigHouse.Sets;
            Assert.IsTrue(allSets.Count() == 3621, "Expected sets : 3621\nActual sets : " + allSets.Count());
        }

        [TestMethod]
        public void GetEventWithIdTest()
        {
            var meleeSingles = TheBigHouse.GetEventWithId(10300);

            Assert.IsTrue(meleeSingles.Id == 10300);
            Assert.IsTrue(meleeSingles.Name.Equals("Melee Singles"));
            Assert.IsTrue(meleeSingles.GameId == 1);
            Assert.IsTrue(meleeSingles.StartTime == 1443812400);
            Assert.IsTrue(meleeSingles.EndTime == 1444014000);
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

        [TestMethod]
        public void GetBracketWithIdTest()
        {
            Bracket bracket4106 = TheBigHouse.GetBracketWithId(4106);

            Assert.IsTrue(bracket4106.Id == 4106);
        }

        [TestMethod]
        public async Task GetSetWithIdTest()
        {
            await Client.GetBracketInfoForAsync(TheBigHouse.GetBracketWithId(4106));

            Set set64209 = TheBigHouse.GetSetWithId(244268);
            Assert.IsTrue(set64209.Id == 244268);
            Assert.IsTrue(set64209.Name.Equals("Winners Round 1"));
            Assert.IsTrue(set64209.Round == 1);
            Assert.IsTrue(set64209.BestOf == 3);
            Assert.IsTrue(set64209.State == 3);
            Assert.IsTrue(set64209.Entrant1Id == 14847);
            Assert.IsTrue(set64209.Entrant2Id == 12937);
            Assert.IsTrue(set64209.WinnerEntrantId == 14847);
            Assert.IsTrue(set64209.LoserEntrantId == 12937);
            Assert.IsTrue(set64209.Entrant1Score == 2);
            Assert.IsTrue(set64209.Entrant2Score == 0);
        }
    }
}