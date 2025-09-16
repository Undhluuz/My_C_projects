using SimpleAccounting.ViewModels;
using System.Windows;

namespace SimpleAccounting
{
    public partial class AddCounterpartyWindow : Window
    {
        public AddCounterpartyWindow(AddCounterpartyViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Window = this; // Сохраняем ссылку на окно
        }

        private void Button_Click()
        {

        }
    }
}