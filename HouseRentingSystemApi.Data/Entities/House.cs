namespace HouseRentingSystemApi.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.House;
    
public class House
{
	[Key]
	public int Id { get; init; }

	[Required]
	[MaxLength(TitleMaxLength)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(AddressMaxLength)]
	public string Address { get; set; } = null!;

	[Required]
	[MaxLength(DescriptionMaxLength)]
	public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

	public decimal PricePerMonth { get; set; }

	public Category Category { get; set; } = null!;

    [ForeignKey(nameof(Category))]
	public int CategoryId { get; set; }

	public AppUser? Agent { get; set; }

	[ForeignKey(nameof(Agent))]
	public string AgentId { get; set; } = null!;

    public AppUser Renter { get; set; } = null!;

    [ForeignKey(nameof(Renter))]
    public string? RenterId { get; set; }
}
