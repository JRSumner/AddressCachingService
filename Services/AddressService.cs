using AddressCachingService.Database;
using AddressCachingService.Models;

namespace AddressCachingService.Services
{
	public class AddressService
	{
		private readonly DataAccess _dataAccess;
		public AddressService()
		{
			_dataAccess = new DataAccess();
		}

		public List<AddressData> GetAddressesByPostcode(string postcode)
		{
			return _dataAccess.GetAddressesByPostcode(postcode);
		}
	}
}
