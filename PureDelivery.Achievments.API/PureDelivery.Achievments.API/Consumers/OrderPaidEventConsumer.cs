using MassTransit;
using PureDelivery.Achievments.Application.Constants;
using PureDelivery.Achievments.Application.EventHandler;
using PureDelivery.Shared.Contracts.Events.Orders;
using PureDelivery.Shared.Contracts.Events.Payments;
using System;

namespace PureDelivery.Achievments.API.Consumers
{
    public class OrderPaidEventConsumer(
        IEventHandler eventHandler) : IConsumer<OrderPaidEvent>
    {
        public async Task Consume(ConsumeContext<OrderPaidEvent> context)
        {
            var e = context.Message;
            var ct = context.CancellationToken;

            await eventHandler.HandleEventAsync(Guid.Parse(e.CustomerId), EventCriterias.OrderPaid, (double)e.Amount);

            if (e.PaymentMethod == 1)
                await eventHandler.HandleEventAsync(Guid.Parse(e.CustomerId), EventCriterias.OrderPaidCreditCard, (double)e.Amount);
            if (e.Amount > 500)
                await eventHandler.HandleEventAsync(Guid.Parse(e.CustomerId), EventCriterias.OrderMoreThan(500), (double)e.Amount);
            else if (e.Amount > 250)
                await eventHandler.HandleEventAsync(Guid.Parse(e.CustomerId), EventCriterias.OrderMoreThan(250), (double)e.Amount);
            else if (e.Amount > 100)
                await eventHandler.HandleEventAsync(Guid.Parse(e.CustomerId), EventCriterias.OrderMoreThan(100), (double)e.Amount);
        }
    }
}
