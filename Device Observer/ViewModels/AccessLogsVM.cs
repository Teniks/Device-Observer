using Device_Observer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Device_Observer.ViewModels
{
    internal class AccessLogsVM : IViewModel, INotifyPropertyChanged
    {
        private AccessLogs selected;
        public AccessLogs Selected { get { return selected; } set { selected = value; } }

        public ObservableCollection<AccessLogs> accessLogs { get; private set; }

        public AccessLogsVM()
        {
            UpdateCollection();
        }

        public ObservableCollection<AccessLogs> SearchByString(string line)
        {
            return new ObservableCollection<AccessLogs>(
                accessLogs.Where(x => x.Details.Contains(line) || x.TimeStampLog.ToString().Contains(line)).ToList());
        }

        public ObservableCollection<AccessLogs> FilterByString(string line)
        {
            return new ObservableCollection<AccessLogs>(
                accessLogs.Where(x => !x.Details.Contains(line) && !x.TimeStampLog.ToString().Contains(line)).ToList());
        }

        public void UpdateCollection()
        {
            ApplicationContext.Instance.AccessLogs?.Load();
            accessLogs = ApplicationContext.Instance.AccessLogs?.Local;
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
