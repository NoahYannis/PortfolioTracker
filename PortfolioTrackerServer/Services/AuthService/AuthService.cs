﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioTrackerServer.Data;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace PortfolioTrackerServer.Services.AuthService;

public class AuthService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : IAuthService
{
    private readonly DataContext _dataContext = dataContext;
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var response = new ServiceResponse<string>() { Success = false };
        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

        if (user is null)
        {
            response.Message = "User not found.";
        }
        else if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) is false)
        {
            response.Message = "Wrong password.";
        }
        else
        {
            response.Success = true;
            response.Data = CreateToken(user);
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
    {
        var user = await _dataContext.Users.FindAsync(userId);

        if (user is null)
            return new ServiceResponse<bool> { Success = false, Message = "User not found." };

        CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    public async Task<bool> UserExists(string email)
    {
        return await _dataContext.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower()));
    }


    /// <summary>
    /// Returns a user from the database with a given email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<User>> GetUserFromDbByEmail(string email)
    {
        ServiceResponse<User> response = new();

        if (!await UserExists(email))
        {
            response.Success = false;
            response.Data = null;
            response.Message = $"User with email '{email}' does not exist.";
        }
        else
        {
            response.Data = await _dataContext.Users.FirstOrDefaultAsync
                (user => user.Email.ToLower().Equals(email.ToLower()));
        }

        return response;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        if (await UserExists(user.Email))
            return new ServiceResponse<int> { Success = false, Message = "User already exists." };

        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;

        _dataContext.Users.Add(user);
        _dataContext.UserSettings.Add(user.Settings);
        _dataContext.Portfolios.Add(user.Portfolio);

        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<int> { Data = user.UserId, Message = "Registration successful." };
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
