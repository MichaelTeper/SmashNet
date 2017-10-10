using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Model
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VenueAddress { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public override string ToString()
        {
            return String.Format(
                "Id : {0}\n" +
                "Name : {1}\n" +
                "VenueAddress : {2}\n" +
                "StartTime : {3}\n" +
                "EndTime : {4}\n" +
                "Events : {5}",
                Id, Name, VenueAddress, StartTime, EndTime, String.Join(",", Events));
        }
    }
}
