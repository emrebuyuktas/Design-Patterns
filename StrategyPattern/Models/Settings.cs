namespace StrategyPattern.Models
{
    public class Settings
    {
        public static string CalimDatabaseType = "DatabaseType";
        public DatabaseType DatabaseType;
        public DatabaseType GetDefaultType => DatabaseType.SqlServer;
    }
}
