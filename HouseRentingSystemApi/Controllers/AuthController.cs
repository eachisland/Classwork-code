namespace HouseRentingSystemApi.Controllers;

using HouseRentingSystemApi.Services.Contracts;
using HouseRentingSystemApi.Services.Models.Auth;
using Microsoft.AspNetCore.Mvc;
    
[Route("api/[controller]")]
public class AuthController(IAuthService service) : Controller
{
	[HttpPost("login")]
	[Produces(typeof(AuthResult))]
	public async Task<IActionResult> Login([FromBody] AuthModel model)
	{
		if (!this.ModelState.IsValid)
		{
			var allErrors = this.ModelState
				.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage)
				.ToArray();

			var authResult = PopulateResult(400, null, allErrors);
            return this.BadRequest(authResult);
		}

		var result = await service.LoginAsync(model);
		if (result.Code != 200)
		{
			return this.Unauthorized(result);
		}

		return this.Ok(result);
	}

	[HttpPost("register")]
	[Produces(typeof(AuthResult))]
	public async Task<IActionResult> Resgister(
		[FromBody] AuthModel model)
	{
		if (!this.ModelState.IsValid)
		{
			var allErrors = this.ModelState
				.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage)
				.ToArray();

			var authResult = PopulateResult(400, null, allErrors);
            return this.Unauthorized(authResult);
		}

		var result = await service.RegisterAsync(model);
		if (result.Code != 200)
		{
			return this.BadRequest(result);
		}

		return this.Ok(result);
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
