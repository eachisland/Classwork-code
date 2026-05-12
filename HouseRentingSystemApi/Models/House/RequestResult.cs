namespace HouseRentingSystemApi.Models.House;

public class RequestResult<T>
{
	public int Code { get; init; }

	public string Message { get; init; } = null!;

	public T Data { get; init; } = default!;
}
