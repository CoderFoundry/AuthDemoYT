using System.Security.Claims;
using AuthDemoYT.Client.Models;
using AuthDemoYT.Data;
using AuthDemoYT.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AuthDemoYT.Components.Account
{
    public class CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> options)
        : UserClaimsPrincipalFactory<ApplicationUser>(userManager, options)
    {
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);

            string profilePicture = user.ImageId.HasValue ? $"/api/uploads/{user.ImageId}" : UploadHelper.DefaultProfilePicture;

            List<Claim> customClaims = [
                new Claim(nameof(UserInfo.ProfilePictureUrl), profilePicture),
                new Claim(nameof(UserInfo.FirstName), user.FirstName!),
                new Claim(nameof(UserInfo.LastName), user.LastName!),
                ];

            identity.AddClaims(customClaims);

            return identity;
        }
    }
}
