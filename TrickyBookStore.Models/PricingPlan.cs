using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickyBookStore.Models
{
    public class PricingPlan
    {
        public List<int> CategoryVouchers { get; set; }

        public List<double> SortedGlobalVouchers { get; set; }

        public List<int> UniqueCategoryIds { get; set; }

        public double GlobalReadRate { get; set; }

        public double CategoryReadRate { get; set; }

        public double FullPriceRate { get; set; }

        public double CategoryDiscountRate { get; set; }

        public double SubscriptionsPrice { get; set; }
    }
}