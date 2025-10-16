using Microsoft.AspNetCore.Authorization;

public class CargoAdministrador : IAuthorizationRequirement
{
    public CargoAdministrador(string cargo)
    {
        Cargo = cargo;
    }

    public string Cargo { get; set; }
}