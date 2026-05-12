namespace HouseRentingSystemApi.Models.Auth;

public class AuthResult
{
	public int Code { get; init; }

	public string Message { get; init; } = null!;

	public string Token { get; init; } = null!;
}
