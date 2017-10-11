using System.Collections;
using System.Collections.Generic;

namespace SmashNet.Model
{
    public class Phase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public ICollection<int> BracketIds { get; set; }
        public ICollection<Bracket> Brackets { get; set; }
    }
}