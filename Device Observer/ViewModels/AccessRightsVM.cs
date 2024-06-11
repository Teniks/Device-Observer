using Device_Observer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Device_Observer.ViewModels
{
    internal class AccessRightsVM : IViewModel, INotifyPropertyChanged
    {
        private AccessRights selected;
        public AccessRights Selected { get { return selected; } set { selected = value; } }

        public ObservableCollection<AccessRights> accessRights { get; private set; }

        public AccessRightsVM()
        {
            UpdateCollection();
        }

        public void UpdateCollection()
        {
            ApplicationContext.Instance.AccessRights?.Load();
            accessRights = ApplicationContext.Instance.AccessRights?.Local;
        }

        public ObservableCollection<AccessRights> SearchByString(string line)
        {
            return new ObservableCollection<AccessRights>(
                accessRights.Where(x => x.Permission.Contains(line) || 
                ApplicationContext.Instance.Users.Where(y => y.FullNameUser.Contains(line) || y.RoleUser.Contains(line))
                .Any(xy => xy.IdUser == x.IdUser) ||
                ApplicationContext.Instance.Resources.Where(z => z.NameResource.Contains(line) || z.TypeResource.Contains(line) || z.DescriptionResource.Contains(line))
                .Any(w => w.IdResource == x.IdResource)).ToList());
        }

        public ObservableCollection<AccessRights> FilterByString(string line)
        {
            return new ObservableCollection<AccessRights>(
                accessRights.Where(x => !x.Permission.Contains(line) &&
                ApplicationContext.Instance.Users.Where(y => !y.FullNameUser.Contains(line) && !y.RoleUser.Contains(line))
                .Any(xy => xy.IdUser == x.IdUser) &&
                ApplicationContext.Instance.Resources.Where(z => !z.NameResource.Contains(line) && !z.TypeResource.Contains(line) && !z.DescriptionResource.Contains(line))
                .Any(w => w.IdResource == x.IdResource)).ToList());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand(obj =>
                    {
                        AccessRights accessRights = obj as AccessRights;
                        this.accessRights.Insert(0, accessRights);
                        Selected = accessRights;

                        using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                        {
                            try
                            {
                                ApplicationContext.Instance.AccessRights.Add(accessRights);
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
                        AccessRights accessRights = obj as AccessRights;
                        if (accessRights != null)
                        {
                            this.accessRights.Remove(accessRights);

                            using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                            {
                                try
                                {
                                    ApplicationContext.Instance.AccessRights.Remove(accessRights);
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
                    }, (obj) => accessRights.Count > 0 && Selected != null
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
                        AccessRights accessRights = obj as AccessRights;
                        if (accessRights != null)
                        {


                            using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                            {
                                try
                                {
                                    Selected = ApplicationContext.Instance.AccessRights.First(x => x.IdRight == Selected.IdRight);

                                    Selected.IdResource = accessRights.IdResource;
                                    Selected.IdUser = accessRights.IdUser;
                                    Selected.Permission = accessRights.Permission;
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
