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
    public partial class InvoicesPage : Window
    {
        public InvoicesPage(int personId)
        {
            InitializeComponent();

            DataContext = new InvoicesViewModel(personId);
            (DataContext as InvoicesViewModel).Page = this;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    e.Handled = true;
                    return;
                }
            }

            TextBox textBox = sender as TextBox;
            string text = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            if (text.Count(x => x == '.') > 1)
            {
                e.Handled = true;
                return;
            }
        }
    }
}
