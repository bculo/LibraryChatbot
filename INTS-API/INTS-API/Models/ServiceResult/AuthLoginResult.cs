using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTS_API.Models.ServiceResult
{
    public class AuthLoginResult : ServiceResult
    {
        public string Token { get; set; }
    }
}
