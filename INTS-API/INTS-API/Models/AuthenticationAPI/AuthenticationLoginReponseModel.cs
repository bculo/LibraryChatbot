using System.Collections.Generic;

namespace INTS_API.Models.AuthenticationAPI
{
    public class AuthenticationLoginReponseModel
    {
        public bool Success { get; set; } = true;
        public string Token { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
