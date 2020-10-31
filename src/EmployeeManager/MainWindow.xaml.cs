using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using EmployeeManager.Data;
using GorestClient;
using GorestClient.Data;

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
            var clientConfig = ConfigurationSettings.GetConfig("GorestClient") as ClientConfig;
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
    }
}
