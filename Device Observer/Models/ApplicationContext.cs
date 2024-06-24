using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

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

        public DataGrid QuerySQL()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ФИО", Binding = new Binding("FullNameUser") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название ресурса", Binding = new Binding("NameResource") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Тип ресурса", Binding = new Binding("TypeResource") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Дозволения", Binding = new Binding("Permission") });

            var select = from user in Users.ToList()
                         join accessRight in AccessRights on user.IdUser equals accessRight.IdUser
                         join resource in Resources on accessRight.IdResource equals resource.IdResource
                         select new
                         {
                             FullNameUser = user.FullNameUser,
                             NameResource = resource.NameResource,
                             TypeResource = resource.TypeResource,
                             Permission = accessRight.Permission
                         };

            dataGrid.Items.Clear();
            dataGrid.ItemsSource = select;

            return dataGrid;
        }
    }
}
