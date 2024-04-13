using System.Data.SQLite;

namespace AddressCachingService.Database;

public class DatabaseUtils
{
	private readonly string _dbFilePath;

	public DatabaseUtils(string dbFilePath)
	{
		_dbFilePath = dbFilePath;
	}

	public void InitDatabase()
	{
		SQLiteConnection.CreateFile(_dbFilePath);
		
		using var connection = new SQLiteConnection(GetConnectionString());
		CreateAddressCacheTable();
	}

	public string GetConnectionString()
	{
		return $"Data Source={_dbFilePath}";
	}

	private void CreateAddressCacheTable()
	{
		using var connection = new SQLiteConnection(GetConnectionString());
		var cmd = new SQLiteCommand(connection);

		try
		{
			connection.Open();
			cmd.CommandText =
				"CREATE TABLE AddressCache (Id INTEGER,Address_Line_1 TEXT, Address_Line_2 TEXT, Address_Line_3 TEXT, " +
				"Address_Line_4 TEXT, Address_Line_5 TEXT, Postal_Code TEXT NOT NULL, Building_Number TEXT, " +
				"Building_Name TEXT, County TEXT, City TEXT,Country_Name TEXT, Company TEXT, PO_Box_Number TEXT, " +
				"Type STRING, Created TEXT, PRIMARY KEY(Id));";
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