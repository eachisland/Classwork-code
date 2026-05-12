namespace HouseRentingSystemApi.Data;

using System.Reflection;
using HouseRentingSystemApi.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
    
public class AppDbContext(
	DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<House> Houses { get; init; }

	public DbSet<Category> Categories { get; init; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly());
	}
}
