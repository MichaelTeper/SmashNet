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
    public class EventTests
    {
        public SmashClient Client { get; set; }
        public Tournament TheBigHouse { get; set; }
        public Event MeleeSingles { get; set; }

        [TestInitialize]
        public async Task AssemblyInit()
        {
            Client = new SmashClient();
            TheBigHouse = await Client.GetTournamentAsync("tbh5");
            MeleeSingles = TheBigHouse.GetEventWithId(10300);
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
        public void BracketsTest()
        {
            var allBrackets = MeleeSingles.Brackets;
            Assert.IsTrue(allBrackets.Count() == 81, "Expected brackets : 81\nActual brackets : " + allBrackets.Count());
        }

        [TestMethod]
        public async Task SetsTest()
        {
            Assert.IsTrue(MeleeSingles.Sets.Count() == 0);

            await Client.GetAllBracketInfoForPhaseAsync(MeleeSingles.GetPhaseWithId(1397));

            var allSets = MeleeSingles.Sets;
            Assert.IsTrue(allSets.Count() == 3621, "Expected sets : 3621\nActual sets : " + allSets.Count());
        }

        [TestMethod]
        public void GetPhasesWithNameTest()
        {
            var top8PhaseIds = MeleeSingles.GetPhasesWithName("Top 64").Select(phase => phase.Id);

            Assert.IsTrue(top8PhaseIds.Count() == 1);
            Assert.IsTrue(top8PhaseIds.Contains(1399));
        }

        [TestMethod]
        public void GetPhaseWithIdTest()
        {
            Phase phase1397 = MeleeSingles.GetPhaseWithId(1397);

            Assert.IsTrue(phase1397.Id == 1397);
            Assert.IsTrue(phase1397.Name == "Phase 1 Pools");
            Assert.IsTrue(phase1397.Order == 1);
        }

        [TestMethod]
        public void GetBracketWithIdTest()
        {
            Bracket bracket4106 = MeleeSingles.GetBracketWithId(4106);

            Assert.IsTrue(bracket4106.Id == 4106);
        }

        [TestMethod]
        public async Task GetSetWithIdTest()
        {
            await Client.GetBracketInfoForAsync(MeleeSingles.GetBracketWithId(4106));

            Set set64209 = MeleeSingles.GetSetWithId(244268);
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