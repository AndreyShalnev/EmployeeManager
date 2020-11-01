using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using EmployeeManager.Data;
using GorestClient;

namespace EmployeeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserClient Client;

        public MainWindow()
        {
            var clientConfig = ConfigurationManager.GetSection("GorestClient") as ClientConfig;
            Client = new UserClient(clientConfig); 

            InitializeComponent();
        }

        private void EmployeeGrid_OnInitialized(object sender, EventArgs e)
        {
            EmployeeGrid.DataContext = Client.GetAll(1).Data;
        }

        private void NameFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            EmployeeGrid.DataContext = Client.GetByName(NameFilter.Text).Data;
        }

        private void AddEmployee_ButtonClicked(object sender, RoutedEventArgs e)
        {
            var childWindow = new EditEmployeeWindow(Client, new User()) {Owner = this};
            childWindow.Closed += new EventHandler(EditEmployeeWindow_Closed);
            childWindow.Show();
        }

        private void EditEmployee_ButtonClicked(object sender, RoutedEventArgs e)
        {
            var selectedItem = EmployeeGrid.SelectedItem as User;
            var childWindow = new EditEmployeeWindow(Client, selectedItem) {Owner = this};
            childWindow.Closed += new EventHandler(EditEmployeeWindow_Closed);
            childWindow.Show();
        }

        private void EditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            EmployeeGrid.DataContext = Client.GetByName(NameFilter.Text).Data;
        }

        private void DeleteEmployee_ButtonClicked(object sender, RoutedEventArgs e)
        {
            if (EmployeeGrid.SelectedItem is User selectedItem)
                MessageBox.Show(selectedItem.Name + " was deleted");
        }

        private void EmployeeGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditButton.IsEnabled = EmployeeGrid.SelectedItem is User;
            DeleteButton.IsEnabled = EmployeeGrid.SelectedItem is User;
        }
    }
}
