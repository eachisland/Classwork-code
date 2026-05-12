namespace HouseRentingSystemApi.Services.Contracts;

using Models.Auth;

public interface IAuthService
{
	Task<AuthResult> LoginAsync(AuthModel model);

	Task<AuthResult> RegisterAsync(AuthModel model);
}
