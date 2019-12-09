using System.Collections.Generic;

namespace INTS_API.Models.AuthenticationAPI
{
    public class AuthenticationRegistrationResponseModel
    {
        public bool Success { get; set; } = true;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
