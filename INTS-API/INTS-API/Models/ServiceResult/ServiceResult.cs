using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTS_API.Models.ServiceResult
{
    public abstract class ServiceResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
