using Laba_15.Dtos;
using Laba_15.Models;

namespace Laba_15.Interfaces
{
    public interface IJwtTokenService
    {
        AuthResponse CreateToken(AppUser user);
    }
}
