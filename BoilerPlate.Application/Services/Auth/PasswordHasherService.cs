using BoilerPlate.Application.Shared.InterFaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Services.Auth
{
    public class PasswordHasherService : IPasswordHasher
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string Password, string PasswordHash) => BCrypt.Net.BCrypt.Verify(Password, PasswordHash);     
    }
}
