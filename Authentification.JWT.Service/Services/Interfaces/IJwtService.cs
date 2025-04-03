using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId);
    }
}
