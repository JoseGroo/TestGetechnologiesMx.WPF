using Accessibility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TestGetechnologiesMx.Repository.Entities;
using TestGetechnologiesMx.WPF.Logic;
using TestGetechnologiesMx.WPF.Models;

namespace TestGetechnologiesMx.WPF.ViewModels
{
    public class InvoicesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Invoice> invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get { return invoices; }
            set
            {
                invoices = value;
                OnPropertyChanged(nameof(Invoices));
            }
        }
        private InvoiceModel invoice;
        public InvoiceModel Invoice
        {
            get { return invoice; }
            set
            {
                invoice = value;
                OnPropertyChanged(nameof(Invoice));
            }
        }
       
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private InvoiceLogic invoiceLogic = new InvoiceLogic();

        public InvoicesPage Page { get; set; }

        private int _personId;

        public InvoicesViewModel(int personId)
        {
            SaveCommand = new RelayCommand(save);
            CancelCommand = new RelayCommand(cancel);
            DeleteCommand = new RelayCommand(delete);

            Invoice = new InvoiceModel();
            Invoice.PersonId = personId;
            _personId = personId;

            retrieveInvoices();
        }

        private void delete(object parameter)
        {
            if (parameter is Invoice)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int invoiceId = (parameter as Invoice).Id;
                    bool deleted = invoiceLogic.Delete(invoiceId);
                    if (deleted)
                    {
                        Invoices = new ObservableCollection<Invoice>(Invoices.Where(r => r.Id != invoiceId));
                        MessageBox.Show("Se elimino correctamente la factura", "Notificación", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al intentar eliminar la factura", "Notificación", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
        }

        private void save(object parameter)
        {
            if (!Invoice.Date.HasValue)
            {
                MessageBox.Show("La fecha es obligatoria.", "Notificación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!Invoice.Amount.HasValue)
            {
                MessageBox.Show("El monto es obligatorio.", "Notificación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool saved = invoiceLogic.Save(Invoice);
            if (saved)
            {
                retrieveInvoices();
                MessageBox.Show("Se guardo correctamente la factura", "Notificación", MessageBoxButton.OK, MessageBoxImage.Information);
                Invoice = new InvoiceModel();
                Invoice.PersonId = _personId;
            }
            else
            {
                string error = invoiceLogic.LastError ?? "Ocurrio un error al intentar guardar la factuar.";
                MessageBox.Show(error, "Notificación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancel(object parameter)
        {
            Invoice = new InvoiceModel();
            Invoice.PersonId = _personId;
        }

        private void retrieveInvoices()
        {
            var invoices = invoiceLogic.GetList(_personId);
            if (invoices?.Any() == true)
                Invoices = new ObservableCollection<Invoice>(invoices);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
