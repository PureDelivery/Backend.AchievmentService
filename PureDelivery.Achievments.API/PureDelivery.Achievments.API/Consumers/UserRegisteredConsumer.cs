using MassTransit;
using PureDelivery.Achievments.Application.Constants;
using PureDelivery.Achievments.Application.EventHandler;
using PureDelivery.Shared.Contracts.Events.Orders;
using PureDelivery.Shared.Contracts.Events.Users;

namespace PureDelivery.Achievments.API.Consumers
{
    public class UserRegisteredConsumer(
        IEventHandler eventHandler) : IConsumer<UserRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var e = context.Message;
            var ct = context.CancellationToken;

            await eventHandler.HandleEventAsync(e.UserId, EventCriterias.Register, 1);
        }
    }
}
