namespace Pearson_CodingChallenge.Utility
{
    public static class ConnectionString
    {
        private static readonly string connectionName = "Data Source=.; Initial Catalog=StudyGuideOrders;Integrated Security=True;";
        public static string ConnectionName { get => connectionName; }
    }
}
