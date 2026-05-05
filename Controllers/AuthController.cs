using FinanceAPI.DTOs;
using FinanceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<AuthDtos.RegisterDto> _registerValidator;

    public AuthController(IAuthService authService, IValidator<AuthDtos.RegisterDto> registerValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthDtos.RegisterDto dto)
    {
        var validation = await _registerValidator.ValidateAsync(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(x => x.ErrorMessage));
        
        var result = await _authService.RegisterAsync(dto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthDtos.LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }
    
}