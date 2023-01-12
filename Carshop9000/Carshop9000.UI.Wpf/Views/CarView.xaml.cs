using Carshop9000.Model.Contracts;
using Carshop9000.UI.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Carshop9000.UI.Wpf.Views
{
    /// <summary>
    /// Interaction logic for CarView.xaml
    /// </summary>
    public partial class CarView : UserControl
    {
        public CarView()
        {
            this.DataContext = App.Current.Services.GetService<CarViewModel>();
            InitializeComponent();
        }
    }
}
