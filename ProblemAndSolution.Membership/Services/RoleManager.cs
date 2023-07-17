using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProblemAndSolution.Infrastructure.Entities.Membership;

namespace ProblemAndSolution.Membership.Services
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger)
           : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
