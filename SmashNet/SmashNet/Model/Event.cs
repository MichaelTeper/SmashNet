using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameName { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
