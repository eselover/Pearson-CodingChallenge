namespace Pearson_CodingChallenge.Utility
{
    public static class ConnectionString
    {
        private static readonly string connectionName = "Data Source=.; Initial Catalog=StudyGuideOrderManagement;User ID=admin;Password=admin123";
        public static string ConnectionName { get => connectionName; }
    }
}
