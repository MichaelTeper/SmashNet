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
    public class BracketTests
    {
        public SmashClient Client { get; set; }
        public Tournament TheBigHouse { get; set; }
        public Bracket MeleeTop64 { get; set; }

        [TestInitialize]
        public async Task AssemblyInit()
        {
            Client = new SmashClient();
            TheBigHouse = await Client.GetTournamentAsync("tbh5");
            MeleeTop64 = TheBigHouse.GetBracketWithId(4106);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            MeleeTop64.Entrants = null;
            MeleeTop64.Sets = null;
        }

        [TestMethod]
        public async Task GetSetWithIdTest()
        {
            await Client.GetBracketInfoForAsync(MeleeTop64);

            Set set64209 = MeleeTop64.GetSetWithId(244268);
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