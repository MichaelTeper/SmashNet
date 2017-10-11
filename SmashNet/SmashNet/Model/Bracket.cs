using System.Collections;
using System.Collections.Generic;

namespace SmashNet.Model
{
    public class Bracket
    {
        public int Id { get; set; }
        public int? StartTime { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}