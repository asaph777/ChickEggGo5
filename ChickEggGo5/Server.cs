using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickEggGo5
{

    class Server
    {
        private TableRequests tablerequest = new TableRequests();
        public static int counReqs = 0;
        private SemaphoreSlim serverSem = new SemaphoreSlim(1, 1);

        public void Recieve(int countChick, int countEgg, string drink, string nameCustomer)
        {
            if (nameCustomer == "")
            {
                throw new Exception("Clients name is empty!");
            }

            serverSem.Wait();
            switch (drink)
            {
                case "Tea":
                    tablerequest.Add<Tea>(nameCustomer);
                    break;
                case "Coca-Cola":
                    tablerequest.Add<CocaCola>(nameCustomer);
                    break;
                case "Pepsi":
                    tablerequest.Add<Pepsi>(nameCustomer);
                    break;
                default:
                    tablerequest.Add<NoDrink>(nameCustomer);
                    break;
            }

            for (int i = 0; i < countChick; i++)
            {
                tablerequest.Add<Chicken>(nameCustomer);
            }
            for (int i = 0; i < countEgg; i++)
            {
                tablerequest.Add<Egg>(nameCustomer);
            }
            counReqs++;
            Thread.Sleep(500);
            serverSem.Release();
        }


        public TableRequests GetTableRequests()
        {
            if (tablerequest.Count == 0)
            {
                throw new Exception("No orders!!!");
            }
            return tablerequest;
        }

        public void Send()
        {

        }

        public List<string> Serve()
        {
            serverSem.Wait();
            List<string> results = new List<string>();
            foreach (var customer in tablerequest.OrderBy(i => i.Key))
            {
                var orders = customer.Value;
                int countchicken = 0;
                int counteggs = 0;
                string drinkName = orders[0].GetType().Name;

                orders[0].Obtain(); // берем напитки
                orders[0].Serve();  // и подаем клиенту
                orders.RemoveAt(0); // вычеркиваем из списка заказов клиента

                foreach (var order in orders)
                {
                    if (order is Chicken)
                    {
                        countchicken++;
                    }
                    if (order is Egg)
                    {
                        counteggs++;
                    }
                    order.Serve();
                }

                results.Add($"Customer Name: {customer.Key} ; Drink: {drinkName} ; Orders: " +
                    $"{countchicken} chickens, {counteggs} eggs");
            }
            results.Add("-----------------");
            tablerequest = new TableRequests();
            Thread.Sleep(500);
            serverSem.Release();
            return results;
        }
    }
}
