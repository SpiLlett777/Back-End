﻿namespace Laba_15.Dtos
{
    public class AuthResponse
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
