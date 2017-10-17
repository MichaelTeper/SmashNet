using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet.Exceptions
{
    class SetNotFoundException : Exception
    {
        public SetNotFoundException(string message) : base(message)
        {

        }
    }
}
