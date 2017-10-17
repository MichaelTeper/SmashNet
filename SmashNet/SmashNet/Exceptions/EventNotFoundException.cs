using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Exceptions
{
    class EventNotFoundException : Exception
    {
        public EventNotFoundException(string message) : base(message)
        {
        }
    }
}
