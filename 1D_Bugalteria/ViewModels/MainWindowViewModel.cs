using SimpleAccounting.DataAccess;
using SimpleAccounting.DataAccess.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace SimpleAccounting.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Counterparty> _counterparties;
        private Counterparty _selectedCounterparty;
        private string _newCounterpartyName;
        private string _newCounterpartyTaxId;

        public ObservableCollection<Counterparty> Counterparties
        {
            get { return _counterparties; }
            set
            {
                _counterparties = value;
                OnPropertyChanged();
            }
        }

        public Counterparty SelectedCounterparty
        {
            get { return _selectedCounterparty; }
            set
            {
                _selectedCounterparty = value;
                OnPropertyChanged();
            }
        }

        public string NewCounterpartyName
        {
            get { return _newCounterpartyName; }
            set
            {
                _newCounterpartyName = value;
                OnPropertyChanged();
            }
        }

        public string NewCounterpartyTaxId
        {
            get { return _newCounterpartyTaxId; }
            set
            {
                _newCounterpartyTaxId = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCounterpartyCommand { get; private set; }
        public ICommand DeleteCounterpartyCommand { get; private set; }
        public ICommand ShowDetailsCommand { get; private set; }

        public MainWindowViewModel()
        {
            AddCounterpartyCommand = new RelayCommand(AddCounterparty, CanAddCounterparty);
            DeleteCounterpartyCommand = new RelayCommand(DeleteCounterparty, CanDeleteCounterparty);
            ShowDetailsCommand = new RelayCommand(ShowDetails, CanShowDetails);

            LoadCounterparties();
        }

        private void LoadCounterparties()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    Counterparties = new ObservableCollection<Counterparty>(db.Counterparties.ToList());
                }
                OnPropertyChanged("Counterparties");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }


        private bool CanAddCounterparty(object parameter)
        {
            return !string.IsNullOrEmpty(NewCounterpartyName) && !string.IsNullOrEmpty(NewCounterpartyTaxId);
        }

        private void AddCounterparty(object parameter)
        {
            var addCounterpartyWindow = new AddCounterpartyWindow(new AddCounterpartyViewModel(new Counterparty()));
            bool? result = addCounterpartyWindow.ShowDialog();
            if (result == true)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LoadCounterparties();
                });
            }
        }

        private bool CanDeleteCounterparty(object parameter)
        {
            return SelectedCounterparty != null;
        }

        private void DeleteCounterparty(object parameter)
        {
            if (SelectedCounterparty == null)
            {
                return; // Ничего не делаем, если ничего не выбрано
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    // Получаем объект из текущего контекста
                    var counterpartyToDelete = db.Counterparties.FirstOrDefault(c => c.Id == SelectedCounterparty.Id);

                    if (counterpartyToDelete != null)
                    {
                        db.Counterparties.Remove(counterpartyToDelete);
                        db.SaveChanges();
                        Counterparties.Remove(SelectedCounterparty); // Удаляем из коллекции
                        SelectedCounterparty = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении контрагента: {ex.Message}");
            }
        }

        private bool CanShowDetails(object parameter)
        {
            return SelectedCounterparty != null;
        }

        private void ShowDetails(object parameter)
        {
            if (SelectedCounterparty != null)
            {
                var addCounterpartyWindow = new AddCounterpartyWindow(new AddCounterpartyViewModel(SelectedCounterparty));
                addCounterpartyWindow.ShowDialog();
                LoadCounterparties();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}