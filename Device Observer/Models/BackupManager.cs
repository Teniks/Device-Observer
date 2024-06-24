using System.Data.Entity;
using System.IO;

namespace Device_Observer.Models
{
    internal class BackupManager
    {
        public static void BackupDatabase(string backupFilePath)
        {
            ApplicationContext.Instance.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, 
                $@"BACKUP DATABASE [{Directory.GetCurrentDirectory()}\data\db_local.mdf] TO  " +
                $@"DISK = N'{backupFilePath}' WITH NOFORMAT, NOINIT,  " +
                $@"NAME = N'{backupFilePath}', SKIP, NOREWIND, NOUNLOAD,  STATS = 10");

        }

        public static void RestoreDatabase(string backupFilePath)
        {
            ApplicationContext.Instance.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                $@"RESTORE DATABASE [{Directory.GetCurrentDirectory()}\data\db_local.mdf] FROM " +
                $@"DISK = N'{backupFilePath}' WITH RECOVERY, REPLACE");
        }
    }
}
