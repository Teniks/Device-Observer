using Device_Observer.Properties;
using System.Data.Entity;

namespace Device_Observer.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Resources> Resources { get; set; }
        public DbSet<AccessRights> AccessRights { get; set; }
        public DbSet<AccessLogs> AccessLogs { get; set; }
        public DbSet<ListOfChanges> ListOfChanges { get; set; }
        private ApplicationContext() : base("name=db_localEntities")
        {

        }

        private static ApplicationContext instance = new ApplicationContext();
        public static ApplicationContext Instance { get { return instance;  } }
    }
}
