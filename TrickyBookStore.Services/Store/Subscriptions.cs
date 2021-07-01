using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class Subscriptions
    {
        public static readonly Dictionary<SubscriptionTypes, Dictionary<string, double>> PriceDetailsPerType = new Dictionary<SubscriptionTypes, Dictionary<string, double>>
        {
            {
                SubscriptionTypes.Premium, new Dictionary<string, double>
                {
                    { "ReadRate", 0 },
                    { "DiscountRate", 0.85 },
                    { "DiscountQuota", 3 },
                    { "SubscriptionPrice", 200 }
                }
            },
            {
                SubscriptionTypes.CategoryAddicted, new Dictionary<string, double>
                {
                    { "ReadRate", 0 },
                    { "DiscountRate", 0.85 },
                    { "DiscountQuota", 3 },
                    { "SubscriptionPrice", 75 }
                }
            },
            {
                SubscriptionTypes.Paid, new Dictionary<string, double>
                {
                    { "ReadRate", 0.05 },
                    { "DiscountRate", 0.95 },
                    { "DiscountQuota", 3 },
                    { "SubscriptionPrice", 50 }
                }
            },
            {
                SubscriptionTypes.Free, new Dictionary<string, double>
                {
                    { "ReadRate", 0.9 },
                    { "DiscountRate", 1 },
                    { "DiscountQuota", 0 },
                    { "SubscriptionPrice", 0 }
                }
            }
        };

        public static readonly IEnumerable<Subscription> Data = new List<Subscription>
        {
            new Subscription { Id = 1, SubscriptionType = SubscriptionTypes.Paid, Priority = 3,
                PriceDetails = PriceDetailsPerType[SubscriptionTypes.Paid]
            },
            new Subscription { Id = 2, SubscriptionType = SubscriptionTypes.Free, Priority = 4,
                PriceDetails = PriceDetailsPerType[SubscriptionTypes.Free]
            },
            new Subscription { Id = 3, SubscriptionType = SubscriptionTypes.Premium, Priority = 2,
                PriceDetails = PriceDetailsPerType[SubscriptionTypes.Premium]
            },
            new Subscription { Id = 4, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 1,
                PriceDetails = PriceDetailsPerType[SubscriptionTypes.CategoryAddicted],
                // changable for testing
                BookCategoryId = 2
            },
            new Subscription { Id = 5, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 1,
                PriceDetails = PriceDetailsPerType[SubscriptionTypes.CategoryAddicted],
                BookCategoryId = 1
            },
            new Subscription { Id = 6, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 1,
                PriceDetails = PriceDetailsPerType[SubscriptionTypes.CategoryAddicted],
                BookCategoryId = 3
            }
        };
    }
}