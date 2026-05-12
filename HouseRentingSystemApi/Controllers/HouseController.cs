namespace HouseRentingSystemApi.Controllers;

using System.Security.Claims;
using HouseRentingSystemApi.Services.Contracts;
using HouseRentingSystemApi.Services.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    
[Route("api/[controller]")]
public class HouseController(IHouseService service) : ControllerBase
{
	[HttpGet]
	[Produces(typeof(RequestResult<IEnumerable<HouseDetailModel>>))]
	public async Task<IActionResult> GetAll()
	{
		var result = await service.GetAllAsync();
		return this.Ok(result);
	}

	[HttpGet("{id}")]
	[Produces(typeof(HouseDetailModel))]
	public async Task<IActionResult> GetById(int id)
	{
		var house = await service.GetByIdAsync(id);
		if (house is null)
		{
			return this.NotFound();
		}

		return this.Ok(house);
	}

	[Authorize]
	[HttpPost]
	[Produces(typeof(HouseDetailModel))]
	public async Task<IActionResult> Create(
		[FromBody] HouseDetailModel model)
	{
		if (!this.ModelState.IsValid)
		{
			return BadRequest();
		}

		var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
		var createdHouse = await service.CreateAsync(model, userId);

		return this.CreatedAtAction(
			nameof(this.GetById),
			new { id = createdHouse.Id },
			createdHouse);
	}

	[Authorize]
	[HttpPut("{id}")]
	public async Task<IActionResult> Edit(
		int id,
		HouseDetailModel model)
	{
		if (!this.ModelState.IsValid)
		{
			var allErrors = this
				.ModelState
				.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage)
				.ToArray();

			return this.BadRequest(string.Join(", ", allErrors));
		}

		return await Task.FromResult(this.Ok());
	}

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
		=> await Task.FromResult(this.Ok());
}
