﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickEggGo5
{
    abstract class CoockedFood : IMenuItem
    {
        public void Cook() { }
        public void Obtain() { }
        public void Serve() { }
    }
}
