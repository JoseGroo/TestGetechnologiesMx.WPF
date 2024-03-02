using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TestGetechnologiesMx.Repository.Entities;
using TestGetechnologiesMx.WPF.Logic;

namespace TestGetechnologiesMx.WPF.ViewModels
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        public Person Person { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private PersonLogic personLogic = new PersonLogic();

        public AddPersonPage Page { get; set; }

        public AddPersonViewModel()
        {
            SaveCommand = new RelayCommand(save);
            CancelCommand = new RelayCommand(cancel);
            Person = new Person();
        }

        private void save(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Person.FirstName))
            {
                MessageBox.Show("Nombre es requerido.", "Notificación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Person.PaternalSurname))
            {
                MessageBox.Show("Apellido paterno es requerido.", "Notificación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Person.Identification))
            {
                MessageBox.Show("Identificación es requerido.", "Notificación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool saved = personLogic.Save(Person);
            if (saved)
            {
                Page.Close();
                MessageBox.Show("Se guardo correctamente la persona", "Notificación", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string error = personLogic.LastError ?? "Ocurrio un error al intentar guardar la persona.";
                MessageBox.Show(error, "Notificación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancel(object parameter)
        {
            Page.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
