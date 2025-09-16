using SimpleAccounting.DataAccess;
using SimpleAccounting.DataAccess.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;

namespace SimpleAccounting.ViewModels
{
    public class AddCounterpartyViewModel : INotifyPropertyChanged
    {
        private Counterparty _newCounterparty = new Counterparty();
        public ObservableCollection<Operation> Operations { get; set; } = new ObservableCollection<Operation>();
        public Window Window { get; set; }
        public Counterparty NewCounterparty
        {
            get { return _newCounterparty; }
            set
            {
                _newCounterparty = value;
                OnPropertyChanged(nameof(NewCounterparty));
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand AddOperationCommand { get; private set; } // Команда для добавления операции

        public AddCounterpartyViewModel(Counterparty counterparty)
        {
            NewCounterparty = counterparty;
            AddCommand = new RelayCommand(Add);
            CancelCommand = new RelayCommand(Cancel);
            AddOperationCommand = new RelayCommand(AddOperation); // Initialize the command

            LoadOperations();
        }

        private void LoadOperations()
        {
          //  // Load operations from the database
            //using (var db = new AppDbContext())
            //{
              //  var operations = db.Operations.Where(o => o.CounterpartyId == NewCounterparty.Id).ToList();
                //foreach (var operation in operations)
                //{
                 //   Operations.Add(operation);
                //}
            //}
        }

        private void Add(object parameter)
        {
            if (!string.IsNullOrEmpty(NewCounterparty.Name) && !string.IsNullOrEmpty(NewCounterparty.TaxId))
            {
                // Сохраняем в базу данных
                using (var db = new AppDbContext())
                {
                    if (NewCounterparty.Id == 0)
                    {
                        // New Counterparty
                        db.Counterparties.Add(NewCounterparty);
                    }
                    else
                    {
                        // Update Counterparty
                        db.Entry(NewCounterparty).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                }

                if (parameter is System.Windows.Window window)
                {
                    // Закрываем окно
                    window.DialogResult = true;
                    window.Close();
                }

            }
        }

        private void Cancel(object parameter)
        {
            if (Window != null)
            {
                Window.DialogResult = false;
                Window.Close();
            }
        }

        private void AddOperation(object parameter)
        {
            // TODO: Open AddOperationWindow
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}