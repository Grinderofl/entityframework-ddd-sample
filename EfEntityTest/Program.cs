using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfEntityTest.Migrations;

namespace EfEntityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyContext, Configuration>());
            var ctx = new MyContext();
            var random = new Random();

            // Simulates 3000 orders that may or may not have been accepted and dispatched,
            // which in turn may or may not be delivered
            using (var tx = ctx.Database.BeginTransaction())
            {
                for (int i = 0; i < 3000; i++)
                {
                    var order = new Order();

                    // Place order
                    ctx.Orders.Add(order);
                    Console.WriteLine($"Placed order {order.Id}");

                    // Accept and dispatch every other order
                    var actualChanceToProcess = random.NextDouble();
                    if (actualChanceToProcess > 0.49)
                    {

                        order.Accept();
                        Console.WriteLine($"Accepting order");

                        order.Dispatch();
                        Console.WriteLine($"Dispatching order");
                        
                        
                        // Deliver roughly 2/3rds of packages...
                        var actualChanceToDeliver = random.NextDouble();
                        if (actualChanceToDeliver > 0.6)
                        {
                            order.Complete();
                            Console.WriteLine("Order complete");
                        }
                        

                    }
                }
                ctx.SaveChanges();
                tx.Commit();
            }


            var placedOrders = ctx.Orders.Where(Order.IsPlaced);
            Console.WriteLine($"Placed orders: {placedOrders.Count()}");

            var dispatchedOrders = ctx.Orders.Where(Order.IsDispatched);
            Console.WriteLine($"Dispatched orders: {dispatchedOrders.Count()}");

            var deliveredOrders = ctx.Orders.Where(Order.IsDelivered);
            Console.WriteLine($"Delivered orders: {deliveredOrders.Count()}");

            Console.ReadKey();
        }

        
    }
}
