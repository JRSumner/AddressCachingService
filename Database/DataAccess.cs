using AddressCachingService.Common;

namespace AddressCachingService.Database;

public class DataAccess
{
	private readonly DatabaseUtils _databaseUtils;
	private readonly string _dbFilePath;

	public DataAccess()
	{
		var projectDirectory = Directory.GetCurrentDirectory();
		var dbDirName = "Database";
		var databaseDirectory = Path.Combine(projectDirectory, dbDirName);
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

	public string GetAddressesByPostcode(string postcode)
	{
		return "";
	}
}