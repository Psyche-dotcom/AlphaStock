using AlpaStock.Core.Context;
using AlpaStock.Core.DTOs.Response.Payment;
using AlpaStock.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AlpaStock.Core.Repositories.Implementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AlphaContext _context;
        public PaymentRepo(AlphaContext context)
        {
            _context = context;
        }
        public async Task<PaginatedPaymentInfo> RetrieveAllPaymentAsync(int pageNumber, int perPageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var payment = _context.Payments
    
                .Select(p => new PaymentWithUserInfo
            {
                Id = p.Id,
                Amount = p.Amount,
                OrderReferenceId = p.OrderReferenceId,
                Description = p.Description,
                PaymentType = p.PaymentType,
                CreatedPaymentTime = p.CreatedPaymentTime,
                CompletePaymentTime = p.CompletePaymentTime,
                IsActive = p.IsActive,
                UserId = p.UserId,
                PaymentStatus = p.PaymentStatus,
                UserName = p.User.UserName,
                FirstName = p.User.FirstName,
                Email = p.User.Email,
                LastName = p.User.LastName,
                Country = p.User.Country,
                SubscriptionTypeName = p.Subscription.Name
            }).OrderBy(u=>u.CompletePaymentTime);
            var paginatedPayment = await payment
                .Skip((pageNumber - 1) * perPageSize)
                .Take(perPageSize)
                .ToListAsync();
            var totalCount = await payment.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var result = new PaginatedPaymentInfo
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                Payments = paginatedPayment,
            };
            return result;


        }
        public async Task<PaginatedPaymentInfo> RetrieveUserAllPaymentAsync(string userid, int pageNumber, int perPageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var paymentsWithUserInfo = _context.Payments
            
                .Where(p => p.UserId == userid)
                .Select(p => new PaymentWithUserInfo
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    OrderReferenceId = p.OrderReferenceId,
                    Description = p.Description,
                    UserId = p.UserId,
                    PaymentType = p.PaymentType,
                    CreatedPaymentTime = p.CreatedPaymentTime,
                    CompletePaymentTime = p.CompletePaymentTime,
                    IsActive = p.IsActive,
                    PaymentStatus = p.PaymentStatus,
                    UserName = p.User.UserName,
                    FirstName = p.User.FirstName,
                    Email = p.User.Email,
                    LastName = p.User.LastName,
                    Country = p.User.Country,
                    SubscriptionTypeName=p.Subscription.Name
                }).OrderBy(u => u.CompletePaymentTime); 
            var paginatedPayment = await paymentsWithUserInfo
                .Skip((pageNumber - 1) * perPageSize)
                .Take(perPageSize)
                .ToListAsync();
            var totalCount = await paymentsWithUserInfo.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var result = new PaginatedPaymentInfo
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                Payments = paginatedPayment,
            };
            return result;


        }

    }
}
