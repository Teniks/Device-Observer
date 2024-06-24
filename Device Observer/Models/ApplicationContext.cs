using System.Data.Entity;
using System.Data.SqlClient;
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

        public DataGrid QuerySQL2()
        {
            string query = @"
                                                                                 SELECT
                                                                                    Users.FullNameUser as FullNameUser,
                                                                                    Resources.NameResource as NameResource,
                                                                                    Resources.TypeResource as TypeResource,
                                                                                    AccessRights.Permission as Permission
                                                                                FROM
                                                                                    Users
                                                                                JOIN
                                                                                    AccessRights ON Users.IdUser = AccessRights.IdUser
                                                                                JOIN
                                                                                    Resources ON AccessRights.IdResource = Resources.IdResource";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Vtx\\source\\repos\\Device Observer\\Device Observer\\data\\db_local.mdf\";Integrated Security=True";
            SqlDataReader sqlDataReader;
            DataGrid dataGrid = new DataGrid();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                using (sqlDataReader = sqlCommand.ExecuteReader())
                {
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = "ФИО", Binding = new Binding("FullNameUser") });
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название ресурса", Binding = new Binding("NameResource") });
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = "Тип ресурса", Binding = new Binding("TypeResource") });
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = "Дозволения", Binding = new Binding("Permission") });

                    while (sqlDataReader.Read())
                    {
                        string fullName = sqlDataReader["FullNameUser"].ToString();
                        string resourceName = sqlDataReader["NameResource"].ToString();
                        string typeResource = sqlDataReader["TypeResource"].ToString();
                        string permission = sqlDataReader["Permission"].ToString();

                        var data = new { FullNameUser = fullName, NameResource = resourceName, TypeResource = typeResource, Permission = permission };

                        dataGrid.Items.Add(data);
                    }
                }
                
                sqlConnection.Close();
            }

            return dataGrid;
        }

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
