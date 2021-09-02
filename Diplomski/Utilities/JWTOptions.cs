using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Utilities
{
    // JWT options binded from appsettings.json
    public class JWTOptions
    {
        public const string JWT = "JWT";
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
