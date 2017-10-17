using SmashNet;
using SmashNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        public static async Task RunAsync()
        {
            SmashClient client = new SmashClient();
            Tournament t = await client.GetTournamentAsync("tbh5");
            foreach (Event e in t.Events)
            {
                e.Id = 0;
            }
        }
    }
}
