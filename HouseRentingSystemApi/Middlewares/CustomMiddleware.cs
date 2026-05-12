namespace HouseRentingSystemApi.Middlewares;

using HouseRentingSystemApi.Data;
using Microsoft.EntityFrameworkCore;
    
public class CustomMiddleware(RequestDelegate next)
    {
	private readonly RequestDelegate next = next;

        public async Task InvokeAsync(
		HttpContext httpContext,
		AppDbContext data) 
	{
		var housesCount = await data
			.Houses
			.CountAsync();

		Console.WriteLine($"Total houses in DB = {housesCount}");
		
		await this.next(httpContext);

		var housesCountNew = await data.Houses.CountAsync();
		if (housesCount != housesCountNew)
		{
			Console.WriteLine($"There is {housesCountNew- housesCount} new houses");
		}
		else
		{
			Console.WriteLine("No new houses added.");
		}
	}
}
