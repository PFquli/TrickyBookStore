using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    internal class SubscriptionService : ISubscriptionService
    {
        public SubscriptionService()
        {
        }

        private bool CheckIfThisSubscriptionIncluded(Subscription subscription, int[] ids)
        {
            int inlucludedSubscriptionIndex = Array.IndexOf(ids, subscription.Id);
            return inlucludedSubscriptionIndex > -1;
        }

        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            List<Subscription> subscriptionList = new List<Subscription>();
            List<Subscription> subscriptionsData = Store.Subscriptions.Data.ToList();
            foreach (Subscription sub in subscriptionsData)
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

        public bool CheckIfIdExist(List<int> list, int id)
        {
            return list.IndexOf(id) > -1;
        }

        public IList<int> GetUniqueCategoryIdsFromSubscriptions(List<Subscription> subscriptions)
        {
            List<int> categoryIds = new List<int>();
            foreach (Subscription sub in subscriptions)
            {
                if (sub.SubscriptionType == SubscriptionTypes.CategoryAddicted)
                {
                    int currentId = (int)sub.BookCategoryId;
                    if (!CheckIfIdExist(categoryIds, currentId))
                    {
                        categoryIds.Add(currentId);
                    }
                }
            }
            return categoryIds;
        }

        public double GetReadRateForSubscriptionType(SubscriptionTypes type)
        {
            Dictionary<SubscriptionTypes, Dictionary<string, double>> priceDetailsPerType = Store.Subscriptions.PriceDetailsPerType;
            return priceDetailsPerType[type]["ReadRate"];
        }
    }
}