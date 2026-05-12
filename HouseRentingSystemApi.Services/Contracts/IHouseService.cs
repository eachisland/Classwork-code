namespace HouseRentingSystemApi.Services.Contracts;

using Models.House;

public interface IHouseService
{
	Task<RequestResult<List<HouseDetailModel>>> GetAllAsync();

	Task<HouseDetailModel?> GetByIdAsync(int id);

	Task<HouseDetailModel> CreateAsync(
		HouseDetailModel model,
		string? userId);
}
