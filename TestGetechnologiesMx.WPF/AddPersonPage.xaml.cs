using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestGetechnologiesMx.WPF.ViewModels;

namespace TestGetechnologiesMx.WPF
{
    /// <summary>
    /// Interaction logic for AddPersonPage.xaml
    /// </summary>
    public partial class AddPersonPage : Window
    {
        public AddPersonPage()
        {
            InitializeComponent();

            DataContext = new AddPersonViewModel();
            (DataContext as AddPersonViewModel).Page = this;
        }
    }
}
