namespace HouseRentingSystemApi.Services.Implementations;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Services.Contracts;
using HouseRentingSystemApi.Services.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
    
public class AuthService(
	UserManager<AppUser>
	userManager, IConfiguration config) : IAuthService
{
    public async Task<AuthResult> LoginAsync(AuthModel model)
	{
		var user = await userManager.FindByEmailAsync(model.Email);
		if (user is null)
		{
			return PopulateResult(400, null, "Invalid email or password");
		}

		var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
		if (!isPasswordValid)
		{
			return PopulateResult(400, null, "Invalid email or password");
		}

		var token = GenerateJwtToken(user);
		return PopulateResult(200, token, "User logged in successfully");
	}

	public async Task<AuthResult> RegisterAsync(AuthModel model)
	{
		var user = await userManager.FindByEmailAsync(model.Email);

		if (user != null)
		{
			return PopulateResult(400, null, "User Already exists");
		}

		var newUser = new AppUser
		{
			Email = model.Email,
			UserName = model.Username
		};

		var result = await userManager.CreateAsync(newUser, model.Password);

		if (result.Succeeded)
		{
			return PopulateResult(200, null, "User registered Successfully");
		}

		return PopulateResult(
			400,
			null,
			result.Errors.Select(e => e.Description).ToArray());
	}

	private string GenerateJwtToken(AppUser user)
	{
		var jwtSection = config.GetSection("Jwt");
		var key = jwtSection["Key"]!;

		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, user.Id),
			new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
			new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
			new(ClaimTypes.NameIdentifier, user.Id),
			new(ClaimTypes.Name, user.UserName!)
		};

		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
		var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSection["ExpiresMinutes"]!));

		var token = new JwtSecurityToken(
			issuer: jwtSection["Issuer"],
			audience: jwtSection["Audience"],
			claims: claims,
			expires: expires,
			signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

    private static AuthResult PopulateResult(
        int code,
        string? token = null,
        params string[] messages)
		=> new()
        {
            Code = code,
            Message = string.Join(Environment.NewLine, messages),
            Token = token
        };
}
