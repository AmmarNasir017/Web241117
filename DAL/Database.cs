using Microsoft.Data.Sqlite;

namespace WebAPIDemo.DAL
{
    public class Database
    {
        public static string ConnectionString = "Data Source=Data/ToDo.db;";
        
        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}