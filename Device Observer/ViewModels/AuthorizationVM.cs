using Device_Observer.Models;

namespace Device_Observer.ViewModels
{
    internal class AuthorizationVM
    {
        public string Role { get {  return AUContext.Role; } }
        public AuthorizationVM() 
        {

        }

        public bool Authorization(string login, string password)
        {
            return AUContext.Authorization(login, password);
        }

        public bool Registration(string login, string password, string details = null)
        {
            if (Role.Equals("Admin"))
            {
                return AUContext.Registration(login, password, details);
            }

            return false;
        }
    }
}
