using System.ComponentModel.DataAnnotations;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.Category;

namespace HouseRentingSystemApi.Data.Entities
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		public ICollection<House> Houses { get; set; } = null!;
	}
}
