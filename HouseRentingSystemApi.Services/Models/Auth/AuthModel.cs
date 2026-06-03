namespace HouseRentingSystemApi.Services.Models.Auth;

using System.ComponentModel.DataAnnotations;

public class AuthModel
{
	[Required]
	[StringLength(20, MinimumLength = 3)]
	public string Username { get; set; } = string.Empty;

	[Required]
	[StringLength(50, MinimumLength = 6)]
	public string Password { get; set; } = string.Empty;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// Role to assign on registration. Must be "Agent" or "Client". Defaults to "Client".
	/// </summary>
	public string Role { get; set; } = "Client";
}
