using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.EventHandler
{
    public interface IEventHandler
    {
        Task HandleEventAsync(Guid userId, string eventCriteria, double value);
    }
}
