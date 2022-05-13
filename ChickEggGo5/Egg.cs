using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickEggGo5
{
    sealed class Egg : CoockedFood, IDisposable
    {
        public Egg() { }
        public void GetQuality() { }
        public void Crack() { }
        public void Discard() { }
        public void Dispose() => Discard();
    }
}
