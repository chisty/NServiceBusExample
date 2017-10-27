using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
    public class PlaceOderHandler : IHandleMessages<PlaceOrder>
    {
        private static ILog log = LogManager.GetLogger<PlaceOderHandler>();
        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId= {message.OrderId}");
            var orderPlaced = new OrderPlaced {OrderId = message.OrderId};
            return context.Publish(orderPlaced);
        }
    }
}
