using System;
using System.Collections.Generic;
using System.Windows;
using SimpleAccounting.DataAccess.Models;
using SimpleAccounting.ViewModels;

namespace SimpleAccounting
{
    public partial class AddOperationWindow : Window
    {
        public Operation NewOperation { get; set; }
        public List<IncomeExpenseCategory> Categories { get; set; }

        public AddOperationWindow(AddOperationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(AmountTextBox.Text, out double amount) && CategoryComboBox.SelectedItem is IncomeExpenseCategory category)
            {
                NewOperation = new Operation
                {
                    Amount = (decimal)amount,
                    CategoryId = category.Id,
                    Date = DateTime.Now
                };
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter valid amount and select category.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}