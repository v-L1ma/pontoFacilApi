using Microsoft.AspNetCore.Authorization;

public class AdministradorAuthorization : AuthorizationHandler<CargoAdministrador>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CargoAdministrador requirement)
    {
        var cargo = context.User.FindFirst(claim => claim.Type == "Cargo");

        if (cargo is null)
        {
            return Task.CompletedTask;
        }

        if (cargo.Value == "Administrador")
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}