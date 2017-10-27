using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Shipping
{
    public class OrderBilledHandler: IHandleMessages<OrderBilled>
    {
        private static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderBilled, Order Id= {message.OrderId} - Taking to Checkout...");
            return Task.CompletedTask;
        }
    }
}
