using AlpaStock.Core.DTOs;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly ILogger<StripePaymentService> _logger;
     
        private readonly IAlphaRepository<Core.Entities.Subscription> _subscriptionRepo;
        private readonly IAlphaRepository<Payments> _paymentRepo;
        private readonly IAlphaRepository<UserSubscription> _userSubRepo;
        private readonly IConfiguration _configuration;

        public StripePaymentService(IConfiguration configuration, ILogger<StripePaymentService> logger = null,  IAlphaRepository<UserSubscription> userSubRepo = null, IAlphaRepository<Payments> paymentRepo = null, IAlphaRepository<Core.Entities.Subscription> subscriptionRepo = null)
        {
            _configuration = configuration;
           
            _logger = logger;
            
            _userSubRepo = userSubRepo;
            _paymentRepo = paymentRepo;
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task<ResponseDto<string>> IntializeStripepayment(string planid, string userid)
        {

            var key = _configuration["StripeKey:TestSecretKey"];
         

            StripeConfiguration.ApiKey = key;
            var stripePayment = new Payments();
            var response = new ResponseDto<string>();
            try
            {
                var retrievePlan = await _subscriptionRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == planid);
                if (retrievePlan == null)
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Plan not available" };
                    return response;
                }
                var description = $"Subcription order purchase for alpha stock - {retrievePlan.Amount}$";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = $"{_configuration["WebHook:StripeCallBackUrl"]}?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = _configuration["WebHook:StripeCallBackUrl"],
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment"
                };

                var sessionListItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = long.Parse(retrievePlan.Amount.ToString()) * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = description
                        }
                    },
                    Quantity = 1
                };

                options.LineItems.Add(sessionListItem);

                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                if (session.Status != "open")
                {
                    response.ErrorMessages = new List<string>() { "Failed to make order for minute" };
                    response.DisplayMessage = $"Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                stripePayment.PaymentType = "STRIPE";
                stripePayment.OrderReferenceId = session.Id;
                stripePayment.Amount = retrievePlan.Amount.ToString("0.00");
                stripePayment.IsActive = true;
                stripePayment.Description = description;
                stripePayment.UserId = userid;
                stripePayment.SubscriptionId = planid;
                var addpaymenttoDb = await _paymentRepo.Add(stripePayment);
                await _paymentRepo.SaveChanges();

                response.Result = session.Url;
                response.StatusCode = 200;
                response.DisplayMessage = "Successfully created stripe payment";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Failed to make order for subscription on stripe" };
                response.DisplayMessage = $"Error";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }
        }
        public async Task<ResponseDto<string>> confirmStripepayment(string session_id)
        {
            var response = new ResponseDto<string>();
            StripeConfiguration.ApiKey = _configuration["StripeKey:TestSecretKey"];
            try
            {
                var retrieveOrder = await _paymentRepo.GetQueryable().Include(u => u.Subscription).
                    FirstOrDefaultAsync(u => u.OrderReferenceId == session_id);
                if (retrieveOrder == null)
                {
                    response.ErrorMessages = new List<string>() {
                        "Invalid order" };
                    response.DisplayMessage = $"Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                var service = new SessionService();
                var session = await service.GetAsync(session_id);
                if (retrieveOrder.IsActive == false)
                {
                    response.ErrorMessages = new List<string> { "Invalid Transaction" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                if (session.PaymentStatus == "paid")
                {
                    retrieveOrder.IsActive = false;
                    retrieveOrder.PaymentStatus = session.PaymentStatus;
                    retrieveOrder.CompletePaymentTime = DateTime.UtcNow;
                    _paymentRepo.Update(retrieveOrder);

                    int billingInterval = int.Parse(retrieveOrder.Subscription.BillingInterval);
                    var checkCurrentOrder = await _userSubRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == retrieveOrder.SubscriptionId && u.IsActive);
                    if (checkCurrentOrder != null)
                    {
                        checkCurrentOrder.SubscrptionEnd = checkCurrentOrder.SubscrptionEnd.AddMonths(billingInterval);
                        _userSubRepo.Update(checkCurrentOrder);


                    }
                    else
                    {
                        await _userSubRepo.Add(new UserSubscription()
                        {
                            SubscriptionId = retrieveOrder.SubscriptionId,
                            UserId = retrieveOrder.UserId,
                            SubscrptionStart = DateTime.UtcNow,
                            SubscrptionEnd = DateTime.UtcNow.AddMonths(billingInterval)
                        });
                    }

                    await _userSubRepo.SaveChanges();
                    response.StatusCode = StatusCodes.Status200OK;
                    response.DisplayMessage = "Successful";
                    response.Result = "Payment Successfully completed";
                    return response;

                }
                retrieveOrder.CompletePaymentTime = DateTime.UtcNow;
                retrieveOrder.IsActive = false;
                retrieveOrder.PaymentStatus = session.PaymentStatus;
                _paymentRepo.Update(retrieveOrder);
                await _paymentRepo.SaveChanges();
                response.ErrorMessages = new List<string> { "Invalid stripe Transaction" };
                response.DisplayMessage = "Error";
                response.StatusCode = StatusCodes.Status400BadRequest;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in validating transaction" };
                response.DisplayMessage = $"Error";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }




        }
    }
}
