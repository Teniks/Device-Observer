using Device_Observer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Device_Observer.ViewModels
{
    internal class UsersAUVM : IViewModel, INotifyPropertyChanged
    {
        private UsersAU selected;
        public UsersAU Selected { get { return selected; } set { selected = value; } }

        public ObservableCollection<UsersAU> users { get; private set; }

        public UsersAUVM() 
        {
            UpdateCollection();
        }

        public void UpdateCollection()
        {
            AUContext.AuContext.UsersAU?.Load();
            users = AUContext.AuContext.UsersAU?.Local;
        }

        public ObservableCollection<UsersAU> SearchByString(string line)
        {
            return new ObservableCollection<UsersAU>(
                users.Where(x => x.RoleUser.Contains(line) || x.Details.Contains(line)).ToList());
        }

        public ObservableCollection<UsersAU> FilterByString(string line)
        {
            return new ObservableCollection<UsersAU>(
                users.Where(x => !x.RoleUser.Contains(line) && x.Details.Contains(line)).ToList());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand(obj =>
                    {
                        UsersAU user = obj as UsersAU;
                        users.Insert(0, user);
                        Selected = user;
                        using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                        {
                            try
                            {
                                AUContext.AuContext.UsersAU.Add(user);
                                AUContext.AuContext.SaveChanges();

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
                        UsersAU user = obj as UsersAU;
                        if (user != null)
                        {
                            users.Remove(user);

                            using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                            {
                                try
                                {
                                    AUContext.AuContext.UsersAU.Remove(user);
                                    AUContext.AuContext.SaveChanges();

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
                                UsersAU user = obj as UsersAU;
                                Selected = AUContext.AuContext.UsersAU.First(x => x.IdUserAU == Selected.IdUserAU);
                                if (user != null)
                                {
                                    Selected.RoleUser = user.RoleUser;
                                }
                                AUContext.AuContext.SaveChanges();

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
