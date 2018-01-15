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
using Bank.Entities.Enums;

namespace Logowanie
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeeManagerWindow.xaml
    /// </summary>
    public partial class EmployeeManagerWindow : Window
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


        public EmployeeManagerWindow()
        {
            InitializeComponent();
            removeEmployeeButton.IsEnabled = false;
            editEmployeeButton.IsEnabled = false;
            suspendEmployeeButton.IsEnabled = false;
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz się wylogować?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                //loginWindow.employeeListProperty = employeeListProperty;
                loginWindow.Show();
                Close();
            }
        }

        private void removeEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            //foreach (Employee employee in repository.getEmployeeList())
            //{
            //    if (employeeDataGrid.SelectedItem == employee)
            //    {
            //        MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć wybranego pracownika?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //        if (result == MessageBoxResult.Yes)
            //        {
            //            employeeList.Remove(employee);
            //            employeeDataGrid.ItemsSource = null;
            //            employeeDataGrid.ItemsSource = employeeList;
            //            break;
            //        }
            //    }
            //}
        }

        private void suspendEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Employee employee in repository.getEmployeeList())
            {
                if (employeeDataGrid.SelectedItem == employee && !employee.IsSuspended)
                {
                    MessageBoxResult result =
                        MessageBox.Show(
                            "Czy na pewno chcesz zawiesić wybranego pracownika?\nUniemożliwi mu to zalogowanie się do systemu.",
                            "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        employee.IsSuspended = true;
                        employeeDataGrid.ItemsSource = null;
                        employeeDataGrid.ItemsSource = repository.getEmployeeList();
                        break;
                    }
                }
                else if (employeeDataGrid.SelectedItem == employee && employee.IsSuspended)
                {
                    MessageBoxResult result =
                        MessageBox.Show(
                            "Czy na pewno chcesz cofnąć zawieszenie wybranego pracownika?",
                            "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        employee.IsSuspended = false;
                        employeeDataGrid.ItemsSource = null;
                        employeeDataGrid.ItemsSource = repository.getEmployeeList();
                        break;
                    }
                }
            }
        }

        private void addEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.repositoryProperty = repository;
            if (addEmployeeWindow.ShowDialog() == true)
            {
                Address address = new Address(addEmployeeWindow.countryProperty, addEmployeeWindow.cityProperty, addEmployeeWindow.postalCodeProperty, addEmployeeWindow.streetProperty, addEmployeeWindow.buildingNrProperty, addEmployeeWindow.apartmentNrProperty);
                Employee employee = new Employee(addEmployeeWindow.nameProperty, addEmployeeWindow.surnnameProperty, addEmployeeWindow.peselProperty.ToString(), addEmployeeWindow.emailProperty, 4, AuthLevel.Basic, addEmployeeWindow.passwordProperty);
                employee.AddressId = address.Id;
                employee.Address = address;

                repository.addNewEmployee(employee);


                employeeDataGrid.ItemsSource = null;
                employeeDataGrid.ItemsSource = repository.getEmployeeList();
            }
        }

        private void editEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow();
            editEmployeeWindow.repositoryProperty = repository;
            Employee editedEmployee = new Employee();
            foreach (Employee employee in repository.getEmployeeList())
            {
                if (employeeDataGrid.SelectedItem == employee)
                {                   
                    editEmployeeWindow.nameTextBox.Text = employee.FirstName;
                    editEmployeeWindow.surnameTextBox.Text = employee.LastName;
                    editEmployeeWindow.peselTextBox.Text = employee.Pesel;
                    editEmployeeWindow.emailTextBox.Text = employee.Email;
                    editEmployeeWindow.passwordBox.Password = employee.Password;
                    editEmployeeWindow.countryTextBox.Text = employee.Address.Country;
                    editEmployeeWindow.cityTextBox.Text = employee.Address.City;
                    editEmployeeWindow.postalCodeTextBox.Text = employee.Address.PostalCode;
                    editEmployeeWindow.streetTextBox.Text = employee.Address.Street;
                    editEmployeeWindow.buildingNrTextBox.Text = employee.Address.BuildingNr;
                    editEmployeeWindow.apartmentNrTextBox.Text = employee.Address.AppartmentNr;

                    editEmployeeWindow.employeeProperty = employee;
                    editedEmployee = employee;
                    break;
                }
            }

            if (editEmployeeWindow.ShowDialog() == true)
            {
                editedEmployee.FirstName = editEmployeeWindow.nameTextBox.Text;
                editedEmployee.LastName = editEmployeeWindow.surnameTextBox.Text;
                editedEmployee.Pesel = editEmployeeWindow.peselTextBox.Text;
                editedEmployee.Email = editEmployeeWindow.emailTextBox.Text;
                editedEmployee.Password = editEmployeeWindow.passwordBox.Password;
                Address address = new Address(editEmployeeWindow.countryTextBox.Text, editEmployeeWindow.cityTextBox.Text, editEmployeeWindow.postalCodeTextBox.Text, editEmployeeWindow.streetTextBox.Text, editEmployeeWindow.buildingNrTextBox.Text, editEmployeeWindow.apartmentNrTextBox.Text);
                editedEmployee.Address = address;

                repository.updateEmployee(editedEmployee);
                employeeDataGrid.ItemsSource = null;
                employeeDataGrid.ItemsSource = repository.getEmployeeList();
            }

        }

        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsWindow detailsWindow = new DetailsWindow();
            foreach (Employee employee in repository.getEmployeeList())
            {
                if (employeeDataGrid.SelectedItem == employee)
                {
                    detailsWindow.firstNameLabel.Content = employee.FirstName;
                    detailsWindow.lastNameLabel.Content = employee.LastName;
                    detailsWindow.peselLabel.Content = employee.Pesel;
                    detailsWindow.emailLabel.Content = employee.Email;
                    detailsWindow.countryLabel.Content = employee.Address.Country;
                    detailsWindow.cityLabel.Content = employee.Address.City;
                    detailsWindow.postalCodeLabel.Content = employee.Address.PostalCode;
                    detailsWindow.streetLabel.Content = employee.Address.Street;
                    detailsWindow.buildingNrLabel.Content = employee.Address.BuildingNr;
                    detailsWindow.apartmentNrLabel.Content = employee.Address.AppartmentNr;
                    detailsWindow.idLabel.Content = employee.Id;
                    detailsWindow.authLevelLabel.Content = employee.AuthLevel.ToString();
                    if (employee.IsSuspended)
                        detailsWindow.isSuspendedLabel.Content = "Tak";
                    else detailsWindow.isSuspendedLabel.Content = "Nie";
                    break;
                }
            }
            detailsWindow.Show();
        }

        private void employeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeeDataGrid.SelectedIndex != -1)
            {
                removeEmployeeButton.IsEnabled = true;
                editEmployeeButton.IsEnabled = true;
                suspendEmployeeButton.IsEnabled = true;
            }
            else
            {
                removeEmployeeButton.IsEnabled = false;
                editEmployeeButton.IsEnabled = false;
                suspendEmployeeButton.IsEnabled = false;
            }
        }

        private void surnameSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtOrig = surnameSearchTextBox.Text;
            string upper = txtOrig.ToUpper();
            string lower = txtOrig.ToLower();

            var employeeListFiltered = from Employee employee in repository.getEmployeeList()
                let employeeSurname = employee.LastName.ToLower()
                where
                    employeeSurname.StartsWith(lower)
                    || employeeSurname.StartsWith(upper)
                    || employeeSurname.Contains(txtOrig)
                select employee;

            employeeDataGrid.ItemsSource = employeeListFiltered;
        }

        private void peselSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtOrig = peselSearchTextBox.Text;

            var employeeListFiltered = from Employee employee in repository.getEmployeeList()
                let employeePesel = employee.Pesel.ToString()
                where
                    employeePesel.Contains(txtOrig)
                select employee;

            employeeDataGrid.ItemsSource = employeeListFiltered;
        }

    }

}
