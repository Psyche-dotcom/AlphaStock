using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Subscription;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IAlphaRepository<Subscription> _subscriptionRepo;
        private readonly ILogger<SubscriptionService> _logger;
        private readonly IAlphaRepository<SubscriptionFeatures> _subscriptionFeatureRepo;
        public SubscriptionService(IAlphaRepository<SubscriptionFeatures> subscriptionFeatureRepo,
            IAlphaRepository<Subscription> subscriptionRepo, ILogger<SubscriptionService> logger)
        {
            _subscriptionFeatureRepo = subscriptionFeatureRepo;
            _subscriptionRepo = subscriptionRepo;
            _logger = logger;
        }
        public async Task<ResponseDto<string>> CreateSubscriptionPlan(AddSubPlanReq req)
        {
            var response = new ResponseDto<string>();
            try
            {
                var AddSubPlan = await _subscriptionRepo.Add(new Subscription()
                {
                    Amount = req.Amount,
                    DiscountRate = req.DiscountRate,
                    BillingInterval = req.BillingInterval,
                    IsDIscounted = req.IsDIscounted,
                    Name = req.Name,
                });
                var ListStock = new Dictionary<string, string>
                {
                    { "SA", "Stock Analysis" },
                    { "DT", "Data Table" },
                    { "R", "Result" }
                };

                foreach (var item in ListStock)
                {
                    await _subscriptionFeatureRepo.Add(new SubscriptionFeatures
                    {
                        SubscriptionId = AddSubPlan.Id,
                        Category = "Stock Analysis",
                        CurrentState = "False",
                        FeatureName = item.Value,
                        ShortName = item.Key
                    });
                }
                var dicFinance = new Dictionary<string, string>
                {
                    { "BS", "Balance Sheet" },
                    { "CF", "Cash Flow" },

                };

                foreach (var item in dicFinance)
                {
                    await _subscriptionFeatureRepo.Add(new SubscriptionFeatures
                    {
                        SubscriptionId = AddSubPlan.Id,
                        Category = "Financial Insight",
                        CurrentState = "False",
                        FeatureName = item.Value,
                        ShortName = item.Key
                    });
                }
                await _subscriptionFeatureRepo.Add(new SubscriptionFeatures
                {
                    SubscriptionId = AddSubPlan.Id,
                    Category = "Financial Insight",
                    CurrentState = "0 per month",
                    FeatureName = "Income Statement",
                    ShortName = "IS"
                }); 
                await _subscriptionFeatureRepo.Add(new SubscriptionFeatures
                {
                    SubscriptionId = AddSubPlan.Id,
                    Category = "Community Management",
                    CurrentState = "False",
                    FeatureName = "Community Management",
                    ShortName = "CM"
                });
                await _subscriptionFeatureRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Plan Created Successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Plan not added successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> updateSubscriptionPlanDetails(UpdateSubDetails req)
        {
            var response = new ResponseDto<string>();
            try
            {
                var retrieveSubId = await _subscriptionRepo.GetByIdAsync(req.SubPlanId);
                if (retrieveSubId == null)
                {
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "invalid Plan" };
                    response.StatusCode = 400;
                    return response;

                }
                retrieveSubId.BillingInterval = req.BillingInterval;
                retrieveSubId.IsDIscounted = req.IsDIscounted;
                retrieveSubId.Amount = req.Amount;
                retrieveSubId.DiscountRate = req.DiscountRate;
                retrieveSubId.Name = req.Name;
                _subscriptionRepo.Update(retrieveSubId);

                await _subscriptionRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Plan Updated Successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Plan not updated successfully" };
                response.StatusCode = 400;
                return response;
            }
        } 
        public async Task<ResponseDto<SubscriptionFeatures>> updateSubscriptionFeature(UpdateSubScriptionFeature req)   
        {
            var response = new ResponseDto<SubscriptionFeatures>();
            try
            {
                var retrieveSubId = await _subscriptionFeatureRepo.GetByIdAsync(req.FeatureId);
                if (retrieveSubId == null)
                {
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "invalid subcription feature" };
                    response.StatusCode = 400;
                    return response;

                }
                retrieveSubId.CurrentState = req.CurrentState;
               
                _subscriptionFeatureRepo.Update(retrieveSubId);

                await _subscriptionFeatureRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = retrieveSubId;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Plan not updated successfully" };
                response.StatusCode = 400;
                return response;
            }
        }

        public async Task<ResponseDto<IEnumerable< Subscription>>> RetrieveAllPlan()
        {
            var response = new ResponseDto<IEnumerable<Subscription>>();
            try
            {
                var GetAllPlan = await _subscriptionRepo.GetQueryable().OrderBy(u=>u.Amount).Where(u=>u.Id != "314561df-5e86-4269-b97e-d3f55b5d3e99")
                    .ToListAsync();

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = GetAllPlan;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Subcription Plan not retrieved successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<Subscription>> RetrievePlanFeature(string planid)
        {
            var response = new ResponseDto<Subscription>();
            try
            {
                var GetPlan = await _subscriptionRepo.GetQueryable().Include(u=>u.SubscriptionFeatures)
                    .FirstOrDefaultAsync(u=>u.Id == planid);

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = GetPlan;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Single Subcription Plan not retrieved successfully" };
                response.StatusCode = 400;
                return response;
            }
        }   
    }
}
