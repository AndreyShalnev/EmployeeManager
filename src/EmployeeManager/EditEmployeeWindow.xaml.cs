using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EmployeeManager.Data;
using EmployeeManager.Data.Enums;
using GorestClient;

namespace EmployeeManager
{
    /// <summary>
    /// Interaction logic for EmployeeChangesWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public UserActivityStatus UserStatus { get; set; }

        private string name;
        private IUserClient Client;
        private User User;

        public EditEmployeeWindow(IUserClient client, User user)
        {
            InitializeComponent();

            User = user;
            Client = client;
            this.DataContext = this;

            GenderCombobox.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            GenderCombobox.SelectedIndex = 0;
            StatusCombobox.ItemsSource = Enum.GetValues(typeof(UserActivityStatus)).Cast<UserActivityStatus>();
            StatusCombobox.SelectedIndex = 0;

            InitValues();
        }

        private void InitValues()
        {
            EmployeeName = User.Name;
            Email = User.Email;
            Gender = User.Gender;
            UserStatus = User.Status;
            SubmitButton.IsEnabled = !string.IsNullOrEmpty(EmployeeName);
        }

        private void Submit_ButtonClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(EmployeeNameTextBox.Text))
            {
                MessageBox.Show("Please, fill all fields");
                return;
            }

            UpdateUserFields();

            var result = User.Id == 0 ? Client.Create(User) : Client.Update(User);
            var resText = result.IsSuccess ? "Operation successful" : "Operation failed: \n" + result.Message;

            MessageBox.Show(resText);
            if(result.IsSuccess)
                this.Close();
        }

        private void UpdateUserFields()
        {
            User.Name = EmployeeName;
            User.Email = Email;
            User.Gender = (Gender)GenderCombobox.SelectedItem;
            User.Status = (UserActivityStatus)StatusCombobox.SelectedItem;
        }

        private void Cancel_ButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckSubmitButtonState();
        }

        private void CheckSubmitButtonState()
        {
            if (!string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(Email))
                SubmitButton.IsEnabled = true;
        }

        private void Combobox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            CheckSubmitButtonState();
        }
    }
}
