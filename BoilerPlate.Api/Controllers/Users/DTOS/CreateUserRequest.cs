namespace BoilerPlate.Api.Controllers.Users.DTOS
{
    public record CreateUserRequest(string Email, string Name, string Password);
}
