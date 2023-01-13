using Carshop9000.Data.EfCore;
using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Carshop9000.UI.Wpf.ViewModels
{
    public class CarViewModel : ObservableObject
    {
        public ObservableCollection<Car> CarList { get; set; }

        public Car SelecteCar
        {
            get => selecteCar;
            set
            {
                selecteCar = value;
                OnPropertyChanged(nameof(SelecteCar));
                OnPropertyChanged(nameof(Hp));
            }
        }

        public double Hp
        {
            get
            {
                if (SelecteCar == null)
                    return 0;

                return SelecteCar.KW * 1.34;
            }
        }

        private Car selecteCar;
        private readonly IUnitOfWork _uow;

        public SaveCommand SaveCommand { get; set; }

        public ICommand SaveCoolCommand { get; set; }

        public ICommand NewCommand { get; set; }

        public CarViewModel(IUnitOfWork uow)
        {
            _uow = uow;

            CarList = new ObservableCollection<Car>(_uow.GetRepo<Car>().Query().ToList());

            SaveCommand = new SaveCommand(_uow);

            SaveCoolCommand = new RelayCommand(() => _uow.SaveAll());

            NewCommand = new RelayCommand(() =>
            {
                var newCar = new Car() { Model = "NEU" };
                CarList.Add(newCar);
                _uow.GetRepo<Car>().Add(newCar);
                SelecteCar = newCar;
            });
        }
    }

    public class SaveCommand : ICommand
    {
        private readonly IUnitOfWork _uow;

        public event EventHandler? CanExecuteChanged;

        public SaveCommand(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _uow.SaveAll();
        }
    }
}
