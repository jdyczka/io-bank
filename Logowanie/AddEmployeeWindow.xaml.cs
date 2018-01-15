using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bank.DataAccess.Repositories;
using Bank.Entities;

namespace Logowanie
{
    /// <summary>
    /// Logika interakcji dla klasy AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        //private List<Employee> employeeList = new List<Employee>();
        //public List<Employee> employeeListProperty
        //{
        //    get { return employeeList; }
        //    set { employeeList = value; }
        //}

        private EmployeeRepository repository;
        public EmployeeRepository repositoryProperty
        {
            get { return repository; }
            set { repository = value; }
        }

        public string nameProperty
        {
            get { return nameTextBox.Text; }
        }

        public string surnnameProperty
        {
            get { return surnameTextBox.Text; }
        }


        public long peselProperty
        {
            get { return Int64.Parse(peselTextBox.Text); }
        }

        public string emailProperty
        {
            get { return emailTextBox.Text; }
        }

        public string passwordProperty
        {
            get { return passwordBox.Password; }
        }

        public string countryProperty
        {
            get { return countryTextBox.Text; }
        }

        public string cityProperty
        {
            get { return cityTextBox.Text; }
        }

        public string postalCodeProperty
        {
            get { return postalCodeTextBox.Text; }
        }

        public string streetProperty
        {
            get { return streetTextBox.Text; }
        }

        public string buildingNrProperty
        {
            get { return buildingNrTextBox.Text; }
        }

        public string apartmentNrProperty
        {
            get { return apartmentNrTextBox.Text; }
        }

        public AddEmployeeWindow()
        {
            InitializeComponent();
            nameTextBox.MaxLength = 30;
            surnameTextBox.MaxLength = 30;
            peselTextBox.MaxLength = 11;
            emailTextBox.MaxLength = 20;
            passwordBox.MaxLength = 30;
        }

        private bool CheckLogin()
        {
            foreach (Employee employee in repository.getEmployeeList())
            {
                if (employee.Email == emailTextBox.Text)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckPesel()
        {
            foreach (Employee employee in repository.getEmployeeList())
            {
                if (employee.Pesel == peselTextBox.Text)
                {
                    return false;
                }
            }
            return true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.GetLineLength(0) <= 2)
                MessageBox.Show("Imię musi składać się z conajmniej 3 znaków", "Błąd", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else if (surnameTextBox.GetLineLength(0) <= 2)
                MessageBox.Show("Nazwisko musi składać się z conajmniej 3 znaków", "Błąd", MessageBoxButton.OK,
                    MessageBoxImage.Error); 
            else if (peselTextBox.GetLineLength(0) != 11)
                MessageBox.Show("Pesel musi składać się z dokładnie 11 znaków", "Błąd", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else if (emailTextBox.GetLineLength(0) <= 4)
                MessageBox.Show("Email musi składać się z conajmniej 5 znaków", "Błąd", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else if (passwordBox.Password.Length <= 7)
                MessageBox.Show("Hasło musi składać się z conajmniej 8 znaków", "Błąd", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else if (!CheckPesel()) MessageBox.Show("Użytkownik o podanym numerze pesel już istnieje", "Błąd", MessageBoxButton.OK,
                MessageBoxImage.Error);
            else if (!CheckLogin()) MessageBox.Show("Użytkownik o podanym emailu już istnieje", "Błąd", MessageBoxButton.OK,
                MessageBoxImage.Error);
            else
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz dodać pracownika?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DialogResult = true;
                    Close();
                }
            }
        }

        private void nameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-żA-Ż]"))
            {
                e.Handled = true;
            }
        }

        private void surnameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-żA-Ż]"))
            {
                e.Handled = true;
            }
        }

        private void phoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void peselTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void peselTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && peselTextBox.IsFocused)
            {
                e.Handled = true;
            }
        }

        private void nameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && nameTextBox.IsFocused)
            {
                e.Handled = true;
            }
        }

        private void surnameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && surnameTextBox.IsFocused)
            {
                e.Handled = true;
            }
        }


        private void emailTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && emailTextBox.IsFocused)
            {
                e.Handled = true;
            }
        }

        private void passwordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && passwordBox.IsFocused)
            {
                e.Handled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                addButton_Click(sender, e);
            }
        }

    }
}
