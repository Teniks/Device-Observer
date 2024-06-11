using Device_Observer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Device_Observer.ViewModels
{
    internal class ListOfChangesVM : IViewModel, INotifyPropertyChanged
    {
        private ListOfChanges selected;
        public ListOfChanges Selected { get { return selected; } set { selected = value; } }

        public ObservableCollection<ListOfChanges> listOfChanges { get; private set; }

        public ListOfChangesVM()
        {
            UpdateCollection();
        }

        public void UpdateCollection()
        {
            ApplicationContext.Instance.ListOfChanges?.Load();
            listOfChanges = ApplicationContext.Instance.ListOfChanges?.Local;
        }

        public ObservableCollection<ListOfChanges> SearchByString(string line)
        {
            return new ObservableCollection<ListOfChanges>(
                listOfChanges.Where(x => x.DateChanges.ToString().Contains(line) || x.Details.Contains(line)).ToList());
        }

        public ObservableCollection<ListOfChanges> FilterByString(string line)
        {
            return new ObservableCollection<ListOfChanges>(
                listOfChanges.Where(x => !x.DateChanges.ToString().Contains(line) && !x.Details.Contains(line)).ToList());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new RelayCommand(obj =>
                    {

                    }, (obj) => false
                    ));
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

                    }, (obj) => false
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

                    }, (obj) => false
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
