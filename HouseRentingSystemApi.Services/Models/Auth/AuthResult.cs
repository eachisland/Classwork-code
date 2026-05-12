namespace HouseRentingSystemApi.Services.Models.Auth;

public class AuthResult
{
	public int Code { get; set; }

	public string Message { get; set; } = string.Empty;

	public string? Token { get; set; }
}
