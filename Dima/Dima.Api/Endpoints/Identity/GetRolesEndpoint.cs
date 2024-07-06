using System.Security.Claims;
using Dima.Api.Common.Api;

namespace Dima.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles", Handler)
            .RequireAuthorization();
    }
    private static Task<IResult> Handler(ClaimsPrincipal user)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Task.FromResult(Results.Unauthorized());

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity
            .FindAll(identity.RoleClaimType)
            .Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
    // Captura os roles do usuário direto do banco
    //app.MapGroup("v1/identity")
    //    .WithTags("Identity")
    //    .MapGet("/roles", async
    //        (UserManager<User> userManager) =>
    //    {
    //        var user = await userManager.FindByEmailAsync("");
    //        return Results.Ok(user?.Roles);
    //    })
    //    .RequireAuthorization();
}