using System.Data.Entity;
using System.Linq;

namespace Device_Observer.Models
{
    internal class AUContext : DbContext
    {
        private static AUContext auContext = new AUContext();
        public static AUContext AuContext { get { return auContext; } }

        public DbSet<UsersAU> UsersAU { get; set; }

        public static string Role { get; private set; }


        private AUContext() : base("au_dbEntities") { UsersAU.Load(); }

        public static bool Authorization(string login, string password)
        {
            string hashLogin = Hashing.CalculateMD5Hash(login);
            string hashPassword = Hashing.CalculateMD5Hash(password);

            UsersAU user = AuContext.UsersAU.FirstOrDefault( x => x.LoginUserAU.Equals(hashLogin) && x.PasswordUserAU.Equals(hashPassword));

            bool f = !AuContext.UsersAU.Any();
            bool s = !AuContext.UsersAU.Any(x => x.RoleUser == "Admin");

            if (f | s)
            {
                Role = "Admin";
                return true;
            }

            if (user != null)
            {
                Role = user.RoleUser.ToString();
                return true;
            }

            return false;
        }

        public static bool Registration(string login, string password, string details = null)
        {
            string hashLogin = Hashing.CalculateMD5Hash(login);
            string hashPassword = Hashing.CalculateMD5Hash(password);

            using(var transsaction = AuContext.Database.BeginTransaction())
            {
                try
                {
                    UsersAU user = new UsersAU();
                    user.LoginUserAU = hashLogin;
                    user.PasswordUserAU = hashPassword;
                    user.RoleUser = "User";
                    user.Details = details;
                    AuContext.UsersAU.Add(user);
                    AuContext.SaveChanges();
                    transsaction.Commit();
                }
                catch
                {
                    transsaction.Rollback();
                }
            }

            if (AuContext.UsersAU.Any(x => x.LoginUserAU == hashLogin && x.PasswordUserAU == hashPassword))
            {
                return true;
            }

            return false;
        }
    }
}
