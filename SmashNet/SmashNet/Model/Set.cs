using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Set
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }
        public int BestOf { get; set; }
        public int State { get; set; }
        public int? EntrantId1 { get; set; }
        public int? EntrantId2 { get; set; }
        public int? WinnerEntrantId { get; set; }
        public int? LoserEntrantId { get; set; }
        public int? Entrant1Score { get; set; }
        public int? Entrant2Score { get; set; }
    }
}
