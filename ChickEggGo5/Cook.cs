using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickEggGo5
{

    class Cook
    {
        public byte flag = 0;
        private SemaphoreSlim semCook = new SemaphoreSlim(2, 2);
        public TableRequests Process(TableRequests tableRequests)
        {
            semCook.Wait();
            flag = 1;
            if (tableRequests == null)
            {
                throw new Exception("Tablerequest is null!!!");
            }
            foreach (Chicken item in tableRequests.Get<Chicken>())
            {
                item.Obtain();
                item.CutUp();
                item.Cook();
            }
            foreach (Egg item in tableRequests.Get<Egg>())
            {
                item.Obtain();
                item.Crack();
                item.Discard();//TODO: don't call
                item.Cook();
                item.Dispose();
            }
            flag = 0;
            semCook.Release();
            return tableRequests;
        }
    }
}
