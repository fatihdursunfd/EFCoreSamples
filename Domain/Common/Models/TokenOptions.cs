﻿namespace Domain.Common.Models
{
    public class TokenOptions
    {
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public string? SecurityKey { get; set; }
        public int AccessTokenExpiration { get; set; }
    }
}