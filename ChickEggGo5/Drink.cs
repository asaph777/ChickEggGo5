using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickEggGo5
{
    abstract class Drink : IMenuItem
    {
        //TODO: these methods should be abstract.
        public void Obtain() { }
        public void Serve() { }
    }
}
