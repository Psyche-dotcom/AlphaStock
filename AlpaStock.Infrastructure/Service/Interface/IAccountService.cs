

using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Auth;
using AlpaStock.Core.DTOs.Response.Auth;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> RegisterUser(SignUp signUp, string Role);

        Task<ResponseDto<LoginResultDto>> LoginUser(SignInModel signIn);

        Task<ResponseDto<UserInfo>> UserInfoAsync(string userId);
        Task<ResponseDto<string>> ForgotPassword(string CompanyEmail);
        Task<ResponseDto<string>> ConfirmEmailAsync(int token, string email);
        Task<ResponseDto<string>> ResetPassword(ResetPassword resetPassword);


    }
}
