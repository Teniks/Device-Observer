using System.Data;
using System.Data.SqlClient;

namespace Device_Observer.Models
{
    internal class BackupManager
    {
        public static void BackupDatabase(string backupFilePath)
        {
            ApplicationContext.Instance.Database.ExecuteSqlCommand("EXEC BackupDataBase @backpath", new SqlParameter("@backath", backupFilePath));
        }

        public static void RestoreDatabase(string backupFilePath)
        {
            ApplicationContext.Instance.Database.ExecuteSqlCommand("EXEC RestoreDatabase @backpath", new SqlParameter("@backath", backupFilePath));
        }
    }
}
