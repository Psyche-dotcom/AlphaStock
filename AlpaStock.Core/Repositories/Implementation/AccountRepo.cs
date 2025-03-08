using AlpaStock.Core.Context;
using AlpaStock.Core.DTOs.Request.Auth;
using AlpaStock.Core.DTOs.Response.Auth;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlpaStock.Core.Repositories.Implementation
{
    public class AccountRepo : IAccountRepo
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AlphaContext _context;

        public AccountRepo(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AlphaContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<bool> AddRoleAsync(ApplicationUser user, string Role)
        {
            var AddRole = await _userManager.AddToRoleAsync(user, Role);
            if (AddRole.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveRoleAsync(ApplicationUser user, IList<string> role)
        {
            var removeRole = await _userManager.RemoveFromRolesAsync(user, role);
            if (removeRole.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<IList<string>> GetUserRoles(ApplicationUser user)
        {
            var getRoles = await _userManager.GetRolesAsync(user);
            if (getRoles != null)
            {
                return getRoles;
            }
            return null;
        }

        public async Task<bool> RoleExist(string Role)
        {
            var check = await _roleManager.RoleExistsAsync(Role);
            return check;
        }
        public async Task<bool> ConfirmEmail(string token, ApplicationUser user)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserByEmail(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser?> FindUserByEmailAsync(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser == null)
            {
                return null;
            }
            return findUser;
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            var findUser = await _userManager.FindByIdAsync(id);
            return findUser;
        } 
        public async Task<ApplicationUser> FindUserByIdFullinfoAsync(string id)
        {
            var findUser = await _userManager.Users
                .Include(u=>u.Subscriptions)
                .ThenInclude(s => s.Subscription).
                ThenInclude(m=>m.SubscriptionFeatures).
                FirstOrDefaultAsync(d=>d.Id == id);
            return findUser;
        }
        public async Task<UserStatisticsResponse> GetUserStatisticsAsync()
        {
            int currentYear = DateTime.UtcNow.Year;

            var monthlyUserCounts = await _userManager.Users
                .Where(u => u.Created.Year == currentYear)
                .GroupBy(u => u.Created.Month)
                .Select(g => new UserStatistics
                {
                    Month = g.Key,
                    UserCount = g.Count()
                })
                .ToListAsync();

            int totalUserCount = await _userManager.Users.CountAsync();
            int totalUserCountCurrentYear = monthlyUserCounts.Sum(m => m.UserCount);
            return new UserStatisticsResponse()
            {
                CurrentYearTotalUserCount = totalUserCountCurrentYear,
                TotalUserCount = totalUserCount,
                UserStatistics = monthlyUserCounts
            };
        }
        public async Task<string> ForgotPassword(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
        public async Task<bool> CheckEmailConfirmed(ApplicationUser user)
        {
            var checkConfirm = user.EmailConfirmed == true;
            return checkConfirm;
        }

        public async Task<bool> CheckAccountPassword(ApplicationUser user, string password)
        {
            var checkUserPassword = await _userManager.CheckPasswordAsync(user, password);
            return checkUserPassword;
        }

        public async Task<ResetPassword> ResetPasswordAsync(ApplicationUser user, ResetPassword resetPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (result.Succeeded)
            {
                return resetPassword;
            }
            return null;
        }

        public async Task<ApplicationUser> SignUpAsync(ApplicationUser user, string Password)
        {
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> UpdateUserInfo(ApplicationUser applicationUser)
        {
            var updateUserInfo = await _userManager.UpdateAsync(applicationUser);
            if (updateUserInfo.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<PaginatedUser> GetAllRegisteredUserAsync(int pageNumber, int perPageSize, string? sinceDate, string? name, UserFilter filter)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;

            var query = _userManager.Users
                .Include(u => u.Subscriptions)
                .ThenInclude(s => s.Subscription)
                .Join(
                    _context.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new { User = user, UserRole = userRole })
                .Join(
                    _roleManager.Roles,
                    userRole => userRole.UserRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { User = userRole.User, Role = role })
                .Where(u => u.Role.NormalizedName == "USER");
            if (filter == UserFilter.SUSPENDED)
            {
                query = query.Where(u => u.User.isSuspended == true);

            }
            if (filter == UserFilter.UNVERIFIED)
            {
                query = query.Where(u => u.User.EmailConfirmed != true);

            }
            if (filter == UserFilter.ACTIVE)
            {
                query = query.Where(u => u.User.EmailConfirmed == true && u.User.isSuspended == false);

            }
            if (!string.IsNullOrEmpty(sinceDate))
            {
                if (DateTime.TryParse(sinceDate, out DateTime parsedDate))
                {

                    DateTime utcDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                    query = query.Where(u => u.User.Created >= utcDate);
                }
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(u => (u.User.FirstName + " " + u.User.LastName).ToLower().Contains(name.ToLower()));
            }

            var filteredUser = query.Select(u => new DisplayFindUserDTO
            {
                ActiveSubcriptionName = u.User.Subscriptions
                    .FirstOrDefault(s => s.IsActive) != null ? u.User.Subscriptions
                    .FirstOrDefault(s => s.IsActive).Subscription.Name: null,
                UserName = u.User.UserName,
                Email = u.User.Email,
                FirstName = u.User.FirstName,
                IsEmailConfirmed = u.User.EmailConfirmed,
                LastName = u.User.LastName,
                PhoneNumber = u.User.PhoneNumber,
                ProfilePicture = u.User.ProfilePicture,
                isSubActive = u.User.Subscriptions.Any(s => s.IsActive),
                IsSuspendUser = u.User.isSuspended,
                Id = u.User.Id,
                Created = u.User.Created
            });

            var totalCount = await filteredUser.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);

            var paginatedUser = await filteredUser
                .Skip((pageNumber - 1) * perPageSize)
                .Take(perPageSize)
                .OrderBy(u => u.Created)
                .ToListAsync();

            var result = new PaginatedUser
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                User = paginatedUser,
                TotalUserCount = totalCount,
            };

            return result;
        }


        public int GenerateConfirmEmailToken()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);
            return randomNumber;
        }
        public async Task<ConfirmEmailToken> SaveGenerateConfirmEmailToken(ConfirmEmailToken emailToken)
        {
            var saveToken = await _context.ConfirmEmailTokens.AddAsync(emailToken);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return emailToken;
            }
            return null;
        }
        public int GenerateToken()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);
            return randomNumber;
        }
        public async Task<ConfirmEmailToken> retrieveUserToken(string userid)
        {
            return await _context.ConfirmEmailTokens.FirstOrDefaultAsync(u => u.UserId == userid);
        }
        public async Task<bool> DeleteUserToken(ConfirmEmailToken token)
        {
            _context.ConfirmEmailTokens.Remove(token);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return true;
            }
            return false;
        }
    }
}
