using Device_Observer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Device_Observer.ViewModels
{
    internal class ResourcesVM : IViewModel, INotifyPropertyChanged
    {
        private Resources selected;
        public Resources Selected { get { return selected; } set { selected = value; } }

        public ObservableCollection<Resources> resources { get; private set; }

        public ResourcesVM()
        {
            UpdateCollection();
        }

        public void UpdateCollection()
        {
            ApplicationContext.Instance.Resources?.Load();
            resources = ApplicationContext.Instance.Resources?.Local;
        }

        public ObservableCollection<Resources> SearchByString(string line)
        {
            return new ObservableCollection<Resources>(
                resources.Where(x => x.NameResource.Contains(line) || x.TypeResource.Contains(line) || x.DescriptionResource.Contains(line)).ToList());
        }

        public ObservableCollection<Resources> FilterByString(string line)
        {
            return new ObservableCollection<Resources>(
                resources.Where(x => !x.NameResource.Contains(line) && !x.TypeResource.Contains(line) || !x.DescriptionResource.Contains(line)).ToList());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand(obj =>
                    {
                        Resources resources = obj as Resources;
                        this.resources.Insert(0, resources);
                        Selected = resources;

                        using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                        {
                            try
                            {
                                ApplicationContext.Instance.Resources.Add(resources);
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
                        Resources resource = obj as Resources;
                        if (resource != null)
                        {
                            resources.Remove(resource);
                            using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                            {
                                try
                                {
                                    ApplicationContext.Instance.Resources.Remove(resource);
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
                    }, (obj) => resources.Count > 0 && Selected != null
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
                        Resources resource = obj as Resources;
                        if (resource != null)
                        {
                            using (var transaction = ApplicationContext.Instance.Database.BeginTransaction())
                            {
                                try
                                {
                                    Selected = ApplicationContext.Instance.Resources.First(x => x.IdResource == Selected.IdResource);

                                    Selected.NameResource = resource.NameResource;
                                    Selected.TypeResource = resource.TypeResource;
                                    Selected.DescriptionResource = resource.DescriptionResource;

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
