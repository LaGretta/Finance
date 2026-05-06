using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceAPI.Data;
using FinanceAPI.DTOs;
using FinanceAPI.Exceptions;
using FinanceAPI.Models;
using FinanceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinanceAPI.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public AuthService(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<AuthDtos.AuthResponseDto> RegisterAsync(AuthDtos.RegisterDto dto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(n => n.UserName == dto.Username);
        if (existingUser != null)
        throw new CustomExceptions.ConflictException("User with this username already exists");

        var user = new User
        {
            UserName = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new AuthDtos.AuthResponseDto
        {
            Token = GenerateToken(user),
            Username = user.UserName
        };
    }

    public async Task<AuthDtos.AuthResponseDto> LoginAsync(AuthDtos.LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(n => n.UserName == dto.Username);
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new CustomExceptions.UnauthorizedException("Invalid username or password");
        
        return new AuthDtos.AuthResponseDto
        {
            Token = GenerateToken(user),
            Username = user.UserName
        };
    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("username", user.UserName),
            new Claim("role", user.Role)
        };
        var token = new JwtSecurityToken
        (
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}