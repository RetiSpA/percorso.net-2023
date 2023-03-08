using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Esercizio5
{
    public class MinimunAgeRequirement : IAuthorizationRequirement
    {
        public int MinAge { get; set; }

        public MinimunAgeRequirement(int age)
        {
            MinAge = age;
        }
    }

    public class MinimumAgeHandler : AuthorizationHandler<MinimunAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimunAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.FromResult(0);
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth)!.Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (calculatedAge >= requirement.MinAge)
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}