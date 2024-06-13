using System.Data.SqlClient;

namespace Device_Observer.Models
{
    internal class BackupManager
    {
        public static void BackupDatabase(string connectionString, string backupFilePath)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string databaseName = connection.Database;
                using (SqlCommand command = new SqlCommand($"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}'", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void RestoreDatabase(string connectionString, string backupFilePath)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string databaseName = connection.Database;
                using (SqlCommand command = new SqlCommand($"RESTORE DATABASE [{databaseName}] FROM DISK = '{backupFilePath}' WITH REPLACE", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
