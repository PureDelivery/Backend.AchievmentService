using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Constants
{
    public static class EventCriterias
    {
        public const string OrderCreated = "OrderCreated";
        public const string OrderPaid = "OrderPaid";
        public const string OrderCreatedNight = "OrderCreated_Night";
        public const string OrderPaidCreditCard = "OrderPaid_CreditCard";
        public const string Register = "UserRegistered";
        public static string OrderMoreThan(double amount) => $"OrderPaidMoreThan_{amount}";

    }
}
