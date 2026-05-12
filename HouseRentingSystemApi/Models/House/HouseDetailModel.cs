namespace HouseRentingSystemApi.Models.House;

using System.ComponentModel.DataAnnotations;
using HouseRentingSystemApi.Models.Enums;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.House;
    
public class HouseDetailModel
{
	[MaxLength(TitleMaxLength)]
	[Required(ErrorMessage = "Test Error Msg")]
	public string Title { get; set; } = null!;

	[MaxLength(AddressMaxLength)]
	public string  Address { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal PricePerMonth { get; set; }

	public CategoryViewEnum Category { get; set; }
}
