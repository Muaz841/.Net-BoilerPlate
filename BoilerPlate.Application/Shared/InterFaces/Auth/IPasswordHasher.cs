using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Shared.InterFaces.Auth
{
    public  interface IPasswordHasher
    {
        bool VerifyPassword(string password, string passwordHash);

        string HashPassword(string password);
    }
}
