using TrickyBookStore.Models;

namespace TrickyBookStore.Services.PricingPlans
{
    public interface IPricingPlanService
    {
        PricingPlan GetPricingPlan(params int[] ids);
    }
}