using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.PricingPlans
{
    internal class PricingPlanService : IPricingPlanService
    {
        public ISubscriptionService Subscription { get; }

        private string ReadRateKey => "ReadRate";

        private string DiscountRateKey => "DiscountRate";

        private string DiscountQuotaKey => "DiscountQuota";

        private double FullPriceRateConst => 1;

        public PricingPlanService(ISubscriptionService subscription)
        {
            Subscription = subscription;
        }

        private List<int> GetUniqueCategoryIds(params int[] ids)
        {
            List<Subscription> subscriptions = Subscription.GetSubscriptions(ids).ToList();
            return Subscription.GetUniqueCategoryIdsFromSubscriptions(subscriptions).ToList();
        }

        private double GetCategoryAddictedReadRate()
        {
            return Subscription.GetPriceDetailForSubscriptionType(SubscriptionTypes.CategoryAddicted, ReadRateKey);
        }

        private double GetCategoryAddictedDiscountRate()
        {
            return Subscription.GetPriceDetailForSubscriptionType(SubscriptionTypes.CategoryAddicted, DiscountRateKey);
        }

        /// <summary>
        /// Filter out category sub type, ordered by priority
        /// </summary>
        /// <param name="ids">
        /// subscription ids of a user
        /// </param>
        /// <returns>
        /// sorted non-category subscriptions
        /// </returns>
        private List<Subscription> GetSortedNonCategorySubscriptions(int[] ids)
        {
            List<Subscription> subscriptions = Subscription.GetSubscriptions(ids).ToList();
            return (from sub in subscriptions
                    where sub.SubscriptionType != SubscriptionTypes.CategoryAddicted
                    orderby sub.Priority
                    select sub).ToList();
        }

        private List<Subscription> GetCategorySubscriptions(int[] ids)
        {
            List<Subscription> subscriptions = Subscription.GetSubscriptions(ids).ToList();
            return (from sub in subscriptions
                    where sub.SubscriptionType == SubscriptionTypes.CategoryAddicted
                    select sub).ToList();
        }

        /// <summary>
        /// Return read rate of higest order if any, else return read rate of Free account.
        /// </summary>
        /// <param name="ids">
        /// subscription ids of a user
        /// </param>
        /// <returns>
        /// double: global read rate
        /// </returns>
        private double GetGlobalReadRate(int[] ids)
        {
            List<Subscription> sortedNonCategorySubscriptions = GetSortedNonCategorySubscriptions(ids);
            double globalReadRate = Subscription.GetPriceDetailForSubscriptionType(SubscriptionTypes.Free, ReadRateKey);
            if (sortedNonCategorySubscriptions.Count() > 0)
            {
                globalReadRate = sortedNonCategorySubscriptions.First().PriceDetails[ReadRateKey];
            }
            return globalReadRate;
        }

        /// <summary>
        /// Generate a list of discount rates from sorted non-category subscriptions.
        /// Each sub add {DiscountQuota} of its discount rates to the list.
        /// </summary>
        /// <param name="ids">
        /// subscription ids of a user
        /// </param>
        /// <returns>
        /// List of global voucher (discount rates that can applied everywhere)
        /// </returns>
        private List<double> GetSortedGlobalVouchers(int[] ids)
        {
            List<double> sortedGlobalVouchers = new List<double>();
            List<Subscription> sortedNonCategorySubscriptions = GetSortedNonCategorySubscriptions(ids);
            foreach (Subscription sub in sortedNonCategorySubscriptions)
            {
                double discountQuota = sub.PriceDetails[DiscountQuotaKey];
                for (int i = 1; i <= discountQuota; i++)
                {
                    sortedGlobalVouchers.Add(sub.PriceDetails[DiscountRateKey]);
                }
            }
            return sortedGlobalVouchers;
        }

        private List<int> GetCategoryVouchers(int[] ids)
        {
            List<int> categoryVouchers = new List<int>();
            List<Subscription> categorySubscriptions = GetCategorySubscriptions(ids);
            foreach (Subscription sub in categorySubscriptions)
            {
                double discountQuota = sub.PriceDetails[DiscountQuotaKey];
                for (int i = 1; i <= discountQuota; i++)
                {
                    categoryVouchers.Add((int)sub.BookCategoryId);
                }
            }
            return categoryVouchers;
        }

        private double GetSubscriptionPrice(int[] ids)
        {
            List<Subscription> subscriptions = Subscription.GetSubscriptions(ids).ToList();
            return Subscription.CalculateSubscriptionsPrice(subscriptions);
        }

        public PricingPlan GetPricingPlan(params int[] ids)
        {
            PricingPlan pricingPlan = new PricingPlan
            {
                CategoryReadRate = GetCategoryAddictedReadRate(),
                CategoryVouchers = GetCategoryVouchers(ids),
                UniqueCategoryIds = GetUniqueCategoryIds(ids),
                GlobalReadRate = GetGlobalReadRate(ids),
                SortedGlobalVouchers = GetSortedGlobalVouchers(ids),
                CategoryDiscountRate = GetCategoryAddictedDiscountRate(),
                FullPriceRate = FullPriceRateConst,
                SubscriptionsPrice = GetSubscriptionPrice(ids)
            };
            return pricingPlan;
        }
    }
}