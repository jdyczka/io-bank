using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bank.DataAccess.Repositories;
using Bank.Entities;

namespace Bank.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClientManagerWindow : Window
    {
        ClientRepository repository = null;
        public ClientManagerWindow()
        {
            InitializeComponent();

            RemoveClient.IsEnabled = false;
            AdminPanel.IsEnabled = false;
            DetailsClient.IsEnabled = false;
            EditClient.IsEnabled = false;

            ClientDataGrid.AutoGenerateColumns = false;

        }
        public void SetRepository(ClientRepository repository)
        {
            this.repository = repository;
            ClientDataGrid.ItemsSource = repository.getClientList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy chcesz wyjść z programu?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz się wylogować?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                /* LoginWindow loginWindow = new LoginWindow();
                 loginWindow.employeeListProperty = employeeListProperty;
                 loginWindow.Show();
                 Close();*/
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            if (addClientWindow.ShowDialog() == true)
            {
                Client client = new Client();
                FillClientInfo(addClientWindow, client);
                repository.addNewClient(client);
                ClientDataGrid.ItemsSource = null;
                ClientDataGrid.AutoGenerateColumns = false;
                ClientDataGrid.ItemsSource = repository.getClientList();


            }
        }

        private void RemoveClient_Click(object sender, RoutedEventArgs e)
        {
            foreach (Bank.Entities.Client client in repository.getClientList())
            {
                if (ClientDataGrid.SelectedItem == client)
                {
                    MessageBoxResult result = MessageBox.Show("Czy chcesz usunąć zaznaczonego klienta?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        //PO DODANIU USUWANIA KLIENTA Z BAZY ODKOMENTOWAC: 
                        //repository.DeleteClient(client);
                        ClientDataGrid.ItemsSource = null;
                        ClientDataGrid.ItemsSource = repository.getClientList();
                    }
                }
            }
        }

        private void DetailsClient_Click(object sender, RoutedEventArgs e)
        {
            DetailsClientWindow detailsClientWindow = new DetailsClientWindow();
            foreach (Client client in repository.getClientList())
            {
                if (ClientDataGrid.SelectedItem == client)
                    detailsClientWindow.ClientDetailsProperty = client;
            }
            detailsClientWindow.Show();
            detailsClientWindow.LoadDetailsClient();

        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            EditClientWindow editClientWindow = new EditClientWindow();
            Client editedClient = null;
            foreach (Client client in repository.getClientList())
            {
                if (ClientDataGrid.SelectedItem == client)
                {
                    editedClient = client;
                    editClientWindow.LoadClientToEdit(editedClient);
                }
            }
            if (editClientWindow.ShowDialog() == true)
            {
                EditClientInfo(editClientWindow, editedClient);
                repository.updateClient(editedClient);


                ClientDataGrid.ItemsSource = null;
                ClientDataGrid.AutoGenerateColumns = false;
                ClientDataGrid.ItemsSource = repository.getClientList();
            }

        }

        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientDataGrid.SelectedIndex != -1)
            {
                RemoveClient.IsEnabled = true;
                DetailsClient.IsEnabled = true;
                EditClient.IsEnabled = true;
            }
            else
            {
                RemoveClient.IsEnabled = false;
                DetailsClient.IsEnabled = false;
                EditClient.IsEnabled = false;
            }
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz przejść do panelu administratora?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                /*EmployeeManagerWindow employeeManagerWindow = new EmployeeManagerWindow();
                employeeManagerWindow.employeeListProperty = employeeListProperty;
                employeeManagerWindow.Show();
                Close();*/
            }

        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var clientListFiltered = from Client client in repository.getClientList()
                                     let foundClients = client
                                     where
                                         foundClients.Pesel.Contains(searchTextBox.Text) ||
                                         Regex.IsMatch(foundClients.FirstName, searchTextBox.Text, RegexOptions.IgnoreCase) ||
                                         Regex.IsMatch(foundClients.LastName, searchTextBox.Text, RegexOptions.IgnoreCase) ||
                                         Regex.IsMatch(foundClients.Email, searchTextBox.Text, RegexOptions.IgnoreCase)
                                     select client;
            ClientDataGrid.ItemsSource = null;
            ClientDataGrid.AutoGenerateColumns = false;
            ClientDataGrid.ItemsSource = clientListFiltered;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FillClientInfo(AddClientWindow addClientWindow, Client client)
        {
            client.FirstName = addClientWindow.FirstNameBox.Text;
            client.LastName = addClientWindow.LastNameBox.Text;
            client.Pesel = addClientWindow.PeselBox.Text;
            client.Email = addClientWindow.EmailBox.Text;
            client.Address = new Address();
            client.Address.Country = addClientWindow.CountryBox.Text;
            client.Address.City = addClientWindow.CityBox.Text;
            client.Address.PostalCode = addClientWindow.PostalCodeBox.Text;
            client.Address.Street = addClientWindow.StreetBox.Text;
            client.Address.BuildingNr = addClientWindow.BuildingNumberBox.Text;
            client.Address.AppartmentNr = addClientWindow.ApartamentNumberBox.Text;


        }
        private void EditClientInfo(EditClientWindow editClientWindow, Client editedClient)
        {
            if (editClientWindow.FirstNameBox.Text != editedClient.FirstName)
                editedClient.FirstName = editClientWindow.FirstNameBox.Text;
            if (editClientWindow.LastNameBox.Text != editedClient.LastName)
                editedClient.LastName= editClientWindow.LastNameBox.Text;

            if (editClientWindow.EmailBox.Text != editedClient.Email)
                editedClient.Email= editClientWindow.EmailBox.Text;
            if (editClientWindow.PeselBox.Text != editedClient.Pesel)
                editedClient.Pesel= editClientWindow.PeselBox.Text;

            if (editClientWindow.CountryBox.Text != editedClient.Address.Country)
                editedClient.Address.Country = editClientWindow.CountryBox.Text;
            if (editClientWindow.CityBox.Text != editedClient.Address.City)
                editedClient.Address.City = editClientWindow.CityBox.Text;
            if (editClientWindow.PostalCodeBox.Text != editedClient.Address.PostalCode)
                editedClient.Address.PostalCode = editClientWindow.PostalCodeBox.Text;
            if (editClientWindow.StreetBox.Text != editedClient.Address.Street)
                editedClient.Address.Street= editClientWindow.StreetBox.Text;
            if (editClientWindow.BuildingNumberBox.Text != editedClient.Address.BuildingNr)
                editedClient.Address.BuildingNr = editClientWindow.BuildingNumberBox.Text;
            if (editClientWindow.ApartamentNumberBox.Text != editedClient.Address.AppartmentNr)
                editedClient.Address.AppartmentNr = editClientWindow.ApartamentNumberBox.Text;
        }
    }



}