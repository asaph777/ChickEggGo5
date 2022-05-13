using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickEggGo5
{
    class TableRequests : Dictionary<string, List<IMenuItem>>
    {
        public void Add<T>(string nameCustomer) where T : IMenuItem
        {
            if (!this.ContainsKey(nameCustomer))
            {
                this.Add(nameCustomer, new List<IMenuItem>());
            }
            this[nameCustomer].Add(Activator.CreateInstance<T>());
        }

        public List<T> Get<T>()
        {
            List<T> list = new List<T>();
            foreach (var order in this.Values)
            {
                foreach (var item in order)
                {
                    if (item is T menuitem)
                    {
                        list.Add(menuitem);
                    }
                }

            }
            return list;
        }
    }
}
