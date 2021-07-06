using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOSAPI.Core
{
    public class AuthService
    {
        public bool Authenticate(string userName, string password)
        {
            return true;
        }

        public bool Unauthenticate()
        {
            return true;
        }
    }
}
