namespace HouseRentingSystemApi.Services.Models.House;

using System.ComponentModel.DataAnnotations;
using HouseRentingSystemApi.Services.Models.Enums;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.House;

public class HouseDetailModel
{
	public int Id { get; set; }

	[MaxLength(TitleMaxLength)]
	[Required(ErrorMessage = "Test Error Msg")]
	public string Title { get; set; } = string.Empty;

	[MaxLength(AddressMaxLength)]
	public string Address { get; set; } = string.Empty;

	public string ImageUrl { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public decimal PricePerMonth { get; set; }

	public CategoryViewEnum Category { get; set; }
}
