using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTS_API.Models.BookAPI
{
    public class ReservationRequest
    {
        public string Username { get; set; }
        public string Book { get; set; }
    }
}
