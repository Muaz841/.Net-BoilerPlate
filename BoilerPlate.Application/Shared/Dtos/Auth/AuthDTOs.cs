using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Shared.Dtos.Auth
{
    public record LoginRequest(string Email, string Password);
    public record LoginResponse(string Token, DateTime Expires);
}
