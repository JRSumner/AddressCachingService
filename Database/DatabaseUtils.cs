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

	private string GetConnectionString()
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
				"CREATE TABLE \"AddressCache\" (\r\n\t\"Id\"\tINTEGER,\r\n\t\"Address_Line_1\"\tTEXT,\r\n\t\"Address_Line_2\"\tTEXT,\r\n\t\"Address_Line_3\"\tTEXT,\r\n\t\"Address_Line_4\"\tTEXT,\r\n\t\"Address_Line_5\"\tTEXT,\r\n\t\"Postal_Code\"\tTEXT NOT NULL,\r\n\t\"Building_Number\"\tTEXT,\r\n\t\"Building_Name\"\tTEXT,\r\n\t\"County\"\tTEXT,\r\n\t\"City\"\tTEXT,\r\n\t\"Country_Name\"\tTEXT,\r\n\t\"Company\"\tTEXT,\r\n\t\"PO_Box_Number\"\tTEXT,\r\n\t\"Type\"\tINTEGER,\r\n\t\"Created\"\tTEXT,\r\n\tPRIMARY KEY(\"Id\")\r\n);";
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