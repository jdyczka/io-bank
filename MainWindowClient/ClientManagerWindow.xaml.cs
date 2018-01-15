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
using Bank.DataAccess;
using Bank.DataAccess.Repositories;
using Bank.Entities;
using System.Data.Entity;
using Bank.Entities.Enums;
using IOMail;

using BankProducts;
using BankProducts.View;

using Logowanie;



namespace Bank.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClientManagerWindow : Window
    {

        BankContext bankContext = null;
        ClientRepository repository = null;

        public Button AdminPanelProperty
        {
            get { return AdminPanel; }
            set { AdminPanel = value; }
        }

        EmployeeRepository employeeRepository;

        public EmployeeRepository employeeRepositoryProperty
        {
            get { return employeeRepository; }
            set { employeeRepository = value; }
        }


        public ClientManagerWindow()
        {
            InitializeComponent();

            //inicjalizacja bazy
            Database.SetInitializer(new BankDBInitializer());
            bankContext = new BankContext();
            repository = new ClientRepository(bankContext);

            DetailsClient.IsEnabled = false;
            EditClient.IsEnabled = false;
            ClientDataGrid.ItemsSource = repository.getClientList();

            ClientDataGrid.AutoGenerateColumns = false;

        }
        public ClientManagerWindow(ClientRepository clientRepository)
        {
            InitializeComponent();


            AdminPanel.IsEnabled = false;
            DetailsClient.IsEnabled = false;
            EditClient.IsEnabled = false;

            ClientDataGrid.AutoGenerateColumns = false;
            this.repository = clientRepository;
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
                SendingEmailByTemplate(client, bankContext, "Witamy w IOBank", "usercreate");
                ClientDataGrid.ItemsSource = null;
                ClientDataGrid.AutoGenerateColumns = false;
                ClientDataGrid.ItemsSource = repository.getClientList();
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
                SendingEmailWrittenManually(editedClient, bankContext, "IOBank: zmiana Danych", "Witaj @Name @Surname! Twoje dane personalne zostaly zmienione");

                ClientDataGrid.ItemsSource = null;
                ClientDataGrid.AutoGenerateColumns = false;
                ClientDataGrid.ItemsSource = repository.getClientList();
            }

        }

        private void AddProducts_Click(object sender, RoutedEventArgs e)
        {
            Client editedClient = null;
            foreach (Client client in repository.getClientList())
            {
                if (ClientDataGrid.SelectedItem == client)
                {
                    editedClient = client;
                }
            }
            MainProductWindow mainProductWindow = new MainProductWindow(repository, editedClient);
            
            if (mainProductWindow.ShowDialog() == true)
            {
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
                DetailsClient.IsEnabled = true;
                EditClient.IsEnabled = true;
            }
            else
            {
                DetailsClient.IsEnabled = false;
                EditClient.IsEnabled = false;
            }
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz przejść do panelu administratora?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                EmployeeManagerWindow employeeManagerWindow = new EmployeeManagerWindow();
                employeeManagerWindow.repositoryProperty = employeeRepository;
                employeeManagerWindow.EmployeeDataGridProperty.ItemsSource = employeeRepository.getEmployeeList();
                if (employeeManagerWindow.ShowDialog() == true)
                {
                    employeeManagerWindow.Close();
                }
            }

        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var clientListFiltered = from Client client in repository.getClientList()
                                     let foundClients = client
                                     where
                                        foundClients.GetSearchTags().Contains(searchTextBox.Text)
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
                editedClient.LastName = editClientWindow.LastNameBox.Text;

            if (editClientWindow.EmailBox.Text != editedClient.Email)
                editedClient.Email = editClientWindow.EmailBox.Text;
            if (editClientWindow.PeselBox.Text != editedClient.Pesel)
                editedClient.Pesel = editClientWindow.PeselBox.Text;

            if (editClientWindow.CountryBox.Text != editedClient.Address.Country)
                editedClient.Address.Country = editClientWindow.CountryBox.Text;
            if (editClientWindow.CityBox.Text != editedClient.Address.City)
                editedClient.Address.City = editClientWindow.CityBox.Text;
            if (editClientWindow.PostalCodeBox.Text != editedClient.Address.PostalCode)
                editedClient.Address.PostalCode = editClientWindow.PostalCodeBox.Text;
            if (editClientWindow.StreetBox.Text != editedClient.Address.Street)
                editedClient.Address.Street = editClientWindow.StreetBox.Text;
            if (editClientWindow.BuildingNumberBox.Text != editedClient.Address.BuildingNr)
                editedClient.Address.BuildingNr = editClientWindow.BuildingNumberBox.Text;
            if (editClientWindow.ApartamentNumberBox.Text != editedClient.Address.AppartmentNr)
                editedClient.Address.AppartmentNr = editClientWindow.ApartamentNumberBox.Text;
        }
        private static void SendingEmailByTemplate(Client client, BankContext context, string subject, string templateName)
        {
            int clientId = client.Id;
            string ourAddress = "ioproject2017pl@gmail.com";

            var email = Email.From(context, ourAddress) // tutaj podajemy naszego maila - moze być na sztywno
                .To(clientId)     // Tutaj podajemy id clienta do którego wysyłamy maila
                .Subject(subject) // Tutaj podajemy tytul maila
                                  //.Body("Library Test Body")
                .UseTemplate(templateName, new { Name = client.FirstName, Surname = client.LastName }) // W pliku ConsoleApp/bin/Debug/szablony.txt są zawarte szablony maili
                .Send();

            Console.WriteLine(email.Data.Body);
        }

        private static void SendingEmailWrittenManually(Client client, BankContext context, string subject, string text)
        {
            int clientId = client.Id;
            string ourAddress = "ioproject2017pl@gmail.com";

            var email = Email.From(context, ourAddress)
                .To(clientId)
                .Subject(subject)
                .Body(text) // Tutaj podajemy tresc wysylanego maila
                            //.UseTemplate(templateName, new { Name = client.FirstName, Surname = client.LastName })
                .Send();

            Console.WriteLine(email.Data.Body);
        }
    }
}