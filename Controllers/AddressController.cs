using AddressCachingService.Database;
using Microsoft.AspNetCore.Mvc;

namespace AddressCachingService.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
	private readonly ILogger<AddressController> _logger;
	private DataAccess _dataAccess;

	public AddressController(ILogger<AddressController> logger)
	{
		_logger = logger;
		_dataAccess = new DataAccess();
	}

	[HttpGet(Name = "GetAddresses")]
	public string Get()
	{
		_dataAccess.GetAddressesByPostcode("");
		return "Test";
	}
}
