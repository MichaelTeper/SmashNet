using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Exceptions
{
    public class BracketNotFoundException : Exception
    {
        public BracketNotFoundException(string message) : base(message)
        {
        }
    }
}
