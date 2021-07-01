using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.PricingPlans
{
    public interface IPricingPlanService
    {
        PricingPlan GetPricingPlan(params int[] ids);
    }
}