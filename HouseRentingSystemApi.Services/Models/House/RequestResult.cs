namespace HouseRentingSystemApi.Services.Models.House;

public class RequestResult<T>
{
	public int Code { get; set; }

	public string Message { get; set; } = string.Empty;

	public T? Data { get; set; }
}
