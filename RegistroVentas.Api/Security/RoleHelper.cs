using System.Security.Claims;

namespace RegistroVentas.Api.Security;

public static class RoleHelper
{
    public static bool HasAnyRole(ClaimsPrincipal user, params string[] allowedRoles)
    {
        var allowed = allowedRoles.ToHashSet(StringComparer.OrdinalIgnoreCase);

        return user.Claims.Any(claim => 
            (claim.Type == ClaimTypes.Role || claim.Type == "role" || claim.Type == "roles") && allowed.Contains(claim.Value));
    }
}