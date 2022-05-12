using System;

namespace JWTMaster.Service.Library
{
    public class JwtConfig
    {
        public bool https { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool IssuerSigningKey { get; set; }
        public bool ValidateLifetime { get; set; }
        public string MyKey { get; set; }
        public TimeSpan ClockSkew = TimeSpan.Zero;
    }
}
