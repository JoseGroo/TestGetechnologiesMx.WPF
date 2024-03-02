using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TestGetechnologiesMx.Repository.Entities;
using TestGetechnologiesMx.WPF.Logic;

namespace TestGetechnologiesMx.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged(nameof(People));
            }
        }
        public ICommand AddPersonCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowInvoicesCommand { get; set; }

        private PersonLogic personLogic = new PersonLogic();

        public MainViewModel()
        {
            retrievePeople();

            AddPersonCommand = new RelayCommand(addPerson);
            DeleteCommand = new RelayCommand(deletePerson);
            ShowInvoicesCommand = new RelayCommand(showInvoices);
        }

        private void addPerson(object parameter)
        {
            AddPersonPage addPersonPage = new AddPersonPage();
            addPersonPage.Closed += addPersonWindow_Closed;
            addPersonPage.ShowDialog();
        }

        private void showInvoices(object parameter)
        {
            InvoicesPage invoicesPage = new InvoicesPage((parameter as Person).Id);
            invoicesPage.ShowDialog();
        }

        private void deletePerson(object parameter)
        {
            if(parameter is Person)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int personId = (parameter as Person).Id;
                    bool deleted = personLogic.Delete(personId);
                    if (deleted)
                    {
                        People = new ObservableCollection<Person>(People.Where(r => r.Id != personId));
                        MessageBox.Show("Se elimino correctamente la persona", "Notificación", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al intentar eliminar la persona", "Notificación", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
        }

        private void addPersonWindow_Closed(object sender, EventArgs e)
        {
            retrievePeople();
        }

        private void retrievePeople()
        {
            var people = personLogic.GetList();
            if (people?.Any() == true)
                People = new ObservableCollection<Person>(people);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
        
    }

}
