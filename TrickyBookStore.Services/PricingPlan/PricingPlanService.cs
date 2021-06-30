using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.PricingPlan
{
    internal class PricingPlanService : IPricingPlanService
    {
        public ISubscriptionService Subscription { get; }

        private List<int> _categoryVouchers;

        private List<double> _sortedGlobalVoucher;

        private List<int> _uniqueCategoryIds;

        private double _globalReadRate;

        private readonly double _categoryReadRate;

        public PricingPlanService(ISubscriptionService subscription)
        {
            Subscription = subscription;
            _categoryReadRate = GetCategoryAddictedReadRate();
        }

        public List<int> GetUniqueCategoryIds(params int[] ids)
        {
            List<Subscription> subscriptions = Subscription.GetSubscriptions(ids).ToList();
            return Subscription.GetUniqueCategoryIdsFromSubscriptions(subscriptions).ToList();
        }

        public double GetCategoryAddictedReadRate()
        {
            return Subscription.GetReadRateForSubscriptionType(SubscriptionTypes.CategoryAddicted);
        }

        public Dictionary<string, Dictionary<string, double>> GetPricingPlan(params int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}