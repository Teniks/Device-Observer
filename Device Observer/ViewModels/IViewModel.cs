using System;

namespace Device_Observer.ViewModels
{
    internal interface IViewModel
    {
        RelayCommand AddCommand { get; }
        RelayCommand RemoveCommand { get; }
        RelayCommand UpdateCommand { get; }
    }
}
