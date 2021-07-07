using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOSAPI.Contexts;
using HOSAPI.Utils;

namespace HOSAPI.Core
{
    public class AuthService
    {
        private readonly SocarDbContext _socarDbContext;

        public AuthService(SocarDbContext socarDbContext)
        {
            _socarDbContext = socarDbContext;
        }
        public bool Authenticate(string userName, string password)
        {
            //var md5Password = Misc.MD5Encrypt(password);

            //var user = _socarDbContext.User.Where(filter =>
            //    filter.User_Name.Contains(userName) && filter.User_Pwd.Contains(md5Password)).ToList();

            //return user.Count == 1;

            return true;
        }

    }
}
