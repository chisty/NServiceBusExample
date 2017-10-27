using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Routing;

namespace ClientUI
{
    class Program
    {
        private static ILog log = LogManager.GetLogger<Program>();
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }


        static async Task AsyncMain()
        {
            Console.Title = "ClientUI";
            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit....");
            await RunLoop(endpointInstance).ConfigureAwait(false);
            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {            
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.ReadLine();
                switch (key.Key)
                {
                    case ConsoleKey.P:
                        var order = new PlaceOrder {OrderId = Guid.NewGuid().ToString()};
                        log.Info($"Sending PlaceOrder command. OrderId= {order.OrderId}");
                        await endpointInstance.Send(order).ConfigureAwait(false);
                        break;
                    case ConsoleKey.Q:
                        return;
                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }            
//            Console.WriteLine("Press Enter to exit ...");
//            Console.ReadLine();
//            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
