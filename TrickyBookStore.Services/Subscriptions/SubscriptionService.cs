using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    internal class SubscriptionService : ISubscriptionService
    {
        private readonly List<Subscription> _subscriptions;

        public SubscriptionService()
        {
            _subscriptions = Store.Subscriptions.Data.ToList();
        }

        private bool CheckIfThisSubscriptionIncluded(Subscription subscription, int[] ids)
        {
            int inlucludedSubscriptionIndex = Array.IndexOf(ids, subscription.Id);
            return inlucludedSubscriptionIndex > -1;
        }

        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            List<Subscription> subscriptionList = new List<Subscription>();
            foreach (Subscription sub in _subscriptions)
            {
                if (CheckIfThisSubscriptionIncluded(sub, ids))
                {
                    subscriptionList.Add(sub);
                }
            }
            return subscriptionList;
        }

        public double CalculateSubscriptionsPrice(List<Subscription> subscriptions)
        {
            double subscriptionsPrice = 0;
            foreach (Subscription sub in subscriptions)
            {
                subscriptionsPrice += sub.PriceDetails["SubscriptionPrice"];
            }
            return subscriptionsPrice;
        }

        public IList<Subscription> SortSubscriptionsByPriority(List<Subscription> subscriptions)
        {
            return subscriptions.OrderBy(sub => sub.Priority).ToList();
        }
    }
}