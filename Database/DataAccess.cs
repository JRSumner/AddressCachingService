using System.Data.SQLite;
using AddressCachingService.Common;
using AddressCachingService.Models;

namespace AddressCachingService.Database;

public class DataAccess
{
	private readonly DatabaseUtils _databaseUtils;
	private readonly string _dbFilePath;

	public DataAccess()
	{
		var projectDirectory = Directory.GetCurrentDirectory();
		var databaseDirectory = Path.Combine(projectDirectory, Constants.DbDirName);
		_dbFilePath = Path.Combine(databaseDirectory, Constants.DbFileName);
		_databaseUtils = new DatabaseUtils(_dbFilePath);
		OnConfiguring();
	}

	private void OnConfiguring()
	{

		if (!File.Exists(_dbFilePath))
		{
			_databaseUtils.InitDatabase();
		}
	}

	public List<AddressData> GetAddressesByPostcode(string postcode)
	{
		using var connection = new SQLiteConnection(_databaseUtils.GetConnectionString());
		var cmd = connection.CreateCommand();
		var addresses = new List<AddressData>();

		try
		{
			connection.Open();
			cmd.CommandText = "SELECT * FROM AddressCache WHERE Postal_code = @PostalCode";
			cmd.Parameters.AddWithValue("@PostalCode", postcode);
			cmd.ExecuteNonQuery();

			using var reader = cmd.ExecuteReader();
			{
				while (reader.Read())
				{
					var address = new AddressData()
					{
						AddressLine1 = reader.GetString(reader.GetOrdinal("Address_Line_1")),
						AddressLine2 = reader.GetString(reader.GetOrdinal("Address_Line_2")),
						AddressLine3 = reader.GetString(reader.GetOrdinal("Address_Line_3")),
						AddressLine4 = reader.GetString(reader.GetOrdinal("Address_Line_4")),
						AddressLine5 = reader.GetString(reader.GetOrdinal("Address_Line_5")),
						PostalCode = reader.GetString(reader.GetOrdinal("Postal_Code")),
						BuildingNumber = reader.GetString(reader.GetOrdinal("Building_Number")),
						BuildingName = reader.GetString(reader.GetOrdinal("Building_Name")),
						County = reader.GetString(reader.GetOrdinal("County")),
						City = reader.GetString(reader.GetOrdinal("City")),
						CountryName = reader.GetString(reader.GetOrdinal("Country_Name")),
						Company = reader.GetString(reader.GetOrdinal("Company")),
						POBoxNumber = reader.GetString(reader.GetOrdinal("PO_Box_Number")),
						Type = reader.GetString(reader.GetOrdinal("Type"))
					};
					addresses.Add(address);
				}
			}

			return addresses;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
		finally
		{
			connection.Close();
			connection.Dispose();
			cmd.Dispose();
		}
	}

	public void InsertAddress(AddressData addressData)
	{
		using var connection = new SQLiteConnection(_databaseUtils.GetConnectionString());
		var cmd = connection.CreateCommand();

		try
		{
			connection.Open();
			cmd.CommandText = @"
                INSERT INTO addressData (Address_Line_1, Address_Line_2, Address_Line_3, Address_Line_4, Address_Line_5,
                    Postal_Code, Building_Number, Building_Name, County, City, Country_Name, Company, PO_Box_Number, Type, Created) 
				VALUES 
					(@AddressLine1, @AddressLine2, @AddressLine3, @AddressLine4, @AddressLine5, @PostalCode, @BuildingNumber,
                    @BuildingName, @County, @City, @CountryName, @Company, @POBoxNumber, @Type, @Created );";

			cmd.Parameters.AddWithValue("@AddressLine1", addressData.AddressLine1);
			cmd.Parameters.AddWithValue("@AddressLine2", addressData.AddressLine2);
			cmd.Parameters.AddWithValue("@AddressLine3", addressData.AddressLine3);
			cmd.Parameters.AddWithValue("@AddressLine4", addressData.AddressLine4);
			cmd.Parameters.AddWithValue("@AddressLine5", addressData.AddressLine5);
			cmd.Parameters.AddWithValue("@PostalCode", addressData.PostalCode);
			cmd.Parameters.AddWithValue("@BuildingNumber", addressData.BuildingNumber);
			cmd.Parameters.AddWithValue("@BuildingName", addressData.BuildingName);
			cmd.Parameters.AddWithValue("@County", addressData.County);
			cmd.Parameters.AddWithValue("@City", addressData.City);
			cmd.Parameters.AddWithValue("@CountryName", addressData.CountryName);
			cmd.Parameters.AddWithValue("@Company", addressData.Company);
			cmd.Parameters.AddWithValue("@POBoxNumber", addressData.POBoxNumber);
			cmd.Parameters.AddWithValue("@Type", addressData.Type);
			cmd.ExecuteNonQuery();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
		finally
		{
			connection.Close();
			connection.Dispose();
			cmd.Dispose();
		}

	}
}