using FinanceAPI.DTOs;

namespace FinanceAPI.Services.Interfaces;

public interface IAuthService
{
    Task<AuthDtos.AuthResponseDto> RegisterAsync(AuthDtos.RegisterDto dto);
    Task<AuthDtos.AuthResponseDto> LoginAsync(AuthDtos.LoginDto dto);
}