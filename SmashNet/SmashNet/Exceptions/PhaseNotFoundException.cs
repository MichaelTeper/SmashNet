using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Exceptions
{
    public class PhaseNotFoundException : Exception
    {
        public PhaseNotFoundException(string message) : base(message)
        {
        }
    }
}
