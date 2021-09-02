using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
