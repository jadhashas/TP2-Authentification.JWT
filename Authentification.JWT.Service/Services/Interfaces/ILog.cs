using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Services.Interfaces
{
    public interface ILog
    {
        void Info(string message);
        void Error(string message, Exception? ex = null);
    }
}
