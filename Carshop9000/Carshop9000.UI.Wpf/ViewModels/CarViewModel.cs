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
        private readonly IRepository _repo;

        public SaveCommand SaveCommand { get; set; }

        public ICommand SaveCoolCommand { get; set; }

        public ICommand NewCommand { get; set; }

        public CarViewModel(IRepository repo)
        {
            this._repo = repo;

            CarList = new ObservableCollection<Car>(_repo.Query<Car>().ToList());

            SaveCommand = new SaveCommand(_repo);

            SaveCoolCommand = new RelayCommand(() => _repo.SaveAll());

            NewCommand = new RelayCommand(() =>
            {
                var newCar = new Car() { Model = "NEU" };
                CarList.Add(newCar);
                _repo.Add(newCar);
                SelecteCar = newCar;
            });
        }
    }

    public class SaveCommand : ICommand
    {
        private readonly IRepository repo;

        public event EventHandler? CanExecuteChanged;

        public SaveCommand(IRepository repo)
        {
            this.repo = repo;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            repo.SaveAll();
        }
    }
}
