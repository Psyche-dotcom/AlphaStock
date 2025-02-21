using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Auth;
using AlpaStock.Core.DTOs.Request.Notification;
using AlpaStock.Core.DTOs.Response.Auth;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepo _accountRepo;
        private readonly IEmailServices _emailServices;
        private readonly IAlphaRepository<ForgetPasswordToken> _forgetPasswordTokenRepo;
        private readonly ILogger<AccountService> _logger;
        private readonly IGenerateJwt _generateJwt;

        public AccountService(IAccountRepo accountRepo,
            ILogger<AccountService> logger,
            IGenerateJwt generateJwt,
            IEmailServices emailServices,
            IAlphaRepository<ForgetPasswordToken> forgetPasswordTokenRepo)
        {
            _accountRepo = accountRepo;
            _logger = logger;
            _generateJwt = generateJwt;
            _emailServices = emailServices;
            _forgetPasswordTokenRepo = forgetPasswordTokenRepo;
        }
        public async Task<ResponseDto<string>> RegisterUser(SignUp signUp, string Role)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByEmailAsync(signUp.Email);
                if (checkUserExist != null)
                {
                    response.ErrorMessages = new List<string>() { "User with the email already exist" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkRole = await _accountRepo.RoleExist(Role);
                if (checkRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Role is not available" };
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mapAccount = new ApplicationUser();

                mapAccount.FirstName = signUp.FirstName;
                mapAccount.LastName = signUp.LastName;
                mapAccount.Country = signUp.Country;
                mapAccount.Email = signUp.Email;
                mapAccount.PhoneNumber = signUp.PhoneNumber;
                mapAccount.UserName = signUp.UserName;


                var createUser = await _accountRepo.SignUpAsync(mapAccount, signUp.Password);
                if (createUser == null)
                {
                    response.ErrorMessages = new List<string>() { "User not created successfully" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var addRole = await _accountRepo.AddRoleAsync(createUser, Role);
                if (addRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Fail to add role to user" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var GenerateConfirmEmailToken = new ConfirmEmailToken()
                {
                    Token = _accountRepo.GenerateConfirmEmailToken(),
                    UserId = createUser.Id
                };
                var Generatetoken = await _accountRepo.SaveGenerateConfirmEmailToken(GenerateConfirmEmailToken);
                if (Generatetoken == null)
                {
                    response.ErrorMessages = new List<string>() { "Fail to generate confirm email token for company" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var message = new Message(new string[] { createUser.Email }, "Confirm Email Token", $"<p>Your confirm email code is below<p><h6>{GenerateConfirmEmailToken.Token}</h6>");
                _emailServices.SendEmail(message);

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "User successfully created";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in resgistering the user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<LoginResultDto>> LoginUser(SignInModel signIn)
        {
            var response = new ResponseDto<LoginResultDto>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByEmailAsync(signIn.Email);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no company with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkUserExist.isSuspended == true)
                {
                    response.ErrorMessages = new List<string>() { "user is suspended, contact admin" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkPassword = await _accountRepo.CheckAccountPassword(checkUserExist, signIn.Password);
                if (checkPassword == false)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Password" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var generateToken = await _generateJwt.GenerateToken(checkUserExist);
                if (generateToken == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in generating jwt for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }

                var getUserRole = await _accountRepo.GetUserRoles(checkUserExist);
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successfully login";
                response.Result = new LoginResultDto() { Jwt = generateToken, UserRole = getUserRole };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in login the user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<UserInfo>> UserInfoAsync(string userId)
        {
            var response = new ResponseDto<UserInfo>();
            try
            {
                var fetchCompany = await _accountRepo.FindUserByIdAsync(userId);
                if (fetchCompany == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid user" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var result = new UserInfo()
                {
                    Id = fetchCompany.Id,
                    Email = fetchCompany.Email,
                    UserName = fetchCompany.UserName,
                    FirstName = fetchCompany.FirstName,
                    LastName = fetchCompany.LastName,
                    Country = fetchCompany.Country,
                    PhoneNumber =fetchCompany.PhoneNumber,
                    isSuspended = fetchCompany.isSuspended,


                };


                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting user info" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> ResetPassword(ResetPassword resetPassword)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findCompany = await _accountRepo.FindUserByEmailAsync(resetPassword.Email);
                if (findCompany == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no user with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveToken = await _forgetPasswordTokenRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == findCompany.Id);
                if (retrieveToken == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid user token" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                resetPassword.Token = retrieveToken.gentoken;
                var resetPasswordAsync = await _accountRepo.ResetPasswordAsync(findCompany, resetPassword);
                if (resetPasswordAsync == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                _forgetPasswordTokenRepo.Delete(retrieveToken);
                await _forgetPasswordTokenRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully reset user password";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in reset user password" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> ConfirmEmailAsync(int token, string email)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findUser = await _accountRepo.FindUserByEmailAsync(email);
                if (findUser == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no user with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveToken = await _accountRepo.retrieveUserToken(findUser.Id);
                if (retrieveToken == null)
                {
                    response.ErrorMessages = new List<string>() { "Error user token " };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                if (retrieveToken.Token != token)
                {
                    response.ErrorMessages = new List<string>() { "Invalid user token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var deleteToken = await _accountRepo.DeleteUserToken(retrieveToken);
                if (deleteToken == false)
                {
                    response.ErrorMessages = new List<string>() { "Error removing user token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                findUser.EmailConfirmed = true;
                var updateUserConfirmState = await _accountRepo.UpdateUserInfo(findUser);
                if (updateUserConfirmState == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in confirming user token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully comfirm user token";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in confirming user token" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> ForgotPassword(string CompanyEmail)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUser = await _accountRepo.FindUserByEmailAsync(CompanyEmail);
                if (checkUser == null)
                {
                    response.ErrorMessages = new List<string>() { "Email is not available" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var result = await _accountRepo.ForgotPassword(checkUser);
                if (result == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in generating reset token for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveToken = await _forgetPasswordTokenRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == checkUser.Id);
                if (retrieveToken != null)
                {
                    _forgetPasswordTokenRepo.Delete(retrieveToken);
                    await _forgetPasswordTokenRepo.SaveChanges();
                }
                var generateToken = _accountRepo.GenerateToken();
                var savetoken = await _forgetPasswordTokenRepo.Add(new ForgetPasswordToken()
                {
                    token = generateToken.ToString(),
                    gentoken = result,
                    userid = checkUser.Id
                });
                await _forgetPasswordTokenRepo.SaveChanges();
                var message = new Message(new string[] { checkUser.Email }, "Reset Password Code", $"<p>Your reset password code is below<p><br/><h6>{generateToken}</h6><br/> <p>Please use it in your reset password page</p>");
                _emailServices.SendEmail(message);
                response.DisplayMessage = "Token generated Successfully";
                response.Result = generateToken.ToString();
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in generating reset token for user" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }



    }
}
