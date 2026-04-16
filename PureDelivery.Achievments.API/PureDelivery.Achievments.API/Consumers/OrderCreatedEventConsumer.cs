using MassTransit;
using PureDelivery.Achievments.Application.Constants;
using PureDelivery.Achievments.Application.EventHandler;
using PureDelivery.Shared.Contracts.Events.Orders;

namespace PureDelivery.Achievments.API.Consumers
{
    public class OrderCreatedEventConsumer(
        IEventHandler eventHandler) : IConsumer<OrderProcessedEvent>
    {
        public async Task Consume(ConsumeContext<OrderProcessedEvent> context)
        {
            var e = context.Message;
            var ct = context.CancellationToken;


            await eventHandler.HandleEventAsync(Guid.Parse(e.CustomerId), EventCriterias.OrderCreated, 1);

            var hour = e.CreatedAt.Hour;
            if (hour >= 0 && hour < 5)
            {
                await eventHandler.HandleEventAsync(Guid.Parse(e.CustomerId), EventCriterias.OrderCreatedNight, 1);
            }
        }
    }
}
