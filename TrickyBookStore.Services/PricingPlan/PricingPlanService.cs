﻿using System;
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

        private string ReadRateKey => "ReadRate";

        private string DiscountRateKey => "DiscountRate";

        private string DiscountQuotaKey => "DiscountQuota";

        private List<int> CategoryVouchers { get; set; }

        private List<double> SortedGlobalVouchers { get; set; }

        private List<int> UniqueCategoryIds { get; set; }

        private double GlobalReadRate { get; set; }

        private double CategoryReadRate { get; set; }

        private double FullPriceRate { get; set; }

        public PricingPlanService(ISubscriptionService subscription)
        {
            Subscription = subscription;
            CategoryReadRate = GetCategoryAddictedReadRate();
            FullPriceRate = 100;
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

        /// <summary>
        /// Filter out category sub type, ordered by priority
        /// </summary>
        /// <param name="ids">
        /// subscription ids of a user
        /// </param>
        /// <returns>
        /// sorted non-category subscriptions
        /// </returns>
        public List<Subscription> GetSortedNonCategorySubscriptions(int[] ids)
        {
            List<Subscription> subscriptions = Subscription.GetSubscriptions(ids).ToList();
            return (from sub in subscriptions
                    where sub.SubscriptionType != SubscriptionTypes.CategoryAddicted
                    orderby sub.Priority
                    select sub).ToList();
        }

        public List<Subscription> GetCategorySubscriptions(int[] ids)
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
        public double GetGlobalReadRate(int[] ids)
        {
            List<Subscription> sortedNonCategorySubscriptions = GetSortedNonCategorySubscriptions(ids);
            double globalReadRate = Subscription.GetReadRateForSubscriptionType(SubscriptionTypes.Free);
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
        public List<double> GetSortedGlobalVouchers(int[] ids)
        {
            List<double> sortedGlobalVouchers = new List<double>();
            List<Subscription> sortedNonCategorySubscriptions = GetSortedNonCategorySubscriptions(ids);
            foreach (Subscription sub in sortedNonCategorySubscriptions)
            {
                double discountQuota = sub.PriceDetails[DiscountQuotaKey];
                for (int i = 1; i <= discountQuota; i++)
                {
                    sortedGlobalVouchers.Add(sub.PriceDetails["DiscountRate"]);
                }
            }
            return sortedGlobalVouchers;
        }

        public List<int> GetCategoryVouchers(int[] ids)
        {
            List<Subscription>
        }

        public Dictionary<string, Dictionary<string, double>> GetPricingPlan(params int[] ids)
        {
            UniqueCategoryIds = GetUniqueCategoryIds(ids);
            GlobalReadRate = GetGlobalReadRate(ids);
            throw new NotImplementedException();
        }
    }
}