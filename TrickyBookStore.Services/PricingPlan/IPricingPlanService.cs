using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.PricingPlan
{
    public interface IPricingPlanService
    {
        Dictionary<string, Dictionary<string, double>> GetPricingPlan(params int[] ids);
    }
}