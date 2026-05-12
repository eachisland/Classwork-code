namespace HouseRentingSystemApi.Data.Entities;

using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public List<House> Houses { get; set; } = null!;
}
