namespace DotNetBar.DataAccess.Config;

public class BarManagementDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string BarsCollectionName { get; set; } = null!;

    public bool Init { get; set; }
}
