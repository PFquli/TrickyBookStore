using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    public interface ISubscriptionService
    {
        IList<Subscription> GetSubscriptions(params int[] ids);

        double CalculateSubscriptionsPrice(List<Subscription> subscriptions);

        IList<Subscription> SortSubscriptionsByPriority(List<Subscription> subscriptions);

        IList<int> GetUniqueCategoryIdsFromSubscriptions(List<Subscription> subscriptions);

        double GetPriceDetailForSubscriptionType(SubscriptionTypes type, string detail);
    }
}