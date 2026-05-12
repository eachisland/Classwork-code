using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Services.Contracts;
using HouseRentingSystemApi.Services.Models.House;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HouseRentingSystemApi.Services.Implementations
{
	public class HouseService(
		AppDbContext data,
		IConfiguration config) : IHouseService
	{
        public async Task<RequestResult<List<HouseDetailModel>>> GetAllAsync()
		{
			var houses = await data
				.Houses
				.AsNoTracking()
				.Select(h => new HouseDetailModel
				{
					Id = h.Id,
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl
				})
				.ToListAsync();

			var configData = config.GetSection("MyConfigData").Value;

			return new()
			{
				Code = 200,
				Message = $"OK and data from config file = {configData}",
				Data = houses
			};
		}

		public async Task<HouseDetailModel?> GetByIdAsync(int id)
			=> await data
				.Houses
				.AsNoTracking()
				.Where(h => h.Id == id)
				.Select(h => new HouseDetailModel
				{
					Id = h.Id,
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl,
					Description = h.Description,
					PricePerMonth = h.PricePerMonth
				})
				.FirstOrDefaultAsync();

		public async Task<HouseDetailModel> CreateAsync(HouseDetailModel model, string? userId)
		{
			var newHouse = new House
			{
				Description = model.Description,
				PricePerMonth = model.PricePerMonth,
				Address = model.Address,
				Title = model.Title,
				ImageUrl = model.ImageUrl,
				AgentId = userId!
			};

			var categoryName = model.Category.ToString();
			var category = await data
				.Categories
				.FirstOrDefaultAsync(c => c.Name == categoryName);

			if (category is null)
			{
				category = new Category
				{
					Name = categoryName
				};

				data.Add(category);
				await data.SaveChangesAsync();
			}

			newHouse.CategoryId = category.Id;

			data.Add(newHouse);
			await data.SaveChangesAsync();

			return new()
			{
				Id = newHouse.Id,
				Address = newHouse.Address,
				ImageUrl = newHouse.ImageUrl,
				Title = newHouse.Title,
				Description = newHouse.Description,
				PricePerMonth = newHouse.PricePerMonth,
				Category = model.Category
			};
		}
	}
}
