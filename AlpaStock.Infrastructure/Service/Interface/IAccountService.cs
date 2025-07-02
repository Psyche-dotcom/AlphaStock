

using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Auth;
using AlpaStock.Core.DTOs.Response.Auth;
using Microsoft.AspNetCore.Http;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<LoginResultDto>> SignInRegisterSocialAccount(string token, string GenericPassword);
        Task<ResponseDto<UserStatisticsResponse>> GetUserStatisticsChartAsync();
        Task<ResponseDto<string>> UploadUserProfilePicture(string email, IFormFile file);
        Task<ResponseDto<string>> UpdateUser(string email, UpdateUserDto updateUser);
        Task<ResponseDto<string>> DeleteUser(string email);
        Task<ResponseDto<string>> RegisterUser(SignUp signUp, string Role);

        Task<ResponseDto<LoginResultDto>> LoginUser(SignInModel signIn);
        Task<ResponseDto<PaginatedUser>> GetAllUsersAsync(int pageNumber, int perPageSize, string? sinceDate, string? name, UserFilter filter);
        Task<ResponseDto<UserInfo>> UserInfoAsync(string userId);
        Task<ResponseDto<string>> ForgotPassword(string CompanyEmail);
        Task<ResponseDto<string>> ConfirmEmailAsync(int token, string email);
        Task<ResponseDto<string>> ResetPassword(ResetPassword resetPassword);
        Task<ResponseDto<string>> SuspendUserAsync(string useremail);
        Task<ResponseDto<string>> UnSuspendUserAsync(string useremail);


    }
}
