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
        private Card card = null;
        private Client clientDetails = null;
        private List<String> listaWalut = new List<String>();
        private List<String> typyKont = new List<String>();
        public IEnumerable<Account> lista;


        public Client ClientDetailsProperty {
            get { return clientDetails; }
            set { clientDetails = value; }
        }

        public MainProductWindow()
        {
            InitializeComponent();
        }

        public void LoadClientToEdit(Client client)
        {
            this.clientDetails = client;
            FirstName_Copy.Content = clientDetails.FirstName;
            LastName_Copy.Content = clientDetails.LastName;
            Email_Copy.Content = clientDetails.Email;
            Pesel_Copy.Content = clientDetails.Pesel;
        }

        private void CancelProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz anulować?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccountToClientWindow addAccountToClientWindow = new AddAccountToClientWindow();
            addAccountToClientWindow.Show();
            listaWalut.Add("Polski złoty");
            listaWalut.Add("Euro");
            listaWalut.Add("Dolar amerykański");
            addAccountToClientWindow.WalutaText.ItemsSource = listaWalut;

            typyKont.Add("Regularne");
            typyKont.Add("Złote");
            typyKont.Add("Platynowe");
            addAccountToClientWindow.TypText.ItemsSource = typyKont;
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            AddCardToClientWindow addCardToClientWindow = new AddCardToClientWindow();
            if (addCardToClientWindow.ShowDialog() == true)
            {
                List<Account> listaKontKlienta = (List<Account>)lista;
                List<String> listaNazwKontKlienta = new List<string>();
                foreach (Account konto in listaKontKlienta)
                {
                    listaNazwKontKlienta.Add(konto.Name);
                }
                addCardToClientWindow.NumerKontaText.ItemsSource = listaNazwKontKlienta;
                card = new Card(clientDetails.Id, addCardToClientWindow.NumerKontaText.SelectedItem.ToString(), addCardToClientWindow.PINText.ToString());
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
