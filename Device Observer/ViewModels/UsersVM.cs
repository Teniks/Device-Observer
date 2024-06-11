using Device_Observer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Device_Observer.ViewModels
{
    internal class UsersVM : IViewModel, INotifyPropertyChanged
    {
        private Users selected;
        public Users Selected { get { return selected; } set { selected = value; } }

        public ObservableCollection<Users> users { get; private set; }

        public UsersVM()
        {
            UpdateCollection();
        }

        public void UpdateCollection()
        {
            ApplicationContext.Instance.Users?.Load();
            users = ApplicationContext.Instance.Users?.Local;
        }

        public ObservableCollection<Users> SearchByString(string line)
        {
            return new ObservableCollection<Users>(
                users.Where(x => x.FullNameUser.Contains(line) || x.RoleUser.Contains(line)).ToList());
        }

        public ObservableCollection<Users> FilterByString(string line)
        {
            return new ObservableCollection<Users>(
                users.Where(x => !x.FullNameUser.Contains(line) && !x.RoleUser.Contains(line)).ToList());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand( obj =>
                    {
                        Users user = obj as Users;
                        users.Insert(0, user);
                        Selected = user;
                        using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                        {
                            try
                            {
                                ApplicationContext.Instance.Users.Add(user);
                                ApplicationContext.Instance.SaveChanges();

                                transaction.Commit();
                                OnPropertyChanged();
                            }
                            catch
                            {
                                transaction.Rollback();
                            }
                        }
                    }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (
                    removeCommand = new RelayCommand(obj =>
                    {
                        Users user = obj as Users;
                        if (user != null)
                        {
                            users.Remove(user);

                            using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                            {
                                try
                                {
                                    ApplicationContext.Instance.Users.Remove(user);
                                    ApplicationContext.Instance.SaveChanges();

                                    transaction.Commit();
                                    OnPropertyChanged();
                                }
                                catch
                                {
                                    transaction.Rollback();
                                }
                            }
                        }
                    }, (obj) => users.Count > 0 && Selected != null
                    ));
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ?? (
                    updateCommand = new RelayCommand(obj =>
                    {
                        using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                        {
                            try
                            {
                                Users user = obj as Users;
                                Selected = ApplicationContext.Instance.Users.First(x => x.IdUser == Selected.IdUser);
                                if (user != null)
                                {
                                    Selected.FullNameUser = user.FullNameUser;
                                    Selected.RoleUser = user.RoleUser;
                                }
                                ApplicationContext.Instance.SaveChanges();

                                transaction.Commit();
                                OnPropertyChanged();
                            }
                            catch
                            {
                                transaction.Rollback();
                            }

                        }
                    }, (obj) => Selected != null
                    ));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
