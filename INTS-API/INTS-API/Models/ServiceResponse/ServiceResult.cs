using System.Collections.Generic;

namespace INTS_API.Models.ServiceResponse
{
    public abstract class ServiceResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
