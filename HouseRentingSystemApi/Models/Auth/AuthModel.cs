namespace HouseRentingSystemApi.Models.Auth;

using System.ComponentModel.DataAnnotations;

public class AuthModel
{
	[Required]
	[StringLength(20, MinimumLength = 3)]
	public string Username { get; init; } = null!;

	[Required]
	[StringLength(50, MinimumLength = 6)]
	public string Password { get; init; } = null!;

    [Required]
	[EmailAddress]
	public string  Email { get; init; } = null!;
}
