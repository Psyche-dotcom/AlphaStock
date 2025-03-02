using AlpaStock.Core.DTOs.Request.Subscription;
using AlpaStock.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpaStock.Core.Entities;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface ISubscriptionService
    {
        Task<ResponseDto<string>> CreateSubscriptionPlan(AddSubPlanReq req);
        Task<ResponseDto<string>> updateSubscriptionPlanDetails(UpdateSubDetails req);
        Task<ResponseDto<SubscriptionFeatures>> updateSubscriptionFeature(UpdateSubScriptionFeature req);
        Task<ResponseDto<IEnumerable<Subscription>>> RetrieveAllPlan();
        Task<ResponseDto<Subscription>> RetrievePlanFeature(string planid);
    }
}
