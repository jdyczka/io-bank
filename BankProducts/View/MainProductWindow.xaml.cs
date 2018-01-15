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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bank.DataAccess;
using Bank.DataAccess.Repositories;
using Bank.Entities;
using Bank.Entities.Enums;

namespace BankProducts.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainProductWindow : Window
    {
        private ClientRepository repository = null;
        private Card card = null;
        private Account account = null;
        private Client client = null;
        private List<String> listaWalut = new List<String>();
        private List<String> typyKont = new List<String>();

        public MainProductWindow(ClientRepository repository, Client client)
        {
            InitializeComponent();
            this.repository = repository;
            this.client = client;
        }
        
        public void LoadDetailsClient(Client clientDetails)
        { 
            FirstName.Content = clientDetails.FirstName;
            LastName.Content = clientDetails.LastName;
            Email.Content = clientDetails.Email;
            Pesel.Content = clientDetails.Pesel;
        }

        private void CancelProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz anulować?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
                Close();
            }
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccountToClientWindow addAccountToClientWindow = new AddAccountToClientWindow();       
            if(addAccountToClientWindow.ShowDialog() == true)
            {
                listaWalut.Add("Polski złoty");
                listaWalut.Add("Euro");
                listaWalut.Add("Dolar amerykański");
                addAccountToClientWindow.WalutaText.ItemsSource = listaWalut;

                typyKont.Add("Regularne");
                typyKont.Add("Złote");
                typyKont.Add("Platynowe");
                addAccountToClientWindow.TypText.ItemsSource = typyKont;


                float rate = float.Parse(addAccountToClientWindow.OprocentowanieText.Text);
                Currency currency;
                AccountType accountType;
                if ((string)addAccountToClientWindow.WalutaText.SelectedItem == "Polski złoty")
                {
                    currency = Currency.PLN;
                }
                else if ((string)addAccountToClientWindow.WalutaText.SelectedItem == "Euro")
                {
                    currency = Currency.EUR;
                }
                else
                    currency = Currency.USD;

                if ((string)addAccountToClientWindow.TypText.SelectedItem == "Regularne")
                {
                    accountType = AccountType.Regular;
                }
                else if ((string)addAccountToClientWindow.TypText.SelectedItem == "Złote")
                {
                    accountType = AccountType.Gold;
                }
                else
                    accountType = AccountType.Platinum;

                account = new Account(addAccountToClientWindow.NazwaKontaText.Text, currency, accountType, rate);
            }
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            AddCardToClientWindow addCardToClientWindow = new AddCardToClientWindow();
            if(addCardToClientWindow.ShowDialog() == true)
            {
                var lista = repository.getClientAccounts(client.Id);
                List<Account> listaKontKlienta = (List<Account>)lista;
                List<String> listaNazwKontKlienta = new List<string>();
                foreach(Account konto in listaKontKlienta)
                {
                    listaNazwKontKlienta.Add(konto.Name);
                }
                addCardToClientWindow.NumerKontaText.ItemsSource = listaNazwKontKlienta;
                card = new Card(client.Id, addCardToClientWindow.NumerKontaText.SelectedItem.ToString(), addCardToClientWindow.PINText.ToString());
            }
        }

        private void AddLoan_Click(object sender, RoutedEventArgs e)
        {
            AddLoanToClientWindow addLoanToClientWindow = new AddLoanToClientWindow();
            if (addLoanToClientWindow.ShowDialog() == true)
            {

            }
        }
    }
}
