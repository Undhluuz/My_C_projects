using SimpleAccounting.DataAccess;
using SimpleAccounting.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SimpleAccounting.ViewModels
{
    public class AddOperationViewModel : INotifyPropertyChanged
    {
        private DateTime _date;
        private decimal _amount;
        private int _operationType;
        private int _counterpartyId;
        private int _categoryId;
        private string _description;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public int OperationType
        {
            get { return _operationType; }
            set
            {
                _operationType = value;
                OnPropertyChanged(nameof(OperationType));
            }
        }

        public int CounterpartyId
        {
            get { return _counterpartyId; }
            set
            {
                _counterpartyId = value;
                OnPropertyChanged(nameof(CounterpartyId));
            }
        }

        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ObservableCollection<string> OperationTypes { get; set; } = new ObservableCollection<string> { "Income", "Expense" };
        public ObservableCollection<Counterparty> Counterparties { get; set; }
        public ObservableCollection<IncomeExpenseCategory> Categories { get; set; }

        public ICommand AddOperationCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public AddOperationViewModel()
        {
            AddOperationCommand = new RelayCommand(AddOperation);
            CancelCommand = new RelayCommand(Cancel);

            // Load data
            using (var db = new AppDbContext())
            {
                Counterparties = new ObservableCollection<Counterparty>(db.Counterparties.ToList());
                Categories = new ObservableCollection<IncomeExpenseCategory>(db.IncomeExpenseCategories.ToList());
            }

            // Set default values
            Date = DateTime.Now;
            OperationType = 1; // Income
        }

        private void AddOperation(object parameter)
        {
            // TODO: Implement adding operation to database
            using (var db = new AppDbContext())
            {
                var newOperation = new Operation
                {
                    Date = Date,
                    Amount = Amount,
                    OperationType = OperationType,
                    CounterpartyId = CounterpartyId,
                    CategoryId = CategoryId,
                    Description = Description
                };

                db.Operations.Add(newOperation);
                db.SaveChanges();
            }

    // Close the window
    (parameter as System.Windows.Window).Close();
        }

        private void Cancel(object parameter)
        {
            // Close the window
            (parameter as System.Windows.Window).Close();
        }

      

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}