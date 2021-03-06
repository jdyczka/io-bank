﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using Bank.DataAccess;
using Bank.DataAccess.Repositories;
using Bank.Entities;

namespace Logowanie
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private EmployeeRepository repository;
        //private List<Employee> employeeList = new List<Employee>();
        //public List<Employee> employeeListProperty
        //{
        //    get { return employeeList; }
        //    set { employeeList = value; }
        //}

        private string nameAndSurname;

        public LoginWindow()
        {
            InitializeComponent();
            var context = new BankContext();
            repository = new EmployeeRepository(context);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckLoginAndPassword())
            {
                EmployeeManagerWindow employeeManagerWindow = new EmployeeManagerWindow();
                employeeManagerWindow.Title = "Zalogowano jako " + nameAndSurname;
                employeeManagerWindow.repositoryProperty = repository;
                employeeManagerWindow.employeeDataGrid.ItemsSource = repository.getEmployeeList();
                employeeManagerWindow.Show();
                Close();
            }
        }

        private bool CheckLoginAndPassword()
        {
            foreach (Employee employee in repository.getEmployeeList())
            {
                if (employee.Email == loginTextBox.Text && employee.Password == passwordBox.Password)
                {
                    if (employee.IsSuspended)
                    {
                        MessageBox.Show("Użytkownik jest zawieszony", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    nameAndSurname += employee.FirstName + " " + employee.LastName;
                    return true;
                }
            }
            MessageBox.Show("Wprowadzone dane są nieprawidłowe", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz wyjść z programu?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginButton_Click(sender, e);
            }
        }
    }
}
