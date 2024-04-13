using System.Net;
using AddressCachingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressCachingService.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
	private readonly ILogger<AddressController> _logger;
	private readonly AddressService _addressService;

	public AddressController(ILogger<AddressController> logger)
	{
		_logger = logger;
		_addressService = new AddressService();
	}

	[HttpGet(Name = "GetAddresses")]
	public IActionResult Get(string postcode)
	{
		try
		{
			var addressList = _addressService.GetAddressesByPostcode(postcode);

			if (addressList.Count <= 0)
				return BadRequest($"Unable to obtain any addresses using postcode:{postcode}");

			var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(addressList);
			return Ok(jsonResult);

		}
		catch (Exception)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError,
				"An error occurred while processing the request.");
		}
	}
}
