using Microsoft.AspNetCore.Identity;
using ProblemAndSolution.Infrastructure.Entities.Membership;
using ProblemAndSolution.Membership.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Membership.Services
{
    public interface IUserManagerAdapter<T> where T:class
    {
        Task<IdentityResult> CreateAsync(T applicationUser, string password);
        Task CreateAccountAsync(T applicationUser, string password);
        bool ConfirmedAccount();
        Task<string> GetUserIdAsync(T applicationUser);
        Task<bool> UpdateAccountAsync(T user);
        Task SignInAsync(Guid id);
        string? GetUserId();
        Task<ApplicationUser> GetById(Guid id);

        Task<IList<string>> GetUserRolesAsync(string email);
        Task<IdentityResult> ChangePassword(string userId, string newPassword,
                                            string confirmPassword);
        Task RolesAsync(string userid, RoleType[] types);
        Task<T> FindByUsernameAsync(string email);
    }
}
