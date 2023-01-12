using Carshop9000.Data.EfCore;
using Carshop9000.Model.Contracts;
using Carshop9000.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Carshop9000.UI.Wpf.ViewModels
{
    public class CarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Car> CarList { get; set; }


        public Car SelecteCar
        {
            get => selecteCar; 
            set
            {
                selecteCar = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(SelecteCar)));
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Hp)));
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

        string conString = "Server=(localdb)\\mssqllocaldb;Database=Carshop9000_Test;Trusted_Connection=true";

        IRepository _repo;
        private Car selecteCar;
        public SaveCommand SaveCommand { get; set; }

        public CarViewModel()
        {
            _repo = new EfRepository(conString);

            CarList = new List<Car>(_repo.Query<Car>().ToList());

            SaveCommand = new SaveCommand(_repo);
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
