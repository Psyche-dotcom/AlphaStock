

using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Response.Payment;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PayPalCheckoutSdk.Core;

using PayPalCheckoutSdk.Orders;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class PaymentService : IPaymentService
    {
        private PayPalHttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        private readonly IAccountRepo _accountRepo;
        private readonly IAlphaRepository<Subscription> _subscriptionRepo;
        private readonly IAlphaRepository<Payments> _paymentRepo;
        private readonly IAlphaRepository<UserSubscription> _userSubRepo;
        private readonly IPaymentRepo _paymentdb;

        public PaymentService(PayPalHttpClient client,
            ILogger<PaymentService> logger,
            IAccountRepo accountRepo,
            IConfiguration configuration,
            IAlphaRepository<Subscription> subscriptionRepo,
            IAlphaRepository<Payments> paymentRepo,
            IAlphaRepository<UserSubscription> userSubRepo,
            IPaymentRepo paymentdb)
        {
            _client = client;
            _logger = logger;
            _accountRepo = accountRepo;
            _configuration = configuration;
            _subscriptionRepo = subscriptionRepo;
            _paymentRepo = paymentRepo;
            _userSubRepo = userSubRepo;
            _paymentdb = paymentdb;
        }

        public async Task<ResponseDto<string>> MakeOrder(string userid, string planid)
        {

            var result = new ResponseDto<string>();

            try
            {
                var retrievePlan = await _subscriptionRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == planid);
                if (retrievePlan == null)
                {
                    result.StatusCode = 400;
                    result.DisplayMessage = "Error";
                    result.ErrorMessages = new List<string>() { "Plan not available" };
                }
                var payment = new Payments();
                decimal amount = retrievePlan.Amount;
                var description = $"{retrievePlan.Name} plan purchase for {amount}$";
                var orderRequest = OrderRequest(amount.ToString("0.00"), "USD", description);
                var response = await _client.Execute(orderRequest);

                var order = response.Result<Order>();
                if (order.Status == "CREATED")
                {
                    payment.OrderReferenceId = order.Id;
                    payment.UserId = userid;
                    payment.Amount = amount.ToString("0.00");
                    payment.Description = description;
                    payment.IsActive = true;
                    payment.SubscriptionId = retrievePlan.Id;
                    var addpaymenttoDb = await _paymentRepo.Add(payment);
                    await _paymentRepo.SaveChanges();

                    var data = order.Links.ToDictionary(links => links.Rel, links => links.Href);
                    string approveUrl = data["approve"]?.ToString();
                    result.Result = approveUrl;
                    result.DisplayMessage = "Payment successfully initialize";
                    result.StatusCode = 201;
                    return result;


                }

                result.ErrorMessages = new List<string>() { "Failed to make order for minute" };
                result.DisplayMessage = $"Error";
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                result.ErrorMessages = new List<string>() { "Failed to make order for minute" };
                result.DisplayMessage = $"Error";
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
            }
        }

        private OrdersCreateRequest OrderRequest(string amount, string currency, string description)
        {
            var orderRequest = new OrdersCreateRequest()
            {
                Body = new OrderRequest()
                {
                    CheckoutPaymentIntent = "CAPTURE",
                    PurchaseUnits = new List<PurchaseUnitRequest>()
                    {
                        new PurchaseUnitRequest()
                        {
                            AmountWithBreakdown = new AmountWithBreakdown()
                            {
                                CurrencyCode = currency,
                                Value = amount
                            },
                            Description = description
                        }
                    },
                    ApplicationContext = new ApplicationContext()
                    {
                        PaymentMethod = new PaymentMethod
                        {
                            PayeePreferred = "IMMEDIATE_PAYMENT_REQUIRED",
                            PayerSelected = "PAYPAL"
                        },
                        LandingPage = "NO_PREFERENCE",
                        ReturnUrl = _configuration["WebHook:PayPalCallBackUrl"],
                        CancelUrl = _configuration["WebHook:PayPalCallBackUrl"]
                    }
                }
            };

            return orderRequest;
        }

        public async Task<ResponseDto<string>> ConfirmPayment(string token)
        {
            var response = new ResponseDto<string>();
            try
            {
                var capture = new OrdersGetRequest(token);

                capture.Body = new Order()
                {
                    Id = token
                };

                var result = await _client.Execute(capture);
                var order = result.Result<Order>();
                bool IsComplete = order.Status == "APPROVED";
                var retrieveOrder = await _paymentRepo.GetQueryable().Include(u=>u.Subscription).
                    FirstOrDefaultAsync(u => u.OrderReferenceId == order.Id);
                var expectedAmount = order.PurchaseUnits[0].AmountWithBreakdown.Value;
                if (retrieveOrder == null || !IsComplete || retrieveOrder.Amount != expectedAmount)
                {
                    retrieveOrder.CompletePaymentTime = DateTime.UtcNow;
                    retrieveOrder.IsActive = false;
                    retrieveOrder.PaymentStatus = order.Status;
                    _paymentRepo.Update(retrieveOrder);
                    await _paymentRepo.SaveChanges();
                    response.ErrorMessages = new List<string> { "Invalid Transaction" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
               
                if (retrieveOrder.IsActive == false)
                {
                    response.ErrorMessages = new List<string> { "Invalid Transaction" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                retrieveOrder.IsActive = false;
                retrieveOrder.PaymentStatus = order.Status;
                retrieveOrder.CompletePaymentTime = DateTime.UtcNow;
                _paymentRepo.Update(retrieveOrder);

                int billingInterval = int.Parse(retrieveOrder.Subscription.BillingInterval);
                var checkCurrentOrder = await _userSubRepo.GetQueryable().FirstOrDefaultAsync( u=>u.Id == retrieveOrder.SubscriptionId && u.IsActive);
                if(checkCurrentOrder != null )
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in validating transaction" };
                response.DisplayMessage = $"Error";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }
        }

        public async Task<ResponseDto<PaginatedPaymentInfo>> RetrieveUserAllPaymentAsync(string userid, int pageNumber, int perPageSize)
        {
            var response = new ResponseDto<PaginatedPaymentInfo>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByIdAsync(userid);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveUserPayment = await _paymentdb.RetrieveUserAllPaymentAsync(userid, pageNumber, perPageSize);
                response.DisplayMessage = "Successful";
                response.Result = retrieveUserPayment;
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Failed to retrieve user payment" };
                response.DisplayMessage = $"Error";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }

        }
        public async Task<ResponseDto<PaginatedPaymentInfo>> RetrieveAllPaymentAsync(int pageNumber, int perPageSize)
        {
            var response = new ResponseDto<PaginatedPaymentInfo>();
            try
            {

                var retrieveUserPayment = await _paymentdb.RetrieveAllPaymentAsync(pageNumber, perPageSize);
                response.DisplayMessage = "Successful";
                response.Result = retrieveUserPayment;
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Failed to retrieve all payment" };
                response.DisplayMessage = $"Error";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }

        }
    }
}